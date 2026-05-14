using System;
using System.Collections.Generic;

namespace ET.DTOs
{
    public class DTO_QuyenLoiDoan
    {
        public int IdQuyenLoi { get; set; }
        public int IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string LoaiSanPham { get; set; }
        public int SoLuongTong { get; set; }
        public int SoLuongDaDung { get; set; }
        public int SoLuongConLai => SoLuongTong - SoLuongDaDung;
        
        public DateTime? NgayHetHan { get; set; }
        public string MaBooking { get; set; }
        public string TenDoanKhach { get; set; }
    }
}
