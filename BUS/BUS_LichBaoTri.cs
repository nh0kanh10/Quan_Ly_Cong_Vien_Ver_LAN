using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_LichBaoTri
    {
        private static BUS_LichBaoTri instance;
        public static BUS_LichBaoTri Instance
        {
            get
            {
                if (instance == null) instance = new BUS_LichBaoTri();
                return instance;
            }
        }

        public List<ET_LichBaoTri> LoadDS() => DAL_LichBaoTri.Instance.LoadDS();
        public List<ET_LichBaoTri> LoadTheoThietBi(int idThietBi) => DAL_LichBaoTri.Instance.LoadTheoThietBi(idThietBi);
        public bool Them(ET_LichBaoTri et) => DAL_LichBaoTri.Instance.Them(et);
        public bool Sua(ET_LichBaoTri et) => DAL_LichBaoTri.Instance.Sua(et);
        public bool Xoa(int id) => DAL_LichBaoTri.Instance.Xoa(id);
        public bool HoanTat(int id) => DAL_LichBaoTri.Instance.HoanTat(id);
        public List<ET_LichBaoTri> LayQuaHan() => DAL_LichBaoTri.Instance.LayQuaHan();
    }
}
