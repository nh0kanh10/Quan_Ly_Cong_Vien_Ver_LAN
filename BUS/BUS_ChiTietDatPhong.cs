using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_ChiTietDatPhong
    {
        private static BUS_ChiTietDatPhong instance;
        public static BUS_ChiTietDatPhong Instance => instance ?? (instance = new BUS_ChiTietDatPhong());

        public List<ET_ChiTietDatPhong> LoadDS() => DAL_ChiTietDatPhong.Instance.LoadDS();
        public bool Them(ET_ChiTietDatPhong et) => DAL_ChiTietDatPhong.Instance.Them(et);
        public bool Sua(ET_ChiTietDatPhong et) => DAL_ChiTietDatPhong.Instance.Sua(et);
        public bool Xoa(int id) => DAL_ChiTietDatPhong.Instance.Xoa(id);
    }
}
