using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_DanhMucDV
    {
        private static DAL_DanhMucDV instance;
        public static DAL_DanhMucDV Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_DanhMucDV();
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
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                var ds = from dv in db.DanhMucDichVus
                         select new ET_DanhMucDV
                         {
                             MaDanhMuc = dv.MaDanhMuc,
                             TenDanhMuc = dv.TenDanhMuc,
                             KieuLogic = dv.KieuLogic,
                             MoTa = dv.MoTa,
                             Icon = dv.Icon
                         };
                return ds.ToList();
            }
        }
    }
}
