USE master;
GO
DECLARE @RecreateDb BIT = 1;
IF @RecreateDb = 1 AND DB_ID('DaiNamResort') IS NOT NULL
BEGIN
    ALTER DATABASE DaiNamResort SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE DaiNamResort;
END
GO
IF DB_ID('DaiNamResort') IS NULL
    CREATE DATABASE DaiNamResort;
GO
USE DaiNamResort;
GO

    -- =================== 1. Danh Mục Cơ Bản  ===================
    CREATE TABLE VaiTro (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TenVaiTro NVARCHAR(50) NOT NULL UNIQUE
    );

    CREATE TABLE QuyenHan (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaQuyen VARCHAR(50) NOT NULL UNIQUE,
        MoTa NVARCHAR(200)
    );

    CREATE TABLE PhanQuyen (
        IdVaiTro INT NOT NULL,
        IdQuyen INT NOT NULL,
        PRIMARY KEY (IdVaiTro, IdQuyen)
    );

    CREATE TABLE NhanVien (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) UNIQUE,
        IdVaiTro INT NOT NULL,
        IdNguoiQuanLy INT NULL REFERENCES NhanVien(Id),
        IdKhuVuc INT NULL,
        HoTen NVARCHAR(100) NOT NULL,
        GioiTinh NVARCHAR(10) CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác')),
        NgaySinh DATE,
        ChucVu NVARCHAR(50) NULL,
        BoPhan NVARCHAR(50) NULL,
        DienThoai NVARCHAR(20) NULL,
        Email NVARCHAR(100) NULL UNIQUE,
        Cccd NVARCHAR(20) NULL UNIQUE,
        DiaChi NVARCHAR(255) NULL,
        HinhAnh VARCHAR(255) NULL,
        TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'ThuViec', N'Đang làm việc', N'Tạm nghỉ', N'Nghỉ việc')) DEFAULT N'Đang làm việc',
        TenDangNhap NVARCHAR(50) NULL UNIQUE,
        MatKhau NVARCHAR(100) NULL,
        GhiChu NVARCHAR(MAX) NULL,
        -- HR Module
        LoaiKhoi NVARCHAR(20) NOT NULL DEFAULT 'VanHanh'
            CONSTRAINT ChkNV_LoaiKhoi CHECK (LoaiKhoi IN ('VanHanh', 'HanhChinh')),
        LoaiHopDong  NVARCHAR(20) NOT NULL DEFAULT 'FullTime'
            CONSTRAINT ChkNV_LoaiHopDong CHECK (LoaiHopDong IN ('FullTime','PartTime','TheoMua','Intern')),
        NhomCongViec NVARCHAR(20) NOT NULL DEFAULT 'ThuongThuong'
            CONSTRAINT ChkNV_NhomCongViec CHECK (NhomCongViec IN ('ThuongThuong','NangNhocNguyHiem','DacBietNguyHiem')),
        LuongCoBan   DECIMAL(15,0) NULL,   -- FullTime: lương/tháng
        LuongTheoGio DECIMAL(10,0) NULL,   -- PartTime/Intern: lương/giờ
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NULL,
        IsDeleted BIT NOT NULL DEFAULT 0
    );

    CREATE TABLE DoanKhach (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaBooking VARCHAR(20) NULL UNIQUE,          -- Mã quét tại cổng
        TenDoan NVARCHAR(100) NOT NULL,
        MaSoThue NVARCHAR(20) NULL,
        NguoiDaiDien NVARCHAR(100) NULL,
        DienThoaiLienHe NVARCHAR(20) NULL,
        ChietKhau DECIMAL(5,2) NOT NULL DEFAULT 0 CHECK (ChietKhau >= 0 AND ChietKhau <= 100),
        SoLuongKhach INT NOT NULL DEFAULT 0 CHECK (SoLuongKhach >= 0), 
        NgayDen DATE NULL,                          -- Ngày đoàn dự kiến đến
        NgayDi DATE NULL,                           -- Ngày đoàn dự kiến đi (NULL = trong ngày)
        TrangThai NVARCHAR(20) CHECK (TrangThai IN (N'DaDat', N'DangPhucVu', N'DaXuatVe', N'DaHoanTat', N'HetHan', N'DaHuy')) DEFAULT N'DaDat',
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NULL,
        CreatedBy INT NULL,
        IsDeleted BIT NOT NULL DEFAULT 0
    );

    -- =========== DỊCH VỤ ĐOÀN KHÁCH (Booking Tổng — Đa dịch vụ) ===========
    CREATE TABLE DoanKhach_DichVu (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdDoan INT NOT NULL,          
        LoaiDichVu NVARCHAR(20) NOT NULL CHECK (LoaiDichVu IN ('Ve','Combo','Phong','BanAn','DichVu')),
        IdCombo INT NULL,             -- FK đến Combo (khi LoaiDichVu = 'Combo')
        IdSanPham INT NULL,           -- FK đến SanPham (khi LoaiDichVu = 'Ve'/'DichVu')
        SoLuong INT NOT NULL DEFAULT 1 CHECK (SoLuong > 0),
        SoLuongDaDung INT NOT NULL DEFAULT 0,             -- kiểm tra tiêu thụ tại các trạm (Gate/POS/Restaurant)
        DonGia DECIMAL(15,0) NOT NULL DEFAULT 0 CHECK (DonGia >= 0),
        ThanhTien AS (SoLuong * DonGia) PERSISTED, -- Tự động tính

        -- Lịch trình (Ngày dùng dịch vụ cụ thể, tour có thể nhiều ngày)
        NgaySuDung DATETIME NULL,

        IdThamChieu INT NULL,         -- IdDatPhongChiTiet / IdDatBan tương ứng

        -- Truy vết gắn hóa đơn khi chốt
        IdChiTietDonHang INT NULL,    -- NULL = chưa chốt, NOT NULL = ĐÃ xuất hóa đơn

        GhiChu NVARCHAR(MAX) NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('ChuaXuLy', 'DaDatCho', 'DangPhucVu', 'DaThanhToan', 'DaHuy')) DEFAULT 'ChuaXuLy'
    );

    CREATE TABLE KhachHang (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) UNIQUE,
        HoTen NVARCHAR(100) NOT NULL,
        NgaySinh DATE,
        GioiTinh NVARCHAR(10) CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác')),
        Email NVARCHAR(100) NULL,
        DienThoai NVARCHAR(20) NULL,
        CmndCccd NVARCHAR(20) NULL,
        DiaChi NVARCHAR(255),
        LoaiKhach NVARCHAR(20) CHECK (LoaiKhach IN ('CaNhan', 'Doan', 'DoanhNghiep', 'HocSinhSinhVien', 'VIP', 'VVIP', 'NoiBo')) NOT NULL DEFAULT 'CaNhan',
        DiemTichLuy INT NOT NULL DEFAULT 0 CHECK (DiemTichLuy >= 0),
        TongChiTieu DECIMAL(18,0) NOT NULL DEFAULT 0 CHECK (TongChiTieu >= 0),
        IdDoan INT NULL,
        HinhAnh VARCHAR(255),
        GhiChu NVARCHAR(500),
        NgayDangKy DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NULL,
        CreatedBy INT NULL, 
        IsDeleted BIT NOT NULL DEFAULT 0
    );

    CREATE UNIQUE NONCLUSTERED INDEX UQ_KhachHang_Email ON KhachHang(Email) WHERE Email IS NOT NULL;

    CREATE TABLE KhuVuc (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) UNIQUE,
        TenKhuVuc NVARCHAR(100) NOT NULL,
        MoTa NVARCHAR(MAX) NULL,
        TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Hoạt động', N'Bảo trì', N'Tạm đóng', N'Ngừng hoạt động')) DEFAULT N'Hoạt động',
        HinhAnh VARCHAR(255),
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NULL,
        CreatedBy INT NULL,
        IsDeleted BIT NOT NULL DEFAULT 0
    );


    -- =========== KIỂM SOÁT ĐƠN VỊ TÍNH (UoM) ===========
    CREATE TABLE DonViTinh (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Ten NVARCHAR(50) NOT NULL, -- 'Lon', 'Chai', 'Thùng', 'Kg', 'Vé'...
        KyHieu NVARCHAR(10) NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NULL,
        CreatedBy INT NULL,
        IsDeleted BIT NOT NULL DEFAULT 0
    );

    CREATE TABLE SanPham (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) UNIQUE,
        Ten NVARCHAR(100) NOT NULL,
        LoaiSanPham NVARCHAR(20) CHECK (LoaiSanPham IN ('Ve', 'Combo', 'Thue', 'AnUong', 'LuuTru', 'DoLuuNiem', 'GuiXe', 'DichVu', 'Khac')) NOT NULL,
        IdDonViCoBan INT NOT NULL,
        DonGia DECIMAL(15,0) NOT NULL CHECK (DonGia >= 0), -- Giá niêm yết 
        IdKhuVuc INT NULL,
        MoTa NVARCHAR(MAX) NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('DangBan', 'TamNgung', 'NgungBan', 'HetHang')) NOT NULL DEFAULT 'DangBan',
        HinhAnh VARCHAR(255) NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NULL,
        CreatedBy INT NULL,
        IsDeleted BIT NOT NULL DEFAULT 0
    );

    -- Thực thể sắp mạnh: Thuộc tính riêng cho SanPham loại 'Ve' 
    CREATE TABLE SanPham_Ve (
        IdSanPham INT NOT NULL PRIMARY KEY,   
        CanTaoToken BIT NOT NULL DEFAULT 1,
        SoLuotQuyDoi INT NOT NULL DEFAULT 1,
        IdThietBi INT NULL                      -- DanhSachThietBi (NULL = vé cổng/combo, NOT NULL = vé trò chơi)
    );

    -- =========== LỊCH NGÀY LỄ TẾT ===========
    CREATE TABLE CauHinhNgayLe (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TenNgayLe NVARCHAR(100) NOT NULL,
        NgayBatDau DATE NOT NULL,
        NgayKetThuc DATE NOT NULL,
        MoTa NVARCHAR(500) NULL,
        CONSTRAINT CHK_NgayKetThuc_LonHon CHECK (NgayKetThuc >= NgayBatDau)
    );

    -- =========== BẢNG GIÁ ===========
    CREATE TABLE BangGia (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdSanPham INT NOT NULL,
        
        -- Mức giá
        GiaBan  DECIMAL(15,0) NOT NULL DEFAULT 0,
        LoaiGiaApDung NVARCHAR(20) CHECK (LoaiGiaApDung IN ('MacDinh', 'CuoiTuan', 'NgayLe')) NOT NULL DEFAULT 'MacDinh',
        IdNgayLe INT NULL REFERENCES CauHinhNgayLe(Id),
        
        -- Khung giờ (mặc định cả ngày)
        GioBatDau  TIME NOT NULL DEFAULT '00:00',
        GioKetThuc TIME NOT NULL DEFAULT '23:59',
        
        --  thuê giờ (NULL = bán đứt)
        PhutBlock  INT NULL,
        PhutTiep   INT NULL,
        GiaPhuThu  DECIMAL(15,0) NULL,
        
        -- Tiền cọc (NULL = không cần cọc)
        TienCoc    DECIMAL(15,0) NULL,
        
        TrangThai  NVARCHAR(20) NOT NULL DEFAULT 'HoatDong',
        CreatedAt  DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy  INT NULL,
        CONSTRAINT ChkGioHopLe CHECK (GioKetThuc >= GioBatDau)
    );

    CREATE TABLE QuyDoiDonVi (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdSanPham INT NOT NULL,
        IdDonViNho INT NOT NULL,
        IdDonViLon INT NOT NULL,
        TyLeQuyDoi DECIMAL(10,2) NOT NULL CHECK (TyLeQuyDoi > 0),
        GiaBanRieng DECIMAL(15,0) NULL CHECK (GiaBanRieng IS NULL OR GiaBanRieng >= 0), 
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );

    -- =========== QUẢN LÝ Combo & PHÂN BỔ DOANH THU ===========
    CREATE TABLE Combo (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) UNIQUE,
        Ten NVARCHAR(100) NOT NULL,
        Gia DECIMAL(15,0) NOT NULL CHECK (Gia >= 0),
        MoTa NVARCHAR(MAX) NULL,
        TrangThai NVARCHAR(50) CHECK (TrangThai IN ('BanNhap', 'KichHoat', 'NgungApDung')) DEFAULT 'BanNhap',
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NULL,
        CreatedBy INT NULL,
        IsDeleted BIT NOT NULL DEFAULT 0
    );

    CREATE TABLE ComboChiTiet (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdCombo INT NOT NULL,
        IdSanPham INT NOT NULL,
        SoLuong INT NOT NULL DEFAULT 1 CHECK (SoLuong > 0),
        TyLePhanBo DECIMAL(5,2) NOT NULL DEFAULT 0 CHECK (TyLePhanBo >= 0 AND TyLePhanBo <= 100) -- Tổng cộng 100% để chia doanh thu
    );

    -- =================== ĐƠN HÀNG & GIAO DỊCH ===================
    CREATE TABLE SuKien (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) UNIQUE,
        TenSuKien NVARCHAR(200) NOT NULL,
        NgayBatDau DATETIME NOT NULL,
        NgayKetThuc DATETIME NOT NULL,
        MoTa NVARCHAR(MAX),
        TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Sắp diễn ra', N'Đang diễn ra', N'Tạm dừng', N'Kết thúc', N'Hủy')) DEFAULT N'Sắp diễn ra',
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NULL,
        CreatedBy INT NULL,
        IsDeleted BIT NOT NULL DEFAULT 0
    );

    CREATE TABLE KhuyenMai (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) NOT NULL UNIQUE,
        TenKhuyenMai NVARCHAR(100) NOT NULL,
        IdSuKien INT NULL,
        LoaiGiamGia NVARCHAR(20) CHECK (LoaiGiamGia IN ('PhanTram', 'SoTien', 'DongGia', 'MuaXTangY')) NOT NULL DEFAULT 'PhanTram',
        GiaTriGiam DECIMAL(15,0) NOT NULL DEFAULT 0 CHECK (GiaTriGiam >= 0),
        DonToiThieu DECIMAL(15,0) NULL CHECK (DonToiThieu IS NULL OR DonToiThieu >= 0),
        NgayBatDau DATETIME NOT NULL DEFAULT GETDATE(),
        NgayKetThuc DATETIME NOT NULL,
        TrangThai BIT NOT NULL DEFAULT 1,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy INT NULL,
        IsDeleted BIT NOT NULL DEFAULT 0
    );

    CREATE TABLE DonHang (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) UNIQUE,
        IdKhachHang INT NULL,
        IdDoan INT NULL,
        IdKhuyenMai INT NULL,
        TienGiamGia DECIMAL(15,0) NOT NULL DEFAULT 0 CHECK (TienGiamGia >= 0),
        ThoiGian DATETIME NOT NULL DEFAULT GETDATE(),
        TongTien DECIMAL(15,0) NOT NULL CHECK (TongTien >= 0),
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('ChoThanhToan', 'DaThanhToan', 'DangXuLy', 'GhiNoCongTy', 'DaHuy', 'HoanTienMotPhan', 'DaHoanTien')) NOT NULL DEFAULT 'ChoThanhToan',
        GhiChu NVARCHAR(500),
        NguonBan NVARCHAR(10) NOT NULL DEFAULT 'POS' CHECK (NguonBan IN ('POS', 'WEB', 'APP')),
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy INT NULL
    );

    CREATE TABLE ChiTietDonHang (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdDonHang INT NOT NULL,
        IdSanPham INT NULL,
        IdCombo INT NULL,
        SoLuong INT NOT NULL CHECK (SoLuong > 0),
        DonGiaGoc DECIMAL(15,0) NOT NULL CHECK (DonGiaGoc >= 0),
        TienGiamGiaDong DECIMAL(15,0) NOT NULL DEFAULT 0 CHECK (TienGiamGiaDong >= 0),
        DonGiaThucTe DECIMAL(15,0) NOT NULL CHECK (DonGiaThucTe >= 0),
        ThanhTien AS (SoLuong * DonGiaThucTe) PERSISTED,  
        
    );

    CREATE TABLE ViTriNgoi (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) UNIQUE,
        Hang NVARCHAR(10) NOT NULL,
        SoGhe INT NOT NULL CHECK (SoGhe > 0),
        LoaiGhe NVARCHAR(20) CHECK (LoaiGhe IN ('Thuong', 'Vip')) NOT NULL,
        IdSanPham INT NULL, 
        IdKhanDai INT NULL, 
        CONSTRAINT UqViTriNgoiTheoKhanDai UNIQUE (IdKhanDai, Hang, SoGhe),
        RowVer ROWVERSION NOT NULL
    );

    CREATE TABLE VeDienTu (
        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
        MaCode VARCHAR(20) UNIQUE,
        IdChiTietDonHang INT NOT NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('ChuaSuDung', 'DaSuDung', 'DaHuy', 'HetHan', 'DangSuDung')) NOT NULL DEFAULT 'ChuaSuDung',
        ThoiGianQuet DATETIME NULL,
        SoLuotConLai INT NOT NULL DEFAULT 1,
        IdKhachHangSuDung INT NULL,
        IdSanPham INT NOT NULL  
    );

    CREATE TABLE VeKhanDai (
        IdVeDienTu UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,  
        IdViTriNgoi INT NOT NULL
    );

    -- =================== 3. Core Tài Chính & Ví (ACID) ===================
    CREATE TABLE ViDienTu (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdKhachHang INT NOT NULL UNIQUE,
        SoDuKhaDung DECIMAL(15,0) NOT NULL DEFAULT 0 CHECK (SoDuKhaDung >= 0),
        SoDuDongBang DECIMAL(15,0) NOT NULL DEFAULT 0 CHECK (SoDuDongBang >= 0),
        RowVer ROWVERSION NOT NULL
    );

    CREATE TABLE TheRFID (
        MaRfid NVARCHAR(50) PRIMARY KEY,
        IdVi INT NOT NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('Active', 'Locked', 'Lost', 'Revoked', 'Pending')) NOT NULL DEFAULT 'Active',
        NgayKichHoat DATETIME NOT NULL DEFAULT GETDATE(),
        NgayHuy DATETIME NULL
    );

    CREATE TABLE GiaoDichVi (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(50) UNIQUE,
        IdVi INT NOT NULL,
        LoaiGiaoDich NVARCHAR(20) CHECK (LoaiGiaoDich IN ('NapTien', 'ThanhToanDichVu', 'ThuCoc', 'HoanCoc', 'ThuTienPhat', 'HoanTien', 'DieuChinhTang', 'DieuChinhGiam')) NOT NULL,
        SoTien DECIMAL(15,0) NOT NULL CHECK (SoTien >= 0),
        IdDonHangLienQuan INT NULL,
        ParentTransactionId INT NULL,
        ThoiGian DATETIME NOT NULL DEFAULT GETDATE(),
        HashSignature NVARCHAR(255) NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy INT NULL
    );

    CREATE TABLE PhieuThu (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) UNIQUE,
        IdDonHang INT NULL,
        IdGiaoDichVi INT NULL,
        SoTien DECIMAL(15,0) NOT NULL CHECK (SoTien >= 0),
        PhuongThuc NVARCHAR(20) CHECK (PhuongThuc IN ('TienMat', 'ChuyenKhoan', 'The', 'TheNganHang', 'ViDienTu', 'ViRFID', 'MoMo', 'VnPay', 'QR')) NOT NULL,
        MaGiaoDichDoiTac VARCHAR(100) NULL,
        ThoiGian DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy INT NULL
    );

    CREATE TABLE PhieuChi (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) UNIQUE,
        SoTien DECIMAL(15,0) NOT NULL CHECK (SoTien >= 0),
        LyDo NVARCHAR(255) NULL,
        ThoiGian DATETIME NOT NULL DEFAULT GETDATE(),
        IdDonHang INT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy INT NULL
    );

    -- =================== 4. DỊCH VỤ THUÊ ===================
    CREATE TABLE ThueDoChiTiet (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdChiTietDonHang INT NOT NULL,           
        IdSanPham INT NOT NULL,
        SoLuong INT NOT NULL CHECK (SoLuong > 0),
        ThoiGianBatDau DATETIME NOT NULL,
        ThoiGianKetThuc DATETIME NULL,
        SoTienCoc DECIMAL(15,0) NOT NULL CHECK (SoTienCoc >= 0),
        TrangThaiCoc NVARCHAR(20) CHECK (TrangThaiCoc IN ('ChuaHoan', 'DaHoan', 'DaPhat')) NOT NULL DEFAULT 'ChuaHoan',
        -- Hỗ trợ cả tiền mặt (PhieuThu.PhuongThuc='TienMat') lẫn RFID (PhuongThuc='ViRFID')
        IdPhieuThuCoc INT NULL,       -- FK -> PhieuThu (thu cọc lúc bắt đầu thuê)
        IdPhieuChiHoanCoc INT NULL,   -- FK → PhieuChi (hoàn cọc khi trả đồ đúng hạn)
        IdPhieuThuPhat INT NULL,      -- FK → PhieuThu (khách đóng phạt khi hỏng/trả trễ)
        TienThueDaThu DECIMAL(18,0) NOT NULL DEFAULT 0
    );

    CREATE TABLE TuDo (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdKhuVuc INT NOT NULL,
        MaTu NVARCHAR(20) NOT NULL,
        KichThuoc NVARCHAR(10) CHECK (KichThuoc IN ('S','M','L')) NOT NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('Trong', 'DangThue', 'BaoTri')) NOT NULL DEFAULT 'Trong'
    );

    CREATE TABLE ThueTu (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdChiTietThue INT NOT NULL,
        IdTuDo INT NOT NULL,
        MaPin NVARCHAR(10) NULL
    );

    -- =================== 5. KHÁCH SẠN ===================
    -- LoaiPhong: Tách "Loại phòng" ra khỏi SanPham (Superior/Deluxe/Family/Villa)
    -- IdSanPham chỉ dùng để link vào BangGia pricing engine — Phong vật lý dùng IdLoaiPhong
    CREATE TABLE LoaiPhong (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaLoai VARCHAR(20) UNIQUE NOT NULL,
        TenLoai NVARCHAR(100) NOT NULL,
        SucChuaMoiPhong INT NOT NULL DEFAULT 2,
        DienTich DECIMAL(8,2) NULL,            -- m²
        SoPhongNgu INT NOT NULL DEFAULT 1,
        LaVilla BIT NOT NULL DEFAULT 0,
        TrangBiTomTat NVARCHAR(500) NULL,
        IdSanPham INT NULL                     -- FK → SanPham để tái dùng BangGia engine
    );
    CREATE TABLE Phong (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) UNIQUE,
        TenPhong NVARCHAR(50) NOT NULL,
        IdLoaiPhong INT NOT NULL,              -- Fix: Phong vật lý → LoaiPhong → SanPham → BangGia
        SucChua INT NULL CHECK (SucChua IS NULL OR SucChua > 0),
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('Trong', 'DaDat', 'DangSuDung', 'BaoTri', 'DonDep', 'TamKhoa')) NOT NULL DEFAULT 'Trong',
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NULL,
        CreatedBy INT NULL,
        IsDeleted BIT NOT NULL DEFAULT 0,
        RowVer ROWVERSION NOT NULL
    );

    CREATE TABLE DatPhongChiTiet (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdChiTietDonHang INT NOT NULL,             -- Fix: qua CTDH (Lưu trú → Universal Line Item)
        NgayNhan DATETIME NOT NULL,
        NgayTra DATETIME NOT NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('DaDat', 'DaNhan', 'DaTra', 'DaHuy', 'HoanTat')) NOT NULL DEFAULT 'DaDat',
        CHECK (NgayTra >= NgayNhan)
    );

    CREATE TABLE ChiTietDatPhong (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdDatPhongChiTiet INT NOT NULL,
        IdPhong INT NOT NULL,
        DonGiaThucTe DECIMAL(15,0) NOT NULL CHECK (DonGiaThucTe >= 0)
    );

    -- =================== 6. BÃI ĐỖ XE ===================
    CREATE TABLE LuotVaoRaBaiXe (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        BienSo NVARCHAR(20) NOT NULL,
        LoaiXe NVARCHAR(20) CHECK (LoaiXe IN ('XeDap', 'XeMay', 'OTo', 'XeDien')) NOT NULL DEFAULT 'XeMay',
        MaRfid NVARCHAR(50) NULL,
        AnhBienSo NVARCHAR(500) NULL,              -- Đường dẫn ảnh chụp biển số (OCR demo)
        ThoiGianVao DATETIME NOT NULL DEFAULT GETDATE(),
        ThoiGianRa DATETIME NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('DangGui', 'DaTra', 'MatVe')) NOT NULL DEFAULT 'DangGui',
        CHECK (ThoiGianRa IS NULL OR ThoiGianRa >= ThoiGianVao)
    );

    -- Bảng giá giữ xe (đơn giản, riêng biệt — không nối qua BangGia engine)
    CREATE TABLE GiaGuiXe (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        LoaiXe NVARCHAR(20) NOT NULL UNIQUE,
        TenLoaiXe NVARCHAR(50) NOT NULL,
        GiaBanNgay DECIMAL(18,0) NOT NULL,         -- Giá gửi ban ngày (≤12h)
        GiaQuaDem DECIMAL(18,0) NOT NULL            -- Phụ thu qua đêm
    );

    CREATE TABLE BaiDoXe (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TenBai NVARCHAR(100) NOT NULL,
        TongCho INT NULL CHECK (TongCho IS NULL OR TongCho >= 0),
        IdKhuVuc INT NULL                      -- Link vào KhuVuc để định vị trên bản đồ
    );

    CREATE TABLE VeDoXeChiTiet (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdChiTietDonHang INT NOT NULL,             -- Fix: qua CTDH (Parking → Universal Line Item)
        IdLuotVaoRa INT NOT NULL,
        TienPhaiTra DECIMAL(15,0) NOT NULL CHECK (TienPhaiTra >= 0)
    );

    -- =================== 7. BIỂN NHÂN TẠO ===================
    CREATE TABLE KhuVucBien (
        IdKhuVuc INT NOT NULL PRIMARY KEY,    -- Weak Entity: PK = FK → KhuVuc(Id)
        DoSauToiDa DECIMAL(5,2) NULL CHECK (DoSauToiDa IS NULL OR DoSauToiDa >= 0),
        YeuCauPhao BIT NOT NULL DEFAULT 0
    );

    CREATE TABLE ThietBiTaoSong (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TenThietBi NVARCHAR(100) NOT NULL,
        CongSuat NVARCHAR(50) NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('HoatDong', 'BaoTri', 'Hong')) NOT NULL DEFAULT 'HoatDong'
    );

    CREATE TABLE LichTaoSong (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdThietBi INT NOT NULL,
        ThoiGianBatDau DATETIME NOT NULL,
        ThoiGianKetThuc DATETIME NOT NULL,
        KieuSong NVARCHAR(50) NULL,
        CHECK (ThoiGianKetThuc > ThoiGianBatDau)
    );

    CREATE TABLE ChatLuongNuoc (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdKhuVucBien INT NOT NULL,
        Ngay DATE NOT NULL,
        DoMan DECIMAL(5,2) NULL,
        PH DECIMAL(3,2) NULL CHECK (PH IS NULL OR (PH >= 0 AND PH <= 14)),
        NhietDo DECIMAL(4,2) NULL,
        DoTrong INT NULL,
        TrangThaiVeSinh NVARCHAR(20) CHECK (TrangThaiVeSinh IN ('Dat', 'KhongDat')) NOT NULL
    );

    CREATE TABLE CaTrucCuuHo (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdNhanVien INT NOT NULL,
        IdKhuVucBien INT NOT NULL,
        ThoiGianBatDau DATETIME NOT NULL,
        ThoiGianKetThuc DATETIME NOT NULL,
        CHECK (ThoiGianKetThuc > ThoiGianBatDau)
    );

    CREATE TABLE ChoiNghiMat (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) UNIQUE,
        IdKhuVucBien INT NOT NULL,
        IdSanPham INT NULL, -- Map "Dịch vụ thuê chòi" để dùng BangGia
        TenChoi NVARCHAR(100) NOT NULL,
        SucChua INT NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('Trong', 'DaDat', 'DangSuDung')) NOT NULL DEFAULT 'Trong',
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy INT NULL,
        IsDeleted BIT NOT NULL DEFAULT 0,
        RowVer ROWVERSION NOT NULL
    );

    CREATE TABLE ThueChoi (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdChiTietThue INT NOT NULL,
        IdChoi INT NOT NULL
    );

    -- =================== 8. TRƯỜNG ĐUA ===================
    CREATE TABLE DuongDua (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TenDuong NVARCHAR(100) NOT NULL,
        ChieuDai FLOAT NULL,
        LoaiMat NVARCHAR(50) NULL
    );

    CREATE TABLE LoaiHinhDua (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TenLoai NVARCHAR(50) NOT NULL
    );

    CREATE TABLE GiaiDua (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TenGiai NVARCHAR(100) NOT NULL,
        ThoiGianBatDau DATETIME NULL,
        ThoiGianKetThuc DATETIME NULL
    );

    CREATE TABLE LichThiDau (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdGiaiDua INT NOT NULL,
        IdDuongDua INT NOT NULL,
        IdLoaiHinh INT NOT NULL,
        ThoiGianDuKien DATETIME NOT NULL
    );

    CREATE TABLE VanDongVien (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        HoTen NVARCHAR(100) NOT NULL,
        LoaiVdv NVARCHAR(20) CHECK (LoaiVdv IN ('NaiNgua', 'TayDua', 'ChoDua')) NOT NULL
    );

    CREATE TABLE NguaDua (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TenNgua NVARCHAR(100) NOT NULL,
        IdVdv INT NOT NULL,
        Tuoi INT NULL,
        ThanhTich NVARCHAR(MAX) NULL
    );

    CREATE TABLE PhuongTienDua (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TenXe NVARCHAR(100) NOT NULL,
        IdVdv INT NOT NULL,
        TinhTrang NVARCHAR(20) CHECK (TinhTrang IN ('Tot', 'BaoTri')) NOT NULL DEFAULT 'Tot'
    );

    CREATE TABLE BaoTriPhuongTienDua (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdPhuongTienDua INT NULL REFERENCES PhuongTienDua(Id),
        IdNguaDua INT NULL REFERENCES NguaDua(Id),
        NgayBaoTri DATE NOT NULL,
        NoiDung NVARCHAR(MAX) NULL,
        ChiPhi DECIMAL(15,0) NOT NULL,
        IdPhieuChi INT NULL,
        CONSTRAINT CHK_BaoTri_Exclusive CHECK (
            (IdPhuongTienDua IS NOT NULL AND IdNguaDua IS NULL) OR 
            (IdPhuongTienDua IS NULL AND IdNguaDua IS NOT NULL)
        )
    );

    CREATE TABLE KetQuaDua (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdLichThiDau INT NOT NULL,
        IdVdv INT NOT NULL,
        IdPhuongTienDua INT NULL REFERENCES PhuongTienDua(Id),
        IdNguaDua INT NULL REFERENCES NguaDua(Id),
        ThuTuVeDich INT NOT NULL,
        ThanhTichThoiGian TIME NULL,
        CONSTRAINT CHK_KetQua_Exclusive CHECK (
            (IdPhuongTienDua IS NOT NULL AND IdNguaDua IS NULL) OR 
            (IdPhuongTienDua IS NULL AND IdNguaDua IS NOT NULL)
        )
    );

    CREATE TABLE KhanDai (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TenKhanDai NVARCHAR(100) NOT NULL,
        SucChua INT NULL
    );

    GO

    -- =================== 9. Kiosk TỰ ĐỘNG ===================
    CREATE TABLE Kiosk (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ViTri NVARCHAR(100) NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('Online', 'Offline', 'BaoTri')) NOT NULL DEFAULT 'Online'
    );

    -- [ĐÃ XÓA] TonKhoKiosk → Merge vào TonKho (dùng KhoHang.LoaiKho = 'Kiosk')
    -- Kiosk đã được khai báo trong bảng KhoHang với LoaiKho='Kiosk'
    -- Dùng V_CanhBaoTonKho để xem mức cảnh báo thay vì push notification

    -- =================== 10. VƯỜN THÚ ===================
    CREATE TABLE KhuVucThu (
        IdKhuVuc INT NOT NULL PRIMARY KEY,    -- Weak Entity: PK = FK → KhuVuc(Id)
        DienTich DECIMAL(10,2) NULL,          -- Diện tích khu (ha)
        SucChuaDongVat INT NULL,              -- Số cá thể tối đa trong khu
        LoaiMoiTruong NVARCHAR(50) NULL       -- 'NgoaiTroi', 'NuocNgot', 'RungRam', 'Chuong'
    );

    CREATE TABLE DongVat (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Ten NVARCHAR(100) NOT NULL,
        Loai NVARCHAR(50) NULL,
        MoTa NVARCHAR(MAX) NULL,
        NgaySinh DATE NULL,
        TinhTrangSucKhoe NVARCHAR(50) NULL
    );

    CREATE TABLE ChuongTrai (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdKhuVucThu INT NOT NULL,
        IdDongVat INT NOT NULL,
        TenChuong NVARCHAR(50) NOT NULL,
        SucChua INT NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('HoatDong', 'BaoTri')) NOT NULL DEFAULT 'HoatDong'
    );

    CREATE TABLE LichChoAn (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdDongVat INT NOT NULL,
        ThoiGian DATETIME NOT NULL,
        ThucAn NVARCHAR(200) NULL,
        NguoiPhuTrach INT NULL
    );

    CREATE TABLE DatChoThuAn (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdChiTietDonHang INT NOT NULL,             -- Fix: qua CTDH (Vườn thú → Universal Line Item)
        IdDongVat INT NOT NULL,
        IdVeDienTu UNIQUEIDENTIFIER NULL,
        ThoiGianDuKien DATETIME NOT NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('ChuaSuDung', 'DaSuDung')) NOT NULL DEFAULT 'ChuaSuDung'
    );

    -- =================== 11. NHÀ HÀNG & ĐẶT BÀN ===================
    CREATE TABLE NhaHang (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TenNhaHang NVARCHAR(100) NOT NULL,
        IdKhuVuc INT NULL,
        SucChua INT NULL,
        MoTa NVARCHAR(MAX) NULL
    );

    CREATE TABLE BanAn (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdNhaHang INT NOT NULL,
        MaBan NVARCHAR(20) NOT NULL,
        SucChua INT NOT NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('Trong', 'DaDat', 'DangSuDung', 'BaoTri')) NOT NULL DEFAULT 'Trong',
        CONSTRAINT UqBanAnTheoNhaHang UNIQUE (IdNhaHang, MaBan),
        RowVer ROWVERSION NOT NULL
    );

    CREATE TABLE DatBan (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdChiTietDonHang INT NOT NULL,             -- Fix: qua CTDH (Ăn uống → Universal Line Item)
        IdNhaHang INT NOT NULL,
        ThoiGianDat DATETIME NOT NULL DEFAULT GETDATE(),
        ThoiGianDenDuKien DATETIME NOT NULL,
        SoLuongKhach INT NOT NULL CHECK (SoLuongKhach > 0),
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('DaDat', 'DaNhan', 'DaHuy', 'DaTra', 'HoanTat')) NOT NULL DEFAULT 'DaDat',
        TenNguoiDat NVARCHAR(100) NULL,
        SoDienThoai VARCHAR(20) NULL,
        GhiChu NVARCHAR(MAX) NULL,
        TienCoc DECIMAL(18,0) NOT NULL DEFAULT 0, -- [Convention] Display only. Truy vết tài chính qua IdPhieuThuCoc → PhieuThu
        IdPhieuThuCoc INT NULL,
        IdKhachHang INT NULL
    );

    CREATE TABLE ChiTietDatBan (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdDatBan INT NOT NULL,
        IdBanAn INT NOT NULL
    );

    -- =================== 12. KHO HÀNG & MUA SẮM ===================
    CREATE TABLE NhaCungCap (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Ten NVARCHAR(200) NOT NULL,
        MaSoThue NVARCHAR(20) NULL,
        DiaChi NVARCHAR(200) NULL,
        DienThoai NVARCHAR(20) NULL,
        NguoiLienHe NVARCHAR(100) NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NULL,
        CreatedBy INT NULL,
        IsDeleted BIT NOT NULL DEFAULT 0
    );

        CREATE TABLE KhoHang (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TenKho NVARCHAR(100) NOT NULL,
        LoaiKho NVARCHAR(20) CHECK (LoaiKho IN ('TrungTam', 'Kiosk', 'NhaHang')) NOT NULL,
        IdKhuVuc INT NULL REFERENCES KhuVuc(Id),
        DiaChi NVARCHAR(200) NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        IsDeleted BIT NOT NULL DEFAULT 0
    );

    CREATE TABLE PhieuNhapKho (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdKho INT NOT NULL,
        IdNhaCungCap INT NULL,
        NgayNhap DATETIME NOT NULL DEFAULT GETDATE(),
        SoChungTu NVARCHAR(50) NULL,
        TongTien DECIMAL(15,0) NOT NULL CHECK (TongTien >= 0),
        IdPhieuChi INT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy INT NULL
    );

    CREATE TABLE ChiTietNhapKho (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdPhieuNhap INT NOT NULL,
        IdSanPham INT NOT NULL,
        SoLuong INT NOT NULL CHECK (SoLuong > 0),
        DonGiaNhap DECIMAL(15,0) NOT NULL CHECK (DonGiaNhap >= 0),
        IdDonViNhap INT NULL,
        TyLeQuyDoi DECIMAL(10,2) NOT NULL DEFAULT 1
    );

    CREATE TABLE PhieuXuatKho (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdKhoXuat INT NOT NULL,
        IdKhoNhan INT NULL,
        NgayXuat DATETIME NOT NULL DEFAULT GETDATE(),
        LyDo NVARCHAR(200) NULL,
        IdDonHangLienQuan INT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy INT NULL
    );

    CREATE TABLE ChiTietXuatKho (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdPhieuXuat INT NOT NULL,
        IdSanPham INT NOT NULL,
        SoLuong INT NOT NULL CHECK (SoLuong > 0),
        DonGiaXuat DECIMAL(15,0) NULL CHECK (DonGiaXuat IS NULL OR DonGiaXuat >= 0),
        IdDonViXuat INT NULL,
        TyLeQuyDoi DECIMAL(10,2) NOT NULL DEFAULT 1
    );

    CREATE TABLE TonKho (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdKho INT NOT NULL,
        IdSanPham INT NOT NULL,
        SoLuong INT NOT NULL DEFAULT 0,
        NguongCanhBao INT NOT NULL DEFAULT 5,
        CONSTRAINT UqTonKho UNIQUE (IdKho, IdSanPham),
        RowVer ROWVERSION NOT NULL
    );
    GO

    CREATE TABLE TheKho (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdKho INT NOT NULL,                                 
        IdSanPham INT NOT NULL,                             
        LoaiGiaoDich VARCHAR(20) CHECK (LoaiGiaoDich IN (
            'NHAP_KHO', 'XUAT_POS', 'XUAT_HUY', 'CHUYEN_KHO', 'KIEM_KE'
        )) NOT NULL,
        SoLuongThayDoi INT NOT NULL,
        TonCuoi INT NULL,
        DonGiaVatTu DECIMAL(18,0) NULL,  
        IdThamChieu INT NULL,            
        ThoiGianGiaoDich DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy INT NOT NULL,
        GhiChu NVARCHAR(255) NULL,
        FOREIGN KEY (IdKho) REFERENCES KhoHang(Id),
        FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id)
    );
    GO

    CREATE INDEX IxTheKho_TruyVanNhanh ON TheKho(IdKho, IdSanPham, ThoiGianGiaoDich DESC);
    GO

    -- [Đã loại bỏ Trigger đồng bộ TonKho để áp dụng kiến trúc Eventual Consistency triệt để]
    GO

    -- =================== 13. AN TOÀN, SỰ CỐ ===================
    CREATE TABLE SuCo (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdKhachHang INT NULL,
        IdNhanVienXuLy INT NULL,
        ThoiGian DATETIME NOT NULL DEFAULT GETDATE(),
        MoTa NVARCHAR(MAX) NOT NULL,
        MucDo NVARCHAR(20) CHECK (MucDo IN ('Nhe', 'TrungBinh', 'NghiemTrong', 'KhanCap')) NOT NULL,
        ToaDoGps NVARCHAR(100) NULL,
        LoaiSuCo NVARCHAR(20) CHECK (LoaiSuCo IN ('Thuong', 'DuoiNuoc', 'MatTre', 'DanhNhau', 'ThietBi', 'Khac')) NOT NULL DEFAULT 'Khac'
    );

    CREATE TABLE ThatLac (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MoTaDoVat NVARCHAR(MAX) NOT NULL,
        NoiTimThay NVARCHAR(200) NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('ChoNhan', 'DaTra', 'DaThanhLy')) NOT NULL DEFAULT 'ChoNhan',
        ThoiGian DATETIME NOT NULL DEFAULT GETDATE(),
        IdKhachHangNhan INT NULL
    );

    -- =================== 13b. LỊCH LÀM VIỆC NHÂN VIÊN ===================
   

    -- =================== 14. LỊCH KIỂM KHO THEO CA ===================
    CREATE TABLE LichKiemKho (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Ca NVARCHAR(10) CHECK (Ca IN ('Sang', 'Trua', 'Chieu', 'Toi')) NOT NULL,
        ThoiGianKiem DATETIME NOT NULL DEFAULT GETDATE(),
        IdNhanVien INT NOT NULL,
        GhiChu NVARCHAR(500) NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );

    CREATE TABLE ChiTietKiemKho (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdLichKiem INT NOT NULL,
        IdKho INT NOT NULL,
        IdSanPham INT NOT NULL,
        SoLuongThucTe INT NOT NULL CHECK (SoLuongThucTe >= 0),
        SoLuongHeThong INT NOT NULL,
        ChenhLech AS (SoLuongThucTe - SoLuongHeThong) PERSISTED,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('OK', 'CanBoSung', 'ChenhLech')) NOT NULL DEFAULT 'OK'
    );

    -- =================== 15. LOYALTY — ĐIỂM TÍCH LŨY ===================
    CREATE TABLE LichSuDiem (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdKhachHang INT NOT NULL,
        LoaiGiaoDich NVARCHAR(20) CHECK (LoaiGiaoDich IN ('TichLuy','SuDung','HetHan','DieuChinh')) NOT NULL,
        SoDiem INT NOT NULL,             -- dương = tích, âm = dùng
        SoDuTruoc INT NOT NULL,
        SoDuSau INT NOT NULL,
        IdDonHang INT NULL,
        LyDo NVARCHAR(200) NULL,
        ThoiGian DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy INT NOT NULL
    );

    CREATE TABLE QuyTacDiem (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TenQuyTac NVARCHAR(100) NOT NULL,
        TongDonToiThieu DECIMAL(15,0) NOT NULL DEFAULT 0,
        SoDiemThuong INT NOT NULL,
        LoaiKhachApDung NVARCHAR(20) NULL,   -- NULL = tất cả loại khách
        TrangThai BIT NOT NULL DEFAULT 1
    );

    -- =================== 16. BẢO TRÌ THIẾT BỊ TẬP TRUNG ===================
    -- Replaced BaoTriPhuongTienDua (chỉ trường đua) → quản lý TOÀN KHU
    CREATE TABLE DanhSachThietBi (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MaCode VARCHAR(20) NULL,                 -- mã thiết bị (VD: TC_TAULUON)
        TenThietBi NVARCHAR(100) NOT NULL,
        LoaiThietBi NVARCHAR(30) CHECK (LoaiThietBi IN
            ('TroChoi','TaoSong','XeDien','Kiosk','BanAn','NguaDua','PhuongTienDua','Khac')) NOT NULL,
        IdKhuVuc INT NULL,
        MoTa NVARCHAR(500) NULL,                 -- mô tả chi tiết
        NgayMua DATE NULL,
        GiaTriMua DECIMAL(15,0) NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('HoatDong','BaoTri','TamDong','Hong','ThanhLy')) NOT NULL DEFAULT 'HoatDong',
        ChuKyBaoTriThang INT NULL                -- bao nhiêu tháng bảo trì 1 lần
    );

    CREATE TABLE LichBaoTri (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdThietBi INT NOT NULL,
        NgayBaoTri DATE NOT NULL,
        LoaiBaoTri NVARCHAR(20) CHECK (LoaiBaoTri IN ('DieuDo','SuaChua','ThayThe','ThanhLy')) NOT NULL,
        NoiDung NVARCHAR(MAX) NULL,
        ChiPhi DECIMAL(15,0) NOT NULL DEFAULT 0,
        IdNhanVienThucHien INT NULL,
        IdPhieuChi INT NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('KeHoach','DangLam','HoanTat')) NOT NULL DEFAULT 'KeHoach'
    );

    -- =================== 17. ĐÁNH GIÁ DỊCH VỤ ===================
    CREATE TABLE DanhGiaDichVu (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdKhachHang INT NOT NULL,
        IdDonHang INT NULL,
        LoaiDichVu NVARCHAR(20) CHECK (LoaiDichVu IN
            ('ToanBo','Phong','NhaHang','TroChoi','VuonThu','TruongDua','Bien')) NOT NULL,
        DiemSo TINYINT NOT NULL CHECK (DiemSo BETWEEN 1 AND 5),
        NhanXet NVARCHAR(1000) NULL,
        ThoiGian DATETIME NOT NULL DEFAULT GETDATE(),
        TrangThai NVARCHAR(20) CHECK (TrangThai IN ('ChoXuLy','DaXuLy','AnDi')) NOT NULL DEFAULT 'ChoXuLy',
        PhanHoiNhanVien NVARCHAR(500) NULL,
        IdNhanVienXuLy INT NULL
    );

    -- =================== 18. LỊCH CA LÀM VIỆC ===================
    CREATE TABLE LichLamViec (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdNhanVien INT NOT NULL,
        IdKhuVuc INT NOT NULL,
        NgayLam DATE NOT NULL,
        CaLam NVARCHAR(10) CHECK (CaLam IN ('Sang','Chieu','Toi','Full')) NOT NULL,
        GioBatDau TIME NOT NULL,
        GioKetThuc TIME NOT NULL,
        TrangThai NVARCHAR(20) CHECK (TrangThai IN
            ('KeHoach','DaXacNhan','TreGio','VangMat','HoanThanh')) NOT NULL DEFAULT 'KeHoach',
        GhiChu NVARCHAR(200) NULL,
        CONSTRAINT UqLichLamViecNgayCa UNIQUE (IdNhanVien, NgayLam, CaLam),
        CONSTRAINT ChkGioLamHopLe CHECK (GioKetThuc > GioBatDau)
    );

    -- =================== 19. AUDIT TRAIL ĐƠN HÀNG ===================
    CREATE TABLE AuditDonHang (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdDonHang INT NOT NULL,
        TrangThaiCu NVARCHAR(20) NULL,
        TrangThaiMoi NVARCHAR(20) NOT NULL,
        ThoiGianThayDoi DATETIME NOT NULL DEFAULT GETDATE(),
        NguoiThayDoi INT NULL
    );

    -- =================== 15. CHỈ MỤC BẮT BUỘC ===================
    CREATE INDEX IxDonHangKhachHang ON DonHang(IdKhachHang);
    CREATE INDEX IxChiTietDonHangDonHang ON ChiTietDonHang(IdDonHang);
    CREATE INDEX IxVeDienTuChiTietDonHang ON VeDienTu(IdChiTietDonHang);
    CREATE INDEX IxThueDoChiTietCTDH ON ThueDoChiTiet(IdChiTietDonHang);
    CREATE INDEX IxGiaoDichViVi ON GiaoDichVi(IdVi);
    CREATE INDEX IxGiaoDichViDonHang ON GiaoDichVi(IdDonHangLienQuan);
    CREATE INDEX IxLuotVaoRaBaiXeRfid ON LuotVaoRaBaiXe(MaRfid);
    CREATE INDEX IxLichTaoSongThietBi ON LichTaoSong(IdThietBi);
    CREATE INDEX IxLichThiDauGiaiDua ON LichThiDau(IdGiaiDua);
    CREATE INDEX IxKetQuaDuaLichThiDau ON KetQuaDua(IdLichThiDau);
    CREATE INDEX IxThueChoiChiTietThue ON ThueChoi(IdChiTietThue);
    CREATE INDEX IxLichChoAnDongVat ON LichChoAn(IdDongVat);
    CREATE INDEX IxDatChoThuAnCTDH ON DatChoThuAn(IdChiTietDonHang);
    CREATE INDEX IxBanAnNhaHang ON BanAn(IdNhaHang);
    CREATE INDEX IxDatBanCTDH ON DatBan(IdChiTietDonHang);
    CREATE INDEX IxChiTietDatBanDatBan ON ChiTietDatBan(IdDatBan);
    CREATE INDEX IxTonKhoKhoHang ON TonKho(IdKho);
    CREATE INDEX IxTonKhoSanPham ON TonKho(IdSanPham);
    CREATE UNIQUE INDEX UqLichTaoSong_DeviceTime ON LichTaoSong(IdThietBi, ThoiGianBatDau, ThoiGianKetThuc);
    CREATE UNIQUE INDEX UqChatLuongNuoc_KhuNgay ON ChatLuongNuoc(IdKhuVucBien, Ngay);
    GO

    -- =================== 16. STORED PROCEDURE TRUY VẤN TỔNG HỢP ===================
    CREATE PROCEDURE SpGetChiTietDonHangToanPhan
        @IdDonHang INT
    AS
    BEGIN
        SET NOCOUNT ON;
        
        -- 1. Thông tin chung đơn hàng
        SELECT dh.*, kh.HoTen, kh.DienThoai
        FROM DonHang dh
        LEFT JOIN KhachHang kh ON dh.IdKhachHang = kh.Id
        WHERE dh.Id = @IdDonHang;

        -- 2. Sản Phẩm/Combo mua đứt (ChiTietDonHang vẫn FK trực tiếp tới DonHang)
        SELECT CTDH.*, ISNULL(sp.Ten, cb.Ten) AS TenItem
        FROM ChiTietDonHang CTDH
        LEFT JOIN SanPham sp ON CTDH.IdSanPham = sp.Id
        LEFT JOIN Combo cb ON CTDH.IdCombo = cb.Id
        WHERE CTDH.IdDonHang = @IdDonHang;

        -- 3. Đặt Phòng khách sạn (qua ChiTietDonHang hub)
        SELECT dp.*, p.TenPhong
        FROM DatPhongChiTiet dp
        JOIN ChiTietDonHang CTDH3 ON dp.IdChiTietDonHang = CTDH3.Id
        JOIN ChiTietDatPhong CTDP ON dp.Id = CTDP.IdDatPhongChiTiet
        JOIN Phong p ON CTDP.IdPhong = p.Id
        WHERE CTDH3.IdDonHang = @IdDonHang;

        -- 4. Thuê đồ (qua ChiTietDonHang hub)
        SELECT td.*, sp.Ten
        FROM ThueDoChiTiet td
        JOIN ChiTietDonHang CTDH4 ON td.IdChiTietDonHang = CTDH4.Id
        JOIN SanPham sp ON td.IdSanPham = sp.Id
        WHERE CTDH4.IdDonHang = @IdDonHang;

        -- 5. Bãi đỗ xe (qua ChiTietDonHang hub)
        SELECT VDX.*, LVR.BienSo, LVR.ThoiGianVao
        FROM VeDoXeChiTiet VDX
        JOIN ChiTietDonHang CTDH5 ON VDX.IdChiTietDonHang = CTDH5.Id
        JOIN LuotVaoRaBaiXe LVR ON VDX.IdLuotVaoRa = LVR.Id
        WHERE CTDH5.IdDonHang = @IdDonHang;

        -- 6. Đặt bàn nhà hàng (qua ChiTietDonHang hub)
        SELECT db.*, nh.TenNhaHang
        FROM DatBan db
        JOIN ChiTietDonHang CTDH6 ON db.IdChiTietDonHang = CTDH6.Id
        JOIN NhaHang nh ON db.IdNhaHang = nh.Id
        WHERE CTDH6.IdDonHang = @IdDonHang;

        -- 7. Vườn thú Cho ăn (qua ChiTietDonHang hub)
        SELECT DCTa.*, dv.Ten AS TenConVat
        FROM DatChoThuAn DCTa
        JOIN ChiTietDonHang CTDH7 ON DCTa.IdChiTietDonHang = CTDH7.Id
        JOIN DongVat dv ON DCTa.IdDongVat = dv.Id
        WHERE CTDH7.IdDonHang = @IdDonHang;
    END;

    GO

    -- =================== 17. Đã Loại bỏ TRIGGER (Đổi sang State Machine Draft/Active) ===================
    -- TRIGGER kiểm tra 100% tỷ lệ Combo bị gỡ bỏ để C# BUS có thể thêm nối lẻ từng ct combo.

    -- =================== 18. RÀNG BUỘC & CHỈ MỤC BỔ SUNG (Tối ưu Hiệu suất & OCC) ===================

    -- [Bỏ Ràng buộc không âm để kho hoạt động mượt mùa cao điểm]

    -- Tối ưu INDEX cho khóa ngoại hay JOIN
    CREATE INDEX IxChiTietDonHangSanPham ON ChiTietDonHang(IdSanPham);
    CREATE INDEX IxChiTietDonHangCombo ON ChiTietDonHang(IdCombo);
    CREATE INDEX IxDatPhongChiTietNgayNhan ON DatPhongChiTiet(NgayNhan);
    CREATE INDEX IxThueDoChiTietThoiGianBatDau ON ThueDoChiTiet(ThoiGianBatDau);
    CREATE INDEX IxLichThiDauThoiGian ON LichThiDau(ThoiGianDuKien);
    -- [ĐÃ XÓA] IxTonKhoKioskKiosk (TonKhoKiosk đã merge vào TonKho)
    CREATE INDEX IxPhieuNhapKhoNgayNhap ON PhieuNhapKho(NgayNhap);
    CREATE INDEX IxPhieuXuatKhoNgayXuat ON PhieuXuatKho(NgayXuat);
    GO

    -- =================== 15.2 TRIGGER RÀNG BUỘC NGHIỆP VỤ ===================
    -- BangGia: trigger overlap đã GỠ BỎ (gây lỗi khi seed nhiều giá cùng SP, kiểm tra bằng BUS layer)

    -- ComboChiTiet: tổng TyLePhanBo theo IdCombo phải = 100
    CREATE TRIGGER TrgComboChiTietTyLe100
    ON ComboChiTiet
    AFTER INSERT, UPDATE, DELETE
    AS
    BEGIN
        SET NOCOUNT ON;

        -- [FIX Lỗ hổng 6]: Chỉ validate combo đã 'Kích hoạt', bỏ qua 'Bản nháp'
        IF EXISTS (
            SELECT 1
            FROM (
                SELECT IdCombo FROM inserted
                UNION
                SELECT IdCombo FROM deleted
            ) x
            JOIN Combo cb ON cb.Id = x.IdCombo
            CROSS APPLY (
                SELECT SUM(ISNULL(TyLePhanBo, 0)) AS TongTyLe
                FROM ComboChiTiet c
                WHERE c.IdCombo = x.IdCombo
            ) s
            WHERE x.IdCombo IS NOT NULL
              AND cb.TrangThai = 'KichHoat'   -- [FIX E] Match CHECK constraint ('KichHoat' không dấu, không khoảng trắng)
              AND ABS(ISNULL(s.TongTyLe, 0) - 100) > 0.01
        )
        BEGIN
            RAISERROR (N'Tổng TyLePhanBo của từng Combo (đã Kích hoạt) phải bằng 100.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
    END
    GO

    -- =================== TỔNG HỢP KHÓA NGOẠI (CENTRALIZED FOREIGN KEYS) ===================
    ALTER TABLE PhanQuyen ADD CONSTRAINT FkPhanQuyenIdVaiTro FOREIGN KEY (IdVaiTro) REFERENCES VaiTro(Id);
    ALTER TABLE PhanQuyen ADD CONSTRAINT FkPhanQuyenIdQuyen FOREIGN KEY (IdQuyen) REFERENCES QuyenHan(Id);
    ALTER TABLE NhanVien ADD CONSTRAINT FkNhanVienIdVaiTro FOREIGN KEY (IdVaiTro) REFERENCES VaiTro(Id);
    ALTER TABLE DoanKhach ADD CONSTRAINT FkDoanKhachCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE KhachHang ADD CONSTRAINT FkKhachHangIdDoan FOREIGN KEY (IdDoan) REFERENCES DoanKhach(Id);
    ALTER TABLE KhachHang ADD CONSTRAINT FkKhachHangCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE KhuVuc ADD CONSTRAINT FkKhuVucCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE DonViTinh ADD CONSTRAINT FkDonViTinhCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE SanPham ADD CONSTRAINT FkSanPhamIdDonViCoBan FOREIGN KEY (IdDonViCoBan) REFERENCES DonViTinh(Id);
    ALTER TABLE SanPham ADD CONSTRAINT FkSanPhamIdKhuVuc FOREIGN KEY (IdKhuVuc) REFERENCES KhuVuc(Id);
    ALTER TABLE SanPham ADD CONSTRAINT FkSanPhamCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE BangGia ADD CONSTRAINT FkBangGiaIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    CREATE INDEX IxBangGia_TraCuu ON BangGia(IdSanPham, TrangThai);
    ALTER TABLE QuyDoiDonVi ADD CONSTRAINT FkQuyDoiDonViIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    ALTER TABLE QuyDoiDonVi ADD CONSTRAINT FkQuyDoiDonViIdDonViNho FOREIGN KEY (IdDonViNho) REFERENCES DonViTinh(Id);
    ALTER TABLE QuyDoiDonVi ADD CONSTRAINT FkQuyDoiDonViIdDonViLon FOREIGN KEY (IdDonViLon) REFERENCES DonViTinh(Id);
    ALTER TABLE Combo ADD CONSTRAINT FkComboCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE ComboChiTiet ADD CONSTRAINT FkComboChiTietIdCombo FOREIGN KEY (IdCombo) REFERENCES Combo(Id);
    ALTER TABLE ComboChiTiet ADD CONSTRAINT FkComboChiTietIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    ALTER TABLE SuKien ADD CONSTRAINT FkSuKienCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE KhuyenMai ADD CONSTRAINT FkKhuyenMaiIdSuKien FOREIGN KEY (IdSuKien) REFERENCES SuKien(Id);
    ALTER TABLE DonHang ADD CONSTRAINT FkDonHangIdKhachHang FOREIGN KEY (IdKhachHang) REFERENCES KhachHang(Id);
    ALTER TABLE DonHang ADD CONSTRAINT FkDonHangIdDoan FOREIGN KEY (IdDoan) REFERENCES DoanKhach(Id);
    ALTER TABLE DonHang ADD CONSTRAINT FkDonHangIdKhuyenMai FOREIGN KEY (IdKhuyenMai) REFERENCES KhuyenMai(Id);
    ALTER TABLE DonHang ADD CONSTRAINT FkDonHangCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE ChiTietDonHang ADD CONSTRAINT FkChiTietDonHangIdDonHang FOREIGN KEY (IdDonHang) REFERENCES DonHang(Id);
    ALTER TABLE ChiTietDonHang ADD CONSTRAINT FkChiTietDonHangIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    ALTER TABLE ChiTietDonHang ADD CONSTRAINT FkChiTietDonHangIdCombo FOREIGN KEY (IdCombo) REFERENCES Combo(Id);
    ALTER TABLE ViTriNgoi ADD CONSTRAINT FkViTriNgoiIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    ALTER TABLE VeDienTu ADD CONSTRAINT FkVeDienTuIdChiTietDonHang FOREIGN KEY (IdChiTietDonHang) REFERENCES ChiTietDonHang(Id);
    ALTER TABLE VeDienTu ADD CONSTRAINT FkVeDienTuIdKhachHangSuDung FOREIGN KEY (IdKhachHangSuDung) REFERENCES KhachHang(Id);
    ALTER TABLE ViDienTu ADD CONSTRAINT FkViDienTuIdKhachHang FOREIGN KEY (IdKhachHang) REFERENCES KhachHang(Id);
    ALTER TABLE TheRFID ADD CONSTRAINT FkTheRFIDIdVi FOREIGN KEY (IdVi) REFERENCES ViDienTu(Id);
    ALTER TABLE GiaoDichVi ADD CONSTRAINT FkGiaoDichViIdVi FOREIGN KEY (IdVi) REFERENCES ViDienTu(Id);
    ALTER TABLE GiaoDichVi ADD CONSTRAINT FkGiaoDichViIdDonHangLienQuan FOREIGN KEY (IdDonHangLienQuan) REFERENCES DonHang(Id);
    ALTER TABLE GiaoDichVi ADD CONSTRAINT FkGiaoDichViParentTransactionId FOREIGN KEY (ParentTransactionId) REFERENCES GiaoDichVi(Id);
    ALTER TABLE GiaoDichVi ADD CONSTRAINT FkGiaoDichViCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE PhieuThu ADD CONSTRAINT FkPhieuThuIdDonHang FOREIGN KEY (IdDonHang) REFERENCES DonHang(Id);
    ALTER TABLE PhieuChi ADD CONSTRAINT FkPhieuChiIdDonHang FOREIGN KEY (IdDonHang) REFERENCES DonHang(Id);
    ALTER TABLE PhieuThu ADD CONSTRAINT FkPhieuThuIdGiaoDichVi FOREIGN KEY (IdGiaoDichVi) REFERENCES GiaoDichVi(Id);
    ALTER TABLE PhieuThu ADD CONSTRAINT FkPhieuThuCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE PhieuChi ADD CONSTRAINT FkPhieuChiCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE ThueDoChiTiet ADD CONSTRAINT FkThueDoChiTietIdCTDH FOREIGN KEY (IdChiTietDonHang) REFERENCES ChiTietDonHang(Id);
    ALTER TABLE ThueDoChiTiet ADD CONSTRAINT FkThueDoChiTietIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    -- [FIX] Cọc/hoàn/phạt qua PhieuThu/PhieuChi thay vì GiaoDichVi
    -- PhieuThu.PhuongThuc ghi nhận TienMat/ViRFID — không bắt buộc khách phải có ví
    ALTER TABLE ThueDoChiTiet ADD CONSTRAINT FkThueDoChiTietIdPhieuThuCoc FOREIGN KEY (IdPhieuThuCoc) REFERENCES PhieuThu(Id);
    ALTER TABLE ThueDoChiTiet ADD CONSTRAINT FkThueDoChiTietIdPhieuChiHoanCoc FOREIGN KEY (IdPhieuChiHoanCoc) REFERENCES PhieuChi(Id);
    ALTER TABLE ThueDoChiTiet ADD CONSTRAINT FkThueDoChiTietIdPhieuThuPhat FOREIGN KEY (IdPhieuThuPhat) REFERENCES PhieuThu(Id);
    ALTER TABLE TuDo ADD CONSTRAINT FkTuDoIdKhuVuc FOREIGN KEY (IdKhuVuc) REFERENCES KhuVuc(Id);
    ALTER TABLE ThueTu ADD CONSTRAINT FkThueTuIdChiTietThue FOREIGN KEY (IdChiTietThue) REFERENCES ThueDoChiTiet(Id);
    ALTER TABLE ThueTu ADD CONSTRAINT FkThueTuIdTuDo FOREIGN KEY (IdTuDo) REFERENCES TuDo(Id);
    ALTER TABLE Phong ADD CONSTRAINT FkPhongIdLoaiPhong FOREIGN KEY (IdLoaiPhong) REFERENCES LoaiPhong(Id);
    ALTER TABLE Phong ADD CONSTRAINT FkPhongCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE DatPhongChiTiet ADD CONSTRAINT FkDatPhongChiTietIdCTDH FOREIGN KEY (IdChiTietDonHang) REFERENCES ChiTietDonHang(Id);
    ALTER TABLE ChiTietDatPhong ADD CONSTRAINT FkChiTietDatPhongIdDatPhongChiTiet FOREIGN KEY (IdDatPhongChiTiet) REFERENCES DatPhongChiTiet(Id);
    ALTER TABLE ChiTietDatPhong ADD CONSTRAINT FkChiTietDatPhongIdPhong FOREIGN KEY (IdPhong) REFERENCES Phong(Id);
    ALTER TABLE LuotVaoRaBaiXe ADD CONSTRAINT FkLuotVaoRaBaiXeMaRfid FOREIGN KEY (MaRfid) REFERENCES TheRFID(MaRfid);
    ALTER TABLE VeDoXeChiTiet ADD CONSTRAINT FkVeDoXeChiTietIdCTDH FOREIGN KEY (IdChiTietDonHang) REFERENCES ChiTietDonHang(Id);
    ALTER TABLE VeDoXeChiTiet ADD CONSTRAINT FkVeDoXeChiTietIdLuotVaoRa FOREIGN KEY (IdLuotVaoRa) REFERENCES LuotVaoRaBaiXe(Id);
    ALTER TABLE LichTaoSong ADD CONSTRAINT FkLichTaoSongIdThietBi FOREIGN KEY (IdThietBi) REFERENCES ThietBiTaoSong(Id);
    ALTER TABLE ChatLuongNuoc ADD CONSTRAINT FkChatLuongNuocIdKhuVucBien FOREIGN KEY (IdKhuVucBien) REFERENCES KhuVucBien(IdKhuVuc);
    ALTER TABLE CaTrucCuuHo ADD CONSTRAINT FkCaTrucCuuHoIdNhanVien FOREIGN KEY (IdNhanVien) REFERENCES NhanVien(Id);
    ALTER TABLE CaTrucCuuHo ADD CONSTRAINT FkCaTrucCuuHoIdKhuVucBien FOREIGN KEY (IdKhuVucBien) REFERENCES KhuVucBien(IdKhuVuc);
    ALTER TABLE ChoiNghiMat ADD CONSTRAINT FkChoiNghiMatIdKhuVucBien FOREIGN KEY (IdKhuVucBien) REFERENCES KhuVucBien(IdKhuVuc);
    ALTER TABLE ChoiNghiMat ADD CONSTRAINT FkChoiNghiMatIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    ALTER TABLE ChoiNghiMat ADD CONSTRAINT FkChoiNghiMatCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE ThueChoi ADD CONSTRAINT FkThueChoiIdChiTietThue FOREIGN KEY (IdChiTietThue) REFERENCES ThueDoChiTiet(Id);
    ALTER TABLE ThueChoi ADD CONSTRAINT FkThueChoiIdChoi FOREIGN KEY (IdChoi) REFERENCES ChoiNghiMat(Id);
    ALTER TABLE LichThiDau ADD CONSTRAINT FkLichThiDauIdGiaiDua FOREIGN KEY (IdGiaiDua) REFERENCES GiaiDua(Id);
    ALTER TABLE LichThiDau ADD CONSTRAINT FkLichThiDauIdDuongDua FOREIGN KEY (IdDuongDua) REFERENCES DuongDua(Id);
    ALTER TABLE LichThiDau ADD CONSTRAINT FkLichThiDauIdLoaiHinh FOREIGN KEY (IdLoaiHinh) REFERENCES LoaiHinhDua(Id);
    ALTER TABLE NguaDua ADD CONSTRAINT FkNguaDuaIdVdv FOREIGN KEY (IdVdv) REFERENCES VanDongVien(Id);
    ALTER TABLE PhuongTienDua ADD CONSTRAINT FkPhuongTienDuaIdVdv FOREIGN KEY (IdVdv) REFERENCES VanDongVien(Id);
    ALTER TABLE BaoTriPhuongTienDua ADD CONSTRAINT FkBaoTriPhuongTienDuaIdPhuongTienDua FOREIGN KEY (IdPhuongTienDua) REFERENCES PhuongTienDua(Id);
    ALTER TABLE BaoTriPhuongTienDua ADD CONSTRAINT FkBaoTriPhuongTienDuaIdNguaDua FOREIGN KEY (IdNguaDua) REFERENCES NguaDua(Id);
    ALTER TABLE BaoTriPhuongTienDua ADD CONSTRAINT FkBaoTriPhuongTienDuaIdPhieuChi FOREIGN KEY (IdPhieuChi) REFERENCES PhieuChi(Id);
    ALTER TABLE KetQuaDua ADD CONSTRAINT FkKetQuaDuaIdLichThiDau FOREIGN KEY (IdLichThiDau) REFERENCES LichThiDau(Id);
    ALTER TABLE KetQuaDua ADD CONSTRAINT FkKetQuaDuaIdVdv FOREIGN KEY (IdVdv) REFERENCES VanDongVien(Id);
    ALTER TABLE KetQuaDua ADD CONSTRAINT FkKetQuaDuaIdPhuongTienDua FOREIGN KEY (IdPhuongTienDua) REFERENCES PhuongTienDua(Id);
    ALTER TABLE KetQuaDua ADD CONSTRAINT FkKetQuaDuaIdNguaDua FOREIGN KEY (IdNguaDua) REFERENCES NguaDua(Id);
    -- TonKhoKiosk: table chưa triển khai trong phiên bản này (module Kiosk future scope)
    -- ALTER TABLE TonKhoKiosk ADD CONSTRAINT FkTonKhoKioskIdKiosk FOREIGN KEY (IdKiosk) REFERENCES Kiosk(Id);
    -- ALTER TABLE TonKhoKiosk ADD CONSTRAINT FkTonKhoKioskIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    ALTER TABLE ChuongTrai ADD CONSTRAINT FkChuongTraiIdKhuVucThu FOREIGN KEY (IdKhuVucThu) REFERENCES KhuVucThu(IdKhuVuc);
    ALTER TABLE ChuongTrai ADD CONSTRAINT FkChuongTraiIdDongVat FOREIGN KEY (IdDongVat) REFERENCES DongVat(Id);
    ALTER TABLE LichChoAn ADD CONSTRAINT FkLichChoAnIdDongVat FOREIGN KEY (IdDongVat) REFERENCES DongVat(Id);
    ALTER TABLE LichChoAn ADD CONSTRAINT FkLichChoAnNguoiPhuTrach FOREIGN KEY (NguoiPhuTrach) REFERENCES NhanVien(Id);
    ALTER TABLE DatChoThuAn ADD CONSTRAINT FkDatChoThuAnIdCTDH FOREIGN KEY (IdChiTietDonHang) REFERENCES ChiTietDonHang(Id);
    ALTER TABLE DatChoThuAn ADD CONSTRAINT FkDatChoThuAnIdDongVat FOREIGN KEY (IdDongVat) REFERENCES DongVat(Id);
    ALTER TABLE DatChoThuAn ADD CONSTRAINT FkDatChoThuAnIdVeDienTu FOREIGN KEY (IdVeDienTu) REFERENCES VeDienTu(Id);
    ALTER TABLE NhaHang ADD CONSTRAINT FkNhaHangIdKhuVuc FOREIGN KEY (IdKhuVuc) REFERENCES KhuVuc(Id);
    ALTER TABLE BanAn ADD CONSTRAINT FkBanAnIdNhaHang FOREIGN KEY (IdNhaHang) REFERENCES NhaHang(Id);
    ALTER TABLE DatBan ADD CONSTRAINT FkDatBanIdCTDH FOREIGN KEY (IdChiTietDonHang) REFERENCES ChiTietDonHang(Id);
    ALTER TABLE DatBan ADD CONSTRAINT FkDatBanIdNhaHang FOREIGN KEY (IdNhaHang) REFERENCES NhaHang(Id);
    ALTER TABLE ChiTietDatBan ADD CONSTRAINT FkChiTietDatBanIdDatBan FOREIGN KEY (IdDatBan) REFERENCES DatBan(Id);
    ALTER TABLE ChiTietDatBan ADD CONSTRAINT FkChiTietDatBanIdBanAn FOREIGN KEY (IdBanAn) REFERENCES BanAn(Id);
    ALTER TABLE NhaCungCap ADD CONSTRAINT FkNhaCungCapCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE PhieuNhapKho ADD CONSTRAINT FkPhieuNhapKhoIdKho FOREIGN KEY (IdKho) REFERENCES KhoHang(Id);
    ALTER TABLE PhieuNhapKho ADD CONSTRAINT FkPhieuNhapKhoIdNhaCungCap FOREIGN KEY (IdNhaCungCap) REFERENCES NhaCungCap(Id);
    ALTER TABLE PhieuNhapKho ADD CONSTRAINT FkPhieuNhapKhoIdPhieuChi FOREIGN KEY (IdPhieuChi) REFERENCES PhieuChi(Id);
    ALTER TABLE PhieuNhapKho ADD CONSTRAINT FkPhieuNhapKhoCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE ChiTietNhapKho ADD CONSTRAINT FkChiTietNhapKhoIdPhieuNhap FOREIGN KEY (IdPhieuNhap) REFERENCES PhieuNhapKho(Id);
    ALTER TABLE ChiTietNhapKho ADD CONSTRAINT FkChiTietNhapKhoIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    ALTER TABLE ChiTietNhapKho ADD CONSTRAINT FkChiTietNhapKhoIdDonViNhap FOREIGN KEY (IdDonViNhap) REFERENCES DonViTinh(Id);
    ALTER TABLE PhieuXuatKho ADD CONSTRAINT FkPhieuXuatKhoIdKhoXuat FOREIGN KEY (IdKhoXuat) REFERENCES KhoHang(Id);
    ALTER TABLE PhieuXuatKho ADD CONSTRAINT FkPhieuXuatKhoIdKhoNhan FOREIGN KEY (IdKhoNhan) REFERENCES KhoHang(Id);
    ALTER TABLE PhieuXuatKho ADD CONSTRAINT FkPhieuXuatKhoIdDonHangLienQuan FOREIGN KEY (IdDonHangLienQuan) REFERENCES DonHang(Id);
    ALTER TABLE PhieuXuatKho ADD CONSTRAINT FkPhieuXuatKhoCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE ChiTietXuatKho ADD CONSTRAINT FkChiTietXuatKhoIdPhieuXuat FOREIGN KEY (IdPhieuXuat) REFERENCES PhieuXuatKho(Id);
    ALTER TABLE ChiTietXuatKho ADD CONSTRAINT FkChiTietXuatKhoIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    ALTER TABLE ChiTietXuatKho ADD CONSTRAINT FkChiTietXuatKhoIdDonViXuat FOREIGN KEY (IdDonViXuat) REFERENCES DonViTinh(Id);
    ALTER TABLE TonKho ADD CONSTRAINT FkTonKhoIdKho FOREIGN KEY (IdKho) REFERENCES KhoHang(Id);
    ALTER TABLE TonKho ADD CONSTRAINT FkTonKhoIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    ALTER TABLE SuCo ADD CONSTRAINT FkSuCoIdKhachHang FOREIGN KEY (IdKhachHang) REFERENCES KhachHang(Id);
    ALTER TABLE SuCo ADD CONSTRAINT FkSuCoIdNhanVienXuLy FOREIGN KEY (IdNhanVienXuLy) REFERENCES NhanVien(Id);
    ALTER TABLE ThatLac ADD CONSTRAINT FkThatLacIdKhachHangNhan FOREIGN KEY (IdKhachHangNhan) REFERENCES KhachHang(Id);
    -- [ĐÃ XÓA] ThietBiApp FK (bảng đã loại bỏ — không phụ thuộc app mobile/FCM)
    ALTER TABLE NhanVien ADD CONSTRAINT FkNhanVienIdKhuVuc FOREIGN KEY (IdKhuVuc) REFERENCES KhuVuc(Id);
    ALTER TABLE ViTriNgoi ADD CONSTRAINT FkViTriNgoiIdKhanDai FOREIGN KEY (IdKhanDai) REFERENCES KhanDai(Id);
    ALTER TABLE VeDienTu ADD CONSTRAINT FkVeDienTuIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);

    -- Dữ liệu FK liên kết mảng Thiết Bị & Vé
    ALTER TABLE SanPham_Ve ADD CONSTRAINT FkSanPhamVeIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    ALTER TABLE SanPham_Ve ADD CONSTRAINT FkSanPhamVeIdThietBi FOREIGN KEY (IdThietBi) REFERENCES DanhSachThietBi(Id);
    ALTER TABLE VeKhanDai ADD CONSTRAINT FkVeKhanDaiIdVeDienTu FOREIGN KEY (IdVeDienTu) REFERENCES VeDienTu(Id);
    ALTER TABLE VeKhanDai ADD CONSTRAINT FkVeKhanDaiIdViTriNgoi FOREIGN KEY (IdViTriNgoi) REFERENCES ViTriNgoi(Id);

    -- Weak Entity FK: KhuVucBien & KhuVucThu → KhuVuc
    ALTER TABLE KhuVucBien ADD CONSTRAINT FkKhuVucBienIdKhuVuc FOREIGN KEY (IdKhuVuc) REFERENCES KhuVuc(Id);
    ALTER TABLE KhuVucThu ADD CONSTRAINT FkKhuVucThuIdKhuVuc FOREIGN KEY (IdKhuVuc) REFERENCES KhuVuc(Id);
    ALTER TABLE LoaiPhong ADD CONSTRAINT FkLoaiPhongIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    ALTER TABLE BaiDoXe ADD CONSTRAINT FkBaiDoXeIdKhuVuc FOREIGN KEY (IdKhuVuc) REFERENCES KhuVuc(Id);
    ALTER TABLE LichKiemKho ADD CONSTRAINT FkLichKiemKhoIdNhanVien FOREIGN KEY (IdNhanVien) REFERENCES NhanVien(Id);
    ALTER TABLE ChiTietKiemKho ADD CONSTRAINT FkChiTietKiemKhoIdLichKiem FOREIGN KEY (IdLichKiem) REFERENCES LichKiemKho(Id);
    ALTER TABLE ChiTietKiemKho ADD CONSTRAINT FkChiTietKiemKhoIdKho FOREIGN KEY (IdKho) REFERENCES KhoHang(Id);
    ALTER TABLE ChiTietKiemKho ADD CONSTRAINT FkChiTietKiemKhoIdSanPham FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    -- FK: Bảng mới Phase 3
    ALTER TABLE LichSuDiem ADD CONSTRAINT FkLichSuDiemIdKhachHang FOREIGN KEY (IdKhachHang) REFERENCES KhachHang(Id);
    ALTER TABLE LichSuDiem ADD CONSTRAINT FkLichSuDiemIdDonHang FOREIGN KEY (IdDonHang) REFERENCES DonHang(Id);
    ALTER TABLE LichSuDiem ADD CONSTRAINT FkLichSuDiemCreatedBy FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);
    ALTER TABLE DanhSachThietBi ADD CONSTRAINT FkDanhSachThietBiIdKhuVuc FOREIGN KEY (IdKhuVuc) REFERENCES KhuVuc(Id);
    ALTER TABLE LichBaoTri ADD CONSTRAINT FkLichBaoTriIdThietBi FOREIGN KEY (IdThietBi) REFERENCES DanhSachThietBi(Id);
    ALTER TABLE LichBaoTri ADD CONSTRAINT FkLichBaoTriIdNhanVien FOREIGN KEY (IdNhanVienThucHien) REFERENCES NhanVien(Id);
    ALTER TABLE LichBaoTri ADD CONSTRAINT FkLichBaoTriIdPhieuChi FOREIGN KEY (IdPhieuChi) REFERENCES PhieuChi(Id);
    ALTER TABLE DanhGiaDichVu ADD CONSTRAINT FkDanhGiaDichVuIdKhachHang FOREIGN KEY (IdKhachHang) REFERENCES KhachHang(Id);
    ALTER TABLE DanhGiaDichVu ADD CONSTRAINT FkDanhGiaDichVuIdDonHang FOREIGN KEY (IdDonHang) REFERENCES DonHang(Id);
    ALTER TABLE DanhGiaDichVu ADD CONSTRAINT FkDanhGiaDichVuIdNhanVien FOREIGN KEY (IdNhanVienXuLy) REFERENCES NhanVien(Id);
    ALTER TABLE LichLamViec ADD CONSTRAINT FkLichLamViecIdNhanVien FOREIGN KEY (IdNhanVien) REFERENCES NhanVien(Id);
    ALTER TABLE LichLamViec ADD CONSTRAINT FkLichLamViecIdKhuVuc FOREIGN KEY (IdKhuVuc) REFERENCES KhuVuc(Id);
    ALTER TABLE AuditDonHang ADD CONSTRAINT FkAuditDonHangIdDonHang FOREIGN KEY (IdDonHang) REFERENCES DonHang(Id);
    ALTER TABLE AuditDonHang ADD CONSTRAINT FkAuditDonHangNguoiThayDoi FOREIGN KEY (NguoiThayDoi) REFERENCES NhanVien(Id);
    -- Indexes bổ sung (Phase 4)
    CREATE INDEX IxDonHangThoiGianTrangThai     ON DonHang(ThoiGian, TrangThai);
    CREATE INDEX IxKhachHangLoaiKhach           ON KhachHang(LoaiKhach, IsDeleted);
    CREATE INDEX IxPhongLoaiPhongTrangThai      ON Phong(IdLoaiPhong, TrangThai);
    CREATE INDEX IxLichSuDiemKhachHang          ON LichSuDiem(IdKhachHang, ThoiGian DESC);
    CREATE INDEX IxDanhGiaLoaiDichVu            ON DanhGiaDichVu(LoaiDichVu, DiemSo);
    CREATE INDEX IxLichBaoTriNgayTrangThai      ON LichBaoTri(NgayBaoTri, TrangThai);
    CREATE INDEX IxLichLamViecNgay              ON LichLamViec(NgayLam, IdKhuVuc);

    -- [Bổ sung từ Đánh Giá] Các Index Foreign Key cho truy vấn lớn
    CREATE INDEX IxCTDH_IdDonHang               ON ChiTietDonHang(IdDonHang);
    CREATE INDEX IxGiaoDichVi_IdVi              ON GiaoDichVi(IdVi, ThoiGian DESC);
    CREATE INDEX IxVeDienTu_IdCTDH              ON VeDienTu(IdChiTietDonHang);
    
    -- [Bổ sung từ Đánh Giá] Filtered Index cho Xóa Mềm (Soft Delete)
    CREATE INDEX IxSanPham_Active               ON SanPham(Id) WHERE IsDeleted = 0;
    CREATE INDEX IxKhachHang_Active             ON KhachHang(Id) WHERE IsDeleted = 0;
    CREATE INDEX IxNhanVien_Active              ON NhanVien(Id) WHERE IsDeleted = 0;

    -- =================================================================
    -- PATCH: VÁ LỖ HỔNG SCHEMA (Audit 04/2026)
    -- Tất cả thay đổi đều ADDITIVE — KHÔNG rename/xóa cột nào.
    -- =================================================================

    -- ── [CAT-1] Missing Foreign Keys ──────────────────────────────────
    --    DoanKhach_DichVu: 4 cột FK comment nhưng CHƯA có constraint
    ALTER TABLE DoanKhach_DichVu ADD CONSTRAINT FkDKDV_IdDoan
        FOREIGN KEY (IdDoan) REFERENCES DoanKhach(Id);
    ALTER TABLE DoanKhach_DichVu ADD CONSTRAINT FkDKDV_IdCombo
        FOREIGN KEY (IdCombo) REFERENCES Combo(Id);
    ALTER TABLE DoanKhach_DichVu ADD CONSTRAINT FkDKDV_IdSanPham
        FOREIGN KEY (IdSanPham) REFERENCES SanPham(Id);
    ALTER TABLE DoanKhach_DichVu ADD CONSTRAINT FkDKDV_IdCTDH
        FOREIGN KEY (IdChiTietDonHang) REFERENCES ChiTietDonHang(Id);

    --    DatBan: IdPhieuThuCoc + IdKhachHang chưa có FK
    ALTER TABLE DatBan ADD CONSTRAINT FkDatBanIdPhieuThuCoc
        FOREIGN KEY (IdPhieuThuCoc) REFERENCES PhieuThu(Id);
    ALTER TABLE DatBan ADD CONSTRAINT FkDatBanIdKhachHang
        FOREIGN KEY (IdKhachHang) REFERENCES KhachHang(Id);

    --    KhuyenMai: CreatedBy chưa có FK
    ALTER TABLE KhuyenMai ADD CONSTRAINT FkKhuyenMaiCreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);

    -- ── [CAT-2] Missing CHECK Constraints ─────────────────────────────
    --    BangGia.TrangThai: Dùng Vietnamese diacritics khớp C# code
    ALTER TABLE BangGia ADD CONSTRAINT ChkBangGiaTrangThai
        CHECK (TrangThai IN ('HoatDong', 'TamNgung', 'NgungApDung'));

    --    VeDienTu.SoLuotConLai: Ngăn quét vượt (< 0)
    ALTER TABLE VeDienTu ADD CONSTRAINT ChkVeDienTuSoLuot
        CHECK (SoLuotConLai >= 0);

    --    KhuVucThu.LoaiMoiTruong: Giới hạn domain values
    ALTER TABLE KhuVucThu ADD CONSTRAINT ChkKhuVucThuLoaiMT
        CHECK (LoaiMoiTruong IN (N'NgoaiTroi', N'NuocNgot', N'RungRam', N'Chuong'));

    -- ── [CAT-3] Missing Audit Columns ─────────────────────────────────
    --    BangGia: Thêm UpdatedAt để truy vết thay đổi giá
    ALTER TABLE BangGia ADD UpdatedAt DATETIME NULL;

    --    GiaGuiXe: Bảng giá gửi xe thiếu hoàn toàn audit trail
    ALTER TABLE GiaGuiXe ADD CreatedAt DATETIME NOT NULL DEFAULT GETDATE();
    ALTER TABLE GiaGuiXe ADD UpdatedAt DATETIME NULL;
    ALTER TABLE GiaGuiXe ADD CreatedBy INT NULL;
    ALTER TABLE GiaGuiXe ADD CONSTRAINT FkGiaGuiXeCreatedBy
        FOREIGN KEY (CreatedBy) REFERENCES NhanVien(Id);

    -- ── [CAT-5] Missing Indexes (FK columns) ─────────────────────────
    CREATE INDEX IxDKDV_IdDoan      ON DoanKhach_DichVu(IdDoan);
    CREATE INDEX IxDKDV_IdCTDH      ON DoanKhach_DichVu(IdChiTietDonHang);
    CREATE INDEX IxCTDP_IdPhong     ON ChiTietDatPhong(IdPhong);
    CREATE INDEX IxCTDB_IdBanAn     ON ChiTietDatBan(IdBanAn);
    CREATE INDEX IxLichSuDiem_IdDH  ON LichSuDiem(IdDonHang);

    -- ── [NOTE: CAT-4] TrgAuditDonHang ─────────────────────────────────
    -- Trigger TrgAuditDonHang.NguoiThayDoi luôn NULL vì trigger không
    -- capture CONTEXT_INFO. Cần .NET set CONTEXT_INFO trước mỗi UPDATE.
    -- → Known limitation: ghi nhận để cải thiện ở phiên bản sau.

    GO

