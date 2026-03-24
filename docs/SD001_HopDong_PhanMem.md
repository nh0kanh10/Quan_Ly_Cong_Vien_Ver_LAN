# HỢP ĐỒNG PHÁT TRIỂN PHẦN MỀM
## (Software Development Agreement)

**Số hợp đồng:** HĐ-SD001/2026  
**Ngày ký:** 02/03/2026  

---

## ĐIỀU 1: CÁC BÊN THAM GIA

### BÊN A — KHÁCH HÀNG (Bên đặt hàng)
| Hạng mục | Chi tiết |
|:---------|:--------|
| **Tên đơn vị** | Khu Du lịch Đại Nam — Bình Dương |
| **Địa chỉ** | Đại Nam, Thủ Dầu Một, Bình Dương |
| **Đại diện** | Ông/Bà Nguyễn Văn Đại (Trưởng phòng CNTT) |
| **Chức vụ** | Product Owner (PO) |
| **Điện thoại** | 0274.xxx.xxx |
| **Email** | po.dainam@example.com |

### BÊN B — NHÀ PHÁT TRIỂN (Bên thực hiện)
| Hạng mục | Chi tiết |
|:---------|:--------|
| **Tên nhóm** | Nhóm NguyenTanNhi — Lớp SD001 |
| **Trường** | [Tên trường Đại học] |
| **Trưởng nhóm** | Nguyên (Team Lead / Scrum Master) |
| **Thành viên** | Tấn (Senior Developer), Nhi (QA/Tester + Documentation) |
| **Giảng viên hướng dẫn** | Thầy [Tên thầy] (Project Supervisor) |

---

## ĐIỀU 2: NỘI DUNG HỢP ĐỒNG

### 2.1 Tên dự án
**Hệ thống Quản Lý Khu Du lịch Đại Nam** — Phần mềm quản lý vận hành nội bộ tích hợp bán hàng trực tuyến (Mô hình O2O: Online-to-Offline)

### 2.2 Phạm vi công việc (Scope of Work)

Bên B cam kết xây dựng và bàn giao cho Bên A hệ thống phần mềm bao gồm:

#### Phase 1 — Nền tảng (Sprint 1-2):
- Ứng dụng Desktop WinForms chạy trên mạng nội bộ (LAN)
- CRUD quản lý danh mục: Khu vực, Trò chơi, Loại vé, Sản phẩm, Dịch vụ
- Quản lý Nhân viên (hình ảnh, chức vụ, khu vực trực)
- Quản lý Khách hàng (SĐT, tích điểm cơ bản)
- Hệ thống đăng nhập / đăng ký tài khoản
- Cấu hình kết nối LAN Server-Client

#### Phase 2 — Mở rộng nghiệp vụ Đại Nam (Sprint 3+):
(*Bổ sung sau khi Bên A cung cấp yêu cầu chi tiết về nghiệp vụ thực tế*)
- Khách sạn: Đặt phòng, Sơ đồ phòng, Check-in/Check-out
- Nhà hàng: Đặt bàn, Quản lý menu F&B, Sơ đồ bàn
- Khu Biển nhân tạo: Quản lý chất lượng nước, Vé bơi
- Vườn thú: Quản lý động vật, Khu chuồng, Lịch chăm sóc
- Ví điện tử RFID: Nạp tiền, Thanh toán, Lịch sử giao dịch
- Bãi xe: OCR biển số, Barcode, Tính phí tự động
- Thuê đồ: Phao, Tủ, Chòi nghỉ, Cọc qua Ví
- Kho hàng: Nhập/Xuất/Kiểm kho, Thẻ kho bất biến
- POS bán vé: Bán hàng cảm ứng, Giỏ hàng, Vé QR, Dynamic Pricing
- Hệ thống tích điểm Loyalty chi tiết
- RBAC: Phân quyền theo vai trò (5 vai trò × 36 quyền)
- Web B2C: Website đặt vé online (Blazor WebAssembly)
- DevOps: Tự động hóa triển khai (run.bat, deploy.bat)

### 2.3 Nền tảng công nghệ

| Thành phần | Công nghệ |
|:-----------|:----------|
| Ngôn ngữ | C# (.NET Framework 4.7.2 + .NET 8) |
| Desktop | Windows Forms + Guna.UI2 + DevExpress |
| Database | SQL Server 2019+ Express |
| Web B2C | Blazor WebAssembly |
| API | Kestrel C# RESTful |
| Cloud | Vercel (Web) + Ngrok (Tunnel) |
| VCS | Git + GitHub |

