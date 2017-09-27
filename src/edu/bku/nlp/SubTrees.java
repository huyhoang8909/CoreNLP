package edu.bku.nlp;

import edu.stanford.nlp.ling.*;
import edu.stanford.nlp.pipeline.Annotation;
import edu.stanford.nlp.pipeline.StanfordCoreNLP;
import edu.stanford.nlp.trees.*;
import edu.stanford.nlp.util.*;

import java.io.*;
import java.util.*;

import edu.stanford.nlp.neural.rnn.RNNCoreAnnotations;
import edu.stanford.nlp.neural.rnn.RNNCoreAnnotations.PredictedClass;
import edu.stanford.nlp.sentiment.SentimentCoreAnnotations;


public class SubTrees {

    public static void printSubTrees(Tree inputTree, ArrayList<Word> previousWords, String spacing) {
        if (inputTree.isLeaf()) {
            return;
        }
        ArrayList<Word> words = new ArrayList<Word>();
        for (Tree leaf : inputTree.getLeaves()) {
            words.addAll(leaf.yieldWords());
        }
        CoreLabel label = (CoreLabel)inputTree.label();
        String cat = label.value();
        if (cat.equals("ROOT") || (previousWords != null && !previousWords.equals(words))) {
          if((label.containsKey(PredictedClass.class))){
        	  System.out.print(RNNCoreAnnotations.getPredictedClass(inputTree) + " ");
          }
        
        for (Word w : words) {
            System.out.print(w.word()+ " ");
        }
        System.out.println();
        }
        for (Tree subTree : inputTree.children()) {
            printSubTrees(subTree, words,spacing + " ");
        }
    }

    public static void main(String[] args) throws FileNotFoundException {
        Properties props = new Properties();
        props.setProperty("annotators", "tokenize,ssplit,pos,parse,sentiment");
        props.setProperty("parse.binaryTrees", "true");
        StanfordCoreNLP pipeline = new StanfordCoreNLP(props);
        //call google api translation
        //log to db
        String text = "Looks out this Asus is not very good, buy dell vostro 3450 has more than 0.2 kg look better.";
        Annotation annotation = new Annotation(text);
        pipeline.annotate(annotation);
//        Tree sentenceTree = annotation.get(CoreAnnotations.SentencesAnnotation.class).get(0).get(
//                TreeCoreAnnotations.BinarizedTreeAnnotation.class);
//        System.out.println("Penn tree:");
//        sentenceTree.pennPrint(System.out);
//        System.out.println();
//        printSubTrees(sentenceTree, null, "");
        
        PrintWriter out;
        if (args.length > 1) {
          out = new PrintWriter(args[1]);
        } else {
          out = new PrintWriter(System.out);
        }
        List<CoreMap> sentences = annotation.get(CoreAnnotations.SentencesAnnotation.class);
        if (sentences != null && sentences.size() > 0) {
          ArrayCoreMap sentence = (ArrayCoreMap) sentences.get(0);

          System.out.println("Sentence's keys: ");
          System.out.println(sentence.keySet());
          
          Tree tree2 = sentence.get(SentimentCoreAnnotations.SentimentAnnotatedTree.class);          
          printSubTrees(tree2, null, "");

        }
    }
}