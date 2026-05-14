using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.Models.Kho;
using DAL.Repositories.Kho;
using ET.Results;

namespace BUS.Services.Kho
{
    public class BUS_ChungTuKho
    {
        #region Khởi tạo

        public static BUS_ChungTuKho Instance { get; } = new BUS_ChungTuKho();

        #endregion

        #region Sinh mã chứng từ

        /// <summary>
        /// Tạo mã chứng từ duy nhất theo format: PREFIX-yyMMdd-HHmm-XXXXX
        /// Dùng Ticks để giảm collision. DB UNIQUE constraint là safety net cuối cùng.
        /// </summary>
        public string SinhMaChungTu(string loaiChungTu)
        {
            string prefix;
            switch (loaiChungTu)
            {
                case AppConstants.LoaiChungTuKho.NHAP_MUA:     prefix = "NK"; break;
                case AppConstants.LoaiChungTuKho.XUAT_BAN:     prefix = "XK"; break;
                case AppConstants.LoaiChungTuKho.CHUYEN_KHO:   prefix = "CK"; break;
                case AppConstants.LoaiChungTuKho.KIEM_KE:       prefix = "KK"; break;
                case AppConstants.LoaiChungTuKho.TRA_NCC:       prefix = "TN"; break;
                case AppConstants.LoaiChungTuKho.KHACH_TRA:     prefix = "KT"; break;
                case AppConstants.LoaiChungTuKho.HUY_HONG:      prefix = "HH"; break;
                case AppConstants.LoaiChungTuKho.XUAT_BAOTRI:   prefix = "BT"; break;
                case AppConstants.LoaiChungTuKho.XUAT_SANXUAT:  prefix = "SX"; break;
                default:                                        prefix = "PK"; break;
            }

            string rand = (DateTime.Now.Ticks % 0x100000).ToString("X5");
            return $"{prefix}-{DateTime.Now:yyMMdd}-{DateTime.Now:HHmm}-{rand}";
        }

        #endregion

        #region Truy vấn

        /// <summary>
        /// Tải danh sách các phiếu nhập xuất ở mức tổng quan (dùng cho Grid bên ngoài).
        /// </summary>
        /// <param name="loaiChungTu">Loại chứng từ cần lọc (VD: NHAP_MUA). Để trống nếu muốn lấy tất cả.</param>
        /// <param name="tuNgay">Ngày bắt đầu tìm kiếm.</param>
        /// <param name="denNgay">Ngày kết thúc tìm kiếm.</param>
        /// <returns>Danh sách thông tin tóm tắt của các phiếu kho.</returns>
        public List<ET_ChungTuKho> GetDanhSachChungTu(string loaiChungTu, DateTime tuNgay, DateTime denNgay, int? idKho = null)
        {
            return DAL_ChungTuKho.Instance.GetDanhSach(loaiChungTu, tuNgay, denNgay, idKho);
        }

        public ET_ChungTuKho GetChiTietChungTu(int idChungTu)
        {
            return DAL_ChungTuKho.Instance.GetChiTiet(idChungTu);
        }

        #endregion

        #region Thêm / Sửa / Xóa / Ký Duyệt

        /// <summary>
        /// Kiểm tra và lưu phiếu kho.
        /// V3: Kho ảo được gán vào từng dòng ChiTietChungTu, không còn ở header.
        /// GUI gửi xuống IdKhoXuat/IdKhoNhap ở từng dòng chi tiết.
        /// BUS tự điền kho ảo complement cho các dòng còn thiếu theo loại chứng từ.
        /// </summary>
        public OperationResult LuuChungTu(ET_ChungTuKho et)
        {
            if (string.IsNullOrWhiteSpace(et.MaChungTu))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CTK_MA_RONG);

            if (et.ChiTiets == null || et.ChiTiets.Count == 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CTK_THIEU_CHITIET);

            if (et.LoaiChungTu != AppConstants.LoaiChungTuKho.KIEM_KE)
            {
                if (et.ChiTiets.Any(x => x.SoLuong <= 0))
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CTK_SOLUONG_AM);
            }

