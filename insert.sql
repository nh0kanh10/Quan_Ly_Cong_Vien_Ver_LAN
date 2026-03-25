--insert dữ liệu khu vực
INSERT INTO KhuVuc
(MaCode, TenKhuVuc, MoTa, TrangThai, NgayTao)
VALUES
('KV001', N'Khu trò chơi cảm giác mạnh', N'Tập trung các trò chơi tốc độ cao và mạo hiểm', N'Mở cửa', GETDATE()),
('KV002', N'Khu vui chơi trẻ em', N'Khu vực dành cho trẻ nhỏ với các trò chơi an toàn', N'Mở cửa', GETDATE()),
('KV003', N'Khu phiêu lưu khám phá', N'Các trò chơi thử thách và phiêu lưu', N'Mở cửa', GETDATE()),
('KV004', N'Công viên nước', N'Các trò chơi liên quan đến nước và hồ bơi', N'Mở cửa', GETDATE()),
('KV005', N'Khu trò chơi gia đình', N'Trò chơi dành cho mọi lứa tuổi', N'Mở cửa', GETDATE()),
('KV006', N'Khu trò chơi thể thao', N'Các trò chơi vận động và thể thao', N'Mở cửa', GETDATE()),
('KV007', N'Khu tham quan', N'Khu vực ngắm cảnh và trải nghiệm nhẹ nhàng', N'Mở cửa', GETDATE()),
('KV008', N'Khu trò chơi trong nhà', N'Trò chơi giải trí trong nhà', N'Mở cửa', GETDATE()),
('KV009', N'Khu ẩm thực', N'Nhà hàng và quầy thức ăn', N'Mở cửa', GETDATE()),
('KV010', N'Khu sự kiện', N'Nơi tổ chức sự kiện và biểu diễn', N'Mở cửa', GETDATE());

INSERT INTO TroChoi
(MaCode, TenTroChoi, MaKhuVuc, LoaiTroChoi, SucChua, TuoiToiThieu, ChieuCaoToiThieu, ThoiGianLuot, MoTa, TrangThai, NgayTao)
VALUES
('G001', N'Tàu lượn siêu tốc', 1, N'Cảm giác mạnh', 20, 12, 140, 5, N'Trò chơi tốc độ cao', N'Đang hoạt động', GETDATE()),
('G002', N'Đu quay dây văng', 1, N'Cảm giác mạnh', 24, 10, 130, 6, N'Đu quay trên không', N'Đang hoạt động', GETDATE()),
('G003', N'Thuyền hải tặc', 1, N'Cảm giác mạnh', 18, 10, 130, 5, N'Thuyền lắc mạnh', N'Đang hoạt động', GETDATE()),

('G004', N'Nhà banh trẻ em', 2, N'Trẻ em', 30, 3, 80, 15, N'Khu bóng nhựa cho trẻ em', N'Đang hoạt động', GETDATE()),
('G005', N'Đu quay ngựa gỗ', 2, N'Trẻ em', 15, 3, 80, 5, N'Trò chơi cổ điển cho trẻ', N'Đang hoạt động', GETDATE()),
('G006', N'Tàu lượn mini', 2, N'Trẻ em', 12, 4, 90, 4, N'Tàu lượn nhỏ cho trẻ em', N'Đang hoạt động', GETDATE()),

('G007', N'Nhà ma', 3, N'Phiêu lưu', 12, 10, 120, 8, N'Khám phá ngôi nhà ma', N'Đang hoạt động', GETDATE()),
('G008', N'Mê cung gương', 3, N'Phiêu lưu', 10, 7, 100, 6, N'Mê cung kính phản chiếu', N'Đang hoạt động', GETDATE()),
('G009', N'Đu dây zipline', 3, N'Phiêu lưu', 6, 12, 140, 4, N'Đu dây qua khu rừng', N'Đang hoạt động', GETDATE()),

('G010', N'Máng trượt nước', 4, N'Nước', 10, 8, 120, 5, N'Trượt nước tốc độ cao', N'Đang hoạt động', GETDATE()),
('G011', N'Hồ bơi tạo sóng', 4, N'Nước', 60, 5, 100, 20, N'Hồ bơi tạo sóng nhân tạo', N'Đang hoạt động', GETDATE()),
('G012', N'Trượt ống xoắn', 4, N'Nước', 12, 9, 120, 4, N'Ống trượt nước xoắn', N'Đang hoạt động', GETDATE()),

('G013', N'Xe điện đụng', 5, N'Gia đình', 15, 6, 100, 7, N'Xe điện va chạm vui nhộn', N'Đang hoạt động', GETDATE()),
('G014', N'Xe jeep safari', 5, N'Gia đình', 10, 6, 100, 8, N'Khám phá khu rừng giả lập', N'Đang hoạt động', GETDATE()),
('G015', N'Bóng đụng sumo', 5, N'Gia đình', 8, 8, 110, 6, N'Chơi bóng sumo vui nhộn', N'Đang hoạt động', GETDATE()),

