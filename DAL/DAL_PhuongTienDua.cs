using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_PhuongTienDua
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_PhuongTienDua instance;
        public static DAL_PhuongTienDua Instance
        {
            get
            {
                if (instance == null) instance = new DAL_PhuongTienDua();
                return instance;
            }
        }

        public List<ET_PhuongTienDua> LoadDS()
        {
            return db.GetTable<PhuongTienDua>().Select(s => new ET_PhuongTienDua
            {
                Id = s.Id,
                TenXe = s.TenXe,
                IdVdv = s.IdVdv,
                TinhTrang = s.TinhTrang
            }).ToList();
        }

        public List<ET_PhuongTienDua> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_PhuongTienDua et)
        {
            try {
                PhuongTienDua obj = new PhuongTienDua();
                obj.TenXe = et.TenXe;
                obj.IdVdv = et.IdVdv;
                obj.TinhTrang = et.TinhTrang;
                db.GetTable<PhuongTienDua>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_PhuongTienDua et)
        {
            try {
                var obj = db.GetTable<PhuongTienDua>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.TenXe = et.TenXe;
                    obj.IdVdv = et.IdVdv;
                    obj.TinhTrang = et.TinhTrang;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<PhuongTienDua>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<PhuongTienDua>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_PhuongTienDua LayTheoId(int id)
        {
            return db.GetTable<PhuongTienDua>().Where(x => x.Id == id).Select(s => new ET_PhuongTienDua {
                Id = s.Id,
                TenXe = s.TenXe,
                IdVdv = s.IdVdv,
                TinhTrang = s.TinhTrang
            }).FirstOrDefault();
        }
    }
}
