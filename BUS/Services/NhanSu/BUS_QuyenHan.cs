using System;
using System.Collections.Generic;
using DAL.Repositories.NhanSu;
using ET.Models.NhanSu;
using ET.Results;

namespace BUS.Services.NhanSu
{
    public class BUS_QuyenHan
    {
        #region Khởi tạo (Singleton)
        
        public static BUS_QuyenHan Instance { get; } = new BUS_QuyenHan();

        #endregion

        #region Truy vấn dữ liệu

        public OperationResult LoadDS()
        {
            try
            {
                var list = DAL_QuyenHan.Instance.LoadDS();
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


