using System;

namespace ET.DTOs
{
    public class DTO_PhongLuuTruView
    {
        public int IdPhong { get; set; }
        public string MaPhong { get; set; }
        public int? Tang { get; set; }
        public string TenLoaiPhong { get; set; }
        public int IdSanPham { get; set; }
        public int? SoNguoiToiDa { get; set; }
        public string TrangThaiPhong { get; set; } 
        public string TrangThaiBooking { get; set; }

        public int? IdChiTietDatPhong { get; set; }
        public int? IdPhieuDatPhong { get; set; }
        public int? IdDonHang { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
        
        public DateTime? NgayCheckIn { get; set; }
        public DateTime? NgayCheckOut { get; set; }
        
        public bool IsLateCheckOut { get { return NgayCheckOut.HasValue && DateTime.Now > NgayCheckOut.Value; } }
        public TimeSpan? ThoiGianO { get { return NgayCheckIn.HasValue ? DateTime.Now - NgayCheckIn.Value : (TimeSpan?)null; } }

        public string CssColor
        {
            get
            {
                if (TrangThaiPhong == Constants.AppConstants.TrangThaiPhong.Trong) return "#10B981";    
                if (TrangThaiPhong == Constants.AppConstants.TrangThaiPhong.DangO) return "#EF4444"; 
                if (TrangThaiPhong == Constants.AppConstants.TrangThaiPhong.ChoDon) return "#F59E0B"; 
                return "#D1D5DB"; 
            }
        }
    }
}
