# PHÂN TÍCH NGHIỆP VỤ & TIÊU CHÍ NGHIỆM THU — SPRINT 3

**Dự án:** Hệ thống Quản lý Khu Du lịch Đại Nam
**Sprint:** 03 — POS Nexus & Dịch Vụ Giải Trí
**Thời gian:** 01/04/2026 → 14/04/2026
**Nhóm 15:** Nguyên (Leader) · Tấn · Nhi

---

## MỤC LỤC

1. [POS Bán Hàng](#1-pos-bán-hàng)
2. [Kiểm Soát Vé — Cổng Quét QR/Barcode](#2-kiểm-soát-vé--cổng-quét)
3. [Đoàn Khách & Quầy Vé Lễ Tân](#3-đoàn-khách--quầy-vé-lễ-tân)
4. [Thuê Đồ — Phao, Tủ, Chòi](#4-thuê-đồ--phao-tủ-chòi)

---
---

# 1. POS BÁN HÀNG

**Mục tiêu:** Quầy bán hàng tổng hợp (Vé vào cổng, Ăn uống, Combo) — xử lý thanh toán đa hình thức, chiết khấu VIP hoặc khuyến mãi, tự động trừ kho và phát vé điện tử cho khách.

## 1.1. Tác nhân (Actors)

| Actor | Mô tả |
|:------|:------|
| **Nhân viên POS (Thu ngân)** | Người thao tác bán hàng tại quầy: quét mã sản phẩm, thu tiền, phát vé |
| **Khách hàng** | Người mua vé, thức ăn, combo; có thể là khách vãng lai hoặc khách thành viên có thẻ RFID |
| **Hệ thống** | Phần mềm tự động xử lý: tính giá theo ngày, áp chiết khấu, trừ kho, sinh vé QR, tích điểm |

## 1.2. Phân tích luồng nghiệp vụ (Business Flow)

| Bước | Nhân viên POS (Thu ngân) | Hệ thống |
|:----:|:-------------------------|:---------|
| 1 | Mở màn hình POS, chọn kho xuất hàng (mặc định kho chính) | Tải danh sách sản phẩm đang bán (dạng lưới có hình ảnh + giá), giá tự cập nhật theo loại ngày hôm nay (ngày thường / cuối tuần / lễ) |
| 2 | Tìm sản phẩm bằng cách nhấn F1 gõ tên, hoặc nhấp đúp sản phẩm trong lưới, hoặc quét mã vạch barcode bằng đầu đọc | Thêm sản phẩm vào giỏ hàng. Nếu đã có → tăng số lượng. Hiển thị: Tên, Đơn vị, SL, Thành tiền |
| 3 | *(Tùy chọn)* Chỉnh sửa giỏ: thay đổi số lượng (gõ số / nút ±), đổi đơn vị tính (VD: Chai→Lốc→Thùng), nhấn Delete để xóa dòng | Khi đổi đơn vị, tự tính lại đơn giá theo đơn vị mới. Luôn kiểm tra tồn kho — nếu vượt quá tồn, hiện cảnh báo và chặn |
| 4 | *(Tùy chọn)* Nhấn F2, quẹt thẻ RFID hoặc nhập mã/SĐT khách hàng | Nhận diện thông tin khách: Họ tên, Hạng thành viên (Thường/VIP/VVIP), Số dư ví, Điểm tích lũy hiện có |
| 5 | Kiểm tra tổng tiền (hiển thị lớn trên màn hình) | Tự động tính chiết khấu: so sánh giữa mức giảm VIP và mức khuyến mãi đang chạy → chọn mức **cao hơn** (không cộng dồn). Chiết khấu chỉ áp cho Vé/Dịch vụ, không giảm giá đồ ăn uống |
| 6 | *(Tùy chọn)* Nếu KH có điểm tích lũy và giá trị điểm cao hơn chiết khấu → hệ thống hỏi "Dùng điểm giảm tiền?" | So sánh: giá trị quy đổi điểm vs tiền giảm chiết khấu. Nếu điểm lợi hơn → gợi ý dùng điểm. Nếu nhân viên chọn dùng → trừ điểm thay cho chiết khấu |
| 7 | Chọn phương thức thanh toán: **F9** = Tiền mặt · **F10** = Ví RFID · **F11** = Chuyển khoản | Nếu tiền mặt: nhân viên nhập số tiền khách đưa → hệ thống tính tiền thừa. Nếu ví RFID: kiểm tra đủ số dư → trừ ví. Nếu CK: ghi nhận |
| 8 | Xác nhận thanh toán → Nhận hóa đơn / vé | Tạo đơn hàng, lưu chi tiết từng dòng. Tự động trừ kho (quy đổi về đơn vị nhỏ nhất). Nếu đơn có Vé → sinh vé điện tử có mã QR riêng → mở màn hình phát vé |
| 9 | *(Nếu có vé)* In vé barcode hoặc lưu QR ra file gửi khách | Hiện từng tấm vé dạng thẻ card: tên dịch vụ, mã vé, số lượt, mã barcode/QR. Hỗ trợ: In tất cả, In lại từng vé, Lưu QR ra Desktop |
| 10 | — | Nếu có khách hàng gắn: tích thêm điểm cho khách. Công thức: Số điểm = Tiền thực thu ÷ 1.000 × Hệ số hạng (Thường = 1, VIP = 2, VVIP = 3). VD: Thanh toán 100.000đ cho khách VIP → tích 200 điểm |

## 1.3. Quy tắc nghiệp vụ (Business Rules)

| Mã | Quy tắc | Mô tả |
|:--:|:--------|:------|
| BR-POS-01 | Giá tự động theo ngày | Đơn giá sản phẩm thay đổi theo loại ngày: Ngày thường, Cuối tuần, Ngày lễ. Hệ thống tự nhận diện loại ngày và áp giá tương ứng từ Bảng Giá |
| BR-POS-02 | Chiết khấu chọn mức cao nhất | Nếu khách có hạng VIP (ví dụ giảm 5%) đồng thời đang có chương trình khuyến mãi (ví dụ giảm 15%) → hệ thống áp mức 15%. Luôn chọn mức cao hơn, không cộng dồn hai mức |
| BR-POS-03 | Chiết khấu chỉ áp cho Vé/Dịch vụ | Đồ ăn uống và đồ lưu niệm không được chiết khấu. Chỉ giảm trên các sản phẩm loại Vé, Dịch vụ, Combo |
| BR-POS-04 | Trừ kho theo đơn vị nhỏ nhất | Khi bán 1 "Thùng" nước (1 Thùng = 24 Chai), hệ thống trừ 24 Chai trong kho. Mọi tính toán tồn kho đều quy về đơn vị nhỏ nhất |
| BR-POS-05 | Chặn bán vượt tồn kho | Trước khi thêm vào giỏ hoặc tăng số lượng, hệ thống kiểm tra tồn kho. Nếu không đủ → hiện cảnh báo "Hết tồn kho" và không cho thêm |
| BR-POS-06 | Phát vé tự động | Với mỗi sản phẩm loại "Vé" trong đơn, hệ thống tự sinh vé điện tử. Mỗi vé có mã riêng (duy nhất), có barcode và QR để quét tại cổng |
| BR-POS-07 | Tích điểm sau thanh toán | Nếu có khách hàng gắn, hệ thống tự tích điểm sau khi thanh toán thành công. Số điểm = Tiền thực thu ÷ 1.000 × Hệ số hạng. Hệ số: Thường = 1, VIP = 2, VVIP = 3 |
| BR-POS-08 | Dùng điểm trừ tiền | Khách có thể dùng điểm tích lũy để giảm tiền thanh toán (1 điểm = 1.000đ). Nếu dùng điểm lợi hơn chiết khấu, hệ thống sẽ gợi ý cho nhân viên |
| BR-POS-09 | Đổi đơn vị tính trong giỏ | Nhân viên có thể đổi đơn vị tính ngay trong giỏ hàng (VD: Chai → Lốc → Thùng). Khi đổi, đơn giá tự cập nhật theo đơn vị mới |
| BR-POS-10 | Phải có nhân viên đăng nhập | Mỗi giao dịch phải có nhân viên phụ trách. Nếu phiên đăng nhập bị mất → hệ thống bắt buộc khởi động lại để đảm bảo trách nhiệm |
| BR-POS-11 | Hỗ trợ quét mã hàng loạt | Nhân viên có thể gõ "10*VEM001" để thêm 10 vé VEM001 cùng lúc, không cần quét từng cái |

## 1.4. Tiêu chí nghiệm thu (Acceptance Criteria)

### AC-POS-01: Tìm và thêm sản phẩm vào giỏ hàng

```gherkin
GIVEN   Nhân viên đang ở màn hình POS
WHEN    Nhấp đúp sản phẩm trong lưới hoặc quét mã vạch bằng đầu đọc barcode
THEN    Sản phẩm xuất hiện trong giỏ hàng với SL = 1
AND     Đơn giá hiển thị = giá theo loại ngày hôm nay
AND     Tổng tiền cập nhật đúng

GIVEN   Sản phẩm đã có trong giỏ
WHEN    Quét mã hoặc nhấp đúp sản phẩm đó lần nữa
THEN    SL tăng thêm 1 (không tạo dòng mới)

GIVEN   Số lượng muốn mua vượt quá tồn kho
WHEN    Thêm vào giỏ hoặc tăng SL
THEN    Hiển thị cảnh báo "Hết tồn kho"
AND     Không cho vượt quá tồn kho thực tế
```

### AC-POS-02: Nhận diện khách hàng qua RFID hoặc mã/SĐT

```gherkin
GIVEN   Khách có thẻ RFID hợp lệ (đã liên kết ví + tài khoản)
WHEN    Quẹt thẻ RFID hoặc nhập mã/SĐT tại ô Khách Hàng (F2)
THEN    Hiện tên khách + Hạng thành viên + Số dư ví + Điểm tích lũy
AND     Sẵn sàng áp chiết khấu VIP khi thanh toán

GIVEN   Thẻ RFID hoặc mã khách không tồn tại trong hệ thống
WHEN    Nhập thông tin
THEN    Hiện cảnh báo "Không tìm thấy khách hàng"
AND     Đơn hàng vẫn tiếp tục với trạng thái "Khách vãng lai"
```

### AC-POS-03: Tính chiết khấu VIP + Khuyến Mãi

```gherkin
GIVEN   Khách hạng VIP (giảm 5%) mua vé đang có chương trình KM giảm 15%
WHEN    Hệ thống tính tổng tiền
THEN    Áp dụng mức 15% (chọn mức cao hơn giữa VIP 5% và KM 15%)
AND     Hiển thị nguồn giảm giá (ví dụ: "🎁 Giảm giá (KM Hè): -15%")

GIVEN   Khách hạng VVIP (giảm 10%) mua vé đang có KM 8%
WHEN    Hệ thống tính tổng tiền
THEN    Áp dụng mức 10% (VIP lợi hơn KM)

GIVEN   Không có khách gắn và không có khuyến mãi
WHEN    Tính tổng
THEN    Không có chiết khấu
```

### AC-POS-04: Thanh toán Tiền Mặt (F9)

```gherkin
GIVEN   Giỏ có sản phẩm, tổng = 500.000đ
WHEN    Nhấn F9, nhập tiền khách đưa = 1.000.000đ
THEN    Hiện tiền thừa = 500.000đ
AND     Tạo đơn hàng (trạng thái: "Đã thanh toán")
AND     Trừ kho tương ứng
AND     Nếu đơn có Vé → mở màn hình phát vé QR

GIVEN   Tiền khách đưa < Tổng tiền
WHEN    Nhấn thanh toán
THEN    Cảnh báo "Tiền khách đưa không đủ", không cho thanh toán
```

### AC-POS-05: Thanh toán Ví RFID (F10)

```gherkin
GIVEN   Khách đã quẹt RFID, số dư ví = 2.000.000đ, tổng đơn = 500.000đ
WHEN    Nhấn F10 (thanh toán ví)
THEN    Trừ ví 500.000đ → Số dư còn 1.500.000đ
AND     Ghi nhận phương thức "Ví RFID"

GIVEN   Số dư ví = 100.000đ, tổng đơn = 500.000đ
WHEN    Nhấn F10
THEN    Hiện "Số dư ví không đủ" → Không cho thanh toán

GIVEN   Chưa quẹt thẻ RFID
WHEN    Nhấn F10
THEN    Yêu cầu "Vui lòng quẹt thẻ RFID trước"
```

### AC-POS-06: Trừ kho tự động sau thanh toán

```gherkin
GIVEN   Bán 2 Thùng nước (1 Thùng = 24 Chai), kho hiện có 100 Chai
WHEN    Thanh toán thành công
THEN    Kho giảm 48 Chai (2 × 24), tồn kho = 52 Chai

GIVEN   Bán 1 Combo (VD: 1 Vé + 1 Nước + 1 Mũ)
WHEN    Thanh toán thành công
THEN    Mỗi thành phần trong Combo đều trừ kho riêng
AND     Vé trong Combo được sinh vé điện tử riêng
```

### AC-POS-07: Phát vé điện tử QR

```gherkin
GIVEN   Đơn hàng có 3 vé "Vé Cổng Thiên Đường" (SL = 3)
WHEN    Thanh toán thành công
THEN    Sinh 3 vé điện tử, mỗi vé có mã riêng (không trùng nhau)
AND     Mở màn hình Phát Vé hiện 3 tấm vé dạng thẻ card
AND     Có nút "In Tất Cả" và "In Lại" từng vé
AND     Có nút "Lưu QR" → tải file ảnh QR về Desktop
```

### AC-POS-08: Đổi đơn vị tính trong giỏ

```gherkin
GIVEN   Giỏ có "Nước Suối" đang hiện đơn vị = "Chai"
WHEN    Nhấn vào cột đơn vị tính → chọn "Lốc" từ danh sách xổ xuống
THEN    Đơn vị chuyển sang "Lốc"
AND     Đơn giá tự cập nhật theo giá Lốc
AND     Số lượng giữ nguyên, thành tiền tính lại
```

### AC-POS-09: Dùng điểm trừ tiền

```gherkin
GIVEN   Khách có 500 điểm (tương đương 500.000đ), tổng đơn = 300.000đ
AND     Giá trị quy đổi điểm (300.000đ) lớn hơn chiết khấu (VD: 45.000đ)
WHEN    Hệ thống hỏi "Dùng 300 điểm giảm 300.000đ?"
AND     Nhân viên chọn "Có"
THEN    Tổng thanh toán = 0đ
AND     Trừ 300 điểm, khách còn 200 điểm

GIVEN   Khách có 50 điểm (50.000đ), nhưng chiết khấu VIP = 100.000đ
WHEN    Hệ thống so sánh
THEN    Không gợi ý dùng điểm, áp chiết khấu VIP (lợi hơn)
```

### AC-POS-10: Tích điểm sau thanh toán

```gherkin
GIVEN   Khách hạng VIP mua đơn 100.000đ (sau chiết khấu), thanh toán thành công
WHEN    Hệ thống xử lý sau thanh toán
THEN    Tích thêm 200 điểm cho khách (100.000 ÷ 1.000 × 2)
AND     Hiện thông báo "+200 điểm" trong hóa đơn xác nhận

GIVEN   Khách vãng lai (không quẹt thẻ)
WHEN    Thanh toán xong
THEN    Không tích điểm (vì không biết khách là ai)
```

### AC-POS-11: Hủy đơn

```gherkin
GIVEN   Giỏ hàng có sản phẩm
WHEN    Nhấn Escape hoặc nút "Hủy Đơn"
THEN    Hiện xác nhận: "Hủy đơn hàng hiện tại và làm mới khách hàng?"
AND     Nếu đồng ý → Xóa giỏ hàng, bỏ thông tin khách, tổng tiền = 0
```

---
---

# 2. KIỂM SOÁT VÉ — CỔNG QUÉT

**Mục tiêu:** Trạm gác cổng tại mỗi khu vực hoặc trò chơi — nhân viên quét mã vé (barcode hoặc QR) để kiểm tra hợp lệ, trừ lượt, và cho khách vào.

## 2.1. Tác nhân (Actors)

| Actor | Mô tả |
|:------|:------|
| **Nhân viên gác cổng** | Người trực tại trạm, quét vé cho khách qua cổng |
| **Khách hàng** | Đưa vé giấy (có barcode) hoặc điện thoại (có QR) để quét |
| **Hệ thống** | Xác thực mã vé, kiểm tra khu vực/trò chơi, trừ lượt, hiện kết quả |

## 2.2. Phân tích luồng nghiệp vụ

| Bước | Nhân viên gác cổng | Hệ thống |
|:----:|:--------------------|:---------|
| 1 | Chọn Khu Vực đang gác (ví dụ: "Khu Thiên Đường") | Tải danh sách trò chơi thuộc khu vực đó |
| 2 | *(Tùy chọn)* Chọn Trò Chơi cụ thể hoặc giữ "Cổng chung (tất cả trò)" | Nếu chọn trò cụ thể → kiểm tra vé phải đúng trò. Nếu "Cổng chung" → chỉ cần đúng khu vực |
| 3 | Quét mã vé bằng đầu đọc barcode, hoặc bật camera webcam để quét QR, hoặc gõ mã thủ công | Nhận mã vé, bắt đầu xử lý kiểm tra |
| 4 | — | Kiểm tra mã vé trong hệ thống và trả về 1 trong 7 kết quả (xem bảng bên dưới) |
| 5 | Nhìn màn hình để biết kết quả: Xanh = cho vào, Đỏ = từ chối, Vàng = cảnh báo | Hiển thị kết quả toàn màn hình: icon lớn + chữ lớn + chớp viền màu. Hiện thông tin vé ở bên phải: tên dịch vụ, mã vé, khu vực, số lượt còn lại |
| 6 | Tiếp tục quét vé tiếp theo (màn hình tự quay về "Sẵn sàng" sau 5 giây) | Cập nhật bộ đếm: Số hợp lệ / Số từ chối / Tổng lượt quét. Ghi lịch sử 50 lượt quét gần nhất |

**Bảng 7 kết quả quét:**

| Kết quả | Hiển thị | Ý nghĩa | Viền |
|:--------|:---------|:--------|:-----|
| Hợp lệ | ✅ MỜI VÀO (nền xanh) | Vé đúng khu vực, còn lượt → trừ 1 lượt | 🟢 |
| Sai khu vực | ⛔ SAI KHU VỰC (nền đỏ) | Vé thuộc khu vực khác, không phải nơi đang gác | 🔴 |
| Hết lượt | 🚫 TỪ CHỐI (nền đỏ) | Vé đã dùng hết số lượt cho phép | 🔴 |
| Không tìm thấy | ❓ KHÔNG TÌM THẤY (nền xám) | Mã vé không tồn tại trong hệ thống | — |
| Sai trò chơi | ⚠️ SAI TRÒ CHƠI (nền vàng) | Vé không khớp trò chơi mà nhân viên đang gác | 🟡 |
| Đoàn hết quota | 🚫 HẾT LƯỢT ĐOÀN (nền đỏ) | Mã booking đoàn đã dùng hết số người cho phép | 🔴 |
| Booking lỗi | ⚠️ BOOKING LỖI (nền vàng) | Mã booking đoàn không hợp lệ (hết hạn, đã hủy…) | 🟡 |

## 2.3. Quy tắc nghiệp vụ

| Mã | Quy tắc | Mô tả |
|:--:|:--------|:------|
| BR-GATE-01 | Trừ lượt tự động | Mỗi lần quét hợp lệ, hệ thống tự trừ 1 lượt. Khi hết lượt (= 0) → vé không dùng được nữa |
| BR-GATE-02 | Kiểm tra khu vực | Mỗi vé gắn với một khu vực cụ thể. Nếu khách dùng vé khu A tại cổng khu B → từ chối |
| BR-GATE-03 | Kiểm tra trò chơi | Nếu nhân viên chọn gác một trò chơi cụ thể, vé phải đúng trò đó. Nếu chọn "Cổng chung" → chỉ cần đúng khu |
| BR-GATE-04 | Quét mã booking đoàn | Nếu mã bắt đầu bằng "BK-" → hệ thống kiểm tra booking: còn quota không, booking còn hiệu lực không. Hợp lệ → tăng số người đã vào |
| BR-GATE-05 | Phản hồi trực quan | Kết quả quét thể hiện bằng màu sắc toàn màn hình + biểu tượng lớn + viền chớp sáng, để nhân viên nhìn từ xa cũng nhận biết |
| BR-GATE-06 | Lịch sử quét | Hiện tối đa 50 dòng gần nhất (giờ, mã vé, kết quả) để nhân viên tra cứu nhanh |
| BR-GATE-07 | Hỗ trợ nhiều cách quét | Đầu đọc barcode USB/Bluetooth (chính), Camera webcam quét QR (phụ), Gõ tay (dự phòng), Quét từ file ảnh |

## 2.4. Tiêu chí nghiệm thu

### AC-GATE-01: Quét vé hợp lệ

```gherkin
GIVEN   Vé "VE-001" thuộc Khu A, còn 3 lượt
AND     Nhân viên đang gác Khu A, chọn "Cổng chung"
WHEN    Quét mã "VE-001"
THEN    Màn hình hiện ✅ "MỜI VÀO" nền xanh + viền chớp xanh
AND     Bên phải hiện: Tên dịch vụ, Mã vé, Khu vực, "Lượt còn: 2"
AND     Bộ đếm Hợp lệ tăng 1
AND     Sau 5 giây tự quay về trạng thái "SẴN SÀNG"
```

### AC-GATE-02: Quét vé sai khu vực

```gherkin
GIVEN   Vé "VE-002" thuộc Khu B
AND     Nhân viên gác Khu A
WHEN    Quét mã "VE-002"
THEN    Hiện ⛔ "SAI KHU VỰC" nền đỏ
AND     Thông tin: "Vé không thuộc trạm: Khu A"
AND     Bộ đếm Từ chối tăng 1
```

### AC-GATE-03: Quét vé hết lượt

```gherkin
GIVEN   Vé "VE-003" đã hết lượt (= 0)
WHEN    Quét mã "VE-003"
THEN    Hiện 🚫 "TỪ CHỐI" nền đỏ
AND     Thông tin: "Vé Cổng Thiên Đường — hết lượt"
```

### AC-GATE-04: Quét mã không tồn tại

```gherkin
GIVEN   Mã "XYZ-999" không có trong hệ thống
WHEN    Quét mã
THEN    Hiện ❓ "KHÔNG TÌM THẤY" nền xám
AND     Thông tin: "Mã: XYZ-999"
```

### AC-GATE-05: Quét booking đoàn

```gherkin
GIVEN   Mã "BK-20260401-001" là đoàn "Công ty ABC" (50 người, đã vào 30)
WHEN    Quét mã booking tại cổng
THEN    Hiện ✅ "MỜI VÀO" + "🏷️ ĐOÀN: Công ty ABC"
AND     Quota hiện: "31/50" (tăng 1)

GIVEN   Đoàn đã vào đủ 50/50 người
WHEN    Quét lại mã booking
THEN    Hiện 🚫 "HẾT LƯỢT ĐOÀN"
```

### AC-GATE-06: Bật camera quét QR

```gherkin
GIVEN   Có camera webcam kết nối
WHEN    Nhấn nút "Bật Camera"
THEN    Camera live hiện trên màn hình
AND     Tự động nhận diện QR/Barcode và xử lý quét
AND     Có tiếng beep khi quét thành công
WHEN    Nhấn nút "Tắt Camera"
THEN    Camera tắt, ô hình ẩn đi
```

---
---

# 3. ĐOÀN KHÁCH & QUẦY VÉ LỄ TÂN

**Mục tiêu:** Tiếp nhận đoàn khách theo booking đã đặt trước — nhân viên lễ tân tìm booking, kiểm tra hợp lệ, và xác nhận đoàn đã đến.

## 3.1. Tác nhân (Actors)

| Actor | Mô tả |
|:------|:------|
| **Nhân viên Lễ Tân** | Tìm booking, xác nhận đoàn đã đến |
| **Trưởng đoàn** | Đại diện khách đoàn, cung cấp mã booking hoặc số điện thoại |
| **Hệ thống** | Kiểm tra booking hợp lệ, đánh dấu đã xác nhận |

## 3.2. Phân tích luồng nghiệp vụ

| Bước | Nhân viên Lễ Tân | Hệ thống |
|:----:|:-----------------|:---------|
| 1 | Trưởng đoàn đến quầy, cung cấp mã booking (VD: "BK-20260401-001") hoặc số điện thoại | — |
| 2 | Nhập mã booking hoặc SĐT vào ô tìm kiếm, nhấn Enter | Tìm trong hệ thống. Nếu không thấy → hiện "Không tìm thấy". Nếu thấy → hiện panel thông tin |
| 3 | — | Hiển thị chi tiết đoàn: Tên đoàn, Người đại diện, SĐT, Số lượng khách, Chiết khấu (%), Ngày đến dự kiến, Gói Combo (nếu có) |
| 4 | — | Tự kiểm tra tính hợp lệ của booking: (1) Đã xác nhận trước đó rồi? (2) Đã bị hủy? (3) Đã hết hạn? (4) Chưa đến ngày hẹn? → Nếu không hợp lệ: hiện lý do bằng chữ đỏ, khóa nút xác nhận |
| 5 | Nếu hợp lệ (chữ xanh "Sẵn sàng xác nhận") → nhấn nút "Xuất Vé" | Hiện popup xác nhận: "Xác nhận đoàn [Tên] (N người) đã đến?" |
| 6 | Nhấn "Có" để xác nhận | Đánh dấu booking đã xác nhận. Từ giờ booking này không thể dùng lại. Thông báo thành công, reset form để nhận đoàn tiếp |

## 3.3. Quy tắc nghiệp vụ

| Mã | Quy tắc | Mô tả |
|:--:|:--------|:------|
| BR-BK-01 | Tìm kiếm linh hoạt | Hỗ trợ tìm bằng mã booking (BK-xxx) hoặc số điện thoại trưởng đoàn |
| BR-BK-02 | Kiểm tra hợp lệ 4 lớp | Booking phải: chưa xác nhận, chưa hủy, chưa hết hạn, và đã đến ngày hẹn |
| BR-BK-03 | Chỉ xác nhận một lần | Sau khi xác nhận, booking chuyển trạng thái "Đã xác nhận" → không thể dùng lại |
| BR-BK-04 | Chiết khấu đoàn | Đoàn có mức chiết khấu riêng (ví dụ: 20%) áp dụng khi POS bán hàng cho đoàn |
| BR-BK-05 | Gói Combo đoàn | Đoàn có thể đặt trước gói Combo (Vé + Ăn trưa + Tủ đồ). Khi quét mã đoàn tại POS → phát hàng theo quota 0đ (đã trả theo hợp đồng) |

## 3.4. Tiêu chí nghiệm thu

### AC-BK-01: Tìm đoàn theo mã booking

```gherkin
GIVEN   Đoàn "Công ty XYZ" có mã booking "BK-20260401-001"
WHEN    Nhập "BK-20260401-001" và nhấn Enter
THEN    Hiện thông tin: Tên đoàn, Người đại diện, SĐT, Số lượng (50 người), Chiết khấu (20%), Gói Combo
AND     Nút "Xuất Vé" sáng lên (✅ hợp lệ)
```

### AC-BK-02: Tìm đoàn theo SĐT

```gherkin
GIVEN   Trưởng đoàn có SĐT "0901234567" đã booking
WHEN    Nhập "0901234567" và nhấn Tìm
THEN    Hiện thông tin đoàn tương ứng
```

### AC-BK-03: Booking đã được xác nhận trước đó

```gherkin
GIVEN   Booking "BK-001" đã xác nhận lần trước
WHEN    Tìm lại booking này
THEN    Hiện ❌ "Đoàn này ĐÃ ĐƯỢC XÁC NHẬN trước đó"
AND     Nút "Xuất Vé" bị khóa
```

### AC-BK-04: Booking bị hủy

```gherkin
GIVEN   Booking "BK-002" đã bị hủy
WHEN    Tìm booking
THEN    Hiện ❌ "Booking đã bị HỦY"
AND     Nút "Xuất Vé" bị khóa
```

### AC-BK-05: Chưa đến ngày hẹn

```gherkin
GIVEN   Booking "BK-003" hẹn ngày 15/04/2026, hôm nay là 10/04
WHEN    Tìm booking
THEN    Hiện ❌ "Chưa đến ngày. Ngày hẹn: 15/04/2026"
AND     Nút "Xuất Vé" bị khóa
```

### AC-BK-06: Xác nhận đoàn đã đến

```gherkin
GIVEN   Booking hợp lệ, hiện ✅, nút Xuất Vé đang sáng
WHEN    Nhấn "Xuất Vé"
THEN    Hiện xác nhận: "Xác nhận đoàn 'Công ty XYZ' (50 người) đã đến?"
AND     Nếu đồng ý → Ghi nhận → Hiện "✅ Thành công!"
AND     Form tự reset, sẵn sàng nhận đoàn tiếp theo
```

---
---

# 4. THUÊ ĐỒ — PHAO, TỦ, CHÒI

**Mục tiêu:** Giao và nhận đồ thuê tại các trạm trong công viên (phao bơi, tủ đồ, chòi nghỉ…). Tích hợp ví RFID, tính tiền cọc + phí thuê theo thời gian, xử lý trường hợp mất/hỏng đồ.

## 4.1. Tác nhân (Actors)

| Actor | Mô tả |
|:------|:------|
| **Nhân viên trạm thuê** | Giao đồ, nhận trả, xử lý trường hợp hỏng/mất |
| **Khách hàng** | Thuê đồ (phao bơi, tủ đồ, chòi nghỉ…), quẹt thẻ RFID hoặc trả tiền mặt |
| **Hệ thống** | Tính tiền thuê + cọc, ghi nhận thời gian thuê, hoàn cọc hoặc tính phạt khi trả |

## 4.2. Phân tích luồng nghiệp vụ

### Luồng A: GIAO ĐỒ (Tab "Cho Thuê")

| Bước | Nhân viên trạm thuê | Hệ thống |
|:----:|:---------------------|:---------|
| 1 | Chọn trạm cho thuê đang đứng (ví dụ: "Khu Biển") | Tải danh sách đồ cho thuê thuộc khu này (tên, giá thuê, tiền cọc) |
| 2 | Nhấp đúp sản phẩm muốn cho thuê (VD: Phao bơi) | Thêm vào giỏ: Tên + SL + Tiền thuê + Tiền cọc. Nếu nhấp lần 2 → SL tăng thêm 1 |
| 3 | Kiểm tra tổng tiền (gồm tiền thuê + tiền cọc) | Hiện rõ 3 dòng: "Tiền thuê: X | Tiền cọc: Y | TỔNG: X+Y" |
| 4 | *(Tùy chọn)* Quẹt thẻ RFID khách hàng | Nhận diện khách: Tên, Số dư ví |
| 5 | Chọn thanh toán: "Ví RFID" hoặc "Tiền mặt" | Nếu ví: kiểm tra đủ số dư → trừ ví. Nếu tiền mặt: thu trực tiếp |
| 6 | Xác nhận → Giao đồ cho khách, đưa biên lai | Tạo đơn hàng thuê, ghi nhận từng món (thời điểm bắt đầu thuê, tiền cọc, trạng thái "Chưa trả"). Sinh mã biên lai → tự copy vào clipboard |

### Luồng B: NHẬN TRẢ (Tab "Nhận Trả")

| Bước | Nhân viên trạm thuê | Hệ thống |
|:----:|:---------------------|:---------|
| 1 | Quẹt thẻ RFID khách HOẶC quét mã biên lai trên vé thuê | Tìm khách → tải danh sách đồ đang thuê (trạng thái "Chưa trả") |
| 2 | Kiểm tra bảng đồ đang mượn: tên SP, SL đang thuê | Hiện bảng cho chỉnh sửa: 4 cột (Tên SP · SL đang thuê · Khách trả · Báo mất) |
| 3 | Nhập số lượng khách trả ở cột "Khách Trả". Nhập số mất/hỏng ở cột "Báo Mất". Hoặc bấm **F12** để điền "Trả đủ" tất cả | Kiểm tra: Trả + Mất ≤ Đang thuê. Nếu vượt → báo lỗi |
| 4 | Nhấn "Xác Nhận Trả" | Nếu có "Báo Mất" → hiện popup hỏi số tiền đền bù cho từng món (gợi ý = tiền cọc của món đó) |
| 5 | *(Nếu mất đồ)* Nhập số tiền phạt đền bù | Giữ cọc + thu thêm phí đền bù cho món mất |
| 6 | — | Đồ trả đủ → hoàn cọc vào ví hoặc tiền mặt. Nếu thuê lố giờ → tính thêm phí lố. Cập nhật trạng thái "Đã hoàn". Tự tải lại danh sách |

## 4.3. Quy tắc nghiệp vụ

| Mã | Quy tắc | Mô tả |
|:--:|:--------|:------|
| BR-RT-01 | Thu tiền cọc | Mỗi sản phẩm thuê có tiền cọc riêng (VD: phao 200.000đ, tủ 100.000đ). Thu khi giao, hoàn khi trả đầy đủ |
| BR-RT-02 | Giá thuê theo ngày | Tiền thuê tự thay đổi theo loại ngày (Thường / Cuối tuần / Lễ) |
| BR-RT-03 | Hai hình thức thanh toán | Ví RFID (trừ ví trực tiếp) hoặc Tiền mặt (thu tay). Khi trả đồ, hoàn cọc theo cùng hình thức |
| BR-RT-04 | Kiểm tra số dư ví | Nếu thanh toán ví, hệ thống kiểm tra: Số dư ≥ Tổng tiền thuê + Tổng tiền cọc. Nếu thiếu → từ chối |
| BR-RT-05 | Mã biên lai | Mỗi phiên thuê có mã biên lai riêng (ví dụ: "DT260401143022ABCD"). Mã này tự copy vào clipboard, nhân viên in hoặc gửi cho khách để dùng khi trả |
| BR-RT-06 | Hoàn cọc khi trả đủ | Khách trả đủ đồ, không hỏng hóc → hoàn toàn bộ tiền cọc |
| BR-RT-07 | Phạt khi mất/hỏng đồ | Nếu khách báo mất → giữ cọc + thu thêm phí đền bù. Nhân viên nhập số tiền phạt cụ thể qua popup (hệ thống gợi ý = tiền cọc) |
| BR-RT-08 | Phí lố giờ | Nếu thời gian thuê thực tế vượt quá gói → tính thêm phí phụ trội theo phút |
| BR-RT-09 | Cho phép trả từng phần | Khách thuê 3 phao, có thể trả 2 trước, 1 trả sau. Phần chưa trả giữ trạng thái "Chưa hoàn" |
| BR-RT-10 | Phải có nhân viên đăng nhập | Mỗi giao dịch thuê/trả phải gắn nhân viên phụ trách. Nếu phiên đăng nhập mất → bắt buộc khởi động lại |
| BR-RT-11 | Giám sát đồ chưa trả | Có tab riêng hiện tất cả phiên thuê chưa hoàn trong ngày, nhóm theo đơn hàng, có tên khách và số lượng món |

## 4.4. Tiêu chí nghiệm thu

### AC-RT-01: Thêm sản phẩm thuê vào giỏ

```gherkin
GIVEN   Nhân viên chọn trạm "Khu Biển" và thấy danh sách đồ thuê
WHEN    Nhấp đúp "Phao bơi" (giá thuê 50.000đ, cọc 200.000đ)
THEN    Giỏ thêm 1 dòng: Phao bơi | SL 1 | Thuê 50K | Cọc 200K
AND     Tổng = "Tiền thuê: 50.000đ | Tiền cọc: 200.000đ | TỔNG: 250.000đ"

WHEN    Nhấp đúp "Phao bơi" lần nữa
THEN    SL tăng thành 2 | Tổng thuê 100K | Tổng cọc 400K | TỔNG: 500K
```

### AC-RT-02: Thanh toán thuê bằng ví RFID

```gherkin
GIVEN   Giỏ có Phao ×2 (tổng 500K), khách quẹt RFID, ví có 1.000.000đ
WHEN    Nhấn "Thanh toán ví"
THEN    Trừ ví 500K → Ví còn 500K
AND     Tạo phiên thuê cho mỗi món (trạng thái "Chưa trả")
AND     Hiện mã biên lai (đã tự copy, nhân viên dán/in cho khách)

GIVEN   Ví chỉ có 100K, tổng 500K
WHEN    Nhấn thanh toán ví
THEN    Hiện "Số dư ví không đủ! Cần: 500K, Có: 100K"
```

### AC-RT-03: Nhận trả đồ bằng RFID

```gherkin
GIVEN   Khách đã thuê 3 phao bơi
WHEN    Tab "Nhận Trả" → Quẹt RFID khách
THEN    Hiện danh sách: "Phao bơi | Đang thuê: 3 | Trả: [  ] | Mất: [  ]"

WHEN    Nhập Trả = 3, Mất = 0, nhấn "Xác Nhận Trả"
THEN    Hoàn cọc 600K vào ví
AND     Trạng thái chuyển "Đã hoàn" cho cả 3 món
```

### AC-RT-04: Nhận trả bằng mã biên lai (khách vãng lai)

```gherkin
GIVEN   Khách vãng lai đã thuê, có biên lai mã "DT260401143022ABCD"
WHEN    Tab "Nhận Trả" → Quét mã biên lai
THEN    Hiện danh sách đồ đang thuê của đơn này
AND     Ghi chú: "Khách vãng lai — Hoàn cọc bằng TIỀN MẶT"
```

### AC-RT-05: Báo mất đồ

```gherkin
GIVEN   Khách thuê 3 phao, trả lại 2, mất 1 (cọc mỗi phao 200K)
WHEN    Nhập Trả = 2, Mất = 1, nhấn "Xác Nhận Trả"
THEN    Hiện popup: "Khách báo mất 1 cái Phao bơi. Tiền cọc 200K. Nhập tiền đền bù:"
AND     Nhân viên nhập 200.000đ

THEN    2 phao trả → hoàn cọc 400K
AND     1 phao mất → giữ cọc 200K + thu phạt 200K
```

### AC-RT-06: Trả đủ nhanh (F12)

```gherkin
GIVEN   Bảng hiện 3 sản phẩm đang thuê
WHEN    Nhấn F12
THEN    Tất cả cột "Khách Trả" tự điền = SL Đang Thuê
AND     Cột "Báo Mất" = 0
AND     Nhân viên chỉ cần nhấn "Xác Nhận Trả" để hoàn tất nhanh
```

### AC-RT-07: Kiểm tra số lượng trả + mất

```gherkin
GIVEN   Đang thuê 3 phao
WHEN    Nhập Trả = 2 + Mất = 2 (tổng = 4, vượt quá 3)
THEN    Hiện lỗi: "Phao bơi: Số lượng trả/mất (4) vượt quá số đang thuê (3)"
AND     Không cho xác nhận
```

### AC-RT-08: Giám sát đồ chưa trả

```gherkin
GIVEN   Trong ngày có 5 phiên thuê chưa trả
WHEN    Tab "Giám sát" → nhấn "Xem chưa trả"
THEN    Hiện bảng 5 đơn, nhóm theo mã biên lai + tên khách
AND     Hiện "Đang có 5 phiên chờ (12 món chưa về)"
AND     Nhấp đúp vào dòng → tự chuyển sang tab Nhận Trả với đơn đó
```

---
---

# TỔNG HỢP SPRINT 3

| Module | Số AC | Số BR | Ghi chú |
|:-------|:-----:|:-----:|:--------|
| POS Bán Hàng | 11 | 11 | Bán hàng, thanh toán 3 hình thức, chiết khấu, tích/dùng điểm, trừ kho, phát vé QR |
| Kiểm Soát Vé | 6 | 7 | 7 trạng thái quét vé, hỗ trợ đoàn, camera |
| Đoàn Khách | 6 | 5 | Tìm booking, validation 4 lớp, xác nhận 1 lần |
| Thuê Đồ | 8 | 11 | Giao/trả đồ, RFID + biên lai, F12 trả nhanh, phạt mất, lố giờ |
| **TỔNG** | **31** | **34** | **4 chức năng chính** |

**Phụ trách Sprint 3:**
- **Nhi**: Viết tài liệu phân tích nghiệp vụ + AC + SRS + Test Cases (~350 TC)
- **Nguyên**: Lập trình nghiệp vụ POS, Vé, Đoàn, Thuê Đồ + Kiểm thử đơn vị BUS
- **Tấn**: Thiết kế giao diện 4 màn hình + Tích hợp đầu đọc barcode + Nối dữ liệu
