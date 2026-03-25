using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ET_BangGia
    {
        private int maBangGia, maDichVu;
        private int? maLoaiVe;
        private string loaiNgay, trangThai;
        private DateTime ngayBatDau, ngayKetThuc, ngayTao, ngayCapNhat;
        private decimal giaBan;

        public ET_BangGia() { }

        public ET_BangGia(int maBangGia, int? maLoaiVe, int maDichVu, string loaiNgay, string trangThai, DateTime ngayBatDau, DateTime ngayKetThuc, DateTime ngayTao, DateTime ngayCapNhat, decimal giaBan)
        {
            this.MaBangGia = maBangGia;
            this.MaLoaiVe = maLoaiVe;
            this.MaDichVu = maDichVu;
            this.LoaiNgay = loaiNgay;
            this.TrangThai = trangThai;
            this.NgayBatDau = ngayBatDau;
            this.NgayKetThuc = ngayKetThuc;
            this.NgayTao = ngayTao;
            this.NgayCapNhat = ngayCapNhat;
            this.GiaBan = giaBan;
        }

        public int MaBangGia { get => maBangGia; set => maBangGia = value; }
        public int? MaLoaiVe { get => maLoaiVe; set => maLoaiVe = value; }
        public int MaDichVu { get => maDichVu; set => maDichVu = value; }
        public string LoaiNgay { get => loaiNgay; set => loaiNgay = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public DateTime NgayBatDau { get => ngayBatDau; set => ngayBatDau = value; }
        public DateTime NgayKetThuc { get => ngayKetThuc; set => ngayKetThuc = value; }
        public DateTime NgayTao { get => ngayTao; set => ngayTao = value; }
        public DateTime NgayCapNhat { get => ngayCapNhat; set => ngayCapNhat = value; }
        public decimal GiaBan { get => giaBan; set => giaBan = value; }
    }
}
