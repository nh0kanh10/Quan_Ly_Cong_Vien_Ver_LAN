using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_KhanDai
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_KhanDai instance;
        public static DAL_KhanDai Instance
        {
            get
            {
                if (instance == null) instance = new DAL_KhanDai();
                return instance;
            }
        }

        public List<ET_KhanDai> LoadDS()
        {
            return db.GetTable<KhanDai>().Select(s => new ET_KhanDai
            {
                Id = s.Id,
                TenKhanDai = s.TenKhanDai,
                SucChua = s.SucChua
            }).ToList();
        }

        public List<ET_KhanDai> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_KhanDai et)
        {
            try {
                KhanDai obj = new KhanDai();
                obj.TenKhanDai = et.TenKhanDai;
                obj.SucChua = et.SucChua;
                db.GetTable<KhanDai>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_KhanDai et)
        {
            try {
                var obj = db.GetTable<KhanDai>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.TenKhanDai = et.TenKhanDai;
                    obj.SucChua = et.SucChua;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<KhanDai>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<KhanDai>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_KhanDai LayTheoId(int id)
        {
            return db.GetTable<KhanDai>().Where(x => x.Id == id).Select(s => new ET_KhanDai {
                Id = s.Id,
                TenKhanDai = s.TenKhanDai,
                SucChua = s.SucChua
            }).FirstOrDefault();
        }
    }
}
