using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_KetQuaDua
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_KetQuaDua instance;
        public static DAL_KetQuaDua Instance
        {
            get
            {
                if (instance == null) instance = new DAL_KetQuaDua();
                return instance;
            }
        }

        public List<ET_KetQuaDua> LoadDS()
        {
            return db.GetTable<KetQuaDua>().Select(s => new ET_KetQuaDua
            {
                Id = s.Id,
                IdLichThiDau = s.IdLichThiDau,
                IdVdv = s.IdVdv,
                IdPhuongTienDua = s.IdPhuongTienDua,
                IdNguaDua = s.IdNguaDua,
                ThuTuVeDich = s.ThuTuVeDich,
                ThanhTichThoiGian = s.ThanhTichThoiGian
            }).ToList();
        }

        public List<ET_KetQuaDua> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_KetQuaDua et)
        {
            try {
                KetQuaDua obj = new KetQuaDua();
                obj.IdLichThiDau = et.IdLichThiDau;
                obj.IdVdv = et.IdVdv;
                obj.IdPhuongTienDua = et.IdPhuongTienDua;
                obj.IdNguaDua = et.IdNguaDua;
                obj.ThuTuVeDich = et.ThuTuVeDich;
                obj.ThanhTichThoiGian = et.ThanhTichThoiGian;
                db.GetTable<KetQuaDua>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_KetQuaDua et)
        {
            try {
                var obj = db.GetTable<KetQuaDua>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdLichThiDau = et.IdLichThiDau;
                    obj.IdVdv = et.IdVdv;
                    obj.IdPhuongTienDua = et.IdPhuongTienDua;
                    obj.IdNguaDua = et.IdNguaDua;
                    obj.ThuTuVeDich = et.ThuTuVeDich;
                    obj.ThanhTichThoiGian = et.ThanhTichThoiGian;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<KetQuaDua>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<KetQuaDua>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_KetQuaDua LayTheoId(int id)
        {
            return db.GetTable<KetQuaDua>().Where(x => x.Id == id).Select(s => new ET_KetQuaDua {
                Id = s.Id,
                IdLichThiDau = s.IdLichThiDau,
                IdVdv = s.IdVdv,
                IdPhuongTienDua = s.IdPhuongTienDua,
                IdNguaDua = s.IdNguaDua,
                ThuTuVeDich = s.ThuTuVeDich,
                ThanhTichThoiGian = s.ThanhTichThoiGian
            }).FirstOrDefault();
        }
    }
}
