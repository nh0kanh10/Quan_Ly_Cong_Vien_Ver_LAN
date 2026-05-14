using System.Collections.Generic;
using System.Linq;
using ET.Models.NhanSu;

namespace DAL.Repositories.NhanSu
{
    public class DAL_QuyenHan
    {
        #region Khởi tạo (Singleton)
        
        public static DAL_QuyenHan Instance { get; } = new DAL_QuyenHan();

        #endregion

        #region Truy vấn dữ liệu

        public List<ET_QuyenHan> LoadDS()
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.QuyenHans.Select(x => new ET_QuyenHan
                {
                    Id = x.Id,
                    MaQuyen = x.MaQuyen,
                    TenQuyen = x.TenQuyen,
                    NhomQuyen = x.NhomQuyen
                }).ToList();
            }
        }

        #endregion
    }
}


