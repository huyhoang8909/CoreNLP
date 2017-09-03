package edu.bku.nlp;

import org.junit.Assert;
import org.junit.Test;
import org.junit.experimental.runners.Enclosed;
import org.junit.runner.RunWith;

import java.io.IOException;
import java.util.List;
import java.util.Properties;
import java.util.function.Function;

import edu.stanford.nlp.coref.CorefCoreAnnotations;
import edu.stanford.nlp.ling.CoreAnnotations;
import edu.stanford.nlp.ling.CoreLabel;
import edu.stanford.nlp.pipeline.Annotation;
import edu.stanford.nlp.pipeline.AnnotationPipeline;
import edu.stanford.nlp.pipeline.BinarizerAnnotator;
import edu.stanford.nlp.pipeline.POSTaggerAnnotator;
import edu.stanford.nlp.pipeline.ParserAnnotator;
import edu.stanford.nlp.pipeline.TokenizerAnnotator;
import edu.stanford.nlp.pipeline.WordsToSentencesAnnotator;
import edu.stanford.nlp.trees.Tree;
import edu.stanford.nlp.trees.TreeCoreAnnotations;
import edu.stanford.nlp.util.CoreMap;
import edu.stanford.nlp.util.Iterables;
import edu.stanford.nlp.util.StringUtils;
import old.edu.stanford.nlp.ling.WordTag;
import vn.hus.nlp.tagger.VietnameseMaxentTagger;

@RunWith(Enclosed.class)
public class CustomSentimentAnnotatorTest {

    private static String join(List<CoreLabel> tokens) {
        return StringUtils.join(Iterables.transform(tokens, new Function<CoreLabel, String>() {
            public String apply(CoreLabel token) {
                return token.get(CoreAnnotations.TextAnnotation.class);
            }
        }));
    }

