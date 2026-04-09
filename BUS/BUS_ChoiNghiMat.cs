using System;
using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_ChoiNghiMat
    {
        private static BUS_ChoiNghiMat instance;
        public static BUS_ChoiNghiMat Instance
        {
            get
            {
                if (instance == null) instance = new BUS_ChoiNghiMat();
                return instance;
            }
        }

        public List<ET_ChoiNghiMat> LoadDS()
        {
            return DAL_ChoiNghiMat.Instance.LoadDS();
        }

        public List<ET_ChoiNghiMat> LoadTheoKhuVucBien(int idKhuVucBien)
        {
            return DAL_ChoiNghiMat.Instance.LoadDS().FindAll(x => x.IdKhuVucBien == idKhuVucBien);
        }

        public ResponseResult Them(ET_ChoiNghiMat et)
        {
            if (string.IsNullOrWhiteSpace(et.TenChoi))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Tên chòi không được rỗng!" };

            if (et.IdKhuVucBien <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Vui lòng chọn khu vực biển!" };

            if (DAL_ChoiNghiMat.Instance.Them(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm chòi nghỉ mát." };
        }

        public ResponseResult Sua(ET_ChoiNghiMat et)
        {
            if (et.Id <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "ID không hợp lệ!" };

            if (DAL_ChoiNghiMat.Instance.Sua(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi cập nhật chòi nghỉ mát." };
        }

        public ResponseResult Xoa(int id)
        {
            if (DAL_ChoiNghiMat.Instance.Xoa(id))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa chòi nghỉ mát." };
        }
    }
}
