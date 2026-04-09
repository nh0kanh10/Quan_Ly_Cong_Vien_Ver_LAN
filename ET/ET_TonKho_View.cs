using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ET
{
    public class ET_TonKho_View
    {
        [Description("Mã sản phẩm")]
        [DisplayName("Mã sản phẩm")]
        [AmbientValue("SP001")]
        [Category("Thông tin sản phẩm")]

        public int Id { get; set; }
        public int IdSanPham { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string LoaiSanPham { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public int NguongCanhBao { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien => SoLuong * DonGia;
    }
}
