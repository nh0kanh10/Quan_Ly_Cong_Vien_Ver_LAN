using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public class DAL_ThatLac
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_ThatLac instance;
        public static DAL_ThatLac Instance
        {
            get
            {
                if (instance == null) instance = new DAL_ThatLac();
                return instance;
            }
        }

        public List<ET_ThatLac> LoadDS()
        {
            return db.GetTable<ThatLac>().Select(t => new ET_ThatLac
            {
                Id = t.Id,
                MoTaDoVat = t.MoTaDoVat,
                NoiTimThay = t.NoiTimThay,
                TrangThai = t.TrangThai,
                ThoiGian = t.ThoiGian,
                IdKhachHangNhan = t.IdKhachHangNhan,
                TenKhachNhan = t.IdKhachHangNhan != null ? t.KhachHang.HoTen : ""
            }).ToList();
        }

        public bool Them(ET_ThatLac et)
        {
            try
            {
                ThatLac obj = new ThatLac
                {
                    MoTaDoVat = et.MoTaDoVat,
                    NoiTimThay = et.NoiTimThay,
                    TrangThai = et.TrangThai,
                    ThoiGian = et.ThoiGian,
                    IdKhachHangNhan = et.IdKhachHangNhan
                };
                db.GetTable<ThatLac>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Sua(ET_ThatLac et)
        {
            try
            {
                var obj = db.GetTable<ThatLac>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null)
                {
                    obj.MoTaDoVat = et.MoTaDoVat;
                    obj.NoiTimThay = et.NoiTimThay;
                    obj.TrangThai = et.TrangThai;
                    obj.ThoiGian = et.ThoiGian;
                    obj.IdKhachHangNhan = et.IdKhachHangNhan;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public bool Xoa(int id)
        {
            try
            {
                var obj = db.GetTable<ThatLac>().FirstOrDefault(x => x.Id == id);
                if (obj != null)
                {
                    db.GetTable<ThatLac>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch { return false; }
        }
    }
}
