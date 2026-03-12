using System;

namespace ET
{
    public class ET_TaiKhoan
    {
        private int _maTaiKhoan;
        private string _tenDangNhap;
        private string _matKhau;
        private string _hoTen;
        private string _vaiTro;
        private bool _trangThai;
        private DateTime _ngayTao;
        private DateTime? _ngayCapNhat;

        public int MaTaiKhoan { get => _maTaiKhoan; set => _maTaiKhoan = value; }
        public string TenDangNhap { get => _tenDangNhap; set => _tenDangNhap = value; }
        public string MatKhau { get => _matKhau; set => _matKhau = value; }
        public string HoTen { get => _hoTen; set => _hoTen = value; }
        public string VaiTro { get => _vaiTro; set => _vaiTro = value; }
        public bool TrangThai { get => _trangThai; set => _trangThai = value; }
        public DateTime NgayTao { get => _ngayTao; set => _ngayTao = value; }
        public DateTime? NgayCapNhat { get => _ngayCapNhat; set => _ngayCapNhat = value; }

        public ET_TaiKhoan() { }

        public ET_TaiKhoan(int maTaiKhoan, string tenDangNhap, string matKhau, string hoTen, string vaiTro, bool trangThai, DateTime ngayTao, DateTime? ngayCapNhat)
        {
            _maTaiKhoan = maTaiKhoan;
            _tenDangNhap = tenDangNhap;
            _matKhau = matKhau;
            _hoTen = hoTen;
            _vaiTro = vaiTro;
            _trangThai = trangThai;
            _ngayTao = ngayTao;
            _ngayCapNhat = ngayCapNhat;
        }
    }
}
