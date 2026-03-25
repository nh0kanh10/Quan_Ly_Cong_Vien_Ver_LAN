using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ET_DichVu
    {
        private string maCode, tenDichVu, donViTinh, trangThai;
        private int maDichVu, maDanhMuc, maKhuVuc, soLuongTon;
        private DateTime ngayTao, ngayCapNhat;
        private decimal giaBan;

        public ET_DichVu() { }
        public ET_DichVu(string maCode, string tenDichVu, string donViTinh, string trangThai, int maDichVu, int maDanhMuc, int maKhuVuc, int soLuongTon, DateTime ngayTao, DateTime ngayCapNhat, decimal giaBan)
        {
            this.MaCode = maCode;
            this.TenDichVu = tenDichVu;
            this.DonViTinh = donViTinh;
            this.TrangThai = trangThai;
            this.MaDichVu = maDichVu;
            this.MaDanhMuc = maDanhMuc;
            this.MaKhuVuc = maKhuVuc;
            this.SoLuongTon = soLuongTon;
            this.NgayTao = ngayTao;
            this.NgayCapNhat = ngayCapNhat;
            this.GiaBan = giaBan;
        }

        public string MaCode { get => maCode; set => maCode = value; }
        public string TenDichVu { get => tenDichVu; set => tenDichVu = value; }
        public string DonViTinh { get => donViTinh; set => donViTinh = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public int MaDichVu { get => maDichVu; set => maDichVu = value; }
        public int MaDanhMuc { get => maDanhMuc; set => maDanhMuc = value; }
        public int MaKhuVuc { get => maKhuVuc; set => maKhuVuc = value; }
        public int SoLuongTon { get => soLuongTon; set => soLuongTon = value; }
        public DateTime NgayTao { get => ngayTao; set => ngayTao = value; }
        public DateTime NgayCapNhat { get => ngayCapNhat; set => ngayCapNhat = value; }
        public decimal GiaBan { get => giaBan; set => giaBan = value; }
    }
}
