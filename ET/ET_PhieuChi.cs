using System;

namespace ET
{
    public partial class ET_PhieuChi
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public decimal SoTien { get; set; }
        public string LyDo { get; set; }
        public DateTime ThoiGian { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
