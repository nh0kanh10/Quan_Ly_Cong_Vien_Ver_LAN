using System;

namespace ET
{
    public partial class ET_NhaCungCap
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string MaSoThue { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string NguoiLienHe { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
