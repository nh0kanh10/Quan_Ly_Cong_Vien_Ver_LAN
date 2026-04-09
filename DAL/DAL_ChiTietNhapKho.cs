using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ChiTietNhapKho
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ChiTietNhapKho instance;
        public static DAL_ChiTietNhapKho Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ChiTietNhapKho();
                return instance;
            }
        }

        public List<ET_ChiTietNhapKho> LoadDS()
        {
            return db.GetTable<ChiTietNhapKho>().Select(s => new ET_ChiTietNhapKho
            {
                Id = s.Id,
                IdPhieuNhap = s.IdPhieuNhap,
                IdSanPham = s.IdSanPham,
                SoLuong = s.SoLuong,
                DonGiaNhap = s.DonGiaNhap
            }).ToList();
        }

        public List<ET_ChiTietNhapKho> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ChiTietNhapKho et)
        {
            try {
                ChiTietNhapKho obj = new ChiTietNhapKho();
                obj.IdPhieuNhap = et.IdPhieuNhap;
                obj.IdSanPham = et.IdSanPham;
                obj.SoLuong = et.SoLuong;
                obj.DonGiaNhap = et.DonGiaNhap;
                db.GetTable<ChiTietNhapKho>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_ChiTietNhapKho et)
        {
            try {
                var obj = db.GetTable<ChiTietNhapKho>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdPhieuNhap = et.IdPhieuNhap;
                    obj.IdSanPham = et.IdSanPham;
                    obj.SoLuong = et.SoLuong;
                    obj.DonGiaNhap = et.DonGiaNhap;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<ChiTietNhapKho>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<ChiTietNhapKho>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_ChiTietNhapKho LayTheoId(int id)
        {
            return db.GetTable<ChiTietNhapKho>().Where(x => x.Id == id).Select(s => new ET_ChiTietNhapKho {
                Id = s.Id,
                IdPhieuNhap = s.IdPhieuNhap,
                IdSanPham = s.IdSanPham,
                SoLuong = s.SoLuong,
                DonGiaNhap = s.DonGiaNhap
            }).FirstOrDefault();
        }
    }
}
