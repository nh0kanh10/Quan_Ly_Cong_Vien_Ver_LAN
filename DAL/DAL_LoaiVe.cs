using ET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class DAL_LoaiVe
    {
        private static DAL_LoaiVe instance;
        public static DAL_LoaiVe Instance
        {
            get
            {
                if (instance == null) instance = new DAL_LoaiVe();
                return instance;
            }
        }

        public List<ET_LoaiVe> LoadDS()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var ds = from lv in db.LoaiVes
                         select new ET_LoaiVe
                         {
                             MaLoaiVe = lv.MaLoaiVe,
                             MaCode = lv.MaCode,
                             TenLoaiVe = lv.TenLoaiVe,
                             GiaVe = lv.GiaVe,
                             //GiaCuoiTuan = lv.GiaCuoiTuan,
                             DoiTuong = lv.DoiTuong,
                             LaCombo = lv.LaCombo,
                             TrangThai = lv.TrangThai,
                             NgayTao = lv.NgayTao,
                             NgayCapNhat = lv.NgayCapNhat
                         };
                return ds.ToList();
            }
        }

        public List<ET_LoaiVe> TimKiem(string tuKhoa)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var ds = from lv in db.LoaiVes
                         where lv.TenLoaiVe.Contains(tuKhoa)
                            || lv.MaCode.Contains(tuKhoa)
                         select new ET_LoaiVe
                         {
                             MaLoaiVe = lv.MaLoaiVe,
                             MaCode = lv.MaCode,
                             TenLoaiVe = lv.TenLoaiVe,
                             GiaVe = lv.GiaVe,
                             //GiaCuoiTuan = lv.GiaCuoiTuan,
                             DoiTuong = lv.DoiTuong,
                             LaCombo = lv.LaCombo,
                             TrangThai = lv.TrangThai,
                             NgayTao = lv.NgayTao,
                             NgayCapNhat = lv.NgayCapNhat
                         };
                return ds.ToList();
            }
        }

        public string LayMaCodeTiepTheo()
        {
            /// Tạo mã code tiếp theo: T01, T02, ...
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                string maxMaCode = db.LoaiVes
                    .Select(lv => lv.MaCode)
                    .ToList()
                    .OrderByDescending(mc => int.TryParse(mc.Substring(1), out int num) ? num : 0)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(maxMaCode))
                    maxMaCode = "T000";

                int maxNumber = 0;
                if (maxMaCode.Length >= 2)
                    int.TryParse(maxMaCode.Substring(1), out maxNumber);

                return "T" + (maxNumber + 1).ToString("D3");
            }
        }


        public bool KiemTraTrungTen(string tenLoaiVe, string maCodeHienTai = null)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var query = db.LoaiVes.Where(lv => lv.TenLoaiVe == tenLoaiVe);
                if (!string.IsNullOrEmpty(maCodeHienTai))
                    query = query.Where(lv => lv.MaCode != maCodeHienTai);
                return query.Any();
            }
        }

        public bool ThemLoaiVe(ET_LoaiVe et)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    string nextMaCode = LayMaCodeTiepTheo();
                    LoaiVe lv = new LoaiVe
                    {
                        MaCode = nextMaCode,
                        TenLoaiVe = et.TenLoaiVe,
                        GiaVe = et.GiaVe,
                        //GiaCuoiTuan = et.GiaCuoiTuan,
                        DoiTuong = et.DoiTuong,
                        LaCombo = et.LaCombo,
                        TrangThai = et.TrangThai,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = null,
                    };
                    db.LoaiVes.InsertOnSubmit(lv);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool SuaLoaiVe(ET_LoaiVe et)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    LoaiVe lv = db.LoaiVes.SingleOrDefault(x => x.MaCode == et.MaCode);
                    if (lv != null)
                    {
                        lv.TenLoaiVe = et.TenLoaiVe;
                        lv.GiaVe = et.GiaVe;
                        //lv.GiaCuoiTuan = et.GiaCuoiTuan;
                        lv.DoiTuong = et.DoiTuong;
                        lv.LaCombo = et.LaCombo;
                        lv.TrangThai = et.TrangThai;
                        lv.NgayCapNhat = DateTime.Now;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch { return false; }
        }

        public bool XoaLoaiVe(string maCode)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    var lv = db.LoaiVes.SingleOrDefault(x => x.MaCode == maCode);
                    if (lv != null)
                    {
                        db.LoaiVes.DeleteOnSubmit(lv);
                        db.SubmitChanges();
                    }
                    return true;
                }
            }
            catch { return false; }
        }
    }
}
