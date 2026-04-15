# Software Requirement Specification — Sprint 3

**Project Name:** Hệ thống Quản lý Công viên Giải trí (Amusement Park Management)  
**Project Code:** APM  
**Document Code:** APM_SRS_Sprint3_v1.0  
**Location/Date:** Tp.Hồ Chí Minh, tháng 04/2026

---

## RECORD OF CHANGE
*A - Added   M - Modified   D - Deleted*

| Effective Date | Changed Items | A / M / D | Change Description | New Version |
|---|---|---|---|---|
| 12-Apr-26 | First release | A | Khởi tạo SRS Sprint 3 – POS, Kiểm Soát Vé, Đoàn Khách, Thuê Đồ | 1.0 |

---

## SIGNATURE PAGE
| Role | Name | Date |
|---|---|---|
| **ORIGINATOR** | Bùi Trí Nguyên, Bùi Thị Yến Nhi, Đỗ Duy Tấn | 12-Apr-26 |
| **REVIEWERS** | | |
| **APPROVAL** | Phan Gia Phước | |

---

## TABLE OF CONTENT

I. BÁN HÀNG TẠI QUẦY (POS)  
   I.9. Form con: Phát vé điện tử (Popup sau thanh toán)  
II. KIỂM SOÁT VÉ TẠI CỔNG  
III. XÁC NHẬN ĐOÀN KHÁCH  
IV. CHO THUÊ ĐỒ  
V. YÊU CẦU KHÁC  
   V.1. Menu chính  
   V.2. Phím tắt  
   V.3. Định dạng dữ liệu  
   V.4. Cơ sở dữ liệu  
   V.5. Danh sách thông báo  

---

## I. BÁN HÀNG TẠI QUẦY (POS)

Phân hệ bán hàng tại quầy phục vụ hoạt động thu ngân chính của công viên. Nhân viên sử dụng để bán vé, thức ăn nước uống, lưu niệm và combo cho khách hàng. Hỗ trợ thanh toán tiền mặt, ví điện tử (RFID) và chuyển khoản.

### 1. Overview

Màn hình bán hàng cho phép nhân viên thu ngân: quét mã sản phẩm bằng máy quét hoặc chọn tay từ danh sách, thêm vào giỏ hàng, chỉnh số lượng, đổi đơn vị tính, áp dụng chiết khấu/tích điểm cho khách thành viên, rồi thanh toán. Sau khi thanh toán xong, nếu đơn hàng có vé thì tự động mở cửa sổ phát vé điện tử (xem mục I.9).

### 2. Actors

- Nhân viên Thu ngân (Cashier)
- Quản lý ca trực (Shift Manager)

### 3. Use-case diagram

```
   ┌─────────────────────────────────────────────────────────────────────┐
   │                    PHÂN HỆ BÁN HÀNG TẠI QUẦY (POS)               │
   │                                                                    │
   │                                                                    │
   │  [Nhân viên        UC-P01                    UC-P02                │
   │   Thu ngân] ─────── (Quét mã          ─────── (Thanh toán          │
   │       │              sản phẩm)         │       đơn hàng)           │
   │       │                                │           │               │
   │       │             UC-P03             │           │               │
   │       ├──────────── (Nhận diện         │      ┌────┤               │
   │       │              khách hàng)       │      │    │               │
   │       │                  ▲             │      │    │               │
   │       │                  │             │      │    ▼               │
   │       │           <<include>>          │      │  UC-P05            │
   │       │                  │             │      │  (Phát vé          │
   │       │                  │             │      │   điện tử)         │
   │       │             UC-P04             │      │  - - <<extend>> - -│
   │       └──────────── (Thanh toán        │      │  Extension Point:  │
   │                      bằng ví RFID)     │      │  Đơn có sản phẩm  │
   │                                        │      │  loại "Vé"        │
   │                                        │      │                   │
   │                                        │      │  UC-P06            │
   │                                        │      └─ (Phục vụ          │
   │                                        │         đoàn khách)       │
   │                                        │       - - <<extend>> - -  │
   │                                        │       Extension Point:    │
   │                                        │       Quét mã "BK-..."    │
   │                                        │                           │
   │                              UC-P07    │                           │
   │  [Quản lý       ──────────── (Huỷ đơn  │                           │
   │   ca trực]                    hàng)     │                           │
   │                                        │                           │
   └─────────────────────────────────────────────────────────────────────┘
```

**Giải thích quan hệ:**

| ID | Use-case (Verb + Noun) | Actor | Quan hệ | Giải thích |
|---|---|---|---|---|
| UC-P01 | Quét mã sản phẩm | Thu ngân | — | Nhân viên quét/chọn SP để thêm giỏ |
| UC-P02 | Thanh toán đơn hàng | Thu ngân | — | Nhân viên chọn phương thức và hoàn tất |
| UC-P03 | Nhận diện khách hàng | Thu ngân | — | Nhân viên nhập mã KH hoặc quẹt thẻ |
| UC-P04 | Thanh toán bằng ví RFID | Thu ngân | <<include>> UC-P03 | Thanh toán ví BẮT BUỘC nhân viên phải nhận diện khách trước |
| UC-P05 | Phát vé điện tử | Thu ngân | <<extend>> UC-P02 | CHỈ KHI đơn có vé, nhân viên in hoặc lưu QR |
| UC-P06 | Phục vụ đoàn khách | Thu ngân | <<extend>> UC-P02 | CHỈ KHI nhân viên quét mã booking "BK-" |
| UC-P07 | Huỷ đơn hàng | Thu ngân, Quản lý | — | Nhân viên huỷ đơn đang soạn |

> **Lưu ý:** Các hành vi nội bộ hệ thống (lưu đơn hàng, trừ tồn kho, tạo phiếu thu, tính chiết khấu, tích điểm…) KHÔNG phải là Use Case vì người dùng không trực tiếp tương tác — hệ thống tự xử lý. Chúng được mô tả tại mục **Business descriptions**.

#### 3.1. Pre-condition

- Nhân viên đã đăng nhập thành công.
- Nhân viên có quyền truy cập chức năng bán hàng (quyền VIEW_POS).
- Sản phẩm đã được tạo sẵn trong hệ thống với giá trong bảng giá.
- Kho hàng đã có tồn kho cho sản phẩm vật lý.

#### 3.2. Post-condition

- Đơn hàng được lưu vào hệ thống với mã duy nhất.
- Tồn kho giảm tương ứng (với sản phẩm vật lý).
- Vé điện tử được phát hành kèm mã vạch/QR (nếu đơn có vé).
- Điểm tích lũy được cộng cho khách thành viên.
- Phiếu thu được tạo (tiền mặt/chuyển khoản) hoặc ví bị trừ (RFID).

#### 3.3. Triggers

- Nhân viên chọn mục "Bán hàng / POS" từ thanh menu chính.

### 4. Workflows

**Tình huống 1 — Bán hàng thông thường (khách vãng lai, tiền mặt)**

| Nhân viên | Hệ thống |
|---|---|
| 1. Mở màn hình Bán hàng. | 2. Hiển thị danh sách sản phẩm dạng thẻ hình ảnh bên trái (chỉ sản phẩm đang bán, lọc ẩn sản phẩm đã xóa/ngừng bán). Giỏ hàng trống bên phải. Con trỏ tự đặt vào ô quét mã. Tên khách hiện "Khách vãng lai". |
| 3. Quét mã vạch sản phẩm bằng máy quét (hoặc gõ mã rồi Enter). Có thể quét nhanh bằng cú pháp "SL*Mã" (ví dụ: 5*VEM001 = thêm 5 cái). | 4. Tìm sản phẩm theo mã. Nếu tìm thấy, hệ thống thêm vào giỏ hàng (số lượng = 1, đơn giá lấy theo bảng giá ngày hiện tại). Nếu quét mã combo thì kiểm tra trạng thái (combo ngừng bán sẽ hiện cảnh báo). Nếu không tìm thấy, hệ thống thông báo "Không tìm thấy sản phẩm". Ô quét tự xóa sạch, sẵn sàng quét tiếp. |
| 5. Nhấp đúp sản phẩm từ lưới bên trái (thay vì quét). | 6. Thêm sản phẩm vào giỏ với số lượng 1. Nếu sản phẩm vật lý mà tồn kho không đủ, hệ thống cảnh báo "chỉ còn [X] trong kho" và không cho thêm. |
| 7. Chỉnh số lượng trực tiếp trên giỏ hàng (gõ số hoặc dùng mũi tên tăng/giảm). | 8. Cập nhật thành tiền. Nếu đặt SL = 0 thì hệ thống tự xóa dòng khỏi giỏ. Nếu tăng SL vượt tồn kho thì giới hạn ở mức tối đa và cảnh báo. |
| 9. Đổi đơn vị tính cho 1 dòng (ví dụ: từ "Lon" sang "Thùng"). | 10. Nếu sản phẩm có nhiều đơn vị thì hiện danh sách chọn. Giá tự cập nhật theo bảng giá đơn vị mới. Nếu sau khi đổi mà vượt tồn kho thì hoàn về đơn vị cũ. |
| 11. Nhập số tiền khách đưa vào ô "Khách đưa" (hoặc bấm nút nhanh 50K, 100K, 200K, 500K, Đưa đủ). | 12. Tự tính tiền thừa = Khách đưa − Tổng tiền. Hiện xanh nếu đủ, đỏ nếu thiếu. Nút thanh toán tiền mặt chỉ bật khi khách đưa ≥ tổng tiền. |
| 13. Nhấn phím F9 (hoặc bấm nút thanh toán tiền mặt). | 14. Hiện hộp thoại xác nhận: tổng đơn gốc, chiết khấu (nếu có), số tiền thực thu, phương thức, số mặt hàng. |
| 15. Nhấn "Có" để xác nhận. | 16. Lưu đơn hàng và chi tiết. Tạo phiếu thu. Trừ tồn kho. Nếu đơn có vé thì tự mở cửa sổ Phát vé (xem mục I.9). Nếu không có vé thì hiện hóa đơn dạng text. Giỏ hàng tự xoá sạch, sẵn sàng đơn kế tiếp. |

**Tình huống 2 — Khách thành viên (RFID), có chiết khấu và tích điểm**

| Nhân viên | Hệ thống |
|---|---|
| 1. Nhập mã khách hàng (SĐT hoặc mã thành viên) vào ô, hoặc nhấn F2 rồi gõ. | 2. Tìm khách theo mã hoặc SĐT. Nếu tìm thấy, hệ thống hiện tên, hạng khách (Thường/VIP/VVIP), chiết khấu tương ứng, số điểm tích lũy hiện có (quy ra tiền). Nếu không tìm thấy thì thông báo lỗi. |
| 3. Quét sản phẩm vào giỏ. | 4. Dòng tóm tắt chiết khấu cập nhật theo thời gian thật bên dưới tổng tiền: hiện "ĐANG CÓ [nguồn] GIẢM X%: -Yđ, THỰC THU: Zđ". Nếu có khuyến mãi gần ngưỡng thì gợi ý: "Mua thêm Xđ để kích hoạt [tên KM]". |
| 5. Nhấn F10 (thanh toán ví RFID). | 6. Tính chiết khấu theo hạng khách (chỉ áp cho Vé/Dịch vụ, không giảm thức ăn nước uống). So sánh với khuyến mãi đang chạy, chọn cái nào lợi hơn cho khách. Nếu khách có điểm tích lũy và trị giá điểm lớn hơn chiết khấu thì hỏi "Bạn có X điểm (=Yđ). Dùng Z điểm giảm Wđ? (Lợi hơn chiết khấu Vđ)". |
| 7. Chọn "Có" (dùng điểm) hoặc "Không" (dùng chiết khấu). | 8. Xác nhận. Trừ tiền ví. Cộng điểm tích lũy mới. Nếu đơn có vé thì mở cửa sổ Phát vé. |

**Tình huống 3 — Phục vụ đoàn khách (suất ăn theo hợp đồng)**

