using System;
using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_LichLamViec
    {
        private static BUS_LichLamViec instance;
        public static BUS_LichLamViec Instance
        {
            get
            {
                if (instance == null) instance = new BUS_LichLamViec();
                return instance;
            }
        }

        public List<ET_LichLamViec> LoadTheoTuan(DateTime monday, string caLam)
        {
            return DAL_LichLamViec.Instance.LoadTheoTuan(monday, caLam);
        }

        public List<ET_LichLamViec> LoadTheoO(int idKhuVuc, DateTime ngay, string caLam)
        {
            return DAL_LichLamViec.Instance.LoadTheoO(idKhuVuc, ngay, caLam);
        }

        public bool ThemNVVaoCa(int idNhanVien, int idKhuVuc, DateTime ngayLam, string caLam)
        {
            return DAL_LichLamViec.Instance.Them(idNhanVien, idKhuVuc, ngayLam, caLam);
        }

        public bool GoNVKhoiCa(int id)
        {
            return DAL_LichLamViec.Instance.Xoa(id);
        }

        public int CopyTuan(DateTime mondayNguon, DateTime mondayDich, string caLam)
        {
            return DAL_LichLamViec.Instance.CopyTuan(mondayNguon, mondayDich, caLam);
        }

        public List<int> LayDsIdNVDaPhanTrongNgay(DateTime ngay, string caLam)
        {
            return DAL_LichLamViec.Instance.LayDsIdNVDaPhanTrongNgay(ngay, caLam);
        }

        /// <summary>
        /// Lấy thứ 2 của tuần chứa ngày cho trước
        /// </summary>
        public static DateTime LayThu2CuaTuan(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-diff).Date;
        }
    }
}
