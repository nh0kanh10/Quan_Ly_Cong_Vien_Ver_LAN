# HƯỚNG DẪN CÀI ĐẶT VÀ SỬ DỤNG – Sprint 3

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_UG_Sprint3_v2.0  
**Nhóm**: NguyenTanNhi  
**Đề tài**: Hệ thống Quản lý Công viên Vui chơi Giải trí  
**Phiên bản**: 2.0 (Chi tiết bản Full)  
**Ngày tạo**: 12/04/2026  
**Tham chiếu**: SD001_UG_Sprint2_v1.0 (Quản lý NV & KH)

---

## 1. THAY ĐỔI SO VỚI SPRINT 2

| Hạng mục | Sprint 2 | Sprint 3 |
|:---------|:---------|:---------|
| Database | 25 bảng (CRUD) | + Các bảng mới (Đơn Hàng, Chi Tiết Đơn, Phiếu Thu, Vé Điện Tử, Lịch Sử RFID, Phiếu Thuê Đồ...) |
| Giao diện mới | Quản lý Dữ liệu | Bán Hàng (POS), Kiểm Soát Vé Cổng (Gate), Thuê Đồ, Xác Nhận Booking |
| Tích hợp phần cứng | None | Đầu đọc thẻ RFID/NFC, Máy quét Camera Barcode/QR, Súng quét mã |

### Yêu cầu phần cứng bổ sung (Nên có)
- **Webcam/Súng quét Barcode 2D**: Dùng để quét vé. Đảm bảo app được cấp quyền `Camera` trên hệ điều hành Windows 10/11.
- **Đầu đọc RFID thẻ từ**: Đang chạy qua giao thức **Keyboard Emulation** (Đầu đọc tự gỏ phím). Phải đảm bảo cắm vào USB trước khi khởi động. **TUYỆT ĐỐI không bật Unikey ở chế độ tiếng Việt** (V) khi thao tác quét thẻ để tránh lỗi chuỗi dữ liệu đầu vào.

---

## 2. HƯỚNG DẪN SỬ DỤNG CHI TIẾT TỪNG PHÂN HỆ SPRINT 3

### 3.1 Bán Hàng Tại Quầy (POS - Màn hình Cốt Lõi)

**Đường dẫn**: Menu → Bán hàng → Bán hàng tại quầy

**Giao diện tổng quan:**
```text
╔══════════════════════════════════════════════════════════════════════════╗
║  BÁN HÀNG TẠI QUẦY (POS)                                        [_][X]   ║
╠══════════════════════════════════════════════════════════════════════════╣
║ [Tất cả] [Vé] [Đồ ăn] [Thuê đồ]   Mã SP/Scanner(F1): [_________________] ║
║                                                                          ║
║ ┌─ DANH SÁCH SẢN PHẨM ───────┐  ┌─ GIỎ HÀNG ───────────────────────────┐ ║
║ │ [A01] Bánh Mì - 20K        │  │ [1] Vé người lớn x 2 = 500K          │ ║
║ │ [A02] Coca Cola - 15K      │  │ [2] Combo Gia Đình x 1 = 600K        │ ║
║ │ [V01] Vé người lớn - 250K  │  │                                      │ ║
║ │ [V02] Vé trẻ em - 150K     │  └──────────────────────────────────────┘ ║
║ │ [T01] Phao bơi - 10K       │  │ KH/RFID(F2): [_____________________]   ║
║ └────────────────────────────┘  │ Mã Booking : [_____________________]   ║
║                                 │ [v] In Hóa Đơn                         ║
║                                 │ TỔNG CỘNG: 1,100,000đ                  ║
║                                 │ Khách đưa(F8): 1500K   Thối: 400K      ║
║                                 │                                        ║
║ Kho: [▼ Kho Cổng Chính]         │ [F9]TiềnMặt [F10]RFID [F11]ChuyểnK.    ║
╚══════════════════════════════════════════════════════════════════════════╝
```

