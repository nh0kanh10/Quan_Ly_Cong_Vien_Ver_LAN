using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_KhachHang
    {
        private static BUS_KhachHang instance;
        public static BUS_KhachHang Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUS_KhachHang();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        public List<ET_KHACHHANG> LayDSKhachHang()
        {
            return DAL_KhachHang.Instance.loadDS();
        }

        public List<ET_KHACHHANG> timKhachHang(string key)
        {
            return DAL_KhachHang.Instance.TimKhachHang(key);
        }
    }
}