-- =================== VIEW: CẢNH BÁO TỒN KHO (Thay thế Kiosk push-notification) ===================
CREATE VIEW V_CanhBaoTonKho AS
SELECT
    kh.TenKho,
    kh.LoaiKho,
    sp.Ten AS TenSanPham,
    tk.SoLuong AS TonHienTai,
    tk.NguongCanhBao,
    CASE
        WHEN tk.SoLuong = 0 THEN N'Hết hàng'
        WHEN tk.NguongCanhBao IS NOT NULL AND tk.SoLuong <= tk.NguongCanhBao THEN N'Sắp hết'
        ELSE N'Đủ hàng'
    END AS TrangThaiCanhBao,
    CASE
        WHEN tk.NguongCanhBao IS NOT NULL AND tk.SoLuong <= tk.NguongCanhBao
        THEN tk.NguongCanhBao - tk.SoLuong ELSE 0
    END AS SoLuongCanBo
FROM TonKho tk
JOIN KhoHang kh ON tk.IdKho = kh.Id
JOIN SanPham sp ON tk.IdSanPham = sp.Id;
GO

-- =================== VIEWS: REPORTING LAYER ===================
-- [FIX Lỗ hổng 11]: LEFT JOIN Combo để phân loại đúng doanh thu combo
CREATE VIEW V_DoanhThuTheoModule AS
SELECT
    CAST(dh.ThoiGian AS DATE) AS Ngay,
    DATENAME(MONTH, dh.ThoiGian) AS Thang,
    CASE
        WHEN ctdh.IdCombo IS NOT NULL THEN N'Combo'
        WHEN sp.LoaiSanPham = 'Ve'      THEN N'Giải trí'
        WHEN sp.LoaiSanPham = 'LuuTru'  THEN N'Lưu trú'
        WHEN sp.LoaiSanPham = 'AnUong'  THEN N'Ăn uống'
        WHEN sp.LoaiSanPham = 'Thue'    THEN N'Cho thuê'
        WHEN sp.LoaiSanPham = 'GuiXe'   THEN N'Bãi xe'
        ELSE N'Khác'
    END AS Module,
    COALESCE(sp.Ten, cb.Ten, N'Dịch vụ khác') AS DichVu,
    SUM(ctdh.SoLuong)                           AS TongSoLuong,
    SUM(ctdh.SoLuong * ctdh.DonGiaThucTe)       AS DoanhThu
