package old.edu.stanford.nlp.tagger.maxent;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Set;


public class CtbDict {

    private static final String defaultFilename = "ctb_dict.txt";
    public HashMap<String, Set<String>> ctb_pre_dict;
    public HashMap<String, Set<String>> ctb_suf_dict;
    private CtbDict() {
        try {
            readCtbDict("/u/nlp/data/pos-tagger/dictionary" + '/' + defaultFilename);
        } catch (IOException e) {
            throw new RuntimeException("can't open file: " + e.getMessage());
      /* java sucks */
        }
    }

    private void readCtbDict(String filename) throws IOException {
        BufferedReader ctbDetectorReader = new BufferedReader(new InputStreamReader(new FileInputStream(filename), "GB18030"));
        String ctbDetectorLine;

        ctb_pre_dict = new HashMap<String, Set<String>>();
        ctb_suf_dict = new HashMap<String, Set<String>>();

        while ((ctbDetectorLine = ctbDetectorReader.readLine()) != null) {
            String[] fields = ctbDetectorLine.split("	");
            String tag = fields[0];
            Set<String> pres = ctb_pre_dict.get(tag);
            Set<String> sufs = ctb_suf_dict.get(tag);

            if (pres == null) {
                pres = new HashSet<String>();
                ctb_pre_dict.put(tag, pres);
            }
            pres.add(fields[1]);

            if (sufs == null) {
                sufs = new HashSet<String>();
                ctb_suf_dict.put(tag, sufs);
            }
            sufs.add(fields[2]);


        }
    }//try

}//class
