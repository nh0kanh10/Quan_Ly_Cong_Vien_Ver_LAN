using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;

namespace BUS.Services.DanhMuc
{
    public class BUS_BangGia
    {
        public static BUS_BangGia Instance { get; } = new BUS_BangGia();

        public decimal GetDynamicPrice(int idSanPham, DateTime thoiDiem, out int? idBangGia)
        {
            using (var db = new DAL.DaiNamDBDataContext())
            {
                var sp = db.SanPhams.FirstOrDefault(x => x.Id == idSanPham);
                if (sp == null)
                {
                    idBangGia = null;
                    return 0m;
                }

                var listBg = db.BangGias.Where(b => b.IdSanPham == idSanPham 
                                                && b.HieuLucTu <= thoiDiem 
                                                && b.HieuLucDen >= thoiDiem
                                                && b.TrangThai == AppConstants.TrangThaiHieuLuc.HoatDong).ToList();

                if (!listBg.Any())
                {
                    idBangGia = null;
                    return sp.DonGia ?? 0m;
                }

                // Check ngày lễ
                var isHoliday = db.CauHinhNgayLes.Any(n => n.NgayBatDau <= thoiDiem.Date && n.NgayKetThuc >= thoiDiem.Date);
                
                var holidayPrice = listBg.FirstOrDefault(b => b.LoaiGia == AppConstants.LoaiGiaBan.NgayLe);
                if (isHoliday && holidayPrice != null)
                {
                    idBangGia = holidayPrice.Id;
                    return holidayPrice.GiaBan;
                }

                // Check cuối tuần
                bool isWeekend = thoiDiem.DayOfWeek == DayOfWeek.Saturday || thoiDiem.DayOfWeek == DayOfWeek.Sunday;
                var weekendPrice = listBg.FirstOrDefault(b => b.LoaiGia == AppConstants.LoaiGiaBan.CuoiTuan);
                if (isWeekend && weekendPrice != null)
                {
                    idBangGia = weekendPrice.Id;
                    return weekendPrice.GiaBan;
                }

                // fallback về mặc định nếu không có giá
                var defaultPrice = listBg.FirstOrDefault(b => b.LoaiGia == AppConstants.LoaiGiaBan.MacDinh);
                if (defaultPrice != null)
                {
                    idBangGia = defaultPrice.Id;
                    return defaultPrice.GiaBan;
                }

                idBangGia = null;
                return sp.DonGia ?? 0m;
            }
        }
    }
}


