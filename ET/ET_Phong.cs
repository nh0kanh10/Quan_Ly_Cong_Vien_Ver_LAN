using System;

namespace ET
{
    public partial class ET_Phong
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public string TenPhong { get; set; }
        public int? IdSanPham { get; set; }
        public int? SucChua { get; set; }
        public string TrangThai { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] RowVer { get; set; }

        public string TenTrangThai
        {
            get
            {
                switch (TrangThai)
                {
                    case "Trong": return "Còn trống";
                    case "DangThue": return "Đang có khách";
                    case "BaoTri": return "Đang bảo trì";
                    case "DonDep": return "Đang dọn dẹp";
                    default: return TrangThai;
                }
            }
        }
    }
}

