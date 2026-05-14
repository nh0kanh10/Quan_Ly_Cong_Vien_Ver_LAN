using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Repositories.DanhMuc;
using ET.Constants;
using ET.DTOs;
using ET.Models.DanhMuc;
using ET.Results;

namespace BUS.Services.DanhMuc
{
    public class BUS_Combo
    {
        public static BUS_Combo Instance { get; } = new BUS_Combo();

        private readonly DAL_Combo _dal = DAL_Combo.Instance;

        #region Truy vấn dữ liệu

        public List<ET_Combo> LayDanhSach()
        {
            return _dal.LayDanhSach().Select(c => new ET_Combo
            {
                Id = c.Id,
                MaCombo = c.MaCombo,
                TenCombo = c.TenCombo,
                GiaCombo = c.GiaCombo,
                MoTa = c.MoTa,
                TrangThai = c.TrangThai,
                DaXoa = c.DaXoa,
                NgayTao = c.NgayTao
            }).ToList();
        }

        public List<DTO_ComboChiTietDisplay> LayChiTiet(int idCombo)
        {
            return _dal.LayChiTiet(idCombo);
        }

        public List<ET_SanPham> LaySanPhamChonCombo()
        {
            return _dal.LaySanPhamChonCombo().Select(sp => new ET_SanPham
            {
                Id = sp.Id,
                MaSanPham = sp.MaSanPham,
                TenSanPham = sp.TenSanPham,
                LoaiSanPham = sp.LoaiSanPham,
                DonGia = sp.DonGia
            }).ToList();
        }

        #endregion

        #region Thêm / Sửa / Xoá

        public OperationResult ThemMoi(ET_Combo dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TenCombo))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_COMBO_TEN_RONG);

            if (dto.GiaCombo < 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_COMBO_GIA_AM);

            try
            {
                string ma = _dal.SinhMaCombo();

                var entity = new Combo
                {
                    MaCombo = ma,
                    TenCombo = dto.TenCombo.Trim(),
                    GiaCombo = dto.GiaCombo,
                    MoTa = dto.MoTa?.Trim(),
                    TrangThai = AppConstants.TrangThaiCombo.BanNhap,
                    DaXoa = false,
                    NgayTao = DateTime.Now
                };

                int id = _dal.ThemMoi(entity);
                return OperationResult.Ok(id, "MSG_LUU_OK");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("ERR_COMBO_THEM|" + ex.Message);
            }
        }

        public OperationResult CapNhat(ET_Combo dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TenCombo))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_COMBO_TEN_RONG);

            if (dto.GiaCombo < 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_COMBO_GIA_AM);

            try
            {
                var entity = new Combo
                {
                    Id = dto.Id,
                    TenCombo = dto.TenCombo.Trim(),
                    GiaCombo = dto.GiaCombo,
                    MoTa = dto.MoTa?.Trim(),
                    TrangThai = dto.TrangThai
                };

                _dal.CapNhat(entity);
                return OperationResult.Ok("MSG_LUU_OK");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("ERR_COMBO_SUA|" + ex.Message);
            }
        }

        public OperationResult XoaMem(int id)
        {
            try
            {
                _dal.XoaMem(id);
                return OperationResult.Ok("MSG_XOA_OK");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("ERR_COMBO_XOA|" + ex.Message);
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        /// <summary>
        /// Lưu danh sách thành phần rổ combo.
        /// Nếu tổng tỷ lệ phân bổ khác 100%, tự ép combo về BanNhap.
        /// </summary>
        public OperationResult LuuChiTiet(int idCombo, List<ET_ComboChiTiet> dsChiTiet)
        {
            decimal tongTyLe = dsChiTiet.Sum(ct => ct.TyLePhanBo);

            if (tongTyLe != 100m)
            {
                _dal.CapNhatTrangThai(idCombo, AppConstants.TrangThaiCombo.BanNhap);
            }

            try
            {
                var dsEntity = dsChiTiet.Select(ct => new ComboChiTiet
                {
                    IdCombo = idCombo,
                    IdSanPham = ct.IdSanPham,
                    SoLuong = ct.SoLuong,
                    TyLePhanBo = ct.TyLePhanBo
                }).ToList();

                _dal.LuuChiTiet(idCombo, dsEntity);
                return OperationResult.Ok("MSG_LUU_RO_OK");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("ERR_COMBO_LUU_RO|" + ex.Message);
            }
        }

        #endregion
    }
}


