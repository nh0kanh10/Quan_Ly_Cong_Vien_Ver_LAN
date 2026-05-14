using System;
using System.Collections.Generic;
using ET.Interfaces;

namespace ET.Models.DanhMuc
{
    public class ET_SanPham : IEntity, ISoftDelete
    {
        public int Id { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string LoaiSanPham { get; set; }
        public int IdDonViTinh { get; set; }
        public string TenDonViTinh { get; set; }
        public decimal? DonGia { get; set; }
        public string AnhDaiDien { get; set; }
        public bool LaVatTu { get; set; }
        public bool CanQuanLyLo { get; set; }
        public string TrangThai { get; set; }
        public bool DaXoa { get; set; }
        public DateTime NgayTao { get; set; }
        public int? NguoiTao { get; set; }

        public List<ET_BangGia> BangGias { get; set; } = new List<ET_BangGia>();
        public ET_SanPham_Ve SanPham_Ve { get; set; }
        public ET_MonAn MonAn { get; set; }
        public List<ET_Ve_QuyenTruyCap> Ve_QuyenTruyCaps { get; set; } = new List<ET_Ve_QuyenTruyCap>();
        public List<ET_DinhMucNguyenLieu> DinhMucNguyenLieus { get; set; } = new List<ET_DinhMucNguyenLieu>();
    }
}
