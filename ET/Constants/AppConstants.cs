namespace ET.Constants
{
    public static class AppConstants
    {
    // Keys cấu hình hệ thống — giá trị cột Khoa trong bảng HeThong.CauHinh.
    // Dùng: BUS_CauHinh.Instance.GetValue(ConfigKeys.DIEM_QUY_DOI)
    public static class ConfigKeys
    {
        public const string DIEM_QUY_DOI = "DIEM_QUY_DOI_1000D";           // 1000 VNĐ = 1 điểm
        public const string DIEM_HET_HAN = "DIEM_HET_HAN_THANG";          // Điểm hết hạn sau N tháng
        public const string DIEM_CAP_PHAN_TRAM = "DIEM_CAP_PHAN_TRAM";    // Dùng điểm tối đa N% tổng đơn
        public const string SO_PHUT_GIU_TON_KHO = "SO_PHUT_GIU_TON_KHO";  //
                                                                          // giữ tồn kho (phút)
        public const string HE_SO_TANG_CA = "HE_SO_TANG_CA";              // Hệ số lương tăng ca (VD: 1.5)
        public const string NGAY_PHEP_MAC_DINH = "NGAY_PHEP_MAC_DINH";    // Số ngày phép/năm mặc định
        public const string NGON_NGU_MAC_DINH = "NGON_NGU_MAC_DINH";      // Ngôn ngữ mặc định (vi/en)
        public const string CK_HANG_PREFIX = "CK_HANG_";                  // Prefix chiết khấu hạng
        public const string HOAN_GIOI_HAN_GIO_HANGHOA = "HOAN_GIOI_HAN_GIO_HANGHOA"; // Giờ tối đa được hoàn Hàng hóa
        public const string HOAN_GIOI_HAN_GIO_VE = "HOAN_GIOI_HAN_GIO_VE";           // Giờ tối đa được hoàn Vé
        public const string PHAT_LO_GIO_PHAN_TRAM_MOI_GIO = "PHAT_LO_GIO_PHAN_TRAM_MOI_GIO"; // Tỷ lệ phạt lố giờ (% / giờ)
    }

    // Tên schema trong DB — dùng khi query bảng theo schema.
    public static class SchemaNames
    {
        public const string HeThong = "HeThong";
        public const string DoiTac = "DoiTac";
        public const string DanhMuc = "DanhMuc";
        public const string BanHang = "BanHang";
        public const string Kho = "Kho";
        public const string TaiChinh = "TaiChinh";
        public const string VanHanh = "VanHanh";
        public const string NhanSu = "NhanSu";
    }

    // NhomMa = 'SP_LOAI' — Phân loại sản phẩm (quyết định POS nào hiện sản phẩm)
    public static class LoaiSanPham
    {
        public const string VeVaoKhu = "VeVaoKhu";
        public const string VeTroChoi = "VeTroChoi";
        public const string AnUong = "AnUong";
        public const string DoUong = "DoUong";
        public const string HangHoa = "HangHoa";
        public const string Combo = "Combo";
        public const string TuDo = "TuDo";
        public const string DoChoThue = "DoChoThue";
        public const string ChoiNghiMat = "ChoiNghiMat";
        public const string DatChoThuAn = "DatChoThuAn";
        public const string LuuTru = "LuuTru";
        public const string GuiXe = "GuiXe";
        public const string NguyenLieu = "NguyenLieu";
        public const string DoUongDongChai = "DoUongDongChai";
        public const string AnUongTienLoi = "AnUongTienLoi";
        public const string PhuongTien = "PhuongTien";
    }

    // NhomMa = 'PHUONG_THUC_TT' — Phương thức thanh toán
    public static class PhuongThucTT
    {
        public const string TienMat = "TienMat";
        public const string ChuyenKhoan = "ChuyenKhoan";
        public const string ViRFID = "ViRFID";
        public const string QR = "QR";
        public const string MoMo = "MoMo";
        public const string TheNganHang = "TheNganHang";
        public const string DiemTichLuy = "DiemTichLuy";
    }

    // Loại giao dịch điểm trong TaiChinh.LichSuDiem
    public static class LoaiGiaoDichDiem
    {
        public const string TruDiem = "TruDiem";
        public const string CongDiem = "CongDiem";
    }

    // Các mã nhóm Từ điển trong bảng HeThong.TuDien
    public static class NhomTuDien
    {
        public const string HangThanhVien = "KH_HANG_TV";
        public const string LoaiKhach = "KH_LOAI_KHACH";
        public const string LoaiDoiTac = "LOAI_DOI_TAC";
        public const string PhuongThucTT = "PHUONG_THUC_TT";
        public const string LoaiSanPham = "SP_LOAI";
    }

    // NhomMa = 'LOAI_DOI_TAC' — Phân loại đối tác
    public static class LoaiDoiTac
    {
        public const string CaNhan = "CaNhan";
        public const string ToChuc = "ToChuc";
        public const string CongTyLuHanh = "CongTyLuHanh";
        public const string NhanVien = "NhanVien";
    }

    // NhomMa = 'KH_LOAI_KHACH' — Phân loại khách hàng
    public static class LoaiKhach
    {
        public const string CaNhan = "CaNhan";
        public const string Doan = "Doan";
        public const string DoanhNghiep = "DoanhNghiep";
        public const string HocSinhSinhVien = "HocSinhSinhVien";
        public const string NoiBo = "NoiBo";
    }

    // NhomMa = 'KH_HANG_TV' — Hạng thành viên
    public static class HangThanhVien
    {
        public const string Thuong = "Thuong";
        public const string Bac = "Bac";
        public const string Vang = "Vang";
        public const string KimCuong = "KimCuong";
    }

    // NhomMa = 'MUC_DO' — Mức độ sự cố / tai nạn
    public static class MucDo
    {
        public const string Nhe = "Nhe";
        public const string TrungBinh = "TrungBinh";
        public const string NangNe = "NangNe";
        public const string CucKyNghiemTrong = "CucKyNghiemTrong";
    }

    // NhomMa = 'KHUYEN_MAI_LOAI' — Loại giảm giá khuyến mãi
    public static class LoaiGiamGia
    {
        public const string PhanTram = "PhanTram";
        public const string SoTien = "SoTien";
        public const string DongGia = "DongGia";
        public const string MuaXTangY = "MuaXTangY";
    }

    // NhomMa = 'KM_DIEU_KIEN' — Loại điều kiện khuyến mãi
    public static class LoaiDieuKienKM
    {
        public const string DoTuoi = "DoTuoi";
        public const string HangThanhVien = "HangThanhVien";
        public const string LoaiKhach = "LoaiKhach";
        public const string NgayApDung = "NgayApDung";
        public const string SanPhamApDung = "SanPhamApDung";
    }

    // Phép so sánh trong KhuyenMai_DieuKien
    public static class PhepSoKM
    {
        public const string BangNhau = "=";
        public const string IN = "IN";
    }

    // ThucThe (EntityName) — Dùng trong bảng HeThong.LuongTrangThai
    public static class ThucThe
    {
        public const string DonHang = "DonHang";
        public const string Phong = "Phong";
        public const string DatPhong = "DatPhong";
        public const string BanAn = "BanAn";
        public const string DatBan = "DatBan";
        public const string VeDienTu = "VeDienTu";
        public const string TheRFID = "TheRFID";
        public const string ChungTuKho = "ChungTuKho";
        public const string ThueDo = "ThueDo";
        public const string BaoTri = "BaoTri";
        public const string SuCo = "SuCo";
        public const string BangLuong = "BangLuong";
        public const string DonXinNghi = "DonXinNghi";
        public const string LenhBEO = "LenhBEO";
        public const string BuongPhong = "BuongPhong";
        public const string ThatLac = "ThatLac";
        public const string CaTruc = "CaTruc";
        public const string TaoSong = "TaoSong";
        public const string GuiXe = "GuiXe";
        public const string GiaiDua = "GiaiDua";
        public const string LichDua = "LichDua";
        public const string DongVatDua = "DongVatDua";
        public const string PhuongTienDua = "PhuongTienDua";
        public const string LoHang = "LoHang";
        public const string TamGiuTonKho = "TamGiuTonKho";
        public const string PhienThuNgan = "PhienThuNgan";
        public const string YTeThu = "YTeThu";
        public const string BaoGia = "BaoGia";
        public const string CongNo = "CongNo";
        public const string LichBieuDien = "LichBieuDien";
    }

    // NhomMa = 'VE_LOAI' — Phân loại vé
    public static class LoaiVe
    {
        public const string VeLe = "VeLe";
        public const string VeCombo = "VeCombo";
        public const string VeMuaVu = "VeMuaVu";
    }

    // NhomMa = 'KET_QUA_QUET_VE' — Kết quả quẹt vé qua cổng
    public static class KetQuaQuetVe
    {
        public const string ThanhCong = "ThanhCong";
        public const string SaiVe = "SaiVe";
        public const string DaHetLuot = "DaHetLuot";
        public const string VeHetHan = "VeHetHan";
        public const string DaHuy = "DaHuy";
    }

    public static class TrangThaiVe
    {
        public const string ChuaSuDung = "ChuaSuDung";
        public const string DaSuDung = "DaSuDung";
        public const string DaHuy = "DaHuy";
        public const string HetHan = "HetHan";
    }

    // Phân loại Kỷ luật / Xin nghỉ (Cho module HR)
    public static class LoaiHinhKyLuat
    {
        public const string CanhCao = "CanhCao";
        public const string TruLuong = "TruLuong";
        public const string DinhChiCoLuong = "DinhChiCoLuong";
        public const string SaThai = "SaThai";
    }

    // Trạng thái cho Sản phẩm / Dịch vụ
    public static class TrangThaiSanPham
    {
        public const string DangBan = "DangBan";
        public const string TamNgung = "TamNgung";
        public const string NgungBan = "NgungBan";
    }

    // Loại giá bán (Bảng giá)
    public static class LoaiGiaBan
    {
        public const string MacDinh = "MacDinh";
        public const string CuoiTuan = "CuoiTuan";
        public const string NgayLe = "NgayLe";
        public const string KhuyenMai = "KhuyenMai";
    }

    // Trạng thái chung (Hiệu lực)
    public static class TrangThaiHieuLuc
    {
        public const string HoatDong = "HoatDong";
        public const string VoHieuHoa = "VoHieuHoa";
        public const string DaXoa = "DaXoa";
    }


    // Trạng thái Quyền Lợi Đoàn Khách (BanHang.QuyenLoiDoanKhach.TrangThai)
    public static class TrangThaiQuyenLoi
    {
        public const string HopLe = "ConHieuLuc";
        public const string DaHetHan = "DaHetHan";
        public const string DaHuy = "DaHuy";
    }

    // NhomMa = 'RFID_TRANG_THAI'
    public static class TrangThaiThe
    {
        public const string ChuaKichHoat = "ChuaKichHoat";
        public const string DangDung = "DangDung";
        public const string DaKhoa = "DaKhoa";
        public const string DaTra = "DaTra";
    }

    // Tên thực thể để truy vấn bảng HeThong.BanDich đa ngôn ngữ
    public static class ThucTheDich
    {
        public const string SanPham = "SanPham";
        public const string DonViTinh = "DonViTinh";
        public const string CauHinhThue = "CauHinhThue";
    }

    // Tên trường dịch để truy vấn đa ngôn ngữ
    public static class TruongDich
    {
        public const string TenSanPham = "TenSanPham";
        public const string TenDonVi = "TenDonVi";
        public const string TenThue = "TenThue";
    }

    // Trạng thái phiếu kho (Moi -> ChoDuyet -> DaDuyet / DaHuy)
    public static class TrangThaiChungTuKho
    {
        public const string Moi = "Moi";
        public const string ChoDuyet = "ChoDuyet";
        public const string ChoDuyet2 = "ChoDuyet2";
        public const string DaDuyet = "DaDuyet";
        public const string DaHuy = "DaHuy";
    }

    public static class TrangThaiKho
    {
        public const string HoatDong = "HoatDong";
        public const string NgungHoatDong = "NgungHoatDong";
    }

    public static class TrangThaiPhienThuNgan
    {
        public const string DangMo = "DangMo";
        public const string DaDong = "DaDong";
    }

    public static class TrangThaiDonHang
    {
        public const string ChoThanhToan = "ChoThanhToan";
        public const string DaThanhToan = "DaThanhToan";
        public const string DaHuy = "DaHuy";
        public const string DangMo = "DangPhucVu"; 
        public const string MotPhan = "DangXuLy";
    }

    // Trạng thái Phòng Khách sạn
    public static class TrangThaiPhong
    {
        public const string Trong = "Trong";
        public const string DangO = "DangSuDung";
        public const string ChoDon = "DonDep";
        public const string BaoTri = "BaoTri";
    }

    // Trạng thái Đặt Phòng 
    public static class TrangThaiBooking
    {
        public const string DatTruoc = "DatTruoc";
        public const string DangO = "DangO";
        public const string DaTra = "DaTra";
        public const string DaHuy = "DaHuy";
    }

    // Trạng thái Chi Tiết Đặt Phòng
    public static class TrangThaiChiTietDatPhong
    {
        public const string ChoDen = "ChoDen";
        public const string DaCheckIn = "DaCheckIn";
        public const string DaCheckOut = "DaCheckOut";
        public const string DaHuy = "DaHuy";
    }

    // Nguồn bán hàng
    public static class NguonBan
    {
        public const string TrucTiep = "TrucTiep";
        public const string Online = "Online";
        public const string OTA = "OTA";
    }

    public static class TrangThaiChungTuTC
    {
        public const string ChoDuyet = "ChoDuyet";
        public const string DaDuyet = "DaDuyet";
        public const string DaHuy = "DaHuy";
    }

    // NhomMa = 'LOAI_CHUNG_TU_KHO' — Phân loại phiếu kho
    public static class LoaiChungTuKho
    {
        public const string NHAP_MUA = "NHAP_MUA";
        public const string XUAT_BAN = "XUAT_BAN";
        public const string TRA_NCC = "TRA_NCC";
        public const string KHACH_TRA = "KHACH_TRA";
        public const string HUY_HONG = "HUY_HONG";
        public const string CHUYEN_KHO = "CHUYEN_KHO";
        public const string KIEM_KE = "KIEM_KE";
        public const string XUAT_BAOTRI = "XUAT_BAOTRI";
        public const string XUAT_SANXUAT = "XUAT_SANXUAT";
    }

    // Trạng thái tồn kho — hiển thị trên grid ucTonKho
    public static class TrangThaiTonKho
    {
        public const string BinhThuong = "BINH_THUONG";
        public const string DuoiMuc = "DUOI_MUC";
    }

    // Trạng thái lô hàng — cột TrangThai của bảng LoHang
    public static class TrangThaiLoHang
    {
        public const string ConHang = "ConHang";
        public const string HetHang = "HetHang";
        public const string DaHuy = "DaHuy";
    }

    // NhomMa = 'LOAI_CHUNG_TU_TC' — Phân loại phiếu tài chính
    public static class LoaiChungTuTC
    {
        public const string THU_THANHTOAN = "THU_THANHTOAN";
        public const string THU_COC = "THU_COC";
        public const string THU_NAP_VI = "THU_NAP_VI";
        public const string THU_PHAT = "THU_PHAT";
        public const string CHI_NHAP_HANG = "CHI_NHAP_HANG";
        public const string CHI_LUONG = "CHI_LUONG";
        public const string CHI_BAO_TRI = "CHI_BAO_TRI";
        public const string HOAN_TIEN = "HOAN_TIEN";
        public const string HOAN_COC = "HOAN_COC";
        public const string HOAN_SO_DU_VI = "HOAN_SO_DU_VI";
    }


    public static class LoaiPhepVi
    {
        public const string Nap = "Nap";
        public const string Tru = "Tru";
        public const string Thu = "Thu";
        public const string Chi = "Chi";
        public const string Cong = "Cong";
    }

    public static class TrangThaiCombo
    {
        public const string BanNhap = "BanNhap";
        public const string HoatDong = "HoatDong";
        public const string NgungApDung = "NgungApDung";
    }

    public static class ErrorMessages
    {
        // SanPham / Product
        public const string ERR_REQUIRED_MASP = "ERR_REQUIRED_MASP";
        public const string ERR_REQUIRED_TENSP = "ERR_REQUIRED_TENSP";
        public const string ERR_REQUIRED_LOAISP = "ERR_REQUIRED_LOAISP";
        public const string ERR_REQUIRED_DVT = "ERR_REQUIRED_DVT";
        public const string ERR_TRUNG_MASP = "ERR_TRUNG_MASP";
        public const string ERR_MASP_CHI_TIENTO = "ERR_MASP_CHI_TIENTO";
        public const string ERR_HESO_KHONGHOPLE = "ERR_HESO_KHONGHOPLE";
        public const string ERR_LUOI_CONG_RONG = "ERR_LUOI_CONG_RONG";
        public const string ERR_NGAY_KHONGHOPLE = "ERR_NGAY_KHONGHOPLE";
        public const string ERR_ANH_SAIDINHDANG = "ERR_ANH_SAIDINHDANG";
        public const string ERR_LUOI_VE_QUA1DONG = "ERR_LUOI_VE_QUA1DONG";
        public const string ERR_GIABAN_AMHOAC0 = "ERR_GIABAN_AMHOAC0";
        public const string ERR_CONTONKHO = "ERR_CONTONKHO";
        public const string ERR_CONDONHANG = "ERR_CONDONHANG";

        // POS
        public const string ERR_POS_OUT_OF_STOCK = "ERR_POS_OUT_OF_STOCK";
        public const string MSG_POS_CART_NOT_EMPTY = "MSG_POS_CART_NOT_EMPTY";
        public const string ERR_POS_NO_MACHINE = "ERR_POS_NO_MACHINE";
        public const string ERR_POS_INVALID_OPENING_CASH = "ERR_POS_INVALID_OPENING_CASH";
        public const string ERR_POS_CART_EMPTY = "ERR_POS_CART_EMPTY";
        public const string ERR_POS_NO_OPEN_SESSION = "ERR_POS_NO_OPEN_SESSION";
        public const string ERR_POS_MACHINE_IN_USE = "ERR_POS_MACHINE_IN_USE";
        public const string ERR_POS_ALREADY_OPENED_SESSION = "ERR_POS_ALREADY_OPENED_SESSION";
        public const string ERR_POS_INVALID_SESSION_ID = "ERR_POS_INVALID_SESSION_ID";
        public const string ERR_POS_INVALID_CLOSING_CASH = "ERR_POS_INVALID_CLOSING_CASH";
        public const string ERR_POS_PAYMENT_INSUFFICIENT = "ERR_POS_PAYMENT_INSUFFICIENT";
        public const string ERR_PAY_NEGATIVE_AMOUNT = "ERR_PAY_NEGATIVE_AMOUNT";
        public const string ERR_POS_RFID_EMPTY = "ERR_POS_RFID_EMPTY";
        public const string ERR_POS_RFID_NOT_FOUND_OR_INACTIVE = "ERR_POS_RFID_NOT_FOUND_OR_INACTIVE";
        public const string ERR_POS_RFID_PAYMENT_NO_ID = "ERR_POS_RFID_PAYMENT_NO_ID";
        public const string ERR_POS_RFID_INSUFFICIENT_BALANCE = "ERR_POS_RFID_INSUFFICIENT_BALANCE";
        public const string ERR_POS_INSUFFICIENT_POINTS = "ERR_POS_INSUFFICIENT_POINTS";
        public const string ERR_POS_PRODUCT_NOT_FOUND = "ERR_POS_PRODUCT_NOT_FOUND";
        public const string ERR_POS_CUSTOMER_NOT_FOUND = "ERR_POS_CUSTOMER_NOT_FOUND";
        public const string ERR_POS_CHECKOUT_FAILED_UNKNOWN = "ERR_POS_CHECKOUT_FAILED_UNKNOWN";
        public const string MSG_POS_MULTI_RESULT = "MSG_POS_MULTI_RESULT";
        
        // Khuyen Mai (POS)
        public const string ERR_KM_EMPTY = "ERR_KM_EMPTY";
        public const string ERR_KM_NOT_FOUND = "ERR_KM_NOT_FOUND";
        public const string ERR_KM_HET_LUOT = "ERR_KM_HET_LUOT";
        public const string ERR_KM_DON_TOI_THIEU = "ERR_KM_DON_TOI_THIEU";
        public const string ERR_KM_DIEU_KIEN = "ERR_KM_DIEU_KIEN";

        // Khuyen Mai (CRUD)
        public const string ERR_KM_MA_RONG = "ERR_KM_MA_RONG";
        public const string ERR_KM_TEN_RONG = "ERR_KM_TEN_RONG";
        public const string ERR_KM_GIA_TRI_SAI = "ERR_KM_GIA_TRI_SAI";
        public const string ERR_KM_PHANTRAM_VUOT = "ERR_KM_PHANTRAM_VUOT";
        public const string ERR_KM_NGAY_SAI = "ERR_KM_NGAY_SAI";
        public const string ERR_KM_TRUNG_MA = "ERR_KM_TRUNG_MA";

        // Refund (Hoan Tra)
        public const string ERR_REFUND_EMPTY_CODE = "ERR_REFUND_EMPTY_CODE";
        public const string ERR_REFUND_NOT_FOUND = "ERR_REFUND_NOT_FOUND";
        public const string ERR_REFUND_NEGATIVE_QTY = "ERR_REFUND_NEGATIVE_QTY";
        public const string ERR_REFUND_OVER_QTY = "ERR_REFUND_OVER_QTY";
        public const string ERR_REFUND_INVALID_NUMBER = "ERR_REFUND_INVALID_NUMBER";
        public const string ERR_HOAN_INVALID_REQUEST = "ERR_HOAN_INVALID_REQUEST";
        public const string ERR_HOAN_NO_ITEMS = "ERR_HOAN_NO_ITEMS";
        public const string ERR_HOAN_REASON_REQUIRED = "ERR_HOAN_REASON_REQUIRED";
        public const string ERR_HOAN_VE_KHONG_DUOC_HOAN = "ERR_HOAN_VE_KHONG_DUOC_HOAN";
        public const string ERR_HOAN_QUA_HAN = "ERR_HOAN_QUA_HAN";

        // Auto-generated keys
        public const string ERR_AUTH_EMPTY = "ERR_AUTH_EMPTY";
        public const string ERR_AUTH_PROFILE_MISSING = "ERR_AUTH_PROFILE_MISSING";
        public const string ERR_AUTH_WRONG_USER_PASS = "ERR_AUTH_WRONG_USER_PASS";
        public const string ERR_CHITIETRONG = "ERR_CHITIETRONG";
        public const string ERR_CHONKHO = "ERR_CHONKHO";
        public const string ERR_CHONNGAY = "ERR_CHONNGAY";
        public const string ERR_COMBO_GIA_AM = "ERR_COMBO_GIA_AM";
        public const string ERR_COMBO_TEN_RONG = "ERR_COMBO_TEN_RONG";
        public const string ERR_CTK_DA_DUYET_TRUOC_DO = "ERR_CTK_DA_DUYET_TRUOC_DO";
        public const string ERR_CTK_KHONG_TON_TAI = "ERR_CTK_KHONG_TON_TAI";
        public const string ERR_CTK_KIEMKE_THIEU_KHONHAP = "ERR_CTK_KIEMKE_THIEU_KHONHAP";
        public const string ERR_CTK_KIEMKE_THIEU_KHOXUAT = "ERR_CTK_KIEMKE_THIEU_KHOXUAT";
        public const string ERR_CTK_MA_RONG = "ERR_CTK_MA_RONG";
        public const string ERR_CTK_SOLUONG_AM = "ERR_CTK_SOLUONG_AM";
        public const string ERR_CTK_THIEU_CHITIET = "ERR_CTK_THIEU_CHITIET";
        public const string ERR_CTK_THIEU_KHO_CHENH_LECH = "ERR_CTK_THIEU_KHO_CHENH_LECH";
        public const string ERR_CTK_THIEU_KHONGUON_KHODICH = "ERR_CTK_THIEU_KHONGUON_KHODICH";
        public const string ERR_CTK_TON_KHONG_DU = "ERR_CTK_TON_KHONG_DU";
        public const string ERR_CTK_TRUNG_KHO_XUAT_NHAP = "ERR_CTK_TRUNG_KHO_XUAT_NHAP";
        public const string ERR_KHO_KHONG_XOA_AO = "ERR_KHO_KHONG_XOA_AO";
        public const string ERR_CUST_EMPTY_KEYWORD = "ERR_CUST_EMPTY_KEYWORD";
        public const string ERR_CUST_NOT_FOUND = "ERR_CUST_NOT_FOUND";
        public const string ERR_CUST_RFID_NOT_FOUND = "ERR_CUST_RFID_NOT_FOUND";
        public const string ERR_DIEM_KHONG_DU = "ERR_DIEM_KHONG_DU";
        public const string ERR_DIEM_SO_AM = "ERR_DIEM_SO_AM";
        public const string ERR_DUP_UNIT_GRID = "ERR_DUP_UNIT_GRID";
        public const string ERR_GIU_SO_LUONG_AM = "ERR_GIU_SO_LUONG_AM";
        public const string ERR_GIU_THIEU_DONHANG = "ERR_GIU_THIEU_DONHANG";
        public const string ERR_GIU_THIEU_SANPHAM = "ERR_GIU_THIEU_SANPHAM";
        public const string ERR_GIU_THOIGIAN_SAI = "ERR_GIU_THOIGIAN_SAI";
        public const string ERR_KHO_DA_CO_GIAO_DICH = "ERR_KHO_DA_CO_GIAO_DICH";
        public const string ERR_KHO_MA_RONG = "ERR_KHO_MA_RONG";
        public const string ERR_KHO_MUC_TON_AM = "ERR_KHO_MUC_TON_AM";
        public const string ERR_KHO_TEN_RONG = "ERR_KHO_TEN_RONG";
        public const string ERR_KHO_TRUNG_MA = "ERR_KHO_TRUNG_MA";

        public const string ERR_CTK_DA_HUY = "ERR_CTK_DA_HUY";
        public const string ERR_CTK_CHUA_DUYET_CAP1 = "ERR_CTK_CHUA_DUYET_CAP1";
        public const string ERR_KH_CCCD_TRUNG = "ERR_KH_CCCD_TRUNG";
        public const string ERR_KH_SDT_RONG = "ERR_KH_SDT_RONG";
        public const string ERR_KH_SDT_TRUNG = "ERR_KH_SDT_TRUNG";
        public const string ERR_KH_TEN_RONG = "ERR_KH_TEN_RONG";
        public const string ERR_LOHANG_HSD_NHO_HON_NSX = "ERR_LOHANG_HSD_NHO_HON_NSX";
        public const string ERR_LOHANG_MA_RONG = "ERR_LOHANG_MA_RONG";
        public const string ERR_LOHANG_NSX_TUONGLAI = "ERR_LOHANG_NSX_TUONGLAI";
        public const string ERR_LOHANG_THIEU_SANPHAM = "ERR_LOHANG_THIEU_SANPHAM";
        public const string ERR_NAP_MAN_HINH = "ERR_NAP_MAN_HINH";
        public const string ERR_PHANQUYEN_INVALID_ROLE = "ERR_PHANQUYEN_INVALID_ROLE";
        public const string ERR_POS_MACHINE_REQUIRED = "ERR_POS_MACHINE_REQUIRED";
        public const string ERR_RENTAL_NO_PRICE = "ERR_RENTAL_NO_PRICE";
        public const string ERR_RFID_KHONG_TIM_THAY = "ERR_RFID_KHONG_TIM_THAY";
        public const string ERR_RFID_LUONG_KHONG_HOP_LE = "ERR_RFID_LUONG_KHONG_HOP_LE";
        public const string ERR_RFID_MA_RONG = "ERR_RFID_MA_RONG";
        public const string ERR_RFID_MA_TRUNG = "ERR_RFID_MA_TRUNG";
        public const string ERR_RFID_TRANG_THAI_TRUNG = "ERR_RFID_TRANG_THAI_TRUNG";
        public const string ERR_SYSTEM_FAIL = "ERR_SYSTEM_FAIL";
        public const string ERR_VI_CHUA_CO = "ERR_VI_CHUA_CO";
        public const string ERR_VI_DA_CO = "ERR_VI_DA_CO";
        public const string ERR_VI_SO_TIEN_AM = "ERR_VI_SO_TIEN_AM";
        public const string MSG_COMBO_TYLE_CHUA_DU = "MSG_COMBO_TYLE_CHUA_DU";
        public const string MSG_CONFIRM_LOGOUT = "MSG_CONFIRM_LOGOUT";
        public const string MSG_DUYETTHANHCONG = "MSG_DUYETTHANHCONG";
        public const string MSG_HUYTHANHCONG = "MSG_HUYTHANHCONG";
        public const string MSG_KH_NHAP_DIEM = "MSG_KH_NHAP_DIEM";
        public const string MSG_KH_NHAP_LYDO = "MSG_KH_NHAP_LYDO";
        public const string MSG_KH_NHAP_MA_THE = "MSG_KH_NHAP_MA_THE";
        public const string MSG_KM_APPLIED = "MSG_KM_APPLIED";
        public const string MSG_LOI = "MSG_LOI";
        public const string MSG_LUUTHATBAI = "MSG_LUUTHATBAI";
        public const string MSG_POS_TOAST_CHANGE = "MSG_POS_TOAST_CHANGE";
        public const string MSG_POS_TOAST_TICKETS = "MSG_POS_TOAST_TICKETS";
        public const string MSG_POS_TOAST_TITLE = "MSG_POS_TOAST_TITLE";
        public const string MSG_REFUND_CONFIRM = "MSG_REFUND_CONFIRM";
        public const string MSG_WARN_UNSAVED = "MSG_WARN_UNSAVED";
        public const string MSG_XAC_NHAN_XOA = "MSG_XAC_NHAN_XOA";

        public const string PARTIAL_TICKET_SCANNED = "PARTIAL_TICKET_SCANNED";
        
        // Success/Status (POS)
        public const string MSG_POS_CHECKOUT_SUCCESS = "MSG_POS_CHECKOUT_SUCCESS";
        public const string MSG_POS_SESSION_OPENED = "MSG_POS_SESSION_OPENED";
        public const string MSG_POS_SESSION_CLOSED = "MSG_POS_SESSION_CLOSED";
        public const string MSG_REFUND_SUCCESS = "MSG_REFUND_SUCCESS";
        public const string MSG_REFUND_FAIL = "MSG_REFUND_FAIL";

        // General Messages
        public const string MSG_LUU_THANH_CONG = "MSG_LUU_THANH_CONG";
        public const string MSG_LUU_THAT_BAI = "MSG_LUU_THAT_BAI";
        public const string MSG_UNSAVED = "MSG_UNSAVED";
        public const string MSG_XOA_THANH_CONG = "MSG_XOA_THANH_CONG";

        // Thuê đồ — Giao
        public const string ERR_RENTAL_CART_EMPTY = "ERR_RENTAL_CART_EMPTY";
        public const string ERR_RENTAL_RFID_REQUIRED = "ERR_RENTAL_RFID_REQUIRED";
        public const string ERR_RENTAL_VI_KHONG_DU = "ERR_RENTAL_VI_KHONG_DU";
        public const string ERR_RENTAL_TAISAN_DANG_THUE = "ERR_RENTAL_TAISAN_DANG_THUE";
        public const string ERR_RENTAL_TAISAN_KHONG_TIM_THAY = "ERR_RENTAL_TAISAN_KHONG_TIM_THAY";
        public const string MSG_RENTAL_CHECKOUT_SUCCESS = "MSG_RENTAL_CHECKOUT_SUCCESS";
        public const string MSG_RENTAL_CONFIRM = "MSG_RENTAL_CONFIRM";

        // Thuê đồ — Nhận trả
        public const string ERR_RETURN_NO_ITEMS = "ERR_RETURN_NO_ITEMS";
        public const string ERR_RETURN_SL_VUOT_QUA = "ERR_RETURN_SL_VUOT_QUA";
        public const string ERR_RETURN_DONHANG_KHONG_TIM_THAY = "ERR_RETURN_DONHANG_KHONG_TIM_THAY";
        public const string ERR_RETURN_KHACH_KHONG_CO_DO = "ERR_RETURN_KHACH_KHONG_CO_DO";
        public const string ERR_RETURN_PHAT_CHUA_NHAP = "ERR_RETURN_PHAT_CHUA_NHAP";
        public const string MSG_RETURN_CONFIRM = "MSG_RETURN_CONFIRM";
        public const string MSG_RETURN_SUCCESS = "MSG_RETURN_SUCCESS";

        public const string RENTAL_PENALTY_EXCEED = "RENTAL_PENALTY_EXCEED";
        public const string RENTAL_SURCHARGE_INVOICE = "RENTAL_SURCHARGE_INVOICE";
        public const string MSG_COPIED_TO_CLIPBOARD = "MSG_COPIED_TO_CLIPBOARD";
        
        public const string RENTAL_ENTER_QUANTITY = "RENTAL_ENTER_QUANTITY";
        public const string RENTAL_QUANTITY_TITLE = "RENTAL_QUANTITY_TITLE";
        public const string ERR_INVALID_QUANTITY = "ERR_INVALID_QUANTITY";
        public const string MSG_PRINT_INVOICE_NOT_READY = "MSG_PRINT_INVOICE_NOT_READY";

        // Lưu Trú — Check-out
        public const string ERR_SYS_PHU_THU_KHONG_TON_TAI = "ERR_SYS_PHU_THU_KHONG_TON_TAI";
        public const string ERR_CHECKOUT_TIEN_KHONG_DU = "ERR_CHECKOUT_TIEN_KHONG_DU";

        // Kiểm soát cổng (Soát vé)
        public const string ERR_GATE_MA_RONG = "ERR_GATE_MA_RONG";
        public const string GATE_HOP_LE = "GATE_HOP_LE";
        public const string GATE_SAI_KHU_VUC = "GATE_SAI_KHU_VUC";
        public const string GATE_HET_LUOT = "GATE_HET_LUOT";
        public const string GATE_KHONG_TIM_THAY = "GATE_KHONG_TIM_THAY";
        public const string GATE_SAI_TRO_CHOI = "GATE_SAI_TRO_CHOI";
        public const string GATE_VE_HET_HAN = "GATE_VE_HET_HAN";
        public const string GATE_VE_DA_HUY = "GATE_VE_DA_HUY";
    }

    // Trạng thái tài sản cho thuê (DanhMuc.TaiSanChoThue.TrangThai)
    public static class TrangThaiTaiSan
    {
        public const string SanSang = "SanSang";
        public const string DangThue = "DangThue";
        public const string HuHong = "HuHong";
        public const string DangBaoTri = "DangBaoTri";
    }

    // Trạng thái tiền cọc (BanHang.ThueDoChiTiet.TrangThaiCoc)
    public static class TrangThaiCoc
    {
        public const string DaCoc = "DaCoc";
        public const string DaHoan = "DaHoan";
        public const string KhongHoan = "KhongHoan";
    }

    // Trạng thái phiên thuê đồ (BanHang.ThueDoChiTiet.TrangThai)
    public static class TrangThaiThueDo
    {
        public const string DangThue = "DangThue";
        public const string DaTra = "DaTra";
        public const string MatDo = "MatDo";
    }

    } 
}
