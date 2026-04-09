# Khảo sát nghiệp vụ: Khách sạn Đại Nam

## 1. Thông tin chung
- **Vị trí**: 1765A Đại Lộ Bình Dương, Phường Hiệp An, TP Thủ Dầu Một, Bình Dương.
- **Quy mô**: > 300 phòng.
- **Xếp hạng**: 3 sao.
- **Đặc trưng**: Phục vụ chủ yếu khách tham quan KDL Đại Nam.

## 2. Danh mục loại phòng thực tế
| Loại phòng | Sức chứa | Diện tích | Giá tham khảo | Đặc điểm |
| :--- | :--- | :--- | :--- | :--- |
| **Superior** | 2 người | 25 m² | 480k - 600k | Tiêu chuẩn, view khuôn viên |
| **Deluxe** | 2 người | 50 m² | 640k - 1tr | Rộng rãi, đầy đủ tiện nghi |
| **Family** | 4 người | 50 m² | 980k - 1.2tr | 2 phòng ngủ riêng biệt |
| **VIP** | 2 người | 50 m² | 1.8tr | Sang trọng, dịch vụ cao cấp |
| **Villa** | 6 người | 154 m² | 2.15tr | 3 PN, phòng khách, bếp |

## 3. Chính sách kinh doanh cốt lõi (Dynamic Pricing)
### Ý nghĩa của chính sách
- **Không có bảng giá "theo giờ" riêng biệt** (Để đơn giản hóa vận hành).
- **Khách thuê ngắn hạn (<= 4 giờ)**: Tính **50%** giá phòng trọn ngày (Gói Nghỉ trưa - Day use).
- **Phù hợp thực tế**: Khách Đại Nam chủ yếu thuê nghỉ trưa, tránh nóng, không qua đêm.

### Quy chuẩn vận hành Resort (Block-based)
- **Gói Nghỉ trưa (Day-use)**: Thường là 2h hoặc 4h. Giá cố định (Ví dụ: 300k - 500k).
- **Gói Qua đêm (Overnight)**: Tính nguyên ngày (Ví dụ: 600k - 1tr).
- **Quy tắc "Burned Room"**: Nếu khách ở **quá 4 tiếng**, phòng đó coi như đã bị "đốt" (burned). Lễ tân không thể bán cho khách qua đêm (thường nhận phòng 14h). Do đó, khách ở lố giờ sẽ bị tính **trọn giá nguyên ngày**.

> [!IMPORTANT]
> **Logic hệ thống**: Nếu `duration <= 4h` -> Lấy giá `NghiTrua`. Nếu `duration > 4h` -> Lấy giá `TheoNgay` * số ngày (Math.Ceiling).

## 4. Phân khu vận hành
### A. Khách sạn Thành Đại Nam (Khu truyền thống)
- Kiến trúc Cổ Loa & Cung đình Huế.
- Tập trung các hạng phòng lẻ: Superior, Deluxe, Family, VIP.
- Đối tượng: Khách lẻ, cặp đôi, gia đình nhỏ.

### B. Khu Biệt Thự Biển (Khu cao cấp)
- Vị trí: Cạnh Biển nhân tạo.
- Quy mô: ~28 căn Villa biệt lập.
- **Cơ chế linh hoạt**:
    - Cho thuê **nguyên căn** (Đại gia đình).
    - Cho thuê **từng phòng nhỏ** trong Villa (Phòng 1, 2, 3) với giá linh hoạt.

---
*Tài liệu này dùng làm căn cứ thiết kế Logic tính giá và Quản lý sơ đồ phòng trong hệ thống.*
