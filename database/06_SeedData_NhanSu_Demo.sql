-- =================================================================
-- DEMO SEED DATA - Module Nhân Sự
-- Chạy SAU KHI đã chạy:
--   1. Database_DaiNam_V2.sql
--   2. SeedData_DemoDashboard.sql
-- =================================================================

BEGIN TRANSACTION;

-- =================================================================
-- PHẦN 1: Thêm 8 nhân viên demo (NV001 và NV002 đã có sẵn)
-- TrangThai hợp lệ: DangLam | NghiPhep | DinhChi | DaNghiViec
-- LoaiHopDong hợp lệ: FullTime | PartTime | TheoMua | Intern
-- =================================================================
DECLARE @id3 INT, @id4 INT, @id5 INT, @id6 INT,
        @id7 INT, @id8 INT, @id9 INT, @id10 INT;

INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac, DaXoa, NgayTao)
VALUES (N'Nguyễn Thị Hoa',   '0901234003', 'hoa.nt@dainam.vn',  N'TP.HCM',     'NhanVien', 0, GETDATE());
SET @id3 = SCOPE_IDENTITY();

INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac, DaXoa, NgayTao)
VALUES (N'Trần Văn Dũng',    '0901234004', 'dung.tv@dainam.vn', N'Bình Dương', 'NhanVien', 0, GETDATE());
SET @id4 = SCOPE_IDENTITY();

INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac, DaXoa, NgayTao)
VALUES (N'Lê Thị Mai',       '0901234005', 'mai.lt@dainam.vn',  N'Đồng Nai',   'NhanVien', 0, GETDATE());
SET @id5 = SCOPE_IDENTITY();

INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac, DaXoa, NgayTao)
VALUES (N'Phạm Văn Hùng',   '0901234006', 'hung.pv@dainam.vn', N'TP.HCM',     'NhanVien', 0, GETDATE());
SET @id6 = SCOPE_IDENTITY();

INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac, DaXoa, NgayTao)
VALUES (N'Đỗ Thị Lan',       '0901234007', 'lan.dt@dainam.vn',  N'Bình Dương', 'NhanVien', 0, GETDATE());
SET @id7 = SCOPE_IDENTITY();

INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac, DaXoa, NgayTao)
VALUES (N'Nguyễn Văn Tùng', '0901234008', 'tung.nv@dainam.vn', N'TP.HCM',     'NhanVien', 0, GETDATE());
SET @id8 = SCOPE_IDENTITY();

INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac, DaXoa, NgayTao)
VALUES (N'Trịnh Thị Thu',   '0901234009', 'thu.tt@dainam.vn',  N'Long An',    'NhanVien', 0, GETDATE());
SET @id9 = SCOPE_IDENTITY();

INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac, DaXoa, NgayTao)
VALUES (N'Lý Văn Nam',       '0901234010', 'nam.lv@dainam.vn',  N'Tây Ninh',   'NhanVien', 0, GETDATE());
SET @id10 = SCOPE_IDENTITY();

