using System.Collections.Generic;
using ET;

namespace BUS
{
    public class BUS_ChiTietXuatKho
    {
        private readonly IChiTietXuatKhoGateway _gateway;

        private static BUS_ChiTietXuatKho instance;
        public static BUS_ChiTietXuatKho Instance => instance ?? (instance = new BUS_ChiTietXuatKho());

        public BUS_ChiTietXuatKho() : this(new DefaultChiTietXuatKhoGateway()) { }
        public BUS_ChiTietXuatKho(IChiTietXuatKhoGateway gw) { _gateway = gw; }

        public List<ET_ChiTietXuatKho> LoadDS() => _gateway.LoadDS();
        public bool Them(ET_ChiTietXuatKho et) => _gateway.Them(et);
        public bool Sua(ET_ChiTietXuatKho et) => _gateway.Sua(et);
        public bool Xoa(int id) => _gateway.Xoa(id);
    }
}
