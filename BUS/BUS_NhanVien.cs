using System;
using System.Collections.Generic;
using System.Linq;
using ET;
using DAL;

namespace BUS
{
    public class BUS_NhanVien : IBaseBUS<ET_NhanVien>
    {
        private readonly INhanVienGateway _gateway;
        private readonly DAL_NhanVien     _dal = DAL_NhanVien.Instance;

        private static BUS_NhanVien instance;
        public static BUS_NhanVien Instance
        {
            get { if (instance == null) instance = new BUS_NhanVien(); return instance; }
        }

        public BUS_NhanVien() : this(new DefaultNhanVienGateway()) { }
        public BUS_NhanVien(INhanVienGateway gw) { _gateway = gw; }

        // ── CRUD Cơ Bản ────────────────────────────────────────────────────
        public List<ET_NhanVien> LoadDS()
        {
            var ds = _gateway.LoadDS();
            var dict = ds.ToDictionary(x => x.Id, x => x.HoTen);
            foreach (var nv in ds)
            {
                if (nv.IdNguoiQuanLy.HasValue && dict.ContainsKey(nv.IdNguoiQuanLy.Value))
                    nv.TenNguoiQuanLy = dict[nv.IdNguoiQuanLy.Value];
            }
            return ds;
        }
        public ET_NhanVien GetById(int id) => _gateway.LayTheoId(id);

        public ResponseResult Them(ET_NhanVien et)
        {
            string err = ValidateNhanVien(et, true);
            if (!string.IsNullOrEmpty(err)) return ResponseResult.Error(err);
            bool ok = _gateway.Them(et);
            return ok ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm nhân viên vào CSDL.");
        }

        public ResponseResult Sua(ET_NhanVien et)
        {
            string err = ValidateNhanVien(et, false);
            if (!string.IsNullOrEmpty(err)) return ResponseResult.Error(err);
            bool ok = _gateway.Sua(et);
            return ok ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật nhân viên.");
        }

        public ResponseResult Xoa(int id)
        {
            bool ok = _gateway.Xoa(id);
            return ok ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa nhân viên này.");
        }

        // ── Tìm kiếm ───────────────────────────────────────────────────────
        public List<ET_NhanVien> TimKiem(string kw, string chucVu = "Tất cả")
        {
            var query = LoadDS();
            if (!string.IsNullOrWhiteSpace(kw))
            {
                kw = kw.ToLower();
                query = query.FindAll(x =>
                    (x.HoTen     != null && x.HoTen.ToLower().Contains(kw)) ||
                    (x.MaCode    != null && x.MaCode.ToLower().Contains(kw)) ||
                    (x.DienThoai != null && x.DienThoai.Contains(kw)));
            }
            if (chucVu != "Tất cả") return TimKiemNangCao(kw, chucVu);
            return query;
        }

        public List<ET_NhanVien> TimKiemNangCao(string kw, string chucVu)
        {
            var query = LoadDS();
            if (!string.IsNullOrWhiteSpace(kw))
            {
                kw = kw.ToLower();
                query = query.FindAll(x =>
                    (x.HoTen     != null && x.HoTen.ToLower().Contains(kw)) ||
                    (x.MaCode    != null && x.MaCode.ToLower().Contains(kw)) ||
                    (x.DienThoai != null && x.DienThoai.Contains(kw)));
            }
            return query;
        }

        public string LayMaCodeTiepTheo()
        {
            int max = 0;
            foreach (var item in LoadDS())
            {
                if (!string.IsNullOrEmpty(item.MaCode) && item.MaCode.StartsWith("NV"))
                    if (int.TryParse(item.MaCode.Substring(2), out int n) && n > max) max = n;
            }
            return "NV" + (max + 1).ToString("D3");
        }

        public string ValidateNhanVien(ET_NhanVien et, bool isAdd)
        {
            if (string.IsNullOrWhiteSpace(et.HoTen))     return "Vui lòng nhập họ tên.";
            if (string.IsNullOrWhiteSpace(et.DienThoai)) return "Vui lòng nhập số điện thoại.";
            if (et.DienThoai.Length < 10)                 return "Số điện thoại phải từ 10 số.";
            if (!et.NgaySinh.HasValue)                    return "Vui lòng nhập ngày sinh.";
            if (DateTime.Now.Year - et.NgaySinh.Value.Year < 15) return "Nhân viên phải từ 15 tuổi trở lên.";
            if (string.IsNullOrWhiteSpace(et.Cccd))       return "Vui lòng nhập số CCCD.";
            if (et.Cccd.Length != 12)                     return "Số CCCD phải đủ 12 số.";
            if (!string.IsNullOrWhiteSpace(et.Email) && !et.Email.Contains("@"))
                return "Email không hợp lệ.";

            var ds = LoadDS();
            if (isAdd)
            {
                if (ds.Any(x => x.DienThoai == et.DienThoai))                     return "Số điện thoại đã tồn tại.";
                if (ds.Any(x => x.Cccd == et.Cccd))                               return "CCCD đã tồn tại.";
                if (!string.IsNullOrWhiteSpace(et.TenDangNhap) &&
                    ds.Any(x => x.TenDangNhap == et.TenDangNhap))                 return "Tên đăng nhập đã tồn tại.";
            }
            else
            {
                if (ds.Any(x => x.DienThoai == et.DienThoai && x.Id != et.Id))    return "Số điện thoại bị trùng.";
                if (ds.Any(x => x.Cccd == et.Cccd && x.Id != et.Id))              return "CCCD bị trùng.";
                if (!string.IsNullOrWhiteSpace(et.TenDangNhap) &&
                    ds.Any(x => x.TenDangNhap == et.TenDangNhap && x.Id != et.Id)) return "Tên đăng nhập bị trùng.";
            }
            return string.Empty;
        }

