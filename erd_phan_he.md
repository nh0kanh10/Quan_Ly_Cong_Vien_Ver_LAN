# Phân Hệ ERD — Đại Nam Resort

> 3 ERD · **119 bảng** hiện · Sắp A→Z theo tên bảng (giống SSMS).  
> Tổng DB: 126 bảng (7 bỏ qua).

---

## ❌ Bỏ qua (7 bảng audit / i18n)

| # | Bảng | Schema |
|---|------|--------|
| 1 | BanDich | HeThong |
| 2 | ChiTietLuotQuet | BanHang |
| 3 | GhiChuPhong | BanHang |
| 4 | LichSuDoiPhong | BanHang |
| 5 | LichSuQuetDoan | BanHang |
| 6 | LichSuQuetVe | VanHanh |
| 7 | LichSuTrangThai | HeThong |

---

## ERD 1: Đối Tác, Nhân Sự & Hệ Thống (23 bảng)

| # | Bảng | Schema | Mô tả |
|---|------|--------|-------|
| 1 | BangChamCong | NhanSu | Dữ liệu máy vân tay |
| 2 | BangLuong | NhanSu | Chốt lương tháng |
| 3 | CaLamMau | NhanSu | Ca sáng, chiều, đêm |
| 4 | CauHinh | HeThong | Key-value cấu hình |
| 5 | ChungChiNhanVien | NhanSu | Bằng cấp, chứng chỉ |
| 6 | DonXinNghi | NhanSu | Đơn nghỉ phép |
| 7 | KhachHang | DoiTac | Con kế thừa — khách |
| 8 | KyLuat | NhanSu | Phạt kỷ luật |
| 9 | LichLamViec | NhanSu | Phân công lịch |
| 10 | LuongTrangThai | HeThong | Ma trận chuyển trạng thái |
| 11 | NghiBu | NhanSu | Giờ làm bù |
| 12 | NhaCungCap | DoiTac | Con kế thừa — NCC |
| 13 | NhanVien | DoiTac | Con kế thừa — nhân viên |
| 14 | PhanQuyen | NhanSu | VaiTro × QuyenHan |
| 15 | QuyenHan | NhanSu | Chức năng phần mềm |
| 16 | SoNgayPhepNam | NhanSu | Phép còn lại năm |
| 17 | TaiKhoan | DoiTac | Đăng nhập hệ thống |
| 18 | TaiNanLaoDong | NhanSu | Tai nạn lao động |
| 19 | ThongTin | DoiTac | Bảng CHA Party Model |
| 20 | TuDien | HeThong | Từ điển trạng thái |
| 21 | VaiTro | NhanSu | Trưởng phòng, NV... |
| 22 | VanDongVien | DoiTac | Con kế thừa — VĐV |
| 23 | YeuCauChungChi_KhuVuc | NhanSu | Yêu cầu bắt buộc theo khu |

---

## ERD 2: Danh Mục, Bán Hàng, Thanh Toán & Kho (67 bảng)

