using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_DichVu
    {
        private static DAL_DichVu instance;

        public static DAL_DichVu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_DichVu();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        /*
         * Tìm kiếm 
         * CI: Case Insensitive (không phân biệt chữ hoa hoặc chữ thường)
         * AI: Accent Insensitive (không phân biệt dấu)
         */
        public List<ET_DichVu> TimKiem(string keyword)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                string sql = @"
        SELECT *
        FROM DichVu
        WHERE TenDichVu COLLATE Latin1_General_CI_AI LIKE N'%' + {0} + '%'
        ";

                var ds = db.ExecuteQuery<DichVu>(sql, keyword)
                    .Select(dv => new ET_DichVu
                    {
                        MaDichVu = dv.MaDichVu,
                        MaCode = dv.MaCode,
                        TenDichVu = dv.TenDichVu,
                        MaDanhMuc = dv.MaDanhMuc ?? 0,
                        MaKhuVuc = dv.MaKhuVuc ?? 0,
                        GiaBan = dv.GiaBan,
                        SoLuongTon = dv.SoLuongTon,
                        DonViTinh = dv.DonViTinh,
                        TrangThai = dv.TrangThai,
                        NgayTao = dv.NgayTao,
                        NgayCapNhat = dv.NgayCapNhat ?? DateTime.Now
                    }).ToList();

                return ds;
            }
        }

        public List<ET_DichVu> loadDS()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var ds = from dv in db.DichVus
                         join kv in db.KhuVucs on dv.MaKhuVuc equals kv.MaKhuVuc
                         join dm in db.DanhMucDichVus on dv.MaDanhMuc equals dm.MaDanhMuc
                         select new ET_DichVu
                         {
                             MaDichVu = dv.MaDichVu,
                             MaCode = dv.MaCode,
                             TenDichVu = dv.TenDichVu,
                             MaDanhMuc = dv.MaDanhMuc ?? 0,
                             MaKhuVuc = dv.MaKhuVuc ?? 0,
                             GiaBan = dv.GiaBan,
                             SoLuongTon = dv.SoLuongTon,
                             DonViTinh = dv.DonViTinh,                            
                             TrangThai = dv.TrangThai,
                             NgayTao = dv.NgayTao,
                             NgayCapNhat = dv.NgayCapNhat ?? DateTime.Now
                         };
                return ds.ToList();
            }
        }

        public string LayMaCodeTiepTheo()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                string maxMaCode = db.DichVus
                    .Select(dv => dv.MaCode)
                    .ToList()
                    .OrderByDescending(dv => int.TryParse(dv.Substring(2), out int num) ? num : 0)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(maxMaCode))
                {
                    maxMaCode = "DV000";
                }

                int maxNumber = 0;
                if (maxMaCode.Length >= 2)
                {
                    string numberPart = maxMaCode.Substring(2);
                    int.TryParse(numberPart, out maxNumber);
                }

                int nextNumber = maxNumber + 1;
                string nextMaCode = "DV" + nextNumber.ToString().PadLeft(3, '0');

                return nextMaCode;
            }
        }
        public bool ThemDichVu(ET_DichVu et)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    string nextMaCode = LayMaCodeTiepTheo();
                    DichVu dv = new DichVu
                    {
                        MaCode = nextMaCode,
                        TenDichVu = et.TenDichVu,
                        MaDanhMuc = et.MaDanhMuc,
                        MaKhuVuc = et.MaKhuVuc,
                        GiaBan = et.GiaBan,
                        SoLuongTon = et.SoLuongTon,
                        DonViTinh = et.DonViTinh,
                        TrangThai = et.TrangThai,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = null
                    };
                    db.DichVus.InsertOnSubmit(dv);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool SuaDichVu(ET_DichVu et)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    DichVu dv = db.DichVus.SingleOrDefault(x => x.MaCode == et.MaCode);
                    if (dv != null)
                    {
                        dv.TenDichVu = et.TenDichVu;
                        dv.MaDanhMuc = et.MaDanhMuc;
                        dv.MaKhuVuc = et.MaKhuVuc;
                        dv.GiaBan = et.GiaBan;
                        dv.SoLuongTon = et.SoLuongTon;
                        dv.DonViTinh = et.DonViTinh;
                        dv.TrangThai = et.TrangThai;
                        // NgayTao giữ nguyên, chỉ cập nhật NgayCapNhat
                        dv.NgayCapNhat = DateTime.Now;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool xoaDV(string maCode)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    var dichVu = db.DichVus.SingleOrDefault(tc => tc.MaCode == maCode);
                    if (dichVu != null)
                    {
                        db.DichVus.DeleteOnSubmit(dichVu);
                        db.SubmitChanges();
                    }
                    return true;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 547)
                {
                    return false;
                }
                return false;
            }
        }
        public int LayMaDichVuLonNhat()
        {
            using (var db = new QLKVCGTDataContext())
            {
                // Lấy giá trị ID lớn nhất trong bảng TroChoi
                int maxId = db.DichVus.Any() ? db.DichVus.Max(tc => tc.MaDichVu) : 0;
                return maxId + 1; // giá trị tiếp theo
            }
        }

        public bool KiemTraTrungTen(string tenDichVu, string maCodeHienTai = null)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var query = db.DichVus.Where(lv => lv.TenDichVu == tenDichVu);
                if (!string.IsNullOrEmpty(maCodeHienTai))
                    query = query.Where(lv => lv.MaCode != maCodeHienTai);
                return query.Any();
            }
        }
    }
}
