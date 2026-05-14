using System.Collections.Generic;
using ET.Models.NhanSu;
using ET.Results;
using DAL.Repositories.NhanSu;

namespace BUS.Services.NhanSu
{
    public class BUS_HopDong
    {
        public static BUS_HopDong Instance { get; } = new BUS_HopDong();
        private BUS_HopDong() { }

        public OperationResult<List<DTO_HopDong>> LayDanhSachTheoNhanVien(string maNhanVien)
        {
            var nv = DAL.Repositories.DoiTac.DAL_NhanVien.Instance.LayChiTiet(maNhanVien);
            if (nv == null)
                return OperationResult<List<DTO_HopDong>>.Fail("ERR_NHANVIEN_KHONG_TON_TAI");

            return DAL_HopDong.Instance.LayDanhSachTheoNhanVien(nv.IdDoiTac);
        }
    }
}


