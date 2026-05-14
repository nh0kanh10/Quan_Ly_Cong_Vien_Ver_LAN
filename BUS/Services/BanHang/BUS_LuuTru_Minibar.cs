using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using ET.Constants;
using ET.Results;

namespace BUS.Services.BanHang
{
    /// <summary>
    /// Nghiệp vụ Minibar: lấy sản phẩm tiện lợi + thêm vào bill phòng đang ở.
    /// </summary>
    public class BUS_LuuTru_Minibar
    {
        #region Khởi tạo Singleton
        public static BUS_LuuTru_Minibar Instance { get; } = new BUS_LuuTru_Minibar();
        #endregion

        #region Truy vấn dữ liệu

        /// <summary>
        /// Lấy danh sách sản phẩm minibar (đồ uống đóng chai + ăn uống tiện lợi).
        /// Chỉ lấy sản phẩm chưa bị xóa mềm.
        /// </summary>
        public OperationResult<List<SanPham>> LayDanhSachSanPhamMinibar()
        {
            try
            {
                var loaiMinibar = new[]
                {
                    AppConstants.LoaiSanPham.DoUongDongChai,
                    AppConstants.LoaiSanPham.AnUongTienLoi
                };

                using (var db = new DaiNamDBDataContext())
                {
                    var ds = db.SanPhams
                        .Where(sp => sp.DaXoa == false && loaiMinibar.Contains(sp.LoaiSanPham))
                        .OrderBy(sp => sp.TenSanPham)
                        .ToList();

                    return OperationResult<List<SanPham>>.Ok(ds);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<List<SanPham>>.Fail("ERR_MINIBAR_LOAD: " + ex.Message);
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        /// <summary>
        /// Thêm danh sách sản phẩm minibar vào bill của phòng đang ở.
        /// Tìm DonHang qua ChiTietDatPhong → ChiTietDonHang → DonHang.
        /// Nếu chưa có bill → tạo mới.
        /// </summary>
        /// <param name="idChiTietDatPhong">FK của chi tiết đặt phòng đang ở</param>
        /// <param name="danhSachItem">Danh sách (IdSanPham, SoLuong, DonGia)</param>
        /// <param name="idNhanVien">Nhân viên thực hiện</param>
        public OperationResult<bool> ThemMinibarVaoBill(
            int idChiTietDatPhong,
            List<MinibarItem> danhSachItem,
            int idNhanVien)
        {
            if (danhSachItem == null || danhSachItem.Count == 0)
                return OperationResult<bool>.Fail("ERR_MINIBAR_EMPTY");

            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    // 1. Tìm chi tiết đặt phòng
                    var ct = db.ChiTietDatPhongs.FirstOrDefault(x => x.Id == idChiTietDatPhong);
                    if (ct == null) return OperationResult<bool>.Fail("ERR_MINIBAR_ROOM_NOT_FOUND");

                    // 2. Tìm DonHang qua FK chain: ChiTietDatPhong → ChiTietDonHang → DonHang
                    var ctDonHang = db.ChiTietDonHangs.FirstOrDefault(x => x.Id == ct.IdChiTietDonHang);
                    DonHang bill = ctDonHang != null
                        ? db.DonHangs.FirstOrDefault(d => d.Id == ctDonHang.IdDonHang)
                        : null;

                    // 3. Nếu chưa có bill → tạo mới
                    if (bill == null)
                    {
                        var phieu = ct.PhieuDatPhong;
                        bill = new DonHang
                        {
                            MaDonHang = "DH" + DateTime.Now.ToString("yyMMddHHmmssfff"),
                            IdKhachHang = phieu != null ? phieu.IdKhachHang : (int?)null,
                            IdNhanVien = idNhanVien,
                            NguonBan = AppConstants.NguonBan.TrucTiep,
                            TrangThai = AppConstants.TrangThaiDonHang.ChoThanhToan,
                            TongTienHang = 0,
                            NgayTao = DateTime.Now
                        };
                        db.DonHangs.InsertOnSubmit(bill);
                        db.SubmitChanges();
                    }

                    // 4. Chèn từng dòng sản phẩm minibar vào bill
                    foreach (var item in danhSachItem)
                    {
                        if (item.SoLuong <= 0) continue;

                        db.ChiTietDonHangs.InsertOnSubmit(new ChiTietDonHang
                        {
                            IdDonHang = bill.Id,
                            IdSanPham = item.IdSanPham,
                            SoLuong = item.SoLuong,
                            DonGiaThucTe = item.DonGia,
                            GhiChu = "Minibar"
                        });
                    }

                    // 5. Cập nhật tổng tiền bill
                    db.SubmitChanges();
                    decimal tongMinibar = danhSachItem.Sum(x => x.SoLuong * x.DonGia);
                    bill.TongTienHang = bill.TongTienHang + tongMinibar;
                    db.SubmitChanges();

                    return OperationResult<bool>.Ok(true);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail("ERR_MINIBAR_SAVE: " + ex.Message);
            }
        }

        #endregion
    }

    /// <summary>
    /// DTO nhỏ chứa thông tin 1 dòng minibar cần thêm vào bill.
    /// </summary>
    public class MinibarItem
    {
        public int IdSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
    }
}


