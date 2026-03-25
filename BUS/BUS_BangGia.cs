using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_BangGia
    {
        private static BUS_BangGia instance;
        public static BUS_BangGia Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUS_BangGia();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public List<ET_BangGia> loadDS(int maDichVu)
        {
            return DAL_BangGia.Instance.loadDS().Where(bg => bg.MaDichVu == maDichVu)
                             .ToList();
        }
        public int layBangGiaLonNhat()
        {
            return DAL_BangGia.Instance.LayMaBangGiaLonNhat();
        }

        public bool themBangGia(ET_BangGia et)
        {
            return DAL_BangGia.Instance.Them(et);
        }
        public bool xoaBangGia(int ma)
        {
            return DAL_BangGia.Instance.Xoa(ma);
        }

        public bool suaBangGia(ET_BangGia et)
        {
            return DAL_BangGia.Instance.CapNhat(et);
        }

        public int layMaBangGiaTiepTheo()
        {
            return DAL_BangGia.Instance.LayMaBangGiaLonNhat();
        }
    }
}
