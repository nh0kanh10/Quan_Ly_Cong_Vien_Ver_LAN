using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_SuCo
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_SuCo instance;
        public static DAL_SuCo Instance
        {
            get
            {
                if (instance == null) instance = new DAL_SuCo();
                return instance;
            }
        }

        public List<ET_SuCo> LoadDS()
        {
            return db.GetTable<SuCo>().Select(s => new ET_SuCo
            {
                Id = s.Id,
                IdKhachHang = s.IdKhachHang,
                IdNhanVienXuLy = s.IdNhanVienXuLy,
                ThoiGian = s.ThoiGian,
                MoTa = s.MoTa,
                MucDo = s.MucDo,
                ToaDoGps = s.ToaDoGps,
                LoaiSuCo = s.LoaiSuCo,
                TenKhachHang = s.KhachHang != null ? s.KhachHang.HoTen : "",
                TenNhanVienXuLy = s.NhanVien != null ? s.NhanVien.HoTen : ""
            }).ToList();
        }

        public List<ET_SuCo> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_SuCo et)
        {
            try {
                SuCo obj = new SuCo();
                obj.IdKhachHang = et.IdKhachHang;
                obj.IdNhanVienXuLy = et.IdNhanVienXuLy;
                obj.ThoiGian = et.ThoiGian;
                obj.MoTa = et.MoTa;
                obj.MucDo = et.MucDo;
                obj.ToaDoGps = et.ToaDoGps;
                obj.LoaiSuCo = et.LoaiSuCo;
                db.GetTable<SuCo>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_SuCo et)
        {
            try {
                var obj = db.GetTable<SuCo>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdKhachHang = et.IdKhachHang;
                    obj.IdNhanVienXuLy = et.IdNhanVienXuLy;
                    obj.ThoiGian = et.ThoiGian;
                    obj.MoTa = et.MoTa;
                    obj.MucDo = et.MucDo;
                    obj.ToaDoGps = et.ToaDoGps;
                    obj.LoaiSuCo = et.LoaiSuCo;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<SuCo>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<SuCo>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_SuCo LayTheoId(int id)
        {
            return db.GetTable<SuCo>().Where(x => x.Id == id).Select(s => new ET_SuCo {
                Id = s.Id,
                IdKhachHang = s.IdKhachHang,
                IdNhanVienXuLy = s.IdNhanVienXuLy,
                ThoiGian = s.ThoiGian,
                MoTa = s.MoTa,
                MucDo = s.MucDo,
                ToaDoGps = s.ToaDoGps,
                LoaiSuCo = s.LoaiSuCo,
                TenKhachHang = s.KhachHang != null ? s.KhachHang.HoTen : "",
                TenNhanVienXuLy = s.NhanVien != null ? s.NhanVien.HoTen : ""
            }).FirstOrDefault();
        }

        public List<ET_SuCo> LayTheoKhachHang(int idKhachHang)
        {
            return db.GetTable<SuCo>()
                .Where(x => x.IdKhachHang == idKhachHang)
                .OrderByDescending(x => x.ThoiGian)
                .Select(s => new ET_SuCo
                {
                    Id = s.Id,
                    IdKhachHang = s.IdKhachHang,
                    IdNhanVienXuLy = s.IdNhanVienXuLy,
                    ThoiGian = s.ThoiGian,
                    MoTa = s.MoTa,
                    MucDo = s.MucDo,
                    ToaDoGps = s.ToaDoGps,
                    LoaiSuCo = s.LoaiSuCo,
                    TenKhachHang = s.KhachHang != null ? s.KhachHang.HoTen : "",
                    TenNhanVienXuLy = s.NhanVien != null ? s.NhanVien.HoTen : ""
                }).ToList();
        }
    }
}
