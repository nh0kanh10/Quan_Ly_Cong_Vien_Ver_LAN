using System;
using System.Collections.Generic;

namespace ET.DTOs
{
    /// <summary>
    /// Thông tin KM dùng chung: quản lý CRUD + áp dụng trên POS.
    /// </summary>
    public class DTO_KhuyenMaiPOS
    {
        public int Id { get; set; }
        public string MaKhuyenMai { get; set; }
        public string TenKhuyenMai { get; set; }
        public string LoaiGiamGia { get; set; }
        public decimal GiaTriGiam { get; set; }
        public decimal DonToiThieu { get; set; }
        public bool CoChongCheo { get; set; }
        public int? SoLanToiDa { get; set; }
        public int SoLanDaDung { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        public List<DTO_DieuKienKM> DieuKiens { get; set; } = new List<DTO_DieuKienKM>();

        // Kết quả tính toán sau khi áp (không lưu DB)
        public decimal SoTienGiamThucTe { get; set; }
    }
}
