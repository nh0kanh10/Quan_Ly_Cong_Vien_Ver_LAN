using System;
using System.Collections.Generic;
using System.Linq;
using ET.DTOs;

namespace DAL.Repositories.HeThong
{
    public class DAL_NhatKyThaoTac
    {
        #region Khởi tạo (Singleton)

        public static DAL_NhatKyThaoTac Instance { get; } = new DAL_NhatKyThaoTac();

        #endregion

        #region Thêm / Sửa / Xoá

        public void GhiLog(string thucThe, int idThucThe, string hanhDong,
            int idNguoiThucHien, string giaTriCu = null, string giaTriMoi = null,
            string ghiChu = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var log = new NhatKyThaoTac
                {
                    ThucThe = thucThe,
                    IdThucThe = idThucThe,
                    HanhDong = hanhDong,
                    GiaTriCu = giaTriCu,
                    GiaTriMoi = giaTriMoi,
                    IdNguoiThucHien = idNguoiThucHien,
                    ThoiGian = DateTime.Now,
                    GhiChu = ghiChu
                };
                db.NhatKyThaoTacs.InsertOnSubmit(log);
                db.SubmitChanges();
            }
        }

        #endregion

        #region Truy vấn dữ liệu
        // Truy vấn nhật ký theo khoảng thời gian, module và nhân viên.
        // Join bảng ThongTin để lấy tên người thực hiện.
        public List<DTO_NhatKyView> TraCuuLog(DateTime tuNgay, DateTime denNgay,
            string thucThe = "", int idNguoiThucHien = 0)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = from log in db.NhatKyThaoTacs
                            join tt in db.ThongTins on log.IdNguoiThucHien equals tt.Id into gj
                            from tt in gj.DefaultIfEmpty()
                            where log.ThoiGian >= tuNgay.Date
                               && log.ThoiGian < denNgay.Date.AddDays(1)
                            select new { log, TenNguoi = tt != null ? tt.HoTen : "Hệ thống" };

                if (!string.IsNullOrEmpty(thucThe))
                    query = query.Where(x => x.log.ThucThe == thucThe);

                if (idNguoiThucHien > 0)
                    query = query.Where(x => x.log.IdNguoiThucHien == idNguoiThucHien);

                return query.OrderByDescending(x => x.log.ThoiGian)
                    .Take(1000)
                    .Select(x => new DTO_NhatKyView
                    {
                        Id = x.log.Id,
                        ThoiGian = x.log.ThoiGian,
                        TenNguoiThucHien = x.TenNguoi,
                        ThucThe = x.log.ThucThe,
                        HanhDong = x.log.HanhDong,
                        IdThucThe = x.log.IdThucThe,
                        GiaTriCu = x.log.GiaTriCu,
                        GiaTriMoi = x.log.GiaTriMoi,
                        GhiChu = x.log.GhiChu
                    }).ToList();
            }
        }

        // Lấy danh sách tên module (ThucThe) đã từng ghi log, dùng cho ComboBox lọc.
        public List<string> LayDanhSachModule()
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.NhatKyThaoTacs
                    .Select(x => x.ThucThe)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();
            }
        }

        #endregion
    }
}


