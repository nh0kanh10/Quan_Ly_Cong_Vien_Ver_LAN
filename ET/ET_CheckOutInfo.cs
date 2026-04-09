using System;

namespace ET
{
    
    public class ET_CheckOutInfo
    {
        public int IdPhong { get; set; }
        public string TenPhong { get; set; }
        public int IdDatPhongChiTiet { get; set; }
        public int IdChiTietDatPhong { get; set; }

        public DateTime NgayNhan { get; set; }
        public DateTime NgayTraDuKien { get; set; }
        public DateTime NgayTraThucTe { get; set; }

        public decimal TienPhongGoc { get; set; }

        public double SoGioTre { get; set; }

        public decimal PhuThuTreGio { get; set; }

        public decimal TongCong => TienPhongGoc + PhuThuTreGio;

        public decimal DaThanhToan { get; set; }

        public decimal ConLai => TongCong - DaThanhToan;
    }
}
