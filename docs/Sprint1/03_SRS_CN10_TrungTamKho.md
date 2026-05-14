# Khu Du Lịch Đại Nam
# Đặc Tả Yêu Cầu Phần Mềm
# Mã dự án: DN01
# Mã tài liệu: DN01_SRS_CN10_TrungTamKho_v1.1

Hồ Chí Minh, Tháng 04/2026

---

## Lịch sử thay đổi

| Ngày hiệu lực | Hạng mục thay đổi | A/M/D | Mô tả | Phiên bản |
|---|---|---|---|---|
| 19/04/2026 | Phát hành lần đầu | A | | 1.0 |
| 25/04/2026 | Cập nhật tài liệu theo triển khai thực tế | M | Cập nhật thanh bên trái lấy dữ liệu thực, tồn kho từ sổ cái, lịch sử mức chứng từ, cảnh báo hạn sử dụng và tồn tối thiểu thực, thêm danh mục trạng thái mới | 1.1 |

*A - Thêm mới, M - Chỉnh sửa, D - Xóa bỏ*

---

## 0. Phạm vi tài liệu

Tài liệu này đặc tả phân hệ Quản lý Kho thuộc hệ thống quản lý vận hành Khu Du lịch Đại Nam.
- **Phạm vi bao gồm:** tạo phiếu kho (nhập, xuất, chuyển, kiểm kê), tra cứu tồn kho, lịch sử giao dịch, cảnh báo hạn sử dụng và tồn tối thiểu.
- **Không bao gồm:** quản lý danh mục kho hàng (được đặc tả tại CN11), phân quyền truy cập, báo cáo tài chính.
- **Đối tượng đọc:** BA, Lập trình viên (Dev), Chuyên viên kiểm thử (Tester).

---

## Mục lục

