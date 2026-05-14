using System;
using DAL.Repositories.HeThong;
using ET.Constants;

namespace BUS.Services.BanHang
{
    /// Nghiệp vụ chiết khấu theo hạng thành viên.
    /// Đọc % từ HeThong.CauHinh (key CK_HANG_*).
    public class BUS_ChietKhau
    {
        public static BUS_ChietKhau Instance { get; } = new BUS_ChietKhau();

        /// <summary>
        /// Tính chiết khấu theo hạng. Chỉ tính trên tongTienDuocGiam (không F&B).
        /// </summary>
        /// <returns>Số tiền giảm, 0 nếu không có hạng</returns>
        public decimal TinhChietKhau(string hangThanhVien, decimal tongTienDuocGiam, out decimal phanTram)
        {
            phanTram = 0;
            if (string.IsNullOrEmpty(hangThanhVien) || tongTienDuocGiam <= 0)
                return 0;

            string key = AppConstants.ConfigKeys.CK_HANG_PREFIX + hangThanhVien;
            phanTram = DAL_CauHinh.Instance.LayGiaTriDecimal(key, 0);

            if (phanTram <= 0) return 0;

            decimal giam = Math.Floor(tongTienDuocGiam * phanTram / 100);
            
            // Loại bỏ các khoản chiết khấu rác < 10đ
            if (giam < 10) giam = 0;

            return giam;
        }
    }
}


