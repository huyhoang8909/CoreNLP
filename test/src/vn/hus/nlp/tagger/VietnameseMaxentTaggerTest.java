package vn.hus.nlp.tagger;

import org.junit.Before;
import org.junit.Test;

import java.util.List;

import edu.stanford.nlp.ling.WordTag;

import static org.junit.Assert.assertEquals;


public class VietnameseMaxentTaggerTest {
    VietnameseMaxentTagger tagger;

    @Before
    public void setup() {
        tagger = new VietnameseMaxentTagger();
    }

    @Test
    public void success() {
        List<WordTag> wordTags = tagger.tagText2("Phân tích cảm xúc trong một văn bản Tiếng Việt");
        assertEquals(6, wordTags.size());
        assertEquals("Phân_tích", wordTags.get(0).word());
        assertEquals("V", wordTags.get(0).tag());
        assertEquals("cảm_xúc", wordTags.get(1).word());
        assertEquals("N", wordTags.get(1).tag());
        assertEquals("trong", wordTags.get(2).word());
        assertEquals("E", wordTags.get(2).tag());
        assertEquals("một", wordTags.get(3).word());
        assertEquals("M", wordTags.get(3).tag());
        assertEquals("văn_bản", wordTags.get(4).word());
        assertEquals("N", wordTags.get(4).tag());
        assertEquals("Tiếng_Việt", wordTags.get(5).word());
        assertEquals("N", wordTags.get(5).tag());
    }

    // Nhập nhằng động từ - kết từ chính phụ
    @Test
    public void type1() {
        List<WordTag> wordTags = tagger.tagText2("Vào dịp nghỉ hè rất đông học sinh phổ thông theo học .");
        assertEquals(10, wordTags.size());

        // Actually, the word Vào in this sentence should be E instead
        assertEquals("Vào", wordTags.get(0).word());
        assertEquals("V", wordTags.get(0).tag());
        assertEquals("dịp", wordTags.get(1).word());
        assertEquals("N", wordTags.get(1).tag());
        // the words nghỉ and hè should be separate but not one word
        assertEquals("nghỉ_hè", wordTags.get(2).word());
        assertEquals("V", wordTags.get(2).tag());
        assertEquals("rất", wordTags.get(3).word());
        assertEquals("R", wordTags.get(3).tag());
        assertEquals("đông", wordTags.get(4).word());
        assertEquals("A", wordTags.get(4).tag());
        assertEquals("học_sinh", wordTags.get(5).word());
        assertEquals("N", wordTags.get(5).tag());
        assertEquals("phổ_thông", wordTags.get(6).word());
        assertEquals("N", wordTags.get(6).tag());
        assertEquals("theo", wordTags.get(7).word());
        assertEquals("V", wordTags.get(7).tag());
        assertEquals("học", wordTags.get(8).word());
        assertEquals("V", wordTags.get(8).tag());
        assertEquals(".", wordTags.get(9).word());
        assertEquals(".", wordTags.get(9).tag());
    }

    // Nhập nhằng động từ - trợ từ
    @Test
    public void type2() {
        List<WordTag> wordTags = tagger.tagText2("Bé đang tập đi .");

        assertEquals("đi", wordTags.get(3).word());
        assertEquals("V", wordTags.get(3).tag());

        wordTags = tagger.tagText2("Trông sạch quá đi");
        assertEquals("đi", wordTags.get(3).word());
        // The word đi tag should T but not V
        assertEquals("V", wordTags.get(3).tag());
    }

    // Nhập nhằng động từ - danh từ
    @Test
    public void type3() {
        List<WordTag> wordTags = tagger.tagText2("Tôi mượn cuốc để cuốc đám đất sau nhà .");

        assertEquals("cuốc", wordTags.get(2).word());
        assertEquals("N", wordTags.get(2).tag());

        assertEquals("cuốc", wordTags.get(4).word());
        assertEquals("V", wordTags.get(4).tag());
    }

    // Nhập nhằng động từ - tính từ
    @Test
    public void type4() {
        List<WordTag> wordTags = tagger.tagText2("Đồng hồ này chạy rất chính xác .");

        assertEquals("chạy", wordTags.get(2).word());
        assertEquals("V", wordTags.get(2).tag());

        wordTags = tagger.tagText2("Hàng bà ấy dạo này bán rất chạy .");
        assertEquals("chạy", wordTags.get(7).word());

        // This should be A instead V
        assertEquals("V", wordTags.get(7).tag());
    }

    // Nhập nhằng động từ - phó từ
    @Test
    public void type5() {
        List<WordTag> wordTags = tagger.tagText2("Mình không nuôi chúng nó được .");

        assertEquals("Mình", wordTags.get(0).word());
        assertEquals("P", wordTags.get(0).tag());

        assertEquals("không", wordTags.get(1).word());
        assertEquals("R", wordTags.get(1).tag());

        assertEquals("nuôi", wordTags.get(2).word());
        assertEquals("V", wordTags.get(2).tag());

        assertEquals("chúng_nó", wordTags.get(3).word());
        assertEquals("P", wordTags.get(3).tag());

        // This tag should be R instead V
        assertEquals("được", wordTags.get(4).word());
        assertEquals("V", wordTags.get(4).tag());
    }
}