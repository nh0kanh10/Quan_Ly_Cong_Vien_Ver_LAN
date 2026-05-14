using System;
using System.Collections.Generic;

namespace ET.DTOs
{
    public class DTO_DonHangHoan
    {
        public int IdDonHang { get; set; }
        public string MaDonHang { get; set; }
        public int? IdKhachHang { get; set; }
        public DateTime NgayTao { get; set; }
        public decimal TongThanhToan { get; set; }
        public List<DTO_ChiTietDonHangHoan> DanhSachChiTiet { get; set; } = new List<DTO_ChiTietDonHangHoan>();
        public List<DTO_PhuongThucThanhToanDaDung> DanhSachThanhToan { get; set; } = new List<DTO_PhuongThucThanhToanDaDung>();
    }
}
