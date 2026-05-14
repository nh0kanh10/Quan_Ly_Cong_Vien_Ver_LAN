using System;
using System.Collections.Generic;

namespace ET.Models.Kho
{
    public class ET_ChungTuKho
    {
        public int Id { get; set; }
        public string MaChungTu { get; set; }
        
        // NHAP_MUA, XUAT_BAN, KIEM_KE, CHUYEN_KHO
        public string LoaiChungTu { get; set; }

        public int? IdDoiTac { get; set; }
        public int? IdDonHang { get; set; }
        public int? IdBaoTri { get; set; }
        public int? IdChungTuGoc { get; set; }
        
        public DateTime NgayChungTu { get; set; }
        public string LyDo { get; set; }
        
        // Nhap, ChoDuyet, DaDuyet, DaHuy
        public string TrangThai { get; set; } 
        
        public int IdNguoiTao { get; set; }
        public int? IdNguoiDuyet1 { get; set; }
        public DateTime? NgayDuyet1 { get; set; }
        public int? IdNguoiDuyet2 { get; set; }
        public DateTime? NgayDuyet2 { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }

        public List<ET_ChiTietChungTu> ChiTiets { get; set; } = new List<ET_ChiTietChungTu>();
    }
}
