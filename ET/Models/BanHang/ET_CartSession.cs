using System.Collections.Generic;
using System.Linq;

namespace ET.Models.BanHang
{
    public class ET_CartSession
    {
        public int? IdPhienThuNgan { get; set; }
        public int IdNhanVien { get; set; }
        public int? IdKhachHang { get; set; }
        public string NguonBan { get; set; } = ET.Constants.AppConstants.NguonBan.TrucTiep;
        public string MaDonHang { get; set; }  
        public string GhiChu { get; set; }

        public List<ET_CartItem> DanhSachDong { get; set; } = new List<ET_CartItem>();
        public List<ET_PaymentLine> DanhSachThanhToan { get; set; } = new List<ET_PaymentLine>();

        //  Tổng tự tính 
        public decimal TongTienHang => DanhSachDong.Sum(x => x.ThanhTien);
        public decimal TienThueVAT => DanhSachDong.Sum(x => x.TienThue);
        public decimal TongGiamGia { get; set; } = 0m;
        public decimal TienPhiDichVu { get; set; } = 0m;

        // Khuyến mãi đã áp 
        public List<ET.DTOs.DTO_KhuyenMaiPOS> DanhSachKMApDung { get; set; } = new List<ET.DTOs.DTO_KhuyenMaiPOS>();
        public decimal SoTienGiamKM { get; set; } = 0m;
        public decimal TongThanhToan => (TongTienHang + TienThueVAT + TienPhiDichVu) - TongGiamGia;

        //  Đối soát thanh toán 
        public decimal TongDaTra => DanhSachThanhToan.Sum(x => x.SoTien);
        public decimal TienThua => TongDaTra > TongThanhToan ? (TongDaTra - TongThanhToan) : 0m;
        public decimal ConThieu => TongThanhToan > TongDaTra ? (TongThanhToan - TongDaTra) : 0m;
    }
}
