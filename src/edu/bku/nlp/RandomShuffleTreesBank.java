package edu.bku.nlp;

import java.io.BufferedOutputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Arrays;
import java.util.Iterator;
import java.util.Random;

import edu.stanford.nlp.parser.lexparser.LexicalizedParser;
import edu.stanford.nlp.parser.lexparser.Options;
import edu.stanford.nlp.parser.lexparser.TreebankLangParserParams;
import edu.stanford.nlp.trees.Tree;
import edu.stanford.nlp.trees.Treebank;
import edu.stanford.nlp.util.logging.Redwood;

import static edu.stanford.nlp.util.logging.Redwood.log;

public class RandomShuffleTreesBank {
    private static final Redwood.RedwoodChannels log = Redwood.channels(RandomShuffleTreesBank.class);

    static int total = 0;

    public static void main(String[] args) throws ClassNotFoundException, IllegalAccessException, InstantiationException {
        String treebankPath = null;
        String outputPath = null;
        int argIndex = 0;
        if (args.length != 6) {
            log.info("Basic usage (see Javadoc for more): java edu.stanford.nlp.parser.lexparser.LexicalizedParser parserFileOrUrl filename*");
            return;
        }

        Options op = new Options();
        while (argIndex < args.length && args[argIndex].charAt(0) == '-') {
            if (args[argIndex].equalsIgnoreCase("-train")) {
                treebankPath = args[argIndex + 1];
            } else if (args[argIndex].equalsIgnoreCase("-output")) {
                outputPath = args[argIndex + 1];
            } else if (args[argIndex].equalsIgnoreCase("-tLPP") && (argIndex + 1 < args.length)) {
                op.tlpParams = (TreebankLangParserParams) Class.forName(args[argIndex + 1]).newInstance();
            }

            argIndex += 2;
        } // end while loop through arguments


        Treebank trees = LexicalizedParser.LoadTreeBank(treebankPath, op);

        Iterator<Tree> iterator = trees.iterator();
        Tree[] treeArray = new Tree[trees.size()];

        int counter = 0;
        while (iterator.hasNext()) {
            Tree next = iterator.next();
            treeArray[counter] = next;
            counter++;
        }

        ShuffleArray(treeArray);

        int totalTree = treeArray.length;
        long trainTreeNumber = Math.round(treeArray.length * 0.9);
        long devTreeNumber = Math.round(treeArray.length * 0.05);

        // Start to save tree to file
        int chunkSize = 40;
        int chunkCount = 0;

        for (int index = 0; index < totalTree; index++) {
            chunkCount++;

            if (chunkCount < chunkSize && index == trainTreeNumber - 1) {
                saveTreeBankToFiles(Arrays.copyOfRange(treeArray, index, index + chunkCount), outputPath + "/train/" + index + ".mrg");
                chunkCount = 0;
            } else if (chunkCount == chunkSize && index < trainTreeNumber - 1) {
                // saveTrainingTree
                saveTreeBankToFiles(Arrays.copyOfRange(treeArray, index, index + chunkCount), outputPath + "/train/" + index + ".mrg");
                chunkCount = 0;
            } else if (chunkCount < chunkSize && index == (trainTreeNumber + devTreeNumber - 1)) {
                // save devTree
                saveTreeBankToFiles(Arrays.copyOfRange(treeArray, index, index + chunkCount), outputPath + "/dev/" + index + ".mrg");
                chunkCount = 0;
            } else if (chunkCount == chunkSize && index < (trainTreeNumber + devTreeNumber - 1)) {
                saveTreeBankToFiles(Arrays.copyOfRange(treeArray, index, index + chunkCount), outputPath + "/dev/" + index + ".mrg");
                chunkCount = 0;
            } else if (chunkCount < chunkSize && index == totalTree - 1) {
                saveTreeBankToFiles(Arrays.copyOfRange(treeArray, index, totalTree - 1), outputPath + "/test/" + index + ".mrg");
                chunkCount = 0;
                // save testTree
            } else if (chunkCount == chunkSize && index < totalTree - 1) {
                // save testTree
                saveTreeBankToFiles(Arrays.copyOfRange(treeArray, index, totalTree - 1), outputPath + "/test/" + index + ".mrg");
                chunkCount = 0;
            }

        }
    }

    private static void ShuffleArray(Tree[] treeArray) {
        // If running on Java 6 or older, use `new Random()` on RHS here
        Random rnd = new Random();
        for (int i = treeArray.length - 1; i > 0; i--) {
            int index = rnd.nextInt(i + 1);
            // Simple swap
            Tree a = treeArray[index];
            treeArray[index] = treeArray[i];
            treeArray[i] = a;
        }
    }

    private static void saveTreeBankToFiles(Tree[] trees, String filename) {
        try {
            BufferedOutputStream os = new BufferedOutputStream(new FileOutputStream(filename));
            PrintWriter out = new PrintWriter(os);

            for (Tree tree : trees) {
                out.write(tree.pennString());
                out.write("\n");
            }

            os.flush();
            out.close();
        } catch (IOException e) {
            log(e);
        }


    }
}
