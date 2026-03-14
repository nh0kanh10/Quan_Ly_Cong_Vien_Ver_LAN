using System;

namespace ET
{
    public class ET_LoaiVe
    {
        public int MaLoaiVe { get; set; }
        public string MaCode { get; set; }
        public string TenLoaiVe { get; set; }
        public decimal GiaVe { get; set; }
        public decimal? GiaCuoiTuan { get; set; }
        public string DoiTuong { get; set; }
        public bool LaCombo { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        public ET_LoaiVe() { }

        public ET_LoaiVe(int maLoaiVe, string maCode, string tenLoaiVe, decimal giaVe,
            decimal? giaCuoiTuan, string doiTuong, bool laCombo, string trangThai,
            DateTime ngayTao, DateTime? ngayCapNhat)
        {
            this.MaLoaiVe = maLoaiVe;
            this.MaCode = maCode;
            this.TenLoaiVe = tenLoaiVe;
            this.GiaVe = giaVe;
            this.GiaCuoiTuan = giaCuoiTuan;
            this.DoiTuong = doiTuong;
            this.LaCombo = laCombo;
            this.TrangThai = trangThai;
            this.NgayTao = ngayTao;
            this.NgayCapNhat = ngayCapNhat;
        }
    }
}