('G016', N'Leo núi nhân tạo', 6, N'Thể thao', 8, 10, 120, 8, N'Leo núi trong nhà', N'Đang hoạt động', GETDATE()),
('G017', N'Đua xe F1 mini', 6, N'Thể thao', 12, 8, 110, 7, N'Đua xe tốc độ', N'Đang hoạt động', GETDATE()),
('G018', N'Bắn cung', 6, N'Thể thao', 6, 10, 120, 10, N'Tập bắn cung', N'Đang hoạt động', GETDATE()),

('G019', N'Vòng quay khổng lồ', 7, N'Tham quan', 40, 5, 100, 10, N'Ngắm toàn cảnh công viên', N'Đang hoạt động', GETDATE()),
('G020', N'Xe lửa tham quan', 7, N'Tham quan', 30, 3, 80, 12, N'Tham quan công viên', N'Đang hoạt động', GETDATE()),

('G021', N'Game bắn súng', 8, N'Trong nhà', 10, 7, 100, 6, N'Trò chơi bắn súng điện tử', N'Đang hoạt động', GETDATE()),
('G022', N'Game đua xe', 8, N'Trong nhà', 8, 7, 100, 5, N'Mô phỏng đua xe', N'Đang hoạt động', GETDATE()),
('G023', N'Máy gắp thú', 8, N'Trong nhà', 5, 5, 80, 3, N'Máy gắp thú bông', N'Đang hoạt động', GETDATE());

-- Insert dữ liệu loại vé
INSERT INTO LoaiVe
(MaCode, TenLoaiVe, GiaVe, DoiTuong, LaCombo, TrangThai, NgayTao)
VALUES
('T001', N'Vé trẻ em', 150000, N'Trẻ em', 0, N'Hoạt động', GETDATE()),
('T002', N'Vé người lớn', 250000, N'Người lớn', 0, N'Hoạt động', GETDATE()),
('T003', N'Vé sinh viên', 200000, N'Sinh viên', 0, N'Hoạt động', GETDATE()),
('T004', N'Vé người cao tuổi', 120000, N'Người cao tuổi', 0, N'Hoạt động', GETDATE()),
('T005', N'Combo gia đình', 700000, N'Tất cả', 1, N'Hoạt động', GETDATE()),
('T006', N'Combo khám phá', 500000, N'Tất cả', 1, N'Hoạt động', GETDATE()),
('T007', N'Ve nuoc uong', 50000, N'Tất cả', 0, N'Hoạt động', GETDATE());

INSERT INTO ChiTietCombo (MaLoaiVeCha, MaLoaiVeCon, SoLuotChoPhep, NgayTao)
VALUES 
(5, 1, 2, GETDATE()), 
(5, 2, 2, GETDATE()), 
(6, 1, 9999, GETDATE()), 
(6, 2, 9999, GETDATE()); 

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, TrangThai, NgayTao)
VALUES ('admin', '123456', N'Quản trị viên', 'Admin', 1, GETDATE());

SELECT MAX(MaTroChoi) FROM TroChoi

INSERT INTO DanhMucDichVu (TenDanhMuc, KieuLogic, MoTa, Icon)
VALUES 
(N'Ve vao cong', 'SALE', N'Ban ve cong vien', 'ticket'),
(N'Tro choi tinh gio', 'TIME', N'Dich vu tinh theo gio', 'clock'),
(N'Thue do', 'SALE', N'Thue vat dung', 'bag'),
(N'Am thuc', 'SALE', N'Do an nuoc uong', 'food');

