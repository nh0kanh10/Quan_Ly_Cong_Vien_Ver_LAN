using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_BaoTriPhuongTienDua
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_BaoTriPhuongTienDua instance;
        public static DAL_BaoTriPhuongTienDua Instance
        {
            get
            {
                if (instance == null) instance = new DAL_BaoTriPhuongTienDua();
                return instance;
            }
        }

        public List<ET_BaoTriPhuongTienDua> LoadDS()
        {
            return db.GetTable<BaoTriPhuongTienDua>().Select(s => new ET_BaoTriPhuongTienDua
            {
                Id = s.Id,
                IdPhuongTienDua = s.IdPhuongTienDua,
                IdNguaDua = s.IdNguaDua,
                NgayBaoTri = s.NgayBaoTri,
                NoiDung = s.NoiDung,
                ChiPhi = s.ChiPhi,
                IdPhieuChi = s.IdPhieuChi
            }).ToList();
        }

        public List<ET_BaoTriPhuongTienDua> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_BaoTriPhuongTienDua et)
        {
            try {
                BaoTriPhuongTienDua obj = new BaoTriPhuongTienDua();
                obj.IdPhuongTienDua = et.IdPhuongTienDua;
                obj.IdNguaDua = et.IdNguaDua;
                obj.NgayBaoTri = et.NgayBaoTri;
                obj.NoiDung = et.NoiDung;
                obj.ChiPhi = et.ChiPhi;
                obj.IdPhieuChi = et.IdPhieuChi;
                db.GetTable<BaoTriPhuongTienDua>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_BaoTriPhuongTienDua et)
        {
            try {
                var obj = db.GetTable<BaoTriPhuongTienDua>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdPhuongTienDua = et.IdPhuongTienDua;
                    obj.IdNguaDua = et.IdNguaDua;
                    obj.NgayBaoTri = et.NgayBaoTri;
                    obj.NoiDung = et.NoiDung;
                    obj.ChiPhi = et.ChiPhi;
                    obj.IdPhieuChi = et.IdPhieuChi;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<BaoTriPhuongTienDua>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<BaoTriPhuongTienDua>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_BaoTriPhuongTienDua LayTheoId(int id)
        {
            return db.GetTable<BaoTriPhuongTienDua>().Where(x => x.Id == id).Select(s => new ET_BaoTriPhuongTienDua {
                Id = s.Id,
                IdPhuongTienDua = s.IdPhuongTienDua,
                IdNguaDua = s.IdNguaDua,
                NgayBaoTri = s.NgayBaoTri,
                NoiDung = s.NoiDung,
                ChiPhi = s.ChiPhi,
                IdPhieuChi = s.IdPhieuChi
            }).FirstOrDefault();
        }
    }
}
