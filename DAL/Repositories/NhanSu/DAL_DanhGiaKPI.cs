using System;
using System.Collections.Generic;
using System.Linq;
using ET.Models.NhanSu;
using ET.Results;
using DAL;

namespace DAL.Repositories.NhanSu
{
    public class DAL_DanhGiaKPI
    {
        public static DAL_DanhGiaKPI Instance { get; } = new DAL_DanhGiaKPI();

        private DAL_DanhGiaKPI() { }

        public OperationResult<List<DTO_DanhGiaKPI>> LayDanhSachTheoNhanVien(int idNhanVien)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    var lst = (from dg in db.DanhGiaNhanViens
                               join tt in db.ThongTins on dg.IdNguoiDanhGia equals tt.Id into nguoiDGGroup
                               from tt in nguoiDGGroup.DefaultIfEmpty()
                               where dg.IdNhanVien == idNhanVien
                               orderby dg.KyDanhGia descending
                               select new DTO_DanhGiaKPI
                               {
                                   Id = dg.Id,
                                   IdNhanVien = dg.IdNhanVien,
                                   KyDanhGia = dg.KyDanhGia,
                                   LoaiDanhGia = dg.LoaiDanhGia,
                                   DiemChuyen_Mon = dg.DiemChuyen_Mon,
                                   DiemThai_Do = dg.DiemThai_Do,
                                   DiemKet_Qua = dg.DiemKet_Qua,
                                   DiemTong = dg.DiemTong,
                                   XepLoai = dg.XepLoai,
                                   NhanXetNguoiDG = dg.NhanXetNguoiDG,
                                   NhanXetNhanVien = dg.NhanXetNhanVien,
                                   IdNguoiDanhGia = dg.IdNguoiDanhGia,
                                   TenNguoiDanhGia = tt != null ? tt.HoTen : null,
                                   TrangThai = dg.TrangThai,
                                   NgayDanhGia = dg.NgayDanhGia,
                                   NgayTao = dg.NgayTao
                               }).ToList();

                    return OperationResult<List<DTO_DanhGiaKPI>>.Ok(lst);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<List<DTO_DanhGiaKPI>>.Fail(ex.Message);
            }
        }
    }
}