FROM ChiTietDonHang ctdh
JOIN DonHang dh ON ctdh.IdDonHang = dh.Id
LEFT JOIN SanPham sp ON ctdh.IdSanPham = sp.Id
LEFT JOIN Combo cb ON ctdh.IdCombo = cb.Id
WHERE dh.TrangThai IN ('DaThanhToan', 'GhiNoCongTy')
GROUP BY CAST(dh.ThoiGian AS DATE), DATENAME(MONTH, dh.ThoiGian),
    CASE
        WHEN ctdh.IdCombo IS NOT NULL THEN N'Combo'
        WHEN sp.LoaiSanPham = 'Ve'      THEN N'Giải trí'
        WHEN sp.LoaiSanPham = 'LuuTru'  THEN N'Lưu trú'
        WHEN sp.LoaiSanPham = 'AnUong'  THEN N'Ăn uống'
        WHEN sp.LoaiSanPham = 'Thue'    THEN N'Cho thuê'
        WHEN sp.LoaiSanPham = 'GuiXe'   THEN N'Bãi xe'
        ELSE N'Khác'
    END,
    COALESCE(sp.Ten, cb.Ten, N'Dịch vụ khác');
GO

CREATE VIEW V_CongSuatPhong AS
SELECT
    lp.TenLoai                              AS LoaiPhong,
    COUNT(p.Id)                             AS TongPhong,
    SUM(CASE WHEN p.TrangThai IN ('DaDat','DangSuDung') THEN 1 ELSE 0 END) AS PhongDaDat,
    CAST(
        SUM(CASE WHEN p.TrangThai IN ('DaDat','DangSuDung') THEN 1.0 ELSE 0 END)
        / COUNT(p.Id) * 100 AS DECIMAL(5,2)
    )                                       AS TyLeLapDay
