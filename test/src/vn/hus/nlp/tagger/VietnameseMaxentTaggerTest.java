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

    // Nhập nhằng danh từ - kết từ chính phụ
    @Test
    public void type6() {
        List<WordTag> wordTags = tagger.tagText2("Nhà ấy nhiều của lắm !");

        assertEquals(6, wordTags.size());
        // this tag should be N instead E
        assertEquals("của", wordTags.get(3).word());
        assertEquals("E", wordTags.get(3).tag());

        wordTags = tagger.tagText2("Sách của thư viện ");
        assertEquals(3, wordTags.size());
        assertEquals("của", wordTags.get(1).word());
        assertEquals("E", wordTags.get(1).tag());

    }


    // Nhập nhằng tính từ - danh từ
    @Test
    public void type7() {
        String result = tagger.tagText("Tuy nhiên, sức chống đỡ trước khó khăn của Việt Nam đã tốt lên rất nhiều");
        assertEquals("Tuy_nhiên,/V sức/N chống_đỡ/V trước/E khó_khăn/N của/E Việt_Nam/Np đã/R tốt/A lên/R rất/R nhiều/A", result);

    }


    @Test
    public void canParseLargeInput() {
        String result = tagger.tagText("Năm 2000, khi vung tiền tỷ mua khu đất vườn ở quận 9, ông Điền bị dèm pha là kẻ đốt tiền vì mua đất chỉ để đào ao thả cá, nuôi gà, trồng dăm ba loại cây ăn quả cho doanh thu bèo bọt. Còn căn nhà 150m2 xây kiểu thôn quê mỗi tháng về chơi không quá vài ba ngày.\n" +
                "\n" +
                "“Khi đó, giao thông về hướng quận 9 còn khó khăn, lầy lội, phải mất thêm tiền thuê người giữ vườn nên anh em họ hàng mắng tôi chơi ngông và tiêu hoang. Nhưng tôi quyết mua khu đất này làm của để dành, xem như kênh đầu tư dài hạn”, ông Điền nhớ lại.\n" +
                "\n" +
                "7 năm sau (tức năm 2007), khu đất của ông Điền được khách ngã giá 7,5 tỷ đồng, nhưng ông vẫn quyến luyến mảnh vườn nhiều năm vun đắp nên giữ lại. Trong các năm 2015-2016, nhiều đầu nậu săn đất ồ ạt đổ về quận 9. Khu đất của ông Điền được nhiều người hỏi mua, giá cứ nhích dần lên 10 rồi 13 tỷ đồng nhưng ông từ chối sang nhượng.\n" +
                "\n" +
                "Mãi đến quý I/2017, một nhà đầu tư ngã giá 16 tỷ đồng, lần này chủ đất mới đồng ý tiến hành giao dịch. Sau 17 năm ôm đất để dành, trừ đi tiền mua đất 3,5 tỷ đồng cộng thêm các chi phí làm hàng rào, san lấp toàn khu, làm nhà và trả công người giữ đất (khoảng gần 2 tỷ), ông Điền nhẩm tính lãi ròng trên chục tỷ đồng. “Nếu ngày xưa tôi dùng số tiền này mua vàng hay gửi tiết kiệm, cũng kiếm được lãi kha khá nhưng khó mà tích cóp dần thành 16 tỷ đồng như hiện nay”, nhà đầu tư này tính toán.");

        assertEquals("Năm/N 2000,/M khi/N vung/V tiền/N tỷ/V mua/V khu/N đất/N vườn/N ở/E quận/N 9,/N ông/Nc Điền/Np bị/V dèm/V pha/V là/V kẻ/N đốt/V tiền/N vì/E mua/V đất/N chỉ/R để/E đào/V ao/N thả/V cá,/N nuôi/V gà,/N trồng/V dăm_ba/N loại/N cây_ăn_quả/N cho/E doanh_thu/N bèo_bọt./N Còn/V căn/Nc nhà/N 150m2/N xây/V kiểu/N thôn_quê/N mỗi/M tháng/N về/V chơi/V không/R quá/R vài_ba/M ngày./N “Khi/V đó,/N giao_thông/N về/E hướng/N quận/N 9/M còn/R khó_khăn,/V lầy_lội,/N phải/V mất/V thêm/V tiền/N thuê/V người/N giữ/V vườn/N nên/C anh_em/N họ_hàng/N mắng/V tôi/P chơi/V ngông/N và/CC tiêu/V hoang./N Nhưng/C tôi/P quyết/V mua/V khu/N đất/N này/P làm/V của/E để_dành,/N xem/V như/C kênh/N đầu_tư/V dài_hạn”,/N ông/Nc Điền/Np nhớ/V lại./N 7/M năm/N sau/A (tức/M năm/N 2007),/M khu/N đất/N của/E ông/Nc Điền/Np được/V khách/N ngã_giá/V 7,5/M tỷ/N đồng,/N nhưng/C ông/N vẫn/R quyến_luyến/V mảnh/Nc vườn/N nhiều/A năm/N vun_đắp/A nên/C giữ/V lại./N Trong/E các/L năm/N 2015-2016,/V nhiều/A đầu_nậu/N săn/V đất/N ồ_ạt/A đổ/V về/E quận/N 9./V Khu/N đất/N của/E ông/Nc Điền/Np được/V nhiều/A người/N hỏi/V mua,/N giá/N cứ/R nhích/V dần/R lên/R 10/M rồi/C 13/M tỷ/N đồng/N nhưng/C ông/N từ_chối/V sang_nhượng./V Mãi/R đến/E quý/N I/2017,/V một/M nhà/N đầu_tư/V ngã_giá/V 16/M tỷ/N đồng,/A lần/N này/P chủ/N đất/N mới/R đồng_ý/V tiến_hành/V giao_dịch./N Sau/N 17/M năm/N ôm/V đất/N để_dành,/N trừ/V đi/V tiền/N mua/V đất/N 3,5/M tỷ/N đồng/N cộng/V thêm/V các/L chi_phí/N làm/V hàng_rào,/N san_lấp/V toàn/R khu,/N làm/V nhà/N và/CC trả/V công/N người/N giữ/V đất/N (khoảng/N gần/A 2/M tỷ),/Nu ông/Nc Điền/Np nhẩm/V tính/V lãi_ròng/N trên/E chục/M tỷ/N đồng./V “Nếu/N ngày_xưa/N tôi/P dùng/V số/N tiền/N này/P mua/V vàng/N hay/C gửi/V tiết_kiệm,/N cũng/R kiếm/V được/R lãi/V kha_khá/A nhưng/C khó/A mà/C tích_cóp/V dần/R thành/V 16/M tỷ/N đồng/N như/C hiện_nay”,/N nhà/N đầu_tư/V này/P tính_toán./V", result);
    }

    @Test
    public void xx() {
        System.out.println(tagger.tagText("Ông giục tôi đi ngủ , \" ngày_mai công_việc dài lắm , cố ngủ một_chút mà lấy sức \" ..."));
    }

}