-- TrangThai = 'DangLam' (đúng enum CHK_NV_TrangThai)
-- LoaiHopDong = 'FullTime'/'PartTime' (đúng enum CHK_NV_LoaiHopDong)
-- IdVaiTro: 1=Admin, 2=QuanLy, 3=ThuNgan, 4=NhanVien, 5=LeTan, 6=ThuKho, 7=BepTruong, 8=CuuHo
INSERT INTO DoiTac.NhanVien (IdDoiTac, MaNhanVien, IdVaiTro, ChucVu, GioiTinh, NgaySinh, LoaiKhoi, LoaiHopDong, NhomCongViec, LuongCoBan, LuongTheoGio, TrangThai)
VALUES
(@id3,  'NV003', 5, N'Lễ tân',      'Nu',  '1998-03-15', 'DichVu',  'FullTime', 'ThuongNgay', 7500000,  0,     'DangLam'),
(@id4,  'NV004', 6, N'Thủ kho',     'Nam', '1995-07-20', 'VanHanh', 'FullTime', 'ThuongNgay', 7000000,  0,     'DangLam'),
(@id5,  'NV005', 3, N'Thu ngân',    'Nu',  '2000-11-05', 'BanHang', 'FullTime', 'ThuongNgay', 6500000,  0,     'DangLam'),
(@id6,  'NV006', 8, N'Cứu hộ',      'Nam', '1993-04-18', 'VanHanh', 'FullTime', 'ThuongNgay', 6800000,  0,     'DangLam'),
(@id7,  'NV007', 4, N'Nhân viên',   'Nu',  '2001-09-22', 'DichVu',  'PartTime', 'ThuongNgay', 4500000,  35000, 'DangLam'),
(@id8,  'NV008', 7, N'Bếp trưởng', 'Nam', '1987-01-30', 'VanHanh', 'FullTime', 'ThuongNgay', 12000000, 0,     'DangLam'),
(@id9,  'NV009', 5, N'Lễ tân',      'Nu',  '1999-06-12', 'DichVu',  'FullTime', 'ThuongNgay', 7500000,  0,     'DangLam'),
(@id10, 'NV010', 4, N'Bảo vệ',      'Nam', '1990-12-08', 'VanHanh', 'FullTime', 'ThuongNgay', 6000000,  0,     'DangLam');

-- Tiêu thụ sequence để không trùng mã khi thêm mới tiếp
DECLARE @s1 INT = NEXT VALUE FOR HeThong.Seq_NhanVien;
DECLARE @s2 INT = NEXT VALUE FOR HeThong.Seq_NhanVien;
DECLARE @s3 INT = NEXT VALUE FOR HeThong.Seq_NhanVien;
DECLARE @s4 INT = NEXT VALUE FOR HeThong.Seq_NhanVien;
DECLARE @s5 INT = NEXT VALUE FOR HeThong.Seq_NhanVien;
DECLARE @s6 INT = NEXT VALUE FOR HeThong.Seq_NhanVien;
DECLARE @s7 INT = NEXT VALUE FOR HeThong.Seq_NhanVien;
DECLARE @s8 INT = NEXT VALUE FOR HeThong.Seq_NhanVien;
GO

-- =================================================================
-- PHẦN 2: Hợp đồng (NhanSu.HopDong từ DemoDashboard)
-- LoaiHopDong hợp lệ: FullTime | PartTime | TheoMua | Intern
-- TrangThai hợp lệ: DangHieuLuc | HetHan | ChamDut | ChuaKyLai
-- =================================================================
INSERT INTO NhanSu.HopDong (IdNhanVien, SoHopDong, LoaiHopDong, ChucDanh, LuongCoBan, NgayHieuLuc, NgayHetHan, TrangThai)
SELECT
    nv.IdDoiTac,
    'HD-' + nv.MaNhanVien + '-2024',
    'FullTime',
    nv.ChucVu,
    nv.LuongCoBan,
    '2024-01-01',
    '2025-12-31',
    'DangHieuLuc'
FROM DoiTac.NhanVien nv
WHERE nv.MaNhanVien IN ('NV001','NV002','NV003','NV004','NV005','NV006','NV008','NV009','NV010');

-- NV007 - Hợp đồng bán thời gian thử việc
INSERT INTO NhanSu.HopDong (IdNhanVien, SoHopDong, LoaiHopDong, ChucDanh, LuongCoBan, NgayHieuLuc, NgayHetHan, TrangThai)
SELECT IdDoiTac, 'HD-NV007-2024', 'PartTime', N'Nhân viên phục vụ', 4500000, '2024-10-01', '2025-03-31', 'ChuaKyLai'
FROM DoiTac.NhanVien WHERE MaNhanVien = 'NV007';

-- NV003 - Hợp đồng cũ đã hết hạn (lịch sử)
INSERT INTO NhanSu.HopDong (IdNhanVien, SoHopDong, LoaiHopDong, ChucDanh, LuongCoBan, NgayHieuLuc, NgayHetHan, TrangThai)
SELECT IdDoiTac, 'HD-NV003-2022', 'FullTime', N'Lễ tân', 6500000, '2022-03-01', '2023-12-31', 'HetHan'
FROM DoiTac.NhanVien WHERE MaNhanVien = 'NV003';
GO