---

## ĐIỀU 3: TIẾN ĐỘ THỰC HIỆN

### 3.1 Phương pháp phát triển
Áp dụng mô hình **Agile/Scrum** với Sprint 2 tuần.

### 3.2 Timeline

| Giai đoạn | Sprint | Thời gian | Sản phẩm bàn giao |
|:----------|:------:|:----------|:-------------------|
| Phase 1 | Sprint 1 | 04/03 → 17/03/2026 | DB v1 (23 bảng generic) + 7 form CRUD danh mục |
| Phase 1 | Sprint 2 | 18/03 → 31/03/2026 | +9 form vận hành (NV, KH, BanHang, Vé, Ví, BaoTri, BangGia, CaLam, SuKien) + KH Đại Nam phản hồi |
| Phase 2 | Sprint 3 | 01/04 → 14/04/2026 | Tái cấu trúc DB + Nghiệp vụ Đại Nam + UI POS |
| Phase 2 | Sprint 4 | 15/04 → 28/04/2026 | Web B2C + Security + Forms bổ sung |
| Phase 2 | Sprint 5 | 29/04 → 12/05/2026 | DevOps + Seed data + Tài liệu + Bàn giao |

### 3.3 Điểm mốc quan trọng (Milestones)

| Mốc | Ngày | Sự kiện |
|:----|:-----|:--------|
| M1 | 02/03/2026 | Ký hợp đồng |
| M2 | 17/03/2026 | Demo Sprint 1 — Chấp nhận nền tảng |
| M3 | 31/03/2026 | Demo Sprint 2 — PO phản hồi yêu cầu nghiệp vụ Đại Nam |
| M4 | 14/04/2026 | Demo Sprint 3 — Chấp nhận hệ thống mới + UI POS |
| M5 | 28/04/2026 | Demo Sprint 4 — Chấp nhận Web B2C |
| **M6** | **12/05/2026** | **Bàn giao chính thức — Nghiệm thu + Thanh lý HĐ** |

---

## ĐIỀU 4: GIÁ TRỊ HỢP ĐỒNG VÀ THANH TOÁN

### 4.1 Tổng giá trị
| Hạng mục | Giá trị (VNĐ) |
|:---------|:--------------|
| Phase 1: Nền tảng (Sprint 1-2) | 15.000.000 |
| Phase 2: Nghiệp vụ Đại Nam (Sprint 3-5) | 35.000.000 |
| **Tổng cộng** | **50.000.000** |

*(Bằng chữ: Năm mươi triệu đồng chẵn)*

> **Ghi chú:** Đây là mô phỏng học tập. Giá trị hợp đồng chỉ mang tính minh họa.

### 4.2 Điều kiện thanh toán
| Đợt | Thời điểm | Tỷ lệ | Số tiền |
|:---:|:----------|:-----:|:--------|
| 1 | Ký hợp đồng (M1) | 30% | 15.000.000 |
| 2 | Nghiệm thu Phase 1 (M3) | 20% | 10.000.000 |
| 3 | Nghiệm thu Phase 2 (M5) | 30% | 15.000.000 |
| 4 | Bàn giao + Thanh lý (M6) | 20% | 10.000.000 |

---

## ĐIỀU 5: QUYỀN VÀ NGHĨA VỤ CÁC BÊN

### 5.1 Quyền và nghĩa vụ Bên A (Đại Nam)
1. Cung cấp **yêu cầu nghiệp vụ chi tiết** cho từng module theo tiến độ Sprint
2. Cử đại diện tham gia **Sprint Review** mỗi 2 tuần để duyệt sản phẩm
3. Phản hồi Change Request trong vòng **3 ngày làm việc**
4. Cung cấp môi trường triển khai thực tế (máy chủ LAN, máy POS, máy in vé)
5. Thanh toán đúng hạn theo Điều 4
6. Có quyền yêu cầu thay đổi phạm vi qua **Change Request** chính thức

### 5.2 Quyền và nghĩa vụ Bên B (Nhóm phát triển)
1. Phát triển phần mềm đúng SRS đã được Bên A duyệt
2. Báo cáo tiến độ hàng tuần qua email/họp
3. Bàn giao **mã nguồn + tài liệu kỹ thuật + hướng dẫn vận hành**
4. Hỗ trợ kỹ thuật miễn phí trong **30 ngày** sau nghiệm thu
5. Bảo mật thông tin dữ liệu kinh doanh của Bên A
6. Không chia sẻ mã nguồn cho bên thứ ba nếu không có sự đồng ý bằng văn bản

