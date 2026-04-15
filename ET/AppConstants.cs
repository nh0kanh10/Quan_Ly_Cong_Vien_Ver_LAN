namespace ET
{
    public static class AppConstants
    {
        public static class RoleIds
        {
            public const int Admin = 1;
            public const int Manager = 2;
            public const int Staff = 3;
            public const int Warehouse = 4;
            public const int Accountant = 5;
        }

        public static class TrangThaiDonHang
        {
            public const string ChoThanhToan = "ChoThanhToan";
            public const string DaThanhToan = "DaThanhToan";
            public const string DangXuLy = "DangXuLy";
            public const string GhiNoCongTy = "GhiNoCongTy";
            public const string DaHuy = "DaHuy";
            public const string HoanTienMotPhan = "HoanTienMotPhan";
            public const string DaHoanTien = "DaHoanTien";

            public static string GetTenTrangThai(string trangThai)
            {
                switch (trangThai)
                {
                    case ChoThanhToan: return "Chờ thanh toán";
                    case DaThanhToan: return "Đã thanh toán";
                    case DangXuLy: return "Đang xử lý";
                    case GhiNoCongTy: return "Ghi nợ công ty";
                    case DaHuy: return "Đã hủy";
                    case HoanTienMotPhan: return "Hoàn tiền một phần";
                    case DaHoanTien: return "Đã hoàn tiền";
                    default: return trangThai;
                }
            }
        }

        public static class TrangThaiTheRfid
        {
            public const string Active = "Active";
            public const string Pending = "Pending";
            public const string Lost = "Lost";
            public const string Revoked = "Revoked";
            public const string Locked = "Locked";
        }

        public static class PhuongThucThanhToan
        {
            public const string TienMat = "TienMat";
            public const string ViRfid = "ViRFID";
            public const string ChuyenKhoan = "ChuyenKhoan";
            public const string The = "The";
            public const string TheNganHang = "TheNganHang";
            public const string ViDienTu = "ViDienTu";
            public const string MoMo = "MoMo";
            public const string VnPay = "VnPay";
            public const string Qr = "QR";
        }

        public static class LoaiGiaoDichVi
        {
            public const string NapTien = "NapTien";
            public const string ThanhToanDichVu = "ThanhToanDichVu";
            public const string ThuCoc = "ThuCoc";
            public const string HoanCoc = "HoanCoc";
            public const string ThuTienPhat = "ThuTienPhat";
            public const string HoanTien = "HoanTien";
            public const string DieuChinhTang = "DieuChinhTang";
            public const string DieuChinhGiam = "DieuChinhGiam";
        }

        public static class LoaiGiaoDichDiem
        {
            public const string TichLuy = "TichLuy";
            public const string SuDung = "SuDung";
            public const string HetHan = "HetHan";
            public const string DieuChinh = "DieuChinh";
        }

        public static class LoaiKhachHang
        {
            public const string CaNhan = "CaNhan";
            public const string Doan = "Doan";
            public const string DoanhNghiep = "DoanhNghiep";
            public const string HocSinhSinhVien = "HocSinhSinhVien";
            public const string Vip = "VIP";
            public const string VVIP = "VVIP";
            public const string NoiBo = "NoiBo";
        }

        public static class LoaiSanPham
        {
            public const string Ve = "Ve";
            public const string Combo = "Combo";
            public const string Thue = "Thue";
            public const string AnUong = "AnUong";
            public const string LuuTru = "LuuTru";
            public const string DoLuuNiem = "DoLuuNiem";
            public const string GuiXe = "GuiXe";
            public const string DichVu = "DichVu";
            public const string Khac = "Khac";
        }

        public static class LoaiGia
        {
            public const string TienCoc = "TienCoc";
            public const string GiaNghiTrua = "GiaNghiTrua";
            public const string GiaTheoNgay = "GiaTheoNgay";
            public const string PhatVatTu = "PhatVatTu";
        }

        public static class LoaiGiaApDung
        {
            public const string MacDinh = "MacDinh";
            public const string CuoiTuan = "CuoiTuan";
            public const string NgayLe = "NgayLe";
        }

        public static class LoaiKhuyenMai
        {
            public const string PhanTram = "PhanTram";
            public const string SoTien = "SoTien";
            public const string DongGia = "DongGia";
            public const string MuaXTangY = "MuaXTangY";
        }

        public static class TrangThaiChung
        {
            public const string HoatDong = "HoatDong";
            public const string TamNgung = "TamNgung";
        }

        public static class TrangThaiSanPham
        {
            public const string DangBan = "DangBan";
            public const string TamNgung = "TamNgung";
            public const string NgungBan = "NgungBan";
            public const string HetHang = "HetHang";
        }

        public static class TrangThaiCombo
        {
            public const string BanNhap = "BanNhap";
            public const string KichHoat = "KichHoat";
            public const string NgungApDung = "NgungApDung";
        }
        
        public static class TrangThaiPhong
        {
            public const string Trong = "Trong";
            public const string DaDat = "DaDat";
            public const string DangSuDung = "DangSuDung";
            public const string DonDep = "DonDep";
            public const string BaoTri = "BaoTri";
            public const string DaHuy = "DaHuy";
            public const string HoanTat = "HoanTat";
        }

        public static class TrangThaiBanAn
        {
            public const string Trong = "Trong";
            public const string DaDat = "DaDat";
            public const string DangSuDung = "DangSuDung";
            public const string BaoTri = "BaoTri";
            public const string DaHuy = "DaHuy";
            public const string HoanTat = "HoanTat";
        }

        public static class TrangThaiCoc
        {
            public const string ChuaHoan = "ChuaHoan";
            public const string DaHoan = "DaHoan";
            public const string DaPhat = "DaPhat";
        }

        public static class TrangThaiVeDienTu
        {
            public const string ChuaSuDung = "ChuaSuDung";
            public const string DaSuDung = "DaSuDung";
            public const string DaHuy = "DaHuy";
            public const string HetHan = "HetHan";
            public const string DangSuDung = "DangSuDung";
        }

        public static class TrangThaiGuiXe
        {
            public const string DangGui = "DangGui";
            public const string DaTra = "DaTra";
            public const string MatVe = "MatVe";
        }

        public static class LoaiXe
        {
            public const string XeDap = "XeDap";
            public const string XeMay = "XeMay";
            public const string OTo = "OTo";
            public const string XeDien = "XeDien";
        }

        public static class TrangThaiDoanKhach
        {
            public const string DaDat = "DaDat";
            public const string DangPhucVu = "DangPhucVu";
            public const string DaXuatVe = "DaXuatVe";
            public const string DaHoanTat = "DaHoanTat";
            public const string HetHan = "HetHan";
            public const string DaHuy = "DaHuy";
        }

        public static class TrangThaiDichVuDoan
        {
            public const string ChuaXuLy = "ChuaXuLy";
            public const string DaDatCho = "DaDatCho";
            public const string DangPhucVu = "DangPhucVu";
            public const string DaThanhToan = "DaThanhToan";
            public const string DaHuy = "DaHuy";
        }

        public static class LoaiDichVuDoan
        {
            public const string Ve = "Ve";
            public const string Combo = "Combo";
            public const string Phong = "Phong";
            public const string BanAn = "BanAn";
            public const string AnUong = "AnUong";
            public const string DichVu = "DichVu";
        }

        public static class Validation
        {
            public const int PhoneLength = 10;
            public const int CccdMinLength = 9;
            public const int CccdMaxLength = 12;
            public const decimal MaxRechargePerTransaction = 50_000_000m;
            public const decimal MinRechargeAmount = 1_000m;

            public static readonly System.Text.RegularExpressions.Regex PhonePattern =
                new System.Text.RegularExpressions.Regex(@"^\d{10}$", System.Text.RegularExpressions.RegexOptions.Compiled);
            public static readonly System.Text.RegularExpressions.Regex EmailPattern =
                new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", System.Text.RegularExpressions.RegexOptions.Compiled);
            public static readonly System.Text.RegularExpressions.Regex CccdPattern =
                new System.Text.RegularExpressions.Regex(@"^\d{9,12}$", System.Text.RegularExpressions.RegexOptions.Compiled);
        }

        // == HR MODULE CONSTANTS ==

        public static class LoaiKhoi
        {
            public const string VanHanh = "VanHanh";
            public const string HanhChinh = "HanhChinh";
        }

        public static class TrangThaiNhanVien
        {
            public const string ThuViec = "ThuViec";
            public const string DangLamViec = "Đang làm việc";
            public const string TamNghi = "Tạm nghỉ";
            public const string NghiViec = "Nghỉ việc";
        }

        public static class LoaiHopDong
        {
            public const string FullTime = "FullTime";
            public const string PartTime = "PartTime";
            public const string TheoMua = "TheoMua";
            public const string Intern = "Intern";
        }

        public static class NhomCongViec
        {
            public const string ThuongThuong = "ThuongThuong";
            public const string NangNhocNguyHiem = "NangNhocNguyHiem";
            public const string DacBietNguyHiem = "DacBietNguyHiem";
        }

        public static class LoaiChungChi
        {
            public const string CuuHoBoiLoi = "CuuHoBoiLoi";
            public const string SoCuuYTe_CPR = "SoCuuYTe_CPR";
            public const string VanHanhThietBiCoKhi = "VanHanhThietBiCoKhi";
            public const string ChamSocDongVatHoangDa = "ChamSocDongVatHoangDa";
            public const string LaiXeNangHang = "LaiXeNangHang";
            public const string AnToanDien = "AnToanDien";
            public const string Khac = "Khac";
        }

        public static class TrangThaiChungChi
        {
            public const string ConHieuLuc = "ConHieuLuc";
            public const string SapHetHan = "SapHetHan";
            public const string HetHan = "HetHan";
        }

        public static class LoaiNghi
        {
            public const string PhepNam = "PhepNam";
            public const string NghiOm = "NghiOm";
            public const string ThaiSanNu = "ThaiSanNu";
            public const string ThaiSanNam = "ThaiSanNam";
            public const string TaiNanLaoDong = "TaiNanLaoDong";
            public const string NghiBu = "NghiBu";
            public const string NghiLe = "NghiLe";
            public const string DotXuatCoLuong = "DotXuatCoLuong";
            public const string NghiKhongLuong = "NghiKhongLuong";
        }

        public static class NguonChiTra
        {
            public const string CongTy = "CongTy";
            public const string BHXH = "BHXH";
        }

        public static class TrangThaiDonNghi
        {
            public const string ChoDuyet = "ChoDuyet";
            public const string DaDuyet = "DaDuyet";
            public const string TuChoi = "TuChoi";
            public const string DaHuy = "DaHuy";
        }

        public static class LoaiTaiNan
        {
            public const string TrongGioLam = "TrongGioLam";
            public const string NgoaiGioLam = "NgoaiGioLam";
            public const string DiLai = "DiLai";
        }

        public static class MucDoTaiNan
        {
            public const string Nhe = "Nhe";
            public const string TrungBinh = "TrungBinh";
            public const string NangNe = "NangNe";
            public const string TuVong = "TuVong";
        }

        public static class TrangThaiTaiNan
        {
            public const string DangDieuTri = "DangDieuTri";
            public const string DaDieuTri = "DaDieuTri";
            public const string TamTat = "TamTat";
            public const string VinhVien = "VinhVien";
            public const string TuVong = "TuVong";
        }

        public static class HinhThucKyLuat
        {
            public const string CanhCao = "CanhCao";
            public const string TruLuong = "TruLuong";
            public const string DinhChiCoLuong = "DinhChiCoLuong";
            public const string DinhChiKhongLuong = "DinhChiKhongLuong";
            public const string SaThai = "SaThai";
        }

        public static class Display
        {
            public const string NoData = "Chưa có";
            public const string NoWallet = "CHƯA CẤP VÍ";
            public const string NoRfid = "CHƯA GẮN THẺ";
            public const string WalletActive = "ĐANG HOẠT ĐỘNG";
            public const string WalletLocked = "ĐÃ KHÓA";
        }
    }
}
