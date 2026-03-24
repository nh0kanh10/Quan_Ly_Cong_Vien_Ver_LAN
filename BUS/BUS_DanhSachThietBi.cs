using System.Collections.Generic;
using ET;

namespace BUS
{
    public class BUS_DanhSachThietBi
    {
        private readonly IDanhSachThietBiGateway _gateway;

        private static BUS_DanhSachThietBi instance;
        public static BUS_DanhSachThietBi Instance
        {
            get
            {
                if (instance == null) instance = new BUS_DanhSachThietBi();
                return instance;
            }
        }

        public BUS_DanhSachThietBi() : this(new DefaultDanhSachThietBiGateway()) { }
        public BUS_DanhSachThietBi(IDanhSachThietBiGateway gw) { _gateway = gw; }

        public List<ET_DanhSachThietBi> LoadDS() => _gateway.LoadDS();
        public List<ET_DanhSachThietBi> LoadDSTheoLoai(string loai) => _gateway.LoadDSTheoLoai(loai);
        public bool Them(ET_DanhSachThietBi et) => _gateway.Them(et);
        public bool Sua(ET_DanhSachThietBi et) => _gateway.Sua(et);
        public bool Xoa(int id) => _gateway.Xoa(id);
        public bool DoiTrangThai(int id, string tt) => _gateway.DoiTrangThai(id, tt);
        public List<ET_DanhSachThietBi> TimKiem(string kw, string loai, string tt) => _gateway.TimKiem(kw, loai, tt);
    }
}
