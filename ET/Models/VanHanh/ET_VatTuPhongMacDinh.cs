using System;

namespace ET.Models.VanHanh
{
    public class ET_VatTuPhongMacDinh
    {
        public int Id { get; set; }
        public int IdLoaiPhong { get; set; }
        public int IdSanPham { get; set; }
        public int SoLuong { get; set; }
        public string GhiChu { get; set; }

        // Mở rộng hiển thị
        public string TenSanPham { get; set; }
        public string TenDonVi { get; set; }
    }
}
