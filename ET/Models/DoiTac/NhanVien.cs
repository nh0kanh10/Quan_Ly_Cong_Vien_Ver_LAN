namespace ET.Models.DoiTac
{
    // Bảng DoiTac.NhanVien — Bảng con kế thừa ThongTin.
    // PK = IdDoiTac (dùng lại Id của ThongTin, không có Id riêng).
    public class NhanVien
    {
        public int IdDoiTac { get; set; }
        public string MaNhanVien { get; set; }
        public int? IdVaiTro { get; set; }
        public int? IdKhuVuc { get; set; }
        public int? IdNguoiQuanLy { get; set; }
        public string ChucVu { get; set; }
        public string GioiTinh { get; set; }
        public System.DateTime? NgaySinh { get; set; }
        public string LoaiKhoi { get; set; }
        public string LoaiHopDong { get; set; }
        public string NhomCongViec { get; set; }
        public decimal? LuongCoBan { get; set; }
        public decimal? LuongTheoGio { get; set; }
        public string TrangThai { get; set; }
    }
}