**Thao tác nghiệp vụ mở rộng (Tính Năng Chuyên Sâu):**
1. **Quét nhanh với Hệ số:** Có thể gõ hệ số nhân số lượng, ví dụ `10*VEM001` vào thanh Scanner để đẩy "10 Vé" vào giỏ hàng ngay lập tức. Tính năng này được tối ưu không cần dùng chuột.
2. **Quy đổi Đơn Vị Tính linh hoạt:** Ngay trên Lưới Giỏ  Hàng (Cart), bạn có thể click vào cột `Đơn vị` của mặt hàng (Ví dụ Nước tinh khiết) để đổi từ **Chai** sang **Thùng**. Hệ thống sẽ tự tra chéo tỷ lệ quy đổi, kiểm tra tồn kho theo đơn vị cơ bản và cấu trúc lại bảng giá cho mặt hàng đó tự động. Giúp mua sỉ cực nhanh.
3. **Khuyến mãi tự động & Gợi ý Upsell:** Dựa trên tổng bill và cấu hình của Kế Toán, giỏ hàng tự động trừ % theo Khuyến Mãi Ngập Tràn. Thậm chí UI sẽ hiện **"Gợi ý: Mua thêm X VNĐ để kích hoạt voucher..."** ở góc dưới giúp Thu ngân chèo kéo khách (Upsell) cực kỳ hiệu quả mà nhân viên không cần phải thuộc lòng chương trình KM thẻ VIP của Công Viên.
4. **Chọn Kho Xử lý:** Đồ ăn và các sản phẩm vật lý sẽ được rút từ Kho đã chọn ở comboBox góc trái dưới. Hàng hóa vật lý bị kiểm soát tồn kho gắt gao (nếu kho hết sẽ báo Popup Tồn Kho 0). Riêng vé không bị trừ tồn.
5. **Thanh Toán & Cấp Phát Vé:**
   - **(F9) Tiền mặt**: Hệ thống ghi nhận luồng tiền mặt vào ca. 
   - **(F10) Thẻ RFID**: Phải gán khách hàng và quét vòng RFID vào ô `(F2)`. Hệ thống kiểm tra số dư Ví KH > Tổng cộng thì mới cho qua, trừ thẳng vào ví điện tử.
   - **(F11) Chuyển khoản**: Xác nhận giao dịch Online qua QR tài khoản công ty.
   - **Xử lý cấp phát vé (Thay thế In giấy)**: Ngay khi thanh toán thành công, **nếu giỏ hàng có mua vé**, hệ thống lập tức mở cửa sổ **Cấp Phát Vé (frmPhatVe)**. Tại đây, tất cả các mã Code vé (`VE-xxxx`) kèm mã vạch QR tĩnh được hiển thị trực tiếp lên màn hình. Thu ngân có thể chụp ảnh/scan cho khách đi qua Cổng một cách linh hoạt, bảo vệ môi trường, chấm dứt việc in giấy cứng. Nếu đơn hàng chỉ mua đồ ăn, nước, hệ thống chỉ show Bill chữ xanh dạng Popup tóm tắt hóa đơn thu tiền.
6. Xử lý **Mã Booking**: Gõ mã `BK-xxxxxx` của đoàn khách, hệ thống sẽ xác nhận lấy danh sách xuất ăn (Quota thức ăn) vào giỏ mức giá 0đ để xuất kho cho Bếp.

---

### 3.2 Kiểm Soát Vé Tại Cổng (Ticket Gate)

**Đường dẫn**: Menu → Kiểm soát vé → Kiểm soát vé tại cổng

**Giao diện tổng quan:**
```text
╔══════════════════════════════════════════════════════════════════════════╗
║  KIỂM SOÁT VÉ CỔNG                                              [_][X]   ║
╠══════════════════════════════════════════════════════════════════════════╣
║ Khu Vực: [▼ Cổng Chính Thủy Cung]  Trò Chơi: [▼ Vào Cổng Thủy Cung]      ║
║                                                                          ║
║ ┌─ CAMERA QUÉT ──────────────┐  ┌─ THÔNG TIN VÉ ───────────────────────┐ ║
║ │                            │  │ Mã vé: BK-1025-ABCD                  │ ║
║ │        [ TRỰC TIẾP ]       │  │ Dịch vụ: Vé vào cổng Thủy Cung       │ ║
║ │                            │  │ Lượt còn: 0 / 1 (Hết lượt)           │ ║
║ │ [📷 Bật Cam] [📂 Chọn Ảnh] │  │ Ngày mua: 12/04/2026                 │ ║
║ └────────────────────────────┘  │                                      │ ║
║ ┌─ TRẠNG THÁI ───────────────┐  │        [ X ] KHÔNG HỢP LỆ            │ ║
║ │          ( X )             │  └──────────────────────────────────────┘ ║
║ │     TỪ CHỐI VÀO CỔNG       │  ┌─ LỊCH SỬ QUÉT (Hôm nay: 125/4/129) ──┐ ║
║ │  Vé đã qua sử dụng!        │  │ 10:15 | VE-0001      | HỢP LỆ        │ ║
║ └────────────────────────────┘  │ 10:16 | BK-1025-ABCD | TỪ CHỐI       │ ║
╚══════════════════════════════════════════════════════════════════════════╝
```

