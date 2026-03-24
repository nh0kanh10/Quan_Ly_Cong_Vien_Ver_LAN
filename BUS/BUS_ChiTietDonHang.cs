using System.Collections.Generic;
using ET;

namespace BUS
{
    public class BUS_ChiTietDonHang
    {
        private readonly IChiTietDonHangGateway _gateway;

        private static BUS_ChiTietDonHang instance;
        public static BUS_ChiTietDonHang Instance
        {
            get { return instance ?? (instance = new BUS_ChiTietDonHang()); }
        }

        public BUS_ChiTietDonHang() : this(new DefaultChiTietDonHangGateway()) { }
        public BUS_ChiTietDonHang(IChiTietDonHangGateway gw) { _gateway = gw; }

        public List<ET_ChiTietDonHang> LoadByDonHang(int idDonHang) => _gateway.LoadByDonHang(idDonHang);
    }
}
