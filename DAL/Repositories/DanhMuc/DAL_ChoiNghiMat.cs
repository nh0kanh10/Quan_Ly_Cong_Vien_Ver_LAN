using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.DTOs;
using ET.Models.DanhMuc;

namespace DAL.Repositories.DanhMuc
{
    public class DAL_ChoiNghiMat
    {
        public static DAL_ChoiNghiMat Instance { get; } = new DAL_ChoiNghiMat();

        #region Khởi tạo
        private DAL_ChoiNghiMat() { }
        #endregion

        #region Truy vấn dữ liệu

        public List<ET_ChoiNghiMat> LayDanhSach(int? idKhuVuc = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = from choi in db.ChoiNghiMats
                            join ts in db.TaiSanChoThues on choi.IdTaiSan equals ts.Id
                            join kv in db.KhuVucs on choi.IdKhuVuc equals kv.Id into kvGroup
                            from kv in kvGroup.DefaultIfEmpty()
                            where !ts.DaXoa
                            select new ET_ChoiNghiMat
                            {
                                IdKhuVuc = choi.IdKhuVuc,
                                TenKhuVuc = kv != null ? kv.TenKhuVuc : "",
                                SucChua = choi.SucChua,
                                Id = ts.Id,
                                MaVachThietBi = ts.MaVachThietBi,
                                TenTaiSan = ts.TenTaiSan,
                                TrangThai = ts.TrangThai,
                                IdSanPham = ts.IdSanPham
                            };

                if (idKhuVuc.HasValue)
                {
                    query = query.Where(x => x.IdKhuVuc == idKhuVuc.Value);
                }

                return query.ToList();
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá
        // Dành cho form Cấu hình tài sản
        public void CapNhatThongTin(int idTaiSan, int idKhuVuc, int sucChua)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var choi = db.ChoiNghiMats.FirstOrDefault(x => x.IdTaiSan == idTaiSan);
                if (choi != null)
                {
                    choi.IdKhuVuc = idKhuVuc;
                    choi.SucChua = sucChua;
                    db.SubmitChanges();
                }
            }
        }
        #endregion
    }
}


