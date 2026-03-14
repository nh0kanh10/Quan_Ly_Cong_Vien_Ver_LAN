--insert dữ liệu khu vực
INSERT INTO KhuVuc
(MaCode, TenKhuVuc, MoTa, TrangThai, NgayTao)
VALUES
('KV001', N'Khu trò chơi cảm giác mạnh', N'Tập trung các trò chơi tốc độ cao và mạo hiểm', N'Hoạt động', GETDATE()),
('KV002', N'Khu vui chơi trẻ em', N'Khu vực dành cho trẻ nhỏ với các trò chơi an toàn', N'Hoạt động', GETDATE()),
('KV003', N'Khu phiêu lưu khám phá', N'Các trò chơi thử thách và phiêu lưu', N'Hoạt động', GETDATE()),
('KV004', N'Công viên nước', N'Các trò chơi liên quan đến nước và hồ bơi', N'Hoạt động', GETDATE()),
('KV005', N'Khu trò chơi gia đình', N'Trò chơi dành cho mọi lứa tuổi', N'Hoạt động', GETDATE()),
('KV006', N'Khu trò chơi thể thao', N'Các trò chơi vận động và thể thao', N'Hoạt động', GETDATE()),
('KV007', N'Khu tham quan', N'Khu vực ngắm cảnh và trải nghiệm nhẹ nhàng', N'Hoạt động', GETDATE()),
('KV008', N'Khu trò chơi trong nhà', N'Trò chơi giải trí trong nhà', N'Hoạt động', GETDATE()),
('KV009', N'Khu ẩm thực', N'Nhà hàng và quầy thức ăn', N'Hoạt động', GETDATE()),
('KV010', N'Khu sự kiện', N'Nơi tổ chức sự kiện và biểu diễn', N'Hoạt động', GETDATE());

INSERT INTO TroChoi
(MaCode, TenTroChoi, MaKhuVuc, LoaiTroChoi, SucChua, TuoiToiThieu, ChieuCaoToiThieu, ThoiGianLuot, MoTa, TrangThai, NgayTao)
VALUES
('G001', N'Tàu lượn siêu tốc', 1, N'Cảm giác mạnh', 20, 12, 140, 5, N'Trò chơi tốc độ cao', N'Hoạt động', GETDATE()),
('G002', N'Đu quay dây văng', 1, N'Cảm giác mạnh', 24, 10, 130, 6, N'Đu quay trên không', N'Hoạt động', GETDATE()),
('G003', N'Thuyền hải tặc', 1, N'Cảm giác mạnh', 18, 10, 130, 5, N'Thuyền lắc mạnh', N'Hoạt động', GETDATE()),

('G004', N'Nhà banh trẻ em', 2, N'Trẻ em', 30, 3, 80, 15, N'Khu bóng nhựa cho trẻ em', N'Hoạt động', GETDATE()),
('G005', N'Đu quay ngựa gỗ', 2, N'Trẻ em', 15, 3, 80, 5, N'Trò chơi cổ điển cho trẻ', N'Hoạt động', GETDATE()),
('G006', N'Tàu lượn mini', 2, N'Trẻ em', 12, 4, 90, 4, N'Tàu lượn nhỏ cho trẻ em', N'Hoạt động', GETDATE()),

('G007', N'Nhà ma', 3, N'Phiêu lưu', 12, 10, 120, 8, N'Khám phá ngôi nhà ma', N'Hoạt động', GETDATE()),
('G008', N'Mê cung gương', 3, N'Phiêu lưu', 10, 7, 100, 6, N'Mê cung kính phản chiếu', N'Hoạt động', GETDATE()),
('G009', N'Đu dây zipline', 3, N'Phiêu lưu', 6, 12, 140, 4, N'Đu dây qua khu rừng', N'Hoạt động', GETDATE()),