| Nhân viên | Hệ thống |
|---|---|
| 1. Quét mã booking đoàn (bắt đầu bằng "BK-...") vào ô quét mã. | 2. Nhận diện mã đoàn. Kiểm tra booking hợp lệ. Hiện tên đoàn, chiết khấu. Hiện danh sách suất ăn/dịch vụ còn lại theo hợp đồng (ví dụ: "Cơm trưa A: còn 3/5, Nước: còn 5/5"). Hỏi "Thêm 1 suất vào giỏ?". |
| 3. Nhấn "Có". | 4. Thêm các mục đoàn vào giỏ với giá 0đ (đã thanh toán theo hợp đồng). Tên hiện dạng "[ĐOÀN] Tên dịch vụ". Thanh tên khách đổi thành "ĐOÀN: [tên] (CK X%)". |
| 5. Thanh toán (bất kỳ phương thức). | 6. Trừ quota dịch vụ đoàn. Tạo đơn hàng ghi nợ công ty (tổng = 0đ). Nếu có dịch vụ đã hết quota thì hệ thống cảnh báo. |

**Tình huống 4 — Xóa dòng / Hủy đơn**

| Nhân viên | Hệ thống |
|---|---|
| 1. Nhấn phím Delete khi đang chọn 1 dòng trong giỏ. | 2. Hỏi xác nhận "Xóa mặt hàng này?". Nếu chọn "Có" thì xóa dòng và cập nhật tổng. |
| 3. Nhấn nút "Xóa giỏ hàng". | 4. Hỏi xác nhận "Xóa toàn bộ giỏ hàng?". Nếu chọn "Có" thì xoá sạch giỏ, NHƯNG giữ nguyên thông tin khách. |
| 5. Nhấn Esc (Hủy đơn). | 6. Hỏi xác nhận "Hủy đơn hàng hiện tại và làm mới khách hàng?". Nếu chọn "Có" thì xóa giỏ, xóa thông tin khách, tên trở về "Khách vãng lai". |

**Tình huống 5 — Thanh toán chuyển khoản (F11)**

| Nhân viên | Hệ thống |
|---|---|
| 1. Nhấn F11. | 2. Xóa ô khách đưa (không cần nhập tiền mặt). Tiến hành thanh toán giống tiền mặt nhưng phương thức ghi là "Chuyển khoản". |

### 5. Activity Flow — Luồng hoạt động chính POS

```
[Bắt đầu]
    │
    ▼
(Mở màn hình POS) ── tải danh sách SP ──→ (Hiển thị có giá theo ngày)
    │
    ▼
(Quét/Chọn SP) ────→ SP tìm thấy? ─── Không ──→ [Thông báo lỗi] → quay lại
    │ Có
    ▼
(Kiểm tra loại SP) ─── Vật lý? ─── Còn tồn kho? ─── Không ──→ [Cảnh báo hết kho]
    │ Có / Không phải vật lý                                        │
    ▼                                                               │
(Thêm vào giỏ, cập nhật tổng) ◄────────────────────────────────────┘
    │
    ▼
(Tiếp tục quét?) ─── Có ──→ quay lại "Quét/Chọn SP"
    │ Không
    ▼
(Nhấn nút thanh toán F9 / F10 / F11)
    │
    ▼
(Tính chiết khấu + điểm + khuyến mãi) ──→ (Hiện xác nhận)
    │
    ▼
(Xác nhận?) ─── Không ──→ quay lại giỏ
    │ Có
    ▼
(Lưu đơn hàng) → (Tạo phiếu thu / Trừ ví) → (Trừ tồn kho)
    │
    ▼
(Đơn có vé?) ─── Có ──→ [Mở cửa sổ Phát Vé (mục I.9)]
    │ Không                     │
    ▼                           ▼
(Hiện hóa đơn text)     (Hiển thị thẻ vé + mã vạch)
    │                           │
    ▼                           ▼
(Cộng điểm cho KH nếu có) → (Xóa giỏ, sẵn sàng đơn mới) → [Kết thúc]
```

### 6. External interfaces

#### 6.1. Prototype — Màn hình bán hàng chính

```
╔══════════════════════════════════════════════════════════════════════════════╗
║  🛒 BÁN HÀNG TẠI QUẦY                                              [X]  ║
╠══════════════════════════════════════════════════════════════════════════════╣
║  ┌─── SẢN PHẨM ─────────────────────────┐ ┌─── GIỎ HÀNG (3) ──────────┐ ║
║  │                                        │ │ Khách: Nguyễn Văn A       │ ║
║  │ Quét mã: [__________________] 📷 Cam  │ │ [VIP -10%] | 500đ (=5K đ) │ ║
║  │ Tìm SP:  [__________________]          │ │ Mã KH: [______] [🔍]     │ ║
║  │                                        │ │                            │ ║
║  │ [Tất cả] [Vé] [F&B] [Thuê]            │ │ ┌──────────────────────┐  │ ║
║  │                                        │ │ │ Tên    │ĐVT│SL│T.Tiền│  │ ║
║  │ ┌──────┐ ┌──────┐ ┌──────┐             │ │ ├────────┼───┼──┼──────┤  │ ║
║  │ │ ☕   │ │ 🎫   │ │ 🍔   │             │ │ │VE NL   │Vé │2 │200K  │  │ ║
║  │ │Nước  │ │Vé NL │ │Burger│             │ │ │Nước cam│Lon│3 │ 45K  │  │ ║
║  │ │15Kđ  │ │100Kđ │ │ 35Kđ │             │ │ │Burger  │Cái│1 │ 35K  │  │ ║
║  │ └──────┘ └──────┘ └──────┘             │ │ │        │   │  │TỔNG: │  │ ║
║  │ (Nhấp đúp để thêm vào giỏ)            │ │ │        │   │  │280K  │  │ ║
║  │                                        │ │ └──────────────────────┘  │ ║
║  │ Kho xuất: [▼ Kho 1              ]     │ │                            │ ║
║  └────────────────────────────────────────┘ │  ĐANG CÓ [VIP] GIẢM 10%:  │ ║
║                                             │  -20,000đ, THỰC THU: 260,000đ  │ ║
║                                             │                            │ ║
║                                             │    260,000 VNĐ             │ ║
║                                             │                            │ ║
║                                             │ K.đưa: [____] [50K][100K] │ ║
║                                             │         [200K][500K][Đủ]  │ ║
║                                             │ Thừa:   40,000 đ          │ ║
║                                             │                            │ ║
║                                             │ [💵 F9] [💳 F10] [🏦 F11] │ ║
║                                             │ [🗑 Xóa giỏ] [❌ Hủy Esc] │ ║
║                                             └────────────────────────────┘ ║
╚══════════════════════════════════════════════════════════════════════════════╝
```

#### 6.2. Screen description

Màn hình chia 2 phần: bên trái là danh sách sản phẩm (có thể lọc theo nhóm), bên phải là giỏ hàng + thanh toán.

| No | Field name | Loại | Bắt buộc | Kiểu dữ liệu | Mặc định | Mô tả |
|---|---|---|---|---|---|---|
| **Vùng Sản phẩm (Trái)** |
| 1 | Ô quét mã | TextBox | No | Text | Trống | Quét hoặc gõ mã vạch sản phẩm. Hỗ trợ cú pháp "SL*Mã" (ví dụ: 5*VEM001 = thêm 5 cái). Sau khi quét xong, ô tự xóa sạch. |
| 2 | Ô tìm kiếm | TextBox | No | Text | Trống | Gõ tên để lọc sản phẩm tức thì (không cần nhấn Enter) |
| 3 | Nút nhóm: Tất cả / Vé / F&B / Thuê | Button (nhóm 4 nút) | — | — | Tất cả | Lọc sản phẩm theo nhóm. Nút đang chọn đổi màu nổi bật. |
| 4 | Lưới sản phẩm | DataGridView (dạng thẻ hình ảnh) | — | — | — | Hiện ảnh, tên, giá. Nhấp đúp để thêm vào giỏ. Giá hiển thị theo bảng giá ngày hiện tại. |
| 5 | Nút bật/tắt camera | Button | — | — | — | Bật/tắt webcam để quét mã vạch thay máy quét |
| 6 | Khung camera | PictureBox | — | — | Ẩn | Hiển thị hình ảnh webcam khi bật. Tự nhận diện mã vạch. |
| 7 | Kho xuất | ComboBox | Yes | Text | Kho 1 | Chọn kho để trừ tồn khi bán hàng vật lý |
| **Vùng Giỏ hàng (Phải)** |
| 8 | Thông tin khách | Label | — | Text | "Khách vãng lai" | Hiện tên khách, hạng, chiết khấu, điểm khi gắn KH. Hiện "ĐOÀN: [tên]" khi quét booking. |
| 9 | Ô nhập mã khách | TextBox | No | Text | Trống | Nhập SĐT hoặc mã thành viên. Nhấn tìm hoặc F2 rồi gõ. |
| 10 | Nút tìm khách | Button | — | — | — | Tìm khách theo mã/SĐT |
| 11 | Lưới giỏ hàng | DataGridView | — | — | — | Cột: Tên SP, ĐVT (chọn được nếu có quy đổi), SL (sửa được bằng mũi tên), Thành tiền, nút Xoá. Dòng cuối: TỔNG cộng. |
| 12 | Dòng tóm tắt chiết khấu | Label | — | Text | Trống | Cập nhật thời gian thật: "ĐANG CÓ [nguồn] GIẢM X%: -Yđ, THỰC THU: Zđ" hoặc gợi ý khuyến mãi. |
| 13 | Tổng tiền (cỡ lớn) | Label (font 32pt) | — | Số | 0 VNĐ | Hiện số tiền thực thu (sau chiết khấu lớn nhất) |
| 14 | Ô khách đưa | TextBox | No | Số | Trống | Nhập số tiền khách đưa. Tự format có dấu phẩy (150,000). |
| 15 | Nút tiền nhanh | Button (nhóm 5 nút) | — | — | — | 50K, 100K, 200K, 500K, Đưa đủ. Nhấn sẽ tự điền vào ô khách đưa. |
| 16 | Tiền thừa | Label | — | Số | 0đ | Xanh: "40,000 đ". Đỏ: "CÒN THIẾU: 10,000 đ" |
| 17 | Thanh toán Tiền mặt (F9) | Button | — | — | Disabled | Chỉ bật khi khách đưa ≥ tổng tiền. Nhấn Enter ở ô khách đưa cũng kích hoạt. |
| 18 | Thanh toán Ví RFID (F10) | Button | — | — | — | Trừ tiền trong ví điện tử. Yêu cầu đã gắn khách. |
| 19 | Thanh toán Chuyển khoản (F11) | Button | — | — | — | Ghi phương thức "Chuyển khoản" |
| 20 | Xoá giỏ | Button | — | — | — | Xoá toàn bộ giỏ, giữ khách. Cần quyền MANAGE_POS. |
| 21 | Huỷ đơn (Esc) | Button | — | — | — | Xoá giỏ + bỏ chọn khách. Cần quyền MANAGE_POS. |

### 7. Business descriptions

