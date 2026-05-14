namespace ET.Models.DanhMuc
{
    /// <summary>
    /// Bảng DanhMuc.DiemBanHang_POS — Điểm bán hàng tại công viên.
    /// Mỗi POS gắn liền 1 khu vực (nhà hàng, cổng vé, quầy lưu niệm...).
    /// </summary>
    public class ET_DiemBanHang_POS
    {
        public int Id { get; set; }
        public string TenDiemBan { get; set; }
        public int? IdKhuVuc { get; set; }
    }
}
