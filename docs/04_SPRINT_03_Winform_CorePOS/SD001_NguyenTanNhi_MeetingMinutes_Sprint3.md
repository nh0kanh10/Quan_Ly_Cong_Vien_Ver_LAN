# ÁP DỤNG MỤC TIÊU: CÁC BIÊN BẢN HỌP VỚI KHÁCH HÀNG (SPRINT 3)

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_MOM_Sprint3_v3.0  
**Nhóm**: NguyenTanNhi  
**Đề tài**: Hệ thống Quản lý Công viên Giải trí (Phân hệ POS WinForms)  
**Khách hàng**: KDL Đại Nam  

*(Tài liệu này đính kèm các biên bản họp làm rõ yêu cầu dự án, demo triển khai hệ thống và các yêu cầu cập nhật chỉnh sửa giao diện theo đúng quy định báo cáo đồ án).*

---

## BIÊN BẢN HỌP #6: SPRINT 3 PLANNING (Làm rõ yêu cầu đầu Sprint)

| Hạng mục | Chi tiết |
|----------|---------|
| **Ngày họp** | 01/04/2026 |
| **Địa điểm** | TP. Hồ Chí Minh |
| **Thành phần tham dự** | Nguyên (Lead), Tấn (Dev), Nhi (QA), **Đại diện KDL Đại Nam (PO)** |
| **Chủ trì** | Nguyên |
| **Thư ký** | Nhi |

### 1. Mục đích cuộc họp
- Làm rõ yêu cầu dự án cho giai đoạn cao điểm: Định hướng phát triển tiếp tục cho Sprint 3.
- Chốt lại yêu cầu cập nhật chỉnh sửa từ khách hàng (CR-01: Chỉnh sửa giao diện POS tối ưu thao tác bằng phím tắt cơ học (Mouse-less), CR-02: Bổ sung chức năng Loyalty Points).
- Lên kế hoạch phân công triển khai phân hệ cốt lõi: Bán Hàng (POS), Kiểm Soát Vé tại Cổng (Gate), Quản lý Thuê Đồ (Rental).

### 2. Nội dung thảo luận (Có sự tham gia của Khách hàng)

#### 2.1. Thống nhất Yêu cầu Cập nhật Giao diện & Chức năng
- Khách hàng yêu cầu: **Giao diện POS phải hỗ trợ vận hành tốc độ cao.** Các nút bấm thiết kế to, nổi bật và toàn bộ luồng phải hỗ trợ phím tắt cứng (F1-F12) để thu ngân không cần tốn thời gian di chuyển chuột (Tính năng Mouse-less Operation cực kì cần thiết trong mùa lễ tết).
- Khách hàng đặc biệt nhấn mạnh: Phải chấm dứt việc in thẻ giấy nhiệt, **triển khai 100% vé điện tử** hiện popup ngay trên màn hình thu ngân để khách tự lấy điện thoại chụp lại.
- Team tiếp nhận chức năng: Thống nhất áp dụng Computer Vision cho Webcam đọc mã vạch chống tràn bộ nhớ đệm (Buffer Override).

#### 2.2. Phân công công việc kĩ thuật nội bộ

| Công việc | Người phụ trách | Deadline |
|-----------|----------------|----------|
| Code Thuật toán giá vé & Core Database | Nguyên | 05/04/2026 |
| Xây dựng UI/UX Phím Tắt, Nhúng Webcam & RFID | Tấn | 09/04/2026 |
| Báo cáo lỗi & Stress-test hệ thống | Nhi | 11/04/2026 |
| Cập nhật Document & Hướng dẫn | Nhi | 12/04/2026 |

### 3. Ký xác nhận
*(Có chữ ký xác nhận của team và khách hàng)*

- **Chủ trì (Lead)**: Nguyên (Đã ký)
- **Developer**: Tấn (Đã ký)
- **QA/Doc**: Nhi (Đã ký)
- **Đại diện Khách hàng**: ____________ (Đại diện KDL Đại Nam)

---

## BIÊN BẢN HỌP #7: SPRINT 3 DEMO TRIỂN KHAI HỆ THỐNG

