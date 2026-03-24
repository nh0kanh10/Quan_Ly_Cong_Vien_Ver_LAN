using System.Collections.Generic;
using ET;

namespace BUS
{
    public class BUS_ChiTietDatPhong
    {
        private readonly IChiTietDatPhongGateway _gateway;

        private static BUS_ChiTietDatPhong instance;
        public static BUS_ChiTietDatPhong Instance => instance ?? (instance = new BUS_ChiTietDatPhong());

        public BUS_ChiTietDatPhong() : this(new DefaultChiTietDatPhongGateway()) { }
        public BUS_ChiTietDatPhong(IChiTietDatPhongGateway gw) { _gateway = gw; }

        public List<ET_ChiTietDatPhong> LoadDS() => _gateway.LoadDS();
        public bool Them(ET_ChiTietDatPhong et) => _gateway.Them(et);
        public bool Sua(ET_ChiTietDatPhong et) => _gateway.Sua(et);
        public bool Xoa(int id) => _gateway.Xoa(id);
    }
}
