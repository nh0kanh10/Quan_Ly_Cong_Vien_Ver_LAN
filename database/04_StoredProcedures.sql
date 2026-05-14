CREATE OR ALTER PROCEDURE [dbo].[sp_BaoCaoDoanhThu]
    @TuNgay DATETIME,
    @DenNgay DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    -- Lấy doanh thu từ Đơn Hàng (Đã Thanh Toán)
    -- Gộp toàn bộ Doanh thu Phòng, Phụ Thu, và Bán hàng POS
    SELECT 
        CAST(dh.NgayTao AS DATE) AS NgayGiaoDich,
        dh.MaDonHang AS MaGiaoDich,
        ISNULL(kh.HoTen, N'Khách Lẻ') AS TenKhachHang,
        CASE sp.LoaiSanPham 
            WHEN 'VeVaoKhu' THEN N'Vé Vào Khu'
            WHEN 'VeTroChoi' THEN N'Vé Trò Chơi'
            WHEN 'AnUong' THEN N'Ăn Uống'
            WHEN 'DoUong' THEN N'Đồ Uống'
            WHEN 'HangHoa' THEN N'Hàng Hóa'
            WHEN 'Combo' THEN N'Combo'
            WHEN 'TuDo' THEN N'Tủ Đồ'
            WHEN 'DoChoThue' THEN N'Đồ Cho Thuê'
            WHEN 'ChoiNghiMat' THEN N'Chòi Nghỉ Mát'
            WHEN 'DatChoThuAn' THEN N'Đặt Chỗ Thú Ăn'
            WHEN 'LuuTru' THEN N'Lưu Trú / Phòng'
            WHEN 'GuiXe' THEN N'Gửi Xe'
            WHEN 'NguyenLieu' THEN N'Nguyên Liệu'
            WHEN 'DoUongDongChai' THEN N'Đồ Uống Đóng Chai'
            WHEN 'AnUongTienLoi' THEN N'Ăn Uống Tiện Lợi'
            WHEN 'PhuongTien' THEN N'Phương Tiện'
            ELSE ISNULL(sp.LoaiSanPham, N'Khác')
        END AS LoaiGiaoDich,
        ISNULL(sp.LoaiSanPham, N'Khác') AS NhomSanPham,
        ISNULL(sp.TenSanPham, N'Sản phẩm khác') AS TenSanPham,
        -- Doanh thu chi tiết
        ct.ThanhTien AS DoanhThu,
        ct.GhiChu AS GhiChuChiTiet,
        ISNULL(nv.HoTen, N'Hệ Thống') AS NhanVienThuNgan,
        -- Phương thức thanh toán
        (SELECT TOP 1 tc.PhuongThuc FROM TaiChinh.ChungTuTC tc WHERE tc.IdDonHang = dh.Id) AS PhuongThucThanhToan
    FROM BanHang.DonHang dh
    INNER JOIN BanHang.ChiTietDonHang ct ON dh.Id = ct.IdDonHang
    LEFT JOIN DanhMuc.SanPham sp ON ct.IdSanPham = sp.Id
    LEFT JOIN DoiTac.ThongTin kh ON dh.IdKhachHang = kh.Id
    LEFT JOIN DoiTac.ThongTin nv ON dh.IdNhanVien = nv.Id
    WHERE dh.TrangThai = 'DaThanhToan'
      AND dh.NgayTao >= @TuNgay
      AND dh.NgayTao <= @DenNgay
    ORDER BY dh.NgayTao DESC;
END
GO
-- Lấy giá trị Sequence tiếp theo cho Nhân viên
CREATE OR ALTER PROCEDURE HeThong.sp_GetNextSeq_NhanVien
AS
BEGIN
    SET NOCOUNT ON;
    SELECT NEXT VALUE FOR HeThong.Seq_NhanVien AS NextVal;
END
GO


-- Lấy giá trị Sequence tiếp theo cho Nhân viên
CREATE OR ALTER PROCEDURE HeThong.sp_GetNextSeq_NhanVien
AS
BEGIN
    SET NOCOUNT ON;
    SELECT NEXT VALUE FOR HeThong.Seq_NhanVien AS NextVal;
END
GO
