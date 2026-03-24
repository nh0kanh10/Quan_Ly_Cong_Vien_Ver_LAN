using System.Collections.Generic;
using ET;

namespace BUS
{
    public class BUS_SuCo
    {
        private readonly ISuCoGateway _gateway;

        private static BUS_SuCo instance;
        public static BUS_SuCo Instance
        {
            get
            {
                if (instance == null) instance = new BUS_SuCo();
                return instance;
            }
        }

        public BUS_SuCo() : this(new DefaultSuCoGateway()) { }
        public BUS_SuCo(ISuCoGateway gw) { _gateway = gw; }

        public List<ET_SuCo> LoadDS() => _gateway.LoadDS();
        public bool Them(ET_SuCo et) => _gateway.Them(et);
        public bool Sua(ET_SuCo et) => _gateway.Sua(et);
        public bool Xoa(int id) => _gateway.Xoa(id);
    }
}
