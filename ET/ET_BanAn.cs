using System;

namespace ET
{
    public partial class ET_BanAn
    {
        public int Id { get; set; }
        public int IdNhaHang { get; set; }
        public string MaBan { get; set; }
        public int SucChua { get; set; }
        public string TrangThai { get; set; }
        public byte[] RowVer { get; set; }

        public string TenTrangThai
        {
            get
            {
                switch (TrangThai)
                {
                    case AppConstants.TrangThaiBanAn.Trong: return "Trống";
                    case AppConstants.TrangThaiBanAn.DaDat: return "Đã đặt";
                    case AppConstants.TrangThaiBanAn.DangSuDung: return "Đang sử dụng";
                    case AppConstants.TrangThaiBanAn.BaoTri: return "Bảo trì";
                    default: return TrangThai;
                }
            }
        }
    }
}
