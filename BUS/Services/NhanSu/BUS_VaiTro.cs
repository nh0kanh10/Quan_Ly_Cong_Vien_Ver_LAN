using System;
using System.Collections.Generic;
using DAL.Repositories.NhanSu;
using ET.Models.NhanSu;
using ET.Results;

namespace BUS.Services.NhanSu
{
    public class BUS_VaiTro
    {
        #region Khởi tạo (Singleton)
        
        public static BUS_VaiTro Instance { get; } = new BUS_VaiTro();

        #endregion

        #region Truy vấn dữ liệu

        public OperationResult LoadDS()
        {
            try
            {
                var list = DAL_VaiTro.Instance.LoadDS();
                return OperationResult.Ok(list);
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        #endregion
    }
}