**Thao tác nghiệp vụ chuẩn:**
1. **Khởi động ca làm việc**: Phải setup đúng `Khu Vực` và `Trò Chơi`. Nếu đứng trạm công viên nước mà chọn Cổng Safari thì khách vào nước sẽ bị TỪ CHỐI.
2. **Nhập liệu linh hoạt:** Con trỏ luôn Focus tĩnh ở form. Quét máy súng cầm tay hệ thống tự hứng. Nếu điện thoại hư khách cung cấp hình ảnh thì bấm `[Chọn ảnh]` để load. Bật Webcam qua nút `[Bật Cam]`. Dấu hiệu `TRẠNG THÁI` sẽ nháy xanh-vàng-đỏ kèm âm báo.
3. **Vé theo Đoàn**: Khi quét mã Đoàn (BK-xxx), phần thông báo phụ sẽ báo: "Khách thuộc đoàn A, số lượt 12/15 người", cho đến khi hết người.

---

### 3.3 Cho Thuê Đồ (Rental Services)

**Đường dẫn**: Menu → Bán hàng → Cho Thuê Dịch Vụ

**Giao diện tổng quan:**
```text
╔══════════════════════════════════════════════════════════════════════════╗
║  CHO THUÊ DỊCH VỤ                                 [Giao Đồ] [Nhận Trả]   ║
╠══════════════════════════════════════════════════════════════════════════╣
║  Trạm: [▼ Công Viên Nước Bắc  ]                                          ║
║                                                                          ║
║  ┌─ SẢN PHẨM ────────────────┐  ┌─ GIỎ THUÊ (GIAO ĐỒ TỚI TAY KHÁCH) ───┐ ║
║  │ Tìm: [__________________] │  │ RFID: [A8 D9 1B 00] Tên: Nguyen A    │ ║
║  │                           │  │ Số dư ví: 500,000đ  (Vàng)           │ ║
║  │ ┌────┬─────────┬────┬────┐│  │                                      │ ║
║  │ │Mã  │ Tên     │Thuê│Cọc ││  │ ┌────┬──┬────┬────┬──────┬──────┐    │ ║
║  │ ├────┼─────────┼────┼────┤│  │ │Tên │SL│Thuê│Cọc │T.Thuê│T.Cọc │    │ ║
║  │ │PH01│Phao bơi │10K │50K ││  │ ├────┼──┼────┼────┼──────┼──────┤    │ ║
║  │ │AO01│Áo phao  │15K │100K││  │ │Phao│ 2│20K │100K│ 20K  │ 100K │    │ ║
║  │ │XD01│Xe đạp   │30K │200K││  │ └────┴──┴────┴────┴──────┴──────┘    │ ║
║  │ └────┴─────────┴────┴────┘│  │ [TT RFID] [TT Tiền Mặt]    [Hủy]     │ ║
║  └───────────────────────────┘  └──────────────────────────────────────┘ ║
╚══════════════════════════════════════════════════════════════════════════╝
```

Nghiệp vụ chia ngang làm 2 Luồng bên trong màn hình này.
- **LUỒNG 1 - Giao đồ (Khách cọc tiền / cọc ví lấy đồ ra chơi)**
  - Tab `Giao đồ`: Quét đồ muốn đưa khách qua lưới Sản Phẩm. 
  - Khách vãng lai: Yêu cầu đóng bằng Tiền mặt (Tiền Thuê + Tiền Cọc). In biên lai.
  - Khách ví: Phải quẹt thẻ trừ ví thẳng.
- **LUỒNG 2 - Nhận Trả / Giám Sát (Khách đem trả lại - Trả lại Cọc)**
  - Chuyển Tab `Nhận Trả & Giám Sát`. Quét mã DT-xxx hoặc Quẹt vòng. Lưới chi tiết đồ đang mượn hiện ra.
  - Khách trả đủ: Nhấn nhanh **`F12 - TRẢ ĐỦ`**, toàn bộ cột Trả=SL thuê và cột Mất=0. Hệ thống đóng Bill và **hoàn đủ tiền cọc**.
  - Khách làm hư đồ: Nhập số lượng làm mất vào cột "Báo Mất". Nhấn LƯU, một Cửa Sổ sẽ hiện ra yêu cầu "NHẬP TỔNG SỐ TIỀN PHẠT/ĐỀN BÙ" dựa trên giá trị đồ đạc. Cọc sẽ lấy trừ đi Phạt rồi trả số dư còn lại cho khách.

---

### 3.4 Xác Nhận Đoàn (Reservation UI)

