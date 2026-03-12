using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public class DAL_TaiKhoan
    {
        private static DAL_TaiKhoan instance;
        public static DAL_TaiKhoan Instance
        {
            get
            {
                if (instance == null) instance = new DAL_TaiKhoan();
                return instance;
            }
        }

        public List<TaiKhoan> LoadDS()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                return db.TaiKhoans.ToList();
            }
        }

        public ET_TaiKhoan DangNhap(string user, string pass)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var tk = db.TaiKhoans.SingleOrDefault(x => x.TenDangNhap == user && x.MatKhau == pass && x.TrangThai == true);
                if (tk != null)
                {
                    return new ET_TaiKhoan(tk.MaTaiKhoan, tk.TenDangNhap, tk.MatKhau, tk.HoTen, tk.VaiTro, tk.TrangThai, tk.NgayTao, tk.NgayCapNhat);
                }
            }
            return null;
        }

        public bool KiemTraTonTai(string user)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                return db.TaiKhoans.Any(x => x.TenDangNhap == user);
            }
        }

        public bool ThemTaiKhoan(ET_TaiKhoan et)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    if (db.TaiKhoans.Any(x => x.TenDangNhap == et.TenDangNhap)) return false;

                    TaiKhoan tk = new TaiKhoan
                    {
                        TenDangNhap = et.TenDangNhap,
                        MatKhau = et.MatKhau,
                        HoTen = et.HoTen,
                        VaiTro = et.VaiTro,
                        TrangThai = et.TrangThai,
                        NgayTao = DateTime.Now
                    };
                    db.TaiKhoans.InsertOnSubmit(tk);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool SuaTaiKhoan(ET_TaiKhoan et)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    TaiKhoan tk = db.TaiKhoans.SingleOrDefault(x => x.MaTaiKhoan == et.MaTaiKhoan);
                    if (tk != null)
                    {
                        tk.MatKhau = et.MatKhau;
                        tk.HoTen = et.HoTen;
                        tk.VaiTro = et.VaiTro;
                        tk.TrangThai = et.TrangThai;
                        tk.NgayCapNhat = DateTime.Now;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch { return false; }
        }

        public bool XoaTaiKhoan(int maTK)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    TaiKhoan tk = db.TaiKhoans.SingleOrDefault(x => x.MaTaiKhoan == maTK);
                    if (tk != null)
                    {
                        db.TaiKhoans.DeleteOnSubmit(tk);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch { return false; }
        }

        public List<TaiKhoan> TimKiem(string tuKhoa)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                return db.TaiKhoans
                    .Where(x => x.TenDangNhap.Contains(tuKhoa) || x.HoTen.Contains(tuKhoa))
                    .ToList();
            }
        }
    }
}
