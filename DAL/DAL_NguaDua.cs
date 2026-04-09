using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_NguaDua
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_NguaDua instance;
        public static DAL_NguaDua Instance
        {
            get
            {
                if (instance == null) instance = new DAL_NguaDua();
                return instance;
            }
        }

        public List<ET_NguaDua> LoadDS()
        {
            return db.GetTable<NguaDua>().Select(s => new ET_NguaDua
            {
                Id = s.Id,
                TenNgua = s.TenNgua,
                IdVdv = s.IdVdv,
                Tuoi = s.Tuoi,
                ThanhTich = s.ThanhTich
            }).ToList();
        }

        public List<ET_NguaDua> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_NguaDua et)
        {
            try {
                NguaDua obj = new NguaDua();
                obj.TenNgua = et.TenNgua;
                obj.IdVdv = et.IdVdv;
                obj.Tuoi = et.Tuoi;
                obj.ThanhTich = et.ThanhTich;
                db.GetTable<NguaDua>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_NguaDua et)
        {
            try {
                var obj = db.GetTable<NguaDua>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.TenNgua = et.TenNgua;
                    obj.IdVdv = et.IdVdv;
                    obj.Tuoi = et.Tuoi;
                    obj.ThanhTich = et.ThanhTich;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<NguaDua>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<NguaDua>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_NguaDua LayTheoId(int id)
        {
            return db.GetTable<NguaDua>().Where(x => x.Id == id).Select(s => new ET_NguaDua {
                Id = s.Id,
                TenNgua = s.TenNgua,
                IdVdv = s.IdVdv,
                Tuoi = s.Tuoi,
                ThanhTich = s.ThanhTich
            }).FirstOrDefault();
        }
    }
}
