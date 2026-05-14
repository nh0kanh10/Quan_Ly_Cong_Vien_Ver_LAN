using System;

namespace ET.Models.DanhMuc
{
    public class ET_PhuongTienDiChuyen : ET_TaiSanChoThue
    {
        public string BienSo { get; set; }
        public int SoGhe { get; set; }
        public string LoaiXe { get; set; } 
        public int? IdKhuVucHienTai { get; set; }
    }
}
