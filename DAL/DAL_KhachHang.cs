using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_KhachHang
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_KhachHang instance;
        public static DAL_KhachHang Instance
        {
            get
            {
                if (instance == null) instance = new DAL_KhachHang();
                return instance;
            }
        }

        public List<ET_KhachHang> LoadDS()
        {
            return db.GetTable<KhachHang>().Select(s => new ET_KhachHang
            {
                Id = s.Id,
                MaCode = s.MaCode,
                HoTen = s.HoTen,
                NgaySinh = s.NgaySinh,
                GioiTinh = s.GioiTinh,
                Email = s.Email,
                DienThoai = s.DienThoai,
                CmndCccd = s.CmndCccd,
                DiaChi = s.DiaChi,
                LoaiKhach = s.LoaiKhach,
                DiemTichLuy = s.DiemTichLuy,
                IdDoan = s.IdDoan,
                HinhAnh = s.HinhAnh,
                NgayDangKy = s.NgayDangKy,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted,
                TongChiTieu = s.TongChiTieu
            }).ToList();
        }

        public List<ET_KhachHang> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_KhachHang et)
        {
            try {
                KhachHang obj = new KhachHang();
                obj.MaCode = et.MaCode;
                obj.HoTen = et.HoTen;
                obj.NgaySinh = et.NgaySinh;
                obj.GioiTinh = et.GioiTinh;
                obj.Email = et.Email;
                obj.DienThoai = et.DienThoai;
                obj.CmndCccd = et.CmndCccd;
                obj.DiaChi = et.DiaChi;
                obj.LoaiKhach = et.LoaiKhach;
                obj.DiemTichLuy = et.DiemTichLuy;
                obj.IdDoan = et.IdDoan;
                obj.HinhAnh = et.HinhAnh;
                obj.GhiChu = et.GhiChu;
                obj.NgayDangKy = et.NgayDangKy;
                obj.CreatedAt = et.CreatedAt;
                obj.UpdatedAt = et.UpdatedAt;
                obj.CreatedBy = et.CreatedBy;
                obj.IsDeleted = et.IsDeleted;
                db.GetTable<KhachHang>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_KhachHang et)
        {
            try {
                var obj = db.GetTable<KhachHang>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.MaCode = et.MaCode;
                    obj.HoTen = et.HoTen;
                    obj.NgaySinh = et.NgaySinh;
                    obj.GioiTinh = et.GioiTinh;
                    obj.Email = et.Email;
                    obj.DienThoai = et.DienThoai;
                    obj.CmndCccd = et.CmndCccd;
                    obj.DiaChi = et.DiaChi;
                    obj.LoaiKhach = et.LoaiKhach;
                    obj.DiemTichLuy = et.DiemTichLuy;
                    obj.IdDoan = et.IdDoan;
                    obj.HinhAnh = et.HinhAnh;
                    obj.GhiChu = et.GhiChu;
                    obj.NgayDangKy = et.NgayDangKy;
                    obj.CreatedAt = et.CreatedAt;
                    obj.UpdatedAt = et.UpdatedAt;
                    obj.CreatedBy = et.CreatedBy;
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
                var obj = db.GetTable<KhachHang>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<KhachHang>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_KhachHang LayTheoId(int id)
        {
            return db.GetTable<KhachHang>().Where(x => x.Id == id).Select(s => new ET_KhachHang {
                Id = s.Id,
                MaCode = s.MaCode,
                HoTen = s.HoTen,
                NgaySinh = s.NgaySinh,
                GioiTinh = s.GioiTinh,
                Email = s.Email,
                DienThoai = s.DienThoai,
                CmndCccd = s.CmndCccd,
                DiaChi = s.DiaChi,
                LoaiKhach = s.LoaiKhach,
                DiemTichLuy = s.DiemTichLuy,
                IdDoan = s.IdDoan,
                HinhAnh = s.HinhAnh,
                NgayDangKy = s.NgayDangKy,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted,
                TongChiTieu = s.TongChiTieu
            }).FirstOrDefault();
        }

        public ET_KhachHang LayTheoMaCodeHoacSdt(string keyword)
        {
            var s = db.GetTable<KhachHang>().FirstOrDefault(x => !x.IsDeleted && (x.MaCode == keyword || x.DienThoai == keyword));

            if (s == null) return null;

            return new ET_KhachHang
            {
                Id = s.Id,
                MaCode = s.MaCode,
                HoTen = s.HoTen,
                NgaySinh = s.NgaySinh,
                GioiTinh = s.GioiTinh,
                Email = s.Email,
                DienThoai = s.DienThoai,
                CmndCccd = s.CmndCccd,
                DiaChi = s.DiaChi,
                LoaiKhach = s.LoaiKhach,
                DiemTichLuy = s.DiemTichLuy,
                IdDoan = s.IdDoan,
                HinhAnh = s.HinhAnh,
                NgayDangKy = s.NgayDangKy,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted,
                TongChiTieu = s.TongChiTieu
            };
        }

        public bool CapNhatChiTieuVaHang(int idKhachHang, decimal thucThu)
        {
            try
            {
                db.ExecuteCommand("EXEC sp_CapNhatChiTieuVaHang {0}, {1}", idKhachHang, thucThu);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
