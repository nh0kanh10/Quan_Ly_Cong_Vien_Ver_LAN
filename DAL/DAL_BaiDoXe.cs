using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_BaiDoXe
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_BaiDoXe instance;
        public static DAL_BaiDoXe Instance
        {
            get
            {
                if (instance == null) instance = new DAL_BaiDoXe();
                return instance;
            }
        }

        public List<ET_BaiDoXe> LoadDS()
        {
            return db.GetTable<BaiDoXe>().Select(s => new ET_BaiDoXe
            {
                Id = s.Id,
                TenBai = s.TenBai,
                TongCho = s.TongCho
            }).ToList();
        }

        public List<ET_BaiDoXe> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_BaiDoXe et)
        {
            try {
                BaiDoXe obj = new BaiDoXe();
                obj.TenBai = et.TenBai;
                obj.TongCho = et.TongCho;
                db.GetTable<BaiDoXe>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_BaiDoXe et)
        {
            try {
                var obj = db.GetTable<BaiDoXe>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.TenBai = et.TenBai;
                    obj.TongCho = et.TongCho;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<BaiDoXe>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<BaiDoXe>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_BaiDoXe LayTheoId(int id)
        {
            return db.GetTable<BaiDoXe>().Where(x => x.Id == id).Select(s => new ET_BaiDoXe {
                Id = s.Id,
                TenBai = s.TenBai,
                TongCho = s.TongCho
            }).FirstOrDefault();
        }
    }
}
