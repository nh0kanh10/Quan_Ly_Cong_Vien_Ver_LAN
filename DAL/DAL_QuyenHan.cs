using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_QuyenHan
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_QuyenHan instance;
        public static DAL_QuyenHan Instance
        {
            get
            {
                if (instance == null) instance = new DAL_QuyenHan();
                return instance;
            }
        }

        public List<ET_QuyenHan> LoadDS()
        {
            return db.GetTable<QuyenHan>().Select(s => new ET_QuyenHan
            {
                Id = s.Id,
                MaQuyen = s.MaQuyen,
                MoTa = s.MoTa
            }).ToList();
        }

        public List<ET_QuyenHan> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_QuyenHan et)
        {
            try {
                QuyenHan obj = new QuyenHan();
                obj.MaQuyen = et.MaQuyen;
                obj.MoTa = et.MoTa;
                db.GetTable<QuyenHan>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_QuyenHan et)
        {
            try {
                var obj = db.GetTable<QuyenHan>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.MaQuyen = et.MaQuyen;
                    obj.MoTa = et.MoTa;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<QuyenHan>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<QuyenHan>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_QuyenHan LayTheoId(int id)
        {
            return db.GetTable<QuyenHan>().Where(x => x.Id == id).Select(s => new ET_QuyenHan {
                Id = s.Id,
                MaQuyen = s.MaQuyen,
                MoTa = s.MoTa
            }).FirstOrDefault();
        }
    }
}