-- =================================================================
-- PHẦN 3: Lịch làm việc (30 ngày gần nhất)
-- CaLamMau: 1=Sáng, 2=Chiều, 3=Đêm, 4=Hành chính
-- =================================================================
DECLARE @today DATE = CAST(GETDATE() AS DATE);

-- NV001 (Admin) - Ca hành chính, bỏ T7+CN
INSERT INTO NhanSu.LichLamViec (IdNhanVien, NgayLam, IdCaLamMau, GioBatDau, GioKetThuc, TrangThai)
SELECT 1,
    DATEADD(DAY, n, DATEADD(DAY, -30, @today)),
    4, '08:00', '17:00',
    CASE
        WHEN DATEADD(DAY, n, DATEADD(DAY, -30, @today)) < @today THEN 'DaHoanThanh'
        WHEN DATEADD(DAY, n, DATEADD(DAY, -30, @today)) = @today THEN 'DangLam'
        ELSE 'KeHoach'
    END
FROM (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12),(13),(14),
            (15),(16),(17),(18),(19),(20),(21),(22),(23),(24),(25),(26),(27),(28),(29)) t(n)
WHERE DATEPART(WEEKDAY, DATEADD(DAY, n, DATEADD(DAY, -30, @today))) NOT IN (1, 7);

-- NV002 (Thu ngân) - Xen kẽ ca sáng/chiều
INSERT INTO NhanSu.LichLamViec (IdNhanVien, NgayLam, IdCaLamMau, GioBatDau, GioKetThuc, TrangThai)
SELECT 2,
    DATEADD(DAY, n, DATEADD(DAY, -15, @today)),
    CASE WHEN n % 2 = 0 THEN 1 ELSE 2 END,
    CASE WHEN n % 2 = 0 THEN '06:00' ELSE '14:00' END,
    CASE WHEN n % 2 = 0 THEN '14:00' ELSE '22:00' END,
    CASE WHEN DATEADD(DAY, n, DATEADD(DAY, -15, @today)) < @today THEN 'DaHoanThanh' ELSE 'KeHoach' END
FROM (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12),(13),(14)) t(n);

-- NV005 (Thu ngân) + NV006 (Cứu hộ)
INSERT INTO NhanSu.LichLamViec (IdNhanVien, NgayLam, IdCaLamMau, GioBatDau, GioKetThuc, TrangThai)
SELECT nv.IdDoiTac,
    DATEADD(DAY, t.n, DATEADD(DAY, -14, @today)),
    CASE WHEN nv.MaNhanVien = 'NV005' THEN 2 ELSE 1 END,
    CASE WHEN nv.MaNhanVien = 'NV005' THEN '14:00' ELSE '06:00' END,
    CASE WHEN nv.MaNhanVien = 'NV005' THEN '22:00' ELSE '14:00' END,
    CASE WHEN DATEADD(DAY, t.n, DATEADD(DAY, -14, @today)) < @today THEN 'DaHoanThanh' ELSE 'KeHoach' END
FROM DoiTac.NhanVien nv,
     (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12),(13)) t(n)
WHERE nv.MaNhanVien IN ('NV005','NV006');
GO

-- =================================================================
-- PHẦN 4: Đánh giá (NhanSu.DanhGiaNhanVien - schema Dashboard)
-- Cột: KyDanhGia (varchar), LoaiDanhGia, DiemChuyen_Mon, DiemThai_Do,
--      DiemKet_Qua, XepLoai, NhanXetNguoiDG, IdNguoiDanhGia, TrangThai
-- =================================================================
INSERT INTO NhanSu.DanhGiaNhanVien
    (IdNhanVien, KyDanhGia, LoaiDanhGia, DiemChuyen_Mon, DiemThai_Do, DiemKet_Qua, XepLoai, NhanXetNguoiDG, IdNguoiDanhGia, TrangThai, NgayDanhGia)
