    -- Database Đại Nam Resort (Đồ án môn học)
    -- Dùng để quản lý toàn bộ Khu vui chơi, Nhà hàng, Khách sạn, Kho bãi
    -- Ngày khởi tạo: 15/04/2026

    SET QUOTED_IDENTIFIER ON;
    SET ANSI_NULLS ON;
    GO

    USE master;
    GO
    IF DB_ID('Database_DaiNamv2') IS NOT NULL
    BEGIN
        ALTER DATABASE Database_DaiNamv2 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
        DROP DATABASE Database_DaiNamv2;
    END
    GO
    CREATE DATABASE Database_DaiNamv2;
    GO
    USE Database_DaiNamv2;
    GO

    -- Danh sách các nhóm chức năng chính (8 nhóm):
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


    -- PHẦN 1: CÁC BẢNG HỆ THỐNG GỐC 
    -- Chứa các bảng dùng chung để cấu hình toàn phần mềm

    -- 1.1 Bảng Từ Điển (Lưu danh sách trạng thái để C# load lển combobox, chống gõ sai)
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

    -- 1.2 Bảng chuyển trạng thái (Nhân phẩm của phần mềm)
    -- Cái này cực hay để khóa quy trình, không cho bấm bậy nhảy cóc.
    -- Ví dụ: Đơn Hàng đang "Chờ Thanh Toán" thì nút duy nhất được sáng lên là "Đã Thanh Toán", chứ đố mà bấm vọt thẳng lên "Đã Hoàn Tiền" được. 
    -- Nhờ bảng này quy định rành mạch Luồng (Từ -> Đến), nên Code C# không cần viết dăm ba cái vòng lặp If/Else dài ngoằng check điều kiện nữa.
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

    -- 1.3 Bảng Cấu hình chung cho toàn hệ thống
    CREATE TABLE HeThong.CauHinh (
        Khoa         VARCHAR(100)   PRIMARY KEY,
        GiaTri       NVARCHAR(500)  NOT NULL,
        MoTa         NVARCHAR(200)  NULL,
        CapNhatLuc   DATETIME       NULL,
        CapNhatBoi   INT            NULL
    );
    GO

    -- 1.4 Bảng dữ liệu dịch đa ngôn ngữ (nếu cần)
    CREATE TABLE HeThong.BanDich (
        LoaiThucThe  VARCHAR(50)    NOT NULL,
        IdThucThe    INT            NOT NULL,
        NgonNgu      VARCHAR(10)    NOT NULL,
        TruongDich   VARCHAR(50)    NOT NULL,
        NoiDung      NVARCHAR(MAX)  NOT NULL,
        PRIMARY KEY (LoaiThucThe, IdThucThe, NgonNgu, TruongDich)
    );
    GO

    -- 1.5 Bộ đếm số tự động
    -- Giúp tự sinh ID (1,2,3) nhanh và không bị lỗi trùng lặp khi nhiều máy truy cập cùng lúc
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

    -- 1.6 Hàm kiểm tra dữ liệu từ điển
    -- Công dụng: Ép tất cả các cột trạng thái trong hệ thống phải hỏi xem chữ truyền vào có nằm trong bảng Từ Điển hay chưa.
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

    -- 1.7 Bảng Lịch Sử Chuyển Trạng Thái (Ghi lại ai đã chuyển trạng thái gì lúc nào)
    -- Ví dụ: Đơn hàng từ ChoThanhToan → DaThanhToan bởi NV005 lúc 14:00
    -- Khi khách khiếu nại, hệ thống có bằng chứng rõ ràng
    CREATE TABLE HeThong.LichSuTrangThai (
        Id               BIGINT IDENTITY(1,1) PRIMARY KEY,
        ThucThe          VARCHAR(50)   NOT NULL,   -- Tên loại: 'DonHang', 'Phong', 'VeDienTu'...
        IdThucThe        INT           NOT NULL,   -- Id của bản ghi được chuyển
        TuTrangThai      VARCHAR(50)   NOT NULL,
        DenTrangThai     VARCHAR(50)   NOT NULL,
        IdNguoiThucHien  INT           NOT NULL,
        ThoiGian         DATETIME      NOT NULL DEFAULT GETDATE(),
        GhiChu           NVARCHAR(300) NULL
    );
    GO

    CREATE INDEX IX_LichSuTT_ThucThe ON HeThong.LichSuTrangThai(ThucThe, IdThucThe);
    GO

    -- 1.6 Nhật ký thao tác (Audit Log)
    -- Ghi lại MỌI thao tác quan trọng của nhân viên trên hệ thống.
    -- Dùng cho: tra cứu ai đã sửa/xóa/khóa cái gì, khi nào, giá trị cũ → mới.
    CREATE TABLE HeThong.NhatKyThaoTac (
        Id               BIGINT IDENTITY(1,1) PRIMARY KEY,
        ThucThe          VARCHAR(50)    NOT NULL,   -- 'KhachHang', 'TheRFID', 'DonHang'...
        IdThucThe        INT            NOT NULL,   -- Id của bản ghi bị tác động
        HanhDong         NVARCHAR(100)  NOT NULL,   -- 'Thêm mới Combo', 'Cập nhật', v.v.
        GiaTriCu         NVARCHAR(MAX)  NULL,       -- Giá trị cũ (có thể rất dài)
        GiaTriMoi        NVARCHAR(MAX)  NULL,       -- Giá trị mới
        IdNguoiThucHien  INT            NOT NULL,   -- FK → DoiTac.ThongTin (nhân viên)
        ThoiGian         DATETIME       NOT NULL DEFAULT GETDATE(),
        GhiChu           NVARCHAR(300)  NULL
    );
    GO

    CREATE INDEX IX_NhatKy_ThucThe ON HeThong.NhatKyThaoTac(ThucThe, IdThucThe);
    CREATE INDEX IX_NhatKy_NguoiThucHien ON HeThong.NhatKyThaoTac(IdNguoiThucHien, ThoiGian DESC);
    GO

    -- PHẦN 2: ĐỐI TÁC (Quản lý con người và công ty)
    -- Dùng mô hình con kế thừa cha. Bảng 'ThongTin' là cha chứa Tên, SĐT.
    -- Các bảng Nhân Viên, Khách... là con, dùng lại ID của cha.

    -- 2.1 Bảng cha: Thông tin chung nhất của một người/tổ chức
    --  Tại sao lại gộp Nhân Viên, Khách Hàng, Nhà Cung Cấp chung vào 1 bảng ThongTin? Lỡ nhập nhằng thì sao?
    -- Bảng Thông Tin (Cha) chỉ giữ các thứ dùng chung như: Tên, SĐT, CCCD để chống trùng lặp. 
    -- Còn cột "LoaiDoiTac" sẽ phân biệt ông này là Khách hay Nhân Viên. Các thông tin chi tiết riêng biệt thì sẽ vứt xuống các Bảng Con ở dưới (DoiTac.NhanVien, DoiTac.KhachHang) để quản lý.
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

    -- 2.2 Bảng Nhân Viên (Dùng lại ID của DoiTac)
    CREATE TABLE DoiTac.NhanVien (
        IdDoiTac       INT            PRIMARY KEY REFERENCES DoiTac.ThongTin(Id),
        MaNhanVien     VARCHAR(20)    NOT NULL UNIQUE,
        IdVaiTro       INT            NULL,  -- FK -> NhanSu.VaiTro (thêm sau)
        IdKhuVuc       INT            NULL,  -- FK -> DanhMuc.KhuVuc (thêm sau)
        IdNguoiQuanLy  INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
        ChucVu         NVARCHAR(50)   NULL,
        GioiTinh       NVARCHAR(10)   NULL,
        NgaySinh       DATE           NULL,
        LoaiKhoi       VARCHAR(20)    NULL DEFAULT 'VanPhong',
        LoaiHopDong    VARCHAR(20)    NULL DEFAULT 'FullTime',
        NhomCongViec   VARCHAR(20)    NULL DEFAULT 'ThuongNgay',
        LuongCoBan     DECIMAL(15,0)  NULL,
        LuongTheoGio   DECIMAL(10,0)  NULL,
        TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'DangLam',

        CONSTRAINT CHK_NV_TrangThai CHECK (HeThong.fn_TuDienHopLe('NV_TRANG_THAI', TrangThai) = 1),
        CONSTRAINT CHK_NV_LoaiKhoi CHECK (LoaiKhoi IS NULL OR HeThong.fn_TuDienHopLe('NV_LOAI_KHOI', LoaiKhoi) = 1),
        CONSTRAINT CHK_NV_LoaiHopDong CHECK (LoaiHopDong IS NULL OR HeThong.fn_TuDienHopLe('NV_LOAI_HOP_DONG', LoaiHopDong) = 1),
        CONSTRAINT CHK_NV_NhomCV CHECK (NhomCongViec IS NULL OR HeThong.fn_TuDienHopLe('NV_NHOM_CONG_VIEC', NhomCongViec) = 1)
    );
    GO

    -- 2.3 Bảng Khách Hàng (Dùng lại ID của DoiTac)
    CREATE TABLE DoiTac.KhachHang (
        IdDoiTac       INT            PRIMARY KEY REFERENCES DoiTac.ThongTin(Id),
        MaKhachHang    VARCHAR(20)    NOT NULL UNIQUE,
        LoaiKhach      VARCHAR(20)    NULL DEFAULT 'CaNhan',
        HangThanhVien  VARCHAR(20)    NULL,
        IdDoanKhach    INT            NULL REFERENCES DoiTac.ThongTin(Id)
    );
    GO

    -- 2.4 Bảng Nhà Cung Cấp (Dùng lại ID của DoiTac)
    CREATE TABLE DoiTac.NhaCungCap (
        IdDoiTac           INT            PRIMARY KEY REFERENCES DoiTac.ThongTin(Id),
        MaNhaCungCap       VARCHAR(20)    NOT NULL UNIQUE,
        MaSoThue           VARCHAR(20)    NULL,
        NguoiLienHe        NVARCHAR(100)  NULL,
        DieuKhoanThanhToan NVARCHAR(200)  NULL
    );
    GO

    -- 2.5 Bảng Vận Động Viên (Đua chó đua ngựa)
    CREATE TABLE DoiTac.VanDongVien (
        IdDoiTac          INT            PRIMARY KEY REFERENCES DoiTac.ThongTin(Id),
        MaVanDongVien     VARCHAR(20)    NOT NULL UNIQUE,
        LoaiVanDongVien   VARCHAR(20)    NULL
    );
    GO

    -- 2.6 Bảng Danh sách Tài khoản đăng nhập phần mềm
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


    -- PHẦN 3: DANH MỤC (Lưu các thông tin cấu hình làm nền tảng)
    -- Bảng giá, Khu vực, Sản phẩm...


    -- 3.3 Bảng Khu vực hoạt động
    CREATE TABLE DanhMuc.KhuVuc (
        Id           INT IDENTITY(1,1) PRIMARY KEY,
        MaKhuVuc     VARCHAR(20)    NOT NULL UNIQUE,
        TenKhuVuc    NVARCHAR(100)  NOT NULL,
        LoaiKhuVuc   VARCHAR(30)    NOT NULL,
        SucChua      INT            NULL,
        TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'HoatDong',
    --   ToaDoX       DECIMAL(9,6)   NULL,
    --  ToaDoY       DECIMAL(9,6)   NULL,
        MoTa         NVARCHAR(500)  NULL,
        HinhAnh      VARCHAR(255)   NULL,
        DaXoa        BIT            NOT NULL DEFAULT 0
    );
    GO

    -- 3.4 Bảng Khu vực Biển tắm
    CREATE TABLE DanhMuc.KhuVucBien (
        IdKhuVuc       INT            PRIMARY KEY REFERENCES DanhMuc.KhuVuc(Id),
        DoSauToiDa     DECIMAL(5,2)   NULL,
        YeuCauPhao     BIT            NOT NULL DEFAULT 0,
        ChoPhepBoi     BIT            NOT NULL DEFAULT 1
    );
    GO

    -- 3.5 Bảng Khu vực Sở Thú
    CREATE TABLE DanhMuc.KhuVucThu (
        IdKhuVuc           INT            PRIMARY KEY REFERENCES DanhMuc.KhuVuc(Id),
        DienTichHectare    DECIMAL(8,2)   NULL,
        LoaiDongVatChinh   NVARCHAR(100)  NULL
    );
    GO

    -- 3.6 Bảng Nhà Hàng
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

    -- 3.1 Bảng Đơn vị tính (Cái, chiếc, ly...)
    CREATE TABLE DanhMuc.DonViTinh (
        Id           INT IDENTITY(1,1) PRIMARY KEY,
        MaDonVi      VARCHAR(20)    NOT NULL UNIQUE,
        TenDonVi     NVARCHAR(50)   NOT NULL,
        ConHoatDong  BIT            NOT NULL DEFAULT 1
    );
    GO

    -- 3.16 Bảng Sản Phẩm chung (Mọi thứ bán được: Vé, đồ ăn, thuê chòi, đặt phòng... đều là "sản phẩm")
    -- LoaiSanPham quyết định màn hình POS nào hiện sản phẩm này (Ve, AnUong, ChoThue, LuuTru...)
    CREATE TABLE DanhMuc.SanPham (
        Id             INT IDENTITY(1,1) PRIMARY KEY,
        MaSanPham      VARCHAR(20)    NOT NULL UNIQUE,
        TenSanPham     NVARCHAR(150)  NOT NULL,
        LoaiSanPham    VARCHAR(30)    NOT NULL,  -- Giá trị hợp lệ: xem NhómMã 'SP_LOAI' trong HeThong.TuDien
        IdDonViTinh    INT            NOT NULL REFERENCES DanhMuc.DonViTinh(Id),
        DonGia         DECIMAL(15,0)  NULL,      -- Giá nhanh (không thay đổi theo mùa). Giá chuẩn lấy từ BangGia.
        AnhDaiDien     VARCHAR(255)   NULL,
        LaVatTu        BIT            NOT NULL DEFAULT 0,  -- Có lưu kho không? Vé = không, Nước suối = có
        CanQuanLyLo    BIT            NOT NULL DEFAULT 0,  -- Có cần theo dõi hạn sử dụng (lô hàng) không?
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

    -- 3.2 Bảng Quy đổi đơn vị theo Sản phẩm
    -- VD: Bia Tiger Lon→Lốc (6 lon, giá 140k), Lon→Thùng (24 lon, giá 500k)
    -- GiaBan là giá RIÊNG của đơn vị đích, KHÔNG phải đơn giá gốc × hệ số
    -- TyLeQuyDoi dùng để trừ kho: bán 1 thùng → trừ kho 24 đơn vị gốc
    CREATE TABLE DanhMuc.QuyDoiDonVi (
        Id           INT IDENTITY(1,1) PRIMARY KEY,
        IdSanPham    INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
        IdDonViGoc   INT            NOT NULL REFERENCES DanhMuc.DonViTinh(Id),
        IdDonViDich  INT            NOT NULL REFERENCES DanhMuc.DonViTinh(Id),
        TyLeQuyDoi   DECIMAL(10,4)  NOT NULL,    -- 6 = lốc 6 lon, 24 = thùng 24 lon
        GiaBan       DECIMAL(15,0)  NULL,         -- Giá bán riêng (NULL = tự tính đơn giá × hệ số)
        ConHoatDong  BIT            NOT NULL DEFAULT 1,
        CONSTRAINT UQ_QuyDoi UNIQUE (IdSanPham, IdDonViGoc, IdDonViDich)
    );
    GO

    -- 3.17 Bảng Cấu Hình Vé (Thông tin gắn thêm nếu sản phẩm là Vé)
    -- Ghi chú: Một loại vé có thể dùng để đi qua nhiều cổng khác nhau
    CREATE TABLE DanhMuc.SanPham_Ve (
        IdSanPham    INT            PRIMARY KEY REFERENCES DanhMuc.SanPham(Id),
        LoaiVe       VARCHAR(30)    NOT NULL DEFAULT 'VeLe',  -- VeLe, VeCombo, VeMuaVu
        DoiTuongVe   VARCHAR(20)    NOT NULL DEFAULT 'NguoiLon',  -- Phân loại giá vé: NguoiLon / TreEm(1m-1m4) / MienPhi(duoi 1m) / NguoiCaoTuoi
        CanTaoToken  BIT            NOT NULL DEFAULT 0
    );
    GO

    -- 3.18 Bảng Món Ăn (Thông tin gắn thêm nếu sản phẩm là Đồ Ăn)
    CREATE TABLE DanhMuc.MonAn (
        IdSanPham    INT            PRIMARY KEY REFERENCES DanhMuc.SanPham(Id),
        IdNhaHang    INT            NOT NULL REFERENCES DanhMuc.NhaHang(Id),
        PhanLoai     VARCHAR(30)    NULL,
        MoTaNgan     NVARCHAR(200)  NULL,
        CoDiUng      BIT            NOT NULL DEFAULT 0,
        AnHienMenu   BIT            NOT NULL DEFAULT 1
    );
    GO
  
    -- 3.18b Bảng Định Mức Nguyên Liệu 
    -- Làm sao phần mềm tự biết bán 1 ly cafe sữa thì trong kho bị trừ bao nhiêu cafe, bao nhiêu sữa? Nhỡ đầu bếp làm hao hụt thì sao?
    --  Nhờ bảng Định Mức Nguyên Liệu này. Nó cho phép "khai báo trước công thức". 
    -- Ví dụ: Khi bán Thành Phẩm "1 Ly Cafe Sữa" -> Code sẽ tự động nhìn vào bảng này và chạy lệnh xuất kho đi "20g Cafe" + "30ml Sữa đặc". Đầu bếp làm hao hụt thì cuối ngày Kế toán sẽ làm phiếu "Kiểm kê" ở bảng Kho để trừ phần hao hụt.
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

    -- 3.7 Bảng Loại Phòng (Khách sạn)
    CREATE TABLE DanhMuc.LoaiPhong (
        IdSanPham     INT            PRIMARY KEY REFERENCES DanhMuc.SanPham(Id), -- Kế thừa 1-1 từ Sản Phẩm (Shared PK)
        TenLoai       NVARCHAR(100)  NOT NULL,
        MoTa          NVARCHAR(500)  NULL,
        SoNguoiToiDa  INT            NULL,
        SoTreEmToiDa  INT            NULL,
        DienTich      DECIMAL(18,2)  NULL,
        TienNghi      NVARCHAR(MAX)  NULL,
        ConHoatDong   BIT            NOT NULL DEFAULT 1
    );
    GO

    -- 3.8 Bảng Phòng Khách sạn
    CREATE TABLE DanhMuc.Phong (
        Id           INT IDENTITY(1,1) PRIMARY KEY,
        MaPhong      VARCHAR(20)    NOT NULL UNIQUE,
        IdLoaiPhong  INT            NOT NULL REFERENCES DanhMuc.LoaiPhong(IdSanPham),
        IdKhuVuc     INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
        Tang         INT            NULL,
        DienTich     DECIMAL(8,2)   NULL,
        TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'Trong',
        RowVersion   ROWVERSION     NOT NULL
    );
    GO

    -- 3.9 Bảng Bàn Ăn
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

    -- 3.10 Bảng Động Vật
    CREATE TABLE DanhMuc.DongVat (
        Id               INT IDENTITY(1,1) PRIMARY KEY,
        TenDongVat       NVARCHAR(100)  NOT NULL,
        Loai             NVARCHAR(50)   NULL,
        IdChuongTrai     INT            NULL,
        GioiTinh         NVARCHAR(10)   NULL,
        NamSinh          INT            NULL,
        IdCha            INT            NULL REFERENCES DanhMuc.DongVat(Id),
        IdMe             INT            NULL REFERENCES DanhMuc.DongVat(Id),
        TrangThai        VARCHAR(20)    NOT NULL DEFAULT 'KhoeManh',
        MoTa             NVARCHAR(500)  NULL,
        HinhAnh          VARCHAR(255)   NULL,
        NguonGoc         VARCHAR(20)    NULL,
        SoGiayPhepCITES  VARCHAR(50)    NULL,
        NoiXuatXu        NVARCHAR(100)  NULL,
        NgayNhap         DATE           NULL,
        SoHieuDanh       VARCHAR(30)    NULL,
        MauSacDacDiem    NVARCHAR(200)  NULL,
        CanNangKhiBat    DECIMAL(10,2)  NULL,
        DaXoa            BIT            NOT NULL DEFAULT 0
    );
    GO

    -- 3.11 Bảng Chuồng Trại
    CREATE TABLE DanhMuc.ChuongTrai (
        Id           INT IDENTITY(1,1) PRIMARY KEY,
        MaChuong     VARCHAR(20)    NOT NULL UNIQUE,
        TenChuong    NVARCHAR(100)  NOT NULL,
        IdKhuVuc     INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
        SucChua      INT            NULL,
        TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'HoatDong',
        LoaiChuong   VARCHAR(30)    NULL,
        DienTich     DECIMAL(10,2)  NULL,
        NhietDoMin   DECIMAL(4,1)   NULL,
        NhietDoMax   DECIMAL(4,1)   NULL,
        SoNguoiTrong INT            NULL,
        GhiChu       NVARCHAR(300)  NULL
    );
    GO

    ALTER TABLE DanhMuc.DongVat ADD CONSTRAINT FK_DongVat_ChuongTrai
        FOREIGN KEY (IdChuongTrai) REFERENCES DanhMuc.ChuongTrai(Id);
    GO

    -- 3.12 Bảng Tài Sản Cho Thuê Đo Đếm Được (Bảng Gốc - Table per Type)
    -- Nhân viên quét barcode → IdSanPham cho biết giá bao nhiêu, BangGia_ThueTheoGio cho biết cọc bao nhiêu.
    CREATE TABLE DanhMuc.TaiSanChoThue (
        Id               INT IDENTITY(1,1) PRIMARY KEY,
        TenTaiSan        NVARCHAR(150)  NOT NULL,
        IdSanPham        INT            NULL,  -- FK thêm sau. Liên kết tài sản vật lý → sản phẩm (để lấy giá)
        MaVachThietBi    VARCHAR(50)    NULL UNIQUE,  -- Dán trên đồ, quét bằng máy
        TrangThai        VARCHAR(20)    NOT NULL DEFAULT 'SanSang',
        DaXoa            BIT            NOT NULL DEFAULT 0,
        RowVersion       ROWVERSION     NOT NULL
    );
    GO

    -- 3.13 Bảng Tủ Đồ (Kế thừa TaiSanChoThue)
    CREATE TABLE DanhMuc.TuDo (
        IdTaiSan     INT PRIMARY KEY REFERENCES DanhMuc.TaiSanChoThue(Id),
        IdKhuVuc     INT            NULL REFERENCES DanhMuc.KhuVuc(Id)
    );
    GO

    -- 3.14 Bảng Chòi Nghỉ Mát (Kế thừa TaiSanChoThue)
    CREATE TABLE DanhMuc.ChoiNghiMat (
        IdTaiSan     INT PRIMARY KEY REFERENCES DanhMuc.TaiSanChoThue(Id),
        IdKhuVuc     INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
        SucChua      INT            NULL
    );
    GO

    -- 3.14b Bảng Phương Tiện Di Chuyển (Kế thừa TaiSanChoThue)
    CREATE TABLE DanhMuc.PhuongTienDiChuyen (
        IdTaiSan         INT PRIMARY KEY REFERENCES DanhMuc.TaiSanChoThue(Id),
        BienSo           VARCHAR(50)    NULL,
        SoGhe            INT            NOT NULL DEFAULT 1,
        LoaiXe           VARCHAR(50)    NULL, -- XeDap, XeDien, XeLua
        IdKhuVucHienTai  INT            NULL REFERENCES DanhMuc.KhuVuc(Id)
    );
    GO

    -- 3.15 Bảng Thiết Bị (Ví dụ: Máy tạo sóng, cửa từ...)
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

    -- 3.16 Bảng Trò Chơi
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
    -- 3.19 Bảng Cấu Hình Thuế (VAT...)
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

    -- 3.20 Bảng Giá (Lưu lại lịch sử thay đổi giá bán theo thời gian)
    CREATE TABLE DanhMuc.BangGia (
        Id           INT IDENTITY(1,1) PRIMARY KEY,
        IdSanPham    INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
        GiaBan       DECIMAL(15,0)  NOT NULL,
        LoaiGia      VARCHAR(20)    NOT NULL DEFAULT 'MacDinh',
        IdCauHinhNgayLe INT         NULL,  -- FK thêm sau (CauHinhNgayLe chưa tạo)
        HieuLucTu    DATETIME       NOT NULL,
        HieuLucDen   DATETIME       NOT NULL DEFAULT '9999-12-31',
        UuTien       INT            NOT NULL DEFAULT 0,
        TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'HoatDong',
        NguoiTao     INT            NULL,
        NgayTao      DATETIME       NOT NULL DEFAULT GETDATE()
    );
    GO

    CREATE INDEX IX_BangGia_TimGia ON DanhMuc.BangGia(IdSanPham, HieuLucTu, HieuLucDen)
    WHERE TrangThai = 'HoatDong';
    GO

    -- 3.20b Bảng Giá Thuê Theo Giờ (Tủ đồ, Chòi, Phòng, Ván lướt...)
    -- Tách riêng khỏi BangGia vì Vé và Đồ ăn không có khái niệm "block giờ".
    -- Ví dụ thuê chòi: Block đầu = 120 phút giá 200k. Sau đó mỗi 30 phút thêm = 50k.
    CREATE TABLE DanhMuc.BangGia_ThueTheoGio (
        IdBangGia    INT            PRIMARY KEY REFERENCES DanhMuc.BangGia(Id),  -- Kế thừa từ BangGia (cùng Id)
        PhutBlock    INT            NULL,       -- Block đầu bao nhiêu phút (VD: 120)
        PhutTiep     INT            NULL,       -- Sau block đầu, cẩ bao nhiêu phút tính thêm 1 lần (VD: 30)
        GiaPhuThu    DECIMAL(15,0)  NULL,       -- Mỗi lần vượt thêm = bao nhiêu tiền (VD: 50k)
        TienCoc      DECIMAL(15,0)  NULL        -- Tiền cọc yêu cầu khi mượn đồ
    );
    GO

    -- 3.21 Bảng Combo (Gộp nhiều sản phẩm lại bán chung)
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
        SoLuong      DECIMAL(10,2)  NOT NULL DEFAULT 1,
        TyLePhanBo   DECIMAL(5,2)   NOT NULL DEFAULT 0   
    );
    GO

    -- 3.22 Bảng Ngày Lễ (Lịch ngày lễ dùng cho: tính lương OT nhân sự + báo cáo doanh thu theo ngày lễ)
    -- Giá ngày lễ được tạo riêng trong BangGia với LoaiGia='NgayLe' + UuTien cao
    CREATE TABLE DanhMuc.CauHinhNgayLe (
        Id           INT IDENTITY(1,1) PRIMARY KEY,
        TenNgayLe    NVARCHAR(100)  NOT NULL,
        NgayBatDau   DATE           NOT NULL,
        NgayKetThuc  DATE           NOT NULL,
        Nam          INT            NOT NULL,
        CONSTRAINT UQ_NgayLe UNIQUE (TenNgayLe, Nam),
        CONSTRAINT CHK_NgayLe CHECK (NgayKetThuc >= NgayBatDau)
    );
    GO

    ALTER TABLE DoiTac.NhanVien ADD CONSTRAINT FK_NhanVien_KhuVuc
        FOREIGN KEY (IdKhuVuc) REFERENCES DanhMuc.KhuVuc(Id);
    GO

    -- FK bổ sung cho TaiSanChoThue -> SanPham (tạo sau vì SanPham khai báo sau TaiSanChoThue)
    ALTER TABLE DanhMuc.TaiSanChoThue ADD CONSTRAINT FK_TaiSanChoThue_SanPham
        FOREIGN KEY (IdSanPham) REFERENCES DanhMuc.SanPham(Id);
    GO

    -- FK bổ sung cho BangGia -> CauHinhNgayLe (tạo sau vì CauHinhNgayLe khai báo sau BangGia)
    ALTER TABLE DanhMuc.BangGia ADD CONSTRAINT FK_BangGia_CauHinhNgayLe
        FOREIGN KEY (IdCauHinhNgayLe) REFERENCES DanhMuc.CauHinhNgayLe(Id);
    GO


    -- PHẦN 4: QUẢN LÝ KHO HÀNG
    -- Áp dụng nguyên lý kế toán: Xuất kho này thì bắt buộc phải nhập vô kho kia. Không để hàng tự nhiên biến mất.

    -- 4.1 Bảng Danh sách các Kho (bao gồm Cửa hàng, Kho trung tâm, Kho bỏ đi...)
    CREATE TABLE Kho.KhoHang (
        Id           INT IDENTITY(1,1) PRIMARY KEY,
        MaKho        VARCHAR(20)    NOT NULL UNIQUE,
        TenKho       NVARCHAR(100)  NOT NULL,
        LaKhoAo      BIT            NOT NULL DEFAULT 0,
        ChoPhepTonAm BIT            NOT NULL DEFAULT 0,  --  Kho bếp cho phép tồn âm
        IdKhuVuc     INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
        TrangThai    VARCHAR(20)    NOT NULL DEFAULT 'HoatDong'
    );
    GO

    -- 4.2 Bảng Lô Hàng (Để theo dõi Hạn Sử Dụng của đồ ăn nhà hàng/siêu thị)
    CREATE TABLE Kho.LoHang (
        Id             INT IDENTITY(1,1) PRIMARY KEY,
        MaLoHang       VARCHAR(30)    NOT NULL UNIQUE,
        IdSanPham      INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
        IdNhaCungCap   INT            NULL,  -- FK -> DoiTac.NhaCungCap (thêm sau)
        NgaySanXuat    DATE           NULL,
        NgayHetHan     DATE           NULL,
        SoLuongNhap    DECIMAL(18,3)  NOT NULL,
        IdChungTuNhap  INT            NULL,  -- FK -> Kho.ChungTu (thêm sau)
        TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'ConHang',
        GhiChu         NVARCHAR(200)  NULL,
        NgayTao        DATETIME       NOT NULL DEFAULT GETDATE()
    );
    GO

    -- 4.2b Bảng Cảnh báo Tồn Kho Tối Thiểu (Auto-reorder)
    CREATE TABLE Kho.MucTonToiThieu (
        Id           INT IDENTITY(1,1) PRIMARY KEY,
        IdSanPham    INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
        IdKho        INT            NOT NULL REFERENCES Kho.KhoHang(Id),
        MucCanhBao   DECIMAL(18,3)  NOT NULL,
        SoLuongDatHang DECIMAL(18,3) NULL,
        TrangThai    BIT            NOT NULL DEFAULT 1,
        CONSTRAINT UQ_MucTon UNIQUE (IdSanPham, IdKho)
    );
    GO

    -- ============================================================
    -- KHO CHỨNG TỪ V3 — Thiết kế lại sạch
    -- Thay thế hoàn toàn 3 bảng cũ:
    --   Kho.ChungTu        → Bỏ IdKhoXuat / IdKhoNhap ở header
    --   Kho.ChiTietChungTu → Thêm IdKhoXuat / IdKhoNhap NOT NULL, tách kiểm kê riêng
    --   Kho.SoCai          → DROP (thay bằng index trên ChiTietChungTu)
    -- ============================================================

    -- 4.3 Phiếu Kho (Header)
    -- Chỉ lưu thông tin chung: ai tạo, ngày, lý do, trạng thái.
    -- Không có kho xuất/nhập ở đây — mỗi dòng chi tiết tự mang kho riêng,
    -- cho phép 1 phiếu nhập từ NCC phân phối thẳng vào nhiều kho đích.
    CREATE TABLE Kho.ChungTu (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        MaChungTu       VARCHAR(30)     NOT NULL UNIQUE,
        LoaiChungTu     VARCHAR(20)     NOT NULL,
        IdDoiTac        INT             NULL REFERENCES DoiTac.ThongTin(Id),
        IdDonHang       INT             NULL,   -- FK -> BanHang.DonHang (thêm sau)
        IdBaoTri        INT             NULL,   -- FK -> VanHanh.BaoTri   (thêm sau)
        IdChungTuGoc    INT             NULL,   -- Self-ref: trả NCC truy nguồn phiếu nhập gốc
        NgayChungTu     DATETIME        NOT NULL DEFAULT GETDATE(),
        LyDo            NVARCHAR(300)   NULL,
        TrangThai       VARCHAR(20)     NOT NULL DEFAULT 'Moi',
        IdNguoiTao      INT             NOT NULL,
        IdNguoiDuyet1   INT             NULL,
        NgayDuyet1      DATETIME        NULL,
        IdNguoiDuyet2   INT             NULL,
        NgayDuyet2      DATETIME        NULL,
        GhiChu          NVARCHAR(500)   NULL,
        NgayTao         DATETIME        NOT NULL DEFAULT GETDATE(),
        CONSTRAINT CHK_CT_LoaiChungTu CHECK (HeThong.fn_TuDienHopLe('LOAI_CHUNG_TU_KHO', LoaiChungTu) = 1)
    );
    GO

    ALTER TABLE Kho.ChungTu ADD CONSTRAINT FK_ChungTu_ChungTuGoc
        FOREIGN KEY (IdChungTuGoc) REFERENCES Kho.ChungTu(Id);
    GO

    -- 4.4 Chi Tiết Phiếu Kho
    -- Mỗi dòng = 1 mặt hàng di chuyển giữa 2 kho cụ thể.
    -- IdKhoXuat + IdKhoNhap bắt buộc NOT NULL — không có hàng nào
    -- được phép xuất mà không có nơi nhập (kể cả hàng hủy → KHO_HUY,
    -- hàng bán → KHO_KHACH, nhập từ NCC → từ KHO_NCC).
    CREATE TABLE Kho.ChiTietChungTu (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        IdChungTu       INT             NOT NULL REFERENCES Kho.ChungTu(Id),
        IdSanPham       INT             NOT NULL REFERENCES DanhMuc.SanPham(Id),
        IdLoHang        INT             NULL     REFERENCES Kho.LoHang(Id),
        IdKhoXuat       INT             NOT NULL REFERENCES Kho.KhoHang(Id),
        IdKhoNhap       INT             NOT NULL REFERENCES Kho.KhoHang(Id),
        SoLuong         DECIMAL(18,3)   NOT NULL,
        DonGia          DECIMAL(15,0)   NULL,
        ThanhTien       AS (SoLuong * ISNULL(DonGia, 0)) PERSISTED,
        GhiChu          NVARCHAR(200)   NULL,
        CONSTRAINT CHK_CTCT_SoLuong    CHECK (SoLuong > 0),
        CONSTRAINT CHK_CTCT_KhacKho    CHECK (IdKhoXuat != IdKhoNhap)
    );
    GO

    -- Index thay thế SoCai — đủ nhanh cho GetTonKhoHienTai và báo cáo tồn kho
    CREATE INDEX IX_CTCT_TonKhoNhap
        ON Kho.ChiTietChungTu (IdSanPham, IdKhoNhap)
        INCLUDE (SoLuong, IdChungTu);

    CREATE INDEX IX_CTCT_TonKhoXuat
        ON Kho.ChiTietChungTu (IdSanPham, IdKhoXuat)
        INCLUDE (SoLuong, IdChungTu);

    CREATE INDEX IX_CTCT_ChungTu
        ON Kho.ChiTietChungTu (IdChungTu);

    -- Index trên ChungTu.TrangThai để JOIN nhanh khi filter DaDuyet
    CREATE INDEX IX_CT_TrangThai
        ON Kho.ChungTu (TrangThai)
        INCLUDE (Id);
    GO

    -- 4.4b Chi Tiết Kiểm Kê
    -- Tách riêng vì kiểm kê không di chuyển hàng —
    -- chỉ đếm đối chiếu tồn thực tế vs hệ thống cho 1 kho.
    -- Sau khi phiếu kiểm kê được DUYỆT, code C# sẽ tự sinh
    -- ChiTietChungTu điều chỉnh chênh lệch vào KHO_CHENH_LECH.
    CREATE TABLE Kho.ChiTietKiemKe (
        Id              INT IDENTITY(1,1) PRIMARY KEY,
        IdChungTu       INT             NOT NULL REFERENCES Kho.ChungTu(Id),
        IdKho           INT             NOT NULL REFERENCES Kho.KhoHang(Id),
        IdSanPham       INT             NOT NULL REFERENCES DanhMuc.SanPham(Id),
        IdLoHang        INT             NULL     REFERENCES Kho.LoHang(Id),
        SoLuongHeThong  DECIMAL(18,3)   NOT NULL,
        SoLuongThucTe   DECIMAL(18,3)   NOT NULL,
        ChenhLech       AS (SoLuongThucTe - SoLuongHeThong) PERSISTED,
        LyDoChenhLech   NVARCHAR(200)   NULL,
        GhiChu          NVARCHAR(200)   NULL,
        CONSTRAINT CHK_CTKK_SoLuong    CHECK (SoLuongHeThong >= 0 AND SoLuongThucTe >= 0)
    );
    GO

    CREATE INDEX IX_CTKK_ChungTu ON Kho.ChiTietKiemKe(IdChungTu);
    GO

    -- 4.6 Bảng Tạm Giữ Hàng (Giữ hàng khi khách đang đặt Online hoặc chưa chốt hóa đơn)
    CREATE TABLE Kho.TamGiuTonKho (
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

    -- FK bổ sung cho LoHang --
    ALTER TABLE Kho.LoHang ADD CONSTRAINT FK_LoHang_NCC
        FOREIGN KEY (IdNhaCungCap) REFERENCES DoiTac.NhaCungCap(IdDoiTac);
    ALTER TABLE Kho.LoHang ADD CONSTRAINT FK_LoHang_ChungTu
        FOREIGN KEY (IdChungTuNhap) REFERENCES Kho.ChungTu(Id);
    GO


    -- PHẦN 5: QUẢN LÝ TÀI CHÍNH VÀ VÍ ĐIỆN TỬ RFID

    -- 5.1 Bảng Phiếu Tài Chính (Dùng chung cho cả Thu tiền, Chi tiền, Nạp tiền ví)
    CREATE TABLE TaiChinh.ChungTuTC (
        Id             INT IDENTITY(1,1) PRIMARY KEY,
        MaChungTu      VARCHAR(30)    NOT NULL UNIQUE,
        LoaiChungTu    VARCHAR(20)    NOT NULL,
        IdDoiTac       INT            NULL REFERENCES DoiTac.ThongTin(Id),
        IdDonHang      INT            NULL,  -- FK -> BanHang.DonHang (thêm sau)
        IdViDienTu     INT            NULL,  -- FK -> TaiChinh.ViDienTu (thêm sau)
        IdPhienThuNgan INT            NULL,  -- FK -> BanHang.PhienThuNgan (thêm sau)
        MaGiaoDichClient UNIQUEIDENTIFIER NULL,
        SoTien         DECIMAL(18,0)  NOT NULL,
        PhuongThuc     VARCHAR(20)    NOT NULL DEFAULT 'TienMat',
        NgayChungTu    DATETIME       NOT NULL DEFAULT GETDATE(),
        MoTa           NVARCHAR(300)  NULL,
        TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'DaDuyet',  -- ChoDuyet / DaDuyet / DaHuy (phiếu POS auto DaDuyet, phiếu chi lớn cần duyệt)
        IdNguoiTao     INT            NOT NULL,
        NgayTao        DATETIME       NOT NULL DEFAULT GETDATE()
    );
    GO

    -- 5.2 Bảng Ví Điện Tử
    CREATE TABLE TaiChinh.ViDienTu (
        Id             INT IDENTITY(1,1) PRIMARY KEY,
        IdKhachHang    INT            NOT NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
        MaVi           VARCHAR(20)    NOT NULL UNIQUE,
        ConHoatDong    BIT            NOT NULL DEFAULT 1,
        RowVersion     ROWVERSION     NOT NULL,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- 5.3 Bảng Sổ Cái Ví (Lưu lịch sử tiền nạp/trừ trong Ví giống hệt sao kê Ngân hàng)
CREATE TABLE TaiChinh.SoCaiVi (
    Id             BIGINT IDENTITY(1,1) PRIMARY KEY,
    IdVi           INT            NOT NULL REFERENCES TaiChinh.ViDienTu(Id),
    LoaiPhep       VARCHAR(10)    NOT NULL,
    SoTien         DECIMAL(15,0)  NOT NULL CHECK (SoTien > 0),
    IdChungTu      INT            NULL REFERENCES TaiChinh.ChungTuTC(Id),
    MoTa           VARCHAR(50)    NULL,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE(),
    NguoiTao       INT            NOT NULL
);
GO

-- 5.4 Bảng Thẻ RFID (Chiếc vòng đeo tay để khách chạm vào quầy thanh toán)
CREATE TABLE TaiChinh.TheRFID (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    MaThe          VARCHAR(50)    NOT NULL UNIQUE,
    IdKhachHang    INT            NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    IdViDienTu     INT            NULL REFERENCES TaiChinh.ViDienTu(Id),
    IdDatPhong     INT            NULL,  -- * FK -> ChiTietDatPhong (thêm sau khi bảng tạo)
    IdKhuVucHienTai INT           NULL REFERENCES DanhMuc.KhuVuc(Id),
    TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'ChuaKichHoat',
    NgayKichHoat   DATETIME       NULL,
    NgayHetHan     DATETIME       NULL,
    TienCocThe     DECIMAL(15,0)  NOT NULL DEFAULT 0,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

ALTER TABLE TaiChinh.ChungTuTC
    ADD CONSTRAINT FK_ChungTuTC_ViDienTu FOREIGN KEY (IdViDienTu) REFERENCES TaiChinh.ViDienTu(Id);
GO

-- 5.2b Bảng Chi Tiết Thanh Toán (Hỗ trợ khách trả nhiều phương thức cho 1 bill)
-- Ví dụ: Khách mua vé 500k, trả 200k tiền mặt + 300k QR
CREATE TABLE TaiChinh.ChiTietThanhToan (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    IdChungTu    INT            NOT NULL REFERENCES TaiChinh.ChungTuTC(Id),
    PhuongThuc   VARCHAR(20)    NOT NULL,  -- TienMat, ChuyenKhoan, ViRFID, QR...
    SoTien       DECIMAL(18,0)  NOT NULL CHECK (SoTien > 0),
    GhiChu       NVARCHAR(200)  NULL
);
GO

-- 5.5 Bảng Kịch bản Điểm Thưởng
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

-- 5.6 Bảng Lịch sử cộng/trừ Điểm thưởng của khách
CREATE TABLE TaiChinh.LichSuDiem (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdKhachHang    INT            NOT NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    LoaiGiaoDich   VARCHAR(20)    NOT NULL,
    SoDiem         INT            NOT NULL,
    SoDuSauGD      INT            NOT NULL,
    IdDonHang      INT            NULL, -- FK to BanHang.DonHang added later
    MoTa           NVARCHAR(200)  NULL,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO



-- PHẦN 6: QUẢN LÝ BÁN HÀNG VÀ DỊCH VỤ (POS, Vé, Khách sạn...)

-- 6.1 Bảng Phiên Thu Ngân (Chốt ca kíp mở két/đóng két của nhân viên)
CREATE TABLE BanHang.PhienThuNgan (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien     INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdMayBan       VARCHAR(20)    NOT NULL,
    ThoiGianMo     DATETIME       NOT NULL DEFAULT GETDATE(),
    ThoiGianDong   DATETIME       NULL,
    TienDauCa      DECIMAL(15,0)  NOT NULL DEFAULT 0,
    TienCuoiCa     DECIMAL(15,0)  NULL,
    TongThuTrongCa DECIMAL(15,0)  NULL,
    TongChiTrongCa DECIMAL(15,0)  NULL,
    ChenhLech      AS (TienCuoiCa - TienDauCa - (ISNULL(TongThuTrongCa,0) - ISNULL(TongChiTrongCa,0))) PERSISTED,
    GhiChu         NVARCHAR(300)  NULL,
    TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'DangMo',
    IdKhoBan       INT            NULL REFERENCES Kho.KhoHang(Id)  -- Kho xuất khi bán hàng (NULL = POS không cần kho)
);
GO

ALTER TABLE TaiChinh.ChungTuTC
    ADD CONSTRAINT FK_ChungTuTC_PhienThuNgan FOREIGN KEY (IdPhienThuNgan) REFERENCES BanHang.PhienThuNgan(Id);
GO

-- 6.2 Bảng Đơn Hàng (Chứa thông tin tổng tiền của bill)
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

-- Thêm FK cho LichSuDiem sau khi bảng DonHang đã được tạo
ALTER TABLE TaiChinh.LichSuDiem
    ADD CONSTRAINT FK_LichSuDiem_DonHang FOREIGN KEY (IdDonHang) REFERENCES BanHang.DonHang(Id);
GO

-- 6.3 Bảng Chi Tiết Đơn Hàng (Các dòng chi tiết trong 1 bill, ví dụ mua 3 chai nước)
-- Trong công viên, khách vừa Mua vé, vừa Mua xúc xích, vừa Thuê tủ đồ giữ đồ. Ba thứ chả liên quan gì nhau thì tính tiền gom vô Bill kiểu gì?
--  Tất cả mọi thứ (Vé, Đồ ăn, Tiền thuê tủ) quy hết thành chữ "Sản Phẩm" và quăng chung vào bảng ChiTietDonHang này để đẻ ra "Tổng Tiền".
-- Sinh Bill ra tiền xong xuôi, ba cái bảng râu ria kia (Thuê Đồ, Gate Vé) mới móc khóa ID kéo tới dòng này để chạy lệnh quét cổng, trừ phútt. Tiêu chí: Xong tiền cái đã, nghiệp vụ tính sau!
CREATE TABLE BanHang.ChiTietDonHang (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdDonHang       INT            NOT NULL REFERENCES BanHang.DonHang(Id),
    IdSanPham       INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    SoLuong         DECIMAL(10,2)  NOT NULL,
    DonGiaThucTe    DECIMAL(15,0)  NOT NULL,
    ThanhTien       AS (SoLuong * DonGiaThucTe) PERSISTED,
    IdBangGia       INT            NULL REFERENCES DanhMuc.BangGia(Id),  -- Lưu lại giá nào áp dụng để sau này có cãi nhau thì còn check
    IdLoHang        INT            NULL REFERENCES Kho.LoHang(Id),
    IdCauHinhThue   INT            NULL REFERENCES DanhMuc.CauHinhThue(Id),
    TienThue        DECIMAL(15,0)  NOT NULL DEFAULT 0,

    GhiChu          NVARCHAR(200)  NULL    -- GhiChu dùng chung cho mọi loại dòng (ghi chú bếp, ghi chú thuê, ghi chú đặt phòng...)
);
GO

-- 6.4 Bảng Vé Điện Tử (Mã vạch QR để đi liền với vé, lấy QR quét qua cửa)
CREATE TABLE BanHang.VeDienTu (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdChiTietDonHang INT           NOT NULL REFERENCES BanHang.ChiTietDonHang(Id),
    IdSanPham       INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    IdKhachHang     INT            NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    IdKhuVucHienTai INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    IdTheRFID       INT            NULL REFERENCES TaiChinh.TheRFID(Id),
    IdVeGoc         INT            NULL,  -- Self-ref: vé cấp lại link về vé gốc (FK thêm sau)
    MaVach          VARCHAR(100)   NOT NULL UNIQUE,
    SoLuotConLai    INT            NOT NULL DEFAULT 1,
    TrangThai       VARCHAR(20)    NOT NULL DEFAULT 'ChuaSuDung',
    ThoiGianQuet    DATETIME       NULL,
    NgayHetHan      DATE           NULL,
    LyDoHuy         NVARCHAR(200)  NULL,   -- Ghi lại lý do khi hủy vé
    IdNguoiDuyetHuy INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),  -- Ai phê duyệt hủy
    RowVersion      ROWVERSION     NOT NULL,  -- Chống xung đột khi 2 cổng quét cùng lúc
    NgayTao         DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- 6.5b Báo Giá B2B (Đoàn khách / Công ty yêu cầu báo giá trước khi đặt)
CREATE TABLE BanHang.BaoGia (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    MaBaoGia         VARCHAR(20)    NOT NULL UNIQUE,
    IdDoiTac         INT            NOT NULL REFERENCES DoiTac.ThongTin(Id),
    IdNhanVienTao    INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    SoNguoi          INT            NULL,          -- Số người trong đoàn
    NgayDenDuKien    DATE           NULL,          -- Đoàn dự kiến đến ngày nào
    NgayDiDuKien     DATE           NULL,          -- Đoàn dự kiến đi ngày nào
    TongTien         DECIMAL(18,0)  NOT NULL DEFAULT 0,
    HanHieuLuc       DATE           NOT NULL,
    TrangThai        VARCHAR(20)    NOT NULL DEFAULT 'MoiBaoGia',
    IdDonHang        INT            NULL,  -- Truy vết: Báo giá này đã sinh ra đơn hàng nào (FK thêm sau)
    GhiChu           NVARCHAR(500)  NULL,
    NgayTao          DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE BanHang.BaoGia_ChiTiet (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    IdBaoGia         INT            NOT NULL REFERENCES BanHang.BaoGia(Id),
    IdSanPham        INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    SoLuong          DECIMAL(10,2)  NOT NULL,
    DonGia           DECIMAL(15,0)  NOT NULL,
    ThanhTien        AS (SoLuong * DonGia) PERSISTED
);
GO

-- 6.5c Công Nợ (Đoàn/Công ty chơi xong không trả liền → ghi nợ → hẹn ngày trả)
-- DonHang chuyển sang trạng thái 'GhiNoCongTy' thì tạo 1 dòng ở đây để kế toán đòi tiền.
CREATE TABLE BanHang.CongNo (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    IdDonHang        INT            NOT NULL REFERENCES BanHang.DonHang(Id),
    IdDoiTac         INT            NOT NULL REFERENCES DoiTac.ThongTin(Id),
    SoTienNo         DECIMAL(18,0)  NOT NULL,
    SoTienDaTra      DECIMAL(18,0)  NOT NULL DEFAULT 0,
    NgayDenHan       DATE           NOT NULL,
    TrangThai        VARCHAR(20)    NOT NULL DEFAULT 'ChuaTra',  -- ChuaTra / DaTraMotPhan / DaTra / QuaHan
    GhiChu           NVARCHAR(300)  NULL,
    NgayTao          DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- 6.5 Bảng Khuyến Mãi (Danh sách các Mức/Mã giảm giá được tung ra)
-- Giải thích: Quản lý các chương trình giảm giá cơ bản. 
-- Ví dụ: Phát hành mã "TET2026", giảm 50k, hạn tới mùng 10 Tết.
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

    TrangThai       BIT            NOT NULL DEFAULT 1,
    DaXoa           BIT            NOT NULL DEFAULT 0,
    NguoiTao        INT            NULL,
    NgayTao         DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- 6.6 Bảng Điều Kiện Khuyến Mãi (Quy định ai/khi nào mới được xài mã)
-- Bảng này CỰC KỲ KHỦNG khi lập trình vì nó giải quyết được mọi nghiệp vụ phức tạp nhất:
-- Ví dụ câu hỏi: Có hỗ trợ khuyến mãi cho học sinh < 18 tuổi dịp Tết không? 
-- Trả lời: HOÀN TOÀN CÓ THỂ LÀM ĐƯỢC MÀ KHÔNG CẦN TẠO BẢNG MỚI.
-- Cách Database hoạt động:
--   - Tạo mã ở Mục 6.5 (Mã: TET2026, Hạn dùng: Dịp Tết)
--   - Dưới Bảng 6.6 này, thêm dòng điều kiện vào:
--         [LoaiDieuKien] = 'Độ Tuổi' | [GiaTriDieuKien] = '<18' 
--         (Code WinForms đọc điều kiện này sẽ yêu cầu KH trình căn cước/vé tháng)
CREATE TABLE BanHang.KhuyenMai_DieuKien (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdKhuyenMai     INT            NOT NULL REFERENCES BanHang.KhuyenMai(Id),
    LoaiDieuKien    VARCHAR(30)    NOT NULL,   -- Loại điều kiện (DoTuoi, HangThanhVien, LoaiKhach...)
    PhepSo          VARCHAR(5)     NOT NULL DEFAULT '=',  -- Phép so sánh: =, <, >, <=, >=, IN
    GiaTriDieuKien  VARCHAR(100)   NOT NULL   -- Giá trị cần so (VD: '18', 'Vang', 'T7,CN')
);
GO

-- 6.7 Bảng Lịch Sự Sử Dụng Khuyến Mãi (Để check xem ông khách này xài cái mã giảm giá này bao nhiêu lần rồi)
CREATE TABLE BanHang.KhuyenMai_LichSu (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdKhuyenMai     INT            NOT NULL REFERENCES BanHang.KhuyenMai(Id),
    IdDonHang       INT            NOT NULL REFERENCES BanHang.DonHang(Id),
    IdKhachHang     INT            NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    SoTienGiam      DECIMAL(15,0)  NOT NULL,
    ThoiGian        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- 6.8 Bảng Đặt Phòng (Header: ai đặt, ở khi nào, cọc bao nhiêu)
-- Thiết kế 2 cấp: PhieuDatPhong (header chung) → ChiTietDatPhong (từng phòng cụ thể)
-- Ví dụ: Đoàn đặt 1 lần (1 header) nhưng cần 10 phòng (10 chi tiết)
CREATE TABLE BanHang.PhieuDatPhong (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    MaDatPhong      VARCHAR(20)    NOT NULL UNIQUE,
    IdKhachHang     INT            NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    TenNguoiDat     NVARCHAR(100)  NULL,       -- Walk-in không có tài khoản KH, ghi tay
    SoDienThoai     VARCHAR(20)    NULL,
    NgayNhanPhong   DATE           NOT NULL,
    NgayTraPhong    DATE           NOT NULL,
    TienPhongTraTruoc DECIMAL(18,0) NOT NULL DEFAULT 0,  -- Tiền phòng thanh toán/cọc trước
    TienCocNapVi    DECIMAL(18,0)  NOT NULL DEFAULT 0,  -- Cọc riêng cho vòng tay RFID (trả khi check-out)
    TrangThai       VARCHAR(20)    NOT NULL DEFAULT 'DatTruoc',
    IdPhieuThuCoc   INT            NULL REFERENCES TaiChinh.ChungTuTC(Id),  -- Link tới phiếu thu cọc trong TaiChinh
    GhiChu          NVARCHAR(300)  NULL,
    NgayTao         DATETIME       NOT NULL DEFAULT GETDATE(),
    CONSTRAINT CHK_DatPhong_Ngay CHECK (NgayTraPhong > NgayNhanPhong)
);
GO

-- 6.8b Từng phòng cụ thể trong 1 lần đặt
-- Trick: Lúc đặt chỉ chọn LOẠI phòng (Standard/VIP), chưa biết số phòng.
--        Lúc check-in LỄ TÂN mới gán số phòng vật lý (101, 102...).
CREATE TABLE BanHang.ChiTietDatPhong (
    Id                 INT IDENTITY(1,1) PRIMARY KEY,
    IdPhieuDatPhong    INT            NOT NULL REFERENCES BanHang.PhieuDatPhong(Id),
    IdChiTietDonHang   INT            NOT NULL REFERENCES BanHang.ChiTietDonHang(Id),  -- Phải có bill trước mới đặt
    IdLoaiPhong        INT            NOT NULL REFERENCES DanhMuc.LoaiPhong(IdSanPham), -- Lúc đặt: chọn loại (5 phòng Standard)
    IdPhong            INT            NULL REFERENCES DanhMuc.Phong(Id), -- Lúc check-in: gán phòng 101, 102. NULL = chưa gán
    GiaBanDem          DECIMAL(15,0)  NOT NULL DEFAULT 0,
    TrangThai          VARCHAR(20)    NOT NULL DEFAULT 'ChoDen',
    NgayCheckIn        DATETIME       NULL,
    NgayCheckOut       DATETIME       NULL
);
GO

-- 6.8c Lịch Sử Đổi Phòng (Audit trail)
CREATE TABLE BanHang.LichSuDoiPhong (
    Id                 INT IDENTITY(1,1) PRIMARY KEY,
    IdChiTietDatPhong  INT            NOT NULL REFERENCES BanHang.ChiTietDatPhong(Id),
    IdPhongCu          INT            NOT NULL REFERENCES DanhMuc.Phong(Id),
    IdPhongMoi         INT            NOT NULL REFERENCES DanhMuc.Phong(Id),
    LyDoDoi            NVARCHAR(300)  NOT NULL,
    IdNhanVien         INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    ThoiGianDoi        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- 6.8d Ghi Chú Yêu Cầu Riêng Của Phòng (View hồ bơi, giường thấp, không hành...)
CREATE TABLE BanHang.GhiChuPhong (
    Id                 INT IDENTITY(1,1) PRIMARY KEY,
    IdChiTietDatPhong  INT            NOT NULL REFERENCES BanHang.ChiTietDatPhong(Id),
    NoiDungGhiChu      NVARCHAR(500)  NOT NULL,
    DaDapUng           BIT            NOT NULL DEFAULT 0,
    NgayTao            DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- 6.9 Bảng Yêu Cầu Đặt Bàn Ăn
CREATE TABLE BanHang.DatBan (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    MaDatBan        VARCHAR(20)    NOT NULL UNIQUE,
    IdKhachHang     INT            NULL REFERENCES DoiTac.KhachHang(IdDoiTac),
    TenNguoiDat     NVARCHAR(100)  NULL,
    SoDienThoai     VARCHAR(20)    NULL,
    NgayDat         DATE           NOT NULL,
    GioDat          TIME           NOT NULL,
    SoNguoi         INT            NOT NULL,
    IdNhaHang       INT            NULL REFERENCES DanhMuc.NhaHang(Id), -- Biết ăn ở nhà hàng nào trước khi gán bàn cụ thể
    TrangThai       VARCHAR(20)    NOT NULL DEFAULT 'ChoDen',
    IdDonHang       INT            NULL REFERENCES BanHang.DonHang(Id),  -- Link tới bill ăn uống khi khách đến
    IdPhieuThuCoc   INT            NULL REFERENCES TaiChinh.ChungTuTC(Id),
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

-- 6.9b Bảng Danh Sách Món Ăn Đặt Trước (Khách đoàn chốt Set Menu khi đặt bàn)
-- DatBan + ChiTietDatBan chỉ lưu "ai đặt bàn nào", nhà bếp không biết nấu món gì.
-- Bảng này lưu danh sách món ăn khách đã chốt trước khi đến.
CREATE TABLE BanHang.DatBan_MonAn (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdDatBan        INT            NOT NULL REFERENCES BanHang.DatBan(Id),
    IdSanPham       INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    SoLuong         INT            NOT NULL DEFAULT 1,
    DonGia          DECIMAL(15,0)  NULL,
    GhiChu          NVARCHAR(200)  NULL,
    CONSTRAINT UQ_DatBan_Mon UNIQUE (IdDatBan, IdSanPham)
);
GO

-- 6.10-6.12: BA BẢNG THUÊ RIÊNG cho 3 loại tài sản khác nhau
-- Tại sao không gộp 1 bảng? Vì mỗi loại có thuộc tính riêng:
--   ThueDoChiTiet: có TienCoc, PhiPhatSinh, link TaiSanChoThue (asset vật lý)
--   ThueTu: link TuDo (tủ cụ thể), không có cọc
--   ThueChoi: link ChoiNghiMat (chòi cụ thể), có sức chứa
-- Tất cả đều trỏ về ChiTietDonHang để gộp vào bill.
-- Phiên thu ngân lúc TRẢ ĐỒ (hoàn cọc). Phiên MỞ (lúc giao đồ) track qua DonHang.IdPhienThuNgan.
-- Giải quyết bài toán Giao Ca Chéo: Ca A thu cọc, Ca B hoàn cọc → rõ ràng ai chịu trách nhiệm.
CREATE TABLE BanHang.ThueDoChiTiet (
    Id                 INT IDENTITY(1,1) PRIMARY KEY,
    IdChiTietDonHang   INT            NOT NULL REFERENCES BanHang.ChiTietDonHang(Id),
    IdTaiSanChoThue    INT            NULL, -- Sẽ FK ở dưới
    SoLuong            INT            NOT NULL DEFAULT 1,
    ThoiGianThue       DATETIME       NOT NULL DEFAULT GETDATE(),
    ThoiGianTra        DATETIME       NULL,
    TrangThaiCoc       VARCHAR(20)    NOT NULL DEFAULT 'DaCoc',
    TienCoc            DECIMAL(15,0)  NOT NULL DEFAULT 0,
    PhiPhatSinh        DECIMAL(15,0)  NOT NULL DEFAULT 0,
    IdChungTuCoc       INT            NULL REFERENCES TaiChinh.ChungTuTC(Id),
    IdChungTuHoan      INT            NULL REFERENCES TaiChinh.ChungTuTC(Id),
    IdPhienTra         INT            NULL REFERENCES BanHang.PhienThuNgan(Id),
    TrangThai          VARCHAR(20)    NOT NULL DEFAULT 'DangThue',
    GhiChu             NVARCHAR(200)  NULL
);
GO

ALTER TABLE BanHang.ThueDoChiTiet ADD CONSTRAINT FK_ThueDoChiTiet_TaiSanChoThue 
    FOREIGN KEY (IdTaiSanChoThue) REFERENCES DanhMuc.TaiSanChoThue(Id);
GO



-- 6.13 Mua Suất Cho Thú Ăn
CREATE TABLE BanHang.DatChoThuAn (
    Id                 INT IDENTITY(1,1) PRIMARY KEY,
    IdChiTietDonHang   INT            NOT NULL REFERENCES BanHang.ChiTietDonHang(Id),
    IdChuongTrai       INT            NOT NULL REFERENCES DanhMuc.ChuongTrai(Id),
    SoLuotMua          INT            NOT NULL DEFAULT 1,
    GhiChu             NVARCHAR(200)  NULL
);
GO

-- FK bổ sung cho ChungTu -> DonHang --
ALTER TABLE Kho.ChungTu ADD CONSTRAINT FK_ChungTuKho_DonHang
    FOREIGN KEY (IdDonHang) REFERENCES BanHang.DonHang(Id);
ALTER TABLE TaiChinh.ChungTuTC ADD CONSTRAINT FK_ChungTuTC_DonHang
    FOREIGN KEY (IdDonHang) REFERENCES BanHang.DonHang(Id);
GO

-- FK bổ sung: BaoGia -> DonHang (truy vết báo giá sinh ra đơn hàng nào)
ALTER TABLE BanHang.BaoGia ADD CONSTRAINT FK_BaoGia_DonHang
    FOREIGN KEY (IdDonHang) REFERENCES BanHang.DonHang(Id);
GO

-- FK bổ sung: TheRFID -> PhieuDatPhong --
ALTER TABLE TaiChinh.TheRFID ADD CONSTRAINT FK_TheRFID_DatPhong
    FOREIGN KEY (IdDatPhong) REFERENCES BanHang.PhieuDatPhong(Id);
GO


-- PHẦN 6B: QUẢN LÝ NGHIỆP VỤ ĐẶC THÙ RIÊNG
-- Bao gồm: Cổng kiểm soát vé, Máy quét vòng tay RFID, Thuê Hướng dẫn viên Khách đoàn...

-- 6b.1 Quyền Truy Cập Vé (Ví dụ: Mua 1 vé Combo được đi 3 khu khác nhau)
-- 1 cái vé Combo cùi bắp làm sao quyết định cho khách vào cổng Thủy Cung mà chặn lại không cho lọt khu Hồ Bơi?
-- Giải pháp: Dùng Ma trận Quyền Truy Cập. Mua 1 Vé Combo 3 Trò -> Phần mềm tự tách làm 3 Dòng Quyền "chui qua cửa" gắn cho Mã Vé đó.
-- Khách đưa Mã xịn mà soi không đúng cổng có trong list thì máy quét kêu "Tít tít" khóa cửa chửi ngay!
CREATE TABLE DanhMuc.Ve_QuyenTruyCap (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdSanPhamVe    INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    IdKhuVuc       INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    IdTroChoi      INT            NULL REFERENCES DanhMuc.TroChoi(Id),
    SoLuotChoPhep  INT            NOT NULL DEFAULT 1,  -- 1 = Không giới hạn
    GhiChu         NVARCHAR(100)  NULL,
    CONSTRAINT CHK_VeQuyen_CoMucTieu CHECK (IdKhuVuc IS NOT NULL OR IdTroChoi IS NOT NULL)
);
GO

-- 6b.2 Bảng Chi tiết Lượt quét (Ghi lại lịch sử khách dùng QR vét qua cửa kiểm soát)
CREATE TABLE BanHang.ChiTietLuotQuet (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    IdVeDienTu       INT            NOT NULL REFERENCES BanHang.VeDienTu(Id),
    IdQuyenTruyCap   INT            NOT NULL REFERENCES DanhMuc.Ve_QuyenTruyCap(Id),
    IdThietBi        INT            NULL REFERENCES DanhMuc.ThietBi(Id),
    ThoiGianQuet     DATETIME       NOT NULL DEFAULT GETDATE(),
    KetQua           VARCHAR(20)    NOT NULL DEFAULT 'ThanhCong',
    GhiChu           NVARCHAR(100)  NULL
);
GO

-- 6b.3 Bảng Thông tin Máy tính tiền POS
CREATE TABLE BanHang.DiemBanHang_POS (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    MaDiemBan      VARCHAR(20)    NOT NULL UNIQUE,
    TenDiemBan     NVARCHAR(100)  NOT NULL,
    IdKhuVuc       INT            NOT NULL REFERENCES DanhMuc.KhuVuc(Id),
    ChoPhepBanVe   BIT            NOT NULL DEFAULT 0,
    ChoPhepBanFNB  BIT            NOT NULL DEFAULT 0,
    ChoPhepThue    BIT            NOT NULL DEFAULT 0,
    TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'HoatDong'
);
GO

-- 6b.4 Bảng Cài đặt Menu cho máy POS (Hạn chế máy bán kẹo thì không được bán vé)
CREATE TABLE BanHang.Menu_POS (
    IdDiemBan      INT            NOT NULL REFERENCES BanHang.DiemBanHang_POS(Id),
    IdSanPham      INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    ThuTuHienThi   INT            NOT NULL DEFAULT 0,
    ConHoatDong    BIT            NOT NULL DEFAULT 1,
    PRIMARY KEY (IdDiemBan, IdSanPham)
);
GO
-- 6b.5 Bảng Số lượng ăn theo mức Khách Đoàn
-- Khách đoàn BĐS 500 mạng bao luôn 500 suất Buffet. Không lẽ kéo vào ăn một đống lúc hoặc bắt nhân viên mỏi tay in 500 cái vé thả ra cho ngta cầm quẹt?
-- Cách giải quyết: Bảng này ôm trọn một rổ 500 Suất cấp duy nhất cho tài khoản Trưởng Đoàn. Nhân viên đoàn tới quăng Thẻ CCCD lên máy chít 1 tiếng check đúng nhóm, tự động nó trừ cái cụp 1 đơn vị xuống còn 499. Trơ tru vl!
CREATE TABLE BanHang.QuyenLoiDoanKhach (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    IdDonHang        INT            NOT NULL REFERENCES BanHang.DonHang(Id),
    IdDoanKhach      INT            NOT NULL REFERENCES DoiTac.ThongTin(Id),
    IdSanPham        INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    TongSoLuongMua   INT            NOT NULL,

    NgayHetHan       DATE           NULL,
    TrangThai        VARCHAR(20)    NOT NULL DEFAULT 'ConHieuLuc',
    GhiChu           NVARCHAR(200)  NULL
);
GO

-- 6b.5.1 Bảng Lịch sử quẹt thẻ ăn của Khách đoàn
CREATE TABLE BanHang.LichSuQuetDoan (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    IdQuyenLoi       INT            NOT NULL REFERENCES BanHang.QuyenLoiDoanKhach(Id),
    SoSuatTru        INT            NOT NULL,
    IdNhanVien       INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdThietBi        INT            NULL REFERENCES DanhMuc.ThietBi(Id),
    ThoiGian         DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- 6b.6 Bảng Lệnh Phục vụ Nhà hàng (Trưởng đoàn đặt món gì, giờ nào lên thức ăn thì nhà bếp coi ở đây)
CREATE TABLE VanHanh.LenhPhucVuDoan_BEO (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    MaBEO          VARCHAR(20)    NOT NULL UNIQUE,
    IdDonHang      INT            NOT NULL REFERENCES BanHang.DonHang(Id),
    IdNhaHang      INT            NOT NULL REFERENCES DanhMuc.NhaHang(Id),
    IdBepTruong    INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    SoLuongSuat    INT            NOT NULL,
    GioDonMon      DATETIME       NOT NULL,
    TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'ChuanBi',
    GhiChu         NVARCHAR(300)  NULL,
    NgayTao        DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- 6b.7 Bảng Bệnh án của Thú vườn y tế
CREATE TABLE VanHanh.NhatKyYTe_Thu (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdDongVat      INT            NOT NULL REFERENCES DanhMuc.DongVat(Id),
    NgayKham       DATETIME       NOT NULL DEFAULT GETDATE(),
    TrieuChung     NVARCHAR(500)  NULL,
    ChuanDoan      NVARCHAR(500)  NULL,
    DieuTri        NVARCHAR(500)  NULL,
    IdBacSi        INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    CanNang        DECIMAL(10,2)  NULL,
    NhietDo        DECIMAL(4,1)   NULL,
    CachLy         BIT            NOT NULL DEFAULT 0,
    GhiChu         NVARCHAR(300)  NULL
);
GO

-- 6b.8 Bảng Yêu cầu Khắt khe (Bắt buộc phải có bằng lái, bằng bơi lội mới được trực ở đây)
CREATE TABLE NhanSu.YeuCauChungChi_KhuVuc (
    IdKhuVuc       INT            NOT NULL REFERENCES DanhMuc.KhuVuc(Id),
    LoaiChungChi   VARCHAR(50)    NOT NULL,
    BatBuoc        BIT            NOT NULL DEFAULT 1,
    PRIMARY KEY (IdKhuVuc, LoaiChungChi)
);
GO


-- PHẦN 7: QUẢN LÝ NHÂN SỰ (Vai trò, Chấm công, Tính lương)

-- 7.1 Bảng Tên các Cấp bậc/Vai trò (Trưởng phòng, Nhân viên...)
CREATE TABLE NhanSu.VaiTro (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    TenVaiTro    NVARCHAR(50)   NOT NULL UNIQUE,
    MoTa         NVARCHAR(200)  NULL,
    ConHoatDong  BIT            NOT NULL DEFAULT 1
);
GO

ALTER TABLE DoiTac.NhanVien ADD CONSTRAINT FK_NhanVien_VaiTro
    FOREIGN KEY (IdVaiTro) REFERENCES NhanSu.VaiTro(Id);
GO

-- 7.2 Bảng Chức năng phần mềm (Mở form abc, Xóa dòng def)
CREATE TABLE NhanSu.QuyenHan (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    MaQuyen      VARCHAR(50)    NOT NULL UNIQUE,
    TenQuyen     NVARCHAR(100)  NOT NULL,
    NhomQuyen    VARCHAR(50)    NULL
);
GO

-- 7.3 Bảng Cấp Quyền cho Vai trò
CREATE TABLE NhanSu.PhanQuyen (
    IdVaiTro     INT            NOT NULL REFERENCES NhanSu.VaiTro(Id),
    IdQuyenHan   INT            NOT NULL REFERENCES NhanSu.QuyenHan(Id),
    PRIMARY KEY (IdVaiTro, IdQuyenHan)
);
GO

-- 7.4 Bảng Quy định Ca Làm Việc (Ca Sáng, Ca Chiều...)
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

-- 7.5 Bảng Lịch Phân Công Nhân Viên
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

-- 7.6 Bảng Dữ liệu Máy Chấm Công Vân Tay
CREATE TABLE NhanSu.BangChamCong (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien    INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdCaLamMau    INT            NULL REFERENCES NhanSu.CaLamMau(Id),
    IdLichLamViec INT            NULL REFERENCES NhanSu.LichLamViec(Id),
    ThoiGianVao   DATETIME       NOT NULL,
    ThoiGianRa    DATETIME       NULL,
    SoGioThucTe   AS (
        CASE WHEN ThoiGianRa IS NOT NULL
             THEN CAST(DATEDIFF(MINUTE, ThoiGianVao, ThoiGianRa) / 60.0 AS DECIMAL(5,2))
             ELSE NULL END
    ) PERSISTED,
    LoaiNgayLam   VARCHAR(20)    NOT NULL DEFAULT 'ThuongNgay',
    NguonChamCong VARCHAR(20)    NOT NULL DEFAULT 'VanTay',  -- VanTay / NhapTay / QRCode
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'DaVao'
);
GO

-- 7.7 Bảng Ngày Nghỉ Bù (Làm lố giờ thì được cộng vô đây)
CREATE TABLE NhanSu.NghiBu (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien      INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    NgayLamBu       DATE           NOT NULL,
    SoGioBu         DECIMAL(4,2)   NOT NULL DEFAULT 0,
    DaSuDung        BIT            NOT NULL DEFAULT 0,
    GhiChu          NVARCHAR(200)  NULL,
    IdNguoiDuyet    INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    NgayDuyet       DATETIME       NULL,
    LyDoTuChoi      NVARCHAR(300)  NULL,
    TrangThaiDuyet  VARCHAR(20)    NOT NULL DEFAULT 'ChoDuyet'
);
GO

-- 7.8 Bảng Đơn Xin Nghỉ Phép
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

-- 7.9 Bảng Số phép còn lại trong năm
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

-- 7.10 Bảng Chốt Lương Cuối Tháng
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

-- 7.11 Bảng Hồ sơ Chứng chỉ bằng cấp (Để máy tính nhắc đi gia hạn)
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

-- 7.12 Bảng Theo dõi Phạt Kỷ Luật
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

-- 7.13 Bảng Khai báo Tai Nạn Lao Động
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

-- Hợp đồng lao động
CREATE SEQUENCE HeThong.Seq_HopDong AS INT START WITH 1 INCREMENT BY 1;
GO

CREATE TABLE NhanSu.HopDong (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien      INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    SoHopDong       VARCHAR(30)    NOT NULL UNIQUE,
    LoaiHopDong     VARCHAR(20)    NOT NULL,
    ChucDanh        NVARCHAR(100)  NULL,
    LuongCoBan      DECIMAL(15,0)  NOT NULL,
    NgayHieuLuc     DATE           NOT NULL,
    NgayHetHan      DATE           NULL,
    LyDoKyMoi       NVARCHAR(200)  NULL,
    TepHopDong      VARCHAR(255)   NULL,
    IdNguoiKy       INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    TrangThai       VARCHAR(20)    NOT NULL DEFAULT 'DangHieuLuc',
    GhiChu          NVARCHAR(300)  NULL,
    NgayTao         DATETIME       NOT NULL DEFAULT GETDATE(),
    CONSTRAINT CHK_HD_Ngay CHECK (NgayHetHan IS NULL OR NgayHetHan > NgayHieuLuc),
    CONSTRAINT CHK_HD_LoaiHopDong CHECK (HeThong.fn_TuDienHopLe('NV_LOAI_HOP_DONG', LoaiHopDong) = 1),
    CONSTRAINT CHK_HD_TrangThai   CHECK (HeThong.fn_TuDienHopLe('HD_TRANG_THAI', TrangThai) = 1)
);
CREATE INDEX IX_HopDong_NhanVien ON NhanSu.HopDong(IdNhanVien, TrangThai);
GO

-- BHXH / BHYT
CREATE TABLE NhanSu.BHXH (
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien          INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    Thang               INT            NOT NULL,
    Nam                 INT            NOT NULL,
    LuongDongBH         DECIMAL(15,0)  NOT NULL,
    TyLeBHXH_NV        DECIMAL(5,2)   NOT NULL DEFAULT 8.0,
    TyLeBHXH_CT        DECIMAL(5,2)   NOT NULL DEFAULT 17.5,
    TyLeBHYT_NV        DECIMAL(5,2)   NOT NULL DEFAULT 1.5,
    TyLeBHYT_CT        DECIMAL(5,2)   NOT NULL DEFAULT 3.0,
    TyLeBHTN_NV        DECIMAL(5,2)   NOT NULL DEFAULT 1.0,
    TyLeBHTN_CT        DECIMAL(5,2)   NOT NULL DEFAULT 1.0,
    DaDong              BIT            NOT NULL DEFAULT 0,
    IdBangLuong         INT            NULL REFERENCES NhanSu.BangLuong(Id),
    NgayTao             DATETIME       NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UQ_BHXH_NV_Thang UNIQUE (IdNhanVien, Thang, Nam)
);
CREATE INDEX IX_BHXH_NhanVien ON NhanSu.BHXH(IdNhanVien, Nam);
GO

-- Phụ cấp chi tiết theo bảng lương
CREATE TABLE NhanSu.ChiTietPhuCap (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdBangLuong     INT            NOT NULL REFERENCES NhanSu.BangLuong(Id),
    TenPhuCap       NVARCHAR(100)  NOT NULL,
    SoTien          DECIMAL(15,0)  NOT NULL DEFAULT 0,
    GhiChu          NVARCHAR(200)  NULL
);
GO

-- Yêu cầu đổi ca
CREATE TABLE NhanSu.DoiCa (
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVienXin       INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdLichLamViecGoc    INT            NOT NULL REFERENCES NhanSu.LichLamViec(Id),
    IdNhanVienNhan      INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdLichLamViecNhan   INT            NULL REFERENCES NhanSu.LichLamViec(Id),
    LyDo                NVARCHAR(300)  NULL,
    IdNguoiDuyet        INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    TrangThai           VARCHAR(20)    NOT NULL DEFAULT 'ChoDuyet',
    NgayTao             DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- Khóa đào tạo nội bộ
CREATE TABLE NhanSu.KhoaDaoTao (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    TenKhoa         NVARCHAR(200)  NOT NULL,
    MoTa            NVARCHAR(MAX)  NULL,
    LoaiDaoTao      VARCHAR(30)    NOT NULL DEFAULT 'NoiBo',
    GiangVien       NVARCHAR(100)  NULL,
    NgayBatDau      DATE           NULL,
    NgayKetThuc     DATE           NULL,
    SoGio           DECIMAL(5,1)   NULL,
    NgayTao         DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- Lịch sử tham gia đào tạo
CREATE TABLE NhanSu.LichSuDaoTao (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien      INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdKhoaDaoTao    INT            NOT NULL REFERENCES NhanSu.KhoaDaoTao(Id),
    NgayHoanThanh   DATE           NULL,
    KetQua          VARCHAR(20)    NULL,
    DiemSo          DECIMAL(4,2)   NULL,
    GhiChu          NVARCHAR(200)  NULL,
    CONSTRAINT UQ_DaoTao_NV_Khoa UNIQUE (IdNhanVien, IdKhoaDaoTao)
);
CREATE INDEX IX_LichSuDaoTao_NV ON NhanSu.LichSuDaoTao(IdNhanVien);
GO

-- Đánh giá hiệu suất nhân viên (KPI)
CREATE TABLE NhanSu.DanhGiaNhanVien (
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien          INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    KyDanhGia           VARCHAR(20)    NOT NULL,
    LoaiDanhGia         VARCHAR(20)    NOT NULL DEFAULT 'DinhKy',
    DiemChuyen_Mon      DECIMAL(4,2)   NULL,
    DiemThai_Do         DECIMAL(4,2)   NULL,
    DiemKet_Qua         DECIMAL(4,2)   NULL,
    DiemTong            AS (
        CASE WHEN DiemChuyen_Mon IS NOT NULL AND DiemThai_Do IS NOT NULL AND DiemKet_Qua IS NOT NULL
             THEN ROUND((DiemChuyen_Mon + DiemThai_Do + DiemKet_Qua) / 3.0, 2)
             ELSE NULL END
    ) PERSISTED,
    XepLoai             VARCHAR(10)    NULL,
    NhanXetNguoiDG      NVARCHAR(MAX)  NULL,
    NhanXetNhanVien     NVARCHAR(MAX)  NULL,
    IdNguoiDanhGia      INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    TrangThai           VARCHAR(20)    NOT NULL DEFAULT 'NhapLieu',
    NgayDanhGia         DATE           NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    NgayTao             DATETIME       NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UQ_DanhGia_NV_Ky UNIQUE (IdNhanVien, KyDanhGia, LoaiDanhGia),
    CONSTRAINT CHK_DanhGia_Diem CHECK (
        (DiemChuyen_Mon IS NULL OR DiemChuyen_Mon BETWEEN 0 AND 10) AND
        (DiemThai_Do    IS NULL OR DiemThai_Do    BETWEEN 0 AND 10) AND
        (DiemKet_Qua    IS NULL OR DiemKet_Qua    BETWEEN 0 AND 10)
    )
);
CREATE INDEX IX_DanhGia_NhanVien ON NhanSu.DanhGiaNhanVien(IdNhanVien);
GO

-- View cảnh báo hợp đồng sắp hết hạn
CREATE VIEW NhanSu.V_HopDongSapHetHan AS
    SELECT nv.MaNhanVien, tt.HoTen, hd.SoHopDong, hd.LoaiHopDong,
           hd.NgayHieuLuc, hd.NgayHetHan, hd.TrangThai,
           DATEDIFF(DAY, GETDATE(), hd.NgayHetHan) AS SoNgayConLai
    FROM NhanSu.HopDong hd
    JOIN DoiTac.NhanVien nv ON hd.IdNhanVien = nv.IdDoiTac
    JOIN DoiTac.ThongTin tt ON nv.IdDoiTac = tt.Id
    WHERE hd.TrangThai = 'DangHieuLuc'
      AND hd.NgayHetHan IS NOT NULL
      AND DATEDIFF(DAY, GETDATE(), hd.NgayHetHan) <= 60;
GO

-- PHẦN 8: QUẢN LÝ VẬN HÀNH DỊCH VỤ (Sự cố, Bảo trì sửa chữa, Trực cứu hộ, Cổng gửi xe...)

-- 8.1 Bảng Sự Kiện (Lễ hội mùa đông, Countdown...)
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

-- 8.1b Sự kiện cho thuê (Công ty thuê khu vực tổ chức riêng)
ALTER TABLE VanHanh.SuKien ADD IdDoanKhach INT NULL REFERENCES DoiTac.ThongTin(Id);
GO

-- Bảng phân công Hướng Dẫn Viên cho đoàn khách
CREATE TABLE VanHanh.HuongDanVien_DoanKhach (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdNhanVien     INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdDonHang      INT            NOT NULL REFERENCES BanHang.DonHang(Id),
    ThoiGianBatDau DATETIME       NOT NULL,
    ThoiGianKetThuc DATETIME      NULL,
    TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'DangDanDoan',
    GhiChu         NVARCHAR(300)  NULL
);
GO



-- 8.1c Lịch Biểu Diễn (Show xiếc, show nước, biểu diễn nghệ thuật)
CREATE TABLE VanHanh.LichBieuDien (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    TenShow          NVARCHAR(200)  NOT NULL,
    IdKhuVuc         INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    ThoiGianBatDau   DATETIME       NOT NULL,
    ThoiGianKetThuc  DATETIME       NULL,
    SucChua          INT            NULL,       -- Số ghế tối đa
    TrangThai        VARCHAR(20)    NOT NULL DEFAULT 'SapDienRa',
    GhiChu           NVARCHAR(300)  NULL
);
GO

-- 8.1d Đặt Chỗ Xem Show (Bán vé xem biểu diễn, link về ChiTietDonHang để tính tiền)
CREATE TABLE BanHang.DatChoXemShow (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    IdLichBieuDien   INT            NOT NULL REFERENCES VanHanh.LichBieuDien(Id),
    IdChiTietDonHang INT            NOT NULL REFERENCES BanHang.ChiTietDonHang(Id),  --  show miễn phí thì DonGiaThucTe = 0, vẫn phải có dòng bill
    SoGhe            INT            NOT NULL DEFAULT 1,
    TrangThai        VARCHAR(20)    NOT NULL DEFAULT 'DaDat'
);
GO

-- 8.2 Bảng Báo Cáo Sự Cố (Khách phàn nàn, cúp điện, hỏng máy lạnh...)
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
    IdKhachHang   INT            NULL REFERENCES DoiTac.KhachHang(IdDoiTac), -- Tích hợp quản lý Khiếu nại của khách
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'MoiGhi',
    BienPhapXuLy  NVARCHAR(500)  NULL,
    ChiPhiXuLy    DECIMAL(15,0)  NULL,
    NgayTao       DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- 8.2b Bảng Giao Việc Dọn Phòng cho Tạp vụ (Khách sạn)
CREATE TABLE VanHanh.PhanCongBuongPhong (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdPhong        INT            NOT NULL REFERENCES DanhMuc.Phong(Id),
    IdNhanVien     INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    ThoiGianNhan   DATETIME       NOT NULL DEFAULT GETDATE(),
    ThoiGianXong   DATETIME       NULL,
    TrangThai      VARCHAR(20)    NOT NULL DEFAULT 'DaGiao',
    MoTa           NVARCHAR(200)  NULL
);
GO

-- 8.2b.2 Bảng Chi Tiết Vật Tư Tiêu Hao Thực Tế (Ghi nhận số lượng khăn, nước thực tế sử dụng)
CREATE TABLE VanHanh.ChiTietVatTuTieuHao (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdPhanCong     INT            NOT NULL REFERENCES VanHanh.PhanCongBuongPhong(Id),
    IdSanPham      INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    SoLuongSuDung  INT            NOT NULL DEFAULT 1
);
GO

-- TRIGGER CHỐNG ĐẶT TRÙNG PHÒNG (OVERBOOKING)
-- Trạng thái phải khớp 100% với AppConstants.TrangThaiBooking trong C#
CREATE TRIGGER trg_CheckOverlappingBooking
ON BanHang.ChiTietDatPhong
AFTER INSERT, UPDATE
AS
BEGIN
    -- VÀ kiểm tra trùng theo IdLoaiPhong khi chưa gán phòng
    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN BanHang.ChiTietDatPhong p ON p.Id <> i.Id
        WHERE p.TrangThai IN ('DatTruoc', 'DangO')
          AND i.TrangThai IN ('DatTruoc', 'DangO')
          AND i.NgayCheckIn  < p.NgayCheckOut
          AND i.NgayCheckOut > p.NgayCheckIn
          AND (
                (i.IdPhong IS NOT NULL AND p.IdPhong IS NOT NULL AND i.IdPhong = p.IdPhong)
                OR
                (i.IdPhong IS NULL AND p.IdLoaiPhong = i.IdLoaiPhong)
              )
    )
    BEGIN
        RAISERROR (N'Lỗi: Phòng này đã được đặt trong khoảng thời gian trên!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- 8.2c Vật tư mặc định theo Loại Phòng (template để Housekeeping biết cần bọ gì vào phòng)
-- VD: Phòng Standard = 2 khăn + 1 dầu gội + 2 nước. VIP = 4 khăn + 2 dầu gội + 4 nước + 1 hoa.
-- Khi dọn phòng, nhân viên mở app → thấy danh sách vật tư cần mang lên → không thiếu, không thừa.
CREATE TABLE VanHanh.VatTuPhongMacDinh (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    IdLoaiPhong    INT            NOT NULL REFERENCES DanhMuc.LoaiPhong(IdSanPham),
    IdSanPham      INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),  -- SP = khăn, dầu gội... (cũng là vật tư trong kho)
    SoLuong        INT            NOT NULL DEFAULT 1,
    CONSTRAINT UQ_VatTuPhong UNIQUE (IdLoaiPhong, IdSanPham)
);
GO

ALTER TABLE NhanSu.TaiNanLaoDong ADD CONSTRAINT FK_TaiNan_SuCo
    FOREIGN KEY (IdSuCo) REFERENCES VanHanh.SuCo(Id);
GO

-- 8.3 Bảng Khai báo Đồ vật Thất Lạc của Khách
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

-- 8.4 Bảng Khách Hàng Đánh Giá Dịch Vụ (1 tới 5 sao)
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

-- 8.5 Bảng Kế Hoạch Bảo Trì Thiết Bị
CREATE TABLE VanHanh.BaoTri (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdThietBi     INT            NULL REFERENCES DanhMuc.ThietBi(Id),
    IdKhuVuc      INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    IdPhong       INT            NULL REFERENCES DanhMuc.Phong(Id),  -- Khách sạn: lock phòng cụ thể khi máy lạnh hỏng
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

-- 8.6 Bảng Liệt kê Vật tư được lấy ra từ kho để đi sửa chữa thiết bị
CREATE TABLE VanHanh.ChiTietVatTuBaoTri (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdBaoTri      INT            NOT NULL REFERENCES VanHanh.BaoTri(Id),
    IdSanPham     INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    SoLuong       INT            NOT NULL,
    DonGia        DECIMAL(15,0)  NULL,
    IdChungTuXuat INT            NULL REFERENCES Kho.ChungTu(Id)
);
GO

-- 8.7 Ca trực Cứu Hộ (Bắt buộc theo luật: hồ bơi phải có cứu hộ trực)
-- Quản lý biết khu nào đang có ai trực, khu nào thiếu người → điều phối ngay.
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

-- 8.8 Chất Lượng Nước Hồ Bơi (đo pH, Clo, nhiệt độ hằng ngày theo quy định Bộ Y Tế)
-- Nếu DatChuan = 0 → quản lý phải xử lý ngay (thêm hóa chất hoặc đóng hồ).
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

-- 8.9 Bảng Lịch chạy Máy Tạo Sóng Nhân Tạo (Ở Biển nhân tạo)
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

-- 8.10 Bảng Lịch sử Quét Vé Khách hàng
CREATE TABLE VanHanh.LichSuQuetVe (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdVeDienTu    INT            NOT NULL REFERENCES BanHang.VeDienTu(Id),
    ThoiGianQuet  DATETIME       NOT NULL DEFAULT GETDATE(),
    KetQua        VARCHAR(20)    NOT NULL,
    IdThietBi     INT            NULL REFERENCES DanhMuc.ThietBi(Id),
    GhiChu        NVARCHAR(200)  NULL
);
GO

-- 8.11 Bảng Bãi Đỗ Xe
CREATE TABLE VanHanh.BaiDoXe (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    TenBai        NVARCHAR(100)  NOT NULL,
    SucChua       INT            NOT NULL,

    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'HoatDong'
);
GO

-- 8.12 Bảng Máy Quét Biển Số Xe Ra Vào Bãi
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
    IdChiTietDonHang INT         NULL REFERENCES BanHang.ChiTietDonHang(Id),  -- ★ Link đúng tầng Polymorphic (dòng chi tiết, không chỉ header)
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'DangGui'
);
GO

-- 8.13 Bảng Thông tin Vé Gửi Xe Của Khách
CREATE TABLE VanHanh.VeDoXeChiTiet (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    IdLuotVaoRa   INT            NOT NULL REFERENCES VanHanh.LuotVaoRaBaiXe(Id),
    MaVeXe        VARCHAR(50)    NOT NULL UNIQUE,
    PhiGuiXe      DECIMAL(15,0)  NOT NULL DEFAULT 0,
    DaThanhToan   BIT            NOT NULL DEFAULT 0
);
GO

-- 8.14 Bảng Máy Kiosk Cảm Ứng (Để khách tự bấm mua vé)
CREATE TABLE VanHanh.Kiosk (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    MaKiosk       VARCHAR(20)    NOT NULL UNIQUE,
    TenKiosk      NVARCHAR(100)  NOT NULL,
    IdKhuVuc      INT            NULL REFERENCES DanhMuc.KhuVuc(Id),
    TrangThai     VARCHAR(20)    NOT NULL DEFAULT 'HoatDong'
);
GO


-- PHẦN 9: QUẢN LÝ TRƯỜNG ĐUA ĐẠI NAM (Đua chó, đua ngựa, đua xe)
-- Đại Nam có trường đua thật, cần quản lý: đường đua, loại hình, giải, lịch thi đấu, vận động viên/ngựa, kết quả.
-- Khách mua vé xem đua thông qua DatChoXemDua → ChiTietDonHang (cũng là satellite như Vé, Đặt Phòng).

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

-- Động vật tham gia thi đua (subtable của DanhMuc.DongVat)
CREATE TABLE VanHanh.DongVatDua (
    IdDongVat       INT            PRIMARY KEY REFERENCES DanhMuc.DongVat(Id),
    GiongLoai       NVARCHAR(50)   NULL,
    SoHieuDua       VARCHAR(20)    NULL UNIQUE,
    TrangThaiDua    VARCHAR(20)    NOT NULL DEFAULT 'SanSang',
    DiemThanh_Tich  INT            NOT NULL DEFAULT 0,
    TongSoLanDua    INT            NOT NULL DEFAULT 0,
    TongSoLanVoDich INT            NOT NULL DEFAULT 0,
    CanNangThiDau   DECIMAL(6,2)   NULL,
    CONSTRAINT CHK_DVDua_TrangThai
        CHECK (HeThong.fn_TuDienHopLe('DVDua_TRANG_THAI', TrangThaiDua) = 1)
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

-- Kết quả thi đua (hỗ trợ cả người lẫn động vật lẫn phương tiện)
CREATE TABLE VanHanh.KetQuaDua (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdLichThiDau    INT            NOT NULL REFERENCES VanHanh.LichThiDau(Id),
    IdVanDongVien   INT            NULL REFERENCES DoiTac.VanDongVien(IdDoiTac),
    IdDongVatDua    INT            NULL REFERENCES VanHanh.DongVatDua(IdDongVat),
    IdPhuongTien    INT            NULL REFERENCES VanHanh.PhuongTienDua(Id),
    HangVeDich      INT            NULL,
    ThoiGianHoan    DECIMAL(10,3)  NULL,
    GhiChu          NVARCHAR(200)  NULL,
    CONSTRAINT CHK_KQDua_CoThiSinh CHECK (
        IdVanDongVien IS NOT NULL OR IdDongVatDua IS NOT NULL OR IdPhuongTien IS NOT NULL
    )
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

-- 9.8 Bảng Đặt Chỗ Xem Đua (Bán vé ghế xem đua cho từng trận)
-- 1 LichThiDau + 1 ViTriNgoi = 1 vé xem đua, link về ChiTietDonHang để tính tiền
CREATE TABLE BanHang.DatChoXemDua (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    IdLichThiDau     INT            NOT NULL REFERENCES VanHanh.LichThiDau(Id),
    IdViTriNgoi      INT            NOT NULL REFERENCES VanHanh.ViTriNgoi(Id),
    IdChiTietDonHang INT            NOT NULL REFERENCES BanHang.ChiTietDonHang(Id),  -- ★ NOT NULL: vé xem đua phải có bill
    TrangThai        VARCHAR(20)    NOT NULL DEFAULT 'DaDat',
    CONSTRAINT UQ_DatChoXemDua UNIQUE (IdLichThiDau, IdViTriNgoi)  -- 1 ghế chỉ bán 1 lần / trận
);
GO


-- Lịch sử chuyển chuồng động vật
CREATE TABLE VanHanh.LichSuChuongTrai (
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    IdDongVat           INT            NOT NULL REFERENCES DanhMuc.DongVat(Id),
    IdChuongCu          INT            NULL REFERENCES DanhMuc.ChuongTrai(Id),
    IdChuongMoi         INT            NOT NULL REFERENCES DanhMuc.ChuongTrai(Id),
    NgayChuyenDoi       DATETIME       NOT NULL DEFAULT GETDATE(),
    LyDo                NVARCHAR(200)  NOT NULL,
    IdNhanVienThucHien  INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    GhiChu              NVARCHAR(300)  NULL
);
CREATE INDEX IX_LichSuChuong_DV ON VanHanh.LichSuChuongTrai(IdDongVat, NgayChuyenDoi DESC);
GO

-- Trigger audit khi cập nhật chuồng
CREATE OR ALTER TRIGGER trg_DongVat_ChuongTrai_AuditTrail
ON DanhMuc.DongVat
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF UPDATE(IdChuongTrai)
    BEGIN
        INSERT INTO VanHanh.LichSuChuongTrai
            (IdDongVat, IdChuongCu, IdChuongMoi, NgayChuyenDoi, LyDo, IdNhanVienThucHien)
        SELECT
            i.Id, d.IdChuongTrai, i.IdChuongTrai, GETDATE(),
            N'Tự động ghi nhận từ cập nhật chuồng',
            ISNULL(CAST(SESSION_CONTEXT(N'CurrentUserId') AS INT), 1)
        FROM inserted i
        JOIN deleted d ON i.Id = d.Id
        WHERE ISNULL(i.IdChuongTrai, -1) <> ISNULL(d.IdChuongTrai, -1);
    END
END;
GO

-- Định mức thức ăn theo loài / cá thể
CREATE TABLE VanHanh.DinhMucThucAn (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdDongVat       INT            NULL REFERENCES DanhMuc.DongVat(Id),
    LoaiDongVat     NVARCHAR(50)   NULL,
    IdSanPham       INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    BuaAn           VARCHAR(20)    NOT NULL DEFAULT 'Sang',
    SoLuong         DECIMAL(10,3)  NOT NULL,
    GhiChu          NVARCHAR(200)  NULL,
    ConHoatDong     BIT            NOT NULL DEFAULT 1,
    CONSTRAINT CHK_DinhMuc_CoMucTieu CHECK (IdDongVat IS NOT NULL OR LoaiDongVat IS NOT NULL)
);
GO

-- Nhật ký cho ăn thực tế
CREATE TABLE VanHanh.NhatKyChoAn (
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    IdDongVat           INT            NOT NULL REFERENCES DanhMuc.DongVat(Id),
    IdSanPham           INT            NOT NULL REFERENCES DanhMuc.SanPham(Id),
    BuaAn               VARCHAR(20)    NOT NULL DEFAULT 'Sang',
    ThoiGian            DATETIME       NOT NULL DEFAULT GETDATE(),
    SoLuongDinhMuc      DECIMAL(10,3)  NULL,
    SoLuongThucTe       DECIMAL(10,3)  NOT NULL,
    LyDoChenhLech       NVARCHAR(200)  NULL,
    IdNhanVien          INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdChungTuXuat       INT            NULL REFERENCES Kho.ChungTu(Id),
    TrangThai           VARCHAR(20)    NOT NULL DEFAULT 'DaChoAn',
    GhiChu              NVARCHAR(300)  NULL
);
CREATE INDEX IX_NhatKyChoAn_DV ON VanHanh.NhatKyChoAn(IdDongVat, ThoiGian DESC);
GO

-- Tiêm phòng & xổ giun
CREATE TABLE VanHanh.TiemPhong (
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    IdDongVat           INT            NOT NULL REFERENCES DanhMuc.DongVat(Id),
    LoaiVaccine         NVARCHAR(100)  NOT NULL,
    LoaiTiemPhong       VARCHAR(20)    NOT NULL DEFAULT 'Vaccine',
    NgayThucHien        DATE           NOT NULL,
    LieuLuong           NVARCHAR(50)   NULL,
    DuongTiem           VARCHAR(20)    NULL,
    NgayTiemTiep        DATE           NULL,
    IdNhanVien          INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    SoLoVaccine         VARCHAR(30)    NULL,
    NhaSanXuat          NVARCHAR(100)  NULL,
    NgayHetHanVaccine   DATE           NULL,
    PhanUng             NVARCHAR(300)  NULL,
    TrangThai           VARCHAR(20)    NOT NULL DEFAULT 'DaThucHien',
    GhiChu              NVARCHAR(300)  NULL,
    NgayTao             DATETIME       NOT NULL DEFAULT GETDATE()
);
CREATE INDEX IX_TiemPhong_DV   ON VanHanh.TiemPhong(IdDongVat, NgayThucHien DESC);
CREATE INDEX IX_TiemPhong_Nhac ON VanHanh.TiemPhong(NgayTiemTiep) WHERE NgayTiemTiep IS NOT NULL;
GO

-- Nhật ký tăng trưởng (cân nặng, kích thước)
CREATE TABLE VanHanh.NhatKyTangTruong (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdDongVat       INT            NOT NULL REFERENCES DanhMuc.DongVat(Id),
    NgayDo          DATE           NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    CanNang         DECIMAL(10,2)  NULL,
    ChieuDai        DECIMAL(8,2)   NULL,
    ChieuCao        DECIMAL(8,2)   NULL,
    DanhGiaSucKhoe  VARCHAR(20)    NULL,
    IdNhanVien      INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    GhiChu          NVARCHAR(300)  NULL
);
CREATE INDEX IX_TangTruong_DV ON VanHanh.NhatKyTangTruong(IdDongVat, NgayDo DESC);
GO

-- Nhật ký huấn luyện hành vi
CREATE TABLE VanHanh.NhatKyHuanLuyen (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdDongVat       INT            NOT NULL REFERENCES DanhMuc.DongVat(Id),
    TenKyNang       NVARCHAR(100)  NOT NULL,
    ThoiGian        DATETIME       NOT NULL DEFAULT GETDATE(),
    SoPhutLuyen     INT            NULL,
    KetQua          VARCHAR(20)    NOT NULL DEFAULT 'DangHoc',
    PhuongPhap      NVARCHAR(200)  NULL,
    IdHuanLuyenVien INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    GhiChu          NVARCHAR(300)  NULL
);
CREATE INDEX IX_HuanLuyen_DV ON VanHanh.NhatKyHuanLuyen(IdDongVat, ThoiGian DESC);
GO

-- Sự kiện sinh sản
CREATE TABLE VanHanh.SuKienSinhSan (
    Id                  INT IDENTITY(1,1) PRIMARY KEY,
    IdMe                INT            NOT NULL REFERENCES DanhMuc.DongVat(Id),
    IdCha               INT            NULL REFERENCES DanhMuc.DongVat(Id),
    NgayPhoiGiong       DATE           NULL,
    NgayDuKienSinh      DATE           NULL,
    NgaySinhThucTe      DATE           NULL,
    SoConSinh           INT            NULL,
    SoConSong           INT            NULL,
    MoTa                NVARCHAR(500)  NULL,
    TrangThai           VARCHAR(20)    NOT NULL DEFAULT 'DangMangThai',
    IdNhanVienGhiNhan   INT            NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    NgayTao             DATETIME       NOT NULL DEFAULT GETDATE()
);
GO

-- View cảnh báo lịch tiêm phòng trong 30 ngày tới
CREATE VIEW VanHanh.V_CanhBaoTiemPhong AS
SELECT dv.Id AS IdDongVat, dv.TenDongVat, dv.Loai,
       tp.LoaiVaccine, tp.NgayTiemTiep,
       DATEDIFF(DAY, GETDATE(), tp.NgayTiemTiep) AS SoNgayConLai,
       ct.TenChuong
FROM VanHanh.TiemPhong tp
JOIN DanhMuc.DongVat dv ON tp.IdDongVat = dv.Id
LEFT JOIN DanhMuc.ChuongTrai ct ON dv.IdChuongTrai = ct.Id
WHERE tp.NgayTiemTiep IS NOT NULL
  AND tp.NgayTiemTiep <= DATEADD(DAY, 30, CAST(GETDATE() AS DATE))
  AND dv.DaXoa = 0;
GO

-- View sức khỏe tổng hợp (khám + cân nặng mới nhất)
CREATE VIEW VanHanh.V_SucKhoeThucTe AS
SELECT dv.Id, dv.TenDongVat, dv.Loai, dv.GioiTinh,
       dv.TrangThai AS TrangThaiSoThu,
       ct.TenChuong,
       yte.NgayKham AS NgayKhamCuoi, yte.ChuanDoan, yte.CachLy,
       tg.CanNang AS CanNangMoiNhat, tg.NgayDo AS NgayDoCanNang
FROM DanhMuc.DongVat dv
LEFT JOIN DanhMuc.ChuongTrai ct ON dv.IdChuongTrai = ct.Id
LEFT JOIN (
    SELECT IdDongVat, NgayKham, ChuanDoan, CachLy,
           ROW_NUMBER() OVER (PARTITION BY IdDongVat ORDER BY NgayKham DESC) AS rn
    FROM VanHanh.NhatKyYTe_Thu
) yte ON dv.Id = yte.IdDongVat AND yte.rn = 1
LEFT JOIN (
    SELECT IdDongVat, CanNang, NgayDo,
           ROW_NUMBER() OVER (PARTITION BY IdDongVat ORDER BY NgayDo DESC) AS rn
    FROM VanHanh.NhatKyTangTruong WHERE CanNang IS NOT NULL
) tg ON dv.Id = tg.IdDongVat AND tg.rn = 1
WHERE dv.DaXoa = 0;
GO

-- Luồng trạng thái HopDong + DoiCa
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('HopDong', 'DangHieuLuc', 'HetHan',      N'Hợp đồng đến hạn tự động'),
('HopDong', 'DangHieuLuc', 'ChamDut',     N'Chấm dứt trước hạn'),
('HopDong', 'DangHieuLuc', 'ChuaKyLai',   N'Sắp hết hạn, chờ ký gia hạn'),
('HopDong', 'ChuaKyLai',   'DangHieuLuc', N'Đã ký hợp đồng mới'),
('HopDong', 'ChuaKyLai',   'ChamDut',     N'Không gia hạn, nghỉ việc'),
('HopDong', 'HetHan',      'DangHieuLuc', N'Ký hợp đồng mới sau khi hết hạn'),
('DoiCa',   'ChoDuyet',    'DaDuyet',     N'Quản lý phê duyệt đổi ca'),
('DoiCa',   'ChoDuyet',    'TuChoi',      N'Quản lý từ chối đổi ca'),
('DoiCa',   'DaDuyet',     'DaThucHien',  N'Ca đã được thực hiện');
GO

-- PHẦN 10: CÁC VIEW BÁO CÁO TỔNG HỢP (Lấy dữ liệu nhanh không cần viết SQL dài)
-- V3: Đọc từ ChiTietChungTu thay vì SoCai (SoCai đã bị DROP)
-- Chỉ tính các phiếu TrangThai = 'DaDuyet' để đảm bảo tồn kho chính xác
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
        SELECT ct.IdSanPham, ct.IdKhoNhap AS IdKho,  ct.SoLuong
        FROM Kho.ChiTietChungTu ct
        JOIN Kho.ChungTu c ON ct.IdChungTu = c.Id
        WHERE c.TrangThai = 'DaDuyet'
        UNION ALL
        SELECT ct.IdSanPham, ct.IdKhoXuat AS IdKho, -ct.SoLuong
        FROM Kho.ChiTietChungTu ct
        JOIN Kho.ChungTu c ON ct.IdChungTu = c.Id
        WHERE c.TrangThai = 'DaDuyet'
    ) raw
    GROUP BY IdSanPham, IdKho
    HAVING SUM(SoLuong) != 0
) t
JOIN Kho.KhoHang kh ON t.IdKho = kh.Id AND kh.LaKhoAo = 0
JOIN DanhMuc.SanPham sp ON t.IdSanPham = sp.Id;
GO

-- V_TonTheoLo: tính tồn lô theo ChiTietChungTu 
CREATE VIEW Kho.V_TonTheoLo AS
SELECT
    lh.Id AS IdLoHang, lh.MaLoHang,
    lh.IdSanPham, sp.TenSanPham,
    lh.NgayHetHan,
    t.TonLo
FROM (
    SELECT IdLoHang, SUM(SoLuongRong) AS TonLo
    FROM (
        -- Hàng nhập VÀO kho thực (+)
        SELECT ct.IdLoHang, ct.SoLuong AS SoLuongRong
        FROM Kho.ChiTietChungTu ct
        JOIN Kho.ChungTu c ON ct.IdChungTu = c.Id
        JOIN Kho.KhoHang kh ON ct.IdKhoNhap = kh.Id AND kh.LaKhoAo = 0
        WHERE ct.IdLoHang IS NOT NULL AND c.TrangThai = 'DaDuyet'
        UNION ALL
        -- Hàng xuất KHỎI kho thực (-)
        SELECT ct.IdLoHang, -ct.SoLuong AS SoLuongRong
        FROM Kho.ChiTietChungTu ct
        JOIN Kho.ChungTu c ON ct.IdChungTu = c.Id
        JOIN Kho.KhoHang kh ON ct.IdKhoXuat = kh.Id AND kh.LaKhoAo = 0
        WHERE ct.IdLoHang IS NOT NULL AND c.TrangThai = 'DaDuyet'
    ) raw
    GROUP BY IdLoHang
    HAVING SUM(SoLuongRong) > 0
) t
JOIN Kho.LoHang lh ON t.IdLoHang = lh.Id
JOIN DanhMuc.SanPham sp ON lh.IdSanPham = sp.Id
WHERE lh.TrangThai != 'DaHuy';
GO

-- Báo cáo: Xem danh sách thực phẩm trong kho còn dưới 7 ngày nữa là hết hạn
CREATE VIEW Kho.V_CanhBaoHetHan AS
SELECT lh.*, sp.TenSanPham,
    DATEDIFF(DAY, GETDATE(), lh.NgayHetHan) AS SoNgayConLai
FROM Kho.LoHang lh
JOIN DanhMuc.SanPham sp ON lh.IdSanPham = sp.Id
WHERE lh.NgayHetHan IS NOT NULL
  AND lh.NgayHetHan <= DATEADD(DAY, 7, GETDATE())
  AND lh.TrangThai = 'ConHang';
GO

-- Báo cáo: Xem tồn kho khả dụng (tồn thực tế trừ đi hàng đang giữ cho đơn online)
CREATE VIEW Kho.V_TonKhaDung AS
SELECT vt.IdSanPham, vt.IdKho, vt.TenKho, vt.TenSanPham,
       vt.TonHienTai,
       ISNULL(gg.TongGiu, 0) AS DangGiu,
       vt.TonHienTai - ISNULL(gg.TongGiu, 0) AS TonKhaDung
FROM Kho.V_TonKho vt
LEFT JOIN (
    SELECT IdSanPham, IdKho, SUM(SoLuongGiu) AS TongGiu
    FROM Kho.TamGiuTonKho WHERE TrangThai = 'DangGiu'
    GROUP BY IdSanPham, IdKho
) gg ON vt.IdSanPham = gg.IdSanPham AND vt.IdKho = gg.IdKho;
GO

-- Báo cáo: Xem tổng số dư khả dụng thực tế của Ví điện tử RFID
CREATE VIEW TaiChinh.V_SoDuVi AS
SELECT
    IdVi,
    SUM(CASE WHEN LoaiPhep = 'CONG' THEN SoTien ELSE 0 END)
  - SUM(CASE WHEN LoaiPhep = 'TRU'  THEN SoTien ELSE 0 END) AS SoDuKhaDung
FROM TaiChinh.SoCaiVi
GROUP BY IdVi;
GO

-- Báo cáo: Xem quản lý/nhân viên nào sắp hết hạn bằng cấp/chứng chỉ để gọi đi thi lại
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

-- Báo cáo: Cảnh báo nhân viên được phân công vào khu vực nhưng thiếu chứng chỉ bắt buộc
CREATE VIEW NhanSu.V_PhanCongThieuChungChi AS
SELECT 
    ll.IdNhanVien, dt.HoTen, ll.NgayLam,
    kv.TenKhuVuc, yc.LoaiChungChi AS ChungChiCanThiet
FROM NhanSu.LichLamViec ll
JOIN DanhMuc.KhuVuc kv ON ll.IdKhuVuc = kv.Id
JOIN NhanSu.YeuCauChungChi_KhuVuc yc ON kv.Id = yc.IdKhuVuc AND yc.BatBuoc = 1
JOIN DoiTac.NhanVien nv ON ll.IdNhanVien = nv.IdDoiTac
JOIN DoiTac.ThongTin dt ON nv.IdDoiTac = dt.Id
WHERE NOT EXISTS (
    SELECT 1 FROM NhanSu.ChungChiNhanVien cc
    WHERE cc.IdNhanVien = ll.IdNhanVien
      AND cc.LoaiChungChi = yc.LoaiChungChi
      AND cc.NgayHetHan >= ll.NgayLam
);
GO

-- Báo cáo: Xem 1 bill đầy đủ - gộp tất cả satellite vào 1 View duy nhất
-- Dev C# chỉ cần: SELECT * FROM BanHang.V_BillChiTiet WHERE IdDonHang = @id
-- Ghi chú: Vé (VeDienTu) là quan hệ 1:N (mua 3 vé = 3 dòng), các satellite khác là 1:1
CREATE VIEW BanHang.V_BillChiTiet AS
SELECT 
    ctdh.Id, ctdh.IdDonHang, ctdh.IdSanPham,
    sp.TenSanPham, sp.LoaiSanPham,
    ctdh.SoLuong, ctdh.DonGiaThucTe, ctdh.ThanhTien,
    ctdh.GhiChu,
    -- Satellite: Vé (1:N - mỗi vé 1 dòng)
    ve.Id AS IdVe, ve.MaVach, ve.TrangThai AS TrangThaiVe, ve.SoLuotConLai,
    -- Satellite: Thuê Tủ Đồ

    -- Satellite: Thuê Đồ (Phao, ván lướt sóng...)
    tdc.ThoiGianThue AS ThoiGianThueDo, tdc.TienCoc AS TienCocThueDo,
    tdc.TrangThai AS TrangThaiThueDo,
    -- Satellite: Đặt Phòng Khách Sạn
    cdp.IdPhong, p.MaPhong, cdp.TrangThai AS TrangThaiDatPhong,
    cdp.NgayCheckIn, cdp.NgayCheckOut,
    -- Satellite: Đặt Chỗ Xem Show
    dcs.IdLichBieuDien, dcs.SoGhe,
    dcs.TrangThai AS TrangThaiChoShow,
    -- Satellite: Đặt Chỗ Xem Đua
    dcd.IdLichThiDau, dcd.IdViTriNgoi,
    dcd.TrangThai AS TrangThaiChoDua,
    -- Satellite: Cho Thú Ăn
    cta.IdChuongTrai, cta.SoLuotMua,
    -- Satellite: Gửi Xe (link qua IdChiTietDonHang mới)
    lvr.BienSoXe, lvr.LoaiXe, lvr.ThoiGianVao AS ThoiGianVaoBai,
    lvr.TrangThai AS TrangThaiGuiXe
FROM BanHang.ChiTietDonHang ctdh
JOIN DanhMuc.SanPham sp ON ctdh.IdSanPham = sp.Id
LEFT JOIN BanHang.VeDienTu ve ON ve.IdChiTietDonHang = ctdh.Id

LEFT JOIN BanHang.ThueDoChiTiet tdc ON tdc.IdChiTietDonHang = ctdh.Id
LEFT JOIN BanHang.ChiTietDatPhong cdp ON cdp.IdChiTietDonHang = ctdh.Id
    LEFT JOIN DanhMuc.Phong p ON cdp.IdPhong = p.Id
LEFT JOIN BanHang.DatChoXemShow dcs ON dcs.IdChiTietDonHang = ctdh.Id
LEFT JOIN BanHang.DatChoXemDua dcd ON dcd.IdChiTietDonHang = ctdh.Id
LEFT JOIN BanHang.DatChoThuAn cta ON cta.IdChiTietDonHang = ctdh.Id
LEFT JOIN VanHanh.LuotVaoRaBaiXe lvr ON lvr.IdChiTietDonHang = ctdh.Id;
GO


-- PHẦN 10B: TRIGGER BẢO VỆ DỮ LIỆU
-- Công dụng: Không cho nhập 2 mức giá đá nhau trong cùng 1 khoảng thời gian
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

-- PHẦN 11A: STORED PROCEDURES (Bảo đảm tính toàn vẹn Transaction khi thanh toán / hủy)
CREATE PROCEDURE BanHang.sp_HuyDonHangDongThoi 
    @IdDonHang INT,
    @IdNguoiHuy INT,
    @LyDo NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1. Cập nhật trạng thái DonHang
        UPDATE BanHang.DonHang SET TrangThai = 'DaHuy', GhiChu = @LyDo
        WHERE Id = @IdDonHang AND TrangThai IN ('ChoThanhToan', 'DangPhucVu');

        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR (N'Đơn hàng không thể hủy (đã thanh toán, đã hủy hoặc không tồn tại).', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
        
        -- 2. Hủy Vé chưa sử dụng
        UPDATE BanHang.VeDienTu SET TrangThai = 'DaHuy', LyDoHuy = @LyDo, IdNguoiDuyetHuy = @IdNguoiHuy
        WHERE IdChiTietDonHang IN (SELECT Id FROM BanHang.ChiTietDonHang WHERE IdDonHang = @IdDonHang) AND TrangThai = 'ChuaSuDung';

        -- 3. Hủy Đặt Phòng chưa nhận
        UPDATE cdp SET TrangThai = 'DaHuy'
        FROM BanHang.ChiTietDatPhong cdp
        JOIN BanHang.ChiTietDonHang ct ON cdp.IdChiTietDonHang = ct.Id
        WHERE ct.IdDonHang = @IdDonHang AND cdp.TrangThai = 'ChoDen';

        -- 4. Hủy Đặt Chỗ Show/Dua
        UPDATE cs SET TrangThai = 'DaHuy'
        FROM BanHang.DatChoXemShow cs JOIN BanHang.ChiTietDonHang ct ON cs.IdChiTietDonHang = ct.Id WHERE ct.IdDonHang = @IdDonHang AND cs.TrangThai = 'DaDat';
        
        -- Giải phóng Tạm Giữ Tồn Kho
        UPDATE Kho.TamGiuTonKho SET TrangThai = 'DaHuy' WHERE IdDonHangNhap = @IdDonHang;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- Thanh Toán Đơn Hàng
-- Validate @SoTien >= TongThanhToan trước khi chốt đơn + xuất kho.
-- Trước đây SP có thể gọi với @SoTien=0 và đơn vẫn chuyển DaThanhToan.
CREATE PROCEDURE BanHang.sp_ThanhToanDonHang
    @IdDonHang INT,
    @IdKhachHang INT = NULL,
    @IdNguoiThu INT,
    @PhuongThuc VARCHAR(50),
    @SoTien DECIMAL(18,0)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- 0. Validate: tiền khách trả phải đủ
        DECLARE @TongBill DECIMAL(18,0) = (
            SELECT ISNULL(TongThanhToan, 0) FROM BanHang.DonHang WHERE Id = @IdDonHang
        );
        IF @SoTien < @TongBill
        BEGIN
            DECLARE @msg NVARCHAR(MAX) = N'Số tiền thanh toán (' + CAST(@SoTien AS VARCHAR) + N') không đủ so với tổng hóa đơn (' + CAST(@TongBill AS VARCHAR) + N').';
            RAISERROR (@msg, 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- 1. Chốt đơn
        UPDATE BanHang.DonHang SET TrangThai = 'DaThanhToan' WHERE Id = @IdDonHang;

        -- 2. Đẻ dữ liệu tài chính
        DECLARE @MaTaiChinh VARCHAR(30) = 'PT_' + CONVERT(VARCHAR, @IdDonHang) + '_' + CONVERT(VARCHAR, GETDATE(), 112) + '_' + REPLACE(CONVERT(VARCHAR, GETDATE(), 108), ':', '');
        INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao)
        VALUES (@MaTaiChinh, 'THU_THANHTOAN', @IdKhachHang, @IdDonHang, @SoTien, @PhuongThuc, @IdNguoiThu);
        
        DECLARE @IdChungTu INT = SCOPE_IDENTITY();
        INSERT INTO TaiChinh.ChiTietThanhToan (IdChungTu, PhuongThuc, SoTien) VALUES (@IdChungTu, @PhuongThuc, @SoTien);

        -- 3. Xuất kho cho hàng hóa vật tư (đồ ăn, nước uống, hàng hóa)
        DECLARE @IdKhoBan INT = (SELECT ptn.IdKhoBan FROM BanHang.DonHang dh JOIN BanHang.PhienThuNgan ptn ON dh.IdPhienThuNgan = ptn.Id WHERE dh.Id = @IdDonHang);
        DECLARE @IdKhoKhach INT = (SELECT Id FROM Kho.KhoHang WHERE MaKho = 'KHO_KHACH');

        IF @IdKhoBan IS NOT NULL AND @IdKhoKhach IS NOT NULL
        BEGIN
            -- Tạo chứng từ kho xuất bán (V3: không có IdKhoXuat/IdKhoNhap ở header)
            DECLARE @MaChungTuKho VARCHAR(30) = 'XB_' + CONVERT(VARCHAR, @IdDonHang) + '_' + CONVERT(VARCHAR, GETDATE(), 112);
            INSERT INTO Kho.ChungTu (MaChungTu, LoaiChungTu, IdDonHang, TrangThai, IdNguoiTao)
            VALUES (@MaChungTuKho, 'XUAT_BAN', @IdDonHang, 'DaDuyet', @IdNguoiThu);

            DECLARE @IdChungTuKho INT = SCOPE_IDENTITY();

            -- Insert ChiTietChungTu với IdKhoXuat/IdKhoNhap ở từng dòng (V3)
            INSERT INTO Kho.ChiTietChungTu (IdChungTu, IdSanPham, IdLoHang, IdKhoXuat, IdKhoNhap, SoLuong, DonGia)
            SELECT @IdChungTuKho, ct.IdSanPham, ct.IdLoHang, @IdKhoBan, @IdKhoKhach, ct.SoLuong, ct.DonGiaThucTe
            FROM BanHang.ChiTietDonHang ct
            JOIN DanhMuc.SanPham sp ON ct.IdSanPham = sp.Id
            WHERE ct.IdDonHang = @IdDonHang AND (sp.LaVatTu = 1 OR sp.CanQuanLyLo = 1);

            -- Xóa tạm giữ tồn kho
            UPDATE Kho.TamGiuTonKho SET TrangThai = 'DaXuatLai' WHERE IdDonHangNhap = @IdDonHang AND TrangThai = 'DangGiu';
        END


        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO


-- PHẦN 11: DỮ LIỆU MẪU BAN ĐẦU DÙNG ĐỂ CHUYỂN GIAO PHẦN MỀM


-- Các Vai trò/Chức vụ chính
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

-- Các Loại Đơn Vị Tính
INSERT INTO DanhMuc.DonViTinh (MaDonVi, TenDonVi) VALUES
('Cai',   N'Cái'),    ('Lon',   N'Lon'),     ('Chai',  N'Chai'),
('Kg',    N'Kilogram'), ('Lit',  N'Lít'),     ('Hop',   N'Hộp'),
('Thung', N'Thùng'),  ('Suat',  N'Suất'),    ('Ve',    N'Vé'),
('Luot',  N'Lượt'),   ('Dem',   N'Đêm'),     ('Gio',   N'Giờ'),
('Phut',  N'Phút'),   ('GoiDv', N'Gói DV');
GO

-- Ca Làm Việc Mẫu
INSERT INTO NhanSu.CaLamMau (TenCa, GioBatDau, GioKetThuc, LoaiCa) VALUES
(N'Sáng',        '06:00', '14:00', 'ChinhThuc'),
(N'Chiều',       '14:00', '22:00', 'ChinhThuc'),
(N'Đêm',         '22:00', '06:00', 'ChinhThuc'),
(N'Hành chính',  '08:00', '17:00', 'ChinhThuc'),
(N'PartTime sáng', '06:00', '10:00', 'BanThoi'),
(N'PartTime chiều','14:00', '18:00', 'BanThoi'),
(N'Toàn ngày',   '06:00', '22:00', 'DacBiet');
GO

-- Các Mức Thuế Cơ Bản
INSERT INTO DanhMuc.CauHinhThue (MaThue, TenThue, TyLePhanTram, ApDungChoLoaiSP, HieuLucTu) VALUES
('VAT_10',   N'VAT 10%',           10.00, NULL,      '2024-01-01'),
('VAT_8',    N'VAT 8% (giảm)',     8.00,  NULL,      '2024-01-01'),
('PHI_DV_5', N'Phí dịch vụ 5%',   5.00,  'LuuTru',  '2024-01-01');
GO
-- Cấu Hình Tham Số Hệ Thống
-- DIEM_QUY_DOI_1000D: key cũ giữ lại để tương thích ngược
INSERT INTO HeThong.CauHinh VALUES ('DIEM_QUY_DOI_1000D', '1', N'1000 VNĐ = 1 điểm (key cũ)', GETDATE(), NULL);
-- DIEM_QUY_DOI: key đúng mà BUS_POS.cs và BUS_LuuTru_Booking.cs đang đọc
INSERT INTO HeThong.CauHinh VALUES ('DIEM_QUY_DOI', '1000', N'Tỷ lệ quy đổi điểm: 1000 VNĐ = 1 điểm', GETDATE(), NULL);
INSERT INTO HeThong.CauHinh VALUES ('DIEM_HET_HAN_THANG', '12', N'Điểm hết hạn sau N tháng', GETDATE(), NULL);
INSERT INTO HeThong.CauHinh VALUES ('SO_PHUT_GIU_TON_KHO', '15', N'Giám giữ tồn kho (phút)', GETDATE(), NULL);
INSERT INTO HeThong.CauHinh VALUES ('HE_SO_TANG_CA', '1.5', N'Hệ số lương tăng ca', GETDATE(), NULL);
INSERT INTO HeThong.CauHinh VALUES ('NGAY_PHEP_MAC_DINH', '12', N'Số ngày phép/năm mặc định', GETDATE(), NULL);
INSERT INTO HeThong.CauHinh VALUES ('NGON_NGU_MAC_DINH', 'vi', N'Ngôn ngữ mặc định', GETDATE(), NULL);

-- Chiết khấu theo hạng thành viên (% giảm trên tổng đơn, KHÔNG áp cho F&B)
INSERT INTO HeThong.CauHinh VALUES ('CK_HANG_Bac', '5',  N'Chiết khấu hạng Bạc: 5%', GETDATE(), NULL);
INSERT INTO HeThong.CauHinh VALUES ('CK_HANG_Vang',  '10', N'Chiết khấu hạng Vàng: 10%', GETDATE(), NULL);
INSERT INTO HeThong.CauHinh VALUES ('CK_HANG_KimCuong',       '15', N'Chiết khấu hạng Kim Cương: 15%', GETDATE(), NULL);

-- Giới hạn dùng điểm thanh toán (% tối đa trên tổng đơn)
INSERT INTO HeThong.CauHinh VALUES ('DIEM_CAP_PHAN_TRAM', '50', N'Dùng điểm tối đa 50% tổng đơn', GETDATE(), NULL);

-- Giới hạn thời gian hoàn trả (đơn vị: GIỜ, 0 = không cho hoàn)
-- Vé khu du lịch: chỉ có giá trị trong ngày, KHÔNG được hoàn trả
INSERT INTO HeThong.CauHinh VALUES ('HOAN_GIOI_HAN_GIO_VE',      '0',   N'Vé: không cho hoàn (0 = cấm hoàn)', GETDATE(), NULL);
-- Hàng hóa/Đồ lưu niệm: cho phép hoàn trong 168 giờ (7 ngày)
INSERT INTO HeThong.CauHinh VALUES ('HOAN_GIOI_HAN_GIO_HANGHOA', '168', N'Hàng hóa: hoàn trong 7 ngày (168h)', GETDATE(), NULL);
GO

-- Từ Điển Hệ Thống (Trạng thái cảnh báo, màu sắc, phân loại dùng chung cho form)

-- Giới Tính
INSERT INTO HeThong.TuDien VALUES ('GIOI_TINH', 'Nam',  N'Nam', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('GIOI_TINH', 'Nu',   N'Nữ', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('GIOI_TINH', 'Khac', N'Khác', 3, NULL, NULL, 1);

-- Trạng Thái Nhân Viên
INSERT INTO HeThong.TuDien VALUES ('NV_TRANG_THAI', 'DangLam',    N'Đang làm', 1, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_TRANG_THAI', 'NghiPhep',   N'Nghỉ phép', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_TRANG_THAI', 'DinhChi',    N'Đình chỉ', 3, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_TRANG_THAI', 'DaNghiViec', N'Đã nghỉ việc', 4, '#6C757D', NULL, 1);

-- Loại Khối
INSERT INTO HeThong.TuDien VALUES ('NV_LOAI_KHOI', 'VanPhong',  N'Văn phòng', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_LOAI_KHOI', 'ThucDia',   N'Thực địa', 2, NULL, NULL, 1);

-- Loại Hợp Đồng
INSERT INTO HeThong.TuDien VALUES ('NV_LOAI_HOP_DONG', 'FullTime', N'Toàn thời gian', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_LOAI_HOP_DONG', 'PartTime', N'Bán thời gian', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_LOAI_HOP_DONG', 'TheoMua',  N'Theo mùa', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_LOAI_HOP_DONG', 'Intern',   N'Thực tập', 4, NULL, NULL, 1);

INSERT INTO HeThong.TuDien (NhomMa, Ma, NhanHienThi, ThuTu, MauSac, BieuTuong, ConHoatDong) VALUES
('HD_TRANG_THAI',      'DangHieuLuc', N'Đang hiệu lực', 1, '#5E9B76', NULL, 1),
('HD_TRANG_THAI',      'HetHan',      N'Hết hạn',       2, '#C05050', NULL, 1),
('HD_TRANG_THAI',      'ChamDut',     N'Chấm dứt',      3, '#6C757D', NULL, 1),
('HD_TRANG_THAI',      'ChuaKyLai',   N'Chờ ký lại',    4, '#D4A843', NULL, 1),
('NGHIBU_TRANG_THAI',  'ChoDuyet',    N'Chờ duyệt',     1, '#D4A843', NULL, 1),
('NGHIBU_TRANG_THAI',  'DaDuyet',     N'Đã duyệt',      2, '#5E9B76', NULL, 1),
('NGHIBU_TRANG_THAI',  'TuChoi',      N'Từ chối',       3, '#C05050', NULL, 1),
('NGHIBU_TRANG_THAI',  'DaSuDung',    N'Đã sử dụng',    4, '#6C757D', NULL, 1),
('DOI_CA_TRANG_THAI',  'ChoDuyet',    N'Chờ duyệt',     1, '#D4A843', NULL, 1),
('DOI_CA_TRANG_THAI',  'DaDuyet',     N'Đã duyệt',      2, '#5E9B76', NULL, 1),
('DOI_CA_TRANG_THAI',  'TuChoi',      N'Từ chối',       3, '#C05050', NULL, 1),
('PHU_CAP_LOAI',       'AnCa',        N'Phụ cấp ăn ca',       1, NULL, NULL, 1),
('PHU_CAP_LOAI',       'DiLai',       N'Phụ cấp đi lại',      2, NULL, NULL, 1),
('PHU_CAP_LOAI',       'DienThoai',   N'Phụ cấp điện thoại',  3, NULL, NULL, 1),
('PHU_CAP_LOAI',       'TrachNhiem',  N'Phụ cấp trách nhiệm', 4, NULL, NULL, 1),
('PHU_CAP_LOAI',       'NguyCap',     N'Phụ cấp nguy hiểm',   5, '#D4A843', NULL, 1),
('PHU_CAP_LOAI',       'NhaCo',       N'Phụ cấp nhà ở',       6, NULL, NULL, 1),
('PHU_CAP_LOAI',       'Khac',        N'Phụ cấp khác',        7, NULL, NULL, 1),
('DVDua_TRANG_THAI',   'SanSang',     N'Sẵn sàng',      1, '#5E9B76', NULL, 1),
('DVDua_TRANG_THAI',   'DangThi',     N'Đang thi đấu',  2, '#D4A843', NULL, 1),
('DVDua_TRANG_THAI',   'NghiNgoi',    N'Nghỉ ngơi',     3, '#5B9BD5', NULL, 1),
('DVDua_TRANG_THAI',   'ChanThuong',  N'Chấn thương',   4, '#C05050', NULL, 1),
('DVDua_TRANG_THAI',   'Nghi',        N'Đã nghỉ hưu',   5, '#6C757D', NULL, 1);

-- Nhóm Công Việc
INSERT INTO HeThong.TuDien VALUES ('NV_NHOM_CONG_VIEC', 'ThuongNgay', N'Thường ngày', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_NHOM_CONG_VIEC', 'NguyCap1',   N'Nguy cấp cấp 1', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NV_NHOM_CONG_VIEC', 'NguyCap2',   N'Nguy cấp cấp 2', 3, '#C05050', NULL, 1);

-- Loại Khách Hàng
INSERT INTO HeThong.TuDien VALUES ('KH_LOAI_KHACH', 'CaNhan',          N'Cá nhân', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_LOAI_KHACH', 'Doan',            N'Đoàn', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_LOAI_KHACH', 'DoanhNghiep',     N'Doanh nghiệp', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_LOAI_KHACH', 'HocSinhSinhVien', N'Học sinh/Sinh viên', 4, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_LOAI_KHACH', 'NoiBo',           N'Nội bộ', 7, NULL, NULL, 1);

-- Hạng Thành Viên
INSERT INTO HeThong.TuDien VALUES ('KH_HANG_TV', 'Thuong', N'Thường (Standard)', 0, '#FFFFFF', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_HANG_TV', 'Bac', N'Bạc (Silver)', 1, '#C0C0C0', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_HANG_TV', 'Vang',  N'Vàng (Gold)', 2, '#FFD700', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KH_HANG_TV', 'KimCuong',       N'Kim Cương (Diamond)', 3, '#B9F2FF', NULL, 1);

-- Trạng Thái Phòng
INSERT INTO HeThong.TuDien VALUES ('PHONG_TRANG_THAI', 'Trong',      N'Trống', 1, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHONG_TRANG_THAI', 'DaDat',      N'Đã đặt', 2, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHONG_TRANG_THAI', 'DangSuDung', N'Đang sử dụng', 3, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHONG_TRANG_THAI', 'DonDep',     N'Đang dọn dẹp', 4, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHONG_TRANG_THAI', 'BaoTri',     N'Bảo trì', 5, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHONG_TRANG_THAI', 'KhoaDai',    N'Khóa dài hạn', 6, '#343A40', NULL, 1);

-- Trạng Thái Bàn Ăn
INSERT INTO HeThong.TuDien VALUES ('BAN_AN_TRANG_THAI', 'Trong',      N'Trống', 1, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BAN_AN_TRANG_THAI', 'DaDat',      N'Đã đặt', 2, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BAN_AN_TRANG_THAI', 'DangSuDung', N'Đang sử dụng', 3, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BAN_AN_TRANG_THAI', 'DonDep',     N'Đang dọn', 4, '#6C757D', NULL, 1);

-- Trạng Thái Đơn Hàng
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'DangPhucVu',     N'Đang phục vụ', 0, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'ChoThanhToan',   N'Chờ thanh toán', 1, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'DaThanhToan',    N'Đã thanh toán', 2, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'DangXuLy',       N'Đang xử lý', 3, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'GhiNoCongTy',    N'Ghi nợ công ty', 4, '#8E7CC3', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'DaHuy',          N'Đã hủy', 5, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'HoanTienMotPhan',N'Hoàn tiền 1 phần', 6, '#CC8844', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'DaHoanTien',     N'Đã hoàn tiền', 7, '#6C757D', NULL, 1);
-- Fix #6: BUS_LuuTru_Booking dùng AppConstants.TrangThaiDonHang.MotPhan = "MotPhan"
-- Thiếu → DonHang.TrangThai CHECK constraint sẽ FAIL khi partial check-out
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'MotPhan',        N'Thanh toán một phần (partial check-out)', 8, '#CC8844', NULL, 1);
-- Fix #7: BUS_LuuTru_Booking dùng AppConstants.TrangThaiDonHang.DangMo = "DangMo"
-- Thiếu → CHECK constraint fail khi tạo bill lưu trú mới
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'DangMo',         N'Đang mở (bill lưu trú chưa check-out)', 9, '#5B9BD5', NULL, 1);

-- Nguồn Bán
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_NGUON', 'TrucTiep', N'Trực tiếp', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_NGUON', 'Web',      N'Website', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DON_HANG_NGUON', 'Kiosk',    N'Kiosk', 3, NULL, NULL, 1);

-- Trạng Thái Vé
INSERT INTO HeThong.TuDien VALUES ('VE_TRANG_THAI', 'ChuaSuDung', N'Chưa sử dụng', 1, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('VE_TRANG_THAI', 'DaSuDung',   N'Đã sử dụng', 2, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('VE_TRANG_THAI', 'HetLuot',    N'Hết lượt', 3, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('VE_TRANG_THAI', 'HetHan',     N'Hết hạn', 4, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('VE_TRANG_THAI', 'DaHuy',      N'Đã hủy', 5, '#343A40', NULL, 1);

-- Loại Chứng Từ Kho
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'NHAP_MUA',    N'Nhập mua từ NCC', 1, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'XUAT_BAN',    N'Xuất bán cho khách', 2, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'TRA_NCC',     N'Trả hàng cho NCC', 3, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'KHACH_TRA',   N'Khách trả hàng', 4, '#CC8844', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'HUY_HONG',    N'Hủy / Hao hụt', 5, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'CHUYEN_KHO',  N'Chuyển kho', 6, '#8E7CC3', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'KIEM_KE',     N'Kiểm kê điều chỉnh', 7, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'XUAT_BAOTRI', N'Xuất vật tư bảo trì', 8, '#5DADA8', NULL, 1);

-- Trạng Thái Kho
INSERT INTO HeThong.TuDien VALUES ('KHO_TRANG_THAI', 'HoatDong',      N'Đang hoạt động', 1, '#198754', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KHO_TRANG_THAI', 'NgungHoatDong', N'Ngừng hoạt động', 2, '#DC3545', NULL, 1);

-- Trạng Thái Chứng Từ Kho
INSERT INTO HeThong.TuDien VALUES ('CHUNG_TU_KHO_TRANG_THAI', 'Nhap',    N'Mới nhập', 1, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CHUNG_TU_KHO_TRANG_THAI', 'ChoDuyet',N'Chờ duyệt', 2, '#FFC107', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CHUNG_TU_KHO_TRANG_THAI', 'DaDuyet', N'Đã duyệt', 3, '#198754', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CHUNG_TU_KHO_TRANG_THAI', 'DaHuy',   N'Đã hủy', 4, '#DC3545', NULL, 1);

-- Loại Chứng Từ Tài Chính
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'THU_THANHTOAN', N'Thu thanh toán', 1, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'THU_COC',       N'Thu tiền cọc', 2, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'THU_NAP_VI',    N'Nạp ví RFID', 3, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'CHI_NHAP_HANG', N'Chi nhập hàng', 4, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'CHI_LUONG',     N'Chi lương', 5, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'CHI_BAO_TRI',   N'Chi bảo trì', 6, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'HOAN_TIEN',     N'Hoàn tiền khách', 7, '#CC8844', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'HOAN_COC',      N'Hoàn tiền cọc', 8, '#6C757D', NULL, 1);

-- Phương Thức Thanh Toán
INSERT INTO HeThong.TuDien VALUES ('PHUONG_THUC_TT', 'TienMat',     N'Tiền mặt', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHUONG_THUC_TT', 'ChuyenKhoan', N'Chuyển khoản', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHUONG_THUC_TT', 'ViRFID',      N'Ví RFID', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHUONG_THUC_TT', 'QR',          N'QR Code', 4, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHUONG_THUC_TT', 'MoMo',        N'Ví MoMo', 5, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PHUONG_THUC_TT', 'TheNganHang', N'Thẻ ngân hàng', 6, NULL, NULL, 1);

-- Loại Sản Phẩm (nhóm cũ SAN_PHAM_LOAI đã xóa → dùng SP_LOAI ở Phần 12, đang gắn CHECK constraint)

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
INSERT INTO HeThong.TuDien VALUES ('MUC_DO', 'Nhe',       N'Nhẹ', 1, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('MUC_DO', 'TrungBinh', N'Trung bình', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('MUC_DO', 'NangNe',    N'Nặng nề', 3, '#C05050', NULL, 1);
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
INSERT INTO HeThong.TuDien VALUES ('KY_LUAT_HINH_THUC', 'CanhCao',         N'Cảnh cáo', 1, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KY_LUAT_HINH_THUC', 'TruLuong',        N'Trừ lương', 2, '#CC8844', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KY_LUAT_HINH_THUC', 'DinhChiCoLuong',  N'Đình chỉ có lương', 3, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KY_LUAT_HINH_THUC', 'SaThai',          N'Sa thải', 4, '#343A40', NULL, 1);

-- Kết Quả Quét Vé
INSERT INTO HeThong.TuDien VALUES ('KET_QUA_QUET_VE', 'ThanhCong', N'Thành công', 1, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KET_QUA_QUET_VE', 'SaiVe',     N'Sai vé', 2, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KET_QUA_QUET_VE', 'DaHetLuot', N'Đã hết lượt', 3, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KET_QUA_QUET_VE', 'VeHetHan',  N'Vé hết hạn', 4, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KET_QUA_QUET_VE', 'DaHuy',     N'Đã hủy', 5, '#343A40', NULL, 1);
GO

-- Các Luồng Chuyển Trạng Thái Mẫu cho phần mềm hiểu

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
('Phong', 'Trong',      'DangSuDung', N'Check-in khách'),
('Phong', 'Trong',      'BaoTri',     N'Chuyển bảo trì'),
('Phong', 'DangSuDung', 'DonDep',     N'Check-out khách'),
('Phong', 'DonDep',     'Trong',      N'Dọn xong'),
('Phong', 'BaoTri',     'Trong',      N'Bảo trì xong');

-- Chứng Từ Kho
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('ChungTuKho', 'Moi',      'ChoDuyet',  N'Gửi duyệt'),
('ChungTuKho', 'ChoDuyet', 'DaDuyet',   N'Người duyệt phê duyệt'),
('ChungTuKho', 'ChoDuyet', 'DaHuy',     N'Từ chối'),
('ChungTuKho', 'Moi',      'DaHuy',     N'Tự hủy');

-- DonHang: thêm đường vào DangXuLy + GhiNoCongTy + DangPhucVu
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('DonHang', 'ChoThanhToan', 'DangXuLy',      N'Bắt đầu xử lý (đoàn/đặt trước)'),
('DonHang', 'ChoThanhToan', 'DangPhucVu',    N'Bắt đầu phục vụ tại chỗ'),
('DonHang', 'DangPhucVu',   'DaThanhToan',    N'Thanh toán ngay khi đang phục vụ'),
('DonHang', 'DaThanhToan',  'GhiNoCongTy',   N'Ghi nợ công ty/đoàn B2B'),
('DonHang', 'GhiNoCongTy',  'DaThanhToan',   N'Đoàn thanh toán nợ');

-- Đặt Phòng (Trạng thái khớp AppConstants.TrangThaiBooking trong C#)
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('DatPhong', 'DatTruoc', 'DangO',   N'Khách check-in'),
('DatPhong', 'DatTruoc', 'DaHuy',   N'Hủy đặt phòng'),
('DatPhong', 'DangO',    'DaTra',   N'Khách check-out');

-- Bàn Ăn (trạng thái vật lý của cái bàn)
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('BanAn', 'Trong',      'DaDat',      N'Đặt bàn'),
('BanAn', 'DaDat',      'DangSuDung', N'Khách ngồi vào'),
('BanAn', 'DaDat',      'Trong',      N'Hủy đặt'),
('BanAn', 'DangSuDung', 'DonDep',     N'Khách rời bàn'),
('BanAn', 'DonDep',     'Trong',      N'Dọn xong');

-- Đặt Bàn (trạng thái của lượt đặt)
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('DatBan', 'ChoDen',  'DenRoi',   N'Khách tới'),
('DatBan', 'ChoDen',  'DaHuy',    N'Hủy đặt bàn'),
('DatBan', 'ChoDen',  'KhongDen', N'Quá giờ không đến');

-- Vé Điện Tử
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('VeDienTu', 'ChuaSuDung', 'DaSuDung', N'Quét vé lần đầu'),
('VeDienTu', 'DaSuDung',   'HetLuot',  N'Hết lượt chơi'),
('VeDienTu', 'ChuaSuDung', 'DaHuy',    N'Hủy vé chưa dùng'),
('VeDienTu', 'ChuaSuDung', 'HetHan',   N'Hết hạn sử dụng'),
('VeDienTu', 'DaSuDung',   'HetHan',   N'Hết hạn khi đang dùng');

-- Thẻ RFID
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('TheRFID', 'ChuaKichHoat', 'DangDung', N'Kích hoạt vòng tay'),
('TheRFID', 'DangDung',     'DaKhoa',   N'Khóa vòng (mất/hỏng)'),
('TheRFID', 'DangDung',     'DaTra',    N'Khách trả vòng'),
('TheRFID', 'DaKhoa',       'DangDung', N'Mở khóa lại');

-- Bảo Trì
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('BaoTri', 'KeHoach',      'DangThucHien', N'Bắt đầu bảo trì'),
('BaoTri', 'DangThucHien', 'HoanThanh',    N'Hoàn thành bảo trì'),
('BaoTri', 'KeHoach',      'DaHuy',        N'Hủy kế hoạch'),
('BaoTri', 'DangThucHien', 'DaHuy',        N'Hủy giữa chừng');

-- Sự Cố
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('SuCo', 'MoiGhi',   'DangXuLy', N'Tiếp nhận xử lý'),
('SuCo', 'DangXuLy', 'DaXuLy',   N'Xử lý xong'),
('SuCo', 'DaXuLy',   'DaDong',   N'Đóng hồ sơ');

-- Thuê Đồ / Tủ Đồ / Chòi Nghỉ (dùng chung ThucThe = 'ThueDo')
-- Edge case: khách mất đồ thuê → không thể "DaTra" → cần đường MatDo + BoiThuong
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('ThueDo', 'DangThue',    'DaTra',       N'Khách trả đồ đúng hạn'),
('ThueDo', 'DangThue',    'QuaHan',      N'Quá giờ trả, chưa trả'),
('ThueDo', 'QuaHan',      'DaTra',       N'Trả muộn (có phạt)'),
('ThueDo', 'DangThue',    'MatDo',       N'Khách mất/hỏng đồ thuê'),
('ThueDo', 'QuaHan',      'MatDo',       N'Quá hạn + mất đồ'),
('ThueDo', 'MatDo',       'DaBoiThuong', N'Khách bồi thường xong'),
('ThueDo', 'DangThue',    'DaHuy',       N'Hủy thuê (trả ngay, hoàn tiền)');

-- Bảng Lương
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('BangLuong', 'DangTinh',  'ChoDuyet',  N'Gửi duyệt lương'),
('BangLuong', 'ChoDuyet',  'DaDuyet',   N'Duyệt bảng lương'),
('BangLuong', 'ChoDuyet',  'DangTinh',  N'Trả về tính lại'),
('BangLuong', 'DaDuyet',   'DaChi',     N'Đã chuyển lương');

-- Đơn Xin Nghỉ
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('DonXinNghi', 'ChoDuyet', 'DaDuyet',  N'Duyệt đơn nghỉ'),
('DonXinNghi', 'ChoDuyet', 'TuChoi',   N'Từ chối đơn nghỉ');

-- Lệnh BEO (Bếp đoàn)
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('LenhBEO', 'ChuanBi',     'DangPhucVu', N'Bắt đầu phục vụ'),
('LenhBEO', 'DangPhucVu',  'HoanThanh',  N'Phục vụ xong'),
('LenhBEO', 'ChuanBi',     'DaHuy',      N'Hủy BEO');

-- Phân Công Buồng Phòng (Housekeeping)
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('BuongPhong', 'DaGiao',   'DangLam',   N'Bắt đầu dọn'),
('BuongPhong', 'DangLam',  'HoanThanh', N'Dọn xong');

-- Đồ Thất Lạc
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('ThatLac', 'ChuaTraLai', 'DaTraLai', N'Đã trả lại chủ'),
('ThatLac', 'ChuaTraLai', 'DaHuy',    N'Xử lý (quá 3 tháng)');

-- Ca Trực Cứu Hộ
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('CaTruc', 'ChuaBatDau', 'DangTruc',  N'Bắt đầu trực'),
('CaTruc', 'DangTruc',   'HoanThanh', N'Hết ca');

-- Lịch Tạo Sóng
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('TaoSong', 'KeHoach',  'DangChay',  N'Bật máy'),
('TaoSong', 'DangChay', 'HoanThanh', N'Tắt máy'),
('TaoSong', 'KeHoach',  'DaHuy',     N'Hủy lịch');

-- Gửi Xe (Lượt Vào Ra Bãi)
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('GuiXe', 'DangGui', 'DaRa', N'Xe đã ra');

-- Giải Đua
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('GiaiDua', 'SapDienRa',  'DangDienRa', N'Khai mạc'),
('GiaiDua', 'DangDienRa', 'KetThuc',    N'Bế mạc'),
('GiaiDua', 'SapDienRa',  'DaHuy',      N'Hủy giải');

-- Lịch Thi Đấu (từng lượt đua)
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('LichDua', 'ChuaBatDau', 'DangDua',  N'Xuất phát'),
('LichDua', 'DangDua',    'KetThuc',  N'Về đích'),
('LichDua', 'ChuaBatDau', 'DaHuy',    N'Hủy lượt');

-- Ngựa Đua
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('DongVatDua', 'SanSang',  'DieuTri', N'Vật bị thương'),
('DongVatDua', 'DieuTri',  'SanSang', N'Hồi phục'),
('DongVatDua', 'SanSang',  'NghiHuu', N'Nghỉ hưu');

-- Phương Tiện Đua
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('PhuongTienDua', 'SanSang', 'BaoTri',  N'Đưa bảo trì'),
('PhuongTienDua', 'BaoTri',  'SanSang', N'Sửa xong'),
('PhuongTienDua', 'SanSang', 'Hong',    N'Hỏng nặng');

-- Lô Hàng
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('LoHang', 'ConHang', 'HetHang', N'Xuất hết'),
('LoHang', 'ConHang', 'HetHan',  N'Quá hạn sử dụng'),
('LoHang', 'ConHang', 'DaHuy',   N'Hủy lô');

-- Tạm Giữ Tồn Kho
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('TamGiuTonKho', 'DangGiu',   'DaXuatLai', N'Xuất lại kho'),
('TamGiuTonKho', 'DangGiu',   'DaHuy',     N'Hủy hàng giữ');

-- Phiên Thu Ngân
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('PhienThuNgan', 'DangMo',  'DaDong',  N'Đóng ca'),
('PhienThuNgan', 'DaDong',  'DaDuyet', N'Quản lý duyệt');

-- Nhật Ký Y Tế Thú
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('YTeThu', 'DangDieuTri', 'DaKhoi',  N'Hồi phục'),
('YTeThu', 'DangDieuTri', 'DaChet',  N'Không qua khỏi');
GO

-- Tạo Dữ Liệu Phân Quyền Mẫu
INSERT INTO NhanSu.QuyenHan (MaQuyen, TenQuyen, NhomQuyen) VALUES 
('ALL', N'Toàn quyền hệ thống', 'SYSTEM'),
('POS_BANHANG', N'Bán hàng POS', 'POS'),
('POS_HOANHANG', N'Hoàn trả hàng', 'POS'),
('KH_XEM', N'Xem danh sách khách hàng', 'KHACH_HANG'),
('KH_THEM', N'Thêm mới khách hàng', 'KHACH_HANG');

-- Quản trị (IdVaiTro = 1) có full quyền (ALL)
INSERT INTO NhanSu.PhanQuyen (IdVaiTro, IdQuyenHan) VALUES (1, 1);
-- Thu ngân (IdVaiTro = 3) có quyền POS và KH
INSERT INTO NhanSu.PhanQuyen (IdVaiTro, IdQuyenHan) VALUES 
(3, 2), (3, 3), (3, 4), (3, 5);

-- Tạo Tài Khoản Quản Trị và Thu Ngân
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, LoaiDoiTac)
VALUES (N'Quản Trị Viên', '0000000000', 'CaNhan'),
       (N'Thu Ngân 1', '0111111111', 'CaNhan');

INSERT INTO DoiTac.NhanVien (IdDoiTac, MaNhanVien, IdVaiTro, TrangThai)
VALUES (1, 'NV001', 1, 'DangLam'),
       (2, 'NV002', 3, 'DangLam');

INSERT INTO DoiTac.TaiKhoan (IdDoiTac, TenDangNhap, MatKhauHash, LoaiTaiKhoan)
VALUES (1, 'admin', '1', 'QuanTri'),
       (2, 'thungan', '1', 'ThuNgan');

DECLARE @dummy INT = NEXT VALUE FOR HeThong.Seq_NhanVien;
DECLARE @dummy2 INT = NEXT VALUE FOR HeThong.Seq_NhanVien;
GO

-- PHẦN 12: RÀNG BUỘC KIỂM TRA DỮ LIỆU LẦN CUỐI
-- Đảm bảo không ai nhập trạng thái linh tinh bằng lệnh INSERT sai mà bắt buộc phải có trong bảng Từ Điển.

-- Bổ sung thêm một số dữ liệu Từ điển cuối cùng

-- Trạng Thái Chứng Từ Kho
INSERT INTO HeThong.TuDien VALUES ('CTK_TRANG_THAI', 'Moi',       N'Mới', 1, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTK_TRANG_THAI', 'ChoDuyet',  N'Chờ duyệt', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTK_TRANG_THAI', 'DaDuyet',   N'Đã duyệt', 3, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTK_TRANG_THAI', 'DaHuy',     N'Đã hủy', 4, '#C05050', NULL, 1);

INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_KHO', 'XUAT_SANXUAT',  N'Xuất sản xuất (BOM)', 9, NULL, NULL, 1);

-- Đã loại bỏ 4 mã chung (THU, CHI, NAP_VI, HOAN_VI) để tránh trùng với mã chi tiết ở Phần 11
-- Chỉ dùng mã chi tiết: THU_THANHTOAN, THU_COC, THU_NAP_VI, CHI_NHAP_HANG, CHI_LUONG, CHI_BAO_TRI, HOAN_TIEN, HOAN_COC
-- Bổ sung thêm mã hoàn số dư ví RFID khi khách trả vòng
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'HOAN_SO_DU_VI',  N'Hoàn số dư ví RFID', 9, '#6C757D', NULL, 1);

-- Trạng Thái RFID
INSERT INTO HeThong.TuDien VALUES ('RFID_TRANG_THAI', 'ChuaKichHoat', N'Chưa kích hoạt', 1, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('RFID_TRANG_THAI', 'DangDung',     N'Đang dùng', 2, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('RFID_TRANG_THAI', 'DaKhoa',       N'Đã khóa', 3, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('RFID_TRANG_THAI', 'DaTra',        N'Đã trả', 4, '#6C757D', NULL, 1);

-- Trạng Thái Đặt Phòng (Khớp AppConstants.TrangThaiBooking trong C#)
INSERT INTO HeThong.TuDien VALUES ('DP_TRANG_THAI', 'DatTruoc', N'Đặt trước', 1, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DP_TRANG_THAI', 'DangO',    N'Đang ở', 2, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DP_TRANG_THAI', 'DaTra',    N'Đã trả phòng', 3, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DP_TRANG_THAI', 'DaHuy',    N'Đã hủy', 4, '#C05050', NULL, 1);

-- Loại Sản Phẩm
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'VeVaoKhu',    N'Vé vào khu', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'VeTroChoi',   N'Vé trò chơi', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'AnUong',      N'Ăn uống', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'DoUong',      N'Đồ uống', 4, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'HangHoa',     N'Hàng hóa', 5, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'TuDo',        N'Tủ đồ', 6, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'DoChoThue',   N'Đồ cho thuê', 7, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'ChoiNghiMat', N'Chòi nghỉ mát', 8, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'DatChoThuAn', N'Đặt chỗ thú ăn', 9, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'LuuTru',      N'Lưu trú', 10, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'GuiXe',       N'Gửi xe', 11, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'NguyenLieu',  N'Nguyên liệu', 12, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'DoUongDongChai', N'Đồ uống đóng chai', 13, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'AnUongTienLoi', N'Đồ ăn tiện lợi', 14, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SP_LOAI', 'PhuongTien', N'Phương tiện di chuyển', 15, NULL, NULL, 1);

-- Loại Đối Tác
INSERT INTO HeThong.TuDien VALUES ('LOAI_DOI_TAC', 'CaNhan',      N'Cá nhân', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_DOI_TAC', 'ToChuc',      N'Tổ chức', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_DOI_TAC', 'CongTyLuHanh', N'Công ty lữ hành', 3, NULL, NULL, 1);

-- Loại Vé
INSERT INTO HeThong.TuDien VALUES ('VE_LOAI', 'VeLe',    N'Vé lẻ', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('VE_LOAI', 'VeCombo', N'Vé Combo', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('VE_LOAI', 'VeMuaVu', N'Vé mùa vụ', 3, NULL, NULL, 1);

--  Phương thức TT: Bỏ toàn bộ 4 dòng trùng 100% với Phần 11 (dòng 1919-1924)

-- Bổ sung Trạng Thái Bảo Trì (cho form ComboBox)
INSERT INTO HeThong.TuDien VALUES ('BAOTRI_TRANG_THAI', 'KeHoach',       N'Kế hoạch', 1, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BAOTRI_TRANG_THAI', 'DangThucHien',  N'Đang thực hiện', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BAOTRI_TRANG_THAI', 'HoanThanh',     N'Hoàn thành', 3, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BAOTRI_TRANG_THAI', 'DaHuy',         N'Đã hủy', 4, '#C05050', NULL, 1);

-- Bổ sung Trạng Thái Sự Cố
INSERT INTO HeThong.TuDien VALUES ('SUCO_TRANG_THAI', 'MoiGhi',    N'Mới ghi', 1, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SUCO_TRANG_THAI', 'DangXuLy',  N'Đang xử lý', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SUCO_TRANG_THAI', 'DaXuLy',    N'Đã xử lý', 3, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('SUCO_TRANG_THAI', 'DaDong',    N'Đã đóng', 4, '#6C757D', NULL, 1);

-- Bổ sung Trạng Thái Đặt Bàn
INSERT INTO HeThong.TuDien VALUES ('DAT_BAN_TRANG_THAI', 'ChoDen',    N'Chờ đến', 1, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DAT_BAN_TRANG_THAI', 'DenRoi',    N'Đã đến', 2, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DAT_BAN_TRANG_THAI', 'KhongDen',  N'Không đến', 3, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DAT_BAN_TRANG_THAI', 'DaHuy',       N'Đã hủy', 4, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DAT_BAN_TRANG_THAI', 'DangPhucVu',  N'Đang phục vụ', 5, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DAT_BAN_TRANG_THAI', 'DaThanhToan', N'Đã thanh toán', 6, '#5E9B76', NULL, 1);

-- Bổ sung Trạng Thái Thuê Đồ (dùng chung cho ThueDoChiTiet, ThueTu, ThueChoi)
INSERT INTO HeThong.TuDien VALUES ('THUE_TRANG_THAI', 'DangThue',    N'Đang thuê', 1, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('THUE_TRANG_THAI', 'DaTra',       N'Đã trả', 2, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('THUE_TRANG_THAI', 'QuaHan',      N'Quá hạn', 3, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('THUE_TRANG_THAI', 'MatDo',       N'Mất/Hỏng đồ', 4, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('THUE_TRANG_THAI', 'DaBoiThuong', N'Đã bồi thường', 5, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('THUE_TRANG_THAI', 'DaHuy',       N'Đã hủy', 6, '#343A40', NULL, 1);

-- Bảng Lương
INSERT INTO HeThong.TuDien VALUES ('BL_TRANG_THAI', 'DangTinh',  N'Đang tính', 1, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BL_TRANG_THAI', 'ChoDuyet',  N'Chờ duyệt', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BL_TRANG_THAI', 'DaDuyet',   N'Đã duyệt', 3, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BL_TRANG_THAI', 'DaChi',     N'Đã chi', 4, '#6C757D', NULL, 1);

-- Đơn Xin Nghỉ
INSERT INTO HeThong.TuDien VALUES ('DXN_TRANG_THAI', 'ChoDuyet', N'Chờ duyệt', 1, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DXN_TRANG_THAI', 'DaDuyet',  N'Đã duyệt', 2, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DXN_TRANG_THAI', 'TuChoi',   N'Từ chối', 3, '#C05050', NULL, 1);

-- Lệnh BEO (Bếp đoàn)
INSERT INTO HeThong.TuDien VALUES ('BEO_TRANG_THAI', 'ChuanBi',     N'Chuẩn bị', 1, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BEO_TRANG_THAI', 'DangPhucVu',  N'Đang phục vụ', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BEO_TRANG_THAI', 'HoanThanh',   N'Hoàn thành', 3, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BEO_TRANG_THAI', 'DaHuy',       N'Đã hủy', 4, '#C05050', NULL, 1);

-- Phân Công Buồng Phòng (Housekeeping)
INSERT INTO HeThong.TuDien VALUES ('BUONGPHONG_TT', 'DaGiao',    N'Đã giao', 1, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BUONGPHONG_TT', 'DangLam',   N'Đang làm', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BUONGPHONG_TT', 'HoanThanh', N'Hoàn thành', 3, '#5E9B76', NULL, 1);

-- Đồ Thất Lạc
INSERT INTO HeThong.TuDien VALUES ('THATLAC_TT', 'ChuaTraLai', N'Chưa trả lại', 1, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('THATLAC_TT', 'DaTraLai',   N'Đã trả lại', 2, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('THATLAC_TT', 'DaHuy',      N'Đã xử lý', 3, '#6C757D', NULL, 1);

-- Ca Trực Cứu Hộ
INSERT INTO HeThong.TuDien VALUES ('CATRUC_TT', 'ChuaBatDau', N'Chưa bắt đầu', 1, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CATRUC_TT', 'DangTruc',   N'Đang trực', 2, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CATRUC_TT', 'HoanThanh',  N'Hoàn thành', 3, '#5E9B76', NULL, 1);

-- Lịch Tạo Sóng
INSERT INTO HeThong.TuDien VALUES ('TAOSONG_TT', 'KeHoach',   N'Kế hoạch', 1, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('TAOSONG_TT', 'DangChay',  N'Đang chạy', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('TAOSONG_TT', 'HoanThanh', N'Hoàn thành', 3, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('TAOSONG_TT', 'DaHuy',     N'Đã hủy', 4, '#C05050', NULL, 1);

-- Gửi Xe (Lượt Vào Ra)
INSERT INTO HeThong.TuDien VALUES ('GUIXE_TT', 'DangGui', N'Đang gửi', 1, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('GUIXE_TT', 'DaRa',    N'Đã ra', 2, '#5E9B76', NULL, 1);

-- Giải Đua
INSERT INTO HeThong.TuDien VALUES ('GIAIDUA_TT', 'SapDienRa',  N'Sắp diễn ra', 1, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('GIAIDUA_TT', 'DangDienRa', N'Đang diễn ra', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('GIAIDUA_TT', 'KetThuc',    N'Kết thúc', 3, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('GIAIDUA_TT', 'DaHuy',      N'Đã hủy', 4, '#C05050', NULL, 1);

-- Lịch Thi Đấu (từng lượt đua)
INSERT INTO HeThong.TuDien VALUES ('LICHDUA_TT', 'ChuaBatDau', N'Chưa bắt đầu', 1, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LICHDUA_TT', 'DangDua',    N'Đang đua', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LICHDUA_TT', 'KetThuc',    N'Kết thúc', 3, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LICHDUA_TT', 'DaHuy',      N'Đã hủy', 4, '#C05050', NULL, 1);

-- Ngựa Đua
INSERT INTO HeThong.TuDien VALUES ('DONGVATDUA_TT', 'SanSang',  N'Sẵn sàng', 1, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DONGVATDUA_TT', 'DieuTri',  N'Đang điều trị', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DONGVATDUA_TT', 'NghiHuu',  N'Nghỉ hưu', 3, '#6C757D', NULL, 1);

-- Phương Tiện Đua
INSERT INTO HeThong.TuDien VALUES ('PTDUA_TT', 'SanSang', N'Sẵn sàng', 1, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PTDUA_TT', 'BaoTri',  N'Đang bảo trì', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PTDUA_TT', 'Hong',    N'Hỏng', 3, '#C05050', NULL, 1);

-- Lô Hàng
INSERT INTO HeThong.TuDien VALUES ('LOHANG_TT', 'ConHang',  N'Còn hàng', 1, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOHANG_TT', 'HetHang',  N'Hết hàng', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOHANG_TT', 'HetHan',   N'Hết hạn', 3, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOHANG_TT', 'DaHuy',    N'Đã hủy', 4, '#343A40', NULL, 1);

-- Giữ Hàng (Kho)
INSERT INTO HeThong.TuDien VALUES ('TAMGIU_TT', 'DangGiu',   N'Đang giữ', 1, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('TAMGIU_TT', 'DaXuatLai', N'Đã xuất lại', 2, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('TAMGIU_TT', 'DaHuy',     N'Đã hủy', 3, '#C05050', NULL, 1);

-- Phiên Thu Ngân
INSERT INTO HeThong.TuDien VALUES ('PTN_TRANG_THAI', 'DangMo',  N'Đang mở', 1, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PTN_TRANG_THAI', 'DaDong',  N'Đã đóng', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('PTN_TRANG_THAI', 'DaDuyet', N'Đã duyệt', 3, '#6C757D', NULL, 1);

-- Y Tế Thú
INSERT INTO HeThong.TuDien VALUES ('YTETHU_TT', 'DangDieuTri', N'Đang điều trị', 1, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('YTETHU_TT', 'DaKhoi',      N'Đã khỏi', 2, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('YTETHU_TT', 'DaChet',      N'Đã chết', 3, '#C05050', NULL, 1);

-- Trạng Thái Báo Giá B2B
INSERT INTO HeThong.TuDien VALUES ('BAO_GIA_TT', 'MoiBaoGia',    N'Mới báo giá', 1, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BAO_GIA_TT', 'DaDuyet',      N'Đã duyệt', 2, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BAO_GIA_TT', 'DaHuy',        N'Đã hủy', 3, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BAO_GIA_TT', 'DaChuyenDon',  N'Đã chuyển đơn', 4, '#5B9BD5', NULL, 1);

-- Trạng Thái Công Nợ
INSERT INTO HeThong.TuDien VALUES ('CONG_NO_TT', 'ChuaTra',       N'Chưa trả', 1, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CONG_NO_TT', 'DaTraMotPhan',  N'Đã trả một phần', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CONG_NO_TT', 'DaTra',         N'Đã trả', 3, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CONG_NO_TT', 'QuaHan',        N'Quá hạn', 4, '#6C757D', NULL, 1);

-- Trạng Thái Lịch Biểu Diễn
INSERT INTO HeThong.TuDien VALUES ('BIEU_DIEN_TT', 'SapDienRa',  N'Sắp diễn ra', 1, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BIEU_DIEN_TT', 'DangDienRa', N'Đang diễn ra', 2, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BIEU_DIEN_TT', 'DaKetThuc',  N'Đã kết thúc', 3, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('BIEU_DIEN_TT', 'DaHuy',      N'Đã hủy', 4, '#C05050', NULL, 1);

-- Trạng Thái Đặt Chỗ (Dùng chung cho DatChoXemDua + DatChoXemShow)
INSERT INTO HeThong.TuDien VALUES ('DAT_CHO_TT', 'DaDat',    N'Đã đặt', 1, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DAT_CHO_TT', 'DaCheckIn', N'Đã check-in', 2, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('DAT_CHO_TT', 'DaHuy',    N'Đã hủy', 3, '#C05050', NULL, 1);

-- Đối Tượng Vé (Phân loại giá vé theo chiều cao/tuổi tại Đại Nam)
INSERT INTO HeThong.TuDien VALUES ('VE_DOI_TUONG', 'NguoiLon',     N'Người lớn', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('VE_DOI_TUONG', 'TreEm',        N'Trẻ em (1m - 1m4)', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('VE_DOI_TUONG', 'MienPhi',      N'Miễn phí (dưới 1m)', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('VE_DOI_TUONG', 'NguoiCaoTuoi', N'Người cao tuổi', 4, NULL, NULL, 1);

-- Nguồn Chấm Công
INSERT INTO HeThong.TuDien VALUES ('NGUON_CHAM_CONG', 'VanTay',  N'Máy vân tay', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NGUON_CHAM_CONG', 'NhapTay', N'Nhập tay', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('NGUON_CHAM_CONG', 'QRCode',  N'Quét QR', 3, NULL, NULL, 1);

-- Loại Điều Kiện Khuyến Mãi (Chuẩn hóa cách nhập điều kiện cho bảng KhuyenMai_DieuKien)
INSERT INTO HeThong.TuDien VALUES ('KM_DIEU_KIEN', 'DoTuoi',           N'Độ tuổi', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KM_DIEU_KIEN', 'HangThanhVien',    N'Hạng thành viên', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KM_DIEU_KIEN', 'LoaiKhach',        N'Loại khách', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KM_DIEU_KIEN', 'NgayApDung',       N'Ngày áp dụng', 4, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KM_DIEU_KIEN', 'SanPhamApDung',    N'Sản phẩm áp dụng', 5, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KM_DIEU_KIEN', 'SoLuongToiThieu',  N'Số lượng tối thiểu', 6, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('KM_DIEU_KIEN', 'KhuVuc',           N'Khu vực', 7, NULL, NULL, 1);

-- Trạng Thái Dòng Chi Tiết Đơn Hàng (Dùng khi hoàn tiền từng dòng trong bill)
INSERT INTO HeThong.TuDien VALUES ('CTDH_TRANG_THAI', 'BinhThuong',  N'Bình thường', 1, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDH_TRANG_THAI', 'DaHoan',      N'Đã hoàn', 2, '#C05050', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDH_TRANG_THAI', 'HoanMotPhan', N'Hoàn 1 phần', 3, '#D4A843', NULL, 1);

-- ★ Loại Dòng Chi Tiết Đơn Hàng (Discriminator cho Polymorphic Order Lines)
-- Dev C# set cột này khi tạo ChiTietDonHang, nhìn vào là biết ngay dòng thuộc loại nào
INSERT INTO HeThong.TuDien VALUES ('CTDH_LOAI_DONG', 'Ve',          N'Vé', 1, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDH_LOAI_DONG', 'AnUong',      N'Ăn uống', 2, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDH_LOAI_DONG', 'DoUong',      N'Đồ uống', 3, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDH_LOAI_DONG', 'HangHoa',     N'Hàng hóa', 4, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDH_LOAI_DONG', 'ThueTuDo',    N'Thuê tủ đồ', 5, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDH_LOAI_DONG', 'ThueDo',      N'Thuê đồ', 6, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDH_LOAI_DONG', 'ThueChoi',    N'Thuê chòi', 7, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDH_LOAI_DONG', 'DatPhong',    N'Đặt phòng', 8, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDH_LOAI_DONG', 'DatChoShow',  N'Đặt chỗ show', 9, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDH_LOAI_DONG', 'DatChoDua',   N'Đặt chỗ đua', 10, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDH_LOAI_DONG', 'ChoThuAn',    N'Cho thú ăn', 11, NULL, NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDH_LOAI_DONG', 'GuiXe',       N'Gửi xe', 12, NULL, NULL, 1);
GO

-- Bổ sung Luồng Trạng Thái cho DonHang (Nhà hàng: khách ngồi gọi món liên tục rồi mới tính tiền)
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('DonHang', 'DangPhucVu',   'ChoThanhToan', N'Khách gọi tính tiền'),
('DonHang', 'DangPhucVu',   'DaHuy',        N'Hủy bill đang phục vụ');

-- Bổ sung Luồng Trạng Thái cho DatBan (Sau khi khách đến, link DonHang rồi phục vụ)
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('DatBan', 'DenRoi',      'DangPhucVu',  N'Bắt đầu phục vụ (link DonHang)'),
('DatBan', 'DangPhucVu',  'DaThanhToan', N'Khách thanh toán xong');

-- Luồng Trạng Thái Báo Giá B2B
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('BaoGia', 'MoiBaoGia',   'DaDuyet',      N'Khách chốt giá'),
('BaoGia', 'MoiBaoGia',   'DaHuy',        N'Hủy báo giá'),
('BaoGia', 'DaDuyet',     'DaChuyenDon',  N'Chuyển thành đơn hàng thật');

-- Luồng Trạng Thái Công Nợ
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('CongNo', 'ChuaTra',       'DaTraMotPhan',  N'Trả 1 phần'),
('CongNo', 'ChuaTra',       'DaTra',         N'Trả hết'),
('CongNo', 'DaTraMotPhan',  'DaTra',         N'Trả nốt'),
('CongNo', 'ChuaTra',       'QuaHan',        N'Quá hạn thanh toán');

-- Luồng Trạng Thái Biểu Diễn
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('LichBieuDien', 'SapDienRa',  'DangDienRa', N'Show bắt đầu'),
('LichBieuDien', 'DangDienRa', 'DaKetThuc',  N'Show kết thúc'),
('LichBieuDien', 'SapDienRa',  'DaHuy',      N'Hủy show');
GO

-- BanHang
ALTER TABLE BanHang.DonHang
ADD CONSTRAINT CHK_DH_TrangThai CHECK (HeThong.fn_TuDienHopLe('DON_HANG_TRANG_THAI', TrangThai) = 1);

ALTER TABLE BanHang.VeDienTu
ADD CONSTRAINT CHK_VE_TrangThai CHECK (HeThong.fn_TuDienHopLe('VE_TRANG_THAI', TrangThai) = 1);

ALTER TABLE BanHang.PhieuDatPhong
ADD CONSTRAINT CHK_DP_TrangThai CHECK (HeThong.fn_TuDienHopLe('DP_TRANG_THAI', TrangThai) = 1);

-- TaiChinh
ALTER TABLE TaiChinh.TheRFID
ADD CONSTRAINT CHK_RFID_TrangThai CHECK (HeThong.fn_TuDienHopLe('RFID_TRANG_THAI', TrangThai) = 1);

-- Kho
ALTER TABLE Kho.ChungTu
ADD CONSTRAINT CHK_CTK_TrangThai CHECK (HeThong.fn_TuDienHopLe('CTK_TRANG_THAI', TrangThai) = 1);

ALTER TABLE Kho.ChungTu
ADD CONSTRAINT CHK_CTK_LoaiChungTu CHECK (HeThong.fn_TuDienHopLe('LOAI_CHUNG_TU_KHO', LoaiChungTu) = 1);

-- DanhMuc
ALTER TABLE DanhMuc.Phong
ADD CONSTRAINT CHK_P_TrangThai CHECK (HeThong.fn_TuDienHopLe('PHONG_TRANG_THAI', TrangThai) = 1);

ALTER TABLE DanhMuc.BanAn
ADD CONSTRAINT CHK_BA_TrangThai CHECK (HeThong.fn_TuDienHopLe('BAN_AN_TRANG_THAI', TrangThai) = 1);

ALTER TABLE DanhMuc.SanPham
ADD CONSTRAINT CHK_SP_LoaiSanPham CHECK (HeThong.fn_TuDienHopLe('SP_LOAI', LoaiSanPham) = 1);

ALTER TABLE DanhMuc.SanPham_Ve
ADD CONSTRAINT CHK_SPV_LoaiVe CHECK (HeThong.fn_TuDienHopLe('VE_LOAI', LoaiVe) = 1);

-- DoiTac
ALTER TABLE DoiTac.ThongTin
ADD CONSTRAINT CHK_DT_LoaiDoiTac CHECK (HeThong.fn_TuDienHopLe('LOAI_DOI_TAC', LoaiDoiTac) = 1);

ALTER TABLE DoiTac.KhachHang
ADD CONSTRAINT CHK_KH_LoaiKhach CHECK (LoaiKhach IS NULL OR HeThong.fn_TuDienHopLe('KH_LOAI_KHACH', LoaiKhach) = 1);

-- TaiChinh ChungTu
ALTER TABLE TaiChinh.ChungTuTC
ADD CONSTRAINT CHK_CTTC_LoaiChungTu CHECK (HeThong.fn_TuDienHopLe('LOAI_CHUNG_TU_TC', LoaiChungTu) = 1);
GO
-- 1. Ràng buộc Lịch trình & Thời gian (Date & Time checks)

ALTER TABLE BanHang.KhuyenMai 
ADD CONSTRAINT CHK_KhuyenMai_Date CHECK (NgayBatDau <= NgayKetThuc);

ALTER TABLE Kho.LoHang 
ADD CONSTRAINT CHK_LoHang_Date CHECK (NgaySanXuat IS NULL OR NgayHetHan IS NULL OR NgaySanXuat <= NgayHetHan);

--  Ca đêm (22:00 → 06:00) cần GioBatDau > GioKetThuc → dùng != thay vì <
ALTER TABLE NhanSu.CaLamMau ADD CONSTRAINT CHK_CaLam_Gio CHECK (GioBatDau != GioKetThuc);
--  BanHang.ThueVatDung không tồn tại → sửa thành ThueDoChiTiet + đúng tên cột
ALTER TABLE BanHang.ThueDoChiTiet 
ADD CONSTRAINT CHK_ThueDo_Gio CHECK (ThoiGianTra IS NULL OR ThoiGianThue <= ThoiGianTra);

-- 2. Ràng buộc Số Lượng, Đơn Giá, Sức Chứa (Phải >= 0, không được âm)
ALTER TABLE DanhMuc.KhuVuc
 ADD CONSTRAINT CHK_KhuVuc_SucChua CHECK (SucChua IS NULL OR SucChua >= 0);
ALTER TABLE DanhMuc.LoaiPhong 
ADD CONSTRAINT CHK_LoaiPhong_SoNguoi CHECK (SoNguoiToiDa IS NULL OR SoNguoiToiDa >= 0);
ALTER TABLE BanHang.ChiTietDonHang 
ADD CONSTRAINT CHK_CTDH_SoLuong CHECK (SoLuong >= 0 AND DonGiaThucTe >= 0 AND TienThue >= 0);

-- 3. Ràng buộc Tỷ lệ phần trăm (Luôn từ 0 đến 100)
ALTER TABLE DanhMuc.CauHinhThue 
ADD CONSTRAINT CHK_Thue_TyLe CHECK (TyLePhanTram >= 0 AND TyLePhanTram <= 100);


ALTER TABLE DanhMuc.SanPham_Ve
ADD CONSTRAINT CHK_SPV_DoiTuong CHECK (HeThong.fn_TuDienHopLe('VE_DOI_TUONG', DoiTuongVe) = 1);

ALTER TABLE BanHang.KhuyenMai_DieuKien
ADD CONSTRAINT CHK_KMDK_Loai CHECK (HeThong.fn_TuDienHopLe('KM_DIEU_KIEN', LoaiDieuKien) = 1);

ALTER TABLE BanHang.DatBan
ADD CONSTRAINT CHK_DB_TrangThai CHECK (HeThong.fn_TuDienHopLe('DAT_BAN_TRANG_THAI', TrangThai) = 1);

ALTER TABLE NhanSu.BangChamCong
ADD CONSTRAINT CHK_BCC_NguonChamCong CHECK (HeThong.fn_TuDienHopLe('NGUON_CHAM_CONG', NguonChamCong) = 1);

ALTER TABLE BanHang.BaoGia
ADD CONSTRAINT CHK_BG_TrangThai CHECK (HeThong.fn_TuDienHopLe('BAO_GIA_TT', TrangThai) = 1);

ALTER TABLE BanHang.CongNo
ADD CONSTRAINT CHK_CN_TrangThai CHECK (HeThong.fn_TuDienHopLe('CONG_NO_TT', TrangThai) = 1);

ALTER TABLE VanHanh.LichBieuDien
ADD CONSTRAINT CHK_LBD_TrangThai CHECK (HeThong.fn_TuDienHopLe('BIEU_DIEN_TT', TrangThai) = 1);

ALTER TABLE BanHang.DatChoXemDua
ADD CONSTRAINT CHK_DCXD_TrangThai CHECK (HeThong.fn_TuDienHopLe('DAT_CHO_TT', TrangThai) = 1);

ALTER TABLE BanHang.DatChoXemShow
ADD CONSTRAINT CHK_DCXS_TrangThai CHECK (HeThong.fn_TuDienHopLe('DAT_CHO_TT', TrangThai) = 1);
GO


-- ============================================
-- PHẦN 13: BỔ SUNG TỪ ĐÁNH GIÁ DATABASE V2.1
-- Ngày: 16/04/2026
-- Nội dung: Fix lỗ hổng được phát hiện khi đối chiếu
--           với báo cáo phân tích nghiệp vụ toàn diện
-- ============================================

-- ─────────────────────────────────────────────
-- 13.1 Bảng Lịch Sử Hoàn Hàng
-- Ghi lại chi tiết từng lần hoàn (truy vết: hoàn lần nào, ai duyệt, lý do gì)
-- Giải quyết: Trước đây chỉ có counter SoLuongDaHoan trên ChiTietDonHang,
-- không biết hoàn bao nhiêu lần, mỗi lần bao nhiêu
-- ─────────────────────────────────────────────
CREATE TABLE BanHang.LichSuHoanHang (
    Id                INT IDENTITY(1,1) PRIMARY KEY,
    IdChiTietDonHang  INT            NOT NULL REFERENCES BanHang.ChiTietDonHang(Id),
    SoLuongHoan       DECIMAL(10,2)  NOT NULL,
    LyDo              NVARCHAR(300)  NOT NULL,
    IdNguoiDuyet      INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    IdChungTuHoan     INT            NULL REFERENCES TaiChinh.ChungTuTC(Id),  -- Phiếu hoàn tiền tương ứng
    ThoiGian          DATETIME       NOT NULL DEFAULT GETDATE(),

    CONSTRAINT CHK_LSH_SoLuong CHECK (SoLuongHoan > 0)
);
GO

-- ─────────────────────────────────────────────
-- 13.2 Bảng Kiểm Tra An Toàn Hàng Ngày
-- Trưởng ca kỹ thuật phải tích "Đạt chuẩn" mỗi sáng cho từng trò chơi/thiết bị
-- Nếu chưa ký duyệt ngày hôm nay → phần mềm CHẶN BÁN VÉ
-- Giải quyết: Báo cáo BA yêu cầu "Daily Maintenance Sign-off"
-- ─────────────────────────────────────────────
CREATE TABLE VanHanh.KiemTraAnToanNgay (
    Id                INT IDENTITY(1,1) PRIMARY KEY,
    IdTroChoi         INT            NULL REFERENCES DanhMuc.TroChoi(Id),
    IdThietBi         INT            NULL REFERENCES DanhMuc.ThietBi(Id),
    NgayKiemTra       DATE           NOT NULL,
    DatChuan          BIT            NOT NULL DEFAULT 0,  -- 0 = Không đạt, 1 = Đạt
    IdNguoiKiemTra    INT            NOT NULL REFERENCES DoiTac.NhanVien(IdDoiTac),
    ThoiGian          DATETIME       NOT NULL DEFAULT GETDATE(),
    GhiChu            NVARCHAR(500)  NULL,

    -- Phải kiểm tra ít nhất 1 trong 2: TroChoi hoặc ThietBi
    CONSTRAINT CHK_KTAN_CoMucTieu CHECK (IdTroChoi IS NOT NULL OR IdThietBi IS NOT NULL)
);
GO

-- Mỗi trò chơi chỉ được kiểm tra 1 lần/ngày (filtered index cho NULL-safe unique)
CREATE UNIQUE NONCLUSTERED INDEX UQ_KTAN_TroChoi
    ON VanHanh.KiemTraAnToanNgay(IdTroChoi, NgayKiemTra)
    WHERE IdTroChoi IS NOT NULL;

CREATE UNIQUE NONCLUSTERED INDEX UQ_KTAN_ThietBi
    ON VanHanh.KiemTraAnToanNgay(IdThietBi, NgayKiemTra)
    WHERE IdThietBi IS NOT NULL;
GO

-- ─────────────────────────────────────────────
-- 13.3 Bảng Lệnh Bếp (Kitchen Display System)
-- Cho khách lẻ gọi món tại nhà hàng: ChoNau → DangNau → DaXong → DaServe
-- Giải quyết: Trước đây BEO chỉ dành cho đoàn, khách lẻ không có KDS
-- ─────────────────────────────────────────────
CREATE TABLE BanHang.LenhBep (
    Id                INT IDENTITY(1,1) PRIMARY KEY,
    IdChiTietDonHang  INT            NOT NULL REFERENCES BanHang.ChiTietDonHang(Id),
    IdNhaHang         INT            NOT NULL REFERENCES DanhMuc.NhaHang(Id),
    SoLuong           INT            NOT NULL DEFAULT 1,
    TrangThai         VARCHAR(20)    NOT NULL DEFAULT 'ChoNau',  -- ChoNau / DangNau / DaXong / DaServe / DaHuy
    ThoiGianGui       DATETIME       NOT NULL DEFAULT GETDATE(),
    ThoiGianBatDauNau DATETIME       NULL,
    ThoiGianXong      DATETIME       NULL,
    GhiChuBep         NVARCHAR(200)  NULL
);
GO

-- FK bổ sung: VeDienTu self-ref (vé cấp lại link về vé gốc bị mất/hỏng)
ALTER TABLE BanHang.VeDienTu
    ADD CONSTRAINT FK_VeDienTu_VeGoc FOREIGN KEY (IdVeGoc) REFERENCES BanHang.VeDienTu(Id);
GO


-- ============================================
-- PHẦN 13B: SEED DATA BỔ SUNG
-- ============================================

-- ── Cấu hình ngưỡng chiều cao phân loại vé tự động ──
INSERT INTO HeThong.CauHinh VALUES ('CHIEU_CAO_MIEN_PHI_CM', '100', N'Dưới 100cm = vé miễn phí (theo quy định Đại Nam)', GETDATE(), NULL);
INSERT INTO HeThong.CauHinh VALUES ('CHIEU_CAO_TRE_EM_CM', '140', N'Từ 100cm đến 140cm = vé trẻ em', GETDATE(), NULL);

-- ── Loại chứng từ tài chính bổ sung ──
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'THU_CONG_DUC',    N'Thu tiền công đức', 10, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LOAI_CHUNG_TU_TC', 'THU_BOI_THUONG',  N'Thu bồi thường tài sản', 11, '#CC8844', NULL, 1);

-- ── Trạng Thái Chứng Từ Tài Chính (MỚI — trước đây ChungTu TC không có trạng thái) ──
INSERT INTO HeThong.TuDien VALUES ('CTTC_TRANG_THAI', 'DaDuyet',   N'Đã duyệt', 1, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTTC_TRANG_THAI', 'ChoDuyet',  N'Chờ duyệt', 2, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTTC_TRANG_THAI', 'DaHuy',     N'Đã hủy', 3, '#C05050', NULL, 1);

-- ── Trạng Thái Chi Tiết Đặt Phòng (từng phòng cụ thể, khác header ChiTietDatPhong) ──
INSERT INTO HeThong.TuDien VALUES ('CTDP_TRANG_THAI', 'ChoDen',     N'Chờ đến', 1, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDP_TRANG_THAI', 'DaCheckIn',  N'Đã check-in', 2, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDP_TRANG_THAI', 'DaCheckOut', N'Đã check-out', 3, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('CTDP_TRANG_THAI', 'DaHuy',      N'Đã hủy', 4, '#C05050', NULL, 1);

-- ── Trạng Thái Lệnh Bếp (Kitchen Display) ──
INSERT INTO HeThong.TuDien VALUES ('LENH_BEP_TT', 'ChoNau',    N'Chờ nấu', 1, '#D4A843', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LENH_BEP_TT', 'DangNau',   N'Đang nấu', 2, '#5B9BD5', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LENH_BEP_TT', 'DaXong',    N'Đã xong', 3, '#5E9B76', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LENH_BEP_TT', 'DaServe',   N'Đã ra món', 4, '#6C757D', NULL, 1);
INSERT INTO HeThong.TuDien VALUES ('LENH_BEP_TT', 'DaHuy',     N'Đã hủy', 5, '#C05050', NULL, 1);
GO


-- ============================================
-- PHẦN 13C: LUỒNG TRẠNG THÁI BỔ SUNG
-- ============================================

-- Chứng Từ Tài Chính (MỚI — trước đây phiếu thu/chi auto-approve, giờ có workflow duyệt)
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('ChungTuTC', 'ChoDuyet',  'DaDuyet',   N'Kế toán phê duyệt phiếu'),
('ChungTuTC', 'ChoDuyet',  'DaHuy',     N'Từ chối / hủy phiếu'),
('ChungTuTC', 'DaDuyet',   'DaHuy',     N'Hủy phiếu đã duyệt (cần quyền QLD)');

-- Chi Tiết Đặt Phòng (từng phòng cụ thể, khác với DatPhong header)
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('ChiTietDatPhong', 'ChoDen',     'DaCheckIn',  N'Lễ tân gán phòng vật lý'),
('ChiTietDatPhong', 'ChoDen',     'DaHuy',      N'Hủy phòng cụ thể trong lượt đặt'),
('ChiTietDatPhong', 'DaCheckIn',  'DaCheckOut', N'Khách trả phòng');

-- Lệnh Bếp (Kitchen Display System)
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('LenhBep', 'ChoNau',  'DangNau',  N'Bếp nhận và bắt đầu nấu'),
('LenhBep', 'DangNau', 'DaXong',   N'Nấu xong, chờ phục vụ'),
('LenhBep', 'DaXong',  'DaServe',  N'Nhân viên mang ra bàn'),
('LenhBep', 'ChoNau',  'DaHuy',    N'Khách hủy món');

-- Vé Điện Tử (thêm trạng thái cấp lại)
INSERT INTO HeThong.LuongTrangThai (ThucThe, TuTrangThai, DenTrangThai, MoTa) VALUES
('VeDienTu', 'ChuaSuDung', 'DaCapLai',  N'Vé bị mất/hỏng → cấp lại vé mới'),
('VeDienTu', 'DaSuDung',   'DaCapLai',  N'Vé đang dùng bị mất → cấp lại');

-- Bổ sung trạng thái DaCapLai cho VeDienTu vào TuDien
INSERT INTO HeThong.TuDien VALUES ('VE_TRANG_THAI', 'DaCapLai', N'Đã cấp lại', 6, '#5B9BD5', NULL, 1);
GO


-- ============================================
-- PHẦN 13D: RÀNG BUỘC BỔ SUNG
-- ============================================

-- CHECK TrangThai cho ChiTietDatPhong (trước đây thiếu — đã phát hiện trong audit)
ALTER TABLE BanHang.ChiTietDatPhong
ADD CONSTRAINT CHK_CTDP_TrangThai CHECK (HeThong.fn_TuDienHopLe('CTDP_TRANG_THAI', TrangThai) = 1);

-- CHECK LoaiGia cho BangGia (trigger chống overlap dựa vào LoaiGia nhưng không validate giá trị)
ALTER TABLE DanhMuc.BangGia
ADD CONSTRAINT CHK_BG_LoaiGia CHECK (HeThong.fn_TuDienHopLe('BANG_GIA_LOAI', LoaiGia) = 1);

-- CHECK TrangThai cho TaiChinh.ChungTuTC (mới thêm cột TrangThai ở Phần 13)
ALTER TABLE TaiChinh.ChungTuTC
ADD CONSTRAINT CHK_CTTC_TrangThai CHECK (HeThong.fn_TuDienHopLe('CTTC_TRANG_THAI', TrangThai) = 1);

-- CHECK TrangThai cho LenhBep
ALTER TABLE BanHang.LenhBep
ADD CONSTRAINT CHK_LB_TrangThai CHECK (HeThong.fn_TuDienHopLe('LENH_BEP_TT', TrangThai) = 1);
GO


CREATE INDEX IX_CTDH_DonHang ON BanHang.ChiTietDonHang(IdDonHang);
CREATE INDEX IX_ThueDo_CTDH ON BanHang.ThueDoChiTiet(IdChiTietDonHang);
-- Bổ sung Index cho ChiTietDatPhong
CREATE INDEX IX_CTDP_DatPhong ON BanHang.ChiTietDatPhong(IdPhieuDatPhong);

CREATE INDEX IX_DatChoShow_CTDH ON BanHang.DatChoXemShow(IdChiTietDonHang);
CREATE INDEX IX_DatChoDua_CTDH ON BanHang.DatChoXemDua(IdChiTietDonHang);
CREATE INDEX IX_LenhBep_CTDH ON BanHang.LenhBep(IdChiTietDonHang);
GO



-- 1. Thêm cấu hình phạt lố giờ (10% / giờ)
INSERT INTO HeThong.CauHinh (Khoa, GiaTri, MoTa, CapNhatLuc)
VALUES ('PHAT_LO_GIO_PHAN_TRAM_MOI_GIO', '10', N'Tỷ lệ phạt lố giờ mỗi tiếng (%/giờ). Tối đa 100%.', GETDATE());

-- 2. Thêm Sản phẩm hệ thống: Phí Phụ Thu / Phạt (Gán sẵn IdDonViTinh = 1)
INSERT INTO DanhMuc.SanPham (MaSanPham, TenSanPham, LoaiSanPham, IdDonViTinh, DonGia, LaVatTu, CanQuanLyLo, TrangThai, DaXoa, NgayTao)
VALUES ('SYS_PHU_THU', N'Phí Phụ Thu / Phạt (Hệ Thống)', 'HangHoa', 1, 0, 0, 0, 'DangBan', 0, GETDATE());
GO

-- Seed Config
INSERT INTO HeThong.CauHinh (Khoa, GiaTri, MoTa) VALUES ('RFID_TIEN_COC', '50000', N'Tiền cọc mở thẻ RFID');

-- ============================================================
-- KHO HÀNG ẢO HỆ THỐNG (BẮT BUỘC - HARDCODE TRONG CODE)
-- ============================================================
INSERT INTO Kho.KhoHang (MaKho, TenKho, LaKhoAo, ChoPhepTonAm, IdKhuVuc, TrangThai) VALUES
('KHO_NCC',        N'Nhà Cung Cấp',                         1, 0, NULL, 'HoatDong'),
('KHO_KHACH',      N'Khách Hàng',                            1, 0, NULL, 'HoatDong'),
('KHO_HUY',        N'Hủy Hỏng / Hết Hạn',                   1, 0, NULL, 'HoatDong'),
('KHO_THAT_THOAT', N'Thất Thoát',                            1, 0, NULL, 'HoatDong'),
('KHO_TRA',        N'Hàng Trả Lại',                          1, 0, NULL, 'HoatDong'),
('KHO_CHENH_LECH', N'Chênh Lệch Kiểm Kê',                     1, 1, NULL, 'HoatDong'),
('KHO_BAOTRI',     N'Bảo Trì',                               1, 0, NULL, 'HoatDong'),
('KHO_SANXUAT',    N'Sản Xuất / Tiêu Hao BOM',               1, 0, NULL, 'HoatDong');
GO


-- ============================================================
-- DỮ LIỆU HỆ THỐNG: PHÂN QUYỀN VÀ VAI TRÒ (BẮT BUỘC)
-- ============================================================
-- 4. PHÂN QUYỀN HỆ THỐNG
-- ==========================================
-- ============================================================
-- Script bổ sung dữ liệu Phân Quyền (chạy trên DB đã có data)
-- Tên: SeedData_PhanQuyen.sql
-- Mục đích: Tạo danh sách Quyền Hạn khớp với các menu đã
--           triển khai thực tế trong _appRouter (frmMain.cs).
--           Gán full quyền cho Admin (IdVaiTro=1)
--           và quyền giới hạn cho ThuNgan (IdVaiTro=3).
-- Cập nhật: 24/04/2026 — Dọn bớt quyền chưa triển khai,
--           gộp THUE_DO_GIAO + THUE_DO_TRA → THUE_DO.
-- ============================================================

-- 1. XÓA dữ liệu cũ (để seed lại từ đầu cho sạch)
DELETE FROM NhanSu.PhanQuyen;
DELETE FROM NhanSu.QuyenHan;
GO

-- 2. SEED DANH SÁCH QUYỀN HẠN — CHỈ các chức năng ĐÃ TRIỂN KHAI
-- MaQuyen khớp với key trong _appRouter của frmMain.cs

SET IDENTITY_INSERT NhanSu.QuyenHan ON;
INSERT INTO NhanSu.QuyenHan (Id, MaQuyen, TenQuyen, NhomQuyen) VALUES
-- Bảng điều khiển
(1,  'DASHBOARD',         N'Xem bảng điều khiển',              'BangDieuKhien'),

-- Tiền sảnh & Bán hàng
(2,  'POS_BAN_LE',        N'Bán hàng POS (Bán lẻ)',            'BanHang'),
(3,  'THUE_DO',           N'Quản lý thuê đồ (Giao & Trả)',    'BanHang'),
(4,  'KHACH_HANG_CRM',    N'Quản lý Khách hàng (CRM)',         'BanHang'),

-- Kho & F&B
(5,  'DANH_MUC_KHO',      N'Danh mục Kho',                     'Kho'),
(6,  'TRUNG_TAM_KHO',     N'Trung tâm Kho',                    'Kho'),

-- Thiết lập hệ thống
(7,  'SAN_PHAM',          N'Quản lý sản phẩm / danh mục',      'HeThong'),
(8,  'COMBO',             N'Combo sản phẩm',                    'HeThong'),
(9,  'PHAN_QUYEN',        N'Phân quyền hệ thống',              'HeThong'),

-- Lưu trú
(10, 'LE_TAN_PHONG',      N'Lễ tân & Lưu trú',                 'BanHang'),

-- Kiểm soát vé
(11, 'KIEM_SOAT_CONG',     N'Kiểm soát vé cổng',                'VanHanh'),
(12, 'BAO_CAO',            N'Xem báo cáo & phân tích doanh thu', 'TaiChinh'),
(13, 'MENU_POS',           N'Cấu hình Menu bán hàng',           'HeThong'),

-- Khuyến mãi
(14, 'KHUYEN_MAI',         N'Quản lý Khuyến mãi',               'HeThong'),
(15, 'NHAT_KY_HE_THONG',   N'Xem nhật ký hệ thống',             'HeThong');


SET IDENTITY_INSERT NhanSu.QuyenHan OFF;
GO

-- 3. GÁN QUYỀN CHO VAI TRÒ

-- Admin (IdVaiTro = 1): FULL quyền — gán tất cả
INSERT INTO NhanSu.PhanQuyen (IdVaiTro, IdQuyenHan)
SELECT 1, Id FROM NhanSu.QuyenHan;
GO

-- Thu Ngân (IdVaiTro = 3): Chỉ có quyền liên quan POS, Thuê đồ, khách hàng và Kiểm soát vé
INSERT INTO NhanSu.PhanQuyen (IdVaiTro, IdQuyenHan) VALUES
(3, 1),   -- DASHBOARD
(3, 2),   -- POS_BAN_LE
(3, 3),   -- THUE_DO
(3, 4),   -- KHACH_HANG_CRM
(3, 11);  -- KIEM_SOAT_CONG
GO


IF OBJECT_ID('dbo.sp_BaoCaoDoanhThu', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_BaoCaoDoanhThu;
GO

CREATE PROCEDURE dbo.sp_BaoCaoDoanhThu
    @TuNgay  DATETIME,
    @DenNgay DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        dh.NgayTao                              AS NgayGiaoDich,
        dh.MaDonHang                            AS MaGiaoDich,
        -- Tên khách hàng: join qua KhachHang → ThongTin
        COALESCE(kh_dt.HoTen, N'Khách vãng lai') AS TenKhachHang,
        -- Loại giao dịch = loại đơn hàng (POS / Online / Đoàn...)
        COALESCE(dh.NguonBan, 'POS')            AS LoaiGiaoDich,
        -- Nhóm sản phẩm từ LoaiSanPham trong DanhMuc
        COALESCE(sp.LoaiSanPham, N'Khác')       AS NhomSanPham,
        COALESCE(sp.TenSanPham, N'Không rõ')    AS TenSanPham,
        -- Doanh thu = đơn giá thực tế × số lượng (đã chiết khấu)
        ctdh.DonGiaThucTe * ctdh.SoLuong       AS DoanhThu,
        ctdh.GhiChu                             AS GhiChuChiTiet,
        -- Thu ngân phụ trách phiên
        COALESCE(nv_dt.HoTen, N'Không rõ')      AS NhanVienThuNgan,
        -- Phương thức thanh toán từ bảng TaiChinh
        COALESCE(tt.PhuongThuc, N'Khác')        AS PhuongThucThanhToan
    FROM BanHang.ChiTietDonHang   ctdh
    INNER JOIN BanHang.DonHang    dh   ON dh.Id          = ctdh.IdDonHang
    LEFT  JOIN DanhMuc.SanPham    sp    ON sp.Id         = ctdh.IdSanPham
    -- Khách hàng: DonHang.IdKhachHang → KhachHang.IdDoiTac → ThongTin.Id
    LEFT  JOIN DoiTac.KhachHang   kh    ON kh.IdDoiTac   = dh.IdKhachHang
    LEFT  JOIN DoiTac.ThongTin    kh_dt ON kh_dt.Id      = kh.IdDoiTac
    -- Nối phiên thu ngân → nhân viên thu ngân
    LEFT  JOIN BanHang.PhienThuNgan ptn ON ptn.Id         = dh.IdPhienThuNgan
    LEFT  JOIN DoiTac.NhanVien    nv   ON nv.IdDoiTac    = ptn.IdNhanVien
    LEFT  JOIN DoiTac.ThongTin    nv_dt ON nv_dt.Id      = nv.IdDoiTac
    -- Phương thức thanh toán (lấy 1 dòng đầu tiên nếu nhiều)
    OUTER APPLY (
        SELECT TOP 1
            COALESCE(td.NhanHienThi, cttc.PhuongThuc) AS PhuongThuc
        FROM TaiChinh.ChungTuTC cttc
        LEFT JOIN HeThong.TuDien td ON td.NhomMa = 'PHUONG_THUC_TT'
                                    AND td.Ma    = cttc.PhuongThuc
        WHERE cttc.IdDonHang = dh.Id
        ORDER BY cttc.Id
    ) tt
    WHERE dh.NgayTao >= @TuNgay
      AND dh.NgayTao <  @DenNgay
      AND dh.TrangThai NOT IN ('DaHuy')
    ORDER BY dh.NgayTao;
END
GO

GO
