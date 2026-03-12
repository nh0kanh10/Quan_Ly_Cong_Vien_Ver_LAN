using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ET;
namespace BUS
{
    public class BUS_TroChoi
    {
        private static BUS_TroChoi instance;
        public static BUS_TroChoi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUS_TroChoi();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public List<ET_TroChoi> loadDS()
        {
            return DAL_TroChoi.Instance.loadDS();
        }
        public string layMaCodeTiepTheo()
        {
            return DAL_TroChoi.Instance.LayMaCodeTiepTheo();
        }
        public int layMaTroChoiLonNhat()
        {
            return DAL_TroChoi.Instance.LayMaTroChoiLonNhat();
        }

        public bool themTroChoi(ET_TroChoi et)
        {
            return DAL_TroChoi.Instance.ThemTroChoi(et);
        }

        public bool xoaTroChoi(string maCode)
        {
            return DAL_TroChoi.Instance.xoaTC(maCode);
        }
        public bool capNhatTroChoi(ET_TroChoi et)
        {
            return DAL_TroChoi.Instance.SuaTroChoi(et);
        }

        public string layMaKVTheoLoaiTC(string loai)
        {
            return DAL_TroChoi.Instance.layMaKVTheoLoaiTC(loai);
        }
    }
}
