using System;

namespace ET
{
    public partial class ET_DoanKhach
    {
        public int Id { get; set; }
        public string MaBooking { get; set; }
        public string TenDoan { get; set; }
        public string MaSoThue { get; set; }
        public string NguoiDaiDien { get; set; }
        public string DienThoaiLienHe { get; set; }
        public decimal ChietKhau { get; set; }
        public int SoLuongKhach { get; set; }
        public DateTime? NgayDen { get; set; }
        public DateTime? NgayDi { get; set; }
        public string TrangThai { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }

        
        public bool IsBookingValid
        {
            get
            {
                if (TrangThai != AppConstants.TrangThaiDoanKhach.DaDat) return false;
                if (!NgayDen.HasValue) return false;
                var today = DateTime.Today;
                var ngayKetThuc = NgayDi ?? NgayDen.Value; 
                return today >= NgayDen.Value && today <= ngayKetThuc;
            }
        }
    }
}
