using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DAL.Repositories.BanHang;
using DAL.Repositories.DanhMuc;
using ET.Constants;
using ET.DTOs;
using ET.Models.BanHang;
using ET.Results;

namespace BUS.Services.BanHang
{
    public class BUS_ThueDo
    {
        #region Khởi tạo (hoặc cấu trúc Singleton)

        public static BUS_ThueDo Instance { get; } = new BUS_ThueDo();

        #endregion

        #region Truy vấn dữ liệu

        // Lấy danh sách sản phẩm cho thuê kèm giá + cọc.
        // Chỉ lấy SP có LoaiSanPham = 'DoChoThue', TrangThai = DangBan.
        public OperationResult LayDanhSachNguonChoThue(int idDiemBan, string langCode = "vi-VN")
        {
            try
            {
                var data = DAL_ThueDo.Instance.LayDanhSachNguonChoThue(idDiemBan, langCode);
                return OperationResult.Ok(data);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        // Lấy phiên thuê chưa trả để hiện trên grid giám sát.
        public OperationResult LayDanhSachChuaTra(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                var data = DAL_ThueDo.Instance.LayDanhSachChuaTra(tuNgay, denNgay);
                return OperationResult.Ok(data);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        // Tìm phiên thuê theo mã biên lai (khách vãng lai đưa giấy).
        public OperationResult LayPhienTheoMaDon(string maDonHang)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maDonHang))
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RETURN_DONHANG_KHONG_TIM_THAY);

                var data = DAL_ThueDo.Instance.LayTheoMaDon(maDonHang);
                if (data == null || !data.Any())
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RETURN_KHACH_KHONG_CO_DO);

