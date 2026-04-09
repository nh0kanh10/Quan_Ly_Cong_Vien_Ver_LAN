using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_Combo
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_Combo instance;
        public static DAL_Combo Instance
        {
            get
            {
                if (instance == null) instance = new DAL_Combo();
                return instance;
            }
        }

        public List<ET_Combo> LoadDS()
        {
            return db.GetTable<Combo>().Select(s => new ET_Combo
            {
                Id = s.Id,
                MaCode = s.MaCode,
                Ten = s.Ten,
                Gia = s.Gia,
                MoTa = s.MoTa,
                TrangThai = s.TrangThai,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted
            }).ToList();
        }

        public List<ET_Combo> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_Combo et)
        {
            try {
                Combo obj = new Combo();
                obj.MaCode = et.MaCode;
                obj.Ten = et.Ten;
                obj.Gia = et.Gia;
                obj.MoTa = et.MoTa;
                obj.TrangThai = et.TrangThai;
                obj.CreatedAt = et.CreatedAt;
                obj.UpdatedAt = et.UpdatedAt;
                obj.CreatedBy = et.CreatedBy;
                obj.IsDeleted = et.IsDeleted;
                db.GetTable<Combo>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_Combo et)
        {
            try {
                var obj = db.GetTable<Combo>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.MaCode = et.MaCode;
                    obj.Ten = et.Ten;
                    obj.Gia = et.Gia;
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
                var obj = db.GetTable<Combo>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<Combo>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_Combo LayTheoId(int id)
        {
            return db.GetTable<Combo>().Where(x => x.Id == id).Select(s => new ET_Combo {
                Id = s.Id,
                MaCode = s.MaCode,
                Ten = s.Ten,
                Gia = s.Gia,
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
