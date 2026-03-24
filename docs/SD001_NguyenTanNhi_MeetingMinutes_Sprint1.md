# BIÊN BẢN CUỘC HỌP – Sprint 1

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_MOM_Sprint1_v1.0  
**Nhóm**: NguyenTanNhi  
**Đề tài**: Hệ thống Quản lý Công viên Vui chơi Giải trí  

---

## BIÊN BẢN HỌP #1: SPRINT PLANNING

| Hạng mục | Chi tiết |
|----------|---------|
| **Ngày họp** | 04/03/2026 |
| **Địa điểm** | TP. Hồ Chí Minh |
| **Thành phần tham dự** | [Thành viên 1], [Thành viên 2], [Thành viên 3] |
| **Chủ trì** | [Thành viên 1] |
| **Thư ký** | [Thành viên 2] |

### 1. Mục đích cuộc họp
- Phân tích yêu cầu Sprint 1
- Phân công công việc
- Thống nhất timeline

### 2. Nội dung thảo luận

#### 2.1. Xác định yêu cầu nghiệp vụ:
- Hệ thống cần phục vụ việc quản lý công viên giải trí theo mô hình tập trung, chạy trên mạng nội bộ (LAN).
- Đảm bảo tính nhất quán dữ liệu giữa các máy trạm (quầy vé) và máy chủ (Server Database).
- Quy trình bán vé phải nhanh chóng, hỗ trợ nhiều loại đối tượng (Người lớn, Trẻ em) và các gói vé combo.
- Quản lý vận hành khu vực và trò chơi để đảm bảo an toàn và hiệu suất (theo dõi trạng thái đóng/mở/bảo trì).

#### 2.2. Xác định rõ những chức năng cần phát triển:
- Phân hệ Quản lý danh mục: Khu vực, Trò chơi, Loại vé, Dịch vụ.
- Phân hệ Kinh doanh: Bán vé tại quầy, quản lý hóa đơn, tích điểm khách hàng.
- Phân hệ Nhân sự: Quản lý nhân viên, lịch phân ca làm việc tại các vị trí trò chơi.
- Phân hệ Marketing: Quản lý sự kiện và các chương trình khuyến mãi/voucher.
- Phân hệ Kỹ thuật: Theo dõi lịch bảo trì thiết bị và báo cáo sửa chữa.

#### 2.3. Xác định Database:
- Sử dụng SQL Server làm hệ quản trị CSDL trung tâm.
- Thống nhất cơ chế PK sử dụng Identity (Tự tăng) và bổ sung mã Code (MaCode) tự định nghĩa để dễ truy vấn (VD: A01, G001, T01).
- Các bảng dữ liệu chính trong Sprint 1 gồm: `KhuVuc`, `TroChoi`, `TicketTypes` (Loại vé), `Users` (hoặc `NhanVien` để đăng nhập).
- Thiết kế các ràng buộc (Constraints) về giá vé (>=0), sức chứa, và quan hệ khóa ngoại (FK) giữa Trò chơi - Khu vực.

#### 2.4. Xác định Phạm vi Sprint 1:
Nhóm thống nhất Sprint 1 sẽ phát triển 4 nhóm chức năng chính:
1. **Quản lý Loại Vé** (CRUD, quản lý combo vé con).
2. **Quản lý Khu Vực** (CRUD, kiểm tra ràng buộc trò chơi).
3. **Quản lý Trò Chơi** (CRUD, lọc theo khu vực).
4. **Các chức năng phụ trợ hệ thống khác**: Màn hình chính (Main Form Ribbon), Đăng nhập, Đăng xuất, Cài đặt kết nối LAN.

#### 2.5. Kiến trúc hệ thống
- Mô hình **3 lớp**: DAL → BUS → GUI
- Database: **SQL Server** (kết nối qua mạng LAN)
- Giao diện: **Windows Forms** + Guna.UI2 + FontAwesome.Sharp
- Layout: **Master-Detail** (Grid bên trái, Form nhập liệu bên phải)

#### 2.6. Phân công công việc

