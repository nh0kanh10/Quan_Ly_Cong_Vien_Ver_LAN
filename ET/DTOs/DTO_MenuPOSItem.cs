namespace ET.DTOs
{
    // DTO hiển thị trên gridMenu phía phải của ucMenuPOS.
    // Các cột MaSanPham, TenSanPham, LoaiSanPham, DonGia là readonly.
    // ThuTuHienThi và ConHoatDong được chỉnh sửa trực tiếp trên lưới.
    public class DTO_MenuPOSItem
    {
        public int     IdSanPham    { get; set; }
        public string  MaSanPham    { get; set; }
        public string  TenSanPham   { get; set; }
        public string  LoaiSanPham  { get; set; }
        public decimal DonGia       { get; set; }
        public int     ThuTuHienThi { get; set; }
        public bool    ConHoatDong  { get; set; } = true;
    }
}
