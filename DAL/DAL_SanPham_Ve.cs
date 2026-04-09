using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public class DAL_SanPham_Ve
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_SanPham_Ve instance;
        public static DAL_SanPham_Ve Instance
        {
            get
            {
                if (instance == null) instance = new DAL_SanPham_Ve();
                return instance;
            }
        }

        public ET_SanPham_Ve LayTheoIdSanPham(int idSanPham)
        {
            return db.GetTable<SanPham_Ve>().Where(x => x.IdSanPham == idSanPham)
                .Select(x => new ET_SanPham_Ve
                {
                    IdSanPham = x.IdSanPham,
                    IdThietBi = x.IdThietBi,
                    SoLuotQuyDoi = x.SoLuotQuyDoi,
                    CanTaoToken = x.CanTaoToken
                }).FirstOrDefault();
        }

        public bool ThemHoacCapNhat(ET_SanPham_Ve et)
        {
            try {
                var existing = db.GetTable<SanPham_Ve>().FirstOrDefault(x => x.IdSanPham == et.IdSanPham);
                if (existing != null)
                {
                    existing.IdThietBi = et.IdThietBi;
                    existing.SoLuotQuyDoi = et.SoLuotQuyDoi;
                    existing.CanTaoToken = et.CanTaoToken;
                }
                else
                {
                    var obj = new SanPham_Ve
                    {
                        IdSanPham = et.IdSanPham,
                        IdThietBi = et.IdThietBi,
                        SoLuotQuyDoi = et.SoLuotQuyDoi,
                        CanTaoToken = et.CanTaoToken
                    };
                    db.GetTable<SanPham_Ve>().InsertOnSubmit(obj);
                }
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Xoa(int idSanPham)
        {
            try {
                var existing = db.GetTable<SanPham_Ve>().FirstOrDefault(x => x.IdSanPham == idSanPham);
                if (existing != null)
                {
                    db.GetTable<SanPham_Ve>().DeleteOnSubmit(existing);
                    db.SubmitChanges();
                }
                return true;
            } catch { return false; }
        }
    }
}
