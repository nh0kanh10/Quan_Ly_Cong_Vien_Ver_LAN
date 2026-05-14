using System;

namespace ET.Models.DanhMuc
{
    public class ET_BangGia
    {
        public int Id { get; set; }
        public int IdSanPham { get; set; }
        public string LoaiGia { get; set; }
        public decimal GiaBan { get; set; }
        public DateTime HieuLucTu { get; set; }
        public DateTime HieuLucDen { get; set; }
        public string TrangThai { get; set; }

        public decimal? TienCoc { get; set; }
        public int? PhutBlock { get; set; }
        public int? PhutTiep { get; set; }
        public decimal? GiaPhuThu { get; set; }

        public ET_BangGia()
        {
            LoaiGia = "MacDinh";
            HieuLucTu = DateTime.Today;
            HieuLucDen = DateTime.Today.AddYears(1);
            TrangThai = "HoatDong";
        }
    }
}
