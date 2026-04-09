using System;

namespace ET
{
    public class ET_DoanKhach_DichVu
    {
        public int Id { get; set; }
        public int IdDoan { get; set; }
        public string LoaiDichVu { get; set; }  
        public int? IdCombo { get; set; }
        public int? IdSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }  
        public DateTime? NgaySuDung { get; set; }
        public int? IdThamChieu { get; set; }   
        public int? IdChiTietDonHang { get; set; } 
        public string GhiChu { get; set; }
        public string TrangThai { get; set; }   
        public int SoLuongDaDung { get; set; }   
        public int SoLuongConLai { get { return SoLuong - SoLuongDaDung; } }

        public string TenDichVu { get; set; }
        public string TenCombo { get; set; }

        public string TenLoaiDichVu
        {
            get
            {
                switch (LoaiDichVu)
                {
                    case "Ve": return "Vé";
                    case "Combo": return "Combo";
                    case "Phong": return "Phòng";
                    case "BanAn": return "Bàn ăn";
                    case "AnUong": return "Ăn uống";
                    case "DichVu": return "Dịch vụ";
                    default: return LoaiDichVu;
                }
            }
        }

        public string TenTrangThai
        {
            get
            {
                switch (TrangThai)
                {
                    case "ChuaXuLy": return "Chưa xử lý";
                    case "DaDatCho": return "Đã đặt chỗ";
                    case "DangPhucVu": return "Đang phục vụ";
                    case "DaThanhToan": return "Đã thanh toán";
                    case "DaHuy": return "Đã hủy";
                    default: return TrangThai;
                }
            }
        }
    }
}
