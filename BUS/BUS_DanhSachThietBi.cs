using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_DanhSachThietBi
    {
        private static BUS_DanhSachThietBi instance;
        public static BUS_DanhSachThietBi Instance
        {
            get
            {
                if (instance == null) instance = new BUS_DanhSachThietBi();
                return instance;
            }
        }

        public List<ET_DanhSachThietBi> LoadDS() => DAL_DanhSachThietBi.Instance.LoadDS();
        public List<ET_DanhSachThietBi> LoadDSTheoLoai(string loai) => DAL_DanhSachThietBi.Instance.LoadDSTheoLoai(loai);
        public bool Them(ET_DanhSachThietBi et) => DAL_DanhSachThietBi.Instance.Them(et);
        public bool Sua(ET_DanhSachThietBi et) => DAL_DanhSachThietBi.Instance.Sua(et);
        public bool Xoa(int id) => DAL_DanhSachThietBi.Instance.Xoa(id);
        public bool DoiTrangThai(int id, string tt) => DAL_DanhSachThietBi.Instance.DoiTrangThai(id, tt);
        public List<ET_DanhSachThietBi> TimKiem(string kw, string loai, string tt) => DAL_DanhSachThietBi.Instance.TimKiem(kw, loai, tt);
    }
}
