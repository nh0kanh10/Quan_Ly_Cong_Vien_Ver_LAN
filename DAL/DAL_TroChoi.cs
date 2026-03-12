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

        public int LayMaTroChoiLonNhat()
        {
            return db.TroChois.Max(x => (int?)x.MaTroChoi) ?? 0;
        }

        public string LayMaCodeTiepTheo()
        {
            string maxMaDatPhong = db.TroChois
            .Select(t => t.MaCode)
            .ToList()
            // Sắp xếp giảm dần và lấy bản ghi đầu tiên
            .OrderByDescending(t => int.TryParse(t.Substring(2), out int num) ? num : 0)
            .FirstOrDefault(); // Trả về mã lớn nhất, hoặc null nếu không có bản ghi nào

            // Xử lý trường hợp bảng rỗng
            if (string.IsNullOrEmpty(maxMaDatPhong))
            {
                maxMaDatPhong = "TC000";
            }

            // --- Logic tạo mã tiếp theo (Tương tự như trước) ---
            int maxNumber = 0;
            if (maxMaDatPhong.Length >= 4)
            {
                string numberPart = maxMaDatPhong.Substring(2);
                int.TryParse(numberPart, out maxNumber);
            }

            int nextNumber = maxNumber + 1;
            string nextMaDatPhong = "TC" + nextNumber.ToString().PadLeft(3, '0');

            return nextMaDatPhong;
        }
        private TroChoi layDuLieu(ET_TroChoi et)
        {
            return new TroChoi
            {
                
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
        }
        public bool ThemTroChoi(ET_TroChoi et)
        {
            try
            {                
                int nextMaTC = LayMaTroChoiLonNhat();
                TroChoi tc = layDuLieu(et);
                tc.MaCode = LayMaCodeTiepTheo();

                db.TroChois.InsertOnSubmit(tc);
                db.SubmitChanges();
                return true;
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
                TroChoi t = db.TroChois.SingleOrDefault(x => x.MaCode == et.MaCode);
                if(t != null)
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
                var xoa = from tc in db.TroChois
                         where tc.MaCode == maCode
                         select tc;

                foreach(var item in xoa)
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

        public string layMaKVTheoLoaiTC(string loai)
        {
            var kv = db.KhuVucs.FirstOrDefault(x => x.TenKhuVuc.Contains(loai));
            if(kv != null)
            {
                return kv.MaKhuVuc.ToString();
            }
            return "";

        }
    }
}
