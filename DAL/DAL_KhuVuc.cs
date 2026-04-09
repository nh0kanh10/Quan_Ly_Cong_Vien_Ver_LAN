using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_KhuVuc
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_KhuVuc instance;
        public static DAL_KhuVuc Instance
        {
            get
            {
                if (instance == null) instance = new DAL_KhuVuc();
                return instance;
            }
        }

        public List<ET_KhuVuc> LoadDS()
        {
            return db.GetTable<KhuVuc>().Select(s => new ET_KhuVuc
            {
                Id = s.Id,
                MaCode = s.MaCode,
                TenKhuVuc = s.TenKhuVuc,
                MoTa = s.MoTa,
                TrangThai = s.TrangThai,
                HinhAnh = s.HinhAnh,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).ToList();
        }

        public List<ET_KhuVuc> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_KhuVuc et)
        {
            try {
                KhuVuc obj = new KhuVuc();
                obj.MaCode = et.MaCode;
                obj.TenKhuVuc = et.TenKhuVuc;
                obj.MoTa = et.MoTa;
                obj.TrangThai = et.TrangThai;
                obj.HinhAnh = et.HinhAnh;
                obj.CreatedAt = et.CreatedAt;
                obj.UpdatedAt = et.UpdatedAt;
                obj.CreatedBy = et.CreatedBy;
                obj.IsDeleted = et.IsDeleted;
                db.GetTable<KhuVuc>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_KhuVuc et)
        {
            try {
                var obj = db.GetTable<KhuVuc>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.MaCode = et.MaCode;
                    obj.TenKhuVuc = et.TenKhuVuc;
                    obj.MoTa = et.MoTa;
                    obj.TrangThai = et.TrangThai;
                    obj.HinhAnh = et.HinhAnh;
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
                var obj = db.GetTable<KhuVuc>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<KhuVuc>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_KhuVuc LayTheoId(int id)
        {
            return db.GetTable<KhuVuc>().Where(x => x.Id == id).Select(s => new ET_KhuVuc {
                Id = s.Id,
                MaCode = s.MaCode,
                TenKhuVuc = s.TenKhuVuc,
                MoTa = s.MoTa,
                TrangThai = s.TrangThai,
                HinhAnh = s.HinhAnh,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).FirstOrDefault();
        }
    }
}
