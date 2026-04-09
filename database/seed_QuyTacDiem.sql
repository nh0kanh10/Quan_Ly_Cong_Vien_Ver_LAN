-- =============================================
-- SEED DATA: QuyTacDiem (Loyalty Rules)
-- Thiết kế từ góc nhìn 6 loại khách hàng
-- =============================================

-- Kiểm tra và chèn nếu chưa có dữ liệu
IF NOT EXISTS (SELECT 1 FROM QuyTacDiem)
BEGIN
    SET IDENTITY_INSERT QuyTacDiem ON;

    -- Khách cá nhân: mỗi 100k = 1 điểm (hệ số x1)
    INSERT INTO QuyTacDiem (Id, TenQuyTac, TongDonToiThieu, SoDiemThuong, LoaiKhachApDung, TrangThai)
    VALUES (1, N'Tích điểm khách cá nhân', 100000, 1, 'CaNhan', 1);

    -- HSSV: mỗi 50k = 1 điểm (threshold thấp hơn → khuyến khích giới trẻ)
    INSERT INTO QuyTacDiem (Id, TenQuyTac, TongDonToiThieu, SoDiemThuong, LoaiKhachApDung, TrangThai)
    VALUES (2, N'Tích điểm HSSV (threshold thấp)', 50000, 1, 'HocSinhSinhVien', 1);

    -- VIP: mỗi 100k = 2 điểm (x2 → thưởng lòng trung thành)
    INSERT INTO QuyTacDiem (Id, TenQuyTac, TongDonToiThieu, SoDiemThuong, LoaiKhachApDung, TrangThai)
    VALUES (3, N'Tích điểm VIP x2', 100000, 2, 'VIP', 1);

    -- Đoàn: mỗi 100k = 1 điểm nhưng hệ số x0.5 trong BUS (đã có 15% chiết khấu)
    INSERT INTO QuyTacDiem (Id, TenQuyTac, TongDonToiThieu, SoDiemThuong, LoaiKhachApDung, TrangThai)
    VALUES (4, N'Tích điểm đoàn (rate thấp)', 100000, 1, 'Doan', 1);

    -- Doanh nghiệp: mỗi 100k = 2 điểm (x1.5 rounded → khuyến khích hợp đồng dài hạn)
    INSERT INTO QuyTacDiem (Id, TenQuyTac, TongDonToiThieu, SoDiemThuong, LoaiKhachApDung, TrangThai)
    VALUES (5, N'Tích điểm doanh nghiệp x1.5', 100000, 2, 'DoanhNghiep', 1);

    -- Nội bộ: Nhân viên không tích điểm
    INSERT INTO QuyTacDiem (Id, TenQuyTac, TongDonToiThieu, SoDiemThuong, LoaiKhachApDung, TrangThai)
    VALUES (6, N'Nội bộ - không tích điểm', 0, 0, 'NoiBo', 0);

    SET IDENTITY_INSERT QuyTacDiem OFF;

END

GO
