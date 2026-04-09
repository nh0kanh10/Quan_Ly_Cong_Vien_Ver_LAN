using System;

namespace ET
{
    public class ET_LichSuDiem
    {
        public int Id { get; set; }
        public int IdKhachHang { get; set; }
        public string LoaiGiaoDich { get; set; } 
        public int SoDiem { get; set; }
        public int SoDuTruoc { get; set; }
        public int SoDuSau { get; set; }
        public int? IdDonHang { get; set; }
        public string LyDo { get; set; }
        public DateTime ThoiGian { get; set; }
        public int CreatedBy { get; set; }
        public string TenNhanVien { get; set; } 
    }
}
