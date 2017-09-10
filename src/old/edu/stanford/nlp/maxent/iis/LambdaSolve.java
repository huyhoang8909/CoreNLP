/**
 * Title:        StanfordMaxEnt<p>
 * Description:  A Maximum Entropy Toolkit<p>
 * Copyright:    Copyright (c) Kristina Toutanova<p>
 * Company:      Stanford University<p>
 */

package old.edu.stanford.nlp.maxent.iis;

import java.io.DataInputStream;
import java.io.IOException;

import old.edu.stanford.nlp.io.InDataStreamFile;
import old.edu.stanford.nlp.maxent.Convert;
import old.edu.stanford.nlp.maxent.Experiments;
import old.edu.stanford.nlp.maxent.Feature;
import old.edu.stanford.nlp.maxent.Problem;


/**
 * This is the main class that does the core computation in IIS.
 *
 * @author Kristina Toutanova
 * @version 1.0
 */
public class LambdaSolve {
    private static boolean smooth = false;
    private static boolean VERBOSE = false;
    /**
     * If this is true, assume that active features are binary, and one
     * does not have to multiply in a feature value.
     */
    private static boolean ASSUME_BINARY = false;
    /**
     * These are the model parameters that have to be learned
     */
    public double[] lambda;
    public boolean[] lambda_converged;
    public double eps;
    public double newtonerr;
    /**
     * This flag is true if all (x,y)  have the same f# in which case the newton equation solving is avoided
     */
    public boolean fixedFnumXY;
    public Problem p;
    public boolean convertValues = false;
    /**
     * Conditional probabilities.
     */
    protected double[][] probConds;
    /**
     * Normalization factors, one for each x.  (CDM questions 2008: Are these
     * only at training time?  Can we not allocate it at test time (unlike
     * what LambdaSolveTagger now does)?  Is the place where it is set from
     * ySize wrong?
     */
    protected double[] zlambda;
    /**
     * This contains the number of features active for each pair (x,y)
     */

    protected byte[][] fnumArr;
    /**
     * This is an array of empirical expectations for the features
     */
    protected double[] ftildeArr;


    public LambdaSolve(Problem p1, double eps1, double nerr1) {
        p = p1;
        eps = eps1;
        newtonerr = nerr1;
        lambda = new double[p1.fSize];
        lambda_converged = new boolean[p1.fSize];
        probConds = new double[Experiments.xSize][];
        System.out.println("xSize is " + Experiments.xSize);

        for (int i = 0; i < Experiments.xSize; i++) {
            probConds[i] = new double[p.data.numY(i)];
        }
        fnumArr = new byte[Experiments.xSize][];
        for (int i = 0; i < Experiments.xSize; i++) {
            fnumArr[i] = new byte[p.data.numY(i)];
        }

        zlambda = new double[Experiments.xSize];
        ftildeArr = new double[p.fSize];
        initCondsZlambdaEtc();
        if (convertValues) {
            transformValues();
        }
    }

    /**
     * Reads the lambda parameters from a file.
     *
     * @param filename File to read from
     */
    public LambdaSolve(String filename) {
        this.readL(filename);
    }

    public LambdaSolve() {
    }

    /**
     * Read the lambdas from the stream.
     *
     * @param rf Stream to read from.
     *
     * @return An array of lambda values read from the stream.
     */
    public static double[] read_lambdas(DataInputStream rf) {
        if (VERBOSE) {
            System.err.println("Entering read_lambdas");
        }
        try {
            int funsize = rf.readInt();
            byte[] b = new byte[funsize * 8];
            if (rf.read(b) != b.length) {
                System.err.println("Whoops! Incomplete read. Rewrite the code!");
            }
            rf.close();
            return Convert.byteArrToDoubleArr(b);
        } catch (IOException e) {
            e.printStackTrace();
            return null;
        }
    }

    /**
     * Given a numerator and denominator in log form, this calculates
     * the conditional model probabilities.
     *
     * @return Math.exp(first)/Math.exp(second);
     */
    public static double divide(double first, double second) {
        return Math.exp(first - second);  // cpu samples #3,#14: 5.3%
    }

    public void setBinary() {
        ASSUME_BINARY = true;
    }

    /**
     * This is a specialized procedure to change the values
     * of parses for semantic ranking.
     * The highest value is changed to 2/3
     * and values of 1 are changed to 1/(3*numones). 0 is unchanged
     * this is used to rank higher the ordering for the best parse
     * values are in p.data.values
     */
    public void transformValues() {
        for (int x = 0; x < p.data.values.length; x++) {
            double highest = p.data.values[x][0];
            double sumhighest = 0;
            double sumrest = 0;
            for (int y = 0; y < p.data.values[x].length; y++) {
                if (p.data.values[x][y] > highest) {
                    highest = p.data.values[x][y];
                }
            }

            for (int y = 0; y < p.data.values[x].length; y++) {
                if (p.data.values[x][y] == highest) {
                    sumhighest += highest;
                } else {
                    sumrest += p.data.values[x][y];
                }
            }

            if (sumrest == 0) {
                continue;
            } // do not change , makes no difference

            //now change them
            for (int y = 0; y < p.data.values[x].length; y++) {
                if (p.data.values[x][y] == highest) {
                    p.data.values[x][y] = .7 * highest / sumhighest;
                } else {
                    p.data.values[x][y] = .3 * p.data.values[x][y] / sumrest;
                }
            }
        }
    }

