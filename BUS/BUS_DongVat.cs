using System;
using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_DongVat
    {
        private static BUS_DongVat instance;
        public static BUS_DongVat Instance
        {
            get
            {
                if (instance == null) instance = new BUS_DongVat();
                return instance;
            }
        }

        public List<ET_DongVat> LoadDS()
        {
            return DAL_DongVat.Instance.LoadDS();
        }

        public ResponseResult Them(ET_DongVat et)
        {
            if (string.IsNullOrWhiteSpace(et.Ten))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Tên động vật không được rỗng!" };

            if (string.IsNullOrWhiteSpace(et.Loai))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Loài không được rỗng!" };

            if (DAL_DongVat.Instance.Them(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm động vật." };
        }

        public ResponseResult Sua(ET_DongVat et)
        {
            if (et.Id <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "ID không hợp lệ!" };

            if (DAL_DongVat.Instance.Sua(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi cập nhật." };
        }

        public ResponseResult Xoa(int id)
        {
            if (DAL_DongVat.Instance.Xoa(id))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa. Động vật có thể đang gán vào chuồng!" };
        }

        public List<ET_DongVat> TimKiem(string keyword)
        {
            var ds = LoadDS();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                ds = ds.FindAll(x => (x.Ten != null && x.Ten.ToLower().Contains(keyword))
                                  || (x.Loai != null && x.Loai.ToLower().Contains(keyword)));
            }
            return ds;
        }
    }
}