FROM Phong p
JOIN LoaiPhong lp ON p.IdLoaiPhong = lp.Id
WHERE p.IsDeleted = 0
GROUP BY lp.TenLoai;
GO

CREATE VIEW V_LichSuKhachHang AS
SELECT
    kh.Id, kh.HoTen, kh.LoaiKhach,
    COUNT(DISTINCT dh.Id)                        AS TongDonHang,
    ISNULL(SUM(ctdh.ThanhTien), 0)               AS TongChiTieu,
    kh.DiemTichLuy,
    MAX(dh.ThoiGian)                             AS LanCuoiDen
FROM KhachHang kh
LEFT JOIN DonHang dh ON dh.IdKhachHang = kh.Id
LEFT JOIN ChiTietDonHang ctdh ON ctdh.IdDonHang = dh.Id
WHERE kh.IsDeleted = 0
GROUP BY kh.Id, kh.HoTen, kh.LoaiKhach, kh.DiemTichLuy;
GO

CREATE VIEW V_DashboardHomNay AS
SELECT
    (SELECT COUNT(*) FROM DonHang
     WHERE CAST(ThoiGian AS DATE) = CAST(GETDATE() AS DATE)
     AND TrangThai = 'DaThanhToan')                              AS DonHangHomNay,
    (SELECT ISNULL(SUM(ctdh.ThanhTien), 0)
     FROM ChiTietDonHang ctdh
     JOIN DonHang dh ON ctdh.IdDonHang = dh.Id
     WHERE CAST(dh.ThoiGian AS DATE) = CAST(GETDATE() AS DATE)
     AND dh.TrangThai = 'DaThanhToan')                          AS DoanhThuHomNay,
    (SELECT COUNT(*) FROM Phong
     WHERE TrangThai IN ('DaDat','DangSuDung'))                 AS PhongDaDat,
    (SELECT COUNT(*) FROM TonKho WHERE SoLuong = 0)             AS SanPhamHetHang,
    (SELECT COUNT(*) FROM DonHang WHERE TrangThai = 'ChoThanhToan') AS DonChoThanhToan;
