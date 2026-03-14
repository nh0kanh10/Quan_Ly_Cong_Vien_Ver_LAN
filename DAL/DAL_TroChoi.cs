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
        QLKVCGTDataContext db = new QLKVCGTDataContext();



        public List<ET_TroChoi> loadDS()
        {
<<<<<<< Updated upstream
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
=======
            using (QLKVCGTDataContext db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))
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
>>>>>>> Stashed changes
        }

        public int LayMaTroChoiLonNhat()
        {
<<<<<<< Updated upstream
            return db.TroChois.Max(x => (int?)x.MaTroChoi) ?? 0;
=======
            using (QLKVCGTDataContext db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))
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
>>>>>>> Stashed changes
        }

        public string LayMaCodeTiepTheo()
        {
<<<<<<< Updated upstream
            string maxMaDatPhong = db.TroChois
            .Select(t => t.MaCode)
            .ToList()
            // Sắp xếp giảm dần và lấy bản ghi đầu tiên
            .OrderByDescending(t => int.TryParse(t.Substring(2), out int num) ? num : 0)
            .FirstOrDefault(); // Trả về mã lớn nhất, hoặc null nếu không có bản ghi nào

            // Xử lý trường hợp bảng rỗng
            if (string.IsNullOrEmpty(maxMaDatPhong))
=======
            using (QLKVCGTDataContext db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))
>>>>>>> Stashed changes
            {
                maxMaDatPhong = "TC000";
            }

<<<<<<< Updated upstream
            // --- Logic tạo mã tiếp theo (Tương tự như trước) ---
            int maxNumber = 0;
            if (maxMaDatPhong.Length >= 4)
=======
        /// <summary>
        /// Kiểm tra tên trò chơi trùng trong cùng khu vực.
        /// maCodeHienTai = null khi thêm mới, = MaCode hiện tại khi sửa.
        /// </summary>
        public bool KiemTraTrungTen(string tenTroChoi, int maKhuVuc, string maCodeHienTai = null)
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))
>>>>>>> Stashed changes
            {
                string numberPart = maxMaDatPhong.Substring(2);
                int.TryParse(numberPart, out maxNumber);
            }

            int nextNumber = maxNumber + 1;
            string nextMaDatPhong = "TC" + nextNumber.ToString().PadLeft(3, '0');

            return nextMaDatPhong;
        }

        public bool ThemTroChoi(ET_TroChoi et)
        {
            try
            {
<<<<<<< Updated upstream
                string nextMaCode = LayMaCodeTiepTheo();
                int nextMaTC = LayMaTroChoiLonNhat();
                TroChoi tc = new TroChoi
                {
                    //gán mã tự động
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
                    NgayTao = et.NgayTao,
                    NgayCapNhat = et.NgayCapNhat,
                };
                db.TroChois.InsertOnSubmit(tc);
                db.SubmitChanges();
                return true;
=======
                using (QLKVCGTDataContext db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))
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
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                TroChoi t = db.TroChois.SingleOrDefault(x => x.MaCode == et.MaCode);
                if(t != null)
=======
                using (QLKVCGTDataContext db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))
>>>>>>> Stashed changes
                {
                    //gán mã tự động

                    t.MaKhuVuc = et.MaKhuVuc;
                    t.TenTroChoi = et.TenTroChoi;
                    t.LoaiTroChoi = et.LoaiTroChoi;
                    t.SucChua = et.SucChua;
                    t.TuoiToiThieu = et.TuoiToiThieu;
                    t.ChieuCaoToiThieu = et.ChieuCaoToiThieu;
                    t.ThoiGianLuot = et.ThoiGianLuot;
                    t.MoTa = et.MoTa;
                    t.TrangThai = et.TrangThai;
                    t.NgayTao = et.NgayTao;
                    t.NgayCapNhat = et.NgayCapNhat;
                    db.SubmitChanges();
                    return true;
                }
                return false;


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
<<<<<<< Updated upstream
                var xoa = from tc in db.TroChois
                         where tc.MaCode == maCode
                         select tc;

                foreach(var item in xoa)
=======
                using (QLKVCGTDataContext db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))
>>>>>>> Stashed changes
                {
                    db.TroChois.DeleteOnSubmit(item);
                    db.SubmitChanges();
                }
                return true;
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
<<<<<<< Updated upstream
=======
        public int LayMaTroChoiLonNhat()
        {
            using (QLKVCGTDataContext db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))
            {
                var nextId = db.ExecuteQuery<decimal>(
                    "SELECT IDENT_CURRENT('TroChoi') + IDENT_INCR('TroChoi')"
                ).First();

                return Convert.ToInt32(nextId);
            }
        }
>>>>>>> Stashed changes
    }
}
