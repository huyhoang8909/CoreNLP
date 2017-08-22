package edu.bku.nlp;

import edu.stanford.nlp.ling.HasWord;
import edu.stanford.nlp.process.AbstractTokenizer;

public class VietnameseTokenizer<T extends HasWord> extends AbstractTokenizer<T> {

    @Override
    protected T getNext() {
        return null;
    }
}
