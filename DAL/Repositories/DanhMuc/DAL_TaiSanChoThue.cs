using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.DTOs;
using ET.Models.DanhMuc;

namespace DAL.Repositories.DanhMuc
{
    public class DAL_TaiSanChoThue
    {
        public static DAL_TaiSanChoThue Instance { get; } = new DAL_TaiSanChoThue();

        #region Truy vấn dữ liệu

        /// Tìm tài sản vật lý theo mã vạch barcode (VD: XE_001, XD_003).
        /// Dùng khi nhân viên quét barcode đồ lớn tại trạm cho thuê.
        public ET_TaiSanChoThue LayTheoBarcode(string maVach)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return (from ts in db.TaiSanChoThues
                        join sp in db.SanPhams on ts.IdSanPham equals sp.Id into spGroup
                        from sp in spGroup.DefaultIfEmpty()
                        where ts.MaVachThietBi == maVach && !ts.DaXoa
                        select new ET_TaiSanChoThue
                        {
                            Id = ts.Id,
                            TenTaiSan = ts.TenTaiSan,
                            IdSanPham = ts.IdSanPham,
                            MaVachThietBi = ts.MaVachThietBi,
                            TrangThai = ts.TrangThai,
                            DaXoa = ts.DaXoa,
                            TenSanPham = sp != null ? sp.TenSanPham : ""
                        }).FirstOrDefault();
            }
        }

        /// Lấy danh sách tài sản vật lý thuộc 1 sản phẩm.
        /// Dùng kiểm tra còn bao nhiêu cái sẵn sàng cho thuê.
        public List<ET_TaiSanChoThue> LayTheoSanPham(int idSanPham)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return (from ts in db.TaiSanChoThues
                        where ts.IdSanPham == idSanPham && !ts.DaXoa
                        select new ET_TaiSanChoThue
                        {
                            Id = ts.Id,
                            TenTaiSan = ts.TenTaiSan,
                            IdSanPham = ts.IdSanPham,
                            MaVachThietBi = ts.MaVachThietBi,
                            TrangThai = ts.TrangThai,
                            DaXoa = ts.DaXoa
                        }).ToList();
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá

        /// Đổi trạng thái tài sản (SanSang <-> DangThue / HuHong).
        /// Gọi trong transaction của BUS khi giao hoặc nhận trả đồ.
        public void CapNhatTrangThai(int id, string trangThaiMoi, DaiNamDBDataContext db = null)
        {
            bool ownsDb = db == null;
            if (ownsDb) db = new DaiNamDBDataContext();
            try
            {
                var ts = db.TaiSanChoThues.FirstOrDefault(x => x.Id == id);
                if (ts != null)
                {
                    ts.TrangThai = trangThaiMoi;
                    db.SubmitChanges();
                }
            }
            finally
            {
                if (ownsDb) db.Dispose();
            }
        }

        /// Đồng bộ danh sách Tài sản vật lý với UI (Thêm/Sửa/Xóa).
        /// Chạy chung Transaction của SanPham.
        public void SyncTaiSan(DaiNamDBDataContext db, int idSanPham, List<ET.Models.DanhMuc.ET_TaiSanChoThue> dsUI)
        {
            if (idSanPham <= 0 || dsUI == null) return;

            // Lấy danh sách cũ trong DB
            var dsDB = db.TaiSanChoThues.Where(x => x.IdSanPham == idSanPham && x.DaXoa == false).ToList();

            // 1. Xóa: Có trong DB nhưng không có trên UI (Bị Xóa)
            var idsUI = dsUI.Where(x => x.Id > 0).Select(x => x.Id).ToList();
            var dsDelete = dsDB.Where(x => !idsUI.Contains(x.Id)).ToList();

            foreach (var item in dsDelete)
            {
                // Chỉ cho phép xóa nếu tài sản rảnh rỗi
                if (item.TrangThai != ET.Constants.AppConstants.TrangThaiTaiSan.SanSang)
                {
                    throw new Exception($"Tài sản {item.MaVachThietBi} đang được thuê hoặc bảo trì, không thể xóa!");
                }
                item.DaXoa = true; // Xoá mềm
            }

            // 2. Thêm mới: Chưa có Id trên UI
            var dsInsert = dsUI.Where(x => x.Id <= 0).Select(x => new TaiSanChoThue
            {
                TenTaiSan = x.TenTaiSan,
                IdSanPham = idSanPham,
                MaVachThietBi = x.MaVachThietBi,
                TrangThai = ET.Constants.AppConstants.TrangThaiTaiSan.SanSang,
                DaXoa = false
            }).ToList();

            if (dsInsert.Any())
            {
                db.TaiSanChoThues.InsertAllOnSubmit(dsInsert);
            }

            // 3. Cập nhật thông tin các item cũ
            foreach (var uiItem in dsUI.Where(x => x.Id > 0))
            {
                var dbItem = dsDB.FirstOrDefault(x => x.Id == uiItem.Id);
                if (dbItem != null)
                {
                    dbItem.TenTaiSan = uiItem.TenTaiSan;
                    dbItem.MaVachThietBi = uiItem.MaVachThietBi;
                }
            }
        }

        #endregion
    }
}


