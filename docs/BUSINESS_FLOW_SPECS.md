# THÔNG SỐ NGHIỆP VỤ HỆ THỐNG ĐẠI NAM (BUSINESS DATASHEET)

> **Lưu ý:** Tài liệu này được trích xuất *chính xác 100% từ Source Code (Tầng BUS)*. Không phỏng đoán, phục vụ cho việc maintain, audit và tham gia bảo vệ đồ án.

---

## 1. NGHIỆP VỤ VÍ ĐIỆN TỬ & CÔNG NGHỆ RFID
*(Nằm tại `BUS_ViDienTu.cs`, `BUS_GiaoDichVi.cs`, `DAL_ViDienTu.cs`)*

### 1.1. Luồng Nạp Tiền (Top-up Flow)
**Hàm:** `BUS_GiaoDichVi.NapTien(maRfid, soTien, phuongThuc)`
- **Rule 1 (Giới hạn nạp):** Tối đa duy nhất **50,000,000 VNĐ / lần nạp**.
- **Rule 2 (Validate Thẻ):** Thẻ RFID phải ở trạng thái `"Active"`. Các trạng thái `"Lost"` (Mất) hoặc `"Locked"` (Khóa) sẽ bị từ chối nạp tiền.
- **Rule 3 (Atomic Transaction):** Khi tiền vào mạng lưới, 3 hành động MỚI được phép thành công cùng lúc (TransactionScope):
  1. Thay đổi cột `SoDuKhaDung` trong bảng `ViDienTu`.
  2. Khởi tạo một Record vào bảng `GiaoDichVi` (Mã bắt đầu bằng `GD-...`) với Type = `NapTien`.
  3. Khởi tạo một Record Kế Toán vào bảng `PhieuThu` (Mã bắt đầu bằng `PT-...`) để Audit sau này.

### 1.2. Luồng Thanh Toán Qua Ví (OCC - Optimistic Concurrency Control)
**Hàm:** `BUS_DonHang.ThanhToanBangVi()`
- **Chống gian lận (Double Spending):** Nếu hai quầy thu ngân cùng trừ tiền một ví tại *cùng một khắc (millisecond)*, tầng DAL sẽ quăng lỗi `DBConcurrencyException`. Giao dịch thứ 2 sẽ bị System Rollback và văng popup: `"Ví vừa bị thay đổi bởi giao dịch khác"`.
- **Transaction Isolation:** Lệnh cấu hình `IsolationLevel.ReadCommitted` chống Dirty Read, bảo vệ tuyệt đối số dư của khách hàng.

---

## 2. NGHIỆP VỤ VÉ ĐIỆN TỬ VÀ KIỂM SOÁT CỔNG (GATE KEEPING)
*(Nằm tại `BUS_VeDienTu.cs`)*

### 2.1. Generation Engine (Sinh vé tự động)
**Hàm:** `BUS_VeDienTu.GenerateTicket()`
- Được kích hoạt **tự động** sau khi Thanh Toán POS.
- Mã code luôn có prefix: `"TK" + MMDDYY + chuỗi ngẫu nhiên cắt 5 ký tự`.
- Đặc biệt: Dữ liệu sử dụng Data Denormalization - nhúng trực tiếp `IdSanPham` vào bảng `VeDienTu` thay vì phải Join 3 bảng (`ChiTietDonHang` -> `SanPham`), để hy sinh ổ cứng giúp tăng 300% tốc độ Quẹt thẻ qua Cổng.