        public ET_NhanVien DangNhap(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null;
            return _gateway.DangNhap(username, password);
        }

        // ── HR — Chứng Chỉ ─────────────────────────────────────────────────
        public List<ET_ChungChiNhanVien> LoadChungChi(int idNhanVien) => _dal.LoadChungChi(idNhanVien);

        public ResponseResult ThemChungChi(ET_ChungChiNhanVien et)
        {
            if (string.IsNullOrWhiteSpace(et.LoaiChungChi)) return ResponseResult.Error("Vui lòng chọn loại chứng chỉ.");
            if (et.NgayHetHan < et.NgayCap)                 return ResponseResult.Error("Ngày hết hạn phải sau ngày cấp.");
            return _dal.ThemChungChi(et) ? ResponseResult.Success() : ResponseResult.Error("Không thể lưu chứng chỉ.");
        }

        public ResponseResult XoaChungChi(int id)
            => _dal.XoaChungChi(id) ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa chứng chỉ.");

        // ── HR — Kỷ Luật ───────────────────────────────────────────────────
        public List<ET_KyLuat> LoadKyLuat(int idNhanVien) => _dal.LoadKyLuat(idNhanVien);

        public ResponseResult ThemKyLuat(ET_KyLuat et)
        {
            if (string.IsNullOrWhiteSpace(et.HinhThuc)) return ResponseResult.Error("Vui lòng chọn hình thức kỷ luật.");
            if (string.IsNullOrWhiteSpace(et.MoTa))     return ResponseResult.Error("Vui lòng nhập lý do.");
            if ((et.HinhThuc == AppConstants.HinhThucKyLuat.DinhChiCoLuong || et.HinhThuc == AppConstants.HinhThucKyLuat.DinhChiKhongLuong) && et.SoNgayDinhChi <= 0) 
                 return ResponseResult.Error("Số ngày đình chỉ phải > 0.");
            if ((et.HinhThuc == AppConstants.HinhThucKyLuat.DinhChiCoLuong || et.HinhThuc == AppConstants.HinhThucKyLuat.DinhChiKhongLuong) && et.SoNgayDinhChi > 15) 
                 return ResponseResult.Error("Tối đa 15 ngày đình chỉ (BLLĐ 2019).");
            return _dal.ThemKyLuat(et) ? ResponseResult.Success() : ResponseResult.Error("Không thể lưu kỷ luật.");
        }

        public ResponseResult XoaKyLuat(int id)
            => _dal.XoaKyLuat(id) ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa kỷ luật.");

        // ── ĐƠN XIN NGHỈ ────────────────────────────────────────────────────
        public List<ET_DonXinNghi> LoadDonXinNghi(int idNhanVien)
        {
            return DAL_NhanVien.Instance.LoadDonXinNghi(idNhanVien);
        }

        public ResponseResult ThemDonXinNghi(ET_DonXinNghi et)
        {
            if (string.IsNullOrWhiteSpace(et.LoaiNghi)) return ResponseResult.Error("Vui lòng chọn loại nghỉ.");
            if (et.NgayKetThuc < et.NgayBatDau) return ResponseResult.Error("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.");
            return DAL_NhanVien.Instance.ThemDonXinNghi(et) ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm đơn xin nghỉ.");
        }

        public ResponseResult XoaDonXinNghi(int id)
        {
            return DAL_NhanVien.Instance.XoaDonXinNghi(id) ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa đơn xin nghỉ.");
        }

        // ── TAI NẠN LAO ĐỘNG ────────────────────────────────────────────────
        public List<ET_TaiNanLaoDong> LoadTaiNanLaoDong(int idNhanVien)
        {
            return DAL_NhanVien.Instance.LoadTaiNanLaoDong(idNhanVien);
        }

        public ResponseResult ThemTaiNanLaoDong(ET_TaiNanLaoDong et)
        {
            if (string.IsNullOrWhiteSpace(et.LoaiTaiNan)) return ResponseResult.Error("Vui lòng chọn loại tai nạn.");
            if (string.IsNullOrWhiteSpace(et.MucDo)) return ResponseResult.Error("Vui lòng chọn mức độ.");
            return DAL_NhanVien.Instance.ThemTaiNanLaoDong(et) ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm sự cố tai nạn.");
        }

        public ResponseResult XoaTaiNanLaoDong(int id)
        {
            return DAL_NhanVien.Instance.XoaTaiNanLaoDong(id) ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa sự cố tai nạn.");
        }
    }
}
