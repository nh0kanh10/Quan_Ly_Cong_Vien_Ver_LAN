using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ChiTietDatBan
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ChiTietDatBan instance;
        public static DAL_ChiTietDatBan Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ChiTietDatBan();
                return instance;
            }
        }

        public List<ET_ChiTietDatBan> LoadDS()
        {
            return db.GetTable<ChiTietDatBan>().Select(s => new ET_ChiTietDatBan
            {
                Id = s.Id,
                IdDatBan = s.IdDatBan,
                IdBanAn = s.IdBanAn
            }).ToList();
        }

        public List<ET_ChiTietDatBan> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ChiTietDatBan et)
        {
            try {
                ChiTietDatBan obj = new ChiTietDatBan();
                obj.IdDatBan = et.IdDatBan;
                obj.IdBanAn = et.IdBanAn;
                db.GetTable<ChiTietDatBan>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_ChiTietDatBan et)
        {
            try {
                var obj = db.GetTable<ChiTietDatBan>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdDatBan = et.IdDatBan;
                    obj.IdBanAn = et.IdBanAn;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<ChiTietDatBan>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<ChiTietDatBan>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_ChiTietDatBan LayTheoId(int id)
        {
            return db.GetTable<ChiTietDatBan>().Where(x => x.Id == id).Select(s => new ET_ChiTietDatBan {
                Id = s.Id,
                IdDatBan = s.IdDatBan,
                IdBanAn = s.IdBanAn
            }).FirstOrDefault();
        }
    }
}
