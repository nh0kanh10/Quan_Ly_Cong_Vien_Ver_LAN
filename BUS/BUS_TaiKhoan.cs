using System;
using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_TaiKhoan
    {
        private static BUS_TaiKhoan instance;
        public static BUS_TaiKhoan Instance
        {
            get
            {
                if (instance == null) instance = new BUS_TaiKhoan();
                return instance;
            }
        }

        public List<TaiKhoan> LoadDS()
        {
            return DAL_TaiKhoan.Instance.LoadDS();
        }

        public ET_TaiKhoan DangNhap(string user, string pass)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass)) return null;
            return DAL_TaiKhoan.Instance.DangNhap(user, pass);
        }

        public string ValidateTaiKhoan(ET_TaiKhoan et, bool laThem = true)
        {
            if (string.IsNullOrWhiteSpace(et.TenDangNhap)) return "Tên đăng nhập không được trống!";
            if (string.IsNullOrWhiteSpace(et.MatKhau)) return "Mật khẩu không được trống!";
            if (string.IsNullOrWhiteSpace(et.HoTen)) return "Họ tên không được trống!";
            
            if (laThem && DAL_TaiKhoan.Instance.KiemTraTonTai(et.TenDangNhap))
                return "Tên đăng nhập đã tồn tại!";

            return string.Empty;
        }

        public bool ThemTaiKhoan(ET_TaiKhoan et)
        {
            return DAL_TaiKhoan.Instance.ThemTaiKhoan(et);
        }

        public bool SuaTaiKhoan(ET_TaiKhoan et)
        {
            return DAL_TaiKhoan.Instance.SuaTaiKhoan(et);
        }

        public bool XoaTaiKhoan(int maTK)
        {
            return DAL_TaiKhoan.Instance.XoaTaiKhoan(maTK);
        }

        public List<TaiKhoan> TimKiem(string tuKhoa)
        {
            return DAL_TaiKhoan.Instance.TimKiem(tuKhoa);
        }
    }
}
