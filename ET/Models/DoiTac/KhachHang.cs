namespace ET.Models.DoiTac
{
    // Bảng DoiTac.KhachHang — Bảng con kế thừa ThongTin.
    // PK = IdDoiTac. DiemTichLuy và TongChiTieu tính từ TaiChinh.LichSuDiem và BanHang.DonHang.
    public class KhachHang
    {
        public int IdDoiTac { get; set; }
        public string MaKhachHang { get; set; }
        public string LoaiKhach { get; set; }
        public string HangThanhVien { get; set; }
        public int? IdDoanKhach { get; set; }
    }
}
