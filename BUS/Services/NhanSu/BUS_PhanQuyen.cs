using ET.Constants;
using System;
using System.Collections.Generic;
using DAL.Repositories.NhanSu;
using ET.Results;

namespace BUS.Services.NhanSu
{
    public class BUS_PhanQuyen
    {
        #region Khởi tạo (Singleton)
        
        public static BUS_PhanQuyen Instance { get; } = new BUS_PhanQuyen();

        #endregion

        #region Truy vấn dữ liệu

        public OperationResult LayQuyenTheoVaiTro(int idVaiTro)
        {
            try
            {
                var quyens = DAL_PhanQuyen.Instance.LayQuyenTheoVaiTro(idVaiTro);
                return OperationResult.Ok(quyens);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        #endregion

        #region Thêm / Sửa / Xóa

        public OperationResult CapNhatQuyen(int idVaiTro, List<int> dsIdQuyen)
        {
            try
            {
                if (idVaiTro <= 0)
                {
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_PHANQUYEN_INVALID_ROLE);
                }

                DAL_PhanQuyen.Instance.CapNhatQuyen(idVaiTro, dsIdQuyen);
                
                return OperationResult.Ok(null, "MSG_PHANQUYEN_SUCCESS");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        #endregion
    }
}