| No | Tên quy tắc | Mô tả chi tiết |
|---|---|---|
| 1 | **Giá động theo ngày** | Đơn giá sản phẩm tự lấy từ bảng giá theo thời điểm hiện tại (ngày thường, cuối tuần, ngày lễ). Áp dụng cho cả lưới sản phẩm lẫn giỏ hàng. Nếu sản phẩm chưa thiết lập giá thì hiện 0đ. |
| 2 | **Kiểm tra tồn kho** | Chỉ áp dụng cho sản phẩm vật lý (thức ăn nước uống, đồ lưu niệm). Vé và Dịch vụ KHÔNG kiểm tra tồn. Nếu thêm vượt tồn thì cảnh báo và chặn. Nếu chỉnh SL vượt tồn thì tự giảm về mức tối đa cho phép. |
| 3 | **Bảng chiết khấu theo hạng khách** | Xem bảng chi tiết bên dưới. Chiết khấu chỉ áp cho Vé và Dịch vụ. Thức ăn nước uống KHÔNG bao giờ được giảm giá. |
| 4 | **So sánh nguồn giảm giá** | Hệ thống so sánh 3 nguồn: (a) Chiết khấu hạng khách, (b) Khuyến mãi đang chạy, (c) Điểm tích lũy, sau đó chọn cái nào có lợi nhất cho khách. KHÔNG cộng dồn — chỉ dùng 1 nguồn. |
| 5 | **Tích điểm** | Sau thanh toán, nếu có khách thành viên: Điểm mới = Số tiền thực thu ÷ 1.000 × Hệ số hạng. Điểm tích từ số tiền SAU giảm giá (tức khách thực xuất). |
| 6 | **Dùng điểm** | Nếu khách chọn dùng điểm: trừ điểm khỏi tài khoản, ghi nhận lý do "Dùng điểm thanh toán [mã đơn]". Điểm và chiết khấu loại trừ nhau. |
| 7 | **Mã đơn hàng** | Tự sinh theo công thức: "DH-" + ngày giờ (yyMMddHHmmss) + "-" + 4 ký tự ngẫu nhiên. Đảm bảo duy nhất. |
| 8 | **Phát vé tự động** | Nếu đơn hàng có sản phẩm loại "Vé" thì tự mở cửa sổ Phát vé (mục I.9) ngay sau thanh toán thành công. |
| 9 | **Đổi đơn vị tính** | Sản phẩm có quy đổi (ví dụ: Lon sang Thùng = 24 lon), nhân viên chọn ĐVT trên giỏ. Giá tự cập nhật. Sau đổi, hệ thống kiểm tra lại tồn kho theo đơn vị cơ bản. |
| 10 | **Giao dịch cần nhân viên** | Mỗi đơn hàng bắt buộc gắn người tạo. Nếu phiên hết hạn thì buộc khởi động lại ứng dụng. |
| 11 | **Phục vụ đoàn** | Khi quét mã booking "BK-...": suất ăn/dịch vụ thêm vào giỏ với giá 0đ. Sau thanh toán thì trừ quota, tạo phiếu ghi nợ công ty (mã "DHD-..."). |
| 12 | **Gợi ý khuyến mãi** | Nếu tổng đơn còn thiếu dưới 100,000đ để kích hoạt khuyến mãi thì hiện dòng gợi ý vàng: "Mua thêm Xđ để kích hoạt [tên KM]". |
| 13 | **Tiền nhanh (Quick Cash)** | 5 nút: 50K, 100K, 200K, 500K, Đưa đủ. Nhấn sẽ tự điền ô khách đưa. Nếu nhấn Enter ở ô khách đưa thì kích hoạt nút F9 (chỉ khi đã đủ tiền). |
| 14 | **Phân quyền** | Nhân viên không có quyền VIEW_POS thì toàn bộ màn hình bị khóa. Nút Hủy đơn và Xóa giỏ đòi hỏi quyền MANAGE_POS. |

#### 7.1. Bảng chiết khấu theo hạng khách hàng

| Hạng khách | Tỷ lệ chiết khấu | Hệ số tích điểm | Ghi chú |
|---|---|---|---|
| Cá nhân (Thường) | 0% | ×1 | Mặc định khi đăng ký |
| Học sinh – Sinh viên | 5% | ×1 | Cần xác minh |
| VIP | 10% | ×2 | Nâng hạng khi đủ điểm |
| VVIP | 25% | ×2 | Hạng cao nhất |
| Nội bộ (Nhân viên) | 0% | ×1 | Dùng nội bộ |

### 8. User-validation rules

| No | Quy tắc | Thông báo | Mã |
|---|---|---|---|
| 1 | Giỏ hàng trống khi thanh toán | "Giỏ hàng trống!" | ERR_POS_EMPTY_CART |
| 2 | Thanh toán ví nhưng chưa gắn khách | "Thanh toán ví RFID bắt buộc phải quét thẻ khách hàng trước!" | ERR_POS_NO_RFID |
| 3 | Số dư ví không đủ | "Số dư ví không đủ! Cần: [X]đ — Có: [Y]đ" | ERR_POS_INSUFFICIENT_BALANCE |
| 4 | Tồn kho không đủ | "Sản phẩm '[tên]' chỉ còn tồn [X] trên toàn hệ thống!" | ERR_POS_OUT_OF_STOCK |
| 5 | Mã sản phẩm không tìm thấy | "Không tìm thấy SP/Combo: [mã]" | ERR_POS_PRODUCT_NOT_FOUND |
| 6 | Combo đang ngừng bán | "Combo '[tên]' đang ở trạng thái [TT], không được phép xuất bán!" | ERR_POS_COMBO_INACTIVE |
| 7 | Phiên đăng nhập hết hạn | "Phiên làm việc không hợp lệ! Vui lòng khởi động lại." | ERR_POS_SESSION_EXPIRED |
| 8 | Đoàn hết quota ăn uống | "Đoàn '[tên]' không có suất ăn hoặc đã dùng hết!" | ERR_POS_BOOKING_NO_QUOTA |
| 9 | Khách hàng không tìm thấy | "Không tìm thấy khách hàng!" | ERR_POS_CUSTOMER_NOT_FOUND |
| 10 | Booking đoàn không hợp lệ | "[Lý do lỗi booking]" | ERR_POS_BOOKING_INVALID |
| 11 | Khách đưa chưa đủ (tiền mặt) | "Khách chưa đưa đủ tiền!" | ERR_POS_INSUFFICIENT_CASH |
| 12 | Đổi ĐVT vượt tồn kho | "Tồn kho không đủ để đáp ứng [X] [ĐVT]!" | ERR_POS_UNIT_STOCK |

### 9. Form con: Phát vé điện tử (Popup sau thanh toán)

Form này **tự động mở** dạng popup ngay sau khi thanh toán thành công tại POS, nếu đơn hàng có ít nhất 1 sản phẩm loại "Vé".

#### 9.1. Overview

Hiển thị danh sách vé vừa phát hành dưới dạng thẻ (card). Mỗi thẻ có: icon, tên vé, mã vạch/QR, số lượt. Hỗ trợ in hàng loạt, in lại từng vé, và lưu QR ra file ảnh.

#### 9.2. Actors

- Nhân viên Thu ngân (tự động sau thanh toán)

#### 9.3. Workflows

| Nhân viên | Hệ thống |
|---|---|
| (Thanh toán xong ở POS) | 1. Tự mở cửa sổ popup. Hiển thị tiêu đề "PHÁT VÉ — [mã đơn]", phụ đề "Khách: [tên] ∣ Tổng: [X] vé ∣ TT: [phương thức]". Mỗi vé hiển thị dạng thẻ card: icon theo loại dịch vụ, tên, mã, số lượt. |
| 2. (Thanh toán tiền mặt) Nhấn "In tất cả". | 3. Mở cửa sổ xem trước in. Mỗi vé in kèm: tên công viên, mã đơn, tên dịch vụ, mã vạch thật (CODE_128), số lượt, lời chúc. 2 vé mỗi hàng. Thẻ chuyển xanh (đã in). |
| 4. Nhấp chọn 1 thẻ, nhấn "In lại". | 5. In riêng 1 vé đó. |
| 6. Nhấn "Lưu QR". | 7. Tạo thư mục trên Desktop tên "QR_Ve_[mã đơn]". Lưu mỗi vé thành 1 file QR code PNG (400×400 pixel). Mở thư mục kết quả. |
| 8. Nhấn "Xong". | 9. Đóng popup. Quay lại POS. |

#### 9.4. Screen description

| No | Field name | Loại | Mô tả |
|---|---|---|---|
| 1 | Tiêu đề | Label | "🎫 PHÁT VÉ — [mã đơn hàng]" |
| 2 | Phụ đề | Label | "Khách: [tên] ∣ Tổng: [X] vé ∣ TT: [phương thức]" |
| 3 | Danh sách thẻ vé | FlowLayout (danh sách card) | Mỗi card: icon dịch vụ, tên vé, mã vạch, số lượt. Nhấp chọn để in riêng. |
| 4 | In tất cả | Button | In toàn bộ vé (chỉ hiện khi thanh toán tiền mặt/CK) |
| 5 | In lại | Button | In riêng vé đang chọn (chỉ hiện khi thanh toán tiền mặt/CK) |
| 6 | Lưu QR | Button | Lưu tất cả vé thành ảnh QR code trên Desktop |
| 7 | Xong | Button | Đóng popup |

#### 9.5. Business descriptions

| No | Tên quy tắc | Mô tả |
|---|---|---|
| 1 | Tự mở khi có vé | Popup chỉ xuất hiện nếu đơn hàng có ≥ 1 vé điện tử. |
| 2 | Ẩn nút in khi RFID | Thanh toán bằng ví RFID → vé đã lưu trong hệ thống, không cần giấy → ẩn nút "In tất cả" và "In lại". Vẫn có thể lưu QR. |
| 3 | Mã vạch thật | In sử dụng mã vạch CODE_128 thật (không phải hình giả). QR code kích thước 400×400px, dễ quét bằng máy quét hoặc điện thoại. |
| 4 | Icon theo dịch vụ | Mỗi loại vé/dịch vụ có icon riêng (ví dụ: vé vào cổng = hình vé, trò chơi = hình tàu lượn, v.v.). |

### 10. Related use-cases

- Quản lý Sản phẩm & Bảng giá (Sprint 1)
- Quản lý Khách hàng & Ví điện tử (Sprint 2)
- Kiểm Soát Vé tại Cổng (Sprint 3 — mục II)
- Xác nhận Đoàn Khách (Sprint 3 — mục III)

---

## II. KIỂM SOÁT VÉ TẠI CỔNG

Phân hệ kiểm soát vé phục vụ nhân viên gác cổng tại từng khu vực/trò chơi. Nhân viên quét vé (mã vạch hoặc QR) để xác minh khách có quyền vào khu vực hay không.

### 1. Overview

Màn hình hiển thị trạng thái sẵn sàng, chờ nhân viên quét mã vé. Khi quét xong, hệ thống kiểm tra tính hợp lệ của vé (đúng khu vực? đúng trò chơi? còn lượt không?) và hiển thị kết quả bằng **màu sắc lớn rõ ràng** kèm hiệu ứng chớp viền.

### 2. Actors

- Nhân viên Gác cổng (Gate Staff)

### 3. Use-case diagram

```
   ┌─────────────────────────────────────────────────────────────────┐
   │                 PHÂN HỆ KIỂM SOÁT VÉ TẠI CỔNG                 │
   │                                                                │
   │  [Nhân viên         UC-G01                  UC-G02             │
   │   Gác cổng] ─────── (Chọn khu vực    ────── (Quét mã          │
   │                       trực)            │      vé khách)        │
   │                                        │         │             │
   │                      UC-G03            │         │             │
   │               ────── (Xem lịch sử     │         ▼             │
   │               │       quét)            │       UC-G04          │
   │               │                        │      (Quét mã         │
   │               │      UC-G05            │       booking đoàn)   │
   │               └───── (Quét mã          │     - - <<extend>> -  │
   │                       bằng camera)     │     Extension Point:  │
   │                                        │     Mã bắt đầu "BK-" │
   │                                        │                       │
   └─────────────────────────────────────────────────────────────────┘
```

**Giải thích quan hệ:**

| ID | Use-case (Verb + Noun) | Actor | Quan hệ | Giải thích |
|---|---|---|---|---|
| UC-G01 | Chọn khu vực trực | Gác cổng | — | Nhân viên chọn khu vực và trò chơi đang gác |
| UC-G02 | Quét mã vé khách | Gác cổng | — | Nhân viên quét/nhập mã vé của khách |
| UC-G03 | Xem lịch sử quét | Gác cổng | — | Nhân viên xem danh sách các lần quét gần đây |
| UC-G04 | Quét mã booking đoàn | Gác cổng | <<extend>> UC-G02 | CHỈ KHI mã bắt đầu "BK-" thì hệ thống nhận diện đoàn |
| UC-G05 | Quét mã bằng camera | Gác cổng | — | Nhân viên bật webcam để quét thay máy quét |

> **Lưu ý:** Các hành vi nội bộ (xác minh vé, trừ lượt, ghi log, đổi màu trạng thái…) KHÔNG phải là Use Case — hệ thống tự xử lý. Chúng được mô tả tại mục **Business descriptions** và **Bảng mã màu trạng thái**.

