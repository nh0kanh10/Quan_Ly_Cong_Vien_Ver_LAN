using System;
using System.Collections.Generic;
using ET;

namespace BUS
{
    public class BUS_ChatLuongNuoc
    {
        private readonly IChatLuongNuocGateway _gateway;

        private static BUS_ChatLuongNuoc instance;
        public static BUS_ChatLuongNuoc Instance
        {
            get
            {
                if (instance == null) instance = new BUS_ChatLuongNuoc();
                return instance;
            }
        }

        public BUS_ChatLuongNuoc() : this(new DefaultChatLuongNuocGateway()) { }
        public BUS_ChatLuongNuoc(IChatLuongNuocGateway gw) { _gateway = gw; }

        public List<ET_ChatLuongNuoc> LoadDS() => _gateway.LoadDS();

        public List<ET_ChatLuongNuoc> LoadTheoKhuVucBien(int idKhuVucBien)
        {
            return _gateway.LoadDS().FindAll(x => x.IdKhuVucBien == idKhuVucBien);
        }

        public ResponseResult Them(ET_ChatLuongNuoc et)
        {
            if (et.IdKhuVucBien <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Vui lòng chọn khu vực biển!" };

            if (_gateway.Them(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm chất lượng nước." };
        }

        public ResponseResult Sua(ET_ChatLuongNuoc et)
        {
            if (et.Id <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "ID không hợp lệ!" };

            if (_gateway.Sua(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi cập nhật." };
        }

        public ResponseResult Xoa(int id)
        {
            if (_gateway.Xoa(id))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa." };
        }
    }
}