GO

CREATE VIEW V_ChuaThanhToan AS
SELECT
    dh.MaCode, kh.HoTen, kh.DienThoai,
    dh.TongTien, dh.ThoiGian,
    DATEDIFF(MINUTE, dh.ThoiGian, GETDATE()) AS PhutChoThanhToan
FROM DonHang dh
LEFT JOIN KhachHang kh ON dh.IdKhachHang = kh.Id
WHERE dh.TrangThai = 'ChoThanhToan';
GO

-- =================== [FIX Lỗ hổng 1]: VIEW AUDIT TỔNG TIỀN ĐƠN HÀNG ===================
-- So sánh DonHang.TongTien vs SUM(CTDH.ThanhTien) để phát hiện bất đồng bộ
CREATE VIEW V_AuditTongTienDonHang AS
SELECT
    dh.Id,
    dh.MaCode,
    dh.TongTien                                     AS TongTien_LuuTrong,
    ISNULL(SUM(ctdh.ThanhTien), 0)                   AS TongTien_TinhLai,
    ISNULL(dh.TienGiamGia, 0)                        AS TienGiamGia,
    ISNULL(SUM(ctdh.ThanhTien), 0) - ISNULL(dh.TienGiamGia, 0) AS TongTien_KyVong,
    dh.TongTien - (ISNULL(SUM(ctdh.ThanhTien), 0) - ISNULL(dh.TienGiamGia, 0)) AS ChenhLech,
    dh.TrangThai,
    dh.ThoiGian
FROM DonHang dh
LEFT JOIN ChiTietDonHang ctdh ON ctdh.IdDonHang = dh.Id
WHERE dh.TrangThai = 'DaThanhToan'
GROUP BY dh.Id, dh.MaCode, dh.TongTien, dh.TienGiamGia, dh.TrangThai, dh.ThoiGian
HAVING ABS(dh.TongTien - (ISNULL(SUM(ctdh.ThanhTien), 0) - ISNULL(dh.TienGiamGia, 0))) > 1;
GO

-- =================== [FIX Lỗ hổng 3]: FILTERED UNIQUE INDEX BẢNG GIÁ ===================
-- Ngăn 2 dòng giá active cho cùng 1 SP cùng khung giờ và cùng loại giá
CREATE UNIQUE INDEX UxBangGia_ActiveSPGio
    ON BangGia(IdSanPham, LoaiGiaApDung, GioBatDau, GioKetThuc)
    WHERE TrangThai = 'HoatDong';
GO

-- =================== [FIX Lỗ hổng 10]: BẢNG LỊCH SỬ GIÁ =================y==
CREATE TABLE BangGia_LichSu (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdBangGia INT NOT NULL,
    IdSanPham INT NOT NULL,
    LoaiGiaApDung_Cu NVARCHAR(50),
    GiaBan_Cu DECIMAL(15,0),
    LoaiGiaApDung_Moi NVARCHAR(50),
    GiaBan_Moi DECIMAL(15,0),
    ThoiGianThayDoi DATETIME NOT NULL DEFAULT GETDATE(),
    GhiChu NVARCHAR(200) NULL
);
GO

-- Trigger tự ghi lịch sử khi UPDATE giá
CREATE TRIGGER TrgBangGiaLichSu ON BangGia AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF UPDATE(GiaBan) OR UPDATE(LoaiGiaApDung)
    BEGIN
        INSERT INTO BangGia_LichSu
            (IdBangGia, IdSanPham,
             LoaiGiaApDung_Cu, GiaBan_Cu,
             LoaiGiaApDung_Moi, GiaBan_Moi)
        SELECT
            i.Id, i.IdSanPham,
            d.LoaiGiaApDung, d.GiaBan,
            i.LoaiGiaApDung, i.GiaBan
        FROM inserted i
        JOIN deleted d ON i.Id = d.Id
        WHERE d.GiaBan <> i.GiaBan
           OR d.LoaiGiaApDung <> i.LoaiGiaApDung;

        -- Đồng thời cập nhật UpdatedAt
        UPDATE bg SET bg.UpdatedAt = GETDATE()
        FROM BangGia bg
        JOIN inserted i ON bg.Id = i.Id;
    END
END;
GO


-- =================== TRIGGER: AUDIT TRAIL ĐƠN HÀNG ===================
CREATE TRIGGER TrgAuditDonHang ON DonHang AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF UPDATE(TrangThai)
        INSERT INTO AuditDonHang (IdDonHang, TrangThaiCu, TrangThaiMoi)
        SELECT d.Id, d.TrangThai, i.TrangThai
        FROM deleted d
        JOIN inserted i ON d.Id = i.Id
        WHERE d.TrangThai <> i.TrangThai;
END;
GO

-- =================== 19. DỮ LIỆU CỐT LÕI (CORE SEED DATA) ===================
-- [QUAN TRỌNG] Phần này PHẢI chạy ngay sau schema.
-- Đảm bảo hệ thống login được, phân quyền hoạt động.
-- CẤM SỬA nếu không hiểu rõ ảnh hưởng tới BUS_QuyenHan, frmPhanQuyen, Form1.

    -- 19.1 VaiTro (5 vai trò chuẩn)
    INSERT INTO VaiTro (TenVaiTro) VALUES
        (N'Admin'), (N'QuanLy'), (N'NhanVien'), (N'ThuKho'), (N'KeToan');

    -- 19.1b CauHinhNgayLe (Lịch ngày lễ)
    INSERT INTO CauHinhNgayLe (TenNgayLe, NgayBatDau, NgayKetThuc) VALUES
    (N'Tết Dương Lịch', '2026-01-01', '2026-01-01'),
    (N'Tết Nguyên Đán', '2026-02-17', '2026-02-19'),
    (N'Giỗ Tổ Hùng Vương', '2026-04-02', '2026-04-02'),
    (N'Lễ 30/4 & 1/5', '2026-04-30', '2026-05-01'),
    (N'Quốc Khánh', '2026-09-02', '2026-09-02');

    -- 19.2 QuyenHan (ĐẦY ĐỦ tất cả MaQuyen mà code C# đang dùng)
    INSERT INTO QuyenHan (MaQuyen, MoTa) VALUES
        -- Module Đơn hàng
        ('VIEW_DONHANG',   N'Xem đơn hàng'),
        ('CREATE_DONHANG', N'Tạo đơn hàng'),
        ('UPDATE_DONHANG', N'Sửa đơn hàng'),
        ('DELETE_DONHANG', N'Hủy đơn hàng'),
        ('MANAGE_DONHANG', N'Quản lý đơn hàng (toàn quyền)'),
        -- Module Nhân viên
        ('VIEW_STAFF',     N'Xem nhân viên'),
        ('MANAGE_STAFF',   N'Quản lý nhân viên'),
        ('MANAGE_USER',    N'Quản lý tài khoản / Vai trò / Phân quyền'),
        -- Module Giá & Sản phẩm
        ('VIEW_PRICE',     N'Xem giá'),
        ('MANAGE_PRICE',   N'Quản lý giá'),
        -- Module Kho
        ('VIEW_INVENTORY', N'Xem tồn kho'),
        ('MANAGE_INVENTORY', N'Quản lý nhập/xuất kho'),
        -- Module Hóa đơn
        ('VIEW_BILL',      N'Xem hóa đơn'),
        ('MANAGE_BILL',    N'Hủy/Sửa hóa đơn'),
        -- Module Đơn vị tính
        ('VIEW_UOM',       N'Xem đơn vị tính'),
        ('MANAGE_UOM',     N'Quản lý đơn vị tính'),
        -- Module Báo cáo
        ('VIEW_REPORT',    N'Xem báo cáo'),
        -- Module Khu vực
        ('VIEW_REGION',    N'Xem khu vực'),
        ('MANAGE_REGION',  N'Quản lý khu vực'),
        -- Module Khách hàng
        ('VIEW_CUSTOMER',  N'Xem khách hàng'),
        ('MANAGE_CUSTOMER', N'Quản lý khách hàng'),
        -- Module Khuyến mãi
        ('VIEW_PROMOTION', N'Xem khuyến mãi'),
        ('MANAGE_PROMOTION', N'Quản lý khuyến mãi'),
        -- Module Tài chính
        ('VIEW_LEDGER',    N'Xem phiếu thu/chi'),
        ('MANAGE_LEDGER',  N'Quản lý phiếu thu/chi'),
        -- Module RFID
        ('VIEW_RFID',      N'Xem thẻ RFID'),
        ('MANAGE_RFID',    N'Quản lý thẻ RFID'),
        -- Module Ví điện tử
        ('VIEW_WALLET',    N'Xem ví điện tử'),
        ('MANAGE_WALLET',  N'Quản lý ví điện tử'),
        -- Module POS
        ('VIEW_POS',       N'Xem bán vé POS'),
        ('MANAGE_POS',     N'Quản lý bán vé POS'),
        -- Module Nạp tiền RFID
        ('VIEW_RFID_TOPUP', N'Xem nạp tiền RFID'),
        ('MANAGE_RFID_TOPUP', N'Quản lý nạp tiền RFID'),
        -- Module Mô phỏng vé
        ('VIEW_TICKET_SIMULATION', N'Xem mô phỏng vé'),
        ('MANAGE_TICKET_SIMULATION', N'Quản lý mô phỏng vé'),
        -- Module Combo
        ('VIEW_COMBO',     N'Xem combo'),
        ('MANAGE_COMBO',   N'Quản lý combo');

    -- 19.3 PhanQuyen: Admin có TẤT CẢ quyền
    INSERT INTO PhanQuyen (IdVaiTro, IdQuyen)
    SELECT (SELECT Id FROM VaiTro WHERE TenVaiTro = N'Admin'), Id
    FROM QuyenHan;

    -- 19.4 PhanQuyen: QuanLy có quyền VIEW tất cả + MANAGE cơ bản
    INSERT INTO PhanQuyen (IdVaiTro, IdQuyen)
    SELECT (SELECT Id FROM VaiTro WHERE TenVaiTro = N'QuanLy'), Id
    FROM QuyenHan
    WHERE MaQuyen LIKE 'VIEW_%'
       OR MaQuyen IN ('CREATE_DONHANG', 'UPDATE_DONHANG', 'MANAGE_DONHANG',
                      'MANAGE_PRICE', 'MANAGE_PROMOTION', 'MANAGE_COMBO',
                      'MANAGE_POS', 'MANAGE_INVENTORY', 'MANAGE_CUSTOMER');

    -- 19.5 PhanQuyen: NhanVien có quyền VIEW + tạo đơn + POS
    INSERT INTO PhanQuyen (IdVaiTro, IdQuyen)
    SELECT (SELECT Id FROM VaiTro WHERE TenVaiTro = N'NhanVien'), Id
    FROM QuyenHan
    WHERE MaQuyen IN ('VIEW_DONHANG', 'CREATE_DONHANG', 'VIEW_PRICE',
                      'VIEW_POS', 'MANAGE_POS', 'VIEW_CUSTOMER',
                      'VIEW_RFID_TOPUP', 'MANAGE_RFID_TOPUP',
                      'VIEW_TICKET_SIMULATION', 'MANAGE_TICKET_SIMULATION',
                      'VIEW_COMBO', 'VIEW_BILL');

    -- 19.6 PhanQuyen: ThuKho
    INSERT INTO PhanQuyen (IdVaiTro, IdQuyen)
    SELECT (SELECT Id FROM VaiTro WHERE TenVaiTro = N'ThuKho'), Id
    FROM QuyenHan
    WHERE MaQuyen IN ('VIEW_INVENTORY', 'MANAGE_INVENTORY', 'VIEW_UOM', 'MANAGE_UOM',
                      'VIEW_PRICE', 'VIEW_DONHANG');

    -- 19.7 PhanQuyen: KeToan
    INSERT INTO PhanQuyen (IdVaiTro, IdQuyen)
    SELECT (SELECT Id FROM VaiTro WHERE TenVaiTro = N'KeToan'), Id
    FROM QuyenHan
    WHERE MaQuyen IN ('VIEW_DONHANG', 'VIEW_BILL', 'MANAGE_BILL',
                      'VIEW_LEDGER', 'MANAGE_LEDGER', 'VIEW_REPORT',
                      'VIEW_PRICE', 'VIEW_INVENTORY', 'VIEW_CUSTOMER');

    -- 19.8 DonViTinh (đơn vị cơ bản cho hệ thống)
    INSERT INTO DonViTinh (Ten, KyHieu) VALUES
        (N'Lon', N'L'), (N'Chai', N'C'), (N'Thùng', N'T'),
        (N'Kg', N'kg'), (N'Vé', N'V'), (N'Suất', N'S');

    -- 19.9 KhuVuc (11 khu vực Đại Nam - theo báo cáo vận hành)
    INSERT INTO KhuVuc (MaCode, TenKhuVuc, MoTa, TrangThai) VALUES
        ('KV01', N'Khu vui chơi nước',               N'Biển nhân tạo 21.6ha, sức chứa 30.000 khách', N'Hoạt động'),
        ('KV02', N'Trường đua',                       N'Đua ngựa, đua chó, go-kart, mô tô',           N'Hoạt động'),
        ('KV03', N'Vườn thú mở',                      N'12.5ha, 600-800 cá thể, 63 loài, SEAZA',     N'Hoạt động'),
        ('KV04', N'Khách sạn',                        N'Khách sạn Thành Đại Nam, >300 phòng',       N'Hoạt động'),
        ('KV05', N'Nhà hàng & ẩm thực',              N'Nhà hàng + phố ẩm thực + 50+ kiosk',      N'Hoạt động'),
        ('KV06', N'Quầy vé cổng',                    N'Bán vé tham quan - MIỄN PHÍ ĐẾ́N HẾ́T 2026', N'Hoạt động'),
        ('KV07', N'Bãi đỗ xe',                        N'Bãi xe trung tâm',                          N'Hoạt động'),
        ('KV08', N'Khu trò chơi cảm giác mạnh',      N'Tàu lượn, vượt thác, lốc xoáy, F1',       N'Hoạt động'),
        ('KV09', N'Khu trò chơi phổ thông',           N'Xe lửa, đu quay, thuyền đụng, xe điện',   N'Hoạt động'),
        ('KV10', N'Khu khám phá mạo hiểm',           N'Ngũ Long, Phim 4D, Thế giới Tuyết',      N'Hoạt động'),
        ('KV11', N'Khu tâm linh',                    N'Đền Đại Nam (Kim Điện), Bảo Tháp 9 tầng', N'Hoạt động');

    -- 19.10 NhanVien Admin (tài khoản mặc định, login = 1/1)
    INSERT INTO NhanVien (MaCode, IdVaiTro, IdKhuVuc, HoTen, GioiTinh, ChucVu, BoPhan,
                          DienThoai, Email, TenDangNhap, MatKhau, Cccd)
    VALUES ('NV001', 1, (SELECT Id FROM KhuVuc WHERE MaCode='KV06'),
            N'Administrator', N'Nam', N'Quản trị viên', N'Ban Giám đốc',
            '0909000001', 'admin@dainam.com', '1', '1', '001090000001');

    -- 19.11 KhoHang
    INSERT INTO KhoHang (TenKho, LoaiKho, DiaChi) VALUES
        (N'Kho trung tâm', 'TrungTam', N'Khu A - Đại Nam'),
        (N'Kho Kiosk biển', 'Kiosk', N'Kiosk 01 - Khu biển'),
        (N'Kho Kiosk trường đua', 'Kiosk', N'Kiosk 02 - Trường đua'),
        (N'Kho nhà hàng chính', 'NhaHang', N'Bếp trung tâm');

    -- 19.12 LoaiHinhDua
    INSERT INTO LoaiHinhDua (TenLoai) VALUES
        (N'Đua ngựa'), (N'Đua chó'), (N'Đua go-kart'), (N'Đua mô tô'), (N'Flyboard');

    -- 19.13 NhaCungCap
    INSERT INTO NhaCungCap (Ten, MaSoThue, DienThoai, NguoiLienHe) VALUES
        (N'Công ty TNHH Thực phẩm ABC', '123456789', '0903123456', N'Nguyễn Văn A'),
        (N'Nhà phân phối Đồ chơi XYZ', '987654321', '0912345678', N'Trần Thị B'),
        (N'Công ty Bia Sài Gòn', '111222333', '0987654321', N'Lê Văn C'),
        (N'Công ty Thực phẩm Đại Nam', '222333444', '0971234567', N'Phạm Văn D');

    -- 19.14 SuKien
    INSERT INTO SuKien (MaCode, TenSuKien, NgayBatDau, NgayKetThuc, MoTa, TrangThai) VALUES
        ('SK01', N'Lễ hội mùa hè 2025', '2025-06-01', '2025-08-31', N'Giảm giá vé mùa hè', N'Sắp diễn ra'),
        ('SK02', N'Tết Nguyên đán 2024', '2024-01-20', '2024-02-05', N'Chương trình Tết', N'Kết thúc'),
        ('SK03', N'Black Friday 2024', '2024-11-25', '2024-11-30', N'Giảm giá 50%', N'Kết thúc');

    -- 19.15 KhanDai (Trường đua)
    INSERT INTO KhanDai (TenKhanDai, SucChua) VALUES
        (N'Khán đài A', 5000), (N'Khán đài B', 3000), (N'Khán đài VIP', 500);

    -- 19.16 DuongDua
    INSERT INTO DuongDua (TenDuong, ChieuDai, LoaiMat) VALUES
        (N'Đường đua ngựa', 1600, N'Cát'),
        (N'Đường đua chó', 500, N'Cát'),
        (N'Đường đua go-kart', 2200, N'Nhựa polymer'),
        (N'Đường đua mô tô', 1800, N'Nhựa');

    -- 19.17 KhuVucBien (biển 22ha — Weak Entity của KhuVuc)
    -- Bước 1: Tạo bản ghi cha trong KhuVuc
    INSERT INTO KhuVuc (MaCode, TenKhuVuc, MoTa, TrangThai) VALUES
        ('KVB01', N'Biển nước mặn - Nông', N'Khu biển nông, an toàn cho trẻ em', N'Hoạt động'),
        ('KVB02', N'Biển nước mặn - Sâu', N'Khu biển sâu cho người lớn', N'Hoạt động'),
        ('KVB03', N'Biển nước ngọt - Trẻ em', N'Hồ nước ngọt dành cho trẻ nhỏ', N'Hoạt động'),
        ('KVB04', N'Khu sóng mạnh (1.6m)', N'Khu vực sóng nhân tạo mạnh', N'Hoạt động'),
        ('KVB05', N'Khu máng trượt cao tốc', N'Máng trượt nước tốc độ cao', N'Hoạt động'),
        ('KVB06', N'Khu lướt ván', N'Khu vực lướt ván trên sóng', N'Hoạt động');
    -- Bước 2: Extend thuộc tính đặc thù biển
    INSERT INTO KhuVucBien (IdKhuVuc, DoSauToiDa, YeuCauPhao) VALUES
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVB01'), 1.5, 0),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVB02'), 3.0, 1),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVB03'), 0.8, 1),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVB04'), 2.2, 1),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVB05'), 1.5, 0),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVB06'), 2.0, 1);

    -- 19.18 ThietBiTaoSong
    INSERT INTO ThietBiTaoSong (TenThietBi, CongSuat, TrangThai) VALUES
        (N'Máy tạo sóng số 1', '15kW', 'HoatDong'),
        (N'Máy tạo sóng số 2', '15kW', 'HoatDong'),
        (N'Máy tạo sóng dự phòng', '10kW', 'BaoTri');

    -- 19.19 KhuVucThu (10 khu vườn thú — Weak Entity của KhuVuc)
    -- Bước 1: Tạo bản ghi cha trong KhuVuc
    INSERT INTO KhuVuc (MaCode, TenKhuVuc, MoTa, TrangThai) VALUES
        ('KVT01', N'Khu hổ', N'Hổ Đông Dương, hổ Bengal', N'Hoạt động'),
        ('KVT02', N'Khu voi', N'Voi châu Á', N'Hoạt động'),
        ('KVT03', N'Khu gấu', N'Gấu chó, gấu ngựa', N'Hoạt động'),
        ('KVT04', N'Khu hươu nai', N'Hươu sao, nai', N'Hoạt động'),
        ('KVT05', N'Khu sư tử', N'Sư tử châu Phi', N'Hoạt động'),
        ('KVT06', N'Khu linh trưởng', N'Khỉ, vượn tay trắng, đười ươi', N'Hoạt động'),
        ('KVT07', N'Đảo khỉ', N'Khỉ đuôi dài, khỉ mặt đỏ', N'Hoạt động'),
        ('KVT08', N'Khu chim', N'Công, vẹt, hồng hạc', N'Hoạt động'),
        ('KVT09', N'Khu bò sát', N'Cá sấu, trăn, rùa', N'Hoạt động'),
        ('KVT10', N'Khu hà mã & tê giác', N'Hà mã, tê giác Java', N'Hoạt động');
    -- Bước 2: Đánh dấu là khu vực thú
    INSERT INTO KhuVucThu (IdKhuVuc, DienTich, SucChuaDongVat, LoaiMoiTruong) VALUES
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVT01'), 0.8,  8,  N'NgoaiTroi'),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVT02'), 2.0,  4,  N'NgoaiTroi'),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVT03'), 0.6,  6,  N'NgoaiTroi'),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVT04'), 1.2,  20, N'NgoaiTroi'),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVT05'), 1.0,  6,  N'NgoaiTroi'),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVT06'), 0.5,  12, N'NgoaiTroi'),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVT07'), 0.3,  30, N'NgoaiTroi'),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVT08'), 0.8,  50, N'NgoaiTroi'),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVT09'), 0.6,  15, N'Chuong'),
        ((SELECT Id FROM KhuVuc WHERE MaCode='KVT10'), 1.5,  5,  N'NuocNgot');

    -- 19.20 NhaHang (sức chứa theo báo cáo thực tế)
    INSERT INTO NhaHang (TenNhaHang, IdKhuVuc, SucChua, MoTa) VALUES
        (N'Nhà hàng Đại Nam', (SELECT Id FROM KhuVuc WHERE MaCode='KV05'), 2000, N'Phục vụ tiệc cưới, sự kiện lớn'),
        (N'Nhà hàng Ngũ Hành Sơn', (SELECT Id FROM KhuVuc WHERE MaCode='KV05'), 300, N'Đặc sản miền Đông Nam Bộ'),
        (N'Khu Fast-food Biển', (SELECT Id FROM KhuVuc WHERE MaCode='KV01'), 200, N'Pizza, Gà rán, Hải sản nướng'),
        (N'Phố ẩm thực', (SELECT Id FROM KhuVuc WHERE MaCode='KV05'), 500, N'Các gian hàng nhỏ');
    GO