| # | Bảng | Schema | Mô tả |
|---|------|--------|-------|
| 1 | BanAn | DanhMuc | Bàn ăn vật lý |
| 2 | BangGia | DanhMuc | Giá theo thời gian |
| 3 | BangGia_ThueTheoGio | DanhMuc | Giá thuê block giờ |
| 4 | BaoGia | BanHang | Báo giá B2B |
| 5 | BaoGia_ChiTiet | BanHang | Chi tiết báo giá |
| 6 | CauHinhNgayLe | DanhMuc | Lịch ngày lễ |
| 7 | CauHinhThue | DanhMuc | VAT 8%, 10%... |
| 8 | ChiTietChungTu | Kho | Dòng phiếu kho |
| 9 | ChiTietDatBan | BanHang | Bàn nào trong đặt bàn |
| 10 | ChiTietDatPhong | BanHang | Từng phòng booking |
| 11 | ChiTietDonHang | BanHang | Dòng chi tiết bill |
| 12 | ChiTietThanhToan | TaiChinh | Tách phương thức |
| 13 | ChoiNghiMat | DanhMuc | Chòi nghỉ mát |
| 14 | ChuongTrai | DanhMuc | Chuồng trại |
| 15 | ChungTu | Kho | Phiếu kho |
| 16 | ChungTuTC | TaiChinh | Phiếu thu/chi/nạp ví |
| 17 | Combo | DanhMuc | Gói combo |
| 18 | ComboChiTiet | DanhMuc | Chi tiết combo |
| 19 | CongNo | BanHang | Công nợ công ty/đoàn |
| 20 | DatBan | BanHang | Đặt bàn nhà hàng |
| 21 | DatBan_MonAn | BanHang | Set menu đặt trước |
| 22 | DatChoThuAn | BanHang | Mua suất cho thú ăn |
| 23 | DatChoXemDua | BanHang | Ghế xem đua |
| 24 | DatChoXemShow | BanHang | Ghế xem biểu diễn |
| 25 | DatPhongChiTiet | BanHang | Header đặt phòng |
| 26 | DiemBanHang_POS | BanHang | Máy POS |
| 27 | DinhMucNguyenLieu | DanhMuc | BOM nguyên liệu |
| 28 | DongVat | DanhMuc | Thú sở thú |
| 29 | DonHang | BanHang | Header bill |
| 30 | DonViTinh | DanhMuc | Cái, ly, kg... |
| 31 | KhoHang | Kho | Danh sách kho |
| 32 | KhuVuc | DanhMuc | Khu vực hoạt động |
| 33 | KhuVucBien | DanhMuc | Biển tắm (kế thừa) |
| 34 | KhuVucThu | DanhMuc | Sở thú (kế thừa) |
| 35 | KhuyenMai | BanHang | Mã giảm giá |
| 36 | KhuyenMai_DieuKien | BanHang | Điều kiện áp dụng |
| 37 | KhuyenMai_LichSu | BanHang | Dùng bao nhiêu lần |
| 38 | LenhBep | BanHang | KDS lệnh bếp khách lẻ |
| 39 | LichSuDiem | TaiChinh | Log cộng/trừ điểm |
| 40 | LichSuHoanHang | BanHang | Log hoàn hàng chi tiết |
| 41 | LoaiPhong | DanhMuc | Loại phòng KS |
| 42 | LoHang | Kho | Lô hàng (HSD) |
| 43 | Menu_POS | BanHang | SP trên máy POS |
| 44 | MonAn | DanhMuc | Cấu hình món ăn |
| 45 | MucTonToiThieu | Kho | Cảnh báo tối thiểu |
| 46 | NhaHang | DanhMuc | Nhà hàng |
| 47 | PhienThuNgan | BanHang | Ca mở/đóng két |
| 48 | Phong | DanhMuc | Phòng vật lý |
| 49 | QuyDoiDonVi | DanhMuc | 1 thùng = 24 chai |
| 50 | QuyenLoiDoanKhach | BanHang | Rổ suất ăn đoàn |
| 51 | QuyTacDiem | TaiChinh | Kịch bản tích điểm |
| 52 | SanPham | DanhMuc | Gốc mọi thứ bán được |
| 53 | SanPham_Ve | DanhMuc | Cấu hình vé |
| 54 | SoCai | Kho | Sổ cái kho |
| 55 | SoCaiVi | TaiChinh | Sao kê ví |
| 56 | TaiSanChoThue | DanhMuc | Phao #1, ván #2 |
| 57 | TamGiuTonKho | Kho | Hold hàng |
| 58 | TheRFID | TaiChinh | Vòng tay RFID |
| 59 | ThietBi | DanhMuc | Thiết bị |
| 60 | ThueChoi | BanHang | Thuê chòi nghỉ mát |
| 61 | ThueDoChiTiet | BanHang | Thuê phao, ván... |
| 62 | ThueTu | BanHang | Thuê tủ gửi đồ |
| 63 | TroChoi | DanhMuc | Trò chơi |
| 64 | TuDo | DanhMuc | Tủ gửi đồ |
| 65 | VeDienTu | BanHang | QR vé điện tử |
| 66 | Ve_QuyenTruyCap | DanhMuc | Vé → khu vực |
| 67 | ViDienTu | TaiChinh | Ví điện tử khách |

---

## ERD 3: Vận Hành, Sự Kiện & Trường Đua (29 bảng)

| # | Bảng | Schema | Mô tả |
|---|------|--------|-------|
| 1 | BaiDoXe | VanHanh | Bãi đỗ xe |
| 2 | BaoTri | VanHanh | Kế hoạch bảo trì |
| 3 | CaTrucCuuHo | VanHanh | Ca trực cứu hộ |
| 4 | ChatLuongNuoc | VanHanh | Đo pH, Clo hồ bơi |
| 5 | ChiTietVatTuBaoTri | VanHanh | Vật tư sửa chữa |
| 6 | DanhGiaDichVu | VanHanh | Đánh giá 1-5 sao |
| 7 | DuongDua | VanHanh | Đường đua |
| 8 | GiaiDua | VanHanh | Giải đua |
| 9 | HuongDanVien_DoanKhach | VanHanh | Phân công HDV |
| 10 | KetQuaDua | VanHanh | Kết quả đua |
| 11 | KhanDai | VanHanh | Khán đài |
| 12 | KiemTraAnToanNgay | VanHanh | Kiểm tra an toàn hàng ngày |
| 13 | Kiosk | VanHanh | Máy bán vé tự động |
| 14 | LenhPhucVuDoan_BEO | VanHanh | Lệnh bếp đoàn |
| 15 | LichBieuDien | VanHanh | Show xiếc, nước |
| 16 | LichTaoSong | VanHanh | Lịch máy sóng |
| 17 | LichThiDau | VanHanh | Lịch thi đấu |
| 18 | LoaiHinhDua | VanHanh | Đua chó/ngựa/xe |
| 19 | LuotVaoRaBaiXe | VanHanh | Quét biển số |
| 20 | NguaDua | VanHanh | Ngựa đua |
| 21 | NhatKyYTe_Thu | VanHanh | Bệnh án thú |
| 22 | PhanCongBuongPhong | VanHanh | Giao dọn phòng |
| 23 | PhuongTienDua | VanHanh | Xe đua |
| 24 | SuCo | VanHanh | Báo cáo sự cố |
| 25 | SuKien | VanHanh | Lễ hội, Countdown |
| 26 | ThatLac | VanHanh | Đồ thất lạc |
| 27 | VatTuPhongMacDinh | VanHanh | Template vật tư/phòng |
| 28 | VeDoXeChiTiet | VanHanh | Vé gửi xe |
| 29 | ViTriNgoi | VanHanh | Ghế ngồi |

---

## Kiểm tra tổng

| ERD | Số bảng |
|-----|---------|
| 1. Đối Tác + Nhân Sự + Hệ Thống | 23 |
| 2. Danh Mục + Bán Hàng + Thanh Toán + Kho | 67 |
| 3. Vận Hành + Sự Kiện + Trường Đua | 29 |
| ❌ Bỏ qua (audit/i18n) | 7 |
| **Tổng** | **126 ✅** |
