using System.Collections.Generic;
using System.Linq;
using ET.Constants;

namespace BUS.Services.DoiTac
{
    public class BUS_DoiTac
    {
        public static BUS_DoiTac Instance { get; } = new BUS_DoiTac();

        public List<ET.Models.DoiTac.ThongTin> GetAllDoiTac()
        {
            return DAL.Repositories.DoiTac.DAL_DoiTac.Instance.GetAllDoiTac();
        }

        public string GetTenDoiTac(int idDoiTac)
        {
            using (var db = new DAL.DaiNamDBDataContext())
            {
                var dt = db.ThongTins.FirstOrDefault(x => x.Id == idDoiTac);
                return dt != null ? dt.HoTen : "";
            }
        }

        public ET.Results.OperationResult<ET.Models.DoiTac.ThongTin> LayChiTietTheoDienThoai(string sdt)
        {
            using (var db = new DAL.DaiNamDBDataContext())
            {
                var kh = db.ThongTins.FirstOrDefault(x => x.DienThoai == sdt && x.LoaiDoiTac == AppConstants.LoaiDoiTac.CaNhan);
                if (kh != null)
                {
                    return ET.Results.OperationResult<ET.Models.DoiTac.ThongTin>.Ok(new ET.Models.DoiTac.ThongTin 
                    { 
                        HoTen = kh.HoTen, 
                        DienThoai = kh.DienThoai 
                    });
                }
                return ET.Results.OperationResult<ET.Models.DoiTac.ThongTin>.Fail("Không tìm thấy khách hàng");
            }
        }
    }
}


