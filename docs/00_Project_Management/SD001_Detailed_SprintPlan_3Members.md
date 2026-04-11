# SD001: KẾ HOẠCH PHÂN CHIA SPRINT CHI TIẾT (TEAM 3 NGƯỜI)

Kế hoạch này phân bổ công việc cho 3 thành viên trong 5 Sprint, đảm bảo tính song song và tối ưu hóa thời gian.

## 1. Định nghĩa Vai trò (Roles)

| Thành viên | Vai trò chính | Trách nhiệm chính |
| :--- | :--- | :--- |
| **Member 1 (M1)** | **Backend & DB** | Thiết kế Database, viết DAL, BUS core logic, xử lý các thuật toán phức tạp. |
| **Member 2 (M2)** | **Frontend & UI** | Thiết kế Windows Forms, Ribbon Bar, User Control và xử lý logic giao diện. |
| **Member 3 (M3)** | **QC & Support** | Viết Test Case, User Guide, hỗ trợ viết code các module phụ, sửa lỗi (Bug fix). |

---

## 2. Kế hoạch chi tiết 5 Sprint

### SPRINT 1: NỀN TẢNG & QUẢN LÝ DANH MỤC (Tổng 108h)
*Mục tiêu: Hoàn thành khung ứng dụng và các chức năng quản lý cốt lõi.*

| Thành viên | Công việc (Task) | Giờ |
| :--- | :--- | :---: |
| **M1** | Thiết kế Database Sprint 1 + Viết DAL/BUS cho Khu vực, Trò chơi, Loại vé. | 40 |
| **M2** | Thiết kế Main Form (Ribbon), giao diện Khu vực, Trò chơi, Loại vé (Master-Detail). | 44 |
| **M3** | Viết Test Case Sprint 1, chuẩn bị dữ liệu mẫu, hỗ trợ M2 validate UI. | 24 |
| **Team** | **Sửa chữa, cập nhật chức năng Sprint 1 (Fix bugs)** | 16 |

### SPRINT 2: KINH DOANH & KHÁCH HÀNG (Tổng 124h)
*Mục tiêu: Xây dựng module Bán vé - linh hồn của dự án.*

| Thành viên | Công việc (Task) | Giờ |
| :--- | :--- | :---: |
| **M1** | Xử lý Core Sales (In hóa đơn, logic trừ lượt Combo, tính thuế VAT). | 44 |
| **M2** | Giao diện Bán vé (POS), Quản lý Khách hàng, Quản lý Dịch vụ. | 44 |
| **M3** | Test Case Sprint 2, Viết User Guide S1, Hỗ trợ M1 viết DAL Khách hàng. | 36 |
| **Team** | **Sửa chữa, cập nhật chức năng Sprint 2 (Fix bugs)** | 36 |

### SPRINT 3: NHÂN SỰ & PHÂN CA (Tổng 116h)
*Mục tiêu: Quản lý con người và vận hành nội bộ.*

| Thành viên | Công việc (Task) | Giờ |
| :--- | :--- | :---: |
| **M1** | Logic kiểm tra trùng ca, BUS Nhân viên, phân quyền truy cập. | 40 |
| **M2** | Giao diện Quản lý Nhân viên, Form Phân ca làm việc chi tiết. | 40 |
| **M3** | Test logic Phân ca (Edge cases), Viết User Guide S2, Hỗ trợ M2 thiết kế Form. | 36 |
| **Team** | **Sửa chữa, cập nhật chức năng Sprint 3 (Fix bugs)** | 36 |

### SPRINT 4: MARKETING & KỸ THUẬT (Tổng 104h)
*Mục tiêu: Tối ưu doanh thu và đảm bảo an toàn thiết bị.*

| Thành viên | Công việc (Task) | Giờ |
| :--- | :--- | :---: |
| **M1** | Logic áp dụng Giá vé theo Sự kiện, mã Voucher/Khuyến mãi. | 36 |
| **M2** | Giao diện Quản lý Sự kiện, Khuyến mãi, Quản lý Bảo trì thiết bị. | 36 |
| **M3** | Kiểm thử logic khuyến mãi, Viết User Guide S3, Quản lý Danh mục Bảo trì. | 32 |
| **Team** | **Sửa chữa, cập nhật chức năng Sprint 4 (Fix bugs)** | 36 |

### SPRINT 5: BÁO CÁO & TRIỂN KHAI (Tổng 64h + 24h Deploy)
*Mục tiêu: Hoàn thiện báo cáo, tối ưu hiệu năng và bàn giao.*

| Thành viên | Công việc (Task) | Giờ |
| :--- | :--- | :---: |
| **M1** | Xây dựng các View/Stored Procedures cho báo cáo doanh thu, triển khai Server. | 40 |
| **M2** | Thiết kế Report viewer (Crystal Reports/RDLC), cài đặt máy trạm (Client). | 24 |
| **M3** | Tài liệu bàn giao cuối cùng (Final User Guide), Video bộ demo, Test S5. | 24 |
| **M1+M2** | **Triển khai hệ thống LAN (Client-Server)** | 24 |

---

## 3. Rủi ro và Đối soát (Risk & Verification)

> [!WARNING]
> **Nút thắt cổ chai (Bottleneck)**: M1 (Backend) cần hoàn thành DAL/BUS sớm để M2 có thể gán dữ liệu vào UI.

### Phương pháp kiểm chứng:
1. **Daily Stand-up**: Mỗi sáng họp 15p kiểm tra task của ngày hôm trước.
2. **Review Sprint**: Cuối mỗi Sprint, cả team Demo sản phẩm cho khách để lấy feedback.
3. **Burndown Chart**: Theo dõi giờ làm còn lại trong Excel để điều chỉnh nhân lực kịp thời.
