namespace ET.Models.BanHang
{
    /// DTO vé điện tử — dùng cho nghiệp vụ soát vé tại cổng.
    public class ET_VeDienTu
    {
        public int Id { get; set; }
        public int IdChiTietDonHang { get; set; }
        public int IdSanPham { get; set; }
        public int? IdKhachHang { get; set; }
        public int? IdKhuVucHienTai { get; set; }
        public string MaVach { get; set; }
        public int SoLuotConLai { get; set; }
        public string TrangThai { get; set; }
        public System.DateTime? ThoiGianQuet { get; set; }
        public System.DateTime? NgayHetHan { get; set; }
        public System.DateTime NgayTao { get; set; }
        public byte[] RowVersion { get; set; }

        public string TenSanPham { get; set; }
        public string LoaiVe { get; set; }
        public string DoiTuongVe { get; set; }
        public string TenKhachHang { get; set; }
    }
}
