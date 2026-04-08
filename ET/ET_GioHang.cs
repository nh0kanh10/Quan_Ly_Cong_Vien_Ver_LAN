using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ET_GioHang
    {
        public int MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string Loai { get; set; } // Vé / Dịch vụ
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;

        public ET_GioHang() { }

        public ET_GioHang(int maSanPham, string tenSanPham, string loai, int soLuong, decimal donGia)
        {
            this.MaSanPham = maSanPham;
            this.TenSanPham = tenSanPham;
            this.Loai = loai;
            this.SoLuong = soLuong;
            this.DonGia = donGia;
        }
    }
}