    public static class EnglishSentiment {
        @Test
        public void success() throws IOException, ClassNotFoundException {
            // create pipeline
            AnnotationPipeline pipeline = new AnnotationPipeline();
            pipeline.addAnnotator(new TokenizerAnnotator(false, "en"));
            pipeline.addAnnotator(new WordsToSentencesAnnotator(false));
            pipeline.addAnnotator(new POSTaggerAnnotator(false));
//            pipeline.addAnnotator(new MorphaAnnotator(false));
//            pipeline.addAnnotator(new NERCombinerAnnotator(false));
//            pipeline.addAnnotator(new ParserAnnotator(false, -1));
            //pipeline.addAnnotator(new CorefAnnotator(null, null, null, false));
            //pipeline.addAnnotator(new SRLAnnotator(false));

            // create annotation with text
            String text = "Dan Ramage is working for\nMicrosoft. He's in Seattle! \n";
            Annotation document = new Annotation(text);
            Assert.assertEquals(text, document.toString());
            Assert.assertEquals(text, document.get(CoreAnnotations.TextAnnotation.class));

            // annotate text with pipeline
            pipeline.annotate(document);

            // demonstrate typical usage
            for (CoreMap sentence : document.get(CoreAnnotations.SentencesAnnotation.class)) {

                // get the tree for the sentence
                Tree tree = sentence.get(TreeCoreAnnotations.TreeAnnotation.class);

                // get the tokens for the sentence and iterate over them
                for (CoreLabel token : sentence.get(CoreAnnotations.TokensAnnotation.class)) {

                    // get token attributes
                    String tokenText = token.get(CoreAnnotations.TextAnnotation.class);
                    String tokenPOS = token.get(CoreAnnotations.PartOfSpeechAnnotation.class);
                    String tokenLemma = token.get(CoreAnnotations.LemmaAnnotation.class);
                    String tokenNE = token.get(CoreAnnotations.NamedEntityTagAnnotation.class);

                    // text, pos, lemma and name entity tag should be defined
                    Assert.assertNotNull(tokenText);
                    Assert.assertNotNull(tokenPOS);
                    Assert.assertNotNull(tokenLemma);
                    Assert.assertNotNull(tokenNE);
                }
                // tree should be defined
                Assert.assertNotNull(tree);
            }

            // get tokens
            List<CoreLabel> tokens = document.get(CoreAnnotations.TokensAnnotation.class);
            String tokensText = "Dan Ramage is working for Microsoft . He 's in Seattle !";
            Assert.assertNotNull(tokens);
            Assert.assertEquals(12, tokens.size());
            Assert.assertEquals(tokensText, join(tokens));
            Assert.assertEquals(0, (int) tokens.get(0).get(CoreAnnotations.CharacterOffsetBeginAnnotation.class));
            Assert.assertEquals(3, (int) tokens.get(0).get(CoreAnnotations.CharacterOffsetEndAnnotation.class));
            Assert.assertEquals("NNP", tokens.get(0).get(CoreAnnotations.PartOfSpeechAnnotation.class));
            Assert.assertEquals("VBZ", tokens.get(2).get(CoreAnnotations.PartOfSpeechAnnotation.class));
            Assert.assertEquals(".", tokens.get(11).get(CoreAnnotations.PartOfSpeechAnnotation.class));
            Assert.assertEquals("Ramage", tokens.get(1).get(CoreAnnotations.LemmaAnnotation.class));
            Assert.assertEquals("be", tokens.get(2).get(CoreAnnotations.LemmaAnnotation.class));
            Assert.assertEquals("PERSON", tokens.get(0).get(CoreAnnotations.NamedEntityTagAnnotation.class));
            Assert.assertEquals("PERSON", tokens.get(1).get(CoreAnnotations.NamedEntityTagAnnotation.class));
            Assert.assertEquals("LOCATION", tokens.get(10).get(CoreAnnotations.NamedEntityTagAnnotation.class));

            // get sentences
            List<CoreMap> sentences = document.get(CoreAnnotations.SentencesAnnotation.class);
            Assert.assertNotNull(sentences);
            Assert.assertEquals(2, sentences.size());

            // sentence 1
            String text1 = "Dan Ramage is working for\nMicrosoft.";
            CoreMap sentence1 = sentences.get(0);
            Assert.assertEquals(text1, sentence1.toString());
            Assert.assertEquals(text1, sentence1.get(CoreAnnotations.TextAnnotation.class));
            Assert.assertEquals(0, (int) sentence1.get(CoreAnnotations.CharacterOffsetBeginAnnotation.class));
            Assert.assertEquals(36, (int) sentence1.get(CoreAnnotations.CharacterOffsetEndAnnotation.class));
            Assert.assertEquals(0, (int) sentence1.get(CoreAnnotations.TokenBeginAnnotation.class));
            Assert.assertEquals(7, (int) sentence1.get(CoreAnnotations.TokenEndAnnotation.class));

            // sentence 1 tree
            Tree tree1 = Tree.valueOf("(ROOT (S (NP (NNP Dan) (NNP Ramage)) (VP (VBZ is) " +
                    "(VP (VBG working) (PP (IN for) (NP (NNP Microsoft))))) (. .)))");
            Assert.assertEquals(tree1, sentence1.get(TreeCoreAnnotations.TreeAnnotation.class));

            // sentence 1 tokens
            String tokenText1 = "Dan Ramage is working for Microsoft .";
            List<CoreLabel> tokens1 = sentence1.get(CoreAnnotations.TokensAnnotation.class);
            Assert.assertNotNull(tokens1);
            Assert.assertEquals(7, tokens1.size());
            Assert.assertEquals(tokenText1, join(tokens1));
            Assert.assertEquals(4, (int) tokens1.get(1).get(CoreAnnotations.CharacterOffsetBeginAnnotation.class));
            Assert.assertEquals(10, (int) tokens1.get(1).get(CoreAnnotations.CharacterOffsetEndAnnotation.class));
            Assert.assertEquals("IN", tokens1.get(4).get(CoreAnnotations.PartOfSpeechAnnotation.class));
            Assert.assertEquals("NNP", tokens1.get(5).get(CoreAnnotations.PartOfSpeechAnnotation.class));
            Assert.assertEquals("work", tokens1.get(3).get(CoreAnnotations.LemmaAnnotation.class));
            Assert.assertEquals(".", tokens1.get(6).get(CoreAnnotations.LemmaAnnotation.class));
            Assert.assertEquals("ORGANIZATION", tokens1.get(5).get(CoreAnnotations.NamedEntityTagAnnotation.class));

            // sentence 2
            String text2 = "He's in Seattle!";
            CoreMap sentence2 = sentences.get(1);
            Assert.assertEquals(text2, sentence2.toString());
            Assert.assertEquals(text2, sentence2.get(CoreAnnotations.TextAnnotation.class));
            Assert.assertEquals(37, (int) sentence2.get(CoreAnnotations.CharacterOffsetBeginAnnotation.class));
            Assert.assertEquals(53, (int) sentence2.get(CoreAnnotations.CharacterOffsetEndAnnotation.class));
            Assert.assertEquals(7, (int) sentence2.get(CoreAnnotations.TokenBeginAnnotation.class));
            Assert.assertEquals(12, (int) sentence2.get(CoreAnnotations.TokenEndAnnotation.class));

            // sentence 2 tree (note error on Seattle, caused by part of speech tagger)
            Tree tree2 = Tree.valueOf("(ROOT (S (NP (PRP He)) (VP (VBZ 's) (PP (IN in) " +
                    "(NP (NNP Seattle)))) (. !)))");
            Assert.assertEquals(tree2, sentence2.get(TreeCoreAnnotations.TreeAnnotation.class));

            // sentence 2 tokens
            String tokenText2 = "He 's in Seattle !";
            List<CoreLabel> tokens2 = sentence2.get(CoreAnnotations.TokensAnnotation.class);
            Assert.assertNotNull(tokens2);
            Assert.assertEquals(5, tokens2.size());
            Assert.assertEquals(tokenText2, join(tokens2));
            Assert.assertEquals(39, (int) tokens2.get(1).get(CoreAnnotations.CharacterOffsetBeginAnnotation.class));
            Assert.assertEquals(41, (int) tokens2.get(1).get(CoreAnnotations.CharacterOffsetEndAnnotation.class));
            Assert.assertEquals("VBZ", tokens2.get(1).get(CoreAnnotations.PartOfSpeechAnnotation.class));
            Assert.assertEquals("be", tokens2.get(1).get(CoreAnnotations.LemmaAnnotation.class));
            Assert.assertEquals("LOCATION", tokens2.get(3).get(CoreAnnotations.NamedEntityTagAnnotation.class));
        }
    }

