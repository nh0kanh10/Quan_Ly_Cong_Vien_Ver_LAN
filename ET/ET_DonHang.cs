using System;

namespace ET
{
    public partial class ET_DonHang
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public int? IdKhachHang { get; set; }
        public int? IdDoan { get; set; }
        public int? IdKhuyenMai { get; set; }
        public decimal TienGiamGia { get; set; }
        public DateTime ThoiGian { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public string NguonBan { get; set; } = "POS";

        public string TenTrangThai
        {
            get
            {
                return AppConstants.TrangThaiDonHang.GetTenTrangThai(TrangThai);
            }
        }
    }
}
