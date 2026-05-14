# Khu Du Lịch Đại Nam
# Đặc Tả Yêu Cầu Phần Mềm
# Mã dự án: DN01
# Mã tài liệu: DN01_SRS_LuuTru_v1.0

Hồ Chí Minh, Tháng 04/2025

---

## Lịch sử thay đổi

| Ngày hiệu lực | Hạng mục thay đổi | A/M/D | Mô tả | Phiên bản |
|---|---|---|---|---|
| 25/04/2025 | Toàn bộ tài liệu | A | Tạo mới tài liệu SRS chức năng Lưu Trú | 1.0 |

---

## Mục lục

- [1. Quản Lý Lưu Trú](#1-quản-lý-lưu-trú)
  - [1.1. Sơ đồ phòng](#11-sơ-đồ-phòng)
  - [1.2. Nhận phòng / Đặt phòng](#12-nhận-phòng--đặt-phòng)
  - [1.3. Trả phòng (Check-out)](#13-trả-phòng-check-out)
  - [1.4. Đổi phòng](#14-đổi-phòng)
  - [1.5. Gia hạn phòng](#15-gia-hạn-phòng)
  - [1.6. Minibar](#16-minibar)
  - [1.7. Phụ thu dịch vụ](#17-phụ-thu-dịch-vụ)
  - [1.8. Cập nhật trạng thái phòng](#18-cập-nhật-trạng-thái-phòng)
- [2. Yêu cầu khác](#2-yêu-cầu-khác)
  - [2.1. Định dạng dữ liệu](#21-định-dạng-dữ-liệu)
  - [2.2. Danh mục dữ liệu tham chiếu](#22-danh-mục-dữ-liệu-tham-chiếu)
  - [2.3. Bảng mã thông báo lỗi](#23-bảng-mã-thông-báo-lỗi)

---

# 1. Quản Lý Lưu Trú

## 1.1. Sơ đồ phòng

### 1.1.1. Tổng quan

Màn hình Sơ đồ phòng là giao diện trung tâm của module Lưu Trú, hiển thị toàn bộ phòng lưu trú dưới dạng lưới ô tile (Tile View). Mỗi ô tile đại diện cho một phòng và thể hiện trạng thái hiện tại bằng màu sắc để nhân viên lễ tân nhận biết nhanh mà không cần đọc từng dòng. Khi nhân viên chọn một phòng, bảng thông tin chi tiết bên phải (Sidebar) hiển thị đầy đủ thông tin khách hàng, thời gian lưu trú và số liệu tài chính tạm tính. Từ Sidebar, nhân viên thực hiện tất cả nghiệp vụ thao tác phòng: trả phòng, phụ thu, đổi phòng, gia hạn, mở minibar. Hành động click đôi trực tiếp lên ô tile mở form phù hợp theo trạng thái phòng đó, còn click phải mở menu ngữ cảnh với các lệnh tương ứng.

### 1.1.2. Tác nhân

- **Nhân viên lễ tân**: sử dụng màn hình này để theo dõi và thao tác toàn bộ phòng trong ca làm việc.
- **Quản lý**: có thể xem nhưng không thực hiện các thao tác nghiệp vụ nếu chưa mở phiên thu ngân.

### 1.1.3. Biểu đồ use-case

_(Sơ đồ tổng quan tham chiếu từ tài liệu thiết kế kiến trúc module Lưu Trú)_

#### 1.1.3.1. Tiền điều kiện

- Nhân viên đã đăng nhập vào hệ thống.
- Nhân viên đã mở phiên thu ngân cho ca làm việc hiện tại. Nếu chưa mở phiên, hệ thống nhắc nhở và cho phép mở ngay trước khi thao tác.

#### 1.1.3.2. Hậu điều kiện

- Sơ đồ phòng hiển thị đúng trạng thái hiện tại của tất cả phòng.
- Mọi thay đổi từ các nghiệp vụ con (nhận phòng, trả phòng…) đều được phản ánh ngay sau khi màn hình làm mới.

#### 1.1.3.3. Điểm kích hoạt

- Nhân viên mở module Lưu Trú từ menu chính của ứng dụng.

### 1.1.4. Luồng thao tác

#### 1.1.4.1. Tình huống 1 — Mở màn hình và kiểm tra phiên thu ngân

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Nhấn vào module Lưu Trú trên menu chính. | Tải màn hình Sơ đồ phòng. Đồng thời kiểm tra xem nhân viên có phiên thu ngân đang mở không. |
| 2 | — | Nếu nhân viên đã có phiên đang mở, hệ thống lưu lại mã phiên đó vào bộ nhớ làm việc và hiển thị sơ đồ phòng bình thường. |
| 3 | — | Nếu chưa có phiên, hệ thống hiện hộp thoại xác nhận: "Bạn chưa mở phiên thu ngân. Vui lòng mở phiên trước khi thao tác." |
| 4 | Nhấn Đồng ý. | Hệ thống mở form Mở Phiên Thu Ngân. Sau khi nhân viên hoàn tất, hệ thống lưu mã phiên mới và tiếp tục hiển thị sơ đồ. |
| 5 | — | Sau khi phiên được xác nhận, hệ thống tải danh sách phòng và hiển thị toàn bộ ô tile theo đúng trạng thái. |

#### 1.1.4.2. Tình huống 2 — Chọn phòng và xem thông tin chi tiết

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Nhấn một lần vào ô tile phòng bất kỳ trên sơ đồ. | Hệ thống xác định phòng được chọn, tải thông tin chi tiết phòng đó từ cơ sở dữ liệu. |
| 2 | — | Nếu phòng đang ở trạng thái Đang ở, hệ thống tính toán tiền phòng và tiền phạt lố giờ (nếu có) tại thời điểm hiện tại, rồi hiển thị vào Sidebar. |
| 3 | — | Sidebar hiển thị: mã phòng, tên loại phòng, nhãn trạng thái, họ tên khách, SĐT, giờ check-in, giờ check-out dự kiến, tiền phòng tạm tính, phụ thu, tiền đã cọc, tổng tiền tạm tính. |
| 4 | — | Hệ thống bật hoặc tắt các nút thao tác trên Sidebar tùy theo trạng thái phòng. Cụ thể: chỉ khi phòng ở trạng thái Đang ở thì các nút Trả phòng, Phụ thu, Đổi phòng, Gia hạn, Minibar mới được bật. |

#### 1.1.4.3. Tình huống 3 — Click đôi vào ô tile để mở form nghiệp vụ

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Nhấn đôi vào ô tile một phòng có trạng thái Trống và chưa có đặt trước. | Hệ thống mở form Nhận phòng / Đặt phòng (xem mục 1.2), với phòng đó được chọn sẵn và cố định. |
| 2 | Nhấn đôi vào ô tile một phòng có trạng thái Trống nhưng đã có đặt trước (trạng thái booking là Chờ đến). | Hệ thống hiện hộp thoại xác nhận: "Phòng này đã được đặt trước. Bạn muốn Check-in khách vào ở ngay bây giờ?" Nếu nhân viên xác nhận, hệ thống yêu cầu nhập mã thẻ RFID (có thể bỏ trống), sau đó thực hiện check-in và làm mới sơ đồ. |
| 3 | Nhấn đôi vào ô tile một phòng có trạng thái Đang ở. | Hệ thống mở form Trả phòng (xem mục 1.3) với thông tin phòng đó được nạp sẵn. |
| 4 | Nhấn đôi vào ô tile một phòng có trạng thái Chờ dọn. | Hệ thống hiện hộp thoại xác nhận: "Xác nhận đã dọn phòng xong?" Nếu đồng ý, hệ thống chuyển phòng về trạng thái Trống và làm mới sơ đồ. |

#### 1.1.4.4. Tình huống 4 — Click phải để mở menu ngữ cảnh

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Nhấn chuột phải vào ô tile bất kỳ. | Hệ thống xác định trạng thái phòng và trạng thái booking của phòng đó, rồi chỉ hiển thị các lệnh phù hợp trong menu ngữ cảnh. |
| 2 | — | Nếu phòng Trống và có booking Chờ đến: hiển thị lệnh Check-in và Hủy đặt phòng. |
| 3 | — | Nếu phòng Đang ở: hiển thị lệnh Minibar, Đổi phòng, Gia hạn. |
| 4 | — | Nếu phòng Chờ dọn: hiển thị lệnh Dọn xong. |
| 5 | — | Nếu phòng Trống và không có booking: hiển thị lệnh Đưa vào bảo trì. |
| 6 | — | Nếu phòng Bảo trì: hiển thị lệnh Sửa xong. |
| 7 | — | Nếu không có lệnh nào phù hợp, menu ngữ cảnh không hiển thị. |
| 8 | Chọn một lệnh trong menu. | Hệ thống thực thi nghiệp vụ tương ứng (xem các mục 1.2 đến 1.8) rồi làm mới sơ đồ. |

### 1.1.5. Giao diện

#### Khu vực Sơ đồ phòng (Tile View)

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Mã phòng | Label (in-tile) | N/A | Nvarchar(50) | N/A | Hiển thị mã định danh phòng trên ô tile. |
| 2 | Loại phòng | Label (in-tile) | N/A | Nvarchar(100) | N/A | Hiển thị tên loại phòng bên dưới mã phòng trên ô tile. |
| 3 | Tên khách hàng | Label (in-tile) | N/A | Nvarchar(200) | Blank | Hiển thị tên khách đang lưu trú. Để trống nếu phòng chưa có khách. |
| 4 | Màu nền ô tile | Tile View | N/A | N/A | N/A | Màu nền thay đổi theo trạng thái phòng: xanh lá cho Trống, đỏ cho Đang ở, cam cho Chờ dọn, xám cho Bảo trì. |

#### Khu vực Sidebar — Thông tin phòng

**Nhóm Khách hàng**

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tiêu đề phòng | Label (Read-only) | N/A | Text | N/A | Hiển thị "Mã phòng - Tên loại phòng" ở đầu Sidebar. |
| 2 | Trạng thái | Label (Read-only) | N/A | Text | N/A | Nhãn trạng thái phòng. Màu chữ: xanh lá cho Trống, đỏ cho Đang ở, cam cho Chờ dọn, xám cho các trạng thái khác. |
| 3 | Họ tên | Label (Read-only) | N/A | Nvarchar(200) | -- | Hiển thị họ tên khách đang lưu trú. Hiển thị -- nếu chưa có dữ liệu. |
| 4 | Số điện thoại | Label (Read-only) | N/A | Nvarchar(20) | -- | Hiển thị SĐT khách. Hiển thị -- nếu chưa có dữ liệu. |

**Nhóm Thời gian**

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 5 | Giờ Check-in | Label (Read-only) | N/A | DateTime | -- | Hiển thị thời điểm check-in thực tế theo định dạng dd/MM/yyyy HH:mm. |
| 6 | Giờ Check-out | Label (Read-only) | N/A | DateTime | -- | Hiển thị thời điểm check-out dự kiến theo định dạng dd/MM/yyyy HH:mm. |

**Nhóm Tài chính** _(chỉ hiển thị giá trị khi phòng ở trạng thái Đang ở)_

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 7 | Tiền phòng | Label (Read-only) | N/A | Decimal(15,0) | -- | Tiền phòng tạm tính tại thời điểm xem, theo số đêm thực tế. Hiển thị theo định dạng N,NNNđ. |
| 8 | Phụ thu | Label (Read-only) | N/A | Decimal(15,0) | -- | Tiền phạt lố giờ tạm tính (nếu có). Hiển thị theo định dạng N,NNNđ. |
| 9 | Đã cọc | Label (Read-only) | N/A | Decimal(15,0) | -- | Số tiền đặt cọc khách đã nộp khi nhận phòng. Hiển thị theo định dạng N,NNNđ. |
| 10 | Tổng tiền | Label (Read-only) | N/A | Decimal(15,0) | -- | Tổng tạm tính = Tiền phòng + Phụ thu - Đã cọc. Màu chữ đỏ khi phòng đang ở. Hiển thị theo định dạng N,NNNđ. |

**Nhóm Thao tác**

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 11 | Trả phòng | Button | N/A | N/A | N/A | Mở form Trả phòng. Chỉ bật khi phòng ở trạng thái Đang ở. |
| 12 | Phụ thu | Button | N/A | N/A | N/A | Mở form Phụ thu dịch vụ. Chỉ bật khi phòng ở trạng thái Đang ở. |
| 13 | Đổi phòng | Button | N/A | N/A | N/A | Mở form Đổi phòng. Chỉ bật khi phòng ở trạng thái Đang ở. |
| 14 | Gia hạn | Button | N/A | N/A | N/A | Mở form Gia hạn phòng. Chỉ bật khi phòng ở trạng thái Đang ở. |
| 15 | Minibar | Button | N/A | N/A | N/A | Mở form Minibar để thêm đồ ăn thức uống vào bill phòng. Chỉ bật khi phòng ở trạng thái Đang ở. |
| 16 | Làm mới | Button | N/A | N/A | N/A | Tải lại toàn bộ danh sách phòng và cập nhật sơ đồ. |

### 1.1.6. Mô tả nghiệp vụ

Khi nhân viên truy cập module Lưu Trú, hệ thống kiểm tra phiên thu ngân đang hoạt động của nhân viên đó. Nếu chưa có phiên, hệ thống yêu cầu mở phiên trước khi cho phép thao tác, vì mọi giao dịch tài chính phát sinh trong module này đều phải gắn với một phiên thu ngân cụ thể để đảm bảo tính toàn vẹn sổ sách.

Sơ đồ phòng hiển thị tất cả phòng thuộc khu vực phụ trách của nhân viên. Màu sắc ô tile phản ánh trạng thái tức thời: phòng Trống có thể nhận khách mới, phòng Đang ở đang có khách lưu trú, phòng Chờ dọn vừa có khách trả phòng và chưa được dọn vệ sinh, phòng Bảo trì tạm thời không thể nhận khách.

Khi nhân viên chọn một phòng đang ở, hệ thống tự động tính tiền phòng và phạt lố giờ (nếu thời điểm hiện tại đã quá giờ trả phòng dự kiến) để hiển thị con số tạm tính. Con số này giúp nhân viên tư vấn nhanh cho khách mà không cần mở form Trả phòng.

Hành động click đôi được thiết kế để rút ngắn thao tác: thay vì phải chọn phòng rồi nhấn nút trên Sidebar, nhân viên có thể click đôi trực tiếp vào ô tile để mở ngay form phù hợp. Hành động click phải cung cấp menu ngữ cảnh chứa các lệnh đặc thù hơn, ví dụ hủy đặt phòng hoặc chuyển phòng sang bảo trì.

### 1.1.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Nếu nhân viên chưa có phiên thu ngân đang mở, hệ thống không cho phép thực hiện bất kỳ thao tác nghiệp vụ nào cho đến khi mở phiên. | ERR_LT_CHUA_MO_PHIEN |
| 2 | Nếu phòng không ở trạng thái Đang ở, tất cả nút thao tác trên Sidebar đều ở trạng thái vô hiệu hóa. | N/A |
| 3 | Nếu không có phòng nào trong danh sách, hệ thống hiển thị thông báo lỗi từ phía máy chủ. | ERR_LT_LOAD_PHONG_THAT_BAI |

### 1.1.8. Liên kết use-case

- Nhận phòng / Đặt phòng (mục 1.2)
- Trả phòng (mục 1.3)
- Đổi phòng (mục 1.4)
- Gia hạn phòng (mục 1.5)
- Minibar (mục 1.6)
- Phụ thu dịch vụ (mục 1.7)
- Cập nhật trạng thái phòng (mục 1.8)

---

## 1.2. Nhận phòng / Đặt phòng

### 1.2.1. Tổng quan

Form Nhận phòng / Đặt phòng cho phép nhân viên lễ tân ghi nhận thông tin khách hàng, chọn phòng, xác định thời gian lưu trú và thực hiện check-in ngay lập tức. Hệ thống tự động tra cứu thông tin khách theo số điện thoại nếu khách đã từng lưu trú trước đó, đồng thời tính giá phòng dự kiến theo số đêm để nhân viên tư vấn cho khách trước khi xác nhận. Mã thẻ RFID có thể được liên kết với lượt lưu trú này để kiểm soát truy cập phòng.

### 1.2.2. Tác nhân

- **Nhân viên lễ tân**: nhập thông tin và thực hiện nhận phòng.

### 1.2.3. Biểu đồ use-case

_(Tham chiếu sơ đồ use-case module Lưu Trú)_

#### 1.2.3.1. Tiền điều kiện

- Nhân viên đã mở phiên thu ngân.
- Có ít nhất một phòng ở trạng thái Trống.
- Form được mở từ màn hình Sơ đồ phòng (click đôi vào phòng Trống hoặc qua menu ngữ cảnh).

#### 1.2.3.2. Hậu điều kiện

- Phòng được chuyển sang trạng thái Đang ở.
- Hồ sơ đặt phòng và thông tin khách hàng được lưu vào cơ sở dữ liệu.
- Sơ đồ phòng được làm mới tự động.

#### 1.2.3.3. Điểm kích hoạt

- Nhân viên click đôi vào ô tile một phòng Trống (không có booking) trên Sơ đồ phòng.

### 1.2.4. Luồng thao tác

#### 1.2.4.1. Tình huống 1 — Nhận phòng cho khách mới

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Click đôi vào ô tile phòng Trống. | Hệ thống mở form Nhận phòng / Đặt phòng. Trường Phòng lựa chọn được điền sẵn tên phòng đó và bị khóa, không cho thay đổi. Giờ Check-in mặc định là thời điểm hiện tại; Giờ Check-out mặc định là 12:00 ngày hôm sau. |
| 2 | Nhập số điện thoại của khách rồi chuyển sang trường tiếp theo. | Hệ thống tra cứu khách hàng theo số điện thoại. Nếu tìm thấy và trường Tên khách hàng đang trống, hệ thống tự động điền tên khách vào. |
| 3 | Nhập tên khách hàng (nếu chưa tự điền), điều chỉnh giờ Check-in và Check-out theo thực tế. | Hệ thống tính lại Giá phòng dự kiến mỗi khi phòng, giờ Check-in hoặc giờ Check-out thay đổi. Kết quả hiển thị theo dạng: "tổng tiền (số đêm x đơn giá)". |
| 4 | Nhập số người, tiền đặt cọc (nếu có) và mã thẻ RFID (nếu có). | — |
| 5 | Nhấn nút Nhận phòng. | Hệ thống kiểm tra toàn bộ dữ liệu đầu vào theo quy tắc tại mục 1.2.7. Nếu hợp lệ, hệ thống thực hiện tạo hồ sơ đặt phòng và check-in, chuyển phòng sang trạng thái Đang ở, hiển thị thông báo thành công rồi đóng form. |

#### 1.2.4.2. Tình huống 2 — Dữ liệu nhập không hợp lệ

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Nhận phòng khi chưa nhập đủ hoặc nhập sai thông tin. | Hệ thống dừng xử lý, hiển thị thông báo lỗi tương ứng (xem mục 1.2.7), đặt con trỏ vào trường lỗi đầu tiên. |
| 2 | Sửa lại thông tin theo hướng dẫn lỗi, nhấn Nhận phòng lại. | Hệ thống kiểm tra lại và xử lý nếu hợp lệ. |

### 1.2.5. Giao diện

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tên khách hàng | Text Edit | Yes | Nvarchar(200) | Blank | Tên đầy đủ của khách lưu trú. Hệ thống tự điền nếu tìm thấy khách theo SĐT. |
| 2 | Số điện thoại | Text Edit | Yes | Nvarchar(20) | Blank | SĐT khách, tối thiểu 10 ký tự số. Khi rời khỏi trường này, hệ thống tra cứu khách tự động. |
| 3 | Phòng lựa chọn | Search Lookup Edit | Yes | Integer | Blank | Danh sách chỉ hiển thị các phòng đang ở trạng thái Trống. Cột hiển thị trong popup: Mã phòng, Loại phòng. Nếu mở form từ click đôi ô tile, trường này được điền sẵn và bị khóa. |
| 4 | Giờ Check-in | Date Edit | Yes | DateTime | Thời điểm hiện tại | Ngày giờ check-in thực tế. Thay đổi giá trị này sẽ kích hoạt tính lại giá dự kiến. |
| 5 | Giờ Check-out | Date Edit | Yes | DateTime | 12:00 ngày hôm sau | Ngày giờ check-out dự kiến. Thay đổi giá trị này sẽ kích hoạt tính lại giá dự kiến. |
| 6 | Giá phòng dự kiến | Text Edit | N/A | Text | 0 VND | Read-only. Hệ thống tự tính theo công thức: đơn giá phòng nhân số đêm. Hiển thị dạng "tổng (số đêm x đơn giá)". Chữ màu đỏ, đậm. Không thể chỉnh sửa. |
| 7 | Số người ở | Spin Edit | Yes | Integer | 1 | Số khách lưu trú trong phòng. Phải lớn hơn 0 và không vượt quá sức chứa tối đa của phòng. |
| 8 | Mã thẻ RFID | Text Edit | No | Nvarchar(100) | Blank | Mã thẻ RFID để liên kết với lượt lưu trú, dùng cho kiểm soát truy cập. Để trống nếu không dùng. |
| 9 | Tiền đặt cọc | Spin Edit | No | Decimal(15,0) | 0 | Số tiền khách nộp trước khi nhận phòng. Được trừ vào tổng thanh toán khi trả phòng. |
| 10 | Nhận phòng | Button | N/A | N/A | N/A | Xác nhận toàn bộ thông tin và thực hiện check-in. Hotkey: F2. |
| 11 | Hủy bỏ | Button | N/A | N/A | N/A | Đóng form mà không lưu. Hotkey: Escape. |

### 1.2.6. Mô tả nghiệp vụ

Nghiệp vụ nhận phòng kết hợp hai bước truyền thống — tạo phiếu đặt phòng và check-in — thành một thao tác duy nhất, phù hợp với tình huống khách walk-in (đến trực tiếp mà không đặt trước). Hệ thống tạo phiếu đặt phòng nội bộ rồi lập tức chuyển trạng thái phòng sang Đang ở trong cùng một giao dịch, đảm bảo không có khoảng thời gian hệ thống hiển thị phòng sai trạng thái.

Giá phòng dự kiến được tính theo cơ chế giá động: hệ thống tra cứu bảng giá hiệu lực tại thời điểm check-in, nhân với số đêm (tính theo ngày tròn của khoảng cách giữa check-in và check-out, tối thiểu 1 đêm). Đây chỉ là con số tham khảo — giá chính thức sẽ được tính lại lúc trả phòng dựa trên thực tế lưu trú.

Khi nhân viên nhập SĐT và chuyển sang trường khác, hệ thống tự tra cứu khách hàng theo số điện thoại. Nếu tìm thấy và trường tên chưa được nhập, hệ thống điền tự động để tiết kiệm thời gian. Nhân viên vẫn có thể sửa tên nếu cần.

Tiền đặt cọc không phải bắt buộc nhưng nếu có, số tiền này được ghi nhận vào hồ sơ đặt phòng và sẽ được tự động trừ vào tổng thanh toán khi khách trả phòng.

### 1.2.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Tên khách hàng không được để trống. | ERR_LT_THIEU_TEN_KHACH |
| 2 | Số điện thoại không được để trống và phải có ít nhất 10 ký tự. | ERR_LT_SDT_KHONG_HOP_LE |
| 3 | Phải chọn phòng lưu trú. | ERR_LT_THIEU_PHONG |
| 4 | Số người phải lớn hơn 0. | ERR_LT_SO_NGUOI_KHONG_HOP_LE |
| 5 | Số người không được vượt quá sức chứa tối đa của phòng đã chọn. | ERR_LT_VUOT_SO_NGUOI_TOI_DA |

### 1.2.8. Liên kết use-case

- Sơ đồ phòng (mục 1.1) — form được mở từ đây.
- Cập nhật trạng thái phòng (mục 1.8) — trạng thái phòng được chuyển sang Đang ở sau khi nhận phòng thành công.

---

## 1.3. Trả phòng (Check-out)

### 1.3.1. Tổng quan

Form Trả phòng tổng hợp toàn bộ chi phí phát sinh trong lượt lưu trú của khách: tiền phòng, tiền phạt lố giờ, phụ thu khác, trừ tiền cọc và giảm giá (khuyến mãi hoặc điểm tích lũy). Nhân viên xác nhận phương thức thanh toán và hoàn tất trả phòng. Hệ thống tự động in hóa đơn sau khi giao dịch thành công và chuyển phòng sang trạng thái Chờ dọn.

### 1.3.2. Tác nhân

- **Nhân viên lễ tân / Thu ngân**: thực hiện thao tác trả phòng và thu tiền.

### 1.3.3. Biểu đồ use-case

_(Tham chiếu sơ đồ use-case module Lưu Trú)_

#### 1.3.3.1. Tiền điều kiện

- Phòng đang ở trạng thái Đang ở.
- Nhân viên đã mở phiên thu ngân.

#### 1.3.3.2. Hậu điều kiện

- Phòng chuyển sang trạng thái Chờ dọn.
- Giao dịch thanh toán được ghi nhận vào phiên thu ngân.
- Điểm tích lũy của khách được cập nhật (nếu áp dụng quy đổi điểm).
- Hóa đơn được in hoặc hiển thị để in.

#### 1.3.3.3. Điểm kích hoạt

- Nhân viên nhấn nút Trả phòng trên Sidebar hoặc click đôi vào ô tile phòng Đang ở.

### 1.3.4. Luồng thao tác

#### 1.3.4.1. Tình huống 1 — Trả phòng thông thường (đúng giờ, không khuyến mãi)

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Mở form Trả phòng từ Sidebar hoặc click đôi ô tile phòng Đang ở. | Hệ thống tải thông tin phòng, tính tiền phòng theo số đêm thực tế và kiểm tra lố giờ. Nếu thời điểm hiện tại chưa qua giờ check-out dự kiến, tiền phạt bằng 0. Hệ thống cũng tải thông tin hạng thành viên và điểm tích lũy của khách. |
| 2 | — | Hệ thống tự kiểm tra và áp dụng khuyến mãi tốt nhất hiện hành phù hợp với tổng tiền và hạng thành viên của khách. Nếu có khuyến mãi gợi ý (gần đủ điều kiện hưởng ưu đãi cao hơn), hệ thống hiển thị gợi ý dưới dạng nhãn màu cam. |
| 3 | Kiểm tra các số liệu tài chính trên form, chọn phương thức thanh toán. | — |
| 4 | Nhấn nút Trả phòng. | Hệ thống kiểm tra phiên thu ngân. Nếu phiên còn hiệu lực, hệ thống ghi nhận giao dịch, chuyển phòng sang Chờ dọn, hiển thị thông báo thành công, mở form xem trước hóa đơn để in. |

#### 1.3.4.2. Tình huống 2 — Khách trả phòng trễ (lố giờ)

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Mở form Trả phòng khi thời điểm hiện tại đã qua giờ check-out dự kiến. | Hệ thống tính tiền phạt lố giờ theo quy tắc nghiệp vụ (ví dụ: phụ phí theo số giờ hoặc tính thêm đêm). Điền sẵn ghi chú phụ thu với nội dung mô tả lý do phạt. |
| 2 | Xem xét tiền phạt, điều chỉnh nếu cần (ví dụ: nhập thêm phụ thu khác). | Hệ thống tự động cập nhật tổng thanh toán mỗi khi giá trị phụ thu thay đổi. |
| 3 | Chọn phương thức thanh toán, nhấn Trả phòng. | Hệ thống xử lý như tình huống 1. |

#### 1.3.4.3. Tình huống 3 — Áp dụng mã khuyến mãi thủ công

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Nhập mã khuyến mãi vào trường Khuyến mãi, nhấn nút Áp dụng. | Hệ thống kiểm tra mã: còn hiệu lực, phù hợp với hạng thành viên và tổng hóa đơn. Nếu hợp lệ, tính số tiền giảm thực tế và cập nhật tổng thanh toán. |
| 2 | — | Nếu mã không hợp lệ hoặc đã hết lượt, hệ thống hiển thị lỗi, xóa mã vừa nhập và giữ nguyên tổng tiền cũ. |

#### 1.3.4.4. Tình huống 4 — Sử dụng điểm tích lũy

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Tích chọn ô Sử dụng điểm tích lũy. | Hệ thống kiểm tra điểm khả dụng của khách. Nếu điểm lớn hơn 0, hệ thống quy đổi toàn bộ điểm sang tiền giảm theo tỉ lệ quy định (1 điểm = 1,000đ) và cập nhật tổng thanh toán. |
| 2 | — | Nếu khách không có điểm, hệ thống hiển thị lỗi và bỏ tích ô tự động. |
| 3 | — | Hệ thống áp dụng quy tắc: chỉ lấy khoản giảm lớn hơn giữa khuyến mãi và điểm, không cộng dồn cả hai. |

### 1.3.5. Giao diện

**Khu vực thông tin (Read-only)**

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Phòng | Text Edit | N/A | Nvarchar(50) | Blank | Read-only. Mã phòng đang trả. |
| 2 | Khách hàng | Text Edit | N/A | Nvarchar(200) | Blank | Read-only. Tên khách đang lưu trú. |
| 3 | Giờ In/Out | Text Edit | N/A | Text | Blank | Read-only. Hiển thị khoảng thời gian lưu trú theo định dạng "dd/MM HH:mm - dd/MM HH:mm". |

**Khu vực tài chính**

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 4 | Tiền phòng | Spin Edit | N/A | Decimal(15,0) | 0 | Read-only. Tiền phòng do hệ thống tính tự động theo số đêm thực tế. |
| 5 | Tiền phạt | Spin Edit | N/A | Decimal(15,0) | 0 | Read-only. Tiền phạt lố giờ (nếu có). Hệ thống tự tính và điền. |
| 6 | Tiền cọc | Spin Edit | N/A | Decimal(15,0) | 0 | Read-only. Số tiền đặt cọc đã thu khi nhận phòng, tự động trừ vào tổng. |
| 7 | Phụ thu khác | Spin Edit | No | Decimal(15,0) | 0 | Các khoản phụ thu phát sinh ngoài tiền phòng và tiền phạt (ví dụ: phí giặt ủi, phá hỏng đồ). |
| 8 | Ghi chú phụ thu | Text Edit | No | Nvarchar(500) | Blank | Ghi chú lý do phụ thu. Hệ thống tự điền ghi chú phạt lố giờ nếu có tiền phạt. |
| 9 | Tổng thanh toán | Spin Edit | N/A | Decimal(15,0) | 0 | Read-only. Tổng = Tiền phòng + Tiền phạt + Phụ thu khác - Tiền cọc - Giảm giá. Tối thiểu bằng 0. |

**Khu vực thanh toán và ưu đãi**

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 10 | Phương thức thanh toán | Combo Box | Yes | Text | Tiền mặt | Danh sách: Tiền mặt, Chuyển khoản, Thẻ ngân hàng. |
| 11 | Mã khuyến mãi | Text Edit | No | Nvarchar(50) | Blank | Nhân viên nhập mã khuyến mãi để áp dụng giảm giá thủ công. |
| 12 | Áp dụng | Button | N/A | N/A | N/A | Kiểm tra và áp dụng mã khuyến mãi đã nhập. |
| 13 | Sử dụng điểm tích lũy | Check Box | No | Boolean | Unchecked | Tích để quy đổi toàn bộ điểm tích lũy của khách sang tiền giảm. |
| 14 | Điểm khả dụng | Label (Read-only) | N/A | Text | N/A | Hiển thị số điểm tích lũy khả dụng hiện tại của khách. |
| 15 | Gợi ý khuyến mãi | Label (Read-only) | N/A | Text | Blank | Hiển thị gợi ý nếu khách gần đủ điều kiện hưởng ưu đãi cao hơn. Màu cam. |
| 16 | Trả phòng | Button | N/A | N/A | N/A | Xác nhận và hoàn tất giao dịch trả phòng. |
| 17 | Hủy bỏ | Button | N/A | N/A | N/A | Đóng form mà không thực hiện trả phòng. |

### 1.3.6. Mô tả nghiệp vụ

Tiền phòng được tính dựa trên số đêm thực tế (chênh lệch ngày giữa ngày check-in và ngày trả phòng hiện tại). Nếu khách trả phòng muộn hơn giờ quy định, hệ thống tính thêm tiền phạt lố giờ. Tiền phạt và lý do phạt được tự động điền vào ghi chú phụ thu để nhân viên thông báo cho khách.

Hệ thống tự động tìm khuyến mãi tốt nhất phù hợp với tổng tiền và hạng thành viên của khách và áp dụng ngay khi mở form. Nhân viên vẫn có thể nhập mã khuyến mãi khác để thay thế. Khi khách muốn dùng điểm tích lũy, hệ thống so sánh số tiền giảm từ khuyến mãi và từ điểm, rồi chỉ áp dụng khoản giảm lớn hơn để bảo vệ lợi ích khách hàng, tránh tình huống hai ưu đãi chồng lên nhau làm hệ thống bị lỗ.

Sau khi xác nhận trả phòng thành công, hệ thống tự mở giao diện xem trước hóa đơn. Nếu xảy ra lỗi khi in, hệ thống chỉ thông báo lỗi in riêng và không hủy giao dịch đã hoàn tất.

### 1.3.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Nếu phiên thu ngân của nhân viên đã hết hiệu lực hoặc chưa mở, hệ thống yêu cầu mở phiên trước khi trả phòng. | ERR_LT_CHUA_MO_PHIEN |
| 2 | Nếu khách tích chọn dùng điểm nhưng số điểm khả dụng bằng 0, hệ thống từ chối và bỏ tích ô tự động. | ERR_LT_KHONG_CO_DIEM |
| 3 | Mã khuyến mãi phải còn hiệu lực và phù hợp với hạng thành viên cũng như tổng hóa đơn. Nếu không hợp lệ, hệ thống xóa mã và không áp dụng giảm giá. | ERR_LT_MA_KM_KHONG_HOP_LE |
| 4 | Tổng thanh toán tối thiểu bằng 0đ, không âm. | N/A |

### 1.3.8. Liên kết use-case

- Sơ đồ phòng (mục 1.1) — điểm khởi động.
- Cập nhật trạng thái phòng (mục 1.8) — phòng chuyển sang Chờ dọn sau khi trả phòng.

---

## 1.4. Đổi phòng

### 1.4.1. Tổng quan

Chức năng Đổi phòng cho phép chuyển toàn bộ hồ sơ lưu trú của khách từ phòng hiện tại sang một phòng trống khác mà không cần trả phòng và nhận phòng lại. Nhân viên chọn phòng mới và ghi lý do đổi phòng. Hệ thống cập nhật trạng thái cả hai phòng và giữ nguyên toàn bộ thông tin khách, thời gian lưu trú và các khoản phụ thu đã phát sinh.

### 1.4.2. Tác nhân

- **Nhân viên lễ tân**: thực hiện đổi phòng theo yêu cầu của khách.

### 1.4.3. Biểu đồ use-case

_(Tham chiếu sơ đồ use-case module Lưu Trú)_

#### 1.4.3.1. Tiền điều kiện

- Phòng hiện tại đang ở trạng thái Đang ở.
- Có ít nhất một phòng khác đang ở trạng thái Trống và chưa có booking nào đang chờ.

#### 1.4.3.2. Hậu điều kiện

- Phòng cũ chuyển sang trạng thái Chờ dọn.
- Phòng mới chuyển sang trạng thái Đang ở với hồ sơ khách từ phòng cũ.
- Sơ đồ phòng được làm mới.

#### 1.4.3.3. Điểm kích hoạt

- Nhân viên nhấn nút Đổi phòng trên Sidebar hoặc chọn lệnh Đổi phòng từ menu ngữ cảnh.

### 1.4.4. Luồng thao tác

#### 1.4.4.1. Tình huống 1 — Đổi phòng thành công

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Đổi phòng (hoặc chọn từ menu ngữ cảnh). | Hệ thống lấy danh sách phòng trống chưa có booking, nếu có ít nhất một phòng thì mở form Đổi phòng. |
| 2 | Chọn phòng mới từ danh sách, nhập lý do đổi phòng (nếu có). | — |
| 3 | Nhấn nút Xác nhận. | Hệ thống chuyển hồ sơ lưu trú sang phòng mới, cập nhật trạng thái phòng cũ thành Chờ dọn, phòng mới thành Đang ở, rồi làm mới sơ đồ phòng. |

#### 1.4.4.2. Tình huống 2 — Không có phòng trống

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Đổi phòng. | Hệ thống kiểm tra danh sách phòng trống. Nếu không có phòng nào phù hợp, hệ thống hiển thị thông báo lỗi và không mở form. |

### 1.4.5. Giao diện

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Chọn phòng mới | Combo Box | Yes | Integer | Blank | Danh sách chỉ chứa các phòng đang trống và chưa có booking đang chờ. |
| 2 | Lý do đổi phòng | Text Edit | No | Nvarchar(500) | Blank | Ghi chú lý do khách yêu cầu đổi phòng để lưu lịch sử. |
| 3 | Xác nhận | Button | N/A | N/A | N/A | Thực hiện đổi phòng. |
| 4 | Hủy bỏ | Button | N/A | N/A | N/A | Đóng form mà không đổi phòng. |

### 1.4.6. Mô tả nghiệp vụ

Khi đổi phòng, hệ thống chỉ chấp nhận phòng trống hoàn toàn (trạng thái Trống và không có booking đang chờ đến). Điều này tránh xung đột khi phòng mới tuy chưa có khách nhưng đã được đặt trước cho khách khác sắp đến. Lý do đổi phòng được lưu vào lịch sử giao dịch phòng để phục vụ tra cứu về sau.

### 1.4.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Nếu không có phòng trống nào phù hợp để đổi, hệ thống thông báo lỗi và không mở form. | ERR_LT_KHONG_CO_PHONG_TRONG |
| 2 | Phòng mới phải được chọn trước khi nhấn Xác nhận. | N/A |

### 1.4.8. Liên kết use-case

- Sơ đồ phòng (mục 1.1) — điểm khởi động.
- Cập nhật trạng thái phòng (mục 1.8) — phòng cũ chuyển sang Chờ dọn, phòng mới chuyển sang Đang ở.

---

## 1.5. Gia hạn phòng

### 1.5.1. Tổng quan

Chức năng Gia hạn phòng cho phép nhân viên dời ngày check-out dự kiến sang một ngày muộn hơn theo yêu cầu của khách. Thao tác này không phát sinh thanh toán ngay; tiền phòng cho số đêm gia hạn sẽ được tính gộp vào hóa đơn khi khách trả phòng.

### 1.5.2. Tác nhân

- **Nhân viên lễ tân**: thực hiện gia hạn theo yêu cầu của khách.

### 1.5.3. Biểu đồ use-case

_(Tham chiếu sơ đồ use-case module Lưu Trú)_

#### 1.5.3.1. Tiền điều kiện

- Phòng đang ở trạng thái Đang ở và có giờ check-out dự kiến.

#### 1.5.3.2. Hậu điều kiện

- Ngày check-out dự kiến của phòng được cập nhật sang ngày mới.
- Sidebar làm mới để phản ánh ngày check-out mới.

#### 1.5.3.3. Điểm kích hoạt

- Nhân viên nhấn nút Gia hạn trên Sidebar hoặc chọn từ menu ngữ cảnh.

### 1.5.4. Luồng thao tác

#### 1.5.4.1. Tình huống 1 — Gia hạn phòng thành công

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Gia hạn. | Hệ thống mở form Gia hạn phòng. Trường Ngày trả mới mặc định bằng ngày check-out hiện tại cộng thêm 1 ngày. Không cho phép chọn ngày trước hoặc bằng ngày check-out hiện tại. |
| 2 | Chọn ngày trả mới từ lịch, nhấn Xác nhận. | Hệ thống cập nhật ngày check-out, làm mới dữ liệu sơ đồ và Sidebar. |

### 1.5.5. Giao diện

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Ngày trả mới | Date Edit | Yes | DateTime | Ngày check-out hiện tại + 1 ngày | Ngày check-out mới. Giá trị nhỏ nhất được phép chọn là ngày check-out hiện tại cộng 1 ngày. |
| 2 | Xác nhận | Button | N/A | N/A | N/A | Lưu ngày trả mới và đóng form. |
| 3 | Hủy bỏ | Button | N/A | N/A | N/A | Đóng form mà không thay đổi ngày trả phòng. |

### 1.5.6. Mô tả nghiệp vụ

Gia hạn chỉ cho phép dời ngày check-out về phía tương lai. Hệ thống khóa lịch để nhân viên không thể chọn ngày trước hoặc bằng ngày check-out hiện tại, tránh tình huống vô tình rút ngắn thời gian lưu trú của khách.

### 1.5.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Ngày trả mới phải lớn hơn ngày check-out hiện tại ít nhất 1 ngày. Lịch tự động chặn không cho chọn ngày không hợp lệ. | N/A |

### 1.5.8. Liên kết use-case

- Sơ đồ phòng (mục 1.1) — điểm khởi động.
- Trả phòng (mục 1.3) — tiền gia hạn được tính gộp khi trả phòng.

---

## 1.6. Minibar

### 1.6.1. Tổng quan

Form Minibar cho phép nhân viên ghi nhận các sản phẩm đồ ăn, nước uống trong tủ minibar phòng mà khách đã sử dụng. Nhân viên nhập số lượng cho từng sản phẩm; hệ thống tính thành tiền tự động và tổng hợp tổng cộng. Sau khi xác nhận, các khoản tiêu thụ được thêm vào bill phòng của khách và sẽ được tính vào hóa đơn khi trả phòng.

### 1.6.2. Tác nhân

- **Nhân viên lễ tân / Buồng phòng**: kiểm tra minibar và ghi nhận tiêu thụ.

### 1.6.3. Biểu đồ use-case

_(Tham chiếu sơ đồ use-case module Lưu Trú)_

#### 1.6.3.1. Tiền điều kiện

- Phòng đang ở trạng thái Đang ở.
- Danh sách sản phẩm minibar đã được cấu hình trong hệ thống.

#### 1.6.3.2. Hậu điều kiện

- Các sản phẩm đã chọn được thêm vào bill phòng, gắn với lượt lưu trú hiện tại.

#### 1.6.3.3. Điểm kích hoạt

- Nhân viên nhấn nút Minibar trên Sidebar hoặc chọn lệnh Minibar từ menu ngữ cảnh.

### 1.6.4. Luồng thao tác

#### 1.6.4.1. Tình huống 1 — Ghi nhận tiêu thụ minibar thành công

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Mở form Minibar. | Hệ thống tải danh sách sản phẩm minibar từ hệ thống, hiển thị vào lưới với đơn giá và số lượng mặc định bằng 0. |
| 2 | Nhập số lượng cho từng sản phẩm khách đã dùng. | Mỗi khi số lượng thay đổi, hệ thống tính lại thành tiền của dòng đó (số lượng nhân đơn giá) và cập nhật tổng cộng bên dưới lưới. |
| 3 | Nhấn nút Xác nhận. | Hệ thống kiểm tra xem có dòng nào số lượng lớn hơn 0 không. Nếu có, hệ thống thêm các mặt hàng đó vào bill phòng và hiển thị thông báo thành công. Form đóng lại. |

#### 1.6.4.2. Tình huống 2 — Chưa chọn sản phẩm nào

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Nhấn Xác nhận khi tất cả số lượng đều bằng 0. | Hệ thống hiển thị thông báo nhắc nhở chưa chọn sản phẩm nào và không ghi nhận vào bill. |

### 1.6.5. Giao diện

**Lưới sản phẩm minibar (có thể nhập liệu trực tiếp)**

| STT | Tên cột | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Sản phẩm | Label (in-grid) | N/A | Nvarchar(200) | N/A | Tên sản phẩm minibar. Read-only. |
| 2 | Đơn giá | Label (in-grid) | N/A | Decimal(15,0) | N/A | Đơn giá bán của sản phẩm. Read-only. Hiển thị theo định dạng N,NNNđ. |
| 3 | Số lượng | Spin Edit (in-grid) | No | Integer | 0 | Số lượng khách đã dùng. Nhân viên nhập trực tiếp vào ô lưới. Thay đổi giá trị kích hoạt tính lại thành tiền. |
| 4 | Thành tiền | Label (in-grid) | N/A | Decimal(15,0) | 0 | Số lượng nhân đơn giá. Hệ thống tự tính, Read-only. |

**Khu vực tổng hợp và hành động**

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 5 | Tổng cộng | Label (Read-only) | N/A | Text | 0đ | Tổng tiền tất cả sản phẩm có số lượng lớn hơn 0. Hiển thị theo định dạng N,NNNđ. |
| 6 | Xác nhận | Button | N/A | N/A | N/A | Ghi nhận tiêu thụ vào bill phòng. |
| 7 | Hủy | Button | N/A | N/A | N/A | Đóng form mà không ghi nhận. |

### 1.6.6. Mô tả nghiệp vụ

Danh sách sản phẩm minibar được cấu hình tập trung trong phân hệ Danh mục. Đơn giá hiển thị trên form được lấy từ bảng giá bán lẻ hiệu lực tại thời điểm ghi nhận. Mỗi lần nhân viên ghi nhận minibar, hệ thống tạo các bản ghi chi tiết bill gắn với lượt lưu trú. Khi khách trả phòng, các khoản này được tổng hợp vào tiền phụ thu của hóa đơn.

### 1.6.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Phải có ít nhất một sản phẩm có số lượng lớn hơn 0 thì mới được xác nhận. | ERR_LT_MINIBAR_RONG |
| 2 | Nếu không tải được danh sách sản phẩm từ hệ thống, hiển thị cảnh báo và không mở lưới. | ERR_LT_MINIBAR_LOAD_THAT_BAI |

### 1.6.8. Liên kết use-case

- Sơ đồ phòng (mục 1.1) — điểm khởi động.
- Trả phòng (mục 1.3) — tiền minibar được tính vào phụ thu của hóa đơn.

---

## 1.7. Phụ thu dịch vụ

### 1.7.1. Tổng quan

Chức năng Phụ thu dịch vụ ghi nhận các khoản chi phí phát sinh ngoài tiền phòng và minibar, ví dụ như phí giặt ủi, đền bù làm vỡ đồ dùng, hoặc các dịch vụ đột xuất khác. Nhân viên nhập số tiền và ghi chú lý do. Khoản phụ thu được thêm vào bill phòng ngay lập tức.

### 1.7.2. Tác nhân

- **Nhân viên lễ tân**: ghi nhận phụ thu theo đề xuất của bộ phận liên quan.

### 1.7.3. Biểu đồ use-case

_(Tham chiếu sơ đồ use-case module Lưu Trú)_

#### 1.7.3.1. Tiền điều kiện

- Phòng đang ở trạng thái Đang ở.

#### 1.7.3.2. Hậu điều kiện

- Khoản phụ thu được ghi nhận vào bill phòng.

#### 1.7.3.3. Điểm kích hoạt

- Nhân viên nhấn nút Phụ thu trên Sidebar.

### 1.7.4. Luồng thao tác

#### 1.7.4.1. Tình huống 1 — Ghi nhận phụ thu thành công

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Phụ thu trên Sidebar. | Hệ thống mở form Phụ thu dịch vụ với hai trường trống. |
| 2 | Nhập số tiền phụ thu và ghi chú lý do, nhấn Lưu (hoặc nhấn phím F2). | Hệ thống kiểm tra số tiền và ghi chú. Nếu hợp lệ, ghi nhận vào bill phòng và hiển thị thông báo thành công. |

#### 1.7.4.2. Tình huống 2 — Dữ liệu không hợp lệ

| | Nhân viên lễ tân | Hệ thống |
|---|---|---|
| 1 | Nhấn Lưu khi số tiền bằng 0 hoặc ghi chú để trống. | Hệ thống hiển thị thông báo lỗi tương ứng, đặt con trỏ vào trường lỗi. Form không đóng. |

### 1.7.5. Giao diện

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Số tiền phụ thu | Text Edit | Yes | Decimal(15,0) | Blank | Số tiền phụ thu. Phải lớn hơn 0. |
| 2 | Ghi chú | Text Edit | Yes | Nvarchar(500) | Blank | Mô tả lý do phụ thu. Ví dụ: "Làm vỡ ly", "Giặt ủi chăn ga". Không được để trống. |
| 3 | Lưu | Button | N/A | N/A | N/A | Ghi nhận phụ thu vào bill. Hotkey: F2. |
| 4 | Hủy | Button | N/A | N/A | N/A | Đóng form mà không ghi nhận. Hotkey: Escape. |

### 1.7.6. Mô tả nghiệp vụ

Phụ thu dịch vụ là cơ chế linh hoạt để ghi nhận mọi khoản chi phí không được phân loại sẵn vào tiền phòng hay minibar. Ghi chú bắt buộc để đảm bảo tính minh bạch: khách hàng có thể tra cứu lý do từng khoản phụ thu trên hóa đơn, và bộ phận kế toán có căn cứ để đối soát.

### 1.7.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Số tiền phụ thu phải lớn hơn 0. | ERR_LT_TIEN_PHU_THU_KHONG_HOP_LE |
| 2 | Ghi chú không được để trống. | ERR_LT_GHI_CHU_THIEU |

### 1.7.8. Liên kết use-case

- Sơ đồ phòng (mục 1.1) — điểm khởi động.
- Trả phòng (mục 1.3) — phụ thu được tổng hợp vào hóa đơn khi trả phòng.

---

## 1.8. Cập nhật trạng thái phòng

### 1.8.1. Tổng quan

Ngoài các luồng nghiệp vụ có form riêng, hệ thống hỗ trợ các thao tác chuyển trạng thái phòng nhanh thông qua menu ngữ cảnh (click phải) trên sơ đồ phòng. Các thao tác này bao gồm: đưa phòng vào bảo trì, xác nhận sửa xong, xác nhận dọn xong, hủy đặt phòng và check-in cho khách đặt trước.

### 1.8.2. Tác nhân

- **Nhân viên lễ tân / Quản lý**: thực hiện chuyển trạng thái phòng.

### 1.8.3. Biểu đồ use-case

_(Tham chiếu sơ đồ trạng thái phòng trong tài liệu thiết kế kiến trúc)_

#### 1.8.3.1. Tiền điều kiện

- Nhân viên đã chọn (click phải) vào ô tile phòng cần thao tác.
- Phòng đang ở trạng thái phù hợp với lệnh muốn thực hiện (xem bảng mô tả nghiệp vụ bên dưới).

#### 1.8.3.2. Hậu điều kiện

- Trạng thái phòng được cập nhật theo đúng lệnh đã thực hiện.
- Sơ đồ phòng được làm mới.

#### 1.8.3.3. Điểm kích hoạt

- Nhân viên nhấn chuột phải vào ô tile phòng và chọn lệnh từ menu ngữ cảnh.

### 1.8.4. Luồng thao tác

#### 1.8.4.1. Tình huống 1 — Đưa phòng vào bảo trì

| | Nhân viên | Hệ thống |
|---|---|---|
| 1 | Click phải vào phòng Trống không có booking, chọn lệnh Đưa vào bảo trì. | Hệ thống chuyển trạng thái phòng sang Bảo trì và làm mới sơ đồ. |

#### 1.8.4.2. Tình huống 2 — Xác nhận sửa xong

| | Nhân viên | Hệ thống |
|---|---|---|
| 1 | Click phải vào phòng Bảo trì, chọn lệnh Sửa xong. | Hệ thống chuyển trạng thái phòng từ Bảo trì về Trống và làm mới sơ đồ. |

#### 1.8.4.3. Tình huống 3 — Xác nhận dọn phòng xong

| | Nhân viên | Hệ thống |
|---|---|---|
| 1 | Click phải vào phòng Chờ dọn, chọn lệnh Dọn xong. | Hệ thống chuyển trạng thái phòng từ Chờ dọn về Trống và làm mới sơ đồ. |

#### 1.8.4.4. Tình huống 4 — Hủy đặt phòng đặt trước

| | Nhân viên | Hệ thống |
|---|---|---|
| 1 | Click phải vào phòng Trống có trạng thái booking Chờ đến, chọn lệnh Hủy đặt phòng. | Hệ thống hiển thị hộp thoại xác nhận: "Bạn có chắc chắn muốn hủy đặt phòng này không?" |
| 2 | Nhấn Có để xác nhận. | Hệ thống hủy phiếu đặt phòng, xóa trạng thái booking khỏi phòng đó, làm mới sơ đồ. |

#### 1.8.4.5. Tình huống 5 — Check-in cho khách đặt trước

| | Nhân viên | Hệ thống |
|---|---|---|
| 1 | Click phải vào phòng Trống có booking Chờ đến, chọn lệnh Check-in. | Hệ thống hiển thị hộp thoại xác nhận: "Phòng này đã được đặt trước. Bạn muốn Check-in khách vào ở ngay bây giờ?" |
| 2 | Nhấn Có để xác nhận. | Hệ thống hiển thị hộp nhập mã thẻ RFID (có thể bỏ trống). Sau khi nhập, hệ thống kiểm tra phiên thu ngân rồi thực hiện check-in, chuyển phòng sang Đang ở, làm mới sơ đồ. |

### 1.8.5. Giao diện

Các thao tác chuyển trạng thái phòng nhanh không có form riêng. Toàn bộ được thực hiện qua menu ngữ cảnh (popup menu) khi click phải vào ô tile. Hệ thống chỉ hiển thị các lệnh phù hợp với trạng thái hiện tại của phòng được click phải.

**Bảng các lệnh menu ngữ cảnh theo trạng thái phòng**

| Trạng thái phòng | Trạng thái booking | Lệnh hiển thị trong menu |
|---|---|---|
| Trống | Chờ đến | Check-in, Hủy đặt phòng |
| Trống | Không có | Đưa vào bảo trì |
| Đang ở | Không có | Minibar, Đổi phòng, Gia hạn |
| Chờ dọn | Không có | Dọn xong |
| Bảo trì | Không có | Sửa xong |

### 1.8.6. Mô tả nghiệp vụ

Các lệnh chuyển trạng thái nhanh được thiết kế để hỗ trợ nghiệp vụ buồng phòng và kỹ thuật. Nhân viên buồng phòng xác nhận dọn xong để phòng Chờ dọn trở về Trống, sẵn sàng đón khách tiếp theo. Bộ phận kỹ thuật dùng lệnh Đưa vào bảo trì để tạm thời khóa phòng khỏi việc nhận khách, và dùng Sửa xong để trả phòng về trạng thái sẵn sàng sau khi hoàn tất sửa chữa.

Hệ thống không hiển thị lệnh không phù hợp với trạng thái phòng, nhờ đó tránh thao tác nhầm. Nếu sau khi lọc không còn lệnh nào hiển thị, menu ngữ cảnh sẽ không xuất hiện.

### 1.8.7. Quy tắc kiểm tra

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Nếu không có lệnh nào phù hợp với trạng thái phòng, menu ngữ cảnh không xuất hiện. | N/A |
| 2 | Lệnh Hủy đặt phòng yêu cầu xác nhận hai lần (hộp thoại Có / Không) trước khi thực hiện, do hành động này không thể hoàn tác. | MSG_LT_XAC_NHAN_HUY_DAT |
| 3 | Lệnh Check-in cho khách đặt trước yêu cầu phiên thu ngân đang mở. Nếu chưa có phiên, hệ thống mở luồng mở phiên trước khi thực hiện check-in. | ERR_LT_CHUA_MO_PHIEN |
| 4 | Nếu cập nhật trạng thái phòng thất bại ở phía máy chủ, hệ thống hiển thị thông báo lỗi và không thay đổi sơ đồ. | ERR_LT_CAP_NHAT_TRANG_THAI_THAT_BAI |

### 1.8.8. Liên kết use-case

- Sơ đồ phòng (mục 1.1) — điểm khởi động và nơi phản ánh kết quả.
- Nhận phòng / Đặt phòng (mục 1.2) — tình huống check-in khách đặt trước là luồng tắt của use-case này.

---

# 2. Yêu cầu khác

## 2.1. Định dạng dữ liệu

| Loại dữ liệu | Định dạng hiển thị | Ví dụ |
|---|---|---|
| Ngày giờ đầy đủ | dd/MM/yyyy HH:mm | 25/04/2025 14:30 |
| Ngày tháng (không giờ) | dd/MM/yyyy | 25/04/2025 |
| Ngày tháng ngắn gọn (in-tile, sidebar) | dd/MM HH:mm | 25/04 14:30 |
| Số tiền | N,NNNđ (phân cách hàng nghìn, ký hiệu đ) | 350,000đ |
| Số lượng | Số nguyên không âm | 2 |
| Điểm tích lũy | Số nguyên, phân cách hàng nghìn | 1,500 |
| Giá dự kiến (text phức hợp) | tổng (số đêm x đơn giá/đêm) | 700,000đ (2 đêm x 350,000đ) |

## 2.2. Danh mục dữ liệu tham chiếu

**Trạng thái phòng**

| Mã trạng thái | Tên hiển thị | Màu nhận diện |
|---|---|---|
| Trống | Trống | Xanh lá |
| DangO | Đang ở | Đỏ |
| ChoDon | Chờ dọn | Cam |
| BaoTri | Bảo trì | Xám |

**Trạng thái booking**

| Mã trạng thái | Tên hiển thị | Ý nghĩa |
|---|---|---|
| ChoDen | Chờ đến | Khách đã đặt phòng nhưng chưa check-in |

**Phương thức thanh toán**

| Giá trị | Tên hiển thị |
|---|---|
| Tiền mặt | Tiền mặt |
| Chuyển khoản | Chuyển khoản |
| Thẻ ngân hàng | Thẻ ngân hàng |

**Tỉ lệ quy đổi điểm tích lũy**

| Thông số | Giá trị |
|---|---|
| 1 điểm tích lũy tương đương | 1,000đ giảm giá |

## 2.3. Bảng mã thông báo lỗi

| Mã thông báo | Nội dung tiếng Việt |
|---|---|
| ERR_LT_CHUA_MO_PHIEN | Bạn chưa mở phiên thu ngân. Vui lòng mở phiên trước khi thao tác. |
| ERR_LT_LOAD_PHONG_THAT_BAI | Không thể tải danh sách phòng. Vui lòng thử lại hoặc liên hệ quản trị viên. |
| ERR_LT_THIEU_TEN_KHACH | Vui lòng nhập tên khách hàng. |
| ERR_LT_SDT_KHONG_HOP_LE | Số điện thoại không hợp lệ. Vui lòng nhập ít nhất 10 số. |
| ERR_LT_THIEU_PHONG | Vui lòng chọn phòng lưu trú. |
| ERR_LT_SO_NGUOI_KHONG_HOP_LE | Số người phải lớn hơn 0. |
| ERR_LT_VUOT_SO_NGUOI_TOI_DA | Phòng này chỉ cho phép tối đa {0} người. |
| ERR_LT_KHONG_CO_DIEM | Khách hàng không có điểm tích lũy khả dụng. |
| ERR_LT_MA_KM_KHONG_HOP_LE | Mã khuyến mãi không hợp lệ hoặc không áp dụng được cho hóa đơn này. |
| ERR_LT_KHONG_CO_PHONG_TRONG | Không có phòng trống nào để thực hiện đổi phòng. |
| ERR_LT_MINIBAR_RONG | Chưa chọn sản phẩm nào. Vui lòng nhập số lượng cho ít nhất một sản phẩm. |
| ERR_LT_MINIBAR_LOAD_THAT_BAI | Không thể tải danh sách sản phẩm minibar. Vui lòng thử lại. |
| ERR_LT_TIEN_PHU_THU_KHONG_HOP_LE | Vui lòng nhập số tiền phụ thu hợp lệ (lớn hơn 0). |
| ERR_LT_GHI_CHU_THIEU | Vui lòng nhập ghi chú lý do phụ thu. |
| ERR_LT_CAP_NHAT_TRANG_THAI_THAT_BAI | Không thể cập nhật trạng thái phòng. Vui lòng thử lại. |
| MSG_LT_NHAN_PHONG_THANH_CONG | Nhận phòng thành công! |
| MSG_LT_TRA_PHONG_THANH_CONG | Trả phòng thành công! |
| MSG_LT_PHU_THU_THANH_CONG | Ghi nhận phụ thu thành công! |
| MSG_LT_MINIBAR_THANH_CONG | Đã thêm sản phẩm minibar vào bill phòng thành công! |
| MSG_LT_XAC_NHAN_HUY_DAT | Bạn có chắc chắn muốn hủy đặt phòng này không? Thao tác này không thể hoàn tác. |
| MSG_LT_XAC_NHAN_CHECKIN_DAT_TRUOC | Phòng này đã được đặt trước. Bạn muốn Check-in khách vào ở ngay bây giờ? |
| MSG_LT_XAC_NHAN_DON_XONG | Xác nhận đã dọn phòng xong? |
