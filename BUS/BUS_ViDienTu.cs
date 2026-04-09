using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_ViDienTu : IBaseBUS<ET_ViDienTu>
    {
        private readonly IViDienTuGateway _viGateway;
        private static BUS_ViDienTu instance;
        public static BUS_ViDienTu Instance
        {
            get { return instance ?? (instance = new BUS_ViDienTu()); }
        }
        public BUS_ViDienTu() : this(new DefaultViDienTuGateway()) { }
        public BUS_ViDienTu(IViDienTuGateway viGateway) { _viGateway = viGateway; }

        public List<ET_ViDienTu> LoadDS()
        {
            return _viGateway.LoadDS();
        }

        public ResponseResult Them(ET_ViDienTu et)
        {
            if (et == null) return ResponseResult.Error("Dữ liệu ví điện tử không hợp lệ.");
            bool ok = _viGateway.Them(et);
            return ok ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm ví điện tử.");
        }

        public ResponseResult Sua(ET_ViDienTu et)
        {
            if (et == null) return ResponseResult.Error("Dữ liệu ví điện tử không hợp lệ.");
            bool ok = _viGateway.Sua(et);
            return ok ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật ví điện tử.");
        }

        public ResponseResult Xoa(int id)
        {
            bool ok = _viGateway.Xoa(id);
            return ok ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa ví điện tử.");
        }

        public List<ET_ViDienTu> TimKiem(string kw, string filter)
        {
            var ds = LoadDS();
            if (string.IsNullOrWhiteSpace(kw)) return ds;
            kw = kw.ToLowerInvariant();
            return ds.Where(x => x.Id.ToString().Contains(kw) || x.IdKhachHang.ToString().Contains(kw)).ToList();
        }
    }
}
