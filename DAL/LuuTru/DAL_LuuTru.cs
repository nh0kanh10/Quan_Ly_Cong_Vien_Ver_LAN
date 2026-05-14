using System;
using System.Collections.Generic;
using System.Linq;
using ET;
using ET.Constants;

namespace DAL.LuuTru
{
    public class DAL_LuuTru
    {
        public static DAL_LuuTru Instance { get; } = new DAL_LuuTru();

        public PhieuDatPhong GetPhieuDatPhong(int idPhieu, DaiNamDBDataContext db)
        {
            return db.PhieuDatPhongs.FirstOrDefault(p => p.Id == idPhieu);
        }

        public List<ChiTietDatPhong> GetChiTietTheoPhieu(int idPhieu, DaiNamDBDataContext db)
        {
            return db.ChiTietDatPhongs.Where(ct => ct.IdPhieuDatPhong == idPhieu).ToList();
        }

        public ChiTietDatPhong GetChiTietPhongDangO(int idPhong, DaiNamDBDataContext db)
        {
            return db.ChiTietDatPhongs.FirstOrDefault(x => x.IdPhong == idPhong && x.TrangThai == AppConstants.TrangThaiBooking.DangO);
        }

        public Phong GetPhong(int idPhong, DaiNamDBDataContext db)
        {
            return db.Phongs.FirstOrDefault(p => p.Id == idPhong);
        }
    }
}


