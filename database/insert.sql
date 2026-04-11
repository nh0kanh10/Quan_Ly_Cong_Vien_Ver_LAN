USE DaiNamResort;
GO
SET QUOTED_IDENTIFIER ON;
SET ARITHABORT ON;
SET XACT_ABORT ON;
BEGIN TRANSACTION;
BEGIN TRY
    DECLARE @SeedMode NVARCHAR(20) = 'baseline'; -- baseline | growth

    -- ==================== WAVE 1: ĐÃ CHUYỂN HOÀN TOÀN ====================
    -- Toàn bộ master data (VaiTro, QuyenHan, PhanQuyen, DonViTinh, KhuVuc,
    -- KhoHang, LoaiHinhDua, NhaCungCap, SuKien, KhanDai, DuongDua,
    -- KhuVucBien, ThietBiTaoSong, KhuVucThu, NhaHang, NhanVien Admin)
    -- → Database_DaiNam.sql Section 19 (19.1 - 19.20)

    -- ==================== WAVE 2: PHỤ THUỘC WAVE 1 ====================
    -- 2.1 NhanVien bổ sung (NV001 Admin đã có trong Database_DaiNam.sql)
    PRINT N'-- Đang nạp danh sách nhân viên bổ sung...';

    -- Lookup ID từ core data (đã tạo bởi Database_DaiNam.sql)
    DECLARE @role_admin INT = COALESCE((SELECT TOP 1 Id FROM VaiTro WHERE TenVaiTro = N'Admin'), 1);
    DECLARE @role_ql INT = COALESCE((SELECT TOP 1 Id FROM VaiTro WHERE TenVaiTro = N'QuanLy'), 2);
    DECLARE @role_nv INT = COALESCE((SELECT TOP 1 Id FROM VaiTro WHERE TenVaiTro = N'NhanVien'), 3);
    DECLARE @role_thukho INT = COALESCE((SELECT TOP 1 Id FROM VaiTro WHERE TenVaiTro = N'ThuKho'), 4);
    DECLARE @role_ketoan INT = COALESCE((SELECT TOP 1 Id FROM VaiTro WHERE TenVaiTro = N'KeToan'), 5);

    DECLARE @khu_ve INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV06');
    DECLARE @khu_bien INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV01');
    DECLARE @khu_dua INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV02');
    DECLARE @khu_thu INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV03');
    DECLARE @khu_nh INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV05');
    DECLARE @khu_ks INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV04');

    IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE MaCode = 'NV002')
        INSERT INTO NhanVien (MaCode, IdVaiTro, IdKhuVuc, HoTen, GioiTinh, ChucVu, BoPhan, DienThoai, Email, TenDangNhap, MatKhau, Cccd) 
        VALUES ('NV002', @role_ql, @khu_bien, N'Trần Thị B', N'Nữ', N'Quản lý khu biển', N'Vận hành', '0908123456', 'b.tran@dainam.com', 'QUANLYBIEN', 'quanly123', '001090654321');

    IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE MaCode = 'NV003')
        INSERT INTO NhanVien (MaCode, IdVaiTro, IdKhuVuc, HoTen, GioiTinh, ChucVu, BoPhan, DienThoai, Email, TenDangNhap, MatKhau, Cccd) 
        VALUES ('NV003', @role_ql, @khu_dua, N'Lê Văn C', N'Nam', N'Quản lý trường đua', N'Vận hành', '0907123456', 'c.le@dainam.com', 'QUANLYDUA', 'quanly123', '001090789012');

    IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE MaCode = 'NV004')
        INSERT INTO NhanVien (MaCode, IdVaiTro, IdKhuVuc, HoTen, GioiTinh, ChucVu, BoPhan, DienThoai, Email, TenDangNhap, MatKhau, Cccd) 
        VALUES ('NV004', @role_nv, @khu_ve, N'Phạm Thị D', N'Nữ', N'Nhân viên bán vé', N'Bán hàng', '0906123456', 'd.pham@dainam.com', 'NVVE', 'nv123', '001090456123');

    IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE MaCode = 'NV005')
        INSERT INTO NhanVien (MaCode, IdVaiTro, IdKhuVuc, HoTen, GioiTinh, ChucVu, BoPhan, DienThoai, Email, TenDangNhap, MatKhau, Cccd) 
        VALUES ('NV005', @role_nv, @khu_bien, N'Hoàng Văn E', N'Nam', N'Nhân viên cứu hộ', N'Cứu hộ', '0905123456', 'e.hoang@dainam.com', 'CUUHO1', 'nv123', '001090159357');

    IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE MaCode = 'NV006')
        INSERT INTO NhanVien (MaCode, IdVaiTro, IdKhuVuc, HoTen, GioiTinh, ChucVu, BoPhan, DienThoai, Email, TenDangNhap, MatKhau, Cccd) 
        VALUES ('NV006', @role_nv, @khu_thu, N'Ngô Thanh Tâm', N'Nữ', N'Nhân viên chăm sóc thú', N'Vườn thú', '0904123456', 'tam.ngo@dainam.com', 'NVTHU', 'nv123', '001090357951');

    IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE MaCode = 'NV007')
        INSERT INTO NhanVien (MaCode, IdVaiTro, IdKhuVuc, HoTen, GioiTinh, ChucVu, BoPhan, DienThoai, Email, TenDangNhap, MatKhau, Cccd) 
        VALUES ('NV007', @role_thukho, NULL, N'Đặng Văn G', N'Nam', N'Thủ kho', N'Kho', '0903123456', 'g.dang@dainam.com', 'THUKHO', 'thukho123', '001090753159');

    IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE MaCode = 'NV008')
        INSERT INTO NhanVien (MaCode, IdVaiTro, IdKhuVuc, HoTen, GioiTinh, ChucVu, BoPhan, DienThoai, Email, TenDangNhap, MatKhau, Cccd) 
        VALUES ('NV008', @role_ketoan, NULL, N'Vũ Thị H', N'Nữ', N'Kế toán trưởng', N'Tài chính', '0902123456', 'h.vu@dainam.com', 'KETOAN', 'ketoan123', '001090852456');

    -- 2.2 PhanQuyen: đã seed đầy đủ trong Database_DaiNam.sql (Section 19.3-19.7)

    -- 2.3 DoanKhach (B2B Pre-booked Groups)
    IF NOT EXISTS (SELECT 1 FROM DoanKhach WHERE TenDoan = N'Công ty Du lịch ABC')
        INSERT INTO DoanKhach (MaBooking, TenDoan, MaSoThue, NguoiDaiDien, DienThoaiLienHe, ChietKhau, NgayDen, TrangThai) VALUES
            ('BK-ABC-001', N'Công ty Du lịch ABC', '0123456789', N'Nguyễn Văn D', '0912345678', 10, CAST(DATEADD(day, 3, GETDATE()) AS DATE), N'DaDat'),
            ('BK-HOASEN-001', N'Trường học Mầm non Hoa Sen', NULL, N'Cô Nguyễn Thị E', '0901234567', 5, CAST(DATEADD(day, 7, GETDATE()) AS DATE), N'DaDat'),
            ('BK-XYZ-001', N'Công ty XYZ', '987654321', N'Lê Văn F', '0987654321', 15 , CAST(DATEADD(day, -2, GETDATE()) AS DATE), N'DaXuatVe');

    -- ==================== WAVE 3: PHỤ THUỘC WAVE 2 ====================
    -- 3.1 KhachHang (dữ liệu thực tế hơn: đủ thông tin cá nhân/công ty)
    DECLARE @doan1 INT = (SELECT Id FROM DoanKhach WHERE TenDoan = N'Công ty Du lịch ABC');
    DECLARE @doan2 INT = (SELECT Id FROM DoanKhach WHERE TenDoan = N'Trường học Mầm non Hoa Sen');

    DECLARE @kh_seed TABLE (
        HoTen NVARCHAR(100),
        NgaySinh DATE,
        GioiTinh NVARCHAR(10),
        Email NVARCHAR(100),
        DienThoai NVARCHAR(20),
        CmndCccd NVARCHAR(20),
        DiaChi NVARCHAR(255),
        LoaiKhach NVARCHAR(20),
        DiemTichLuy INT,
        IdDoan INT NULL
    );

    INSERT INTO @kh_seed (HoTen, NgaySinh, GioiTinh, Email, DienThoai, CmndCccd, DiaChi, LoaiKhach, DiemTichLuy, IdDoan)
    VALUES
    (N'Nguyễn Minh Khang', '1994-03-12', N'Nam', N'khang.nguyen@khach.vn', '0903001001', '079194001001', N'Phú Hòa, Thủ Dầu Một, Bình Dương', 'CaNhan', 120, NULL),
    (N'Trần Ngọc Diễm', '1997-08-20', N'Nữ', N'diem.tran@khach.vn', '0903001002', '079197001002', N'Lái Thiêu, Thuận An, Bình Dương', 'CaNhan', 85, NULL),
    (N'Lê Hoàng Nam', '1989-11-05', N'Nam', N'nam.le@khach.vn', '0903001003', '079189001003', N'Dĩ An, Bình Dương', 'CaNhan', 200, NULL),
    (N'Phạm Gia Hân', '2001-05-09', N'Nữ', N'han.pham@khach.vn', '0903001004', '079201001004', N'Bến Cát, Bình Dương', 'HocSinhSinhVien', 40, NULL),
    (N'Đặng Quốc Bảo', '1992-07-17', N'Nam', N'bao.dang@khach.vn', '0903001005', '079192001005', N'P. Phú Lợi, Thủ Dầu Một, Bình Dương', 'CaNhan', 150, NULL),
    (N'Vũ Thu Trang', '1996-12-01', N'Nữ', N'trang.vu@khach.vn', '0903001006', '079196001006', N'Hiệp Thành, Thủ Dầu Một, Bình Dương', 'CaNhan', 130, NULL),
    (N'Bùi Nhật Anh', '1988-02-26', N'Nam', N'anh.bui@khach.vn', '0903001007', '079188001007', N'Tân Uyên, Bình Dương', 'VIP', 300, NULL),
    (N'Ngô Mỹ Linh', '1999-09-19', N'Nữ', N'linh.ngo@khach.vn', '0903001008', '079199001008', N'Biên Hòa, Đồng Nai', 'CaNhan', 95, NULL),
    (N'Đoàn Đức Huy', '1990-04-14', N'Nam', N'huy.doan@khach.vn', '0903001009', '079190001009', N'Quận 12, TP. HCM', 'VIP', 520, NULL),
    (N'Phan Thảo My', '1998-10-23', N'Nữ', N'my.phan@khach.vn', '0903001010', '079198001010', N'Tân Bình, TP. HCM', 'CaNhan', 110, NULL),
    (N'Nguyễn Thanh Tùng', '1987-06-02', N'Nam', N'tung.nguyen@doanhnghiep.vn', '0903001011', '079187001011', N'KCN VSIP 1, Thuận An, Bình Dương', 'VIP', 260, NULL),
    (N'Lý Quỳnh Chi', '2000-01-16', N'Nữ', N'chi.ly@sinhvien.vn', '0903001012', '079200001012', N'Thủ Đức, TP. HCM', 'HocSinhSinhVien', 35, NULL),
    (N'Tạ Hải Yến', '1995-03-29', N'Nữ', N'yen.ta@khach.vn', '0903001013', '079195001013', N'Gò Vấp, TP. HCM', 'CaNhan', 160, NULL),
    (N'Nguyễn Hữu Lộc', '1985-12-11', N'Nam', N'loc.nguyen@vip.vn', '0903001014', '079185001014', N'Phú Nhuận, TP. HCM', 'VIP', 640, NULL),
    (N'Võ Gia Bảo', '1993-05-18', N'Nam', N'bao.vo@khach.vn', '0903001015', '079193001015', N'Tân Phước Khánh, Tân Uyên, Bình Dương', 'CaNhan', 75, NULL),
    (N'Hoàng Bích Ngọc', '1991-07-30', N'Nữ', N'ngoc.hoang@khach.vn', '0903001016', '079191001016', N'Bình Chuẩn, Thuận An, Bình Dương', 'CaNhan', 145, NULL),
    (N'Đinh Trọng Phúc', '1986-08-08', N'Nam', N'phuc.dinh@doanhnghiep.vn', '0903001017', '079186001017', N'KCN Mỹ Phước, Bến Cát, Bình Dương', 'VIP', 210, NULL),
    (N'Nguyễn Hồng Nhung', '1994-09-25', N'Nữ', N'nhung.nguyen@khach.vn', '0903001018', '079194001018', N'Phường An Phú, Thuận An, Bình Dương', 'CaNhan', 170, NULL),
    (N'Phạm Đức Tài', '1992-01-07', N'Nam', N'tai.pham@khach.vn', '0903001019', '079192001019', N'Bình Thắng, Dĩ An, Bình Dương', 'CaNhan', 180, NULL),
    (N'Đỗ Quỳnh Anh', '1998-11-27', N'Nữ', N'quynhanh.do@khach.vn', '0903001020', '079198001020', N'Quận 9, TP. HCM', 'CaNhan', 115, NULL),
    (N'Lâm Văn Trí', '1990-02-15', N'Nam', N'tri.lam@khach.vn', '0903001021', '079190001021', N'Bến Cát, Bình Dương', 'NoiBo', 25, NULL),
    (N'Trương Khánh Vy', '2002-04-21', N'Nữ', N'vy.truong@sinhvien.vn', '0903001022', '079202001022', N'Dĩ An, Bình Dương', 'HocSinhSinhVien', 30, NULL),
    (N'Đinh Minh Hiếu', '1996-06-10', N'Nam', N'hieu.dinh@khach.vn', '0903001023', '079196001023', N'Thủ Dầu Một, Bình Dương', 'CaNhan', 90, NULL),
    (N'Phạm Ngọc Ánh', '1997-12-09', N'Nữ', N'anh.phamngoc@khach.vn', '0903001024', '079197001024', N'Biên Hòa, Đồng Nai', 'CaNhan', 88, NULL);

    ;WITH src AS (
        SELECT
            ROW_NUMBER() OVER (ORDER BY HoTen, DienThoai) AS rn,
            *
        FROM @kh_seed
    )
    INSERT INTO KhachHang
        (MaCode, HoTen, NgaySinh, GioiTinh, Email, DienThoai, CmndCccd, DiaChi, LoaiKhach, DiemTichLuy, IdDoan, NgayDangKy)
    SELECT
        'KH' + RIGHT('0000' + CAST(s.rn AS VARCHAR(10)), 4),
        s.HoTen, s.NgaySinh, s.GioiTinh, s.Email, s.DienThoai, s.CmndCccd, s.DiaChi, s.LoaiKhach, s.DiemTichLuy, s.IdDoan,
        DATEADD(day, -(s.rn * 7 % 900), GETDATE())
    FROM src s
    WHERE NOT EXISTS (
        SELECT 1
        FROM KhachHang kh
        WHERE kh.Email = s.Email OR kh.DienThoai = s.DienThoai
    );

    -- Khách đoàn CŨ → Chuyển thành khách CaNhan (B2B booking tách riêng, không gắn vào profile cá nhân)
    INSERT INTO KhachHang (MaCode, HoTen, NgaySinh, GioiTinh, Email, DienThoai, CmndCccd, DiaChi, LoaiKhach, IdDoan, DiemTichLuy, NgayDangKy)
    SELECT q.MaCode, q.HoTen, q.NgaySinh, q.GioiTinh, q.Email, q.DienThoai, q.CmndCccd, q.DiaChi, q.LoaiKhach, q.IdDoan, q.DiemTichLuy, q.NgayDangKy
    FROM (
        SELECT 'KH0901' as MaCode, N'Nguyễn Gia Huy' as HoTen, CAST('1995-04-11' AS DATE) as NgaySinh, N'Nam' as GioiTinh, N'huy.nguyen@abc-travel.vn' as Email, '0903001901' as DienThoai, '079195009901' as CmndCccd, N'Thủ Dầu Một, Bình Dương' as DiaChi, 'CaNhan' as LoaiKhach, NULL as IdDoan, 0 as DiemTichLuy, GETDATE() as NgayDangKy UNION ALL
        SELECT 'KH0902', N'Trần Mỹ Duyên', CAST('1998-02-19' AS DATE), N'Nữ', N'duyen.tran@abc-travel.vn', '0903001902', '079198009902', N'Thuận An, Bình Dương', 'CaNhan', NULL, 0, GETDATE() UNION ALL
        SELECT 'KH0903', N'Phạm Quốc Khánh', CAST('1991-10-03' AS DATE), N'Nam', N'khanh.pham@hoasen.edu.vn', '0903001903', '079191009903', N'Dĩ An, Bình Dương', 'CaNhan', NULL, 0, GETDATE() UNION ALL
        SELECT 'KH0904', N'Đỗ Hà My', CAST('2000-12-27' AS DATE), N'Nữ', N'my.do@hoasen.edu.vn', '0903001904', '079200009904', N'Quận 12, TP. HCM', 'CaNhan', NULL, 0, GETDATE() UNION ALL
        SELECT 'KH0905', N'Bùi Văn Long', CAST('1993-06-22' AS DATE), N'Nam', N'long.bui@abc-travel.vn', '0903001905', '079193009905', N'Biên Hòa, Đồng Nai', 'CaNhan', NULL, 0, GETDATE()
    ) q
    WHERE NOT EXISTS (SELECT 1 FROM KhachHang WHERE MaCode = q.MaCode OR Email = q.Email OR DienThoai = q.DienThoai);

    -- 3.2 SanPham
    -- ==================== WAVE 3 + WAVE 4: ĐÃ CHUYỂN HOÀN TOÀN ====================
    -- SanPham, DongVat, Kiosk, BaiDoXe, Phong, TuDo, BanAn, ChoiNghiMat,
    -- QuyDoiDonVi, BangGia → Database_DaiNam.sql Section 20 (20.1 - 20.10)

    -- Khai báo biến cần cho WAVE 5 (giao dịch giả lập)
    DECLARE @sp_sup INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP011');
    DECLARE @sp_dlx INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP012');
    DECLARE @sp_fam INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP013_F');
    DECLARE @sp_vip INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP014_V');


    -- ==================== WAVE 5: GIAO DỊCH & VẬN HÀNH ====================
    -- 5.1 ViDienTu & TheRFID (cho 100 khách)
    DECLARE khach_hang_cursor CURSOR FOR 
        SELECT Id, MaCode FROM KhachHang WHERE LoaiKhach = 'CaNhan' OR Id IN (SELECT Id FROM KhachHang WHERE MaCode LIKE 'KH20%');
    DECLARE @kh_id INT, @kh_ma VARCHAR(20);
    OPEN khach_hang_cursor;
    FETCH NEXT FROM khach_hang_cursor INTO @kh_id, @kh_ma;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM ViDienTu WHERE IdKhachHang = @kh_id)
        BEGIN
            DECLARE @soTienNap INT = CAST(RAND()*2000000 AS INT);
            INSERT INTO ViDienTu (IdKhachHang, SoDuKhaDung, SoDuDongBang) VALUES (@kh_id, @soTienNap, 0);
            DECLARE @vi_id INT = SCOPE_IDENTITY();
            IF NOT EXISTS (SELECT 1 FROM TheRFID WHERE MaRfid = 'RFID' + @kh_ma)
                INSERT INTO TheRFID (MaRfid, IdVi, TrangThai) VALUES ('RFID' + @kh_ma, @vi_id, 'Active');

            -- [VÁ LỖ HỔNG]: Tạo vết Nạp Tiền
            IF @soTienNap > 0
            BEGIN
                INSERT INTO GiaoDichVi (MaCode, IdVi, LoaiGiaoDich, SoTien, ThoiGian, CreatedBy)
                VALUES (CONCAT('GDN-', @kh_ma), @vi_id, 'NapTien', @soTienNap, GETDATE(), 1);
                DECLARE @gd_nap_id INT = SCOPE_IDENTITY();

                INSERT INTO PhieuThu (MaCode, IdGiaoDichVi, SoTien, PhuongThuc, ThoiGian, CreatedBy)
                VALUES (CONCAT('PTN-', @kh_ma), @gd_nap_id, @soTienNap, 'TienMat', GETDATE(), 1);
            END
        END
        FETCH NEXT FROM khach_hang_cursor INTO @kh_id, @kh_ma;
    END
    CLOSE khach_hang_cursor;
    DEALLOCATE khach_hang_cursor;

    -- 5.2 Combo & ComboChiTiet (tạo combo và kích hoạt)
    DECLARE @combo_id1 INT, @combo_id2 INT;
    IF NOT EXISTS (SELECT 1 FROM Combo WHERE MaCode = 'COMBO01')
    BEGIN
        -- DANH MỤC COMBO ĐẠI NAM (CẤM SỬA - QUY ĐỔI TOKEN TỰ ĐỘNG)
        INSERT INTO Combo (MaCode, Ten, Gia, MoTa, TrangThai) VALUES
            ('COMBO01', N'Combo Gia Đình Biển', 450000, N'2 vé người lớn + 1 vé trẻ em + 1 suất ăn trưa', 'BanNhap'),
            ('COMBO02', N'Combo Trải Nghiệm Đua', 350000, N'Vé đua ngựa + thuê xe điện 1h', 'BanNhap'),
            ('CB_GAME_3', N'Combo 3 trò chơi', 120000, N'Hợp lệ cho 3 lượt chơi bất kỳ', 'BanNhap'),
            ('CB_GAME_6', N'Combo 6 trò chơi', 200000, N'Hợp lệ cho 6 lượt chơi bất kỳ', 'BanNhap'),
            ('CB_GAME_12', N'Combo 12 trò chơi', 400000, N'Hợp lệ cho 12 lượt chơi bất kỳ', 'BanNhap');
        SET @combo_id1 = SCOPE_IDENTITY() - 4;
        SET @combo_id2 = SCOPE_IDENTITY() - 3;
    END
    ELSE
    BEGIN
        SELECT @combo_id1 = Id FROM Combo WHERE MaCode = 'COMBO01';
        SELECT @combo_id2 = Id FROM Combo WHERE MaCode = 'COMBO02';
    END

    -- Xóa chi tiết cũ nếu có và thêm mới
    DELETE FROM ComboChiTiet WHERE IdCombo IN (@combo_id1, @combo_id2, (SELECT Id FROM Combo WHERE MaCode LIKE 'CB_GAME_%'));
    DECLARE @sp_ve_nguoi_lon INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP001');
    DECLARE @sp_ve_tre_em INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP002');
    DECLARE @sp_combo_an_trua INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP101');
    DECLARE @sp_ve_xem_dua INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP008');
    DECLARE @sp_thue_xe_dien INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP007');
    DECLARE @sp_game_any INT = (SELECT Id FROM SanPham WHERE MaCode = 'GAME_01'); -- Đại diện cho 1 trò chơi

    INSERT INTO ComboChiTiet (IdCombo, IdSanPham, SoLuong, TyLePhanBo) VALUES
        (@combo_id1, @sp_ve_nguoi_lon, 2, 33),
        (@combo_id1, @sp_ve_tre_em, 1, 18),
        (@combo_id1, @sp_combo_an_trua, 1, 49),
        (@combo_id2, @sp_ve_xem_dua, 1, 50),
        (@combo_id2, @sp_thue_xe_dien, 1, 50),
        ((SELECT Id FROM Combo WHERE MaCode = 'CB_GAME_3'), @sp_game_any, 3, 100),
        ((SELECT Id FROM Combo WHERE MaCode = 'CB_GAME_6'), @sp_game_any, 6, 100),
        ((SELECT Id FROM Combo WHERE MaCode = 'CB_GAME_12'), @sp_game_any, 12, 100);
    -- Kích hoạt combo
    UPDATE Combo SET TrangThai = 'KichHoat' WHERE Id IN (@combo_id1, @combo_id2) OR MaCode LIKE 'CB_GAME_%';

    -- 5.3 DonHang & ChiTietDonHang (tạo 1000 đơn hàng, thời gian từ 2023 đến nay)
    -- Danh sách sản phẩm và combo để random
    DECLARE @sp_ids TABLE (Id INT, Gia DECIMAL(15,0));
    INSERT INTO @sp_ids SELECT Id, DonGia FROM SanPham WHERE LoaiSanPham IN ('Ve','AnUong');
    DECLARE @combo_ids TABLE (Id INT, Gia DECIMAL(15,0));
    INSERT INTO @combo_ids SELECT Id, Gia FROM Combo WHERE TrangThai = 'KichHoat';

    DECLARE kh_cursor CURSOR FOR SELECT Id FROM KhachHang WHERE LoaiKhach = 'CaNhan' OR Id IN (SELECT Id FROM KhachHang WHERE MaCode LIKE 'KH20%');
    OPEN kh_cursor;
    FETCH NEXT FROM kh_cursor INTO @kh_id;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @dh_count INT = CAST(RAND()*10 AS INT); -- mỗi khách 0-10 đơn
        WHILE @dh_count > 0
        BEGIN
            DECLARE @don_hang_id INT;
            DECLARE @ngay_dat DATETIME = DATEADD(day, -CAST(RAND()*1095 AS INT), GETDATE());
            -- Tạo mã đơn hàng độc bản dựa trên thông tin thời gian và một phần GUID để cực kỳ khó trùng
            DECLARE @ma_dh VARCHAR(20) = CONCAT('DH', FORMAT(@ngay_dat, 'yyMM'), RIGHT(REPLACE(NEWID(),'-',''), 8));
            IF NOT EXISTS (SELECT 1 FROM DonHang WHERE MaCode = @ma_dh)
            BEGIN
                INSERT INTO DonHang (MaCode, IdKhachHang, ThoiGian, TongTien, TrangThai, CreatedBy)
                VALUES (@ma_dh, @kh_id, @ngay_dat, 0, 'DaThanhToan', 1);
                SET @don_hang_id = SCOPE_IDENTITY();
                DECLARE @total DECIMAL(15,0) = 0;
                DECLARE @line_count INT = CAST(RAND()*4 AS INT) + 1;
                WHILE @line_count > 0
                BEGIN
                    DECLARE @type INT = CAST(RAND()*2 AS INT);
                    DECLARE @item_id INT, @price DECIMAL(15,0), @qty INT;
                    IF @type = 0 AND EXISTS (SELECT 1 FROM @sp_ids)
                    BEGIN
                        SELECT TOP 1 @item_id = Id, @price = Gia FROM @sp_ids ORDER BY NEWID();
                        SET @qty = CAST(RAND()*3 AS INT) + 1;
                        INSERT INTO ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaGoc, DonGiaThucTe)
                        VALUES (@don_hang_id, @item_id, @qty, @price, @price);

                        -- [VÁ LỖ HỔNG]: Ghi Sổ Cái Kho
                        INSERT INTO TheKho (IdKho, IdSanPham, LoaiGiaoDich, SoLuongThayDoi, TonCuoi, IdThamChieu, ThoiGianGiaoDich, CreatedBy, GhiChu)
                        VALUES ((SELECT TOP 1 Id FROM KhoHang WHERE TenKho = N'Kho Kiosk biển'), @item_id, 'XUAT_POS', -@qty, NULL, @don_hang_id, @ngay_dat, 1, N'Bán hàng POS (Đợt 1)');
                        SET @total = @total + @price * @qty;
                    END
                    ELSE IF @type = 1 AND EXISTS (SELECT 1 FROM @combo_ids)
                    BEGIN
                        SELECT TOP 1 @item_id = Id, @price = Gia FROM @combo_ids ORDER BY NEWID();
                        SET @qty = 1;
                        INSERT INTO ChiTietDonHang (IdDonHang, IdCombo, SoLuong, DonGiaGoc, DonGiaThucTe)
                        VALUES (@don_hang_id, @item_id, @qty, @price, @price);
                        SET @total = @total + @price;
                    END
                    SET @line_count = @line_count - 1;
                END
                UPDATE DonHang SET TongTien = @total WHERE Id = @don_hang_id;
            END
            SET @dh_count = @dh_count - 1;
        END
        FETCH NEXT FROM kh_cursor INTO @kh_id;
    END
    CLOSE kh_cursor;
    DEALLOCATE kh_cursor;

    -- 5.4 ThueDoChiTiet, ThueTu (thuê đồ)
    DECLARE @sp_phao INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP004');
    DECLARE @sp_tu_s INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP005');
    DECLARE @sp_tu_l INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP006');
    DECLARE @sp_xe_dien INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP007');
    DECLARE @map_thue TABLE (IdCTDH INT, IdDonHang INT);
    INSERT INTO ChiTietDonHang (IdDonHang, SoLuong, DonGiaGoc, DonGiaThucTe)
    OUTPUT INSERTED.Id, INSERTED.IdDonHang INTO @map_thue
    SELECT TOP 300 dh.Id, ABS(CHECKSUM(NEWID())) % 3 + 1, 0, 0
    FROM DonHang dh
    WHERE dh.Id % 5 = 0
      AND NOT EXISTS (SELECT 1 FROM ThueDoChiTiet t JOIN ChiTietDonHang c ON c.Id = t.IdChiTietDonHang WHERE c.IdDonHang = dh.Id);

    INSERT INTO ThueDoChiTiet (IdChiTietDonHang, IdSanPham, SoLuong, ThoiGianBatDau, SoTienCoc, TrangThaiCoc)
    SELECT ct.IdCTDH, CASE WHEN ABS(CHECKSUM(NEWID())) % 3 = 0 THEN @sp_phao ELSE @sp_tu_s END, c.SoLuong, dh.ThoiGian, 50000, 'ChuaHoan'
    FROM @map_thue ct
    JOIN DonHang dh ON ct.IdDonHang = dh.Id
    JOIN ChiTietDonHang c ON c.Id = ct.IdCTDH;

    INSERT INTO ThueTu (IdChiTietThue, IdTuDo, MaPin)
    SELECT td.Id, t.Id, RIGHT('0000' + CAST(ABS(CHECKSUM(NEWID())) % 10000 AS VARCHAR), 4)
    FROM ThueDoChiTiet td
    CROSS APPLY (SELECT TOP 1 Id FROM TuDo WHERE TrangThai = 'Trong' ORDER BY NEWID()) t
    WHERE td.IdSanPham IN (@sp_tu_s, @sp_tu_l);

    -- 5.5 DatPhongChiTiet & ChiTietDatPhong
    DECLARE @map_phong TABLE (IdCTDH INT, IdDonHang INT);
    INSERT INTO ChiTietDonHang (IdDonHang, SoLuong, DonGiaGoc, DonGiaThucTe)
    OUTPUT INSERTED.Id, INSERTED.IdDonHang INTO @map_phong
    SELECT TOP 200 dh.Id, 1, 0, 0
    FROM DonHang dh
    WHERE dh.Id % 7 = 0
      AND NOT EXISTS (SELECT 1 FROM DatPhongChiTiet dp JOIN ChiTietDonHang c ON c.Id = dp.IdChiTietDonHang WHERE c.IdDonHang = dh.Id);

    INSERT INTO DatPhongChiTiet (IdChiTietDonHang, NgayNhan, NgayTra, TrangThai)
    SELECT ct.IdCTDH, 
           DATEADD(day, r.off1, dh.ThoiGian) AS NgayNhan,
           DATEADD(day, r.off1 + r.off2, dh.ThoiGian) AS NgayTra,
           'DaDat'
    FROM @map_phong ct
    JOIN DonHang dh ON ct.IdDonHang = dh.Id
    CROSS APPLY (
        SELECT 
            ABS(CHECKSUM(NEWID())) % 30 AS off1,
            ABS(CHECKSUM(NEWID())) % 5 + 1 AS off2
    ) r
    WHERE dh.Id % 7 = 0;
    INSERT INTO ChiTietDatPhong (IdDatPhongChiTiet, IdPhong, DonGiaThucTe)
    SELECT dp.Id, p.Id, sp.DonGia
    FROM DatPhongChiTiet dp
    CROSS APPLY (
        SELECT TOP 1 ph.Id, lp.IdSanPham 
        FROM Phong ph 
        JOIN LoaiPhong lp ON ph.IdLoaiPhong = lp.Id 
        WHERE ph.TrangThai = 'Trong' 
        ORDER BY NEWID()
    ) p
    JOIN SanPham sp ON sp.Id = p.IdSanPham;

    -- 5.6 GiaGuiXe (Bảng giá giữ xe)
    IF NOT EXISTS (SELECT 1 FROM GiaGuiXe)
    BEGIN
        INSERT INTO GiaGuiXe (LoaiXe, TenLoaiXe, GiaBanNgay, GiaQuaDem) VALUES
            ('XeDap',  N'Xe đạp',    5000,  3000),
            ('XeMay',  N'Xe máy',   10000,  5000),
            ('OTo',    N'Ô tô',    30000, 20000),
            ('XeDien', N'Xe điện',  15000,  8000);
    END

    -- 5.7 LuotVaoRaBaiXe & VeDoXeChiTiet
    DECLARE @rfid_list TABLE (MaRfid VARCHAR(50));
    INSERT INTO @rfid_list SELECT MaRfid FROM TheRFID;
    DECLARE rfid_cursor CURSOR FOR SELECT MaRfid FROM @rfid_list;
    DECLARE @rfid VARCHAR(50);
    OPEN rfid_cursor;
    FETCH NEXT FROM rfid_cursor INTO @rfid;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @xe_vao INT = CAST(RAND()*10 AS INT);
        WHILE @xe_vao > 0
        BEGIN
            DECLARE @vao DATETIME = DATEADD(hour, -CAST(RAND()*72 AS INT), GETDATE());
            DECLARE @rand_loai INT = ABS(CHECKSUM(NEWID())) % 4;
            DECLARE @loai_xe_val NVARCHAR(20) = CASE @rand_loai WHEN 0 THEN 'XeDap' WHEN 1 THEN 'XeMay' WHEN 2 THEN 'OTo' ELSE 'XeDien' END;
            INSERT INTO LuotVaoRaBaiXe (BienSo, LoaiXe, MaRfid, ThoiGianVao, TrangThai)
            VALUES (CONCAT('51A-', CAST(ABS(CHECKSUM(NEWID())) % 100000 AS VARCHAR)), @loai_xe_val, @rfid, @vao, 'DaTra');
            DECLARE @luot_id INT = SCOPE_IDENTITY();
            DECLARE @ra DATETIME = DATEADD(hour, CAST(RAND()*5 AS INT)+1, @vao);
            UPDATE LuotVaoRaBaiXe SET ThoiGianRa = @ra WHERE Id = @luot_id;
            -- Tạo vé đỗ xe (Sử dụng ID của LuotVaoRa để đảm bảo duy nhất tuyệt đối)
            DECLARE @don_hang_xe INT;
            DECLARE @ma_dx VARCHAR(20) = CONCAT('DX', RIGHT('0000000' + CAST(@luot_id AS VARCHAR), 8));
            IF NOT EXISTS (SELECT 1 FROM DonHang WHERE MaCode = @ma_dx)
            BEGIN
                INSERT INTO DonHang (MaCode, ThoiGian, TongTien, TrangThai) 
                VALUES (@ma_dx, @vao, 0, 'DaThanhToan');
                SET @don_hang_xe = SCOPE_IDENTITY();
                -- Tính giá từ bảng GiaGuiXe
                DECLARE @gia_ngay DECIMAL(18,0) = ISNULL((SELECT GiaBanNgay FROM GiaGuiXe WHERE LoaiXe = @loai_xe_val), 10000);
                DECLARE @tien_xe DECIMAL(15,0) = @gia_ngay;
                UPDATE DonHang SET TongTien = @tien_xe WHERE Id = @don_hang_xe;
                DECLARE @map_xe TABLE (IdCTDH INT, IdDonHang INT);
                INSERT INTO ChiTietDonHang (IdDonHang, SoLuong, DonGiaGoc, DonGiaThucTe)
                OUTPUT INSERTED.Id, INSERTED.IdDonHang INTO @map_xe
                VALUES (@don_hang_xe, 1, @tien_xe, @tien_xe);
                
                INSERT INTO VeDoXeChiTiet (IdChiTietDonHang, IdLuotVaoRa, TienPhaiTra)
                SELECT ct.IdCTDH, @luot_id, @tien_xe
                FROM @map_xe ct;
                DELETE FROM @map_xe;
            END
            SET @xe_vao = @xe_vao - 1;
        END
        FETCH NEXT FROM rfid_cursor INTO @rfid;
    END
    CLOSE rfid_cursor;
    DEALLOCATE rfid_cursor;

    -- 5.7 LichTaoSong & ChatLuongNuoc (lịch sử 3 năm)
    DECLARE song_cursor CURSOR FOR SELECT Id FROM ThietBiTaoSong;
    DECLARE @tb_id INT;
    OPEN song_cursor;
    FETCH NEXT FROM song_cursor INTO @tb_id;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @song_ngay DATETIME = '2023-01-01';
        WHILE @song_ngay <= GETDATE()
        BEGIN
            IF NOT EXISTS (
                SELECT 1 FROM LichTaoSong
                WHERE IdThietBi = @tb_id
                  AND ThoiGianBatDau = DATEADD(hour, 8, @song_ngay)
                  AND ThoiGianKetThuc = DATEADD(hour, 17, @song_ngay)
            )
                INSERT INTO LichTaoSong (IdThietBi, ThoiGianBatDau, ThoiGianKetThuc, KieuSong)
                VALUES (@tb_id, DATEADD(hour, 8, @song_ngay), DATEADD(hour, 17, @song_ngay), N'Sóng nhẹ');
            SET @song_ngay = DATEADD(day, 1, @song_ngay);
        END
        FETCH NEXT FROM song_cursor INTO @tb_id;
    END
    CLOSE song_cursor;
    DEALLOCATE song_cursor;

    -- Chất lượng nước hàng ngày
    INSERT INTO ChatLuongNuoc (IdKhuVucBien, Ngay, DoMan, PH, NhietDo, DoTrong, TrangThaiVeSinh)
    SELECT k.IdKhuVuc, CAST(GETDATE() AS DATE), 
           2.5 + r.val1, 
           7 + r.val2, 
           28 + r.val3, 
           100 + r.val4, 
           'Dat'
    FROM KhuVucBien k
    CROSS APPLY (
        SELECT 
            CAST(ABS(CHECKSUM(NEWID())) % 100 AS FLOAT) / 100.0 AS val1,
            CAST(ABS(CHECKSUM(NEWID())) % 50 AS FLOAT) / 100.0 AS val2,
            CAST(ABS(CHECKSUM(NEWID())) % 300 AS FLOAT) / 100.0 AS val3,
            ABS(CHECKSUM(NEWID())) % 30 AS val4
    ) r
    WHERE NOT EXISTS (
        SELECT 1 FROM ChatLuongNuoc c
        WHERE c.IdKhuVucBien = k.IdKhuVuc AND c.Ngay = CAST(GETDATE() AS DATE)
    );

    -- 5.8 CaTrucCuuHo
    DECLARE @nv_cuuho_id INT = (SELECT Id FROM NhanVien WHERE MaCode='NV005');
    INSERT INTO CaTrucCuuHo (IdNhanVien, IdKhuVucBien, ThoiGianBatDau, ThoiGianKetThuc)
    SELECT
        @nv_cuuho_id,
        kb.IdKhuVuc,
        ca.BatDau,
        DATEADD(hour, 8, ca.BatDau)
    FROM KhuVucBien kb
    CROSS APPLY (
        SELECT DATEADD(day, -ABS(CHECKSUM(NEWID())) % 365, CAST(CAST(GETDATE() AS DATE) AS DATETIME)) AS BatDau
    ) ca
    WHERE NOT EXISTS (
        SELECT 1 FROM CaTrucCuuHo ex
        WHERE ex.IdNhanVien = @nv_cuuho_id
          AND ex.IdKhuVucBien = kb.IdKhuVuc
          AND CAST(ex.ThoiGianBatDau AS DATE) = CAST(ca.BatDau AS DATE)
    );

    -- 5.9 GiaiDua, LichThiDau, VanDongVien, NguaDua, KetQuaDua
    IF NOT EXISTS (SELECT 1 FROM GiaiDua WHERE TenGiai = N'Giải vô địch Đại Nam 2025')
    BEGIN
        INSERT INTO GiaiDua (TenGiai, ThoiGianBatDau, ThoiGianKetThuc) VALUES 
            (N'Giải vô địch Đại Nam 2025', '2025-04-10', '2025-04-12'),
            (N'Giải vô địch Đại Nam 2024', '2024-04-10', '2024-04-12'),
            (N'Giải vô địch Đại Nam 2023', '2023-04-10', '2023-04-12');
    END

    IF NOT EXISTS (SELECT 1 FROM VanDongVien WHERE HoTen = N'Quốc Bảo')
        INSERT INTO VanDongVien (HoTen, LoaiVdv) VALUES
            (N'Quốc Bảo', 'NaiNgua'), (N'Trung Kiên', 'TayDua'), (N'Hoàng Anh', 'ChoDua');
    INSERT INTO NguaDua (TenNgua, IdVdv, Tuoi) VALUES 
        (N'Xích Thố', (SELECT Id FROM VanDongVien WHERE HoTen=N'Quốc Bảo'), 5);

    -- 5.10 DatChoThuAn & LichChoAn
    INSERT INTO LichChoAn (IdDongVat, ThoiGian, ThucAn, NguoiPhuTrach)
    SELECT Id, DATEADD(day, -CAST(RAND()*1095 AS INT), GETDATE()), N'Thịt bò 5kg', @nv_cuuho_id
    FROM DongVat WHERE Loai = N'Thú ăn thịt';

    DECLARE @map_thu TABLE (IdCTDH INT, IdDonHang INT);
    INSERT INTO ChiTietDonHang (IdDonHang, SoLuong, DonGiaGoc, DonGiaThucTe)
    OUTPUT INSERTED.Id, INSERTED.IdDonHang INTO @map_thu
    SELECT TOP 200 dh.Id, 1, 0, 0
    FROM DonHang dh
    WHERE dh.Id % 3 = 0
      AND NOT EXISTS (SELECT 1 FROM DatChoThuAn d JOIN ChiTietDonHang c ON c.Id = d.IdChiTietDonHang WHERE c.IdDonHang = dh.Id);

    INSERT INTO DatChoThuAn (IdChiTietDonHang, IdDongVat, ThoiGianDuKien, TrangThai)
    SELECT ct.IdCTDH, (SELECT TOP 1 Id FROM DongVat ORDER BY NEWID()), 
           DATEADD(day, ABS(CHECKSUM(NEWID())) % 30, dh.ThoiGian), 
           'ChuaSuDung'
    FROM @map_thu ct
    JOIN DonHang dh ON ct.IdDonHang = dh.Id;

    -- 5.11 DatBan & ChiTietDatBan
    DECLARE @map_ban TABLE (IdCTDH INT, IdDonHang INT);
    INSERT INTO ChiTietDonHang (IdDonHang, SoLuong, DonGiaGoc, DonGiaThucTe)
    OUTPUT INSERTED.Id, INSERTED.IdDonHang INTO @map_ban
    SELECT TOP 200 dh.Id, 1, 0, 0
    FROM DonHang dh
    WHERE dh.Id % 5 = 0
      AND NOT EXISTS (SELECT 1 FROM DatBan db JOIN ChiTietDonHang c ON c.Id = db.IdChiTietDonHang WHERE c.IdDonHang = dh.Id);

    INSERT INTO DatBan (IdChiTietDonHang, IdNhaHang, ThoiGianDenDuKien, SoLuongKhach, TrangThai)
    SELECT ct.IdCTDH, (SELECT TOP 1 Id FROM NhaHang ORDER BY NEWID()), 
           DATEADD(day, r.offSet, dh.ThoiGian), 
           r.khach, 
           'DaDat'
    FROM @map_ban ct
    JOIN DonHang dh ON ct.IdDonHang = dh.Id
    CROSS APPLY (
        SELECT 
            ABS(CHECKSUM(NEWID())) % 30 AS offSet,
            (ABS(CHECKSUM(NEWID())) % 10) + 2 AS khach
    ) r;

    INSERT INTO ChiTietDatBan (IdDatBan, IdBanAn)
    SELECT db.Id, b.Id
    FROM DatBan db
    CROSS APPLY (SELECT TOP 1 Id FROM BanAn WHERE TrangThai='Trong' ORDER BY NEWID()) b;

    -- 5.12 PhieuNhapKho, ChiTietNhapKho, PhieuXuatKho, TonKho
    DECLARE @kho_tw_id INT = (SELECT Id FROM KhoHang WHERE TenKho = N'Kho trung tâm');
    DECLARE @kho_kiosk_id INT = (SELECT Id FROM KhoHang WHERE TenKho = N'Kho Kiosk biển');
    DECLARE @sp_nuoc_id_2 INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP015');

    -- Nhập kho hàng tháng trong 3 năm
    DECLARE @ngay_nhap DATE = '2023-01-01';
    DECLARE @pc_nhap_id INT;
    DECLARE @pn_id INT;
    WHILE @ngay_nhap <= GETDATE()
    BEGIN
        INSERT INTO PhieuChi (MaCode, SoTien, LyDo, ThoiGian, CreatedBy)
        VALUES (CONCAT('PC-BIA-', FORMAT(@ngay_nhap, 'yyyyMMdd')), 4400000, N'Thanh toán tiền nhập bia', @ngay_nhap, 1);
        SET @pc_nhap_id = SCOPE_IDENTITY();

        INSERT INTO PhieuNhapKho (IdKho, IdNhaCungCap, NgayNhap, TongTien, IdPhieuChi, CreatedBy)
        VALUES (@kho_tw_id, (SELECT Id FROM NhaCungCap WHERE Ten=N'Công ty Bia Sài Gòn'), @ngay_nhap, 4400000, @pc_nhap_id, 1);
        SET @pn_id = SCOPE_IDENTITY();

        INSERT INTO ChiTietNhapKho (IdPhieuNhap, IdSanPham, SoLuong, DonGiaNhap)
        VALUES (@pn_id, @sp_nuoc_id_2, 20, 220000);

        INSERT INTO TheKho (IdKho, IdSanPham, LoaiGiaoDich, SoLuongThayDoi, TonCuoi, DonGiaVatTu, IdThamChieu, ThoiGianGiaoDich, CreatedBy, GhiChu)
        VALUES (@kho_tw_id, @sp_nuoc_id_2, 'NHAP_KHO', 20, NULL, 22000, @pn_id, @ngay_nhap, 1, N'Seed Data Nhập Kho');
        SET @ngay_nhap = DATEADD(month, 1, @ngay_nhap);
    END

    -- Xuất kho hàng tháng
    SET @ngay_nhap = '2023-01-01';
    DECLARE @px_id INT;
    WHILE @ngay_nhap <= GETDATE()
    BEGIN
        INSERT INTO PhieuXuatKho (IdKhoXuat, IdKhoNhan, NgayXuat, LyDo, CreatedBy)
        VALUES (@kho_tw_id, @kho_kiosk_id, @ngay_nhap, N'Chuyển hàng ra kiosk', 1);
        SET @px_id = SCOPE_IDENTITY();

        INSERT INTO ChiTietXuatKho (IdPhieuXuat, IdSanPham, SoLuong)
        VALUES (@px_id, @sp_nuoc_id_2, 5);

        INSERT INTO TheKho (IdKho, IdSanPham, LoaiGiaoDich, SoLuongThayDoi, TonCuoi, IdThamChieu, ThoiGianGiaoDich, CreatedBy, GhiChu)
        VALUES (@kho_tw_id, @sp_nuoc_id_2, 'CHUYEN_KHO', -5, NULL, @px_id, @ngay_nhap, 1, N'Chuyển ra Kiosk');
    
        INSERT INTO TheKho (IdKho, IdSanPham, LoaiGiaoDich, SoLuongThayDoi, TonCuoi, IdThamChieu, ThoiGianGiaoDich, CreatedBy, GhiChu)
        VALUES (@kho_kiosk_id, @sp_nuoc_id_2, 'CHUYEN_KHO', 5, NULL, @px_id, @ngay_nhap, 1, N'Nhận từ Kho Trung Tâm');
        SET @ngay_nhap = DATEADD(month, 1, @ngay_nhap);
    END

    -- Tồn kho cuối kỳ: tính theo chứng từ nhập/xuất để tránh nghịch lý
    DECLARE @nhap_tw INT = ISNULL((
        SELECT SUM(ctn.SoLuong)
        FROM ChiTietNhapKho ctn
        JOIN PhieuNhapKho pn ON pn.Id = ctn.IdPhieuNhap
        WHERE pn.IdKho = @kho_tw_id AND ctn.IdSanPham = @sp_nuoc_id_2
    ), 0);

    DECLARE @xuat_tw INT = ISNULL((
        SELECT SUM(ctx.SoLuong)
        FROM ChiTietXuatKho ctx
        JOIN PhieuXuatKho px ON px.Id = ctx.IdPhieuXuat
        WHERE px.IdKhoXuat = @kho_tw_id AND ctx.IdSanPham = @sp_nuoc_id_2
    ), 0);

    DECLARE @nhan_kiosk INT = ISNULL((
        SELECT SUM(ctx.SoLuong)
        FROM ChiTietXuatKho ctx
        JOIN PhieuXuatKho px ON px.Id = ctx.IdPhieuXuat
        WHERE px.IdKhoNhan = @kho_kiosk_id AND ctx.IdSanPham = @sp_nuoc_id_2
    ), 0);

    DECLARE @ton_tw INT = CASE WHEN @nhap_tw - @xuat_tw < 0 THEN 0 ELSE @nhap_tw - @xuat_tw END;
    DECLARE @kiosk_sold INT = ISNULL((
        SELECT SUM(ctx.SoLuong)
        FROM ChiTietDonHang ctx
        JOIN DonHang dh ON dh.Id = ctx.IdDonHang
        WHERE ctx.IdSanPham = @sp_nuoc_id_2 AND dh.TrangThai = 'DaThanhToan'
    ), 0);
    DECLARE @ton_kiosk INT = CASE WHEN @nhan_kiosk - @kiosk_sold < 0 THEN 0 ELSE @nhan_kiosk - @kiosk_sold END; -- Đã trừ doanh số thực tế bán ra

    IF EXISTS (SELECT 1 FROM TonKho WHERE IdKho = @kho_tw_id AND IdSanPham = @sp_nuoc_id_2)
        UPDATE TonKho SET SoLuong = @ton_tw WHERE IdKho = @kho_tw_id AND IdSanPham = @sp_nuoc_id_2;
    ELSE
        INSERT INTO TonKho (IdKho, IdSanPham, SoLuong) VALUES (@kho_tw_id, @sp_nuoc_id_2, @ton_tw);

    IF EXISTS (SELECT 1 FROM TonKho WHERE IdKho = @kho_kiosk_id AND IdSanPham = @sp_nuoc_id_2)
        UPDATE TonKho SET SoLuong = @ton_kiosk WHERE IdKho = @kho_kiosk_id AND IdSanPham = @sp_nuoc_id_2;
    ELSE
        INSERT INTO TonKho (IdKho, IdSanPham, SoLuong) VALUES (@kho_kiosk_id, @sp_nuoc_id_2, @ton_kiosk);

    -- TonKhoKiosk has been merged into TonKho
    -- DECLARE @kiosk_bien_id INT = (SELECT Id FROM Kiosk WHERE ViTri = N'Cạnh biển nhân tạo');
    -- IF EXISTS (SELECT 1 FROM TonKhoKiosk WHERE IdKiosk = @kiosk_bien_id AND IdSanPham = @sp_nuoc_id_2)
    --     UPDATE TonKhoKiosk
    --     SET SoLuong = @ton_kiosk, NguongCanhBao = 20
    --     WHERE IdKiosk = @kiosk_bien_id AND IdSanPham = @sp_nuoc_id_2;
    -- ELSE
    --     INSERT INTO TonKhoKiosk (IdKiosk, IdSanPham, SoLuong, NguongCanhBao)
    --     VALUES (@kiosk_bien_id, @sp_nuoc_id_2, @ton_kiosk, 20);

    -- 5.13 SuCo & ThatLac
    INSERT INTO SuCo (IdKhachHang, IdNhanVienXuLy, MoTa, MucDo, LoaiSuCo, ThoiGian)
    SELECT TOP 50 Id, @nv_cuuho_id, N'Khách bị trượt chân nhẹ', 'Nhe', 'Thuong', DATEADD(day, -CAST(RAND()*1095 AS INT), GETDATE())
    FROM KhachHang
    WHERE Id % 7 = 0;

    INSERT INTO ThatLac (MoTaDoVat, NoiTimThay, TrangThai, IdKhachHangNhan)
    VALUES (N'Ví da màu nâu', N'Khu ẩm thực', 'ChoNhan', NULL),
           (N'Điện thoại iPhone', N'Bãi biển', 'DaTra', (SELECT TOP 1 Id FROM KhachHang));

    -- 5.14 ThietBiApp
  

    -- 5.15 GiaoDichVi & PhieuThu
    INSERT INTO GiaoDichVi (MaCode, IdVi, LoaiGiaoDich, SoTien, IdDonHangLienQuan, ThoiGian, CreatedBy)
    SELECT CONCAT('GD', RIGHT('0000000' + CAST(dh.Id AS VARCHAR), 8)),
           v.Id, 'ThanhToanDichVu', dh.TongTien, dh.Id, dh.ThoiGian, 1
    FROM DonHang dh
    JOIN KhachHang kh ON dh.IdKhachHang = kh.Id
    JOIN ViDienTu v ON v.IdKhachHang = kh.Id
    WHERE dh.TrangThai = 'DaThanhToan'
      AND NOT EXISTS (SELECT 1 FROM GiaoDichVi WHERE IdDonHangLienQuan = dh.Id OR MaCode = CONCAT('GD', RIGHT('0000000' + CAST(dh.Id AS VARCHAR), 8)));

    INSERT INTO PhieuThu (MaCode, IdDonHang, SoTien, PhuongThuc, ThoiGian, CreatedBy)
    SELECT CONCAT('PT', RIGHT('0000000' + CAST(dh.Id AS VARCHAR), 8)),
           dh.Id, dh.TongTien, 'TienMat', dh.ThoiGian, 1
    FROM DonHang dh
    WHERE dh.TrangThai = 'DaThanhToan'
      AND NOT EXISTS (SELECT 1 FROM PhieuThu pt WHERE pt.IdDonHang = dh.Id OR MaCode = CONCAT('PT', RIGHT('0000000' + CAST(dh.Id AS VARCHAR), 8)));

    -- Cập nhật SoDuKhaDung của Ví Điện Tử bị trừ tiền đợt 1
    UPDATE ViDienTu
    SET SoDuKhaDung = CASE 
                        WHEN SoDuKhaDung - gd.TotalTien < 0 THEN 0 
                        ELSE SoDuKhaDung - gd.TotalTien 
                      END
    FROM ViDienTu v
    INNER JOIN (
        SELECT IdVi, SUM(SoTien) as TotalTien 
        FROM GiaoDichVi 
        WHERE LoaiGiaoDich = 'ThanhToanDichVu'
        GROUP BY IdVi
    ) gd ON v.Id = gd.IdVi;
    -- 5.16 BỔ SUNG DỮ LIỆU "ĐỦ BẢNG" THEO NGHIỆP VỤ THỰC TẾ ĐẠI NAM
    -- 5.16.1 ViTriNgoi (khán đài trường đua)
    DECLARE @khandai_a INT = (SELECT TOP 1 Id FROM KhanDai WHERE TenKhanDai = N'Khán đài A');
    IF @khandai_a IS NULL SET @khandai_a = (SELECT TOP 1 Id FROM KhanDai ORDER BY Id);
    IF NOT EXISTS (SELECT 1 FROM ViTriNgoi WHERE IdKhanDai = @khandai_a AND Hang = N'A' AND SoGhe = 1)
    BEGIN
        INSERT INTO ViTriNgoi (MaCode, Hang, SoGhe, LoaiGhe, IdSanPham, IdKhanDai)
        VALUES
            ('NGOI-A01', N'A', 1, 'Vip', (SELECT TOP 1 Id FROM SanPham WHERE MaCode = 'SP008'), @khandai_a),
            ('NGOI-A02', N'A', 2, 'Vip', (SELECT TOP 1 Id FROM SanPham WHERE MaCode = 'SP008'), @khandai_a),
            ('NGOI-B10', N'B', 10, 'Thuong', (SELECT TOP 1 Id FROM SanPham WHERE MaCode = 'SP009'), @khandai_a);
    END

    -- 5.16.2 VeDienTu (chỉ phát hành cho dòng vé từ ChiTietDonHang)
    INSERT INTO VeDienTu (MaCode, IdChiTietDonHang, TrangThai, IdKhachHangSuDung, IdSanPham)
    SELECT TOP 30
        CONCAT('VEDT', RIGHT('0000000' + CAST(ct.Id AS VARCHAR), 7)),
        ct.Id,
        'ChuaSuDung',
        dh.IdKhachHang,
        ct.IdSanPham
    FROM ChiTietDonHang ct
    JOIN DonHang dh ON dh.Id = ct.IdDonHang
    JOIN SanPham sp ON sp.Id = ct.IdSanPham
    WHERE sp.LoaiSanPham = 'Ve'
      AND NOT EXISTS (SELECT 1 FROM VeDienTu v WHERE v.IdChiTietDonHang = ct.Id)
    ORDER BY ct.Id DESC;

    -- 5.16.3 ThueChoi (thuê chòi nghỉ mát gắn với giao dịch thuê đồ)
    IF NOT EXISTS (SELECT 1 FROM ThueChoi)
    BEGIN
        INSERT INTO ThueChoi (IdChiTietThue, IdChoi)
        SELECT TOP 20 td.Id, c.Id
        FROM ThueDoChiTiet td
        CROSS APPLY (SELECT TOP 1 Id FROM ChoiNghiMat ORDER BY NEWID()) c
        WHERE NOT EXISTS (
            SELECT 1 FROM ThueChoi tc WHERE tc.IdChiTietThue = td.Id
        );
    END

    -- 5.16.4 LichThiDau (đua ngựa/chó/go-kart theo khung giờ thực tế)
    DECLARE @giai_2025 INT = (SELECT TOP 1 Id FROM GiaiDua WHERE TenGiai = N'Giải vô địch Đại Nam 2025');
    DECLARE @duong_ngua INT = (SELECT TOP 1 Id FROM DuongDua WHERE TenDuong = N'Đường đua ngựa');
    DECLARE @duong_cho INT = (SELECT TOP 1 Id FROM DuongDua WHERE TenDuong = N'Đường đua chó');
    DECLARE @duong_kart INT = (SELECT TOP 1 Id FROM DuongDua WHERE TenDuong = N'Đường đua go-kart');
    DECLARE @loai_ngua INT = (SELECT TOP 1 Id FROM LoaiHinhDua WHERE TenLoai = N'Đua ngựa');
    DECLARE @loai_cho INT = (SELECT TOP 1 Id FROM LoaiHinhDua WHERE TenLoai = N'Đua chó');
    DECLARE @loai_kart INT = (SELECT TOP 1 Id FROM LoaiHinhDua WHERE TenLoai = N'Đua go-kart');

    IF NOT EXISTS (SELECT 1 FROM LichThiDau)
    BEGIN
        INSERT INTO LichThiDau (IdGiaiDua, IdDuongDua, IdLoaiHinh, ThoiGianDuKien)
        VALUES
            (@giai_2025, @duong_ngua, @loai_ngua, '2025-04-10T09:00:00'),
            (@giai_2025, @duong_cho, @loai_cho, '2025-04-10T15:00:00'),
            (@giai_2025, @duong_kart, @loai_kart, '2025-04-11T10:00:00');
    END

    -- 5.16.5 PhuongTienDua
    DECLARE @vdv_taydua INT = (SELECT TOP 1 Id FROM VanDongVien WHERE LoaiVdv = 'TayDua' ORDER BY Id);
    IF @vdv_taydua IS NOT NULL AND NOT EXISTS (SELECT 1 FROM PhuongTienDua WHERE TenXe = N'Go-kart Sodi RT8')
    BEGIN
        INSERT INTO PhuongTienDua (TenXe, IdVdv, TinhTrang)
        VALUES
            (N'Go-kart Sodi RT8', @vdv_taydua, 'Tot'),
            (N'Go-kart CRG KT2', @vdv_taydua, 'Tot');
    END

    -- 5.16.6 PhieuChi + BaoTriPhuongTienDua (bảo trì định kỳ)
    DECLARE @phieuchi_bt INT;
    IF NOT EXISTS (SELECT 1 FROM PhieuChi WHERE MaCode = 'PC-BAOTRI-0001')
    BEGIN
        INSERT INTO PhieuChi (MaCode, SoTien, LyDo, ThoiGian, CreatedBy)
        VALUES ('PC-BAOTRI-0001', 3500000, N'Bảo trì định kỳ xe đua go-kart khu trường đua', '2025-03-20T10:00:00', 1);
        SET @phieuchi_bt = SCOPE_IDENTITY();
    END
    ELSE
        SET @phieuchi_bt = (SELECT TOP 1 Id FROM PhieuChi WHERE MaCode = 'PC-BAOTRI-0001');

    DECLARE @xe_baotri INT = (SELECT TOP 1 Id FROM PhuongTienDua ORDER BY Id);
    IF @xe_baotri IS NOT NULL AND NOT EXISTS (SELECT 1 FROM BaoTriPhuongTienDua WHERE IdPhuongTienDua = @xe_baotri AND CAST(NgayBaoTri AS DATE) = '2025-03-20')
    BEGIN
        INSERT INTO BaoTriPhuongTienDua (IdPhuongTienDua, NgayBaoTri, NoiDung, ChiPhi, IdPhieuChi)
        VALUES (@xe_baotri, '2025-03-20T09:00:00', N'Thay dầu máy, kiểm tra phanh và hệ thống lái', 3500000, @phieuchi_bt);
    END

    -- 5.16.7 KetQuaDua (kết quả minh họa hợp lý)
    DECLARE @lich_first INT = (SELECT TOP 1 Id FROM LichThiDau ORDER BY ThoiGianDuKien);
    DECLARE @vdv_1 INT = (SELECT TOP 1 Id FROM VanDongVien ORDER BY Id);
    DECLARE @vdv_2 INT = (SELECT TOP 1 Id FROM VanDongVien WHERE Id > ISNULL(@vdv_1, 0) ORDER BY Id);
    DECLARE @ngua_1 INT = (SELECT TOP 1 Id FROM NguaDua ORDER BY Id);
    DECLARE @xe_1 INT = (SELECT TOP 1 Id FROM PhuongTienDua ORDER BY Id);

    IF @lich_first IS NOT NULL AND @vdv_1 IS NOT NULL AND NOT EXISTS (SELECT 1 FROM KetQuaDua WHERE IdLichThiDau = @lich_first)
    BEGIN
        INSERT INTO KetQuaDua (IdLichThiDau, IdVdv, IdPhuongTienDua, IdNguaDua, ThuTuVeDich, ThanhTichThoiGian)
        VALUES
            (@lich_first, @vdv_1, @xe_1, NULL, 1, '00:02:31'),
            (@lich_first, ISNULL(@vdv_2, @vdv_1), @xe_1, NULL, 2, '00:02:37');
    END

    -- 5.16.8 ChuongTrai (gán động vật vào khu chuồng)
    IF NOT EXISTS (SELECT 1 FROM ChuongTrai)
    BEGIN
        ;WITH dv AS (
            SELECT TOP 4 Id, ROW_NUMBER() OVER (ORDER BY Id) AS rn
            FROM DongVat
        ),
        kvt AS (
            SELECT TOP 4 IdKhuVuc, ROW_NUMBER() OVER (ORDER BY IdKhuVuc) AS rn
            FROM KhuVucThu
        )
        INSERT INTO ChuongTrai (IdKhuVucThu, IdDongVat, TenChuong, SucChua, TrangThai)
        SELECT
            kvt.IdKhuVuc,
            dv.Id,
            CONCAT(N'Chuồng ', CAST(dv.rn AS NVARCHAR(10))),
            4 + dv.rn,
            'HoatDong'
        FROM dv
        JOIN kvt ON kvt.rn = dv.rn;
    END

    -- 5.16.9 Lấp tối thiểu bản ghi cho bảng chưa phát sinh (an toàn chạy lại)
    IF NOT EXISTS (SELECT 1 FROM PhieuChi)
    BEGIN
        INSERT INTO PhieuChi (MaCode, SoTien, LyDo, CreatedBy)
        VALUES ('PC-OPS-0001', 1200000, N'Chi phí vận hành phát sinh trong ngày', 1);
    END

    -- 5.17 GROWTH SEED: DỮ LIỆU TRẢI DÀI NHIỀU NĂM, CÓ THỂ CHẠY NHIỀU LẦN
    -- Mỗi lần chạy sẽ thêm dữ liệu mới (mã ngẫu nhiên), không ghi đè dữ liệu cũ.
    IF @SeedMode = 'growth'
    BEGIN
        DECLARE @growth_start DATE = '2019-01-01';
        DECLARE @growth_end DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);
        DECLARE @growth_month DATE = @growth_start;
        DECLARE @orders_per_month INT = 12; -- tăng số này nếu muốn dày dữ liệu hơn

        DECLARE @pool_sp TABLE (Id INT, DonGia DECIMAL(15,0));
        INSERT INTO @pool_sp (Id, DonGia)
        SELECT Id, DonGia
        FROM SanPham
        WHERE TrangThai = 'DangBan'
          AND LoaiSanPham IN ('Ve', 'AnUong', 'Thue', 'LuuTru', 'DichVu', 'Khac');

        WHILE @growth_month <= @growth_end
        BEGIN
        DECLARE @m INT = 1;
        WHILE @m <= @orders_per_month
        BEGIN
            DECLARE @kh_growth INT = (
                SELECT TOP 1 Id FROM KhachHang
                WHERE LoaiKhach IN ('CaNhan', 'Doan', 'DoanhNghiep', 'HocSinhSinhVien', 'VIP')
                ORDER BY NEWID()
            );

            DECLARE @ngay_order DATETIME = DATEADD(
                minute,
                ABS(CHECKSUM(NEWID())) % 1440,
                DATEADD(
                    day,
                    ABS(CHECKSUM(NEWID())) % (DAY(EOMONTH(@growth_month))),
                    CAST(@growth_month AS DATETIME)
                )
            );

            DECLARE @ma_dh_growth VARCHAR(20) = CONCAT('DH', FORMAT(@ngay_order, 'yyMM'), RIGHT(REPLACE(NEWID(), '-', ''), 8));

            INSERT INTO DonHang (MaCode, IdKhachHang, ThoiGian, TongTien, TrangThai, CreatedBy)
            VALUES (@ma_dh_growth, @kh_growth, @ngay_order, 0, 'DaThanhToan', 1);

            DECLARE @dh_growth_id INT = SCOPE_IDENTITY();
            DECLARE @line_growth INT = 1;
            DECLARE @line_target INT = 1 + ABS(CHECKSUM(NEWID())) % 4;
            DECLARE @tong_growth DECIMAL(15,0) = 0;

            WHILE @line_growth <= @line_target
            BEGIN
                DECLARE @sp_growth INT;
                DECLARE @gia_growth DECIMAL(15,0);
                DECLARE @qty_growth INT = 1 + ABS(CHECKSUM(NEWID())) % 3;

                SELECT TOP 1 @sp_growth = Id, @gia_growth = DonGia
                FROM @pool_sp
                ORDER BY NEWID();

                INSERT INTO ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaGoc, DonGiaThucTe)
                VALUES (@dh_growth_id, @sp_growth, @qty_growth, @gia_growth, @gia_growth);

                -- [VÁ LỖ HỔNG]: GHI SỔ CÁI KHO (LEDGER) CHO GIAO DỊCH XUẤT POS
                INSERT INTO TheKho (IdKho, IdSanPham, LoaiGiaoDich, SoLuongThayDoi, TonCuoi, IdThamChieu, ThoiGianGiaoDich, CreatedBy, GhiChu)
                VALUES ((SELECT TOP 1 Id FROM KhoHang WHERE TenKho = N'Kho Kiosk biển'), @sp_growth, 'XUAT_POS', -@qty_growth, NULL, @dh_growth_id, @ngay_order, 1, N'Bán hàng POS (Growth Seed)');

                SET @tong_growth = @tong_growth + (@gia_growth * @qty_growth);
                SET @line_growth = @line_growth + 1;
            END

            UPDATE DonHang SET TongTien = @tong_growth WHERE Id = @dh_growth_id;

            -- Tạo chứng từ thu cho mọi đơn để dữ liệu tài chính khớp.
            INSERT INTO PhieuThu (MaCode, IdDonHang, SoTien, PhuongThuc, ThoiGian, CreatedBy)
            VALUES (
                CONCAT('PT', RIGHT('0000000' + CAST(@dh_growth_id AS VARCHAR(10)), 8)),
                @dh_growth_id,
                @tong_growth,
                CASE (ABS(CHECKSUM(NEWID())) % 4)
                    WHEN 0 THEN 'TienMat'
                    WHEN 1 THEN 'ChuyenKhoan'
                    WHEN 2 THEN 'QR'
                    ELSE 'ViRFID'
                END,
                @ngay_order,
                1
            );

            -- Nếu khách có ví thì ghi thêm giao dịch ví để phục vụ báo cáo lịch sử ví.
            DECLARE @vi_growth INT = (SELECT TOP 1 Id FROM ViDienTu WHERE IdKhachHang = @kh_growth);
            IF @vi_growth IS NOT NULL
            BEGIN
                INSERT INTO GiaoDichVi (MaCode, IdVi, LoaiGiaoDich, SoTien, IdDonHangLienQuan, ThoiGian, CreatedBy)
                VALUES (
                    CONCAT('GD', RIGHT(REPLACE(NEWID(), '-', ''), 10)),
                    @vi_growth,
                    'ThanhToanDichVu',
                    @tong_growth,
                    @dh_growth_id,
                    @ngay_order,
                    1
                );

                -- Trừ tiền ví tương ứng
                UPDATE ViDienTu
                SET SoDuKhaDung = CASE 
                                    WHEN SoDuKhaDung - @tong_growth < 0 THEN 0 
                                    ELSE SoDuKhaDung - @tong_growth 
                                  END
                WHERE Id = @vi_growth;
            END

            SET @m = @m + 1;
        END

            SET @growth_month = DATEADD(month, 1, @growth_month);
        END
    END

    -- 5.18 ENSURE MIN 4 RECORDS CHO CÁC BẢNG DỄ THIẾU DỮ LIỆU
    -- Mục tiêu: dữ liệu dày, nhưng vẫn giữ logic nghiệp vụ.
    WHILE (SELECT COUNT(1) FROM DoanKhach) < 4
    BEGIN
        INSERT INTO DoanKhach (TenDoan, MaSoThue, NguoiDaiDien, DienThoaiLienHe, ChietKhau)
        VALUES (
            CONCAT(N'Đoàn khách doanh nghiệp ', RIGHT(REPLACE(NEWID(), '-', ''), 4)),
            CONCAT('03', RIGHT(CAST(ABS(CHECKSUM(NEWID())) AS VARCHAR(20)), 8)),
            N'Phụ trách đoàn',
            CONCAT('09', RIGHT('00000000' + CAST(ABS(CHECKSUM(NEWID())) % 100000000 AS VARCHAR(8)), 8)),
            8
        );
    END

    WHILE (SELECT COUNT(1) FROM SuKien) < 4
    BEGIN
        DECLARE @sk_code VARCHAR(20) = CONCAT('SK', RIGHT(REPLACE(NEWID(), '-', ''), 4));
        DECLARE @sk_start DATE = DATEADD(day, ABS(CHECKSUM(NEWID())) % 120, GETDATE());
        INSERT INTO SuKien (MaCode, TenSuKien, NgayBatDau, NgayKetThuc, MoTa, TrangThai)
        VALUES (@sk_code, CONCAT(N'Sự kiện theo mùa ', @sk_code), @sk_start, DATEADD(day, 10, @sk_start), N'Chương trình thu hút khách theo mùa', N'Sắp diễn ra');
    END

    WHILE (SELECT COUNT(1) FROM KhuyenMai) < 4
    BEGIN
        DECLARE @km_start DATE = DATEADD(day, ABS(CHECKSUM(NEWID())) % 45, GETDATE());
        INSERT INTO KhuyenMai (MaCode, TenKhuyenMai, LoaiGiamGia, GiaTriGiam, DonToiThieu, NgayBatDau, NgayKetThuc, TrangThai)
        VALUES (
            CONCAT('KM', RIGHT(REPLACE(NEWID(), '-', ''), 6)),
            N'Ưu đãi cuối tuần',
            'PhanTram',
            10,
            300000,
            @km_start,
            DATEADD(day, 14, @km_start),
            1
        );
    END

    WHILE (SELECT COUNT(1) FROM Kiosk) < 4
    BEGIN
        INSERT INTO Kiosk (ViTri, TrangThai)
        VALUES (CONCAT(N'Kiosk bổ sung ', RIGHT(REPLACE(NEWID(), '-', ''), 3)), 'Online');
    END

    WHILE (SELECT COUNT(1) FROM BaiDoXe) < 4
    BEGIN
        INSERT INTO BaiDoXe (TenBai, TongCho)
        VALUES (CONCAT(N'Bãi xe mở rộng ', RIGHT(REPLACE(NEWID(), '-', ''), 3)), 150);
    END

    WHILE (SELECT COUNT(1) FROM NhaHang) < 4
    BEGIN
        INSERT INTO NhaHang (TenNhaHang, IdKhuVuc, SucChua, MoTa)
        VALUES (
            CONCAT(N'Nhà hàng khu ', RIGHT(REPLACE(NEWID(), '-', ''), 3)),
            (SELECT TOP 1 Id FROM KhuVuc WHERE MaCode = 'KV05'),
            120,
            N'Nhà hàng phục vụ theo khung giờ cao điểm'
        );
    END

    WHILE (SELECT COUNT(1) FROM KhanDai) < 4
    BEGIN
        INSERT INTO KhanDai (TenKhanDai, SucChua)
        VALUES (CONCAT(N'Khán đài ', CHAR(65 + (SELECT COUNT(1) FROM KhanDai))), 2500);
    END

    WHILE (SELECT COUNT(1) FROM VanDongVien) < 4
    BEGIN
        INSERT INTO VanDongVien (HoTen, LoaiVdv)
        VALUES (CONCAT(N'VĐV ', RIGHT(REPLACE(NEWID(), '-', ''), 4)), 'TayDua');
    END

    WHILE (SELECT COUNT(1) FROM NguaDua) < 4
    BEGIN
        INSERT INTO NguaDua (TenNgua, IdVdv, Tuoi)
        VALUES (
            CONCAT(N'Ngựa ', RIGHT(REPLACE(NEWID(), '-', ''), 4)),
            (SELECT TOP 1 Id FROM VanDongVien WHERE LoaiVdv = 'NaiNgua' ORDER BY NEWID()),
            4 + ABS(CHECKSUM(NEWID())) % 4
        );
    END

    WHILE (SELECT COUNT(1) FROM PhuongTienDua) < 4
    BEGIN
        INSERT INTO PhuongTienDua (TenXe, IdVdv, TinhTrang)
        VALUES (
            CONCAT(N'Go-kart ', RIGHT(REPLACE(NEWID(), '-', ''), 4)),
            (SELECT TOP 1 Id FROM VanDongVien WHERE LoaiVdv = 'TayDua' ORDER BY NEWID()),
            'Tot'
        );
    END

    WHILE (SELECT COUNT(1) FROM LichThiDau) < 4
    BEGIN
        INSERT INTO LichThiDau (IdGiaiDua, IdDuongDua, IdLoaiHinh, ThoiGianDuKien)
        VALUES (
            (SELECT TOP 1 Id FROM GiaiDua ORDER BY NEWID()),
            (SELECT TOP 1 Id FROM DuongDua ORDER BY NEWID()),
            (SELECT TOP 1 Id FROM LoaiHinhDua ORDER BY NEWID()),
            DATEADD(day, ABS(CHECKSUM(NEWID())) % 90, GETDATE())
        );
    END

    WHILE (SELECT COUNT(1) FROM KetQuaDua) < 4
    BEGIN
        DECLARE @is_xe BIT = ABS(CHECKSUM(NEWID())) % 2;
        INSERT INTO KetQuaDua (IdLichThiDau, IdVdv, IdPhuongTienDua, IdNguaDua, ThuTuVeDich, ThanhTichThoiGian)
        VALUES (
            (SELECT TOP 1 Id FROM LichThiDau ORDER BY NEWID()),
            (SELECT TOP 1 Id FROM VanDongVien ORDER BY NEWID()),
            CASE WHEN @is_xe = 1 THEN (SELECT TOP 1 Id FROM PhuongTienDua ORDER BY NEWID()) ELSE NULL END,
            CASE WHEN @is_xe = 0 THEN (SELECT TOP 1 Id FROM NguaDua ORDER BY NEWID()) ELSE NULL END,
            1 + ABS(CHECKSUM(NEWID())) % 6,
            '00:02:45'
        );
    END

    WHILE (SELECT COUNT(1) FROM PhieuChi) < 4
    BEGIN
        INSERT INTO PhieuChi (MaCode, SoTien, LyDo, ThoiGian, CreatedBy)
        VALUES (
            CONCAT('PC', RIGHT(REPLACE(NEWID(), '-', ''), 8)),
            800000 + ABS(CHECKSUM(NEWID())) % 4200000,
            N'Chi phí vận hành/tiếp liệu định kỳ',
            DATEADD(day, -ABS(CHECKSUM(NEWID())) % 120, GETDATE()),
            1
        );
    END

    WHILE (SELECT COUNT(1) FROM BaoTriPhuongTienDua) < 4
    BEGIN
        INSERT INTO BaoTriPhuongTienDua (IdPhuongTienDua, NgayBaoTri, NoiDung, ChiPhi, IdPhieuChi)
        VALUES (
            (SELECT TOP 1 Id FROM PhuongTienDua ORDER BY NEWID()),
            DATEADD(day, -ABS(CHECKSUM(NEWID())) % 180, GETDATE()),
            N'Bảo trì định kỳ phương tiện đua',
            1500000 + ABS(CHECKSUM(NEWID())) % 3000000,
            (SELECT TOP 1 Id FROM PhieuChi ORDER BY NEWID())
        );
    END

    WHILE (SELECT COUNT(1) FROM ThatLac) < 4
    BEGIN
        INSERT INTO ThatLac (MoTaDoVat, NoiTimThay, TrangThai, IdKhachHangNhan)
        VALUES (
            N'Vật dụng cá nhân',
            N'Khu cổng chính',
            'ChoNhan',
            NULL
        );
    END

    WHILE (SELECT COUNT(1) FROM QuyDoiDonVi) < 4
    BEGIN
        INSERT INTO QuyDoiDonVi (IdSanPham, IdDonViNho, IdDonViLon, TyLeQuyDoi, GiaBanRieng)
        SELECT TOP 1
            sp.Id,
            dvn.Id,
            dvl.Id,
            12,
            sp.DonGia * 10
        FROM SanPham sp
        CROSS JOIN (SELECT TOP 1 Id FROM DonViTinh WHERE KyHieu = 'L') dvn
        CROSS JOIN (SELECT TOP 1 Id FROM DonViTinh WHERE KyHieu = 'T') dvl
        WHERE NOT EXISTS (SELECT 1 FROM QuyDoiDonVi q WHERE q.IdSanPham = sp.Id);
        IF @@ROWCOUNT = 0 BREAK;
    END

    WHILE (SELECT COUNT(1) FROM BangGia) < 12
    BEGIN
        DECLARE @sp_id INT = (
            SELECT TOP 1 Id FROM SanPham sp 
            WHERE NOT EXISTS (SELECT 1 FROM BangGia bg WHERE bg.IdSanPham = sp.Id)
        );
        IF @sp_id IS NULL BREAK;

        DECLARE @dongia DECIMAL(15,0) = (SELECT DonGia FROM SanPham WHERE Id = @sp_id);

        INSERT INTO BangGia (IdSanPham, LoaiGiaApDung, GiaBan)
        VALUES 
            (@sp_id, 'MacDinh', @dongia),
            (@sp_id, 'CuoiTuan', CAST(@dongia * 1.2 AS DECIMAL(15,0))),
            (@sp_id, 'NgayLe', CAST(@dongia * 1.5 AS DECIMAL(15,0)));
    END

    WHILE (SELECT COUNT(1) FROM ViTriNgoi) < 4
    BEGIN
        INSERT INTO ViTriNgoi (MaCode, Hang, SoGhe, LoaiGhe, IdSanPham, IdKhanDai)
        VALUES (
            CONCAT('NGOI-', RIGHT(REPLACE(NEWID(), '-', ''), 6)),
            N'C',
            1 + ABS(CHECKSUM(NEWID())) % 80,
            'Thuong',
            (SELECT TOP 1 Id FROM SanPham WHERE LoaiSanPham = 'Ve' ORDER BY NEWID()),
            (SELECT TOP 1 Id FROM KhanDai ORDER BY NEWID())
        );
    END

    PRINT N'Insert dữ liệu mẫu thành công! Hàng nghìn bản ghi đã được thêm vào với thời gian trải dài 2023-2026.';

    -- ======================================================================
    -- 5.19 BỔ SUNG SẢN PHẨM THUÊ ĐỒ CHI TIẾT (theo thực tế Đại Nam)
    -- ======================================================================
    DECLARE @don_vi_chiec INT = (SELECT Id FROM DonViTinh WHERE KyHieu = 'L');
    DECLARE @khu_bien_ref INT = (SELECT Id FROM KhuVuc WHERE MaCode = 'KV01');
    INSERT INTO SanPham (MaCode, Ten, LoaiSanPham, IdDonViCoBan, DonGia, IdKhuVuc, MoTa, TrangThai)
    SELECT q.MaCode, q.Ten, q.LoaiSanPham, q.IdDonViCoBan, q.DonGia, q.IdKhuVuc, q.MoTa, q.TrangThai
    FROM (
        SELECT 'THUE-PHAO-02' as MaCode, N'Phao hình thú (trẻ em)' as Ten, 'Thue' as LoaiSanPham, @don_vi_chiec as IdDonViCoBan, 70000 as DonGia, @khu_bien_ref as IdKhuVuc, N'Phao hình vịt, cá heo cho trẻ em' as MoTa, 'DangBan' as TrangThai UNION ALL
        SELECT 'THUE-KHAN-01',  N'Khăn tắm',            'Thue', @don_vi_chiec, 20000, @khu_bien_ref, N'Khăn bông lớn',              'DangBan' UNION ALL
        SELECT 'THUE-AOPHAO',   N'Áo phao cứu sinh',    'Thue', @don_vi_chiec, 30000, @khu_bien_ref, N'Áo phao trẻ em + người lớn', 'DangBan' UNION ALL
        SELECT 'THUE-XEDIEN-02',N'Xe điện 4 chỗ',       'Thue', @don_vi_chiec, 250000, @khu_bien_ref, N'Xe điện gia đình 4 chỗ (1 giờ)', 'DangBan' UNION ALL
        SELECT 'THUE-XEDAP-01', N'Xe đạp đôi',          'Thue', @don_vi_chiec, 80000, @khu_bien_ref, N'Xe đạp đôi dạo mát (1 giờ)',    'DangBan' UNION ALL
        SELECT 'THUE-XEDAP-02', N'Xe đạp nước',         'Thue', @don_vi_chiec, 100000, @khu_bien_ref, N'Xe đạp nước trên hồ (1 giờ)',   'DangBan'
    ) AS q
    WHERE NOT EXISTS (SELECT 1 FROM SanPham p WHERE p.MaCode = q.MaCode);

    -- 5.20 BangGia: Flat pricing cho sản phẩm thuê (có tiền cọc)
    DECLARE @sp004 INT = (SELECT Id FROM SanPham WHERE MaCode = 'SP004');
    DECLARE @dg004 DECIMAL(15,0) = (SELECT DonGia FROM SanPham WHERE MaCode = 'SP004');
    IF @sp004 IS NOT NULL AND NOT EXISTS (SELECT 1 FROM BangGia WHERE IdSanPham = @sp004)
    BEGIN
        INSERT INTO BangGia (IdSanPham, LoaiGiaApDung, GiaBan, TienCoc, PhutBlock, PhutTiep, GiaPhuThu) VALUES
        (@sp004, 'MacDinh', @dg004, 50000, 60, 30, CAST(@dg004 * 0.5 AS DECIMAL(15,0))),
        (@sp004, 'CuoiTuan', CAST(@dg004 * 1.2 AS DECIMAL(15,0)), 50000, 60, 30, CAST(@dg004 * 0.5 AS DECIMAL(15,0))),
        (@sp004, 'NgayLe', CAST(@dg004 * 1.5 AS DECIMAL(15,0)), 50000, 60, 30, CAST(@dg004 * 0.5 AS DECIMAL(15,0)));
    END

    DECLARE @spphao INT = (SELECT Id FROM SanPham WHERE MaCode = 'THUE-PHAO-02');
    DECLARE @dgphao DECIMAL(15,0) = (SELECT DonGia FROM SanPham WHERE MaCode = 'THUE-PHAO-02');
    IF @spphao IS NOT NULL AND NOT EXISTS (SELECT 1 FROM BangGia WHERE IdSanPham = @spphao)
    BEGIN
        INSERT INTO BangGia (IdSanPham, LoaiGiaApDung, GiaBan, TienCoc, PhutBlock, PhutTiep, GiaPhuThu) VALUES
        (@spphao, 'MacDinh', @dgphao, 100000, 60, 30, CAST(@dgphao * 0.5 AS DECIMAL(15,0))),
        (@spphao, 'CuoiTuan', CAST(@dgphao * 1.2 AS DECIMAL(15,0)), 100000, 60, 30, CAST(@dgphao * 0.5 AS DECIMAL(15,0))),
        (@spphao, 'NgayLe', CAST(@dgphao * 1.5 AS DECIMAL(15,0)), 100000, 60, 30, CAST(@dgphao * 0.5 AS DECIMAL(15,0)));
    END

    -- ======================================================================
    -- 5.21 SEED DATA ĐOÀN KHÁCH & ĐOÀN KHÁCH DỊCH VỤ (MULTI-SERVICE)
    -- ======================================================================

    IF NOT EXISTS (SELECT 1 FROM DoanKhach WHERE MaSoThue = '0312345678')
    BEGIN
        INSERT INTO DoanKhach (TenDoan, MaBooking, MaSoThue, NguoiDaiDien, DienThoaiLienHe, ChietKhau, SoLuongKhach, NgayDen, NgayDi, TrangThai, IdCombo)
        VALUES (N'Đoàn ABC Travel', 'BK-' + FORMAT(GETDATE(), 'yyMMdd') + '-ABCD', '0312345678', N'Nguyễn Văn A', '0901234567', 10, 50, GETDATE(), DATEADD(day, 2, GETDATE()), N'DaDat', 1);

        DECLARE @IdDoan1 INT = SCOPE_IDENTITY();

        INSERT INTO DoanKhach_DichVu (IdDoan, LoaiDichVu, IdCombo, IdSanPham, SoLuong, DonGia, GhiChu, TrangThai)
        VALUES 
        (@IdDoan1, 'Combo', (SELECT TOP 1 Id FROM Combo WHERE MaCode='COMBO01'), NULL, 50, 500000, N'Gói VIP', 'ChuaXuLy'),
        (@IdDoan1, 'Phong', NULL, (SELECT TOP 1 Id FROM SanPham WHERE LoaiSanPham='Phong' ORDER BY Id), 5, 2000000, N'Phòng Standard', 'ChuaXuLy'),
        (@IdDoan1, 'BanAn', NULL, NULL, 10, 0, N'10 bàn', 'ChuaXuLy');
    END

    IF NOT EXISTS (SELECT 1 FROM DoanKhach WHERE MaSoThue = '0312345679')
    BEGIN
        INSERT INTO DoanKhach (TenDoan, MaBooking, MaSoThue, NguoiDaiDien, DienThoaiLienHe, ChietKhau, SoLuongKhach, NgayDen, NgayDi, TrangThai, IdCombo)
        VALUES (N'Đoàn Trường Hoa Sen', 'BK-' + FORMAT(GETDATE(), 'yyMMdd') + '-WXYZ', '0312345679', N'Lê Thị B', '0901234568', 5, 80, GETDATE(), GETDATE(), N'DaDat', 2);

        DECLARE @IdDoan2 INT = SCOPE_IDENTITY();

        INSERT INTO DoanKhach_DichVu (IdDoan, LoaiDichVu, IdCombo, IdSanPham, SoLuong, DonGia, GhiChu, TrangThai)
        VALUES 
        (@IdDoan2, 'Combo', (SELECT TOP 1 Id FROM Combo WHERE MaCode='COMBO02'), NULL, 80, 250000, N'Combo trẻ em', 'ChuaXuLy'),
        (@IdDoan2, 'BanAn', NULL, NULL, 15, 0, N'15 bàn ăn (ăn buffet)', 'ChuaXuLy');
    END

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    RAISERROR(@ErrorMessage, @ErrorSeverity, 1);
END CATCH;

GO

-- =================== ĐỒNG BỘ MOCK DATA TỔNG CHI TIÊU KHÁCH HÀNG ===================
UPDATE KhachHang
SET TongChiTieu = ISNULL((
    SELECT SUM(TongTien - TienGiamGia)
    FROM DonHang
    WHERE DonHang.IdKhachHang = KhachHang.Id AND TrangThai = 'DaThanhToan'
), 0);

UPDATE KhachHang SET LoaiKhach = 'CaNhan' WHERE TongChiTieu < 20000000;
UPDATE KhachHang SET LoaiKhach = 'VIP' WHERE TongChiTieu >= 20000000 AND TongChiTieu < 50000000;
UPDATE KhachHang SET LoaiKhach = 'VVIP' WHERE TongChiTieu >= 50000000;
GO