| Công việc | Người phụ trách | Deadline |
|-----------|----------------|----------|
| Viết SRS Sprint 1 | [Thành viên 1] | 06/03/2026 |
| Thiết kế Database | [Thành viên 1] | 07/03/2026 |
| Thiết kế UI Mockups | [Thành viên 2] | 08/03/2026 |
| Code DAL + BUS | [Thành viên 1] | 12/03/2026 |
| Code GUI | [Thành viên 2] | 14/03/2026 |
| Test + Fix bug | [Thành viên 3] | 15/03/2026 |
| Viết tài liệu | [Thành viên 3] | 16/03/2026 |

### 3. Kết luận
- Sprint 1 kéo dài **2 tuần** (04/03 – 16/03/2026)
- Ưu tiên hoàn thành 3 form quản lý chính
- Review code mỗi 2 ngày

### 4. Ký xác nhận

| Vai trò | Họ tên | Chữ ký |
|---------|--------|--------|
| Chủ trì | [Thành viên 1] | |
| Thành viên | [Thành viên 2] | |
| Thành viên | [Thành viên 3] | |

---

## BIÊN BẢN HỌP #2: SPRINT REVIEW / RETROSPECTIVE

| Hạng mục | Chi tiết |
|----------|---------|
| **Ngày họp** | 16/03/2026 |
| **Địa điểm** | TP. Hồ Chí Minh |
| **Thành phần tham dự** | [Thành viên 1], [Thành viên 2], [Thành viên 3] |
| **Chủ trì** | [Thành viên 1] |
| **Thư ký** | [Thành viên 3] |

### 1. Mục đích cuộc họp
- Review kết quả Sprint 1
- Đánh giá chất lượng sản phẩm
- Rút kinh nghiệm (Retrospective)

### 2. Kết quả Sprint 1

#### 2.1. Chức năng đã hoàn thành

| # | Chức năng | Trạng thái | Ghi chú |
|---|----------|:----------:|---------|
| 1 | Quản lý Loại Vé (CRUD + Combo) | ✅ Hoàn thành | Bao gồm vé combo chi tiết |
| 2 | Quản lý Khu Vực (CRUD) | ✅ Hoàn thành | Có ràng buộc xóa |
| 3 | Quản lý Trò Chơi (CRUD) | ✅ Hoàn thành | Lọc theo khu vực + loại |
| 4 | Đăng nhập / Đăng ký | ✅ Hoàn thành | Bổ sung thêm |
| 5 | Quản lý Tài khoản | ✅ Hoàn thành | Bổ sung thêm |
| 6 | Cấu hình kết nối LAN | ✅ Hoàn thành | Lưu connection string |

#### 2.2. Kết quả kiểm thử
- **Tổng Test Case**: 478 (KhuVuc 105, TroChoi 108, LoaiVe+Combo 112, DichVu 103, Login 32, Config 18)
- **Defect phát hiện**: 45 (8 Critical, 14 Major, 13 Medium, 10 Minor)
- Các defect đều đã được ghi nhận trong Defect List

### 3. Retrospective

#### 3.1. Những điều làm tốt (What went well)
- Hoàn thành đúng deadline
- Giao diện thiết kế đồng nhất, professional
- Kiến trúc 3 lớp rõ ràng, dễ mở rộng

#### 3.2. Những điều cần cải thiện (What to improve)
- Validate chưa đầy đủ so với SRS (12 defects)
- Trạng thái ComboBox chưa khớp SRS
- Cần review code kỹ hơn trước khi test

#### 3.3. Hành động cải tiến (Action items)
| # | Action | Người phụ trách | Deadline |
|---|--------|----------------|----------|
| 1 | Fix 12 defects đã phát hiện | [Thành viên 1] | Sprint 2 |
| 2 | Đồng bộ giá trị ComboBox với SRS | [Thành viên 2] | Sprint 2 |
| 3 | Thêm unit test cho BUS layer | [Thành viên 3] | Sprint 2 |

### 4. Ký xác nhận

| Vai trò | Họ tên | Chữ ký |
|---------|--------|--------|
| Chủ trì | [Thành viên 1] | |
| Thành viên | [Thành viên 2] | |
| Thành viên | [Thành viên 3] | |
