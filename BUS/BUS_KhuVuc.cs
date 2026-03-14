using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class BUS_KhuVuc
    {
        private static BUS_KhuVuc instance;
        public static BUS_KhuVuc Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUS_KhuVuc();
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
            return DAL_KhuVuc.Instance.LoadDSKhuVucHoatDong();
        }

        /// <summary>
        /// Load tất cả khu vực — dùng cho ComboBox lọc
        /// </summary>
        public List<KhuVuc> LoadDSKhuVuc()
        {
            return DAL_KhuVuc.Instance.LoadDSKhuVuc();
        }

        public List<KhuVuc> TimKiem(string tuKhoa)
        {
            return DAL_KhuVuc.Instance.TimKiem(tuKhoa);
        }

        public string LayMaCodeTiepTheo()
        {
            return DAL_KhuVuc.Instance.LayMaCodeTiepTheo();
        }

        public string ValidateKhuVuc(ET.ET_KhuVuc et, bool laThem = true)
        {
            // Tên không được để trống
            if (string.IsNullOrWhiteSpace(et.TenKhuVuc))
                return "Tên khu vực không được để trống!";

            // Không cho khoảng trắng đầu
            if (et.TenKhuVuc.StartsWith(" "))
                return "Tên khu vực không được bắt đầu bằng khoảng trắng";

            // Không cho 2 khoảng trắng liên tiếp
            if (et.TenKhuVuc.Contains("  "))
                return "Tên khu vực không được chứa 2 khoảng trắng liên tiếp";

            // Không cho ký tự đặc biệt
            if (!System.Text.RegularExpressions.Regex.IsMatch(et.TenKhuVuc, @"^[\p{L}0-9 ]+$"))
                return "Tên khu vực không được chứa ký tự đặc biệt";

            // Tên > 150 ký tự
            if (et.TenKhuVuc.Trim().Length > 150)
                return "Tên khu vực không được vượt quá 150 ký tự";

            // Tên < 3 ký tự
            if (et.TenKhuVuc.Trim().Length < 3)
                return "Tên khu vực có ít nhất 3 ký tự";
            
            //Tên đã tồn tại
            if (DAL_KhuVuc.Instance.KiemTraTrungTen(et.TenKhuVuc, laThem ? null : et.MaCode))
                return "Tên khu vực đã tồn tại!";

            //Trạng thái null
            if (string.IsNullOrWhiteSpace(et.TrangThai))
                return "Vui lòng chọn trạng thái";

            //Mô tả > 500 ký tự
            if (!string.IsNullOrEmpty(et.MoTa) && et.MoTa.Length > 500)
                return "Mô tả không được vượt quá 500 ký tự";

            return string.Empty;
        }

        public bool ThemKhuVuc(ET.ET_KhuVuc et)
        {
            return DAL_KhuVuc.Instance.ThemKhuVuc(et);
        }

        public bool SuaKhuVuc(ET.ET_KhuVuc et)
        {
            return DAL_KhuVuc.Instance.SuaKhuVuc(et);
        }

        public bool XoaKhuVuc(string maCode)
        {
            return DAL_KhuVuc.Instance.XoaKhuVuc(maCode);
        }
    }
}
