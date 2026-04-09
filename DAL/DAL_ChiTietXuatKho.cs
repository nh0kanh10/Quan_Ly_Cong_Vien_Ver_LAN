using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ChiTietXuatKho
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ChiTietXuatKho instance;
        public static DAL_ChiTietXuatKho Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ChiTietXuatKho();
                return instance;
            }
        }

        public List<ET_ChiTietXuatKho> LoadDS()
        {
            return db.GetTable<ChiTietXuatKho>().Select(s => new ET_ChiTietXuatKho
            {
                Id = s.Id,
                IdPhieuXuat = s.IdPhieuXuat,
                IdSanPham = s.IdSanPham,
                SoLuong = s.SoLuong,
                DonGiaXuat = s.DonGiaXuat
            }).ToList();
        }

        public List<ET_ChiTietXuatKho> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ChiTietXuatKho et)
        {
            try {
                ChiTietXuatKho obj = new ChiTietXuatKho();
                obj.IdPhieuXuat = et.IdPhieuXuat;
                obj.IdSanPham = et.IdSanPham;
                obj.SoLuong = et.SoLuong;
                obj.DonGiaXuat = et.DonGiaXuat;
                db.GetTable<ChiTietXuatKho>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_ChiTietXuatKho et)
        {
            try {
                var obj = db.GetTable<ChiTietXuatKho>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdPhieuXuat = et.IdPhieuXuat;
                    obj.IdSanPham = et.IdSanPham;
                    obj.SoLuong = et.SoLuong;
                    obj.DonGiaXuat = et.DonGiaXuat;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<ChiTietXuatKho>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<ChiTietXuatKho>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_ChiTietXuatKho LayTheoId(int id)
        {
            return db.GetTable<ChiTietXuatKho>().Where(x => x.Id == id).Select(s => new ET_ChiTietXuatKho {
                Id = s.Id,
                IdPhieuXuat = s.IdPhieuXuat,
                IdSanPham = s.IdSanPham,
                SoLuong = s.SoLuong,
                DonGiaXuat = s.DonGiaXuat
            }).FirstOrDefault();
        }
    }
}
