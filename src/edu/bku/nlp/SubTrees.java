package edu.bku.nlp;

import edu.stanford.nlp.ling.CoreAnnotations;
import edu.stanford.nlp.ling.Label;
import edu.stanford.nlp.ling.Word;
import edu.stanford.nlp.pipeline.Annotation;
import edu.stanford.nlp.pipeline.StanfordCoreNLP;
import edu.stanford.nlp.trees.*;

import java.util.ArrayList;
import java.util.Properties;

public class SubTrees {

    public static void printSubTrees(Tree inputTree, ArrayList<Word> previousWords, String spacing) {
        if (inputTree.isLeaf()) {
            return;
        }
        ArrayList<Word> words = new ArrayList<Word>();
        for (Tree leaf : inputTree.getLeaves()) {
            words.addAll(leaf.yieldWords());
        }
        Label label = inputTree.label();
        String cat = label.value();
        if (cat.equals("ROOT") || (previousWords != null && !previousWords.equals(words))) {
        System.out.print(label+" ");
        for (Word w : words) {
            System.out.print(w.word()+ " ");
        }
        System.out.println();
        }
        for (Tree subTree : inputTree.children()) {
            printSubTrees(subTree, words,spacing + " ");
        }
    }

    public static void main(String[] args) {
        Properties props = new Properties();
        props.setProperty("annotators", "tokenize,ssplit,pos,parse");
        props.setProperty("parse.binaryTrees", "true");
        StanfordCoreNLP pipeline = new StanfordCoreNLP(props);
        String text = "Yet the act is still charming here.";
        Annotation annotation = new Annotation(text);
        pipeline.annotate(annotation);
        Tree sentenceTree = annotation.get(CoreAnnotations.SentencesAnnotation.class).get(0).get(
                TreeCoreAnnotations.BinarizedTreeAnnotation.class);
        System.out.println("Penn tree:");
        sentenceTree.pennPrint(System.out);
        System.out.println();
        printSubTrees(sentenceTree, null, "");
        

    }
}