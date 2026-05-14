-- ============================================================================
-- FILE: 03_SeedData_Transactions.sql
-- MỤC ĐÍCH: Dữ liệu giao dịch mock hoàn chỉnh (2024-01 → 2026-05) - ĐÃ GỘP
-- NGUYÊN TẮC: Mua hàng → Trừ kho | KM có/không dùng | Booking đầy đủ luồng
-- ============================================================================
USE [Database_DaiNamv2];
GO
SET NOCOUNT ON;
GO

-- ============================================================================
-- PHẦN 1: NHÂN VIÊN BỔ SUNG
-- ============================================================================
IF NOT EXISTS (SELECT 1 FROM DoiTac.NhanVien WHERE MaNhanVien='NV003')
BEGIN
    INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, LoaiDoiTac) VALUES
    (N'Nguyễn Thị Lan', '0911000003', 'lan.nt@dainam.vn', 'CaNhan');
    INSERT INTO DoiTac.NhanVien (IdDoiTac, MaNhanVien, IdVaiTro, TrangThai, IdKhuVuc)
    VALUES (SCOPE_IDENTITY(), 'NV003', 3, 'DangLam', (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_KS'));

    INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, LoaiDoiTac) VALUES
    (N'Trần Văn Hùng', '0911000004', 'hung.tv@dainam.vn', 'CaNhan');
    INSERT INTO DoiTac.NhanVien (IdDoiTac, MaNhanVien, IdVaiTro, TrangThai, IdKhuVuc)
    VALUES (SCOPE_IDENTITY(), 'NV004', 3, 'DangLam', (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_AMTHUC'));

    INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, LoaiDoiTac) VALUES
    (N'Lê Minh Đức', '0911000005', 'duc.lm@dainam.vn', 'CaNhan');
    INSERT INTO DoiTac.NhanVien (IdDoiTac, MaNhanVien, IdVaiTro, TrangThai, IdKhuVuc)
    VALUES (SCOPE_IDENTITY(), 'NV005', 3, 'DangLam', (SELECT Id FROM DanhMuc.KhuVuc WHERE MaKhuVuc='KV_CONG'));
END
GO

-- ============================================================================
-- PHẦN 2: NHẬP KHO, CHUYỂN KHO TỪ NCC
-- ============================================================================
DECLARE @IdNV1 INT = (SELECT TOP 1 IdDoiTac FROM DoiTac.NhanVien WHERE MaNhanVien='NV001');
DECLARE @IdNCC1 INT = (SELECT TOP 1 dt.Id FROM DoiTac.ThongTin dt JOIN DoiTac.NhaCungCap ncc ON dt.Id=ncc.IdDoiTac WHERE ncc.MaNhaCungCap='NCC001');
DECLARE @IdNCC2 INT = (SELECT TOP 1 dt.Id FROM DoiTac.ThongTin dt JOIN DoiTac.NhaCungCap ncc ON dt.Id=ncc.IdDoiTac WHERE ncc.MaNhaCungCap='NCC002');
DECLARE @KhoNCC INT = (SELECT Id FROM Kho.KhoHang WHERE MaKho='KHO_NCC');
DECLARE @KhoTong INT = (SELECT Id FROM Kho.KhoHang WHERE MaKho='KHO_TONG');
DECLARE @KhoNH INT = (SELECT Id FROM Kho.KhoHang WHERE MaKho='KHO_NH_PALACE');
DECLARE @KhoAV INT = (SELECT Id FROM Kho.KhoHang WHERE MaKho='KHO_ANVAT_BIEN');

-- Đợt 1: Nhập đồ uống đóng chai + nguyên liệu
INSERT INTO Kho.ChungTu (MaChungTu, LoaiChungTu, IdDoiTac, NgayChungTu, LyDo, TrangThai, IdNguoiTao, NgayTao)
VALUES ('NK-240115-001', 'NHAP_MUA', @IdNCC2, '2024-01-15', N'Nhập đồ uống đợt 1', 'DaDuyet', @IdNV1, '2024-01-15');
DECLARE @CT1 INT = SCOPE_IDENTITY();
INSERT INTO Kho.ChiTietChungTu (IdChungTu, IdSanPham, IdKhoXuat, IdKhoNhap, SoLuong, DonGia) VALUES
(@CT1, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='DU-NS-001'),  @KhoNCC, @KhoTong, 500, 8000),
(@CT1, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='DU-CCC-001'), @KhoNCC, @KhoTong, 300, 12000),
(@CT1, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='DU-BIA-001'), @KhoNCC, @KhoTong, 200, 15000);

