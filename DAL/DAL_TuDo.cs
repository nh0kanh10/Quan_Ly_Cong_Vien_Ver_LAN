using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_TuDo
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_TuDo instance;
        public static DAL_TuDo Instance
        {
            get
            {
                if (instance == null) instance = new DAL_TuDo();
                return instance;
            }
        }

        public List<ET_TuDo> LoadDS()
        {
            return db.GetTable<TuDo>().Select(s => new ET_TuDo
            {
                Id = s.Id,
                IdKhuVuc = s.IdKhuVuc,
                MaTu = s.MaTu,
                KichThuoc = s.KichThuoc,
                TrangThai = s.TrangThai
            }).ToList();
        }

        public List<ET_TuDo> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_TuDo et)
        {
            try {
                TuDo obj = new TuDo();
                obj.IdKhuVuc = et.IdKhuVuc;
                obj.MaTu = et.MaTu;
                obj.KichThuoc = et.KichThuoc;
                obj.TrangThai = et.TrangThai;
                db.GetTable<TuDo>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_TuDo et)
        {
            try {
                var obj = db.GetTable<TuDo>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdKhuVuc = et.IdKhuVuc;
                    obj.MaTu = et.MaTu;
                    obj.KichThuoc = et.KichThuoc;
                    obj.TrangThai = et.TrangThai;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<TuDo>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<TuDo>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_TuDo LayTheoId(int id)
        {
            return db.GetTable<TuDo>().Where(x => x.Id == id).Select(s => new ET_TuDo {
                Id = s.Id,
                IdKhuVuc = s.IdKhuVuc,
                MaTu = s.MaTu,
                KichThuoc = s.KichThuoc,
                TrangThai = s.TrangThai
            }).FirstOrDefault();
        }
    }
}
