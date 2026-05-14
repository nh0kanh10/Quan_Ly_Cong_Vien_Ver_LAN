using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories.HeThong
{
    public class DAL_TuDien
    {
        public static DAL_TuDien Instance { get; } = new DAL_TuDien();

        public List<TuDien> LayDanhSachNhom(string nhomMa)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.TuDiens
                    .Where(t => t.NhomMa == nhomMa && t.ConHoatDong == true)
                    .OrderBy(t => t.ThuTu)
                    .ToList();
            }
        }
    }
}


