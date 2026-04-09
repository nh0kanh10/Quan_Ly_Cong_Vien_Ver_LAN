using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_ThatLac
    {
        private static BUS_ThatLac instance;
        public static BUS_ThatLac Instance
        {
            get
            {
                if (instance == null) instance = new BUS_ThatLac();
                return instance;
            }
        }

        public List<ET_ThatLac> LoadDS()
        {
            return DAL_ThatLac.Instance.LoadDS();
        }

        public bool Them(ET_ThatLac et)
        {
            return DAL_ThatLac.Instance.Them(et);
        }

        public bool Sua(ET_ThatLac et)
        {
            return DAL_ThatLac.Instance.Sua(et);
        }

        public bool Xoa(int id)
        {
            return DAL_ThatLac.Instance.Xoa(id);
        }
    }
}
