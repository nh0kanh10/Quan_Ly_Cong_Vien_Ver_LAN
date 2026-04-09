using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_DongVat
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_DongVat instance;
        public static DAL_DongVat Instance
        {
            get
            {
                if (instance == null) instance = new DAL_DongVat();
                return instance;
            }
        }

        public List<ET_DongVat> LoadDS()
        {
            return db.GetTable<DongVat>().Select(s => new ET_DongVat
            {
                Id = s.Id,
                Ten = s.Ten,
                Loai = s.Loai,
                MoTa = s.MoTa,
                NgaySinh = s.NgaySinh,
                TinhTrangSucKhoe = s.TinhTrangSucKhoe
            }).ToList();
        }

        public List<ET_DongVat> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_DongVat et)
        {
            try {
                DongVat obj = new DongVat();
                obj.Ten = et.Ten;
                obj.Loai = et.Loai;
                obj.MoTa = et.MoTa;
                obj.NgaySinh = et.NgaySinh;
                obj.TinhTrangSucKhoe = et.TinhTrangSucKhoe;
                db.GetTable<DongVat>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_DongVat et)
        {
            try {
                var obj = db.GetTable<DongVat>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.Ten = et.Ten;
                    obj.Loai = et.Loai;
                    obj.MoTa = et.MoTa;
                    obj.NgaySinh = et.NgaySinh;
                    obj.TinhTrangSucKhoe = et.TinhTrangSucKhoe;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<DongVat>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<DongVat>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_DongVat LayTheoId(int id)
        {
            return db.GetTable<DongVat>().Where(x => x.Id == id).Select(s => new ET_DongVat {
                Id = s.Id,
                Ten = s.Ten,
                Loai = s.Loai,
                MoTa = s.MoTa,
                NgaySinh = s.NgaySinh,
                TinhTrangSucKhoe = s.TinhTrangSucKhoe
            }).FirstOrDefault();
        }
    }
}
