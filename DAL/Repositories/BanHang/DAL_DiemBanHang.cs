using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;

namespace DAL.Repositories.BanHang
{
    public class DAL_DiemBanHang
    {
        public static DAL_DiemBanHang Instance { get; } = new DAL_DiemBanHang();

        public object GetAllDiemBanHangHoatDong()
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.DiemBanHang_POs
                         .Where(d => d.TrangThai == AppConstants.TrangThaiHieuLuc.HoatDong)
                         .Select(d => new { d.Id, d.MaDiemBan, d.TenDiemBan })
                         .OrderBy(d => d.TenDiemBan)
                         .ToList();
            }
        }

        public int? GetIdDiemBanByMa(string maDiemBan)
        {
            if (string.IsNullOrEmpty(maDiemBan)) return null;
            using (var db = new DaiNamDBDataContext())
            {
                var diemBan = db.DiemBanHang_POs.FirstOrDefault(d => d.MaDiemBan == maDiemBan && d.TrangThai == AppConstants.TrangThaiHieuLuc.HoatDong);
                return diemBan?.Id;
            }
        }
    }
}


