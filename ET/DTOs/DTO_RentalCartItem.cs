namespace ET.DTOs
{
    // Một dòng trong giỏ thuê (trên giao diện ucGiaoDo).
    // Nhân viên double-click SP hoặc quét barcode -> thêm vào giỏ.
    public class DTO_RentalCartItem
    {
        public int IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal TienThue { get; set; }
        public decimal TienCoc { get; set; }

        // Chỉ có khi quét barcode tài sản vật lý (xe điện, xe đạp...)
        // NULL = thuê theo SP + SL (phao, ván)
        public int? IdTaiSanChoThue { get; set; }
        public string MaVachTaiSan { get; set; }

        public decimal TongThue => SoLuong * TienThue;
        public decimal TongCoc => SoLuong * TienCoc;
    }
}
