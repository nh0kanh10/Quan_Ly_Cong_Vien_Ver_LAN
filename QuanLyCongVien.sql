-- ============================================================
-- HỆ THỐNG QUẢN LÝ CÔNG VIÊN VUI CHƠI GIẢI TRÍ
-- ============================================================

CREATE DATABASE QuanLyCongVien;
GO

USE QuanLyCongVien;
GO

-- ************************************************************
-- TẠO BẢNG 
-- ************************************************************

-- Tài khoản đăng nhập
CREATE TABLE TaiKhoan (
MaTaiKhoan INT IDENTITY(1,1),
TenDangNhap NVARCHAR(50) NOT NULL,
MatKhau NVARCHAR(256) NOT NULL,
HoTen NVARCHAR(100) NOT NULL,
VaiTro NVARCHAR(20) NOT NULL,
TrangThai BIT NOT NULL,
NgayTao DATETIME NOT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- Khu vực trong công viên
CREATE TABLE KhuVuc (
MaKhuVuc INT IDENTITY(1,1),
MaCode NVARCHAR(10) NOT NULL,
TenKhuVuc NVARCHAR(100) NOT NULL,
MoTa NVARCHAR(500) NULL,
TrangThai NVARCHAR(20) NOT NULL,
NgayTao DATETIME NOT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- Trò chơi / Thiết bị giải trí
CREATE TABLE TroChoi (
MaTroChoi INT IDENTITY(1,1),
MaCode NVARCHAR(10) NOT NULL,
TenTroChoi NVARCHAR(150) NOT NULL,
MaKhuVuc INT NOT NULL,
LoaiTroChoi NVARCHAR(50) NOT NULL,
SucChua INT NOT NULL,
TuoiToiThieu INT NOT NULL,
ChieuCaoToiThieu INT NOT NULL,
ThoiGianLuot INT NOT NULL,
MoTa NVARCHAR(500) NULL,
TrangThai NVARCHAR(20) NOT NULL,
NgayTao DATETIME NOT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- Loại vé
CREATE TABLE LoaiVe (
MaLoaiVe INT IDENTITY(1,1),
MaCode NVARCHAR(10) NOT NULL,
TenLoaiVe NVARCHAR(100) NOT NULL,
GiaVe DECIMAL(18,2) NOT NULL,
GiaCuoiTuan DECIMAL(18,2) NULL,
DoiTuong NVARCHAR(50) NOT NULL,
LaCombo BIT NOT NULL,
TrangThai NVARCHAR(20) NOT NULL,
NgayTao DATETIME NOT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- Chi tiết combo (loại vé nào chứa dịch vụ/vé con nào)
CREATE TABLE ChiTietCombo (
MaChiTietCombo INT IDENTITY(1,1),
MaLoaiVeCha INT NOT NULL,
MaLoaiVeCon INT NOT NULL,
SoLuotChoPhep INT NOT NULL,
NgayTao DATETIME NOT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- Dịch vụ (ăn uống, quà lưu niệm, cho thuê)
CREATE TABLE DichVu (
MaDichVu INT IDENTITY(1,1),
MaCode NVARCHAR(10) NOT NULL,
TenDichVu NVARCHAR(150) NOT NULL,
DanhMuc NVARCHAR(50) NOT NULL,
MaKhuVuc INT NULL,
GiaBan DECIMAL(18,2) NOT NULL,
SoLuongTon INT NOT NULL,
DonViTinh NVARCHAR(20) NOT NULL,
TrangThai NVARCHAR(20) NOT NULL,
NgayTao DATETIME NOT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- Nhân viên
CREATE TABLE NhanVien (
MaNhanVien INT IDENTITY(1,1),
MaCode NVARCHAR(10) NOT NULL,
HoTen NVARCHAR(100) NOT NULL,
SoDienThoai NVARCHAR(15) NULL,
Email NVARCHAR(100) NULL,
ChucVu NVARCHAR(50) NOT NULL,
MaKhuVuc INT NULL,
NgayVaoLam DATE NOT NULL,
TrangThai BIT NOT NULL,
NgayTao DATETIME NOT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- Ca làm việc
CREATE TABLE CaLam (
MaCaLam INT IDENTITY(1,1),
TenCaLam NVARCHAR(50) NOT NULL,
GioBatDau TIME NOT NULL,
GioKetThuc TIME NOT NULL,
NgayTao DATETIME NOT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- Phân ca nhân viên
CREATE TABLE PhanCa (
MaPhanCa INT IDENTITY(1,1),
MaNhanVien INT NOT NULL,
MaCaLam INT NOT NULL,
NgayLam DATE NOT NULL,
GhiChu NVARCHAR(200) NULL,
NgayTao DATETIME NOT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- Khách hàng
CREATE TABLE KhachHang (
MaKhachHang INT IDENTITY(1,1),
HoTen NVARCHAR(100) NOT NULL,
SoDienThoai NVARCHAR(15) NULL,
Email NVARCHAR(100) NULL,
MaThanhVien NVARCHAR(20) NULL,
NgayHetHanThe DATE NULL,
TongChiTieu DECIMAL(18,2) NOT NULL,
NgayTao DATETIME NOT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- Hóa đơn
CREATE TABLE HoaDon (
MaHoaDon INT IDENTITY(1,1),
MaCode NVARCHAR(20) NOT NULL,
MaKhachHang INT NULL,
MaNhanVien INT NOT NULL,
NgayLap DATETIME NOT NULL,
TongTien DECIMAL(18,2) NOT NULL,
TienGiamGia DECIMAL(18,2) NOT NULL,
ThanhToan DECIMAL(18,2) NOT NULL,
PhuongThuc NVARCHAR(20) NOT NULL,
GhiChu NVARCHAR(500) NULL,
MaKhuyenMai INT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- Chi tiết hóa đơn
CREATE TABLE ChiTietHoaDon (
MaChiTiet INT IDENTITY(1,1),
MaHoaDon INT NOT NULL,
LoaiSanPham NVARCHAR(10) NOT NULL,
MaSanPham INT NOT NULL,
TenSanPham NVARCHAR(150) NOT NULL,
DonGia DECIMAL(18,2) NOT NULL,
SoLuong INT NOT NULL,
ThanhTien DECIMAL(18,2) NOT NULL,
NgayTao DATETIME NOT NULL
);
GO

-- Sự kiện
CREATE TABLE SuKien (
MaSuKien INT IDENTITY(1,1),
TenSuKien NVARCHAR(200) NOT NULL,
MoTa NVARCHAR(1000) NULL,
NgayBatDau DATE NOT NULL,
NgayKetThuc DATE NOT NULL,
TrangThai NVARCHAR(20) NOT NULL,
NgayTao DATETIME NOT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- Khuyến mãi
CREATE TABLE KhuyenMai (
MaKhuyenMai INT IDENTITY(1,1),
TenKhuyenMai NVARCHAR(200) NOT NULL,
LoaiGiamGia NVARCHAR(20) NOT NULL,
GiaTriGiam DECIMAL(18,2) NOT NULL,
DonToiThieu DECIMAL(18,2) NOT NULL,
NgayBatDau DATE NOT NULL,
NgayKetThuc DATE NOT NULL,
MaSuKien INT NULL,
TrangThai BIT NOT NULL,
NgayTao DATETIME NOT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- Bảo trì thiết bị
CREATE TABLE BaoTri (
MaBaoTri INT IDENTITY(1,1),
MaTroChoi INT NOT NULL,
LoaiBaoTri NVARCHAR(20) NOT NULL,
MoTa NVARCHAR(500) NOT NULL,
NgayDuKien DATE NULL,
NgayHoanThanh DATE NULL,
ChiPhi DECIMAL(18,2) NOT NULL,
TrangThai NVARCHAR(20) NOT NULL,
NguoiThucHien INT NULL,
NgayTao DATETIME NOT NULL,
NgayCapNhat DATETIME NULL
);
GO

-- ************************************************************
-- RÀNG BUỘC KHÓA CHÍNH 
-- ************************************************************

ALTER TABLE TaiKhoan ADD CONSTRAINT PK_TaiKhoan PRIMARY KEY (MaTaiKhoan);
ALTER TABLE KhuVuc ADD CONSTRAINT PK_KhuVuc PRIMARY KEY (MaKhuVuc);
ALTER TABLE TroChoi ADD CONSTRAINT PK_TroChoi PRIMARY KEY (MaTroChoi);
ALTER TABLE LoaiVe ADD CONSTRAINT PK_LoaiVe PRIMARY KEY (MaLoaiVe);
ALTER TABLE ChiTietCombo ADD CONSTRAINT PK_ChiTietCombo PRIMARY KEY (MaChiTietCombo);
ALTER TABLE DichVu ADD CONSTRAINT PK_DichVu PRIMARY KEY (MaDichVu);
ALTER TABLE NhanVien ADD CONSTRAINT PK_NhanVien PRIMARY KEY (MaNhanVien);
ALTER TABLE CaLam ADD CONSTRAINT PK_CaLam PRIMARY KEY (MaCaLam);
ALTER TABLE PhanCa ADD CONSTRAINT PK_PhanCa PRIMARY KEY (MaPhanCa);
ALTER TABLE KhachHang ADD CONSTRAINT PK_KhachHang PRIMARY KEY (MaKhachHang);
ALTER TABLE HoaDon ADD CONSTRAINT PK_HoaDon PRIMARY KEY (MaHoaDon);
ALTER TABLE ChiTietHoaDon ADD CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY (MaChiTiet);
ALTER TABLE SuKien ADD CONSTRAINT PK_SuKien PRIMARY KEY (MaSuKien);
ALTER TABLE KhuyenMai ADD CONSTRAINT PK_KhuyenMai PRIMARY KEY (MaKhuyenMai);
ALTER TABLE BaoTri ADD CONSTRAINT PK_BaoTri PRIMARY KEY (MaBaoTri);
GO

-- ************************************************************
-- RÀNG BUỘC KHÓA NGOẠI 
-- ************************************************************

-- Trò chơi - Khu vực
ALTER TABLE TroChoi
ADD CONSTRAINT FK_TroChoi_KhuVuc
FOREIGN KEY (MaKhuVuc) REFERENCES KhuVuc(MaKhuVuc);
GO

-- Dịch vụ - Khu vực (quầy hàng nằm ở khu nào)
ALTER TABLE DichVu
ADD CONSTRAINT FK_DichVu_KhuVuc
FOREIGN KEY (MaKhuVuc) REFERENCES KhuVuc(MaKhuVuc);
GO

-- Chi tiết combo - Loại vé cha
ALTER TABLE ChiTietCombo
ADD CONSTRAINT FK_ChiTietCombo_VeCha
FOREIGN KEY (MaLoaiVeCha) REFERENCES LoaiVe(MaLoaiVe);
GO

-- Chi tiết combo - Loại vé con
ALTER TABLE ChiTietCombo
ADD CONSTRAINT FK_ChiTietCombo_VeCon
FOREIGN KEY (MaLoaiVeCon) REFERENCES LoaiVe(MaLoaiVe);
GO

-- Nhân viên - Khu vực (phụ trách)
ALTER TABLE NhanVien
ADD CONSTRAINT FK_NhanVien_KhuVuc
FOREIGN KEY (MaKhuVuc) REFERENCES KhuVuc(MaKhuVuc);
GO

-- Phân ca - Nhân viên
ALTER TABLE PhanCa
ADD CONSTRAINT FK_PhanCa_NhanVien
FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien);
GO

-- Phân ca - Ca làm
ALTER TABLE PhanCa
ADD CONSTRAINT FK_PhanCa_CaLam
FOREIGN KEY (MaCaLam) REFERENCES CaLam(MaCaLam);
GO

-- Hóa đơn - Khách hàng
ALTER TABLE HoaDon
ADD CONSTRAINT FK_HoaDon_KhachHang
FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang);
GO

-- Hóa đơn - Nhân viên (người bán)
ALTER TABLE HoaDon
ADD CONSTRAINT FK_HoaDon_NhanVien
FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien);
GO

-- Hóa đơn - Khuyến mãi
ALTER TABLE HoaDon
ADD CONSTRAINT FK_HoaDon_KhuyenMai
FOREIGN KEY (MaKhuyenMai) REFERENCES KhuyenMai(MaKhuyenMai);
GO

-- Chi tiết hóa đơn - Hóa đơn
ALTER TABLE ChiTietHoaDon
ADD CONSTRAINT FK_ChiTietHD_HoaDon
FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon);
GO

-- Khuyến mãi - Sự kiện
ALTER TABLE KhuyenMai
ADD CONSTRAINT FK_KhuyenMai_SuKien
FOREIGN KEY (MaSuKien) REFERENCES SuKien(MaSuKien);
GO

-- Bảo trì - Trò chơi
ALTER TABLE BaoTri
ADD CONSTRAINT FK_BaoTri_TroChoi
FOREIGN KEY (MaTroChoi) REFERENCES TroChoi(MaTroChoi);
GO

-- Bảo trì - Nhân viên (người thực hiện)
ALTER TABLE BaoTri
ADD CONSTRAINT FK_BaoTri_NhanVien
FOREIGN KEY (NguoiThucHien) REFERENCES NhanVien(MaNhanVien);
GO

-- ************************************************************
-- RÀNG BUỘC UNIQUE
-- ************************************************************

ALTER TABLE TaiKhoan ADD CONSTRAINT UQ_TaiKhoan_TenDN UNIQUE (TenDangNhap);

ALTER TABLE KhuVuc ADD CONSTRAINT UQ_KhuVuc_MaCode UNIQUE (MaCode);
ALTER TABLE KhuVuc ADD CONSTRAINT UQ_KhuVuc_TenKV UNIQUE (TenKhuVuc);

ALTER TABLE TroChoi ADD CONSTRAINT UQ_TroChoi_MaCode UNIQUE (MaCode);

ALTER TABLE LoaiVe ADD CONSTRAINT UQ_LoaiVe_MaCode UNIQUE (MaCode);

ALTER TABLE DichVu ADD CONSTRAINT UQ_DichVu_MaCode UNIQUE (MaCode);

ALTER TABLE NhanVien ADD CONSTRAINT UQ_NhanVien_MaCode UNIQUE (MaCode);

ALTER TABLE KhachHang ADD CONSTRAINT UQ_KhachHang_SDT UNIQUE (SoDienThoai);
ALTER TABLE KhachHang ADD CONSTRAINT UQ_KhachHang_ThanhVien UNIQUE (MaThanhVien);

ALTER TABLE HoaDon ADD CONSTRAINT UQ_HoaDon_MaCode UNIQUE (MaCode);

ALTER TABLE PhanCa ADD CONSTRAINT UQ_PhanCa_NV_Ca_Ngay UNIQUE (MaNhanVien, MaCaLam, NgayLam);
GO

-- ************************************************************
-- RÀNG BUỘC CHECK
-- ************************************************************

-- Tài khoản: Vai trò chỉ Admin hoặc NhanVien
ALTER TABLE TaiKhoan
ADD CONSTRAINT CK_TaiKhoan_VaiTro
CHECK (VaiTro IN (N'Admin', N'NhanVien'));
GO

-- Khu vực: Trạng thái hợp lệ
ALTER TABLE KhuVuc
ADD CONSTRAINT CK_KhuVuc_TrangThai
CHECK (TrangThai IN (N'Hoạt động', N'Bảo trì', N'Ngừng hoạt động'));
GO

-- Trò chơi: Sức chứa > 0
ALTER TABLE TroChoi
ADD CONSTRAINT CK_TroChoi_SucChua
CHECK (SucChua > 0);
GO

-- Trò chơi: Tuổi tối thiểu >= 0
ALTER TABLE TroChoi
ADD CONSTRAINT CK_TroChoi_Tuoi
CHECK (TuoiToiThieu >= 0);
GO

-- Trò chơi: Chiều cao tối thiểu >= 0 (đơn vị cm, 0 = không giới hạn)
ALTER TABLE TroChoi
ADD CONSTRAINT CK_TroChoi_ChieuCao
CHECK (ChieuCaoToiThieu >= 0);
GO

-- Trò chơi: Thời gian lượt > 0
ALTER TABLE TroChoi
ADD CONSTRAINT CK_TroChoi_ThoiGian
CHECK (ThoiGianLuot > 0);
GO

-- Trò chơi: Trạng thái hợp lệ
ALTER TABLE TroChoi
ADD CONSTRAINT CK_TroChoi_TrangThai
CHECK (TrangThai IN (N'Hoạt động', N'Bảo trì', N'Ngừng hoạt động'));
GO

-- Loại vé: Giá >= 0
ALTER TABLE LoaiVe
ADD CONSTRAINT CK_LoaiVe_Gia
CHECK (GiaVe >= 0);
GO

-- Loại vé: Giá cuối tuần >= 0 (nếu có)
ALTER TABLE LoaiVe
ADD CONSTRAINT CK_LoaiVe_GiaCuoiTuan
CHECK (GiaCuoiTuan IS NULL OR GiaCuoiTuan >= 0);
GO

-- Loại vé: Trạng thái
ALTER TABLE LoaiVe
ADD CONSTRAINT CK_LoaiVe_TrangThai
CHECK (TrangThai IN (N'Hoạt động', N'Ngừng bán'));
GO

-- Chi tiết combo: Số lượt > 0 hoặc -1 (vô hạn)
ALTER TABLE ChiTietCombo
ADD CONSTRAINT CK_ChiTietCombo_SoLuot
CHECK (SoLuotChoPhep > 0 OR SoLuotChoPhep = -1);
GO

-- Dịch vụ: Giá >= 0
ALTER TABLE DichVu
ADD CONSTRAINT CK_DichVu_Gia
CHECK (GiaBan >= 0);
GO

-- Dịch vụ: Số lượng tồn >= 0
ALTER TABLE DichVu
ADD CONSTRAINT CK_DichVu_SoLuong
CHECK (SoLuongTon >= 0);
GO

-- Dịch vụ: Danh mục hợp lệ
ALTER TABLE DichVu
ADD CONSTRAINT CK_DichVu_DanhMuc
CHECK (DanhMuc IN (N'Ăn uống', N'Lưu niệm', N'Cho thuê'));
GO

-- Chi tiết hóa đơn: Loại sản phẩm
ALTER TABLE ChiTietHoaDon
ADD CONSTRAINT CK_ChiTietHD_LoaiSP
CHECK (LoaiSanPham IN (N'Ve', N'DichVu'));
GO

-- Chi tiết hóa đơn: Số lượng > 0
ALTER TABLE ChiTietHoaDon
ADD CONSTRAINT CK_ChiTietHD_SoLuong
CHECK (SoLuong > 0);
GO

-- Chi tiết hóa đơn: Đơn giá >= 0
ALTER TABLE ChiTietHoaDon
ADD CONSTRAINT CK_ChiTietHD_DonGia
CHECK (DonGia >= 0);
GO

-- Hóa đơn: Tổng tiền >= 0
ALTER TABLE HoaDon
ADD CONSTRAINT CK_HoaDon_TongTien
CHECK (TongTien >= 0);
GO

-- Hóa đơn: Tiền giảm giá >= 0
ALTER TABLE HoaDon
ADD CONSTRAINT CK_HoaDon_GiamGia
CHECK (TienGiamGia >= 0);
GO

-- Hóa đơn: Thanh toán >= 0
ALTER TABLE HoaDon
ADD CONSTRAINT CK_HoaDon_ThanhToan
CHECK (ThanhToan >= 0);
GO

-- Hóa đơn: Phương thức thanh toán
ALTER TABLE HoaDon
ADD CONSTRAINT CK_HoaDon_PhuongThuc
CHECK (PhuongThuc IN (N'Tiền mặt', N'Chuyển khoản', N'Thẻ'));
GO

-- Khuyến mãi: Loại giảm giá
ALTER TABLE KhuyenMai
ADD CONSTRAINT CK_KhuyenMai_Loai
CHECK (LoaiGiamGia IN (N'PhanTram', N'SoTien'));
GO

-- Khuyến mãi: Giá trị giảm > 0
ALTER TABLE KhuyenMai
ADD CONSTRAINT CK_KhuyenMai_GiaTri
CHECK (GiaTriGiam > 0);
GO

-- Khuyến mãi: Đơn tối thiểu >= 0
ALTER TABLE KhuyenMai
ADD CONSTRAINT CK_KhuyenMai_DonMin
CHECK (DonToiThieu >= 0);
GO

-- Khuyến mãi: Ngày kết thúc >= ngày bắt đầu
ALTER TABLE KhuyenMai
ADD CONSTRAINT CK_KhuyenMai_Ngay
CHECK (NgayKetThuc >= NgayBatDau);
GO

-- Sự kiện: Ngày kết thúc >= ngày bắt đầu
ALTER TABLE SuKien
ADD CONSTRAINT CK_SuKien_Ngay
CHECK (NgayKetThuc >= NgayBatDau);
GO

-- Sự kiện: Trạng thái
ALTER TABLE SuKien
ADD CONSTRAINT CK_SuKien_TrangThai
CHECK (TrangThai IN (N'Sắp diễn ra', N'Đang diễn ra', N'Kết thúc'));
GO

-- Bảo trì: Loại bảo trì
ALTER TABLE BaoTri
ADD CONSTRAINT CK_BaoTri_Loai
CHECK (LoaiBaoTri IN (N'Định kỳ', N'Khẩn cấp'));
GO

-- Bảo trì: Chi phí >= 0
ALTER TABLE BaoTri
ADD CONSTRAINT CK_BaoTri_ChiPhi
CHECK (ChiPhi >= 0);
GO

-- Bảo trì: Trạng thái
ALTER TABLE BaoTri
ADD CONSTRAINT CK_BaoTri_TrangThai
CHECK (TrangThai IN (N'Chờ xử lý', N'Đang thực hiện', N'Hoàn thành'));
GO

-- Khách hàng: Tổng chi tiêu >= 0
ALTER TABLE KhachHang
ADD CONSTRAINT CK_KhachHang_TongChi
CHECK (TongChiTieu >= 0);
GO

-- ************************************************************
-- GIÁ TRỊ MẶC ĐỊNH
-- ************************************************************

ALTER TABLE TaiKhoan ADD CONSTRAINT DF_TaiKhoan_TrangThai DEFAULT 1 FOR TrangThai;
ALTER TABLE TaiKhoan ADD CONSTRAINT DF_TaiKhoan_NgayTao DEFAULT GETDATE() FOR NgayTao;

ALTER TABLE KhuVuc ADD CONSTRAINT DF_KhuVuc_TrangThai DEFAULT N'Hoạt động' FOR TrangThai;
ALTER TABLE KhuVuc ADD CONSTRAINT DF_KhuVuc_NgayTao DEFAULT GETDATE() FOR NgayTao;

ALTER TABLE TroChoi ADD CONSTRAINT DF_TroChoi_SucChua DEFAULT 1 FOR SucChua;
ALTER TABLE TroChoi ADD CONSTRAINT DF_TroChoi_Tuoi DEFAULT 0 FOR TuoiToiThieu;
ALTER TABLE TroChoi ADD CONSTRAINT DF_TroChoi_ChieuCao DEFAULT 0 FOR ChieuCaoToiThieu;
ALTER TABLE TroChoi ADD CONSTRAINT DF_TroChoi_ThoiGian DEFAULT 5 FOR ThoiGianLuot;
ALTER TABLE TroChoi ADD CONSTRAINT DF_TroChoi_TrangThai DEFAULT N'Hoạt động' FOR TrangThai;
ALTER TABLE TroChoi ADD CONSTRAINT DF_TroChoi_NgayTao DEFAULT GETDATE() FOR NgayTao;

ALTER TABLE LoaiVe ADD CONSTRAINT DF_LoaiVe_LaCombo DEFAULT 0 FOR LaCombo;
ALTER TABLE LoaiVe ADD CONSTRAINT DF_LoaiVe_TrangThai DEFAULT N'Hoạt động' FOR TrangThai;
ALTER TABLE LoaiVe ADD CONSTRAINT DF_LoaiVe_NgayTao DEFAULT GETDATE() FOR NgayTao;

ALTER TABLE DichVu ADD CONSTRAINT DF_DichVu_SoLuong DEFAULT 0 FOR SoLuongTon;
ALTER TABLE DichVu ADD CONSTRAINT DF_DichVu_DonVi DEFAULT N'Cái' FOR DonViTinh;
ALTER TABLE DichVu ADD CONSTRAINT DF_DichVu_TrangThai DEFAULT N'Hoạt động' FOR TrangThai;
ALTER TABLE DichVu ADD CONSTRAINT DF_DichVu_NgayTao DEFAULT GETDATE() FOR NgayTao;

ALTER TABLE NhanVien ADD CONSTRAINT DF_NhanVien_TrangThai DEFAULT 1 FOR TrangThai;
ALTER TABLE NhanVien ADD CONSTRAINT DF_NhanVien_NgayTao DEFAULT GETDATE() FOR NgayTao;

ALTER TABLE KhachHang ADD CONSTRAINT DF_KhachHang_TongChi DEFAULT 0 FOR TongChiTieu;
ALTER TABLE KhachHang ADD CONSTRAINT DF_KhachHang_NgayTao DEFAULT GETDATE() FOR NgayTao;

ALTER TABLE HoaDon ADD CONSTRAINT DF_HoaDon_NgayLap DEFAULT GETDATE() FOR NgayLap;
ALTER TABLE HoaDon ADD CONSTRAINT DF_HoaDon_GiamGia DEFAULT 0 FOR TienGiamGia;
ALTER TABLE HoaDon ADD CONSTRAINT DF_HoaDon_PhuongThuc DEFAULT N'Tiền mặt' FOR PhuongThuc;

ALTER TABLE SuKien ADD CONSTRAINT DF_SuKien_TrangThai DEFAULT N'Sắp diễn ra' FOR TrangThai;
ALTER TABLE SuKien ADD CONSTRAINT DF_SuKien_NgayTao DEFAULT GETDATE() FOR NgayTao;

ALTER TABLE KhuyenMai ADD CONSTRAINT DF_KhuyenMai_DonMin DEFAULT 0 FOR DonToiThieu;
ALTER TABLE KhuyenMai ADD CONSTRAINT DF_KhuyenMai_TrangThai DEFAULT 1 FOR TrangThai;
ALTER TABLE KhuyenMai ADD CONSTRAINT DF_KhuyenMai_NgayTao DEFAULT GETDATE() FOR NgayTao;

ALTER TABLE BaoTri ADD CONSTRAINT DF_BaoTri_ChiPhi DEFAULT 0 FOR ChiPhi;
ALTER TABLE BaoTri ADD CONSTRAINT DF_BaoTri_TrangThai DEFAULT N'Chờ xử lý' FOR TrangThai;
ALTER TABLE BaoTri ADD CONSTRAINT DF_BaoTri_NgayTao DEFAULT GETDATE() FOR NgayTao;

ALTER TABLE ChiTietCombo ADD CONSTRAINT DF_ChiTietCombo_NgayTao DEFAULT GETDATE() FOR NgayTao;

ALTER TABLE CaLam ADD CONSTRAINT DF_CaLam_NgayTao DEFAULT GETDATE() FOR NgayTao;

ALTER TABLE PhanCa ADD CONSTRAINT DF_PhanCa_NgayTao DEFAULT GETDATE() FOR NgayTao;

ALTER TABLE ChiTietHoaDon ADD CONSTRAINT DF_ChiTietHD_NgayTao DEFAULT GETDATE() FOR NgayTao;
GO
