using System;
using System.Collections.Generic;
using ET.Models.DoiTac;
using ET.Results;

namespace BUS.Services.DoiTac
{
    public class BUS_NhanVien
    {
        #region Khởi tạo (Singleton)
        public static BUS_NhanVien Instance { get; } = new BUS_NhanVien();
        #endregion

        #region Truy vấn dữ liệu

        public List<DTO_NhanVienChiTiet> LayDanhSach(string tuKhoa = null)
        {
            return DAL.Repositories.DoiTac.DAL_NhanVien.Instance.LayDanhSach(tuKhoa);
        }

        public DTO_NhanVienChiTiet LayChiTiet(int idDoiTac)
        {
            return DAL.Repositories.DoiTac.DAL_NhanVien.Instance.LayChiTiet(idDoiTac);
        }

        public OperationResult<DTO_NhanVienChiTiet> LayChiTiet(string maNhanVien)
        {
            var data = DAL.Repositories.DoiTac.DAL_NhanVien.Instance.LayChiTiet(maNhanVien);
            if (data == null)
                return OperationResult<DTO_NhanVienChiTiet>.Fail("ERR_NHANVIEN_KHONG_TON_TAI");
            return OperationResult<DTO_NhanVienChiTiet>.Ok(data);
        }

        #endregion

        #region Thêm / Sửa / Xoá

        public OperationResult Them(DTO_NhanVienChiTiet dto)
        {
            if (string.IsNullOrWhiteSpace(dto.HoTen))
                return OperationResult.Fail("ERR_HOTEN_RONG");

            if (string.IsNullOrWhiteSpace(dto.DienThoai))
                return OperationResult.Fail("ERR_DIENTHOAI_RONG");

            if (DAL.Repositories.DoiTac.DAL_NhanVien.Instance.KiemTraTrungSDT(dto.DienThoai))
                return OperationResult.Fail($"ERR_TRUNG_SDT|{dto.DienThoai}");

            try
            {
                DAL.Repositories.DoiTac.DAL_NhanVien.Instance.Them(dto);
                return OperationResult.Ok("MSG_LUU_THANH_CONG");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"ERR_LOI_HETHONG|{ex.Message}");
            }
        }

        public OperationResult CapNhat(DTO_NhanVienChiTiet dto)
        {
            if (dto.IdDoiTac <= 0)
                return OperationResult.Fail("ERR_ID_KHONGHOPHOP");

            if (string.IsNullOrWhiteSpace(dto.HoTen))
                return OperationResult.Fail("ERR_HOTEN_RONG");

            if (string.IsNullOrWhiteSpace(dto.DienThoai))
                return OperationResult.Fail("ERR_DIENTHOAI_RONG");

            if (DAL.Repositories.DoiTac.DAL_NhanVien.Instance.KiemTraTrungSDT(dto.DienThoai, dto.IdDoiTac))
                return OperationResult.Fail($"ERR_TRUNG_SDT|{dto.DienThoai}");

            try
            {
                DAL.Repositories.DoiTac.DAL_NhanVien.Instance.CapNhat(dto);
                return OperationResult.Ok("MSG_CAPNHAT_THANH_CONG");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"ERR_LOI_HETHONG|{ex.Message}");
            }
        }

        public OperationResult XoaMem(int idDoiTac)
        {
            if (idDoiTac <= 0)
                return OperationResult.Fail("ERR_ID_KHONGHOPHOP");

            try
            {
                DAL.Repositories.DoiTac.DAL_NhanVien.Instance.XoaMem(idDoiTac);
                return OperationResult.Ok("MSG_XOA_THANH_CONG");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"ERR_LOI_HETHONG|{ex.Message}");
            }
        }

        #endregion
    }
}


