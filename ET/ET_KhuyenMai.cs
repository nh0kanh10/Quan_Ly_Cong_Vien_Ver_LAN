using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ET_KhuyenMai
    {
        public int MaKhuyenMai { get; set; }
        public string TenKhuyenMai { get; set; }
        public string LoaiGiamGia { get; set; }
        public decimal GiaTriGiam { get; set; }
        public decimal DonToiThieu { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int? MaSuKien { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        public ET_KhuyenMai() { }
        public ET_KhuyenMai(int maKhuyenMai, string tenKhuyenMai, string loaiGiamGia, decimal giaTriGiam, decimal donToiThieu, DateTime ngayBatDau, DateTime ngayKetThuc, int? maSuKien, bool trangThai, DateTime ngayTao, DateTime? ngayCapNhat)
        {
            this.MaKhuyenMai = maKhuyenMai;
            this.TenKhuyenMai = tenKhuyenMai;
            this.LoaiGiamGia = loaiGiamGia;
            this.GiaTriGiam = giaTriGiam;
            this.DonToiThieu = donToiThieu;
            this.NgayBatDau = ngayBatDau;
            this.NgayKetThuc = ngayKetThuc;
            this.MaSuKien = maSuKien;
            this.TrangThai = trangThai;
            this.NgayTao = ngayTao;
            this.NgayCapNhat = ngayCapNhat;
        }
    }
}