| Hạng mục | Chi tiết |
|----------|---------|
| **Ngày họp** | 12/04/2026 |
| **Địa điểm** | TP. Hồ Chí Minh |
| **Thành phần tham dự** | Nguyên (Lead), Tấn (Dev), Nhi (QA), **Đại diện KDL Đại Nam (PO)** |
| **Chủ trì** | Nguyên |
| **Thư ký** | Tấn |

### 1. Mục đích cuộc họp
- Demo triển khai hệ thống Vận Hành Trực Tiếp (POS, Gate, Trạm Thuê Đồ) trên phần cứng thật (Đầu gõ phím RFID, Scanner).
- Thu thập đánh giá, nghiệm thu thực tế với Khách Hàng.

### 2. Kết quả Demo & Phản hồi Khách hàng

> **Phản hồi từ PO Đại Nam (Khách Hàng) sau khi kết thúc Demo:**
> - 🔥 **Vượt mong đợi về Tốc Độ Quét**: Cực kì ấn tượng với tốc độ quét của trạm Kiểm soát cổng. Hiệu ứng đồ họa đỏ/xanh rõ ràng, tích hợp thành công loa cảnh báo trực quan.
> - 🔥 **Đảm bảo yêu cầu cập nhật giao diện (CR-01)**: Toàn bộ layout đã liên kết mượt mà với sự kiện bàn phím (Form_KeyDown). Phương pháp vé điện tử hiển thị mã QR trên màn hình được khách hàng khen ngợi nồng nhiệt vì tính thực tiễn kinh tế cao.
> - 🔥 **Xử lý trơn tru quy trình phức tạp**: Tính năng Nhận Trả lưới đôi của phần Thuê Đồ trừ tiền đặt cọc/tiền phạt tự động chạy rất tốt, giảm thiểu 90% thời gian nhân viên thao tác.

### 3. Ký xác nhận Bàn giao Demo
*(Có chữ ký xác nhận của team và khách hàng)*

- **Chủ trì (Lead)**: Nguyên (Đã ký)
- **Developer**: Tấn (Đã ký)
- **QA/Doc**: Nhi (Đã ký)
- **Đại diện Khách hàng**: ____________ (KDL Đại Nam xác nhận Demo)

---

## BIÊN BẢN HỌP #8: LÀM RÕ YÊU CẦU MỞ RỘNG (GIAI ĐOẠN 2)

| Hạng mục | Chi tiết |
|----------|---------|
| **Ngày họp** | 12/04/2026 (Diễn ra ngay sau buổi Demo) |
| **Địa điểm** | TP. Hồ Chí Minh |
| **Thành phần tham dự** | Nguyên (Lead), **Đại diện KDL Đại Nam (PO)** |

### 1. Mục đích cuộc họp
- Bàn luận về yêu cầu cập nhật phần mềm theo định hướng tương lai sau khi đã hoàn thiện mảng Core POS tại công viên.
- Phân tích yêu cầu Business-to-Consumer (B2C) trước mùa cao điểm.

### 2. Yêu cầu Cập nhật / Chức năng (Thay đổi chiến lược đồ án)
- **Khách hàng phát biểu:** "Module vận hành nội bộ công viên (WinForms) hiện tại rất xuất sắc vì chạy phần cứng ngoại vi mượt. Tuy nhiên để giảm tải lượng hành khách tụ tập tại quầy mua vé, chúng tôi có **yêu cầu bổ sung thêm hệ thống Web B2C** để khách tự túc thanh toán."
- **Yêu cầu cụ thể:** Xây dựng cổng mua vé Web Application Booking cho khách mua trực tuyến từ nhà. Mọi mã vé ảo mua trên Web phải đồng bộ về cơ sở dữ liệu để dùng được tại các cổng quét Webcam (Gate WinForms) mới làm xong ở Sprint này.

### 3. Kết luận
- Team dev xác nhận Yêu cầu cập nhật lớn này hoàn toàn xử lý được nhờ cơ sở dữ liệu đã chuẩn hóa.
- Khách hàng đồng ý mở Epic chiến lược Web B2C ứng dụng giải pháp Online Ticket cho Sprint 4 sắp tới.

### 4. Ký xác nhận Change Request
*(Có chữ ký xác nhận của team và khách hàng)*

- **Đại diện Team**: Nguyên (Đã ký)
- **Đại diện Khách hàng**: ____________ (KDL Đại Nam xác nhận yêu cầu)
