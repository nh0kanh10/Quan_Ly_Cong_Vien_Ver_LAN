-- ============================================================================
-- FILE: 02_SeedData_MasterData.sql  (REWRITTEN - Fixed all 12 issues)
-- MỤC ĐÍCH: Nạp Dữ Liệu Nền cho toàn hệ thống Đại Nam Resort
-- CHẠY SAU: Database_DaiNam_V2.sql
-- FIX: Không hard-code Id, dùng subquery theo MaXxx; bổ sung đủ dữ liệu còn thiếu
-- ============================================================================
USE [Database_DaiNamv2];
GO
SET NOCOUNT ON;
GO

-- ============================================================================
-- PHẦN 0: CẤU HÌNH HỆ THỐNG (HeThong.CauHinh)
-- Fix #1: Thêm DIEM_QUY_DOI (code C# dùng key này, DB chỉ có DIEM_QUY_DOI_1000D)
-- Fix #2: Thêm PHAT_LO_GIO_PHAN_TRAM_MOI_GIO tường minh
-- ============================================================================
-- Xóa key cũ sai tên nếu có, thêm key đúng
DELETE FROM HeThong.CauHinh WHERE Khoa IN ('DIEM_QUY_DOI', 'PHAT_LO_GIO_PHAN_TRAM_MOI_GIO');

INSERT INTO HeThong.CauHinh (Khoa, GiaTri, MoTa) VALUES
('DIEM_QUY_DOI',                  '1000', N'1000 VNĐ = 1 điểm tích lũy (key dùng bởi BUS_POS, BUS_LuuTru_Booking)'),
('PHAT_LO_GIO_PHAN_TRAM_MOI_GIO', '10',   N'Phạt lố giờ: 10% giá phòng mỗi giờ trễ (BUS_LuuTru_TinhToan)');
GO

-- ============================================================================
-- PHẦN 0B: TỪ ĐIỂN — Bổ sung trạng thái còn thiếu
-- Fix #6: Thiếu 'MotPhan' trong DON_HANG_TRANG_THAI → CHECK constraint fail
-- Fix #7: Thiếu 'DangMo' trong DON_HANG_TRANG_THAI → CHECK constraint fail
-- ============================================================================
-- Thêm an toàn (bỏ qua nếu đã có)
IF NOT EXISTS (SELECT 1 FROM HeThong.TuDien WHERE NhomMa='DON_HANG_TRANG_THAI' AND Ma='MotPhan')
    INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'MotPhan', N'Thanh toán một phần', 8, '#CC8844', NULL, 1);

IF NOT EXISTS (SELECT 1 FROM HeThong.TuDien WHERE NhomMa='DON_HANG_TRANG_THAI' AND Ma='DangMo')
    INSERT INTO HeThong.TuDien VALUES ('DON_HANG_TRANG_THAI', 'DangMo', N'Đang mở (bill lưu trú)', 9, '#5B9BD5', NULL, 1);
GO

-- ============================================================================
-- PHẦN 1: KHU VỰC
-- ============================================================================
DELETE FROM DanhMuc.KhuVucBien;
DELETE FROM DanhMuc.KhuVucThu;
DELETE FROM DanhMuc.KhuVuc;

INSERT INTO DanhMuc.KhuVuc (MaKhuVuc, TenKhuVuc, LoaiKhuVuc, SucChua, MoTa) VALUES
('KV_CONG',     N'Khu Cổng Chính',          'CongVao',   5000, N'Cổng chính, quầy vé, bãi gửi xe'),
('KV_BIEN',     N'Khu Biển Nhân Tạo',        'VuiChoi',   3000, N'Biển nhân tạo tạo sóng, bờ cát trắng'),
('KV_NUOC',     N'Khu Công Viên Nước',        'VuiChoi',   2000, N'Cầu trượt, hồ bơi, sông lười'),
('KV_DEN',      N'Khu Đền Đại Nam',           'ThamQuan',  1000, N'Đền thờ, khu tâm linh'),
('KV_THU',      N'Khu Vườn Thú',              'ThamQuan',  1500, N'Vườn thú mở, chuồng thú hoang dã'),
('KV_KS',       N'Khu Khách Sạn',             'LuuTru',     500, N'Khách sạn 5 sao, hồ bơi riêng'),
('KV_AMTHUC',   N'Khu Ẩm Thực',               'AnUong',    1200, N'Nhà hàng, quầy ăn vặt, bar'),
('KV_TROICHOI', N'Khu Trò Chơi Cơ Giới',      'VuiChoi',   2500, N'Đu quay, tàu lượn, nhà ma'),
('KV_BAIXE',    N'Bãi Đỗ Xe',                 'HaTang',    1000, N'Bãi đỗ xe ô tô và xe máy'),
('KV_TRUONGDUA',N'Trường Đua Đại Nam',         'GiaiTri',   5000, N'Trường đua ngựa, đua chó');
GO

INSERT INTO DanhMuc.KhuVucBien (IdKhuVuc, DoSauToiDa, YeuCauPhao, ChoPhepBoi)
SELECT Id, 1.80, 1, 1 FROM DanhMuc.KhuVuc WHERE MaKhuVuc = 'KV_BIEN';

INSERT INTO DanhMuc.KhuVucThu (IdKhuVuc, DienTichHectare, LoaiDongVatChinh)
SELECT Id, 5.50, N'Hổ, Sư tử, Voi, Hươu cao cổ' FROM DanhMuc.KhuVuc WHERE MaKhuVuc = 'KV_THU';
GO

-- ============================================================================
-- PHẦN 2: NHÀ HÀNG & BÀN ĂN
-- ============================================================================
DELETE FROM DanhMuc.BanAn;
DELETE FROM DanhMuc.NhaHang;

