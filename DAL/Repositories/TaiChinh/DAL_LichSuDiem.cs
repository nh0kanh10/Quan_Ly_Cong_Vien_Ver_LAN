using System;
using System.Collections.Generic;
using System.Linq;
using ET.Models.TaiChinh;

namespace DAL.Repositories.TaiChinh
{
    public class DAL_LichSuDiem
    {
        #region Khởi tạo (Singleton)

        public static DAL_LichSuDiem Instance { get; } = new DAL_LichSuDiem();

        #endregion

        #region Truy vấn dữ liệu

        public int LaySoDiem(int idKhachHang)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.LichSuDiems
                    .Where(ld => ld.IdKhachHang == idKhachHang)
                    .OrderByDescending(ld => ld.NgayTao)
                    .Select(ld => ld.SoDuSauGD)
                    .FirstOrDefault();
            }
        }

        public List<ET_LichSuDiem> LayLichSu(int idKhachHang)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.LichSuDiems
                    .Where(ld => ld.IdKhachHang == idKhachHang)
                    .OrderByDescending(ld => ld.NgayTao)
                    .Select(ld => new ET_LichSuDiem
                    {
                        Id = ld.Id,
                        IdKhachHang = ld.IdKhachHang,
                        LoaiGiaoDich = ld.LoaiGiaoDich,
                        SoDiem = ld.SoDiem,
                        SoDuSauGD = ld.SoDuSauGD,
                        IdDonHang = ld.IdDonHang,
                        MoTa = ld.MoTa,
                        NgayTao = ld.NgayTao
                    }).ToList();
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá

        /// Thêm 1 dòng điểm mới.
        /// Trước khi gọi, BUS đã tính SoDuSauGD = soDuHienTai + soDiem (hoặc - soDiem).
        public void ThemGiaoDich(int idKhachHang, string loaiGd, int soDiem, int soDuSauGD, int? idDonHang, string moTa)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var record = new LichSuDiem
                {
                    IdKhachHang = idKhachHang,
                    LoaiGiaoDich = loaiGd,
                    SoDiem = soDiem,
                    SoDuSauGD = soDuSauGD,
                    IdDonHang = idDonHang,
                    MoTa = moTa,
                    NgayTao = DateTime.Now
                };
                db.LichSuDiems.InsertOnSubmit(record);
                db.SubmitChanges();
            }
        }

        #endregion
    }
}


