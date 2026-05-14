using System.Collections.Generic;
using System.Linq;
using DAL.Repositories.BanHang;
using ET.DTOs;
using ET.Models.BanHang;
using ET.Models.DanhMuc;
using ET.Results;

namespace BUS.Services.BanHang
{
    public class BUS_MenuPOS
    {
        public static BUS_MenuPOS Instance { get; } = new BUS_MenuPOS();

        #region Truy vấn dữ liệu

        public List<ET_DiemBanHang> LayDanhSachDiemBan() => DAL_MenuPOS.Instance.LayDanhSachDiemBan();

        public List<DTO_MenuPOSItem> LayMenuTheoDiemBan(int idDiemBan) => DAL_MenuPOS.Instance.LayMenuTheoDiemBan(idDiemBan);

        public List<ET_SanPham> LayKhoSanPham() => DAL_MenuPOS.Instance.LayKhoSanPham();

        #endregion

        #region Thêm / Sửa / Xoá

        public OperationResult ThemDiemBan(ET_DiemBanHang dto)
        {
            if (string.IsNullOrWhiteSpace(dto.MaDiemBan))
                return OperationResult.Fail("ERR_MENU_MA_RONG");

            if (string.IsNullOrWhiteSpace(dto.TenDiemBan))
                return OperationResult.Fail("ERR_MENU_TEN_RONG");

            if (DAL_MenuPOS.Instance.TrungMaDiemBan(dto.MaDiemBan))
                return OperationResult.Fail($"ERR_MENU_TRUNG_MA|{dto.MaDiemBan}");

            int newId = DAL_MenuPOS.Instance.ThemDiemBan(dto);
            return OperationResult.Ok("MSG_THEM_OK");
        }

        public OperationResult CapNhatDiemBan(ET_DiemBanHang dto)
        {
            if (string.IsNullOrWhiteSpace(dto.TenDiemBan))
                return OperationResult.Fail("ERR_MENU_TEN_RONG");

            DAL_MenuPOS.Instance.CapNhatDiemBan(dto);
            return OperationResult.Ok("MSG_LUU_OK");
        }

        public OperationResult XoaDiemBan(int id, string tenDiemBan)
        {
            // Không cho xoá khi đang có phiên thu ngân đang mở
            if (DAL_MenuPOS.Instance.CoBatKyPhienMo(id))
                return OperationResult.Fail($"ERR_MENU_PHIEN_DANG_MO|{tenDiemBan}");

            DAL_MenuPOS.Instance.XoaMem(id);
            return OperationResult.Ok("MSG_XOA_OK");
        }

        public OperationResult LuuMenu(int idDiemBan, List<DTO_MenuPOSItem> danhSach)
        {
            var entities = danhSach.Select(x => new ET_MenuPOS
            {
                IdDiemBan    = idDiemBan,
                IdSanPham    = x.IdSanPham,
                ThuTuHienThi = x.ThuTuHienThi,
                ConHoatDong  = x.ConHoatDong
            }).ToList();

            DAL_MenuPOS.Instance.LuuMenu(idDiemBan, entities);
            return OperationResult.Ok("MSG_LUU_OK");
        }

        #endregion
    }
}


