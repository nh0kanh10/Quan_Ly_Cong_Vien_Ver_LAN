using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_VaiTro
    {
        private readonly IVaiTroGateway _gateway;

        private static BUS_VaiTro instance;
        public static BUS_VaiTro Instance
        {
            get
            {
                if (instance == null) instance = new BUS_VaiTro();
                return instance;
            }
        }

        public BUS_VaiTro() : this(new DefaultVaiTroGateway()) { }
        public BUS_VaiTro(IVaiTroGateway gw) { _gateway = gw; }

        public List<ET_VaiTro> LoadDS()
        {
            // DAL chưa có filter IsDeleted => dùng toàn bộ.
            return _gateway.LoadDS();
        }

        public ET_VaiTro LayTheoId(int id) => _gateway.LayTheoId(id);

        public List<int> GetAllRoleIds()
        {
            return LoadDS().Select(x => x.Id).ToList();
        }

        public bool Them(ET_VaiTro et) => _gateway.Them(et);
        public bool Sua(ET_VaiTro et) => _gateway.Sua(et);
        public bool Xoa(int id) => _gateway.Xoa(id);
    }
}
