using System;

namespace ET
{
    public partial class ET_VeDienTu
    {
        public Guid Id { get; set; }
        public string MaCode { get; set; }
        public int IdChiTietDonHang { get; set; }
        public int? IdSanPham { get; set; }        
        public string TrangThai { get; set; }
        public DateTime? ThoiGianQuet { get; set; }
        public int? IdKhachHangSuDung { get; set; }
        public int SoLuotConLai { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
    }
}
