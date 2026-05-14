using System;
using System.Collections.Generic;
using System.Linq;
using ET.Models.NhanSu;
using ET.Results;
using DAL;

namespace DAL.Repositories.NhanSu
{
    public class DAL_LichLamViec
    {
        private static DAL_LichLamViec _instance = new DAL_LichLamViec();
        public static DAL_LichLamViec Instance => _instance;

        private DAL_LichLamViec() { }

        public OperationResult<List<DTO_LichLamViecChiTiet>> LayDanhSachTheoNhanVien(int idNhanVien)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    var lst = (from llv in db.LichLamViecs
                               join ca in db.CaLamMaus on llv.IdCaLamMau equals ca.Id into caGroup
                               from ca in caGroup.DefaultIfEmpty()
                               where llv.IdNhanVien == idNhanVien
                               orderby llv.NgayLam descending, llv.GioBatDau ascending
                               select new DTO_LichLamViecChiTiet
                               {
                                   Id = llv.Id,
                                   IdNhanVien = llv.IdNhanVien,
                                   NgayLam = llv.NgayLam,
                                   IdCaLamMau = llv.IdCaLamMau,
                                   TenCa = ca != null ? ca.TenCa : null,
                                   GioBatDau = llv.GioBatDau,
                                   GioKetThuc = llv.GioKetThuc,
                                   TrangThai = llv.TrangThai,
                                   GhiChu = llv.GhiChu
                               }).ToList();

                    return OperationResult<List<DTO_LichLamViecChiTiet>>.Ok(lst);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<List<DTO_LichLamViecChiTiet>>.Fail(ex.Message);
            }
        }
    }
}
