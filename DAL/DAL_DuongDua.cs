using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_DuongDua
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_DuongDua instance;
        public static DAL_DuongDua Instance
        {
            get
            {
                if (instance == null) instance = new DAL_DuongDua();
                return instance;
            }
        }

        public List<ET_DuongDua> LoadDS()
        {
            return db.GetTable<DuongDua>().Select(s => new ET_DuongDua
            {
                Id = s.Id,
                TenDuong = s.TenDuong,
                ChieuDai = s.ChieuDai,
                LoaiMat = s.LoaiMat
            }).ToList();
        }

        public List<ET_DuongDua> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_DuongDua et)
        {
            try {
                DuongDua obj = new DuongDua();
                obj.TenDuong = et.TenDuong;
                obj.ChieuDai = et.ChieuDai;
                obj.LoaiMat = et.LoaiMat;
                db.GetTable<DuongDua>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_DuongDua et)
        {
            try {
                var obj = db.GetTable<DuongDua>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.TenDuong = et.TenDuong;
                    obj.ChieuDai = et.ChieuDai;
                    obj.LoaiMat = et.LoaiMat;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<DuongDua>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<DuongDua>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_DuongDua LayTheoId(int id)
        {
            return db.GetTable<DuongDua>().Where(x => x.Id == id).Select(s => new ET_DuongDua {
                Id = s.Id,
                TenDuong = s.TenDuong,
                ChieuDai = s.ChieuDai,
                LoaiMat = s.LoaiMat
            }).FirstOrDefault();
        }
    }
}
