# Khu Du Lịch Đại Nam
# Đặc Tả Yêu Cầu Phần Mềm
# Mã dự án: DN01
# Mã tài liệu: DN01_SRS_BanHangPOS_v1.0

Hồ Chí Minh, Tháng 04/2026

---

## Lịch sử thay đổi

| Ngày hiệu lực | Hạng mục thay đổi | A/M/D | Mô tả | Phiên bản |
|---|---|---|---|---|
| 21/04/2026 | Phát hành lần đầu | A | | 1.0 |
| 23/04/2026 | Bổ sung tính năng bán Combo | M | Cập nhật bóc tách Combo trên POS | 1.1 |

*A - Thêm mới, M - Chỉnh sửa, D - Xóa bỏ*

---

## Mục lục

1. [Bán hàng tại quầy (POS)](#1-bán-hàng-tại-quầy-pos)
   - 1.1. [Phiên thu ngân](#11-phiên-thu-ngân)
   - 1.2. [Màn hình bán hàng chính](#12-màn-hình-bán-hàng-chính)
   - 1.3. [Quét mã vạch và tìm sản phẩm](#13-quét-mã-vạch-và-tìm-sản-phẩm)
   - 1.4. [Giỏ hàng](#14-giỏ-hàng)
   - 1.5. [Khách hàng và chiết khấu thành viên](#15-khách-hàng-và-chiết-khấu-thành-viên)
   - 1.6. [Khuyến mãi](#16-khuyến-mãi)
   - 1.7. [Thanh toán](#17-thanh-toán)
   - 1.8. [Hoàn hàng và hoàn vé](#18-hoàn-hàng-và-hoàn-vé)
   - 1.9. [Đóng phiên thu ngân](#19-đóng-phiên-thu-ngân)
   - 1.10. [Lịch sử đơn hàng](#110-lịch-sử-đơn-hàng)
   - 1.11. [Nạp ví RFID](#111-nạp-ví-rfid)
2. [Yêu cầu khác](#2-yêu-cầu-khác)
   - 2.1. [Định dạng dữ liệu](#21-định-dạng-dữ-liệu)
   - 2.2. [Danh mục dữ liệu tham chiếu](#22-danh-mục-dữ-liệu-tham-chiếu)
   - 2.3. [Bảng mã thông báo lỗi](#23-bảng-mã-thông-báo-lỗi)

## 0. Phạm vi tài liệu

Tài liệu này đặc tả phân hệ Bán hàng tại quầy (POS) thuộc hệ thống quản lý vận hành Khu Du lịch Đại Nam.
- **Phạm vi bao gồm:** mở và đóng phiên thu ngân; bán sản phẩm; áp dụng khuyến mãi; thu tiền đa phương thức; hoàn hàng và hoàn vé; quản lý phiên cà làm việc.
- **Không bao gồm:** quản lý danh mục sản phẩm (đặc tả tại SRS Sản phẩm), quản lý thông tin khách hàng (đặc tả tại SRS Khách hàng), báo cáo doanh thu (đặc tả tại SRS Báo cáo).
- **Đối tượng đọc:** Product Owner, Lập trình viên (Dev), QA/Tester.

---

# 1. Bán hàng tại quầy (POS)

Phân hệ bán hàng tại quầy bao gồm các chức năng sau:

- Mở và đóng phiên thu ngân (quản lý ca làm việc)
- Quét hoặc gõ mã sản phẩm để thêm vào giỏ hàng
- Gắn khách hàng thành viên vào đơn hàng
- Áp dụng chương trình khuyến mãi
- Thu tiền đa phương thức (tiền mặt, chuyển khoản, ví RFID, điểm tích lũy)
- Phát hành vé điện tử (mã QR) sau khi thanh toán
- Hoàn hàng, hoàn vé theo nghiệp vụ khu du lịch
- Chốt ca và đối soát tiền mặt trong két

---

## 1.1. Phiên thu ngân

### 1.1.1. Tổng quan

Trước khi bắt đầu bán hàng, nhân viên thu ngân bắt buộc phải mở một phiên làm việc. Phiên thu ngân ghi nhận thời gian mở ca, số tiền ban đầu trong két, và máy POS đang sử dụng. Khi kết thúc ca, nhân viên đóng phiên và hệ thống tự đối soát tổng thu thực tế với tổng thu hệ thống ghi nhận.

### 1.1.2. Tác nhân

- Thu ngân (mở ca, bán hàng, đóng ca)
- Quản lý (xem lịch sử phiên, xử lý chênh lệch)

### 1.1.3. Biểu đồ use-case

```
Thu ngân ──── Mở phiên thu ngân
         ├── Bán hàng (xem mục 1.2)
         ├── Hoàn hàng (xem mục 1.8)
         └── Đóng phiên thu ngân

Quản lý ──── Xem lịch sử phiên
         └── Xử lý chênh lệch
```

#### 1.1.3.1. Tiền điều kiện

- Nhân viên đã đăng nhập vào hệ thống.
- Danh mục máy POS (điểm bán) đã được thiết lập.
- Nhân viên chưa có phiên thu ngân nào đang mở.

#### 1.1.3.2. Hậu điều kiện

Hệ thống tạo một bản ghi phiên thu ngân ở trạng thái Đang mở. Tất cả hóa đơn phát sinh trong ca sẽ gắn vào phiên này.

#### 1.1.3.3. Điểm kích hoạt

Nhân viên truy cập module POS. Nếu chưa có phiên mở, hệ thống tự bật form mở phiên.

### 1.1.4. Luồng thao tác

#### 1.1.4.1. Tình huống 1 — Mở phiên bình thường

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Chọn máy POS từ danh sách, nhập số tiền đầu ca trong két. | Kiểm tra máy đó có đang bị nhân viên khác sử dụng không. |
| 2 | Nhấn nút Mở phiên. | Nếu máy trống, hệ thống tạo phiên mới, ghi nhận thời gian mở, rồi chuyển sang màn hình bán hàng. Nếu máy đang bận thì thông báo từ chối. |

#### 1.1.4.2. Tình huống 2 — Mở phiên khi đã có phiên chưa đóng (Mở thủ công)

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Mở form Mở phiên và nhấn nút Mở phiên. | Phát hiện nhân viên này vẫn còn phiên đang mở, hệ thống thông báo yêu cầu đóng phiên cũ trước khi mở phiên mới. |

#### 1.1.4.3. Tình huống 3 — Phục hồi phiên khi thoát ứng dụng

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Truy cập lại màn hình POS sau khi thoát app hoặc đăng nhập từ máy tính khác. | Phát hiện nhân viên này vẫn còn phiên đang mở, hệ thống **tự động phục hồi** phiên làm việc cũ mà không yêu cầu nhập lại tiền đầu ca. |
| 2 | — | Tự động load lại mã máy POS, danh mục hàng hóa của quầy, và lịch sử giỏ hàng (nếu có lưu) để tiếp tục bán hàng. |

### 1.1.5. Giao diện

#### 1.1.5.1. Mô tả màn hình — Form mở phiên

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Máy POS | Combo Box | Yes | Integer (FK) | Blank | Danh sách các máy POS đang hoạt động. Chỉ hiển thị máy chưa bị nhân viên khác chiếm giữ. |
| 1b | Kho bán hàng | Combo Box | Yes | Integer (FK) | Blank | Chọn kho lưu trữ gắn với ca làm việc. Mọi thao tác xuất/nhập tồn kho sẽ mặc định chọc vào kho này. |
| 2 | Tiền đầu ca | Spin Edit | Yes | Decimal(15,0) | 0 | Số tiền mặt nhân viên nhận trước ca. Định dạng N0 (phân cách hàng nghìn). (*) Tooltip: Nhập số tiền mặt đang có trong két |
| 3 | Ghi chú | Memo Edit | No | Text | Blank | Ghi chú tùy ý (ví dụ: Ca sáng, Nhận bàn giao từ ca trước). |
| 4 | Mở phiên | Button | N/A | N/A | N/A | Xác nhận mở ca. Hotkey: Enter. |

### 1.1.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Một nhân viên — một phiên | Mỗi nhân viên chỉ được mở tối đa 1 phiên cùng lúc. Phải đóng phiên hiện tại trước khi mở phiên mới. |
| 2 | Một máy — một phiên | Mỗi máy POS chỉ cho phép 1 phiên đang mở tại một thời điểm. Tránh 2 nhân viên dùng chung máy gây rối đối soát. |
| 3 | Kho gắn theo máy | Mỗi máy POS được gắn với một kho hàng. Đây là kho mặc định dùng để trừ tồn khi bán và nhập trả khi hoàn hàng. |
| 4 | Bắt buộc có phiên | Mọi thao tác bán hàng, hoàn hàng đều yêu cầu phiên thu ngân đang mở. Nếu không có phiên, hệ thống sẽ chặn thao tác. |
| 5 | Tự động phục hồi phiên | Khi thu ngân vô tình đóng ứng dụng, chuyển tab, hoặc đăng nhập sang một máy tính khác, hệ thống sẽ tự động quét trạng thái. Nếu có phiên Đang mở của nhân viên đó, hệ thống sẽ tự động phục hồi phiên thay vì bắt mở phiên mới, đảm bảo tính liên tục của ca làm việc. |

### 1.1.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Bắt buộc chọn máy POS | ERR_POS_NO_MACHINE |
| 2 | Tiền đầu ca phải là số không âm | ERR_POS_INVALID_OPENING_CASH |
| 3 | Máy POS đã được nhân viên khác sử dụng | ERR_POS_MACHINE_IN_USE |
| 4 | Nhân viên đã có phiên đang mở | ERR_POS_ALREADY_OPENED_SESSION |

### 1.1.8. Liên kết use-case

- Màn hình bán hàng chính (1.2)
- Đóng phiên thu ngân (1.9)

---

## 1.2. Màn hình bán hàng chính

### 1.2.1. Tổng quan

Màn hình bán hàng chia làm 3 vùng chính: phía trên là thanh quét mã và thông tin khách hàng; giữa bên trái là danh mục sản phẩm dạng lưới tile (có ảnh, có tên, có giá); giữa bên phải là giỏ hàng chi tiết; phía dưới là bảng tổng hợp tài chính và các nút thao tác nhanh.

### 1.2.2. Tác nhân

- Thu ngân (thao tác chính)

### 1.2.3. Biểu đồ use-case
Không có

### 1.2.4. Luồng thao tác
Tham khảo các luồng quét mã, giỏ hàng bên dưới.

### 1.2.5. Giao diện

#### 1.2.5.1. Mô tả màn hình — Thanh trên (Top bar)

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tiêu đề hệ thống | Label | N/A | Text | N/A | Hiển thị tên hệ thống, ví dụ: ĐẠI NAM POS. |
| 2 | Trạng thái phiên | Label | N/A | Text | N/A | Hiển thị mã máy, tên nhân viên, thời gian mở ca. Cập nhật tự động khi mở hoặc đóng phiên. |
| 3 | Ô quét mã | Text Edit | No | Text | Blank | Nhận đầu vào từ bàn phím, máy quét, hoặc ứng dụng quét trên điện thoại. Xem chi tiết mục 1.3. (*) Placeholder: Quét mã vạch hoặc gõ tên sản phẩm... |

#### 1.2.5.2. Mô tả màn hình — Thanh tab lọc danh mục

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tất cả | Tab Page | N/A | N/A | Đang chọn | Hiển thị toàn bộ sản phẩm đang bán. |
| 2 | Vé | Tab Page | N/A | N/A | N/A | Lọc sản phẩm thuộc nhóm Vé vào khu và Vé trò chơi. |
| 2b | Combo | Tab Page | N/A | N/A | N/A | Lọc các gói Combo sản phẩm/dịch vụ kết hợp. |
| 3 | F&B | Tab Page | N/A | N/A | N/A | Lọc nhóm Ăn uống và Đồ uống. |
| 4 | Hàng hóa | Tab Page | N/A | N/A | N/A | Lọc các nhóm còn lại (Hàng hóa, Tủ đồ, Cho thuê...). |

Tab đang chọn được tô nổi bật (màu primary), các tab khác ở trạng thái mờ.

#### 1.2.5.3. Mô tả màn hình — Lưới sản phẩm (Tile View, Read-only)

| STT | Tên hiển thị | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Ảnh đại diện | Image | Image | Ảnh sản phẩm thu nhỏ. Nếu chưa có ảnh, hiển thị ảnh mặc định theo loại sản phẩm. |
| 2 | Tên sản phẩm | Label | Text | Tên hiển thị, tối đa 2 dòng. Cắt dấu ba chấm nếu quá dài. |
| 3 | Loại sản phẩm | Label | Text | Nhãn phân loại. Giá trị hiển thị được dịch đa ngôn ngữ. |
| 4 | Giá bán | Label | Decimal | Giá bán hiện tại. Định dạng N0 kèm ký hiệu tiền tệ. |
| 5 | Trạng thái tồn kho | Badge / Label | Text | Nếu sản phẩm là vật tư/hàng hóa có tồn kho = 0, hiển thị badge "Hết hàng" (màu đỏ) đè lên ảnh sản phẩm và làm mờ toàn bộ tile để người dùng dễ nhận biết. |

Mỗi tile có kích thước cố định 180×230 pixel. Khi nhấn vào tile, hệ thống thêm 1 đơn vị sản phẩm đó vào giỏ hàng.

#### 1.2.5.4. Mô tả màn hình — Panel khách hàng

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Ô tìm khách | Text Edit | No | Text | Blank | Nhập số điện thoại, mã khách hàng, hoặc mã RFID. (*) Placeholder: SĐT / Mã KH / Quét RFID... |
| 2 | Nút Tìm | Button | N/A | N/A | N/A | Tra cứu khách hàng theo nội dung đã nhập. Nếu tìm thấy, hiển thị thông tin bên dưới. Nếu không, thông báo. |
| 3 | Tên khách | Label | N/A | Text | Trống | Tên khách hàng sau khi gắn. Trống nếu khách vãng lai. |
| 4 | Hạng thành viên | Label | N/A | Text | Trống | Hiển thị hạng thành viên (Bạc, Vàng, Kim cương). Quyết định mức chiết khấu tự động. |
| 5 | Số điểm tích lũy | Label | N/A | Integer | 0 | Số điểm hiện có. Có thể dùng để thanh toán một phần đơn hàng. |
| 5b | Số dư ví RFID | Label | N/A | Decimal(15,0) | Trống | Số dư ví RFID hiện tại. Chỉ hiển thị khi khách có ví RFID. Định dạng N0 kèm ký hiệu đ. |
| 6 | Nút bỏ chọn | Button | N/A | N/A | N/A | Bỏ gắn khách hàng, đơn hàng quay về chế độ khách vãng lai. (*) Tooltip: Bỏ chọn khách hàng |

#### 1.2.5.5. Mô tả màn hình — Thanh nút thao tác

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Thanh toán | Button | N/A | N/A | N/A | Mở form thanh toán (xem mục 1.7). Hotkey: F2. Bị vô hiệu hóa khi giỏ hàng trống. |
| 1b | Lịch sử | Button | N/A | N/A | N/A | Mở danh sách đơn hàng trong phiên hiện tại (xem mục 1.10). Hotkey: F4. |
| 1c | Nạp ví | Button | N/A | N/A | N/A | Mở form nạp tiền vào ví RFID của khách (xem mục 1.11). Hotkey: F5. |
| 2 | Xóa giỏ | Button | N/A | N/A | N/A | Xóa toàn bộ giỏ hàng sau khi người dùng xác nhận qua hộp thoại. Hotkey: Esc. |
| 3 | Xóa KM | Button | N/A | N/A | N/A | Xóa mã khuyến mãi đang áp dụng trên đơn hàng. |
| 4 | Hoàn trả | Button | N/A | N/A | N/A | Mở form hoàn hàng (xem mục 1.8). Hotkey: F9. |
| 5 | Đóng phiên | Button | N/A | N/A | N/A | Mở form đóng phiên thu ngân (xem mục 1.9). Hotkey: F8. |

### 1.2.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Phím tắt | F2 thanh toán, F4 lịch sử đơn hàng, F5 nạp ví RFID, F8 đóng phiên, F9 hoàn hàng, Esc xóa giỏ. |
| 2 | Auto-focus | Sau mỗi thao tác (thêm sản phẩm, thanh toán xong, xóa giỏ), con trỏ tự quay về ô quét mã để sẵn sàng quét tiếp. |
| 3 | Reset khách sau thanh toán | Sau khi hoàn tất thanh toán, hệ thống tự bỏ chọn khách hàng. Tránh trường hợp đơn sau thừa hưởng chiết khấu đơn trước. |
| 4 | Chặn khi chưa mở phiên | Nếu thu ngân chưa mở phiên, hệ thống chặn mọi thao tác bán hàng, hoàn hàng, đóng phiên và hiển thị thông báo yêu cầu mở phiên. |
| 5 | Đa ngôn ngữ | Khi ngôn ngữ thay đổi, tải lại toàn bộ label, nút, tab lọc, tiêu đề cột giỏ hàng, và giá trị cột Loại SP trên tile. |
| 6 | Đơn giá hiển thị danh mục | Mọi ô sản phẩm trên lưới danh mục luôn niêm yết giá bán của đơn vị gốc ban đầu. Các biến động giá khi hoán đổi đơn vị bán lớn hơn chỉ được áp dụng cục bộ vào lưới giỏ hàng. |
| 7 | Bán Combo | Khi thu ngân chọn một Combo, hệ thống thêm đúng một dòng Combo vào giỏ hàng với giá bán tổng của Combo. Danh sách các sản phẩm/dịch vụ thành phần sẽ được hiển thị dưới dạng Tooltip khi người dùng di chuột vào dòng Combo đó. |
| 8 | Giới hạn danh mục (Menu_POS) | Danh sách sản phẩm hiển thị trên màn hình POS được tải tự động theo cấu hình Menu (thực đơn) của từng Điểm bán hàng (máy POS). Hệ thống tự động loại bỏ các sản phẩm thuộc loại "Lưu trú" và "Đồ cho thuê" khỏi màn hình POS bán lẻ, nhằm tránh sai sót trong nghiệp vụ (do các dịch vụ này cần luồng Check-in riêng). |
| 9 | Ghi nhật ký thao tác | Hệ thống tự động ghi nhật ký mỗi khi thu ngân thanh toán hoặc hủy giỏ hàng. Mỗi bản ghi bao gồm mã phiên, hành động, mã nhân viên, và thời gian. Dữ liệu nhật ký phục vụ đối soát và kiểm toán nội bộ. |

### 1.2.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Không có | |

### 1.2.8. Liên kết use-case

- Quét mã vạch và tìm sản phẩm (1.3)
- Giỏ hàng (1.4)
- Thanh toán (1.7)
- Hoàn hàng (1.8)
- Đóng phiên thu ngân (1.9)

---

## 1.3. Quét mã vạch, QR Code và tìm sản phẩm

### 1.3.1. Tổng quan

Hệ thống hỗ trợ 3 luồng đọc mã vạch/QR Code:
1. **Máy quét vật lý / Bàn phím:** Ô tìm kiếm nhận tín hiệu từ máy quét phần cứng hoặc bàn phím (Trigger bằng phím Enter).
2. **Camera / Điện thoại (Webcam / DroidCam):** Tích hợp engine giải mã hình ảnh (ZXing). Hỗ trợ quét live-feed qua Webcam laptop hoặc dùng điện thoại làm máy quét qua mạng LAN (DroidCam). Tần số quét: 400ms/frame.
3. **Quét từ file ảnh:** Upload file ảnh có chứa mã vạch/QR để phần mềm tự bóc tách dữ liệu.

### 1.3.2. Tác nhân

- Thu ngân

### 1.3.3. Biểu đồ use-case
Không có

### 1.3.4. Luồng thao tác

Khi thu ngân nhấn Enter trên ô quét mã, hệ thống thực hiện các bước sau theo thứ tự ưu tiên:

| Ưu tiên | Điều kiện | Hành động |
|---|---|---|
| 1 | Nội dung bắt đầu bằng tiền tố RF- | Hệ thống cắt tiền tố, tra cứu thẻ RFID. Nếu tìm thấy khách hàng thì gắn vào đơn hàng hiện tại. Nếu không tìm thấy thì thông báo lỗi. |
| 2 | Nội dung bắt đầu bằng tiền tố KM- | Áp dụng mã khuyến mãi (xem mục 1.6). |
| 2b | Nội dung bắt đầu bằng tiền tố BK- | Kiểm tra mã Booking đoàn. Nếu hợp lệ, hệ thống hiển thị hộp thoại xác nhận cho biết số lượng quyền lợi chưa sử dụng và hỏi thu ngân có muốn đưa vào giỏ hàng với giá 0đ không. Nếu thu ngân chọn Có, lấy danh sách quyền lợi chưa sử dụng và đưa từng dòng vào giỏ. |
| 2c | Nội dung chứa ký tự * | Hệ thống tách chuỗi thành 2 phần: số lượng và mã sản phẩm. Ví dụ: 5*VE123 nghĩa là thêm 5 đơn vị sản phẩm VE123 vào giỏ. Nếu phần trước dấu * không phải số nguyên dương, xử lý như chuỗi thông thường. |
| 3 | Khớp chính xác mã sản phẩm | So sánh với mã sản phẩm trong danh mục, không phân biệt hoa thường. Nếu tìm thấy thì thêm 1 đơn vị vào giỏ. |
| 4 | Nội dung là số nguyên | Thử tra theo mã định danh nội bộ của sản phẩm. Nếu trùng thì xử lý như khớp chính xác. |
| 5 | Không khớp mã nào | Tìm kiếm mờ theo tên sản phẩm. Nếu trả về đúng 1 kết quả thì xử lý như khớp chính xác. Nếu nhiều hơn 1, thông báo số lượng kết quả và hướng dẫn thu ngân chọn trên danh mục. Nếu không có kết quả nào thì thông báo không tìm thấy. |
| 6 | Khớp sản phẩm nhưng hết hàng (tồn kho = 0) | Nếu sản phẩm là vật tư/hàng hóa có theo dõi tồn kho và số lượng tồn hiện tại bằng 0, hệ thống cảnh báo "Sản phẩm đã hết hàng" và chặn không cho thêm vào giỏ. (Dịch vụ và vé không kiểm tra tồn kho). |

### 1.3.5. Giao diện

#### 1.3.5.1. Mô tả — Khu vực quét mã

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Ô quét mã | Text Edit | No | Text | Blank | Nhận chuỗi ký tự. Xử lý khi Enter. Tự động clear sau khi quét. (*) Placeholder: Quét mã vạch hoặc gõ tên sản phẩm... |
| 2 | Quét qua Camera | Button | N/A | N/A | N/A | Bật cửa sổ live-feed camera. Tự động nhận diện mã và ném sản phẩm vào giỏ. |
| 3 | Quét từ file ảnh | Button | N/A | N/A | N/A | Mở hộp thoại chọn file ảnh chứa mã vạch/QR Code. |

### 1.3.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Tương thích thiết bị đầu vào | Hỗ trợ: (1) Máy quét mã vạch 1D/2D cổng USB (Keyboard wedge). (2) Webcam máy tính. (3) Điện thoại di động (qua DroidCam). |
| 2 | Xóa ô sau khi xử lý | Tự động clear ô text sau mỗi lần nhấn Enter để sẵn sàng nhận luồng quét tiếp theo. |
| 3 | Không phân biệt hoa thường | Case-insensitive. `ve_001` và `VE_001` là như nhau. |
| 4 | Gộp dòng trùng | Nếu sản phẩm đã có trong giỏ, tự động +1 Số lượng. Nếu quét thành phần của Combo thì chỉ gộp vào đúng Combo đó. |
| 5 | Anti-duplicate (Cooldown) | Ở chế độ quét Camera, áp dụng cooldown 2000ms cho cùng một mã để chống tình trạng quét đúp (thêm 2 lần cùng 1 sản phẩm do cầm tay giữ lâu). |
| 6 | Âm thanh xác nhận | Khi camera quét được mã thành công, hệ thống phát âm thanh beep ngắn. |

### 1.3.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Sản phẩm không tồn tại trong danh mục | ERR_POS_PRODUCT_NOT_FOUND |
| 2 | Nhiều sản phẩm trùng tên, không xác định được duy nhất | MSG_POS_MULTI_RESULT |
| 3 | Thẻ RFID không tồn tại hoặc bị vô hiệu hóa | ERR_POS_RFID_NOT_FOUND_OR_INACTIVE |

### 1.3.8. Liên kết use-case

- Giỏ hàng (1.4)
- Khách hàng và chiết khấu thành viên (1.5)
- Khuyến mãi (1.6)

---

## 1.4. Giỏ hàng

### 1.4.1. Tổng quan

Giỏ hàng là danh sách các sản phẩm mà khách hàng đang muốn mua. Mỗi dòng trong giỏ hiển thị tên sản phẩm, đơn giá, số lượng, và thành tiền. Thu ngân có thể tăng, giảm số lượng hoặc xóa từng dòng.

### 1.4.2. Tác nhân

- Thu ngân

### 1.4.3. Biểu đồ use-case
Không có

### 1.4.4. Luồng thao tác
Sử dụng các lưới giỏ hàng để thao tác (thêm/sửa/xóa).

### 1.4.5. Giao diện

#### 1.4.5.1. Mô tả màn hình — Lưới giỏ hàng (Grid Control, Editable)

| STT | Tên cột | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Sản phẩm | Label (Read-only) | N/A | Text | N/A | Tên sản phẩm. Không cho chỉnh sửa. |
| 2 | Đơn giá | Label (Read-only) | N/A | Decimal(15,0) | N/A | Giá bán 1 đơn vị. Định dạng N0 (phân cách hàng nghìn). Không cho chỉnh sửa. |
| 3 | Số lượng | Button Edit (nút [-] [+] và ô số) | Yes | Decimal | 1 | Cho phép tăng, giảm bằng nút hoặc gõ trực tiếp. Hỗ trợ số thập phân (ví dụ: 1.5) nếu mua theo thùng/lốc. Nếu giảm về 0, hệ thống tự xóa dòng khỏi giỏ. |
| 3b | Đơn vị | Lookup Edit (in-grid) | No | Text | Đơn vị gốc | Danh sách đơn vị tính quy đổi kèm giá bán. Khi chọn đơn vị mới, hệ thống cập nhật đơn giá và tính lại thành tiền. |
| 4 | Thành tiền | Label (Read-only, auto-calc) | N/A | Decimal(15,0) | N/A | Bằng Đơn giá nhân Số lượng. Tự cập nhật khi số lượng thay đổi. Định dạng N0. |
| 5 | Xóa | Button (Trash icon) | N/A | N/A | N/A | Xóa dòng khỏi giỏ hàng. Cố định bên phải. |

#### 1.4.5.2. Mô tả màn hình — Bảng tổng hợp (Summary Panel)

| STT | Tên trường | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Tiền hàng | Label (Read-only) | Decimal(15,0) | Tổng thành tiền tất cả các dòng trong giỏ. Định dạng N0. |
| 2 | Thuế VAT | Label (Read-only) | Decimal(15,0) | Tổng thuế của tất cả sản phẩm. Tính từ thuế suất gắn theo từng sản phẩm. |
| 3 | Giảm giá | Label (Read-only) | Decimal(15,0) | Tổng giảm giá đang áp dụng (chiết khấu hạng hoặc khuyến mãi, lấy mức cao hơn). |
| 4 | TỔNG | Label (Read-only, Bold) | Decimal(15,0) | Bằng Tiền hàng cộng Thuế VAT trừ Giảm giá. Đây là số tiền khách phải trả. Font chữ lớn, in đậm. |
| 5 | Gợi ý Khuyến mãi | Label (Read-only) | Text | Hiển thị mô tả nguồn gốc số tiền được giảm giá, đồng thời hiển thị câu gợi ý Upsell (ví dụ: "Gợi ý: Mua thêm 50,000đ để giảm 10%"). Ẩn khi không có giảm giá và không có gợi ý. |

### 1.4.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Gộp dòng trùng | Nếu quét hoặc nhấn tile sản phẩm đã có trong giỏ, hệ thống tăng số lượng dòng đó thêm 1 thay vì tạo dòng mới. Nếu sản phẩm là thành phần được xé lẻ từ một Combo thì chỉ gộp dòng với các thành phần cùng loại thuộc cùng một Combo đó. |
| 2 | Tự tính thành tiền | Mỗi khi số lượng thay đổi, hệ thống tính lại thành tiền của dòng đó và cập nhật bảng tổng hợp. |
| 3 | Xóa dòng khi số lượng bằng 0 | Nếu thu ngân nhấn nút trừ hoặc nhập số lượng bằng 0, hệ thống tự xóa dòng khỏi giỏ. |
| 4 | Xóa toàn bộ | Nút Xóa giỏ sẽ xóa tất cả các dòng sau khi người dùng xác nhận qua hộp thoại. |
| 5 | Quy đổi đơn vị linh hoạt | Nếu sản phẩm có đơn vị quy đổi, thu ngân được phép sửa giá trị tại cột đơn vị tính trên lưới giỏ hàng. Danh sách thả xuống sẽ chỉ hiển thị tên đơn vị tính và giá bán. Khi chọn đơn vị tính mới, hệ thống tự động tải giá bán và tính lại đơn giá cho dòng sản phẩm đó mà không làm ảnh hưởng đến lưới danh mục hiển thị bên ngoài. |
| 6 | Quản lý thành phần Combo | Thu ngân có thể tăng giảm số lượng hoặc xóa từng món thành phần của Combo như một sản phẩm độc lập. Hệ thống ngầm ghi nhận nguồn gốc của sản phẩm để phục vụ việc trừ kho, xuất vé và thống kê doanh thu Combo chính xác. |
| 7 | Gợi ý Upsell (Khuyến mãi) | Mỗi khi giỏ hàng thay đổi, hệ thống sẽ tính toán khoảng cách đến mốc khuyến mãi hoặc mốc chiết khấu hạng thành viên tiếp theo. Nếu giỏ hàng còn thiếu một khoản tiền nhỏ sẽ đạt mốc giảm giá, hệ thống sẽ tự động hiển thị dòng Gợi ý (Upsell) dưới bảng tổng hợp để nhắc thu ngân tư vấn khách mua thêm. |
| 8 | Kiểm tra tồn kho khi tăng số lượng | Khi thu ngân tăng số lượng sản phẩm vật tư trên giỏ hàng hoặc thêm sản phẩm đã có trong giỏ, hệ thống kiểm tra tổng số lượng (quy đổi về đơn vị gốc) không vượt quá tồn kho hiện tại tại kho bán của phiên. Nếu vượt, hệ thống cảnh báo và tự động giới hạn số lượng về mức tối đa có thể bán. |

### 1.4.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Không có | |

### 1.4.8. Liên kết use-case

- Quét mã vạch và tìm sản phẩm (1.3)
- Thanh toán (1.7)

---

## 1.5. Khách hàng và chiết khấu thành viên

### 1.5.1. Tổng quan

Hệ thống cho phép gắn khách hàng thành viên vào đơn hàng. Khi gắn, hệ thống tự tính chiết khấu theo hạng (Bạc 5%, Vàng 10%, Kim cương 15%). Chiết khấu chỉ áp dụng cho các dòng sản phẩm đủ điều kiện giảm giá (không áp cho đồ ăn, đồ uống).

### 1.5.2. Tác nhân

- Thu ngân

### 1.5.3. Biểu đồ use-case
Không có

### 1.5.4. Luồng thao tác

#### 1.5.4.1. Tình huống 1 — Gắn khách hàng bằng số điện thoại

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Nhập số điện thoại vào ô Tìm khách, nhấn nút Tìm. | Tra cứu trong danh sách khách hàng. Nếu tìm thấy, hiển thị tên, hạng, số điểm. Nếu không, thông báo không tìm thấy. |
| 2 | — | Hệ thống tự tính lại bảng tổng hợp với mức chiết khấu mới theo hạng thành viên. |

#### 1.5.4.2. Tình huống 2 — Gắn khách hàng bằng quét RFID

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Quét vòng tay RFID vào ô quét mã (tiền tố RF-). | Cắt tiền tố, tra cứu thẻ RFID. Nếu tìm thấy, hiển thị thông tin khách hàng. |

#### 1.5.4.3. Tình huống 3 — Bỏ chọn khách hàng

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Nhấn nút bỏ chọn (X). | Xóa thông tin khách khỏi đơn. Chiết khấu hạng về 0%. Tính lại bảng tổng hợp. |

### 1.5.5. Giao diện
Không có giao diện riêng.

### 1.5.6. Mô tả nghiệp vụ

#### 1.5.6.1. Quy tắc tính giảm giá

Hệ thống xử lý 2 nguồn giảm giá theo quy tắc lấy mức cao nhất (không cộng dồn), trừ khi chương trình khuyến mãi cho phép chồng chéo:

| Nguồn | Cách tính | Ví dụ |
|---|---|---|
| Chiết khấu hạng | Phần trăm trên tổng tiền các dòng được giảm. Tỷ lệ lấy từ bảng cấu hình theo hạng thành viên. | Vàng 10% trên 500,000đ giảm 50,000đ |
| Khuyến mãi | Giảm theo phần trăm hoặc số tiền cố định (xem mục 1.6). | Mã khuyến mãi giảm 30,000đ |
| Kết quả | Lấy mức lớn hơn giữa 2 nguồn (mặc định không chồng chéo). | So sánh **số tiền được giảm** thực tế. Ví dụ: max(50,000 ; 30,000) lấy 50,000đ. |

#### 1.5.6.2. Các quy tắc khác

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | F&B không giảm giá | Đồ ăn, đồ uống, và nguyên liệu không bao giờ được tính vào tổng tiền giảm giá. |
| 2 | Chồng chéo có điều kiện | Nếu chương trình khuyến mãi được cấu hình cho phép chồng chéo, chiết khấu hạng và khuyến mãi được cộng dồn thay vì lấy mức lớn hơn. |
| 3 | Khách vãng lai | Nếu không gắn khách hàng, chiết khấu hạng bằng 0%. Chỉ có thể áp dụng khuyến mãi bằng cách quét mã thủ công. |
| 4 | Tỷ lệ cấu hình động | Phần trăm chiết khấu theo hạng lấy từ bảng cấu hình hệ thống. Quản trị viên có thể thay đổi tỷ lệ mà không cần sửa phần mềm. |

### 1.5.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Không tìm thấy khách hàng theo SĐT hoặc mã | ERR_POS_CUSTOMER_NOT_FOUND |
| 2 | Thẻ RFID không gắn với khách hàng nào | ERR_POS_RFID_NOT_FOUND_OR_INACTIVE |

### 1.5.8. Liên kết use-case

- Màn hình bán hàng chính (1.2)
- Thanh toán (1.7)

---

## 1.6. Khuyến mãi

### 1.6.1. Tổng quan

Thu ngân quét hoặc gõ mã khuyến mãi (dạng KM-ABC123) vào ô quét mã. Hệ thống kiểm tra tính hợp lệ, điều kiện áp dụng, và tính số tiền giảm.

### 1.6.2. Tác nhân

- Thu ngân

### 1.6.3. Biểu đồ use-case
Không có

### 1.6.4. Luồng thao tác

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Quét hoặc gõ mã khuyến mãi vào ô quét mã. | Tra cứu mã trong danh sách chương trình khuyến mãi đang hoạt động. |
| 2 | — | Kiểm tra: (a) mã có tồn tại không, (b) còn lượt sử dụng không, (c) đơn hàng đạt giá trị tối thiểu không, (d) khách hàng có thuộc đối tượng áp dụng không. |
| 3 | — | Nếu hợp lệ, hiển thị tên chương trình và số tiền giảm trên bảng tổng hợp. Nếu không hợp lệ, thông báo lý do từ chối cụ thể. |
| 4 | Nếu muốn hủy, nhấn nút Xóa KM. | Xóa khuyến mãi khỏi đơn, tính lại bảng tổng hợp. |

### 1.6.5. Giao diện
Không có giao diện riêng.

### 1.6.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Loại giảm giá | Hỗ trợ 2 loại: giảm theo phần trăm trên tổng tiền được giảm, hoặc giảm cố định một số tiền. |
| 2 | Giới hạn lượt dùng | Mỗi mã khuyến mãi có thể giới hạn tổng số lượt sử dụng. Khi hết lượt, hệ thống từ chối áp dụng. |
| 3 | Đơn tối thiểu | Mỗi mã khuyến mãi có thể yêu cầu giá trị đơn hàng tối thiểu. Nếu chưa đạt, hệ thống từ chối. |
| 4 | Điều kiện đối tượng | Một số mã khuyến mãi chỉ dành cho hạng thành viên nhất định (ví dụ: chỉ khách Vàng, Kim cương). |
| 5 | Ghi lịch sử | Sau khi thanh toán thành công, hệ thống ghi lại mã khuyến mãi đã dùng, mã đơn hàng, và số tiền giảm thực tế vào bảng lịch sử. |

### 1.6.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Chưa nhập mã khuyến mãi | ERR_KM_EMPTY |
| 2 | Mã khuyến mãi không tồn tại | ERR_KM_NOT_FOUND |
| 3 | Mã khuyến mãi đã hết lượt sử dụng | ERR_KM_HET_LUOT |
| 4 | Đơn hàng chưa đạt giá trị tối thiểu | ERR_KM_DON_TOI_THIEU |
| 5 | Khách hàng không thuộc đối tượng áp dụng | ERR_KM_DIEU_KIEN |

### 1.6.8. Liên kết use-case

- Quét mã vạch và tìm sản phẩm (1.3)
- Thanh toán (1.7)

---

## 1.7. Thanh toán

### 1.7.1. Tổng quan

Khi giỏ hàng đã sẵn sàng, thu ngân nhấn F2 hoặc nút Thanh toán để mở form thanh toán. Form này cho phép chia nhỏ số tiền thanh toán thành nhiều phương thức khác nhau (ví dụ: trả 200,000đ tiền mặt, trả 100,000đ bằng ví RFID).

### 1.7.2. Tác nhân

- Thu ngân

### 1.7.3. Biểu đồ use-case
Không có

### 1.7.4. Luồng thao tác

#### 1.7.4.1. Tình huống 1 — Thanh toán tiền mặt đơn giản

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Nhấn F5. | Mở form thanh toán. Hiển thị tổng số tiền cần thu. Hệ thống tự thêm 1 dòng phương thức Tiền mặt với số tiền bằng tổng cần thu. |
| 2 | Nhập số tiền khách đưa (có thể lớn hơn tổng). | Hiển thị tiền thừa bằng tiền khách đưa trừ tổng cần thu. |
| 3 | Nhấn nút Xác nhận. | Kiểm tra tổng đã trả lớn hơn hoặc bằng tổng phải thu. Nếu đủ, hệ thống tạo đơn hàng, trừ tồn kho (nếu là vật tư), phát hành vé điện tử (nếu là vé), cộng điểm tích lũy (nếu có khách hàng), và lập chứng từ tài chính. |
| 4 | — | Hiển thị form Kết quả thanh toán gồm mã đơn, tiền thừa, danh sách vé đã phát. Thu ngân có thể chọn In Hóa Đơn hoặc In Vé. Khi đóng form, hệ thống xóa giỏ hàng, bỏ chọn khách hàng và sẵn sàng cho đơn mới. |

#### 1.7.4.2. Tình huống 2 — Thanh toán hỗn hợp (tiền mặt kết hợp ví RFID)

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Mở form thanh toán. Thêm dòng phương thức Ví RFID với số tiền 100,000đ. | Hiện panel nhập mã thẻ RFID. |
| 2 | Quét hoặc gõ mã thẻ RFID. | Tra cứu thẻ, xác minh số dư trong ví. Nếu đủ thì xác nhận. Nếu không đủ thì thông báo lỗi. |
| 3 | Thêm dòng Tiền mặt với phần còn lại. Nhấn nút Xác nhận. | Trừ tiền ví RFID, thu tiền mặt, hoàn tất đơn hàng. |

#### 1.7.4.3. Tình huống 3 — Thanh toán bằng điểm tích lũy

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Mở form thanh toán. Tích ô Dùng điểm tích lũy. | Tính số điểm tối đa được dùng dựa trên: (a) số điểm khách đang có, (b) giới hạn phần trăm tối đa trên tổng đơn (cấu hình hệ thống, mặc định 50%). Quy đổi điểm ra tiền theo tỷ lệ cấu hình (mặc định: 1 điểm tương đương 1,000đ). |
| 2 | Thu ngân nhập phần tiền mặt còn lại. Nhấn nút Xác nhận. | Trừ điểm, thu tiền mặt, hoàn tất đơn hàng. |

### 1.7.5. Giao diện

#### 1.7.5.1. Mô tả màn hình — Form thanh toán

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tổng cần thu | Label (Read-only, Bold) | N/A | Decimal(15,0) | N/A | Tổng tiền khách cần trả. Font chữ lớn, in đậm. Chỉ đọc. |
| 2 | Lưới phương thức | Grid Control (Editable) | Yes | — | 1 dòng Tiền mặt | Gồm 2 cột: Phương thức (Image Combo Box) và Số tiền (Spin Edit). Cho phép thêm, xóa dòng. |
| 3 | Phương thức (cột trong lưới) | Image Combo Box (in-grid) | Yes | Text | TienMat | Chọn phương thức: Tiền mặt, Chuyển khoản, Ví RFID, QR, MoMo, Thẻ ngân hàng. |
| 4 | Số tiền (cột trong lưới) | Spin Edit (in-grid) | Yes | Decimal(15,0) | 0 | Số tiền khách trả theo phương thức này. Định dạng N0. Không cho nhập số âm. |
| 5 | Panel RFID | Text Edit | Conditional | Text | Blank | Chỉ hiển thị khi có ít nhất 1 dòng chọn phương thức Ví RFID. Thu ngân quét hoặc gõ mã thẻ vào đây. |
| 5b | Thao tác nhanh | Button | N/A | N/A | N/A | Cụm nút (Vừa đủ, 500.000, 1.000.000) giúp thu ngân điền nhanh số tiền khách đưa vào dòng Tiền mặt. |
| 6 | Dùng điểm tích lũy | Check Box / Spin Edit | No | Boolean/Integer | Unchecked/0 | Chỉ hiển thị khi khách hàng có điểm. Nếu đơn hàng đã được giảm giá (KM/VIP), phần nhập điểm sẽ bị khóa và báo đỏ. |
| 7 | Điểm sẽ dùng | Label (Read-only) | N/A | Text | — | Hiển thị số điểm sẽ dùng và số tiền quy đổi tương ứng. Ví dụ: 50 điểm tương đương 50,000đ. |
| 8 | Đã trả | Label (Read-only) | N/A | Decimal(15,0) | 0 | Tổng tiền đã nhập từ tất cả các dòng. Đổi màu xanh khi đủ, đỏ khi thiếu. |
| 9 | Còn thiếu | Label (Read-only) | N/A | Decimal(15,0) | 0 | Bằng Tổng cần thu trừ Đã trả. Ẩn khi đã trả đủ. |
| 10 | Tiền thừa | Label (Read-only) | N/A | Decimal(15,0) | 0 | Bằng Đã trả trừ Tổng cần thu. Chỉ hiện khi Đã trả lớn hơn Tổng cần thu. |
| 11 | Nút Xác nhận | Button | N/A | N/A | N/A | Bị vô hiệu hóa cho đến khi tổng đã trả lớn hơn hoặc bằng tổng cần thu. |
| 12 | Nút Hủy | Button | N/A | N/A | N/A | Đóng form, quay lại màn hình bán hàng. Giỏ hàng được giữ nguyên. |

#### 1.7.5.2. Mô tả màn hình — Form kết quả thanh toán (In hóa đơn / In vé)

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Mã đơn | Label (Read-only) | N/A | Text | N/A | Hiển thị mã đơn hàng. |
| 2 | Tiền thừa | Label (Read-only) | N/A | Decimal(15,0) | N/A | Số tiền thừa trả lại cho khách. |
| 3 | Lưới vé phát hành | Grid Control (Read-only) | N/A | — | N/A | Hiển thị danh sách vé (Mã vạch, Tên vé) nếu đơn hàng có vé. Ẩn nếu không có vé. |
| 4 | In Hóa Đơn | Button | N/A | N/A | N/A | Sử dụng DevExpress XtraReport để preview và in hóa đơn khổ K80. |
| 5 | In Vé | Button | N/A | N/A | N/A | Mở giao diện preview danh sách thẻ vé điện tử (QR Code) để in. Bị vô hiệu hóa nếu đơn không có vé. |
| 6 | Đóng | Button | N/A | N/A | N/A | Đóng form. Hotkey: Enter hoặc Esc. |

### 1.7.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Không cho số tiền âm | Mọi dòng thanh toán phải có số tiền lớn hơn hoặc bằng 0. Nếu nhập âm, hệ thống thông báo lỗi và chặn xác nhận. |
| 2 | Kiểm tra số dư RFID | Trước khi xác nhận, hệ thống tra cứu thẻ qua mã RFID, kiểm tra số dư ví lớn hơn hoặc bằng số tiền dòng đó. |
| 3 | Kiểm tra số dư điểm | Nếu dùng điểm, hệ thống xác minh số điểm hiện tại của khách lớn hơn hoặc bằng số điểm sẽ dùng. |
| 4 | Giao dịch nguyên tử | Toàn bộ quá trình sau khi xác nhận thanh toán (tạo đơn hàng, trừ kho, trừ ví, cộng điểm, lập chứng từ) diễn ra trong một giao dịch duy nhất. Nếu bất kỳ bước nào thất bại, hệ thống hủy toàn bộ, không lưu dữ liệu nửa vời. |
| 5 | Phát vé điện tử | Nếu đơn hàng chứa sản phẩm loại Vé, hệ thống tự phát hành vé điện tử kèm mã QR. Số lượng vé bằng đúng số lượng vé đã mua. |
| 6 | Tích điểm | Nếu đơn có gắn khách hàng, hệ thống cộng điểm theo tỷ lệ cấu hình. Điểm được tính trên **số tiền khách thực trả** (sau khi đã trừ mọi chiết khấu và khuyến mãi). |
| 7 | Tiền thừa | Tiền thừa bằng tổng đã trả trừ tổng cần thu. Hiển thị trên thông báo kết quả để thu ngân trả lại khách. |
| 8 | Trừ tồn kho theo đơn vị gốc | Các mặt hàng có khả năng theo dõi vật lý, khi bán theo đơn vị quy đổi lớn hơn, kho hàng tự động quy đổi thành lượng hệ số và giảm trực tiếp theo đơn vị gốc nhằm đảm bảo số liệu đối soát chính xác tuyệt đối. |
| 9 | Không cộng dồn điểm | Nếu đơn hàng đã áp dụng mã khuyến mãi hoặc chiết khấu hạng thành viên, hệ thống sẽ chặn không cho phép thanh toán bằng điểm tích lũy (không áp dụng đồng thời giảm giá và trừ điểm). |
| 10 | Thông báo nổi | Sau khi thanh toán thành công, hệ thống hiển thị thông báo nổi (toast) trong 3 giây gồm mã đơn, tiền thừa, và số vé phát hành (nếu có). |

### 1.7.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Giỏ hàng không được rỗng | ERR_POS_CART_EMPTY |
| 2 | Phải có phiên thu ngân đang mở | ERR_POS_NO_OPEN_SESSION |
| 3 | Tổng tiền đã trả phải lớn hơn hoặc bằng tổng cần thu | ERR_POS_PAYMENT_INSUFFICIENT |
| 4 | Ví RFID: phải có mã thẻ hợp lệ | ERR_POS_RFID_PAYMENT_NO_ID |
| 5 | Ví RFID: số dư ví không đủ | ERR_POS_RFID_INSUFFICIENT_BALANCE |
| 6 | Điểm tích lũy không đủ | ERR_POS_INSUFFICIENT_POINTS |
| 7 | Số tiền thanh toán không được âm | ERR_PAY_NEGATIVE_AMOUNT |

### 1.7.8. Liên kết use-case

- Giỏ hàng (1.4)
- Khách hàng và chiết khấu thành viên (1.5)
- Khuyến mãi (1.6)

---

## 1.8. Hoàn hàng và hoàn vé

### 1.8.1. Tổng quan

Khi khách hàng muốn trả hàng hoặc hủy vé chưa sử dụng, thu ngân truy cập chức năng hoàn hàng (F9). Hệ thống tìm đơn hàng gốc theo mã, hiển thị danh sách các dòng có thể hoàn, tính số tiền hoàn theo tỷ lệ thực trả (sau giảm giá), và lập phiếu chi hoàn tiền.

### 1.8.2. Tác nhân

- Thu ngân (thực hiện hoàn hàng)
- Quản lý (phê duyệt nếu quy trình yêu cầu)

### 1.8.3. Biểu đồ use-case
Không có

### 1.8.4. Luồng thao tác

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Nhấn F9 hoặc nút Hoàn trả. | Kiểm tra phiên thu ngân đang mở. Nếu có phiên, hệ thống mở form hoàn hàng. Nếu chưa có phiên, thông báo yêu cầu mở phiên trước. |
| 2 | Nhập mã đơn hàng cần hoàn, nhấn nút Tìm. | Tra cứu đơn hàng. Nếu không tìm thấy hoặc đơn đã hủy, hệ thống thông báo cho thu ngân. Nếu tìm thấy, hiển thị thông tin đơn (ngày tạo, khách hàng, tổng thanh toán) cùng danh sách chi tiết. |
| 3 | Với mỗi dòng muốn hoàn: nhập số lượng hoàn và lý do. | Kiểm tra ngay tại ô nhập: số lượng hoàn không vượt quá số lượng còn lại (bằng số đã mua trừ đi số đã hoàn trước đó). Nếu vượt, hệ thống cảnh báo ngay tại ô nhập. |
| 4 | Nhấn nút Hoàn hàng. | Hiển thị hộp thoại xác nhận với nội dung cảnh báo thao tác này không thể phục hồi. |
| 5 | Nhấn nút Có. | Thực hiện hoàn hàng trong một giao dịch (xem mục 1.8.5). |

### 1.8.5. Giao diện

#### 1.8.5.1. Mô tả màn hình — Form hoàn hàng

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Mã đơn hàng | Text Edit | Yes | Text | Blank | Nhập mã đơn hàng cần hoàn. (*) Tooltip: Nhập mã đơn hàng gốc |
| 2 | Nút Tìm | Button | N/A | N/A | N/A | Tra cứu và hiển thị thông tin đơn hàng bên dưới. |
| 3 | Thông tin đơn | Label (Read-only) | N/A | Text | Trống | Hiển thị ngày tạo, mã khách hàng (hoặc Khách lẻ), tổng thanh toán. Chỉ hiện sau khi tìm thấy đơn. |

#### 1.8.5.2. Mô tả màn hình — Lưới chi tiết hoàn hàng (Grid Control, Editable)

| STT | Tên cột | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Sản phẩm | Label (Read-only) | N/A | Text | N/A | Tên sản phẩm trong đơn hàng gốc. |
| 2 | Loại | Label (Read-only) | N/A | Text | N/A | Loại sản phẩm (Vé, Hàng hóa, F&B...). Giá trị hiển thị được dịch đa ngôn ngữ. |
| 3 | Đã mua | Label (Read-only) | N/A | Integer | N/A | Số lượng đã mua trong đơn gốc. |
| 4 | Đã hoàn | Label (Read-only) | N/A | Integer | 0 | Số lượng đã hoàn từ các lần hoàn trước. |
| 5 | Đơn giá | Label (Read-only) | N/A | Decimal(15,0) | N/A | Đơn giá lúc mua. Định dạng N0. |
| 6 | SL muốn hoàn | Spin Edit (in-grid) | Conditional | Integer | 0 | Số lượng muốn hoàn lần này. Tối đa bằng Đã mua trừ Đã hoàn. Kiểm tra ngay khi rời ô. (*) Tooltip: Nhập số lượng muốn hoàn |
| 7 | Lý do | Text Edit (in-grid) | Conditional | Text | Blank | Lý do hoàn. Bắt buộc nhập khi SL muốn hoàn > 0. Nếu để trống hệ thống sẽ chặn hoàn. |

#### 1.8.5.3. Mô tả màn hình — Thanh nút

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Nút Hoàn hàng | Button | N/A | N/A | N/A | Thực hiện hoàn. Bị vô hiệu hóa khi chưa nhập số lượng hoàn cho bất kỳ dòng nào. |
| 2 | Nút Đóng | Button | N/A | N/A | N/A | Đóng form, không thực hiện hoàn. |

### 1.8.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Hoàn theo tỷ lệ thực trả | Nếu đơn hàng gốc có giảm giá tổng bill (chiết khấu hạng, khuyến mãi), số tiền hoàn mỗi dòng phải tính đúng theo tỷ lệ khách thực chi. Ví dụ: mua áo 100,000đ, bill được giảm 10%, khách chỉ trả 90,000đ. Khi hoàn, hệ thống trả lại 90,000đ chứ không phải 100,000đ. |
| 2 | Giới hạn thời gian hoàn | Vé (vé vào khu, vé trò chơi): mặc định không cho hoàn vì vé chỉ có giá trị trong ngày mua. Hàng hóa: cho phép hoàn trong 7 ngày kể từ ngày mua. Giới hạn này lấy từ bảng cấu hình hệ thống, quản trị viên có thể thay đổi mà không cần sửa phần mềm. |
| 3 | Hủy vé chưa sử dụng | Nếu dòng hoàn là Vé, hệ thống tìm các vé điện tử đã phát hành mà chưa quét cổng, rồi hủy đúng số lượng hoàn. Nếu vé đã bị quét sử dụng, hệ thống từ chối hoàn vé đó. |
| 4 | Nhập trả kho | Hàng hóa vật lý (có đánh dấu là vật tư) được nhập trả về kho mặc định gắn với máy POS. Dịch vụ và đồ ăn, đồ uống không nhập kho. |
| 5 | Phân bổ hoàn tiền | Hệ thống tự phân bổ tiền hoàn theo phương thức mà khách đã trả lúc mua. Ưu tiên hoàn vào ví RFID trước, sau đó đến chuyển khoản và ví điện tử, cuối cùng mới mở két chi tiền mặt. |
| 6 | Thu hồi điểm | Nếu lúc mua khách được cộng điểm tích lũy, hệ thống sẽ trừ lại phần điểm tương ứng với giá trị hàng hoàn. Cho phép điểm âm (theo quy tắc kế toán). |
| 7 | Cập nhật trạng thái đơn | Nếu tất cả dòng chi tiết trong đơn đã hoàn hết, trạng thái đơn hàng chuyển thành Đã hoàn. Nếu chỉ hoàn một phần thì trạng thái dòng ghi nhận là Hoàn một phần. |
| 8 | Mã phiếu chi duy nhất | Mỗi lần hoàn tạo ra 1 phiếu chi riêng với mã duy nhất (kèm dấu thời gian). Cho phép hoàn nhiều lần cho cùng 1 đơn hàng mà không bị trùng mã. |
| 9 | Lý do bắt buộc | Bắt buộc nhập lý do hoàn cho mỗi dòng sản phẩm. Nếu để trống, hệ thống chặn không cho hoàn. |

### 1.8.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Mã đơn hàng không được để trống | ERR_REFUND_EMPTY_CODE |
| 2 | Đơn hàng không tồn tại hoặc đã hủy | ERR_REFUND_NOT_FOUND |
| 3 | Phải có ít nhất 1 dòng có số lượng hoàn lớn hơn 0 | ERR_HOAN_NO_ITEMS |
| 4 | Số lượng hoàn vượt quá số lượng còn lại | ERR_REFUND_OVER_QTY |
| 5 | Số lượng hoàn không được âm | ERR_REFUND_NEGATIVE_QTY |
| 6 | Lý do hoàn không được để trống | ERR_HOAN_REASON_REQUIRED |
| 7 | Vé không được hoàn (theo cấu hình) | ERR_HOAN_VE_KHONG_DUOC_HOAN |
| 8 | Đơn hàng đã quá hạn hoàn | ERR_HOAN_QUA_HAN |
| 9 | Vé đã được quét sử dụng, không thể hoàn | PARTIAL_TICKET_SCANNED |

### 1.8.8. Liên kết use-case

- Phiên thu ngân (1.1)
- Màn hình bán hàng chính (1.2)

---

## 1.9. Đóng phiên thu ngân

### 1.9.1. Tổng quan

Khi kết thúc ca, thu ngân đóng phiên (F8). Hệ thống tính tổng doanh thu trong ca, yêu cầu nhập số tiền mặt thực tế trong két, và tính chênh lệch. Kết quả chốt ca được lưu lại để phục vụ đối soát.

### 1.9.2. Tác nhân

- Thu ngân

### 1.9.3. Biểu đồ use-case
Không có

### 1.9.4. Luồng thao tác

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Nhấn F8 hoặc nút Đóng phiên. | Kiểm tra phiên đang mở. Nếu có phiên, hệ thống kiểm tra trạng thái giỏ hàng. |
| 2 | Giỏ hàng có sản phẩm chưa thanh toán | Hệ thống hiển thị hộp thoại cảnh báo: Giỏ hàng đang có sản phẩm chưa thanh toán. Đóng phiên sẽ hủy toàn bộ giỏ hàng này. Bạn có chắc chắn muốn tiếp tục?. Nếu thu ngân chọn Không, hủy đóng phiên. Nếu chọn Có, tiếp tục bước 3. |
| 3 | Đếm tiền trong két, nhập số tiền thực tế. Nhập ghi chú (tùy chọn). Nhấn nút Chốt Ca. | Hệ thống tính tổng thu trong ca (từ các chứng từ thu), cập nhật trạng thái phiên thành Đã đóng, ghi thời gian đóng. Xóa giỏ hàng hiện tại nếu có. |
| 4 | — | Hiển thị bảng chốt ca gồm tổng thu hệ thống và mức chênh lệch trực tiếp trên form (không đóng form ngay). Nút Chốt Ca biến thành nút Đóng. Thu ngân xem kết quả rồi nhấn Đóng. |

### 1.9.5. Giao diện

#### 1.9.5.1. Mô tả màn hình — Form đóng phiên

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tiền đầu ca | Label (Read-only) | N/A | Decimal(15,0) | N/A | Số tiền nhập lúc mở phiên. Chỉ đọc, định dạng N0. |
| 2 | Tiền cuối ca (thực tế) | Spin Edit | Yes | Decimal(15,0) | 0 | Số tiền mặt thực tế trong két khi đóng. Định dạng N0. (*) Tooltip: Đếm tiền trong két rồi nhập vào đây |
| 3 | Tổng thu hệ thống | Label (Read-only) | N/A | Decimal(15,0) | N/A | (Chỉ hiện SAU khi chốt ca) Tổng các chứng từ thanh toán tiền mặt. |
| 4 | Chênh lệch | Label (Read-only) | N/A | Decimal(15,0) | 0 | (Chỉ hiện SAU khi chốt ca) Bằng Tiền cuối ca trừ đi (Tiền đầu ca cộng Tổng thu). Dương là thừa, âm là thiếu. Đổi màu đỏ nếu thiếu. |
| 5 | Ghi chú | Memo Edit | No | Text | Blank | Ghi chú tùy ý. |
| 6 | Nút Chốt Ca / Đóng | Button | N/A | N/A | N/A | Ban đầu là nút Chốt Ca. Sau khi chốt thành công thì chuyển thành nút Đóng. |

### 1.9.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Tổng thu tự động | Tổng thu được tính tự động từ các hóa đơn trong ca, không cho phép sửa tay. |
| 2 | Cho phép chênh lệch | Hệ thống không chặn đóng phiên khi có chênh lệch. Chênh lệch được ghi nhận để quản lý xử lý sau. |
| 3 | Khóa phiên | Sau khi đóng phiên, phiên chuyển trạng thái Đã đóng và không thể mở lại. Nhân viên muốn bán tiếp phải mở phiên mới. |
| 4 | Xóa giỏ hàng | Khi đóng phiên, giỏ hàng hiện tại bị xóa. Dữ liệu chưa thanh toán sẽ mất. |
| 5 | Đối soát cảnh báo | Hệ thống tự động tính toán chênh lệch giữa (Tiền đầu ca + Tổng thu) và Tiền thực tế. Nếu phát hiện thừa hoặc thiếu tiền, số chênh lệch được highlight màu đỏ cảnh báo thu ngân, đồng thời ghi log "Chênh lệch két" để chuyển lên cấp Quản lý đối soát. |

### 1.9.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Chưa có phiên đang mở | ERR_POS_NO_OPEN_SESSION |
| 2 | Tiền cuối ca phải là số không âm | ERR_POS_INVALID_CLOSING_CASH |

### 1.9.8. Liên kết use-case

- Phiên thu ngân (1.1)
- Màn hình bán hàng chính (1.2)

---

## 1.10. Lịch sử đơn hàng

### 1.10.1. Tổng quan

Thu ngân có thể xem lại danh sách các đơn hàng đã thanh toán thành công trong phiên thu ngân hiện tại, đồng thời có thể in lại hóa đơn nếu cần.

### 1.10.2. Tác nhân

- Thu ngân

### 1.10.3. Biểu đồ use-case
Không có

### 1.10.4. Luồng thao tác

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Nhấn F4. | Kiểm tra phiên thu ngân đang mở. Nếu có, mở form Lịch sử đơn hàng và tải danh sách các đơn của phiên này. |
| 2 | Nhấp đúp vào một dòng đơn hàng. | Mở hộp thoại Kết quả thanh toán của đơn hàng đó, cho phép thu ngân chọn in lại hóa đơn. |

### 1.10.5. Giao diện

#### 1.10.5.1. Lưới lịch sử đơn hàng (Grid Control, Read-only)

| STT | Tên hiển thị | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Mã đơn hàng | Label | Text | Mã hóa đơn duy nhất. |
| 2 | Khách hàng | Label | Text | Tên khách hàng (nếu có). |
| 3 | Thời gian | Label | DateTime | Thời điểm thanh toán. |
| 4 | Tổng tiền | Label | Decimal(15,0) | Tổng tiền khách đã trả. Định dạng N0. |

### 1.10.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Phạm vi hiển thị | Chỉ hiển thị các đơn hàng thuộc phiên thu ngân Đang mở của chính thu ngân đó. Không hiển thị đơn của ca trước hoặc của nhân viên khác. |
| 2 | Không tự động in | Khi nhấp đúp xem chi tiết, hệ thống mở form Kết quả thanh toán nhưng không tự động đẩy lệnh xuống máy in nhiệt để tránh in thừa, mà phải do thu ngân chủ động nhấn nút In Hóa Đơn hoặc In Vé. |

### 1.10.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Không có | |

### 1.10.8. Liên kết use-case

- Màn hình bán hàng chính (1.2)

---

## 1.11. Nạp ví RFID

### 1.11.1. Tổng quan

Khách hàng có thể nạp thêm tiền vào vòng tay RFID trực tiếp tại quầy POS bán lẻ thay vì phải đến quầy trung tâm.

### 1.11.2. Tác nhân

- Thu ngân

### 1.11.3. Biểu đồ use-case
Không có

### 1.11.4. Luồng thao tác

| | Thu ngân | Hệ thống |
|---|---|---|
| 1 | Nhấn F5. | Mở form Nạp tiền Ví RFID. |
| 2 | Quét vòng tay RFID. | Truy xuất thông tin khách hàng và số dư hiện tại. |
| 3 | Nhập số tiền khách muốn nạp và chọn phương thức thanh toán. Nhấn Xác nhận. | Cộng tiền vào số dư ví RFID, ghi nhận giao dịch nạp tiền, in biên lai nạp tiền. Nếu khách hàng này đang được gắn trong giỏ hàng hiện tại, hệ thống tự động làm mới số dư ví hiển thị trên POS. |

### 1.11.5. Giao diện

(Chi tiết giao diện Nạp ví RFID được mô tả ở SRS Tài Chính. Tại đây chỉ gọi form sang).

### 1.11.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Làm mới số dư | Sau khi nạp thành công và đóng form, nếu màn hình POS đang chọn đúng vị khách đó, hệ thống tự động cập nhật số dư hiển thị mới nhất để khách có thể mua hàng ngay lập tức. |

### 1.11.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | (Xem thêm SRS Tài Chính) | |

### 1.11.8. Liên kết use-case

- Màn hình bán hàng chính (1.2)

---

# 2. Yêu cầu khác

## 2.1. Định dạng dữ liệu

### 2.1.1. Ngày giờ

- Định dạng ngày mặc định: dd/MM/yyyy. Ví dụ: 21/04/2026
- Định dạng giờ: HH:mm. Ví dụ: 08:30

### 2.1.2. Số

- Số tiền: dấu phẩy phân cách hàng nghìn, không có chữ số thập phân, kèm ký hiệu đ. Ví dụ: 150,000đ
- Số lượng: số nguyên dương. Ví dụ: 3
- Điểm tích lũy: số nguyên. Ví dụ: 1,250

## 2.2. Danh mục dữ liệu tham chiếu

### 2.2.1. Phương thức thanh toán

| Mã | Tên hiển thị |
|---|---|
| TienMat | Tiền mặt |
| ChuyenKhoan | Chuyển khoản ngân hàng |
| ViRFID | Ví vòng tay RFID |
| QR | Quét mã QR |
| MoMo | Ví MoMo |
| TheNganHang | Thẻ ngân hàng |
| DiemTichLuy | Điểm tích lũy |

### 2.2.2. Hạng thành viên và chiết khấu

| Hạng | Tỷ lệ chiết khấu | Điều kiện |
|---|---|---|
| Bạc (Silver) | 5% | Tổng chi tiêu từ 2,000,000đ |
| Vàng (Gold) | 10% | Tổng chi tiêu từ 10,000,000đ |
| Kim cương (Diamond) | 15% | Tổng chi tiêu từ 50,000,000đ |

Tỷ lệ chiết khấu lấy từ bảng cấu hình hệ thống. Giá trị trên là mặc định.

### 2.2.3. Trạng thái phiên thu ngân

| Mã | Tên hiển thị |
|---|---|
| DangMo | Đang mở |
| DaDong | Đã đóng |

### 2.2.4. Trạng thái đơn hàng

| Mã | Tên hiển thị |
|---|---|
| DaThanhToan | Đã thanh toán |
| DaHoan | Đã hoàn (toàn bộ) |
| DaHuy | Đã hủy |

### 2.2.5. Cấu hình POS liên quan

| Khóa cấu hình | Giá trị mặc định | Ý nghĩa |
|---|---|---|
| DIEM_QUY_DOI_1000D | 1 | Mỗi 1,000đ chi tiêu được 1 điểm |
| DIEM_CAP_PHAN_TRAM | 50 | Tối đa dùng điểm thanh toán 50% tổng đơn |
| HOAN_GIOI_HAN_GIO_VE | 0 | Vé: 0 nghĩa là không cho hoàn |
| HOAN_GIOI_HAN_GIO_HANGHOA | 168 | Hàng hóa: hoàn trong 168 giờ (7 ngày) |

## 2.3. Bảng mã thông báo lỗi

| Mã thông báo | Nội dung tiếng Việt |
|---|---|
| ERR_POS_TONKHO_KHONGDU | Tồn kho không đủ! (Hiện còn: {0}) |
| ERR_POS_NO_MACHINE | Bắt buộc chọn máy POS |
| ERR_POS_INVALID_OPENING_CASH | Tiền đầu ca phải là số không âm |
| ERR_POS_CART_EMPTY | Giỏ hàng trống, không thể thanh toán |
| ERR_POS_NO_OPEN_SESSION | Bạn chưa mở phiên thu ngân / Không tìm thấy phiên thu ngân đang mở |
| ERR_POS_MACHINE_IN_USE | Máy POS này đang được nhân viên khác sử dụng |
| ERR_POS_ALREADY_OPENED_SESSION | Bạn đã có phiên thu ngân đang mở |
| MSG_POS_CART_NOT_EMPTY | Giỏ hàng đang có sản phẩm, bạn có chắc chắn muốn đóng phiên và hủy giỏ hàng này? |
| ERR_POS_INVALID_SESSION_ID | Mã phiên không hợp lệ |
| ERR_POS_INVALID_CLOSING_CASH | Tiền cuối ca phải là số không âm |
| ERR_POS_PAYMENT_INSUFFICIENT | Số tiền thanh toán chưa đủ |
| ERR_PAY_NEGATIVE_AMOUNT | Số tiền thanh toán không được âm |
| ERR_POS_RFID_EMPTY | Vui lòng quét thẻ RFID |
| ERR_POS_RFID_NOT_FOUND_OR_INACTIVE | Thẻ RFID không tồn tại hoặc đã bị vô hiệu hóa |
| ERR_POS_RFID_PAYMENT_NO_ID | Chưa xác định được ví RFID |
| ERR_POS_RFID_INSUFFICIENT_BALANCE | Số dư ví RFID không đủ |
| ERR_POS_INSUFFICIENT_POINTS | Điểm tích lũy không đủ |
| ERR_POS_PRODUCT_NOT_FOUND | Không tìm thấy sản phẩm: {0} |
| ERR_POS_CUSTOMER_NOT_FOUND | Không tìm thấy khách hàng |
| ERR_POS_CHECKOUT_FAILED_UNKNOWN | Thanh toán thất bại, lỗi không xác định |
| MSG_POS_MULTI_RESULT | Tìm thấy {0} sản phẩm, vui lòng chọn trên danh mục |
| ERR_KM_EMPTY | Chưa nhập mã khuyến mãi |
| ERR_KM_NOT_FOUND | Mã khuyến mãi không tồn tại |
| ERR_KM_HET_LUOT | Mã khuyến mãi đã hết lượt sử dụng |
| ERR_KM_DON_TOI_THIEU | Đơn hàng chưa đạt giá trị tối thiểu để áp dụng khuyến mãi |
| ERR_KM_DIEU_KIEN | Khách hàng không thuộc đối tượng áp dụng khuyến mãi |
| ERR_REFUND_EMPTY_CODE | Vui lòng nhập mã đơn hàng |
| ERR_REFUND_NOT_FOUND | Không tìm thấy đơn hàng |
| ERR_REFUND_NEGATIVE_QTY | Số lượng hoàn không được âm |
| ERR_REFUND_OVER_QTY | Số lượng hoàn không được vượt quá {0} |
| ERR_REFUND_INVALID_NUMBER | Vui lòng nhập số hợp lệ |
| ERR_HOAN_INVALID_REQUEST | Yêu cầu hoàn hàng không hợp lệ |
| ERR_HOAN_NO_ITEMS | Chưa chọn sản phẩm nào để hoàn |
| ERR_HOAN_REASON_REQUIRED | Bắt buộc nhập lý do hoàn |
| ERR_HOAN_VE_KHONG_DUOC_HOAN | Vé không được phép hoàn trả |
| ERR_HOAN_QUA_HAN | Đã quá thời hạn hoàn hàng cho phép |
| PARTIAL_TICKET_SCANNED | Vé đã được quét sử dụng, không thể hoàn |
| MSG_POS_CHECKOUT_SUCCESS | Thanh toán thành công |
| MSG_POS_SESSION_OPENED | Mở phiên thu ngân thành công |
| MSG_POS_SESSION_CLOSED | Đóng phiên thu ngân thành công |
| MSG_REFUND_SUCCESS | Hoàn hàng thành công |
| MSG_REFUND_FAIL | Hoàn hàng thất bại |