-- Nhập nguyên liệu cho nhà hàng
INSERT INTO Kho.ChungTu (MaChungTu, LoaiChungTu, IdDoiTac, NgayChungTu, LyDo, TrangThai, IdNguoiTao, NgayTao)
VALUES ('NK-240115-002', 'NHAP_MUA', @IdNCC1, '2024-01-15', N'Nhập nguyên liệu đợt 1', 'DaDuyet', @IdNV1, '2024-01-15');
DECLARE @CT2 INT = SCOPE_IDENTITY();
INSERT INTO Kho.ChiTietChungTu (IdChungTu, IdSanPham, IdKhoXuat, IdKhoNhap, SoLuong, DonGia) VALUES
(@CT2, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='NL-GAO-01'),  @KhoNCC, @KhoNH, 100, 16000),
(@CT2, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='NL-THIT-01'), @KhoNCC, @KhoNH, 50, 280000),
(@CT2, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='NL-SUA-01'),  @KhoNCC, @KhoNH, 100, 18000);

-- Chuyển kho: Tổng → Ăn vặt biển
INSERT INTO Kho.ChungTu (MaChungTu, LoaiChungTu, NgayChungTu, LyDo, TrangThai, IdNguoiTao, NgayTao)
VALUES ('CK-240120-001', 'CHUYEN_KHO', '2024-01-20', N'Cấp hàng cho quầy biển', 'DaDuyet', @IdNV1, '2024-01-20');
DECLARE @CK1 INT = SCOPE_IDENTITY();
INSERT INTO Kho.ChiTietChungTu (IdChungTu, IdSanPham, IdKhoXuat, IdKhoNhap, SoLuong, DonGia) VALUES
(@CK1, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='DU-NS-001'),  @KhoTong, @KhoAV, 100, 8000),
(@CK1, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='DU-CCC-001'), @KhoTong, @KhoAV, 100, 12000);
GO

-- ============================================================================
-- PHẦN 3: KHUYẾN MÃI
-- ============================================================================
INSERT INTO BanHang.KhuyenMai (MaKhuyenMai, TenKhuyenMai, LoaiGiamGia, GiaTriGiam, DonToiThieu, NgayBatDau, NgayKetThuc, UuTien, SoLanToiDa, TrangThai, NguoiTao, NgayTao) VALUES
('TET2024',    N'Giảm 50K Tết',      'SoTien',    50000,  200000, '2024-02-01', '2024-02-20', 10, 500,  0, 1, '2024-01-25'),
('SALE30',     N'Hè 30% Combo',      'PhanTram',  30,     100000, '2024-06-01', '2024-08-31', 5,  300,  0, 1, '2024-05-25'),
('VIP20',      N'VIP Vàng giảm 20%', 'PhanTram',  20,     300000, '2025-11-01', '2026-06-30', 8,  200,  1, 1, '2025-10-25');

-- Điều kiện: VIP20 chỉ cho Vàng + Kim Cương
INSERT INTO BanHang.KhuyenMai_DieuKien (IdKhuyenMai, LoaiDieuKien, PhepSo, GiaTriDieuKien)
SELECT Id, 'HangThanhVien', 'IN', 'VangGold,KimCuong' FROM BanHang.KhuyenMai WHERE MaKhuyenMai='VIP20';

