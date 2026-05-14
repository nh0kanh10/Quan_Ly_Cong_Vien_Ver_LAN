

USE Database_DaiNamv2;
GO

    
-- KH1: Khách Bạc - thường
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, Cccd, DiaChi, LoaiDoiTac)
VALUES (N'Nguyễn Văn An', '0901234567', 'an.nguyen@gmail.com', '079090001234', N'123 Nguyễn Huệ, Q1, TP.HCM', 'CaNhan');
INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
VALUES (SCOPE_IDENTITY(), 'KH00001', 'CaNhan', 'BacSilver');

-- KH2: Khách Vàng - chi tiêu nhiều
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, Cccd, DiaChi, LoaiDoiTac)
VALUES (N'Trần Thị Bích Ngọc', '0912345678', 'ngoc.tran@yahoo.com', '079080005678', N'45 Lê Lợi, Q1, TP.HCM', 'CaNhan');
INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
VALUES (SCOPE_IDENTITY(), 'KH00002', 'CaNhan', 'VangGold');

-- KH3: Khách Kim Cương - VIP
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, Cccd, DiaChi, LoaiDoiTac)
VALUES (N'Lê Hoàng Minh', '0923456789', 'minh.le@outlook.com', '079070009012', N'789 Điện Biên Phủ, Q3, TP.HCM', 'CaNhan');
INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
VALUES (SCOPE_IDENTITY(), 'KH00003', 'CaNhan', 'KimCuong');
-- KH4: Khách Thường
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac)
VALUES (N'Phạm Minh Tuấn', '0934567890', 'tuan.pham@gmail.com', N'12 Trần Hưng Đạo, Thủ Dầu Một, Bình Dương', 'CaNhan');
INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
VALUES (SCOPE_IDENTITY(), 'KH00004', 'CaNhan', 'Thuong');

-- KH5: Khách Bạc
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, Cccd, DiaChi, LoaiDoiTac)
VALUES (N'Võ Thị Mai Hương', '0945678901', 'huong.vo@gmail.com', '079095003456', N'67 Nguyễn Văn Cừ, Q5, TP.HCM', 'CaNhan');
INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
VALUES (SCOPE_IDENTITY(), 'KH00005', 'CaNhan', 'BacSilver');

-- KH6: Khách nước ngoài
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac)
VALUES (N'David Johnson', '0956789012', 'david.j@gmail.com', N'District 2, Thu Duc City, HCMC', 'CaNhan');
INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
VALUES (SCOPE_IDENTITY(), 'KH00006', 'CaNhan', 'VangGold');

-- KH7: Khách sinh viên
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac)
VALUES (N'Huỳnh Thanh Tùng', '0967890123', NULL, N'234 Phạm Ngũ Lão, Q1, TP.HCM', 'CaNhan');
INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
VALUES (SCOPE_IDENTITY(), 'KH00007', 'HocSinhSinhVien', 'Thuong');

-- KH8: Khách Bạc
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, Cccd, DiaChi, LoaiDoiTac)
VALUES (N'Đặng Thị Kim Anh', '0978901234', 'kimanh.dang@gmail.com', '079085007890', N'89 Cách Mạng Tháng 8, Q3, TP.HCM', 'CaNhan');
INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
VALUES (SCOPE_IDENTITY(), 'KH00008', 'CaNhan', 'BacSilver');

-- KH9: Khách Vàng
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac)
VALUES (N'Bùi Quốc Huy', '0989012345', 'huy.bui@gmail.com', N'56 Hai Bà Trưng, Q1, TP.HCM', 'CaNhan');
INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
VALUES (SCOPE_IDENTITY(), 'KH00009', 'CaNhan', 'VangGold');

-- KH10: Khách mới
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac)
VALUES (N'Lý Thị Thanh Thảo', '0990123456', 'thao.ly@hotmail.com', N'Tân Uyên, Bình Dương', 'CaNhan');
INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
VALUES (SCOPE_IDENTITY(), 'KH00010', 'CaNhan', 'Thuong');

GO


-- PHẦN 2: KHÁCH HÀNG TỔ CHỨC / ĐOÀN (3 khách B2B)
-- LoaiKhach hợp lệ: CaNhan, Doan, DoanhNghiep, HocSinhSinhVien, NoiBo


