using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public class DAL_QuyTacDiem
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_QuyTacDiem instance;
        public static DAL_QuyTacDiem Instance
        {
            get
            {
                if (instance == null) instance = new DAL_QuyTacDiem();
                return instance;
            }
        }

        public List<ET_QuyTacDiem> LoadDS()
        {
            return db.GetTable<QuyTacDiem>().Select(s => new ET_QuyTacDiem
            {
                Id = s.Id,
                TenQuyTac = s.TenQuyTac,
                TongDonToiThieu = s.TongDonToiThieu,
                SoDiemThuong = s.SoDiemThuong,
                LoaiKhachApDung = s.LoaiKhachApDung,
                TrangThai = s.TrangThai
            }).ToList();
        }

        public List<ET_QuyTacDiem> LayQuyTacActive()
        {
            return db.GetTable<QuyTacDiem>()
                .Where(x => x.TrangThai)
                .Select(s => new ET_QuyTacDiem
                {
                    Id = s.Id,
                    TenQuyTac = s.TenQuyTac,
                    TongDonToiThieu = s.TongDonToiThieu,
                    SoDiemThuong = s.SoDiemThuong,
                    LoaiKhachApDung = s.LoaiKhachApDung,
                    TrangThai = s.TrangThai
                }).ToList();
        }
    }
}
