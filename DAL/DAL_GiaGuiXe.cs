using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_GiaGuiXe
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_GiaGuiXe instance;
        public static DAL_GiaGuiXe Instance
        {
            get
            {
                if (instance == null) instance = new DAL_GiaGuiXe();
                return instance;
            }
        }

        public List<ET_GiaGuiXe> LoadDS()
        {
            return db.GetTable<GiaGuiXe>().Select(s => new ET_GiaGuiXe
            {
                Id = s.Id,
                LoaiXe = s.LoaiXe,
                TenLoaiXe = s.TenLoaiXe,
                GiaBanNgay = s.GiaBanNgay,
                GiaQuaDem = s.GiaQuaDem
            }).ToList();
        }

        public ET_GiaGuiXe LayTheoLoaiXe(string loaiXe)
        {
            return db.GetTable<GiaGuiXe>().Where(x => x.LoaiXe == loaiXe).Select(s => new ET_GiaGuiXe
            {
                Id = s.Id,
                LoaiXe = s.LoaiXe,
                TenLoaiXe = s.TenLoaiXe,
                GiaBanNgay = s.GiaBanNgay,
                GiaQuaDem = s.GiaQuaDem
            }).FirstOrDefault();
        }

        public bool Sua(ET_GiaGuiXe et)
        {
            try {
                var obj = db.GetTable<GiaGuiXe>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.GiaBanNgay = et.GiaBanNgay;
                    obj.GiaQuaDem = et.GiaQuaDem;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }
    }
}