-- ============================================================
-- SECTION 20: SẢN PHẨM, ĐỘNG VẬT, TÀI NGUYÊN VẬT LÝ
-- (DỮ LIỆU THỰC TẾ ĐẠI NAM - KHÔNG PHẢI GIẢ LẬP)
-- ============================================================

DECLARE @don_vi_ve INT = (SELECT Id FROM DonViTinh WHERE KyHieu = 'V');
DECLARE @don_vi_lon INT = (SELECT Id FROM DonViTinh WHERE KyHieu = 'L');

DECLARE @khu_ve_id INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV06');
DECLARE @khu_bien_id INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV01');
DECLARE @khu_dua_id INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV02');
DECLARE @khu_thu_id INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV03');
DECLARE @khu_ks_id INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV04');
DECLARE @khu_nh_id INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV05');
DECLARE @khu_cgm_id INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV08');
DECLARE @khu_pt_id INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV09');
DECLARE @khu_mh_id INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV10');

-- 20.1 SanPham (giá theo báo cáo vận hành 2025-2026)
INSERT INTO SanPham (MaCode, Ten, LoaiSanPham, IdDonViCoBan, DonGia, IdKhuVuc, TrangThai) VALUES
    ('SP001', N'Vé cổng người lớn (A3)', 'Ve', @don_vi_ve, 100000, @khu_ve_id, 'DangBan'),
    ('SP002', N'Vé cổng trẻ em (A3)', 'Ve', @don_vi_ve, 50000, @khu_ve_id, 'DangBan'),
    ('SP003', N'Vé biển nhân tạo (NL)', 'Ve', @don_vi_ve, 150000, @khu_bien_id, 'DangBan'),
    ('SP003B', N'Vé biển nhân tạo (TE)', 'Ve', @don_vi_ve, 50000, @khu_bien_id, 'DangBan'),
    ('SP021', N'Vé vườn thú (NL)', 'Ve', @don_vi_ve, 100000, @khu_thu_id, 'DangBan'),
    ('SP022', N'Vé vườn thú (TE)', 'Ve', @don_vi_ve, 50000, @khu_thu_id, 'DangBan'),
    ('COMBO_A1_NL', N'Combo A1 NL (Cổng+Biển+Thú)', 'Ve', @don_vi_ve, 300000, @khu_ve_id, 'DangBan'),
    ('COMBO_A1_TE', N'Combo A1 TE (Cổng+Biển+Thú)', 'Ve', @don_vi_ve, 150000, @khu_ve_id, 'DangBan'),
    ('COMBO_A2_NL', N'Combo A2 NL (Cổng+Biển/Thú)', 'Ve', @don_vi_ve, 200000, @khu_ve_id, 'DangBan'),
    ('COMBO_A2_TE', N'Combo A2 TE (Cổng+Biển/Thú)', 'Ve', @don_vi_ve, 100000, @khu_ve_id, 'DangBan'),
    ('GAME_01', N'Tàu lượn siêu tốc', 'Ve', @don_vi_ve, 70000, @khu_cgm_id, 'DangBan'),
    ('GAME_02', N'Thám hiểm bầu trời', 'Ve', @don_vi_ve, 50000, @khu_cgm_id, 'DangBan'),
    ('GAME_03', N'Vòng xoay vũ trụ', 'Ve', @don_vi_ve, 30000, @khu_cgm_id, 'DangBan'),
    ('GAME_04', N'Vượt thác', 'Ve', @don_vi_ve, 50000, @khu_cgm_id, 'DangBan'),
    ('GAME_05', N'Con tàu lốc xoáy', 'Ve', @don_vi_ve, 40000, @khu_cgm_id, 'DangBan'),
    ('GAME_06', N'Tàu hòa bình', 'Ve', @don_vi_ve, 50000, @khu_cgm_id, 'DangBan'),
    ('GAME_F1D', N'Đua xe F1 (đơn)', 'Ve', @don_vi_ve, 80000, @khu_cgm_id, 'DangBan'),
    ('GAME_F1P', N'Đua xe F1 (đôi)', 'Ve', @don_vi_ve, 100000, @khu_cgm_id, 'DangBan'),
    ('KID_01', N'Xe lửa sâu', 'Ve', @don_vi_ve, 20000, @khu_pt_id, 'DangBan'),
    ('KID_02', N'Phi thuyền đại chiến', 'Ve', @don_vi_ve, 30000, @khu_pt_id, 'DangBan'),
    ('KID_03', N'Khủng long lướt gió', 'Ve', @don_vi_ve, 30000, @khu_pt_id, 'DangBan'),
    ('KID_04', N'Xe điện đụng', 'Ve', @don_vi_ve, 40000, @khu_pt_id, 'DangBan'),
    ('KID_05', N'Đu quay hai tầng', 'Ve', @don_vi_ve, 30000, @khu_pt_id, 'DangBan'),
    ('KID_06', N'Thuyền đụng (đơn)', 'Ve', @don_vi_ve, 80000, @khu_pt_id, 'DangBan'),
    ('KID_07', N'Thuyền đụng (đôi)', 'Ve', @don_vi_ve, 100000, @khu_pt_id, 'DangBan'),
    ('ADV_01', N'Ngũ long cung (NL)', 'Ve', @don_vi_ve, 50000, @khu_mh_id, 'DangBan'),
    ('ADV_02', N'Luân hồi chuyển kiếp (NL)', 'Ve', @don_vi_ve, 50000, @khu_mh_id, 'DangBan'),
    ('ADV_03', N'Phim 4D', 'Ve', @don_vi_ve, 50000, @khu_mh_id, 'DangBan'),
    ('ADV_04', N'Phim vòm', 'Ve', @don_vi_ve, 50000, @khu_mh_id, 'DangBan'),
    ('ADV_05', N'Thế giới tuyết (NL)', 'Ve', @don_vi_ve, 70000, @khu_mh_id, 'DangBan'),
    ('ADV_06', N'Hành trình mạo hiểm trên cao (NL)', 'Ve', @don_vi_ve, 200000, @khu_mh_id, 'DangBan'),
    ('CB_GAME_12', N'Combo 12 trò (NL)', 'Ve', @don_vi_ve, 400000, @khu_cgm_id, 'DangBan'),
    ('CB_GAME_6', N'Combo 6 trò (NL)', 'Ve', @don_vi_ve, 200000, @khu_cgm_id, 'DangBan'),
    ('CB_GAME_3', N'Combo 3 trò (NL)', 'Ve', @don_vi_ve, 120000, @khu_cgm_id, 'DangBan'),
    ('CB_GAME_12_TE', N'Combo 12 trò (TE)', 'Ve', @don_vi_ve, 200000, @khu_cgm_id, 'DangBan'),
    ('CB_GAME_6_TE', N'Combo 6 trò (TE)', 'Ve', @don_vi_ve, 100000, @khu_cgm_id, 'DangBan'),
    ('SP004', N'Thuê phao bơi', 'Thue', @don_vi_lon, 10000, @khu_bien_id, 'DangBan'),
    ('SP005', N'Gửi đồ / Tủ S', 'Thue', @don_vi_lon, 10000, @khu_bien_id, 'DangBan'),
    ('SP006', N'Tủ đồ gia đình L', 'Thue', @don_vi_lon, 20000, @khu_bien_id, 'DangBan'),
    ('SP007', N'Thuê xe điện 4-7 chỗ (giờ đầu)', 'Thue', @don_vi_lon, 300000, @khu_ve_id, 'DangBan'),
    ('SP026', N'Thuê xe điện 11-14 chỗ (giờ đầu)', 'Thue', @don_vi_lon, 400000, @khu_ve_id, 'DangBan'),
    ('SP027', N'Thuê xe đạp', 'Thue', @don_vi_lon, 50000, @khu_ve_id, 'DangBan'),
    ('SP020', N'Thuê chòi nghỉ mát', 'Thue', @don_vi_ve, 150000, @khu_bien_id, 'DangBan'),
    ('SP008', N'Vé xem đua ngựa', 'Ve', @don_vi_ve, 100000, @khu_dua_id, 'DangBan'),
    ('SP009', N'Vé xem đua chó', 'Ve', @don_vi_ve, 80000, @khu_dua_id, 'DangBan'),
    ('SP010', N'Cho thú ăn (lượt)', 'Ve', @don_vi_ve, 50000, @khu_thu_id, 'DangBan'),
    ('ZOO_RIDE', N'Cưỡi ngựa chụp ảnh', 'Ve', @don_vi_ve, 50000, @khu_thu_id, 'DangBan'),
    ('SP016', N'Vé đỗ xe ô tô', 'Khac', @don_vi_lon, 30000, (SELECT Id FROM KhuVuc WHERE MaCode='KV07'), 'DangBan'),
    ('SP024', N'Vé đỗ xe máy', 'Khac', @don_vi_lon, 10000, (SELECT Id FROM KhuVuc WHERE MaCode='KV07'), 'DangBan'),
    ('SP011', N'Phòng Standard (hướng phố)', 'LuuTru', @don_vi_ve, 600000, @khu_ks_id, 'DangBan'),
    ('SP012', N'Phòng Deluxe (hướng biển)', 'LuuTru', @don_vi_ve, 1000000, @khu_ks_id, 'DangBan'),
    ('SP013_F', N'Phòng Family (4 người)', 'LuuTru', @don_vi_ve, 1200000, @khu_ks_id, 'DangBan'),
    ('SP014_V', N'Villa VIP (hồ bơi riêng)', 'LuuTru', @don_vi_ve, 2500000, @khu_ks_id, 'DangBan'),
    ('SP015', N'Nước suối 500ml', 'AnUong', @don_vi_lon, 10000, @khu_nh_id, 'DangBan'),
    ('SP101', N'Cơm phần', 'AnUong', (SELECT TOP 1 Id FROM DonViTinh WHERE Ten = N'Suất'), 45000, @khu_nh_id, 'DangBan'),
    ('SP102', N'Mì/Phở', 'AnUong', (SELECT TOP 1 Id FROM DonViTinh WHERE Ten = N'Suất'), 35000, @khu_nh_id, 'DangBan'),
    ('SP103', N'Nước ngọt lon', 'AnUong', @don_vi_lon, 15000, @khu_nh_id, 'DangBan'),
    ('SP104', N'Kem/Đá bào', 'AnUong', @don_vi_lon, 20000, @khu_nh_id, 'DangBan'),
    ('SP019', N'Massage/Spa 60 phút', 'DichVu', @don_vi_ve, 500000, @khu_ks_id, 'DangBan');

    -- Trò chơi đã nằm trong DanhSachThietBi (LoaiThietBi='TroChoi') — seed ở phần 21.2
       

    -- Cấp phát SanPham_Ve cho tất cả vé (ban đầu IdThietBi = NULL)
    INSERT INTO SanPham_Ve (IdSanPham, IdThietBi)
    SELECT Id, NULL FROM SanPham WHERE LoaiSanPham = 'Ve';

    -- ========== MAP SanPham_Ve.IdThietBi → DanhSachThietBi ==========
    -- Nguyên tắc: Nhiều SanPham (vé NL/TE/đơn/đôi) → 1 DanhSachThietBi (trò chơi vật lý)
    -- Vé cổng, vé biển, vé vườn thú, combo: giữ NULL (check theo KhuVuc, không check trò)

    -- KV08: Khu cảm giác mạnh
    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_TAULUON')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'GAME_01');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_THAMHIEM')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'GAME_02');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_VONGXOAY')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'GAME_03');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_VUOTTHAC')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'GAME_04');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_LOCXOAY')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'GAME_05');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_HOABINH')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'GAME_06');

    -- Đua xe F1: vé đơn + đôi → CÙNG 1 trò chơi vật lý
    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_DUAXEF1')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode IN ('GAME_F1D', 'GAME_F1P'));

    -- KV09: Khu phổ thông
    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_XELUASAU')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'KID_01');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_PHITHUYEN')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'KID_02');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_KHUNGLONG')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'KID_03');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_XEDUNG')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'KID_04');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_DUQUAY')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'KID_05');

    -- Thuyền đụng: vé đơn + đôi → CÙNG 1 trò chơi vật lý
    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_THUYENDUNG')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode IN ('KID_06', 'KID_07'));

    -- KV10: Khu khám phá mạo hiểm
    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_NGULONG')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'ADV_01');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_LUANHOI')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'ADV_02');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_PHIM4D')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'ADV_03');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_PHIMVOM')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'ADV_04');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_TUYET')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'ADV_05');

    UPDATE SanPham_Ve SET IdThietBi = (SELECT Id FROM DanhSachThietBi WHERE MaCode = 'TC_MAOHIEM')
    WHERE IdSanPham IN (SELECT Id FROM SanPham WHERE MaCode = 'ADV_06');

-- 20.2 DongVat (vườn thú mở 12.5ha, 63 loài, thành viên SEAZA)
INSERT INTO DongVat (Ten, Loai, NgaySinh, TinhTrangSucKhoe) VALUES
    (N'Hổ Đông Dương', N'Thú ăn thịt', '2018-05-10', N'Tốt'),
    (N'Hổ Bengal', N'Thú ăn thịt', '2019-02-15', N'Tốt'),
    (N'Cọp trắng (White Tiger)', N'Thú ăn thịt', '2020-01-20', N'Tốt'),
    (N'Sư tử châu Phi', N'Thú ăn thịt', '2017-08-20', N'Tốt'),
    (N'Sư tử trắng', N'Thú ăn thịt', '2019-11-15', N'Tốt'),
    (N'Báo lửa', N'Thú ăn thịt', '2020-06-10', N'Tốt'),
    (N'Voi châu Á', N'Thú ăn cỏ', '2015-03-20', N'Tốt'),
    (N'Gấu chó', N'Thú ăn tạp', '2017-07-30', N'Tốt'),
    (N'Gấu ngựa', N'Thú ăn tạp', '2016-11-05', N'Tốt'),
    (N'Hươu cao cổ', N'Thú ăn cỏ', '2019-03-12', N'Tốt'),
    (N'Hươu sao', N'Thú ăn cỏ', '2020-11-12', N'Tốt'),
    (N'Linh dương sừng xoắn', N'Thú ăn cỏ', '2018-09-01', N'Tốt'),
    (N'Linh dương đầu bò', N'Thú ăn cỏ', '2017-12-15', N'Tốt'),
    (N'Linh dương sừng kiếm', N'Thú ăn cỏ', '2019-06-20', N'Tốt'),
    (N'Cá sấu hỏa tiễn', N'Bò sát', '2016-09-09', N'Tốt'),
    (N'Hổ mang chúa', N'Bò sát', '2019-07-22', N'Tốt'),
    (N'Kỳ đà nước', N'Bò sát', '2020-02-18', N'Tốt'),
    (N'Đà điểu', N'Chim', '2021-01-05', N'Tốt'),
    (N'Công xanh', N'Chim', '2020-06-22', N'Tốt'),
    (N'Hồng hạc', N'Chim', '2019-09-10', N'Tốt'),
    (N'Ngựa vằn', N'Thú ăn cỏ', '2018-06-18', N'Tốt'),
    (N'Hà mã', N'Thú ăn cỏ', '2016-02-28', N'Tốt'),
    (N'Tê giác trắng', N'Thú ăn cỏ', '2014-10-15', N'Tốt'),
    (N'Voọc chà vá chân nâu', N'Linh trưởng', '2018-04-10', N'Tốt'),
    (N'Khỉ đuôi dài', N'Linh trưởng', '2020-05-07', N'Tốt'),
    (N'Khỉ sóc Nam Mỹ', N'Linh trưởng', '2021-08-10', N'Tốt'),
    (N'Vượn tay trắng', N'Linh trưởng', '2019-12-20', N'Tốt'),
    (N'Rùa Galapagos', N'Bò sát', '2010-01-01', N'Tốt'),
    (N'Lạc đà', N'Thú ăn cỏ', '2019-08-25', N'Tốt');

-- 20.3 Hạ tầng: Kiosk, BaiDoXe
INSERT INTO Kiosk (ViTri, TrangThai) VALUES
    (N'Cạnh biển nhân tạo', 'Online'), (N'Cạnh trường đua', 'Online'), (N'Cổng chính', 'Online');

