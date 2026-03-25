using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_DanhMucDichVu
    {
            private static BUS_DanhMucDichVu instance;
            public static BUS_DanhMucDichVu Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new BUS_DanhMucDichVu();
                    }
                    return instance;
                }
                private set
                {
                    instance = value;
                }
            }
        public List<ET_DanhMucDV> loadDS()
        {
            return DAL_DanhMucDV.Instance.loadDS();
        }
    }
}
