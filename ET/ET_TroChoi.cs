using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ET_TroChoi
    {
        private int maTroChoi, maKhuVuc, sucChua, tuoiToiThieu, chieuCaoToiThieu, thoiGianLuot;
        private string maCode, tenTroChoi, loaiTroChoi, moTa, trangThai;
        private DateTime ngayTao;
        private DateTime? ngayCapNhat;

        public ET_TroChoi() { }
        public ET_TroChoi(int maTroChoi, int maKhuVuc, int sucChua, int tuoiToiThieu, int chieuCaoToiThieu, int thoiGianLuot, string maCode, string tenTroChoi, string loaiTroChoi, string moTa, string trangThai, DateTime ngayTao, DateTime ngayCapNhat)
        {
            this.MaTroChoi = maTroChoi;
            this.MaKhuVuc = maKhuVuc;
            this.SucChua = sucChua;
            this.TuoiToiThieu = tuoiToiThieu;
            this.ChieuCaoToiThieu = chieuCaoToiThieu;
            this.ThoiGianLuot = thoiGianLuot;
            this.MaCode = maCode;
            this.TenTroChoi = tenTroChoi;
            this.LoaiTroChoi = loaiTroChoi;
            this.MoTa = moTa;
            this.TrangThai = trangThai;
            this.NgayTao = ngayTao;
            this.NgayCapNhat = ngayCapNhat;
        }

        public int MaTroChoi { get => maTroChoi; set => maTroChoi = value; }
        public int MaKhuVuc { get => maKhuVuc; set => maKhuVuc = value; }
        public int SucChua { get => sucChua; set => sucChua = value; }
        public int TuoiToiThieu { get => tuoiToiThieu; set => tuoiToiThieu = value; }
        public int ChieuCaoToiThieu { get => chieuCaoToiThieu; set => chieuCaoToiThieu = value; }
        public int ThoiGianLuot { get => thoiGianLuot; set => thoiGianLuot = value; }
        public string MaCode { get => maCode; set => maCode = value; }
        public string TenTroChoi { get => tenTroChoi; set => tenTroChoi = value; }
        public string LoaiTroChoi { get => loaiTroChoi; set => loaiTroChoi = value; }
        public string MoTa { get => moTa; set => moTa = value; }
        public string TrangThai { get => trangThai; set => trangThai = value; }
        public DateTime NgayTao { get => ngayTao; set => ngayTao = value; }
        public DateTime? NgayCapNhat { get => ngayCapNhat; set => ngayCapNhat = value; }
    }
}