('G010', N'Máng trượt nước', 4, N'Nước', 10, 8, 120, 5, N'Trượt nước tốc độ cao', N'Hoạt động', GETDATE()),
('G011', N'Hồ bơi tạo sóng', 4, N'Nước', 60, 5, 100, 20, N'Hồ bơi tạo sóng nhân tạo', N'Hoạt động', GETDATE()),
('G012', N'Trượt ống xoắn', 4, N'Nước', 12, 9, 120, 4, N'Ống trượt nước xoắn', N'Hoạt động', GETDATE()),

('G013', N'Xe điện đụng', 5, N'Gia đình', 15, 6, 100, 7, N'Xe điện va chạm vui nhộn', N'Hoạt động', GETDATE()),
('G014', N'Xe jeep safari', 5, N'Gia đình', 10, 6, 100, 8, N'Khám phá khu rừng giả lập', N'Hoạt động', GETDATE()),
('G015', N'Bóng đụng sumo', 5, N'Gia đình', 8, 8, 110, 6, N'Chơi bóng sumo vui nhộn', N'Hoạt động', GETDATE()),

('G016', N'Leo núi nhân tạo', 6, N'Thể thao', 8, 10, 120, 8, N'Leo núi trong nhà', N'Hoạt động', GETDATE()),
('G017', N'Đua xe F1 mini', 6, N'Thể thao', 12, 8, 110, 7, N'Đua xe tốc độ', N'Hoạt động', GETDATE()),
('G018', N'Bắn cung', 6, N'Thể thao', 6, 10, 120, 10, N'Tập bắn cung', N'Hoạt động', GETDATE()),

('G019', N'Vòng quay khổng lồ', 7, N'Tham quan', 40, 5, 100, 10, N'Ngắm toàn cảnh công viên', N'Hoạt động', GETDATE()),
('G020', N'Xe lửa tham quan', 7, N'Tham quan', 30, 3, 80, 12, N'Tham quan công viên', N'Hoạt động', GETDATE()),

('G021', N'Game bắn súng', 8, N'Trong nhà', 10, 7, 100, 6, N'Trò chơi bắn súng điện tử', N'Hoạt động', GETDATE()),
('G022', N'Game đua xe', 8, N'Trong nhà', 8, 7, 100, 5, N'Mô phỏng đua xe', N'Hoạt động', GETDATE()),
('G023', N'Máy gắp thú', 8, N'Trong nhà', 5, 5, 80, 3, N'Máy gắp thú bông', N'Hoạt động', GETDATE());

-- Insert dữ liệu loại vé
INSERT INTO LoaiVe
(MaCode, TenLoaiVe, GiaVe, GiaCuoiTuan, DoiTuong, LaCombo, TrangThai, NgayTao)
VALUES
('T001', N'Vé trẻ em (Ngày thường)', 150000, 180000, N'Trẻ em', 0, N'Hoạt động', GETDATE()),
('T002', N'Vé người lớn (Ngày thường)', 250000, 300000, N'Người lớn', 0, N'Hoạt động', GETDATE()),
('T003', N'Vé sinh viên', 200000, 230000, N'Sinh viên', 0, N'Hoạt động', GETDATE()),
('T004', N'Vé người cao tuổi', 120000, 150000, N'Người cao tuổi', 0, N'Hoạt động', GETDATE()),
('T005', N'Combo gia đình (2 lớn 2 trẻ)', 700000, 850000, N'Tất cả', 1, N'Hoạt động', GETDATE()),
('T006', N'Combo khám phá (Toàn bộ trò chơi)', 500000, 600000, N'Tất cả', 1, N'Hoạt động', GETDATE()),
('T007', N'Vé dịch vụ nước uống', 50000, 50000, N'Tất cả', 0, N'Hoạt động', GETDATE());

INSERT INTO ChiTietCombo (MaLoaiVeCha, MaLoaiVeCon, SoLuotChoPhep, NgayTao)
VALUES 
(5, 1, 2, GETDATE()), 
(5, 2, 2, GETDATE()), 
(6, 1, -1, GETDATE()), 
(6, 2, -1, GETDATE()); 

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, TrangThai, NgayTao)
VALUES ('admin', '123456', N'Quản trị viên', 'Admin', 1, GETDATE());

SELECT MAX(MaTroChoi) FROM TroChoi