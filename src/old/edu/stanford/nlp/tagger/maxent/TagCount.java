
/**
 * Title:        StanfordMaxEnt<p>
 * Description:  A Maximum Entropy Toolkit<p>
 * Copyright:    Copyright (c) Kristina Toutanova<p>
 * Company:      Stanford University<p>
 */

package old.edu.stanford.nlp.tagger.maxent;

import java.util.HashMap;
import java.io.DataInputStream;


/**
 * This class was created to store the possible tags of a word along with how many times
 * the word appeared with each tag.
 *
 * @author Kristina Toutanova
 * @version 1.0
 */
class TagCount {

  private HashMap<String, Integer> map = new HashMap<String, Integer>();
  private int ambClassId = -1; /* wtf this is i have no idea -wtm */

  private String[] getTagsCache; // = null;
  private boolean dirty = true;

  TagCount() { }

  private static final String NULL_SYMBOL = "<<NULL>>";


  public void setAmbClassId(int ambClassId) {
    this.ambClassId = ambClassId;
  }


  // The object's fields are read form the file. They are read from the current position and the
  // file is not closed afterwards.
  protected void read(DataInputStream rf) {
    try {

      int numTags = rf.readInt();
      map = new HashMap<String, Integer>(numTags);

      for (int i = 0; i < numTags; i++) {
	String tag = rf.readUTF();
        int count = rf.readInt();

	if (tag.equals(NULL_SYMBOL)) tag = null;
	map.put(tag, count);
      }
      dirty = true;
    } catch (Exception e) {
      e.printStackTrace();
    }
  }

  /**
   * @return the number of total occurrences of the word .
   */
  protected int sum() {
    int s = 0;
    for (Integer i : map.values()) {
      s += i;
    }
    return s;
  }

  // Add a tag to the list. If the tag is already there, increment its count.
  // Otherwise, add it and set its count to 1.
  protected void add(String tag) {
    int val;

    if (map.get(tag) != null) {
      val = map.get(tag);
    } else {
      val = 0;
      dirty = true;  // the set of tags has changed
    }

    map.put(tag, val + 1);
  }

  // Returns the number of occurrence of a particular tag. Not very efficient implementation.
  protected int get(String tag) {
    if(map.get(tag) != null) return map.get(tag);
    else return 0;
  }

  /**
   * @return an array of the tags the word has had.
   */
  public String[] getTags() {
    if (dirty) {
      getTagsCache = map.keySet().toArray(new String[map.keySet().size()]);
      dirty = false;
    }
    return getTagsCache; //map.keySet().toArray(new String[0]);
  }


  protected int numTags() { return map.size(); }


  @Override
  public String toString() {
    return map.toString();
  }

}

