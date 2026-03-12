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
('KV010', N'Khu sự kiện', N'Nơi tổ chức sự kiện và biểu diễn', N'Hoạt động', GETDATE()),
('KV011', N'Khu trò chơi thực tế ảo', N'Trải nghiệm game và mô phỏng bằng công nghệ VR', N'Hoạt động', GETDATE());
--- insert dữ liệu trò chơi
INSERT INTO TroChoi
(MaCode, TenTroChoi, MaKhuVuc, LoaiTroChoi, SucChua, TuoiToiThieu, ChieuCaoToiThieu, ThoiGianLuot, MoTa, TrangThai, NgayTao)
VALUES
('TC001', N'Tàu lượn siêu tốc', 1, N'Cảm giác mạnh', 20, 12, 140, 5, N'Trò chơi tốc độ cao', N'Hoạt động', GETDATE()),
('TC002', N'Đu quay dây văng', 1, N'Cảm giác mạnh', 24, 10, 130, 6, N'Đu quay trên không', N'Hoạt động', GETDATE()),
('TC003', N'Thuyền hải tặc', 1, N'Cảm giác mạnh', 18, 10, 130, 5, N'Thuyền lắc mạnh', N'Hoạt động', GETDATE()),

('TC004', N'Nhà banh trẻ em', 2, N'Trẻ em', 30, 3, 80, 15, N'Khu bóng nhựa cho trẻ em', N'Hoạt động', GETDATE()),
('TC005', N'Đu quay ngựa gỗ', 2, N'Trẻ em', 15, 3, 80, 5, N'Trò chơi cổ điển cho trẻ', N'Hoạt động', GETDATE()),
('TC006', N'Tàu lượn mini', 2, N'Trẻ em', 12, 4, 90, 4, N'Tàu lượn nhỏ cho trẻ em', N'Hoạt động', GETDATE()),

('TC007', N'Nhà ma', 3, N'Phiêu lưu', 12, 10, 120, 8, N'Khám phá ngôi nhà ma', N'Hoạt động', GETDATE()),
('TC008', N'Mê cung gương', 3, N'Phiêu lưu', 10, 7, 100, 6, N'Mê cung kính phản chiếu', N'Hoạt động', GETDATE()),
('TC009', N'Đu dây zipline', 3, N'Phiêu lưu', 6, 12, 140, 4, N'Đu dây qua khu rừng', N'Hoạt động', GETDATE()),

('TC010', N'Máng trượt nước', 4, N'Nước', 10, 8, 120, 5, N'Trượt nước tốc độ cao', N'Hoạt động', GETDATE()),
('TC011', N'Hồ bơi tạo sóng', 4, N'Nước', 60, 5, 100, 20, N'Hồ bơi tạo sóng nhân tạo', N'Hoạt động', GETDATE()),
('TC012', N'Trượt ống xoắn', 4, N'Nước', 12, 9, 120, 4, N'Ống trượt nước xoắn', N'Hoạt động', GETDATE()),

('TC013', N'Xe điện đụng', 5, N'Gia đình', 15, 6, 100, 7, N'Xe điện va chạm vui nhộn', N'Hoạt động', GETDATE()),
('TC014', N'Xe jeep safari', 5, N'Gia đình', 10, 6, 100, 8, N'Khám phá khu rừng giả lập', N'Hoạt động', GETDATE()),
('TC015', N'Bóng đụng sumo', 5, N'Gia đình', 8, 8, 110, 6, N'Chơi bóng sumo vui nhộn', N'Hoạt động', GETDATE()),

('TC016', N'Leo núi nhân tạo', 6, N'Thể thao', 8, 10, 120, 8, N'Leo núi trong nhà', N'Hoạt động', GETDATE()),
('TC017', N'Đua xe F1 mini', 6, N'Thể thao', 12, 8, 110, 7, N'Đua xe tốc độ', N'Hoạt động', GETDATE()),
('TC018', N'Bắn cung', 6, N'Thể thao', 6, 10, 120, 10, N'Tập bắn cung', N'Hoạt động', GETDATE()),

('TC019', N'Vòng quay khổng lồ', 7, N'Tham quan', 40, 5, 100, 10, N'Ngắm toàn cảnh công viên', N'Hoạt động', GETDATE()),
('TC020', N'Xe lửa tham quan', 7, N'Tham quan', 30, 3, 80, 12, N'Tham quan công viên', N'Hoạt động', GETDATE()),

('TC021', N'Game bắn súng', 8, N'Trong nhà', 10, 7, 100, 6, N'Trọ chơi bắn súng điện tử', N'Hoạt động', GETDATE()),
('TC022', N'Game đua xe', 8, N'Trong nhà', 8, 7, 100, 5, N'Mô phỏng đua xe', N'Hoạt động', GETDATE()),
('TC023', N'Máy gắp thú', 8, N'Trong nhà', 5, 5, 80, 3, N'Máy gắp thú bông', N'Hoạt động', GETDATE()),

('TC024', N'Đua xe VR', 11, N'Thực tế ảo', 6, 10, 120, 8, N'Mô phỏng đua xe bằng kính VR', N'Hoạt động', GETDATE()),
('TC025', N'Bắn zombie VR', 11, N'Thực tế ảo', 4, 12, 130, 7, N'Trò chơi bắn zombie bằng VR', N'Hoạt động', GETDATE());

SELECT MaKhuVuc, TenKhuVuc
FROM KhuVuc
SELECT *
FROM TroChoi


