using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_KhuVucThu : IBaseBUS<ET_KhuVucThu>
    {
        private readonly IKhuVucThuGateway _gateway;

        private static BUS_KhuVucThu instance;
        public static BUS_KhuVucThu Instance
        {
            get
            {
                if (instance == null) instance = new BUS_KhuVucThu();
                return instance;
            }
        }

        public BUS_KhuVucThu() : this(new DefaultKhuVucThuGateway()) { }
        public BUS_KhuVucThu(IKhuVucThuGateway gw) { _gateway = gw; }

        public List<ET_KhuVucThu> LoadDS() => _gateway.LoadDS();

        public ResponseResult Them(ET_KhuVucThu et)
        {
            if (string.IsNullOrWhiteSpace(et.TenKhuVuc))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Tên khu vực thú không được rỗng!" };

            if (string.IsNullOrWhiteSpace(et.MaCode))
                return new ResponseResult { IsSuccess = false, ErrorMessage = "Mã khu vực không được rỗng!" };

            if (_gateway.Them(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm khu vực thú." };
        }

        public ResponseResult Sua(ET_KhuVucThu et)
        {
            if (et.Id <= 0)
                return new ResponseResult { IsSuccess = false, ErrorMessage = "ID không hợp lệ!" };

            if (_gateway.Sua(et))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi cập nhật thông tin." };
        }

        public ResponseResult Xoa(int id)
        {
            if (_gateway.Xoa(id))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa. Khu vực này có thể đang chứa chuồng trại!" };
        }

        public List<ET_KhuVucThu> TimKiemNangCao(string keyword) => _gateway.TimKiem(keyword);

        public List<ET_KhuVucThu> TimKiem(string kw, string filter) => TimKiemNangCao(kw);
    }
}
