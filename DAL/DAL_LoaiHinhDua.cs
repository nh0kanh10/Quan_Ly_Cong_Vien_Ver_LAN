using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_LoaiHinhDua
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_LoaiHinhDua instance;
        public static DAL_LoaiHinhDua Instance
        {
            get
            {
                if (instance == null) instance = new DAL_LoaiHinhDua();
                return instance;
            }
        }

        public List<ET_LoaiHinhDua> LoadDS()
        {
            return db.GetTable<LoaiHinhDua>().Select(s => new ET_LoaiHinhDua
            {
                Id = s.Id,
                TenLoai = s.TenLoai
            }).ToList();
        }

        public List<ET_LoaiHinhDua> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_LoaiHinhDua et)
        {
            try {
                LoaiHinhDua obj = new LoaiHinhDua();
                obj.TenLoai = et.TenLoai;
                db.GetTable<LoaiHinhDua>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_LoaiHinhDua et)
        {
            try {
                var obj = db.GetTable<LoaiHinhDua>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.TenLoai = et.TenLoai;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<LoaiHinhDua>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<LoaiHinhDua>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_LoaiHinhDua LayTheoId(int id)
        {
            return db.GetTable<LoaiHinhDua>().Where(x => x.Id == id).Select(s => new ET_LoaiHinhDua {
                Id = s.Id,
                TenLoai = s.TenLoai
            }).FirstOrDefault();
        }
    }
}
