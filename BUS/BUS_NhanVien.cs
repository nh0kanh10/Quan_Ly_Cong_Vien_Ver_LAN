using System;
using DAL;
using ET;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BUS
{
    public class BUS_NhanVien : IBaseBUS<ET_NhanVien>
    {
        private static BUS_NhanVien instance;
        public static BUS_NhanVien Instance
        {
            get
            {
                if (instance == null) instance = new BUS_NhanVien();
                return instance;
            }
        }

        public List<ET_NhanVien> LoadDS()
        {
            return DAL_NhanVien.Instance.LoadDS();
        }

        public ET_NhanVien GetById(int id)
        {
            return DAL_NhanVien.Instance.LayTheoId(id);
        }

        public ResponseResult Them(ET_NhanVien et)
        {
            string err = ValidateNhanVien(et, true);
            if (!string.IsNullOrEmpty(err)) return ResponseResult.Error(err);
            
            bool success = DAL_NhanVien.Instance.Them(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm nhân viên vào CSDL.");
        }

        public ResponseResult Sua(ET_NhanVien et)
        {
            string err = ValidateNhanVien(et, false);
            if (!string.IsNullOrEmpty(err)) return ResponseResult.Error(err);

            bool success = DAL_NhanVien.Instance.Sua(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật nhân viên.");
        }

        public ResponseResult Xoa(int id)
        {
            bool success = DAL_NhanVien.Instance.Xoa(id);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa nhân viên này.");
        }

        public List<ET_NhanVien> TimKiem(string kw, string chucVu = "Tất cả")
        {
            var query = LoadDS();
            if (!string.IsNullOrWhiteSpace(kw))
            {
                kw = kw.ToLower();
                query = query.FindAll(x => (x.HoTen != null && x.HoTen.ToLower().Contains(kw)) || 
                                           (x.MaCode != null && x.MaCode.ToLower().Contains(kw)) ||
                                           (x.DienThoai != null && x.DienThoai.Contains(kw)));
            }
            if (chucVu != "Tất cả")
            {
                
                return TimKiemNangCao(kw, chucVu);
            }
            return query;
        }

        public List<ET_NhanVien> TimKiemNangCao(string kw, string chucVu)
        {
            var query = LoadDS();
            if (!string.IsNullOrWhiteSpace(kw))
            {
                kw = kw.ToLower();
                query = query.FindAll(x => (x.HoTen != null && x.HoTen.ToLower().Contains(kw)) || 
                                           (x.MaCode != null && x.MaCode.ToLower().Contains(kw)) ||
                                           (x.DienThoai != null && x.DienThoai.Contains(kw)));
            }
            if (chucVu != "Tất cả")
            {
                
            }
            return query;
        }

        public string LayMaCodeTiepTheo()
        {
            var list = LoadDS();
            int max = 0;
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.MaCode) && item.MaCode.StartsWith("NV"))
                {
                    int num;
                    if (int.TryParse(item.MaCode.Substring(2), out num) && num > max) max = num;
                }
            }
            return "NV" + (max + 1).ToString("D3");
        }

        public string ValidateNhanVien(ET_NhanVien et, bool isAdd)
        {
            if (string.IsNullOrWhiteSpace(et.HoTen)) return "Vui lòng nhập họ tên.";
            if (string.IsNullOrWhiteSpace(et.DienThoai)) return "Vui lòng nhập số điện thoại.";
            if (et.DienThoai.Length < 10) return "Số điện thoại không hợp lệ (phải từ 10 số).";
            
            if (string.IsNullOrWhiteSpace(et.Cccd)) return "Vui lòng nhập số CCCD.";
            if (et.Cccd.Length != 12) return "Số CCCD phải đủ 12 số.";

            int year = et.NgaySinh.HasValue ? et.NgaySinh.Value.Year : 0;
            if (DateTime.Now.Year - year < 18)
                return "Nhân viên phải từ 18 tuổi trở lên.";

            if (!string.IsNullOrWhiteSpace(et.Email) && !et.Email.Contains("@")) 
                return "Email không hợp lệ.";

            var ds = LoadDS();
            if (isAdd)
            {
                if (ds.Any(x => x.DienThoai == et.DienThoai)) return "Số điện thoại đã tồn tại trong hệ thống.";
                if (ds.Any(x => x.Cccd == et.Cccd)) return "CCCD đã tồn tại trong hệ thống.";
                if (!string.IsNullOrWhiteSpace(et.TenDangNhap) && ds.Any(x => x.TenDangNhap == et.TenDangNhap))
                    return "Tên đăng nhập đã tồn tại.";
            }
            else
            {
                if (ds.Any(x => x.DienThoai == et.DienThoai && x.Id != et.Id)) 
                    return "Số điện thoại bị trùng with nhân viên khác.";
                if (ds.Any(x => x.Cccd == et.Cccd && x.Id != et.Id)) 
                    return "CCCD bị trùng with nhân viên khác.";
                if (!string.IsNullOrWhiteSpace(et.TenDangNhap) && ds.Any(x => x.TenDangNhap == et.TenDangNhap && x.Id != et.Id))
                    return "Tên đăng nhập bị trùng with nhân viên khác.";
            }

            return string.Empty;
        }

        // End of TimKiem methods

        public ET_NhanVien DangNhap(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) 
                return null;
            return DAL_NhanVien.Instance.DangNhap(username, password);
        }
    }
}
