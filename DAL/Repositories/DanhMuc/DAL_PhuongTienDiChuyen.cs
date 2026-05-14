using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.DTOs;
using ET.Models.DanhMuc;

namespace DAL.Repositories.DanhMuc
{
    public class DAL_PhuongTienDiChuyen
    {
        public static DAL_PhuongTienDiChuyen Instance { get; } = new DAL_PhuongTienDiChuyen();

        #region Khởi tạo
        private DAL_PhuongTienDiChuyen() { }
        #endregion

        #region Truy vấn dữ liệu

        public List<ET_PhuongTienDiChuyen> LayDanhSach(string loaiXe = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = from xe in db.PhuongTienDiChuyens
                            join ts in db.TaiSanChoThues on xe.IdTaiSan equals ts.Id
                            where !ts.DaXoa
                            select new ET_PhuongTienDiChuyen
                            {
                                BienSo = xe.BienSo,
                                SoGhe = xe.SoGhe,
                                LoaiXe = xe.LoaiXe,
                                Id = ts.Id,
                                MaVachThietBi = ts.MaVachThietBi,
                                TenTaiSan = ts.TenTaiSan,
                                TrangThai = ts.TrangThai,
                                IdSanPham = ts.IdSanPham
                            };

                if (!string.IsNullOrEmpty(loaiXe))
                {
                    query = query.Where(x => x.LoaiXe == loaiXe);
                }

                return query.ToList();
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá
        public void CapNhatThongTin(int idTaiSan, string bienSo, int soGhe, string loaiXe)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var xe = db.PhuongTienDiChuyens.FirstOrDefault(x => x.IdTaiSan == idTaiSan);
                if (xe != null)
                {
                    xe.BienSo = bienSo;
                    xe.SoGhe = soGhe;
                    xe.LoaiXe = loaiXe;
                    db.SubmitChanges();
                }
            }
        }
        #endregion
    }
}


