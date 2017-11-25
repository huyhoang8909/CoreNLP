Stanford CoreNLP
================

[![Build Status](https://travis-ci.org/stanfordnlp/CoreNLP.svg?branch=master)](https://travis-ci.org/stanfordnlp/CoreNLP)

Stanford CoreNLP provides a set of natural language analysis tools written in Java. It can take raw human language text input and give the base forms of words, their parts of speech, whether they are names of companies, people, etc., normalize and interpret dates, times, and numeric quantities, mark up the structure of sentences in terms of phrases or word dependencies, and indicate which noun phrases refer to the same entities. It was originally developed for English, but now also provides varying levels of support for (Modern Standard) Arabic, (mainland) Chinese, French, German, and Spanish. Stanford CoreNLP is an integrated framework, which make it very easy to apply a bunch of language analysis tools to a piece of text. Starting from plain text, you can run all the tools with just two lines of code. Its analyses provide the foundational building blocks for higher-level and domain-specific text understanding applications. Stanford CoreNLP is a set of stable and well-tested natural language processing tools, widely used by various groups in academia, industry, and government. The tools variously use rule-based, probabilistic machine learning, and deep learning components.

The Stanford CoreNLP code is written in Java and licensed under the GNU General Public License (v3 or later). Note that this is the full GPL, which allows many free uses, but not its use in proprietary software that you distribute to others.

#### Build Instructions

Several times a year we distribute a new version of the software, which corresponds to a stable commit.

During the time between releases, one can always use the latest, under development version of our code.

Here are some helpful instructions to use the latest code:

#### provided build
Periodically we will provide updated jars here which have the latest version of the code:

[stanford-corenlp.jar (last built: 2017-04-14)](http://nlp.stanford.edu/software/stanford-corenlp-2017-04-14-build.jar)

#### build with Ant

1. Make sure you have Ant installed, details here: [http://ant.apache.org/](http://ant.apache.org/)
2. Compile the code with this command: `cd CoreNLP ; ant`
3. Then run this command to build a jar with the latest version of the code: `cd CoreNLP/classes ; jar -cf ../stanford-corenlp.jar edu`
4. This will create a new jar called stanford-corenlp.jar in the CoreNLP folder which contains the latest code
5. The dependencies that work with the latest code are in CoreNLP/lib and CoreNLP/liblocal, so make sure to include those in your CLASSPATH.
6. When using the latest version of the code make sure to download the latest versions of the [corenlp-models](http://nlp.stanford.edu/software/stanford-corenlp-models-current.jar), [english-models](http://nlp.stanford.edu/software/stanford-english-corenlp-models-current.jar), and [english-models-kbp](http://nlp.stanford.edu/software/stanford-english-kbp-corenlp-models-current.jar) and include them in your CLASSPATH.  If you are processing languages other than English, make sure to download the latest version of the models jar for the language you are interested in.

#### build with Maven

1. Make sure you have Maven installed, details here: [https://maven.apache.org/](https://maven.apache.org/)
2. If you run this command in the CoreNLP directory: `mvn package` , it should run the tests and build this jar file: `CoreNLP/target/stanford-corenlp-3.7.0.jar`
3. When using the latest version of the code make sure to download the latest versions of the [corenlp-models](http://nlp.stanford.edu/software/stanford-corenlp-models-current.jar), [english-models](http://nlp.stanford.edu/software/stanford-english-corenlp-models-current.jar), and [english-models-kbp](http://nlp.stanford.edu/software/stanford-english-kbp-corenlp-models-current.jar) and include them in your CLASSPATH.  If you are processing languages other than English, make sure to download the latest version of the models jar for the language you are interested in.  
4. If you want to use Stanford CoreNLP as part of a Maven project you need to install the models jars into your Maven repository.  Below is a sample command for installing the Spanish models jar.  For other languages just change the language name in the command.  To install `stanford-corenlp-models-current.jar` you will need to set `-Dclassifier=models`.  Here is the sample command for Spanish: `mvn install:install-file -Dfile=/location/of/stanford-spanish-corenlp-models-current.jar -DgroupId=edu.stanford.nlp -DartifactId=stanford-corenlp -Dversion=3.7.0 -Dclassifier=models-spanish -Dpackaging=jar` 

You can find releases of Stanford CoreNLP on [Maven Central](https://search.maven.org/#artifactdetails%7Cedu.stanford.nlp%7Cstanford-corenlp%7C3.7.0%7Cjar).

You can find more explanation and documentation on [the Stanford CoreNLP homepage](http://nlp.stanford.edu/software/corenlp.shtml#Demo).

The most recent models associated with the code in the HEAD of this repository can be found [here](http://nlp.stanford.edu/software/stanford-corenlp-models-current.jar).

Some of the larger (English) models -- like the shift-reduce parser and WikiDict -- are not distributed with our default models jar.
The most recent version of these models can be found [here](http://nlp.stanford.edu/software/stanford-english-corenlp-models-current.jar).

We distribute resources for other languages as well, including [Arabic models](http://nlp.stanford.edu/software/stanford-arabic-corenlp-models-current.jar),
[Chinese models](http://nlp.stanford.edu/software/stanford-chinese-corenlp-models-current.jar),
[French models](http://nlp.stanford.edu/software/stanford-french-corenlp-models-current.jar),
[German models](http://nlp.stanford.edu/software/stanford-german-corenlp-models-current.jar),
and [Spanish models](http://nlp.stanford.edu/software/stanford-spanish-corenlp-models-current.jar).

For information about making contributions to Stanford CoreNLP, see the file [CONTRIBUTING.md](CONTRIBUTING.md).

Questions about CoreNLP can either be posted on StackOverflow with the tag [stanford-nlp](http://stackoverflow.com/questions/tagged/stanford-nlp),
  or on the [mailing lists](http://nlp.stanford.edu/software/corenlp.shtml#Mail).
  
## Input
* Training Treebank has 9046 trees (8312 of length <= 40) and 194969 words (tokens)
PCFG parsing 7 words (incl. stop): insideScore = -66.84719

* Dev Treebank has 453 trees (421 of length <= 40) and 10148 words (tokens)

* Testing Treebank has 900 trees (816 of length <= 40) and 20166 words (tokens)

## Output
##### without config any parameters
* dev
    * pcfg LP/LR summary evalb: LP: 56.05 LR: 56.2 F1: **56.12** Exact: 7.28 N: 453
* test
    * pcfg LP/LR summary evalb: LP: 58.08 LR: 59.04 F1: **58.55** Exact: 8.88 N: 900

##### markUnaryTags is true (at training time not test time)
```$xslt
  /** Mark POS tags which are the sole member of their phrasal constituent.
  *  This is like markUnary=2, applied to POS tags.
  */
  public boolean markUnaryTags = true;
```

* dev
    * pcfg LP/LR summary evalb: LP: 57.15 LR: 56.61 F1: **56.88** Exact: 7.72 N: 453
* test
    * pcfg LP/LR summary evalb: LP: 58.09 LR: 59.74 F1: **58.9** Exact: 8.88 N: 900

##### Limit sentence length <= 40
* dev
    * pcfg LP/LR summary evalb: LP: 58.63 LR: 57.58 F1: **58.1** Exact: 8.31 N: 421
* test
    * pcfg LP/LR summary evalb: LP: 60.66 LR: 62.05 F1: **61.35** Exact: 9.8 N: 816
