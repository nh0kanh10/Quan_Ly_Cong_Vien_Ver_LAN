namespace ET.Models.BanHang
{
    public class ET_CartItem
    {
        public int IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string LoaiSanPham { get; set; }  // Lấy từ SanPham.LoaiSanPham (Ve, AnUong, ThueDo, HangHoa...)

        public decimal SoLuong { get; set; }
        public decimal DonGiaGoc { get; set; }
        public decimal TienGiamGiaDong { get; set; }
        public decimal DonGiaThucTe { get; set; }
        public decimal ThanhTien => SoLuong * DonGiaThucTe;

        public int? IdBangGia { get; set; }
        public int? IdCauHinhThue { get; set; }
        public decimal PhanTramThue { get; set; } = 0m;
        public decimal TienThue => ThanhTien * (PhanTramThue / 100m);

        public string GhiChu { get; set; }

        public bool LaVatTu { get; set; }
        public int? IdLoHang { get; set; }
        public decimal HeSoQuyDoi { get; set; } = 1m;  // 1 = đơn vị cơ sở, 24 = thùng 24 lon

        // Combo: khi item này được tách từ combo
        public int? IdCombo { get; set; }
        public string TenCombo { get; set; }

        // Khách đoàn (Zero-dollar billing)
        public int? IdQuyenLoiDoan { get; set; }
    }
}
