using System.Collections.Generic;
using System.Linq;
using DAL;
using ET;

namespace BUS
{
    public class BUS_PhanQuyen
    {
        private static BUS_PhanQuyen instance;
        public static BUS_PhanQuyen Instance
        {
            get
            {
                if (instance == null) instance = new BUS_PhanQuyen();
                return instance;
            }
        }

        public List<ET_PhanQuyen> LoadDS()
        {
            return DAL_PhanQuyen.Instance.LoadDS();
        }

        public bool CapNhatQuyen(int idVaiTro, List<int> dsIdQuyen)
        {
            // Xóa cũ
            DAL_PhanQuyen.Instance.XoaTheoVaiTro(idVaiTro);
            
            // Thêm mới
            foreach (int idQuyen in dsIdQuyen)
            {
                DAL_PhanQuyen.Instance.Them(new ET_PhanQuyen { IdVaiTro = idVaiTro, IdQuyen = idQuyen });
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
