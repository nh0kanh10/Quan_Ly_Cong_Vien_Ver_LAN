using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_TroChoi
    {
        private static DAL_TroChoi instance;

        public static DAL_TroChoi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_TroChoi();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public List<ET_TroChoi> loadDS()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var ds = from tc in db.TroChois
                         join kv in db.KhuVucs on tc.MaKhuVuc equals kv.MaKhuVuc
                         select new ET_TroChoi
                         {
                             MaTroChoi = tc.MaTroChoi,
                             MaCode = tc.MaCode,
                             TenTroChoi = tc.TenTroChoi,
                             MaKhuVuc = tc.MaKhuVuc,
                             LoaiTroChoi = tc.LoaiTroChoi,
                             SucChua = tc.SucChua,
                             TuoiToiThieu = tc.TuoiToiThieu,
                             ChieuCaoToiThieu = tc.ChieuCaoToiThieu,
                             ThoiGianLuot = tc.ThoiGianLuot,
                             MoTa = tc.MoTa,
                             TrangThai = tc.TrangThai,
                             NgayTao = tc.NgayTao,
                             NgayCapNhat = tc.NgayCapNhat
                         };
                return ds.ToList();
            }
        }

        /// <summary>
        /// Tìm kiếm trò chơi theo tên (không phân biệt hoa-thường)
        /// </summary>
        public List<ET_TroChoi> TimKiem(string tuKhoa)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var ds = from tc in db.TroChois
                         join kv in db.KhuVucs on tc.MaKhuVuc equals kv.MaKhuVuc
                         where tc.TenTroChoi.Contains(tuKhoa)
                            || tc.MaCode.Contains(tuKhoa)
                         select new ET_TroChoi
                         {
                             MaTroChoi = tc.MaTroChoi,
                             MaCode = tc.MaCode,
                             TenTroChoi = tc.TenTroChoi,
                             MaKhuVuc = tc.MaKhuVuc,
                             LoaiTroChoi = tc.LoaiTroChoi,
                             SucChua = tc.SucChua,
                             TuoiToiThieu = tc.TuoiToiThieu,
                             ChieuCaoToiThieu = tc.ChieuCaoToiThieu,
                             ThoiGianLuot = tc.ThoiGianLuot,
                             MoTa = tc.MoTa,
                             TrangThai = tc.TrangThai,
                             NgayTao = tc.NgayTao,
                             NgayCapNhat = tc.NgayCapNhat
                         };
                return ds.ToList();
            }
        }

        /// <summary>
        /// Tạo mã code tiếp theo theo format G + 3 chữ số (G001, G002...)
        /// </summary>
        public string LayMaCodeTiepTheo()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                string maxMaCode = db.TroChois
                    .Select(t => t.MaCode)
                    .ToList()
                    .OrderByDescending(t => int.TryParse(t.Substring(1), out int num) ? num : 0)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(maxMaCode))
                {
                    maxMaCode = "G000";
                }

                int maxNumber = 0;
                if (maxMaCode.Length >= 2)
                {
                    string numberPart = maxMaCode.Substring(1);
                    int.TryParse(numberPart, out maxNumber);
                }

                int nextNumber = maxNumber + 1;
                string nextMaCode = "G" + nextNumber.ToString().PadLeft(3, '0');

                return nextMaCode;
            }
        }

        /// <summary>
        /// Kiểm tra tên trò chơi trùng trong cùng khu vực.
        /// maCodeHienTai = null khi thêm mới, = MaCode hiện tại khi sửa.
        /// </summary>
        public bool KiemTraTrungTen(string tenTroChoi, int maKhuVuc, string maCodeHienTai = null)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var query = db.TroChois.Where(
                    tc => tc.TenTroChoi == tenTroChoi && tc.MaKhuVuc == maKhuVuc);

                if (!string.IsNullOrEmpty(maCodeHienTai))
                {
                    query = query.Where(tc => tc.MaCode != maCodeHienTai);
                }

                return query.Any();
            }
        }

        public bool ThemTroChoi(ET_TroChoi et)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {                  
                    string nextMaCode = LayMaCodeTiepTheo();
                    TroChoi tc = new TroChoi
                    {
                        MaCode = nextMaCode,
                        MaKhuVuc = et.MaKhuVuc,
                        TenTroChoi = et.TenTroChoi,
                        LoaiTroChoi = et.LoaiTroChoi,
                        SucChua = et.SucChua,
                        TuoiToiThieu = et.TuoiToiThieu,
                        ChieuCaoToiThieu = et.ChieuCaoToiThieu,
                        ThoiGianLuot = et.ThoiGianLuot,
                        MoTa = et.MoTa,
                        TrangThai = et.TrangThai,
                        NgayTao = DateTime.Now,
                        NgayCapNhat = null,
                    };
                    db.TroChois.InsertOnSubmit(tc);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool SuaTroChoi(ET_TroChoi et)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    TroChoi t = db.TroChois.SingleOrDefault(x => x.MaCode == et.MaCode);
                    if (t != null)
                    {
                        t.MaKhuVuc = et.MaKhuVuc;
                        t.TenTroChoi = et.TenTroChoi;
                        t.LoaiTroChoi = et.LoaiTroChoi;
                        t.SucChua = et.SucChua;
                        t.TuoiToiThieu = et.TuoiToiThieu;
                        t.ChieuCaoToiThieu = et.ChieuCaoToiThieu;
                        t.ThoiGianLuot = et.ThoiGianLuot;
                        t.MoTa = et.MoTa;
                        t.TrangThai = et.TrangThai;
                        // NgayTao giữ nguyên, chỉ cập nhật NgayCapNhat
                        t.NgayCapNhat = DateTime.Now;
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

        public bool xoaTC(string maCode)
        {
            try
            {
                using (QLKVCGTDataContext db = new QLKVCGTDataContext())
                {
                    var troChoi = db.TroChois.SingleOrDefault(tc => tc.MaCode == maCode);
                    if (troChoi != null)
                    {
                        db.TroChois.DeleteOnSubmit(troChoi);
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
        public int LayMaTroChoiLonNhat()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var nextId = db.ExecuteQuery<decimal>(
                    "SELECT IDENT_CURRENT('TroChoi') + IDENT_INCR('TroChoi')"
                ).First();

                return Convert.ToInt32(nextId);
            }
        }
    }
}