-- KH11: Công ty lữ hành → Doan
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac)
VALUES (N'Công ty TNHH Du lịch Saigon Tourist', '0281234567', 'booking@saigontourist.vn', N'45 Lê Thánh Tôn, Q1, TP.HCM', 'CongTyLuHanh');
INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
VALUES (SCOPE_IDENTITY(), 'KH00011', 'Doan', 'KimCuongDiamond');

-- KH12: Trường học → Doan
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac)
VALUES (N'Trường THPT Nguyễn Thị Minh Khai', '0282345678', 'vp@ntmk.edu.vn', N'275 Điện Biên Phủ, Q3, TP.HCM', 'ToChuc');
INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
VALUES (SCOPE_IDENTITY(), 'KH00012', 'Doan', 'VangGold');

-- KH13: Công ty sự kiện → DoanhNghiep
INSERT INTO DoiTac.ThongTin (HoTen, DienThoai, Email, DiaChi, LoaiDoiTac)
VALUES (N'Công ty CP Sự kiện Sunrise Event', '0283456789', 'info@sunrise-event.vn', N'100 Nguyễn Du, Q1, TP.HCM', 'ToChuc');
INSERT INTO DoiTac.KhachHang (IdDoiTac, MaKhachHang, LoaiKhach, HangThanhVien)
VALUES (SCOPE_IDENTITY(), 'KH00013', 'DoanhNghiep', 'BacSilver');

GO


-- PHẦN 3: THẺ RFID + VÍ ĐIỆN TỬ (Cấp cho 5 khách đầu tiên)
-- ViDienTu: MaVi (UNIQUE NOT NULL), ConHoatDong (BIT)
-- TheRFID: TrangThai = 'ChuaKichHoat' | 'DangDung' | 'DaKhoa' | 'DaTra'
-- SoCaiVi: LoaiPhep (Cong/Tru), SoTien > 0

DECLARE @Id1 INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00001');
DECLARE @Id2 INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00002');
DECLARE @Id3 INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00003');
DECLARE @Id4 INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00004');
DECLARE @Id5 INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00005');

-- Ví điện tử (MaVi là UNIQUE NOT NULL)
INSERT INTO TaiChinh.ViDienTu (IdKhachHang, MaVi, ConHoatDong) VALUES
(@Id1, 'VI-KH00001', 1),
(@Id2, 'VI-KH00002', 1),
(@Id3, 'VI-KH00003', 1),
(@Id4, 'VI-KH00004', 1),
(@Id5, 'VI-KH00005', 1);

-- Thẻ RFID (link ViDienTu)
DECLARE @IdVi1 INT = (SELECT Id FROM TaiChinh.ViDienTu WHERE IdKhachHang = @Id1);
DECLARE @IdVi2 INT = (SELECT Id FROM TaiChinh.ViDienTu WHERE IdKhachHang = @Id2);
DECLARE @IdVi3 INT = (SELECT Id FROM TaiChinh.ViDienTu WHERE IdKhachHang = @Id3);
DECLARE @IdVi4 INT = (SELECT Id FROM TaiChinh.ViDienTu WHERE IdKhachHang = @Id4);
DECLARE @IdVi5 INT = (SELECT Id FROM TaiChinh.ViDienTu WHERE IdKhachHang = @Id5);

INSERT INTO TaiChinh.TheRFID (MaThe, IdKhachHang, IdViDienTu, TrangThai) VALUES
('RFID-A0001', @Id1, @IdVi1, 'DangDung'),
('RFID-A0002', @Id2, @IdVi2, 'DangDung'),
('RFID-A0003', @Id3, @IdVi3, 'DangDung'),
('RFID-A0004', @Id4, @IdVi4, 'DangDung'),
('RFID-A0005', @Id5, @IdVi5, 'DaKhoa');

-- Sổ cái ví: LoaiPhep = 'Cong' hoặc 'Tru', SoTien > 0 (luôn dương)
INSERT INTO TaiChinh.SoCaiVi (IdVi, LoaiPhep, SoTien, MoTa, NguoiTao) VALUES
(@IdVi1, 'Cong', 500000,   N'Nạp tiền lần đầu', 1),
(@IdVi2, 'Cong', 2000000,  N'Nạp tiền VIP', 1),
(@IdVi2, 'Tru',  300000,   N'Thanh toán Buffet trưa', 1),
(@IdVi3, 'Cong', 5000000,  N'Nạp tiền Kim Cương', 1),
(@IdVi3, 'Tru',  1200000,  N'Thanh toán phòng Deluxe', 1),
(@IdVi4, 'Cong', 200000,   N'Nạp tiền mới', 1),
(@IdVi5, 'Cong', 300000,   N'Nạp tiền lần đầu', 1);

