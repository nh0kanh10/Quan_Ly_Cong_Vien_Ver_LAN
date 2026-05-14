using System;
using System.Collections.Generic;

namespace ET.DTOs
{
    public class DTO_HoanHangRequest
    {
        public int IdDonHang { get; set; }
        public int IdNguoiDuyet { get; set; }    // Nhân viên thao tác hiện tại
        public int IdKhoMacDinh { get; set; }    // Kho nhập hàng trả về (kho thật)
        public int IdChungTuHoanKho { get; set; } 
        public int IdKhoKhachAo { get; set; }    // Id kho ảo KHO_KHACH (chiều xuất)
        public DateTime NgayTaoDonHang { get; set; }
        public List<DTO_ChiTietDonHangHoan> ChiTietHoan { get; set; } = new List<DTO_ChiTietDonHangHoan>();
    }
}
