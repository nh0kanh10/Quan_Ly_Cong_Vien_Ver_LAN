using System;

namespace ET
{
    public partial class ET_DatBan
    {
        public int Id { get; set; }
        public int IdChiTietDonHang { get; set; }  
        public int IdNhaHang { get; set; }
        public DateTime ThoiGianDat { get; set; }
        public DateTime ThoiGianDenDuKien { get; set; }
        public int SoLuongKhach { get; set; }
        public string TrangThai { get; set; }

        public string TenNguoiDat { get; set; }
        public string SoDienThoai { get; set; }
        public string GhiChu { get; set; }
        public decimal TienCoc { get; set; }
        public int? IdPhieuThuCoc { get; set; }
        public int? IdKhachHang { get; set; }
    }
}
