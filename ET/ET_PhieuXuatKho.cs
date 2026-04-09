using System;

namespace ET
{
    public partial class ET_PhieuXuatKho
    {
        public int Id { get; set; }
        public int IdKhoXuat { get; set; }
        public int? IdKhoNhan { get; set; }
        public DateTime NgayXuat { get; set; }
        public string LyDo { get; set; }
        public int? IdDonHangLienQuan { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
