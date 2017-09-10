//MaxentTagger -- StanfordMaxEnt, A Maximum Entropy Toolkit
//Copyright (c) 2002-2009 Leland Stanford Junior University


//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation; either version 2
//of the License, or (at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

//For more information, bug reports, fixes, contact:
//Christopher Manning
//Dept of Computer Science, Gates 1A
//Stanford CA 94305-9010
//USA
//Support/Questions: java-nlp-user@lists.stanford.edu
//Licensing: java-nlp-support@lists.stanford.edu
//http://www-nlp.stanford.edu/software/tagger.shtml


package old.edu.stanford.nlp.tagger.maxent;

import java.util.Arrays;

import old.edu.stanford.nlp.ling.Sentence;
import edu.stanford.nlp.ling.Word;


public class MaxentTagger {

    // TODO: Add a flag to lemmatize words (Morphology class) on output of tagging

    public static final String DEFAULT_NLP_GROUP_MODEL_PATH = "/u/nlp/data/pos-tagger/wsj3t0-18-left3words/left3words-wsj-0-18.tagger";
    private static boolean isInitialized; // = false;
    private static TestSentence ts;


    /**
     * Constructor for a tagger using a model stored in a particular file.
     * The <code>modelFile</code> is a filename for the model data.
     * The tagger data is loaded when the
     * constructor is called (this can be slow).
     * This constructor first constructs a TaggerConfig object, which loads
     * the tagger options from the modelFile.
     * <p>
     * The tagger does not support
     * multithreaded operation.  Since some of the data
     * for the tagger is static, two different taggers cannot exist at
     * the same time.
     *
     * @param modelFile filename of the trained model
     *
     * @throws Exception if IO problem
     */
    public MaxentTagger(String modelFile) throws Exception {
        TaggerConfig config = new TaggerConfig(new String[]{"-model", modelFile});
        init(modelFile, config);
    }

    /**
     * Constructor for a tagger using a model stored in a particular file,
     * with options taken from the supplied TaggerConfig.
     * The <code>modelFile</code> is a filename for the model data.
     * The tagger data is loaded when the
     * constructor is called (this can be slow).
     * This version assumes that the tagger options in the modelFile have
     * already been loaded into the TaggerConfig (if that is desired).
     * <p>
     * The tagger does not support
     * multithreaded operation.  Since some of the data
     * for the tagger is static, two different taggers cannot exist at
     * the same time.
     *
     * @param modelFile filename of the trained model
     * @param config    The configuration for the tagger
     *
     * @throws Exception if IO problem
     */
    public MaxentTagger(String modelFile, TaggerConfig config) throws Exception {
        init(modelFile, config);
    }

    /**
     * Static initializer that loads the tagger.  This maintains a flag as
     * to whether initialization has been done previously, and if so,
     * running this is a no-op.
     *
     * @param config    TaggerConfig based on command-line arguments
     * @param modelFile Where to initialize the tagger from.
     *                  Most commonly, this is the filename of the trained model, for example, <code>
     *                  /u/nlp/data/pos-tagger/wsj3t0-18-left3words/left3words-wsj-0-18.tagger
     *                  </code>.  However, if it starts with "https?://" it will be
     *                  interpreted as a URL, and if it starts with "jar:" it will be
     *                  taken as a resources in the /models/ path of the current jar file.
     *
     * @throws Exception if IO problem
     */
    private static void init(String modelFile, TaggerConfig config) throws Exception {
        if (!isInitialized) {
            GlobalHolder.readModelAndInit(config, modelFile, true);
            ts = new TestSentence(GlobalHolder.getLambdaSolve());
            isInitialized = true;
        }
    }

    /**
     * Tags the tokenized input string and returns the tagged version.
     * This method requires the input to already be tokenized.
     * The tagger wants input that is whitespace separated tokens, tokenized
     * according to the conventions of the training data. (For instance,
     * for the Penn Treebank, punctuation marks and possessive "'s" should
     * be separated from words.)
     *
     * @param toTag The untagged input String
     *
     * @return The same string with tags inserted in the form word/tag
     *
     * @throws Exception If there are IO errors or class initialization problems
     */
    public static synchronized String tagTokenizedString(String toTag) throws Exception {

        if (!isInitialized) {
            new MaxentTagger(DEFAULT_NLP_GROUP_MODEL_PATH); // initialize static data structures
        }

        if (isInitialized) {
            try {
                Sentence<Word> sent = Sentence.toSentence(Arrays.asList(toTag.split("\\s+")));
                ts.tagSentence(sent);
                return ts.getTaggedNice();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        return null;
    }
}
