using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ET;
namespace BUS
{
    public class BUS_TroChoi
    {
        private static BUS_TroChoi instance;
        public static BUS_TroChoi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUS_TroChoi();
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
            return DAL_TroChoi.Instance.loadDS();
        }

        public List<ET_TroChoi> loadDSTheoKhuVuc(int maKhuVuc)
        {
            return DAL_TroChoi.Instance.loadDS()
                .Where(tc => tc.MaKhuVuc == maKhuVuc).ToList();
        }

        public List<ET_TroChoi> timKiem(string tuKhoa)
        {
            return DAL_TroChoi.Instance.TimKiem(tuKhoa);
        }

        public List<ET_TroChoi> timKiemTheoKhuVuc(string tuKhoa, int maKhuVuc)
        {
            return DAL_TroChoi.Instance.TimKiem(tuKhoa)
                .Where(tc => tc.MaKhuVuc == maKhuVuc).ToList();
        }

        public string layMaCodeTiepTheo()
        {
            return DAL_TroChoi.Instance.LayMaCodeTiepTheo();
        }

        /// <summary>
        /// Validate dữ liệu trò chơi trước khi thêm/sửa.
        /// Return: chuỗi rỗng = hợp lệ, ngược lại = thông báo lỗi.
        /// </summary>
        public string ValidateTroChoi(ET_TroChoi et, bool laThem = true)
        {
            // Rule 1: Tên trò chơi bỏ trống
            if (string.IsNullOrWhiteSpace(et.TenTroChoi))
                return "Vui lòng nhập tên trò chơi";

            // Không cho khoảng trắng đầu
            if (et.TenTroChoi.StartsWith(" "))
                return "Tên trò chơi không được bắt đầu bằng khoảng trắng";

            // Không cho 2 khoảng trắng liên tiếp
            if (et.TenTroChoi.Contains("  "))
                return "Tên trò chơi không được chứa 2 khoảng trắng liên tiếp";

            // Không cho ký tự đặc biệt
            if (!System.Text.RegularExpressions.Regex.IsMatch(et.TenTroChoi, @"^[\p{L}0-9 ]+$"))
                return "Tên trò chơi không được chứa ký tự đặc biệt";

            // Rule 6: Tên > 150 ký tự
            if (et.TenTroChoi.Trim().Length > 150)
                return "Tên trò chơi không được vượt quá 150 ký tự";

            // Tên < 3 ký tự
            if (et.TenTroChoi.Trim().Length < 3)
                return "Tên trò chơi có ít nhất 3 ký tự";



            //Tuổi dựa theo loại trò chơi
            int tuoiMin = 0;

            switch (et.LoaiTroChoi)
            {
                case "Trẻ em":
                    tuoiMin = 3;
                    break;

                case "Gia đình":
                    tuoiMin = 5;
                    break;

                case "Phiêu lưu":
                    tuoiMin = 10;
                    break;

                case "Cảm giác mạnh":
                    tuoiMin = 16;
                    break;

                case "Thể thao":
                    tuoiMin = 12;
                    break;

                case "Nước":
                    tuoiMin = 6;
                    break;

                case "Tham quan":
                    tuoiMin = 6;
                    break;

                case "Trong nhà":
                    tuoiMin = 10;
                    break;
            }

            if (et.TuoiToiThieu < tuoiMin)
            {
                return $"Loại trò chơi {et.LoaiTroChoi} yêu cầu tuổi tối thiểu {tuoiMin}";
            }


            // Rule 2: Chưa chọn khu vực
            if (et.MaKhuVuc <= 0)
                return "Vui lòng chọn khu vực";


            // Rule 3: Chưa chọn loại trò chơi
            if (string.IsNullOrWhiteSpace(et.LoaiTroChoi))
                return "Vui lòng chọn loại trò chơi";


            // Rule 4: Sức chứa <= 0
            if (et.SucChua <= 0)
                return "Sức chứa phải lớn hơn 0";

           
            // Rule 9: Chiều cao < 0
            if (et.ChieuCaoToiThieu <= 0)
                return "Chiều cao tối thiểu không được âm";

            // Rule: Thời gian lượt <= 0
            if (et.ThoiGianLuot <= 0)
                return "Thời gian lượt phải lớn hơn 0";

            // Rule 7: Mô tả > 500 ký tự
            if (!string.IsNullOrEmpty(et.MoTa) && et.MoTa.Length > 500)
                return "Mô tả không được vượt quá 500 ký tự";

            // Rule 5: Tên trùng trong cùng khu vực
            if (DAL_TroChoi.Instance.KiemTraTrungTen(et.TenTroChoi.Trim(), et.MaKhuVuc, laThem ? null : et.MaCode))
                return "Tên trò chơi đã tồn tại trong khu vực này";

            

            return "";
        }

        public bool themTroChoi(ET_TroChoi et)
        {
            et.TenTroChoi = et.TenTroChoi.Trim();
            return DAL_TroChoi.Instance.ThemTroChoi(et);
        }

        public bool xoaTroChoi(string maCode)
        {
            return DAL_TroChoi.Instance.xoaTC(maCode);
        }
        public bool capNhatTroChoi(ET_TroChoi et)
        {
            et.TenTroChoi = et.TenTroChoi.Trim();
            return DAL_TroChoi.Instance.SuaTroChoi(et);
        }
        public int layMaTroChoiTiepTheo()
        {
            return DAL_TroChoi.Instance.LayMaTroChoiLonNhat();
        }
    }
}
