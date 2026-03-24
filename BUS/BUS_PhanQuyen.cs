using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_PhanQuyen
    {
        private readonly IPhanQuyenGateway _gateway;

        private static BUS_PhanQuyen instance;
        public static BUS_PhanQuyen Instance
        {
            get
            {
                if (instance == null) instance = new BUS_PhanQuyen();
                return instance;
            }
        }

        public BUS_PhanQuyen() : this(new DefaultPhanQuyenGateway()) { }
        public BUS_PhanQuyen(IPhanQuyenGateway gw) { _gateway = gw; }

        public List<ET_PhanQuyen> LoadDS() => _gateway.LoadDS();

        public bool CapNhatQuyen(int idVaiTro, List<int> dsIdQuyen)
        {
            // Xóa cũ
            _gateway.XoaTheoVaiTro(idVaiTro);
            
            // Thêm mới
            foreach (int idQuyen in dsIdQuyen)
            {
                _gateway.Them(new ET_PhanQuyen { IdVaiTro = idVaiTro, IdQuyen = idQuyen });
            }
            
            // Xóa cache quyền ở BUS_QuyenHan (nếu có dùng cache)
            BUS_QuyenHan.Instance.ClearCache();
            
            return true;
        }

        public List<int> LayQuyenTheoVaiTro(int idVaiTro)
        {
            return LoadDS().Where(x => x.IdVaiTro == idVaiTro).Select(x => x.IdQuyen).ToList();
        }
    }
}
