/**
 * Title:        StanfordMaxEnt<p>
 * Description:  A Maximum Entropy Toolkit<p>
 * Copyright:    Copyright (c) Kristina Toutanova<p>
 * Company:      Stanford University<p>
 */
package old.edu.stanford.nlp.tagger.maxent;

import java.io.DataInputStream;
import java.io.IOException;


/**
 * A simple data structure for some tag counts.
 *
 * @author Kristina Toutanova
 * @version 1.0
 */
public class CountWrapper {

    private String word;
    //private Dictionary dictLocal = new Dictionary();
    //private static final String rpTag = "RP";
    //private static final String inTag = "IN";
    //private static final String rbTag = "RB";

    public CountWrapper() {
    }

    protected CountWrapper(String word, int countPart, int countThat, int countIn, int countRB) {
        assert (word != null);
        this.word = word;

    }

    public String getWord() {
        return word;
    }

    @Override
    public int hashCode() {
        return word.hashCode();
    }

    /**
     * Equality is tested only on the word, and not the various counts
     * that are maintained.
     *
     * @param obj Item tested for equality
     *
     * @return Whether equal
     */
    @Override
    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof CountWrapper)) {
            return false;
        }
        CountWrapper cw = (CountWrapper) obj;
        return word.equals(cw.word);
    }

    protected void read(DataInputStream rf) {
        try {
            int len = rf.readInt();
            byte[] buff = new byte[len];
            if (rf.read(buff) != len) {
                System.err.println("Error: rewrite CountWrapper.read");
            }
            word = new String(buff);
            assert (word != null);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }


}


