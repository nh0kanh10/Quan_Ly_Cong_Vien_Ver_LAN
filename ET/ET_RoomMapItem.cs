using System;

namespace ET
{
    public class ET_RoomMapItem
    {
        public int Id { get; set; }
        public string TenPhong { get; set; }
        public string TrangThai { get; set; }
        public string KhachHienTai { get; set; }
        public string SdtKhach { get; set; }
        public DateTime? NgayCheckIn { get; set; }
        public DateTime? NgayTraDuKien { get; set; }
        public int? IdDatPhongTiepTheo { get; set; }
        public DateTime? NgayNhanTiepTheo { get; set; }
        public int IdSanPham { get; set; }
        public int SucChua { get; set; }

        public string TenLoaiPhong { get; set; }
        public string MaKhuVuc { get; set; }
        public string TenKhuVuc { get; set; }
        public decimal DonGia { get; set; }
        public decimal TongTienTamTinh { get; set; }
    }
}
