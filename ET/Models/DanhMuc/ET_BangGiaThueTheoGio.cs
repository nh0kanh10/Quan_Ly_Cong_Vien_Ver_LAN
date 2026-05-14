namespace ET.Models.DanhMuc
{
    // Cấu hình block giờ cho sản phẩm thuê.
    // Ví dụ: Block đầu 120 phút giá 200k. Mỗi 30 phút sau đó thêm 50k.
    // Kế thừa Id từ BangGia (cùng Id).
    public class ET_BangGiaThueTheoGio
    {
        public int IdBangGia { get; set; }
        public int? PhutBlock { get; set; }
        public int? PhutTiep { get; set; }
        public decimal? GiaPhuThu { get; set; }
        public decimal? TienCoc { get; set; }
    }
}
