package edu.stanford.nlp.pipeline;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Properties;
import java.util.Set;

import edu.stanford.nlp.ling.CoreAnnotation;
import edu.stanford.nlp.ling.CoreAnnotations;
import edu.stanford.nlp.ling.CoreLabel;
import edu.stanford.nlp.util.CoreMap;
import edu.stanford.nlp.util.PropertiesUtils;
import edu.stanford.nlp.util.logging.Redwood;
import vn.hus.nlp.tokenizer.VietTokenizer;

/**
 * This class will add segmentation information to an Annotation. It assumes that the original document is a List of
 * sentences under the {@code SentencesAnnotation.class} key, and that each sentence has a {@code TextAnnotation.class
 * key}. This Annotator adds corresponding information under a {@code CharactersAnnotation.class} key prior to
 * segmentation, and a {@code TokensAnnotation.class} key with value of a List of CoreLabel after segmentation.
 *
 * @author lainam
 */
public class VietnameseSegmenterAnnotator implements Annotator {

    private static final String DEFAULT_SEG_LOC =
            "/u/nlp/data/arabic-segmenter/arabic-segmenter-atb+bn+arztrain.ser.gz";
    /**
     * A logger for this class
     */
    private static Redwood.RedwoodChannels log = Redwood.channels(VietnameseSegmenterAnnotator.class);
    private final boolean VERBOSE;
    private VietTokenizer segmenter;

    public VietnameseSegmenterAnnotator() {
        this(DEFAULT_SEG_LOC, false);
    }

    public VietnameseSegmenterAnnotator(boolean verbose) {
        this(DEFAULT_SEG_LOC, verbose);
    }

    public VietnameseSegmenterAnnotator(String segLoc, boolean verbose) {
        VERBOSE = verbose;
        Properties props = new Properties();
        loadModel(segLoc, props);
    }

    public VietnameseSegmenterAnnotator(String name, Properties props) {
        String model = null;
        // Keep only the properties that apply to this annotator
        Properties modelProps = new Properties();
        String desiredKey = name + '.';
        for (String key : props.stringPropertyNames()) {
            if (key.startsWith(desiredKey)) {
                // skip past name and the subsequent "."
                String modelKey = key.substring(desiredKey.length());
                if (modelKey.equals("model")) {
                    model = props.getProperty(key);
                } else {
                    modelProps.setProperty(modelKey, props.getProperty(key));
                }
            }
        }
        this.VERBOSE = PropertiesUtils.getBool(props, name + ".verbose", false);
        if (model == null) {
            log.info("load Vietnamese tokenizer model...");
        }

        loadModel(model, modelProps);
    }

    @SuppressWarnings("unused")
    private void loadModel(String segLoc) {
        // don't write very much, because the CRFClassifier already reports loading
        if (VERBOSE) {
            log.info("Loading segmentation model ... ");
        }
        Properties modelProps = new Properties();
        modelProps.setProperty("model", segLoc);
        segmenter = new VietTokenizer();
    }

    private void loadModel(String segLoc, Properties props) {
        // don't write very much, because the CRFClassifier already reports loading
        if (VERBOSE) {
            log.info("Loading Segmentation Model ... ");
        }
//        Properties modelProps = new Properties();
//        modelProps.setProperty("model", segLoc);
//        modelProps.putAll(props);
        try {
            segmenter = new VietTokenizer();
        } catch (RuntimeException e) {
            throw e;
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public void annotate(Annotation annotation) {
        if (VERBOSE) {
            log.info("Adding Segmentation annotation ... ");
        }
        List<CoreMap> sentences = annotation.get(CoreAnnotations.SentencesAnnotation.class);
        if (sentences != null) {
            for (CoreMap sentence : sentences) {
                doOneSentence(sentence);
            }
        } else {
            doOneSentence(annotation);
        }
    }

    private void doOneSentence(CoreMap annotation) {
        String text = annotation.get(CoreAnnotations.TextAnnotation.class);
        String[] tokenizes = segmenter.tokenize(text)[0].split(" ");
        System.out.println(tokenizes.length);
        int beginPosition = 0;
        int endPosition = -1;
        List<CoreLabel> tokens = new ArrayList<>();
        for (String token : tokenizes) {
            CoreLabel coreLabel = new CoreLabel() {{
                setWord(token);
                setValue(token);
                setOriginalText(token);
            }};

            beginPosition = endPosition + 1;
            endPosition = beginPosition + token.length() - 1;

            coreLabel.setEndPosition(endPosition);
            coreLabel.setBeginPosition(beginPosition);
            tokens.add(coreLabel);
        }


        annotation.set(CoreAnnotations.TokensAnnotation.class, tokens);
    }


    @Override
    public Set<Class<? extends CoreAnnotation>> requires() {
        return Collections.emptySet();
    }

    @Override
    public Set<Class<? extends CoreAnnotation>> requirementsSatisfied() {
        return new HashSet<>(Arrays.asList(
                CoreAnnotations.TextAnnotation.class,
                CoreAnnotations.TokensAnnotation.class,
                CoreAnnotations.CharacterOffsetBeginAnnotation.class,
                CoreAnnotations.CharacterOffsetEndAnnotation.class,
                CoreAnnotations.BeforeAnnotation.class,
                CoreAnnotations.AfterAnnotation.class,
                CoreAnnotations.TokenBeginAnnotation.class,
                CoreAnnotations.TokenEndAnnotation.class,
                CoreAnnotations.PositionAnnotation.class,
                CoreAnnotations.IndexAnnotation.class,
                CoreAnnotations.OriginalTextAnnotation.class,
                CoreAnnotations.ValueAnnotation.class
        ));
    }

}