INSERT INTO BaiDoXe (TenBai, TongCho, IdKhuVuc) VALUES
    (N'Bãi xe trung tâm', 500, (SELECT Id FROM KhuVuc WHERE MaCode='KV07')),
    (N'Bãi xe biển',      200, (SELECT Id FROM KhuVuc WHERE MaCode='KV01')),
    (N'Bãi xe trường đua', 300, (SELECT Id FROM KhuVuc WHERE MaCode='KV02'));

-- 20.4 LoaiPhong + Phong (Khách sạn Đại Nam — 4 loại chuẩn, seed 18 phòng đại diện)
-- Luồng giá: Phong → LoaiPhong → SanPham → BangGia (engine không đổi)
INSERT INTO LoaiPhong (MaLoai, TenLoai, SucChuaMoiPhong, DienTich, SoPhongNgu, LaVilla, TrangBiTomTat, IdSanPham) VALUES
    ('SUPERIOR', N'Phòng Superior', 2, 25,  1, 0, N'1 giường đôi, tiện nghi cơ bản',             (SELECT Id FROM SanPham WHERE MaCode='SP011')),
    ('DELUXE',   N'Phòng Deluxe',   2, 50,  1, 0, N'View hướng hồ, không gian rộng, cao cấp',   (SELECT Id FROM SanPham WHERE MaCode='SP012')),
    ('FAMILY',   N'Phòng Family',   4, 60,  2, 0, N'2 phòng ngủ độc lập, phù hợp gia đình',     (SELECT Id FROM SanPham WHERE MaCode='SP013_F')),
    ('VILLA',    N'Villa VIP',      6, 150, 3, 1, N'Hồ bơi riêng, bếp riêng, 3 phòng ngủ',     (SELECT Id FROM SanPham WHERE MaCode='SP014_V'));

DECLARE @lp_sup INT = (SELECT Id FROM LoaiPhong WHERE MaLoai = 'SUPERIOR');
DECLARE @lp_dlx INT = (SELECT Id FROM LoaiPhong WHERE MaLoai = 'DELUXE');
DECLARE @lp_fam INT = (SELECT Id FROM LoaiPhong WHERE MaLoai = 'FAMILY');
DECLARE @lp_vip INT = (SELECT Id FROM LoaiPhong WHERE MaLoai = 'VILLA');

INSERT INTO Phong (MaCode, TenPhong, IdLoaiPhong, SucChua, TrangThai) VALUES
    ('P01', N'Phòng 101', @lp_sup, 2, 'Trong'), ('P02', N'Phòng 102', @lp_sup, 2, 'Trong'),
    ('P03', N'Phòng 103', @lp_sup, 2, 'Trong'), ('P04', N'Phòng 104', @lp_sup, 2, 'Trong'),
    ('P05', N'Phòng 105', @lp_sup, 2, 'Trong'), ('P06', N'Phòng 106', @lp_sup, 2, 'Trong'),
    ('P07', N'Phòng 107', @lp_sup, 2, 'Trong'), ('P08', N'Phòng 108', @lp_sup, 2, 'Trong'),
    ('P09', N'Phòng 201', @lp_dlx, 2, 'Trong'), ('P10', N'Phòng 202', @lp_dlx, 2, 'Trong'),
    ('P11', N'Phòng 203', @lp_dlx, 2, 'Trong'), ('P12', N'Phòng 204', @lp_dlx, 2, 'Trong'),
    ('P13', N'Phòng 205', @lp_dlx, 2, 'Trong'),
    ('P14', N'Phòng 301', @lp_fam, 4, 'Trong'), ('P15', N'Phòng 302', @lp_fam, 4, 'Trong'),
    ('P16', N'Phòng 303', @lp_fam, 4, 'Trong'),
    ('P17', N'Villa 01',  @lp_vip, 6, 'Trong'), ('P18', N'Villa 02',  @lp_vip, 6, 'Trong');
GO

-- 20.5 Tài nguyên vật lý (cần batch riêng vì dùng WHILE loop)
DECLARE @khu_bien_vatly INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV01');

-- TuDo (50 tủ đồ khu biển)
DECLARE @k INT = 1;
WHILE @k <= 50
BEGIN
    INSERT INTO TuDo (IdKhuVuc, MaTu, KichThuoc, TrangThai)
    VALUES (@khu_bien_vatly, 'TU' + RIGHT('00' + CAST(@k AS VARCHAR), 2),
            CASE WHEN @k % 10 < 7 THEN 'S' ELSE 'L' END, 'Trong');
    SET @k = @k + 1;
END

-- BanAn (30 bàn NH Đại Nam + 20 bàn Phố ẩm thực)
DECLARE @nh_dn INT = (SELECT Id FROM NhaHang WHERE TenNhaHang = N'Nhà hàng Đại Nam');
DECLARE @nh_pa INT = (SELECT Id FROM NhaHang WHERE TenNhaHang = N'Phố ẩm thực');
DECLARE @b INT = 1;
WHILE @b <= 30
BEGIN
    INSERT INTO BanAn (IdNhaHang, MaBan, SucChua, TrangThai)
    VALUES (@nh_dn, 'B' + RIGHT('00' + CAST(@b AS VARCHAR), 2),
            CASE WHEN @b % 3 = 0 THEN 6 ELSE 4 END, 'Trong');
    SET @b = @b + 1;
END
WHILE @b <= 50
BEGIN
    INSERT INTO BanAn (IdNhaHang, MaBan, SucChua, TrangThai)
    VALUES (@nh_pa, 'P' + RIGHT('00' + CAST(@b-30 AS VARCHAR), 2), 4, 'Trong');
    SET @b = @b + 1;
END

-- KhuVucBien (Weak Entity — insert KhuVuc trước, rồi extend)
INSERT INTO KhuVuc (MaCode, TenKhuVuc, MoTa, TrangThai) VALUES
('KVB07', N'Biển nước mặn Đại Nam (Khu trẻ em)', N'Khu biển nhân tạo dành cho trẻ em', N'Hoạt động'),
('KVB08', N'Biển nước mặn Đại Nam (Khu biển sâu)', N'Khu biển sâu cho người lớn', N'Hoạt động'),
('KVB09', N'Biển nước ngọt Đại Nam (Hồ tạo sóng)', N'Hồ tạo sóng nhân tạo', N'Hoạt động'),
('KVB10', N'Khu vực Bãi Cát Cọ Nhân Tạo', N'Bãi cát nhân tạo phục vụ tắm nắng', N'Hoạt động');

INSERT INTO KhuVucBien (IdKhuVuc, DoSauToiDa, YeuCauPhao) VALUES
((SELECT Id FROM KhuVuc WHERE MaCode='KVB07'), 0.8, 0),
((SELECT Id FROM KhuVuc WHERE MaCode='KVB08'), 2.0, 1),
((SELECT Id FROM KhuVuc WHERE MaCode='KVB09'), 1.5, 1),
((SELECT Id FROM KhuVuc WHERE MaCode='KVB10'), 0.0, 0);

-- ChoiNghiMat (10 chòi khu biển)
DECLARE @khu_bien_bien INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KVB07');
DECLARE @sp_choi INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP020');
DECLARE @c INT = 1;
WHILE @c <= 10
BEGIN
    INSERT INTO ChoiNghiMat (MaCode, IdKhuVucBien, IdSanPham, TenChoi, SucChua, TrangThai)
    VALUES ('CHOI' + RIGHT('00' + CAST(@c AS VARCHAR), 2), @khu_bien_bien, @sp_choi,
            N'Chòi ' + CAST(@c AS NVARCHAR), 4, 'Trong');
    SET @c = @c + 1;
END

-- QuyDoiDonVi
INSERT INTO QuyDoiDonVi (IdSanPham, IdDonViNho, IdDonViLon, TyLeQuyDoi, GiaBanRieng)
VALUES ((SELECT Id FROM SanPham WHERE MaCode = 'SP103'),
        (SELECT Id FROM DonViTinh WHERE KyHieu = 'L'),
        (SELECT Id FROM DonViTinh WHERE KyHieu = 'T'), 24, 220000);

-- BangGia (Flat Pricing Matrix)
DECLARE @sp_sup2 INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP011');
DECLARE @sp_dlx2 INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP012');
DECLARE @sp_fam2 INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP013_F');
DECLARE @sp_vip2 INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP014_V');

-- Phòng KS: GiaBan
IF @sp_sup2 IS NOT NULL
INSERT INTO BangGia (IdSanPham, LoaiGiaApDung, GiaBan) VALUES
    (@sp_sup2, 'MacDinh', 600000), (@sp_sup2, 'CuoiTuan', 800000), (@sp_sup2, 'NgayLe', 1000000);
IF @sp_dlx2 IS NOT NULL
INSERT INTO BangGia (IdSanPham, LoaiGiaApDung, GiaBan) VALUES
    (@sp_dlx2, 'MacDinh', 1000000), (@sp_dlx2, 'CuoiTuan', 1200000), (@sp_dlx2, 'NgayLe', 1500000);
IF @sp_fam2 IS NOT NULL
INSERT INTO BangGia (IdSanPham, LoaiGiaApDung, GiaBan) VALUES
    (@sp_fam2, 'MacDinh', 1200000), (@sp_fam2, 'CuoiTuan', 1500000), (@sp_fam2, 'NgayLe', 1800000);
IF @sp_vip2 IS NOT NULL
INSERT INTO BangGia (IdSanPham, LoaiGiaApDung, GiaBan) VALUES
    (@sp_vip2, 'MacDinh', 2500000), (@sp_vip2, 'CuoiTuan', 3000000), (@sp_vip2, 'NgayLe', 3500000);

-- Thuê xe điện: Block 60p đầu, lố mỗi 30p thêm 100k
DECLARE @sp_xe_dien INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP007');
IF @sp_xe_dien IS NOT NULL
INSERT INTO BangGia (IdSanPham, LoaiGiaApDung, GiaBan, PhutBlock, PhutTiep, GiaPhuThu, TienCoc) VALUES
    (@sp_xe_dien, 'MacDinh', 300000, 60, 30, 100000, 500000),
    (@sp_xe_dien, 'CuoiTuan', 350000, 60, 30, 100000, 500000),
    (@sp_xe_dien, 'NgayLe', 400000, 60, 30, 100000, 500000);
GO

-- =================== SECTION 21: SEED DỮ LIỆU BẢNG MỚI ===================

-- 21.1 QuyTacDiem (quy tắc tích điểm theo hạng khách)
INSERT INTO QuyTacDiem (TenQuyTac, TongDonToiThieu, SoDiemThuong, LoaiKhachApDung, TrangThai) VALUES
    (N'Thưởng khách thường',   100000,  1,   NULL,        1),
    (N'Thưởng khách bạc',      200000,  3,   'BacSilver', 1),
    (N'Thưởng khách vàng',     500000,  8,   'VangGold',  1),
    (N'Thưởng khách diamond',  500000,  15,  'Kim',       1);

-- 21.2 DanhSachThietBi (thiết bị đại diện toàn khu)
INSERT INTO DanhSachThietBi
    (MaCode, TenThietBi, LoaiThietBi, IdKhuVuc, MoTa, NgayMua, GiaTriMua, TrangThai, ChuKyBaoTriThang)
VALUES
    -- Trò chơi KV08: Khu cảm giác mạnh
    ('TC_TAULUON',    N'Tàu lượn siêu tốc', 'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV08'), N'Tàu lượn cao tốc 60km/h, 3 vòng xoắn',    '2015-01-01', 5000000000, 'HoatDong', 3),
    ('TC_VUOTTHAC',   N'Vượt thác',          'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV08'), N'Trượt nước từ đỉnh thác 20m',              '2015-01-01', 2000000000, 'HoatDong', 3),
    ('TC_THAMHIEM',   N'Thám hiểm bầu trời', 'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV08'), N'Bay lượn trên không 360 độ',               '2016-01-01', 1500000000, 'HoatDong', 3),
    ('TC_VONGXOAY',   N'Vòng xoay vũ trụ',   'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV08'), N'Vòng xoay khổng lồ cảm giác mạnh',        '2016-01-01', 2000000000, 'HoatDong', 3),
    ('TC_LOCXOAY',    N'Con tàu lốc xoáy',   'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV08'), N'Tàu lắc 360 độ',                          '2016-01-01', 1800000000, 'HoatDong', 3),
    ('TC_HOABINH',    N'Tàu hòa bình',       'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV08'), N'Tàu du ngoạn quanh khu vui chơi',          '2016-01-01',  800000000, 'HoatDong', 6),
    ('TC_DUAXEF1',    N'Đua xe F1',           'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV08'), N'Đường đua F1 mini cho khách (đơn + đôi)',  '2018-01-01', 3000000000, 'HoatDong', 3),
    ('TC_BANGCHUYEN', N'Băng chuyền',         'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV08'), N'Băng chuyền cao tốc vui nhộn',             '2016-01-01',  600000000, 'HoatDong', 6),
    -- Trò chơi KV09: Khu phổ thông
    ('TC_XEDUNG',     N'Xe điện đụng',        'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV09'), N'Xe điện đụng cho trẻ em và gia đình',      '2018-06-01',  500000000, 'HoatDong', 6),
    ('TC_DUQUAY',     N'Đu quay hai tầng',    'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV09'), N'Đu quay 2 tầng 36 ghế',                   '2018-06-01',  800000000, 'HoatDong', 6),
    ('TC_XELUASAU',   N'Xe lửa sâu',          'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV09'), N'Xe lửa ma ám trong hang tối',              '2018-06-01',  600000000, 'HoatDong', 6),
    ('TC_PHITHUYEN',  N'Phi thuyền đại chiến','TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV09'), N'Phi thuyền xoay chiến đấu',                '2018-06-01',  700000000, 'HoatDong', 6),
    ('TC_KHUNGLONG',  N'Khủng long lướt gió', 'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV09'), N'Tàu khủng long cho trẻ em',                '2018-06-01',  500000000, 'HoatDong', 6),
    ('TC_THUYENDUNG', N'Thuyền đụng',         'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV09'), N'Thuyền đụng trên mặt hồ (đơn + đôi)',     '2018-06-01',  400000000, 'HoatDong', 6),
    -- Trò chơi KV10: Khu khám phá mạo hiểm
    ('TC_NGULONG',    N'Ngũ long cung',                'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV10'), N'Nhà ma Ngũ Long Cung',           '2015-01-01', 1000000000, 'HoatDong', 6),
    ('TC_LUANHOI',    N'Luân hồi chuyển kiếp',         'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV10'), N'Nhà ma Luân hồi chuyển kiếp',    '2015-01-01', 1000000000, 'HoatDong', 6),
    ('TC_PHIM4D',     N'Phim 4D',                      'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV10'), N'Rạp phim 4D hiệu ứng đặc biệt',  '2015-01-01',  500000000, 'HoatDong', 12),
    ('TC_PHIMVOM',    N'Phim vòm',                     'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV10'), N'Rạp chiếu phim vòm 180 độ',       '2015-01-01',  500000000, 'HoatDong', 12),
    ('TC_TUYET',      N'Thế giới tuyết',               'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV10'), N'Khu tuyết nhân tạo -5°C',         '2017-01-01', 2000000000, 'HoatDong', 1),
    ('TC_MAOHIEM',    N'Hành trình mạo hiểm trên cao', 'TroChoi', (SELECT Id FROM KhuVuc WHERE MaCode='KV10'), N'Ziplining + cầu treo mạo hiểm',   '2019-01-01', 1500000000, 'HoatDong', 3),
    -- Máy tạo sóng
    (NULL, N'Máy tạo sóng số 1',    'TaoSong', (SELECT Id FROM KhuVuc WHERE MaCode='KV01'), NULL, '2016-03-01', 1500000000, 'HoatDong', 1),
    (NULL, N'Máy tạo sóng số 2',    'TaoSong', (SELECT Id FROM KhuVuc WHERE MaCode='KV01'), NULL, '2016-03-01', 1500000000, 'HoatDong', 1),
    (NULL, N'Máy tạo sóng dự phòng','TaoSong', (SELECT Id FROM KhuVuc WHERE MaCode='KV01'), NULL, '2019-01-01', 1200000000, 'BaoTri',   1),
    -- Xe điện
    (NULL, N'Xe điện 7 chỗ #01',    'XeDien',  (SELECT Id FROM KhuVuc WHERE MaCode='KV06'), NULL, '2020-01-01',  350000000, 'HoatDong', 6),
    (NULL, N'Xe điện 7 chỗ #02',    'XeDien',  (SELECT Id FROM KhuVuc WHERE MaCode='KV06'), NULL, '2020-01-01',  350000000, 'HoatDong', 6),
    (NULL, N'Xe điện 14 chỗ #01',   'XeDien',  (SELECT Id FROM KhuVuc WHERE MaCode='KV06'), NULL, '2021-06-01',  500000000, 'HoatDong', 6),
    -- Kiosk
    (NULL, N'Kiosk biển',           'Kiosk',   (SELECT Id FROM KhuVuc WHERE MaCode='KV01'), NULL, '2019-01-01',   50000000, 'HoatDong', 12),
    (NULL, N'Kiosk trường đua',     'Kiosk',   (SELECT Id FROM KhuVuc WHERE MaCode='KV02'), NULL, '2019-01-01',   50000000, 'HoatDong', 12),
    (NULL, N'Kiosk cổng chính',     'Kiosk',   (SELECT Id FROM KhuVuc WHERE MaCode='KV06'), NULL, '2019-01-01',   50000000, 'HoatDong', 12);
GO

-- =====================================================================
-- 🚨 CHÈN GIẢ LẬP SỐ DƯ TỒN KHO ĐẦU KỲ ĐỂ TEST (Mock Inventory Data)
-- Giả lập Quản lý Kho đã Nhập Kho Lần Đầu (Initial Stock) 
-- =====================================================================

-- 1. Tạo Phiếu Nhập Kho gốc giả lập
INSERT INTO PhieuNhapKho (IdKho, IdNhaCungCap, NgayNhap, SoChungTu, TongTien, CreatedBy)
VALUES (1, (SELECT TOP 1 Id FROM NhaCungCap), GETDATE() - 7, 'NK-INIT-001', 50000000, (SELECT TOP 1 Id FROM NhanVien));

DECLARE @IdPhieuNhapInit INT = SCOPE_IDENTITY();

-- 2. Nhập Kho các Sản Phẩm F&B (Mỗi loại 500 đơn vị Base Unit)
INSERT INTO ChiTietNhapKho (IdPhieuNhap, IdSanPham, SoLuong, DonGiaNhap, IdDonViNhap, TyLeQuyDoi)
SELECT 
    @IdPhieuNhapInit, 
    Id, 
    500,  -- Cố tình ép 500 lon/cái
    DonGia * 0.5, -- Giả lập giá nhập bằng 50% giá bán lẻ POS
    IdDonViCoBan, 1
FROM SanPham 
WHERE LoaiSanPham IN ('AnUong', 'DoLuuNiem');

-- 3. Đánh vào sổ cái Tồn Kho (TonKho)
INSERT INTO TonKho (IdKho, IdSanPham, SoLuong)
SELECT 
    1, 
    IdSanPham, 
    SoLuong
FROM ChiTietNhapKho WHERE IdPhieuNhap = @IdPhieuNhapInit;

-- 4. Ghi Lịch sử Thẻ Kho (TheKho - Ledger) để có Giá Vốn (DonGiaVatTu)
INSERT INTO TheKho (IdKho, IdSanPham, LoaiGiaoDich, SoLuongThayDoi, TonCuoi, DonGiaVatTu, IdThamChieu, GhiChu, ThoiGianGiaoDich, CreatedBy)
SELECT 
    1,
    IdSanPham,
    'NHAP_KHO',
    SoLuong,
    SoLuong,
    DonGiaNhap,
    @IdPhieuNhapInit,
    N'Khởi tạo số dư đầu kỳ (Mock Data)',
    GETDATE() - 7,
    (SELECT TOP 1 Id FROM NhanVien)
FROM ChiTietNhapKho WHERE IdPhieuNhap = @IdPhieuNhapInit;
-- =====================================================================
-- 🚨 GIẢ LẬP GIAO DỊCH LỊCH SỬ THẺ KHO (MOCK TRANSACTIONS CHO SỔ THẺ KHO)
-- =====================================================================

DECLARE @IdSP104 INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP104');
DECLARE @IdSP101 INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP101');
DECLARE @NvId INT = (SELECT TOP 1 Id FROM NhanVien);

-- Kem/Đá Bào (Ban đầu nhập 500)
INSERT INTO TheKho (IdKho, IdSanPham, LoaiGiaoDich, SoLuongThayDoi, TonCuoi, DonGiaVatTu, IdThamChieu, GhiChu, ThoiGianGiaoDich, CreatedBy)
VALUES (1, @IdSP104, 'XUAT_POS', -15, 485, 10000, 101, N'Bán lẻ POS (Ca Sáng)', GETDATE() - 6, @NvId);

INSERT INTO TheKho (IdKho, IdSanPham, LoaiGiaoDich, SoLuongThayDoi, TonCuoi, DonGiaVatTu, IdThamChieu, GhiChu, ThoiGianGiaoDich, CreatedBy)
VALUES (1, @IdSP104, 'NHAP_KHO', 200, 685, 11000, 102, N'Nhập thêm chuẩn bị cuối tuần', GETDATE() - 4, @NvId);

INSERT INTO TheKho (IdKho, IdSanPham, LoaiGiaoDich, SoLuongThayDoi, TonCuoi, DonGiaVatTu, IdThamChieu, GhiChu, ThoiGianGiaoDich, CreatedBy)
VALUES (1, @IdSP104, 'XUAT_HUY', -5, 680, 11000, 103, N'Hủy do hỏng lạnh, chảy kem', GETDATE() - 1, @NvId);

-- Cơm phần (Ban đầu nhập 500)
INSERT INTO TheKho (IdKho, IdSanPham, LoaiGiaoDich, SoLuongThayDoi, TonCuoi, DonGiaVatTu, IdThamChieu, GhiChu, ThoiGianGiaoDich, CreatedBy)
VALUES (1, @IdSP101, 'XUAT_POS', -80, 420, 15000, 104, N'Bán suất ăn trưa Khách Đoàn', GETDATE() - 5, @NvId);

