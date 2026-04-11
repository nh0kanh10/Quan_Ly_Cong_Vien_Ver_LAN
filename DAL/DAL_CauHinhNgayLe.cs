using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public class DAL_CauHinhNgayLe
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_CauHinhNgayLe instance;
        public static DAL_CauHinhNgayLe Instance
        {
            get
            {
                if (instance == null) instance = new DAL_CauHinhNgayLe();
                return instance;
            }
        }

        // Cache events
        private List<ET_CauHinhNgayLe> _cache;
        private DateTime _lastCacheTime;

        public ET_CauHinhNgayLe LayNgayLeChoNgay(DateTime ngay)
        {
            if (_cache == null || (DateTime.Now - _lastCacheTime).TotalMinutes > 5)
            {
                _cache = LoadDS();
                _lastCacheTime = DateTime.Now;
            }
            return _cache.FirstOrDefault(x => ngay.Date >= x.NgayBatDau.Date && ngay.Date <= x.NgayKetThuc.Date);
        }

        public List<ET_CauHinhNgayLe> LoadDS()
        {
            return db.GetTable<CauHinhNgayLe>().Select(s => new ET_CauHinhNgayLe
            {
                Id = s.Id,
                NgayBatDau = s.NgayBatDau,
                NgayKetThuc = s.NgayKetThuc,
                TenNgayLe = s.TenNgayLe,
                MoTa = s.MoTa
            }).OrderBy(x => x.NgayBatDau).ToList();
        }

        public bool Them(ET_CauHinhNgayLe et)
        {
            try
            {
                db.GetTable<CauHinhNgayLe>().InsertOnSubmit(new CauHinhNgayLe
                {
                    NgayBatDau = et.NgayBatDau.Date,
                    NgayKetThuc = et.NgayKetThuc.Date,
                    TenNgayLe = et.TenNgayLe,
                    MoTa = et.MoTa
                });
                db.SubmitChanges();
                _cache = null; // Invalidate cache
                return true;
            }
            catch { return false; }
        }

        public bool Sua(ET_CauHinhNgayLe et)
        {
            try
            {
                var obj = db.GetTable<CauHinhNgayLe>().FirstOrDefault(x => x.Id == et.Id);
                if (obj == null) return false;
                obj.NgayBatDau = et.NgayBatDau.Date;
                obj.NgayKetThuc = et.NgayKetThuc.Date;
                obj.TenNgayLe = et.TenNgayLe;
                obj.MoTa = et.MoTa;
                db.SubmitChanges();
                _cache = null;
                return true;
            }
            catch { return false; }
        }

        public bool Xoa(int id)
        {
            try
            {
                var obj = db.GetTable<CauHinhNgayLe>().FirstOrDefault(x => x.Id == id);
                if (obj == null) return false;
                db.GetTable<CauHinhNgayLe>().DeleteOnSubmit(obj);
                db.SubmitChanges();
                _cache = null;
                return true;
            }
            catch { return false; }
        }
    }
}
