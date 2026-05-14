using System;
using System.Collections.Generic;
using DAL.Repositories.HeThong;
using ET.Results;

namespace BUS.Services.HeThong
{
    public class BUS_NhatKy
    {
        public static BUS_NhatKy Instance { get; } = new BUS_NhatKy();

        private readonly DAL_NhatKyThaoTac _dal = DAL_NhatKyThaoTac.Instance;

        public void GhiLog(string thucThe, int idThucThe, string hanhDong,
            int idNguoiThucHien, string giaTriCu = null, string giaTriMoi = null,
            string ghiChu = null)
        {
            try
            {
                _dal.GhiLog(thucThe, idThucThe, hanhDong, idNguoiThucHien,
                    giaTriCu, giaTriMoi, ghiChu);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("BUS_NhatKy.GhiLog failed: " + ex.Message);
            }
        }

        public OperationResult TraCuuLog(DateTime tuNgay, DateTime denNgay,
            string thucThe = "", int idNguoiThucHien = 0)
        {
            try
            {
                var data = _dal.TraCuuLog(tuNgay, denNgay, thucThe, idNguoiThucHien);
                return new OperationResult { Success = true, Data = data };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = ex.Message };
            }
        }

        public OperationResult LayDanhSachModule()
        {
            try
            {
                var data = _dal.LayDanhSachModule();
                return new OperationResult { Success = true, Data = data };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = ex.Message };
            }
        }
    }
}


