using ET.Constants;

namespace ET.Models.Kho
{
    public class ET_TonKho
    {
        public int IdSanPham { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string DVT { get; set; }
        public int IdKho { get; set; }
        public string TenKho { get; set; }
        public decimal TonHienTai { get; set; }
        public decimal MucCanhBao { get; set; }
        public decimal Thieu { get; set; }

        public string TrangThai
        {
            get
            {
                if (MucCanhBao > 0 && TonHienTai < MucCanhBao) return AppConstants.TrangThaiTonKho.DuoiMuc;
                return AppConstants.TrangThaiTonKho.BinhThuong;
            }
        }
    }
}
