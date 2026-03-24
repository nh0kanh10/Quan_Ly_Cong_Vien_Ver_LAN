using System.Collections.Generic;
using ET;

namespace BUS
{
    public class BUS_LichBaoTri
    {
        private readonly ILichBaoTriGateway _gateway;

        private static BUS_LichBaoTri instance;
        public static BUS_LichBaoTri Instance
        {
            get
            {
                if (instance == null) instance = new BUS_LichBaoTri();
                return instance;
            }
        }

        public BUS_LichBaoTri() : this(new DefaultLichBaoTriGateway()) { }
        public BUS_LichBaoTri(ILichBaoTriGateway gw) { _gateway = gw; }

        public List<ET_LichBaoTri> LoadDS() => _gateway.LoadDS();
        public List<ET_LichBaoTri> LoadTheoThietBi(int idThietBi) => _gateway.LoadTheoThietBi(idThietBi);
        public bool Them(ET_LichBaoTri et) => _gateway.Them(et);
        public bool Sua(ET_LichBaoTri et) => _gateway.Sua(et);
        public bool Xoa(int id) => _gateway.Xoa(id);
        public bool HoanTat(int id) => _gateway.HoanTat(id);
        public List<ET_LichBaoTri> LayQuaHan() => _gateway.LayQuaHan();
    }
}
