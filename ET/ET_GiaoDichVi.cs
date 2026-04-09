using System;

namespace ET
{
    public partial class ET_GiaoDichVi
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public int IdVi { get; set; }
        public string LoaiGiaoDich { get; set; }
        public decimal SoTien { get; set; }
        public int? IdDonHangLienQuan { get; set; }
        public int? ParentTransactionId { get; set; }
        public DateTime ThoiGian { get; set; }
        public string HashSignature { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
