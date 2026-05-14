using DAL.Repositories.HeThong;

namespace BUS.Services.BanHang
{
    public class BUS_CauHinh
    {
        public static BUS_CauHinh Instance { get; } = new BUS_CauHinh();

        public decimal LayGiaTriDecimal(string key, decimal defaultValue)
        {
            return DAL_CauHinh.Instance.LayGiaTriDecimal(key, defaultValue);
        }

        public string LayGiaTri(string key)
        {
            return DAL_CauHinh.Instance.LayGiaTri(key);
        }
    }
}


