using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories.NhanSu
{
    public class DAL_PhanQuyen
    {
        #region Khởi tạo (Singleton)
        
        public static DAL_PhanQuyen Instance { get; } = new DAL_PhanQuyen();

        #endregion

        #region Truy vấn dữ liệu

 
        public List<int> LayQuyenTheoVaiTro(int idVaiTro)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.PhanQuyens
                    .Where(x => x.IdVaiTro == idVaiTro)
                    .Select(x => x.IdQuyenHan)
                    .ToList();
            }
        }

        #endregion

        #region Thêm / Sửa / Xóa

        public void CapNhatQuyen(int idVaiTro, List<int> dsIdQuyen)
        {
            using (var db = new DaiNamDBDataContext())
            {
                // 1. Xóa tất cả quyền cũ của vai trò này
                var quyenCu = db.PhanQuyens.Where(x => x.IdVaiTro == idVaiTro);
                db.PhanQuyens.DeleteAllOnSubmit(quyenCu);

                // 2. Thêm quyền mới
                if (dsIdQuyen != null && dsIdQuyen.Count > 0)
                {
                    var quyenMoi = dsIdQuyen.Select(idQ => new PhanQuyen
                    {
                        IdVaiTro = idVaiTro,
                        IdQuyenHan = idQ
                    });
                    db.PhanQuyens.InsertAllOnSubmit(quyenMoi);
                }

                db.SubmitChanges();
            }
        }

        #endregion
    }
}


