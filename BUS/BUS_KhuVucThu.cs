using System;
using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_KhuVucThu : IBaseBUS<ET_KhuVucThu>
    {
        private static BUS_KhuVucThu instance;
        public static BUS_KhuVucThu Instance
        {
            get
            {
                if (instance == null) instance = new BUS_KhuVucThu();
                return instance;
            }
        }

        public List<ET_KhuVucThu> LoadDS()
        {
            return DAL_KhuVucThu.Instance.LoadDS();
        }

        public ResponseResult Them(ET_KhuVucThu et)
        {
            if (string.IsNullOrWhiteSpace(et.TenKhuVuc))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Tên khu vực thú không được rỗng!" };

            if (string.IsNullOrWhiteSpace(et.MaCode))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Mã khu vực không được rỗng!" };

            if (DAL_KhuVucThu.Instance.Them(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm khu vực thú." };
        }

        public ResponseResult Sua(ET_KhuVucThu et)
        {
            if (et.Id <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "ID không hợp lệ!" };

            if (DAL_KhuVucThu.Instance.Sua(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi cập nhật thông tin." };
        }

        public ResponseResult Xoa(int id)
        {
            if (DAL_KhuVucThu.Instance.Xoa(id))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa. Khu vực này có thể đang chứa chuồng trại!" };
        }

        public List<ET_KhuVucThu> TimKiemNangCao(string keyword)
        {
            return DAL_KhuVucThu.Instance.TimKiem(keyword);
        }

        public List<ET_KhuVucThu> TimKiem(string kw, string filter)
        {
            return TimKiemNangCao(kw);
        }
    }
}
