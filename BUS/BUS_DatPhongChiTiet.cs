using System.Collections.Generic;
using ET;

namespace BUS
{
    public class BUS_DatPhongChiTiet
    {
        private readonly IDatPhongChiTietGateway _gateway;

        private static BUS_DatPhongChiTiet instance;
        public static BUS_DatPhongChiTiet Instance => instance ?? (instance = new BUS_DatPhongChiTiet());

        public BUS_DatPhongChiTiet() : this(new DefaultDatPhongChiTietGateway()) { }
        public BUS_DatPhongChiTiet(IDatPhongChiTietGateway gw) { _gateway = gw; }

        public List<ET_DatPhongChiTiet> LoadDS() => _gateway.LoadDS();
        public bool Them(ET_DatPhongChiTiet et) => _gateway.Them(et);
        public bool Sua(ET_DatPhongChiTiet et) => _gateway.Sua(et);
        public bool Xoa(int id) => _gateway.Xoa(id);
    }
}
