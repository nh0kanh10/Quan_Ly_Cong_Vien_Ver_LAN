using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_ChiTietXuatKho
    {
        private static BUS_ChiTietXuatKho instance;
        public static BUS_ChiTietXuatKho Instance => instance ?? (instance = new BUS_ChiTietXuatKho());

        public List<ET_ChiTietXuatKho> LoadDS() => DAL_ChiTietXuatKho.Instance.LoadDS();
        public bool Them(ET_ChiTietXuatKho et) => DAL_ChiTietXuatKho.Instance.Them(et);
        public bool Sua(ET_ChiTietXuatKho et) => DAL_ChiTietXuatKho.Instance.Sua(et);
        public bool Xoa(int id) => DAL_ChiTietXuatKho.Instance.Xoa(id);
    }
}
