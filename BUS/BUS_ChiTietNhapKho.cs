using System.Collections.Generic;
using ET;

namespace BUS
{
    public class BUS_ChiTietNhapKho
    {
        private readonly IChiTietNhapKhoGateway _gateway;

        private static BUS_ChiTietNhapKho instance;
        public static BUS_ChiTietNhapKho Instance => instance ?? (instance = new BUS_ChiTietNhapKho());

        public BUS_ChiTietNhapKho() : this(new DefaultChiTietNhapKhoGateway()) { }
        public BUS_ChiTietNhapKho(IChiTietNhapKhoGateway gw) { _gateway = gw; }

        public List<ET_ChiTietNhapKho> LoadDS() => _gateway.LoadDS();
        public bool Them(ET_ChiTietNhapKho et) => _gateway.Them(et);
        public bool Sua(ET_ChiTietNhapKho et) => _gateway.Sua(et);
        public bool Xoa(int id) => _gateway.Xoa(id);
    }
}