-- Điều kiện: SALE30 chỉ cho vé combo
INSERT INTO BanHang.KhuyenMai_DieuKien (IdKhuyenMai, LoaiDieuKien, PhepSo, GiaTriDieuKien)
SELECT Id, 'SanPhamApDung', 'IN', 'VE-NL-CMB,VE-TE-CMB' FROM BanHang.KhuyenMai WHERE MaKhuyenMai='SALE30';
GO

-- ============================================================================
-- PHẦN 4: PHIÊN THU NGÂN
-- ============================================================================
DECLARE @NV2 INT = (SELECT IdDoiTac FROM DoiTac.NhanVien WHERE MaNhanVien='NV002');
DECLARE @KhoBanCong INT = (SELECT Id FROM Kho.KhoHang WHERE MaKho='KHO_TONG');

INSERT INTO BanHang.PhienThuNgan (IdNhanVien, IdMayBan, ThoiGianMo, ThoiGianDong, TienDauCa, TienCuoiCa, TongThuTrongCa, TrangThai, IdKhoBan) VALUES
(@NV2, 'POS_CONG01', '2024-02-10 07:00', '2024-02-10 15:00', 500000, 3200000, 2700000, 'DaDong', @KhoBanCong),
(@NV2, 'POS_CONG01', '2024-03-15 07:00', '2024-03-15 15:00', 500000, 2100000, 1600000, 'DaDong', @KhoBanCong),
(@NV2, 'POS_CONG01', '2026-05-01 07:00', NULL,               500000, NULL,    NULL,    'DangMo', @KhoBanCong);
GO

-- ============================================================================
-- PHẦN 5: ĐƠN HÀNG POS (Vé, Đồ ăn, Xuất kho)
-- ============================================================================
DECLARE @NV2 INT = (SELECT TOP 1 IdDoiTac FROM DoiTac.NhanVien WHERE MaNhanVien='NV002');
DECLARE @NV3 INT = (SELECT TOP 1 IdDoiTac FROM DoiTac.NhanVien WHERE MaNhanVien='NV003');
DECLARE @KhoNH INT = (SELECT Id FROM Kho.KhoHang WHERE MaKho='KHO_NH_PALACE');
DECLARE @KhoAV INT = (SELECT Id FROM Kho.KhoHang WHERE MaKho='KHO_ANVAT_BIEN');
DECLARE @KH1 INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00001');
DECLARE @KH2 INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00002');
DECLARE @Phien1 INT = (SELECT TOP 1 Id FROM BanHang.PhienThuNgan WHERE TrangThai='DaDong' ORDER BY Id ASC);

-- Đơn POS 1: Mua vé & đồ ăn (Dùng KM TET2024)
INSERT INTO BanHang.DonHang (MaDonHang, IdKhachHang, IdNhanVien, TongTienHang, TongGiamGia, TienThuKM, TongThanhToan, TrangThai, NgayTao, IdPhienThuNgan)
VALUES ('DH-240212-001', @KH1, @NV2, 400000, 50000, 0, 350000, 'DaThanhToan', '2024-02-12 09:15:00', @Phien1);
DECLARE @DH1 INT = SCOPE_IDENTITY();

INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe) VALUES
(@DH1, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='VE-NL-001'), 2, 150000),
(@DH1, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='VE-TE-001'), 1, 100000);

INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao, NgayChungTu, TrangThai)
VALUES ('PT-240212-001', 'THU_THANHTOAN', @KH1, @DH1, 350000, 'TienMat', @NV2, '2024-02-12 09:16:00', 'DaDuyet');

INSERT INTO BanHang.KhuyenMai_LichSu (IdKhuyenMai, IdDonHang, SoTienGiam, TrangThai)
VALUES ((SELECT Id FROM BanHang.KhuyenMai WHERE MaKhuyenMai='TET2024'), @DH1, 50000, 'DaSuDung');

