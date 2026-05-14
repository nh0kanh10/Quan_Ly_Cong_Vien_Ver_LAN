using System;

namespace ET.Models.DoiTac
{
    // DTO hiển thị form frmKhachHang.
    // Gộp 2 bảng ThongTin + KhachHang, kèm các chỉ số tính toán (số dư ví, điểm, tổng chi tiêu).
    public class DTO_KhachHangChiTiet
    {
        // -- Từ DoiTac.ThongTin --
        public int IdDoiTac { get; set; }
        public string HoTen { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string Cccd { get; set; }
        public string DiaChi { get; set; }
        public string HinhAnh { get; set; }
        public DateTime NgayTao { get; set; }

        // -- Từ DoiTac.KhachHang --
        public string MaKhachHang { get; set; }
        public string LoaiKhach { get; set; }
        public string HangThanhVien { get; set; }
        public int? IdDoanKhach { get; set; }

        // -- Metrics (tính toán từ các bảng TaiChinh) --
        public decimal SoDuVi { get; set; }
        public bool CoViDienTu { get; set; }
        public int DiemTichLuy { get; set; }
        public string TrangThaiThe { get; set; }
        public string MaTheRFID { get; set; }
        public decimal TongChiTieu { get; set; }
    }
}
