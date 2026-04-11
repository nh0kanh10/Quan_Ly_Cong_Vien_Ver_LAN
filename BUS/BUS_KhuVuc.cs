using ET;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BUS
{
    public class BUS_KhuVuc : IBaseBUS<ET_KhuVuc>
    {
        private readonly IKhuVucGateway _gateway;
        private readonly ITroChoiGateway _troChoiGateway;

        private static BUS_KhuVuc instance;
        public static BUS_KhuVuc Instance
        {
            get
            {
                if (instance == null) instance = new BUS_KhuVuc();
                return instance;
            }
        }

        public BUS_KhuVuc() : this(new DefaultKhuVucGateway(), new DefaultTroChoiGateway()) { }
        public BUS_KhuVuc(IKhuVucGateway gw, ITroChoiGateway troChoiGw) 
        { 
            _gateway = gw; 
            _troChoiGateway = troChoiGw;
        }

        public List<ET_KhuVuc> LoadDS()
        {
            return _gateway.LoadDS().Where(x => !x.IsDeleted).ToList();
        }

        public ResponseResult Them(ET_KhuVuc et)
        {
            return ThemKhuVuc(et);
        }

        public ResponseResult Sua(ET_KhuVuc et)
        {
            return SuaKhuVuc(et);
        }

        public ResponseResult Xoa(int id)
        {
            var obj = _gateway.LoadDS().FirstOrDefault(x => x.Id == id);
            if (obj == null) return ResponseResult.Error("Không tìm thấy khu vực.");
            return XoaKhuVuc(obj.MaCode);
        }

        public List<ET_KhuVuc> TimKiem(string kw, string filter = "Tất cả")
        {
            return TimKiemNangCao(kw, filter);
        }

        public ET_KhuVuc GetById(int id)
        {
            return _gateway.LayTheoId(id);
        }

        public ResponseResult ThemKhuVuc(ET_KhuVuc et)
        {
            if (string.IsNullOrEmpty(et.MaCode)) et.MaCode = LayMaCodeTiepTheo();
            et.CreatedAt = DateTime.Now;

            bool success = _gateway.Them(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm khu vực vào CSDL.");
        }

        public ResponseResult SuaKhuVuc(ET_KhuVuc et)
        {
            et.UpdatedAt = DateTime.Now;
            bool success = _gateway.Sua(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật thông tin khu vực.");
        }

        public ResponseResult XoaKhuVuc(string code)
        {
            var existing = _gateway.LoadDS().FirstOrDefault(x => x.MaCode == code);
            if (existing == null) return ResponseResult.Error("Không tìm thấy khu vực.");
            
            // [Bugfix SD-001]: Ngăn chặn xóa khu vực đang chứa trò chơi
            var danhSachTroChoi = _troChoiGateway.LoadDS();
            if (danhSachTroChoi.Any(t => t.IdKhuVuc == existing.Id))
            {
                return ResponseResult.Error("Không thể xóa. Khu vực này đang chứa trò chơi trực thuộc.");
            }

            bool success = _gateway.Xoa(existing.Id);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa khu vực.");
        }

        public string LayMaCodeTiepTheo()
        {
            var ds = _gateway.LoadDS();
            int max = 0;
            foreach (var item in ds)
            {
                if (!string.IsNullOrEmpty(item.MaCode) && item.MaCode.StartsWith("KV"))
                {
                    int num;
                    if (int.TryParse(item.MaCode.Substring(2), out num) && num > max) max = num;
                }
            }
            return "KV" + (max + 1).ToString("D2");
        }

        public string ValidateKhuVuc(ET_KhuVuc et, bool isAdd)
        {
            if (string.IsNullOrWhiteSpace(et.TenKhuVuc)) return "Tên khu vực không được để trống.";
            if (et.TenKhuVuc.Length <= 2) return "Tên khu vực phải dài hơn 2 ký tự.";
            
            // [Bugfix SD-003]: Giới hạn độ dài Tên khu vực 100 ký tự, Mô tả 500 ký tự
            if (et.TenKhuVuc.Length > 100) return "Tên khu vực không được vượt quá 100 ký tự.";
            if (!string.IsNullOrEmpty(et.MoTa) && et.MoTa.Length > 500) return "Mô tả không được vượt quá 500 ký tự.";

            var ds = _gateway.LoadDS();
            if (isAdd)
            {
                if (ds.Any(x => x.TenKhuVuc.ToLower() == et.TenKhuVuc.ToLower())) return "Tên khu vực này đã tồn tại.";
                if (!string.IsNullOrEmpty(et.MaCode) && ds.Any(x => x.MaCode == et.MaCode)) return "Mã khu vực này đã tồn tại.";
            }
            else
            {
                if (ds.Any(x => x.TenKhuVuc.ToLower() == et.TenKhuVuc.ToLower() && x.Id != et.Id)) return "Tên khu vực bị trùng với khu vực khác.";
            }
            return string.Empty;
        }

        public List<ET_KhuVuc> TimKiemNangCao(string tuKhoa, string trangThai)
        {
            var ds = LoadDS();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                tuKhoa = tuKhoa.ToLower();
                ds = ds.Where(x => x.TenKhuVuc.ToLower().Contains(tuKhoa) || 
                                  (x.MaCode != null && x.MaCode.ToLower().Contains(tuKhoa))).ToList();
            }
            if (trangThai != "Tất cả")
            {
                ds = ds.Where(x => x.TrangThai == trangThai).ToList();
            }
            return ds;
        }
    }
}