VALUES
-- NV001 - Admin (3 kỳ gần nhất)
(1, FORMAT(DATEADD(MONTH,-3,GETDATE()),'yyyy-MM'), 'DinhKy', 9.2, 9.0, 9.4, 'XuatSac', N'Hoàn thành tốt, hệ thống ổn định.', 1, 'DaDuyet', CAST(DATEADD(MONTH,-3,GETDATE()) AS DATE)),
(1, FORMAT(DATEADD(MONTH,-2,GETDATE()),'yyyy-MM'), 'DinhKy', 9.5, 9.3, 9.6, 'XuatSac', N'Hoàn thành xuất sắc, không có sự cố.', 1, 'DaDuyet', CAST(DATEADD(MONTH,-2,GETDATE()) AS DATE)),
(1, FORMAT(DATEADD(MONTH,-1,GETDATE()),'yyyy-MM'), 'DinhKy', 9.0, 9.5, 9.2, 'XuatSac', N'Hiệu suất tuyệt vời tháng vừa qua.', 1, 'DaDuyet', CAST(DATEADD(MONTH,-1,GETDATE()) AS DATE)),
-- NV002 - Thu ngân (3 kỳ)
(2, FORMAT(DATEADD(MONTH,-3,GETDATE()),'yyyy-MM'), 'DinhKy', 7.5, 8.0, 7.8, 'Kha', N'Cần cải thiện tốc độ phục vụ.', 1, 'DaDuyet', CAST(DATEADD(MONTH,-3,GETDATE()) AS DATE)),
(2, FORMAT(DATEADD(MONTH,-2,GETDATE()),'yyyy-MM'), 'DinhKy', 8.0, 8.5, 8.2, 'Tot',  N'Cải thiện rõ rệt so với kỳ trước.', 1, 'DaDuyet', CAST(DATEADD(MONTH,-2,GETDATE()) AS DATE)),
(2, FORMAT(DATEADD(MONTH,-1,GETDATE()),'yyyy-MM'), 'DinhKy', 8.5, 8.0, 8.5, 'Tot',  N'Ổn định, phục vụ khách chu đáo.', 1, 'DaDuyet', CAST(DATEADD(MONTH,-1,GETDATE()) AS DATE));

-- NV008 - Bếp trưởng (2 kỳ)
INSERT INTO NhanSu.DanhGiaNhanVien
    (IdNhanVien, KyDanhGia, LoaiDanhGia, DiemChuyen_Mon, DiemThai_Do, DiemKet_Qua, XepLoai, NhanXetNguoiDG, IdNguoiDanhGia, TrangThai, NgayDanhGia)
SELECT
    nv.IdDoiTac,
    FORMAT(DATEADD(MONTH,-2,GETDATE()),'yyyy-MM'), 'DinhKy',
    8.8, 9.0, 8.5, 'Tot',
    N'Chất lượng bếp ổn định, kiểm soát nguyên liệu tốt.',
    1, 'DaDuyet', CAST(DATEADD(MONTH,-2,GETDATE()) AS DATE)
FROM DoiTac.NhanVien nv WHERE nv.MaNhanVien = 'NV008';

INSERT INTO NhanSu.DanhGiaNhanVien
    (IdNhanVien, KyDanhGia, LoaiDanhGia, DiemChuyen_Mon, DiemThai_Do, DiemKet_Qua, XepLoai, NhanXetNguoiDG, IdNguoiDanhGia, TrangThai, NgayDanhGia)
SELECT
    nv.IdDoiTac,
    FORMAT(DATEADD(MONTH,-1,GETDATE()),'yyyy-MM'), 'DinhKy',
    9.2, 9.0, 9.3, 'XuatSac',
    N'Thực đơn mới được khách hài lòng cao, sáng tạo xuất sắc.',
    1, 'DaDuyet', CAST(DATEADD(MONTH,-1,GETDATE()) AS DATE)
FROM DoiTac.NhanVien nv WHERE nv.MaNhanVien = 'NV008';
GO

COMMIT TRANSACTION;
PRINT N'✅ Demo data NhanSu nạp thành công! (10 NV, 12 HĐ, ~60 Lịch, 8 Đánh giá)';