    /**
     * Initializes the model parameters, empirical expectations of the
     * features, and f#(x,y).
     */
    void initCondsZlambdaEtc() {
        // init pcond
        for (int x = 0; x < Experiments.xSize; x++) {
            for (int y = 0; y < probConds[x].length; y++) {
                probConds[x][y] = 1.0 / probConds[x].length;
            }
        }

        if (VERBOSE) {
            System.err.println(" pcond initialized ");
        }
        // init zlambda
        for (int x = 0; x < Experiments.xSize; x++) {
            zlambda[x] = probConds[x].length;
        }
        if (VERBOSE) {
            System.err.println(" zlambda initialized ");
        }
        // init ftildeArr
        for (int i = 0; i < p.fSize; i++) {
            ftildeArr[i] = p.functions.get(i).ftilde();
            p.functions.get(i).setSum();

            // if the expectation of a fetaure is zero make sure we are not
            // trying to find a lambda for it
            if (ftildeArr[i] == 0) {
                //lambda_converged[i]=true;
                //lambda[i]=0;
            }

            //dumb smoothing that is not sound and doesn't seem to work
            if (smooth) {
                double alfa = .015;
                for (int j = 0; j < p.fSize; j++) {
                    ftildeArr[j] = (ftildeArr[j] * Experiments.xSize + alfa) / Experiments.xSize;
                }
            }

            Feature f = p.functions.get(i);
            //collecting f#(x,y)
            for (int j = 0; j < f.len(); j++) {
                int x = f.getX(j);
                int y = f.getY(j);
                fnumArr[x][y] += f.getVal(j);
            }//j
        }//i
        int constAll = fnumArr[0][0];
        fixedFnumXY = true;
        for (int x = 0; x < Experiments.xSize; x++) {
            for (int y = 0; y < fnumArr[x].length; y++) {
                if (fnumArr[x][y] != constAll) {
                    fixedFnumXY = false;
                    break;
                }
            }
        }//x
        if (VERBOSE) {
            System.err.println(" ftildeArr initialized " + (fixedFnumXY ? "fixed sum " : ""));
        }
    }


    public double pcond(int y, int x) {
        return probConds[x][y];
    }

    protected double fnum(int x, int y) {
        return fnumArr[x][y];
    }

    double g(double lambdaP, int index) {
        double s = 0.0;

        for (int i = 0; i < p.functions.get(index).len(); i++) {
            int y = p.functions.get(index).getY(i);
            int x = p.functions.get(index).getX(i);
            double exponent = Math.exp(lambdaP * fnum(x, y));
            s = s + p.data.ptildeX(x) * pcond(y, x) * p.functions.get(index).getVal(i) * exponent;
        }
        s = s - ftildeArr[index];

        return s;
    }


    /**
     * Print out p(y|x) for all pairs to the standard output.
     */
    public void print() {
        for (int i = 0; i < Experiments.xSize; i++) {
            for (int j = 0; j < probConds[i].length; j++) {
                System.out.println("P(" + j + " | " + i + ") = " + pcond(j, i));
            }
        }
    }


    /**
     * Read the lambdas from the file.
     * The file contains the number of lambda weights (int) followed by
     * the weights.
     * <i>Historical note:</i> The file does not contain
     * xSize and ySize as for the method read(String).
     *
     * @param filename The file to read from
     */
    public void readL(String filename) {
        try {
            InDataStreamFile rf = new InDataStreamFile(filename);
            lambda = read_lambdas(rf);
            rf.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**
     * assuming we have the lambdas in the array and we need only the
     * derivatives now.
     */
    public double[] getDerivatives() {

        double[] drvs = new double[lambda.length];
        Experiments exp = p.data;

        for (int fNo = 0; fNo < drvs.length; fNo++) {  // cpu samples #2,#10,#12: 27.3%
            Feature f = p.functions.get(fNo);
            double sum = ftildeArr[fNo] * exp.getNumber();
            drvs[fNo] = -sum;
            for (int index = 0, length = f.len(); index < length; index++) {
                int x = f.getX(index);
                int y = f.getY(index);
                if (ASSUME_BINARY) {
                    drvs[fNo] += probConds[x][y] * exp.ptildeX(x) * exp.getNumber();
                } else {
                    double val = f.getVal(index);
                    drvs[fNo] += probConds[x][y] * val * exp.ptildeX(x) * exp.getNumber();
                }
            }//for
            //if(sum==0){drvs[fNo]=0;}
        }
        return drvs;
    }

}
