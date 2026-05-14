using System.Collections.Generic;
using ET.Models.NhanSu;
using ET.Results;
using DAL.Repositories.NhanSu;

namespace BUS.Services.NhanSu
{
    public class BUS_LichLamViec
    {
        public static BUS_LichLamViec Instance { get; } = new BUS_LichLamViec();
        private BUS_LichLamViec() { }

        public OperationResult<List<DTO_LichLamViecChiTiet>> LayDanhSachTheoNhanVien(string maNhanVien)
        {
            var nv = DAL.Repositories.DoiTac.DAL_NhanVien.Instance.LayChiTiet(maNhanVien);
            if (nv == null)
                return OperationResult<List<DTO_LichLamViecChiTiet>>.Fail("ERR_NHANVIEN_KHONG_TON_TAI");

            return DAL_LichLamViec.Instance.LayDanhSachTheoNhanVien(nv.IdDoiTac);
        }
    }
}


