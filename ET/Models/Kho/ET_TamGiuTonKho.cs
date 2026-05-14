using System;

namespace ET.Models.Kho
{
    public class ET_TamGiuTonKho
    {
        public int Id { get; set; }
        public int IdSanPham { get; set; }
        public int IdKho { get; set; }
        public int? IdLoHang { get; set; }
        public decimal SoLuongGiu { get; set; }
        public int? IdDonHangNhap { get; set; }
        public DateTime GiuTu { get; set; }
        public DateTime HetHan { get; set; }
        public string TrangThai { get; set; }
    }
}
