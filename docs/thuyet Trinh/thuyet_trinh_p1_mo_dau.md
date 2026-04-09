# THUYẾT TRÌNH — PHẦN 1: MỞ ĐẦU & KIẾN TRÚC
## Thời gian: ~3 phút đầu

---

## 1.1 Giới thiệu đề tài (0:00 — 1:00)

> Chào thầy/cô và các bạn. Nhóm em làm đề tài **"Quản lý Khu du lịch Đại Nam"**.

> Nói ngắn gọn thì Đại Nam ở Bình Dương, diện tích khoảng 450 hecta. Nó có rất nhiều mảng: biển nhân tạo, vườn thú, trường đua ngựa, khách sạn, nhà hàng, trò chơi. Mỗi ngày đón hàng chục ngàn khách.

> **Vấn đề là gì?** Một nơi lớn như vậy mà quản lý thủ công thì gặp mấy chuyện:
> - Bán vé giấy → khách screenshot QR gửi cho bạn bè → lọt cổng miễn phí
> - Kiosk hết hàng mà kho không biết
> - 2 lễ tân cùng đặt 1 phòng cho 2 khách khác nhau → cãi nhau
> - Cuối ngày kế toán ngồi cộng Excel → sai số

> Nên nhóm em làm phần mềm để **quản lý tập trung** tất cả: bán vé, khách sạn, nhà hàng, cho thuê đồ, gửi xe, kho hàng, ví điện tử RFID.

---

## 1.2 Công nghệ sử dụng (1:00 — 1:30)

| Thành phần | Công nghệ | Lý do chọn |
|------------|-----------|------------|
| Database | SQL Server | Môn học yêu cầu, hỗ trợ Transaction tốt |
| Backend | C# .NET Framework 4.7.2 | WinForms chạy Desktop, dễ debug |
| UI Controls | DevExpress + Guna.UI2 | Grid mạnh, control đẹp sẵn |
| OCR biển số | Tesseract | Miễn phí, chạy offline |
| Quét QR | ZXing.Net | Thư viện mã nguồn mở |
| Kiến trúc | 3-tier (DAL → BUS → GUI) | Tách rời trách nhiệm |

> Không có gì đặc biệt lắm, toàn thư viện phổ biến. Điểm chính nằm ở thiết kế database và luồng nghiệp vụ.

---

## 1.3 Kiến trúc 3-tier (1:30 — 2:30)

> Hệ thống chia 3 lớp:

```
GUI (WinForms)  ←── Chỉ lo hiển thị, bắt sự kiện click
     │
     ↓
BUS (Business)  ←── Xử lý nghiệp vụ: tính giá, check tồn kho, validate
     │
     ↓
DAL (Data Access) ←── Gọi SQL Server, trả DataTable/Entity lên BUS
```

> **Tại sao tách 3 lớp?** Thật ra là vì môn học yêu cầu. Nhưng nó cũng có lợi thật: nếu sau này đổi từ WinForms sang Web, chỉ viết lại GUI, còn BUS và DAL giữ nguyên.

> Mỗi module có 1 class BUS, dùng Singleton Pattern. Ví dụ: `BUS_DonHang.Instance.TaoDonHang(...)`. BUS gọi DAL, DAL gọi SQL. GUI không bao giờ gọi thẳng DAL.

---

## 1.4 Cấu trúc thư mục (2:30 — 3:00)

> Dự án có 15 thư mục module trong GUI:

| # | Thư mục | Chức năng |
|---|---------|-----------|
| 1 | Auth | Đăng nhập |
| 2 | POS | Bán vé, bán hàng tại quầy |
| 3 | Ticket | Quét vé QR/RFID |
| 4 | Hotel | Đặt phòng khách sạn |
| 5 | Restaurant | Đặt bàn, gọi món |
| 6 | Parking | Gửi xe, đọc biển số OCR |
| 7 | Rental | Cho thuê phao, tủ đồ, xe điện |
| 8 | Wallet_RFID | Ví điện tử, nạp tiền, thẻ RFID |
| 9 | Inventory | Nhập kho, xuất kho, kiểm kê |
| 10 | Catalog | Sản phẩm, bảng giá, combo |
| 11 | Customer | Khách hàng, đoàn khách |
| 12 | Staff | Nhân viên, ca làm việc |
| 13 | Finance | Phiếu thu, phiếu chi |
| 14 | Marketing | Sự kiện, khuyến mãi |
| 15 | Maintenance | Bảo trì thiết bị |

> Ngoài ra có thư mục `Controls` chứa `ThemeManager` — class quản lý giao diện chung cho toàn bộ app (màu sắc, font chữ, style grid).

---

## 1.5 Tổng quan Database (3:00 — 3:30)

> Database tên **DaiNamResort**, gồm:
> - **56 bảng** (chia 7 nhóm chính)
> - **5 Views** (báo cáo doanh thu, tồn kho, dashboard)
> - **2 Triggers** (audit đơn hàng, kiểm tra combo)
> - **1 Stored Procedure** (truy vấn chi tiết đơn hàng tổng hợp)
> - **90+ Foreign Keys**
> - **30+ Indexes** (bao gồm Filtered Index cho Soft Delete)

> Phần tiếp theo em sẽ đi chi tiết từng nhóm bảng.
