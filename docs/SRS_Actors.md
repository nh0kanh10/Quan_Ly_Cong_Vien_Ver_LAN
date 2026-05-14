# Bảng xác định các Tác nhân (Actors)

Dưới đây là danh sách các tác nhân tương tác với hệ thống quản lý tổng thể khu du lịch Đại Nam:

| STT | Tác nhân | Mô tả | Nhiệm vụ chính |
|:---:|:---|:---|:---|
| 1 | **Nhân viên Bán hàng**<br/>(Sales / F&B Staff) | Người trực tiếp thực hiện giao dịch tại các quầy POS (vé, quà tặng) và khu vực nhà hàng. | - Chọn sản phẩm, món ăn, combo.<br/>- Nạp tiền ví RFID.<br/>- Thực hiện thanh toán và in hóa đơn. |
| 2 | **Nhân viên Lễ tân**<br/>(Receptionist) | Phụ trách quản lý khu vực khách sạn và lưu trú của khách hàng. | - Tiếp nhận đặt phòng.<br/>- Thực hiện thủ tục Check-in/Check-out.<br/>- Quản lý trạng thái và vệ sinh phòng. |
| 3 | **Nhân viên Kiểm soát**<br/>(Validator) | Nhân viên trực tại các cổng vào khu vui chơi, sở thú hoặc các trò chơi đặc thù. | - Quét vé hoặc vòng tay RFID của khách.<br/>- Kiểm tra tính hợp lệ và xác nhận quyền vào cổng. |
| 4 | **Nhân viên Kho**<br/>(Storekeeper) | Người quản lý kho hàng, vật tư và nguyên liệu chế biến. | - Nhập kho từ nhà cung cấp.<br/>- Xuất kho nội bộ cho các quầy bán hàng/bếp.<br/>- Kiểm kê tồn kho định kỳ. |
| 5 | **Nhân viên Vận hành**<br/>(Operator) | Đội ngũ quản lý các khu vực chuyên biệt như Sở thú, Trường đua, Trò chơi. | - Cập nhật trạng thái sức khỏe động vật.<br/>- Theo dõi tình trạng vận hành thiết bị.<br/>- Báo cáo sự cố cần bảo trì. |
| 6 | **Kế toán**<br/>(Accountant) | Người quản lý dòng tiền và các chỉ số tài chính của hệ thống. | - Đối soát doanh thu ví RFID và tiền mặt.<br/>- Quản lý công nợ nhà cung cấp.<br/>- Lập báo cáo tài chính tổng hợp. |
| 7 | **Quản lý**<br/>(Manager / Admin) | Người điều hành có quyền hạn cao nhất trên hệ thống phần mềm. | - Thiết lập danh mục sản phẩm, bảng giá.<br/>- Cấu hình khuyến mãi và phân quyền.<br/>- Theo dõi báo cáo quản trị tổng thể. |
| 8 | **Thiết bị đọc RFID**<br/>(RFID Reader) | Tác nhân thiết bị (Hardware Actor) kết nối trực tiếp với phần mềm. | - Đọc mã định danh từ chip trên vòng tay khách.<br/>- Gửi dữ liệu mã thẻ vào hệ thống để truy cứu. |

---
*Ghi chú: Danh sách trên bao quát toàn bộ quy trình từ bán hàng, vận hành đến quản trị cấp cao.*
