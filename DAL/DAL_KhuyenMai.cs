using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_KhuyenMai
    {
        private static DAL_KhuyenMai instance;
        public static DAL_KhuyenMai Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_KhuyenMai();
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
            using (QLKVCGTDataContext db = new QLKVCGTDataContext())
            {
                DateTime now = DateTime.Now;

                return db.KhuyenMais
                    .Where(km => km.TrangThai == true
                              && km.NgayBatDau <= now
                              && km.NgayKetThuc >= now)
                    .ToList();
            }
        }
    }
}
