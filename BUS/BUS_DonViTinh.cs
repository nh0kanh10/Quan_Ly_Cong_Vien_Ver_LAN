using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_DonViTinh : IBaseBUS<ET_DonViTinh>
    {
        private readonly IDonViTinhGateway _gateway;

        private static BUS_DonViTinh instance;
        public static BUS_DonViTinh Instance
        {
            get
            {
                if (instance == null) instance = new BUS_DonViTinh();
                return instance;
            }
        }

        public BUS_DonViTinh() : this(new DefaultDonViTinhGateway()) { }
        public BUS_DonViTinh(IDonViTinhGateway gw) { _gateway = gw; }

        public List<ET_DonViTinh> LoadDS()
        {
            return _gateway.LoadDS().Where(x => !x.IsDeleted).ToList();
        }

        public ET_DonViTinh GetById(int id)
        {
            return LoadDS().FirstOrDefault(x => x.Id == id);
        }

        public ResponseResult Them(ET_DonViTinh et)
        {
            if (string.IsNullOrEmpty(et.Ten)) return ResponseResult.Error("Tên đơn vị không được để trống.");
            bool success = _gateway.Them(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm đơn vị tính.");
        }

        public ResponseResult Sua(ET_DonViTinh et)
        {
            bool success = _gateway.Sua(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật đơn vị tính.");
        }

        public ResponseResult Xoa(int id)
        {
            bool success = _gateway.Xoa(id);
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
