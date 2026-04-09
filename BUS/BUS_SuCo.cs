using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_SuCo
    {
        private static BUS_SuCo instance;
        public static BUS_SuCo Instance
        {
            get
            {
                if (instance == null) instance = new BUS_SuCo();
                return instance;
            }
        }

        public List<ET_SuCo> LoadDS()
        {
            return DAL_SuCo.Instance.LoadDS();
        }

        public bool Them(ET_SuCo et)
        {
            return DAL_SuCo.Instance.Them(et);
        }

        public bool Sua(ET_SuCo et)
        {
            return DAL_SuCo.Instance.Sua(et);
        }

        public bool Xoa(int id)
        {
            return DAL_SuCo.Instance.Xoa(id);
        }
    }
}
