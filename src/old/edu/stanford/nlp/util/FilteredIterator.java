package old.edu.stanford.nlp.util;

import java.util.Arrays;
import java.util.Collection;
import java.util.Iterator;

/**
 * Iterator that suppresses items in another iterator based on a filter function.
 *
 * @author Dan Klein (klein@cs.stanford.edu)
 */
public class FilteredIterator<T> implements Iterator<T> {
  Iterator<T> iterator = null;
  Filter<T> filter = null;
  T current = null;
  boolean hasCurrent = false;

  T currentCandidate() {
    return current;
  }

  void advanceCandidate() {
    if (!iterator.hasNext()) {
      hasCurrent = false;
      current = null;
      return;
    }
    hasCurrent = true;
    current = iterator.next();
  }

  boolean hasCurrentCandidate() {
    return hasCurrent;
  }

  boolean currentCandidateIsAcceptable() {
    return filter.accept(currentCandidate());
  }

  void skipUnacceptableCandidates() {
    while (hasCurrentCandidate() && !currentCandidateIsAcceptable()) {
      advanceCandidate();
    }
  }

  public boolean hasNext() {
    return hasCurrentCandidate();
  }

  public T next() {
    T result = currentCandidate();
    advanceCandidate();
    skipUnacceptableCandidates();
    return result;
  }

  public void remove() {
    throw new UnsupportedOperationException();
  }

  public FilteredIterator(Iterator<T> iterator, Filter<T> filter) {
    this.iterator = iterator;
    this.filter = filter;
    advanceCandidate();
    skipUnacceptableCandidates();
  }
}
