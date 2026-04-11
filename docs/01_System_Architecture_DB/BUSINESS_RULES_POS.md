# NGHIỆP VỤ BÁN HÀNG (POS) VÀ LOYALTY (TÍCH ĐIỂM)

Tài liệu này giải thích chi tiết các quy tắc nghiệp vụ (Business Rules) đang chạy trong module Bán Hàng (`frmBanHang`) để các lập trình viên khác có thể đọc hiểu code nhanh chóng.

---

## 1. Hệ thống Khuyến Mãi (Promotions) & Chiết Khấu (Discount)
Hệ thống **không cho phép cộng dồn (No-Stacking)** các loại giảm giá, mà áp dụng **Quy tắc MAX** (Lấy cái nào có lợi nhất cho khách hàng).

1. **Chiết Khấu Hạng Khách Hàng (VIP Discount):**
   Được cấu hình cứng (hardcode) trong hàm `GetVipDiscount()`:
   - Khách VVIP: Giảm 25%
   - Khách Doanh Nghiệp: Giảm 20%
   - Khách VIP: Giảm 10%
   - Đoàn Khách: Giảm 15%
   - Học Sinh/Sinh Viên: Giảm 5%

2. **Chương Trình Khuyến Mãi (Events):**
   Được lấy từ DB (`BUS_KhuyenMai.GetBestActivePromotion(tổng_tiền)`). Hệ thống sẽ tìm ra event giảm giá cao nhất đang active.

3. **Áp dụng MAX:**
   Code: `decimal pctApDung = Math.Max(pctVip, pctEvent);`
   Hệ thống sẽ lấy giá trị phần trăm chiết khấu cao nhất giữa (Event) và (Hạng thẻ KH) để giảm giá. Đơn hàng ưu tiên hiển thị lý do giảm giá có lợi nhất ("🎁 Giảm giá (VIP): ..." hoặc "🎁 Giảm giá (Sự kiện): ...").

---

## 2. Hệ Thống Loyalty (Tích Điểm / Tiêu Điểm)
Được cô lập hoàn toàn logic trong `BUS_TichDiem.cs` để dễ scale.

### 2.1. Quy đổi giá trị
- **1 Điểm Tích Lũy = 1,000 VNĐ**.

### 2.2. Quy tắc Tiêu Điểm
1. **Chỉ được dùng tối đa 50% giá trị đơn hàng**.
   (Ví dụ: Đơn 500k, khách được dùng tối đa 250 điểm = 250,000đ).
2. **Quy tắc MAX với Chiết Khấu:**
   Khách hàng **KHÔNG THỂ** vừa dùng chiết khấu 10% VIP, vừa dùng điểm cho cùng 1 đơn hàng.
   Khi `BtnThanhToan_Click` được gọi:
   - Hệ thống tính "Số tiền được giảm nhờ Chiết khấu".
   - Hệ thống tính "Số tiền được giảm nhờ Điểm khả dụng (max 50%)".
   - Nếu dùng Điểm mang lại số tiền giảm cao hơn lợi ích từ Chiết khấu, một `MessageBox` sẽ hiện lên **Gợi ý khách dùng điểm**. Nếu khách đồng ý, Điểm được dùng; nếu khách từ chối, Chiết khấu được áp dụng.

### 2.3. Quy tắc Tích Điểm (Earn Points)
Số điểm được phép cộng lấy từ `BUS_TichDiem.TinhDiemThuong()`:
* `Công thức: Điểm = Floor(Tổng tiền gốc / Đơn vị tích(100k)) * Hệ Số Loại Khách`

**Hệ số Loại Khách:**
| Loại Khách | Hệ Số Nhân | Ví dụ đơn 500,000đ |
|---|---|---|
| Cá Nhân / HSSV | x1.0 | 5 Điểm |
| Doanh Nghiệp | x1.5 | 7 Điểm |
| VIP | x2.0 | 10 Điểm |
| VVIP | x3.0 | 15 Điểm |
| Đoàn | x0.5 | 2 Điểm (vì đã có CK 15%) |
| Nội Bộ | x0.0 | 0 Điểm |

### 2.4. Tính Năng Nâng Hạng Tự Động (Auto-Upgrade)
Hệ thống **không bán gói VIP**, mà tự động nâng hạng khi khách đạt mốc Tích điểm:
Nằm ở `BUS_TichDiem.CongDiem()`, sau khi cộng điểm vào `KhachHang.DiemTichLuy`:
- ` >= 200 điểm ` → tự động Up lên **VIP**
- ` >= 500 điểm ` → tự động Up lên **VVIP**
*(Tính năng nâng hạng tự động chỉ chạy với các loại khách Cá Nhân, HSSV, VIP. Bỏ qua Doanh Nghiệp và Đoàn)*.

### 2.5. Audit Trail Lịch Sử Điểm
Tất cả giao dịch Cộng / Trừ điểm đều ghi đè vào bảng `LichSuDiem` (`DAL_LichSuDiem.Them()`):
- Lưu vết `SoDuTruoc`, `SoDuSau`.
- Reference đến `IdDonHang`.
- Loại giao dịch `"TichDiem"` hoặc `"TieuDiem"`.

---

## 3. Kiến Trúc Luồng Thanh Toán (Payment Workflow)
Hàm `ThanhToan(phuongThuc)` trong `frmBanHang.cs`:

1. **Validation**: Check KH nếu trả bằng RFID (`ViRFID`).
2. **Pre-Calculation**: Tính Max(Discount, Loyalty). Nếu Loyalty lớn hơn, show Confirm Dialog.
3. **Save DB**:
   - Tạo dòng `DonHang` với tổng tiền gốc và tiền đã giảm (`TienGiamGia`).
   - Lặp ghi các dòng `ChiTietDonHang`.
4. **Transaction (Phiếu Thu / Trừ Ví)**:
   - Nếu `ViRFID`: Trực tiếp update số dư `ViDienTu` và tạo nhật ký giao dịch. (Có xử lý DB Concurrency OCC).
   - Nếu phương thức khác (Tiền mặt / Thẻ...): Ghi xuống `PhieuThu`.
5. **Post-Payment Logs**:
   - Nếu KH dùng điểm → gọi `BUS_TichDiem.TieuDiem()`.
   - Tính toán điểm thưởng và gọi `BUS_TichDiem.CongDiem()`.
6. **Smart Popup - Phát Vé Điện Tử**:
   Dựa vào nghiệp vụ, hệ thống sẽ chọc xuống `BUS_VeDienTu` kiểm tra: `Có mặt hàng vé nào vừa được mua không?`.
   - NẾU CÓ: Tự khởi tạo và show dạng popup form `frmPhatVe` đưa khách quét vé hoặc gửi email.
   - NẾU KHÔNG CÓ (như Mua nước/Thức ăn): Chỉ hiện Inform MessageBox("Thanh toán thành công").