-- Cập nhật đồng bộ lại Tồn kho hiện tại
UPDATE TonKho SET SoLuong = 680 WHERE IdKho = 1 AND IdSanPham = @IdSP104;
UPDATE TonKho SET SoLuong = 420 WHERE IdKho = 1 AND IdSanPham = @IdSP101;
GO

-- ==================================================================
-- 📱 TÀI KHOẢN WEB (Tách riêng khỏi KhachHang)
-- ==================================================================
CREATE TABLE TaiKhoanWeb (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SoDienThoai NVARCHAR(20) NOT NULL UNIQUE,
    MatKhau NVARCHAR(100) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    IdKhachHang INT NULL,               -- Link tuỳ chọn sang KhachHang (khi bổ sung thông tin)
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    IsDeleted BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_TaiKhoanWeb_KhachHang FOREIGN KEY (IdKhachHang) REFERENCES KhachHang(Id)
);

-- Mock data: 1 tài khoản web để test có liên kết với Khách Hàng
INSERT INTO KhachHang (MaCode, HoTen, DienThoai, NgayDangKy, LoaiKhach, DiemTichLuy) 
VALUES ('KH0901234567', N'Nguyễn Văn Test', '0901234567', GETDATE(), 'CaNhan', 2000);

DECLARE @mock_kh int = SCOPE_IDENTITY();

INSERT INTO TaiKhoanWeb (SoDienThoai, MatKhau, HoTen, IdKhachHang) 
VALUES ('0901234567', '123456', N'Nguyễn Văn Test', @mock_kh);
GO

-- =================== STORED PROCEDURE NÂNG HẠNG ATOMIC ===================
CREATE PROCEDURE sp_CapNhatChiTieuVaHang
    @IdKhachHang INT,
    @ThucThu DECIMAL(18,0)
AS
BEGIN
    SET NOCOUNT ON;

    -- Chỉ cập nhật và Nâng Hạng Khách nếu Id tồn tại hợp lệ
    IF @IdKhachHang IS NOT NULL AND @ThucThu > 0
    BEGIN
        UPDATE KhachHang 
        SET TongChiTieu = TongChiTieu + @ThucThu,
            LoaiKhach = CASE 
                WHEN TongChiTieu + @ThucThu >= 50000000 THEN 'VVIP'
                WHEN TongChiTieu + @ThucThu >= 20000000 THEN 'VIP'
                ELSE LoaiKhach
            END
        WHERE Id = @IdKhachHang;
    END
END
GO

-- =======================================================================
-- PATCH HR MODULE — Quản Lý Nhân Sự Đầy Đủ
-- Căn cứ: BLLĐ 2019, Luật BHXH, Thông tư 02/2011/TT-BVHTTDL
-- Tài liệu nghiệp vụ: docs/HR_BUSINESS_RULES.md
-- Tất cả con số (hệ số, ngày phép...) lưu trong CauHinhHeThong
-- =======================================================================

-- ── BƯỚC 1: CẤU HÌNH HỆ THỐNG (Rule Engine thay cho hardcode) ──────────
CREATE TABLE CauHinhHeThong (
    Khoa        NVARCHAR(100) NOT NULL PRIMARY KEY,
    GiaTri      NVARCHAR(500) NOT NULL,
    MoTa        NVARCHAR(300) NULL,
    UpdatedAt   DATETIME NULL,
    UpdatedBy   INT NULL REFERENCES NhanVien(Id)
);
GO

INSERT INTO CauHinhHeThong (Khoa, GiaTri, MoTa) VALUES
    ('PHEP_NAM_THUONG_THUONG',  '12',      N'Ngày phép cơ bản NV thường (Đ.113 BLLĐ 2019)'),
    ('PHEP_NAM_NANG_NHOC',      '14',      N'Ngày phép NV nặng nhọc nguy hiểm'),
    ('PHEP_NAM_DAC_BIET',       '16',      N'Ngày phép NV đặc biệt nguy hiểm'),
    ('PHEP_NAM_THAMNIEN_CYCLE', '5',       N'Cứ N năm thâm niên thì +1 ngày phép'),
    ('HE_SO_TANG_CA_THUONG',    '1.5',     N'Hệ số tăng ca ngày thường (Đ.98 BLLĐ 2019)'),
    ('HE_SO_TANG_CA_CUOI_TUAN', '2.0',     N'Hệ số tăng ca ngày nghỉ hàng tuần'),
    ('HE_SO_TANG_CA_NGAY_LE',   '3.0',     N'Hệ số tăng ca ngày lễ Tết'),
    ('HE_SO_CA_DEM',            '1.3',     N'Phụ trội ban đêm 22h-6h'),
    ('TANG_CA_TOIDA_THANG',     '40',      N'Giờ tăng ca tối đa/tháng (Đ.107 BLLĐ)'),
    ('TANG_CA_TOIDA_NAM',       '200',     N'Giờ tăng ca tối đa/năm (thông thường)'),
    ('CHUNGCHI_CANHBAO_NGAY',   '30',      N'Ngày trước hết hạn cảnh báo chứng chỉ'),
    ('NHIBBU_DEADLINE_NGAY',    '30',      N'Số ngày phải dùng ngày bù sau khi tích lũy'),
    ('PHUCAP_NGUYHIEM_THANG',   '500000',  N'Phụ cấp khu nguy hiểm/tháng (nội bộ Đại Nam)'),
    ('DIEM_QUY_DOI_VND',        '1000',    N'Số tiền để đổi 1 điểm tích lũy'),
    ('DIEM_HET_HAN_THANG',      '24',      N'Số tháng điểm hết hạn');
GO

-- ── Cột HR đã thêm trực tiếp vào CREATE TABLE NhanVien ở trên ──────────

-- ── BƯỚC 3: CaLamMau — Template thay thế CHECK cứng ────────────────────
CREATE TABLE CaLamMau (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    TenCa       NVARCHAR(50) NOT NULL,
    GioBatDau   TIME NOT NULL,
    GioKetThuc  TIME NOT NULL,
    SoGioChuan  AS (CAST((CASE WHEN GioKetThuc >= GioBatDau THEN DATEDIFF(MINUTE, GioBatDau, GioKetThuc) ELSE DATEDIFF(MINUTE, GioBatDau, GioKetThuc) + 1440 END) / 60.0 AS DECIMAL(4,2))) PERSISTED,
    LoaiCa      NVARCHAR(20) NOT NULL
        CHECK (LoaiCa IN ('CoDinh','LinhHoat')),
    IsActive    BIT NOT NULL DEFAULT 1
);
GO

INSERT INTO CaLamMau (TenCa, GioBatDau, GioKetThuc, LoaiCa) VALUES
    (N'Ca Sáng',          '06:00', '14:00', 'CoDinh'),
    (N'Ca Chiều',         '14:00', '22:00', 'CoDinh'),
    (N'Ca Tối/Đêm',       '22:00', '06:00', 'CoDinh'),
    (N'Ca Hành Chính',    '08:00', '17:00', 'CoDinh'),
    (N'PT Buổi Sáng',     '07:00', '12:00', 'LinhHoat'),
    (N'PT Buổi Chiều',    '13:00', '18:00', 'LinhHoat'),
    (N'PT Cuối Tuần',     '08:00', '20:00', 'LinhHoat');
GO

-- ── BƯỚC 4: Redesign LichLamViec ────────────────────────────────────────
-- Xóa bảng cũ trước (đã có dữ liệu seed trong schema gốc nếu có)
IF OBJECT_ID('LichLamViec', 'U') IS NOT NULL
    DROP TABLE LichLamViec;
GO

CREATE TABLE LichLamViec (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien      INT NOT NULL REFERENCES NhanVien(Id),
    IdKhuVuc        INT NOT NULL REFERENCES KhuVuc(Id),
    NgayLam         DATE NOT NULL,
    IdCaLamMau      INT NOT NULL REFERENCES CaLamMau(Id),
    LoaiNgayLam     NVARCHAR(20) NOT NULL DEFAULT 'ThuongThuong'
        CHECK (LoaiNgayLam IN ('ThuongThuong','CuoiTuan','NgayLe','LamBu')),
    LoaiNV          NVARCHAR(20) NOT NULL DEFAULT 'FullTime'
        CHECK (LoaiNV IN ('FullTime','PartTime','TheoMua','Intern')),
    TrangThai       NVARCHAR(20) NOT NULL DEFAULT 'KeHoach'
        CHECK (TrangThai IN ('KeHoach','DaXacNhan','DaNghi','NghiPhep','HoanThanh')),
    GhiChu          NVARCHAR(200) NULL,
    CreatedBy       INT NULL REFERENCES NhanVien(Id),
    CreatedAt       DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UqLich_NV_Ngay_Ca UNIQUE (IdNhanVien, NgayLam, IdCaLamMau)
);
GO

-- ── BƯỚC 5: BangChamCong — Quẹt thẻ RFID thực tế ───────────────────────
CREATE TABLE BangChamCong (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien      INT NOT NULL REFERENCES NhanVien(Id),
    IdLichLamViec   INT NULL REFERENCES LichLamViec(Id),  -- NULL = quẹt ngoài lịch (OT đột xuất)
    ThoiGianVao     DATETIME NOT NULL,
    ThoiGianRa      DATETIME NULL,   -- SoGioThucTe tính ở BUS layer (C# code)
    LoaiNgayLam     NVARCHAR(20) NOT NULL DEFAULT 'ThuongThuong'
        CHECK (LoaiNgayLam IN ('ThuongThuong','CuoiTuan','NgayLe','LamBu')),
    ChinhSachLe     NVARCHAR(20) NULL
        CHECK (ChinhSachLe IN ('TinhTien300pct','TichNgayBu')),
    TrangThai       NVARCHAR(20) NOT NULL DEFAULT 'DangLam'
        CHECK (TrangThai IN ('DangLam','DungGio','TreGio','VeSom','TangCa')),
    GhiChu          NVARCHAR(200) NULL,
    CONSTRAINT UqChamCong_NV_Vao UNIQUE (IdNhanVien, ThoiGianVao)
);
GO

-- ── BƯỚC 6: NghiBu — Tích lũy ngày bù khi làm lễ ───────────────────────
CREATE TABLE NghiBu (
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien          INT NOT NULL REFERENCES NhanVien(Id),
    NgayLamBuGoc        DATE NOT NULL,           -- ngày lễ đã làm
    SoNgayBuTichLuy     DECIMAL(3,1) NOT NULL,   -- thường = 1.0
    SoNgayBuDaDung      DECIMAL(3,1) NOT NULL DEFAULT 0,
    SoNgayBuConLai      AS (SoNgayBuTichLuy - SoNgayBuDaDung) PERSISTED,
    NgayHetHan          DATE NOT NULL,           -- +30 ngày theo Luật
    IdBangChamCong      INT NULL REFERENCES BangChamCong(Id)
);
GO


-- ── BƯỚC 8: ChungChiNhanVien — Chứng chỉ hành nghề ──────────────────────
CREATE TABLE ChungChiNhanVien (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien      INT NOT NULL REFERENCES NhanVien(Id),
    LoaiChungChi    NVARCHAR(50) NOT NULL
        CHECK (LoaiChungChi IN (
            'CuuHoBoiLoi',
            'SoCuuYTe_CPR',
            'VanHanhThietBiCoKhi',
            'ChamSocDongVatHoangDa',
            'LaiXeNangHang',
            'AnToanDien',
            'Khac'
        )),
    SoChungChi      NVARCHAR(50) NULL,
    NhaCap          NVARCHAR(100) NULL,
    NgayCap         DATE NOT NULL,
    NgayHetHan      DATE NOT NULL,
    TrangThai       AS (
        CASE
            WHEN NgayHetHan < CAST(GETDATE() AS DATE) THEN 'HetHan'
            WHEN DATEDIFF(DAY, GETDATE(), NgayHetHan) <= 30 THEN 'SapHetHan'
            ELSE 'ConHieuLuc'
        END
    ),
    HinhAnhFile     VARCHAR(255) NULL
);
GO

-- ── BƯỚC 9: SoNgayPhepNam — Theo dõi số ngày phép mỗi năm ───────────────
CREATE TABLE SoNgayPhepNam (
    Id                      INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien              INT NOT NULL REFERENCES NhanVien(Id),
    Nam                     INT NOT NULL,
    SoNgayPhepPhatSinh      INT NOT NULL,   -- Tính từ NhomCongViec + Thâm niên
    SoNgayDaDung            INT NOT NULL DEFAULT 0,
    SoNgayConLai            AS (SoNgayPhepPhatSinh - SoNgayDaDung) PERSISTED,
    CONSTRAINT UqPhepNam_NV_Nam UNIQUE (IdNhanVien, Nam)
);
GO

-- ── BƯỚC 10: DonXinNghi — Nhân viên tự xin, Manager duyệt ──────────────
CREATE TABLE DonXinNghi (
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien          INT NOT NULL REFERENCES NhanVien(Id),
    LoaiNghi            NVARCHAR(30) NOT NULL
        CHECK (LoaiNghi IN (
            'PhepNam',           -- 100% Công ty
            'NghiOm',            -- 75% BHXH
            'ThaiSanNu',         -- 100% BHXH (6 tháng)
            'ThaiSanNam',        -- 100% BHXH (5-14 ngày)
            'TaiNanLaoDong',     -- 100% BHXH
            'NghiBu',            -- dùng ngày bù tích lũy
            'NghiLe',            -- nghỉ lễ bắt buộc 11 ngày/năm
            'DotXuatCoLuong',    -- cưới/tang 3 ngày, 100% Công ty
            'NghiKhongLuong'     -- 0%
        )),
    NgayBatDau          DATE NOT NULL,
    NgayKetThuc         DATE NOT NULL,
    SoNgay              AS (DATEDIFF(DAY, NgayBatDau, NgayKetThuc) + 1) PERSISTED,
    TiLeLuongHuong      DECIMAL(5,2) NOT NULL,  -- 0, 75.00, 100.00
    NguonChiTra         NVARCHAR(10) NOT NULL
        CHECK (NguonChiTra IN ('CongTy','BHXH')),
    LyDo                NVARCHAR(500) NULL,
    TepDinhKem          VARCHAR(255) NULL,       -- đường dẫn file giấy tờ
    TrangThai           NVARCHAR(20) NOT NULL DEFAULT 'ChoDuyet'
        CHECK (TrangThai IN ('ChoDuyet','DaDuyet','TuChoi','DaHuy')),
    IdNguoiDuyet        INT NULL REFERENCES NhanVien(Id),
    NgayDuyet           DATETIME NULL,
    GhiChuDuyet         NVARCHAR(200) NULL,
    CreatedAt           DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT ChkDonNghi_Ngay CHECK (NgayKetThuc >= NgayBatDau)
);
GO

-- ── BƯỚC 11: TaiNanLaoDong ───────────────────────────────────────────────
CREATE TABLE TaiNanLaoDong (
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien          INT NOT NULL REFERENCES NhanVien(Id),
    NgayTaiNan          DATE NOT NULL,
    LoaiTaiNan          NVARCHAR(30) NOT NULL
        CHECK (LoaiTaiNan IN ('NguoiMay','DongVat','DuoiNuoc','NgaHuong','ChayNam','Khac')),
    MucDo               NVARCHAR(20) NOT NULL
        CHECK (MucDo IN ('Nhe','TrungBinh','NangNe','TuVong')),
    MoTa                NVARCHAR(MAX) NOT NULL,
    IdSuCo              INT NULL REFERENCES SuCo(Id),
    NgayNghiBatDau      DATE NULL,
    NgayNghiKetThuc     DATE NULL,
    SoNgayNghi          AS (
        CASE WHEN NgayNghiBatDau IS NOT NULL AND NgayNghiKetThuc IS NOT NULL
             THEN DATEDIFF(DAY, NgayNghiBatDau, NgayNghiKetThuc) + 1
             ELSE NULL END
    ) PERSISTED,
    TiLeBHXH            DECIMAL(5,2) NULL,       -- % lương BHXH chi trả
    TrangThai           NVARCHAR(20) NOT NULL DEFAULT 'DangDieuTri'
        CHECK (TrangThai IN ('DangDieuTri','DaDieuTri','TamTat','VinhVien','TuVong')),
    CreatedAt           DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ── BƯỚC 12: KyLuat ─────────────────────────────────────────────────────
CREATE TABLE KyLuat (
    Id                      INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien              INT NOT NULL REFERENCES NhanVien(Id),
    NgayApDung              DATE NOT NULL,
    HinhThuc                NVARCHAR(30) NOT NULL
        CHECK (HinhThuc IN (
            'CanhCao',           -- Lưu 1 năm, xóa nếu không tái phạm
            'TruLuong',          -- Trừ tiền lương kỳ đó
            'DinhChiCoLuong',    -- Tối đa 15 ngày, chờ điều tra
            'SaThái'             -- Vi phạm nghiêm trọng
        )),
    SoTienTru               DECIMAL(15,0) NOT NULL DEFAULT 0,
    SoNgayDinhChi           INT NOT NULL DEFAULT 0,
    MoTa                    NVARCHAR(500) NOT NULL,
    IdNguoiQuyetDinh        INT NOT NULL REFERENCES NhanVien(Id),
    NgayHetHieuLuc          DATE NULL,           -- NULL = vĩnh viễn
    CreatedAt               DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ── BƯỚC 13: BangLuong — Chốt tháng FullTime + PartTime ─────────────────
CREATE TABLE BangLuong (
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien          INT NOT NULL REFERENCES NhanVien(Id),
    Thang               TINYINT NOT NULL CHECK (Thang BETWEEN 1 AND 12),
    Nam                 INT NOT NULL,
    LoaiHopDong         NVARCHAR(20) NOT NULL,   -- Snapshot tại thời điểm chốt

    -- FullTime
    LuongCoBan          DECIMAL(15,0) NULL,
    NgayLamKeHoach      INT NULL,
    NgayLamThucTe       INT NULL,

    -- PartTime
    SoGioThucTe         DECIMAL(7,2) NULL,
    LuongTheoGio        DECIMAL(10,0) NULL,

    -- Chung
    SoGioTangCa         DECIMAL(5,2) NOT NULL DEFAULT 0,
    ThuongTangCa        DECIMAL(15,0) NOT NULL DEFAULT 0,
    PhuCapNguyHiem      DECIMAL(15,0) NOT NULL DEFAULT 0,
    PhuCapKhac          DECIMAL(15,0) NOT NULL DEFAULT 0,
    TongTruKyLuat       DECIMAL(15,0) NOT NULL DEFAULT 0,
    TongGross           DECIMAL(15,0) NOT NULL DEFAULT 0,
    ThucLinh            DECIMAL(15,0) NOT NULL DEFAULT 0,
    TrangThai           NVARCHAR(20) NOT NULL DEFAULT 'DangTinh'
        CHECK (TrangThai IN ('DangTinh','DaChot','DaThanhToan')),
    GhiChu              NVARCHAR(300) NULL,
    CreatedBy           INT NULL REFERENCES NhanVien(Id),
    CreatedAt           DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT UqBangLuong_NV_Thang UNIQUE (IdNhanVien, Thang, Nam)
);
GO

-- ── INDEX BỔ SUNG ────────────────────────────────────────────────────────
CREATE INDEX Ix_Lich_NV_Thang        ON LichLamViec(IdNhanVien, NgayLam);
CREATE INDEX Ix_Lich_KhuVuc_Ngay     ON LichLamViec(IdKhuVuc, NgayLam);
CREATE INDEX Ix_Cham_NV_Ngay         ON BangChamCong(IdNhanVien, ThoiGianVao);
CREATE INDEX Ix_DonNghi_NV_TT        ON DonXinNghi(IdNhanVien, TrangThai);
CREATE INDEX Ix_ChungChi_NV          ON ChungChiNhanVien(IdNhanVien, NgayHetHan);
CREATE INDEX Ix_Luong_NV             ON BangLuong(IdNhanVien, Nam, Thang);
CREATE INDEX Ix_TaiNan_NV            ON TaiNanLaoDong(IdNhanVien, NgayTaiNan);
GO

-- ── VIEW: Cảnh báo chứng chỉ sắp hết hạn ───────────────────────────────
CREATE VIEW V_ChungChiSapHetHan AS
SELECT
    nv.HoTen,
    nv.MaCode          AS MaNhanVien,
    kv.TenKhuVuc       AS KhuVucChinh,
    cc.LoaiChungChi,
    cc.SoChungChi,
    cc.NgayHetHan,
    DATEDIFF(DAY, GETDATE(), cc.NgayHetHan) AS SoNgayConLai,
    cc.TrangThai
FROM ChungChiNhanVien cc
JOIN NhanVien nv ON cc.IdNhanVien = nv.Id
LEFT JOIN KhuVuc kv ON nv.IdKhuVuc = kv.Id
WHERE cc.TrangThai IN ('SapHetHan','HetHan')
  AND nv.IsDeleted = 0;
GO

-- ── VIEW: Tổng hợp ngày phép còn lại toàn công ty ──────────────────────
CREATE VIEW V_PhepNamToanCongTy AS
SELECT
    nv.HoTen, nv.MaCode, nv.LoaiHopDong, nv.NhomCongViec,
    p.Nam,
    p.SoNgayPhepPhatSinh,
    p.SoNgayDaDung,
    p.SoNgayConLai,
    DATEDIFF(YEAR, nv.CreatedAt, GETDATE()) AS NamThamnien
FROM SoNgayPhepNam p
JOIN NhanVien nv ON p.IdNhanVien = nv.Id
WHERE nv.IsDeleted = 0
  AND p.Nam = YEAR(GETDATE());
GO

-- ── SEED: Tạo bản ghi phép năm 2026 cho NV Admin ────────────────────────
INSERT INTO SoNgayPhepNam (IdNhanVien, Nam, SoNgayPhepPhatSinh)
SELECT Id, 2026, 12 FROM NhanVien WHERE IsDeleted = 0;
GO

GO
