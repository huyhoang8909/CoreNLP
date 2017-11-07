package old.edu.stanford.nlp.util;

import java.util.*;

/**
 * Class for memoizing the result of string concatenations.
 * This class is probably a better choice than StringBuilder
 * and StringBuffer if many string concatenations are identical.
 * 
 * @author Michel Galley
 */
public class StringBuildMemoizer {

  private static int stringAllowance = 8; // reduces default 16 -> 8

  private static Map<ArrayWrapper<String>,String> m
    = new HashMap<ArrayWrapper<String>,String>();

  public static String toString(String... arr) {
    ArrayWrapper<String> aw = new ArrayWrapper<String>(arr);
    String oldstr = m.get(aw);
    if(oldstr != null)
      return oldstr;
    StringBuilder sb = new StringBuilder(stringAllowance);
    for(String s : arr)
      sb.append(s);
    String newstr = sb.toString();
    m.put(aw,newstr);
    return newstr;
  }
}
