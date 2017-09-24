/**
 * Title:       Stanford JavaNLP.
 * Description: A Maximum Entropy Toolkit.
 * Copyright:   Copyright (c) 2000. Kristina Toutanova, Stanford University
 * Company:     Stanford University, All Rights Reserved.
 */
package old.edu.stanford.nlp.tagger.maxent;

import edu.stanford.nlp.util.MutableDouble;
import old.edu.stanford.nlp.maxent.Experiments;
import old.edu.stanford.nlp.maxent.Problem;
import old.edu.stanford.nlp.maxent.iis.LambdaSolve;

import java.io.DataInputStream;


/**
 * This module does the working out of lambda parameters for binary tagger
 * features.  It can use either IIS or CG.
 *
 * @author Kristina Toutanova
 * @version 1.0
 */
public class LambdaSolveTagger extends LambdaSolve {

  /**
   * Suppress extraneous printouts
   */
  @SuppressWarnings("unused")
  //private static final boolean VERBOSE = false;


  LambdaSolveTagger(Problem p1, double eps1, double nerr1) {
    p = p1;
    eps = eps1;
    newtonerr = nerr1;
    lambda = new double[p1.fSize];
    lambda_converged = new boolean[p1.fSize];
    probConds = new double[Experiments.xSize][Experiments.ySize];  // cdm 2008: Memory hog. Is there anything we can do to avoid this square array allocation?
    fnumArr = GlobalHolder.fnumArr;
    zlambda = new double[Experiments.xSize];
    ftildeArr = new double[p.fSize];
    initCondsZlambdaEtc();
    super.setBinary();
  }


  LambdaSolveTagger(DataInputStream dataStream) {
    lambda = read_lambdas(dataStream);
    super.setBinary();
  }

  void initCondsZlambdaEtc() {
    // init pcond
    for (int x = 0; x < Experiments.xSize; x++) {
      for (int y = 0; y < Experiments.ySize; y++) {
        probConds[x][y] = 1.0 / Experiments.ySize;
      }
    }
    System.out.println(" pcond initialized ");
    // init zlambda
    for (int x = 0; x < Experiments.xSize; x++) {
      zlambda[x] = Experiments.ySize;
    }
    System.out.println(" zlambda initialized ");
    // init ftildeArr
    for (int i = 0; i < p.fSize; i++) {
      ftildeArr[i] = p.functions.get(i).ftilde();
      if (ftildeArr[i] == 0) {
        System.out.println(" Empirical expectation 0 for feature " + i);
      }
    }
    System.out.println(" ftildeArr initialized ");
  }


  /**
   * Iteration for lambda[index].
   *
   * @return true if this lambda hasn't converged.
   */
  @SuppressWarnings({"UnusedDeclaration"})
  boolean iterate(int index, double err, MutableDouble ret) {
    double deltaL = 0.0;
    deltaL = newton(deltaL, index, err);
    lambda[index] = lambda[index] + deltaL;
    if (!(deltaL == deltaL)) {
      System.out.println(" NaN " + index + ' ' + deltaL);
    }
    ret.set(deltaL);
    return (Math.abs(deltaL) >= eps);
  }


  /*
   * Finds the root of an equation by Newton's method. This is my
   * implementation. It might be improved if we looked at some official
   * library for numerical methods.
   */
  double newton(double lambda0, int index, double err) {
    double lambdaN = lambda0;
    int i = 0;
    do {
      i++;
      double lambdaP = lambdaN;
      double gPrimeVal = gprime(lambdaP, index);
      if (!(gPrimeVal == gPrimeVal)) {
        System.out.println("gPrime of " + lambdaP + ' ' + index + " is NaN " + gPrimeVal);
      }
      double gVal = g(lambdaP, index);
      if (gPrimeVal == 0.0) {
        return 0.0;
      }
      lambdaN = lambdaP - gVal / gPrimeVal;
      if (!(lambdaN == lambdaN)) {
        System.out.println("the division of " + gVal + ' ' + gPrimeVal + ' ' + index + " is NaN " + lambdaN);
        return 0;
      }
      if (Math.abs(lambdaN - lambdaP) < err) {
        return lambdaN;
      }
      if (i > 100) {
        if (Math.abs(gVal) > 1) {
          return 0;
        }
        return lambdaN;
      }
    } while (true);
  }

  double g(double lambdaP, int index) {
    double s = 0.0;
    for (int i = 0; i < p.functions.get(index).len(); i++) {
      int y = ((TaggerFeature) p.functions.get(index)).getYTag();
      int x = (p.functions.get(index)).getX(i);
      s = s + p.data.ptildeX(x) * pcond(y, x) * 1 * Math.exp(lambdaP * fnum(x, y));
    }
    s = s - ftildeArr[index];

    return s;
  }

  double gprime(double lambdaP, int index) {
    double s = 0.0;
    for (int i = 0; i < p.functions.get(index).len(); i++) {
      int y = ((TaggerFeature) (p.functions.get(index))).getYTag();
      int x = (p.functions.get(index)).getX(i);
      s = s + p.data.ptildeX(x) * pcond(y, x) * 1 * Math.exp(lambdaP * fnum(x, y)) * fnum(x, y);
    }
    return s;
  }

}

