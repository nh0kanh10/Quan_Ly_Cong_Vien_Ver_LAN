using System;

namespace ET
{
    public partial class ET_DonViTinh
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string KyHieu { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
