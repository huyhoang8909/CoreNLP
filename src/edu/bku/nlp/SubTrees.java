package edu.bku.nlp;

import edu.stanford.nlp.io.FileSequentialCollection;
import edu.stanford.nlp.io.IOUtils;
import edu.stanford.nlp.ling.*;
import edu.stanford.nlp.pipeline.Annotation;
import edu.stanford.nlp.pipeline.StanfordCoreNLP;
import edu.stanford.nlp.trees.*;
import edu.stanford.nlp.util.*;
import edu.stanford.nlp.util.StringUtils;
import edu.stanford.nlp.util.logging.Redwood;

import java.io.*;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.sql.*;
import java.util.*;

import com.mysql.jdbc.Connection;

import edu.stanford.nlp.neural.rnn.RNNCoreAnnotations;
import edu.stanford.nlp.neural.rnn.RNNCoreAnnotations.PredictedClass;
import edu.stanford.nlp.sentiment.SentimentCoreAnnotations;


public class SubTrees {
	
	/** A logger for this class */
	private static Redwood.RedwoodChannels log = Redwood.channels(SubTrees.class);

	/** Setiment values */
	static class Sentiment {
	    public static final int VERY_NEGATIVE = 0;
	    public static final int NEGATIVE = 1;
	    public static final int NEUTRAL = 2;
	    public static final int POSITIVE = 3;
	    public static final int VERY_POSITIVE = 4;
	}
	
	/** DB configuration params */
    private static String url = "jdbc:mysql://192.168.33.10:3306/vietnamese_treebank";
    private static String username = "root";
    private static String password = "";
    private static Connection connection = null;
    
    private static int order = 1;
    
	public static void printSubTrees(Tree inputTree, ArrayList<Word> previousWords, 
			int sentence_id, PreparedStatement words_statement, 
			PreparedStatement sentences_words_statement) 
	{
        if (inputTree.isLeaf()) {
            return;
        }
        ArrayList<Word> words = new ArrayList<Word>();
        for (Tree leaf : inputTree.getLeaves()) {
            words.addAll(leaf.yieldWords());
        }
        CoreLabel label = (CoreLabel)inputTree.label();
        String cat = label.value();
        String sentence = "";
        int sentiment = Sentiment.NEUTRAL;

        if (cat.equals("ROOT") || (previousWords != null && !previousWords.equals(words))) {
            if((label.containsKey(PredictedClass.class))){
            	sentiment = RNNCoreAnnotations.getPredictedClass(inputTree);
            }
        
	        for (Word w : words) {
	        	sentence += w.word() + " ";
	        }
        	sentence = sentence.trim();
        	System.out.println(sentiment + " " + sentence);
        	try {
            	// insert words
        		words_statement.setString(1, sentence);
            	words_statement.setInt(2, sentiment);
				words_statement.executeUpdate();
                ResultSet rs = words_statement.getGeneratedKeys();
                int word_id = 1;
                if(rs.next())
                {
                    word_id = rs.getInt(1);
                }

				// insert sentences_words
				sentences_words_statement.setInt(1, sentence_id);
				sentences_words_statement.setInt(2, word_id);
				sentences_words_statement.setInt(3, order++);
				sentences_words_statement.setString(4, cat);
				sentences_words_statement.executeUpdate();
			} catch (SQLException e) {
				//log.info(sentiment + " " + sentence);
				//log.info(e.getMessage());
				//e.printStackTrace();
				return;
			}
        }
        for (Tree subTree : inputTree.children()) {
            printSubTrees(subTree, words,sentence_id, words_statement, sentences_words_statement);
        }
    }

    public static void main(String[] args) throws IOException, NoSuchAlgorithmException {
        Properties props = new Properties();
        
        if (args.length > 0) {
            props = StringUtils.argsToProperties(args);
        }
        StanfordCoreNLP pipeline = new StanfordCoreNLP(props);
        String fileName = props.getProperty("file");
        Collection<File> files = new FileSequentialCollection(new File(fileName), props.getProperty("extension"), true);
        Annotation annotation = null;
        
        for (final File file : files) {
	        String encoding = props.getProperty("encoding", "UTF-8");
	        String text = IOUtils.slurpFile(file.getAbsoluteFile(), encoding);
	        annotation = new Annotation(text);
	        pipeline.annotate(annotation);
        }
        
        String[] returnId = { "id" };

        try {
        	connection = (Connection) DriverManager.getConnection(url, username, password);
        	log.info("DB connected!");
        } catch (SQLException e) {
            throw new IllegalStateException("Cannot connect the database!", e);
        }
        
        List<CoreMap> sentences = annotation.get(CoreAnnotations.SentencesAnnotation.class);
        if (sentences != null && sentences.size() > 0) {
        	String sentences_sql = "INSERT INTO sentences(parsed_tree,hash_tree) VALUES (?,?)";
        	String words_sql = "INSERT INTO words(content,sentiment) VALUES (?,?)";
        	String sentences_words_sql = "INSERT INTO sentences_words VALUES (?,?,?,?)";
        	PreparedStatement statement = null, words_statement = null, sentences_words_statement = null;
			try {
				statement = connection.prepareStatement(sentences_sql, returnId);
				words_statement = connection.prepareStatement(words_sql, Statement.RETURN_GENERATED_KEYS);
				sentences_words_statement = connection.prepareStatement(sentences_words_sql);
			} catch (SQLException e) {
				e.printStackTrace();
			}
        	Tree sentenceTree = null;
        	Tree tree2 = null;
        	MessageDigest messageDigest = MessageDigest.getInstance("SHA-256");

        	for(CoreMap sentence : sentences) {
              sentenceTree = sentence.get(TreeCoreAnnotations.BinarizedTreeAnnotation.class);            
              String content = sentenceTree.pennString();
              //hash penn tree.
              messageDigest.update(content.getBytes());
              String hash = new String(messageDigest.digest());
              
              try {
            	  statement.setString(1, content);

	              statement.setString(2, hash);
	              statement.executeUpdate();
	              ResultSet rs = statement.getGeneratedKeys();
	              int id = 1;
	              if(rs.next())
	              {
	                  id = rs.getInt(1);
	              }
	        	  tree2 = sentence.get(SentimentCoreAnnotations.SentimentAnnotatedTree.class);
	        	  order = 1;
	        	  printSubTrees(tree2, null, id, words_statement, sentences_words_statement);
	        	  System.out.println();
  			  } catch (SQLException e) {
				continue;
			  }
        	}
        }
    }
}