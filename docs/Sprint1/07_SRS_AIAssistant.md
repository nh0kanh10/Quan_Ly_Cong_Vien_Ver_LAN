# Khu Du Lịch Đại Nam
# Đặc Tả Yêu Cầu Phần Mềm
# Mã dự án: DN01
# Mã tài liệu: DN01_SRS_TroLyAI_v1.0

Hồ Chí Minh, Tháng 04/2026

---

## Lịch sử thay đổi

| Ngày hiệu lực | Hạng mục thay đổi | A/M/D | Mô tả | Phiên bản |
|---|---|---|---|---|
| 23/04/2026 | Phát hành lần đầu | A | | 1.0 |

*A - Thêm mới, M - Chỉnh sửa, D - Xóa bỏ*

---

## Mục lục

1. [Trợ lý AI](#1-trợ-lý-ai)
   - 1.1. [Bảng trò chuyện](#11-bảng-trò-chuyện)
   - 1.2. [Gửi câu hỏi](#12-gửi-câu-hỏi)
   - 1.3. [Gợi ý theo ngữ cảnh](#13-gợi-ý-theo-ngữ-cảnh)
   - 1.4. [Lệnh thông minh](#14-lệnh-thông-minh)
   - 1.5. [Cài đặt kết nối AI](#15-cài-đặt-kết-nối-ai)
2. [Yêu cầu khác](#2-yêu-cầu-khác)
   - 2.1. [Bảng mã thông báo](#21-bảng-mã-thông-báo)

---

# 1. Trợ lý AI

Trợ lý AI là tính năng hỗ trợ người dùng tương tác với hệ thống bằng ngôn ngữ tự nhiên. Người dùng có thể hỏi đáp, yêu cầu hướng dẫn sử dụng, tra cứu dữ liệu, hoặc ra lệnh lọc danh sách và mở chức năng mà không cần thao tác thủ công. Bao gồm các chức năng sau:

- Trò chuyện hỏi đáp với AI
- Nhận gợi ý câu hỏi phù hợp với chức năng đang mở
- Ra lệnh bằng ngôn ngữ tự nhiên để lọc dữ liệu, mở chức năng
- Cài đặt khóa kết nối và phiên bản AI

---

## 1.1. Bảng trò chuyện

### 1.1.1. Tổng quan

Bảng trò chuyện là một khung nổi trên giao diện chính, có thể kéo thả di chuyển. Người dùng nhập câu hỏi bằng ngôn ngữ tự nhiên (Ngôn ngữ), AI trả lời trong cùng khung hội thoại. Bảng này hiển thị trên mọi chức năng và tự động thay đổi nội dung gợi ý khi người dùng chuyển sang chức năng khác.

### 1.1.2. Tác nhân

- Tất cả người dùng đã đăng nhập

### 1.1.3. Biểu đồ use-case

```
Người dùng ──── Gửi câu hỏi cho AI
            ├── Nhấn gợi ý câu hỏi
            ├── Ra lệnh lọc danh sách         <<extend>> Gửi câu hỏi
            ├── Ra lệnh mở chức năng          <<extend>> Gửi câu hỏi
            ├── Xóa lịch sử trò chuyện
            └── Cài đặt kết nối AI
```

#### 1.1.3.1. Tiền điều kiện

- Người dùng đã đăng nhập vào hệ thống.
- Khóa kết nối AI (API Key) đã được thiết lập trong phần Cài đặt.

#### 1.1.3.2. Hậu điều kiện

Không thay đổi dữ liệu nghiệp vụ. Trợ lý AI chỉ đọc dữ liệu và điều khiển giao diện.

#### 1.1.3.3. Điểm kích hoạt

Người dùng nhấn nút AI trên thanh công cụ chính hoặc bảng trò chuyện đang hiển thị sẵn.

### 1.1.4. Giao diện

#### 1.1.4.1. Mô tả màn hình — Thanh tiêu đề bảng trò chuyện

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tiêu đề | Label | N/A | N/A | AI Assistant | Tên bảng trò chuyện. Đổi theo ngôn ngữ hệ thống. |
| 2 | Cài đặt | Button (icon) | N/A | N/A | N/A | Mở cửa sổ Cài đặt kết nối AI. |
| 3 | Xóa lịch sử | Button (icon) | N/A | N/A | N/A | Xóa toàn bộ nội dung hội thoại và đặt lại ngữ cảnh. |
| 4 | Đóng | Button (icon) | N/A | N/A | N/A | Ẩn bảng trò chuyện khỏi màn hình. |

#### 1.1.4.2. Mô tả màn hình — Vùng hội thoại

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Khung hội thoại | Text Area (Read-only) | N/A | Text | Blank | Hiển thị toàn bộ lịch sử trò chuyện giữa người dùng và AI. Tự cuộn xuống cuối khi có tin nhắn mới. |
| 2 | Chỉ báo xử lý | Label | N/A | Text | Blank | Hiển thị dòng chữ nhấp nháy khi AI đang xử lý câu trả lời. Ẩn khi AI trả lời xong. |

#### 1.1.4.3. Mô tả màn hình — Vùng gợi ý

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Nút gợi ý | Button (tối đa 3 nút) | N/A | N/A | N/A | Hiển thị tối đa 3 câu hỏi gợi ý phù hợp với chức năng đang mở. Nhấn vào nút sẽ tự điền câu hỏi và gửi cho AI. |

#### 1.1.4.4. Mô tả màn hình — Thanh nhập liệu

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Ô nhập câu hỏi | Text Edit | Yes | Text | Blank | Placeholder hiển thị gợi ý nhập. Hỗ trợ gửi bằng phím Enter. |
| 2 | Gửi | Button | N/A | N/A | N/A | Gửi nội dung trong ô nhập cho AI xử lý. |

### 1.1.5. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Kéo thả | Người dùng có thể nhấn giữ thanh tiêu đề để kéo bảng trò chuyện đến vị trí bất kỳ trên màn hình. |
| 2 | Nổi trên giao diện | Bảng trò chuyện luôn hiển thị phía trên các chức năng khác, không bị che khuất. |
| 3 | Đa ngôn ngữ | Tiêu đề, nút bấm, gợi ý nhập và dòng chỉ báo xử lý thay đổi theo ngôn ngữ hệ thống. |
| 4 | Xóa lịch sử | Khi nhấn nút Xóa lịch sử, toàn bộ nội dung hội thoại bị xóa và AI quên hết ngữ cảnh trước đó. |

### 1.1.6. Liên kết use-case

- Gửi câu hỏi (1.2)
- Gợi ý theo ngữ cảnh (1.3)
- Cài đặt kết nối AI (1.5)

---

## 1.2. Gửi câu hỏi

### 1.2.1. Tổng quan

Người dùng nhập câu hỏi bằng ngôn ngữ tự nhiên, AI phân tích và trả lời. Nếu câu hỏi liên quan đến dữ liệu trong hệ thống, AI tự động tra cứu rồi tổng hợp kết quả. Nếu câu hỏi yêu cầu hành động (lọc danh sách, mở chức năng), AI thực hiện lệnh tương ứng trên giao diện.

### 1.2.2. Tác nhân

- Tất cả người dùng đã đăng nhập

### 1.2.3. Luồng thao tác

#### 1.2.3.1. Tình huống 1 — Gửi câu hỏi thành công

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhập câu hỏi vào ô nhập liệu, nhấn nút Gửi hoặc phím Enter. | Kiểm tra khóa kết nối đã thiết lập chưa. |
| 2 | | Hiển thị câu hỏi trong khung hội thoại với nhãn Bạn. Hiển thị chỉ báo xử lý (AI đang xử lý...). |
| 3 | | AI phân tích câu hỏi. Nếu cần tra cứu dữ liệu, AI tự gọi chức năng phù hợp. |
| 4 | | Hiển thị câu trả lời trong khung hội thoại với nhãn AI. Tắt chỉ báo xử lý. |

#### 1.2.3.2. Tình huống 2 — Chưa có khóa kết nối

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhập câu hỏi, nhấn Gửi. | Phát hiện chưa có khóa kết nối. Hiển thị thông báo yêu cầu thiết lập. Tự mở cửa sổ Cài đặt kết nối AI. |

### 1.2.4. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Chống gửi liên tục | Hệ thống chặn gửi câu hỏi mới trong vòng 1.5 giây sau lần gửi trước để tránh trùng lặp. |
| 2 | Ô nhập trống | Nếu ô nhập không có nội dung, nút Gửi không thực hiện hành động nào. |
| 3 | Khóa nhập khi xử lý | Trong lúc AI đang xử lý, ô nhập và nút Gửi bị vô hiệu hóa. Khi AI trả lời xong, tự bật lại. |
| 4 | Định dạng hội thoại | Mỗi tin nhắn hiển thị trên dòng riêng. Tin nhắn của người dùng có ký hiệu mở đầu khác tin nhắn của AI để dễ phân biệt. |

### 1.2.5. Liên kết use-case

- Bảng trò chuyện (1.1)
- Lệnh thông minh (1.4)
- Cài đặt kết nối AI (1.5)

---

## 1.3. Gợi ý theo ngữ cảnh

### 1.3.1. Tổng quan

Khi người dùng chuyển sang một chức năng khác (ví dụ: từ Sản phẩm sang Khách hàng), bảng trò chuyện tự động cập nhật danh sách gợi ý câu hỏi phù hợp với chức năng đang mở. Tối đa 3 gợi ý hiển thị dưới dạng nút bấm nhanh.

### 1.3.2. Tác nhân

- Tất cả người dùng đã đăng nhập

### 1.3.3. Luồng thao tác

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Mở hoặc chuyển sang một chức năng trong hệ thống. | Nhận diện chức năng hiện tại. Cập nhật ngữ cảnh cho AI. Hiển thị tối đa 3 câu hỏi gợi ý dưới khung hội thoại. |
| 2 | Nhấn vào một nút gợi ý. | Tự điền nội dung gợi ý vào ô nhập và gửi câu hỏi cho AI. |

### 1.3.4. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Danh sách gợi ý | Mỗi chức năng có bộ gợi ý riêng. Ví dụ: chức năng Sản phẩm gợi ý "Liệt kê sản phẩm đang bán", chức năng Kho gợi ý "Xem tồn kho hiện tại". |
| 2 | Ẩn khi không có | Nếu chức năng hiện tại không có gợi ý nào, vùng gợi ý tự ẩn. |
| 3 | Tối đa 3 gợi ý | Hệ thống chỉ hiển thị tối đa 3 nút gợi ý để không chiếm quá nhiều diện tích. |
| 4 | Đa ngôn ngữ | Nội dung gợi ý hiển thị theo ngôn ngữ hệ thống hiện tại. |

### 1.3.5. Danh sách gợi ý theo chức năng

| Chức năng | Gợi ý mẫu |
|---|---|
| Sản phẩm | Liệt kê sản phẩm đang bán / Lọc sản phẩm loại vé / Hướng dẫn thêm sản phẩm |
| Combo | Xem danh sách combo / Kiểm tra combo nào chưa đủ tỷ lệ phân bổ |
| Kho hàng | Xem tồn kho hiện tại / Hướng dẫn tạo phiếu nhập kho |
| Khách hàng | Tìm khách hàng / Lọc khách hạng Vàng trở lên |
| Phân quyền | Hướng dẫn phân quyền / Liệt kê vai trò hiện có |
| Trang chủ | Hôm nay là ngày mấy / Hướng dẫn sử dụng hệ thống |

### 1.3.6. Liên kết use-case

- Bảng trò chuyện (1.1)
- Gửi câu hỏi (1.2)

---

## 1.4. Lệnh thông minh

### 1.4.1. Tổng quan

Ngoài việc trả lời câu hỏi, AI có khả năng thực hiện hành động trên giao diện thay người dùng. Khi nhận được yêu cầu phù hợp, AI tự động tra cứu dữ liệu từ hệ thống, lọc danh sách trên lưới, hoặc mở chức năng khác. Người dùng chỉ cần diễn đạt bằng ngôn ngữ tự nhiên mà không cần biết cách thao tác cụ thể.

### 1.4.2. Tác nhân

- Tất cả người dùng đã đăng nhập

### 1.4.3. Danh sách lệnh hỗ trợ

#### 1.4.3.1. Nhóm lệnh — Tra cứu dữ liệu

| STT | Khả năng | Phạm vi | Ví dụ câu hỏi |
|---|---|---|---|
| 1 | Xem danh sách sản phẩm | Chức năng Sản phẩm | "Liệt kê tất cả sản phẩm loại vé" |
| 2 | Xem danh sách combo | Chức năng Combo | "Có bao nhiêu combo đang hoạt động?" |
| 3 | Xem chi tiết thành phần combo | Chức năng Combo | "Combo số 5 gồm những sản phẩm nào?" |
| 4 | Xem danh sách kho hàng | Chức năng Kho | "Liệt kê các kho đang hoạt động" |
| 5 | Tìm kiếm khách hàng | Chức năng Khách hàng | "Tìm khách hàng tên Nguyễn Văn A" |
| 6 | Xem ngày giờ hiện tại | Mọi chức năng | "Hôm nay là thứ mấy?" |

#### 1.4.3.2. Nhóm lệnh — Điều khiển giao diện

| STT | Khả năng | Phạm vi | Ví dụ câu hỏi |
|---|---|---|---|
| 1 | Lọc danh sách trên lưới | Sản phẩm, Combo, Khách hàng | "Lọc sản phẩm có giá trên 200,000" |
| 2 | Mở chức năng | Mọi chức năng | "Mở quản lý kho hàng" |

#### 1.4.3.3. Nhóm lệnh — Hướng dẫn sử dụng

| STT | Khả năng | Phạm vi | Ví dụ câu hỏi |
|---|---|---|---|
| 1 | Hướng dẫn tạo đơn hàng | Mọi chức năng | "Làm sao để tạo đơn hàng mới?" |
| 2 | Hướng dẫn nhập xuất kho | Mọi chức năng | "Cách tạo phiếu nhập kho" |
| 3 | Hướng dẫn quản lý khách hàng | Mọi chức năng | "Làm sao thêm khách hàng?" |
| 4 | Hướng dẫn quản lý combo | Mọi chức năng | "Hướng dẫn tạo combo" |
| 5 | Hướng dẫn phân quyền | Mọi chức năng | "Cách phân quyền cho nhân viên" |

### 1.4.4. Luồng thao tác

#### 1.4.4.1. Tình huống 1 — Tra cứu dữ liệu

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhập câu hỏi liên quan đến dữ liệu, ví dụ: "Liệt kê sản phẩm loại vé". | AI nhận diện yêu cầu tra cứu dữ liệu. |
| 2 | | AI tự động truy vấn dữ liệu từ hệ thống với bộ lọc phù hợp. |
| 3 | | Tổng hợp kết quả (giới hạn số dòng hiển thị) và trả lời trong khung hội thoại. |

#### 1.4.4.2. Tình huống 2 — Lọc danh sách trên lưới

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhập yêu cầu lọc, ví dụ: "Lọc sản phẩm giá trên 200,000". | AI chuyển đổi yêu cầu thành điều kiện lọc. |
| 2 | | Gửi lệnh lọc về lưới dữ liệu đang hiển thị. Lưới tự cập nhật chỉ hiển thị các dòng thỏa điều kiện. |
| 3 | | AI trả lời xác nhận: đã lọc thành công. |

#### 1.4.4.3. Tình huống 3 — Mở chức năng

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhập yêu cầu mở chức năng, ví dụ: "Mở quản lý khách hàng". | AI nhận diện tên chức năng. |
| 2 | | Gửi lệnh mở chức năng tương ứng. Giao diện chuyển sang chức năng được yêu cầu. |

### 1.4.5. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Chỉ đọc dữ liệu | AI chỉ được phép tra cứu và hiển thị dữ liệu. Không được thêm, sửa hoặc xóa bất kỳ dữ liệu nghiệp vụ nào. |
| 2 | Phạm vi theo chức năng | AI chỉ tra cứu được dữ liệu liên quan đến chức năng đang mở. Ví dụ: đang ở chức năng Sản phẩm thì chỉ tra cứu được sản phẩm, không tra cứu được khách hàng. |
| 3 | Giới hạn kết quả | Khi dữ liệu trả về nhiều, AI chỉ hiển thị một số dòng đầu tiên kèm tổng số bản ghi. |
| 4 | Ngôn ngữ tự nhiên | Người dùng không cần nhớ cú pháp hay mã lệnh. Chỉ cần diễn đạt bằng Ngôn ngywx tự nhiên. |
| 5 | Danh sách chức năng có thể mở | Trang chủ, POS (Bán lẻ), Sản phẩm, Combo, Danh mục kho, Trung tâm kho, Khách hàng, Phân quyền. |

### 1.4.6. Liên kết use-case

- Gửi câu hỏi (1.2)
- Gợi ý theo ngữ cảnh (1.3)

---

## 1.5. Cài đặt kết nối AI

### 1.5.1. Tổng quan

Cửa sổ cho phép quản trị viên thiết lập khóa kết nối (API Key) và phiên bản AI sử dụng. Có chức năng kiểm tra kết nối trước khi lưu để đảm bảo khóa hợp lệ.

### 1.5.2. Tác nhân

- Quản trị viên

### 1.5.3. Giao diện

#### 1.5.3.1. Mô tả màn hình — Cửa sổ Cài đặt kết nối AI

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Khóa kết nối | Text Edit (mật khẩu) | Yes | Text | Blank | Nhập khóa kết nối AI. Ký tự hiển thị dạng dấu sao để bảo mật. |
| 2 | Phiên bản AI | Text Edit | No | Text | gemini-2.5-flash | Tên phiên bản AI sử dụng. Mặc định là phiên bản mới nhất. |
| 3 | Kiểm tra kết nối | Button | N/A | N/A | N/A | Gửi yêu cầu thử nghiệm đến dịch vụ AI để kiểm tra khóa có hoạt động không. |
| 4 | Lưu | Button | N/A | N/A | N/A | Lưu khóa kết nối và phiên bản AI vào bộ nhớ hệ thống. |
| 5 | Trạng thái | Label | N/A | Text | Blank | Hiển thị kết quả kiểm tra hoặc thông báo lưu thành công. Đổi màu theo trạng thái (xanh lá = thành công, đỏ = lỗi, xanh dương = đang kiểm tra). |

### 1.5.4. Luồng thao tác

#### 1.5.4.1. Tình huống 1 — Cài đặt thành công

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Cài đặt trên thanh tiêu đề bảng trò chuyện. | Mở cửa sổ Cài đặt kết nối AI. Hiển thị giá trị đã lưu (nếu có). |
| 2 | Nhập khóa kết nối. Tùy chọn sửa phiên bản AI. | Ghi nhận. |
| 3 | Nhấn nút Kiểm tra kết nối. | Hiển thị trạng thái "Đang kiểm tra..." (chữ xanh dương). Gửi yêu cầu thử nghiệm. Nếu thành công, hiển thị "Kết nối thành công!" (chữ xanh lá). |
| 4 | Nhấn nút Lưu. | Lưu khóa và phiên bản. Hiển thị "Đã lưu!" (chữ xanh lá). |

#### 1.5.4.2. Tình huống 2 — Khóa kết nối không hợp lệ

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhập khóa sai, nhấn Kiểm tra kết nối. | Gửi yêu cầu thử nghiệm. Nhận phản hồi lỗi. Hiển thị mã lỗi và lý do (chữ đỏ). |

#### 1.5.4.3. Tình huống 3 — Chưa nhập khóa

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Để trống ô khóa kết nối, nhấn Kiểm tra kết nối. | Hiển thị thông báo "Chưa nhập khóa kết nối" (chữ đỏ). |

### 1.5.5. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Bảo mật khóa | Khóa kết nối hiển thị dạng dấu sao, không hiện rõ ký tự. Khóa được lưu trên máy người dùng, không gửi đến máy chủ nội bộ. |
| 2 | Thời gian chờ kiểm tra | Yêu cầu kiểm tra kết nối có giới hạn 10 giây. Nếu quá thời gian, hiển thị lỗi hết thời gian chờ. |
| 3 | Vô hiệu hóa nút | Trong lúc đang kiểm tra, nút Kiểm tra kết nối bị vô hiệu hóa. Khi xong tự bật lại. |
| 4 | Đa ngôn ngữ | Tiêu đề cửa sổ, nhãn trường, nút bấm, thông báo kết quả đổi theo ngôn ngữ hệ thống. |

### 1.5.6. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Khóa kết nối bắt buộc khi kiểm tra | AI_SETTINGS_NO_KEY |
| 2 | Phiên bản AI mặc định nếu để trống | Không có thông báo, tự dùng giá trị mặc định |

### 1.5.7. Liên kết use-case

- Bảng trò chuyện (1.1)
- Gửi câu hỏi (1.2)

---

# 2. Yêu cầu khác

## 2.1. Bảng mã thông báo

| Mã thông báo | Nội dung tiếng Việt |
|---|---|
| AI_TITLE | AI Assistant |
| AI_BTN_SEND | Gửi |
| AI_INPUT_HINT | Nhập câu hỏi... |
| AI_TYPING | AI đang xử lý |
| AI_LABEL_YOU | Bạn |
| AI_LABEL_SYSTEM | Hệ thống |
| AI_ERR_NO_KEY | Chưa có khóa kết nối. Mở Cài đặt để thiết lập. |
| AI_SETTINGS_TITLE | Cài đặt kết nối AI |
| AI_SETTINGS_APIKEY | Khóa kết nối (API Key): |
| AI_SETTINGS_MODEL | Phiên bản AI: |
| AI_SETTINGS_SAVE | Lưu |
| AI_SETTINGS_TEST | Kiểm tra kết nối |
| AI_SETTINGS_SAVED | Đã lưu! |
| AI_SETTINGS_OK | Kết nối thành công! |
| AI_SETTINGS_NO_KEY | Chưa nhập khóa kết nối |
| AI_SETTINGS_TESTING | Đang kiểm tra... |
| AI_SETTINGS_ERR | Lỗi |

