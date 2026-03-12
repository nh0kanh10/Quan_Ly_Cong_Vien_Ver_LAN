using System;

namespace ET
{
    public class ET_KhuVuc
    {
        public int MaKhuVuc { get; set; }
        public string MaCode { get; set; }
        public string TenKhuVuc { get; set; }
        public string MoTa { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        public ET_KhuVuc() { }

        public ET_KhuVuc(int maKhuVuc, string maCode, string tenKhuVuc, string moTa, string trangThai, DateTime ngayTao, DateTime? ngayCapNhat)
        {
            this.MaKhuVuc = maKhuVuc;
            this.MaCode = maCode;
            this.TenKhuVuc = tenKhuVuc;
            this.MoTa = moTa;
            this.TrangThai = trangThai;
            this.NgayTao = ngayTao;
            this.NgayCapNhat = ngayCapNhat;
        }
    }
}
