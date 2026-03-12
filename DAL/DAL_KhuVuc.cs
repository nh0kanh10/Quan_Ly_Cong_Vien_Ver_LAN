using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_KhuVuc
    {
        private static DAL_KhuVuc instance;

        public static DAL_KhuVuc Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_KhuVuc();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        
        public List<KhuVuc> LoadDSKhuVucHoatDong()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                return db.KhuVucs
                    .Where(kv => kv.TrangThai == "Hoạt động")
                    .OrderBy(kv => kv.MaCode)
                    .ToList();
            }
        }

        /// <summary>
        /// Load tất cả khu vực (dùng cho ComboBox lọc, bao gồm cả ngừng hoạt động)
        /// </summary>
        public List<KhuVuc> LoadDSKhuVuc()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                return db.KhuVucs
                    .OrderBy(kv => kv.MaCode)
                    .ToList();
            }
        }

        public List<KhuVuc> TimKiem(string tuKhoa)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                return db.KhuVucs
                    .Where(kv => kv.TenKhuVuc.Contains(tuKhoa) || kv.MaCode.Contains(tuKhoa))
                    .OrderBy(kv => kv.MaCode)
                    .ToList();
            }
        }

        public string LayMaCodeTiepTheo()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var lastKV = db.KhuVucs
                    .Where(kv => kv.MaCode.StartsWith("KV"))
                    .OrderByDescending(kv => kv.MaCode)
                    .FirstOrDefault();

                if (lastKV == null) return "KV001";

                string lastCode = lastKV.MaCode.Substring(2);
                if (int.TryParse(lastCode, out int lastNum))
                {
                    return "KV" + (lastNum + 1).ToString("D3");
                }
                return "KV001";
            }
        }

        public bool KiemTraTrungTen(string tenKV, string maCodeHienTai = null)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                if (string.IsNullOrEmpty(maCodeHienTai))
                {
                    return db.KhuVucs.Any(kv => kv.TenKhuVuc == tenKV);
                }
                return db.KhuVucs.Any(kv => kv.TenKhuVuc == tenKV && kv.MaCode != maCodeHienTai);
            }
        }

        public bool ThemKhuVuc(ET.ET_KhuVuc et)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    KhuVuc kv = new KhuVuc
                    {
                        MaCode = et.MaCode,
                        TenKhuVuc = et.TenKhuVuc,
                        MoTa = et.MoTa,
                        TrangThai = et.TrangThai,
                        NgayTao = DateTime.Now
                    };
                    db.KhuVucs.InsertOnSubmit(kv);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch { return false; }
        }

        public bool SuaKhuVuc(ET.ET_KhuVuc et)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    KhuVuc kv = db.KhuVucs.SingleOrDefault(x => x.MaCode == et.MaCode);
                    if (kv != null)
                    {
                        kv.TenKhuVuc = et.TenKhuVuc;
                        kv.MoTa = et.MoTa;
                        kv.TrangThai = et.TrangThai;
                        kv.NgayCapNhat = DateTime.Now;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch { return false; }
        }

        public bool XoaKhuVuc(string maCode)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    KhuVuc kv = db.KhuVucs.SingleOrDefault(x => x.MaCode == maCode);
                    if (kv != null)
                    {
                        db.KhuVucs.DeleteOnSubmit(kv);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch { return false; }
        }
    }
}
