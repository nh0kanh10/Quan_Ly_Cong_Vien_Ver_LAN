using System;

namespace ET
{
    public partial class ET_KhuyenMai
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public string TenKhuyenMai { get; set; }
        public int? IdSuKien { get; set; }
        public string LoaiGiamGia { get; set; }
        public decimal GiaTriGiam { get; set; }
        public decimal? DonToiThieu { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public bool TrangThai { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
