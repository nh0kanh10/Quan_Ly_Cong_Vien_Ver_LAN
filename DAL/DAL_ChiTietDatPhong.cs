using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_ChiTietDatPhong
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ChiTietDatPhong instance;
        public static DAL_ChiTietDatPhong Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ChiTietDatPhong();
                return instance;
            }
        }

        public List<ET_ChiTietDatPhong> LoadDS()
        {
            return db.GetTable<ChiTietDatPhong>().Select(s => new ET_ChiTietDatPhong
            {
                Id = s.Id,
                IdDatPhongChiTiet = s.IdDatPhongChiTiet,
                IdPhong = s.IdPhong,
                DonGiaThucTe = s.DonGiaThucTe
            }).ToList();
        }

        public List<ET_ChiTietDatPhong> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_ChiTietDatPhong et)
        {
            try {
                ChiTietDatPhong obj = new ChiTietDatPhong();
                obj.IdDatPhongChiTiet = et.IdDatPhongChiTiet;
                obj.IdPhong = et.IdPhong;
                obj.DonGiaThucTe = et.DonGiaThucTe;
                db.GetTable<ChiTietDatPhong>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public int ThemVaLayId(ET_ChiTietDatPhong et)
        {
            try
            {
                ChiTietDatPhong obj = new ChiTietDatPhong();
                obj.IdDatPhongChiTiet = et.IdDatPhongChiTiet;
                obj.IdPhong = et.IdPhong;
                obj.DonGiaThucTe = et.DonGiaThucTe;
                db.GetTable<ChiTietDatPhong>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return obj.Id;
            }
            catch { return 0; }
        }

        public bool Sua(ET_ChiTietDatPhong et)
        {
            try {
                var obj = db.GetTable<ChiTietDatPhong>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.IdDatPhongChiTiet = et.IdDatPhongChiTiet;
                    obj.IdPhong = et.IdPhong;
                    obj.DonGiaThucTe = et.DonGiaThucTe;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<ChiTietDatPhong>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<ChiTietDatPhong>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_ChiTietDatPhong LayTheoId(int id)
        {
            return db.GetTable<ChiTietDatPhong>().Where(x => x.Id == id).Select(s => new ET_ChiTietDatPhong {
                Id = s.Id,
                IdDatPhongChiTiet = s.IdDatPhongChiTiet,
                IdPhong = s.IdPhong,
                DonGiaThucTe = s.DonGiaThucTe
            }).FirstOrDefault();
        }
    }
}
