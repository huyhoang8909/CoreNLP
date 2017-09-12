
# Installation
## Install CoreNLP model
Include [CoreNLP model](https://repo1.maven.org/maven2/edu/stanford/nlp/stanford-corenlp/3.8.0/stanford-corenlp-3.8.0-models.jar) to your classpath

## Include the library to your classpath to make vnTokenizer work

```
lib/commons-cli-1.2.jar
lib/maxent-2.5.2.jar
lib/opennlp-tools-1.4.3.jar
lib/trove.jar
lib/vn.hus.nlp.fsm-1.0.0.jar
lib/vn.hus.nlp.sd-2.0.0.jar
lib/vn.hus.nlp.utils-1.0.0.jar
```
# Parser metric
Evalb LP/LR summary evalb: LP: XX LR: XX F1: XX Exact: XX N: XX where
LP is precision
LR is recall
F1 is F1
Exact is the percentage of trees that matched exactly between the system and the gold
N the number of sentences

# Run sentiment analysis

```
java -cp "*" -Xmx2g edu.stanford.nlp.pipeline.StanfordCoreNLP -props test-data/custom-sentiment.properties -file test-data/sentiment.txt
```
