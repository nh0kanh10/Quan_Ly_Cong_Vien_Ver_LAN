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

        // Cache ngày lễ trong năm (load 1 lần duy nhất)
        private HashSet<DateTime> _cache;
        private int _cachedYear = -1;

        /// <summary>
        /// Kiểm tra ngày có phải ngày lễ không. Cache theo năm.
        /// </summary>
        public bool LaNgayLe(DateTime ngay)
        {
            if (_cachedYear != ngay.Year)
            {
                _cache = new HashSet<DateTime>(
                    db.GetTable<CauHinhNgayLe>()
                      .Where(x => x.Ngay.Year == ngay.Year)
                      .Select(x => x.Ngay.Date)
                );
                _cachedYear = ngay.Year;
            }
            return _cache.Contains(ngay.Date);
        }

        public List<ET_CauHinhNgayLe> LoadDS()
        {
            return db.GetTable<CauHinhNgayLe>().Select(s => new ET_CauHinhNgayLe
            {
                Ngay = s.Ngay,
                TenNgayLe = s.TenNgayLe
            }).OrderBy(x => x.Ngay).ToList();
        }

        public bool Them(ET_CauHinhNgayLe et)
        {
            try
            {
                db.GetTable<CauHinhNgayLe>().InsertOnSubmit(new CauHinhNgayLe
                {
                    Ngay = et.Ngay.Date,
                    TenNgayLe = et.TenNgayLe
                });
                db.SubmitChanges();
                _cachedYear = -1; // Invalidate cache
                return true;
            }
            catch { return false; }
        }

        public bool Xoa(DateTime ngay)
        {
            try
            {
                var obj = db.GetTable<CauHinhNgayLe>().FirstOrDefault(x => x.Ngay == ngay.Date);
                if (obj == null) return false;
                db.GetTable<CauHinhNgayLe>().DeleteOnSubmit(obj);
                db.SubmitChanges();
                _cachedYear = -1;
                return true;
            }
            catch { return false; }
        }
    }
}
