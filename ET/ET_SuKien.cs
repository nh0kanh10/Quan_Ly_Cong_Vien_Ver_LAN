using System;

namespace ET
{
    public partial class ET_SuKien
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public string TenSuKien { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string MoTa { get; set; }
        public string TrangThai { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
