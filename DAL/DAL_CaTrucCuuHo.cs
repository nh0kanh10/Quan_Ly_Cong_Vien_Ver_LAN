using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_CaTrucCuuHo
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_CaTrucCuuHo instance;
        public static DAL_CaTrucCuuHo Instance
        {
            get
            {
                if (instance == null) instance = new DAL_CaTrucCuuHo();
                return instance;
            }
        }

        public List<ET_CaTrucCuuHo> LoadDS()
        {
            return db.GetTable<CaTrucCuuHo>().Select(s => new ET_CaTrucCuuHo
            {
                Id = s.Id,
                IdNhanVien = s.IdNhanVien,
                IdKhuVucBien = s.IdKhuVucBien,
                ThoiGianBatDau = s.ThoiGianBatDau,
                ThoiGianKetThuc = s.ThoiGianKetThuc
            }).ToList();
        }

        public List<ET_CaTrucCuuHo> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_CaTrucCuuHo et)
        {
            try {
                CaTrucCuuHo obj = new CaTrucCuuHo();
                obj.IdNhanVien = et.IdNhanVien;
                obj.IdKhuVucBien = et.IdKhuVucBien;
                obj.ThoiGianBatDau = et.ThoiGianBatDau;
                obj.ThoiGianKetThuc = et.ThoiGianKetThuc;
                db.GetTable<CaTrucCuuHo>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_CaTrucCuuHo et)
        {
            try {
                var obj = db.GetTable<CaTrucCuuHo>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdNhanVien = et.IdNhanVien;
                    obj.IdKhuVucBien = et.IdKhuVucBien;
                    obj.ThoiGianBatDau = et.ThoiGianBatDau;
                    obj.ThoiGianKetThuc = et.ThoiGianKetThuc;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<CaTrucCuuHo>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<CaTrucCuuHo>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_CaTrucCuuHo LayTheoId(int id)
        {
            return db.GetTable<CaTrucCuuHo>().Where(x => x.Id == id).Select(s => new ET_CaTrucCuuHo {
                Id = s.Id,
                IdNhanVien = s.IdNhanVien,
                IdKhuVucBien = s.IdKhuVucBien,
                ThoiGianBatDau = s.ThoiGianBatDau,
                ThoiGianKetThuc = s.ThoiGianKetThuc
            }).FirstOrDefault();
        }
    }
}