**Đường dẫn**: Menu → Bán hàng → Xác nhận đoàn

Dành cho tổ Guest Relations hoặc Lễ tân Cổng để Validate một đoàn du lịch trước khi xé vé cho họ vào cổng.

1. Bật Form "Xác Nhận Đoàn".
2. Thu ngân gõ Mã Hợp Đồng (Ví dụ DB-0412) vào khung Search.
3. Nếu Đoàn đủ điều kiện (Chưa tới hạn hoàn tất, Chưa hủy, Chưa xuất), nút `Xác nhận & Xuất Vé` mới hiển thị rõ màu Xanh. 
4. Hệ thống sẽ thay đổi toàn bộ trạng thái để Barcode tổng của đoàn có thể quẹt qua các trạm Kiểm Soát vé.

---

## 4. BẢNG TỪ ĐIỂN PHÍM TẮT CHUYÊN DỤNG (SHORTCUTS)

Hệ thống tối ưu thao tác bằng bàn phím để tăng tốc độ xử lý cho Thao Tác Viên, giảm sự phụ thuộc vào chuột.

| Phím Tắt | Ý Nghĩa / Chức Năng | Form Áp Dụng |
|:---:|:---|:---|
| **F1** | Đưa con trỏ ngay lânh tức vào [Ô Tìm Code/Scanner] | Bán Hàng (POS) |
| **F2** | Chuyển con trỏ vào ô đọc thẻ [RFID ID/ SĐT KH] | Bán Hàng (POS) |
| **F8** | Nhảy vào ô [Khách Đưa] dể gõ nhanh tiền mặt khách tính thối | Bán Hàng (POS) |
| **F9** | Gửi yêu cầu tất toán hóa đơn bằng `Tiền Mặt` | Bán Hàng (POS) |
| **F10** | Gửi yêu cầu trừ tiền giao dịch qua `Ví điện tử RFID` | Bán Hàng (POS) |
| **F11** | Chờ xác nhận giao dịch qua `Chuyển Khoản Mã QR` | Bán Hàng (POS) |
| **F12** | **Full-Return**: Đánh dấu trả đủ tất cả thiết bị không hư hao | Thuê Đồ (Tab Trả Đồ) |
| **Esc** | Ngắt giao dịch / Làm sạch mảng Giỏ hàng và UI | Hầu hết các luồng |

---

## 5. XỬ LÝ NHANH CÁC SỰ CỐ SPRINT 3 (TROUBLE-SHOOTING)

| Cảnh báo UI | Nguyên Nhân | Giải pháp khẩn cấp (Fixes) |
|:---|:---|:---|
| Quét RFID/Mã Vạch bị lỗi dấu (VD: VÉ -> VW, 12 -> 1ă) | Phím gõ Emulation bị dính tiếng Việt | **Rất Quan Trọng:** Bấm `Ctrl+Shift` hoặc tùy chỉnh Unikey sang E (Tiếng Anh) ngay khi đang để trỏ chuột ở POS. |
| Form Ticket Gate màn hình đen thui | OS Chặn/ Device in use | Vào Setting Windows > Privacy > Camera. Cho phép `Desktop App access camera`. Reset form. |
| Pos báo "Số lượng quá hạn mức tồn kho" | Kho cạn nguồn cung loại Tạp Hóa | Chuyển comboBox Kho qua Kho phụ chờ cấp hàng, hoặc nhờ IT bypass nhập kho. Vé không có Limit trừ khi là Combo. |
| Cổng báo "Lập Lại Vé (Already Scanned)" | Khách chuyền vé qua rào / Quét lặp lần 2 | Vặn hỏi người cầm vé, hệ thống Lịch Sử sẽ lưu thời gian cực tiểu của cả 2 lần quét. |
| Mất giao dịch do Click 'Thoát' giữa chừng | Sự kiện FormClosing không lưu Tạm Giỏ | Sprint này UI không hỗ trợ Auto-save Draft Pos. Người dùng bị bắt ấn `CÓ` khi hỏi "Thoát và hủy đơn?". Hãy cẩn trọng. |

---

## 6. LIÊN HỆ ĐỘI NGŨ (SUPPORT LINE)

Trong trường hợp luồng giao dịch bị gián đoạn, cổng soát vé treo:
- **Team**: Nhóm NguyenTanNhi (Kỹ Sư Phần Mềm Nội Bộ)
- **Tầng hỗ trợ**: Trực tiếp Level-1 / Level-2 Database Fix
- **Hệ thống Ticket**: Confluence JIRA #Sprint3 Board.
