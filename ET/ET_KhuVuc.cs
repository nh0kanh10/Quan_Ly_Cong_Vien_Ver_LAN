using System;

namespace ET
{
    public partial class ET_KhuVuc
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public string TenKhuVuc { get; set; }
        public string MoTa { get; set; }
        public string TrangThai { get; set; }
        public string HinhAnh { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
