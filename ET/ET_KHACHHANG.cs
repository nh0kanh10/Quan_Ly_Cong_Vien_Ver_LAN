using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ET_KHACHHANG
    {
        public int MaKhachHang { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string MaThanhVien { get; set; }
        public DateTime NgayHetHanThe { get; set; }
        public decimal TongChiTieu { get; set; }
        public int DiemTichLuy { get; set; }
        public string DiaChi { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        public ET_KHACHHANG() { }

        public ET_KHACHHANG(int maKhachHang, string hoTen, string soDienThoai, string email, string maThanhVien, DateTime ngayHetHanThe, decimal tongChiTieu, int diemTichLuy, string diaChi, DateTime ngaySinh, string gioiTinh, DateTime ngayTao, DateTime? ngayCapNhat)
        {
            this.MaKhachHang = maKhachHang;
            this.HoTen = hoTen;
            this.SoDienThoai = soDienThoai;
            this.Email = email;
            this.MaThanhVien = maThanhVien;
            this.NgayHetHanThe = ngayHetHanThe;
            this.TongChiTieu = tongChiTieu;
            this.DiemTichLuy = diemTichLuy;
            this.DiaChi = diaChi;
            this.NgaySinh = ngaySinh;
            this.GioiTinh = gioiTinh;
            this.NgayTao = ngayTao;
            this.NgayCapNhat = ngayCapNhat;
        }
    }
}