GO


-- PHẦN 4: LỊCH SỬ ĐIỂM (Tích lũy cho 5 khách đầu)
-- LichSuDiem: LoaiGiaoDich, SoDiem, SoDuSauGD, MoTa (không phải LyDo)

DECLARE @Id1p INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00001');
DECLARE @Id2p INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00002');
DECLARE @Id3p INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00003');
DECLARE @Id4p INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00004');
DECLARE @Id5p INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00005');

INSERT INTO TaiChinh.LichSuDiem (IdKhachHang, LoaiGiaoDich, SoDiem, SoDuSauGD, MoTa) VALUES
(@Id1p, 'CongDiem', 150,  150, N'Mua vé combo NL'),
(@Id1p, 'CongDiem', 55,   205, N'Ăn phở bò'),
(@Id2p, 'CongDiem', 500,  500, N'Mua vé + buffet'),
(@Id2p, 'CongDiem', 300,  800, N'Nạp tiền ví 2 triệu'),
(@Id2p, 'TruDiem',  -100, 700, N'Đổi voucher nước'),
(@Id3p, 'CongDiem', 2000, 2000, N'Đặt phòng Suite 2 đêm'),
(@Id3p, 'CongDiem', 500,  2500, N'Buffet hải sản tối'),
(@Id4p, 'CongDiem', 50,   50,  N'Mua vé lẻ NL'),
(@Id5p, 'CongDiem', 80,   80,  N'Mua vé + thuê tủ');

GO


-- PHẦN 5: ĐƠN HÀNG MẪU (Tạo lịch sử chi tiêu)
-- DonHang: MaDonHang, IdKhachHang, IdNhanVien, TongTienHang, TongGiamGia, 
--          TienThueVAT, TongThanhToan, TrangThai, NgayTao (auto)
-- Cần IdNhanVien (NOT NULL) → lấy Id=1 (Admin)

DECLARE @Id1d INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00001');
DECLARE @Id2d INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00002');
DECLARE @Id3d INT = (SELECT TOP 1 kh.IdDoiTac FROM DoiTac.KhachHang kh WHERE kh.MaKhachHang = 'KH00003');
DECLARE @IdNV INT = (SELECT TOP 1 IdDoiTac FROM DoiTac.NhanVien);

-- Đơn hàng KH1
-- Đơn hàng KH1
INSERT INTO BanHang.DonHang (MaDonHang, IdKhachHang, IdNhanVien, TongTienHang, TongGiamGia, TongThanhToan, TrangThai)
VALUES ('DH-260401-001', @Id1d, @IdNV, 400000, 0, 400000, 'DaThanhToan');
DECLARE @IdDH1 INT = SCOPE_IDENTITY();
INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue) VALUES
(@IdDH1, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'VE-NL-001'), 2, 150000, 0),
(@IdDH1, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'TC-TL-001'), 1, 100000, 0);
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao)
VALUES ('PT-260401-001', 'THU_THANHTOAN', @Id1d, @IdDH1, 400000, 'TienMat', @IdNV);

INSERT INTO BanHang.DonHang (MaDonHang, IdKhachHang, IdNhanVien, TongTienHang, TongGiamGia, TongThanhToan, TrangThai)
VALUES ('DH-260405-002', @Id1d, @IdNV, 110000, 0, 110000, 'DaThanhToan');
DECLARE @IdDH2 INT = SCOPE_IDENTITY();
INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue) VALUES
(@IdDH2, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'DA-PHO-01'), 2, 55000, 0);
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao)
VALUES ('PT-260405-002', 'THU_THANHTOAN', @Id1d, @IdDH2, 110000, 'ViRFID', @IdNV);

-- Đơn hàng KH2
INSERT INTO BanHang.DonHang (MaDonHang, IdKhachHang, IdNhanVien, TongTienHang, TongGiamGia, TongThanhToan, TrangThai)
VALUES ('DH-260402-003', @Id2d, @IdNV, 750000, 50000, 700000, 'DaThanhToan');
DECLARE @IdDH3 INT = SCOPE_IDENTITY();
INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue) VALUES
(@IdDH3, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'VE-NL-CMB'), 3, 250000, 0);
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao)
VALUES ('PT-260402-003', 'THU_THANHTOAN', @Id2d, @IdDH3, 700000, 'ChuyenKhoan', @IdNV);

