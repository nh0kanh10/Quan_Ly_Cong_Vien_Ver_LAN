-- ================================================================
-- 🏗️ DATABASE ĐẠI NAM RESORT — ENTERPRISE EDITION V2
-- ================================================================
-- 12 Enterprise Patterns | ~85 Bảng | Thuần Việt
-- SQL Server 2022 Express (16.0.1170.5)
-- Ngày tạo: 15/04/2026
-- ================================================================

SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO

USE master;
GO
IF DB_ID('Database_DaiNam') IS NOT NULL
BEGIN
    ALTER DATABASE Database_DaiNam SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Database_DaiNam;
END
GO
CREATE DATABASE Database_DaiNam;
GO
USE Database_DaiNam;
GO

-- ================================================================
-- QUY HOẠCH SCHEMA (8 domain)
-- ================================================================
CREATE SCHEMA HeThong;   -- Từ điển, Luồng trạng thái, Cấu hình
GO
CREATE SCHEMA DoiTac;    -- Party Model (Con người & Tổ chức)
GO
CREATE SCHEMA DanhMuc;   -- Sản phẩm, Bảng giá, Khu vực, Tài sản
GO
CREATE SCHEMA BanHang;   -- Đơn hàng, Vé, Đặt phòng/bàn, Khuyến mãi
GO
CREATE SCHEMA Kho;       -- Chứng từ kho, Sổ cái, Lô hàng
GO
CREATE SCHEMA TaiChinh;  -- Chứng từ tài chính, Sổ cái ví, RFID
GO
CREATE SCHEMA NhanSu;    -- Ca làm, Chấm công, Lương, Kỷ luật
GO
CREATE SCHEMA VanHanh;   -- Sự kiện, Sự cố, Bảo trì, Bãi xe, Trường đua
GO


-- ================================================================
-- ████ PHẦN 1: HeThong — HẠ TẦNG HỆ THỐNG (5 bảng) ████
-- Pattern: Dictionary, State Machine, Sequence
-- ================================================================

-- ── 1.1 Từ Điển Tập Trung (thay 30+ CHECK constraints) ──────────
CREATE TABLE HeThong.TuDien (
    NhomMa       VARCHAR(50)    NOT NULL,
    Ma           VARCHAR(50)    NOT NULL,
    NhanHienThi  NVARCHAR(100)  NOT NULL,
    ThuTu        INT            NOT NULL DEFAULT 0,
    MauSac       VARCHAR(10)    NULL,
    BieuTuong    VARCHAR(50)    NULL,
    ConHoatDong  BIT            NOT NULL DEFAULT 1,
    PRIMARY KEY (NhomMa, Ma)
);
GO

-- ── 1.2 Luồng Trạng Thái (State Machine) ────────────────────────
CREATE TABLE HeThong.LuongTrangThai (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    ThucThe          VARCHAR(50)   NOT NULL,
    TuTrangThai      VARCHAR(50)   NOT NULL,
    DenTrangThai     VARCHAR(50)   NOT NULL,
    MaQuyenCanThiet  VARCHAR(50)   NULL,
    MoTa             NVARCHAR(200) NULL,
    CONSTRAINT UQ_LuongTT UNIQUE (ThucThe, TuTrangThai, DenTrangThai)
);
GO

-- ── 1.3 Cấu Hình Hệ Thống (Key-Value) ──────────────────────────
CREATE TABLE HeThong.CauHinh (
    Khoa         VARCHAR(100)   PRIMARY KEY,
    GiaTri       NVARCHAR(500)  NOT NULL,
    MoTa         NVARCHAR(200)  NULL,
    CapNhatLuc   DATETIME       NULL,
    CapNhatBoi   INT            NULL
);
GO

-- ── 1.4 Bản Dịch (i18n sẵn sàng) ───────────────────────────────
CREATE TABLE HeThong.BanDich (
    LoaiThucThe  VARCHAR(50)    NOT NULL,
    IdThucThe    INT            NOT NULL,
    NgonNgu      VARCHAR(10)    NOT NULL,
    TruongDich   VARCHAR(50)    NOT NULL,
    NoiDung      NVARCHAR(MAX)  NOT NULL,
    PRIMARY KEY (LoaiThucThe, IdThucThe, NgonNgu, TruongDich)
);
GO

-- ── 1.5 Bộ Đếm Mã Tự Động (SQL SEQUENCE — Atomic, Race-Free) ───
-- Dùng SQL SEQUENCE (có sẵn SQL 2012+) → không cần SP, không race condition
CREATE SEQUENCE HeThong.Seq_NhanVien   AS INT START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE HeThong.Seq_KhachHang  AS INT START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE HeThong.Seq_SanPham    AS INT START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE HeThong.Seq_DonHang    AS INT START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE HeThong.Seq_NhaCungCap AS INT START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE HeThong.Seq_ChungTuKho AS INT START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE HeThong.Seq_ChungTuTC  AS INT START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE HeThong.Seq_DatPhong   AS INT START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE HeThong.Seq_DatBan     AS INT START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE HeThong.Seq_KhuyenMai  AS INT START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE HeThong.Seq_VanDongVien AS INT START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE HeThong.Seq_LoHang     AS INT START WITH 1 INCREMENT BY 1;
GO

-- ── 1.6 Hàm kiểm tra Từ Điển (Dictionary Enforcement) ──────────
-- Fix "Paper Tiger": mọi cột TrangThai phải được validate qua TuDien
CREATE FUNCTION HeThong.fn_TuDienHopLe(
    @NhomMa VARCHAR(50),
    @Ma     VARCHAR(50)
) RETURNS BIT
AS
BEGIN
    DECLARE @KetQua BIT = 0;
    IF EXISTS (
        SELECT 1 FROM HeThong.TuDien
        WHERE NhomMa = @NhomMa AND Ma = @Ma AND ConHoatDong = 1
    )
        SET @KetQua = 1;
    RETURN @KetQua;
END
GO


-- ================================================================
-- ████ PHẦN 2: DoiTac — PARTY MODEL (6 bảng) ████
-- Pattern: Party Model, Class Table Inheritance, Identity & Credential
-- ================================================================

-- ── 2.1 Thông Tin Đối Tác (Bảng gốc chung) ─────────────────────
CREATE TABLE DoiTac.ThongTin (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    HoTen        NVARCHAR(150)  NOT NULL,
    DienThoai    VARCHAR(20)    NULL,
    Email        VARCHAR(100)   NULL,
    Cccd         VARCHAR(20)    NULL,
    DiaChi       NVARCHAR(300)  NULL,
    HinhAnh      VARCHAR(255)   NULL,
    LoaiDoiTac   VARCHAR(20)    NOT NULL DEFAULT 'CaNhan',
    DaXoa        BIT            NOT NULL DEFAULT 0,
    NgayTao      DATETIME       NOT NULL DEFAULT GETDATE(),
    CapNhatLuc   DATETIME       NULL
);
GO

CREATE UNIQUE NONCLUSTERED INDEX UQ_DoiTac_Cccd_Active
ON DoiTac.ThongTin(Cccd) WHERE Cccd IS NOT NULL AND DaXoa = 0;

CREATE UNIQUE NONCLUSTERED INDEX UQ_DoiTac_DienThoai_Active
ON DoiTac.ThongTin(DienThoai) WHERE DienThoai IS NOT NULL AND DaXoa = 0;

CREATE NONCLUSTERED INDEX IX_DoiTac_LoaiDoiTac
ON DoiTac.ThongTin(LoaiDoiTac) WHERE DaXoa = 0;
GO

-- ── 2.2 Nhân Viên (IS-A DoiTac) ─────────────────────────────────
CREATE TABLE DoiTac.NhanVien (
    IdDoiTac       INT            PRIMARY KEY REFERENCES DoiTac.ThongTin(Id),
    MaNhanVien     VARCHAR(20)    NOT NULL UNIQUE,
    IdVaiTro       INT            NULL,  -- FK → NhanSu.VaiTro (thêm sau)
    IdKhuVuc       INT            NULL,  -- FK → DanhMuc.KhuVuc (thêm sau)
    IdNguoiQuanLy  INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    DuongDanCay    VARCHAR(255)   NULL,
    ChucVu         NVARCHAR(50)   NULL,
    GioiTinh       NVARCHAR(10)   NULL,
    NgaySinh       DATE           NULL,
    LoaiKhoi       VARCHAR(20)    NULL DEFAULT 'VanPhong',
    LoaiHopDong    VARCHAR(20)    NULL DEFAULT 'FullTime',
    NhomCongViec   VARCHAR(20)    NULL DEFAULT 'ThuongNgay',
    LuongCoBan     DECIMAL(15,0)  NULL,
    LuongTheoGio   DECIMAL(10,0)  NULL,
    TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'DangLam',
    -- ★ FIX Lỗi 1: Dictionary Enforcement — không ai nhét 'DangNgu' được nữa
    CONSTRAINT CHK_NV_TrangThai CHECK (HeThong.fn_TuDienHopLe('NV_TRANG_THAI', TrangThai) = 1),
    CONSTRAINT CHK_NV_LoaiKhoi CHECK (LoaiKhoi IS NULL OR HeThong.fn_TuDienHopLe('NV_LOAI_KHOI', LoaiKhoi) = 1),
    CONSTRAINT CHK_NV_LoaiHopDong CHECK (LoaiHopDong IS NULL OR HeThong.fn_TuDienHopLe('NV_LOAI_HOP_DONG', LoaiHopDong) = 1),
    CONSTRAINT CHK_NV_NhomCV CHECK (NhomCongViec IS NULL OR HeThong.fn_TuDienHopLe('NV_NHOM_CONG_VIEC', NhomCongViec) = 1)
);
GO

-- ── 2.3 Khách Hàng (IS-A DoiTac) ────────────────────────────────
CREATE TABLE DoiTac.KhachHang (
    IdDoiTac       INT            PRIMARY KEY REFERENCES DoiTac.ThongTin(Id),
    MaKhachHang    VARCHAR(20)    NOT NULL UNIQUE,
    LoaiKhach      VARCHAR(20)    NULL DEFAULT 'CaNhan',
    HangThanhVien  VARCHAR(20)    NULL,
    DiemTichLuy    INT            NOT NULL DEFAULT 0,
    TongChiTieu    DECIMAL(18,0)  NOT NULL DEFAULT 0,
    IdDoanKhach    INT            NULL REFERENCES DoiTac.ThongTin(Id)
);
GO

-- ── 2.4 Nhà Cung Cấp (IS-A DoiTac) ─────────────────────────────
CREATE TABLE DoiTac.NhaCungCap (
    IdDoiTac           INT            PRIMARY KEY REFERENCES DoiTac.ThongTin(Id),
    MaNhaCungCap       VARCHAR(20)    NOT NULL UNIQUE,
    MaSoThue           VARCHAR(20)    NULL,
    NguoiLienHe        NVARCHAR(100)  NULL,
    DieuKhoanThanhToan NVARCHAR(200)  NULL
);
GO

-- ── 2.5 Vận Động Viên (IS-A DoiTac) ─────────────────────────────
CREATE TABLE DoiTac.VanDongVien (
    IdDoiTac          INT            PRIMARY KEY REFERENCES DoiTac.ThongTin(Id),
    MaVanDongVien     VARCHAR(20)    NOT NULL UNIQUE,
    LoaiVanDongVien   VARCHAR(20)    NULL
);
GO