INSERT INTO BanHang.VeDienTu (IdChiTietDonHang, MaVe, TrangThai, NgayHetHan)
SELECT Id, 'V-' + CAST(Id AS VARCHAR) + '-1', 'DaSuDung', '2024-02-12 23:59:59' FROM BanHang.ChiTietDonHang WHERE IdDonHang=@DH1 AND SoLuong=2;
INSERT INTO BanHang.VeDienTu (IdChiTietDonHang, MaVe, TrangThai, NgayHetHan)
SELECT Id, 'V-' + CAST(Id AS VARCHAR) + '-2', 'DaSuDung', '2024-02-12 23:59:59' FROM BanHang.ChiTietDonHang WHERE IdDonHang=@DH1 AND SoLuong=2;

-- Đơn POS 2: Bán đồ ăn NHÀ HÀNG -> Xuất kho BOM
INSERT INTO BanHang.DonHang (MaDonHang, IdKhachHang, IdNhanVien, TongTienHang, TongGiamGia, TienThuKM, TongThanhToan, TrangThai, NgayTao)
VALUES ('DH-240315-002', @KH2, @NV3, 135000, 0, 0, 135000, 'DaThanhToan', '2024-03-15 12:30:00');
DECLARE @DH2 INT = SCOPE_IDENTITY();

INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe) VALUES
(@DH2, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='DA-PHO-01'), 2, 55000),
(@DH2, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='DU-CCC-001'), 2, 12500);

-- Xuất kho trừ nước ngọt
INSERT INTO Kho.ChungTu (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, NgayChungTu, LyDo, TrangThai, IdNguoiTao, NgayTao)
VALUES ('XK-240315-001', 'XUAT_BAN', @KH2, @DH2, '2024-03-15 12:31:00', N'Xuất bán', 'DaDuyet', @NV3, '2024-03-15 12:31:00');
DECLARE @XK1 INT = SCOPE_IDENTITY();
INSERT INTO Kho.ChiTietChungTu (IdChungTu, IdSanPham, IdKhoXuat, IdKhoNhap, SoLuong, DonGia)
VALUES (@XK1, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='DU-CCC-001'), @KhoAV, (SELECT Id FROM Kho.KhoHang WHERE MaKho='KHO_KHACH'), 2, 12500);

-- Xuất kho BOM cho phở (Trừ thịt, phở)
INSERT INTO Kho.ChungTu (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, NgayChungTu, LyDo, TrangThai, IdNguoiTao, NgayTao)
VALUES ('XK-240315-002', 'XUAT_SANXUAT', NULL, @DH2, '2024-03-15 12:35:00', N'Xuất chế biến', 'DaDuyet', @NV3, '2024-03-15 12:35:00');
DECLARE @XK2 INT = SCOPE_IDENTITY();
INSERT INTO Kho.ChiTietChungTu (IdChungTu, IdSanPham, IdKhoXuat, IdKhoNhap, SoLuong, DonGia)
VALUES (@XK2, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='NL-THIT-01'), @KhoNH, (SELECT Id FROM Kho.KhoHang WHERE MaKho='KHO_SANXUAT'), 0.4, 280000);
GO

-- ============================================================================
-- PHẦN 6: LƯU TRÚ KHÁCH SẠN (Booking + Check-in + Dịch vụ + Checkout)
-- Khách: KH3 (Kim Cương). Phòng: P-101 (Deluxe). 
-- Đặt 1 đêm từ 25/04/2026 -> 26/04/2026
-- ============================================================================
DECLARE @KH3 INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00003');
DECLARE @NV3 INT = (SELECT TOP 1 IdDoiTac FROM DoiTac.NhanVien WHERE MaNhanVien='NV003'); -- Lễ tân

