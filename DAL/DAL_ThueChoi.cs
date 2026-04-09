using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ThueChoi
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ThueChoi instance;
        public static DAL_ThueChoi Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ThueChoi();
                return instance;
            }
        }

        public List<ET_ThueChoi> LoadDS()
        {
            return db.GetTable<ThueChoi>().Select(s => new ET_ThueChoi
            {
                Id = s.Id,
                IdChiTietThue = s.IdChiTietThue,
                IdChoi = s.IdChoi
            }).ToList();
        }

        public List<ET_ThueChoi> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ThueChoi et)
        {
            try {
                ThueChoi obj = new ThueChoi();
                obj.IdChiTietThue = et.IdChiTietThue;
                obj.IdChoi = et.IdChoi;
                db.GetTable<ThueChoi>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_ThueChoi et)
        {
            try {
                var obj = db.GetTable<ThueChoi>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdChiTietThue = et.IdChiTietThue;
                    obj.IdChoi = et.IdChoi;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<ThueChoi>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<ThueChoi>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_ThueChoi LayTheoId(int id)
        {
            return db.GetTable<ThueChoi>().Where(x => x.Id == id).Select(s => new ET_ThueChoi {
                Id = s.Id,
                IdChiTietThue = s.IdChiTietThue,
                IdChoi = s.IdChoi
            }).FirstOrDefault();
        }
    }
}
