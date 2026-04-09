using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_LichChoAn
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_LichChoAn instance;
        public static DAL_LichChoAn Instance
        {
            get
            {
                if (instance == null) instance = new DAL_LichChoAn();
                return instance;
            }
        }

        public List<ET_LichChoAn> LoadDS()
        {
            return db.GetTable<LichChoAn>().Select(s => new ET_LichChoAn
            {
                Id = s.Id,
                IdDongVat = s.IdDongVat,
                ThoiGian = s.ThoiGian,
                ThucAn = s.ThucAn,
                NguoiPhuTrach = s.NguoiPhuTrach
            }).ToList();
        }

        public List<ET_LichChoAn> LoadTheoDongVat(int idDongVat)
        {
            return db.GetTable<LichChoAn>().Where(x => x.IdDongVat == idDongVat)
                .OrderByDescending(x => x.ThoiGian)
                .Select(s => new ET_LichChoAn
                {
                    Id = s.Id,
                    IdDongVat = s.IdDongVat,
                    ThoiGian = s.ThoiGian,
                    ThucAn = s.ThucAn,
                    NguoiPhuTrach = s.NguoiPhuTrach
                }).ToList();
        }

        public List<ET_LichChoAn> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_LichChoAn et)
        {
            try {
                LichChoAn obj = new LichChoAn();
                obj.IdDongVat = et.IdDongVat;
                obj.ThoiGian = et.ThoiGian;
                obj.ThucAn = et.ThucAn;
                obj.NguoiPhuTrach = et.NguoiPhuTrach;
                db.GetTable<LichChoAn>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_LichChoAn et)
        {
            try {
                var obj = db.GetTable<LichChoAn>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdDongVat = et.IdDongVat;
                    obj.ThoiGian = et.ThoiGian;
                    obj.ThucAn = et.ThucAn;
                    obj.NguoiPhuTrach = et.NguoiPhuTrach;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<LichChoAn>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<LichChoAn>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_LichChoAn LayTheoId(int id)
        {
            return db.GetTable<LichChoAn>().Where(x => x.Id == id).Select(s => new ET_LichChoAn {
                Id = s.Id,
                IdDongVat = s.IdDongVat,
                ThoiGian = s.ThoiGian,
                ThucAn = s.ThucAn,
                NguoiPhuTrach = s.NguoiPhuTrach
            }).FirstOrDefault();
        }
    }
}
