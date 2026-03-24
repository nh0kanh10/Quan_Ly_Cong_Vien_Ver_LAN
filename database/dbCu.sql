
CREATE DATABASE QuanLyCongVien;
GO

USE QuanLyCongVien;
GO

-- ************************************************************
-- 1) TẠO BẢNG
-- ************************************************************

CREATE TABLE TaiKhoan (
    MaTaiKhoan INT IDENTITY(1,1) NOT NULL,
    TenDangNhap NVARCHAR(50) NOT NULL,
    MatKhau NVARCHAR(256) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    VaiTro NVARCHAR(20) NOT NULL,
    MaNhanVien INT NULL, -- Liên kết với hồ sơ nhân viên để biết Khu vực/Chức vụ
    TrangThai BIT NOT NULL,
    NgayTao DATETIME NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE DanhMucDichVu (
    MaDanhMuc INT IDENTITY(1,1) PRIMARY KEY,
    TenDanhMuc NVARCHAR(100) NOT NULL,
    KieuLogic NVARCHAR(20) NOT NULL, -- 'SALE' (Bán đứt), 'TIME' (Tính giờ), 'DEPOSIT' (Tiền cọc)
    MoTa NVARCHAR(250) NULL,
    Icon NVARCHAR(50) NULL
);
GO

CREATE TABLE KhuVuc (
    MaKhuVuc INT IDENTITY(1,1) NOT NULL,
    MaCode NVARCHAR(10) NOT NULL,
    TenKhuVuc NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(500) NULL,
    TrangThai NVARCHAR(20) NOT NULL,
    NgayTao DATETIME NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE TroChoi (
    MaTroChoi INT IDENTITY(1,1) NOT NULL,
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

CREATE TABLE LoaiVe (
    MaLoaiVe INT IDENTITY(1,1) NOT NULL,
    MaCode NVARCHAR(10) NOT NULL,
    TenLoaiVe NVARCHAR(100) NOT NULL,
    GiaVe DECIMAL(18,2) NOT NULL,
    DoiTuong NVARCHAR(50) NOT NULL,
    LaCombo BIT NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL,
    NgayTao DATETIME NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE ChiTietCombo (
    MaChiTietCombo INT IDENTITY(1,1) NOT NULL,
    MaLoaiVeCha INT NOT NULL,
    MaLoaiVeCon INT NOT NULL,
    SoLuotChoPhep INT NOT NULL,
    NgayTao DATETIME NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE DichVu (
    MaDichVu INT IDENTITY(1,1) NOT NULL,
    MaCode NVARCHAR(10) NOT NULL,
    TenDichVu NVARCHAR(150) NOT NULL,
    MaDanhMuc INT NULL, 
    MaKhuVuc INT NULL,
    GiaBan DECIMAL(18,2) NOT NULL,
    SoLuongTon INT NOT NULL,
    DonViTinh NVARCHAR(20) NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL,
    NgayTao DATETIME NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE NhanVien (
    MaNhanVien INT IDENTITY(1,1) NOT NULL,
    MaCode NVARCHAR(10) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(15) NULL,
    Email NVARCHAR(100) NULL,
    CCCD NVARCHAR(12) NULL,
    GioiTinh NVARCHAR(5) NULL,
    NgaySinh DATE NULL,
    DiaChi NVARCHAR(200) NULL,
    ChucVu NVARCHAR(50) NOT NULL,
    MaKhuVuc INT NULL,
    NgayVaoLam DATE NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL,
    NgayTao DATETIME NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE CaLam (
    MaCaLam INT IDENTITY(1,1) NOT NULL,
    TenCaLam NVARCHAR(50) NOT NULL,
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL,
    NgayTao DATETIME NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE PhanCa (
    MaPhanCa INT IDENTITY(1,1) NOT NULL,
    MaNhanVien INT NOT NULL,
    MaCaLam INT NOT NULL,
    NgayLam DATE NOT NULL,
    GhiChu NVARCHAR(200) NULL,
    NgayTao DATETIME NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE KhachHang (
    MaKhachHang INT IDENTITY(1,1) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(15) NULL,
    Email NVARCHAR(100) NULL,
    MaThanhVien NVARCHAR(20) NULL,
    NgayHetHanThe DATE NULL,
    TongChiTieu DECIMAL(18,2) NOT NULL,
    DiemTichLuy INT NOT NULL CONSTRAINT DF_KhachHang_Diem DEFAULT 0,
    DiaChi NVARCHAR(200) NULL,
    NgaySinh DATE NULL,
    GioiTinh NVARCHAR(5) NULL,
    NgayTao DATETIME NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE SuKien (
    MaSuKien INT IDENTITY(1,1) NOT NULL,
    TenSuKien NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(1000) NULL,
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL,
    NgayTao DATETIME NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE KhuyenMai (
    MaKhuyenMai INT IDENTITY(1,1) NOT NULL,
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

CREATE TABLE HoaDon (
    MaHoaDon INT IDENTITY(1,1) NOT NULL,
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
    DiemNhan INT NOT NULL CONSTRAINT DF_HoaDon_DiemNhan DEFAULT 0,
    DiemDung INT NOT NULL CONSTRAINT DF_HoaDon_DiemDung DEFAULT 0,
    TrangThai NVARCHAR(20) NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE ChiTietHoaDon_Ve (
    MaChiTietVe INT IDENTITY(1,1) NOT NULL,
    MaHoaDon INT NOT NULL,
    MaLoaiVe INT NOT NULL,
    TenSanPham NVARCHAR(150) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,
    SoLuong INT NOT NULL,
    ThanhTien DECIMAL(18,2) NOT NULL,
    GhiChuChiTiet NVARCHAR(MAX) NULL,
    NgayTao DATETIME NOT NULL
);
GO

CREATE TABLE ChiTietHoaDon_DichVu (
    MaChiTietDichVu INT IDENTITY(1,1) NOT NULL,
    MaHoaDon INT NOT NULL,
    MaDichVu INT NOT NULL,
    TenSanPham NVARCHAR(150) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,
    SoLuong INT NOT NULL,
    ThanhTien DECIMAL(18,2) NOT NULL,
    GhiChuChiTiet NVARCHAR(MAX) NULL,
    NgayTao DATETIME NOT NULL
);
GO

CREATE TABLE BaoTri (
    MaBaoTri INT IDENTITY(1,1) NOT NULL,
    MaCode NVARCHAR(10) NULL,
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

CREATE TABLE VeDaPhatHanh (
    MaVePhatHanh INT IDENTITY(1,1) NOT NULL,
    MaQR NVARCHAR(100) NOT NULL,
    MaChiTietVe INT NOT NULL,
    MaLoaiVe INT NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL,
    SoLuotConLai INT NULL,
    NgayPhatHanh DATETIME NOT NULL,
    NgayHetHan DATETIME NULL,
    NgaySuDungDauTien DATETIME NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE LichSuQuetVe (
    MaLichSu INT IDENTITY(1,1) NOT NULL,
    MaVePhatHanh INT NOT NULL,
    MaTroChoi INT NULL,
    MaKhuVuc INT NULL,
    ThoiGianQuet DATETIME NOT NULL,
    KetQua NVARCHAR(10) NOT NULL,
    LyDoTuChoi NVARCHAR(200) NULL,
    MaNhanVienSoatVe INT NULL
);
GO

CREATE TABLE QuyenLoiVe (
    MaQuyenLoi INT IDENTITY(1,1) NOT NULL,
    MaLoaiVe INT NOT NULL,
    LoaiQuyen NVARCHAR(20) NOT NULL,
    MaTroChoi INT NULL,
    MaKhuVuc INT NULL,
    SoLuotChoPhep INT NULL,
    CooldownGiay INT NULL,
    NgayTao DATETIME NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE BangGia (
    MaBangGia INT IDENTITY(1,1) NOT NULL,
    MaLoaiVe INT NULL,
    MaDichVu INT NULL,
    LoaiNgay NVARCHAR(20) NOT NULL,
    GiaBan DECIMAL(18,2) NOT NULL,
    NgayBatDau DATETIME NOT NULL,
    NgayKetThuc DATETIME NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL,
    NgayTao DATETIME NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE ViDienTu (
    MaVi INT IDENTITY(1,1) NOT NULL,
    MaKhachHang INT NULL,
    MaRFID NVARCHAR(50) NOT NULL,
    SoDu DECIMAL(18,2) NOT NULL,
    TienCoc DECIMAL(18,2) NOT NULL,
    TrangThai NVARCHAR(20) NOT NULL,
    NgayKichHoat DATETIME NOT NULL,
    NgayCapNhat DATETIME NULL
);
GO

CREATE TABLE LichSuGiaoDichVi (
    MaGiaoDich INT IDENTITY(1,1) NOT NULL,
    MaVi INT NOT NULL,
    LoaiGiaoDich NVARCHAR(20) NOT NULL,
    SoTien DECIMAL(18,2) NOT NULL,
    SoDuTruoc DECIMAL(18,2) NOT NULL,
    SoDuSau DECIMAL(18,2) NOT NULL,
    MaThamChieu NVARCHAR(50) NULL,
    GhiChu NVARCHAR(255) NULL,
    NgayGiaoDich DATETIME NOT NULL
);
GO

CREATE TABLE CauHinhHeThong (
    MaCauHinh NVARCHAR(50) PRIMARY KEY,
    GiaTri NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(250),
    NgayCapNhat DATETIME DEFAULT GETDATE()
);
GO

-- ************************************************************
-- 2) KHÓA CHÍNH
-- ************************************************************

ALTER TABLE TaiKhoan       ADD CONSTRAINT PK_TaiKhoan        PRIMARY KEY (MaTaiKhoan);
ALTER TABLE KhuVuc         ADD CONSTRAINT PK_KhuVuc          PRIMARY KEY (MaKhuVuc);
ALTER TABLE TroChoi        ADD CONSTRAINT PK_TroChoi         PRIMARY KEY (MaTroChoi);
ALTER TABLE LoaiVe         ADD CONSTRAINT PK_LoaiVe          PRIMARY KEY (MaLoaiVe);
ALTER TABLE ChiTietCombo   ADD CONSTRAINT PK_ChiTietCombo    PRIMARY KEY (MaChiTietCombo);
ALTER TABLE DichVu         ADD CONSTRAINT PK_DichVu          PRIMARY KEY (MaDichVu);
ALTER TABLE NhanVien       ADD CONSTRAINT PK_NhanVien        PRIMARY KEY (MaNhanVien);
ALTER TABLE CaLam          ADD CONSTRAINT PK_CaLam           PRIMARY KEY (MaCaLam);
ALTER TABLE PhanCa         ADD CONSTRAINT PK_PhanCa          PRIMARY KEY (MaPhanCa);
ALTER TABLE KhachHang      ADD CONSTRAINT PK_KhachHang       PRIMARY KEY (MaKhachHang);
ALTER TABLE SuKien         ADD CONSTRAINT PK_SuKien          PRIMARY KEY (MaSuKien);
ALTER TABLE KhuyenMai      ADD CONSTRAINT PK_KhuyenMai       PRIMARY KEY (MaKhuyenMai);
ALTER TABLE HoaDon         ADD CONSTRAINT PK_HoaDon          PRIMARY KEY (MaHoaDon);
ALTER TABLE ChiTietHoaDon_Ve        ADD CONSTRAINT PK_ChiTietHoaDon_Ve        PRIMARY KEY (MaChiTietVe);
ALTER TABLE ChiTietHoaDon_DichVu    ADD CONSTRAINT PK_ChiTietHoaDon_DichVu    PRIMARY KEY (MaChiTietDichVu);
ALTER TABLE BaoTri         ADD CONSTRAINT PK_BaoTri          PRIMARY KEY (MaBaoTri);
ALTER TABLE VeDaPhatHanh   ADD CONSTRAINT PK_VeDaPhatHanh    PRIMARY KEY (MaVePhatHanh);
ALTER TABLE LichSuQuetVe   ADD CONSTRAINT PK_LichSuQuetVe    PRIMARY KEY (MaLichSu);
ALTER TABLE QuyenLoiVe     ADD CONSTRAINT PK_QuyenLoiVe      PRIMARY KEY (MaQuyenLoi);
ALTER TABLE BangGia        ADD CONSTRAINT PK_BangGia         PRIMARY KEY (MaBangGia);
ALTER TABLE ViDienTu       ADD CONSTRAINT PK_ViDienTu        PRIMARY KEY (MaVi);
ALTER TABLE LichSuGiaoDichVi ADD CONSTRAINT PK_LichSuGiaoDichVi PRIMARY KEY (MaGiaoDich);
GO

-- ************************************************************
-- 3) KHÓA NGOẠI
-- ************************************************************

ALTER TABLE TroChoi      ADD CONSTRAINT FK_TroChoi_KhuVuc       FOREIGN KEY (MaKhuVuc)      REFERENCES KhuVuc(MaKhuVuc);
ALTER TABLE DichVu       ADD CONSTRAINT FK_DichVu_KhuVuc        FOREIGN KEY (MaKhuVuc)      REFERENCES KhuVuc(MaKhuVuc);
ALTER TABLE ChiTietCombo ADD CONSTRAINT FK_Combo_VeCha          FOREIGN KEY (MaLoaiVeCha)   REFERENCES LoaiVe(MaLoaiVe);
ALTER TABLE ChiTietCombo ADD CONSTRAINT FK_Combo_VeCon          FOREIGN KEY (MaLoaiVeCon)   REFERENCES LoaiVe(MaLoaiVe);
ALTER TABLE NhanVien     ADD CONSTRAINT FK_NhanVien_KhuVuc      FOREIGN KEY (MaKhuVuc)      REFERENCES KhuVuc(MaKhuVuc);
ALTER TABLE PhanCa       ADD CONSTRAINT FK_PhanCa_NhanVien      FOREIGN KEY (MaNhanVien)    REFERENCES NhanVien(MaNhanVien);
ALTER TABLE PhanCa       ADD CONSTRAINT FK_PhanCa_CaLam         FOREIGN KEY (MaCaLam)       REFERENCES CaLam(MaCaLam);
ALTER TABLE KhuyenMai    ADD CONSTRAINT FK_KhuyenMai_SuKien     FOREIGN KEY (MaSuKien)      REFERENCES SuKien(MaSuKien);
ALTER TABLE HoaDon       ADD CONSTRAINT FK_HoaDon_KhachHang     FOREIGN KEY (MaKhachHang)   REFERENCES KhachHang(MaKhachHang);
ALTER TABLE HoaDon       ADD CONSTRAINT FK_HoaDon_NhanVien      FOREIGN KEY (MaNhanVien)    REFERENCES NhanVien(MaNhanVien);
ALTER TABLE HoaDon       ADD CONSTRAINT FK_HoaDon_KhuyenMai     FOREIGN KEY (MaKhuyenMai)   REFERENCES KhuyenMai(MaKhuyenMai);
ALTER TABLE ChiTietHoaDon_Ve ADD CONSTRAINT FK_ChiTietHD_Ve_HoaDon FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon) ON DELETE CASCADE;
ALTER TABLE ChiTietHoaDon_DichVu ADD CONSTRAINT FK_ChiTietHD_DichVu_HoaDon FOREIGN KEY (MaHoaDon) REFERENCES HoaDon(MaHoaDon) ON DELETE CASCADE;
ALTER TABLE ChiTietHoaDon_Ve ADD CONSTRAINT FK_ChiTietHD_Ve_LoaiVe FOREIGN KEY (MaLoaiVe) REFERENCES LoaiVe(MaLoaiVe);
ALTER TABLE ChiTietHoaDon_DichVu ADD CONSTRAINT FK_ChiTietHD_DichVu_DichVu FOREIGN KEY (MaDichVu) REFERENCES DichVu(MaDichVu);
ALTER TABLE TaiKhoan ADD CONSTRAINT FK_TaiKhoan_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien);
ALTER TABLE BaoTri       ADD CONSTRAINT FK_BaoTri_TroChoi       FOREIGN KEY (MaTroChoi)     REFERENCES TroChoi(MaTroChoi);
ALTER TABLE BaoTri       ADD CONSTRAINT FK_BaoTri_NhanVien      FOREIGN KEY (NguoiThucHien) REFERENCES NhanVien(MaNhanVien);
ALTER TABLE VeDaPhatHanh ADD CONSTRAINT FK_VePhatHanh_ChiTietVe FOREIGN KEY (MaChiTietVe) REFERENCES ChiTietHoaDon_Ve(MaChiTietVe) ON DELETE CASCADE;
ALTER TABLE VeDaPhatHanh ADD CONSTRAINT FK_VePhatHanh_LoaiVe    FOREIGN KEY (MaLoaiVe)      REFERENCES LoaiVe(MaLoaiVe);
ALTER TABLE LichSuQuetVe ADD CONSTRAINT FK_LogQuet_VePhatHanh   FOREIGN KEY (MaVePhatHanh)  REFERENCES VeDaPhatHanh(MaVePhatHanh) ON DELETE CASCADE;
ALTER TABLE LichSuQuetVe ADD CONSTRAINT FK_LogQuet_TroChoi      FOREIGN KEY (MaTroChoi)     REFERENCES TroChoi(MaTroChoi);
ALTER TABLE LichSuQuetVe ADD CONSTRAINT FK_LogQuet_KhuVuc       FOREIGN KEY (MaKhuVuc)      REFERENCES KhuVuc(MaKhuVuc);
ALTER TABLE LichSuQuetVe ADD CONSTRAINT FK_LogQuet_NhanVien     FOREIGN KEY (MaNhanVienSoatVe) REFERENCES NhanVien(MaNhanVien);
ALTER TABLE QuyenLoiVe ADD CONSTRAINT FK_QuyenLoiVe_LoaiVe      FOREIGN KEY (MaLoaiVe) REFERENCES LoaiVe(MaLoaiVe) ON DELETE CASCADE;
ALTER TABLE QuyenLoiVe ADD CONSTRAINT FK_QuyenLoiVe_TroChoi     FOREIGN KEY (MaTroChoi) REFERENCES TroChoi(MaTroChoi);
ALTER TABLE QuyenLoiVe ADD CONSTRAINT FK_QuyenLoiVe_KhuVuc      FOREIGN KEY (MaKhuVuc) REFERENCES KhuVuc(MaKhuVuc);
ALTER TABLE BangGia ADD CONSTRAINT FK_BangGia_LoaiVe            FOREIGN KEY (MaLoaiVe) REFERENCES LoaiVe(MaLoaiVe);
ALTER TABLE BangGia ADD CONSTRAINT FK_BangGia_DichVu            FOREIGN KEY (MaDichVu) REFERENCES DichVu(MaDichVu);
ALTER TABLE DichVu ADD CONSTRAINT FK_DichVu_DanhMuc FOREIGN KEY (MaDanhMuc) REFERENCES DanhMucDichVu(MaDanhMuc);
ALTER TABLE ViDienTu ADD CONSTRAINT FK_ViDienTu_KhachHang       FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang);
ALTER TABLE LichSuGiaoDichVi ADD CONSTRAINT FK_LSGDVi_ViDienTu  FOREIGN KEY (MaVi) REFERENCES ViDienTu(MaVi) ON DELETE CASCADE;
GO

-- ************************************************************
-- 4) UNIQUE
-- ************************************************************

ALTER TABLE TaiKhoan ADD CONSTRAINT UQ_TaiKhoan_TenDangNhap UNIQUE (TenDangNhap);
ALTER TABLE KhuVuc   ADD CONSTRAINT UQ_KhuVuc_MaCode UNIQUE (MaCode);
ALTER TABLE KhuVuc   ADD CONSTRAINT UQ_KhuVuc_TenKhuVuc UNIQUE (TenKhuVuc);
ALTER TABLE TroChoi  ADD CONSTRAINT UQ_TroChoi_MaCode UNIQUE (MaCode);
ALTER TABLE LoaiVe   ADD CONSTRAINT UQ_LoaiVe_MaCode UNIQUE (MaCode);
ALTER TABLE DichVu   ADD CONSTRAINT UQ_DichVu_MaCode UNIQUE (MaCode);
ALTER TABLE NhanVien ADD CONSTRAINT UQ_NhanVien_MaCode UNIQUE (MaCode);
ALTER TABLE HoaDon   ADD CONSTRAINT UQ_HoaDon_MaCode UNIQUE (MaCode);
ALTER TABLE BaoTri   ADD CONSTRAINT UQ_BaoTri_MaCode UNIQUE (MaCode);
ALTER TABLE VeDaPhatHanh ADD CONSTRAINT UQ_VePhatHanh_MaQR UNIQUE (MaQR);
ALTER TABLE ViDienTu ADD CONSTRAINT UQ_ViDienTu_MaRFID UNIQUE (MaRFID);
ALTER TABLE PhanCa ADD CONSTRAINT UQ_PhanCa_NhanVien_Ca_Ngay UNIQUE (MaNhanVien, MaCaLam, NgayLam);
ALTER TABLE ChiTietCombo ADD CONSTRAINT UQ_ChiTietCombo_ChaCon UNIQUE (MaLoaiVeCha, MaLoaiVeCon);
GO

CREATE UNIQUE INDEX UX_KhachHang_SDT_NotNull ON KhachHang(SoDienThoai) WHERE SoDienThoai IS NOT NULL;
GO
CREATE UNIQUE INDEX UX_KhachHang_MaThanhVien_NotNull ON KhachHang(MaThanhVien) WHERE MaThanhVien IS NOT NULL;
GO

-- ************************************************************
-- 5) CHECK CONSTRAINTS
-- ************************************************************

ALTER TABLE TaiKhoan ADD CONSTRAINT CK_TaiKhoan_VaiTro CHECK (VaiTro IN (N'Admin', N'NhanVien'));
ALTER TABLE DanhMucDichVu ADD CONSTRAINT CK_DanhMuc_KieuLogic CHECK (KieuLogic IN (N'SALE', N'TIME', N'DEPOSIT'));

ALTER TABLE KhuVuc ADD CONSTRAINT CK_KhuVuc_TrangThai CHECK (TrangThai IN (N'Mở cửa', N'Bảo trì', N'Đóng cửa'));

ALTER TABLE TroChoi ADD CONSTRAINT CK_TroChoi_TrangThai CHECK (TrangThai IN (N'Đang hoạt động', N'Đang bảo trì', N'Hỏng/Sự cố', N'Ngừng hoạt động'));
ALTER TABLE TroChoi ADD CONSTRAINT CK_TroChoi_SucChua CHECK (SucChua > 0);
ALTER TABLE TroChoi ADD CONSTRAINT CK_TroChoi_TuoiToiThieu CHECK (TuoiToiThieu >= 0);
ALTER TABLE TroChoi ADD CONSTRAINT CK_TroChoi_ChieuCaoToiThieu CHECK (ChieuCaoToiThieu >= 0);
ALTER TABLE TroChoi ADD CONSTRAINT CK_TroChoi_ThoiGianLuot CHECK (ThoiGianLuot > 0);

ALTER TABLE LoaiVe ADD CONSTRAINT CK_LoaiVe_GiaVe CHECK (GiaVe >= 0);
ALTER TABLE LoaiVe ADD CONSTRAINT CK_LoaiVe_TrangThai CHECK (TrangThai IN (N'Hoạt động', N'Ngừng bán'));

ALTER TABLE ChiTietCombo ADD CONSTRAINT CK_ChiTietCombo_SoLuot CHECK (SoLuotChoPhep > 0);

ALTER TABLE DichVu ADD CONSTRAINT CK_DichVu_GiaBan CHECK (GiaBan >= 0);
ALTER TABLE DichVu ADD CONSTRAINT CK_DichVu_SoLuongTon CHECK (SoLuongTon >= 0);
ALTER TABLE DichVu ADD CONSTRAINT CK_DichVu_TrangThai CHECK (TrangThai IN (N'Hoạt động', N'Ngừng bán'));

ALTER TABLE NhanVien ADD CONSTRAINT CK_NhanVien_TrangThai CHECK (TrangThai IN (N'Đang làm việc', N'Tạm nghỉ', N'Đã nghỉ'));

ALTER TABLE KhachHang ADD CONSTRAINT CK_KhachHang_TongChiTieu CHECK (TongChiTieu >= 0);
ALTER TABLE KhachHang ADD CONSTRAINT CK_KhachHang_DiemTichLuy CHECK (DiemTichLuy >= 0);

ALTER TABLE SuKien ADD CONSTRAINT CK_SuKien_TrangThai CHECK (TrangThai IN (N'Sắp diễn ra', N'Đang diễn ra', N'Đã kết thúc', N'Đã hủy'));
ALTER TABLE SuKien ADD CONSTRAINT CK_SuKien_Ngay CHECK (NgayKetThuc >= NgayBatDau);

ALTER TABLE KhuyenMai ADD CONSTRAINT CK_KhuyenMai_LoaiGiamGia CHECK (LoaiGiamGia IN (N'PhanTram', N'SoTien'));
ALTER TABLE KhuyenMai ADD CONSTRAINT CK_KhuyenMai_GiaTriGiam CHECK (GiaTriGiam > 0);
ALTER TABLE KhuyenMai ADD CONSTRAINT CK_KhuyenMai_DonToiThieu CHECK (DonToiThieu >= 0);
ALTER TABLE KhuyenMai ADD CONSTRAINT CK_KhuyenMai_Ngay CHECK (NgayKetThuc >= NgayBatDau);

ALTER TABLE HoaDon ADD CONSTRAINT CK_HoaDon_TongTien CHECK (TongTien >= 0);
ALTER TABLE HoaDon ADD CONSTRAINT CK_HoaDon_TienGiamGia CHECK (TienGiamGia >= 0);
ALTER TABLE HoaDon ADD CONSTRAINT CK_HoaDon_ThanhToan CHECK (ThanhToan >= 0);
ALTER TABLE HoaDon ADD CONSTRAINT CK_HoaDon_TrangThai CHECK (TrangThai IN (N'Đã thanh toán', N'Đã hủy', N'Chờ xử lý'));
ALTER TABLE HoaDon ADD CONSTRAINT CK_HoaDon_PhuongThuc CHECK (PhuongThuc IN (N'TienMat', N'ViDienTu', N'ChuyenKhoan'));
ALTER TABLE HoaDon ADD CONSTRAINT CK_HoaDon_DiemNhan CHECK (DiemNhan >= 0);
ALTER TABLE HoaDon ADD CONSTRAINT CK_HoaDon_DiemDung CHECK (DiemDung >= 0);

ALTER TABLE ChiTietHoaDon_Ve ADD CONSTRAINT CK_ChiTietHoaDon_Ve_SoLuong CHECK (SoLuong > 0);
ALTER TABLE ChiTietHoaDon_Ve ADD CONSTRAINT CK_ChiTietHoaDon_Ve_DonGia CHECK (DonGia >= 0);
ALTER TABLE ChiTietHoaDon_Ve ADD CONSTRAINT CK_ChiTietHoaDon_Ve_ThanhTien CHECK (ThanhTien >= 0);

ALTER TABLE ChiTietHoaDon_DichVu ADD CONSTRAINT CK_ChiTietHoaDon_DichVu_SoLuong CHECK (SoLuong > 0);
ALTER TABLE ChiTietHoaDon_DichVu ADD CONSTRAINT CK_ChiTietHoaDon_DichVu_DonGia CHECK (DonGia >= 0);
ALTER TABLE ChiTietHoaDon_DichVu ADD CONSTRAINT CK_ChiTietHoaDon_DichVu_ThanhTien CHECK (ThanhTien >= 0);

ALTER TABLE BaoTri ADD CONSTRAINT CK_BaoTri_TrangThai CHECK (TrangThai IN (N'Đang chờ', N'Đang thực hiện', N'Hoàn thành', N'Đã hủy'));
ALTER TABLE BaoTri ADD CONSTRAINT CK_BaoTri_LoaiBaoTri CHECK (LoaiBaoTri IN (N'Định kỳ', N'Khẩn cấp', N'Nâng cấp'));
ALTER TABLE BaoTri ADD CONSTRAINT CK_BaoTri_ChiPhi CHECK (ChiPhi >= 0);

ALTER TABLE VeDaPhatHanh ADD CONSTRAINT CK_VePhatHanh_TrangThai CHECK (TrangThai IN (N'Chưa sử dụng', N'Đã sử dụng', N'Hết hạn', N'Đã hủy'));
ALTER TABLE VeDaPhatHanh ADD CONSTRAINT CK_VePhatHanh_SoLuot CHECK (SoLuotConLai IS NULL OR SoLuotConLai >= 0);

ALTER TABLE LichSuQuetVe ADD CONSTRAINT CK_LichSuQuet_KetQua CHECK (KetQua IN (N'OK', N'Từ chối'));

ALTER TABLE QuyenLoiVe ADD CONSTRAINT CK_QuyenLoiVe_LoaiQuyen CHECK (LoaiQuyen IN (N'Vào cổng', N'Vào khu vực', N'Chơi trò chơi'));
ALTER TABLE QuyenLoiVe ADD CONSTRAINT CK_QuyenLoiVe_SoLuot CHECK (SoLuotChoPhep IS NULL OR SoLuotChoPhep > 0);
ALTER TABLE QuyenLoiVe ADD CONSTRAINT CK_QuyenLoiVe_Cooldown CHECK (CooldownGiay IS NULL OR CooldownGiay >= 0);

ALTER TABLE BangGia ADD CONSTRAINT CK_BangGia_XorSanPham
CHECK (
    (MaLoaiVe IS NOT NULL AND MaDichVu IS NULL)
 OR (MaLoaiVe IS NULL AND MaDichVu IS NOT NULL)
);
ALTER TABLE BangGia ADD CONSTRAINT CK_BangGia_LoaiNgay
CHECK (LoaiNgay IN (N'NgayThuong', N'CuoiTuan', N'LeTet', N'CaoDiem'));
ALTER TABLE BangGia ADD CONSTRAINT CK_BangGia_Gia CHECK (GiaBan >= 0);
ALTER TABLE BangGia ADD CONSTRAINT CK_BangGia_Ngay CHECK (NgayKetThuc >= NgayBatDau);
ALTER TABLE BangGia ADD CONSTRAINT CK_BangGia_TrangThai CHECK (TrangThai IN (N'Hoạt động', N'Ngưng áp dụng'));

ALTER TABLE ViDienTu ADD CONSTRAINT CK_ViDienTu_SoDu CHECK (SoDu >= 0);
ALTER TABLE ViDienTu ADD CONSTRAINT CK_ViDienTu_TienCoc CHECK (TienCoc >= 0);
ALTER TABLE ViDienTu ADD CONSTRAINT CK_ViDienTu_TrangThai CHECK (TrangThai IN (N'Kích hoạt', N'Bị khóa', N'Đã thu hồi'));

ALTER TABLE LichSuGiaoDichVi ADD CONSTRAINT CK_LSGDVi_LoaiGD CHECK (LoaiGiaoDich IN (N'NapTien', N'ThanhToan', N'HoanTien', N'RutTien'));
ALTER TABLE LichSuGiaoDichVi ADD CONSTRAINT CK_LSGDVi_SoTien CHECK (SoTien > 0);
ALTER TABLE LichSuGiaoDichVi ADD CONSTRAINT CK_LSGDVi_SoDuTruoc CHECK (SoDuTruoc >= 0);
ALTER TABLE LichSuGiaoDichVi ADD CONSTRAINT CK_LSGDVi_SoDuSau CHECK (SoDuSau >= 0);

-- ************************************************************
-- 6) DEFAULTS 
-- ************************************************************

-- Bảng chính
ALTER TABLE TaiKhoan      ADD CONSTRAINT DF_TaiKhoan_NgayTao      DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE KhuVuc        ADD CONSTRAINT DF_KhuVuc_NgayTao        DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE TroChoi       ADD CONSTRAINT DF_TroChoi_NgayTao       DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE LoaiVe        ADD CONSTRAINT DF_LoaiVe_NgayTao        DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE ChiTietCombo  ADD CONSTRAINT DF_ChiTietCombo_NgayTao  DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE DichVu        ADD CONSTRAINT DF_DichVu_NgayTao        DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE NhanVien      ADD CONSTRAINT DF_NhanVien_NgayTao      DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE CaLam         ADD CONSTRAINT DF_CaLam_NgayTao         DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE PhanCa        ADD CONSTRAINT DF_PhanCa_NgayTao        DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE KhachHang     ADD CONSTRAINT DF_KhachHang_NgayTao     DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE SuKien        ADD CONSTRAINT DF_SuKien_NgayTao        DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE KhuyenMai     ADD CONSTRAINT DF_KhuyenMai_NgayTao     DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE HoaDon        ADD CONSTRAINT DF_HoaDon_NgayLap        DEFAULT GETDATE() FOR NgayLap;
ALTER TABLE ChiTietHoaDon_Ve     ADD CONSTRAINT DF_ChiTietVe_NgayTao     DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE ChiTietHoaDon_DichVu ADD CONSTRAINT DF_ChiTietDV_NgayTao     DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE BaoTri        ADD CONSTRAINT DF_BaoTri_NgayTao        DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE VeDaPhatHanh  ADD CONSTRAINT DF_VePhatHanh_NgayPH     DEFAULT GETDATE() FOR NgayPhatHanh;
ALTER TABLE QuyenLoiVe    ADD CONSTRAINT DF_QuyenLoiVe_NgayTao    DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE BangGia       ADD CONSTRAINT DF_BangGia_NgayTao       DEFAULT GETDATE() FOR NgayTao;
ALTER TABLE ViDienTu      ADD CONSTRAINT DF_ViDienTu_NgayKH       DEFAULT GETDATE() FOR NgayKichHoat;
ALTER TABLE LichSuGiaoDichVi ADD CONSTRAINT DF_LSGDVi_NgayGD      DEFAULT GETDATE() FOR NgayGiaoDich;

INSERT INTO CauHinhHeThong (MaCauHinh, GiaTri, MoTa) VALUES 
('Loyalty_Earn_Rate', '10000', N'Số tiền tương ứng với 1 điểm (Ví dụ: 10,000đ = 1đ)'),
('Loyalty_Redeem_Rate', '1000', N'Giá trị quy đổi của 1 điểm khi tiêu (Ví dụ: 1đ = 1,000đ)');
GO

-- ************************************************************
-- 7) INDEXES
-- ************************************************************
CREATE INDEX IX_TroChoi_MaKhuVuc ON TroChoi(MaKhuVuc);
CREATE INDEX IX_DichVu_MaKhuVuc ON DichVu(MaKhuVuc);
CREATE INDEX IX_DichVu_MaDanhMuc ON DichVu(MaDanhMuc);
CREATE INDEX IX_HoaDon_NgayLap ON HoaDon(NgayLap);
CREATE INDEX IX_HoaDon_MaKhachHang ON HoaDon(MaKhachHang);
CREATE INDEX IX_HoaDon_MaNhanVien ON HoaDon(MaNhanVien);
CREATE INDEX IX_ChiTietHD_Ve_MaHoaDon ON ChiTietHoaDon_Ve(MaHoaDon);
CREATE INDEX IX_ChiTietHD_DichVu_MaHoaDon ON ChiTietHoaDon_DichVu(MaHoaDon);
CREATE INDEX IX_VePhatHanh_MaChiTietVe ON VeDaPhatHanh(MaChiTietVe);
CREATE INDEX IX_LichSuQuet_MaVePH ON LichSuQuetVe(MaVePhatHanh);
CREATE INDEX IX_QuyenLoiVe_MaLoaiVe ON QuyenLoiVe(MaLoaiVe);
CREATE INDEX IX_BangGia_MaLoaiVe ON BangGia(MaLoaiVe);
CREATE INDEX IX_BangGia_MaDichVu ON BangGia(MaDichVu);
CREATE INDEX IX_PhanCa_NhanVien ON PhanCa(MaNhanVien);
CREATE INDEX IX_ViDienTu_KhachHang ON ViDienTu(MaKhachHang);
CREATE INDEX IX_LSGDVi_MaVi ON LichSuGiaoDichVi(MaVi);
GO
