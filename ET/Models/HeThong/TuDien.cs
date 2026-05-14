namespace ET.Models.HeThong
{
    // Bảng HeThong.TuDien — Danh sách trạng thái/phân loại dùng cho ComboBox.
    // Primary Key: (NhomMa, Ma) — composite key, không có Id.
    public class TuDien
    {
        public string NhomMa { get; set; }
        public string Ma { get; set; }
        public string NhanHienThi { get; set; }
        public int ThuTu { get; set; }
        public string MauSac { get; set; }
        public string BieuTuong { get; set; }
        public bool ConHoatDong { get; set; }
    }
}
