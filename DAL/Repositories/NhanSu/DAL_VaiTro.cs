using System.Collections.Generic;
using System.Linq;
using ET.Models.NhanSu;

namespace DAL.Repositories.NhanSu
{
    public class DAL_VaiTro
    {
        #region Khởi tạo (Singleton)
        
        public static DAL_VaiTro Instance { get; } = new DAL_VaiTro();

        #endregion

        #region Truy vấn dữ liệu


        public List<ET_VaiTro> LoadDS()
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.VaiTros
                    .Where(x => x.ConHoatDong == true)
                    .Select(x => new ET_VaiTro
                    {
                        Id = x.Id,
                        TenVaiTro = x.TenVaiTro,
                        MoTa = x.MoTa,
                        ConHoatDong = x.ConHoatDong
                    }).ToList();
            }
        }

        #endregion
    }
}


