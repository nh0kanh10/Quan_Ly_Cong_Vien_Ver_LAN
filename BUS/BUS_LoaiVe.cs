using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using ET;

namespace BUS
{
    public class BUS_LoaiVe
    {
        private static BUS_LoaiVe instance;
        public static BUS_LoaiVe Instance
        {
            get
            {
                if (instance == null) instance = new BUS_LoaiVe();
                return instance;
            }
        }

        public List<ET_LoaiVe> LoadDS()
        {
            return DAL_LoaiVe.Instance.LoadDS();
        }

        public List<ET_LoaiVe> TimKiem(string tuKhoa)
        {
            return DAL_LoaiVe.Instance.TimKiem(tuKhoa);
        }

        public string LayMaCodeTiepTheo()
        {
            return DAL_LoaiVe.Instance.LayMaCodeTiepTheo();
        }

        /// <summary>
        /// Validate dữ liệu loại vé. Return rỗng = hợp lệ, ngược lại = lỗi.
        /// </summary>
        public string ValidateLoaiVe(ET_LoaiVe et, bool laThem = true)
        {
            if (string.IsNullOrWhiteSpace(et.TenLoaiVe))
                return "Vui lòng nhập tên loại vé";

            if (et.TenLoaiVe.Trim().Length > 100)
                return "Tên loại vé không được vượt quá 100 ký tự";

            // Không cho khoảng trắng đầu
            if (et.TenLoaiVe.StartsWith(" "))
                return "Tên loại vé không được bắt đầu bằng khoảng trắng";

            // Không cho 2 khoảng trắng liên tiếp
            if (et.TenLoaiVe.Contains("  "))
                return "Tên loại vé không được chứa 2 khoảng trắng liên tiếp";

            // Không cho ký tự đặc biệt
            if (!System.Text.RegularExpressions.Regex.IsMatch(et.TenLoaiVe, @"^[\p{L}0-9 ]+$"))
                return "Tên loại vé không được chứa ký tự đặc biệt";


            if (string.IsNullOrWhiteSpace(et.DoiTuong))
                return "Vui lòng chọn đối tượng";

            if (string.IsNullOrWhiteSpace(et.TrangThai))
                return "Vui lòng chọn trạng thái";

            if (et.GiaVe < 0)
                return "Giá vé không được âm";

            if (et.GiaCuoiTuan.HasValue && et.GiaCuoiTuan.Value < 0)
                return "Giá cuối tuần không được âm";

            // Kiểm tra trùng tên
            if (DAL_LoaiVe.Instance.KiemTraTrungTen(et.TenLoaiVe.Trim(), laThem ? null : et.MaCode))
                return "Tên loại vé đã tồn tại";

            return "";
        }

        public bool ThemLoaiVe(ET_LoaiVe et)
        {
            et.TenLoaiVe = et.TenLoaiVe.Trim();
            return DAL_LoaiVe.Instance.ThemLoaiVe(et);
        }

        public bool SuaLoaiVe(ET_LoaiVe et)
        {
            et.TenLoaiVe = et.TenLoaiVe.Trim();
            return DAL_LoaiVe.Instance.SuaLoaiVe(et);
        }

        public bool XoaLoaiVe(string maCode)
        {
            return DAL_LoaiVe.Instance.XoaLoaiVe(maCode);
        }
    }
}
