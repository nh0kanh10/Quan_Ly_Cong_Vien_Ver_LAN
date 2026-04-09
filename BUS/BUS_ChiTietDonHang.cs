using System;
using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_ChiTietDonHang
    {
        private static BUS_ChiTietDonHang instance;
        public static BUS_ChiTietDonHang Instance
        {
            get { return instance ?? (instance = new BUS_ChiTietDonHang()); }
        }

        public List<ET_ChiTietDonHang> LoadByDonHang(int idDonHang)
        {
            return DAL_ChiTietDonHang.Instance.LoadByDonHang(idDonHang);
        }
    }
}