1. [Quản lý kho](#1-quản-lý-kho)
   - 1.1. [Màn hình trung tâm kho](#11-màn-hình-trung-tâm-kho)
   - 1.2. [Màn hình tạo phiếu kho](#12-màn-hình-tạo-phiếu-kho)
   - 1.3. [Màn hình tồn kho](#13-màn-hình-tồn-kho)
   - 1.4. [Màn hình lịch sử giao dịch](#14-màn-hình-lịch-sử-giao-dịch)
   - 1.5. [Màn hình cảnh báo](#15-màn-hình-cảnh-báo)
   - 1.6. [Vòng đời chứng từ kho](#16-vòng-đời-chứng-từ-kho)
2. [Yêu cầu khác](#2-yêu-cầu-khác)
   - 2.1. [Định dạng dữ liệu](#21-định-dạng-dữ-liệu)
   - 2.2. [Danh mục dữ liệu tham chiếu](#22-danh-mục-dữ-liệu-tham-chiếu)
   - 2.3. [Bảng mã thông báo lỗi](#23-bảng-mã-thông-báo-lỗi)

---

# 1. Quản lý kho

Phân hệ quản lý kho bao gồm các chức năng sau:

- Trung tâm kho (điều hướng)
- Tạo phiếu kho
- Tra cứu tồn kho
- Lịch sử giao dịch
- Cảnh báo (hạn sử dụng, tồn tối thiểu)

---

## 1.1. Màn hình trung tâm kho

### 1.1.1. Tổng quan

Màn hình này là giao diện điều hướng chính của phân hệ kho. Bên trái hiển thị thanh chức năng cố định, bên phải hiển thị nội dung thay đổi tùy theo chức năng đang chọn.

### 1.1.2. Tác nhân

Tất cả nhân viên có quyền truy cập module kho.

### 1.1.3. Biểu đồ use-case

```
Nhân viên kho ──── Điều hướng chức năng
                   ├── Tạo phiếu
                   ├── Xem tồn kho
                   ├── Xem lịch sử
                   ├── Xem cảnh báo
                   └── Phê duyệt / Hủy phiếu
```

#### 1.1.3.1. Tiền điều kiện

- Người dùng đã đăng nhập hệ thống.
- Người dùng có quyền truy cập module kho.

#### 1.1.3.2. Hậu điều kiện

Màn hình con tương ứng được nạp vào vùng nội dung bên phải.

#### 1.1.3.3. Điểm kích hoạt

Người dùng truy cập menu Kho, chọn mục Trung tâm kho.

### 1.1.4. Luồng thao tác

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Mở màn hình trung tâm kho. | Hiển thị thanh điều hướng bên trái. Nạp mặc định màn hình Tạo phiếu vào vùng nội dung bên phải. Truy vấn cơ sở dữ liệu để hiển thị tối đa 10 phiếu gần đây (trong vòng 30 ngày). Tính số lô sắp hết hạn và số mặt hàng dưới mức tồn tối thiểu thực tế để hiển thị ô cảnh báo. |
| 2 | Nhấn một trong 4 nút chức năng trên thanh trái. | Nếu màn hình hiện tại là Tạo phiếu và phiếu đang soạn chưa lưu thì hiển thị hộp thoại xác nhận. Nếu chọn Không thì giữ nguyên. Nếu chọn Có thì hủy dữ liệu và nạp màn hình mới. |
| 3 | Nhấn chuột phải vào phiếu trong danh sách bên trái. | Hiển thị context menu: Phê duyệt ghi sổ, Hủy phiếu. Nếu chọn Phê duyệt, hệ thống ghi sổ cái kho và cập nhật trạng thái phiếu. Nếu chọn Hủy, hệ thống hiển thị hộp thoại xác nhận trước khi xóa phiếu. |
| 4 | Nhấn đúp vào phiếu trong danh sách bên trái. | Mở lại phiếu đó ở chế độ xem trong màn hình tạo phiếu. |

### 1.1.5. Giao diện

#### 1.1.5.1. Mô tả màn hình — Thanh điều hướng bên trái

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tạo phiếu | Button | N/A | N/A | Đang chọn | Nạp màn hình tạo phiếu mới vào vùng phải |
| 2 | Tồn kho | Button | N/A | N/A | N/A | Nạp màn hình tra cứu tồn kho |
| 3 | Lịch sử G.Dịch | Button | N/A | N/A | N/A | Nạp màn hình xem lịch sử phiếu |
| 4 | Cảnh báo | Button | N/A | N/A | N/A | Nạp màn hình cảnh báo. Nút này được tô nổi bật khác biệt. |
| 5 | DS phiếu gần đây | Image List Box | N/A | N/A | N/A | Hiển thị tối đa 10 phiếu gần nhất trong vòng 30 ngày, lấy từ cơ sở dữ liệu. Mỗi dòng gồm mã phiếu kèm biểu tượng trạng thái dạng vòng tròn màu: xanh lá cho Đã duyệt, đỏ cho Đã hủy, vàng cho Mới hoặc Chờ duyệt. Hỗ trợ nhấn đúp và nhấn chuột phải. |
| 6 | Cảnh báo HSD | Label | N/A | N/A | N/A | Hiển thị số lượng thực tế từ cơ sở dữ liệu theo mẫu: "{X} Lô sắp hết HSD". X là số lô hàng có hạn sử dụng còn lại dưới 7 ngày. |
| 7 | Cảnh báo tồn min | Label | N/A | N/A | N/A | Hiển thị số lượng thực tế từ cơ sở dữ liệu theo mẫu: "{X} Mặt hàng dưới mức tối thiểu". X là số sản phẩm có tồn kho thấp hơn mức cảnh báo đã thiết lập. |

#### 1.1.5.2. Context menu — Danh sách phiếu gần đây

Kích hoạt khi: người dùng nhấn chuột phải vào một phiếu trong danh sách.

| STT | Menu item | Mô tả |
|---|---|---|
| 1 | Phê duyệt ghi sổ | Duyệt phiếu đang chọn. Hệ thống ghi sổ cái kho. Cập nhật trạng thái phiếu thành Đã duyệt. Hiển thị thông báo kết quả. |
| 2 | Hủy phiếu | Hủy phiếu đang chọn. Cập nhật trạng thái thành Đã hủy. Hiển thị thông báo kết quả. |

### 1.1.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Nạp mặc định | Khi mở trung tâm kho, hệ thống tự động nạp màn hình Tạo phiếu |
| 2 | Chuyển trang | Khi chuyển sang màn hình khác, hủy và giải phóng bộ nhớ màn hình cũ, khởi tạo màn hình mới |
| 3 | Bảo vệ dữ liệu | Chỉ cảnh báo khi rời khỏi màn hình Tạo phiếu có dữ liệu chưa lưu |
| 4 | Bỏ qua kiểm tra tồn kho | Khi phê duyệt chứng từ, nếu là phiếu Nhập mua (NHAP_MUA) hoặc Khách trả hàng (KHACH_TRA) từ nguồn kho ảo, hệ thống sẽ bỏ qua bước kiểm tra tồn kho hiện tại để cho phép nhập hàng mới vào. |
| 5 | Đa ngôn ngữ | Khi ngôn ngữ hệ thống thay đổi, tất cả nhãn, nút bấm, tiêu đề cột trên thanh trái và màn hình con đang mở đều cập nhật tức thì |
| 6 | Double-click phiếu | Nhấn đúp vào phiếu để mở lại phiếu ở chế độ xem |

### 1.1.7. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Không có quy tắc đặc biệt | — |

### 1.1.8. Liên kết use-case

- Tạo phiếu kho
- Tồn kho
- Lịch sử giao dịch
- Cảnh báo

---

## 1.2. Màn hình tạo phiếu kho

### 1.2.1. Tổng quan

Màn hình này dùng để tạo mới phiếu xuất nhập kho. Giao diện thay đổi linh hoạt theo loại phiếu đang chọn: ẩn/hiện các trường nhập liệu và các cột trên lưới chi tiết cho phù hợp.

Hệ thống áp dụng nguyên lý kế toán kép: mọi di chuyển hàng đều có kho xuất và kho nhập. Các đầu mối bên ngoài (nhà cung cấp, khách hàng, thùng rác) được mô phỏng thành kho ảo. Người dùng chỉ chọn kho thật, kho ảo do hệ thống tự gán.

### 1.2.2. Tác nhân

- Thủ kho (tạo phiếu nhập, xuất, chuyển, hủy)
- Quản lý kho (tạo phiếu kiểm kê)
- Bếp trưởng (tạo phiếu xuất sản xuất)

### 1.2.3. Biểu đồ use-case

```
Thủ kho ──── Tạo phiếu kho
```

#### 1.2.3.1. Tiền điều kiện

- Người dùng đã đăng nhập.
- Danh mục kho hàng, sản phẩm, đơn vị tính, nhà cung cấp đã tồn tại trong hệ thống.

#### 1.2.3.2. Hậu điều kiện

Phiếu mới được lưu vào cơ sở dữ liệu với trạng thái Mới.

#### 1.2.3.3. Điểm kích hoạt

Nhấn nút Tạo phiếu trên thanh điều hướng bên trái của trung tâm kho.

### 1.2.4. Luồng thao tác

#### 1.2.4.1. Tình huống 1 — Nhập hàng từ nhà cung cấp

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Mở màn hình tạo phiếu. | Hiển thị màn hình tạo phiếu. Loại phiếu mặc định là phiếu đầu tiên trong danh sách. Sinh mã phiếu tự động. |
| 2 | Chọn loại phiếu Nhập mua từ NCC. | Ẩn ô Kho xuất. Hiện ô Kho nhập và ô Nhà cung cấp. Hiện cột đơn giá, thành tiền trên lưới chi tiết. |
| 3 | Chọn kho nhập. Chọn nhà cung cấp. | Ghi nhận. |
| 4 | Bấm vào vùng trống lưới chi tiết để thêm dòng. Chọn sản phẩm, nhập số lượng và đơn giá. | Tự điền tên sản phẩm, đơn vị tính. Tính thành tiền bằng số lượng nhân đơn giá. |
| 5 | Nhấn nút Lưu. | Kiểm tra các trường bắt buộc. Nếu hợp lệ thì lưu phiếu vào cơ sở dữ liệu, thông báo thành công và sinh mã phiếu mới cho lần kế. |

#### 1.2.4.2. Tình huống 2 — Chuyển kho nội bộ

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Chọn loại phiếu Chuyển kho. | Hiện ô Kho xuất và Kho nhập. Hiện bảng hàng tồn tại kho xuất ở nửa trái. Ẩn dòng nhập mới trên lưới chi tiết. |
| 2 | Chọn kho xuất và kho nhập. | Nạp danh sách hàng tồn tại kho xuất vào bảng trái. |
| 3 | Nhấn đúp hàng ở bảng trái. | Hàng nhảy sang lưới chi tiết bên phải. |
| 4 | Nhấn đúp hàng ở lưới phải (nếu muốn bỏ ra). | Hàng quay lại bảng trái. |
| 5 | Nhấn nút Lưu. | Kiểm tra, lưu phiếu chuyển kho. |

#### 1.2.4.3. Tình huống 3 — Kiểm kê kho

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Chọn loại phiếu Kiểm kê điều chỉnh. | Ẩn cột đơn giá, thành tiền. Hiện cột Đếm thực tế. Nếu người dùng có quyền quản lý kho thì hiển thị thêm cột Tồn máy và Lệch. Nếu không có quyền thì chỉ hiển thị cột Đếm thực tế. |
| 2 | Chọn kho kiểm. Thêm hàng, nhập số đếm thực tế. | Tự tính cột Lệch bằng đếm thực tế trừ tồn máy. |
| 3 | Nhấn nút Lưu. | Lưu phiếu kiểm kê với trạng thái Mới. |

#### 1.2.4.4. Tình huống 4 — Dữ liệu không hợp lệ

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Mở màn hình tạo phiếu. Chọn loại phiếu bất kỳ. | Hiển thị form nhập liệu tương ứng. |
| 2 | Không chọn kho hoặc lưới chi tiết rồng, nhấn Lưu. | Hiển thị thông báo lỗi tương ứng (ERR_CHONKHO, ERR_CHITIETRONG). Không lưu phiếu. |

### 1.2.5. Giao diện

#### 1.2.5.1. Mô tả màn hình — Phần header (thông tin chung)

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Loại phiếu | Image Combo Box | Yes | Text | Phiếu đầu tiên | Chọn loại nghiệp vụ. Khi đổi loại thì toàn bộ giao diện thay đổi theo (xem bảng 1.2.5.3). |
| 2 | Mã phiếu | Text field (Read-only) | Yes | Text | Auto-generated | Sinh theo quy tắc: tiền tố loại phiếu + số thứ tự. Không cho sửa. (*) Tooltip: "Mã phiếu tự động sinh bởi hệ thống" |
| 3 | Ngày lập phiếu | Date picker | Yes | Date | Today | Định dạng dd/MM/yyyy. |
| 4 | Kho xuất | Search Lookup Edit | Conditional | Integer | Blank | Chỉ hiển thị kho thật (kho ảo bị lọc ra). Ẩn/hiện tùy loại phiếu. (*) Tooltip: "Chọn kho xuất hàng" |
| 5 | Kho nhập | Search Lookup Edit | Conditional | Integer | Blank | Chỉ hiển thị kho thật. Ẩn/hiện tùy loại phiếu. (*) Tooltip: "Chọn kho nhận hàng" |
| 6 | Đối tác | Search Lookup Edit | Conditional | Integer | Blank | Danh sách nhà cung cấp hoặc khách hàng. Ẩn/hiện tùy loại phiếu. (*) Tooltip: "Chọn nhà cung cấp hoặc khách hàng liên quan" |
| 7 | Ghi chú | Memo Edit | No | Text | Blank | |
| 8 | Lưu | Button | N/A | N/A | N/A | Lưu phiếu vào cơ sở dữ liệu. |
| 9 | Hủy | Button | N/A | N/A | N/A | Xóa trắng lưới chi tiết, đặt lại trạng thái chưa thay đổi. |
| 10 | In phiếu | Button | N/A | N/A | N/A | Mở bản xem trước phiếu kho để in. Hệ thống báo lỗi nếu phiếu chưa được lưu. |

#### 1.2.5.2. Mô tả màn hình — Lưới chi tiết hàng hóa (Grid Control)

| STT | Tên cột | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Mã sản phẩm | Search Lookup Edit (in-grid) | Yes | Text | Blank | Gõ mã hoặc tên sản phẩm đều tìm được. Khi chọn thì hệ thống tự điền tên và đơn vị tính. (*) Tooltip: "Gõ để tìm kiếm sản phẩm" |
| 2 | Tên sản phẩm | Label (Read-only) | — | Text | Blank | Tự điền sau khi chọn mã sản phẩm. Không cho sửa. |
| 3 | Đơn vị tính | Label (Read-only) | — | Text | Blank | Lấy đơn vị tính gốc của sản phẩm. Không cho sửa. |
| 4 | Số lượng | Spin Edit | Yes | Decimal(18,3) | 1 | Không cho nhập giá trị âm. (*) Tooltip: "Nhập số lượng xuất/nhập" |
| 5 | Đơn giá | Spin Edit | Conditional | Decimal(15,0) | 0 | Chỉ hiện ở loại phiếu có liên quan đến tiền. (*) Tooltip: "Đơn giá tại thời điểm giao dịch" |
| 6 | Thành tiền | Label (Read-only) | — | Decimal | 0 | Tự tính bằng Số lượng nhân Đơn giá. Không cho sửa. |
| 7 | Tồn máy | Label (Read-only) | — | Decimal(18,3) | 0 | Chỉ hiện khi loại phiếu là Kiểm kê. Tồn kho hệ thống đang ghi nhận. (*) Tooltip: "Số lượng hệ thống đang ghi nhận" |
| 8 | Đếm thực tế | Spin Edit | Conditional | Decimal(18,3) | 0 | Chỉ hiện khi loại phiếu là Kiểm kê. Nhân viên nhập số đếm tay. (*) Tooltip: "Nhập số lượng đếm thực tế ngoài kho" |
| 9 | Lệch | Label (Read-only) | — | Decimal(18,3) | 0 | Chỉ hiện khi loại phiếu là Kiểm kê. Tự tính bằng Đếm thực tế trừ Tồn máy. (*) Tooltip: "Chênh lệch giữa thực tế và hệ thống" |

**Thao tác thêm dòng:**
- Nhấn chuột vào vùng trống phía dưới lưới. Khi lưới rỗng, hệ thống hiển thị dòng chữ hướng dẫn dạng chữ nghiêng, màu xám: "Bấm chuột vào vùng này để thêm sản phẩm..."

#### 1.2.5.3. Context menu — Lưới chi tiết hàng hóa

Kích hoạt khi: người dùng nhấn chuột phải vào một dòng hàng trên lưới chi tiết.

| STT | Menu item | Mô tả |
|---|---|---|
| 1 | Xóa dòng | Xóa dòng hàng đang chọn khỏi lưới. Nếu loại phiếu là Chuyển kho, dòng bị xóa sẽ được trả lại bảng nguồn bên trái. |

#### 1.2.5.4. Bảng quy tắc ẩn/hiện theo loại phiếu

| Loại phiếu | Kho xuất | Kho nhập | Đối tác | Đơn giá / Thành tiền | Bảng nguồn (nửa trái) |
|---|---|---|---|---|---|
| Nhập mua từ NCC | Hidden | Visible | Visible (label: Nhà cung cấp) | Visible | Hidden |
| Xuất bán | Visible | Hidden | Hidden | Visible | Hidden |
| Trả hàng NCC | Visible | Hidden | Visible (label: Nhà cung cấp) | Visible | Hidden |
| Khách trả hàng | Hidden | Visible | Visible (label: "Đối tác") | Visible | Hidden |
| Chuyển kho | Visible | Visible | Hidden | Hidden | Visible |
| Hủy / Hao hụt | Visible | Hidden | Hidden | Hidden | Hidden |
| Xuất bảo trì | Visible | Hidden | Hidden | Hidden | Hidden |
| Xuất sản xuất | Visible | Hidden | Hidden | Hidden | Hidden |
| Kiểm kê điều chỉnh | Visible (label: "Kho kiểm") | Hidden | Hidden | Hidden | Hidden |

### 1.2.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Loại phiếu | Khi thay đổi: sinh lại mã phiếu mới, ẩn tất cả cột lưới rồi hiện lại theo quy tắc bảng 1.2.5.4, ẩn/hiện các ô nhập header tương ứng |
| 2 | Mã phiếu | Tự sinh theo quy tắc tiền tố. Ví dụ: phiếu nhập mua dùng mã PN_001, xuất bán dùng mã PX_001, chuyển kho dùng mã PC_001, kiểm kê dùng mã PK_001 |
| 3 | Kho ảo tự gán | Nhập mua: kho xuất là kho ảo KHO_NCC. Xuất bán: kho nhập là kho ảo KHO_KHACH. Trả NCC: kho nhập là kho ảo KHO_NCC. Khách trả: kho xuất là kho ảo KHO_KHACH. Hủy hàng: kho nhập là kho ảo KHO_HUY. Kiểm kê thiếu: kho nhập là kho ảo KHO_THAT_THOAT. Người dùng không thấy và không chọn kho ảo. |
| 4 | Chuyển kho — bảng nguồn | Bảng nửa trái hiển thị hàng tồn tại kho xuất đã chọn. Nhấn đúp dòng bên trái để chuyển sang lưới phải. Nhấn đúp dòng bên phải để trả lại bên trái. Không cho thêm dòng mới bằng tay (ẩn New Item Row). |
| 5 | Kiểm kê — phân quyền | Nếu người dùng có quyền quản lý kho thì hiển thị cột Tồn máy và Lệch. Nếu không thì chỉ hiển thị cột Đếm thực tế. Mục đích: tránh nhân viên đếm thiên vị theo số máy. |
| 6 | Dirty tracking | Mọi thao tác sửa đổi trên phiếu (đổi loại, chọn kho, thêm/xóa/sửa dòng) đều kích hoạt cờ "đã thay đổi". Cờ này phục vụ cảnh báo khi chuyển trang. Khi lưu thành công hoặc nhấn nút Hủy thì cờ được reset. |
| 7 | Nút Lưu | Kiểm tra dữ liệu, nếu hợp lệ thì lưu phiếu ở trạng thái Mới, thông báo thành công, xóa lưới và sinh mã phiếu mới |
| 8 | Nút Hủy | Xóa trắng lưới chi tiết, reset cờ dirty |
| 9 | Chọn sản phẩm | Khi người dùng chọn sản phẩm từ Search Lookup, hệ thống tự động điền "Tên sản phẩm" và "Đơn vị tính" vào dòng tương ứng |
| 10 | Tính thành tiền | Khi Số lượng hoặc Đơn giá thay đổi thì Thành tiền tự tính lại bằng Số lượng nhân Đơn giá |
| 11 | Tính lệch | Khi Đếm thực tế hoặc Tồn máy thay đổi thì Lệch tự tính lại bằng Đếm thực tế trừ Tồn máy |

### 1.2.7. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Phiếu nhập mua hoặc khách trả: bắt buộc chọn kho nhập | ERR_CHON_KHO_NHAP |
| 2 | Phiếu chuyển kho: bắt buộc chọn cả kho xuất lẫn kho nhập | ERR_CHON_KHO_CHUYEN |
| 3 | Phiếu chuyển kho: kho xuất không được trùng kho nhập | ERR_KHO_TRUNG |
| 4 | Các loại phiếu khác: bắt buộc chọn kho xuất | ERR_CHON_KHO_XUAT |
| 5 | Lưới chi tiết phải có ít nhất 1 dòng hàng hợp lệ (có mã sản phẩm) | ERR_CHITIETRONG |
| 6 | Ngày lập phiếu không được trống, không được trước năm 1900 | ERR_CHONNGAY |
| 7 | Phiếu nhập mua, trả NCC: bắt buộc chọn đối tác | ERR_CHONDOITAC |
| 8 | Phiếu xuất: tồn kho không đủ để xuất | ERR_TONKHO_KHONGDU |

### 1.2.8. Liên kết use-case

- Trung tâm kho
- Tồn kho
- Lịch sử giao dịch

---

## 1.3. Màn hình tồn kho

### 1.3.1. Tổng quan

Màn hình này hiển thị số lượng tồn hiện tại của từng mặt hàng tại từng kho. Hỗ trợ lọc theo kho và nhóm sản phẩm. Hỗ trợ xuất dữ liệu ra file Excel.

### 1.3.2. Tác nhân

Tất cả nhân viên có quyền truy cập module kho.

### 1.3.3. Biểu đồ use-case

```
Nhân viên kho ──── Tra cứu tồn kho
                   └── Xuất Excel
```

#### 1.3.3.1. Tiền điều kiện

Người dùng đã đăng nhập.

#### 1.3.3.2. Hậu điều kiện

Danh sách tồn kho được hiển thị theo bộ lọc đã chọn.

#### 1.3.3.3. Điểm kích hoạt

Nhấn nút Tồn kho trên thanh điều hướng bên trái.

### 1.3.4. Luồng thao tác

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Mở màn hình tồn kho. | Hiển thị bộ lọc và lưới tồn kho với dữ liệu mặc định (tất cả kho, tất cả nhóm). |
| 2 | Chọn kho và/hoặc nhóm sản phẩm, nhấn Lọc. | Tải lại dữ liệu theo bộ lọc. |
| 3 | Nhấn Xuất Excel. | Xuất toàn bộ lưới ra file .xlsx, mở file sau khi xuất. |

### 1.3.5. Giao diện

#### 1.3.5.1. Mô tả màn hình — Bộ lọc

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Kho | Search Lookup Edit | No | Integer | All | Danh sách kho thật. Để trống để lọc tất cả. (*) Tooltip: "Chọn kho cần xem tồn" |
| 2 | Nhóm sản phẩm | Combo Box | No | Text | All | Lọc theo nhóm: ăn uống, nguyên liệu, hàng hóa... |
| 3 | Lọc | Button | N/A | N/A | N/A | Tải lại dữ liệu theo bộ lọc |
| 4 | Xuất Excel | Button | N/A | N/A | N/A | Xuất lưới ra file .xlsx và mở. (*) Tooltip: "Xuất toàn bộ dữ liệu sang Excel" |

#### 1.3.5.2. Mô tả màn hình — Lưới tồn kho (Grid Control, Read-only)

Dữ liệu tồn kho được tính từ sổ cái kho: tổng số lượng nhập vào kho thật trừ đi tổng số lượng xuất khỏi kho thật. Chỉ hiển thị các kho thật, không hiển thị kho ảo.

| STT | Tên hiển thị | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Mã SP | Label | Text | Mã định danh sản phẩm |
| 2 | Tên sản phẩm | Label | Text | Tên hiển thị |
| 3 | ĐVT | Label | Text | Đơn vị tính gốc của sản phẩm |
| 4 | Kho | Label | Text | Tên kho thật đang chứa hàng |
| 5 | Tồn kho | Label | Decimal(18,3) | Số lượng tồn hiện tại, tính bằng tổng nhập trừ tổng xuất |
| 6 | Trạng thái | Label | Text | Bình thường hoặc Dưới mức. Hệ thống tự động đánh giá dựa trên mức cảnh báo đã thiết lập cho từng sản phẩm. |

### 1.3.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Row style | Dòng có trạng thái Dưới mức thì nền đỏ nhạt (#FFEBEE), chữ đỏ đậm (DarkRed) |
| 2 | Xuất Excel | Xuất toàn bộ dữ liệu trên lưới (không phân trang) ra file TonKho.xlsx. Tự mở file sau khi xuất. |

### 1.3.7. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Không có quy tắc đặc biệt | — |

### 1.3.8. Liên kết use-case

- Trung tâm kho
- Cảnh báo (thẻ tồn tối thiểu)

---

## 1.4. Màn hình lịch sử giao dịch

### 1.4.1. Tổng quan

Màn hình này hiển thị danh sách các chứng từ kho đã tạo trong một khoảng thời gian, lấy từ cơ sở dữ liệu. Mỗi dòng là một phiếu kho.

### 1.4.2. Tác nhân

Tất cả nhân viên có quyền truy cập module kho.

### 1.4.3. Biểu đồ use-case

```
Nhân viên kho ──── Tra cứu lịch sử giao dịch
```

#### 1.4.3.1. Tiền điều kiện

Người dùng đã đăng nhập.

#### 1.4.3.2. Hậu điều kiện

Danh sách giao dịch được hiển thị theo bộ lọc.

#### 1.4.3.3. Điểm kích hoạt

Nhấn nút Lịch sử giao dịch trên thanh điều hướng bên trái.

### 1.4.4. Luồng thao tác

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Mở màn hình lịch sử. | Hiển thị bộ lọc với giá trị mặc định: từ ngày là 30 ngày trước, đến ngày là hôm nay. Tải dữ liệu theo mặc định. |
| 2 | Thay đổi bộ lọc (kho, khoảng ngày), nhấn Tìm kiếm. | Tải lại dữ liệu theo bộ lọc mới. |

### 1.4.5. Giao diện

#### 1.4.5.1. Mô tả màn hình — Bộ lọc

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Kho | Search Lookup Edit | No | Integer | All | Danh sách kho thật. (*) Tooltip: "Chọn kho cần xem lịch sử" |
| 2 | Từ ngày | Date Edit | No | Date | Today − 30 | Mốc bắt đầu. Định dạng dd/MM/yyyy. |
| 3 | Đến ngày | Date Edit | No | Date | Today | Mốc kết thúc. Định dạng dd/MM/yyyy. |
| 4 | Tìm kiếm | Button | N/A | N/A | N/A | Tải lại dữ liệu theo bộ lọc |

#### 1.4.5.2. Mô tả màn hình — Lưới lịch sử (Grid Control, Read-only)

Mỗi dòng hiển thị một chứng từ kho. Người dùng nhấn đúp để xem chi tiết mặt hàng bên trong.

| STT | Tên hiển thị | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Ngày | Label | DateTime | Ngày lập chứng từ. Định dạng dd/MM/yyyy HH:mm |
| 2 | Mã phiếu | Label | Text | Mã chứng từ kho |
| 3 | Loại | Label | Text | Loại phiếu: nhập mua, xuất bán, chuyển kho, kiểm kê... |
| 4 | Trạng thái | Label | Text | Trạng thái hiện tại của phiếu: Mới, Chờ duyệt, Đã duyệt, Đã hủy |
| 5 | Ghi chú | Label | Text | Ghi chú kèm theo phiếu |

### 1.4.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Dữ liệu mặc định | Khi mở lần đầu, tự lấy dữ liệu 30 ngày gần nhất |
| 2 | Lọc theo kho | Để trống để hiển thị tất cả kho |

### 1.4.7. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | "Từ ngày" không được lớn hơn "Đến ngày" | ERR_CONSTRAINT_DATE_FROMTO |
| 2 | Định dạng ngày không hợp lệ | ERR_FORMAT_DATE |

### 1.4.8. Liên kết use-case

- Trung tâm kho
- Tạo phiếu kho

---

## 1.5. Màn hình cảnh báo

### 1.5.1. Tổng quan

Màn hình này tổng hợp hai loại cảnh báo quan trọng của kho, trình bày dưới dạng hai thẻ chuyển đổi (Tab Control): cảnh báo hạn sử dụng và cảnh báo tồn tối thiểu.

### 1.5.2. Tác nhân

- Quản lý kho
- Thủ kho

### 1.5.3. Biểu đồ use-case

```
Quản lý kho ──── Xem cảnh báo hạn sử dụng
             └── Xem cảnh báo tồn tối thiểu
```

#### 1.5.3.1. Tiền điều kiện

Người dùng đã đăng nhập.

#### 1.5.3.2. Hậu điều kiện

Danh sách cảnh báo được hiển thị.

#### 1.5.3.3. Điểm kích hoạt

Nhấn nút Cảnh báo trên thanh điều hướng bên trái.

### 1.5.4. Luồng thao tác

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Mở màn hình cảnh báo. | Hiển thị Tab Control gồm 2 tab page. Tab Hạn sử dụng mở mặc định. Tải dữ liệu cho cả 2 tab. |
| 2 | Chuyển sang tab Tồn tối thiểu. | Hiển thị danh sách hàng dưới mức tồn tối thiểu. |

### 1.5.5. Giao diện

#### 1.5.5.1. Mô tả màn hình — Tab 1: Hạn sử dụng (Grid Control, Read-only)

Dữ liệu lấy từ bảng lô hàng: các lô có hạn sử dụng còn lại dưới 7 ngày và vẫn còn hàng trong kho.

| STT | Tên hiển thị | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Lô hàng | Label | Text | Mã lô nhập |
| 2 | Tên sản phẩm | Label | Text | Tên mặt hàng |
| 3 | Hạn sử dụng | Label | Date | Định dạng dd/MM/yyyy |
| 4 | Còn lại (ngày) | Label | Integer | Số ngày còn lại đến khi hết hạn. Hệ thống tự tính dựa trên ngày hiện tại. |
| 5 | Số lượng | Label | Decimal(18,3) | Số lượng còn tồn của lô |

#### 1.5.5.2. Mô tả màn hình — Tab 2: Tồn tối thiểu (Grid Control, Read-only)

Dữ liệu lấy bằng cách so sánh tồn kho thực tế với mức tồn tối thiểu đã thiết lập. Chỉ hiển những sản phẩm có tồn kho thấp hơn mức cảnh báo.

| STT | Tên hiển thị | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Mã SP | Label | Text | Mã định danh sản phẩm |
| 2 | Tên sản phẩm | Label | Text | Tên hiển thị |
| 3 | Kho | Label | Text | Tên kho |
| 4 | Tồn kho | Label | Decimal(18,3) | Số lượng hiện có, tính từ sổ cái kho |
| 5 | Mức cảnh báo | Label | Decimal(18,3) | Ngưỡng tối thiểu đã thiết lập |
| 6 | Thiếu | Label | Decimal(18,3) | Hiệu của tồn kho trừ mức cảnh báo, luôn là số âm |

### 1.5.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Row style — Tab HSD | Còn lại từ 3 ngày trở xuống thì nền đỏ nhạt (#FFEBEE), chữ đỏ đậm (DarkRed). Còn lại từ 4 đến 7 ngày thì nền cam nhạt (#FFF3E0), chữ cam đậm (DarkOrange). |
| 2 | Row style — Tab tồn min | Tất cả dòng thì nền đỏ nhạt (#FFEBEE), chữ đỏ đậm (DarkRed). Lý do: xuất hiện ở tab này tức là đã dưới mức). |
| 3 | Nguồn dữ liệu HSD | Lấy các lô hàng có hạn sử dụng còn lại dưới 7 ngày, chỉ lấy lô vẫn còn hàng (trạng thái Còn hàng). Số ngày còn lại được hệ thống tự tính dựa trên ngày hiện tại. |
| 4 | Nguồn dữ liệu tồn min | Hệ thống so sánh tồn kho thực tế (tính từ sổ cái kho) với mức cảnh báo đã thiết lập cho từng mặt hàng tại từng kho. Chỉ hiện những hàng có tồn thấp hơn ngưỡng cảnh báo. |

### 1.5.7. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Không có quy tắc đặc biệt | — |

### 1.5.8. Liên kết use-case

- Trung tâm kho
- Tồn kho
- Tạo phiếu kho (tạo phiếu hủy cho hàng hết hạn, tạo phiếu nhập bổ sung cho hàng thiếu)

---

## 1.6. Vòng đời chứng từ kho

```
Mới -- Chờ duyệt -- Đã duyệt
              └-- Đã hủy
Mới -- Đã hủy
```

Mô tả:

1. Nhân viên tạo phiếu mới, trạng thái là Mới.
2. Nhân viên gửi phiếu lên quản lý, trạng thái chuyển thành Chờ duyệt.
3. Quản lý xem xét phiếu:
   - 3a. Nếu đồng ý thì trạng thái chuyển thành Đã duyệt. Hệ thống ghi sổ cái kho và cập nhật tồn kho.
   - 3b. Nếu từ chối thì trạng thái chuyển thành Đã hủy.
4. Nhân viên có thể tự hủy phiếu đang ở trạng thái Mới, trạng thái chuyển thành Đã hủy.

Bảng chuyển trạng thái:

| Trạng thái hiện tại / Tiếp theo | Mới | Chờ duyệt | Đã duyệt | Đã hủy |
|---|---|---|---|---|
| Mới | | x | | x |
| Chờ duyệt | | | x | x |
| Đã duyệt | | | | |
| Đã hủy | | | | |

---

# 2. Yêu cầu khác

## 2.1. Định dạng dữ liệu

### 2.1.1. Ngày giờ

- Định dạng ngày mặc định: dd/MM/yyyy. Ví dụ: 19/04/2026
- Định dạng ngày giờ: dd/MM/yyyy HH:mm. Ví dụ: 19/04/2026 14:30

### 2.1.2. Số

- Số tiền: dấu phẩy phân cách hàng nghìn, không có chữ số thập phân, kèm ký hiệu ₫. Ví dụ: 14,000,000₫
- Số lượng: tối đa 3 chữ số thập phân. Ví dụ: 0.500
- Không có số 0 ở đầu. Ví dụ: 5 (không phải 05)

## 2.2. Danh mục dữ liệu tham chiếu

### 2.2.1. Loại phiếu kho

| Mã | Tên hiển thị |
|---|---|
| NHAP_MUA | Nhập mua từ NCC |
| XUAT_BAN | Xuất bán cho khách |
| TRA_NCC | Trả hàng cho NCC |
| KHACH_TRA | Khách trả hàng |
| HUY_HONG | Hủy / Hao hụt |
| CHUYEN_KHO | Chuyển kho |
| KIEM_KE | Kiểm kê điều chỉnh |
| XUAT_BAOTRI | Xuất vật tư bảo trì |
| XUAT_SANXUAT | Xuất sản xuất (BOM) |

### 2.2.2. Trạng thái chứng từ kho

| Mã | Tên hiển thị |
|---|---|
| Moi | Mới |
| ChoDuyet | Chờ duyệt |
| DaDuyet | Đã duyệt |
| DaHuy | Đã hủy |

### 2.2.3. Kho ảo hệ thống

| Mã kho | Tên | Mô tả |
|---|---|---|
| KHO_NCC | Nhà cung cấp | Đại diện cho nguồn hàng bên ngoài |
| KHO_KHACH | Khách hàng | Đại diện cho hàng đã bán ra |
| KHO_HUY | Hủy hỏng / Hết hạn | Đại diện cho hàng bị loại bỏ |
| KHO_THAT_THOAT | Thất thoát | Đại diện cho hàng mất mát không rõ nguyên nhân |
| KHO_TRA | Hàng trả lại | Đại diện cho hàng NCC nhận trả |
| KHO_THU | Hàng cho thú ăn | Đại diện cho thức ăn xuất cho khu thú |

### 2.2.4. Trạng thái kho

| Mã | Tên hiển thị |
|---|---|
| HoatDong | Đang hoạt động |
| NgungHoatDong | Ngừng hoạt động |

### 2.2.5. Trạng thái tồn kho

| Mã | Tên hiển thị | Mô tả |
|---|---|---|
| BINH_THUONG | Bình thường | Tồn kho lớn hơn hoặc bằng mức cảnh báo |
| DUOI_MUC | Dưới mức | Tồn kho thấp hơn mức cảnh báo đã thiết lập |

### 2.2.6. Trạng thái lô hàng

| Mã | Tên hiển thị |
|---|---|
| ConHang | Còn hàng |
| HetHang | Hết hàng |
| DaHuy | Đã hủy |

## 2.3. Bảng mã thông báo lỗi

| Mã thông báo | Nội dung tiếng Việt |
|---|---|
| ERR_CHON_KHO_NHAP | Vui lòng chọn kho nhập |
| ERR_CHON_KHO_XUAT | Vui lòng chọn kho xuất |
| ERR_CHON_KHO_CHUYEN | Vui lòng chọn cả kho xuất và kho nhập |
| ERR_KHO_TRUNG | Kho xuất và kho nhập không được trùng nhau |
| ERR_CHITIETRONG | Chưa có chi tiết hàng hóa |
| ERR_CHONNGAY | Vui lòng chọn ngày lập phiếu hợp lệ |
| ERR_CHONDOITAC | Vui lòng chọn đối tác |
| ERR_TONKHO_KHONGDU | Tồn kho không đủ để xuất |
| ERR_CONSTRAINT_DATE_FROMTO | "Từ ngày" không được lớn hơn "Đến ngày" |
| ERR_FORMAT_DATE | Định dạng ngày không hợp lệ |
| MSG_LUUTHANHCONG | Lưu phiếu thành công |
| MSG_LUUTHATBAI | Lưu thất bại |
| MSG_DUYETTHANHCONG | Đã phê duyệt phiếu |
| MSG_HUYTHANHCONG | Đã hủy phiếu |
| MSG_WARN_UNSAVED | Phiếu chưa lưu. Chuyển trang sẽ mất dữ liệu đang nhập. Bạn có muốn tiếp tục? |
| MSG_XAC_NHAN_HUY_PHIEU | Xác nhận hủy phiếu này? |
| MSG_CTK_DUYET_OK | Phê duyệt chứng từ thành công |
| MSG_CTK_XOA_OK | Xóa chứng từ thành công |
| ERR_CTK_KHONG_TON_TAI | Không tìm thấy chứng từ |
| ERR_CTK_DA_DUYET_TRUOC_DO | Chứng từ này đã được duyệt trước đó |
| MSG_KHO_CHUA_LUU_KHONG_IN | Vui lòng lưu phiếu trước khi in. |




