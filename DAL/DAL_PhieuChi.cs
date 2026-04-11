using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_PhieuChi
    {
        private static DAL_PhieuChi instance;
        private DataQuanLyDaiNamDataContext CreateDb() { return new DataQuanLyDaiNamDataContext(); }
        public static DAL_PhieuChi Instance
        {
            get
            {
                if (instance == null) instance = new DAL_PhieuChi();
                return instance;
            }
        }

        public List<ET_PhieuChi> LoadDS()
        {
            using (var db = CreateDb())
            {
                return db.GetTable<PhieuChi>().Select(s => new ET_PhieuChi
                {
                    Id = s.Id,
                    MaCode = s.MaCode,
                    SoTien = s.SoTien,
                    LyDo = s.LyDo,
                    ThoiGian = s.ThoiGian,
                    IdDonHang = s.IdDonHang,
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedBy
                }).ToList();
            }
        }

        public List<ET_PhieuChi> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_PhieuChi et)
        {
            if (et == null) return false;
            try {
                using (var db = CreateDb())
                {
                    PhieuChi obj = new PhieuChi();
                    obj.MaCode = et.MaCode;
                    obj.SoTien = et.SoTien;
                    obj.LyDo = et.LyDo;
                    obj.ThoiGian = et.ThoiGian;
                    obj.IdDonHang = et.IdDonHang;
                    obj.CreatedAt = et.CreatedAt;
                    obj.CreatedBy = et.CreatedBy;
                    db.GetTable<PhieuChi>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                }
                return true;
            } catch (Exception ex) { DalErrorLogger.Log("DAL_PhieuChi.Them", ex); return false; }
        }

        public bool Sua(ET_PhieuChi et)
        {
            if (et == null) return false;
            try {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<PhieuChi>().FirstOrDefault(x => x.Id == et.Id);
                    if (obj != null) {
                        obj.MaCode = et.MaCode;
                        obj.SoTien = et.SoTien;
                        obj.LyDo = et.LyDo;
                        obj.ThoiGian = et.ThoiGian;
                        obj.IdDonHang = et.IdDonHang;
                        obj.CreatedAt = et.CreatedAt;
                        obj.CreatedBy = et.CreatedBy;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            } catch (Exception ex) { 
                DalErrorLogger.Log("DAL_PhieuChi.Sua", ex);
                return false; 
            }
        }

        public bool Xoa(int id)
        {
            try {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<PhieuChi>().FirstOrDefault(x => x.Id == id);
                    if (obj != null) {
                        db.GetTable<PhieuChi>().DeleteOnSubmit(obj);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            } catch (Exception ex) { DalErrorLogger.Log("DAL_PhieuChi.Xoa", ex); return false; }
        }

        public ET_PhieuChi LayTheoId(int id)
        {
            using (var db = CreateDb())
            {
                return db.GetTable<PhieuChi>().Where(x => x.Id == id).Select(s => new ET_PhieuChi {
                    Id = s.Id,
                    MaCode = s.MaCode,
                    SoTien = s.SoTien,
                    LyDo = s.LyDo,
                    ThoiGian = s.ThoiGian,
                    IdDonHang = s.IdDonHang,
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedBy
                }).FirstOrDefault();
            }
        }
    }
}
