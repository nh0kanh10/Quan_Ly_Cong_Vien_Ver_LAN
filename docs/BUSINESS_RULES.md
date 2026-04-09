# MASTER BUSINESS RULES: Dai Nam Amusement Park & Resort

Dài Nam không chỉ là Công viên, mà là một **Hệ sinh thái du lịch**. Mọi logic trong code phải tuân thủ các quy tắc nghiệp vụ thực tế sau đây để đảm bảo tính chuyên nghiệp khi bảo vệ đồ án.

---

## 🕒 1. THỜI GIAN VẬN HÀNH (OPERATING HOURS)
Hệ thống phải tự động kiểm tra giờ đóng/mở cửa khi khách đặt vé hoặc đặt phòng.
- **Ngày thường (Thứ 2 - Thứ 6)**: 08:00 - 17:00.
- **Cuối tuần (Thứ 7 - CN)**: 08:00 - 18:00.
- **Siêu thị 24h & Khách sạn**: Hoạt động 24/24.

---

## 🏛️ 2. LƯU TRÚ (HOTEL - YIELD MANAGEMENT)
Chiến lược giá "Burned Room" để tối ưu công suất phòng.
- **Giá Block (Nghỉ trưa)**: Áp dụng cho khách ở **<= 4 giờ**. Giá = 50% giá ngày.
- **Giá Theo ngày**: Ở > 4 giờ tính nguyên 1 ngày.
- **Check-out muộn**: Quá 12:00 trưa hôm sau (nếu không có thỏa thuận) sẽ tính thêm 1 ngày.
- **Hạng phòng Thực tế**: Superior, Deluxe, Family, VIP, Villa.

---

## 🎫 3. VÉ TRÒ CHƠI & DỊCH VỤ (TOKEN SYSTEM)
Đại Nam bán theo "Số lượng trải nghiệm" thay vì bán lẻ từng trò.
- **Combo 3 trò**: 120.000đ.
- **Combo 6 trò**: 200.000đ (Tiết kiệm 40k).
- **Combo 12 trò**: 400.000đ.
- **Cơ chế kỹ thuật**: Hệ thống sinh ra N mã Token (GUID). Mỗi lần quẹt tại cổng trò chơi sẽ trừ 1 Token.

---

## 🚲 4. PHƯƠNG TIỆN DI CHUYỂN (DECREASING PRICE)
Khuyến khích thuê dài hạn bằng cơ chế giá giảm dần.
| Loại xe | Giờ đầu | Giờ thứ hai | Giờ thứ ba trở đi |
| :--- | :--- | :--- | :--- |
| Xe điện (4-7 chỗ) | 300.000đ | 200.000đ | 100.000đ |
| Xe lửa | 600.000đ | 500.000đ | 400.000đ |

---

## 👶 5. CHÍNH SÁCH GIÁ THEO ĐỐI TƯỢNG (HEIGHT-BASED)
Kiểm soát tại cổng bằng thước đo chiều cao (Nhanh & Chính xác hơn độ tuổi).
- **Dưới 1m**: Miễn phí.
- **1m - 1.4m**: Giá trẻ em (Thường là 50-60% người lớn).
- **Trên 1.4m**: Tính như Người lớn.

---

## 📦 6. TOUR TRỌN GÓI (INTEGRATED PACKAGES)
Bán "Kỳ nghỉ" thay vì bán lẻ dịch vụ.
- **Combo 2N1Đ**: Bao gồm [Phòng KS] + [Ăn sáng] + [Vé cổng/Biển/Vườn thú].
- **Hạch toán**: Dùng `TyLePhanBo` trong bảng `ComboChiTiet` để chia doanh thu về từng bộ phận (Hotel, F&B, Ticket).

---
> [!IMPORTANT]
> **Ghi chú cho Lập trình viên**: Tuyệt đối không hardcode giá vào UI. Mọi thông số trên phải được cấu hình trong bảng `BangGia` hoặc `ThamSoHeThong`.
