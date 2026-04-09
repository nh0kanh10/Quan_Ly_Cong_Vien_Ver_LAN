using System;
using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_ChuongTrai
    {
        private static BUS_ChuongTrai instance;
        public static BUS_ChuongTrai Instance
        {
            get
            {
                if (instance == null) instance = new BUS_ChuongTrai();
                return instance;
            }
        }

        public List<ET_ChuongTrai> LoadDS()
        {
            return DAL_ChuongTrai.Instance.LoadDS();
        }

        public List<ET_ChuongTrai> LoadTheoKhuVucThu(int idKhuVucThu)
        {
            return DAL_ChuongTrai.Instance.LoadDS().FindAll(x => x.IdKhuVucThu == idKhuVucThu);
        }

        public ResponseResult Them(ET_ChuongTrai et)
        {
            if (string.IsNullOrWhiteSpace(et.TenChuong))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Tên chuồng không được rỗng!" };

            if (et.IdKhuVucThu <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Vui lòng chọn khu vực thú!" };

            if (DAL_ChuongTrai.Instance.Them(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm chuồng trại." };
        }

        public ResponseResult Sua(ET_ChuongTrai et)
        {
            if (et.Id <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "ID không hợp lệ!" };

            if (DAL_ChuongTrai.Instance.Sua(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi cập nhật chuồng trại." };
        }

        public ResponseResult Xoa(int id)
        {
            if (DAL_ChuongTrai.Instance.Xoa(id))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa chuồng trại. Chuồng có thể đang có động vật!" };
        }
    }
}
