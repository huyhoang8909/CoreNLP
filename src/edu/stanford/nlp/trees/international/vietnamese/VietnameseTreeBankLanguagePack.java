package edu.stanford.nlp.trees.international.vietnamese;

import edu.stanford.nlp.ling.CoreLabel;
import edu.stanford.nlp.process.PTBTokenizer;
import edu.stanford.nlp.process.TokenizerFactory;
import edu.stanford.nlp.trees.AbstractTreebankLanguagePack;
import edu.stanford.nlp.trees.EnglishGrammaticalStructureFactory;
import edu.stanford.nlp.trees.GrammaticalStructureFactory;
import edu.stanford.nlp.trees.HeadFinder;
import edu.stanford.nlp.trees.UniversalEnglishGrammaticalStructureFactory;
import edu.stanford.nlp.trees.UniversalSemanticHeadFinder;

public class VietnameseTreeBankLanguagePack extends AbstractTreebankLanguagePack {
    public static final String[] pennPunctTags = {"''", "``", "-LRB-", "-RRB-", ".", ":", ","};

    /**
     * S: Câu Trần Thuật, khẳng định hoặc phủ định
     * SQ: Câu hỏi
     * S-EXC: Câu cảm thán
     * S-CMD: Câu mệnh lệnh
     * SBAR: Mệnh đề phụ kết (bổ nghĩa cho danh từ, động từ và tính từ.)
     */
    private static final String[] vnStartSymbols = {"S", "S-EXC", "SQ", "S-CMD", "SBAR"};
    private static final String[] vnSFPunctWords = {".", "!", "?", "..."};
    private static final String[] vnSFPunctTags = {".", "!", ",", "...", ":", "?", "\"", "-"};
    private static final String[] collinsPunctTags = {"''", "``", ".", ":", ","};

    private static final String[] vnPunctWords = {"''", "'", "``", "`", "-LRB-", "-RRB-", "-LCB-", "-RCB-", ".", "?", "!", ",", ":", "-", "--", "...", ";"};
    /**
     * The first 3 are used by the Penn Treebank; # is used by the
     * BLLIP corpus, and ^ and ~ are used by Klein's lexparser.
     * Teg added _ (let me know if it hurts).
     * John Bauer added [ on account of category annotations added when
     * printing out lexicalized dependencies.  Note that ] ought to be
     * unnecessary, since it would end the annotation, not start it.
     */
    private static final char[] annotationIntroducingChars = {'-', '=', '|', '#', '^', '~', '_', '['};

    /**
     * Gives a handle to the VietnameseTreeBankLanguagePack
     */
    public VietnameseTreeBankLanguagePack() {
    }


    /**
     * Returns a String array of punctuation tags for this treebank/language.
     *
     * @return The punctuation tags
     */
    @Override
    public String[] punctuationTags() {
        return pennPunctTags;
    }

    /**
     * Returns a String array of punctuation words for this treebank/language.
     *
     * @return The punctuation words
     */
    @Override
    public String[] punctuationWords() {
        return vnPunctWords;
    }

    /**
     * Returns a String array of sentence final punctuation tags for this
     * treebank/language.
     *
     * @return The sentence final punctuation tags
     */
    @Override
    public String[] sentenceFinalPunctuationTags() {
        return vnSFPunctTags;
    }

    /**
     * Returns a String array of sentence final punctuation words for this
     * treebank/language.
     *
     * @return The sentence final punctuation tags
     */
    @Override
    public String[] sentenceFinalPunctuationWords() {
        return vnSFPunctWords;
    }

    /**
     * Returns a String array of punctuation tags that EVALB-style evaluation
     * should ignore for this treebank/language.
     * Traditionally, EVALB has ignored a subset of the total set of
     * punctuation tags in the English Penn Treebank (quotes and
     * period, comma, colon, etc., but not brackets)
     *
     * @return Whether this is a EVALB-ignored punctuation tag
     */
    @Override
    public String[] evalBIgnoredPunctuationTags() {
        return collinsPunctTags;
    }

    /**
     * Return an array of characters at which a String should be
     * truncated to give the basic syntactic category of a label.
     * The idea here is that Penn treebank style labels follow a syntactic
     * category with various functional and crossreferencing information
     * introduced by special characters (such as "NP-SBJ=1").  This would
     * be truncated to "NP" by the array containing '-' and "=".
     *
     * @return An array of characters that set off label name suffixes
     */
    @Override
    public char[] labelAnnotationIntroducingCharacters() {
        return annotationIntroducingChars;
    }

    /**
     * Returns a String array of treebank start symbols.
     *
     * @return The start symbols
     */
    @Override
    public String[] startSymbols() {
        return vnStartSymbols;
    }

    /**
     * Returns a factory for {@link PTBTokenizer}.
     *
     * @return A tokenizer
     */
    @Override
    public TokenizerFactory<CoreLabel> getTokenizerFactory() {
        return PTBTokenizer.coreLabelFactory();
    }

    /**
     * Returns the extension of treebank files for this treebank.
     * This is "mrg".
     */
    @Override
    public String treebankFileExtension() {
        return "prd";
    }

    /**
     * Return a GrammaticalStructure suitable for this language/treebank.
     *
     * @return A GrammaticalStructure suitable for this language/treebank.
     */
    @Override
    public GrammaticalStructureFactory grammaticalStructureFactory() {
        if (generateOriginalDependencies) {
            return new EnglishGrammaticalStructureFactory();
        } else {
            return new UniversalEnglishGrammaticalStructureFactory();
        }
    }


    @Override
    public boolean supportsGrammaticalStructures() {
        return false;
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public HeadFinder headFinder() {
        return new VietnameseHeadFinder(this);
    }

    /**
     * {@inheritDoc}
     */
    @Override
    public HeadFinder typedDependencyHeadFinder() {
        if (generateOriginalDependencies) {
            return new VietnameseHeadFinder(this);
        } else {
            return new UniversalSemanticHeadFinder(this, true);
        }
    }
}
