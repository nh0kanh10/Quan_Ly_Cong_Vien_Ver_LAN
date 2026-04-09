using System.Collections.Generic;
using System.Linq;
using DAL;
using ET;

namespace BUS
{
    public class BUS_VaiTro
    {
        private static BUS_VaiTro instance;
        public static BUS_VaiTro Instance
        {
            get
            {
                if (instance == null) instance = new BUS_VaiTro();
                return instance;
            }
        }

        public List<ET_VaiTro> LoadDS()
        {
            // DAL chưa có filter IsDeleted => dùng toàn bộ.
            return DAL_VaiTro.Instance.LoadDS();
        }

        public ET_VaiTro LayTheoId(int id)
        {
            return DAL_VaiTro.Instance.LayTheoId(id);
        }

        public List<int> GetAllRoleIds()
        {
            return LoadDS().Select(x => x.Id).ToList();
        }

        public bool Them(ET_VaiTro et)
        {
            return DAL_VaiTro.Instance.Them(et);
        }

        public bool Sua(ET_VaiTro et)
        {
            return DAL_VaiTro.Instance.Sua(et);
        }

        public bool Xoa(int id)
        {
            return DAL_VaiTro.Instance.Xoa(id);
        }
    }
}

