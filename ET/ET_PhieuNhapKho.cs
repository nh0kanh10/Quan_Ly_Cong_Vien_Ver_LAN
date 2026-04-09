using System;

namespace ET
{
    public partial class ET_PhieuNhapKho
    {
        public int Id { get; set; }
        public int IdKho { get; set; }
        public int? IdNhaCungCap { get; set; }
        public DateTime NgayNhap { get; set; }
        public string SoChungTu { get; set; }
        public decimal TongTien { get; set; }
        public int? IdPhieuChi { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
