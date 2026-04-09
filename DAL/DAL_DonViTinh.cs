using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_DonViTinh
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_DonViTinh instance;
        public static DAL_DonViTinh Instance
        {
            get
            {
                if (instance == null) instance = new DAL_DonViTinh();
                return instance;
            }
        }

        public List<ET_DonViTinh> LoadDS()
        {
            return db.GetTable<DonViTinh>().Select(s => new ET_DonViTinh
            {
                Id = s.Id,
                Ten = s.Ten,
                KyHieu = s.KyHieu,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).ToList();
        }

        public List<ET_DonViTinh> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_DonViTinh et)
        {
            try {
                DonViTinh obj = new DonViTinh();
                obj.Ten = et.Ten;
                obj.KyHieu = et.KyHieu;
                obj.CreatedAt = et.CreatedAt;
                obj.UpdatedAt = et.UpdatedAt;
                obj.CreatedBy = et.CreatedBy;
                obj.IsDeleted = et.IsDeleted;
                db.GetTable<DonViTinh>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_DonViTinh et)
        {
            try {
                var obj = db.GetTable<DonViTinh>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.Ten = et.Ten;
                    obj.KyHieu = et.KyHieu;
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
                var obj = db.GetTable<DonViTinh>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<DonViTinh>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_DonViTinh LayTheoId(int id)
        {
            return db.GetTable<DonViTinh>().Where(x => x.Id == id).Select(s => new ET_DonViTinh {
                Id = s.Id,
                Ten = s.Ten,
                KyHieu = s.KyHieu,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).FirstOrDefault();
        }
    }
}
