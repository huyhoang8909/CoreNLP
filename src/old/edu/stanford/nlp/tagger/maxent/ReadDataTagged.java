/**
 * Title:        StanfordMaxEnt<p>
 * Description:  A Maximum Entropy Toolkit<p>
 * Copyright:    Copyright (c) Kristina Toutanova<p>
 * Company:      Stanford University<p>
 */
package old.edu.stanford.nlp.tagger.maxent;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.StringTokenizer;

import old.edu.stanford.nlp.ling.WordTag;


/**
 * Reads tagged data from a file and creates a dictionary.
 * The tagged data has to be whitespace-separated items, with the word and
 * tag split off by a delimiter character, which is found as the last instance
 * of the delimiter character in the item.
 *
 * @author Kristina Toutanova
 * @version 1.0
 */
public class ReadDataTagged {

    private static final String eosWord = "EOS";
    private static final String eosTag = "EOS";
    private final String filename;
    private final PairsHolder pairs;
    private ArrayList<DataWordTag> v = new ArrayList<DataWordTag>();
    private int numElements = 0;
    //TODO: make a class DataHolder that holds the dict, tags, pairs, etc, for tagger
    // and pass it around

    protected ReadDataTagged(TaggerConfig config, PairsHolder pairs) {
        this.pairs = pairs;
        this.filename = config.getFile();
        try {
            if (config.getInitFromTrees()) {
                initFromTrees(config);
            } else {
                init(config.getTagSeparator(), config.getEncoding());
            }
        } catch (Exception e) {
            System.err.println("Error reading data from " + filename);
            e.printStackTrace();
        }
    }


    /**
     * Frees the memory that is stored in this object by dropping the word-tag data.
     */
    void release() {
        v = null;
    }


    DataWordTag get(int index) {
        return v.get(index);
    }

    private void initFromTrees(TaggerConfig config) throws Exception {
    }


    // Read the data.
    private void init(String tagSeparator, String encoding) throws IOException {
        ArrayList<String> words = new ArrayList<String>();
        ArrayList<String> tags = new ArrayList<String>();
        int numSentences = 0;
        int numWords = 0;
        int endPos = 0;
        int prevPos = 0;
        BufferedReader in = new BufferedReader(new InputStreamReader(new FileInputStream(filename), encoding));

        int maxLen = Integer.MIN_VALUE;
        int minLen = Integer.MAX_VALUE;

        //loop over sentences
        for (String s; (s = in.readLine()) != null; ) {
            StringTokenizer st = new StringTokenizer(s);
            //loop over words in a single sentence

            while (st.hasMoreTokens()) {
                String token = st.nextToken();
                numWords++;
                int indexUnd = token.lastIndexOf(tagSeparator);
                if (indexUnd < 0) {
                    throw new RuntimeException("Data format error: can't find delimiter \"" + tagSeparator + "\" in word \"" + token + "\" (line " + numSentences + " of " + filename + ')');
                }
                String word = token.substring(0, indexUnd).intern();
                String tag = token.substring(indexUnd + 1).intern();
                words.add(word);
                tags.add(tag);
                if (!GlobalHolder.tagTokens.containsKey(tag)) {
                    GlobalHolder.tagTokens.put(tag, new HashSet<String>());
                }
                GlobalHolder.tagTokens.get(tag).add(word);
                endPos++;
            }

            if (endPos > maxLen) maxLen = endPos;
            if (endPos < minLen) minLen = endPos;

            // add the EOS as well
            words.add(eosWord);
            tags.add(eosTag);
            numElements = numElements + endPos + 1;

            // iterate over the words in the sentence
            for (int i = 0; i < endPos + 1; i++) {
                History h = new History(prevPos, prevPos + endPos, prevPos + i, pairs);
                String tag = tags.get(i);
                String word = words.get(i);
                pairs.add(new WordTag(word, tag));
                int y = GlobalHolder.tags.add(tag);
                DataWordTag dat = new DataWordTag(h, y);
                v.add(dat);
                GlobalHolder.dict.add(word, tag);
            }

            numSentences++;
            prevPos += endPos + 1;
            endPos = 0;
            words.clear();
            tags.clear();
            if ((numSentences % 100000) == 0)
                System.err.println("Read " + numSentences + " sentences, min " + minLen + " words, max " + maxLen + " words ... [still reading]");
        }

        in.close();
        System.err.println("Read " + numWords + " words from " + filename + " [done].");
        System.err.println("Read " + numSentences + " sentences, min " + minLen + " words, max " + maxLen + " words.");
    }

    /**
     * Returns the number of tokens in the data read, which is the number of words
     * plus one end sentence token per sentence.
     *
     * @return The number of tokens in the data
     */
    public int getSize() {
        return numElements;
    }
}