-- 6.1 Tạo Phiếu Đặt Phòng
INSERT INTO BanHang.PhieuDatPhong (MaPhieu, IdKhachHang, IdNhanVien, NgayTao, ThoiGianDat, ThoiGianTra, TienPhong, TienPhuThu, TienCoc, TrangThai, LoaiDatPhong, KenhDat)
VALUES ('BK-260425-001', @KH3, @NV3, '2026-04-20 10:00:00', '2026-04-25 14:00:00', '2026-04-26 12:00:00', 1200000, 0, 600000, 'DaTraPhong', 'TheoNgay', 'TrucTiep');
DECLARE @IdBK1 INT = SCOPE_IDENTITY();

-- Thu cọc
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao, NgayChungTu, TrangThai)
VALUES ('PT-260420-001', 'THU_TIEN_COC', @KH3, @IdBK1, 600000, 'ChuyenKhoan', @NV3, '2026-04-20 10:05:00', 'DaDuyet');

-- Gán phòng
DECLARE @PhongDLX INT = (SELECT TOP 1 Id FROM DanhMuc.Phong WHERE MaPhong='P-101');
INSERT INTO BanHang.ChiTietDatPhong (IdPhieuDat, IdPhong, ThoiGianDat, ThoiGianTra, GiaPhong, TinhTrangCheckIn, NguoiOThucTe)
VALUES (@IdBK1, @PhongDLX, '2026-04-25 14:00:00', '2026-04-26 12:00:00', 1200000, 'DaCheckOut', N'Lê Hoàng Minh');
DECLARE @IdCTDP1 INT = SCOPE_IDENTITY();

-- 6.2 Sử dụng Dịch Vụ / Minibar trong lúc lưu trú (Ngày 25/04/2026 tối)
INSERT INTO BanHang.DichVuDatPhong (IdChiTietDat, IdSanPham, SoLuong, DonGia, ThanhTien, GhiChu)
VALUES (@IdCTDP1, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='DU-NS-001'), 2, 10000, 20000, N'Minibar');
INSERT INTO BanHang.DichVuDatPhong (IdChiTietDat, IdSanPham, SoLuong, DonGia, ThanhTien, GhiChu)
VALUES (@IdCTDP1, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham='DA-PHO-01'), 1, 65000, 65000, N'Room Service');

-- 6.3 Checkout & Thanh toán (26/04/2026 11:30)
-- Tổng = 1.200.000 (phòng) + 85.000 (dịch vụ) = 1.285.000. Đã cọc 600.000 => Còn 685.000
-- Áp dụng KM 'VIP20' (20%) -> Giảm 257.000 -> Còn phải thu 428.000
INSERT INTO BanHang.DonHang (MaDonHang, IdKhachHang, IdNhanVien, TongTienHang, TongGiamGia, TongThanhToan, TrangThai, NgayTao, IdPhieuDatPhong)
VALUES ('DH-260426-BK1', @KH3, @NV3, 1285000, 257000, 428000, 'DaThanhToan', '2026-04-26 11:30:00', @IdBK1);
DECLARE @DH_BK1 INT = SCOPE_IDENTITY();

-- Thanh toán Checkout
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao, NgayChungTu, TrangThai)
VALUES ('PT-260426-BK1', 'THU_THANHTOAN', @KH3, @DH_BK1, 428000, 'TheNganHang', @NV3, '2026-04-26 11:30:00', 'DaDuyet');

-- Áp KM
INSERT INTO BanHang.KhuyenMai_LichSu (IdKhuyenMai, IdDonHang, SoTienGiam, TrangThai)
VALUES ((SELECT Id FROM BanHang.KhuyenMai WHERE MaKhuyenMai='VIP20'), @DH_BK1, 257000, 'DaSuDung');

GO

PRINT N'✅ SEED DỮ LIỆU GIAO DỊCH HOÀN TẤT VÀ ĐÃ ĐƯỢC GỘP CHUNG VÀO 1 FILE 03_SeedData_Transactions.sql';
GO