#### 3.1. Pre-condition

- Nhân viên đã đăng nhập thành công.
- Đã có ít nhất 1 khu vực và trò chơi trong hệ thống.
- Khách hàng đã mua vé (từ POS hoặc đặt trước).

#### 3.2. Post-condition

- Số lượt còn lại trên vé giảm 1 (nếu vé hợp lệ).
- Lịch sử quét được ghi nhận trong danh sách.
- Quota đoàn được trừ (nếu quét mã booking đoàn).

#### 3.3. Triggers

- Nhân viên chọn mục "Kiểm soát vé" từ thanh menu chính.

### 4. Workflows

**Tình huống 1 — Quét vé thông thường (hợp lệ)**

| Nhân viên | Hệ thống |
|---|---|
| 1. Mở màn hình kiểm soát vé. | 2. Hiển thị nền tối. Bảng trạng thái hiện "SẴN SÀNG" với icon mã vạch lớn. Bộ đếm: Hợp lệ = 0, Từ chối = 0. Load danh sách khu vực vào ô chọn. |
| 3. Chọn khu vực đang gác (bắt buộc). | 4. Load danh sách trò chơi thuộc khu vực đó. Thêm 1 mục "Cổng chung (tất cả trò)" ở đầu. |
| 5. Chọn trò chơi cụ thể hoặc để "Cổng chung". | 6. Sẵn sàng quét. |
| 7. Quét mã vé của khách (bằng máy quét, camera, hoặc gõ tay vào ô demo). | 8. Kiểm tra: (a) Tìm vé → (b) So khớp khu vực → (c) So khớp trò chơi (nếu chọn cụ thể) → (d) Kiểm tra còn lượt → (e) Trừ 1 lượt. Kết quả: toàn bộ bảng trạng thái chuyển XANH, icon dấu tích, hiện "MỜI VÀO" + tên dịch vụ. Thẻ thông tin vé bên phải hiện: mã vé, tên, khu vực, lượt còn lại, ngày mua. Viền màn hình chớp xanh 2 lần. Bộ đếm "Hợp lệ" +1. Lịch sử ghi: [giờ] [mã] "✅ HỢP LỆ". |
| 9. Cho khách đi qua. | 10. Sau 3 giây, bảng trạng thái tự trở về "SẴN SÀNG". |

**Tình huống 2 — Vé sai khu vực**

| Nhân viên | Hệ thống |
|---|---|
| 1. Quét mã vé. | 2. Tìm thấy vé nhưng khu vực trên vé khác với khu vực đang gác. Bảng chuyển sang ĐỎ, icon bản đồ, "SAI KHU VỰC — Vé không thuộc trạm: [tên khu]". Thẻ vé: kết luận "SAI KHU" (vàng). Viền chớp đỏ. Lịch sử: "⛔ SAI KHU". |

**Tình huống 3 — Vé sai trò chơi**

| Nhân viên | Hệ thống |
|---|---|
| 1. Quét mã vé. | 2. Vé đúng khu vực nhưng sai trò chơi đang gác (khi chọn trò cụ thể). Bảng chuyển sang VÀNG, icon cảnh báo, "SAI TRÒ CHƠI — Vé: [tên vé] — Trạm: [tên trò]". |

**Tình huống 4 — Vé hết lượt**

| Nhân viên | Hệ thống |
|---|---|
| 1. Quét mã vé. | 2. Vé tồn tại, đúng khu vực, nhưng lượt = 0. Bảng chuyển sang ĐỎ, icon dấu X, "TỪ CHỐI — [tên vé] — hết lượt". Thẻ vé: lượt = "0". |

**Tình huống 5 — Không tìm thấy mã**

| Nhân viên | Hệ thống |
|---|---|
| 1. Quét mã vé. | 2. Mã không có trong hệ thống. Bảng chuyển sang XÁM, icon dấu hỏi, "KHÔNG TÌM THẤY — Mã: [mã]". Thẻ vé không hiển thị. |

**Tình huống 6 — Quét mã booking đoàn (hợp lệ)**

| Nhân viên | Hệ thống |
|---|---|
| 1. Quét mã bắt đầu bằng "BK-...". | 2. Nhận diện đây là mã đoàn. Kiểm tra: booking hợp lệ và còn quota vé. Nếu hợp lệ thì bảng chuyển sang XANH, "MỜI VÀO — ĐOÀN: [tên đoàn]". Thẻ vé: mã booking, tên đoàn, quota "X/Y". Trừ 1 quota vé. |

**Tình huống 7 — Booking đoàn lỗi**

| Nhân viên | Hệ thống |
|---|---|
| 1. Quét mã booking. | 2. Nếu đoàn hết quota vé thì bảng chuyển sang ĐỎ, "HẾT LƯỢT ĐOÀN". Nếu booking đã hủy hoặc hết hạn thì bảng chuyển sang VÀNG, "BOOKING LỖI — [lý do]". |

### 5. Activity Flow — Luồng quét vé

```
[Bắt đầu]
    │
    ▼
(Chọn Khu vực + Trò chơi) → (SẴN SÀNG)
    │
    ▼
(Quét mã) → Mã bắt đầu "BK-"?
    │              │
    │ Không         │ Có
    ▼              ▼
(Tìm vé?)     (Kiểm tra Booking)
    │ Không         │
    ▼              ▼
[XÁM: KHÔNG    Hợp lệ + Còn quota?
 TÌM THẤY]        │ Có        │ Không
    │              ▼           ▼
    │         [XANH: MỜI   [ĐỎ: HẾT LƯỢT
    │          VÀO ĐOÀN]    ĐOÀN / LỖI BK]
    │
    ▼ Tìm thấy
(Đúng khu vực?)
    │ Không         │ Có
    ▼              ▼
[ĐỎ: SAI      (Đúng trò chơi?)
 KHU VỰC]         │ Không      │ Có
                   ▼           ▼
              [VÀNG: SAI   (Còn lượt?)
               TRÒ CHƠI]      │ Không   │ Có
                               ▼        ▼
                          [ĐỎ: HẾT  [XANH: MỜI VÀO]
                           LƯỢT]    → Trừ 1 lượt
    │
    ▼ (tất cả nhánh)
(Ghi lịch sử + Cập nhật bộ đếm)
    │
    ▼
(Chờ 3 giây) → (SẴN SÀNG) → quay lại "Quét mã"
```

### 6. External interfaces

#### 6.1. Prototype — Kiểm soát vé

```
╔══════════════════════════════════════════════════════════════════════════════╗
║  🛡️ KIỂM SOÁT VÉ                                                   [X]  ║
║  Khu vực: [▼ Khu Nước        ]  Trò chơi: [▼ Cổng chung (tất cả trò) ]  ║
╠══════════════════════════════════════════════════════════════════════════════╣
║                                                                            ║
║  ┌─── TRẠNG THÁI ─────────────────────┐  ┌─── THÔNG TIN VÉ ───────────┐  ║
║  │                                     │  │  Mã vé: VE-240412-A3F2      │  ║
║  │          ███████████████             │  │  Tên:   Vé vào khu Nước     │  ║
║  │         ██    ✅     ██              │  │  📍 Khu Nước                │  ║
║  │          ███████████████             │  │  🎫 Lượt còn: 2             │  ║
║  │                                     │  │  📅 12/04/2026 09:30        │  ║
║  │        M Ờ I   V À O               │  │                              │  ║
║  │        Vé vào khu Nước              │  │  ┌────────────────────┐      │  ║
║  │                                     │  │  │   HỢP LỆ           │      │  ║
║  └─────────────────────────────────────┘  │  └────────────────────┘      │  ║
║                                           └──────────────────────────────┘  ║
║                                                                            ║
║  ✅ Hợp lệ: 15     ❌ Từ chối: 3     📊 Tổng quét: 18                    ║
║                                                                            ║
║  Gõ mã: [____________________________] [📷 Bật Cam] [📁 Quét file]       ║
║                                                                            ║
║  ┌─── LỊCH SỬ QUÉT ────────────────────────────────────────────────────┐  ║
║  │  09:30:15  │ VE-240412-A3F2  │ ✅ HỢP LỆ                          │  ║  
║  │  09:29:52  │ VE-240411-B1C3  │ ⛔ SAI KHU                          │  ║
║  │  09:28:10  │ BK-DOAN-001     │ ✅ ĐOÀN 3/5                         │  ║
║  └──────────────────────────────────────────────────────────────────────┘  ║
╚══════════════════════════════════════════════════════════════════════════════╝
```

#### 6.2. Screen description

| No | Field name | Loại | Bắt buộc | Kiểu dữ liệu | Mặc định | Mô tả |
|---|---|---|---|---|---|---|
| **Vùng Header (Trên)** |
| 1 | Khu vực | ComboBox | Yes | Text | — | Danh sách khu vực (Trạm/Cổng) mà nhân viên được phân công. |
| 2 | Trò chơi | ComboBox | Yes | Text | "Cổng chung" | Danh sách trò chơi thuộc khu vực đang chọn. |
| **Vùng Quét mã & Lịch sử (Dưới)** |
| 3 | Ô nhập/quét mã | TextBox | No | Text | Trống | Cho phép gõ tay mã vé hoặc hứng sự kiện từ máy quét (lấy focus mặc định). |
| 4 | Nút Bật Cam | Button | — | — | — | Bật/tắt giao diện webcam để quét mã QR/Barcode thay cho máy quét vật lý. |
| 5 | Nút Chọn Ảnh | Button | — | — | — | Chọn file ảnh mã QR/Barcode từ máy tính tĩnh. |
| 6 | Lưới lịch sử quét | ListView | — | — | — | Lưu lại lịch sử 50 mã vé gần nhất đã xử lý, bao gồm Giờ, Mã Vé, Kết Quả. |
| 7 | Thống kê kết quả | Label | — | Số | 0 | Thống kê số lượng: Hợp lệ, Từ chối, và Tổng số lượt quét trong phiên. |
| **Vùng Phản hồi Trạng thái (Trái)** |
| 8 | Khung Camera | PictureBox | — | — | Ẩn | Khung hiển thị luồng video từ webcam. |
| 9 | Icon Trạng thái | PictureBox | — | — | Icon | Thay đổi linh hoạt theo kết quả quét: Dấu tích, Cảnh báo, Chữ X... |
| 10 | Tiêu đề trạng thái | Label | — | Text | "SẴN SÀNG" | Tiêu đề text lớn, đổi màu theo kết quả (MỜI VÀO, SAI KHU VỰC...). |
| 11 | Thông báo phụ | Label | — | Text | — | Tóm tắt bổ sung kết quả xác minh. |
| **Vùng Thông tin vé (Phải)** |
| 12 | Mã vé | Label | — | Text | "---" | Mã code trên vé (Barcode/QR) vừa quét. |
| 13 | Tên dịch vụ | Label | — | Text | Trống | Loại vé / Trò chơi. |
| 14 | Khu vực áp dụng | Label | — | Text | Trống | Vị trí giới hạn được dùng vé. |
| 15 | Lượt còn lại | Label | — | Số | Trống | Cập nhật số lượt còn lại sau khi quét (Xanh hoặc Đỏ). |
| 16 | Ngày mua | Label | — | Text | Trống | Ngày xuất vé từ phiên bán hàng hoặc hệ thống onine. |
| 17 | Nhãn Kết Luận | Label | — | Text | "N/A" | Badge hiển thị trạng thái tối giản dạng nhãn (HỢP LỆ, KHÔNG TÌM THẤY...). |

### 7. Business descriptions

