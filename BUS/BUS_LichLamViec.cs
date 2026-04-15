using System;
using System.Collections.Generic;
using ET;
using DAL;

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

        public List<ET_LichLamViec> LoadTheoTuan(DateTime monday, int idCaLamMau) => _gateway.LoadTheoTuan(monday, idCaLamMau);

        public List<ET_LichLamViec> LoadTheoO(int idKhuVuc, DateTime ngay, int idCaLamMau) => _gateway.LoadTheoO(idKhuVuc, ngay, idCaLamMau);

        public bool ThemNVVaoCa(int idNhanVien, int idKhuVuc, DateTime ngayLam, int idCaLamMau) => _gateway.Them(idNhanVien, idKhuVuc, ngayLam, idCaLamMau);

        public bool GoNVKhoiCa(int id) => _gateway.Xoa(id);

        public int CopyTuan(DateTime mondayNguon, DateTime mondayDich, int idCaLamMau) => _gateway.CopyTuan(mondayNguon, mondayDich, idCaLamMau);

        public List<int> LayDsIdNVDaPhanTrongNgay(DateTime ngay, int idCaLamMau) => _gateway.LayDsIdNVDaPhanTrongNgay(ngay, idCaLamMau);

        public List<ET_CaLamMau> LoadCaLamMau() => _gateway.LoadCaLamMau();

        /// <summary>
        /// Lấy thứ 2 của tuần chứa ngày cho trước
        /// </summary>
        public static DateTime LayThu2CuaTuan(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-diff).Date;
        }

        public (int? IdKhuVuc, string TenKhuVuc) GetKhuVucHienTai(int idNhanVien)
        {
            return DAL_LichLamViec.Instance.GetKhuVucHienTai(idNhanVien);
        }
    }
}
