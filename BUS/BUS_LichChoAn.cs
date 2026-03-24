using System;
using System.Collections.Generic;
using ET;

namespace BUS
{
    public class BUS_LichChoAn
    {
        private readonly ILichChoAnGateway _gateway;

        private static BUS_LichChoAn instance;
        public static BUS_LichChoAn Instance
        {
            get
            {
                if (instance == null) instance = new BUS_LichChoAn();
                return instance;
            }
        }

        public BUS_LichChoAn() : this(new DefaultLichChoAnGateway()) { }
        public BUS_LichChoAn(ILichChoAnGateway gw) { _gateway = gw; }

        public List<ET_LichChoAn> LoadDS() => _gateway.LoadDS();

        public List<ET_LichChoAn> LoadTheoDongVat(int idDongVat) => _gateway.LoadTheoDongVat(idDongVat);

        public ResponseResult Them(ET_LichChoAn et)
        {
            if (et.IdDongVat <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Chưa chọn động vật!" };

            if (string.IsNullOrWhiteSpace(et.ThucAn))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Vui lòng nhập thức ăn!" };

            if (_gateway.Them(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm lịch cho ăn." };
        }

        public ResponseResult Sua(ET_LichChoAn et)
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
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa lịch cho ăn." };
        }
    }
}
