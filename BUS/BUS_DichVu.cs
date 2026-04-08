using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BUS
{
    public class BUS_DichVu
    {
        private static BUS_DichVu instance;
        public static BUS_DichVu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUS_DichVu();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public List<ET_DichVu> loadDS()
        {
            return DAL_DichVu.Instance.loadDS();
        }

        public string layMaCodeTiepTheo()
        {
            return DAL_DichVu.Instance.LayMaCodeTiepTheo();
        }
        public bool themDichVu(ET_DichVu et)
        {
            return DAL_DichVu.Instance.ThemDichVu(et);
        }
        public bool suaDichVu(ET_DichVu et)
        {
            return DAL_DichVu.Instance.SuaDichVu(et);
        }
        public bool Xoa(string maCode)
        {
            return DAL_DichVu.Instance.xoaDV(maCode);
        }
        public int layMaDichVuLonNhat()
        {
            return DAL_DichVu.Instance.LayMaDichVuLonNhat();
        }
        public string ValidateDichVu(ET_DichVu et, bool laThem = true)
        {
            if (string.IsNullOrWhiteSpace(et.TenDichVu))
                return "Vui lòng nhập tên dịch vụ";

            if (et.TenDichVu.Trim().Length > 100)
                return "Tên dịch vụ không được vượt quá 100 ký tự";

            // Không cho khoảng trắng đầu
            if (et.TenDichVu.StartsWith(" "))
                return "Tên dịch vụ không được bắt đầu bằng khoảng trắng";

            // Không cho 2 khoảng trắng liên tiếp
            if (et.TenDichVu.Contains("  "))
                return "Tên dịch vụ không được chứa 2 khoảng trắng liên tiếp";

            // Không cho ký tự đặc biệt
            //if (!System.Text.RegularExpressions.Regex.IsMatch(et.TenDichVu, @"^[\p{L}0-9 ]+$"))
            //    return "Tên dịch vụ không được chứa ký tự đặc biệt";

            if (string.IsNullOrWhiteSpace(et.DonViTinh))
                return "Vui lòng nhập đơn vị tính";

            if (et.TenDichVu.Trim().Length > 50)
                return "Đơn vị không được vượt quá 50 ký tự";

            // Không cho khoảng trắng đầu
            if (et.TenDichVu.StartsWith(" "))
                return "Đơn vị tính không được bắt đầu bằng khoảng trắng";

            // Không cho 2 khoảng trắng liên tiếp
            if (et.TenDichVu.Contains("  "))
                return "Đơn vị tính không được chứa 2 khoảng trắng liên tiếp";

            //// Không cho ký tự đặc biệt
            //if (!System.Text.RegularExpressions.Regex.IsMatch(et.TenDichVu, @"^[\p{L}0-9 ]+$"))
            //    return "Tên dịch vụ không được chứa ký tự đặc biệt";


            if (string.IsNullOrWhiteSpace(et.TrangThai))
                return "Vui lòng chọn trạng thái";

            //if (et.SoLuongTon < 0)
            //    return "Số lượng tồn không được âm";

            //if (et.GiaBan < 0)
            //    return "Giá bán không được âm";
           
            // Kiểm tra trùng tên
            if (DAL_DichVu.Instance.KiemTraTrungTen(et.TenDichVu.Trim(), laThem ? null : et.MaCode))
                return "Tên dịch vụ đã tồn tại";
            return "";
        }
        public List<ET_DichVu> TimKiem(string tuKhoa)
        {
            return DAL_DichVu.Instance.TimKiem(tuKhoa);
        }
    }
}