INSERT INTO BanHang.DonHang (MaDonHang, IdKhachHang, IdNhanVien, TongTienHang, TongGiamGia, TongThanhToan, TrangThai)
VALUES ('DH-260408-004', @Id2d, @IdNV, 598000, 0, 598000, 'DaThanhToan');
DECLARE @IdDH4 INT = SCOPE_IDENTITY();
INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue) VALUES
(@IdDH4, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'VE-NL-001'), 2, 150000, 0),
(@IdDH4, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'DA-PHO-01'), 4, 55000, 0),
(@IdDH4, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'TD-TU-M01'), 1, 30000, 0),
(@IdDH4, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'TD-PHAO-01'), 1, 40000, 0);
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao)
VALUES ('PT-260408-004', 'THU_THANHTOAN', @Id2d, @IdDH4, 598000, 'TienMat', @IdNV);

-- Đơn hàng KH3
INSERT INTO BanHang.DonHang (MaDonHang, IdKhachHang, IdNhanVien, TongTienHang, TongGiamGia, TongThanhToan, TrangThai)
VALUES ('DH-260403-005', @Id3d, @IdNV, 3500000, 0, 3500000, 'DaThanhToan');
DECLARE @IdDH5 INT = SCOPE_IDENTITY();
INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue) VALUES
(@IdDH5, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'LT-SUI-01'), 1, 3500000, 0);
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao)
VALUES ('PT-260403-005', 'THU_THANHTOAN', @Id3d, @IdDH5, 3500000, 'TheNganHang', @IdNV);

INSERT INTO BanHang.DonHang (MaDonHang, IdKhachHang, IdNhanVien, TongTienHang, TongGiamGia, TongThanhToan, TrangThai)
VALUES ('DH-260410-006', @Id3d, @IdNV, 998000, 0, 998000, 'DaThanhToan');
DECLARE @IdDH6 INT = SCOPE_IDENTITY();
INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue) VALUES
(@IdDH6, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'DA-BF-02'), 2, 499000, 0);
INSERT INTO TaiChinh.ChungTuTC (MaChungTu, LoaiChungTu, IdDoiTac, IdDonHang, SoTien, PhuongThuc, IdNguoiTao)
VALUES ('PT-260410-006', 'THU_THANHTOAN', @Id3d, @IdDH6, 998000, 'TienMat', @IdNV);

-- Đơn hàng bị hủy
INSERT INTO BanHang.DonHang (MaDonHang, IdKhachHang, IdNhanVien, TongTienHang, TongGiamGia, TongThanhToan, TrangThai)
VALUES ('DH-260411-007', @Id1d, @IdNV, 250000, 0, 250000, 'DaHuy');
DECLARE @IdDH7 INT = SCOPE_IDENTITY();
INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue) VALUES
(@IdDH7, (SELECT Id FROM DanhMuc.SanPham WHERE MaSanPham = 'VE-NL-CMB'), 1, 250000, 0);

GO



SELECT N' THỐNG KÊ KHÁCH HÀNG ' AS [Info];

SELECT 
    kh.HangThanhVien, 
    COUNT(*) AS SoLuong
FROM DoiTac.KhachHang kh
JOIN DoiTac.ThongTin tt ON kh.IdDoiTac = tt.Id
WHERE tt.DaXoa = 0
GROUP BY kh.HangThanhVien;

SELECT N' TOP 5 CHI TIÊU ' AS [Info];

SELECT TOP 5
    kh.MaKhachHang,
    tt.HoTen,
    kh.HangThanhVien,
    ISNULL(SUM(dh.TongThanhToan), 0) AS TongChiTieu
FROM DoiTac.KhachHang kh
JOIN DoiTac.ThongTin tt ON kh.IdDoiTac = tt.Id
LEFT JOIN BanHang.DonHang dh ON dh.IdKhachHang = kh.IdDoiTac AND dh.TrangThai != 'DaHuy'
WHERE tt.DaXoa = 0
GROUP BY kh.MaKhachHang, tt.HoTen, kh.HangThanhVien
ORDER BY TongChiTieu DESC;

GO
