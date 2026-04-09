using System;
using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_KhuVucBien : IBaseBUS<ET_KhuVucBien>
    {
        private static BUS_KhuVucBien instance;
        public static BUS_KhuVucBien Instance
        {
            get
            {
                if (instance == null) instance = new BUS_KhuVucBien();
                return instance;
            }
        }

        public List<ET_KhuVucBien> LoadDS()
        {
            return DAL_KhuVucBien.Instance.LoadDS();
        }

        public ResponseResult Them(ET_KhuVucBien et)
        {
            if (string.IsNullOrWhiteSpace(et.TenKhuVuc))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Tên vùng biển không được rỗng!" };

            if (string.IsNullOrWhiteSpace(et.MaCode))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Mã khu vực không được rỗng!" };

            if (et.DoSauToiDa < 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Độ sâu không được nhỏ hơn 0m!" };

            if (DAL_KhuVucBien.Instance.Them(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm vùng biển." };
        }

        public ResponseResult Sua(ET_KhuVucBien et)
        {
            if (et.Id <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "ID không hợp lệ!" };

            if (et.DoSauToiDa < 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Độ sâu tối đa không được âm!" };

            if (DAL_KhuVucBien.Instance.Sua(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi cập nhật thông tin biển." };
        }

        public ResponseResult Xoa(int id)
        {
            if (DAL_KhuVucBien.Instance.Xoa(id))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa. Bãi biển này có thể đang liên kết với chòi cứu hộ / nghỉ mát!" };
        }

        public List<ET_KhuVucBien> TimKiemNangCao(string keyword)
        {
            return DAL_KhuVucBien.Instance.TimKiem(keyword);
        }

        public List<ET_KhuVucBien> TimKiem(string kw, string filter)
        {
            return TimKiemNangCao(kw);
        }
    }
}
