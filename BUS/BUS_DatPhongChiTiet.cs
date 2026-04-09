using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_DatPhongChiTiet
    {
        private static BUS_DatPhongChiTiet instance;
        public static BUS_DatPhongChiTiet Instance => instance ?? (instance = new BUS_DatPhongChiTiet());

        public List<ET_DatPhongChiTiet> LoadDS() => DAL_DatPhongChiTiet.Instance.LoadDS();
        public bool Them(ET_DatPhongChiTiet et) => DAL_DatPhongChiTiet.Instance.Them(et);
        public bool Sua(ET_DatPhongChiTiet et) => DAL_DatPhongChiTiet.Instance.Sua(et);
        public bool Xoa(int id) => DAL_DatPhongChiTiet.Instance.Xoa(id);
    }
}
