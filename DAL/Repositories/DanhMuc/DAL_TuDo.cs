using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.DTOs;
using ET.Models.DanhMuc;

namespace DAL.Repositories.DanhMuc
{
    public class DAL_TuDo
    {
        public static DAL_TuDo Instance { get; } = new DAL_TuDo();

        #region Khởi tạo
        private DAL_TuDo() { }
        #endregion

        #region Truy vấn dữ liệu

        // Lấy danh sách Tủ đồ có thông tin gốc từ TaiSanChoThue
        public List<ET_TuDo> LayDanhSach(int? idKhuVuc = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = from tu in db.TuDos
                            join ts in db.TaiSanChoThues on tu.IdTaiSan equals ts.Id
                            join kv in db.KhuVucs on tu.IdKhuVuc equals kv.Id into kvGroup
                            from kv in kvGroup.DefaultIfEmpty()
                            where !ts.DaXoa
                            select new ET_TuDo
                            {
                                IdKhuVuc = tu.IdKhuVuc,
                                TenKhuVuc = kv != null ? kv.TenKhuVuc : "",
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

        public void CapNhatKhuVuc(int idTaiSan, int idKhuVuc)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var tu = db.TuDos.FirstOrDefault(x => x.IdTaiSan == idTaiSan);
                if (tu != null)
                {
                    tu.IdKhuVuc = idKhuVuc;
                    db.SubmitChanges();
                }
            }
        }

        #endregion
    }
}


