using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_SuKien
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_SuKien instance;
        public static DAL_SuKien Instance
        {
            get
            {
                if (instance == null) instance = new DAL_SuKien();
                return instance;
            }
        }

        public List<ET_SuKien> LoadDS()
        {
            return db.GetTable<SuKien>().Select(s => new ET_SuKien
            {
                Id = s.Id,
                MaCode = s.MaCode,
                TenSuKien = s.TenSuKien,
                NgayBatDau = s.NgayBatDau,
                NgayKetThuc = s.NgayKetThuc,
                MoTa = s.MoTa,
                TrangThai = s.TrangThai,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).ToList();
        }

        public List<ET_SuKien> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_SuKien et)
        {
            try {
                SuKien obj = new SuKien();
                obj.MaCode = et.MaCode;
                obj.TenSuKien = et.TenSuKien;
                obj.NgayBatDau = et.NgayBatDau;
                obj.NgayKetThuc = et.NgayKetThuc;
                obj.MoTa = et.MoTa;
                obj.TrangThai = et.TrangThai;
                obj.CreatedAt = et.CreatedAt;
                obj.UpdatedAt = et.UpdatedAt;
                obj.CreatedBy = et.CreatedBy;
                obj.IsDeleted = et.IsDeleted;
                db.GetTable<SuKien>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_SuKien et)
        {
            try {
                var obj = db.GetTable<SuKien>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.MaCode = et.MaCode;
                    obj.TenSuKien = et.TenSuKien;
                    obj.NgayBatDau = et.NgayBatDau;
                    obj.NgayKetThuc = et.NgayKetThuc;
                    obj.MoTa = et.MoTa;
                    obj.TrangThai = et.TrangThai;
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
                var obj = db.GetTable<SuKien>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<SuKien>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_SuKien LayTheoId(int id)
        {
            return db.GetTable<SuKien>().Where(x => x.Id == id).Select(s => new ET_SuKien {
                Id = s.Id,
                MaCode = s.MaCode,
                TenSuKien = s.TenSuKien,
                NgayBatDau = s.NgayBatDau,
                NgayKetThuc = s.NgayKetThuc,
                MoTa = s.MoTa,
                TrangThai = s.TrangThai,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).FirstOrDefault();
        }
    }
}