### 5.3 Quyền và nghĩa vụ Giảng viên hướng dẫn (Thầy)
1. Giám sát tiến độ và chất lượng kỹ thuật
2. Tham dự các buổi Sprint Review khi cần thiết
3. Phê duyệt các Architecture Decision Records (ADR)
4. Đánh giá điểm số cuối cùng theo rubric môn học

---

## ĐIỀU 6: BẢO HÀNH VÀ BẢO TRÌ

| Hạng mục | Chi tiết |
|:---------|:--------|
| **Thời gian bảo hành** | 30 ngày kể từ ngày nghiệm thu (M6) |
| **Phạm vi bảo hành** | Fix bug trong chức năng đã nghiệm thu |
| **Không bảo hành** | Lỗi do phần cứng, mạng, hoặc thay đổi yêu cầu mới |
| **Thời gian phản hồi** | Bug Critical: 24h, Bug Major: 48h, Bug Minor: 5 ngày |

---

## ĐIỀU 7: THAY ĐỔI PHẠM VI (Change Management)

1. Mọi thay đổi phạm vi phải thông qua **Change Request** bằng văn bản
2. Change Request phải có đánh giá **Impact Analysis** (Tác động tiến độ + chi phí)
3. Bên A và Bên B phải ký duyệt Change Request trước khi thực hiện
4. Change Request phê duyệt được ghi nhận trong Sprint Backlog tương ứng

---

## ĐIỀU 8: BẢO MẬT THÔNG TIN

1. Bên B cam kết **không tiết lộ** dữ liệu kinh doanh, doanh thu, thông tin khách hàng của Đại Nam
2. Mã nguồn thuộc sở hữu Bên A sau khi thanh toán đầy đủ
3. Bên B được phép sử dụng dự án cho mục đích **học tập và portfolio** cá nhân, nhưng phải ẩn dữ liệu thực
4. Database seed data chỉ chứa **dữ liệu mô phỏng**, không dùng dữ liệu thực

---

## ĐIỀU 9: ĐIỀU KHOẢN CHUNG

1. Hợp đồng có hiệu lực từ ngày ký đến khi hoàn thành nghiệm thu và thanh lý
2. Trường hợp bất khả kháng (dịch bệnh, thiên tai): Hai bên thương lượng gia hạn
3. Tranh chấp (nếu có) giải quyết qua thương lượng → Hòa giải → Trọng tài
4. Hợp đồng được lập thành **02 bản** có giá trị pháp lý như nhau

---

## ĐIỀU 10: CHỮ KÝ

| | **BÊN A** (Khách hàng) | **BÊN B** (Nhà phát triển) | **GIÁM SÁT** |
|:--------:|:-----------------------|:---------------------------|:-------------|
| **Đại diện** | Ông/Bà Nguyễn Văn Đại | Nguyên (Trưởng nhóm) | Thầy [Tên thầy] |
| **Chức vụ** | Trưởng phòng CNTT Đại Nam | Team Lead / Scrum Master | Giảng viên hướng dẫn |
| **Chữ ký** | __________________ | __________________ | __________________ |
| **Ngày ký** | 02/03/2026 | 02/03/2026 | 02/03/2026 |

---

## PHỤ LỤC

### Phụ lục A: Danh sách sản phẩm bàn giao cuối cùng

| # | Sản phẩm | Định dạng |
|:-:|:---------|:----------|
| 1 | Mã nguồn (Source Code) | Git Repository (.zip + GitHub) |
| 2 | Database Script | `Database_DaiNam.sql` + `insert.sql` |
| 3 | SRS Sprint 1 + 2 + 3 | Markdown / PDF |
| 4 | Test Cases (≥400 TCs/Sprint) | Markdown / Excel |
| 5 | Defect List (≥40 lỗi/Sprint) | Markdown / Excel |
| 6 | User Guide + Ảnh thực | Markdown / PDF |
| 7 | Biên bản họp (có chữ ký) | Markdown / PDF |
| 8 | Architecture Decision Records | Markdown |
| 9 | Deploy Scripts (run.bat, deploy.bat) | Batch / PowerShell |
| 10 | Video demo (nếu có) | MP4 |

### Phụ lục B: Quy trình Change Request

```
Bên A đề xuất CR → Bên B đánh giá Impact → Hai bên ký duyệt → Cập nhật Backlog → Thực hiện
```