                return OperationResult.Ok(data);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        // Tìm phiên thuê theo khách hàng (quẹt RFID -> có IdKhachHang).
        public OperationResult LayPhienTheoKhach(int idKhachHang)
        {
            try
            {
                var data = DAL_ThueDo.Instance.LayTheoKhachHang(idKhachHang);
                if (data == null || !data.Any())
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RETURN_KHACH_KHONG_CO_DO);

                return OperationResult.Ok(data);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        // Quét barcode tài sản vật lý (đồ lớn: xe điện, xe đạp).
        // Kiểm tra trạng thái: SanSang mới cho thuê được.
        public OperationResult QuetBarcodeTaiSan(string maVach)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maVach))
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RENTAL_TAISAN_KHONG_TIM_THAY);

                var taiSan = DAL_TaiSanChoThue.Instance.LayTheoBarcode(maVach);
                if (taiSan == null)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RENTAL_TAISAN_KHONG_TIM_THAY);

                if (taiSan.TrangThai != AppConstants.TrangThaiTaiSan.SanSang)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RENTAL_TAISAN_DANG_THUE);

                return OperationResult.Ok(taiSan);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        /// <summary>
        /// Xử lý giao đồ thuê: validate giỏ -> tạo đơn hàng + phiên thuê + thu cọc.
        /// Nếu RFID thì kiểm tra số dư ví trước.
        /// </summary>
        /// <param name="req">Dữ liệu giỏ thuê + phương thức thanh toán. Bắt buộc có IdPhienThuNgan > 0.</param>
        /// <returns>Mã đơn hàng (biên lai) nếu thành công</returns>
        public OperationResult XuLyGiaoDo(DTO_RentalCheckoutRequest req)
        {
            try
            {
                // 1. Giỏ thuê rỗng
                if (req.GioThue == null || !req.GioThue.Any())
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RENTAL_CART_EMPTY);

                // 2. Bắt buộc phải có phiên thu ngân để đơn thuê xuất hiện trong báo cáo ca
                if (req.IdPhienThuNgan <= 0)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_NO_OPEN_SESSION);

                // 3. RFID -> kiểm tra ví đủ tiền
                if (req.PhuongThucTT == AppConstants.PhuongThucTT.ViRFID)
                {
                    if (!req.IdViDienTu.HasValue)
                        return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RENTAL_RFID_REQUIRED);

                    decimal tongTT = req.GioThue.Sum(x => x.TongThue + x.TongCoc);
                    decimal soDu = DAL.Repositories.BanHang.DAL_POS.Instance.LaySoDuVi(req.IdViDienTu.Value);
                    if (soDu < tongTT)
                        return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RENTAL_VI_KHONG_DU);
                }

                // 3. Kiểm tra tài sản vật lý (đồ lớn) có đang bị thuê không
                foreach (var item in req.GioThue.Where(x => x.IdTaiSanChoThue.HasValue))
                {
                    var ts = DAL_TaiSanChoThue.Instance.LayTheoBarcode(item.MaVachTaiSan ?? "");
                    if (ts != null && ts.TrangThai != AppConstants.TrangThaiTaiSan.SanSang)
                        return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RENTAL_TAISAN_DANG_THUE);
                }

                // 4. Gọi DAL tạo transaction
                string maDon = DAL_ThueDo.Instance.GiaoDo(req);
                return OperationResult.Ok(maDon, AppConstants.ErrorMessages.MSG_RENTAL_CHECKOUT_SUCCESS);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Xử lý nhận trả đồ: validate SL -> cập nhật trạng thái -> hoàn cọc / ghi phạt.
        /// </summary>
        /// <param name="dsTraDo">Danh sách đồ trả + đồ mất (NV nhập trên grid)</param>
        /// <param name="maDonHang">Mã biên lai gốc</param>
        /// <param name="idNhanVien">NV đang thao tác</param>
        /// <param name="phuongThucHoan">Hoàn cọc bằng cách nào (TienMat, ViRFID)</param>
        /// <param name="idDiemBan">ID Điểm bán hiện tại (Máy POS thu hồi)</param>
        /// <param name="idPhienThuNgan">Phiên thu ngân đang mở — ghi vào IdPhienTra để đối soát giao ca chéo</param>
        /// <returns>Kết quả thao tác</returns>
        public OperationResult XuLyTraDo(List<DTO_ThuHoiRequest> dsTraDo, string maDonHang, int idNhanVien, string phuongThucHoan, int idDiemBan, int idPhienThuNgan)
        {
            try
            {
                // 1. Rỗng
                if (dsTraDo == null || !dsTraDo.Any())
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RETURN_NO_ITEMS);

                // 2. Bắt buộc phải có phiên thu ngân để ghi nhận ca hoàn cọc
                if (idPhienThuNgan <= 0)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_NO_OPEN_SESSION);

                // 3. Kiểm tra SL trả + SL mất không vượt SL đang thuê
                foreach (var item in dsTraDo)
                {
                    if (item.SoLuongTra + item.SoLuongMat > item.SoLuongDangThue)
                        return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RETURN_SL_VUOT_QUA);

                    // Nếu mất mà chưa nhập tiền phạt -> nhắc NV
                    if (item.SoLuongMat > 0 && item.TienPhat <= 0)
                        return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RETURN_PHAT_CHUA_NHAP);
                }

                // 4. Gọi DAL
                DAL_ThueDo.Instance.TraDo(dsTraDo, maDonHang, idNhanVien, phuongThucHoan, idDiemBan, idPhienThuNgan);
                return OperationResult.Ok(AppConstants.ErrorMessages.MSG_RETURN_SUCCESS);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tính phí lố giờ dựa trên BangGia_ThueTheoGio.
        /// Công thức: Block đầu = GiaBan. Vượt block -> ceil((phút vượt) / PhutTiep) × GiaPhuThu.
        /// <param name="tienThueBlockDau">Giá thuê block đầu (từ BangGia.GiaBan)</param>
        /// <param name="phutBlock">Số phút block đầu</param>
        /// <param name="phutTiep">Số phút mỗi block tiếp theo</param>
        /// <param name="giaPhuThu">Giá mỗi block tiếp theo</param>
        /// <param name="thoiGianThue">Thời điểm bắt đầu thuê</param>
        /// <returns>Tổng phí thuê tính đến hiện tại</returns>
        /// </summary>
        public decimal TinhPhiThue(decimal tienThueBlockDau, int? phutBlock, int? phutTiep, decimal? giaPhuThu, DateTime thoiGianThue)
        {
            int tongPhut = (int)Math.Ceiling((DateTime.Now - thoiGianThue).TotalMinutes);
            if (tongPhut <= 0) return 0m;

            // Không có cấu hình block -> tính phẳng theo giá block đầu
            if (!phutBlock.HasValue || phutBlock.Value <= 0)
                return tienThueBlockDau;

            // Trong block đầu -> chỉ tính giá block đầu
            if (tongPhut <= phutBlock.Value)
                return tienThueBlockDau;

            // Vượt block -> tính phụ thu
            int phutVuot = tongPhut - phutBlock.Value;
            int soBlockTiep = (!phutTiep.HasValue || phutTiep.Value <= 0)
                ? 1
                : (int)Math.Ceiling((double)phutVuot / phutTiep.Value);

            decimal phuThu = soBlockTiep * (giaPhuThu ?? 0m);
            return tienThueBlockDau + phuThu;
        }

        #endregion
    }
}


