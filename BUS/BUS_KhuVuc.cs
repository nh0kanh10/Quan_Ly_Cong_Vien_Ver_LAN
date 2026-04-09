using DAL;
using ET;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BUS
{
    public class BUS_KhuVuc : IBaseBUS<ET_KhuVuc>
    {
        private static BUS_KhuVuc instance;
        public static BUS_KhuVuc Instance
        {
            get
            {
                if (instance == null) instance = new BUS_KhuVuc();
                return instance;
            }
        }

        public List<ET_KhuVuc> LoadDS()
        {
            return DAL_KhuVuc.Instance.LoadDS().Where(x => !x.IsDeleted).ToList();
        }

        public ResponseResult Them(ET_KhuVuc et)
        {
            var res = ThemKhuVuc(et);
            return res;
        }

        public ResponseResult Sua(ET_KhuVuc et)
        {
            return SuaKhuVuc(et);
        }

        public ResponseResult Xoa(int id)
        {
            // The existing XoaKhuVuc takes a string code. 
            // We should ideally have an Id-based delete or find the code first.
            var obj = DAL_KhuVuc.Instance.LoadDS().FirstOrDefault(x => x.Id == id);
            if (obj == null) return ResponseResult.Error("Không tìm thấy khu vực.");
            return XoaKhuVuc(obj.MaCode);
        }

        public List<ET_KhuVuc> TimKiem(string kw, string filter = "Tất cả")
        {
            return TimKiemNangCao(kw, filter);
        }

        public ET_KhuVuc GetById(int id)
        {
            return DAL_KhuVuc.Instance.LayTheoId(id);
        }

        public ResponseResult ThemKhuVuc(ET_KhuVuc et)
        {
            if (string.IsNullOrEmpty(et.MaCode)) et.MaCode = LayMaCodeTiepTheo();
            et.CreatedAt = DateTime.Now;

            bool success = DAL_KhuVuc.Instance.Them(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm khu vực vào CSDL.");
        }

        public ResponseResult SuaKhuVuc(ET_KhuVuc et)
        {
            et.UpdatedAt = DateTime.Now;
            bool success = DAL_KhuVuc.Instance.Sua(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật thông tin khu vực.");
        }

        public ResponseResult XoaKhuVuc(string code)
        {
            var existing = DAL_KhuVuc.Instance.LoadDS().FirstOrDefault(x => x.MaCode == code);
            if (existing == null) return ResponseResult.Error("Không tìm thấy khu vực.");
            bool success = DAL_KhuVuc.Instance.Xoa(existing.Id);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa khu vực.");
        }

        public string LayMaCodeTiepTheo()
        {
            var ds = DAL_KhuVuc.Instance.LoadDS();
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
            
            var ds = DAL_KhuVuc.Instance.LoadDS();
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
