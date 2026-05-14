namespace ET.DTOs
{
    /// <summary>
    /// DTO hiển thị trên lưới Rổ Combo 
    /// </summary>
    public class DTO_ComboChiTietDisplay
    {
        public int IdSanPham { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal TyLePhanBo { get; set; }
        public decimal ThanhTien => DonGia * SoLuong;
    }
}
