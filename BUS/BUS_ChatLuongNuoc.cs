using System;
using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_ChatLuongNuoc
    {
        private static BUS_ChatLuongNuoc instance;
        public static BUS_ChatLuongNuoc Instance
        {
            get
            {
                if (instance == null) instance = new BUS_ChatLuongNuoc();
                return instance;
            }
        }

        public List<ET_ChatLuongNuoc> LoadDS()
        {
            return DAL_ChatLuongNuoc.Instance.LoadDS();
        }

        public List<ET_ChatLuongNuoc> LoadTheoKhuVucBien(int idKhuVucBien)
        {
            return DAL_ChatLuongNuoc.Instance.LoadDS().FindAll(x => x.IdKhuVucBien == idKhuVucBien);
        }

        public ResponseResult Them(ET_ChatLuongNuoc et)
        {
            if (et.IdKhuVucBien <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Vui lòng chọn khu vực biển!" };

            if (DAL_ChatLuongNuoc.Instance.Them(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm chất lượng nước." };
        }

        public ResponseResult Sua(ET_ChatLuongNuoc et)
        {
            if (et.Id <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "ID không hợp lệ!" };

            if (DAL_ChatLuongNuoc.Instance.Sua(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi cập nhật." };
        }

        public ResponseResult Xoa(int id)
        {
            if (DAL_ChatLuongNuoc.Instance.Xoa(id))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa." };
        }
    }
}
