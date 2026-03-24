using System;
using System.Collections.Generic;
using ET;

namespace BUS
{
    public class BUS_LichLamViec
    {
        private readonly ILichLamViecGateway _gateway;

        private static BUS_LichLamViec instance;
        public static BUS_LichLamViec Instance
        {
            get
            {
                if (instance == null) instance = new BUS_LichLamViec();
                return instance;
            }
        }

        public BUS_LichLamViec() : this(new DefaultLichLamViecGateway()) { }
        public BUS_LichLamViec(ILichLamViecGateway gw) { _gateway = gw; }

        public List<ET_LichLamViec> LoadTheoTuan(DateTime monday, string caLam) => _gateway.LoadTheoTuan(monday, caLam);

        public List<ET_LichLamViec> LoadTheoO(int idKhuVuc, DateTime ngay, string caLam) => _gateway.LoadTheoO(idKhuVuc, ngay, caLam);

        public bool ThemNVVaoCa(int idNhanVien, int idKhuVuc, DateTime ngayLam, string caLam) => _gateway.Them(idNhanVien, idKhuVuc, ngayLam, caLam);

        public bool GoNVKhoiCa(int id) => _gateway.Xoa(id);

        public int CopyTuan(DateTime mondayNguon, DateTime mondayDich, string caLam) => _gateway.CopyTuan(mondayNguon, mondayDich, caLam);

        public List<int> LayDsIdNVDaPhanTrongNgay(DateTime ngay, string caLam) => _gateway.LayDsIdNVDaPhanTrongNgay(ngay, caLam);

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
