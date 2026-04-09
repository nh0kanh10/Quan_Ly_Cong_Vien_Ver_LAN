using System;

namespace ET
{
    public partial class ET_KhoHang
    {
        public int Id { get; set; }
        public string TenKho { get; set; }
        public string LoaiKho { get; set; }
        public string DiaChi { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
