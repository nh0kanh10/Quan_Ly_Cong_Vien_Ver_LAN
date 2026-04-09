using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ViTriNgoi
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ViTriNgoi instance;
        public static DAL_ViTriNgoi Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ViTriNgoi();
                return instance;
            }
        }

        public List<ET_ViTriNgoi> LoadDS()
        {
            return db.GetTable<ViTriNgoi>().Select(s => new ET_ViTriNgoi
            {
                Id = s.Id,
                MaCode = s.MaCode,
                Hang = s.Hang,
                SoGhe = s.SoGhe,
                LoaiGhe = s.LoaiGhe,
                IdSanPham = s.IdSanPham,
                IdKhanDai = s.IdKhanDai,
                RowVer = s.RowVer == null ? null : s.RowVer.ToArray()
            }).ToList();
        }

        public List<ET_ViTriNgoi> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ViTriNgoi et)
        {
            try {
                ViTriNgoi obj = new ViTriNgoi();
                obj.MaCode = et.MaCode;
                obj.Hang = et.Hang;
                obj.SoGhe = et.SoGhe;
                obj.LoaiGhe = et.LoaiGhe;
                obj.IdSanPham = et.IdSanPham;
                obj.IdKhanDai = et.IdKhanDai;
                obj.RowVer = et.RowVer;
                db.GetTable<ViTriNgoi>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_ViTriNgoi et)
        {
            try {
                var obj = db.GetTable<ViTriNgoi>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.MaCode = et.MaCode;
                    obj.Hang = et.Hang;
                    obj.SoGhe = et.SoGhe;
                    obj.LoaiGhe = et.LoaiGhe;
                    obj.IdSanPham = et.IdSanPham;
                    obj.IdKhanDai = et.IdKhanDai;
                    obj.RowVer = et.RowVer;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<ViTriNgoi>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<ViTriNgoi>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_ViTriNgoi LayTheoId(int id)
        {
            return db.GetTable<ViTriNgoi>().Where(x => x.Id == id).Select(s => new ET_ViTriNgoi {
                Id = s.Id,
                MaCode = s.MaCode,
                Hang = s.Hang,
                SoGhe = s.SoGhe,
                LoaiGhe = s.LoaiGhe,
                IdSanPham = s.IdSanPham,
                IdKhanDai = s.IdKhanDai,
                RowVer = s.RowVer == null ? null : s.RowVer.ToArray()
            }).FirstOrDefault();
        }
    }
}
