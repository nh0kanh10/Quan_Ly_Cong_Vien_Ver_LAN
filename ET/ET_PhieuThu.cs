using System;

namespace ET
{
    public partial class ET_PhieuThu
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public int? IdDonHang { get; set; }
        public int? IdGiaoDichVi { get; set; }
        public decimal SoTien { get; set; }
        public string PhuongThuc { get; set; }
        public string MaGiaoDichDoiTac { get; set; }
        public DateTime ThoiGian { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
