using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories.DoiTac
{
    public class DAL_DoiTac
    {
        public static DAL_DoiTac Instance { get; } = new DAL_DoiTac();

        public List<ET.Models.DoiTac.ThongTin> GetAllDoiTac()
        {
            using (var db = new DaiNamDBDataContext())
            {
                if (!db.DatabaseExists()) return new List<ET.Models.DoiTac.ThongTin>();

                var query = db.ThongTins.Where(t => t.DaXoa == false);

                return query.OrderBy(t => t.HoTen).Select(t => new ET.Models.DoiTac.ThongTin {
                    Id = t.Id,
                    HoTen = t.HoTen,
                    DienThoai = t.DienThoai,
                    LoaiDoiTac = t.LoaiDoiTac,
                    DiaChi = t.DiaChi,
                    DaXoa = t.DaXoa
                }).ToList();
            }
        }
    }
}


