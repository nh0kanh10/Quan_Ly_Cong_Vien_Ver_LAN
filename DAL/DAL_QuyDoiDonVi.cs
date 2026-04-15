using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_QuyDoiDonVi
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_QuyDoiDonVi instance;
        public static DAL_QuyDoiDonVi Instance
        {
            get
            {
                if (instance == null) instance = new DAL_QuyDoiDonVi();
                return instance;
            }
        }

        public List<ET_QuyDoiDonVi> LoadDS()
        {
            return db.GetTable<QuyDoiDonVi>().Select(s => new ET_QuyDoiDonVi
            {
                Id = s.Id,
                IdSanPham = s.IdSanPham,
                IdDonViNho = s.IdDonViNho,
                IdDonViLon = s.IdDonViLon,
                TyLeQuyDoi = s.TyLeQuyDoi,
                GiaBanRieng = s.GiaBanRieng,
                CreatedAt = s.CreatedAt
            }).ToList();
        }

        public List<ET_QuyDoiDonVi> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_QuyDoiDonVi et)
        {
            try {
                QuyDoiDonVi obj = new QuyDoiDonVi();
                obj.IdSanPham = et.IdSanPham;
                obj.IdDonViNho = et.IdDonViNho;
                obj.IdDonViLon = et.IdDonViLon;
                obj.TyLeQuyDoi = et.TyLeQuyDoi;
                obj.GiaBanRieng = et.GiaBanRieng;
                obj.CreatedAt = et.CreatedAt;
                db.GetTable<QuyDoiDonVi>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_QuyDoiDonVi et)
        {
            try {
                var obj = db.GetTable<QuyDoiDonVi>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdSanPham = et.IdSanPham;
                    obj.IdDonViNho = et.IdDonViNho;
                    obj.IdDonViLon = et.IdDonViLon;
                    obj.TyLeQuyDoi = et.TyLeQuyDoi;
                    obj.GiaBanRieng = et.GiaBanRieng;
                    obj.CreatedAt = et.CreatedAt;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<QuyDoiDonVi>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<QuyDoiDonVi>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_QuyDoiDonVi LayTheoId(int id)
        {
            return db.GetTable<QuyDoiDonVi>().Where(x => x.Id == id).Select(s => new ET_QuyDoiDonVi {
                Id = s.Id,
                IdSanPham = s.IdSanPham,
                IdDonViNho = s.IdDonViNho,
                IdDonViLon = s.IdDonViLon,
                TyLeQuyDoi = s.TyLeQuyDoi,
                GiaBanRieng = s.GiaBanRieng,
                CreatedAt = s.CreatedAt
            }).FirstOrDefault();
        }
    }
}