| No | Tên quy tắc | Mô tả |
|---|---|---|
| 1 | Kiểm tra khu vực | Vé chỉ hợp lệ nếu khu vực ghi trên vé trùng với khu vực nhân viên đang gác. |
| 2 | Kiểm tra trò chơi | Nếu nhân viên chọn trò cụ thể (không phải "Cổng chung") thì vé phải đúng trò chơi đó. Nếu chọn "Cổng chung" thì chỉ cần đúng khu vực. |
| 3 | Trừ lượt | Mỗi lần quét hợp lệ thì lượt còn lại trên vé giảm 1. Khi lượt = 0 thì từ chối lần quét tiếp. |
| 4 | Tự động reset | Sau 3 giây kể từ khi hiện kết quả, bảng trạng thái tự trở về "SẴN SÀNG". |
| 5 | Hiệu ứng chớp viền | Viền màn hình chớp 2 lần theo kết quả (xem bảng mã màu). |
| 6 | Lịch sử cuộn | Giữ tối đa 50 dòng gần nhất. Dòng cũ nhất tự xóa. |
| 7 | Đoàn khách | Mã bắt đầu "BK-" thì hệ thống nhận diện đoàn. Kiểm tra quota vé. |
| 8 | Lọc trò chơi | Danh sách trò chơi chỉ hiện các trò thuộc khu vực đang chọn. Khi đổi khu vực thì danh sách trò chơi tự tải lại. |
| 9 | Nhiều phương thức quét | Hỗ trợ 3 cách: (a) máy quét mã vạch (nhập ẩn), (b) webcam nhận diện QR/mã vạch, (c) quét từ file ảnh (nút riêng). |

#### 7.1. Bảng mã màu trạng thái cổng

