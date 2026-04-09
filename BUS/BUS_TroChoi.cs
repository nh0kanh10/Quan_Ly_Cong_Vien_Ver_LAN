using System;
using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_TroChoi : IBaseBUS<ET_TroChoi>
    {
        private static BUS_TroChoi instance;
        public static BUS_TroChoi Instance
        {
            get
            {
                if (instance == null) instance = new BUS_TroChoi();
                return instance;
            }
        }

        public List<ET_TroChoi> LoadDS()
        {
            return DAL_TroChoi.Instance.LoadDS();
        }

        public ResponseResult Them(ET_TroChoi et)
        {
            if (string.IsNullOrWhiteSpace(et.TenTroChoi))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Tên trò chơi không được rỗng!" };

            if (string.IsNullOrWhiteSpace(et.MaCode))
                et.MaCode = "TC-" + DateTime.Now.ToString("yyyyMMddHHmmss");

            if (DAL_TroChoi.Instance.Them(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm trò chơi vào CSDL." };
        }

        public ResponseResult Sua(ET_TroChoi et)
        {
            if (et.Id <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "ID không hợp lệ!" };

            if (DAL_TroChoi.Instance.Sua(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi cập nhật thông tin." };
        }

        public ResponseResult Xoa(int id)
        {
            if (DAL_TroChoi.Instance.Xoa(id))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa trò chơi!" };
        }
        
        public List<ET_TroChoi> TimKiemNangCao(string keyword, string idKhuVuc)
        {
            return DAL_TroChoi.Instance.TimKiem(keyword, idKhuVuc);
        }

        public List<ET_TroChoi> TimKiem(string kw, string filter)
        {
            return TimKiemNangCao(kw, filter);
        }
    }
}
