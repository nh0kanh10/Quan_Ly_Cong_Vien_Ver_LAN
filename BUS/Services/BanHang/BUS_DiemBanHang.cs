using System;
using DAL.Repositories.BanHang;

namespace BUS.Services.BanHang
{
    public class BUS_DiemBanHang
    {
        public static BUS_DiemBanHang Instance { get; } = new BUS_DiemBanHang();

        public object GetAllDiemBanHangHoatDong()
        {
            return DAL.Repositories.BanHang.DAL_DiemBanHang.Instance.GetAllDiemBanHangHoatDong();
        }

        public int? GetIdDiemBanByMa(string maDiemBan)
        {
            return DAL.Repositories.BanHang.DAL_DiemBanHang.Instance.GetIdDiemBanByMa(maDiemBan);
        }
    }
}