| Mã kết quả | Màu nền | Icon | Tiêu đề | Viền chớp |
|---|---|---|---|---|
| 0 — Hợp lệ | 🟢 Xanh (#22C55E) | ✅ Dấu tích | MỜI VÀO | Xanh |
| 1 — Sai khu vực | 🔴 Đỏ (#EF4444) | 📍 Bản đồ | SAI KHU VỰC | Đỏ |
| 2 — Hết lượt | 🔴 Đỏ (#EF4444) | ❌ Dấu X | TỪ CHỐI | Đỏ |
| 3 — Không tìm thấy | ⚫ Xám (#50505F) | ❓ Dấu hỏi | KHÔNG TÌM THẤY | Không |
| 4 — Sai trò chơi | 🟡 Vàng (#F59E0B) | ⚠️ Cảnh báo | SAI TRÒ CHƠI | Vàng |
| 5 — Đoàn hết quota | 🔴 Đỏ (#EF4444) | 🚫 Cấm | HẾT LƯỢT ĐOÀN | Đỏ |
| 6 — Booking lỗi | 🟡 Vàng (#F59E0B) | ⚠️ Cảnh báo | BOOKING LỖI | Vàng |
| — Chờ quét — | ⚫ Tối (#1E2032) | 📊 Mã vạch | SẴN SÀNG | Không |

### 8. User-validation rules

| No | Quy tắc | Thông báo (trên bảng trạng thái) | Mã |
|---|---|---|---|
| 1 | Mã vé không tồn tại | "KHÔNG TÌM THẤY — Mã: [mã]" | GATE_NOT_FOUND |
| 2 | Vé sai khu vực | "SAI KHU VỰC — Vé không thuộc trạm: [tên khu]" | GATE_WRONG_AREA |
| 3 | Vé sai trò chơi | "SAI TRÒ CHƠI — Vé: [tên vé] — Trạm: [tên trò]" | GATE_WRONG_GAME |
| 4 | Vé hết lượt | "TỪ CHỐI — [tên vé] — hết lượt" | GATE_EXPIRED |
| 5 | Đoàn hết quota | "HẾT LƯỢT ĐOÀN — Quota vé đã hết" | GATE_BOOKING_QUOTA |
| 6 | Booking không hợp lệ | "BOOKING LỖI — [lý do]" | GATE_BOOKING_INVALID |

### 9. Related use-cases

- Bán hàng tại quầy POS (Sprint 3 — mục I)
- Quản lý Khu vực & Trò chơi (Sprint 1)
- Xác nhận Đoàn Khách (Sprint 3 — mục III)

---

## III. XÁC NHẬN ĐOÀN KHÁCH

Phân hệ xác nhận đoàn khách phục vụ việc check-in cho các đoàn đã đặt trước. Đây là **form độc lập** mở từ menu chính (không phải form con của POS).

### 1. Overview

Màn hình cho phép nhân viên nhập mã booking hoặc số điện thoại trưởng đoàn để tìm kiếm. Sau khi tìm thấy, hệ thống hiện thông tin đoàn kèm kết quả kiểm tra tự động: booking hợp lệ (xanh) hay không hợp lệ (đỏ kèm lý do). Nếu hợp lệ, nhân viên nhấn "Xuất vé" để xác nhận đoàn đã đến.

### 2. Actors

- Nhân viên Lễ tân / Thu ngân (Reception/Cashier)

### 3. Use-case diagram

```
   ┌─────────────────────────────────────────────────────────────────┐
   │                    PHÂN HỆ XÁC NHẬN ĐOÀN KHÁCH                 │
   │                                                                │
   │  [Nhân viên          UC-B01                   UC-B02           │
   │   Lễ tân /   ─────── (Tìm đoàn         ────── (Xác nhận       │
   │   Thu ngân]           khách)              │     đoàn đã đến)   │
   │                                           │                    │
   │                                           │                    │
   └─────────────────────────────────────────────────────────────────┘
```

**Giải thích quan hệ:**

| ID | Use-case (Verb + Noun) | Actor | Quan hệ | Giải thích |
|---|---|---|---|---|
| UC-B01 | Tìm đoàn khách | Lễ tân / Thu ngân | — | Nhân viên nhập mã booking hoặc SĐT để tìm đoàn |
| UC-B02 | Xác nhận đoàn đã đến | Lễ tân / Thu ngân | — | Nhân viên bấm "Xuất vé" để xác nhận đoàn check-in |

> **Lưu ý:** Chức năng này đơn giản, chỉ có 2 hành động người dùng. Các hành vi nội bộ (kiểm tra trạng thái, hiện nhãn xanh/đỏ, cập nhật trạng thái, khoá mã booking…) do hệ thống tự xử lý — được mô tả tại mục **Business descriptions**.
> 
> Không có mối quan hệ Include hay Extend trong diagram này vì cả 2 Use Case đều là hành động độc lập, trực tiếp của người dùng.

#### 3.1. Pre-condition

- Nhân viên đã đăng nhập thành công.
- Đoàn khách đã được tạo trước trong bộ phận đặt chỗ (quản lý đoàn).

#### 3.2. Post-condition

- Trạng thái booking chuyển thành "Đã xuất vé".
- Mã booking không thể dùng lại lần 2.

#### 3.3. Triggers

- Nhân viên chọn mục "Bán hàng, chọn Đoàn khách" từ thanh menu chính.

### 4. Workflows

**Tình huống 1 — Xác nhận đoàn hợp lệ**

| Nhân viên | Hệ thống |
|---|---|
| 1. Mở màn hình Đoàn khách. | 2. Hiển thị ô tìm kiếm (con trỏ tự đặt vào đây). Phần thông tin booking ẩn. |
| 3. Nhập mã booking (hoặc SĐT trưởng đoàn) rồi nhấn "Tìm" hoặc Enter. | 4. Tìm đoàn theo mã. Nếu không tìm thấy thì thông báo "Không tìm thấy đoàn nào khớp với: [từ khoá]". Ẩn panel thông tin. |
| 5. (Tìm thấy) | 6. Hiện đầy đủ thông tin: tên đoàn, người đại diện, SĐT, số lượng khách, chiết khấu (%), ngày hẹn, trạng thái, gói combo (nếu có). Kiểm tra tự động: trạng thái = "Mới tạo" VÀ Ngày hẹn ≤ hôm nay thì hiện nhãn XANH " Booking hợp lệ — sẵn sàng xác nhận đoàn đã đến." Nút "Xuất vé" được bật. |
| 7. Nhấn "Xuất vé". | 8. Hỏi xác nhận: "Xác nhận đoàn "[tên]" ([X] người) đã đến? Sau khi xác nhận, mã booking sẽ không thể dùng lại." |
| 9. Nhấn "Có". | 10. Cập nhật trạng thái thành "Đã xuất vé". Thông báo thành công kèm tóm tắt: tên, số người, chiết khấu. Xoá ô tìm kiếm, ẩn panel thông tin, sẵn sàng đoàn kế tiếp. |

**Tình huống 2 — Booking không hợp lệ**

| Nhân viên | Hệ thống |
|---|---|
| 1. Nhập mã booking rồi nhấn "Tìm". | 2. Tìm thấy đoàn nhưng trạng thái không đạt. Hiện nhãn ĐỎ "❌ [Lý do]". Nút "Xuất vé" bị khoá (disabled). |

Các lý do từ chối:

| Trạng thái đoàn | Lý do hiển thị |
|---|---|
| Đã xuất vé | "Đoàn này ĐÃ ĐƯỢC XÁC NHẬN trước đó." |
| Đã hủy | "Booking đã bị HỦY." |
| Hết hạn | "Booking đã HẾT HẠN." |
| Mới tạo nhưng chưa đến ngày | "Chưa đến ngày. Ngày hẹn: [dd/MM/yyyy]." |
| Trạng thái lạ | "Trạng thái không hợp lệ: [trạng thái]." |

### 5. External interfaces

#### 5.1. Prototype

```
╔══════════════════════════════════════════════════════════════════════════════╗
║  XÁC NHẬN ĐOÀN KHÁCH                                                [X]  ║
╠══════════════════════════════════════════════════════════════════════════════╣
║                                                                            ║
║  Mã Booking / SĐT: [________________________] [🔍 Tìm]                   ║
║                                                                            ║
║  ┌─── THÔNG TIN BOOKING ───────────────────────────────────────────────┐  ║
║  │  Tên đoàn:       Công ty ABC                                       │  ║
║  │  Người đại diện:  Nguyễn Văn Manager                                │  ║
║  │  SĐT:            0901234567                                         │  ║
║  │  Số lượng khách:  50 người                                          │  ║
║  │  Chiết khấu:      15.0%                                             │  ║
║  │  Ngày hẹn đến:    12/04/2026                                        │  ║
║  │  Trạng thái:      Mới tạo                                           │  ║
║  │  Gói combo:       🎁 Gói Team Building Premium                      │  ║
║  │                                                                      │  ║
║  │  ✅ Booking hợp lệ — sẵn sàng xác nhận đoàn đã đến.                │  ║
║  └──────────────────────────────────────────────────────────────────────┘  ║
║                                                                            ║
║                          [✅ Xuất vé]                                      ║
║                                                                            ║
╚══════════════════════════════════════════════════════════════════════════════╝
```

#### 5.2. Screen description

| No | Field name | Loại | Bắt buộc | Kiểu dữ liệu | Mặc định | Mô tả |
|---|---|---|---|---|---|---|
| **Vùng Tìm kiếm** |
| 1 | Ô nhập mã booking | TextBox | Yes | Text | Trống | Nhập mã booking hoặc SĐT trưởng đoàn |
| 2 | Nút Tìm | Button | — | — | — | Tìm kiếm. Hoặc nhấn Enter. |
| **Vùng Thông tin Booking (ẩn ban đầu)** |
| 3 | Tên đoàn | Label | — | Text | — | Tên đoàn khách |
| 4 | Người đại diện | Label | — | Text | "(Chưa có)" | Họ tên trưởng đoàn |
| 5 | Số điện thoại | Label | — | Text | "(Chưa có)" | SĐT liên hệ |
| 6 | Số lượng khách | Label | — | Số | — | "[X] người" |
| 7 | Chiết khấu | Label | — | Số | — | "[X]%" (luôn hiện, kể cả 0.0%) |
| 8 | Ngày hẹn đến | Label | — | Ngày | "(Chưa xác định)" | dd/MM/yyyy |
| 9 | Trạng thái | Label (màu sắc) | — | Text | — | Xanh = Mới tạo. Đỏ = Đã xuất vé/Đã hủy/Hết hạn. |
| 10 | Gói combo | Label | — | Text | "(Chưa chọn gói)" | "🎁 [tên gói]" nếu có |
| 11 | Nhãn kiểm tra | Label (màu sắc, in đậm) | — | Text | — | ✅ hoặc ❌ + lý do |
| **Nút thao tác** |
| 12 | Xuất vé | Button | — | — | Disabled | Chỉ bật khi booking hợp lệ |

### 6. Business descriptions

| No | Tên quy tắc | Mô tả |
|---|---|---|
| 1 | Điều kiện hợp lệ | Booking hợp lệ khi: Trạng thái = "Mới tạo" VÀ Ngày hẹn đến ≤ Ngày hôm nay. |
| 2 | Một lần duy nhất | Sau khi xác nhận "Đã xuất vé", mã booking không thể sử dụng lại. |
| 3 | Luôn hiện rõ | Chiết khấu luôn hiện kể cả = 0% (hiện "0.0%"). Gói combo nếu chưa chọn thì hiện "(Chưa chọn gói)". Người đại diện nếu trống thì hiện "(Chưa có)". |

### 7. User-validation rules

| No | Quy tắc | Thông báo | Mã |
|---|---|---|---|
| 1 | Ô tìm kiếm trống | "Vui lòng nhập Mã Booking hoặc SĐT trưởng đoàn." | ERR_BK_EMPTY_SEARCH |
| 2 | Không tìm thấy đoàn | "Không tìm thấy đoàn nào khớp với: [từ khoá]" | ERR_BK_NOT_FOUND |
| 3 | Đã xác nhận rồi | "Đoàn này ĐÃ ĐƯỢC XÁC NHẬN trước đó." | ERR_BK_ALREADY_CONFIRMED |
| 4 | Booking đã huỷ | "Booking đã bị HỦY." | ERR_BK_CANCELLED |
| 5 | Booking hết hạn | "Booking đã HẾT HẠN." | ERR_BK_EXPIRED |
| 6 | Chưa đến ngày | "Chưa đến ngày. Ngày hẹn: [dd/MM/yyyy]" | ERR_BK_NOT_YET |

### 8. Related use-cases

- Bán hàng POS — Phục vụ đoàn (Sprint 3 — mục I, tình huống 3)
- Kiểm Soát Vé — Quét mã booking tại cổng (Sprint 3 — mục II)

---

## IV. CHO THUÊ ĐỒ

Phân hệ cho thuê đồ phục vụ hoạt động cho mượn/cho thuê trang thiết bị giải trí tại các trạm (ví dụ: phao bơi, áo phao, xe đạp nước, dép đi mưa...). Bao gồm 2 nghiệp vụ chính trong 2 tab: Cho thuê, Nhận trả (bao gồm Giám sát).

### 1. Overview

Màn hình chia làm 2 tab:
- **Tab Cho thuê**: Chọn trạm, chọn sản phẩm, thêm vào giỏ, gán khách hàng RFID (nếu có), thanh toán (tiền mặt hoặc ví RFID), tạo phiên thuê và in mã biên lai.
- **Tab Nhận trả & Giám sát**: Màn hình tích hợp. 
  - Phần Nhận trả: Quẹt RFID hoặc quét mã biên lai, hiển thị đồ khách đang mượn, thao tác Trả Đủ hoặc nhập thông tin thất lạc, hoàn cọc vào ví/đưa tay, có hộp thoại tính phạt.
  - Phần Giám sát: Nằm chung trong không gian màn hình Nhận trả, hỗ trợ quản lý xem tất cả phiên thuê chưa trả lố hạn. Nhấp đúp vào 1 phiên sẽ tự đẩy dữ liệu sang panel Nhận Trả để lập tức thu hồi.

### 2. Actors

- Nhân viên Trạm thuê (Rental Staff)
- Quản lý (Manager)

### 3. Use-case diagram

```
   ┌─────────────────────────────────────────────────────────────────┐
   │                       PHÂN HỆ CHO THUÊ ĐỒ                     │
   │                                                                │
   │  [Nhân viên        UC-R01                   UC-R02             │
   │   Trạm thuê] ───── (Cho thuê          ───── (Nhận trả         │
   │       │              đồ)                │     đồ)              │
   │       │                                 │       │              │
   │       │             UC-R03              │       │              │
   │       ├──────────── (Giám sát           │       ▼              │
   │       │              phiên thuê)        │     UC-R04           │
   │       │                                 │    (Báo mất đồ)     │
   │       │             UC-R05              │   - - <<extend>> - - │
   │       └──────────── (Quẹt thẻ           │   Extension Point:   │
   │                      RFID khách)        │   Khách báo mất     │
   │                        ▲                │   ≥ 1 món            │
   │                        │                │                      │
   │                   <<include>>           │                      │
   │                        │                │                      │
   │                     UC-R06              │                      │
   │                    (Thanh toán          │                      │
   │                     bằng ví RFID)       │                      │
   │                                         │                      │
   │                                         │                      │
   │  [Quản lý]  ────────────────────────────┘                      │
   │               (cùng quyền Nhận trả + Giám sát)                 │
   │                                                                │
   └─────────────────────────────────────────────────────────────────┘
```

**Giải thích quan hệ:**

| ID | Use-case (Verb + Noun) | Actor | Quan hệ | Giải thích |
|---|---|---|---|---|
| UC-R01 | Cho thuê đồ | Trạm thuê | — | Nhân viên chọn SP, thêm giỏ, thanh toán |
| UC-R02 | Nhận trả đồ | Trạm thuê, Quản lý | — | Nhân viên quét mã biên lai/RFID, nhập SL trả |
| UC-R03 | Giám sát phiên thuê | Trạm thuê, Quản lý | — | Nhân viên xem DS phiên chưa trả |
| UC-R04 | Báo mất đồ | Trạm thuê | <<extend>> UC-R02 | CHỈ KHI khách báo mất từ 1 món trở lên, nhân viên nhập tiền phạt |
| UC-R05 | Quẹt thẻ RFID khách | Trạm thuê | — | Nhân viên quẹt thẻ để nhận diện khách |
| UC-R06 | Thanh toán bằng ví RFID | Trạm thuê | <<include>> UC-R05 | Thanh toán ví BẮT BUỘC nhân viên quẹt thẻ trước |

> **Lưu ý:** Các hành vi nội bộ (sinh mã biên lai, tính cọc hoàn, trừ ví, hoàn cọc vào ví/tiền mặt, xoá giỏ khi đổi trạm…) KHÔNG phải là Use Case — hệ thống tự xử lý. Chúng được mô tả tại mục **Business descriptions**.

#### 3.1. Pre-condition

- Nhân viên đã đăng nhập thành công.
- Sản phẩm loại "Thuê" đã được tạo sẵn kèm giá thuê và tiền cọc trong bảng giá.
- Có ít nhất 1 khu vực (trạm cho thuê).

#### 3.2. Post-condition

- Phiên thuê được tạo, mã biên lai được sinh (khi cho thuê).
- Cọc được hoàn vào ví hoặc tiền mặt (khi nhận trả).
- Khách bị tính phạt nếu mất/hỏng đồ.

#### 3.3. Triggers

- Nhân viên chọn mục "Thuê đồ" từ thanh menu chính.

### 4. Workflows

**Tình huống 1 — Cho thuê đồ (khách vãng lai, tiền mặt)**

| Nhân viên | Hệ thống |
|---|---|
| 1. Mở tab "Cho thuê". Chọn trạm cho thuê từ danh sách. | 2. Hiện danh sách sản phẩm thuộc trạm đó: mã, tên, giá thuê (theo ngày hiện tại), tiền cọc. |
| 3. Nhấp đúp sản phẩm để thêm vào giỏ. Hoặc gõ tên/mã vào ô tìm kiếm để lọc. | 4. Thêm vào giỏ thuê. Nếu sản phẩm đã có thì tăng số lượng. Cập nhật: tổng tiền thuê, tổng tiền cọc, tổng cộng. |
| 5. Nhấn "Thanh toán tiền mặt". | 6. Hiện xác nhận: "Xác nhận thuê cho Khách vãng lai? Tiền thuê: [X]đ, Tiền cọc: [Y]đ, Tổng thu tiền mặt: [Z]đ". |
| 7. Nhấn "Có". | 8. Kiểm tra phiên đăng nhập. Tạo đơn hàng + chi tiết thuê. Sinh mã biên lai "DT..." tự động. Copy mã vào bộ nhớ tạm. Thông báo: "Thuê đồ thành công! MÃ TRẢ ĐỒ: [mã] — đã copy sẵn, hãy dán/in để khách dùng khi trả đồ." Xóa giỏ + xóa khách. |

**Tình huống 2 — Cho thuê bằng ví RFID**

| Nhân viên | Hệ thống |
|---|---|
| 1. Quẹt thẻ RFID trước khi chọn sản phẩm. | 2. Tìm theo chuỗi: Thẻ RFID liên kết Ví điện tử liên kết Khách hàng. Hiện tên khách, số dư ví (xanh nếu > 0, đỏ nếu = 0). Nếu bất kỳ bước nào thất bại thì thông báo cụ thể (thẻ không tồn tại / chưa liên kết ví / ví chưa liên kết KH). |
| 3. Thêm sản phẩm vào giỏ. Nhấn "Thanh toán RFID". | 4. Kiểm tra số dư có đủ để thanh toán không. Nếu đủ thì trừ ví và tạo phiên thuê. Nếu thiếu thì thông báo "Số dư ví không đủ! Cần: [X]đ — Có: [Y]đ". |

**Tình huống 3 — Đổi trạm**

| Nhân viên | Hệ thống |
|---|---|
| 1. Đổi trạm cho thuê (chọn trạm khác trên danh sách). | 2. Tự xóa giỏ hàng để tránh lẫn sản phẩm giữa các trạm. Load lại danh sách SP của trạm mới. |

**Tình huống 4 — Nhận trả đồ bằng RFID (trả đủ, hoàn cọc)**

| Nhân viên | Hệ thống |
|---|---|
| 1. Chuyển tab "Nhận trả". Quẹt RFID. | 2. Tìm khách, sau đó tìm tất cả đơn hàng của khách, lọc các phiên thuê chưa hoàn cọc và hiện bảng: Tên đồ, SL đang thuê, ô nhập "Khách trả (SL)", ô nhập "Báo mất (SL)". Nếu không có đồ đang thuê thì thông báo "Khách này không có đồ đang thuê." |
| 3. Nhập SL trả đủ (hoặc nhấn F12 để tự điền "trả hết" cho tất cả). | 4. Tự điền cột "Khách trả" = SL đang thuê cho tất cả dòng. Cột "Báo mất" = 0. |
| 5. Nhấn "Xác nhận trả". | 6. Kiểm tra: SL trả + SL mất ≤ SL đang thuê (từng dòng). Nếu vi phạm thì thông báo "Số lượng trả/mất vượt quá số đang thuê". Nếu hợp lệ thì tính cọc hoàn, thực hiện hoàn cọc vào ví và thông báo thành công. |

**Tình huống 5 — Nhận trả bằng mã biên lai (khách vãng lai)**

| Nhân viên | Hệ thống |
|---|---|
| 1. Tab "Nhận trả". Quét/nhập mã biên lai (DT...) vào ô mã đơn hàng. | 2. Tìm đơn hàng, lấy chi tiết, lọc phiên thuê chưa hoàn cọc và hiện bảng. Nếu đơn có khách thì hiện tên. Nếu khách vãng lai thì hiện "Khách vãng lai (Biên lai: [mã])" kèm dòng "Hoàn cọc bằng TIỀN MẶT" (chữ vàng). |
| 3. Nhập SL trả. Nhấn "Xác nhận trả". | 4. Hoàn cọc bằng tiền mặt (nhân viên tự trả tay cho khách, hệ thống chỉ ghi nhận). |

**Tình huống 6 — Báo mất đồ (hộp thoại nhập tiền phạt)**

| Nhân viên | Hệ thống |
|---|---|
| 1. Tại bảng nhận trả, nhập SL mất > 0 cho 1 sản phẩm. Nhấn "Xác nhận trả". | 2. Hệ thống tính thời gian thuê, tính phí lố giờ. Hiện hộp thoại nhập liệu với nội dung: "Khách báo mất [X] cái [tên SP]. Tổng tiền cọc: [Y]đ. (Chưa tính [Z]đ phí lố giờ chung). Vui lòng nhập TỔNG SỐ TIỀN ĐỀN BÙ (Phạt) cho [X] món này:". Giá trị mặc định = tiền cọc. |
| 3. Nhập số tiền phạt hoặc giữ mặc định. Nhấn OK. | 4. Nếu bỏ trống hoặc hủy thì hệ thống hủy toàn bộ thao tác trả. Nếu nhập hợp lệ thì ghi nhận phạt, tính cọc hoàn lại = tổng cọc trừ phạt. Hoàn phần còn lại (nếu > 0). |

**Tình huống 7 — Giám sát phiên thuê**

| Nhân viên | Hệ thống |
|---|---|
| 1. Tại tab "Nhận trả", xem phân vùng "Giám sát" ở giữa, chọn khoảng ngày và nhấn "Xem". | 2. Hiện danh sách tất cả phiên thuê chưa hoàn cọc trong khoảng ngày. Nhóm theo đơn hàng. Hiện: Mã đơn, Tên SP, SL, Thời gian lấy ra. Dòng tóm tắt: "Đang có [X] phiên chờ ([Y] món chưa về)" (đỏ) hoặc "Đã thu hồi sạch!" (xanh). |
| 3. Nhấp đúp 1 dòng. | 4. Hệ thống tự đẩy thông tin đơn hàng đó sang lưới Trả đồ để tiến hành tất toán ngay mà không cần quét lại mã. |

### 5. External interfaces

#### 5.1. Prototype — Tab cho thuê

```
╔══════════════════════════════════════════════════════════════════════════════╗
║  CHO THUÊ DỊCH VỤ                                 [Giao Đồ] [Nhận Trả]       ║
╠══════════════════════════════════════════════════════════════════════════════╣
║  Trạm: [▼ Khu Nước            ]                                           ║
║                                                                            ║
║  ┌─ SẢN PHẨM ──────────────────┐  ┌─ GIỎ THUÊ ──────────────────────────┐║
║  │ Tìm: [__________________]   │  │ RFID: [__________________]          │║
║  │                              │  │ Khách: Nguyễn Văn A                 │║
║  │ ┌────┬─────────┬─────┬─────┐│  │ Số dư ví: 500,000đ                  │║
║  │ │ Mã │ Tên     │Thuê │Cọc  ││  │                                      │║
║  │ ├────┼─────────┼─────┼─────┤│  │ ┌──────┬──┬─────┬─────┬──────┬────┐ │║
║  │ │PH01│Phao bơi │10K  │50K  ││  │ │Tên   │SL│Thuê │Cọc  │T.Thuê│T.Cọc│║
║  │ │AO01│Áo phao  │15K  │100K ││  │ ├──────┼──┼─────┼─────┼──────┼────┤ │║
║  │ │XD01│Xe đạp   │30K  │200K ││  │ │Phao  │ 2│20K  │100K │ 20K  │100K│ │║
║  │ └────┴─────────┴─────┴─────┘│  │ └──────┴──┴─────┴─────┴──────┴────┘ │║
║  │ (Nhấp đúp để thêm vào giỏ) │  │                                      │║
║  └──────────────────────────────┘  │ Tiền thuê: 20,000đ                  │║
║                                    │ Tiền cọc:  100,000đ                 │║
║                                    │ TỔNG:      120,000đ                 │║
║                                    │                                      │║
║                                    │ [💳 TT RFID] [💵 TT Tiền mặt] [❌] │║
║                                    └──────────────────────────────────────┘║
╚══════════════════════════════════════════════════════════════════════════════╝
```

#### 5.2. Screen description

Màn hình được chia làm 2 tab chính: Giao đồ (Cho thuê) và Nhận trả (bao gồm chức năng giảm sát).

| No | Field name | Loại | Bắt buộc | Kiểu dữ liệu | Mặc định | Mô tả |
|---|---|---|---|---|---|---|
| **Vùng Header Chung** |
| 1 | Cbo Trạm | ComboBox | Yes | Text | — | Chọn trạm đang thao tác. Đổi trạm sẽ xoá giỏ hàng/làm mới danh sách sản phẩm. |
| **Tab 1: GIAO ĐỒ CHO THUÊ** |
| 2 | Ô Tìm sản phẩm | TextBox | No | Text | Trống | Tìm nhanh sản phẩm theo mã hoặc tên để đưa vào giỏ. |
| 3 | Lưới Sản phẩm | GridControl | — | — | — | Danh sách đồ cho thuê theo trạm. Nhấp đúp để chọn. |
| 4 | Lưới Giỏ hàng | GridControl | — | — | — | Các món đồ đang định thuê, gồm đơn giá thuê và tiền cọc tĩnh. |
| 5 | Quẹt RFID Khách | TextBox | No | Text | Trống | Quẹt thẻ/vòng RFID để nhận diện khách lưu vào đơn hàng. |
| 6 | Tên KH & Số Dư | Label | — | Text | "Khách: ---" | Hiện tên và giá trị ví của khách (số dư chuyển đỏ nếu không đủ thuê). |
| 7 | Tổng Tiền Đã Nhập | Label | — | Số | 0đ | Liệt kê tách biệt giữa Tiền Thuê (không hoàn) và Tiền Cọc (sẽ hoàn). |
| 8 | Tổng Cộng | Label | — | Số | 0đ | Bằng Tiền Thuê + Tiền Cọc, làm mức chỉ định trừ ví/thu tiền. |
| 9 | TT Tiền / RFID | Button | — | — | — | 2 nút tương ứng tự động sinh phiếu thuê / trừ nguồn dòng tiền thanh toán. |
| 10 | Hủy | Button | — | — | — | Xóa trắng giỏ hàng và danh tính khách, trả về phiên tạo trắng. |
| **Tab 2: NHẬN TRẢ ĐỒ & GIÁM SÁT** |
| 11 | Quẹt RFID Trả | TextBox | No | Text | Trống | Dành cho khách dùng thẻ từ. Tự động load tất cả sản phẩm chờ hoàn trả của họ. |
| 12 | Quét Biên Lai | TextBox | No | Text | Trống | Dành cho khách vãng lai. Quét mã vạch (ví dụ: DT-2401...) in trên biên lai. |
| 13 | Lưới Sản Phẩm Đã Thuê | GridControl | — | — | — | Liệt kê thiết bị của người mượn. (Số Lượng Đang Thuê - không gộp với biên lai khác nếu quy quét offline). |
| 14 | Nút Trả Đủ (F12) | Button | — | — | — | Fill toàn bộ số lượng thực tế khách trả y hệt số lượng hệ thống yêu cầu thu hồi. |
| 15 | Xác Nhận Thu Hồi | Button | — | — | — | Đóng bill, xử lý hoàn tiền cọc vào tài khoản/ đưa tay và đóng phiên. Nếu khách báo mất phạt, gọi Input form. |
| **Phân vùng Quản lý (Chờ trả / Giám Sát Giữa)** |
| 16 | Từ - Đến Ngày | DateEdit | Yes | Date | Hôm nay | Ràng khoảng ngày quét để lọc đồ trễ hạn. |
| 17 | Xem / Tìm Chưa Trả | Button | — | — | — | Tìm đơn chưa cấn trừ trả để load danh sách dưới. |
| 18 | Thông Báo | Label | — | Text | Trống | Thông báo tóm tắt cảnh báo số lượng tồn đồ thiếu ở khu. |
| 19 | Lưới Chưa Trả | GridControl | — | — | — | Thống kê danh sách item và đơn đang chờ. Hỗ trợ nhấp đúp qua lưới thu hồi. |

### 6. Business descriptions

| No | Tên quy tắc | Mô tả chi tiết |
|---|---|---|
| 1 | **Giá thuê theo ngày** | Giá thuê lấy từ bảng giá theo thời điểm hiện tại (ngày thường, cuối tuần, lễ). Đổi sang giờ cao điểm thì giá có thể thay đổi. |
| 2 | **Tiền cọc cứng** | Tiền cọc mỗi sản phẩm cố định theo bảng giá, KHÔNG thay đổi theo ngày/giờ. |
| 3 | **Mã biên lai** | Sinh tự động: "DT" + ngày giờ (yyMMddHHmmss) + 4 ký tự ngẫu nhiên. Không trùng. Mã này là chìa khoá để khách vãng lai trả đồ. |
| 4 | **Hoàn cọc** | Trả đủ đồ thì hoàn 100% tiền cọc. Tiền thuê KHÔNG hoàn. |
| 5 | **Phạt mất đồ** | Khi báo mất: hệ thống tính tổng cọc của các món mất + phí lố giờ. Nhân viên tự nhập số tiền phạt cuối cùng (có thể cao hơn hoặc thấp hơn cọc). Phần cọc của đồ trả đủ vẫn được hoàn. |
| 6 | **Đổi trạm xoá giỏ** | Khi đổi trạm cho thuê, giỏ hàng tự động xoá để tránh lẫn sản phẩm giữa các trạm. |
| 7 | **Hai luồng trả đồ** | Khách RFID: quẹt thẻ, hệ thống tìm theo khách (toàn bộ đơn). Khách vãng lai: quét mã biên lai, hệ thống tìm theo đơn hàng cụ thể. |
| 8 | **Hoàn cọc theo phương thức** | Khách RFID thì hoàn tiền vào ví điện tử. Khách vãng lai thì hoàn bằng tiền mặt (nhân viên tự trả, hệ thống ghi nhận). |
| 9 | **Gộp theo sản phẩm** | Bảng nhận trả gộp các phiên thuê cùng sản phẩm thành 1 dòng (hiện tổng SL đang thuê). |
| 10 | **Giám sát nhấp đúp** | Nhấp đúp dòng trong tab giám sát sẽ tự chuyển sang tab Nhận trả, tải đơn hàng đó lên bảng trả. Tiết kiệm nhập lại mã. |

### 7. User-validation rules

| No | Quy tắc | Thông báo | Mã |
|---|---|---|---|
| 1 | Giỏ thuê trống | "Giỏ hàng trống!" | ERR_RT_EMPTY_CART |
| 2 | TT RFID chưa quẹt thẻ | "Vui lòng quẹt thẻ RFID khách hàng trước!" | ERR_RT_NO_RFID |
| 3 | Số dư ví không đủ | "Số dư ví không đủ! Cần: [X]đ — Có: [Y]đ" | ERR_RT_INSUFFICIENT_BALANCE |
| 4 | Thẻ RFID không tồn tại | "Không tìm thấy thẻ RFID!" | ERR_RT_RFID_NOT_FOUND |
| 5 | Thẻ chưa liên kết ví | "Thẻ chưa liên kết ví điện tử!" | ERR_RT_NO_WALLET |
| 6 | Ví chưa liên kết KH | "Ví chưa liên kết khách hàng!" | ERR_RT_NO_CUSTOMER |
| 7 | Đơn hàng biên lai không tìm thấy | "Không tìm thấy đơn hàng với mã: [mã]" | ERR_RT_ORDER_NOT_FOUND |
| 8 | SL trả + mất vượt SL thuê | "[Tên SP]: Số lượng trả/mất ([A]) vượt quá số đang thuê ([B])" | ERR_RT_EXCEED_QUANTITY |
| 9 | Chưa nhập SL trả hoặc mất | "Vui lòng nhập số lượng Trả hoặc Báo Mất ít nhất 1 món đồ!" | ERR_RT_NO_INPUT |
| 10 | Phiên đăng nhập hết hạn | "Phiên làm việc không hợp lệ! Vui lòng khởi động lại." | ERR_RT_SESSION_EXPIRED |
| 11 | Hủy nhập tiền phạt | "Lệnh xác nhận bị hủy vì chưa cung cấp tiền phạt cho thao tác Báo mất [tên SP]." | ERR_RT_PENALTY_CANCELLED |
| 12 | Khách không có đồ thuê | "Khách này không có đồ đang thuê." | ERR_RT_NOTHING_RENTED |
| 13 | Đơn đã hoàn cọc hết | "Đơn hàng này không có đồ đang thuê (hoặc đã hoàn cọc hết)." | ERR_RT_ALL_RETURNED |

### 8. Related use-cases

- Quản lý Sản phẩm & Bảng giá (Sprint 1)
- Quản lý Khách hàng & Ví điện tử (Sprint 2)

---

## V. YÊU CẦU KHÁC

### 1. Menu chính — Cập nhật Sprint 3

| No | Menu | Submenu | Vai trò | Mô tả | Cách mở |
|---|---|---|---|---|---|
| 1 | Bán hàng | POS – Bán hàng | Cashier, Manager | Mở màn hình bán hàng tại quầy | Mở trong MDI chính |
| 2 | Bán hàng | POS – Đoàn khách | Cashier, Manager | Mở màn hình xác nhận đoàn | Mở dạng popup |
| 3 | Kiểm soát vé | Kiểm soát vé tại cổng | Gate Staff | Mở màn hình quét vé | Mở trong MDI chính |
| 4 | Thuê đồ | Cho thuê đồ | Rental Staff | Mở màn hình cho thuê & nhận trả | Mở trong MDI chính |

### 2. Phím tắt

| Phím | Chức năng | Màn hình |
|---|---|---|
| F1 | Đặt con trỏ vào ô tìm kiếm sản phẩm | POS |
| F2 | Đặt con trỏ vào ô mã khách hàng | POS |
| F8 | Đặt con trỏ vào ô khách đưa tiền | POS |
| F9 | Thanh toán tiền mặt | POS |
| F10 | Thanh toán ví RFID | POS |
| F11 | Thanh toán chuyển khoản | POS |
| Esc | Huỷ đơn hàng | POS |
| Delete | Xoá 1 dòng trong giỏ hàng (có xác nhận) | POS |
| Enter (ô khách đưa) | Kích hoạt nút F9 nếu đã đủ tiền | POS |
| F12 | Tự điền "Trả đủ" cho tất cả | Thuê đồ (Nhận trả) |
| Enter (ô quét mã) | Quét mã sản phẩm / vé / booking | POS, Gate |

### 3. Định dạng dữ liệu (Data Format)

| Loại | Format | Ví dụ |
|---|---|---|
| Ngày | dd/MM/yyyy | 12/04/2026 |
| Giờ | HH:mm:ss | 14:30:00 |
| Ngày giờ | dd/MM/yyyy HH:mm | 12/04/2026 14:30 |
| Tiền tệ | Có dấu phẩy + đ | 150,000đ |
| Phần trăm | 1 chữ số thập phân + % | 10.0% |

### 4. Cơ sở dữ liệu — Sprint 3

#### 4.1. Bảng DonHang (Đơn hàng)

| Cột | Kiểu | Ràng buộc | Mô tả |
|---|---|---|---|
| Id | INT | Khoá chính, tự tăng | Mã nội bộ |
| MaCode | NVARCHAR(30) | Không trùng | Mã hiển thị (DH-..., DT-..., DHD-...) |
| IdKhachHang | INT | Khoá ngoại (có thể trống) | Khách hàng (trống = vãng lai) |
| IdDoan | INT | Khoá ngoại (có thể trống) | Đoàn khách (nếu phục vụ đoàn) |
| TongTien | DECIMAL(18,2) | Không trống | Tổng tiền gốc |
| TienGiamGia | DECIMAL(18,2) | Mặc định 0 | Số tiền giảm (chiết khấu hoặc điểm) |
| ThoiGian | DATETIME | Không trống | Thời điểm tạo đơn |
| TrangThai | NVARCHAR(30) | Không trống | Đã thanh toán / Ghi nợ công ty |
| NguonBan | NVARCHAR(20) | Mặc định "POS" | Nguồn: POS, Web |
| GhiChu | NVARCHAR(200) | Có thể trống | Ghi chú (ví dụ: "POS phục vụ đoàn ABC") |
| CreatedAt | DATETIME | Không trống | Ngày tạo |
| CreatedBy | INT | Khoá ngoại | Nhân viên tạo đơn |

#### 4.2. Bảng ChiTietDonHang

| Cột | Kiểu | Ràng buộc | Mô tả |
|---|---|---|---|
| Id | INT | Khoá chính, tự tăng | |
| IdDonHang | INT | Khoá ngoại | Thuộc đơn hàng nào |
| IdSanPham | INT | Khoá ngoại (có thể trống) | Sản phẩm (trống nếu là combo) |
| IdCombo | INT | Khoá ngoại (có thể trống) | Combo (trống nếu là SP lẻ) |
| SoLuong | INT | Không trống | Số lượng mua |
| DonGiaGoc | DECIMAL(18,2) | Không trống | Giá trước giảm |
| TienGiamGiaDong | DECIMAL(18,2) | Mặc định 0 | Phần giảm phân bổ cho dòng này |
| DonGiaThucTe | DECIMAL(18,2) | Không trống | Giá sau giảm |
| TyLeQuyDoi | INT | Mặc định 1 | Quy đổi đơn vị (1 thùng = 24 lon → 24) |

#### 4.3. Bảng PhieuThu (Phiếu thu tiền mặt / chuyển khoản)

| Cột | Kiểu | Ràng buộc | Mô tả |
|---|---|---|---|
| Id | INT | Khoá chính, tự tăng | |
| MaCode | NVARCHAR(30) | Không trùng | Mã phiếu thu (PT-..., PT-DOAN-...) |
| IdDonHang | INT | Khoá ngoại | Liên kết đơn hàng |
| SoTien | DECIMAL(18,2) | Không trống | Số tiền thực thu |
| PhuongThuc | NVARCHAR(20) | Không trống | TienMat / ChuyenKhoan |
| ThoiGian | DATETIME | Không trống | Thời điểm thu |
| CreatedAt | DATETIME | Không trống | Ngày tạo |
| CreatedBy | INT | Khoá ngoại | Nhân viên thu |

#### 4.4. Bảng VeDienTu

| Cột | Kiểu | Ràng buộc | Mô tả |
|---|---|---|---|
| Id | INT | Khoá chính, tự tăng | |
| MaCode | NVARCHAR(50) | Không trùng | Mã vạch / QR trên vé |
| IdSanPham | INT | Khoá ngoại | Loại vé |
| IdDonHang | INT | Khoá ngoại | Đơn hàng phát sinh vé |
| SoLuotConLai | INT | Không trống | Số lượt còn (giảm mỗi khi quét hợp lệ) |
| TrangThai | NVARCHAR(20) | Không trống | Hoạt động / Hết hạn |
| CreatedAt | DATETIME | Không trống | Ngày phát hành |

#### 4.5. Bảng ThueDoChiTiet

| Cột | Kiểu | Ràng buộc | Mô tả |
|---|---|---|---|
| Id | INT | Khoá chính, tự tăng | |
| IdChiTietDonHang | INT | Khoá ngoại | Liên kết với chi tiết đơn hàng |
| IdSanPham | INT | Khoá ngoại | Sản phẩm thuê |
| IdNhanVien | INT | Khoá ngoại | Nhân viên xử lý |
| ThoiGianBatDau | DATETIME | Không trống | Bắt đầu thuê |
| ThoiGianKetThuc | DATETIME | Có thể trống | Kết thúc thuê (trống = chưa trả) |
| TienThueDaThu | DECIMAL(18,2) | Không trống | Tiền thuê đã thu lúc cho thuê |
| SoTienCoc | DECIMAL(18,2) | Không trống | Tiền cọc đã thu lúc cho thuê |
| TrangThaiCoc | NVARCHAR(20) | Không trống | Chưa hoàn / Đã hoàn / Phạt |

#### 4.6. Bảng LichSuDiem (Lịch sử tích/tiêu điểm)

| Cột | Kiểu | Ràng buộc | Mô tả |
|---|---|---|---|
| Id | INT | Khoá chính, tự tăng | |
| IdKhachHang | INT | Khoá ngoại | Khách hàng |
| IdDonHang | INT | Khoá ngoại | Đơn hàng liên quan |
| SoDiem | INT | Không trống | Số điểm (+cộng, −trừ) |
| Loai | NVARCHAR(20) | Không trống | CongDiem / TieuDiem |
| GhiChu | NVARCHAR(200) | | Ví dụ: "Tích điểm DH-240412-A1B2" |
| CreatedAt | DATETIME | Không trống | Thời điểm ghi nhận |
| CreatedBy | INT | Khoá ngoại | Nhân viên thực hiện |

### 5. Message List — Sprint 3

| Mã thông báo | Nội dung tiếng Việt |
|---|---|
| **POS** |
| ERR_POS_EMPTY_CART | Giỏ hàng trống! |
| ERR_POS_NO_RFID | Thanh toán ví RFID bắt buộc phải quét thẻ khách hàng trước! |
| ERR_POS_INSUFFICIENT_BALANCE | Số dư ví không đủ! |
| ERR_POS_OUT_OF_STOCK | Sản phẩm hết hàng trong kho |
| ERR_POS_PRODUCT_NOT_FOUND | Không tìm thấy SP/Combo |
| ERR_POS_COMBO_INACTIVE | Combo đang ngừng bán |
| ERR_POS_SESSION_EXPIRED | Phiên đăng nhập hết hạn |
| ERR_POS_BOOKING_NO_QUOTA | Đoàn hết suất ăn/dịch vụ |
| ERR_POS_BOOKING_INVALID | Booking không hợp lệ |
| ERR_POS_CUSTOMER_NOT_FOUND | Không tìm thấy khách hàng |
| ERR_POS_INSUFFICIENT_CASH | Khách chưa đưa đủ tiền |
| ERR_POS_UNIT_STOCK | Tồn kho không đủ sau khi đổi đơn vị |
| MSG_POS_PAY_SUCCESS | Thanh toán thành công |
| **Kiểm soát vé** |
| GATE_NOT_FOUND | Mã vé không tồn tại trong hệ thống |
| GATE_WRONG_AREA | Vé không thuộc khu vực đang gác |
| GATE_WRONG_GAME | Vé không thuộc trò chơi đang gác |
| GATE_EXPIRED | Vé đã hết lượt sử dụng |
| GATE_BOOKING_QUOTA | Đoàn đã hết quota vé cổng |
| GATE_BOOKING_INVALID | Booking không hợp lệ |
| **Đoàn khách** |
| ERR_BK_EMPTY_SEARCH | Vui lòng nhập mã booking hoặc SĐT |
| ERR_BK_NOT_FOUND | Không tìm thấy đoàn khách |
| ERR_BK_ALREADY_CONFIRMED | Đoàn đã được xác nhận trước đó |
| ERR_BK_CANCELLED | Booking đã bị huỷ |
| ERR_BK_EXPIRED | Booking đã hết hạn |
| ERR_BK_NOT_YET | Chưa đến ngày hẹn |
| MSG_BK_CONFIRM_SUCCESS | Xác nhận đoàn thành công |
| **Thuê đồ** |
| ERR_RT_EMPTY_CART | Giỏ thuê trống |
| ERR_RT_NO_RFID | Chưa quẹt thẻ RFID |
| ERR_RT_INSUFFICIENT_BALANCE | Số dư ví không đủ để thuê |
| ERR_RT_RFID_NOT_FOUND | Không tìm thấy thẻ RFID |
| ERR_RT_NO_WALLET | Thẻ chưa liên kết ví điện tử |
| ERR_RT_NO_CUSTOMER | Ví chưa liên kết khách hàng |
| ERR_RT_ORDER_NOT_FOUND | Không tìm thấy đơn hàng theo mã biên lai |
| ERR_RT_EXCEED_QUANTITY | SL trả/mất vượt quá SL đang thuê |
| ERR_RT_NO_INPUT | Chưa nhập số lượng trả hoặc báo mất |
| ERR_RT_SESSION_EXPIRED | Phiên đăng nhập hết hạn |
| ERR_RT_PENALTY_CANCELLED | Hủy nhập tiền phạt mất đồ |
| ERR_RT_NOTHING_RENTED | Khách không có đồ đang thuê |
| ERR_RT_ALL_RETURNED | Đơn đã hoàn cọc hết |
| MSG_RT_RENT_SUCCESS | Thuê đồ thành công |
| MSG_RT_RETURN_SUCCESS | Trả đồ thành công, cọc đã hoàn |