### 2.2. Validation Engine (Thuật toán soát vé tại cổng trạm)
**Hàm:** `BUS_VeDienTu.CheckTicket()`
- Trạm kiểm soát trên App sẽ gửi `IdKhuVucTramGac` của chính nó và `Mã Vé` lên máy chủ.
- **Rule 1 (Bảo mật vị trí):** So sánh `IdKhuVucTramGac` của hệ thống quét có giống với `IdKhuVuc` của dịch vụ trên vé hay không. Nếu Mua vé Vườn Thú mà cầm qua Công Viên Nước quét $\rightarrow$ Lỗi `Return 1` (Sai trạm gác).
- **Rule 2 (Trừ số điểm/Luợt):** Sau khi xác nhận Hợp lệ, trừ bớt 1 lượt (`SoLuotConLai--`). 
- **Rule 3 (Đổi trạng thái):** 
  - Nếu số lượt về `0` $\rightarrow$ Đổi trạng thái vé thành `"DaSuDung"` (Tuyệt đối không Xóa record để lưu Log đối soát).
  - Nếu số lượt `> 0` (Ví dụ vé kẹp dắt 3 người) $\rightarrow$ Vé mang trạng thái `"DangSuDung"`.

---

## 3. ENGINE TRỪ KHO ĐỘNG (DEDUCTION ENGINE B.O.M)
*(Nằm tại `BUS_KhoHang.WriteLedgerTuDong`)*

Đây là một Engine tinh vi để giải quyết bài toán: *Khách mua vé Combo, làm sao để trừ kho lon Coca và ổ bánh mì nằm phía trong combo đó?*

### 3.1. Pre-Fetching O(1) Cache Lookup
- Thay vì query database vòng lặp `FOR` cho từng món hàng (N+1 Query Issue), hệ thống bọc tất cả Cấu Hình Sản Phẩm gom thành 1 tập duy nhất lưu vào RAM: `dictSanPham`. Biến vòng lặp DB thành vòng lặp RAM nội bộ.

### 3.2. Bóc Tách Combo (BOM - Bill Of Materials)
Hệ thống sẽ chạy qua giỏ hàng `ChiTietDonHang`:
- **NẾU LÀ HÀNG HÓA LẺ (Nước, Kính bơi...):**
  - Skip các món được đánh dấu `LoaiSanPham = "DichVu"` (Ví dụ như vé tham quan, cho thuê đồ... không phải là hiện vật vật lý).
  - Khởi tạo lệnh xuất vào `listTheKhoToInsert` với cấu trúc `-(Số lượng)`.
- **NẾU LÀ COMBO:**
  - Hệ thống lục tìm tất cả cấu trúc định mức của Combo (`ComboChiTiet`).
  - *Công thức trừ:* `Số Lượng thực tế = Số lượng mua * Định mức vật tư trong 1 Combo`.
  - Khởi tạo hàng loạt các lệnh XUÁT với ghi chú chuyên biệt `"POS BOM-Deduct (Combo #ID)"`.

### 3.3. Bulk Insert (Đổ lô xuống Database)
- **Cấm thao tác** Update trừ con số vào bảng vật tư (`UPDATE Kho SET SoLuong = SoLuong - 1`). Hành vi này rủi ro Data chênh lệch.
- **Tiêu chuẩn Ghi Sổ (Ledger-based):** Mọi tác động xuất hàng ở bước 3.2 đều lưu xuống bảng Kế toán kho (`TheKho`) dưới dạng các Transaction `+` hoặc `-`. Sau này truy vết hàng tồn chỉ cần chạy lệnh `SUM()` TheKho.

---

## 4. NGHIỆP VỤ VẬN HÀNH DỊCH VỤ NHÀ HÀNG COFFEE
*(Nằm tại `BUS_NhaHang.cs`)*

- **Loại hình (IsTakeAway):** Khi Tạo Đơn, Nhân viên chọn `Mang Đi` hoặc `Dùng Tại Bàn`.
- Thiết kế Data Model nhúng **Biến Trạng Thái Mảng**: Cụm trạng thái Order của riêng nhà hàng hoàn toàn tách rời với Cụm trạng thái POS Thường. (Bao gồm: `ChoXuLy` -> `DangChuanBi` -> `DaPhucVu`).
- Gắn liền Module Gửi Bếp/Pha Chế để tương tác ngầm với các Màn Hình Hiển Thị của Bộ Phận Bếp (KDS - Kitchen Display System).

*(Lưu ý cuối: Toàn bộ 4 luồng trên đã được Deploy ổn định ở V1.1 của Codebase dưới dạng Solid và tuân thủ chặt Transaction SQL).*
