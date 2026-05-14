USE Database_DaiNamv2;
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- Xóa rác cũ nếu có
DELETE FROM BanHang.ChiTietDonHang WHERE IdDonHang IN (1,2,3,4,5,6,7);
DELETE FROM TaiChinh.ChungTuTC WHERE IdDonHang IN (1,2,3,4,5,6,7);

-- Chèn ChiTietDonHang cho DH-260401-001 (Id=1)
INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue) VALUES
(1, (SELECT TOP 1 Id FROM DanhMuc.SanPham WHERE MaSanPham = 'VE-NL-001'), 2, 150000, 0),
(1, (SELECT TOP 1 Id FROM DanhMuc.SanPham WHERE MaSanPham = 'TC-TL-001'), 1, 100000, 0);
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao)
VALUES ('PT-260401-001', 'THU_THANHTOAN', (SELECT TOP 1 IdKhachHang FROM BanHang.DonHang WHERE Id = 1), 1, 400000, 'TienMat', 1);

-- Chèn ChiTietDonHang cho DH-260405-002 (Id=2)
INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue) VALUES
(2, (SELECT TOP 1 Id FROM DanhMuc.SanPham WHERE MaSanPham = 'DA-PHO-01'), 2, 55000, 0);
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao)
VALUES ('PT-260405-002', 'THU_THANHTOAN', (SELECT TOP 1 IdKhachHang FROM BanHang.DonHang WHERE Id = 2), 2, 110000, 'ViRFID', 1);

-- Chèn ChiTietDonHang cho DH-260402-003 (Id=3)
INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue) VALUES
(3, (SELECT TOP 1 Id FROM DanhMuc.SanPham WHERE MaSanPham = 'VE-NL-CMB'), 3, 250000, 0);
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao)
VALUES ('PT-260402-003', 'THU_THANHTOAN', (SELECT TOP 1 IdKhachHang FROM BanHang.DonHang WHERE Id = 3), 3, 700000, 'ChuyenKhoan', 1);

-- Chèn ChiTietDonHang cho DH-260408-004 (Id=4)
INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue) VALUES
(4, (SELECT TOP 1 Id FROM DanhMuc.SanPham WHERE MaSanPham = 'VE-NL-001'), 2, 150000, 0),
(4, (SELECT TOP 1 Id FROM DanhMuc.SanPham WHERE MaSanPham = 'DA-PHO-01'), 4, 55000, 0),
(4, (SELECT TOP 1 Id FROM DanhMuc.SanPham WHERE MaSanPham = 'TD-TU-M01'), 1, 30000, 0),
(4, (SELECT TOP 1 Id FROM DanhMuc.SanPham WHERE MaSanPham = 'TD-PHAO-01'), 1, 40000, 0);
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao)
VALUES ('PT-260408-004', 'THU_THANHTOAN', (SELECT TOP 1 IdKhachHang FROM BanHang.DonHang WHERE Id = 4), 4, 598000, 'TienMat', 1);

-- Chèn ChiTietDonHang cho DH-260403-005 (Id=5)
INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue) VALUES
(5, (SELECT TOP 1 Id FROM DanhMuc.SanPham WHERE MaSanPham = 'LT-SUI-01'), 1, 3500000, 0);
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao)
VALUES ('PT-260403-005', 'THU_THANHTOAN', (SELECT TOP 1 IdKhachHang FROM BanHang.DonHang WHERE Id = 5), 5, 3500000, 'TheNganHang', 1);

-- Chèn ChiTietDonHang cho DH-260410-006 (Id=6)
INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue) VALUES
(6, (SELECT TOP 1 Id FROM DanhMuc.SanPham WHERE MaSanPham = 'DA-BF-02'), 2, 499000, 0);
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao)
VALUES ('PT-260410-006', 'THU_THANHTOAN', (SELECT TOP 1 IdKhachHang FROM BanHang.DonHang WHERE Id = 6), 6, 998000, 'TienMat', 1);
GO
