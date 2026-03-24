using System;
using ET;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class BUS_SuKien
    {
        private readonly ISuKienGateway _gateway;

        private static BUS_SuKien instance;
        public static BUS_SuKien Instance
        {
            get
            {
                if (instance == null) instance = new BUS_SuKien();
                return instance;
            }
        }

        public BUS_SuKien() : this(new DefaultSuKienGateway()) { }
        public BUS_SuKien(ISuKienGateway gw) { _gateway = gw; }

        public List<ET_SuKien> LoadDS()
        {
            return _gateway.LoadDS().Where(x => !x.IsDeleted).ToList();
        }

        public ResponseResult ThemSuKien(ET_SuKien et)
        {
            if (string.IsNullOrEmpty(et.MaCode)) et.MaCode = "SK" + DateTime.Now.ToString("yyyyMMddHHmmss");
            et.CreatedAt = DateTime.Now;
            et.CreatedBy = (SessionManager.CurrentUser != null) ? (int?)SessionManager.CurrentUser.Id : null;

            bool success = _gateway.Them(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm sự kiện.");
        }

        public ResponseResult SuaSuKien(ET_SuKien et)
        {
            bool success = _gateway.Sua(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật sự kiện.");
        }

        public ResponseResult XoaSuKien(int id)
        {
            bool success = _gateway.Xoa(id);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa sự kiện.");
        }
    }
}
