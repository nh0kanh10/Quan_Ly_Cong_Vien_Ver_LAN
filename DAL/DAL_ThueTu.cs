using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ThueTu
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ThueTu instance;
        public static DAL_ThueTu Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ThueTu();
                return instance;
            }
        }

        public List<ET_ThueTu> LoadDS()
        {
            return db.GetTable<ThueTu>().Select(s => new ET_ThueTu
            {
                Id = s.Id,
                IdChiTietThue = s.IdChiTietThue,
                IdTuDo = s.IdTuDo,
                MaPin = s.MaPin
            }).ToList();
        }

        public List<ET_ThueTu> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ThueTu et)
        {
            try {
                ThueTu obj = new ThueTu();
                obj.IdChiTietThue = et.IdChiTietThue;
                obj.IdTuDo = et.IdTuDo;
                obj.MaPin = et.MaPin;
                db.GetTable<ThueTu>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_ThueTu et)
        {
            try {
                var obj = db.GetTable<ThueTu>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdChiTietThue = et.IdChiTietThue;
                    obj.IdTuDo = et.IdTuDo;
                    obj.MaPin = et.MaPin;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<ThueTu>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<ThueTu>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_ThueTu LayTheoId(int id)
        {
            return db.GetTable<ThueTu>().Where(x => x.Id == id).Select(s => new ET_ThueTu {
                Id = s.Id,
                IdChiTietThue = s.IdChiTietThue,
                IdTuDo = s.IdTuDo,
                MaPin = s.MaPin
            }).FirstOrDefault();
        }
    }
}