INSERT INTO DichVu
(MaCode, TenDichVu, MaDanhMuc, MaKhuVuc, GiaBan, SoLuongTon, DonViTinh, TrangThai, NgayTao)
VALUES
('DV001', N'Ve tre em', 1, 7, 150000, 1000, N'Ve', N'Hoạt động', GETDATE()),
('DV002', N'Ve nguoi lon', 1, 7, 250000, 1000, N'Ve', N'Hoạt động', GETDATE()),
('DV003', N'Choi VR 30 phut', 2, 8, 60000, 999, N'Lan', N'Hoạt động', GETDATE()),
('DV004', N'Thue tu do', 3, 9, 30000, 50, N'Cai', N'Hoạt động', GETDATE()),
('DV005', N'Thue phao boi', 3, 4, 20000, 100, N'Cai', N'Hoạt động', GETDATE()),
('DV006', N'Combo nuoc + snack', 4, 9, 40000, 200, N'Phan', N'Hoạt động', GETDATE()),
('DV007', N'Ve gia dinh', 1, 7, 500000, 200, N'Ve', N'Hoạt động', GETDATE()),
('DV008', N'Ve VIP', 1, 7, 800000, 100, N'Ve', N'Hoạt động', GETDATE()),
('DV009', N'Choi VR 60 phut', 2, 8, 100000, 999, N'Lan', N'Hoạt động', GETDATE()),
('DV010', N'Thue locker', 3, 9, 15000, 300, N'Cai', N'Hoạt động', GETDATE()),
('DV011', N'Thue khan', 3, 4, 10000, 500, N'Cai', N'Hoạt động', GETDATE()),
('DV012', N'Nuoc suoi', 4, 9, 10000, 1000, N'Chai', N'Hoạt động', GETDATE()),
('DV013', N'Nuoc ngot', 4, 9, 15000, 1000, N'Lon', N'Hoạt động', GETDATE()),
('DV014', N'Combo fastfood', 4, 9, 80000, 300, N'Phan', N'Hoạt động', GETDATE()),
('DV015', N'Ve sinh vien', 1, 7, 120000, 500, N'Ve', N'Hoạt động', GETDATE()),
('DV016', N'Ve doi', 1, 7, 280000, 300, N'Ve', N'Hoạt động', GETDATE()),
('DV017', N'Choi VR 15 phut', 2, 8, 40000, 999, N'Lan', N'Hoạt động', GETDATE()),
('DV018', N'Game ban sung', 2, 8, 50000, 999, N'Lan', N'Hoạt động', GETDATE()),
('DV019', N'Thue xe dien', 3, 5, 70000, 50, N'Gio', N'Hoạt động', GETDATE()),
('DV020', N'Thue ghe ngoi', 3, 5, 20000, 200, N'Cai', N'Hoạt động', GETDATE()),
('DV021', N'Kem oc que', 4, 9, 20000, 500, N'Cay', N'Hoạt động', GETDATE()),
('DV022', N'Kem ly', 4, 9, 30000, 300, N'Ly', N'Hoạt động', GETDATE()),
('DV023', N'Bap rang', 4, 9, 25000, 400, N'Goi', N'Hoạt động', GETDATE()),
('DV024', N'Xuc xich', 4, 9, 20000, 400, N'Cay', N'Hoạt động', GETDATE()),
('DV025', N'Ve tre em VIP', 1, 7, 200000, 200, N'Ve', N'Hoạt động', GETDATE()),
('DV026', N'Ve nguoi lon VIP', 1, 7, 350000, 200, N'Ve', N'Hoạt động', GETDATE()),
('DV027', N'Choi VR cao cap', 2, 8, 150000, 999, N'Lan', N'Hoạt động', GETDATE()),
('DV028', N'Choi game dua xe', 2, 8, 60000, 999, N'Lan', N'Hoạt động', GETDATE()),
('DV029', N'Thue phao lon', 3, 4, 50000, 100, N'Cai', N'Hoạt động', GETDATE()),
('DV030', N'Thue ao phao', 3, 4, 25000, 200, N'Cai', N'Hoạt động', GETDATE()),
('DV031', N'Nuoc ep', 4, 9, 30000, 300, N'Ly', N'Hoạt động', GETDATE()),
('DV032', N'Sinh to', 4, 9, 35000, 300, N'Ly', N'Hoạt động', GETDATE()),
('DV033', N'Tra sua', 4, 9, 40000, 300, N'Ly', N'Hoạt động', GETDATE()),
('DV034', N'Cafe', 4, 9, 30000, 300, N'Ly', N'Hoạt động', GETDATE()),
('DV035', N'Combo gia dinh VIP', 1, 7, 900000, 100, N'Ve', N'Hoạt động', GETDATE()),
('DV036', N'Ve tron goi', 1, 7, 600000, 200, N'Ve', N'Hoạt động', GETDATE());

INSERT INTO BangGia
(MaDichVu, LoaiNgay, GiaBan, NgayBatDau, NgayKetThuc, TrangThai, NgayTao)
VALUES
-- Vé trẻ em
(1, N'NgayThuong', 150000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),
(1, N'CuoiTuan', 180000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),

-- Vé người lớn
(2, N'NgayThuong', 250000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),
(2, N'LeTet', 300000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),

-- VR
(3, N'CaoDiem', 70000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),
(3, N'NgayThuong', 60000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),
(3, N'CuoiTuan', 75000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),
(3, N'LeTet', 90000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),

-- DV4: Thuê tủ đồ
(4, N'NgayThuong', 30000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),
(4, N'CuoiTuan', 40000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),

-- DV5: Thuê phao bơi
(5, N'NgayThuong', 20000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),
(5, N'CaoDiem', 30000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),

-- DV6: Combo nước + snack
(6, N'NgayThuong', 40000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),
(6, N'CuoiTuan', 50000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE()),
(6, N'LeTet', 60000, GETDATE(), '2099-12-31', N'Hoạt động', GETDATE());




SELECT * FROM BangGia WHERE MaDichVu = 3