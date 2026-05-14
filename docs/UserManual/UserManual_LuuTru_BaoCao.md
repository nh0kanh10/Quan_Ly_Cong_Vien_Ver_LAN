# HƯỚNG DẪN SỬ DỤNG HỆ THỐNG ĐẠI NAM POS
## PHÂN HỆ: QUẢN LÝ LƯU TRÚ & BÁO CÁO DOANH THU

---

### 1. PHÂN HỆ QUẢN LÝ LƯU TRÚ (HOTEL MANAGEMENT)

Giao diện sơ đồ phòng là trái tim của hệ thống khách sạn, giúp bạn bao quát toàn bộ trạng thái phòng nghỉ theo thời gian thực.

#### 1.1. Hiểu về các trạng thái phòng
Mỗi ô phòng trên sơ đồ (Tile View) được phân biệt bằng màu sắc giúp bạn nhận diện nhanh:
- **Màu Trắng:** Phòng đang Trống, sẵn sàng đón khách.
- **Màu Xanh dương:** Phòng đang có khách lưu trú.
- **Màu Cam:** Phòng chờ dọn (khách vừa trả phòng).
- **Màu Đỏ:** Phòng đang bảo trì (không thể kinh doanh).

#### 1.2. Thao tác Nhận phòng (Check-in)
1. Trên sơ đồ, nhấn **Chuột phải** vào phòng trống (màu trắng).
2. Chọn **Nhận phòng nhanh**.
3. Tại hộp thoại hiện ra, nhập các thông tin:
   - **Số điện thoại:** Nếu là khách cũ, hệ thống sẽ tự điền tên.
   - **Họ tên khách:** Nhập đầy đủ họ tên.
   - **Tiền ký quỹ (nếu có):** Số tiền khách đặt cọc trước.
4. Nhấn **Xác nhận**. Ô phòng sẽ chuyển sang màu xanh.

#### 1.3. Ghi nhận dịch vụ Minibar & Phụ thu
Trong quá trình khách ở, nếu khách dùng đồ uống trong tủ lạnh hoặc phát sinh phí khác:
- **Minibar:** Chuột phải vào phòng đang ở > Chọn **Dùng Minibar** > Nhập số lượng sản phẩm khách dùng > **Xác nhận**.
- **Phụ thu:** Chuột phải > Chọn **Phụ thu đền bù** > Nhập số tiền và nội dung (VD: Giặt ủi, đền bù đồ vỡ) > **Xác nhận**.
*Lưu ý: Các khoản này sẽ tự động cộng dồn vào hóa đơn cuối cùng.*

#### 1.4. Đổi phòng cho khách
1. Chuột phải vào phòng khách đang ở > Chọn **Đổi phòng**.
2. Chọn **Phòng chuyển đến** trong danh sách các phòng đang trống.
3. Nhập **Lý do đổi phòng** (Bắt buộc).
4. Nhấn **Xác nhận**. Toàn bộ tiền phòng và dịch vụ sẽ được kết chuyển sang phòng mới.

#### 1.5. Thanh toán & Trả phòng (Check-out)
1. Chuột phải vào phòng > Chọn **Trả phòng (Thanh toán)**.
2. Kiểm tra lại bảng kê: Tiền phòng (tính tự động theo giờ/ngày), Minibar, Phụ thu, và Tiền cọc.
3. Chọn **Phương thức thanh toán** (Tiền mặt, Thẻ, Chuyển khoản, hoặc Ví điện tử Đại Nam).
4. Nhấn **Quyết toán & In hóa đơn**.
5. Sau khi thanh toán, phòng chuyển sang màu Cam (Chờ dọn). Khi dọn xong, chuột phải chọn **Xác nhận dọn xong** để đưa phòng về trạng thái Trống (màu trắng).

---

### 2. PHÂN HỆ BÁO CÁO DOANH THU (REPORTING)

Cung cấp các công cụ phân tích số liệu kinh doanh đa chiều dành cho Kế toán và Quản lý.

#### 2.1. Truy vấn báo cáo tổng hợp
1. Chọn tab **Báo cáo** trên thanh menu chính.
2. Chọn khoảng thời gian tại ô **Từ ngày** và **Đến ngày**.
3. Nhấn nút **Làm mới** (hoặc biểu tượng xoay).
4. Hệ thống sẽ liệt kê toàn bộ giao dịch đã thanh toán trong kỳ. Tổng doanh thu sẽ hiển thị nổi bật ở góc dưới.

#### 2.2. Phân tích dữ liệu đa chiều (Pivot Grid)
Đây là tính năng mạnh mẽ nhất cho phép bạn tự dựng báo cáo theo ý muốn bằng cách kéo thả:
- **Xem doanh thu theo Thu ngân:** Kéo cột "Thu ngân" thả vào vùng Dòng (Rows).
- **Xem doanh thu theo Dịch vụ:** Kéo cột "Loại giao dịch" thả vào vùng Cột (Columns).
- **Lọc nhanh:** Nhấn vào biểu tượng phễu trên mỗi đầu cột để lọc riêng một khách hàng hoặc một ngày cụ thể.

#### 2.3. Xuất dữ liệu ra Excel
Để phục vụ việc lưu trữ hoặc in ấn báo cáo giấy:
1. Sau khi đã dàn trang báo cáo ưng ý trên màn hình.
2. Nhấn nút **Xuất Excel** trên thanh công cụ.
3. Chọn vị trí lưu file trên máy tính và đặt tên file.
4. Nhấn **Save**. Hệ thống sẽ xuất file Excel giữ nguyên định dạng các nhóm và tổng tiền bạn đang xem.

---

### 3. CÁC LƯU Ý QUAN TRỌNG

- **Quy tắc làm tròn:** Hệ thống tự động làm tròn tiền về đơn vị 500đ hoặc 1.000đ theo cấu hình tài chính của công ty.
- **An toàn dữ liệu:** Luôn nhấn **Làm mới** trước khi xem báo cáo để đảm bảo số liệu là mới nhất từ máy chủ.
- **Hỗ trợ:** Nếu gặp lỗi "Mất kết nối cơ sở dữ liệu", vui lòng kiểm tra dây mạng LAN hoặc liên hệ phòng IT (Máy lẻ: 101).

---
*Tài liệu soạn thảo bởi Đội ngũ phát triển Đại Nam POS - Phiên bản 1.0*
