using System;
using System.Collections.Generic;
using System.Linq;
using ET.Models.NhanSu;
using ET.Results;
using DAL;

namespace DAL.Repositories.NhanSu
{
    public class DAL_HopDong
    {
        private static DAL_HopDong _instance = new DAL_HopDong();
        public static DAL_HopDong Instance => _instance;

        private DAL_HopDong() { }

        public OperationResult<List<DTO_HopDong>> LayDanhSachTheoNhanVien(int idNhanVien)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    var lst = (from hd in db.HopDongs
                               where hd.IdNhanVien == idNhanVien
                               orderby hd.NgayHieuLuc descending
                               select new DTO_HopDong
                               {
                                   Id = hd.Id,
                                   IdNhanVien = hd.IdNhanVien,
                                   SoHopDong = hd.SoHopDong,
                                   LoaiHopDong = hd.LoaiHopDong,
                                   ChucDanh = hd.ChucDanh,
                                   LuongCoBan = hd.LuongCoBan,
                                   NgayHieuLuc = hd.NgayHieuLuc,
                                   NgayHetHan = hd.NgayHetHan,
                                   LyDoKyMoi = hd.LyDoKyMoi,
                                   TepHopDong = hd.TepHopDong,
                                   IdNguoiKy = hd.IdNguoiKy,
                                   TrangThai = hd.TrangThai,
                                   GhiChu = hd.GhiChu,
                                   NgayTao = hd.NgayTao
                               }).ToList();

                    return OperationResult<List<DTO_HopDong>>.Ok(lst);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<List<DTO_HopDong>>.Fail(ex.Message);
            }
        }
    }
}
