use DaiNamResort
GO

-- 1. TÍNH LẠI TỔNG CHI TIÊU HỒI TỐ
PRINT N'Đang đồng bộ hóa Tổng Chi Tiêu cho hệ thống Khách Hàng...';

UPDATE KhachHang
SET TongChiTieu = ISNULL((
    SELECT SUM(dh.TongTien - ISNULL(dh.TienGiamGia, 0))
    FROM DonHang dh
    WHERE dh.IdKhachHang = KhachHang.Id AND dh.TrangThai = 'DaThanhToan'
), 0);



-- 2. ĐÁNH GIÁ LẠI HẠNG (RANK) CHUẨN
PRINT N'Đang thiết lập Phân Hạng Loyalty...';

UPDATE KhachHang SET LoaiKhach = 'CaNhan' WHERE TongChiTieu < 20000000 AND LoaiKhach IN ('CaNhan', 'VIP', 'VVIP');
UPDATE KhachHang SET LoaiKhach = 'VIP' WHERE TongChiTieu >= 20000000 AND TongChiTieu < 50000000;
UPDATE KhachHang SET LoaiKhach = 'VVIP' WHERE TongChiTieu >= 50000000;

PRINT N'Thành công! Toàn bộ Database đã được đồng bộ.';
