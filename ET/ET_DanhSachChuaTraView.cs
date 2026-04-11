using System;

namespace ET
{
    public class ET_DanhSachChuaTraView
    {
        public int IdThueDo { get; set; }
        public string HeaderNhom { get; set; } // Grouping text: MaCode + TenKhachHang
        public string MaCode { get; set; }
        public string TenKhachHang { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public DateTime ThoiGianThue { get; set; }
    }
}
