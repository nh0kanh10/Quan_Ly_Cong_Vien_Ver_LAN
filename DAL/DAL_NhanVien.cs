using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_NhanVien
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_NhanVien instance;
        public static DAL_NhanVien Instance
        {
            get
            {
                if (instance == null) instance = new DAL_NhanVien();
                return instance;
            }
        }

        public List<ET_NhanVien> LoadDS()
        {
            return db.GetTable<NhanVien>().Select(s => new ET_NhanVien
            {
                Id = s.Id,
                MaCode = s.MaCode,
                IdVaiTro = s.IdVaiTro,
                IdKhuVuc = s.IdKhuVuc,
                HoTen = s.HoTen,
                GioiTinh = s.GioiTinh,
                NgaySinh = s.NgaySinh,
                ChucVu = s.ChucVu,
                BoPhan = s.BoPhan,
                DienThoai = s.DienThoai,
                Email = s.Email,
                Cccd = s.Cccd,
                DiaChi = s.DiaChi,
                HinhAnh = s.HinhAnh,
                TrangThai = s.TrangThai,
                TenDangNhap = s.TenDangNhap,
                MatKhau = s.MatKhau,
                GhiChu = s.GhiChu,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                IsDeleted = s.IsDeleted
            }).ToList();
        }

        public List<ET_NhanVien> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_NhanVien et)
        {
            try {
                NhanVien obj = new NhanVien();
                obj.MaCode = et.MaCode;
                obj.IdVaiTro = et.IdVaiTro;
                obj.IdKhuVuc = et.IdKhuVuc;
                obj.HoTen = et.HoTen;
                obj.GioiTinh = et.GioiTinh;
                obj.NgaySinh = et.NgaySinh;
                obj.ChucVu = et.ChucVu;
                obj.BoPhan = et.BoPhan;
                obj.DienThoai = et.DienThoai;
                obj.Email = et.Email;
                obj.Cccd = et.Cccd;
                obj.DiaChi = et.DiaChi;
                obj.HinhAnh = et.HinhAnh;
                obj.TrangThai = et.TrangThai;
                obj.TenDangNhap = et.TenDangNhap;
                obj.MatKhau = et.MatKhau;
                obj.GhiChu = et.GhiChu;
                obj.CreatedAt = et.CreatedAt;
                obj.UpdatedAt = et.UpdatedAt;
                obj.IsDeleted = et.IsDeleted;
                db.GetTable<NhanVien>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_NhanVien et)
        {
            try {
                var obj = db.GetTable<NhanVien>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.MaCode = et.MaCode;
                    obj.IdVaiTro = et.IdVaiTro;
                    obj.IdKhuVuc = et.IdKhuVuc;
                    obj.HoTen = et.HoTen;
                    obj.GioiTinh = et.GioiTinh;
                    obj.NgaySinh = et.NgaySinh;
                    obj.ChucVu = et.ChucVu;
                    obj.BoPhan = et.BoPhan;
                    obj.DienThoai = et.DienThoai;
                    obj.Email = et.Email;
                    obj.Cccd = et.Cccd;
                    obj.DiaChi = et.DiaChi;
                    obj.HinhAnh = et.HinhAnh;
                    obj.TrangThai = et.TrangThai;
                    obj.TenDangNhap = et.TenDangNhap;
                    obj.MatKhau = et.MatKhau;
                    obj.GhiChu = et.GhiChu;
                    obj.CreatedAt = et.CreatedAt;
                    obj.UpdatedAt = et.UpdatedAt;
                    obj.IsDeleted = et.IsDeleted;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<NhanVien>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<NhanVien>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_NhanVien LayTheoId(int id)
        {
            return db.GetTable<NhanVien>().Where(x => x.Id == id).Select(s => new ET_NhanVien {
                Id = s.Id,
                MaCode = s.MaCode,
                IdVaiTro = s.IdVaiTro,
                IdKhuVuc = s.IdKhuVuc,
                HoTen = s.HoTen,
                GioiTinh = s.GioiTinh,
                NgaySinh = s.NgaySinh,
                ChucVu = s.ChucVu,
                BoPhan = s.BoPhan,
                DienThoai = s.DienThoai,
                Email = s.Email,
                Cccd = s.Cccd,
                DiaChi = s.DiaChi,
                HinhAnh = s.HinhAnh,
                TrangThai = s.TrangThai,
                TenDangNhap = s.TenDangNhap,
                MatKhau = s.MatKhau,
                GhiChu = s.GhiChu,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                IsDeleted = s.IsDeleted
            }).FirstOrDefault();
        }
    }
}