    public static class VietnameseSentiment {
        @Test
        public void success() throws IOException, ClassNotFoundException {
            Properties properties = new Properties();
            properties.setProperty("tokenize.language", "vietnamese");
            properties.setProperty("pos.model", "resources/models/vtb.tagger");
            properties.setProperty("vn.parser.model", "resources/models/VietNamesePCFG.ser.gz");

            AnnotationPipeline pipeline = new AnnotationPipeline();

            pipeline.addAnnotator(new CustomTokenizerAnnotator(true, properties));

            pipeline.addAnnotator(new WordsToSentencesAnnotator(true));

            pipeline.addAnnotator(new CustomPOSTaggerAnnotator(true));

            pipeline.addAnnotator(new CustomParserAnnotator(false, -1));
//            pipeline.addAnnotator(new BinarizerAnnotator("", properties));

            String text = "Cựu lãnh đạo Ngân hàng Sacombank Trầm Bê bị cho có sai phạm, tiếp tay cho Phạm Công Danh gây thất thoát hàng nghìn tỷ đồng.";
            Annotation document = new Annotation(text);

            // Run all Annotations on this text
            pipeline.annotate(document);

            List<CoreMap> sentences = document.get(CoreAnnotations.SentencesAnnotation.class);
            for (CoreMap sentence : sentences) {

                // get the tokens for the sentence and iterate over them
                for (CoreLabel token : sentence.get(CoreAnnotations.TokensAnnotation.class)) {


                }

                Tree tree = sentence.get(TreeCoreAnnotations.TreeAnnotation.class);
                tree.pennPrint();
//                String s = sentence.get(CoreAnnotations.SentencesAnnotation.class);
//                System.out.println(s);
            }

        }

        @Test
        public void maxentTest() throws Exception {
            Properties properties = new Properties();
            properties.setProperty("tokenize.language", "vietnamese");
            properties.setProperty("model", "resources/models/vtb.tagger");

//            old.edu.stanford.nlp.tagger.maxent.TaggerConfig taggerConfig = new old.edu.stanford.nlp.tagger.maxent.TaggerConfig(properties);
//            old.edu.stanford.nlp.tagger.maxent.MaxentTagger maxentTagger = new old.edu.stanford.nlp.tagger.maxent.MaxentTagger(taggerConfig);

            VietnameseMaxentTagger vn = new VietnameseMaxentTagger();
            List<WordTag> results = vn.tagText2("Cựu lãnh đạo Ngân hàng Sacombank Trầm Bê bị cho có sai phạm, tiếp tay cho Phạm Công Danh gây thất thoát hàng nghìn tỷ đồng.");
            System.out.println(results);

        }

        public static class EnglishSegmenter {
            @Test
            public void success() throws IOException, ClassNotFoundException {
                AnnotationPipeline pipeline = new AnnotationPipeline();
                pipeline.addAnnotator(new CustomTokenizerAnnotator(false, "en"));
                pipeline.addAnnotator(new WordsToSentencesAnnotator(false));
                pipeline.addAnnotator(new POSTaggerAnnotator(false));

                // create annotation with text
                String text = "I like dog. But don't like cat.";
                Annotation document = new Annotation(text);
                Assert.assertEquals(text, document.toString());
                Assert.assertEquals(text, document.get(CoreAnnotations.TextAnnotation.class));


//            Annotation document = new Annotation("Cuộc đời là những chuyến đi và bạn có thể quyết định được con đường đi của mình.");
                // Thỉnh thoảng, sẽ có nhiều chuyện không may xảy ra nhưng hãy vững bước bạn nhé.

                // Run all Annotations on this text
                pipeline.annotate(document);

                List<CoreMap> sentences = document.get(CoreAnnotations.SentencesAnnotation.class);
                for (CoreMap sentence : sentences) {
                    System.out.println(sentence.toShorterString());
                    for (CoreLabel coreLabel : sentence.get(CoreAnnotations.TokensAnnotation.class)) {
                        System.out.println(coreLabel.index());
                    }
                }

            }
        }
    }
}