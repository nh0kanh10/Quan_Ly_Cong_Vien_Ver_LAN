using System.Collections.Generic;
using ET;

namespace BUS
{
    public class BUS_ThatLac
    {
        private readonly IThatLacGateway _gateway;

        private static BUS_ThatLac instance;
        public static BUS_ThatLac Instance
        {
            get
            {
                if (instance == null) instance = new BUS_ThatLac();
                return instance;
            }
        }

        public BUS_ThatLac() : this(new DefaultThatLacGateway()) { }
        public BUS_ThatLac(IThatLacGateway gw) { _gateway = gw; }

        public List<ET_ThatLac> LoadDS() => _gateway.LoadDS();
        public bool Them(ET_ThatLac et) => _gateway.Them(et);
        public bool Sua(ET_ThatLac et) => _gateway.Sua(et);
        public bool Xoa(int id) => _gateway.Xoa(id);
    }
}
