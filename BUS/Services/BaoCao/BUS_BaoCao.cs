using System;
using System.Collections.Generic;
using ET.DTOs;
using ET.Results;
using DAL.Repositories.BaoCao;

namespace BUS.Services.BaoCao
{
    public class BUS_BaoCao
    {
        public static BUS_BaoCao Instance { get; } = new BUS_BaoCao();

        private BUS_BaoCao() { }

        public OperationResult<List<DTO_BaoCaoDoanhThu>> LayBaoCaoDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                if (tuNgay > denNgay)
                {
                    return OperationResult<List<DTO_BaoCaoDoanhThu>>.Fail("ERR_NGAY_BAT_DAU_LON_HON_KET_THUC");
                }

                var data = DAL_BaoCao.Instance.LayBaoCaoDoanhThu(tuNgay, denNgay);
                return OperationResult<List<DTO_BaoCaoDoanhThu>>.Ok(data);
            }
            catch (Exception ex)
            {
                return OperationResult<List<DTO_BaoCaoDoanhThu>>.Fail(ex.Message);
            }
        }
    }
}


