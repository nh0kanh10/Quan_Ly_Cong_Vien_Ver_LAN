using System;
using DAL;
using ET;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class BUS_DonViTinh : IBaseBUS<ET_DonViTinh>
    {
        private static BUS_DonViTinh instance;
        public static BUS_DonViTinh Instance
        {
            get
            {
                if (instance == null) instance = new BUS_DonViTinh();
                return instance;
            }
        }

        public List<ET_DonViTinh> LoadDS()
        {
            return DAL_DonViTinh.Instance.LoadDS().Where(x => !x.IsDeleted).ToList();
        }

        public ET_DonViTinh GetById(int id)
        {
            return LoadDS().FirstOrDefault(x => x.Id == id);
        }

        public ResponseResult Them(ET_DonViTinh et)
        {
            if (string.IsNullOrEmpty(et.Ten)) return ResponseResult.Error("Tên đơn vị không được để trống.");
            bool success = DAL_DonViTinh.Instance.Them(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm đơn vị tính.");
        }

        public ResponseResult Sua(ET_DonViTinh et)
        {
            bool success = DAL_DonViTinh.Instance.Sua(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật đơn vị tính.");
        }

        public ResponseResult Xoa(int id)
        {
            bool success = DAL_DonViTinh.Instance.Xoa(id);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa đơn vị tính.");
        }

        public List<ET_DonViTinh> TimKiem(string kw, string filter = "Tất cả")
        {
            var ds = LoadDS();
            if (string.IsNullOrEmpty(kw)) return ds;
            kw = kw.ToLower();
            return ds.Where(x => x.Ten.ToLower().Contains(kw)).ToList();
        }
    }
}