-- ── 2.6 Tài Khoản Đăng Nhập (Identity & Credential Pattern) ────
CREATE TABLE DoiTac.TaiKhoan (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdDoiTac       INT            NOT NULL REFERENCES DoiTac.ThongTin(Id),
    TenDangNhap    VARCHAR(100)   NOT NULL UNIQUE,
    MatKhauHash    NVARCHAR(100)  NOT NULL,
    LoaiTaiKhoan   VARCHAR(20)    NOT NULL DEFAULT 'NhanVien',
    LoaiDangNhap   VARCHAR(20)    NOT NULL DEFAULT 'MatKhau',
    ConHoatDong    BIT            NOT NULL DEFAULT 1,
    LanDangNhapCuoi DATETIME      NULL,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO


-- ================================================================
-- ████ PHẦN 3: DanhMuc — CATALOG (~22 bảng) ████
-- Pattern: Class Table Inheritance, Effective Dating
-- ================================================================

-- ── 3.1 Đơn Vị Tính ─────────────────────────────────────────────
CREATE TABLE DanhMuc.DonViTinh (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    MaDonVi      VARCHAR(20)    NOT NULL UNIQUE,
    TenDonVi     NVARCHAR(50)   NOT NULL,
    ConHoatDong  BIT            NOT NULL DEFAULT 1
);
GO

-- ── 3.2 Quy Đổi Đơn Vị ─────────────────────────────────────────
CREATE TABLE DanhMuc.QuyDoiDonVi (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    IdDonViGoc   INT            NOT NULL REFERENCES DanhMuc.DonViTinh(Id),
    IdDonViDich  INT            NOT NULL REFERENCES DanhMuc.DonViTinh(Id),
    TyLeQuyDoi   DECIMAL(10,4)  NOT NULL,
    CONSTRAINT UQ_QuyDoi UNIQUE (IdDonViGoc, IdDonViDich)
);
GO

-- ── 3.3 Khu Vực ─────────────────────────────────────────────────
CREATE TABLE DanhMuc.KhuVuc (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    MaKhuVuc     VARCHAR(20)    NOT NULL UNIQUE,
    TenKhuVuc    NVARCHAR(100)  NOT NULL,
    LoaiKhuVuc   VARCHAR(30)    NOT NULL,
    SucChua      INT            NULL,
    TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'HoatDong',
    ToaDoX       DECIMAL(9,6)   NULL,
    ToaDoY       DECIMAL(9,6)   NULL,
    MoTa         NVARCHAR(500)  NULL,
    HinhAnh      VARCHAR(255)   NULL,
    DaXoa        BIT            NOT NULL DEFAULT 0
);
GO

-- ── 3.4 Khu Vực Biển (Weak Entity) ──────────────────────────────
CREATE TABLE DanhMuc.KhuVucBien (
    IdKhuVuc       INT            PRIMARY KEY REFERENCES DanhMuc.KhuVuc(Id),
    DoSauToiDa     DECIMAL(5,2)   NULL,
    YeuCauPhao     BIT            NOT NULL DEFAULT 0,
    ChoPhepBoi     BIT            NOT NULL DEFAULT 1
);
GO

-- ── 3.5 Khu Vực Thú (Weak Entity) ──────────────────────────────
CREATE TABLE DanhMuc.KhuVucThu (
    IdKhuVuc           INT            PRIMARY KEY REFERENCES DanhMuc.KhuVuc(Id),
    DienTichHectare    DECIMAL(8,2)   NULL,
    LoaiDongVatChinh   NVARCHAR(100)  NULL
);
GO

-- ── 3.6 Nhà Hàng ────────────────────────────────────────────────
CREATE TABLE DanhMuc.NhaHang (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    TenNhaHang   NVARCHAR(100)  NOT NULL,
    IdKhuVuc     INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    SoLuongBan   INT            NULL,
    GioMoCua     TIME           NULL,
    GioDongCua   TIME           NULL,
    TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'HoatDong',
    DaXoa        BIT            NOT NULL DEFAULT 0
);
GO

-- ── 3.7 Loại Phòng ──────────────────────────────────────────────
CREATE TABLE DanhMuc.LoaiPhong (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai       NVARCHAR(100)  NOT NULL,
    MoTa          NVARCHAR(500)  NULL,
    SoNguoiToiDa  INT            NULL
);
GO

-- ── 3.8 Phòng ───────────────────────────────────────────────────
CREATE TABLE DanhMuc.Phong (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    MaPhong      VARCHAR(20)    NOT NULL UNIQUE,
    IdLoaiPhong  INT            NOT NULL REFERENCES DanhMuc.LoaiPhong(Id),
    IdKhuVuc     INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    Tang         INT            NULL,
    DienTich     DECIMAL(8,2)   NULL,
    TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'Trong',
    RowVersion   ROWVERSION     NOT NULL
);
GO

-- ── 3.9 Bàn Ăn ──────────────────────────────────────────────────
CREATE TABLE DanhMuc.BanAn (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    MaBan        VARCHAR(20)    NOT NULL UNIQUE,
    IdNhaHang    INT            NOT NULL REFERENCES DanhMuc.NhaHang(Id),
    SoChoNgoi    INT            NOT NULL DEFAULT 4,
    ViTri        NVARCHAR(50)   NULL,
    TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'Trong',
    RowVersion   ROWVERSION     NOT NULL
);
GO

-- ── 3.10 Động Vật ───────────────────────────────────────────────
CREATE TABLE DanhMuc.DongVat (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    TenDongVat   NVARCHAR(100)  NOT NULL,
    Loai         NVARCHAR(50)   NULL,
    IdChuongTrai INT            NULL,  -- FK sau
    GioiTinh     NVARCHAR(10)   NULL,
    NamSinh      INT            NULL,
    TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'KhoeManh',
    MoTa         NVARCHAR(500)  NULL,
    HinhAnh      VARCHAR(255)   NULL,
    DaXoa        BIT            NOT NULL DEFAULT 0
);
GO

-- ── 3.11 Chuồng Trại ────────────────────────────────────────────
CREATE TABLE DanhMuc.ChuongTrai (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    MaChuong     VARCHAR(20)    NOT NULL UNIQUE,
    TenChuong    NVARCHAR(100)  NOT NULL,
    IdKhuVuc     INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    SucChua      INT            NULL,
    TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'HoatDong'
);
GO

ALTER TABLE DanhMuc.DongVat ADD CONSTRAINT FK_DongVat_ChuongTrai
    FOREIGN KEY (IdChuongTrai) REFERENCES DanhMuc.ChuongTrai(Id);
GO

-- ── 3.12 Tủ Đồ ──────────────────────────────────────────────────
CREATE TABLE DanhMuc.TuDo (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    MaTuDo       VARCHAR(20)    NOT NULL UNIQUE,
    IdKhuVuc     INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'Trong'
);
GO

-- ── 3.13 Chòi Nghỉ Mát ─────────────────────────────────────────
CREATE TABLE DanhMuc.ChoiNghiMat (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    MaChoi       VARCHAR(20)    NOT NULL UNIQUE,
    IdKhuVuc     INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    SucChua      INT            NULL,
    TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'Trong'
);
GO

-- ── 3.14 Thiết Bị (gộp DanhSachThietBi + ThietBiTaoSong) ───────
CREATE TABLE DanhMuc.ThietBi (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    MaThietBi      VARCHAR(20)    NOT NULL UNIQUE,
    TenThietBi     NVARCHAR(100)  NOT NULL,
    LoaiThietBi    VARCHAR(30)    NOT NULL,
    IdKhuVuc       INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    CongSuat       NVARCHAR(50)   NULL,
    TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'HoatDong',
    NgayMua        DATE           NULL,
    NgayBaoTriTiep DATE           NULL,
    DaXoa          BIT            NOT NULL DEFAULT 0
);
GO

-- ── 3.15 Trò Chơi ───────────────────────────────────────────────
CREATE TABLE DanhMuc.TroChoi (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    TenTroChoi       NVARCHAR(100)  NOT NULL,
    IdKhuVuc         INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    SucChua          INT            NULL,
    DoTuoiToiThieu   INT            NULL,
    ThoiGianLuot     INT            NULL,
    TrangThai        VARCHAR(20)    NOT NULL DEFAULT 'HoatDong',
    DaXoa            BIT            NOT NULL DEFAULT 0
);
GO

-- ── 3.16 Sản Phẩm ───────────────────────────────────────────────
CREATE TABLE DanhMuc.SanPham (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    MaSanPham      VARCHAR(20)    NOT NULL UNIQUE,
    TenSanPham     NVARCHAR(150)  NOT NULL,
    LoaiSanPham    VARCHAR(30)    NOT NULL,
    IdDonViTinh    INT            NOT NULL REFERENCES DanhMuc.DonViTinh(Id),
    DonGia         DECIMAL(15,0)  NULL,
    AnhDaiDien     VARCHAR(255)   NULL,
    LaVatTu        BIT            NOT NULL DEFAULT 0,
    CanQuanLyLo    BIT            NOT NULL DEFAULT 0,
    TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'DangBan',
    DaXoa          BIT            NOT NULL DEFAULT 0,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE(),
    NguoiTao       INT            NULL
);
GO

CREATE NONCLUSTERED INDEX IX_SanPham_Active
ON DanhMuc.SanPham(Id) WHERE DaXoa = 0;

CREATE NONCLUSTERED INDEX IX_SanPham_LoaiSP
ON DanhMuc.SanPham(LoaiSanPham) WHERE DaXoa = 0;
GO

-- ── 3.17 Sản Phẩm Vé (Weak Entity) ─────────────────────────────
CREATE TABLE DanhMuc.SanPham_Ve (
    IdSanPham    INT            PRIMARY KEY REFERENCES DanhMuc.SanPham(Id),
    CanTaoToken  BIT            NOT NULL DEFAULT 0,
    IdThietBi    INT            NULL REFERENCES DanhMuc.ThietBi(Id)
);
GO

-- ── 3.18 Món Ăn (Weak Entity) ───────────────────────────────────
CREATE TABLE DanhMuc.MonAn (
    IdSanPham    INT            PRIMARY KEY REFERENCES DanhMuc.SanPham(Id),
    IdNhaHang    INT            NOT NULL REFERENCES DanhMuc.NhaHang(Id),
    PhanLoai     VARCHAR(30)    NULL,
    MoTaNgan     NVARCHAR(200)  NULL,
    CoDiUng      BIT            NOT NULL DEFAULT 0,
    AnHienMenu   BIT            NOT NULL DEFAULT 1
);
GO

-- ── 3.18b Định Mức Nguyên Liệu / BOM (Bill of Materials) ────────
-- ★ FIX Lỗi 3: Dĩa Cơm Sườn = 200g gạo + 150g sườn + 50ml nước mắm
CREATE TABLE DanhMuc.DinhMucNguyenLieu (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdThanhPham   INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    IdNguyenLieu  INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    SoLuong       DECIMAL(10,3)  NOT NULL,
    GhiChu        NVARCHAR(200)  NULL,
    CONSTRAINT UQ_DinhMuc UNIQUE (IdThanhPham, IdNguyenLieu),
    CONSTRAINT CHK_DinhMuc_KhacNhau CHECK (IdThanhPham != IdNguyenLieu)
);
GO

-- ── 3.19 Cấu Hình Thuế ─────────────────────────────────────────
CREATE TABLE DanhMuc.CauHinhThue (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    MaThue           VARCHAR(20)    NOT NULL UNIQUE,
    TenThue          NVARCHAR(100)  NOT NULL,
    TyLePhanTram     DECIMAL(5,2)   NOT NULL,
    ApDungChoLoaiSP  VARCHAR(50)    NULL,
    ConHoatDong      BIT            NOT NULL DEFAULT 1,
    HieuLucTu        DATE           NOT NULL,
    HieuLucDen       DATE           NULL
);
GO

-- ── 3.20 Bảng Giá (Effective Dating) ────────────────────────────
CREATE TABLE DanhMuc.BangGia (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    IdSanPham    INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    GiaBan       DECIMAL(15,0)  NOT NULL,
    LoaiGia      VARCHAR(20)    NOT NULL DEFAULT 'MacDinh',
    HieuLucTu    DATETIME       NOT NULL,
    HieuLucDen   DATETIME       NOT NULL,
    PhutBlock    INT            NULL,
    PhutTiep     INT            NULL,
    GiaPhuThu    DECIMAL(15,0)  NULL,
    TienCoc      DECIMAL(15,0)  NULL,
    UuTien       INT            NOT NULL DEFAULT 0,
    TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'HoatDong',
    NguoiTao     INT            NULL,
    NgayTao      DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

CREATE INDEX IX_BangGia_TimGia ON DanhMuc.BangGia(IdSanPham, HieuLucTu, HieuLucDen)
WHERE TrangThai = 'HoatDong';
GO

-- ── 3.21 Combo ──────────────────────────────────────────────────
CREATE TABLE DanhMuc.Combo (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    MaCombo      VARCHAR(20)    NOT NULL UNIQUE,
    TenCombo     NVARCHAR(100)  NOT NULL,
    GiaCombo     DECIMAL(15,0)  NOT NULL,
    MoTa         NVARCHAR(500)  NULL,
    TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'HoatDong',
    DaXoa        BIT            NOT NULL DEFAULT 0,
    NgayTao      DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE DanhMuc.ComboChiTiet (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    IdCombo      INT            NOT NULL REFERENCES DanhMuc.Combo(Id),
    IdSanPham    INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    SoLuong      DECIMAL(10,2)  NOT NULL DEFAULT 1
);
GO

-- ── 3.22 Cấu Hình Ngày Lễ ──────────────────────────────────────
CREATE TABLE DanhMuc.CauHinhNgayLe (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    TenNgayLe    NVARCHAR(100)  NOT NULL,
    NgayBatDau   DATE           NOT NULL,
    NgayKetThuc  DATE           NOT NULL,
    Nam          INT            NOT NULL,
    HeSoGia      DECIMAL(5,2)   NOT NULL DEFAULT 1.0,
    CONSTRAINT UQ_NgayLe UNIQUE (TenNgayLe, Nam),
    CONSTRAINT CHK_NgayLe CHECK (NgayKetThuc >= NgayBatDau)
);
GO

-- ── 3.23 FK bổ sung cho NhanVien.IdKhuVuc ───────────────────────
ALTER TABLE DoiTac.NhanVien ADD CONSTRAINT FK_NhanVien_KhuVuc
    FOREIGN KEY (IdKhuVuc) REFERENCES DanhMuc.KhuVuc(Id);
GO


-- ================================================================
-- ████ PHẦN 4: Kho — UNIVERSAL DOCUMENT + LEDGER (6 bảng) ████
-- Pattern: Universal Document, Double-Entry Ledger, Lot Tracking, Soft Allocation
-- ================================================================

-- ── 4.1 Kho Hàng (bao gồm kho ảo) ──────────────────────────────
CREATE TABLE Kho.KhoHang (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    MaKho        VARCHAR(20)    NOT NULL UNIQUE,
    TenKho       NVARCHAR(100)  NOT NULL,
    LaKhoAo      BIT            NOT NULL DEFAULT 0,
    IdKhuVuc     INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'HoatDong'
);
GO

-- ── 4.2 Lô Hàng (Batch/Lot Tracking) ───────────────────────────
CREATE TABLE Kho.LoHang (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    MaLoHang       VARCHAR(30)    NOT NULL UNIQUE,
    IdSanPham      INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    IdNhaCungCap   INT            NULL,  -- FK → DoiTac.NhaCungCap (thêm sau)
    NgaySanXuat    DATE           NULL,
    NgayHetHan     DATE           NULL,
    SoLuongNhap    DECIMAL(18,3)  NOT NULL,
    IdChungTuNhap  INT            NULL,  -- FK → Kho.ChungTu (thêm sau)
    TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'ConHang',
    GhiChu         NVARCHAR(200)  NULL,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- ── 4.3 Chứng Từ Kho (Universal Document — thay TẤT CẢ phiếu) ─
CREATE TABLE Kho.ChungTu (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    MaChungTu      VARCHAR(30)    NOT NULL UNIQUE,
    LoaiChungTu    VARCHAR(20)    NOT NULL,
    IdKhoXuat      INT            NULL REFERENCES Kho.KhoHang(Id),
    IdKhoNhap      INT            NULL REFERENCES Kho.KhoHang(Id),
    IdDoiTac       INT            NULL REFERENCES DoiTac.ThongTin(Id),
    IdDonHang      INT            NULL,  -- FK → BanHang.DonHang (thêm sau)
    IdBaoTri       INT            NULL,  -- FK → VanHanh.BaoTri (thêm sau)
    NgayChungTu    DATETIME       NOT NULL DEFAULT GETDATE(),
    TongGiaTri     DECIMAL(18,0)  NOT NULL DEFAULT 0,
    LyDo           NVARCHAR(300)  NULL,
    TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'Moi',
    IdNguoiTao     INT            NOT NULL,
    IdNguoiDuyet   INT            NULL,
    NgayDuyet      DATETIME       NULL,
    GhiChu         NVARCHAR(500)  NULL,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- ── 4.4 Chi Tiết Chứng Từ Kho ───────────────────────────────────
CREATE TABLE Kho.ChiTietChungTu (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdChungTu      INT            NOT NULL REFERENCES Kho.ChungTu(Id),
    IdSanPham      INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    IdLoHang       INT            NULL REFERENCES Kho.LoHang(Id),
    SoLuong        DECIMAL(18,3)  NOT NULL,
    DonGia         DECIMAL(15,0)  NULL,
    ThanhTien      AS (SoLuong * DonGia) PERSISTED,
    GhiChu         NVARCHAR(200)  NULL
);
GO

-- ── 4.5 Sổ Cái Kho (Double-Entry Ledger — Append Only) ─────────
-- ★ FIX Lỗi 2: NOT NULL cho cả 2 kho — Hàng không thể tự sinh/biến mất
-- Nhập từ NCC → KhoXuat = KHO_NCC (ảo), KhoNhap = KHO_TRUNGTAM (thật)
-- Hủy hỏng   → KhoXuat = KHO_TRUNGTAM,  KhoNhap = KHO_HUY (ảo)
CREATE TABLE Kho.SoCai (
    Id             BIGINT IDENTITY(1,1) PRIMARY KEY,
    IdChungTu      INT            NOT NULL REFERENCES Kho.ChungTu(Id),
    IdSanPham      INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    IdLoHang       INT            NULL REFERENCES Kho.LoHang(Id),
    IdKhoXuat      INT            NOT NULL REFERENCES Kho.KhoHang(Id),
    IdKhoNhap      INT            NOT NULL REFERENCES Kho.KhoHang(Id),
    SoLuong        DECIMAL(18,3)  NOT NULL CHECK (SoLuong > 0),
    DonGia         DECIMAL(15,0)  NULL,
    ThoiGian       DATETIME       NOT NULL DEFAULT GETDATE(),
    NguoiTao       INT            NOT NULL,
    CONSTRAINT CHK_SoCai_KhacKho CHECK (IdKhoXuat != IdKhoNhap)
);
GO

CREATE INDEX IX_SoCai_SanPham_Kho ON Kho.SoCai(IdSanPham, IdKhoNhap);
CREATE INDEX IX_SoCai_ChungTu ON Kho.SoCai(IdChungTu);
GO

-- ── 4.6 Giám Giữ (Soft Allocation) ─────────────────────────────
CREATE TABLE Kho.GiamGiu (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdSanPham      INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    IdKho          INT            NOT NULL REFERENCES Kho.KhoHang(Id),
    IdLoHang       INT            NULL REFERENCES Kho.LoHang(Id),
    SoLuongGiu     DECIMAL(18,3)  NOT NULL,
    IdDonHangNhap  INT            NULL,
    GiuTu          DATETIME       NOT NULL DEFAULT GETDATE(),
    HetHan         DATETIME       NOT NULL,
    TrangThai      VARCHAR(10)    NOT NULL DEFAULT 'DangGiu'
);
GO

-- ── FK bổ sung cho LoHang ────────────────────────────────────────
ALTER TABLE Kho.LoHang ADD CONSTRAINT FK_LoHang_NCC
    FOREIGN KEY (IdNhaCungCap) REFERENCES DoiTac.NhaCungCap(IdDoiTac);
ALTER TABLE Kho.LoHang ADD CONSTRAINT FK_LoHang_ChungTu
    FOREIGN KEY (IdChungTuNhap) REFERENCES Kho.ChungTu(Id);
GO


-- ================================================================
-- ████ PHẦN 5: TaiChinh — UNIVERSAL DOCUMENT + WALLET LEDGER (6 bảng) ████
-- ================================================================

-- ── 5.1 Chứng Từ Tài Chính (Universal — thay PhieuThu + PhieuChi)
CREATE TABLE TaiChinh.ChungTu (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    MaChungTu      VARCHAR(30)    NOT NULL UNIQUE,
    LoaiChungTu    VARCHAR(20)    NOT NULL,
    IdDoiTac       INT            NULL REFERENCES DoiTac.ThongTin(Id),
    IdDonHang      INT            NULL,  -- FK → BanHang.DonHang (thêm sau)
    SoTien         DECIMAL(18,0)  NOT NULL,
    PhuongThuc     VARCHAR(20)    NOT NULL DEFAULT 'TienMat',
    NgayChungTu    DATETIME       NOT NULL DEFAULT GETDATE(),
    MoTa           NVARCHAR(300)  NULL,
    IdNguoiTao     INT            NOT NULL,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- ── 5.2 Ví Điện Tử ──────────────────────────────────────────────
CREATE TABLE TaiChinh.ViDienTu (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdKhachHang    INT            NOT NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    MaVi           VARCHAR(20)    NOT NULL UNIQUE,
    ConHoatDong    BIT            NOT NULL DEFAULT 1,
    RowVersion     ROWVERSION     NOT NULL,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- ── 5.3 Sổ Cái Ví (Wallet Ledger — Append Only) ────────────────
CREATE TABLE TaiChinh.SoCaiVi (
    Id             BIGINT IDENTITY(1,1) PRIMARY KEY,
    IdVi           INT            NOT NULL REFERENCES TaiChinh.ViDienTu(Id),
    LoaiPhep       VARCHAR(10)    NOT NULL,
    SoTien         DECIMAL(15,0)  NOT NULL CHECK (SoTien > 0),
    IdChungTu      INT            NULL REFERENCES TaiChinh.ChungTu(Id),
    MoTa           VARCHAR(50)    NULL,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE(),
    NguoiTao       INT            NOT NULL
);
GO

-- ── 5.4 Thẻ RFID ────────────────────────────────────────────────
CREATE TABLE TaiChinh.TheRFID (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    MaThe          VARCHAR(50)    NOT NULL UNIQUE,
    IdKhachHang    INT            NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    IdViDienTu     INT            NULL REFERENCES TaiChinh.ViDienTu(Id),
    TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'ChuaKichHoat',
    NgayKichHoat   DATETIME       NULL,
    NgayHetHan     DATETIME       NULL,
    TienCocThe     DECIMAL(15,0)  NOT NULL DEFAULT 0,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- ── 5.5 Quy Tắc Điểm Tích Lũy ──────────────────────────────────
CREATE TABLE TaiChinh.QuyTacDiem (
    Id                INT IDENTITY(1,1) PRIMARY KEY,
    TenQuyTac         NVARCHAR(100)  NOT NULL,
    LoaiKhachApDung   VARCHAR(20)    NULL,
    MocChiTieu        DECIMAL(18,0)  NOT NULL DEFAULT 0,
    DiemThuong        INT            NOT NULL DEFAULT 0,
    HeSoNhan          DECIMAL(5,2)   NOT NULL DEFAULT 1.0,
    ConHoatDong       BIT            NOT NULL DEFAULT 1
);
GO

-- ── 5.6 Lịch Sử Điểm ───────────────────────────────────────────
CREATE TABLE TaiChinh.LichSuDiem (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdKhachHang    INT            NOT NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    LoaiGiaoDich   VARCHAR(20)    NOT NULL,
    SoDiem         INT            NOT NULL,
    SoDuSauGD      INT            NOT NULL,
    IdDonHang      INT            NULL,
    MoTa           NVARCHAR(200)  NULL,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO


-- ================================================================
-- ████ PHẦN 6: BanHang — SALES (~16 bảng) ████
-- Pattern: Promotion Engine, Cashier Shift
-- ================================================================

-- ── 6.1 Phiên Thu Ngân ──────────────────────────────────────────
CREATE TABLE BanHang.PhienThuNgan (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien     INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdMayBan       VARCHAR(20)    NOT NULL,
    ThoiGianMo     DATETIME       NOT NULL DEFAULT GETDATE(),
    ThoiGianDong   DATETIME       NULL,
    TienDauCa      DECIMAL(15,0)  NOT NULL DEFAULT 0,
    TienCuoiCa     DECIMAL(15,0)  NULL,
    TienHeThong    DECIMAL(15,0)  NULL,
    ChenhLech      AS (TienCuoiCa - TienDauCa - TienHeThong) PERSISTED,
    GhiChu         NVARCHAR(300)  NULL,
    TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'DangMo'
);
GO

-- ── 6.2 Đơn Hàng ────────────────────────────────────────────────
CREATE TABLE BanHang.DonHang (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    MaDonHang       VARCHAR(20)    NOT NULL UNIQUE,
    IdKhachHang     INT            NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    IdNhanVien      INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdPhienThuNgan  INT            NULL REFERENCES BanHang.PhienThuNgan(Id),
    NguonBan        VARCHAR(20)    NOT NULL DEFAULT 'TrucTiep',
    TongTienHang    DECIMAL(18,0)  NOT NULL DEFAULT 0,
    TongGiamGia     DECIMAL(18,0)  NOT NULL DEFAULT 0,
    TienThueVAT     DECIMAL(18,0)  NOT NULL DEFAULT 0,
    TienPhiDichVu   DECIMAL(18,0)  NOT NULL DEFAULT 0,
    TongThanhToan   DECIMAL(18,0)  NOT NULL DEFAULT 0,
    TrangThai       VARCHAR(30)    NOT NULL DEFAULT 'ChoThanhToan',
    GhiChu          NVARCHAR(300)  NULL,
    RowVersion      ROWVERSION     NOT NULL,
    NgayTao         DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- ── 6.3 Chi Tiết Đơn Hàng ───────────────────────────────────────
CREATE TABLE BanHang.ChiTietDonHang (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdDonHang       INT            NOT NULL REFERENCES BanHang.DonHang(Id),
    IdSanPham       INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    SoLuong         DECIMAL(10,2)  NOT NULL,
    DonGiaThucTe    DECIMAL(15,0)  NOT NULL,
    ThanhTien       AS (SoLuong * DonGiaThucTe) PERSISTED,
    IdLoHang        INT            NULL REFERENCES Kho.LoHang(Id),
    IdCauHinhThue   INT            NULL REFERENCES DanhMuc.CauHinhThue(Id),
    TienThue        DECIMAL(15,0)  NOT NULL DEFAULT 0,
    GhiChu          NVARCHAR(200)  NULL
);
GO

-- ── 6.4 Vé Điện Tử ──────────────────────────────────────────────
CREATE TABLE BanHang.VeDienTu (
    Id              UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    IdDonHang       INT            NOT NULL REFERENCES BanHang.DonHang(Id),
    IdSanPham       INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    IdKhachHang     INT            NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    MaVach          VARCHAR(100)   NOT NULL UNIQUE,
    SoLuotConLai    INT            NOT NULL DEFAULT 1,
    TrangThai       VARCHAR(20)    NOT NULL DEFAULT 'ChuaSuDung',
    ThoiGianQuet    DATETIME       NULL,
    NgayHetHan      DATE           NULL,
    NgayTao         DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- ── 6.5 Khuyến Mãi (Promotion Engine) ───────────────────────────
CREATE TABLE BanHang.KhuyenMai (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    MaKhuyenMai     VARCHAR(20)    NOT NULL UNIQUE,
    TenKhuyenMai    NVARCHAR(150)  NOT NULL,
    IdSuKien        INT            NULL,
    LoaiGiamGia     VARCHAR(20)    NOT NULL,
    GiaTriGiam      DECIMAL(15,0)  NOT NULL,
    DonToiThieu     DECIMAL(15,0)  NULL,
    NgayBatDau      DATETIME       NOT NULL,
    NgayKetThuc     DATETIME       NOT NULL,
    UuTien          INT            NOT NULL DEFAULT 0,
    CoChongCheo     BIT            NOT NULL DEFAULT 0,
    SoLanToiDa      INT            NULL,
    SoLanDaDung     INT            NOT NULL DEFAULT 0,
    TrangThai       BIT            NOT NULL DEFAULT 1,
    DaXoa           BIT            NOT NULL DEFAULT 0,
    NguoiTao        INT            NULL,
    NgayTao         DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- ── 6.6 Điều Kiện Khuyến Mãi (Rules Engine) ────────────────────
CREATE TABLE BanHang.KhuyenMai_DieuKien (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdKhuyenMai     INT            NOT NULL REFERENCES BanHang.KhuyenMai(Id),
    LoaiDieuKien    VARCHAR(30)    NOT NULL,
    GiaTriDieuKien  VARCHAR(100)   NOT NULL
);
GO

-- ── 6.7 Lịch Sử Sử Dụng Khuyến Mãi ────────────────────────────
CREATE TABLE BanHang.KhuyenMai_LichSu (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdKhuyenMai     INT            NOT NULL REFERENCES BanHang.KhuyenMai(Id),
    IdDonHang       INT            NOT NULL REFERENCES BanHang.DonHang(Id),
    IdKhachHang     INT            NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    SoTienGiam      DECIMAL(15,0)  NOT NULL,
    ThoiGian        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- ── 6.8 Đặt Phòng ──────────────────────────────────────────────
CREATE TABLE BanHang.DatPhongChiTiet (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    MaDatPhong      VARCHAR(20)    NOT NULL UNIQUE,
    IdKhachHang     INT            NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    TenNguoiDat     NVARCHAR(100)  NULL,
    SoDienThoai     VARCHAR(20)    NULL,
    NgayNhanPhong   DATE           NOT NULL,
    NgayTraPhong    DATE           NOT NULL,
    TrangThai       VARCHAR(20)    NOT NULL DEFAULT 'ChoNhanPhong',
    IdPhieuThuCoc   INT            NULL REFERENCES TaiChinh.ChungTu(Id),
    GhiChu          NVARCHAR(300)  NULL,
    NgayTao         DATETIME       NOT NULL DEFAULT GETDATE(),
    CONSTRAINT CHK_DatPhong_Ngay CHECK (NgayTraPhong > NgayNhanPhong)
);
GO

-- ★ FIX Lỗi 4: ChiTietDatPhong trỏ về ChiTietDonHang (tiền ở đó)
CREATE TABLE BanHang.ChiTietDatPhong (
    Id                 INT IDENTITY(1,1) PRIMARY KEY,
    IdDatPhong         INT            NOT NULL REFERENCES BanHang.DatPhongChiTiet(Id),
    IdChiTietDonHang   INT            NULL REFERENCES BanHang.ChiTietDonHang(Id),
    IdPhong            INT            NOT NULL REFERENCES DanhMuc.Phong(Id),
    TrangThai          VARCHAR(20)    NOT NULL DEFAULT 'ChoDen',
    NgayCheckIn        DATETIME       NULL,
    NgayCheckOut       DATETIME       NULL
);
GO

-- ── 6.9 Đặt Bàn ────────────────────────────────────────────────
CREATE TABLE BanHang.DatBan (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    MaDatBan        VARCHAR(20)    NOT NULL UNIQUE,
    IdKhachHang     INT            NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    TenNguoiDat     NVARCHAR(100)  NULL,
    SoDienThoai     VARCHAR(20)    NULL,
    NgayDat         DATE           NOT NULL,
    GioDat          TIME           NOT NULL,
    SoNguoi         INT            NOT NULL,
    TrangThai       VARCHAR(20)    NOT NULL DEFAULT 'ChoDen',
    IdPhieuThuCoc   INT            NULL REFERENCES TaiChinh.ChungTu(Id),
    GhiChu          NVARCHAR(300)  NULL,
    NgayTao         DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE BanHang.ChiTietDatBan (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdDatBan        INT            NOT NULL REFERENCES BanHang.DatBan(Id),
    IdBanAn         INT            NOT NULL REFERENCES DanhMuc.BanAn(Id)
);
GO

-- ★ FIX Lỗi 4: Polymorphic Order Lines
-- Mọi thứ QUY RA TIỀN đều nằm trong ChiTietDonHang
-- Bảng vệ tinh chỉ lưu METADATA vận hành và trỏ FK về ChiTietDonHang.Id

-- ── 6.10 Thuê Đồ (metadata vận hành → trỏ về ChiTietDonHang) ───
CREATE TABLE BanHang.ThueDoChiTiet (
    Id                 INT IDENTITY(1,1) PRIMARY KEY,
    IdChiTietDonHang   INT            NOT NULL REFERENCES BanHang.ChiTietDonHang(Id),
    SoLuong            INT            NOT NULL DEFAULT 1,
    ThoiGianThue       DATETIME       NOT NULL DEFAULT GETDATE(),
    ThoiGianTra        DATETIME       NULL,
    TrangThaiCoc       VARCHAR(20)    NOT NULL DEFAULT 'DaCoc',
    TienCoc            DECIMAL(15,0)  NOT NULL DEFAULT 0,
    PhiPhatSinh        DECIMAL(15,0)  NOT NULL DEFAULT 0,
    TrangThai          VARCHAR(20)    NOT NULL DEFAULT 'DangThue',
    GhiChu             NVARCHAR(200)  NULL
);
GO

-- ── 6.11 Thuê Tủ (metadata → trỏ về ChiTietDonHang) ────────────
CREATE TABLE BanHang.ThueTu (
    Id                 INT IDENTITY(1,1) PRIMARY KEY,
    IdChiTietDonHang   INT            NOT NULL REFERENCES BanHang.ChiTietDonHang(Id),
    IdTuDo             INT            NOT NULL REFERENCES DanhMuc.TuDo(Id),
    ThoiGianThue       DATETIME       NOT NULL DEFAULT GETDATE(),
    ThoiGianTra        DATETIME       NULL,
    TrangThai          VARCHAR(20)    NOT NULL DEFAULT 'DangThue'
);
GO

-- ── 6.12 Thuê Chòi (metadata → trỏ về ChiTietDonHang) ──────────
CREATE TABLE BanHang.ThueChoi (
    Id                 INT IDENTITY(1,1) PRIMARY KEY,
    IdChiTietDonHang   INT            NOT NULL REFERENCES BanHang.ChiTietDonHang(Id),
    IdChoiNghiMat      INT            NOT NULL REFERENCES DanhMuc.ChoiNghiMat(Id),
    ThoiGianThue       DATETIME       NOT NULL DEFAULT GETDATE(),
    ThoiGianTra        DATETIME       NULL,
    TrangThai          VARCHAR(20)    NOT NULL DEFAULT 'DangThue'
);
GO

-- ── 6.13 Đặt Chỗ Thú Ăn (metadata → trỏ về ChiTietDonHang) ────
CREATE TABLE BanHang.DatChoThuAn (
    Id                 INT IDENTITY(1,1) PRIMARY KEY,
    IdChiTietDonHang   INT            NOT NULL REFERENCES BanHang.ChiTietDonHang(Id),
    IdChuongTrai       INT            NOT NULL REFERENCES DanhMuc.ChuongTrai(Id),
    SoLuotMua          INT            NOT NULL DEFAULT 1,
    GhiChu             NVARCHAR(200)  NULL
);
GO

-- ── FK bổ sung cho ChungTu → DonHang ────────────────────────────
ALTER TABLE Kho.ChungTu ADD CONSTRAINT FK_ChungTuKho_DonHang
    FOREIGN KEY (IdDonHang) REFERENCES BanHang.DonHang(Id);
ALTER TABLE TaiChinh.ChungTu ADD CONSTRAINT FK_ChungTuTC_DonHang
    FOREIGN KEY (IdDonHang) REFERENCES BanHang.DonHang(Id);
GO


-- ================================================================
-- ████ PHẦN 7: NhanSu — HR (~12 bảng) ████
-- ================================================================

-- ── 7.1 Vai Trò ─────────────────────────────────────────────────
CREATE TABLE NhanSu.VaiTro (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    TenVaiTro    NVARCHAR(50)   NOT NULL UNIQUE,
    MoTa         NVARCHAR(200)  NULL
);
GO

ALTER TABLE DoiTac.NhanVien ADD CONSTRAINT FK_NhanVien_VaiTro
    FOREIGN KEY (IdVaiTro) REFERENCES NhanSu.VaiTro(Id);
GO

-- ── 7.2 Quyền Hạn ──────────────────────────────────────────────
CREATE TABLE NhanSu.QuyenHan (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    MaQuyen      VARCHAR(50)    NOT NULL UNIQUE,
    TenQuyen     NVARCHAR(100)  NOT NULL,
    NhomQuyen    VARCHAR(50)    NULL
);
GO

-- ── 7.3 Phân Quyền (N:N VaiTro ↔ QuyenHan) ────────────────────
CREATE TABLE NhanSu.PhanQuyen (
    IdVaiTro     INT            NOT NULL REFERENCES NhanSu.VaiTro(Id),
    IdQuyenHan   INT            NOT NULL REFERENCES NhanSu.QuyenHan(Id),
    PRIMARY KEY (IdVaiTro, IdQuyenHan)
);
GO

-- ── 7.4 Ca Làm Mẫu ─────────────────────────────────────────────
CREATE TABLE NhanSu.CaLamMau (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    TenCa         NVARCHAR(50)   NOT NULL,
    GioBatDau     TIME           NOT NULL,
    GioKetThuc    TIME           NOT NULL,
    SoGioChuan    AS (DATEDIFF(MINUTE, GioBatDau, GioKetThuc) / 60.0) PERSISTED,
    LoaiCa        VARCHAR(20)    NOT NULL DEFAULT 'ChinhThuc',
    ConHoatDong   BIT            NOT NULL DEFAULT 1
);
GO

-- ── 7.5 Lịch Làm Việc ──────────────────────────────────────────
CREATE TABLE NhanSu.LichLamViec (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien    INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdKhuVuc      INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    NgayLam       DATE           NOT NULL,
    IdCaLamMau    INT            NOT NULL REFERENCES NhanSu.CaLamMau(Id),
    GioBatDau     TIME           NOT NULL,
    GioKetThuc    TIME           NOT NULL,
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'KeHoach',
    GhiChu        NVARCHAR(200)  NULL,
    CONSTRAINT UQ_LichLam UNIQUE (IdNhanVien, NgayLam, GioBatDau)
);
GO

-- ── 7.6 Bảng Chấm Công ─────────────────────────────────────────
CREATE TABLE NhanSu.BangChamCong (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien    INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdLichLamViec INT            NULL REFERENCES NhanSu.LichLamViec(Id),
    ThoiGianVao   DATETIME       NOT NULL,
    ThoiGianRa    DATETIME       NULL,
    SoGioThucTe   AS (
        CASE WHEN ThoiGianRa IS NOT NULL
             THEN CAST(DATEDIFF(MINUTE, ThoiGianVao, ThoiGianRa) / 60.0 AS DECIMAL(5,2))
             ELSE NULL END
    ) PERSISTED,
    LoaiNgayLam   VARCHAR(20)    NOT NULL DEFAULT 'ThuongNgay',
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'DaVao'
);
GO

-- ── 7.7 Nghỉ Bù ────────────────────────────────────────────────
CREATE TABLE NhanSu.NghiBu (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien    INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    NgayLamBu     DATE           NOT NULL,
    SoGioBu       DECIMAL(4,2)   NOT NULL DEFAULT 0,
    DaSuDung      BIT            NOT NULL DEFAULT 0,
    GhiChu        NVARCHAR(200)  NULL
);
GO

-- ── 7.8 Đơn Xin Nghỉ ───────────────────────────────────────────
CREATE TABLE NhanSu.DonXinNghi (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien      INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    LoaiNghi        VARCHAR(30)    NOT NULL,
    NgayBatDau      DATE           NOT NULL,
    NgayKetThuc     DATE           NOT NULL,
    SoNgay          AS (DATEDIFF(DAY, NgayBatDau, NgayKetThuc) + 1) PERSISTED,
    TiLeLuongHuong  DECIMAL(5,2)   NOT NULL,
    NguonChiTra     VARCHAR(10)    NOT NULL,
    LyDo            NVARCHAR(500)  NULL,
    TepDinhKem      VARCHAR(255)   NULL,
    TrangThai       VARCHAR(20)    NOT NULL DEFAULT 'ChoDuyet',
    IdNguoiDuyet    INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    NgayDuyet       DATETIME       NULL,
    GhiChuDuyet     NVARCHAR(200)  NULL,
    NgayTao         DATETIME       NOT NULL DEFAULT GETDATE(),
    CONSTRAINT CHK_DonNghi CHECK (NgayKetThuc >= NgayBatDau)
);
GO

-- ── 7.9 Số Ngày Phép Năm ───────────────────────────────────────
CREATE TABLE NhanSu.SoNgayPhepNam (
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien          INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    Nam                 INT            NOT NULL,
    SoNgayPhepPhatSinh  INT            NOT NULL,
    SoNgayDaDung        INT            NOT NULL DEFAULT 0,
    SoNgayConLai        AS (SoNgayPhepPhatSinh - SoNgayDaDung) PERSISTED,
    CONSTRAINT UQ_PhepNam UNIQUE (IdNhanVien, Nam)
);
GO

-- ── 7.10 Bảng Lương ─────────────────────────────────────────────
CREATE TABLE NhanSu.BangLuong (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien      INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    Thang           TINYINT        NOT NULL CHECK (Thang BETWEEN 1 AND 12),
    Nam             INT            NOT NULL,
    LoaiHopDong     NVARCHAR(20)   NOT NULL,
    LuongCoBan      DECIMAL(15,0)  NULL,
    NgayLamKeHoach  INT            NULL,
    NgayLamThucTe   INT            NULL,
    SoGioThucTe     DECIMAL(7,2)   NULL,
    LuongTheoGio    DECIMAL(10,0)  NULL,
    SoGioTangCa     DECIMAL(5,2)   NOT NULL DEFAULT 0,
    ThuongTangCa    DECIMAL(15,0)  NOT NULL DEFAULT 0,
    PhuCapNguyHiem  DECIMAL(15,0)  NOT NULL DEFAULT 0,
    PhuCapKhac      DECIMAL(15,0)  NOT NULL DEFAULT 0,
    TongTruKyLuat   DECIMAL(15,0)  NOT NULL DEFAULT 0,
    TongGross       DECIMAL(15,0)  NOT NULL DEFAULT 0,
    ThucLinh        DECIMAL(15,0)  NOT NULL DEFAULT 0,
    TrangThai       VARCHAR(20)    NOT NULL DEFAULT 'DangTinh',
    GhiChu          NVARCHAR(300)  NULL,
    NguoiTao        INT            NULL,
    NgayTao         DATETIME       NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UQ_BangLuong UNIQUE (IdNhanVien, Thang, Nam)
);
GO

-- ── 7.11 Chứng Chỉ Nhân Viên ───────────────────────────────────
CREATE TABLE NhanSu.ChungChiNhanVien (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien    INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    LoaiChungChi  VARCHAR(30)    NOT NULL,
    SoChungChi    NVARCHAR(50)   NULL,
    NhaCap        NVARCHAR(100)  NULL,
    NgayCap       DATE           NOT NULL,
    NgayHetHan    DATE           NOT NULL,
    TrangThai     AS (
        CASE
            WHEN NgayHetHan < CAST(GETDATE() AS DATE) THEN 'HetHan'
            WHEN DATEDIFF(DAY, GETDATE(), NgayHetHan) <= 30 THEN 'SapHetHan'
            ELSE 'ConHieuLuc'
        END
    ),
    HinhAnhFile   VARCHAR(255)   NULL
);
GO

-- ── 7.12 Kỷ Luật ───────────────────────────────────────────────
CREATE TABLE NhanSu.KyLuat (
    Id                INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien        INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    NgayApDung        DATE           NOT NULL,
    HinhThuc          VARCHAR(30)    NOT NULL,
    SoTienTru         DECIMAL(15,0)  NOT NULL DEFAULT 0,
    SoNgayDinhChi     INT            NOT NULL DEFAULT 0,
    MoTa              NVARCHAR(500)  NOT NULL,
    IdNguoiQuyetDinh  INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    NgayHetHieuLuc    DATE           NULL,
    NgayTao           DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- ── 7.13 Tai Nạn Lao Động ──────────────────────────────────────
CREATE TABLE NhanSu.TaiNanLaoDong (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien      INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    NgayTaiNan      DATE           NOT NULL,
    LoaiTaiNan      VARCHAR(30)    NOT NULL,
    MucDo           VARCHAR(20)    NOT NULL,
    MoTa            NVARCHAR(MAX)  NOT NULL,
    IdSuCo          INT            NULL,
    NgayNghiBatDau  DATE           NULL,
    NgayNghiKetThuc DATE           NULL,
    SoNgayNghi      AS (
        CASE WHEN NgayNghiBatDau IS NOT NULL AND NgayNghiKetThuc IS NOT NULL
             THEN DATEDIFF(DAY, NgayNghiBatDau, NgayNghiKetThuc) + 1
             ELSE NULL END
    ) PERSISTED,
    TiLeBHXH        DECIMAL(5,2)   NULL,
    TrangThai       VARCHAR(20)    NOT NULL DEFAULT 'DangDieuTri',
    NgayTao         DATETIME       NOT NULL DEFAULT GETDATE()
);
GO


-- ================================================================
-- ████ PHẦN 8: VanHanh — OPERATIONS (~15 bảng) ████
-- ================================================================

-- ── 8.1 Sự Kiện ─────────────────────────────────────────────────
CREATE TABLE VanHanh.SuKien (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    TenSuKien    NVARCHAR(200)  NOT NULL,
    MoTa         NVARCHAR(MAX)  NULL,
    NgayBatDau   DATETIME       NOT NULL,
    NgayKetThuc  DATETIME       NULL,
    IdKhuVuc     INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'SapDienRa',
    HinhAnh      VARCHAR(255)   NULL,
    NgayTao      DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

ALTER TABLE BanHang.KhuyenMai ADD CONSTRAINT FK_KhuyenMai_SuKien
    FOREIGN KEY (IdSuKien) REFERENCES VanHanh.SuKien(Id);
GO

-- ── 8.2 Sự Cố ──────────────────────────────────────────────────
CREATE TABLE VanHanh.SuCo (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    MaSuCo        VARCHAR(20)    NOT NULL UNIQUE,
    LoaiSuCo      VARCHAR(30)    NOT NULL,
    MucDo         VARCHAR(20)    NOT NULL,
    MoTa          NVARCHAR(MAX)  NOT NULL,
    IdKhuVuc      INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    IdThietBi     INT            NULL REFERENCES DanhMuc.ThietBi(Id),
    ThoiGianXayRa DATETIME       NOT NULL DEFAULT GETDATE(),
    IdNguoiBaoCao INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'MoiGhi',
    BienPhapXuLy  NVARCHAR(500)  NULL,
    ChiPhiXuLy    DECIMAL(15,0)  NULL,
    NgayTao       DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

ALTER TABLE NhanSu.TaiNanLaoDong ADD CONSTRAINT FK_TaiNan_SuCo
    FOREIGN KEY (IdSuCo) REFERENCES VanHanh.SuCo(Id);
GO

-- ── 8.3 Thất Lạc ────────────────────────────────────────────────
CREATE TABLE VanHanh.ThatLac (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    MoTaDoVat     NVARCHAR(200)  NOT NULL,
    ViTriTimThay  NVARCHAR(200)  NULL,
    IdKhuVuc      INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    NgayTimThay   DATETIME       NOT NULL DEFAULT GETDATE(),
    IdNguoiNhan   INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'ChuaTraLai',
    NgayTraLai    DATETIME       NULL,
    NguoiNhanLai  NVARCHAR(100)  NULL,
    GhiChu        NVARCHAR(300)  NULL
);
GO

-- ── 8.4 Đánh Giá Dịch Vụ ───────────────────────────────────────
CREATE TABLE VanHanh.DanhGiaDichVu (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdKhachHang   INT            NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    IdDonHang     INT            NULL REFERENCES BanHang.DonHang(Id),
    DiemDanhGia   TINYINT        NOT NULL CHECK (DiemDanhGia BETWEEN 1 AND 5),
    NoiDung       NVARCHAR(500)  NULL,
    IdKhuVuc      INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    NgayTao       DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- ── 8.5 Bảo Trì ────────────────────────────────────────────────
CREATE TABLE VanHanh.BaoTri (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdThietBi     INT            NULL REFERENCES DanhMuc.ThietBi(Id),
    IdKhuVuc      INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    LoaiBaoTri    VARCHAR(20)    NOT NULL,
    NgayBaoTri    DATE           NOT NULL,
    NoiDung       NVARCHAR(500)  NOT NULL,
    ChiPhi        DECIMAL(15,0)  NULL,
    IdNguoiThucHien INT          NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'KeHoach',
    NgayTao       DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

ALTER TABLE Kho.ChungTu ADD CONSTRAINT FK_ChungTuKho_BaoTri
    FOREIGN KEY (IdBaoTri) REFERENCES VanHanh.BaoTri(Id);
GO

-- ── 8.6 Chi Tiết Vật Tư Bảo Trì ────────────────────────────────
CREATE TABLE VanHanh.ChiTietVatTuBaoTri (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdBaoTri      INT            NOT NULL REFERENCES VanHanh.BaoTri(Id),
    IdSanPham     INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    SoLuong       INT            NOT NULL,
    DonGia        DECIMAL(15,0)  NULL,
    IdChungTuXuat INT            NULL REFERENCES Kho.ChungTu(Id)
);
GO

-- ── 8.7 Ca Trực Cứu Hộ ─────────────────────────────────────────
CREATE TABLE VanHanh.CaTrucCuuHo (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien    INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdKhuVuc      INT            NOT NULL REFERENCES DanhMuc.KhuVuc(Id),
    NgayTruc      DATE           NOT NULL,
    CaTruc        VARCHAR(20)    NOT NULL,
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'ChuaBatDau',
    GhiChu        NVARCHAR(200)  NULL,
    CONSTRAINT UQ_CaTruc UNIQUE (IdNhanVien, NgayTruc, CaTruc)
);
GO

-- ── 8.8 Chất Lượng Nước ─────────────────────────────────────────
CREATE TABLE VanHanh.ChatLuongNuoc (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdKhuVuc      INT            NOT NULL REFERENCES DanhMuc.KhuVuc(Id),
    NgayDo        DATETIME       NOT NULL DEFAULT GETDATE(),
    NhietDo       DECIMAL(5,2)   NULL,
    DoPH          DECIMAL(4,2)   NULL,
    DoClo         DECIMAL(5,3)   NULL,
    DoDoc         DECIMAL(5,2)   NULL,
    DatChuan      BIT            NOT NULL DEFAULT 1,
    IdNguoiDo     INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    GhiChu        NVARCHAR(200)  NULL
);
GO

-- ── 8.9 Lịch Tạo Sóng ──────────────────────────────────────────
CREATE TABLE VanHanh.LichTaoSong (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdKhuVuc      INT            NOT NULL REFERENCES DanhMuc.KhuVuc(Id),
    IdThietBi     INT            NULL REFERENCES DanhMuc.ThietBi(Id),
    NgayChay      DATE           NOT NULL,
    GioBatDau     TIME           NOT NULL,
    GioKetThuc    TIME           NOT NULL,
    CheDoSong     NVARCHAR(50)   NULL,
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'KeHoach'
);
GO

-- ── 8.10 Lịch Sử Quét Vé ───────────────────────────────────────
CREATE TABLE VanHanh.LichSuQuetVe (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdVeDienTu    UNIQUEIDENTIFIER NOT NULL REFERENCES BanHang.VeDienTu(Id),
    ThoiGianQuet  DATETIME       NOT NULL DEFAULT GETDATE(),
    KetQua        VARCHAR(20)    NOT NULL,
    IdThietBi     INT            NULL REFERENCES DanhMuc.ThietBi(Id),
    GhiChu        NVARCHAR(200)  NULL
);
GO

-- ── 8.11 Bãi Đỗ Xe ─────────────────────────────────────────────
CREATE TABLE VanHanh.BaiDoXe (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    TenBai        NVARCHAR(100)  NOT NULL,
    SucChua       INT            NOT NULL,
    SoDangDo      INT            NOT NULL DEFAULT 0,
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'HoatDong'
);
GO

-- ── 8.12 Lượt Vào Ra Bãi Xe ────────────────────────────────────
CREATE TABLE VanHanh.LuotVaoRaBaiXe (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdBaiDoXe     INT            NOT NULL REFERENCES VanHanh.BaiDoXe(Id),
    BienSoXe      VARCHAR(20)    NOT NULL,
    LoaiXe        VARCHAR(20)    NOT NULL,
    ThoiGianVao   DATETIME       NOT NULL DEFAULT GETDATE(),
    ThoiGianRa    DATETIME       NULL,
    AnhBienSoVao  VARCHAR(255)   NULL,
    AnhBienSoRa   VARCHAR(255)   NULL,
    IdDonHang     INT            NULL REFERENCES BanHang.DonHang(Id),
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'DangGui'
);
GO

-- ── 8.13 Vé Đỗ Xe Chi Tiết ─────────────────────────────────────
CREATE TABLE VanHanh.VeDoXeChiTiet (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdLuotVaoRa   INT            NOT NULL REFERENCES VanHanh.LuotVaoRaBaiXe(Id),
    MaVeXe        VARCHAR(50)    NOT NULL UNIQUE,
    PhiGuiXe      DECIMAL(15,0)  NOT NULL DEFAULT 0,
    DaThanhToan   BIT            NOT NULL DEFAULT 0
);
GO

-- ── 8.14 Kiosk ──────────────────────────────────────────────────
CREATE TABLE VanHanh.Kiosk (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    MaKiosk       VARCHAR(20)    NOT NULL UNIQUE,
    TenKiosk      NVARCHAR(100)  NOT NULL,
    IdKhuVuc      INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'HoatDong'
);
GO


-- ================================================================
-- ████ PHẦN 9: VanHanh.TruongDua — TRƯỜNG ĐUA (7 bảng) ████
-- ================================================================

CREATE TABLE VanHanh.DuongDua (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    TenDuong      NVARCHAR(100)  NOT NULL,
    ChieuDai      DECIMAL(8,2)   NULL,
    IdKhuVuc      INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'HoatDong'
);
GO

CREATE TABLE VanHanh.LoaiHinhDua (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai       NVARCHAR(50)   NOT NULL,
    MoTa          NVARCHAR(200)  NULL
);
GO

CREATE TABLE VanHanh.NguaDua (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    TenNgua       NVARCHAR(100)  NOT NULL,
    Tuoi          INT            NULL,
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'SanSang',
    HinhAnh       VARCHAR(255)   NULL
);
GO

CREATE TABLE VanHanh.PhuongTienDua (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    TenPhuongTien NVARCHAR(100)  NOT NULL,
    LoaiPhuongTien VARCHAR(20)   NOT NULL,
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'SanSang'
);
GO

CREATE TABLE VanHanh.GiaiDua (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    TenGiai       NVARCHAR(200)  NOT NULL,
    IdLoaiHinhDua INT            NULL REFERENCES VanHanh.LoaiHinhDua(Id),
    NgayBatDau    DATE           NOT NULL,
    NgayKetThuc   DATE           NULL,
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'SapDienRa'
);
GO

CREATE TABLE VanHanh.LichThiDau (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdGiaiDua     INT            NOT NULL REFERENCES VanHanh.GiaiDua(Id),
    IdDuongDua    INT            NOT NULL REFERENCES VanHanh.DuongDua(Id),
    ThoiGianBatDau DATETIME      NOT NULL,
    VongDua       INT            NULL,
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'ChuaBatDau'
);
GO

CREATE TABLE VanHanh.KetQuaDua (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdLichThiDau  INT            NOT NULL REFERENCES VanHanh.LichThiDau(Id),
    IdVanDongVien INT            NULL REFERENCES DoiTac.VanDongVien(IdDoiTac),
    IdNguaDua     INT            NULL REFERENCES VanHanh.NguaDua(Id),
    IdPhuongTien  INT            NULL REFERENCES VanHanh.PhuongTienDua(Id),
    HangVeDich    INT            NULL,
    ThoiGianHoan  DECIMAL(10,3)  NULL,
    GhiChu        NVARCHAR(200)  NULL
);
GO

CREATE TABLE VanHanh.KhanDai (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    TenKhanDai    NVARCHAR(100)  NOT NULL,
    IdKhuVuc      INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    SucChua       INT            NOT NULL,
    LoaiKhanDai   VARCHAR(20)    NOT NULL DEFAULT 'ThuongGia'
);
GO

CREATE TABLE VanHanh.ViTriNgoi (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdKhanDai     INT            NOT NULL REFERENCES VanHanh.KhanDai(Id),
    MaGhe         VARCHAR(10)    NOT NULL,
    Hang          VARCHAR(5)     NOT NULL,
    Cot           INT            NOT NULL,
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'Trong',
    RowVersion    ROWVERSION     NOT NULL,
    CONSTRAINT UQ_ViTriNgoi UNIQUE (IdKhanDai, Hang, Cot)
);
GO

-- ── Đoàn Khách Dịch Vụ (liên kết đoàn → dịch vụ đặc biệt) ────
CREATE TABLE VanHanh.DoanKhach_DichVu (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdDoanKhach   INT            NOT NULL REFERENCES DoiTac.ThongTin(Id),
    IdSanPham     INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    SoLuong       INT            NOT NULL,
    DonGiaDacBiet DECIMAL(15,0)  NULL,
    GhiChu        NVARCHAR(200)  NULL
);
GO


-- ================================================================
-- ████ PHẦN 10: VIEWs ████
-- ================================================================

-- ★ FIX CROSS JOIN: chỉ đọc dòng có giao dịch, không tạo tích Đề-các
CREATE VIEW Kho.V_TonKho AS
SELECT
    t.IdSanPham,
    t.IdKho,
    kh.TenKho,
    sp.TenSanPham,
    t.TonHienTai
FROM (
    SELECT IdSanPham, IdKho, SUM(SoLuong) AS TonHienTai
    FROM (
        SELECT IdSanPham, IdKhoNhap AS IdKho,  SoLuong FROM Kho.SoCai
        UNION ALL
        SELECT IdSanPham, IdKhoXuat AS IdKho, -SoLuong FROM Kho.SoCai
    ) raw
    GROUP BY IdSanPham, IdKho
    HAVING SUM(SoLuong) > 0
) t
JOIN Kho.KhoHang kh ON t.IdKho = kh.Id AND kh.LaKhoAo = 0
JOIN DanhMuc.SanPham sp ON t.IdSanPham = sp.Id;
GO

-- ── Tồn kho theo lô (cũng dùng UNION ALL, không CROSS JOIN) ────
CREATE VIEW Kho.V_TonTheoLo AS
SELECT
    lh.Id AS IdLoHang, lh.MaLoHang,
    lh.IdSanPham, sp.TenSanPham,
    lh.NgayHetHan,
    t.TonLo
FROM (
    SELECT IdLoHang, SUM(SoLuong) AS TonLo
    FROM (
        SELECT IdLoHang, SoLuong FROM Kho.SoCai WHERE IdLoHang IS NOT NULL
        UNION ALL
        SELECT IdLoHang, -SoLuong FROM Kho.SoCai WHERE IdLoHang IS NOT NULL
              AND IdKhoXuat IS NOT NULL
    ) raw
    WHERE IdLoHang IS NOT NULL
    GROUP BY IdLoHang
    HAVING SUM(SoLuong) > 0
) t
JOIN Kho.LoHang lh ON t.IdLoHang = lh.Id
JOIN DanhMuc.SanPham sp ON lh.IdSanPham = sp.Id
WHERE lh.TrangThai != 'DaHuy';
GO

-- ── Cảnh báo hết hạn (7 ngày tới) ──────────────────────────────
CREATE VIEW Kho.V_CanhBaoHetHan AS
SELECT lh.*, sp.TenSanPham,
    DATEDIFF(DAY, GETDATE(), lh.NgayHetHan) AS SoNgayConLai
FROM Kho.LoHang lh
JOIN DanhMuc.SanPham sp ON lh.IdSanPham = sp.Id
WHERE lh.NgayHetHan IS NOT NULL
  AND lh.NgayHetHan <= DATEADD(DAY, 7, GETDATE())
  AND lh.TrangThai = 'ConHang';
GO

-- ── Số dư ví ────────────────────────────────────────────────────
CREATE VIEW TaiChinh.V_SoDuVi AS
SELECT
    IdVi,
    SUM(CASE WHEN LoaiPhep = 'CONG' THEN SoTien ELSE 0 END)
  - SUM(CASE WHEN LoaiPhep = 'TRU'  THEN SoTien ELSE 0 END) AS SoDuKhaDung
FROM TaiChinh.SoCaiVi
GROUP BY IdVi;
GO

-- ── Chứng chỉ sắp hết hạn ──────────────────────────────────────
CREATE VIEW NhanSu.V_ChungChiSapHetHan AS
SELECT
    dt.HoTen, nv.MaNhanVien,
    cc.LoaiChungChi, cc.SoChungChi, cc.NgayHetHan,
    DATEDIFF(DAY, GETDATE(), cc.NgayHetHan) AS SoNgayConLai,
    cc.TrangThai
FROM NhanSu.ChungChiNhanVien cc
JOIN DoiTac.NhanVien nv ON cc.IdNhanVien = nv.IdDoiTac
JOIN DoiTac.ThongTin dt ON nv.IdDoiTac = dt.Id
WHERE cc.TrangThai IN ('SapHetHan', 'HetHan')
  AND dt.DaXoa = 0;
GO


-- ================================================================
-- ████ PHẦN 10b: TRIGGER CHỐNG CHỒNG CHÉO BẢNG GIÁ ████
-- ================================================================

-- ★ FIX Lỗi 5: Chống overlap thời gian → POS không bao giờ trả 2 giá
CREATE TRIGGER DanhMuc.trg_BangGia_ChongOverlap
ON DanhMuc.BangGia
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN DanhMuc.BangGia bg ON bg.IdSanPham = i.IdSanPham
            AND bg.LoaiGia = i.LoaiGia
            AND bg.Id != i.Id
            AND bg.TrangThai = 'HoatDong'
            AND i.TrangThai = 'HoatDong'
            AND bg.HieuLucTu < i.HieuLucDen
            AND bg.HieuLucDen > i.HieuLucTu
    )
    BEGIN
        RAISERROR(N'Lỗi: Khoảng thời gian giá bị chồng chéo cho cùng sản phẩm và loại giá!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO


-- ================================================================
-- ████ PHẦN 11: SEED DATA ████
-- ================================================================

-- (Bộ Đếm Mã đã thay bằng SQL SEQUENCE ở PHẦN 1 — không cần seed)

-- ── Kho Hàng (thật + ảo) ────────────────────────────────────────
INSERT INTO Kho.KhoHang (MaKho, TenKho, LaKhoAo) VALUES
('KHO_TRUNGTAM', N'Kho Trung Tâm', 0),
('KHO_NHABEP',   N'Kho Nhà Bếp', 0),
('KHO_KIOSK_01', N'Kho Kiosk Biển', 0),
('KHO_KIOSK_02', N'Kho Kiosk Trường Đua', 0),
('KHO_NCC',      N'Kho Nhà Cung Cấp (ảo)', 1),
('KHO_KHACH',    N'Kho Khách Hàng (ảo)', 1),
('KHO_HUY',      N'Kho Hủy & Hao Hụt (ảo)', 1),
('KHO_BAOHANH',  N'Kho Bảo Hành NCC (ảo)', 1);
GO

-- ── Vai Trò ─────────────────────────────────────────────────────
INSERT INTO NhanSu.VaiTro (TenVaiTro, MoTa) VALUES
(N'Admin',       N'Quản trị hệ thống'),
(N'QuanLy',      N'Quản lý chung'),
(N'ThuNgan',     N'Thu ngân POS'),
(N'NhanVien',    N'Nhân viên thường'),
(N'LeTan',       N'Lễ tân khách sạn'),
(N'ThuKho',      N'Thủ kho'),
(N'BepTruong',   N'Bếp trưởng'),
(N'CuuHo',       N'Nhân viên cứu hộ');
GO

-- ── Đơn Vị Tính ─────────────────────────────────────────────────
INSERT INTO DanhMuc.DonViTinh (MaDonVi, TenDonVi) VALUES
('Cai',   N'Cái'),    ('Lon',   N'Lon'),     ('Chai',  N'Chai'),
('Kg',    N'Kilogram'), ('Lit',  N'Lít'),     ('Hop',   N'Hộp'),
('Thung', N'Thùng'),  ('Suat',  N'Suất'),    ('Ve',    N'Vé'),
('Luot',  N'Lượt'),   ('Dem',   N'Đêm'),     ('Gio',   N'Giờ'),
('Phut',  N'Phút'),   ('GoiDv', N'Gói DV');
GO

-- ── Ca Làm Mẫu ──────────────────────────────────────────────────
INSERT INTO NhanSu.CaLamMau (TenCa, GioBatDau, GioKetThuc, LoaiCa) VALUES
(N'Sáng',        '06:00', '14:00', 'ChinhThuc'),
(N'Chiều',       '14:00', '22:00', 'ChinhThuc'),
(N'Đêm',         '22:00', '06:00', 'ChinhThuc'),
(N'Hành chính',  '08:00', '17:00', 'ChinhThuc'),
(N'PartTime sáng', '06:00', '10:00', 'BanThoi'),
(N'PartTime chiều','14:00', '18:00', 'BanThoi'),
(N'Toàn ngày',   '06:00', '22:00', 'DacBiet');
GO

-- ── Cấu Hình Thuế ──────────────────────────────────────────────
INSERT INTO DanhMuc.CauHinhThue (MaThue, TenThue, TyLePhanTram, ApDungChoLoaiSP, HieuLucTu) VALUES
('VAT_10',   N'VAT 10%',           10.00, NULL,      '2024-01-01'),
('VAT_8',    N'VAT 8% (giảm)',     8.00,  NULL,      '2024-01-01'),
('PHI_DV_5', N'Phí dịch vụ 5%',   5.00,  'LuuTru',  '2024-01-01');
GO

-- ── Cấu Hình Hệ Thống ──────────────────────────────────────────
INSERT INTO HeThong.CauHinh VALUES ('DIEM_QUY_DOI_1000D', '1', N'1000 VNĐ = 1 điểm', GETDATE(), NULL);
INSERT INTO HeThong.CauHinh VALUES ('DIEM_HET_HAN_THANG', '12', N'Điểm hết hạn sau N tháng', GETDATE(), NULL);
INSERT INTO HeThong.CauHinh VALUES ('SO_PHUT_GIU_TON_KHO', '15', N'Giám giữ tồn kho (phút)', GETDATE(), NULL);
INSERT INTO HeThong.CauHinh VALUES ('HE_SO_TANG_CA', '1.5', N'Hệ số lương tăng ca', GETDATE(), NULL);
INSERT INTO HeThong.CauHinh VALUES ('NGAY_PHEP_MAC_DINH', '12', N'Số ngày phép/năm mặc định', GETDATE(), NULL);
INSERT INTO HeThong.CauHinh VALUES ('NGON_NGU_MAC_DINH', 'vi', N'Ngôn ngữ mặc định', GETDATE(), NULL);
GO

-- ── Từ Điển — SEED (30+ categories) ─────────────────────────────

-- Giới Tính
INSERT INTO HeThong.TuDien VALUES ('GIOI_TINH', 'Nam',  N'Nam', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('GIOI_TINH', 'Nu',   N'Nữ', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('GIOI_TINH', 'Khac', N'Khác', 3, NULL, NULL, 1);

-- Trạng Thái Nhân Viên
INSERT INTO HeThong.TuDien VALUES ('NV_TRANG_THAI', 'DangLam',    N'Đang làm', 1, '#28A745', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_TRANG_THAI', 'NghiPhep',   N'Nghỉ phép', 2, '#FFC107', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_TRANG_THAI', 'DinhChi',    N'Đình chỉ', 3, '#DC3545', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_TRANG_THAI', 'DaNghiViec', N'Đã nghỉ việc', 4, '#6C757D', NULL, 1);

-- Loại Khối
INSERT INTO HeThong.TuDien VALUES ('NV_LOAI_KHOI', 'VanPhong',  N'Văn phòng', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_LOAI_KHOI', 'ThucDia',   N'Thực địa', 2, NULL, NULL, 1);

-- Loại Hợp Đồng
INSERT INTO HeThong.TuDien VALUES ('NV_LOAI_HOP_DONG', 'FullTime', N'Toàn thời gian', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_LOAI_HOP_DONG', 'PartTime', N'Bán thời gian', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_LOAI_HOP_DONG', 'TheoMua',  N'Theo mùa', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_LOAI_HOP_DONG', 'Intern',   N'Thực tập', 4, NULL, NULL, 1);

-- Nhóm Công Việc
INSERT INTO HeThong.TuDien VALUES ('NV_NHOM_CONG_VIEC', 'ThuongNgay', N'Thường ngày', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_NHOM_CONG_VIEC', 'NguyCap1',   N'Nguy cấp cấp 1', 2, '#FFC107', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_NHOM_CONG_VIEC', 'NguyCap2',   N'Nguy cấp cấp 2', 3, '#DC3545', NULL, 1);

-- Loại Khách Hàng
INSERT INTO HeThong.TuDien VALUES ('KH_LOAI_KHACH', 'CaNhan',          N'Cá nhân', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_LOAI_KHACH', 'Doan',            N'Đoàn', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_LOAI_KHACH', 'DoanhNghiep',     N'Doanh nghiệp', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_LOAI_KHACH', 'HocSinhSinhVien', N'Học sinh/Sinh viên', 4, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_LOAI_KHACH', 'VIP',             N'VIP', 5, '#FFD700', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_LOAI_KHACH', 'VVIP',            N'VVIP', 6, '#FF6347', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_LOAI_KHACH', 'NoiBo',           N'Nội bộ', 7, NULL, NULL, 1);

-- Hạng Thành Viên
INSERT INTO HeThong.TuDien VALUES ('KH_HANG_TV', 'BacSilver', N'Bạc (Silver)', 1, '#C0C0C0', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_HANG_TV', 'VangGold',  N'Vàng (Gold)', 2, '#FFD700', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_HANG_TV', 'Kim',       N'Kim Cương (Diamond)', 3, '#B9F2FF', NULL, 1);

-- Trạng Thái Phòng
INSERT INTO HeThong.TuDien VALUES ('PHONG_TRANG_THAI', 'Trong',      N'Trống', 1, '#28A745', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHONG_TRANG_THAI', 'DaDat',      N'Đã đặt', 2, '#17A2B8', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHONG_TRANG_THAI', 'DangSuDung', N'Đang sử dụng', 3, '#FFC107', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHONG_TRANG_THAI', 'DonDep',     N'Đang dọn dẹp', 4, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHONG_TRANG_THAI', 'BaoTri',     N'Bảo trì', 5, '#DC3545', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHONG_TRANG_THAI', 'KhoaDai',    N'Khóa dài hạn', 6, '#343A40', NULL, 1);

-- Trạng Thái Bàn Ăn
INSERT INTO HeThong.TuDien VALUES ('BAN_AN_TRANG_THAI', 'Trong',      N'Trống', 1, '#28A745', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BAN_AN_TRANG_THAI', 'DaDat',      N'Đã đặt', 2, '#17A2B8', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BAN_AN_TRANG_THAI', 'DangSuDung', N'Đang sử dụng', 3, '#FFC107', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BAN_AN_TRANG_THAI', 'DonDep',     N'Đang dọn', 4, '#6C757D', NULL, 1);

-- Trạng Thái Đơn Hàng
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'ChoThanhToan',   N'Chờ thanh toán', 1, '#FFC107', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'DaThanhToan',    N'Đã thanh toán', 2, '#28A745', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'DangXuLy',       N'Đang xử lý', 3, '#17A2B8', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'GhiNoCongTy',    N'Ghi nợ công ty', 4, '#6610F2', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'DaHuy',          N'Đã hủy', 5, '#DC3545', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'HoanTienMotPhan',N'Hoàn tiền 1 phần', 6, '#FD7E14', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'DaHoanTien',     N'Đã hoàn tiền', 7, '#6C757D', NULL, 1);

-- Nguồn Bán
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_NGUON', 'TrucTiep', N'Trực tiếp', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_NGUON', 'Web',      N'Website', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_NGUON', 'Kiosk',    N'Kiosk', 3, NULL, NULL, 1);

-- Trạng Thái Vé
INSERT INTO HeThong.TuDien VALUES ('VE_TRANG_THAI', 'ChuaSuDung', N'Chưa sử dụng', 1, '#17A2B8', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('VE_TRANG_THAI', 'DaSuDung',   N'Đã sử dụng', 2, '#28A745', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('VE_TRANG_THAI', 'HetLuot',    N'Hết lượt', 3, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('VE_TRANG_THAI', 'HetHan',     N'Hết hạn', 4, '#DC3545', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('VE_TRANG_THAI', 'DaHuy',      N'Đã hủy', 5, '#343A40', NULL, 1);

-- Loại Chứng Từ Kho
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'NHAP_MUA',    N'Nhập mua từ NCC', 1, '#28A745', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'XUAT_BAN',    N'Xuất bán cho khách', 2, '#17A2B8', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'TRA_NCC',     N'Trả hàng cho NCC', 3, '#FFC107', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'KHACH_TRA',   N'Khách trả hàng', 4, '#FD7E14', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'HUY_HONG',    N'Hủy / Hao hụt', 5, '#DC3545', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'CHUYEN_KHO',  N'Chuyển kho', 6, '#6610F2', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'KIEM_KE',     N'Kiểm kê điều chỉnh', 7, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'XUAT_BAOTRI', N'Xuất vật tư bảo trì', 8, '#20C997', NULL, 1);

-- Loại Chứng Từ Tài Chính
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'THU_THANHTOAN', N'Thu thanh toán', 1, '#28A745', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'THU_COC',       N'Thu tiền cọc', 2, '#17A2B8', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'THU_NAP_VI',    N'Nạp ví RFID', 3, '#FFC107', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'CHI_NHAP_HANG', N'Chi nhập hàng', 4, '#DC3545', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'CHI_LUONG',     N'Chi lương', 5, '#DC3545', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'CHI_BAO_TRI',   N'Chi bảo trì', 6, '#DC3545', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'HOAN_TIEN',     N'Hoàn tiền khách', 7, '#FD7E14', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'HOAN_COC',      N'Hoàn tiền cọc', 8, '#6C757D', NULL, 1);

-- Phương Thức Thanh Toán
INSERT INTO HeThong.TuDien VALUES ('PHUONG_THUC_TT', 'TienMat',     N'Tiền mặt', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHUONG_THUC_TT', 'ChuyenKhoan', N'Chuyển khoản', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHUONG_THUC_TT', 'ViRFID',      N'Ví RFID', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHUONG_THUC_TT', 'QR',          N'QR Code', 4, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHUONG_THUC_TT', 'MoMo',        N'Ví MoMo', 5, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHUONG_THUC_TT', 'TheNganHang', N'Thẻ ngân hàng', 6, NULL, NULL, 1);

-- Loại Sản Phẩm
INSERT INTO HeThong.TuDien VALUES ('SAN_PHAM_LOAI', 'Ve',        N'Vé', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SAN_PHAM_LOAI', 'AnUong',    N'Ăn uống', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SAN_PHAM_LOAI', 'DoUong',    N'Đồ uống', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SAN_PHAM_LOAI', 'LuuTru',    N'Lưu trú', 4, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SAN_PHAM_LOAI', 'ThueDo',    N'Thuê đồ', 5, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SAN_PHAM_LOAI', 'DichVu',    N'Dịch vụ', 6, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SAN_PHAM_LOAI', 'TroChoi',   N'Trò chơi', 7, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SAN_PHAM_LOAI', 'GuiXe',     N'Gửi xe', 8, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SAN_PHAM_LOAI', 'VatTu',     N'Vật tư nội bộ', 9, NULL, NULL, 1);

-- Loại Giảm Giá (Khuyến Mãi)
INSERT INTO HeThong.TuDien VALUES ('KHUYEN_MAI_LOAI', 'PhanTram',  N'Phần trăm', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KHUYEN_MAI_LOAI', 'SoTien',    N'Số tiền cố định', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KHUYEN_MAI_LOAI', 'DongGia',   N'Đồng giá', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KHUYEN_MAI_LOAI', 'MuaXTangY', N'Mua X tặng Y', 4, NULL, NULL, 1);

-- Loại Bảng Giá
INSERT INTO HeThong.TuDien VALUES ('BANG_GIA_LOAI', 'MacDinh',  N'Mặc định', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BANG_GIA_LOAI', 'CuoiTuan', N'Cuối tuần', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BANG_GIA_LOAI', 'NgayLe',   N'Ngày lễ', 3, NULL, NULL, 1);

-- Loại Sự Cố
INSERT INTO HeThong.TuDien VALUES ('SU_CO_LOAI', 'Thuong',    N'Thường', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SU_CO_LOAI', 'DuoiNuoc',  N'Dưới nước', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SU_CO_LOAI', 'MatTre',    N'Trẻ lạc/mất', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SU_CO_LOAI', 'DienCo',    N'Điện/cơ khí', 4, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SU_CO_LOAI', 'AnNinh',    N'An ninh', 5, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SU_CO_LOAI', 'Khac',      N'Khác', 6, NULL, NULL, 1);

-- Mức Độ
INSERT INTO HeThong.TuDien VALUES ('MUC_DO', 'Nhe',       N'Nhẹ', 1, '#28A745', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('MUC_DO', 'TrungBinh', N'Trung bình', 2, '#FFC107', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('MUC_DO', 'NangNe',    N'Nặng nề', 3, '#DC3545', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('MUC_DO', 'CucKyNghiemTrong', N'Cực kỳ nghiêm trọng', 4, '#343A40', NULL, 1);

-- Loại Nghỉ
INSERT INTO HeThong.TuDien VALUES ('NGHI_LOAI', 'PhepNam',         N'Phép năm', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NGHI_LOAI', 'NghiOm',          N'Nghỉ ốm', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NGHI_LOAI', 'ThaiSanNu',       N'Thai sản nữ', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NGHI_LOAI', 'ThaiSanNam',      N'Thai sản nam', 4, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NGHI_LOAI', 'TaiNanLaoDong',   N'Tai nạn lao động', 5, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NGHI_LOAI', 'NghiBu',          N'Nghỉ bù', 6, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NGHI_LOAI', 'NghiLe',          N'Nghỉ lễ', 7, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NGHI_LOAI', 'DotXuatCoLuong',  N'Đột xuất có lương', 8, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NGHI_LOAI', 'NghiKhongLuong',  N'Nghỉ không lương', 9, NULL, NULL, 1);

-- Hình Thức Kỷ Luật
INSERT INTO HeThong.TuDien VALUES ('KY_LUAT_HINH_THUC', 'CanhCao',         N'Cảnh cáo', 1, '#FFC107', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KY_LUAT_HINH_THUC', 'TruLuong',        N'Trừ lương', 2, '#FD7E14', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KY_LUAT_HINH_THUC', 'DinhChiCoLuong',  N'Đình chỉ có lương', 3, '#DC3545', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KY_LUAT_HINH_THUC', 'SaThai',          N'Sa thải', 4, '#343A40', NULL, 1);

-- Kết Quả Quét Vé
INSERT INTO HeThong.TuDien VALUES ('KET_QUA_QUET_VE', 'ThanhCong', N'Thành công', 1, '#28A745', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KET_QUA_QUET_VE', 'SaiVe',     N'Sai vé', 2, '#DC3545', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KET_QUA_QUET_VE', 'DaHetLuot', N'Đã hết lượt', 3, '#FFC107', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KET_QUA_QUET_VE', 'VeHetHan',  N'Vé hết hạn', 4, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KET_QUA_QUET_VE', 'DaHuy',     N'Đã hủy', 5, '#343A40', NULL, 1);
GO

-- ── Luồng Trạng Thái — SEED ────────────────────────────────────

-- Đơn Hàng
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('DonHang', 'ChoThanhToan', 'DaThanhToan',     N'Thu ngân thanh toán'),
('DonHang', 'ChoThanhToan', 'DaHuy',           N'Hủy đơn'),
('DonHang', 'DaThanhToan',  'HoanTienMotPhan', N'Hoàn tiền 1 phần'),
('DonHang', 'DaThanhToan',  'DaHoanTien',      N'Hoàn toàn bộ'),
('DonHang', 'DangXuLy',     'DaThanhToan',     N'Xử lý xong'),
('DonHang', 'DangXuLy',     'DaHuy',           N'Hủy khi đang xử lý');

-- Phòng
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('Phong', 'Trong',      'DaDat',      N'Đặt phòng'),
('Phong', 'Trong',      'BaoTri',     N'Chuyển bảo trì'),
('Phong', 'DaDat',      'DangSuDung', N'Check-in'),
('Phong', 'DaDat',      'Trong',      N'Hủy đặt'),
('Phong', 'DangSuDung', 'DonDep',     N'Check-out'),
('Phong', 'DonDep',     'Trong',      N'Dọn xong'),
('Phong', 'BaoTri',     'Trong',      N'Bảo trì xong');

-- Chứng Từ Kho
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('ChungTuKho', 'Moi',      'ChoDuyet',  N'Gửi duyệt'),
('ChungTuKho', 'ChoDuyet', 'DaDuyet',   N'Người duyệt phê duyệt'),
('ChungTuKho', 'ChoDuyet', 'DaHuy',     N'Từ chối'),
('ChungTuKho', 'Moi',      'DaHuy',     N'Tự hủy');
GO

-- ── Admin Account Mặc Định ──────────────────────────────────────
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, LoaiDoiTac)
VALUES (N'Quản Trị Viên', '0000000000', 'CaNhan');

INSERT INTO DoiTac.NhanVien (IdDoiTac, MaNhanVien, IdVaiTro, TrangThai)
VALUES (1, 'NV001', 1, 'DangLam');

INSERT INTO DoiTac.TaiKhoan (IdDoiTac, TenDangNhap, MatKhauHash, LoaiTaiKhoan)
VALUES (1, 'admin', '1', 'NhanVien');

-- Advance NhanVien sequence (NV001 đã gán thủ công)
DECLARE @dummy INT = NEXT VALUE FOR HeThong.Seq_NhanVien;
GO


PRINT N'═══════════════════════════════════════════════════════════';
PRINT N'  ✅ DATABASE ĐẠI NAM V2 — TẠO THÀNH CÔNG!';
PRINT N'  📊 12 Enterprise Patterns | ~90 Bảng | Thuần Việt';
PRINT N'  🛡️  7 Architectural Fixes Applied';
PRINT N'═══════════════════════════════════════════════════════════';
GO
