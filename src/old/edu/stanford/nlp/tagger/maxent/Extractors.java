/**
 * Title:        StanfordMaxEnt<p>
 * Description:  A Maximum Entropy Toolkit<p>
 * Copyright:    Copyright (c) Kristina Toutanova<p>
 * Company:      Stanford University<p>
 */


package old.edu.stanford.nlp.tagger.maxent;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;


/** Maintains a set of feature extractors and applies them.
 *
 *  @author Kristina Toutanova
 *  @version 1.0
 */
public class Extractors implements Serializable {

  private Extractor[] v;

  private static final boolean DEBUG = false;

  volatile Map<Integer,Extractor>
    local, // extractors only looking at current word
    localContext, // extractors only looking at words, except those in "local"
    dynamic; // extractors depending on class labels

  public Extractors() {
  }

  /**
   * Set the extractors from an array.
   * @param extrs The array of extractors.  It is copied in this init.
   */
  void init(Extractor[] extrs) {
    v = new Extractor[extrs.length];
    System.arraycopy(extrs, 0, v, 0, extrs.length);
    initTypes();
  }

  /**
   * Determine type of each feature extractor.
   */
  void initTypes() {

    local = new HashMap<Integer,Extractor>();
    localContext = new HashMap<Integer,Extractor>();
    dynamic = new HashMap<Integer,Extractor>();

    for(int i=0; i<v.length; ++i) {
      Extractor e = v[i];
      if(e.isLocal() && e.isDynamic())
        throw new RuntimeException("Extractors can't both be local and dynamic!");
      if(e.isLocal()) {
        local.put(i,e);
        //localContext.put(i,e);
      } else if(e.isDynamic()) {
        dynamic.put(i,e);
      } else {
        localContext.put(i,e);
      }
    }
    if(DEBUG) {
      System.err.println("Extractors: "+this);
      System.err.printf("Local: %d extractors\n",local.size());
      System.err.printf("Local context: %d extractors\n",localContext.size());
      System.err.printf("Dynamic: %d extractors\n",dynamic.size());
    }
  }

  /**
   * Extract using the i'th extractor.
   * @param i The extractor to use
   * @param h The history to extract from
   * @return String The feature value
   */

  String extract(int i, History h) {
    return v[i].extract(h);
  }

  boolean equals(History h, History h1) {
    for (Extractor extractor : v) {
      if ( ! (extractor.extract(h).equals(extractor.extract(h1)))) {
        return false;
      }
    }
    return true;
  }


  /** Find maximum left context of extractors. Used in TagInference to decide windows for dynamic programming.
   * @return The maximum of the left contexts used by all extractors.
   */
  int leftContext() {
    int max = 0;

    for (Extractor extractor : v) {
      int lf = extractor.leftContext();
      if (lf > max) {
        max = lf;
      }
    }

    return max;
  }


  /** Find maximum right context of extractors. Used in TagInference to decide windows for dynamic programming.
   * @return The maximum of the right contexts used by all extractors.
   */
  int rightContext() {
    int max = 0;

    for (Extractor extractor : v) {
      int lf = extractor.rightContext();
      if (lf > max) {
        max = lf;
      }
    }

    return max;
  }


  public int getSize() {
    return v.length;
  }


  Extractor get(int index) {
    return v[index];
  }

  @Override
  public String toString() {
    StringBuilder sb = new StringBuilder("Extractors[");
    for (int i = 0; i < v.length; i++) {
      sb.append(v[i].toString());
      if (i < v.length - 1) {
        sb.append(", ");
      }
    }
    sb.append(']');
    return sb.toString();
  }
  private static final long serialVersionUID = -4777107742414749890L;

}
