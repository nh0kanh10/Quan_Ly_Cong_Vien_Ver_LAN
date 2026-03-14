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
            if (string.IsNullOrWhiteSpace(et.TenKhuVuc))
                return "Tên khu vực không được để trống!";

            if (DAL_KhuVuc.Instance.KiemTraTrungTen(et.TenKhuVuc, laThem ? null : et.MaCode))
                return "Tên khu vực đã tồn tại!";

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
