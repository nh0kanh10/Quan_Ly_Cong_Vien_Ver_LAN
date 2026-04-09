using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_ChiTietNhapKho
    {
        private static BUS_ChiTietNhapKho instance;
        public static BUS_ChiTietNhapKho Instance => instance ?? (instance = new BUS_ChiTietNhapKho());

        public List<ET_ChiTietNhapKho> LoadDS() => DAL_ChiTietNhapKho.Instance.LoadDS();
        public bool Them(ET_ChiTietNhapKho et) => DAL_ChiTietNhapKho.Instance.Them(et);
        public bool Sua(ET_ChiTietNhapKho et) => DAL_ChiTietNhapKho.Instance.Sua(et);
        public bool Xoa(int id) => DAL_ChiTietNhapKho.Instance.Xoa(id);
    }
}
