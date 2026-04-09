using System;

namespace ET
{
    public partial class ET_ChoiNghiMat
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public int IdKhuVucBien { get; set; }
        public int? IdSanPham { get; set; }
        public string TenChoi { get; set; }
        public int? SucChua { get; set; }
        public string TrangThai { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] RowVer { get; set; }

        public string TenTrangThai
        {
            get
            {
                switch (TrangThai)
                {
                    case AppConstants.TrangThaiPhong.Trong: return "Trống";
                    case AppConstants.TrangThaiPhong.DangSuDung: return "Đang sử dụng";
                    case AppConstants.TrangThaiPhong.BaoTri: return "Bảo trì";
                    default: return TrangThai;
                }
            }
        }
    }
}
