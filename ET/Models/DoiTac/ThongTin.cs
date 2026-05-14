using System;
using ET.Interfaces;

namespace ET.Models.DoiTac
{
    // Bảng DoiTac.ThongTin — Bảng cha (Party Model).
    // Chứa thông tin chung: Tên, SĐT, CCCD. Các bảng con (NhanVien, KhachHang) dùng lại Id.
    public class ThongTin : IEntity, ISoftDelete
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string Cccd { get; set; }
        public string DiaChi { get; set; }
        public string HinhAnh { get; set; }
        public string LoaiDoiTac { get; set; }
        public bool DaXoa { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? CapNhatLuc { get; set; }
    }
}
