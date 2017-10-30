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
    private static String url = "jdbc:mysql://%s:3306/vietnamese_treebank?autoReconnect=true&useUnicode=yes&characterEncoding=UTF-8";
    private static Connection connection = null;
    
	public static void printSubTrees(Tree inputTree, ArrayList<Word> previousWords, 
			int sentiment, int parent_id,PreparedStatement words_statement, 
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
        int isVerifed = 0;
        int isInDict = 0;

        if (cat.equals("ROOT") || (previousWords != null && !previousWords.equals(words))) {
        	
        	if((label.containsKey(PredictedClass.class))){
        	 	 sentiment = RNNCoreAnnotations.getPredictedClass(inputTree);
        	}
        
	        for (Word w : words) {
	        	sentence += w.word() + " ";
	        }
        	sentence = sentence.trim();
        	try {
//            int new_sen = getSentiment(sentence);
//            if ( new_sen >= 0 ) {
//                sentiment = new_sen;
//                isInDict = 1;
//            }

//            String lowerCaseSentence = sentence.toLowerCase(); 
//            
//            if (lowerCaseSentence.startsWith("ưu_điểm") || lowerCaseSentence.startsWith("ưu")) {
//            	sentiment = Sentiment.POSITIVE;
//
//            } else if (lowerCaseSentence.startsWith("khuyết_điểm") || lowerCaseSentence.startsWith("khuyết")) {
//            	sentiment = Sentiment.NEGATIVE;
//
//            }
//            
//            if (cat.equals("N") || cat.equals("M") || cat.equals("Np")|| cat.equals("R") || cat.equals("E") || cat.equals("C") 
//            		|| cat.contains("QP") || cat.contains("L") || cat.contains("CC") || cat.contains("EOS") || cat.contains("Nc")
//            		|| sentence.equals("-LRB-") || sentence.equals("-RRB-")) {
//            	sentiment = Sentiment.NEUTRAL;
//
//            }
            
//        	System.out.println(sentiment + " " + cat + " " + sentence);
            
            System.out.println(sentiment + " " + sentence);
//            if (cat.equals("ROOT")) {
//    			writer.println(sentiment + " " + sentence);
//    			writer.println();
//            }
            	// insert words
//              if ( new_sen == -1 ) {
            	  words_statement.setInt(1, sentiment);
            	  words_statement.setString(2, sentence);
//         		words_statement.setString(1, sentence);
//         		words_statement.setInt(2, parent_id);
//         		words_statement.setString(3, cat);
//         		words_statement.setInt(4, sentiment);
//             	words_statement.setInt(5, isInDict);
				words_statement.executeUpdate();
//              }
//				 if (cat.equals("ROOT")) {
//					 ResultSet rs = words_statement.getGeneratedKeys();
//                 	if(rs.next())
//	                 {
//	                     parent_id = rs.getInt(1);
//	                 }
//				 }

				// // insert sentences_words
				// sentences_words_statement.setInt(1, sentence_id);
				// sentences_words_statement.setInt(2, word_id);
				// sentences_words_statement.setInt(3, order++);
				// sentences_words_statement.setString(4, cat);
				// sentences_words_statement.executeUpdate();
			} catch (SQLException e) {
				//log.info(sentiment + " " + sentence);
				log.info(e.getMessage());
				//e.printStackTrace();
				return;
			}
        }
        for (Tree subTree : inputTree.children()) {
            printSubTrees(subTree, words,sentiment, parent_id,words_statement, sentences_words_statement);
        }
    }
	
	public static void setSentimentWithParser(Tree inputTree, ArrayList<Word> previousWords, 
			int sentiment, int parent_id,PreparedStatement words_statement) 
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
        int isInDict = 0;

        if (cat.equals("ROOT") || (previousWords != null && !previousWords.equals(words))) {
        	
        	if((label.containsKey(PredictedClass.class))){
        	 	 sentiment = RNNCoreAnnotations.getPredictedClass(inputTree);
        	}
        
	        for (Word w : words) {
	        	sentence += w.word() + " ";
	        }
        	sentence = sentence.trim();
        	
        	try {
	            int new_sen = getSentiment(sentence);
	            if ( new_sen >= 0 ) {
	                sentiment = new_sen;
	                isInDict = 1;
	            }
	
	            String lowerCaseSentence = sentence.toLowerCase(); 
	            
	            if (lowerCaseSentence.startsWith("ưu_điểm") || lowerCaseSentence.startsWith("ưu")) {
	            	sentiment = Sentiment.POSITIVE;
	
	            } else if (lowerCaseSentence.startsWith("khuyết_điểm") || lowerCaseSentence.startsWith("khuyết")) {
	            	sentiment = Sentiment.NEGATIVE;
	
	            }
	            
	            if (cat.equals("N") || cat.equals("M") || cat.equals("Np")|| cat.equals("R") || cat.equals("E") || cat.equals("C") 
	            		|| cat.contains("QP") || cat.contains("L") || cat.contains("CC") || cat.contains("EOS") || cat.contains("Nc")
	            		|| sentence.equals("-LRB-") || sentence.equals("-RRB-")) {
	            	sentiment = Sentiment.NEUTRAL;
	
	            }
	            
	            System.out.println(sentiment + " " + sentence);
	
	     		words_statement.setString(1, sentence);
	     		words_statement.setInt(2, parent_id);
	     		words_statement.setString(3, cat);
	     		words_statement.setInt(4, sentiment);
	         	words_statement.setInt(5, isInDict);
				words_statement.executeUpdate();
				
				if (cat.equals("ROOT")) {
					 ResultSet rs = words_statement.getGeneratedKeys();
	             	if(rs.next())
	                 {
	                     parent_id = rs.getInt(1);
	                 }
				}
			} catch (SQLException e) {
				log.info(e.getMessage());
				return;
			}
        }
        for (Tree subTree : inputTree.children()) {
            setSentimentWithParser(subTree, words,sentiment, parent_id,words_statement);
        }
    }

    private static PreparedStatement select_words_stmt = null;

    public static int getSentiment(String word) throws SQLException {
    	select_words_stmt.setString(1, word);
        ResultSet rs = select_words_stmt.executeQuery();
        if(rs.next())
        {
            return rs.getInt("sentiment");
        }

        return -1;
    }
    
    public static void runwithParser(Properties props) {
        
        StanfordCoreNLP pipeline = new StanfordCoreNLP(props);
        Annotation annotation = null;
        
        String table = props.getProperty("category");
        String select_reviews_sql = "SELECT * FROM reviews WHERE category = '%s' and length < 95 and is_verified = 1";
        select_reviews_sql = String.format(select_reviews_sql, table);
        
        PreparedStatement reviews_stmt;
		try {
			reviews_stmt = connection.prepareStatement(select_reviews_sql);

	        ResultSet reviews_rs = reviews_stmt.executeQuery();
	        List<Integer> sentiment_list = new ArrayList<>();
	        String text = "";
	
	        while(reviews_rs.next())
	        {
	        	text += reviews_rs.getString("content") + "\r\n";
	        	sentiment_list.add(reviews_rs.getInt("rating"));
	        }
		    annotation = new Annotation(text);
		    pipeline.annotate(annotation);
	        
	        int index = 0;
	        int sentiment = 0;
	        
	        List<CoreMap> sentences = annotation.get(CoreAnnotations.SentencesAnnotation.class);
	        if (sentences != null && sentences.size() > 0) {
	        	String words_sql = "INSERT INTO %s(content,parent,category,sentiment,is_in_dict) VALUES (?,?,?,?,?)";
	        	words_sql = String.format(words_sql, table);
	            String select_words_sql = "SELECT sentiment FROM words WHERE content = ? and is_verified = 1";
	            
	        	PreparedStatement words_statement = null;
				try {
					words_statement = connection.prepareStatement(words_sql);
					select_words_stmt = connection.prepareStatement(select_words_sql);
				} catch (SQLException e) {
					e.printStackTrace();
				}
	        	Tree sentenceTree = null;
	
	        	for(CoreMap sentence : sentences) {
	        	  sentiment = sentiment_list.get(index++);
	              sentenceTree = sentence.get(TreeCoreAnnotations.BinarizedTreeAnnotation.class);
	        	  setSentimentWithParser(sentenceTree, null, sentiment, 0,words_statement);
		          System.out.println();
	
	        	}
	        	
	        }
		} catch (SQLException e1) {
			// TODO Auto-generated catch block
			log.info(e1.getMessage());
		}
    	
    }
    
    
    public static void main(String[] args) throws IOException, NoSuchAlgorithmException {
        Properties props = new Properties();
        
        if (args.length > 0) {
            props = StringUtils.argsToProperties(args);
        }
        String action = props.getProperty("action");
        StanfordCoreNLP pipeline = new StanfordCoreNLP(props);
        Annotation annotation = null;

        try {
        	String username = props.getProperty("db.username", "root");
        	String password = props.getProperty("db.password", "");
        	url = String.format(url, props.getProperty("db.ip", "192.168.33.10"));
            connection = (Connection) DriverManager.getConnection(url, username, password);
            log.info("DB connected!");
        } catch (SQLException e) {
            throw new IllegalStateException("Cannot connect the database!", e);
        }
        
        if (action.equals("insertSentiment")) {
        	runwithParser(props);
        	return;
        }
       

        String select_reviews_sql = "SELECT * FROM reviews WHERE category = 'phu-kien' and length < 95 limit 1";
        PreparedStatement reviews_stmt;
		try {
			reviews_stmt = connection.prepareStatement(select_reviews_sql);

			ResultSet reviews_rs = reviews_stmt.executeQuery();
			List<Integer> sentiment_list = new ArrayList<>();
			String text = "";

			while(reviews_rs.next())
			{
				text += reviews_rs.getString("content") + "\r\n";
				sentiment_list.add(reviews_rs.getInt("rating"));
			}
			text="Sản_phẩm cho âm_thanh ấm , phù_hợp nghe nhạc hoà_tấu và nhạc_nhẹ , cách_âm_tốt . Kiểu_dáng chắc_chắn , nhẹ , nghe lâu không bị đau tai . Sản_phẩm được Tiki đóng_gói cẩn_thận , giá của tai_nghe này so với những trang bán hàng online khác rẻ hơn nhiều";
			annotation = new Annotation(text);
			pipeline.annotate(annotation);
        
			String[] returnId = { "id" };
			int index = 0;
			int sentiment = 0;
        
			List<CoreMap> sentences = annotation.get(CoreAnnotations.SentencesAnnotation.class);
			if (sentences != null && sentences.size() > 0) {
				String sentences_sql = "INSERT INTO sentences(parsed_tree,hash_tree) VALUES (?,?)";
        	String words_sql = "update accessory set predicted_sentiment = ? where content = ? and predicted_sentiment is null";
        	String sentences_words_sql = "INSERT INTO sentences_words VALUES (?,?,?,?)";
            String select_words_sql = "SELECT content FROM accessory WHERE content = ?";
            String select_accessory_words_sql = "SELECT sentiment FROM accessory_words WHERE content = ?";
            
        	PreparedStatement statement = null, words_statement = null, sentences_words_statement = null;
			try {
				statement = connection.prepareStatement(sentences_sql, returnId);
				words_statement = connection.prepareStatement(words_sql, Statement.RETURN_GENERATED_KEYS);
//                sentences_words_statement = connection.prepareStatement(sentences_words_sql);
//				select_words_stmt = connection.prepareStatement(select_words_sql);
//				select_accessory_words_stmt = connection.prepareStatement(select_accessory_words_sql);
			} catch (SQLException e) {
				e.printStackTrace();
			}
        	Tree tree2 = null;
        	for(CoreMap sentence : sentences) {
	        	  tree2 = sentence.get(SentimentCoreAnnotations.SentimentAnnotatedTree.class);
	        	  printSubTrees(tree2, null, sentiment, 0,words_statement, sentences_words_statement);
	        	  System.out.println();

        	}
        	
        }
		} catch (SQLException e1) {
			// TODO Auto-generated catch block
			log.info(e1.getMessage());
		}
    }
}
