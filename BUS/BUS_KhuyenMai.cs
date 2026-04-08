using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_KhuyenMai
    {
        private static BUS_KhuyenMai instance;
        public static BUS_KhuyenMai Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUS_KhuyenMai();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        public List<KhuyenMai> LayKhuyenMaiHopLe()
        {
            return DAL_KhuyenMai.Instance.LayKhuyenMaiHopLe();
        }
    }
}