            try
            {
                // Tự điền kho ảo complement cho từng dòng chi tiết còn thiếu
                int? idKhoAo = null;
                switch (et.LoaiChungTu)
                {
                    case AppConstants.LoaiChungTuKho.NHAP_MUA:
                        idKhoAo = DAL_Kho.Instance.GetIdKhoTheoMa("KHO_NCC");
                        foreach (var ct in et.ChiTiets.Where(x => x.IdKhoXuat <= 0))
                            ct.IdKhoXuat = idKhoAo ?? 0;
                        break;
                    case AppConstants.LoaiChungTuKho.XUAT_BAN:
                        idKhoAo = DAL_Kho.Instance.GetIdKhoTheoMa("KHO_KHACH");
                        foreach (var ct in et.ChiTiets.Where(x => x.IdKhoNhap <= 0))
                            ct.IdKhoNhap = idKhoAo ?? 0;
                        break;
                    case AppConstants.LoaiChungTuKho.TRA_NCC:
                        idKhoAo = DAL_Kho.Instance.GetIdKhoTheoMa("KHO_NCC");
                        foreach (var ct in et.ChiTiets.Where(x => x.IdKhoNhap <= 0))
                            ct.IdKhoNhap = idKhoAo ?? 0;
                        break;
                    case AppConstants.LoaiChungTuKho.KHACH_TRA:
                        idKhoAo = DAL_Kho.Instance.GetIdKhoTheoMa("KHO_KHACH");
                        foreach (var ct in et.ChiTiets.Where(x => x.IdKhoXuat <= 0))
                            ct.IdKhoXuat = idKhoAo ?? 0;
                        break;
                    case AppConstants.LoaiChungTuKho.HUY_HONG:
                        idKhoAo = DAL_Kho.Instance.GetIdKhoTheoMa("KHO_HUY");
                        foreach (var ct in et.ChiTiets.Where(x => x.IdKhoNhap <= 0))
                            ct.IdKhoNhap = idKhoAo ?? 0;
                        break;
                    case AppConstants.LoaiChungTuKho.XUAT_BAOTRI:
                        idKhoAo = DAL_Kho.Instance.GetIdKhoTheoMa("KHO_BAOTRI");
                        foreach (var ct in et.ChiTiets.Where(x => x.IdKhoNhap <= 0))
                            ct.IdKhoNhap = idKhoAo ?? 0;
                        break;
                    case AppConstants.LoaiChungTuKho.XUAT_SANXUAT:
                        idKhoAo = DAL_Kho.Instance.GetIdKhoTheoMa("KHO_SANXUAT");
                        foreach (var ct in et.ChiTiets.Where(x => x.IdKhoNhap <= 0))
                            ct.IdKhoNhap = idKhoAo ?? 0;
                        break;
                }

                // Validate: mọi dòng phải có đủ IdKhoXuat và IdKhoNhap (trừ KIEM_KE)
                if (et.LoaiChungTu != AppConstants.LoaiChungTuKho.KIEM_KE)
                {
                    if (et.ChiTiets.Any(x => x.IdKhoXuat <= 0 || x.IdKhoNhap <= 0))
                        return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CTK_THIEU_KHONGUON_KHODICH);

                    if (et.ChiTiets.Any(x => x.IdKhoXuat == x.IdKhoNhap))
                        return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CTK_TRUNG_KHO_XUAT_NHAP);
                }

                DAL_ChungTuKho.Instance.LuuChungTu(et);
                return OperationResult.Ok("MSG_CTK_LUU_OK");
            }
            catch (InvalidOperationException ex)
            {
                return OperationResult.Fail("ERR_CTK_DA_DUYET|" + ex.Message);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("ERR_SYSTEM_FAIL|" + ex.Message);
            }
        }

        public OperationResult XoaChungTu(int idChungTu)
        {
             try
             {
                 DAL_ChungTuKho.Instance.XoaChungTu(idChungTu);
                 return OperationResult.Ok("MSG_CTK_XOA_OK");
             }
             catch (InvalidOperationException ex)
             {
                 return OperationResult.Fail("ERR_CTK_DA_DUYET|" + ex.Message); 
             }
             catch (Exception ex)
             {
                 return OperationResult.Fail("ERR_SYSTEM_FAIL|" + ex.Message);
             }
        }

        /// <summary>
        /// Ký duyệt phiếu kho.
        /// V3: Không còn ghi SoCai — tồn kho tính từ ChiTietChungTu qua index.
        /// Với phiếu KIEM_KE: tự sinh ChiTietChungTu điều chỉnh chênh lệch vào KHO_CHENH_LECH.
        /// </summary>
        public OperationResult DuyetChungTu(int idChungTu, int idNguoiDuyet)
        {
            var et = DAL_ChungTuKho.Instance.GetChiTiet(idChungTu);
            if (et == null) return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CTK_KHONG_TON_TAI);

            if (et.TrangThai == AppConstants.TrangThaiChungTuKho.DaDuyet)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CTK_DA_DUYET_TRUOC_DO);

            // Kiểm tra tồn kho đủ xuất (trừ Nhập mua và Khách trả)
            if (et.LoaiChungTu != AppConstants.LoaiChungTuKho.NHAP_MUA &&
                et.LoaiChungTu != AppConstants.LoaiChungTuKho.KHACH_TRA &&
                et.LoaiChungTu != AppConstants.LoaiChungTuKho.KIEM_KE)
            {
                // Kiểm tra từng dòng chi tiết (mỗi dòng có IdKhoXuat riêng)
                foreach (var chiTiet in et.ChiTiets)
                {
                    var khoXuat = DAL_Kho.Instance.GetById(chiTiet.IdKhoXuat);
                    if (khoXuat != null && !khoXuat.ChoPhepTonAm)
                    {
                        decimal ton = DAL_SoCai.Instance.GetTonKhoHienTai(chiTiet.IdKhoXuat, chiTiet.IdSanPham, chiTiet.IdLoHang);
                        if (ton < chiTiet.SoLuong)
                            return OperationResult.Fail($"ERR_CTK_TON_KHONG_DU|{chiTiet.IdSanPham}|{ton}|{chiTiet.SoLuong}");
                    }
                }
            }

            // Với phiếu KIEM_KE: sinh thêm ChiTietChungTu điều chỉnh chênh lệch
            if (et.LoaiChungTu == AppConstants.LoaiChungTuKho.KIEM_KE)
            {
                // Đọc ChiTietKiemKe từ DB để lấy SoLuongHeThong/SoLuongThucTe
                var chiTietKiemKe = DAL_ChungTuKho.Instance.GetChiTietKiemKe(idChungTu);
                if (chiTietKiemKe == null || !chiTietKiemKe.Any())
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CTK_THIEU_CHITIET);

                int? idKhoCL = DAL_Kho.Instance.GetIdKhoTheoMa("KHO_CHENH_LECH");
                if (idKhoCL == null) return OperationResult.Fail("ERR_CTK_THIEU_KHO_CHENH_LECH");

                var dsChiTietDieuChinh = new List<ET_ChiTietChungTu>();
                foreach (var kk in chiTietKiemKe)
                {
                    if (kk.ChenhLech == 0) continue;
                    dsChiTietDieuChinh.Add(new ET_ChiTietChungTu
                    {
                        IdSanPham = kk.IdSanPham,
                        IdLoHang = kk.IdLoHang,
                        // Thừa: KHO_CHENH_LECH → Kho kiểm | Thiếu: Kho kiểm → KHO_CHENH_LECH
                        IdKhoXuat = kk.ChenhLech > 0 ? idKhoCL.Value : kk.IdKho,
                        IdKhoNhap = kk.ChenhLech > 0 ? kk.IdKho : idKhoCL.Value,
                        SoLuong = Math.Abs(kk.ChenhLech),
                        GhiChu = $"Điều chỉnh kiểm kê: {kk.ChenhLech:+#;-#;0}"
                    });
                }

                if (dsChiTietDieuChinh.Any())
                    DAL_ChungTuKho.Instance.ThemChiTietDieuChinh(idChungTu, dsChiTietDieuChinh);
            }

            try
            {
                DAL_ChungTuKho.Instance.DuyetChungTu(idChungTu, idNguoiDuyet);
                return OperationResult.Ok("MSG_CTK_DUYET_OK");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"ERR_SYSTEM_FAIL|{ex.Message}");
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        public OperationResult HuyChungTu(int idChungTu, int idNguoiHuy, string lyDo)
        {
            var et = DAL_ChungTuKho.Instance.GetChiTiet(idChungTu);
            if (et == null) return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CTK_KHONG_TON_TAI);

            if (et.TrangThai == AppConstants.TrangThaiChungTuKho.DaDuyet)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CTK_DA_DUYET_TRUOC_DO);

            if (et.TrangThai == AppConstants.TrangThaiChungTuKho.DaHuy)
                return OperationResult.Fail("ERR_CTK_DA_HUY");

            try
            {
                DAL_ChungTuKho.Instance.DoiTrangThai(idChungTu,
                    AppConstants.TrangThaiChungTuKho.DaHuy, idNguoiHuy, lyDo);
                return OperationResult.Ok("MSG_CTK_HUY_OK");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"ERR_SYSTEM_FAIL|{ex.Message}");
            }
        }

        // Duyệt cấp 2: phiếu đã qua duyệt cấp 1 (ChoDuyet2) → DaDuyet + ghi sổ cái.
        public OperationResult DuyetCap2(int idChungTu, int idNguoiDuyet)
        {
            var et = DAL_ChungTuKho.Instance.GetChiTiet(idChungTu);
            if (et == null) return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CTK_KHONG_TON_TAI);

            if (et.TrangThai != AppConstants.TrangThaiChungTuKho.ChoDuyet2)
                return OperationResult.Fail("ERR_CTK_CHUA_DUYET_CAP1");

            return DuyetChungTu(idChungTu, idNguoiDuyet);
        }

        #endregion
    }
}


