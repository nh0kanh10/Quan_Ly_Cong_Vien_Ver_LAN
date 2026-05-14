namespace ET.DTOs
{
    // Combo hiển thị trên danh mục POS
    public class DTO_ComboPOS
    {
        public int Id { get; set; }
        public string MaCombo { get; set; }
        public string TenCombo { get; set; }
        public decimal GiaCombo { get; set; }
        public string MoTa { get; set; }
        public int SoThanhPhan { get; set; }
    }

    // Thành phần combo khi tách vào giỏ POS
    public class DTO_ComboItemPOS
    {
        public int IdSanPham { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string LoaiSanPham { get; set; }
        public bool LaVatTu { get; set; }
        public int SoLuong { get; set; }
        public decimal TyLePhanBo { get; set; }
        public decimal? DonGia { get; set; }
        public int? IdCauHinhThue { get; set; }
        public decimal PhanTramThue { get; set; }
    }
}
