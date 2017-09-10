/**
 * Title:        StanfordMaxEnt<p>
 * Description:  A Maximum Entropy Toolkit<p>
 * Copyright:    Copyright (c) Kristina Toutanova<p>
 * Company:      Stanford University<p>
 */
package old.edu.stanford.nlp.tagger.maxent;

import java.io.DataInputStream;
import java.io.IOException;
import java.util.HashMap;

import old.edu.stanford.nlp.io.InDataStreamFile;
import old.edu.stanford.nlp.io.OutDataStreamFile;


/**
 * Maintains a map from words to tags and their counts.
 *
 * @author Kristina Toutanova
 * @version 1.0
 */
public class Dictionary {

    private static final String naWord = "NA";
    private final HashMap<String, TagCount> dict = new HashMap<String, TagCount>();
    private final HashMap<Integer, CountWrapper> partTakingVerbs = new HashMap<Integer, CountWrapper>();

    public Dictionary() {
    }

    protected void add(String word, String tag) {
        if (dict.containsKey(word)) {
            TagCount cT = dict.get(word);
            cT.add(tag);
            return;
        }
        TagCount cT = new TagCount();
        cT.add(tag);
        dict.put(word, cT);
    }


    protected int getCount(String word, String tag) {
        TagCount tc = dict.get(word);
        if (tc == null) {
            return 0;
        } else {
            return tc.get(tag);
        }
    }

    protected String[] getTags(String word) {
        TagCount tC = get(word);
        if (tC == null) {
            return null;
        }
        return tC.getTags();
    }

    protected TagCount get(String word) {
        return dict.get(word);
    }

    protected int sum(String word) {
        if (dict.containsKey(word)) {
            return dict.get(word).sum();
        }
        return 0;
    }


  /*
  public void save(String filename) {
    try {
      OutDataStreamFile rf = new OutDataStreamFile(filename);
      save(rf);
      rf.close();
    } catch (Exception e) {
      e.printStackTrace();
    }
  }
  */

    boolean isUnknown(String word) {
        return !dict.containsKey(word);
    }


    private void read(DataInputStream rf, String filename) throws IOException {
        // Object[] arr=dict.keySet().toArray();

        int maxNumTags = 0;
        int len = rf.readInt();
        if (TestSentence.VERBOSE) {
            System.err.println("Reading Dictionary of " + len + " words from " + filename + '.');
        }

        for (int i = 0; i < len; i++) {
            String word = rf.readUTF();
            TagCount tC = new TagCount();
            tC.read(rf);
            int numTags = tC.numTags();
            if (numTags > maxNumTags) {
                maxNumTags = numTags;
            }
            this.dict.put(word, tC);
            if (TestSentence.VERBOSE) {
                System.err.println("  " + word + " [idx=" + i + "]: " + tC);
            }
        }
        if (TestSentence.VERBOSE) {
            System.err.println("Read dictionary of " + len + " words; max tags for word was " + maxNumTags + '.');
        }
    }

    private void readTags(DataInputStream rf) throws IOException {
        // Object[] arr=dict.keySet().toArray();

        int maxNumTags = 0;
        int len = rf.readInt();
        if (TestSentence.VERBOSE) {
            System.err.println("Reading Dictionary of " + len + " words.");
        }

        for (int i = 0; i < len; i++) {
            String word = rf.readUTF();
            TagCount tC = new TagCount();
            tC.read(rf);
            int numTags = tC.numTags();
            if (numTags > maxNumTags) {
                maxNumTags = numTags;
            }
            this.dict.put(word, tC);
            if (TestSentence.VERBOSE) {
                System.err.println("  " + word + " [idx=" + i + "]: " + tC);
            }
        }
        if (TestSentence.VERBOSE) {
            System.err.println("Read dictionary of " + len + " words; max tags for word was " + maxNumTags + '.');
        }
    }

    protected void read(String filename) {
        try {
            InDataStreamFile rf = new InDataStreamFile(filename);
            read(rf, filename);

            int len1 = rf.readInt();
            for (int i = 0; i < len1; i++) {
                int iO = rf.readInt();
                CountWrapper tC = new CountWrapper();
                tC.read(rf);

                this.partTakingVerbs.put(iO, tC);
            }
            rf.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    protected void read(DataInputStream file) {
        try {
            readTags(file);

            int len1 = file.readInt();
            for (int i = 0; i < len1; i++) {
                int iO = file.readInt();
                CountWrapper tC = new CountWrapper();
                tC.read(file);

                this.partTakingVerbs.put(iO, tC);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    /**
     * This makes ambiguity classes from all words in the dictionary and remembers
     * their classes in the TagCounts
     */

    protected void setAmbClasses() {
        String[] arr = dict.keySet().toArray(new String[dict.keySet().size()]);
        for (String w : arr) {
            int ambClassId = GlobalHolder.ambClasses.getClass(w);
            dict.get(w).setAmbClassId(ambClassId);
        }
    }

}