INSERT INTO DanhMuc.NhaHang (TenNhaHang, IdKhuVuc, SoLuongBan, GioMoCua, GioDongCua) VALUES
(N'Nhà Hàng Đại Nam Palace',  (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_AMTHUC'),  50, '10:00', '22:00'),
(N'Quầy Ăn Vặt Biển',         (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_BIEN'),    20, '08:00', '18:00'),
(N'Bar Nước Công Viên Nước',   (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_NUOC'),    15, '08:00', '20:00'),
(N'Food Court Trò Chơi',       (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_TROICHOI'),30, '09:00', '21:00');
GO

-- Bàn ăn cho Nhà Hàng Palace (10 bàn)
DECLARE @IdNhHang INT = (SELECT Id FROM DanhMuc.NhaHang WHERE TenNhaHang = N'Nhà Hàng Đại Nam Palace');
INSERT INTO DanhMuc.BanAn (MaBan, IdNhaHang, SoChoNgoi, ViTri) VALUES
('BAN-P01', @IdNhHang, 4, N'Tầng 1 - Cửa sổ'),
('BAN-P02', @IdNhHang, 4, N'Tầng 1 - Cửa sổ'),
('BAN-P03', @IdNhHang, 6, N'Tầng 1 - Trung tâm'),
('BAN-P04', @IdNhHang, 6, N'Tầng 1 - Trung tâm'),
('BAN-P05', @IdNhHang, 8, N'Tầng 1 - VIP'),
('BAN-P06', @IdNhHang, 4, N'Tầng 2 - Ban công'),
('BAN-P07', @IdNhHang, 4, N'Tầng 2 - Ban công'),
('BAN-P08', @IdNhHang, 10,N'Tầng 2 - Phòng riêng'),
('BAN-P09', @IdNhHang, 10,N'Tầng 2 - Phòng riêng'),
('BAN-P10', @IdNhHang, 20,N'Tầng 2 - Hội trường');
GO

-- ============================================================================
-- PHẦN 3: ĐIỂM BÁN HÀNG POS
-- ============================================================================
DELETE FROM BanHang.Menu_POS;
DELETE FROM BanHang.DiemBanHang_POS;

INSERT INTO BanHang.DiemBanHang_POS (MaDiemBan, TenDiemBan, IdKhuVuc, ChoPhepBanVe, ChoPhepBanFNB, ChoPhepThue) VALUES
('POS_CONG01',  N'Quầy Vé Cổng 1',               (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_CONG'),     1, 0, 0),
('POS_CONG02',  N'Quầy Vé Cổng 2',               (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_CONG'),     1, 0, 0),
('POS_NH01',    N'Thu Ngân Đại Nam Palace',        (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_AMTHUC'),  0, 1, 0),
('POS_BQA01',   N'Quầy Ăn Vặt Biển',              (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_BIEN'),    0, 1, 0),
('POS_BAR01',   N'Bar Nước Công Viên Nước',        (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_NUOC'),    0, 1, 0),
('POS_THUE01',  N'Quầy Cho Thuê Tủ & Phao',       (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_BIEN'),    0, 0, 1),
('POS_KS01',    N'Lễ Tân Khách Sạn',              (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_KS'),      0, 0, 1),
('POS_FC01',    N'Food Court Trò Chơi',            (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_TROICHOI'),0, 1, 0);
GO

-- ============================================================================
-- PHẦN 4: SẢN PHẨM
-- Fix #5: Thêm SYS_PHU_THU (bắt buộc cho BUS_LuuTru_Booking sau khi fix)
-- ============================================================================
DELETE FROM DanhMuc.DinhMucNguyenLieu;
DELETE FROM DanhMuc.MonAn;
DELETE FROM DanhMuc.SanPham_Ve;
DELETE FROM DanhMuc.LoaiPhong;
DELETE FROM DanhMuc.SanPham WHERE MaSanPham NOT IN ('SYS_PHU_THU'); -- giữ lại nếu đã có
DELETE FROM DanhMuc.SanPham;
GO

-- 4A. Vé vào khu
INSERT INTO DanhMuc.SanPham (MaSanPham, TenSanPham, LoaiSanPham, IdDonViTinh, DonGia, LaVatTu, TrangThai) VALUES
('VE-NL-001', N'Vé Vào Cổng Người Lớn',            'VeVaoKhu', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Ve'),   150000, 0, 'DangBan'),
('VE-TE-001', N'Vé Vào Cổng Trẻ Em (1m–1m4)',       'VeVaoKhu', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Ve'),    80000, 0, 'DangBan'),
('VE-NL-CMB', N'Vé Combo Người Lớn (Cổng+Nước)',    'VeVaoKhu', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Ve'),   250000, 0, 'DangBan'),
('VE-TE-CMB', N'Vé Combo Trẻ Em (Cổng+Nước)',       'VeVaoKhu', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Ve'),   150000, 0, 'DangBan'),
('VE-NCT-01', N'Vé Ưu Đãi Người Cao Tuổi',          'VeVaoKhu', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Ve'),    50000, 0, 'DangBan');
GO

-- 4B. Vé trò chơi
INSERT INTO DanhMuc.SanPham (MaSanPham, TenSanPham, LoaiSanPham, IdDonViTinh, DonGia, LaVatTu, TrangThai) VALUES
('TC-DQ-001',  N'Vé Đu Quay Khổng Lồ',         'VeTroChoi', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Luot'),  80000, 0, 'DangBan'),
('TC-TL-001',  N'Vé Tàu Lượn Siêu Tốc',        'VeTroChoi', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Luot'), 100000, 0, 'DangBan'),
('TC-NMA-001', N'Vé Nhà Ma Kinh Dị',            'VeTroChoi', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Luot'),  60000, 0, 'DangBan'),
('TC-OTO-001', N'Vé Xe Điện Đụng (15 phút)',    'VeTroChoi', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Luot'),  50000, 0, 'DangBan'),
('TC-XQV-001', N'Vé Xoay Quanh Vòng',           'VeTroChoi', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Luot'),  70000, 0, 'TamNgung');
GO

-- 4C. Đồ ăn
INSERT INTO DanhMuc.SanPham (MaSanPham, TenSanPham, LoaiSanPham, IdDonViTinh, DonGia, LaVatTu, TrangThai) VALUES
('DA-PHO-01',  N'Phở Bò Tái Nạm',               'AnUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Suat'),  55000, 0, 'DangBan'),
('DA-COM-01',  N'Cơm Tấm Sườn Bì Chả',          'AnUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Suat'),  50000, 0, 'DangBan'),
('DA-BUN-01',  N'Bún Bò Huế',                    'AnUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Suat'),  50000, 0, 'DangBan'),
('DA-MI-01',   N'Mì Xào Hải Sản',                'AnUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Suat'),  65000, 0, 'DangBan'),
('DA-GA-01',   N'Gà Rán Giòn (3 miếng)',         'AnUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Cai'),   75000, 0, 'DangBan'),
('DA-BF-01',   N'Buffet Trưa Đại Nam Palace',    'AnUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Suat'), 299000, 0, 'DangBan'),
('DA-BF-02',   N'Buffet Tối Hải Sản',            'AnUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Suat'), 499000, 0, 'DangBan'),
('DA-KM-01',   N'Khoai Môn Chiên',               'AnUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Cai'),   35000, 0, 'DangBan'),
('DA-XUC-01',  N'Xúc Xích Nướng',                'AnUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Cai'),   25000, 0, 'DangBan');
GO

-- 4D. Đồ uống (LaVatTu=1 → quản lý kho)
INSERT INTO DanhMuc.SanPham (MaSanPham, TenSanPham, LoaiSanPham, IdDonViTinh, DonGia, LaVatTu, CanQuanLyLo, TrangThai) VALUES
('DU-NS-001',  N'Nước Suối Lavie 500ml',         'DoUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Chai'),  15000, 1, 0, 'DangBan'),
('DU-CCC-001', N'Coca Cola Lon 330ml',            'DoUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Lon'),   20000, 1, 1, 'DangBan'),
('DU-PEP-001', N'Pepsi Lon 330ml',                'DoUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Lon'),   20000, 1, 1, 'DangBan'),
('DU-TRA-001', N'Trà Đào Cam Sả',                'DoUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Cai'),   35000, 0, 0, 'DangBan'),
('DU-CF-001',  N'Cà Phê Sữa Đá',                'DoUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Cai'),   30000, 0, 0, 'DangBan'),
('DU-SM-001',  N'Sinh Tố Xoài',                  'DoUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Cai'),   40000, 0, 0, 'DangBan'),
('DU-BIA-001', N'Bia Tiger Lon 330ml',            'DoUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Lon'),   25000, 1, 1, 'DangBan'),
('DU-RBL-001', N'Red Bull Lon 250ml',             'DoUong', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Lon'),   22000, 1, 1, 'DangBan');
GO

-- 4E. Cho thuê tủ đồ & phao
INSERT INTO DanhMuc.SanPham (MaSanPham, TenSanPham, LoaiSanPham, IdDonViTinh, DonGia, LaVatTu, TrangThai) VALUES
('TD-TU-S01',  N'Thuê Tủ Đồ Nhỏ (S)',            'TuDo',      (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Luot'), 30000, 0, 'DangBan'),
('TD-TU-L01',  N'Thuê Tủ Đồ Lớn (L)',            'TuDo',      (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Luot'), 50000, 0, 'DangBan'),
('TD-PHAO-01', N'Thuê Phao Tròn',                 'DoChoThue', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Luot'), 40000, 0, 'DangBan'),
('TD-PHAO-02', N'Thuê Phao Ngồi (Flamingo)',      'DoChoThue', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Luot'), 60000, 0, 'DangBan'),
('TD-AO-01',   N'Thuê Áo Phao',                   'DoChoThue', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Luot'), 20000, 0, 'DangBan'),
('TD-XE-DI',   N'Thuê Xe Điện',                   'PhuongTien',(SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Gio'),  80000, 0, 'DangBan'),
('TD-XE-DAP',  N'Thuê Xe Đạp',                    'PhuongTien',(SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Gio'),  50000, 0, 'DangBan'),
('TD-CHOI-01', N'Thuê Chòi Nghỉ Mát (4 người)',   'ChoiNghiMat',(SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Gio'),200000, 0, 'DangBan');
GO

-- 4F. Lưu trú (LoaiSanPham = 'LuuTru' — dùng làm IdSanPham cho LoaiPhong)
INSERT INTO DanhMuc.SanPham (MaSanPham, TenSanPham, LoaiSanPham, IdDonViTinh, DonGia, LaVatTu, TrangThai) VALUES
('LT-STD-01',  N'Phòng Standard Twin',            'LuuTru', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Dem'), 1200000, 0, 'DangBan'),
('LT-DLX-01',  N'Phòng Deluxe King',              'LuuTru', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Dem'), 1800000, 0, 'DangBan'),
('LT-SUI-01',  N'Phòng Suite View Hồ',            'LuuTru', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Dem'), 3500000, 0, 'DangBan'),
('LT-VIL-01',  N'Villa Gia Đình (4 người)',       'LuuTru', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Dem'), 5000000, 0, 'DangBan');
GO

-- 4G. Gửi xe
INSERT INTO DanhMuc.SanPham (MaSanPham, TenSanPham, LoaiSanPham, IdDonViTinh, DonGia, LaVatTu, TrangThai) VALUES
('GX-XM-001',  N'Gửi Xe Máy',                    'GuiXe', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Luot'),  10000, 0, 'DangBan'),
('GX-OTO4-01', N'Gửi Ô Tô (4–7 chỗ)',            'GuiXe', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Luot'),  30000, 0, 'DangBan'),
('GX-OTO16',   N'Gửi Ô Tô (16 chỗ trở lên)',     'GuiXe', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Luot'),  50000, 0, 'DangBan');
GO

-- 4H. Nguyên liệu kho (LaVatTu=1, TamNgung = không hiện POS)
INSERT INTO DanhMuc.SanPham (MaSanPham, TenSanPham, LoaiSanPham, IdDonViTinh, DonGia, LaVatTu, CanQuanLyLo, TrangThai) VALUES
('NL-GAO-01',  N'Gạo ST25',                       'NguyenLieu', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Kg'),    18000, 1, 0, 'TamNgung'),
('NL-THIT-01', N'Thịt Bò Úc Nhập',               'NguyenLieu', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Kg'),   320000, 1, 1, 'TamNgung'),
('NL-CAPH-01', N'Cà Phê Rang Xay',               'NguyenLieu', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Kg'),   180000, 1, 1, 'TamNgung'),
('NL-SUA-01',  N'Sữa Đặc Ông Thọ',               'NguyenLieu', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Lon'),   23000, 1, 1, 'TamNgung'),
('NL-DUONG01', N'Đường Trắng',                    'NguyenLieu', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Kg'),    22000, 1, 0, 'TamNgung'),
('NL-DA-01',   N'Đá Viên',                        'NguyenLieu', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Kg'),     1500, 1, 0, 'TamNgung'),
('NL-TRUNG01', N'Trứng Gà',                       'NguyenLieu', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Cai'),    3000, 1, 1, 'TamNgung'),
('NL-RAXL-01', N'Rau Xà Lách',                   'NguyenLieu', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Kg'),    25000, 1, 1, 'TamNgung');
GO

-- Fix #5: SYS_PHU_THU — bắt buộc cho BUS_LuuTru_Booking (phụ thu lố giờ, hư hỏng)
INSERT INTO DanhMuc.SanPham (MaSanPham, TenSanPham, LoaiSanPham, IdDonViTinh, DonGia, LaVatTu, TrangThai) VALUES
('SYS_PHU_THU', N'[Hệ Thống] Phụ Thu', 'HangHoa', (SELECT Id FROM DanhMuc.DonViTinh WHERE MaDonVi='Cai'), 0, 0, 'TamNgung');
GO

-- ============================================================================
-- PHẦN 5: CẤU HÌNH VÉ (SanPham_Ve)
-- Fix #2: Dùng subquery theo MaSanPham thay vì hard-code Id
-- ============================================================================
INSERT INTO DanhMuc.SanPham_Ve (IdSanPham, LoaiVe, DoiTuongVe, CanTaoToken)
SELECT Id, 'VeLe',    'NguoiLon',     1 FROM DanhMuc.SanPham WHERE MaSanPham = 'VE-NL-001'
UNION ALL
SELECT Id, 'VeLe',    'TreEm',        1 FROM DanhMuc.SanPham WHERE MaSanPham = 'VE-TE-001'
UNION ALL
SELECT Id, 'VeCombo', 'NguoiLon',     1 FROM DanhMuc.SanPham WHERE MaSanPham = 'VE-NL-CMB'
UNION ALL
SELECT Id, 'VeCombo', 'TreEm',        1 FROM DanhMuc.SanPham WHERE MaSanPham = 'VE-TE-CMB'
UNION ALL
SELECT Id, 'VeLe',    'NguoiCaoTuoi', 0 FROM DanhMuc.SanPham WHERE MaSanPham = 'VE-NCT-01';
GO

-- ============================================================================
-- PHẦN 6: MÓN ĂN (DanhMuc.MonAn)
-- Fix #2: Dùng subquery theo MaSanPham và TenNhaHang
-- ============================================================================
INSERT INTO DanhMuc.MonAn (IdSanPham, IdNhaHang, PhanLoai, MoTaNgan)
SELECT sp.Id, nh.Id, N'MONCHINH', N'Phở bò truyền thống, nước dùng ninh 12 tiếng'
FROM DanhMuc.SanPham sp, DanhMuc.NhaHang nh
WHERE sp.MaSanPham='DA-PHO-01' AND nh.TenNhaHang=N'Nhà Hàng Đại Nam Palace';

INSERT INTO DanhMuc.MonAn (IdSanPham, IdNhaHang, PhanLoai, MoTaNgan)
SELECT sp.Id, nh.Id, N'MONCHINH', N'Cơm tấm Sài Gòn, sườn cốt lết nướng than'
FROM DanhMuc.SanPham sp, DanhMuc.NhaHang nh
WHERE sp.MaSanPham='DA-COM-01' AND nh.TenNhaHang=N'Nhà Hàng Đại Nam Palace';

INSERT INTO DanhMuc.MonAn (IdSanPham, IdNhaHang, PhanLoai, MoTaNgan)
SELECT sp.Id, nh.Id, N'MONCHINH', N'Bún bò Huế, ớt sa tế cay nồng'
FROM DanhMuc.SanPham sp, DanhMuc.NhaHang nh
WHERE sp.MaSanPham='DA-BUN-01' AND nh.TenNhaHang=N'Nhà Hàng Đại Nam Palace';

INSERT INTO DanhMuc.MonAn (IdSanPham, IdNhaHang, PhanLoai, MoTaNgan)
SELECT sp.Id, nh.Id, N'MONCHINH', N'Mì xào các loại hải sản tươi'
FROM DanhMuc.SanPham sp, DanhMuc.NhaHang nh
WHERE sp.MaSanPham='DA-MI-01' AND nh.TenNhaHang=N'Nhà Hàng Đại Nam Palace';

INSERT INTO DanhMuc.MonAn (IdSanPham, IdNhaHang, PhanLoai, MoTaNgan)
SELECT sp.Id, nh.Id, N'ANVAT', N'Gà rán giòn kiểu Mỹ'
FROM DanhMuc.SanPham sp, DanhMuc.NhaHang nh
WHERE sp.MaSanPham='DA-GA-01' AND nh.TenNhaHang=N'Food Court Trò Chơi';

INSERT INTO DanhMuc.MonAn (IdSanPham, IdNhaHang, PhanLoai, MoTaNgan)
SELECT sp.Id, nh.Id, N'MONCHINH', N'Buffet Trưa Đại Nam Palace'
FROM DanhMuc.SanPham sp, DanhMuc.NhaHang nh
WHERE sp.MaSanPham='DA-BF-01' AND nh.TenNhaHang=N'Nhà Hàng Đại Nam Palace';

INSERT INTO DanhMuc.MonAn (IdSanPham, IdNhaHang, PhanLoai, MoTaNgan)
SELECT sp.Id, nh.Id, N'MONCHINH', N'Buffet Tối Hải Sản'
FROM DanhMuc.SanPham sp, DanhMuc.NhaHang nh
WHERE sp.MaSanPham='DA-BF-02' AND nh.TenNhaHang=N'Nhà Hàng Đại Nam Palace';

INSERT INTO DanhMuc.MonAn (IdSanPham, IdNhaHang, PhanLoai, MoTaNgan)
SELECT sp.Id, nh.Id, N'ANVAT', N'Khoai môn chiên giòn'
FROM DanhMuc.SanPham sp, DanhMuc.NhaHang nh
WHERE sp.MaSanPham='DA-KM-01' AND nh.TenNhaHang=N'Quầy Ăn Vặt Biển';

INSERT INTO DanhMuc.MonAn (IdSanPham, IdNhaHang, PhanLoai, MoTaNgan)
SELECT sp.Id, nh.Id, N'ANVAT', N'Xúc xích nướng than hoa'
FROM DanhMuc.SanPham sp, DanhMuc.NhaHang nh
WHERE sp.MaSanPham='DA-XUC-01' AND nh.TenNhaHang=N'Quầy Ăn Vặt Biển';
GO

-- ============================================================================
-- PHẦN 7: LOẠI PHÒNG & PHÒNG KHÁCH SẠN
-- Fix #9: Phần 3 bị cắt đứt — bổ sung đầy đủ
-- ============================================================================
-- LoaiPhong kế thừa IdSanPham (Shared PK)
INSERT INTO DanhMuc.LoaiPhong (IdSanPham, TenLoai, MoTa, SoNguoiToiDa, SoTreEmToiDa, DienTich, ConHoatDong)
SELECT Id, N'Standard Twin', N'Phòng tiêu chuẩn 2 giường đơn, view vườn', 2, 1, 28.00, 1
FROM DanhMuc.SanPham WHERE MaSanPham = 'LT-STD-01';

INSERT INTO DanhMuc.LoaiPhong (IdSanPham, TenLoai, MoTa, SoNguoiToiDa, SoTreEmToiDa, DienTich, ConHoatDong)
SELECT Id, N'Deluxe King', N'Phòng cao cấp giường đôi, view hồ bơi', 2, 1, 35.00, 1
FROM DanhMuc.SanPham WHERE MaSanPham = 'LT-DLX-01';

INSERT INTO DanhMuc.LoaiPhong (IdSanPham, TenLoai, MoTa, SoNguoiToiDa, SoTreEmToiDa, DienTich, ConHoatDong)
SELECT Id, N'Suite View Hồ', N'Suite sang trọng, phòng khách riêng, view hồ', 3, 2, 65.00, 1
FROM DanhMuc.SanPham WHERE MaSanPham = 'LT-SUI-01';

INSERT INTO DanhMuc.LoaiPhong (IdSanPham, TenLoai, MoTa, SoNguoiToiDa, SoTreEmToiDa, DienTich, ConHoatDong)
SELECT Id, N'Villa Gia Đình', N'Villa riêng biệt, hồ bơi mini, 2 phòng ngủ', 4, 2, 120.00, 1
FROM DanhMuc.SanPham WHERE MaSanPham = 'LT-VIL-01';
GO

-- Phòng vật lý (12 phòng: 6 Standard, 4 Deluxe, 2 Suite)
DECLARE @IdKvKS INT = (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc = 'KV_KS');
DECLARE @IdStd  INT = (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'LT-STD-01');
DECLARE @IdDlx  INT = (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'LT-DLX-01');
DECLARE @IdSui  INT = (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'LT-SUI-01');
DECLARE @IdVil  INT = (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'LT-VIL-01');

INSERT INTO DanhMuc.Phong (MaPhong, IdLoaiPhong, IdKhuVuc, Tang, DienTich, TrangThai) VALUES
('P101', @IdStd, @IdKvKS, 1, 28.00, 'Trong'),
('P102', @IdStd, @IdKvKS, 1, 28.00, 'Trong'),
('P103', @IdStd, @IdKvKS, 1, 28.00, 'Trong'),
('P201', @IdStd, @IdKvKS, 2, 28.00, 'Trong'),
('P202', @IdStd, @IdKvKS, 2, 28.00, 'Trong'),
('P203', @IdStd, @IdKvKS, 2, 28.00, 'Trong'),
('P301', @IdDlx, @IdKvKS, 3, 35.00, 'Trong'),
('P302', @IdDlx, @IdKvKS, 3, 35.00, 'Trong'),
('P303', @IdDlx, @IdKvKS, 3, 35.00, 'Trong'),
('P304', @IdDlx, @IdKvKS, 3, 35.00, 'Trong'),
('P401', @IdSui, @IdKvKS, 4, 65.00, 'Trong'),
('P402', @IdSui, @IdKvKS, 4, 65.00, 'Trong'),
('VIL01',@IdVil, @IdKvKS, 1,120.00, 'Trong'),
('VIL02',@IdVil, @IdKvKS, 1,120.00, 'Trong');
GO

-- ============================================================================
-- PHẦN 8: TÀI SẢN CHO THUÊ VẬT LÝ
-- Fix #10: TaiSanChoThue, TuDo, ChoiNghiMat, PhuongTienDiChuyen chưa có
-- ============================================================================
DECLARE @IdKvBien    INT = (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc = 'KV_BIEN');
DECLARE @IdKvNuoc    INT = (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc = 'KV_NUOC');
DECLARE @IdSpTuS     INT = (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'TD-TU-S01');
DECLARE @IdSpTuL     INT = (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'TD-TU-L01');
DECLARE @IdSpChoi    INT = (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'TD-CHOI-01');
DECLARE @IdSpXeDi    INT = (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'TD-XE-DI');
DECLARE @IdSpXeDap   INT = (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'TD-XE-DAP');

-- Tủ đồ nhỏ (S) — cố định tại khu biển
INSERT INTO DanhMuc.TaiSanChoThue (TenTaiSan, IdSanPham, MaVachThietBi, TrangThai) VALUES
(N'Tủ S-01', @IdSpTuS, 'TU-S-001', 'SanSang'),
(N'Tủ S-02', @IdSpTuS, 'TU-S-002', 'SanSang'),
(N'Tủ S-03', @IdSpTuS, 'TU-S-003', 'SanSang'),
(N'Tủ S-04', @IdSpTuS, 'TU-S-004', 'SanSang'),
(N'Tủ S-05', @IdSpTuS, 'TU-S-005', 'SanSang');

INSERT INTO DanhMuc.TuDo (IdTaiSan, IdKhuVuc)
SELECT Id, @IdKvBien FROM DanhMuc.TaiSanChoThue WHERE MaVachThietBi IN ('TU-S-001','TU-S-002','TU-S-003','TU-S-004','TU-S-005');

-- Tủ đồ lớn (L) — cố định tại khu nước
INSERT INTO DanhMuc.TaiSanChoThue (TenTaiSan, IdSanPham, MaVachThietBi, TrangThai) VALUES
(N'Tủ L-01', @IdSpTuL, 'TU-L-001', 'SanSang'),
(N'Tủ L-02', @IdSpTuL, 'TU-L-002', 'SanSang'),
(N'Tủ L-03', @IdSpTuL, 'TU-L-003', 'SanSang');

INSERT INTO DanhMuc.TuDo (IdTaiSan, IdKhuVuc)
SELECT Id, @IdKvNuoc FROM DanhMuc.TaiSanChoThue WHERE MaVachThietBi IN ('TU-L-001','TU-L-002','TU-L-003');

-- Chòi nghỉ mát — cố định tại khu biển
INSERT INTO DanhMuc.TaiSanChoThue (TenTaiSan, IdSanPham, MaVachThietBi, TrangThai) VALUES
(N'Chòi Biển 01', @IdSpChoi, 'CHOI-B-001', 'SanSang'),
(N'Chòi Biển 02', @IdSpChoi, 'CHOI-B-002', 'SanSang'),
(N'Chòi Biển 03', @IdSpChoi, 'CHOI-B-003', 'SanSang'),
(N'Chòi Biển 04', @IdSpChoi, 'CHOI-B-004', 'SanSang'),
(N'Chòi Biển 05', @IdSpChoi, 'CHOI-B-005', 'SanSang');

INSERT INTO DanhMuc.ChoiNghiMat (IdTaiSan, IdKhuVuc, SucChua)
SELECT Id, @IdKvBien, 4 FROM DanhMuc.TaiSanChoThue
WHERE MaVachThietBi IN ('CHOI-B-001','CHOI-B-002','CHOI-B-003','CHOI-B-004','CHOI-B-005');

-- Xe điện — lưu động (IdKhuVucHienTai = khu biển ban đầu)
INSERT INTO DanhMuc.TaiSanChoThue (TenTaiSan, IdSanPham, MaVachThietBi, TrangThai) VALUES
(N'Xe Điện 01', @IdSpXeDi, 'XE-DI-001', 'SanSang'),
(N'Xe Điện 02', @IdSpXeDi, 'XE-DI-002', 'SanSang'),
(N'Xe Điện 03', @IdSpXeDi, 'XE-DI-003', 'SanSang'),
(N'Xe Điện 04', @IdSpXeDi, 'XE-DI-004', 'SanSang'),
(N'Xe Điện 05', @IdSpXeDi, 'XE-DI-005', 'SanSang');

INSERT INTO DanhMuc.PhuongTienDiChuyen (IdTaiSan, BienSo, SoGhe, LoaiXe, IdKhuVucHienTai)
SELECT Id, 'XD-' + RIGHT(MaVachThietBi, 3), 2, 'XeDien', @IdKvBien
FROM DanhMuc.TaiSanChoThue WHERE MaVachThietBi IN ('XE-DI-001','XE-DI-002','XE-DI-003','XE-DI-004','XE-DI-005');

-- Xe đạp — lưu động
INSERT INTO DanhMuc.TaiSanChoThue (TenTaiSan, IdSanPham, MaVachThietBi, TrangThai) VALUES
(N'Xe Đạp 01', @IdSpXeDap, 'XE-DAP-001', 'SanSang'),
(N'Xe Đạp 02', @IdSpXeDap, 'XE-DAP-002', 'SanSang'),
(N'Xe Đạp 03', @IdSpXeDap, 'XE-DAP-003', 'SanSang'),
(N'Xe Đạp 04', @IdSpXeDap, 'XE-DAP-004', 'SanSang'),
(N'Xe Đạp 05', @IdSpXeDap, 'XE-DAP-005', 'SanSang');

INSERT INTO DanhMuc.PhuongTienDiChuyen (IdTaiSan, BienSo, SoGhe, LoaiXe, IdKhuVucHienTai)
SELECT Id, 'XDP-' + RIGHT(MaVachThietBi, 3), 1, 'XeDap', @IdKvBien
FROM DanhMuc.TaiSanChoThue WHERE MaVachThietBi IN ('XE-DAP-001','XE-DAP-002','XE-DAP-003','XE-DAP-004','XE-DAP-005');
GO

-- ============================================================================
-- PHẦN 9: NGÀY LỄ (CauHinhNgayLe)
-- Fix #11: isHoliday luôn = false vì không có dữ liệu ngày lễ
-- ============================================================================
DELETE FROM DanhMuc.CauHinhNgayLe;

INSERT INTO DanhMuc.CauHinhNgayLe (TenNgayLe, NgayBatDau, NgayKetThuc, Nam) VALUES
-- 2024
(N'Tết Nguyên Đán 2024',    '2024-02-08', '2024-02-14', 2024),
(N'Giỗ Tổ Hùng Vương 2024', '2024-04-18', '2024-04-18', 2024),
(N'Ngày 30/4 - 1/5 2024',   '2024-04-30', '2024-05-01', 2024),
(N'Quốc Khánh 2/9 2024',    '2024-09-02', '2024-09-03', 2024),
-- 2025
(N'Tết Nguyên Đán 2025',    '2025-01-28', '2025-02-03', 2025),
(N'Giỗ Tổ Hùng Vương 2025', '2025-04-07', '2025-04-07', 2025),
(N'Ngày 30/4 - 1/5 2025',   '2025-04-30', '2025-05-01', 2025),
(N'Quốc Khánh 2/9 2025',    '2025-09-02', '2025-09-03', 2025),
-- 2026
(N'Tết Nguyên Đán 2026',    '2026-02-17', '2026-02-23', 2026),
(N'Giỗ Tổ Hùng Vương 2026', '2026-04-27', '2026-04-27', 2026),
(N'Ngày 30/4 - 1/5 2026',   '2026-04-30', '2026-05-01', 2026),
(N'Quốc Khánh 2/9 2026',    '2026-09-02', '2026-09-03', 2026);
GO

-- ============================================================================
-- PHẦN 10: BẢNG GIÁ
-- Fix #1: Dùng subquery theo MaSanPham thay vì hard-code Id
-- Fix #8: Thêm BangGia_ThueTheoGio cho SP cho thuê (tiền cọc + block giờ)
-- ============================================================================

-- 10A. Giá vé vào khu — Thường ngày
INSERT INTO DanhMuc.BangGia (IdSanPham, GiaBan, LoaiGia, HieuLucTu, HieuLucDen, UuTien, TrangThai)
SELECT Id, 150000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='VE-NL-001' UNION ALL
SELECT Id,  80000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='VE-TE-001' UNION ALL
SELECT Id, 250000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='VE-NL-CMB' UNION ALL
SELECT Id, 150000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='VE-TE-CMB' UNION ALL
SELECT Id,  50000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='VE-NCT-01';

-- 10B. Giá vé vào khu — Ngày lễ (+30%)
INSERT INTO DanhMuc.BangGia (IdSanPham, GiaBan, LoaiGia, HieuLucTu, HieuLucDen, UuTien, TrangThai)
SELECT Id, 200000, 'NgayLe', '2024-01-01', '2030-12-31', 10, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='VE-NL-001' UNION ALL
SELECT Id, 120000, 'NgayLe', '2024-01-01', '2030-12-31', 10, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='VE-TE-001' UNION ALL
SELECT Id, 330000, 'NgayLe', '2024-01-01', '2030-12-31', 10, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='VE-NL-CMB' UNION ALL
SELECT Id, 200000, 'NgayLe', '2024-01-01', '2030-12-31', 10, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='VE-TE-CMB';

-- 10C. Giá vé vào khu — Cuối tuần (+15%)
INSERT INTO DanhMuc.BangGia (IdSanPham, GiaBan, LoaiGia, HieuLucTu, HieuLucDen, UuTien, TrangThai)
SELECT Id, 175000, 'CuoiTuan', '2024-01-01', '2030-12-31', 5, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='VE-NL-001' UNION ALL
SELECT Id, 100000, 'CuoiTuan', '2024-01-01', '2030-12-31', 5, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='VE-TE-001' UNION ALL
SELECT Id, 290000, 'CuoiTuan', '2024-01-01', '2030-12-31', 5, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='VE-NL-CMB';

-- 10D. Giá vé trò chơi
INSERT INTO DanhMuc.BangGia (IdSanPham, GiaBan, LoaiGia, HieuLucTu, HieuLucDen, UuTien, TrangThai)
SELECT Id,  80000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='TC-DQ-001'  UNION ALL
SELECT Id, 100000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='TC-TL-001'  UNION ALL
SELECT Id,  60000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='TC-NMA-001' UNION ALL
SELECT Id,  50000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='TC-OTO-001';

-- 10E. Giá đồ ăn
INSERT INTO DanhMuc.BangGia (IdSanPham, GiaBan, LoaiGia, HieuLucTu, HieuLucDen, UuTien, TrangThai)
SELECT Id,  55000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DA-PHO-01'  UNION ALL
SELECT Id,  50000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DA-COM-01'  UNION ALL
SELECT Id,  50000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DA-BUN-01'  UNION ALL
SELECT Id,  65000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DA-MI-01'   UNION ALL
SELECT Id,  75000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DA-GA-01'   UNION ALL
SELECT Id, 299000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DA-BF-01'   UNION ALL
SELECT Id, 499000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DA-BF-02'   UNION ALL
SELECT Id,  35000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DA-KM-01'   UNION ALL
SELECT Id,  25000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DA-XUC-01';

-- 10F. Giá đồ uống
INSERT INTO DanhMuc.BangGia (IdSanPham, GiaBan, LoaiGia, HieuLucTu, HieuLucDen, UuTien, TrangThai)
SELECT Id, 15000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DU-NS-001'  UNION ALL
SELECT Id, 20000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DU-CCC-001' UNION ALL
SELECT Id, 20000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DU-PEP-001' UNION ALL
SELECT Id, 35000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DU-TRA-001' UNION ALL
SELECT Id, 30000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DU-CF-001'  UNION ALL
SELECT Id, 40000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DU-SM-001'  UNION ALL
SELECT Id, 25000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DU-BIA-001' UNION ALL
SELECT Id, 22000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='DU-RBL-001';

-- 10G. Giá cho thuê (BangGia gốc — GiaBan = giá block đầu)
INSERT INTO DanhMuc.BangGia (IdSanPham, GiaBan, LoaiGia, HieuLucTu, HieuLucDen, UuTien, TrangThai)
SELECT Id,  30000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='TD-TU-S01'  UNION ALL
SELECT Id,  50000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='TD-TU-L01'  UNION ALL
SELECT Id,  40000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='TD-PHAO-01' UNION ALL
SELECT Id,  60000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='TD-PHAO-02' UNION ALL
SELECT Id,  20000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='TD-AO-01'   UNION ALL
SELECT Id,  80000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='TD-XE-DI'   UNION ALL
SELECT Id,  50000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='TD-XE-DAP'  UNION ALL
SELECT Id, 200000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='TD-CHOI-01';

-- BangGia_ThueTheoGio được insert ở PHẦN 11 bên dưới (có DELETE trước) — bỏ block này
-- ============================================================================
-- PHẦN 11: ĐỊNH MỨC NGUYÊN LIỆU (BOM)
-- Fix #2: Dùng subquery theo MaSanPham
-- ============================================================================
-- Cà Phê Sữa Đá: 20g cafe + 30ml sữa đặc + 200g đá
INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.020, N'20g Cà phê rang xay'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl
WHERE tp.MaSanPham='DU-CF-001' AND nl.MaSanPham='NL-CAPH-01'
  AND NOT EXISTS (SELECT 1 FROM DanhMuc.DinhMucNguyenLieu WHERE IdThanhPham=tp.Id AND IdNguyenLieu=nl.Id);

INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.030, N'30ml Sữa đặc'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl
WHERE tp.MaSanPham='DU-CF-001' AND nl.MaSanPham='NL-SUA-01'
  AND NOT EXISTS (SELECT 1 FROM DanhMuc.DinhMucNguyenLieu WHERE IdThanhPham=tp.Id AND IdNguyenLieu=nl.Id);

INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.200, N'200g Đá viên'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl WHERE tp.MaSanPham='DU-CF-001' AND nl.MaSanPham='NL-DA-01';

-- Trà Đào Cam Sả: 200g đá + 20g đường
INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.200, N'200g Đá viên'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl WHERE tp.MaSanPham='DU-TRA-001' AND nl.MaSanPham='NL-DA-01';

INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.020, N'20g Đường'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl WHERE tp.MaSanPham='DU-TRA-001' AND nl.MaSanPham='NL-DUONG01';

-- Sinh Tố Xoài: 200g đá + 30ml sữa + 15g đường
INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.200, N'200g Đá viên'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl WHERE tp.MaSanPham='DU-SM-001' AND nl.MaSanPham='NL-DA-01';

INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.030, N'30ml Sữa đặc'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl WHERE tp.MaSanPham='DU-SM-001' AND nl.MaSanPham='NL-SUA-01';

INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.015, N'15g Đường'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl WHERE tp.MaSanPham='DU-SM-001' AND nl.MaSanPham='NL-DUONG01';
GO

-- ============================================================================
-- PHẦN 12: KHO HÀNG ĐẦY ĐỦ
-- Fix #7: KHO_KHACH, KHO_NCC, KHO_HUY... phải INSERT trước khi BanDich tham chiếu
-- Kho ảo đã có trong Database_DaiNam_V2.sql (KHO_NCC, KHO_KHACH, KHO_HUY...)
-- Kho vật lý thêm ở đây
-- ============================================================================
-- Xóa kho vật lý cũ nếu có (kho ảo giữ nguyên vì đã có trong file chính)
DELETE FROM Kho.KhoHang WHERE LaKhoAo = 0;

INSERT INTO Kho.KhoHang (MaKho, TenKho, LaKhoAo, ChoPhepTonAm, IdKhuVuc, TrangThai) VALUES
('KHO_TONG',       N'Kho Tổng Đại Nam',              0, 0, (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_CONG'),    'HoatDong'),
('KHO_NH_PALACE',  N'Kho Nhà Hàng Đại Nam Palace',   0, 1, (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_AMTHUC'), 'HoatDong'),
('KHO_ANVAT_BIEN', N'Kho Quầy Ăn Vặt Biển',          0, 1, (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_BIEN'),   'HoatDong'),
('KHO_THUE_BIEN',  N'Kho Thiết Bị Cho Thuê Biển',    0, 0, (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_BIEN'),   'HoatDong'),
('KHO_THU',        N'Kho Thức Ăn Thú',               0, 0, (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_THU'),    'HoatDong');
GO

-- ============================================================================
-- PHẦN 13: NHÀ CUNG CẤP
-- Fix #12: NhaCungCap chưa có  LoHang.IdNhaCungCap NULL hết
-- ============================================================================
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, LoaiDoiTac) VALUES
(N'Công Ty TNHH Thực Phẩm Sạch Miền Nam', '02838001234', 'contact@thucphammienmam.vn', 'ToChuc'),
(N'Công Ty CP Đồ Uống Đại Việt',           '02838005678', 'sales@daivietdrink.vn',      'ToChuc');

INSERT INTO DoiTac.NhaCungCap (IdDoiTac, MaNhaCungCap, MaSoThue, NguoiLienHe, DieuKhoanThanhToan)
SELECT Id, 'NCC001', '0312345678', N'Nguyễn Văn An', N'Thanh toán 30 ngày'
FROM DoiTac.ThongTin WHERE HoTen = N'Công Ty TNHH Thực Phẩm Sạch Miền Nam' AND DaXoa = 0;

INSERT INTO DoiTac.NhaCungCap (IdDoiTac, MaNhaCungCap, MaSoThue, NguoiLienHe, DieuKhoanThanhToan)
SELECT Id, 'NCC002', '0398765432', N'Trần Thị Bình', N'Thanh toán ngay'
FROM DoiTac.ThongTin WHERE HoTen = N'Công Ty CP Đồ Uống Đại Việt' AND DaXoa = 0;
GO
-- 10H. Giá lưu trú — Thường ngày
INSERT INTO DanhMuc.BangGia (IdSanPham, GiaBan, LoaiGia, HieuLucTu, HieuLucDen, UuTien, TrangThai)
SELECT Id, 1200000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='LT-STD-01' UNION ALL
SELECT Id, 1800000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='LT-DLX-01' UNION ALL
SELECT Id, 3500000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='LT-SUI-01' UNION ALL
SELECT Id, 5000000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='LT-VIL-01';

-- 10I. Giá lưu trú — Ngày lễ (+40%)
INSERT INTO DanhMuc.BangGia (IdSanPham, GiaBan, LoaiGia, HieuLucTu, HieuLucDen, UuTien, TrangThai)
SELECT Id, 1700000, 'NgayLe', '2024-01-01', '2030-12-31', 10, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='LT-STD-01' UNION ALL
SELECT Id, 2500000, 'NgayLe', '2024-01-01', '2030-12-31', 10, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='LT-DLX-01' UNION ALL
SELECT Id, 5000000, 'NgayLe', '2024-01-01', '2030-12-31', 10, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='LT-SUI-01' UNION ALL
SELECT Id, 7000000, 'NgayLe', '2024-01-01', '2030-12-31', 10, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='LT-VIL-01';

-- 10J. Giá gửi xe
INSERT INTO DanhMuc.BangGia (IdSanPham, GiaBan, LoaiGia, HieuLucTu, HieuLucDen, UuTien, TrangThai)
SELECT Id, 10000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='GX-XM-001'  UNION ALL
SELECT Id, 30000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='GX-OTO4-01' UNION ALL
SELECT Id, 50000, 'MacDinh', '2024-01-01', '2030-12-31', 0, 'HoatDong' FROM DanhMuc.SanPham WHERE MaSanPham='GX-OTO16';
GO

-- ============================================================================
-- PHẦN 11: BẢNG GIÁ THUÊ THEO GIỜ (Fix #8)
-- DAL_ThueDo.LayDanhSachNguonChoThue() đọc bảng này để lấy TienCoc + block giờ
-- Xóa trước để tránh duplicate khi chạy lại
-- ============================================================================
DELETE FROM DanhMuc.BangGia_ThueTheoGio
WHERE IdBangGia IN (
    SELECT bg.Id FROM DanhMuc.BangGia bg
    JOIN DanhMuc.SanPham sp ON bg.IdSanPham = sp.Id
    WHERE sp.MaSanPham IN ('TD-TU-S01','TD-TU-L01','TD-PHAO-01','TD-PHAO-02',
                           'TD-AO-01','TD-XE-DI','TD-XE-DAP','TD-CHOI-01')
);
-- Tủ S: block 4h, mỗi 1h thêm 15k, cọc 50k
INSERT INTO DanhMuc.BangGia_ThueTheoGio (IdBangGia, PhutBlock, PhutTiep, GiaPhuThu, TienCoc)
SELECT bg.Id, 240, 60, 15000, 50000
FROM DanhMuc.BangGia bg JOIN DanhMuc.SanPham sp ON bg.IdSanPham=sp.Id
WHERE sp.MaSanPham='TD-TU-S01' AND bg.LoaiGia='MacDinh';

-- Tủ L: block 4h, mỗi 1h thêm 25k, cọc 100k
INSERT INTO DanhMuc.BangGia_ThueTheoGio (IdBangGia, PhutBlock, PhutTiep, GiaPhuThu, TienCoc)
SELECT bg.Id, 240, 60, 25000, 100000
FROM DanhMuc.BangGia bg JOIN DanhMuc.SanPham sp ON bg.IdSanPham=sp.Id
WHERE sp.MaSanPham='TD-TU-L01' AND bg.LoaiGia='MacDinh';

-- Phao tròn: block 2h, mỗi 30 phút thêm 20k, cọc 50k
INSERT INTO DanhMuc.BangGia_ThueTheoGio (IdBangGia, PhutBlock, PhutTiep, GiaPhuThu, TienCoc)
SELECT bg.Id, 120, 30, 20000, 50000
FROM DanhMuc.BangGia bg JOIN DanhMuc.SanPham sp ON bg.IdSanPham=sp.Id
WHERE sp.MaSanPham='TD-PHAO-01' AND bg.LoaiGia='MacDinh';

-- Phao Flamingo: block 2h, mỗi 30 phút thêm 30k, cọc 100k
INSERT INTO DanhMuc.BangGia_ThueTheoGio (IdBangGia, PhutBlock, PhutTiep, GiaPhuThu, TienCoc)
SELECT bg.Id, 120, 30, 30000, 100000
FROM DanhMuc.BangGia bg JOIN DanhMuc.SanPham sp ON bg.IdSanPham=sp.Id
WHERE sp.MaSanPham='TD-PHAO-02' AND bg.LoaiGia='MacDinh';

-- Áo phao: block 4h, mỗi 1h thêm 10k, cọc 30k
INSERT INTO DanhMuc.BangGia_ThueTheoGio (IdBangGia, PhutBlock, PhutTiep, GiaPhuThu, TienCoc)
SELECT bg.Id, 240, 60, 10000, 30000
FROM DanhMuc.BangGia bg JOIN DanhMuc.SanPham sp ON bg.IdSanPham=sp.Id
WHERE sp.MaSanPham='TD-AO-01' AND bg.LoaiGia='MacDinh';

-- Xe điện: block 1h, mỗi 30 phút thêm 40k, cọc 200k
INSERT INTO DanhMuc.BangGia_ThueTheoGio (IdBangGia, PhutBlock, PhutTiep, GiaPhuThu, TienCoc)
SELECT bg.Id, 60, 30, 40000, 200000
FROM DanhMuc.BangGia bg JOIN DanhMuc.SanPham sp ON bg.IdSanPham=sp.Id
WHERE sp.MaSanPham='TD-XE-DI' AND bg.LoaiGia='MacDinh';

-- Xe đạp: block 1h, mỗi 30 phút thêm 25k, cọc 100k
INSERT INTO DanhMuc.BangGia_ThueTheoGio (IdBangGia, PhutBlock, PhutTiep, GiaPhuThu, TienCoc)
SELECT bg.Id, 60, 30, 25000, 100000
FROM DanhMuc.BangGia bg JOIN DanhMuc.SanPham sp ON bg.IdSanPham=sp.Id
WHERE sp.MaSanPham='TD-XE-DAP' AND bg.LoaiGia='MacDinh';

-- Chòi nghỉ mát: block 2h, mỗi 30 phút thêm 80k, cọc 300k
INSERT INTO DanhMuc.BangGia_ThueTheoGio (IdBangGia, PhutBlock, PhutTiep, GiaPhuThu, TienCoc)
SELECT bg.Id, 120, 30, 80000, 300000
FROM DanhMuc.BangGia bg JOIN DanhMuc.SanPham sp ON bg.IdSanPham=sp.Id
WHERE sp.MaSanPham='TD-CHOI-01' AND bg.LoaiGia='MacDinh';
GO

-- ============================================================================
-- PHẦN 12: ĐỊNH MỨC NGUYÊN LIỆU (Fix #2 — dùng subquery + guard chống duplicate)
-- ============================================================================
-- Cà Phê Sữa Đá: 20g cafe + 30ml sữa + 200g đá
INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.020, N'20g Cà phê rang xay'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl
WHERE tp.MaSanPham='DU-CF-001' AND nl.MaSanPham='NL-CAPH-01'
  AND NOT EXISTS (SELECT 1 FROM DanhMuc.DinhMucNguyenLieu x WHERE x.IdThanhPham=tp.Id AND x.IdNguyenLieu=nl.Id);

INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.030, N'30ml Sữa đặc'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl
WHERE tp.MaSanPham='DU-CF-001' AND nl.MaSanPham='NL-SUA-01'
  AND NOT EXISTS (SELECT 1 FROM DanhMuc.DinhMucNguyenLieu x WHERE x.IdThanhPham=tp.Id AND x.IdNguyenLieu=nl.Id);

INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.200, N'200g Đá viên'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl
WHERE tp.MaSanPham='DU-CF-001' AND nl.MaSanPham='NL-DA-01'
  AND NOT EXISTS (SELECT 1 FROM DanhMuc.DinhMucNguyenLieu x WHERE x.IdThanhPham=tp.Id AND x.IdNguyenLieu=nl.Id);

-- Trà Đào: 200g đá + 20g đường
INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.200, N'200g Đá viên'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl
WHERE tp.MaSanPham='DU-TRA-001' AND nl.MaSanPham='NL-DA-01'
  AND NOT EXISTS (SELECT 1 FROM DanhMuc.DinhMucNguyenLieu x WHERE x.IdThanhPham=tp.Id AND x.IdNguyenLieu=nl.Id);

INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.020, N'20g Đường'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl
WHERE tp.MaSanPham='DU-TRA-001' AND nl.MaSanPham='NL-DUONG01'
  AND NOT EXISTS (SELECT 1 FROM DanhMuc.DinhMucNguyenLieu x WHERE x.IdThanhPham=tp.Id AND x.IdNguyenLieu=nl.Id);

-- Sinh Tố Xoài: 200g đá + 30ml sữa + 15g đường
INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.200, N'200g Đá viên'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl
WHERE tp.MaSanPham='DU-SM-001' AND nl.MaSanPham='NL-DA-01'
  AND NOT EXISTS (SELECT 1 FROM DanhMuc.DinhMucNguyenLieu x WHERE x.IdThanhPham=tp.Id AND x.IdNguyenLieu=nl.Id);

INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.030, N'30ml Sữa đặc'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl
WHERE tp.MaSanPham='DU-SM-001' AND nl.MaSanPham='NL-SUA-01'
  AND NOT EXISTS (SELECT 1 FROM DanhMuc.DinhMucNguyenLieu x WHERE x.IdThanhPham=tp.Id AND x.IdNguyenLieu=nl.Id);

INSERT INTO DanhMuc.DinhMucNguyenLieu (IdThanhPham, IdNguyenLieu, SoLuong, GhiChu)
SELECT tp.Id, nl.Id, 0.015, N'15g Đường'
FROM DanhMuc.SanPham tp, DanhMuc.SanPham nl
WHERE tp.MaSanPham='DU-SM-001' AND nl.MaSanPham='NL-DUONG01'
  AND NOT EXISTS (SELECT 1 FROM DanhMuc.DinhMucNguyenLieu x WHERE x.IdThanhPham=tp.Id AND x.IdNguyenLieu=nl.Id);
GO

-- ============================================================================
-- PHẦN 13: KHO HÀNG ĐẦY ĐỦ (Fix #7 — thêm kho ảo để BanDich không NULL)
-- Kho ảo đã có trong Database_DaiNam_V2.sql (KHO_NCC, KHO_KHACH, KHO_HUY...)
-- Chỉ thêm kho vật lý chưa có
-- ============================================================================
-- Fix syntax: WHERE NOT EXISTS phải nằm trong subquery, không phải sau FROM
IF NOT EXISTS (SELECT 1 FROM Kho.KhoHang WHERE MaKho='KHO_TONG')
    INSERT INTO Kho.KhoHang (MaKho, TenKho, LaKhoAo, ChoPhepTonAm, IdKhuVuc, TrangThai)
    SELECT 'KHO_TONG', N'Kho Tổng Đại Nam', 0, 0, Id, 'HoatDong' FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_CONG';

IF NOT EXISTS (SELECT 1 FROM Kho.KhoHang WHERE MaKho='KHO_NH_PALACE')
    INSERT INTO Kho.KhoHang (MaKho, TenKho, LaKhoAo, ChoPhepTonAm, IdKhuVuc, TrangThai)
    SELECT 'KHO_NH_PALACE', N'Kho Nhà Hàng Đại Nam Palace', 0, 1, Id, 'HoatDong' FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_AMTHUC';

IF NOT EXISTS (SELECT 1 FROM Kho.KhoHang WHERE MaKho='KHO_ANVAT_BIEN')
    INSERT INTO Kho.KhoHang (MaKho, TenKho, LaKhoAo, ChoPhepTonAm, IdKhuVuc, TrangThai)
    SELECT 'KHO_ANVAT_BIEN', N'Kho Quầy Ăn Vặt Biển', 0, 1, Id, 'HoatDong' FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_BIEN';

IF NOT EXISTS (SELECT 1 FROM Kho.KhoHang WHERE MaKho='KHO_THUE_BIEN')
    INSERT INTO Kho.KhoHang (MaKho, TenKho, LaKhoAo, ChoPhepTonAm, IdKhuVuc, TrangThai)
    SELECT 'KHO_THUE_BIEN', N'Kho Thiết Bị Cho Thuê Biển', 0, 0, Id, 'HoatDong' FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_BIEN';
GO

-- ============================================================================
-- PHẦN 14: NHÀ CUNG CẤP (Fix #12)
-- ============================================================================
-- Fix duplicate: chỉ insert ThongTin nếu SĐT chưa tồn tại
IF NOT EXISTS (SELECT 1 FROM DoiTac.ThongTin WHERE DienThoai='02838001234' AND DaXoa=0)
    INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac)
    VALUES (N'Công Ty TNHH Thực Phẩm Sạch Miền Nam', '02838001234', 'contact@thucphammienmam.vn', N'123 Nguyễn Văn Linh, Q7, TP.HCM', 'ToChuc');

IF NOT EXISTS (SELECT 1 FROM DoiTac.ThongTin WHERE DienThoai='02838005678' AND DaXoa=0)
    INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac)
    VALUES (N'Công Ty CP Đồ Uống Đại Việt', '02838005678', 'sales@daivietdrink.vn', N'456 Lê Văn Việt, Q9, TP.HCM', 'ToChuc');

IF NOT EXISTS (SELECT 1 FROM DoiTac.NhaCungCap WHERE MaNhaCungCap='NCC001')
    INSERT INTO DoiTac.NhaCungCap (IdDoiTac, MaNhaCungCap, MaSoThue, NguoiLienHe, DieuKhoanThanhToan)
    SELECT Id, 'NCC001', '0312345678', N'Nguyễn Văn A', N'Thanh toán 30 ngày sau khi nhận hàng'
    FROM DoiTac.ThongTin WHERE HoTen = N'Công Ty TNHH Thực Phẩm Sạch Miền Nam' AND DaXoa=0;

IF NOT EXISTS (SELECT 1 FROM DoiTac.NhaCungCap WHERE MaNhaCungCap='NCC002')
    INSERT INTO DoiTac.NhaCungCap (IdDoiTac, MaNhaCungCap, MaSoThue, NguoiLienHe, DieuKhoanThanhToan)
    SELECT Id, 'NCC002', '0398765432', N'Trần Thị B', N'Thanh toán ngay khi nhận hàng'
    FROM DoiTac.ThongTin WHERE HoTen = N'Công Ty CP Đồ Uống Đại Việt' AND DaXoa=0;
GO

-- ============================================================================
-- PHẦN 15: MENU POS (Fix #4 — dùng subquery theo MaDiemBan và MaSanPham)
-- ============================================================================
-- Quầy Vé Cổng 1 + 2: Vé + Gửi xe
INSERT INTO BanHang.Menu_POS (IdDiemBan, IdSanPham, ThuTuHienThi)
SELECT pos.Id, sp.Id, 1 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_CONG01' AND sp.MaSanPham='VE-NL-001' UNION ALL
SELECT pos.Id, sp.Id, 2 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_CONG01' AND sp.MaSanPham='VE-TE-001' UNION ALL
SELECT pos.Id, sp.Id, 3 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_CONG01' AND sp.MaSanPham='VE-NL-CMB' UNION ALL
SELECT pos.Id, sp.Id, 4 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_CONG01' AND sp.MaSanPham='VE-TE-CMB' UNION ALL
SELECT pos.Id, sp.Id, 5 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_CONG01' AND sp.MaSanPham='VE-NCT-01' UNION ALL
SELECT pos.Id, sp.Id, 10 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_CONG01' AND sp.MaSanPham='GX-XM-001' UNION ALL
SELECT pos.Id, sp.Id, 11 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_CONG01' AND sp.MaSanPham='GX-OTO4-01' UNION ALL
SELECT pos.Id, sp.Id, 12 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_CONG01' AND sp.MaSanPham='GX-OTO16';

INSERT INTO BanHang.Menu_POS (IdDiemBan, IdSanPham, ThuTuHienThi)
SELECT pos.Id, sp.Id, 1 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_CONG02' AND sp.MaSanPham='VE-NL-001' UNION ALL
SELECT pos.Id, sp.Id, 2 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_CONG02' AND sp.MaSanPham='VE-TE-001' UNION ALL
SELECT pos.Id, sp.Id, 3 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_CONG02' AND sp.MaSanPham='VE-NL-CMB' UNION ALL
SELECT pos.Id, sp.Id, 4 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_CONG02' AND sp.MaSanPham='VE-TE-CMB' UNION ALL
SELECT pos.Id, sp.Id, 5 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_CONG02' AND sp.MaSanPham='VE-NCT-01';

-- Nhà Hàng Palace: Đồ ăn + đồ uống
INSERT INTO BanHang.Menu_POS (IdDiemBan, IdSanPham, ThuTuHienThi)
SELECT pos.Id, sp.Id, 1  FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_NH01' AND sp.MaSanPham='DA-PHO-01' UNION ALL
SELECT pos.Id, sp.Id, 2  FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_NH01' AND sp.MaSanPham='DA-COM-01' UNION ALL
SELECT pos.Id, sp.Id, 3  FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_NH01' AND sp.MaSanPham='DA-BUN-01' UNION ALL
SELECT pos.Id, sp.Id, 4  FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_NH01' AND sp.MaSanPham='DA-MI-01'  UNION ALL
SELECT pos.Id, sp.Id, 5  FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_NH01' AND sp.MaSanPham='DA-BF-01'  UNION ALL
SELECT pos.Id, sp.Id, 6  FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_NH01' AND sp.MaSanPham='DA-BF-02'  UNION ALL
SELECT pos.Id, sp.Id, 10 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_NH01' AND sp.MaSanPham='DU-NS-001' UNION ALL
SELECT pos.Id, sp.Id, 11 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_NH01' AND sp.MaSanPham='DU-TRA-001' UNION ALL
SELECT pos.Id, sp.Id, 12 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_NH01' AND sp.MaSanPham='DU-CF-001'  UNION ALL
SELECT pos.Id, sp.Id, 13 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_NH01' AND sp.MaSanPham='DU-SM-001'  UNION ALL
SELECT pos.Id, sp.Id, 14 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_NH01' AND sp.MaSanPham='DU-BIA-001';

-- Quầy Ăn Vặt Biển
INSERT INTO BanHang.Menu_POS (IdDiemBan, IdSanPham, ThuTuHienThi)
SELECT pos.Id, sp.Id, 1 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BQA01' AND sp.MaSanPham='DA-GA-01'  UNION ALL
SELECT pos.Id, sp.Id, 2 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BQA01' AND sp.MaSanPham='DA-KM-01'  UNION ALL
SELECT pos.Id, sp.Id, 3 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BQA01' AND sp.MaSanPham='DA-XUC-01' UNION ALL
SELECT pos.Id, sp.Id, 4 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BQA01' AND sp.MaSanPham='DU-NS-001' UNION ALL
SELECT pos.Id, sp.Id, 5 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BQA01' AND sp.MaSanPham='DU-CCC-001' UNION ALL
SELECT pos.Id, sp.Id, 6 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BQA01' AND sp.MaSanPham='DU-PEP-001';

-- Bar Nước Công Viên Nước
INSERT INTO BanHang.Menu_POS (IdDiemBan, IdSanPham, ThuTuHienThi)
SELECT pos.Id, sp.Id, 1 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BAR01' AND sp.MaSanPham='DU-NS-001'  UNION ALL
SELECT pos.Id, sp.Id, 2 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BAR01' AND sp.MaSanPham='DU-CCC-001' UNION ALL
SELECT pos.Id, sp.Id, 3 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BAR01' AND sp.MaSanPham='DU-PEP-001' UNION ALL
SELECT pos.Id, sp.Id, 4 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BAR01' AND sp.MaSanPham='DU-TRA-001' UNION ALL
SELECT pos.Id, sp.Id, 5 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BAR01' AND sp.MaSanPham='DU-CF-001'  UNION ALL
SELECT pos.Id, sp.Id, 6 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BAR01' AND sp.MaSanPham='DU-SM-001'  UNION ALL
SELECT pos.Id, sp.Id, 7 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BAR01' AND sp.MaSanPham='DU-BIA-001' UNION ALL
SELECT pos.Id, sp.Id, 8 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_BAR01' AND sp.MaSanPham='DU-RBL-001';

-- Quầy Cho Thuê Tủ & Phao
-- Quầy Cho Thuê Tủ & Phao
INSERT INTO BanHang.Menu_POS (IdDiemBan, IdSanPham, ThuTuHienThi)
SELECT pos.Id, sp.Id, 1 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_THUE01' AND sp.MaSanPham='TD-TU-S01'  UNION ALL
SELECT pos.Id, sp.Id, 2 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_THUE01' AND sp.MaSanPham='TD-TU-L01'  UNION ALL
SELECT pos.Id, sp.Id, 3 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_THUE01' AND sp.MaSanPham='TD-PHAO-01' UNION ALL
SELECT pos.Id, sp.Id, 4 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_THUE01' AND sp.MaSanPham='TD-PHAO-02' UNION ALL
SELECT pos.Id, sp.Id, 5 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_THUE01' AND sp.MaSanPham='TD-AO-01'   UNION ALL
SELECT pos.Id, sp.Id, 6 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_THUE01' AND sp.MaSanPham='TD-XE-DI'   UNION ALL
SELECT pos.Id, sp.Id, 7 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_THUE01' AND sp.MaSanPham='TD-XE-DAP'  UNION ALL
SELECT pos.Id, sp.Id, 8 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_THUE01' AND sp.MaSanPham='TD-CHOI-01';

-- Lễ Tân Khách Sạn
INSERT INTO BanHang.Menu_POS (IdDiemBan, IdSanPham, ThuTuHienThi)
SELECT pos.Id, sp.Id, 1 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_KS01' AND sp.MaSanPham='LT-STD-01' UNION ALL
SELECT pos.Id, sp.Id, 2 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_KS01' AND sp.MaSanPham='LT-DLX-01' UNION ALL
SELECT pos.Id, sp.Id, 3 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_KS01' AND sp.MaSanPham='LT-SUI-01' UNION ALL
SELECT pos.Id, sp.Id, 4 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_KS01' AND sp.MaSanPham='LT-VIL-01';

-- Food Court Trò Chơi
INSERT INTO BanHang.Menu_POS (IdDiemBan, IdSanPham, ThuTuHienThi)
SELECT pos.Id, sp.Id, 1 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_FC01' AND sp.MaSanPham='DA-GA-01'   UNION ALL
SELECT pos.Id, sp.Id, 2 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_FC01' AND sp.MaSanPham='DA-KM-01'   UNION ALL
SELECT pos.Id, sp.Id, 3 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_FC01' AND sp.MaSanPham='DA-XUC-01'  UNION ALL
SELECT pos.Id, sp.Id, 4 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_FC01' AND sp.MaSanPham='DU-NS-001'  UNION ALL
SELECT pos.Id, sp.Id, 5 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_FC01' AND sp.MaSanPham='DU-CCC-001' UNION ALL
SELECT pos.Id, sp.Id, 6 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_FC01' AND sp.MaSanPham='DU-PEP-001' UNION ALL
SELECT pos.Id, sp.Id, 7 FROM BanHang.DiemBanHang_POS pos, DanhMuc.SanPham sp WHERE pos.MaDiemBan='POS_FC01' AND sp.MaSanPham='DU-BIA-001';
GO

-- ============================================================================
-- PHẦN 16: BẢN DỊCH i18n (Fix #3 — dùng subquery theo MaSanPham)
-- ============================================================================
-- Vé
INSERT INTO HeThong.BanDich (LoaiThucThe, IdThucThe, NgonNgu, TruongDich, NoiDung)
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Adult Entrance Ticket'          FROM DanhMuc.SanPham WHERE MaSanPham='VE-NL-001' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Child Entrance Ticket'          FROM DanhMuc.SanPham WHERE MaSanPham='VE-TE-001' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Adult Combo Ticket'             FROM DanhMuc.SanPham WHERE MaSanPham='VE-NL-CMB' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Child Combo Ticket'             FROM DanhMuc.SanPham WHERE MaSanPham='VE-TE-CMB' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Senior Discount Ticket'         FROM DanhMuc.SanPham WHERE MaSanPham='VE-NCT-01' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Ferris Wheel Ticket'            FROM DanhMuc.SanPham WHERE MaSanPham='TC-DQ-001' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Roller Coaster Ticket'          FROM DanhMuc.SanPham WHERE MaSanPham='TC-TL-001' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Haunted House Ticket'           FROM DanhMuc.SanPham WHERE MaSanPham='TC-NMA-001' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Bumper Cars (15 mins)'          FROM DanhMuc.SanPham WHERE MaSanPham='TC-OTO-001';

INSERT INTO HeThong.BanDich (LoaiThucThe, IdThucThe, NgonNgu, TruongDich, NoiDung)
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'成人门票'     FROM DanhMuc.SanPham WHERE MaSanPham='VE-NL-001' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'儿童门票'     FROM DanhMuc.SanPham WHERE MaSanPham='VE-TE-001' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'成人套票'     FROM DanhMuc.SanPham WHERE MaSanPham='VE-NL-CMB' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'儿童套票'     FROM DanhMuc.SanPham WHERE MaSanPham='VE-TE-CMB' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'长者优惠票'   FROM DanhMuc.SanPham WHERE MaSanPham='VE-NCT-01' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'摩天轮票'     FROM DanhMuc.SanPham WHERE MaSanPham='TC-DQ-001' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'过山车票'     FROM DanhMuc.SanPham WHERE MaSanPham='TC-TL-001' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'鬼屋票'       FROM DanhMuc.SanPham WHERE MaSanPham='TC-NMA-001' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'碰碰车(15分)' FROM DanhMuc.SanPham WHERE MaSanPham='TC-OTO-001';

-- Đồ ăn & uống
INSERT INTO HeThong.BanDich (LoaiThucThe, IdThucThe, NgonNgu, TruongDich, NoiDung)
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Traditional Beef Pho'           FROM DanhMuc.SanPham WHERE MaSanPham='DA-PHO-01' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Broken Rice with Grilled Pork'  FROM DanhMuc.SanPham WHERE MaSanPham='DA-COM-01' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Spicy Beef Noodle Soup'         FROM DanhMuc.SanPham WHERE MaSanPham='DA-BUN-01' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Seafood Fried Noodles'          FROM DanhMuc.SanPham WHERE MaSanPham='DA-MI-01'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Crispy Fried Chicken (3 pcs)'   FROM DanhMuc.SanPham WHERE MaSanPham='DA-GA-01'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Lunch Buffet Palace'            FROM DanhMuc.SanPham WHERE MaSanPham='DA-BF-01'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Seafood Dinner Buffet'          FROM DanhMuc.SanPham WHERE MaSanPham='DA-BF-02'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Fried Taro'                     FROM DanhMuc.SanPham WHERE MaSanPham='DA-KM-01'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Grilled Sausage'                FROM DanhMuc.SanPham WHERE MaSanPham='DA-XUC-01' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Lavie Mineral Water 500ml'      FROM DanhMuc.SanPham WHERE MaSanPham='DU-NS-001' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Coca Cola Can 330ml'            FROM DanhMuc.SanPham WHERE MaSanPham='DU-CCC-001' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Pepsi Can 330ml'                FROM DanhMuc.SanPham WHERE MaSanPham='DU-PEP-001' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Peach Orange Lemongrass Tea'    FROM DanhMuc.SanPham WHERE MaSanPham='DU-TRA-001' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Iced Milk Coffee'               FROM DanhMuc.SanPham WHERE MaSanPham='DU-CF-001'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Mango Smoothie'                 FROM DanhMuc.SanPham WHERE MaSanPham='DU-SM-001'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Tiger Beer Can 330ml'           FROM DanhMuc.SanPham WHERE MaSanPham='DU-BIA-001' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Red Bull Can 250ml'             FROM DanhMuc.SanPham WHERE MaSanPham='DU-RBL-001';

INSERT INTO HeThong.BanDich (LoaiThucThe, IdThucThe, NgonNgu, TruongDich, NoiDung)
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'传统牛肉河粉'     FROM DanhMuc.SanPham WHERE MaSanPham='DA-PHO-01' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'烤猪排碎米饭'     FROM DanhMuc.SanPham WHERE MaSanPham='DA-COM-01' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'顺化牛肉粉'       FROM DanhMuc.SanPham WHERE MaSanPham='DA-BUN-01' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'海鲜炒面'         FROM DanhMuc.SanPham WHERE MaSanPham='DA-MI-01'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'脆皮炸鸡(3件)'    FROM DanhMuc.SanPham WHERE MaSanPham='DA-GA-01'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'皇宫自助午餐'     FROM DanhMuc.SanPham WHERE MaSanPham='DA-BF-01'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'海鲜自助晚餐'     FROM DanhMuc.SanPham WHERE MaSanPham='DA-BF-02'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'炸芋头'           FROM DanhMuc.SanPham WHERE MaSanPham='DA-KM-01'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'烤香肠'           FROM DanhMuc.SanPham WHERE MaSanPham='DA-XUC-01' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'拉维矿泉水500ml'  FROM DanhMuc.SanPham WHERE MaSanPham='DU-NS-001' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'可口可乐330ml'    FROM DanhMuc.SanPham WHERE MaSanPham='DU-CCC-001' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'百事可乐330ml'    FROM DanhMuc.SanPham WHERE MaSanPham='DU-PEP-001' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'桃子香橙香茅茶'   FROM DanhMuc.SanPham WHERE MaSanPham='DU-TRA-001' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'冰炼乳咖啡'       FROM DanhMuc.SanPham WHERE MaSanPham='DU-CF-001'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'芒果冰沙'         FROM DanhMuc.SanPham WHERE MaSanPham='DU-SM-001'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'老虎啤酒330ml'    FROM DanhMuc.SanPham WHERE MaSanPham='DU-BIA-001' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'红牛饮料250ml'    FROM DanhMuc.SanPham WHERE MaSanPham='DU-RBL-001';

-- Cho thuê & Lưu trú
INSERT INTO HeThong.BanDich (LoaiThucThe, IdThucThe, NgonNgu, TruongDich, NoiDung)
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Small Locker Rental (S)'        FROM DanhMuc.SanPham WHERE MaSanPham='TD-TU-S01'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Large Locker Rental (L)'        FROM DanhMuc.SanPham WHERE MaSanPham='TD-TU-L01'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Round Float Rental'             FROM DanhMuc.SanPham WHERE MaSanPham='TD-PHAO-01' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Flamingo Float Rental'          FROM DanhMuc.SanPham WHERE MaSanPham='TD-PHAO-02' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Life Jacket Rental'             FROM DanhMuc.SanPham WHERE MaSanPham='TD-AO-01'   UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Electric Bike Rental'           FROM DanhMuc.SanPham WHERE MaSanPham='TD-XE-DI'   UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Bicycle Rental'                 FROM DanhMuc.SanPham WHERE MaSanPham='TD-XE-DAP'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Beach Gazebo Rental (4 pax)'    FROM DanhMuc.SanPham WHERE MaSanPham='TD-CHOI-01' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Standard Twin Room'             FROM DanhMuc.SanPham WHERE MaSanPham='LT-STD-01'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Deluxe King Room'               FROM DanhMuc.SanPham WHERE MaSanPham='LT-DLX-01'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Lake View Suite'                FROM DanhMuc.SanPham WHERE MaSanPham='LT-SUI-01'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Family Villa (4 persons)'       FROM DanhMuc.SanPham WHERE MaSanPham='LT-VIL-01'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Motorbike Parking'              FROM DanhMuc.SanPham WHERE MaSanPham='GX-XM-001'  UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Car Parking (4-7 seats)'        FROM DanhMuc.SanPham WHERE MaSanPham='GX-OTO4-01' UNION ALL
SELECT 'SanPham', Id, 'en-US', 'TenSanPham', N'Bus Parking (16+ seats)'        FROM DanhMuc.SanPham WHERE MaSanPham='GX-OTO16';

INSERT INTO HeThong.BanDich (LoaiThucThe, IdThucThe, NgonNgu, TruongDich, NoiDung)
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'租小储物柜(S)'    FROM DanhMuc.SanPham WHERE MaSanPham='TD-TU-S01'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'租大储物柜(L)'    FROM DanhMuc.SanPham WHERE MaSanPham='TD-TU-L01'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'租圆浮块'         FROM DanhMuc.SanPham WHERE MaSanPham='TD-PHAO-01' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'租火烈鸟浮块'     FROM DanhMuc.SanPham WHERE MaSanPham='TD-PHAO-02' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'租救生衣'         FROM DanhMuc.SanPham WHERE MaSanPham='TD-AO-01'   UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'租电动车'         FROM DanhMuc.SanPham WHERE MaSanPham='TD-XE-DI'   UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'租自行车'         FROM DanhMuc.SanPham WHERE MaSanPham='TD-XE-DAP'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'租海滩凉亭(4人)'  FROM DanhMuc.SanPham WHERE MaSanPham='TD-CHOI-01' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'标准双床房'       FROM DanhMuc.SanPham WHERE MaSanPham='LT-STD-01'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'豪华大床房'       FROM DanhMuc.SanPham WHERE MaSanPham='LT-DLX-01'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'湖景套房'         FROM DanhMuc.SanPham WHERE MaSanPham='LT-SUI-01'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'家庭别墅(4人)'    FROM DanhMuc.SanPham WHERE MaSanPham='LT-VIL-01'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'摩托车停车'       FROM DanhMuc.SanPham WHERE MaSanPham='GX-XM-001'  UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'汽车停车(4-7座)'  FROM DanhMuc.SanPham WHERE MaSanPham='GX-OTO4-01' UNION ALL
SELECT 'SanPham', Id, 'zh-CN', 'TenSanPham', N'巴士停车(16座+)'  FROM DanhMuc.SanPham WHERE MaSanPham='GX-OTO16';

-- Khu Vực
INSERT INTO HeThong.BanDich (LoaiThucThe, IdThucThe, NgonNgu, TruongDich, NoiDung)
SELECT 'KhuVuc', Id, 'en-US', 'TenKhuVuc', N'Main Gate Area'        FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_CONG'     UNION ALL
SELECT 'KhuVuc', Id, 'en-US', 'TenKhuVuc', N'Artificial Beach Area' FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_BIEN'     UNION ALL
SELECT 'KhuVuc', Id, 'en-US', 'TenKhuVuc', N'Water Park Area'       FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_NUOC'     UNION ALL
SELECT 'KhuVuc', Id, 'en-US', 'TenKhuVuc', N'Dai Nam Temple Area'   FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_DEN'      UNION ALL
SELECT 'KhuVuc', Id, 'en-US', 'TenKhuVuc', N'Zoo Area'              FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_THU'      UNION ALL
SELECT 'KhuVuc', Id, 'en-US', 'TenKhuVuc', N'Hotel Area'            FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_KS'       UNION ALL
SELECT 'KhuVuc', Id, 'en-US', 'TenKhuVuc', N'Food Court Area'       FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_AMTHUC'   UNION ALL
SELECT 'KhuVuc', Id, 'en-US', 'TenKhuVuc', N'Amusement Ride Area'   FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_TROICHOI' UNION ALL
SELECT 'KhuVuc', Id, 'en-US', 'TenKhuVuc', N'Parking Area'          FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_BAIXE'    UNION ALL
SELECT 'KhuVuc', Id, 'en-US', 'TenKhuVuc', N'Racing Track'          FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_TRUONGDUA';

INSERT INTO HeThong.BanDich (LoaiThucThe, IdThucThe, NgonNgu, TruongDich, NoiDung)
SELECT 'KhuVuc', Id, 'zh-CN', 'TenKhuVuc', N'主门区'     FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_CONG'     UNION ALL
SELECT 'KhuVuc', Id, 'zh-CN', 'TenKhuVuc', N'人造海滩区' FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_BIEN'     UNION ALL
SELECT 'KhuVuc', Id, 'zh-CN', 'TenKhuVuc', N'水上公园区' FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_NUOC'     UNION ALL
SELECT 'KhuVuc', Id, 'zh-CN', 'TenKhuVuc', N'大南寺庙区'   FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_DEN'      UNION ALL
SELECT 'KhuVuc', Id, 'zh-CN', 'TenKhuVuc', N'动物园区'     FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_THU'      UNION ALL
SELECT 'KhuVuc', Id, 'zh-CN', 'TenKhuVuc', N'酒店区'       FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_KS'       UNION ALL
SELECT 'KhuVuc', Id, 'zh-CN', 'TenKhuVuc', N'美食餐饮区'   FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_AMTHUC'   UNION ALL
SELECT 'KhuVuc', Id, 'zh-CN', 'TenKhuVuc', N'机动游戏区'   FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_TROICHOI' UNION ALL
SELECT 'KhuVuc', Id, 'zh-CN', 'TenKhuVuc', N'停车场'       FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_BAIXE'    UNION ALL
SELECT 'KhuVuc', Id, 'zh-CN', 'TenKhuVuc', N'大南赛马场'   FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_TRUONGDUA';

-- Nhà Hàng
INSERT INTO HeThong.BanDich (LoaiThucThe, IdThucThe, NgonNgu, TruongDich, NoiDung)
SELECT 'NhaHang', Id, 'en-US', 'TenNhaHang', N'Dai Nam Palace Restaurant' FROM DanhMuc.NhaHang WHERE TenNhaHang=N'Nhà Hàng Đại Nam Palace' UNION ALL
SELECT 'NhaHang', Id, 'en-US', 'TenNhaHang', N'Beach Snack Bar'           FROM DanhMuc.NhaHang WHERE TenNhaHang=N'Quầy Ăn Vặt Biển'       UNION ALL
SELECT 'NhaHang', Id, 'en-US', 'TenNhaHang', N'Water Park Drinks Bar'     FROM DanhMuc.NhaHang WHERE TenNhaHang=N'Bar Nước Công Viên Nước' UNION ALL
SELECT 'NhaHang', Id, 'en-US', 'TenNhaHang', N'Amusement Food Court'      FROM DanhMuc.NhaHang WHERE TenNhaHang=N'Food Court Trò Chơi';

INSERT INTO HeThong.BanDich (LoaiThucThe, IdThucThe, NgonNgu, TruongDich, NoiDung)
SELECT 'NhaHang', Id, 'zh-CN', 'TenNhaHang', N'大南皇宫餐厅'   FROM DanhMuc.NhaHang WHERE TenNhaHang=N'Nhà Hàng Đại Nam Palace' UNION ALL
SELECT 'NhaHang', Id, 'zh-CN', 'TenNhaHang', N'海滩小吃吧'     FROM DanhMuc.NhaHang WHERE TenNhaHang=N'Quầy Ăn Vặt Biển'       UNION ALL
SELECT 'NhaHang', Id, 'zh-CN', 'TenNhaHang', N'水上乐园饮料吧' FROM DanhMuc.NhaHang WHERE TenNhaHang=N'Bar Nước Công Viên Nước' UNION ALL
SELECT 'NhaHang', Id, 'zh-CN', 'TenNhaHang', N'游乐区美食广场' FROM DanhMuc.NhaHang WHERE TenNhaHang=N'Food Court Trò Chơi';

-- Kho hàng (Fix #7 — dùng subquery, không hard-code Id)
INSERT INTO HeThong.BanDich (LoaiThucThe, IdThucThe, NgonNgu, TruongDich, NoiDung)
SELECT 'KhoHang', Id, 'en-US', 'TenKho', N'Dai Nam Main Warehouse'          FROM Kho.KhoHang WHERE MaKho='KHO_TONG'       UNION ALL
SELECT 'KhoHang', Id, 'en-US', 'TenKho', N'Palace Restaurant Storage'       FROM Kho.KhoHang WHERE MaKho='KHO_NH_PALACE'  UNION ALL
SELECT 'KhoHang', Id, 'en-US', 'TenKho', N'Beach Snack Bar Storage'         FROM Kho.KhoHang WHERE MaKho='KHO_ANVAT_BIEN' UNION ALL
SELECT 'KhoHang', Id, 'en-US', 'TenKho', N'Beach Rental Equipment Storage'  FROM Kho.KhoHang WHERE MaKho='KHO_THUE_BIEN'  UNION ALL
SELECT 'KhoHang', Id, 'en-US', 'TenKho', N'Supplier (Virtual)'              FROM Kho.KhoHang WHERE MaKho='KHO_NCC'         UNION ALL
SELECT 'KhoHang', Id, 'en-US', 'TenKho', N'Customer (Virtual)'              FROM Kho.KhoHang WHERE MaKho='KHO_KHACH'       UNION ALL
SELECT 'KhoHang', Id, 'en-US', 'TenKho', N'Damaged / Expired (Virtual)'     FROM Kho.KhoHang WHERE MaKho='KHO_HUY'         UNION ALL
SELECT 'KhoHang', Id, 'en-US', 'TenKho', N'Shrinkage (Virtual)'             FROM Kho.KhoHang WHERE MaKho='KHO_THAT_THOAT'  UNION ALL
SELECT 'KhoHang', Id, 'en-US', 'TenKho', N'Returns (Virtual)'               FROM Kho.KhoHang WHERE MaKho='KHO_TRA'         UNION ALL
SELECT 'KhoHang', Id, 'en-US', 'TenKho', N'Inventory Variance (Virtual)'    FROM Kho.KhoHang WHERE MaKho='KHO_CHENH_LECH'  UNION ALL
SELECT 'KhoHang', Id, 'en-US', 'TenKho', N'Maintenance (Virtual)'           FROM Kho.KhoHang WHERE MaKho='KHO_BAOTRI'      UNION ALL
SELECT 'KhoHang', Id, 'en-US', 'TenKho', N'Production / BOM (Virtual)'      FROM Kho.KhoHang WHERE MaKho='KHO_SANXUAT';

INSERT INTO HeThong.BanDich (LoaiThucThe, IdThucThe, NgonNgu, TruongDich, NoiDung)
SELECT 'KhoHang', Id, 'zh-CN', 'TenKho', N'大南总仓库'     FROM Kho.KhoHang WHERE MaKho='KHO_TONG'       UNION ALL
SELECT 'KhoHang', Id, 'zh-CN', 'TenKho', N'皇宫餐厅仓库'   FROM Kho.KhoHang WHERE MaKho='KHO_NH_PALACE'  UNION ALL
SELECT 'KhoHang', Id, 'zh-CN', 'TenKho', N'海滩小吃仓库'   FROM Kho.KhoHang WHERE MaKho='KHO_ANVAT_BIEN' UNION ALL
SELECT 'KhoHang', Id, 'zh-CN', 'TenKho', N'海滩租赁仓库'   FROM Kho.KhoHang WHERE MaKho='KHO_THUE_BIEN'  UNION ALL
SELECT 'KhoHang', Id, 'zh-CN', 'TenKho', N'供应商(虚拟)'   FROM Kho.KhoHang WHERE MaKho='KHO_NCC'         UNION ALL
SELECT 'KhoHang', Id, 'zh-CN', 'TenKho', N'客户(虚拟)'     FROM Kho.KhoHang WHERE MaKho='KHO_KHACH'       UNION ALL
SELECT 'KhoHang', Id, 'zh-CN', 'TenKho', N'损坏/过期(虚拟)'FROM Kho.KhoHang WHERE MaKho='KHO_HUY'         UNION ALL
SELECT 'KhoHang', Id, 'zh-CN', 'TenKho', N'损耗(虚拟)'     FROM Kho.KhoHang WHERE MaKho='KHO_THAT_THOAT'  UNION ALL
SELECT 'KhoHang', Id, 'zh-CN', 'TenKho', N'退货(虚拟)'     FROM Kho.KhoHang WHERE MaKho='KHO_TRA'         UNION ALL
SELECT 'KhoHang', Id, 'zh-CN', 'TenKho', N'盘点差异(虚拟)' FROM Kho.KhoHang WHERE MaKho='KHO_CHENH_LECH'  UNION ALL
SELECT 'KhoHang', Id, 'zh-CN', 'TenKho', N'维修(虚拟)'     FROM Kho.KhoHang WHERE MaKho='KHO_BAOTRI'      UNION ALL
SELECT 'KhoHang', Id, 'zh-CN', 'TenKho', N'生产/BOM(虚拟)' FROM Kho.KhoHang WHERE MaKho='KHO_SANXUAT';
GO

-- ============================================================================
-- PHẦN 17: CẬP NHẬT NHÂN VIÊN — GÁN KHU VỰC (Fix #5)
-- KhuVuc chưa tồn tại lúc insert NhanVien trong file chính
-- Chạy sau khi KhuVuc đã được seed ở Phần 1
-- ============================================================================
UPDATE DoiTac.NhanVien
SET IdKhuVuc = (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_CONG')
WHERE MaNhanVien = 'NV001';  -- Admin → Khu Cổng Chính

UPDATE DoiTac.NhanVien
SET IdKhuVuc = (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_CONG')
WHERE MaNhanVien = 'NV002';  -- Thu Ngân → Khu Cổng Chính
GO

-- ============================================================================
-- KIỂM TRA CUỐI — Chạy để xác nhận dữ liệu đã đúng
-- ============================================================================
SELECT 'SanPham'          AS Bang, COUNT(*) AS SoLuong FROM DanhMuc.SanPham   WHERE DaXoa=0
UNION ALL SELECT 'BangGia',         COUNT(*) FROM DanhMuc.BangGia
UNION ALL SELECT 'BangGia_ThueGio', COUNT(*) FROM DanhMuc.BangGia_ThueTheoGio
UNION ALL SELECT 'LoaiPhong',       COUNT(*) FROM DanhMuc.LoaiPhong
UNION ALL SELECT 'Phong',           COUNT(*) FROM DanhMuc.Phong
UNION ALL SELECT 'TaiSanChoThue',   COUNT(*) FROM DanhMuc.TaiSanChoThue WHERE DaXoa=0
UNION ALL SELECT 'KhoHang',         COUNT(*) FROM Kho.KhoHang
UNION ALL SELECT 'NhaCungCap',      COUNT(*) FROM DoiTac.NhaCungCap
UNION ALL SELECT 'Menu_POS',        COUNT(*) FROM BanHang.Menu_POS
UNION ALL SELECT 'BanDich',         COUNT(*) FROM HeThong.BanDich
UNION ALL SELECT 'CauHinhNgayLe',   COUNT(*) FROM DanhMuc.CauHinhNgayLe
UNION ALL SELECT 'CauHinh_DiemQuyDoi', COUNT(*) FROM HeThong.CauHinh WHERE Khoa='DIEM_QUY_DOI';
GO

