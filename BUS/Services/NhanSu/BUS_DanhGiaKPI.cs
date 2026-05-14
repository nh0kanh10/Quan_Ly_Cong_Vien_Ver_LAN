using System.Collections.Generic;
using ET.Models.NhanSu;
using ET.Results;
using DAL.Repositories.NhanSu;

namespace BUS.Services.NhanSu
{
    public class BUS_DanhGiaKPI
    {
        public static BUS_DanhGiaKPI Instance { get; } = new BUS_DanhGiaKPI();
        private BUS_DanhGiaKPI() { }

        public OperationResult<List<DTO_DanhGiaKPI>> LayDanhSachTheoNhanVien(string maNhanVien)
        {
            var nv = DAL.Repositories.DoiTac.DAL_NhanVien.Instance.LayChiTiet(maNhanVien);
            if (nv == null)
                return OperationResult<List<DTO_DanhGiaKPI>>.Fail("ERR_NHANVIEN_KHONG_TON_TAI");

            return DAL_DanhGiaKPI.Instance.LayDanhSachTheoNhanVien(nv.IdDoiTac);
        }
    }
}


