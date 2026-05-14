# Khu Du Lịch Đại Nam — Product Backlog
# Mã dự án: DN01 | Phiên bản: 1.2
# Bắt đầu: 04/03/2026 | 5 Sprint × 2 tuần

---

## Lịch sử thay đổi

| Ngày | Nội dung | Người cập nhật |
|---|---|---|
| 04/03/2026 | Khởi tạo Product Backlog lần đầu | Bùi Trí Nguyên |
| 24/04/2026 | Cân bằng lại Story Point các Sprint và bổ sung Tra cứu hóa đơn POS | Bùi Trí Nguyên |

---

## Kiến trúc 3 nền tảng

```
┌─────────────────────┐   ┌──────────────────────┐   ┌──────────────────────┐
│   WinForms Desktop  │   │    Web (Khách hàng)   │   │  Android (Nhân viên) │
│   Quản lý nội bộ    │   │   Self-service Portal │   │    Staff Mobile App  │
├─────────────────────┤   ├──────────────────────┤   ├──────────────────────┤
│ • Quản lý sản phẩm  │   │ • Đặt vé online      │   │ • Quẹt vé cổng       │
│ • Quản lý combo     │   │ • Đặt món ăn         │   │ • Nhận đơn → Nấu     │
│ • Quản lý khách     │   │ • Đặt phòng KS       │   │ • Xác nhận dọn phòng │
│ • POS bán hàng      │   │ • Xem QR vé đã mua   │   │                      │
│ • Kho hàng          │   │ • Theo dõi đơn        │   │                      │
│ • Dashboard         │   │                      │   │                      │
│ • Báo cáo           │   │                      │   │                      │
│ • AI Assistant      │   │                      │   │                      │
└─────────────────────┘   └──────────────────────┘   └──────────────────────┘
         ↑                           ↑                          ↑
         └───────────────────────────┴──────────────────────────┘
                              SQL Server (Shared DB)
                              REST API (ASP.NET Core)
```

---

## Lịch Sprint

| Sprint | Thời gian | Nền tảng | Sprint Goal | Tổng SP |
|---|---|---|---|---|
| Sprint 1 | 04/03 – 17/03/2026 | WinForms | Nền tảng: Phân quyền, Danh mục Sản phẩm, Combo, i18n | 29 SP |
| Sprint 2 | 18/03 – 31/03/2026 | WinForms | Khách hàng, Dashboard, Thuê đồ, Kho hàng | 31 SP |
| Sprint 3 | 01/04 – 14/04/2026 | WinForms | frmPOS (Bán hàng + Tra cứu HĐ + Hoàn tiền), Khuyến mãi, Nhà hàng | 29 SP |
| Sprint 4 | 15/04 – 28/04/2026 | WinForms + API | Khách sạn, AI Assistant, Báo cáo, REST API Core | 32 SP |
| Sprint 5 | 29/04 – 12/05/2026 | Web + App | Customer Portal (Web) và Staff App (Android) | 31 SP |

---

## Bảng Product Backlog

> **Story Point:** 1 = rất nhỏ | 2 = nhỏ | 3 = trung bình | 5 = lớn | 8 = rất lớn

---

### 🖥️ SPRINT 1 — WinForms: Nền tảng (04/03 – 17/03)

| ID | User Story | Chức năng | Priority | SP | Status |
|---|---|---|---|---|---|
| TASK-00 | **[Task Hạ tầng]** Xây dựng frmMain Shell: Đăng nhập (xác thực tài khoản), Điều hướng module, Đa ngôn ngữ (VI/EN/ZH) — hạ tầng dùng chung toàn hệ thống, không tính là chức năng nghiệp vụ | frmMain + frmLogin + i18n | High | 8 | ✅ Done |
| US-01 | Là Quản trị viên, tôi muốn phân quyền theo vai trò (Quản lý / Kế toán / Thu ngân) để mỗi người chỉ truy cập đúng chức năng được phép | CN02 – Phân quyền | High | 5 | ✅ Done |
| US-02 | Là Quản lý, tôi muốn thêm/sửa/xóa sản phẩm kèm bảng giá đa mức, cấu hình vé (cổng xoay, đối tượng) và định mức BOM cho F&B để quản lý toàn bộ danh mục hàng hoá | CN09 – Quản lý Sản phẩm (SP + Vé + BOM) | High | 8 | ✅ Done |
| US-03 | Là Quản lý, tôi muốn tạo combo sản phẩm kèm tỷ lệ phân bổ doanh thu để bán gói trọn | CN06 – Quản lý Combo | High | 8 | ✅ Done |

**Tổng Sprint 1: 29 SP**

---

### 🖥️ SPRINT 2 — WinForms: Khách hàng & Kho (18/03 – 31/03)

| ID | User Story | Chức năng | Priority | SP | Status |
|---|---|---|---|---|---|
| US-08 | Là Nhân viên, tôi muốn thêm/sửa/xóa hồ sơ khách hàng kèm lịch sử giao dịch để chăm sóc khách VIP | CN04 – Quản lý Khách hàng | High | 5 | ✅ Done |
| US-09 | Là Quản lý, tôi muốn xem Dashboard tổng quan (doanh thu hôm nay, khách, SP bán chạy) để nắm nhanh tình hình | CN10 – Dashboard | High | 5 | ✅ Done |
| US-10 | Là Nhân viên, tôi muốn lập phiếu cho thuê đồ (tủ đồ, chòi) theo block giờ và thu tiền khi trả để vận hành dịch vụ thuê | CN11 – Thuê đồ | High | 5 | ✅ Done |
| US-11 | Là Thủ kho, tôi muốn nhập kho nguyên liệu theo lô (có HSD) và xem tồn kho realtime để theo dõi hàng tồn | CN08 – Trung tâm Kho | High | 8 | ✅ Done |
| US-12 | Là Thủ kho, tôi muốn hệ thống tự động trừ tồn kho theo BOM mỗi khi POS thanh toán F&B, để tôi không phải nhập phiếu xuất kho bằng tay | CN08 – Auto xuất kho | Medium | 8 | ✅ Done |

**Tổng Sprint 2: 31 SP**

---

### 🖥️ SPRINT 3 — WinForms: POS & Nhà hàng (01/04 – 14/04)

| ID | User Story | Chức năng | Priority | SP | Status |
|---|---|---|---|---|---|
| US-14 | Là Thu ngân, tôi muốn tạo đơn hàng POS (tìm SP / quét mã), thanh toán đa phương thức (tiền mặt / QR) và in hóa đơn tại quầy để phục vụ khách nhanh nhất | CN05 – Bán hàng POS | High | 8 | ✅ Done |
| US-15 | Là Thu ngân, tôi muốn áp dụng mã khuyến mãi / voucher và bán theo combo để hệ thống tự tính giảm giá và phân bổ doanh thu đúng chính sách | CN07 – Khuyến mãi POS | Medium | 5 | ✅ Done |
| US-16 | Là Thu ngân, tôi muốn **tra cứu lại hóa đơn** ngay trong màn hình POS (bằng mã HĐ hoặc ngày) để in lại biên lai hoặc thực hiện hoàn trả hàng cho khách | CN05 – Tra cứu hóa đơn & Hoàn hàng | High | 8 | ✅ Done |
| US-17 | Là Quản lý nhà hàng, tôi muốn quản lý bàn (trạng thái: trống/có khách) và tạo phiếu gọi món tại bàn để phục vụ khách ăn uống | CN12 – Nhà hàng (Bàn + Gọi món) | High | 8 | ✅ Done |

**Tổng Sprint 3: 29 SP**

---

### 🖥️ SPRINT 4 — WinForms + API: Khách sạn, AI, Báo cáo (15/04 – 28/04)

| ID | User Story | Chức năng | Priority | SP | Status |
|---|---|---|---|---|---|
| US-18 | Là Lễ tân, tôi muốn đặt phòng, check-in, check-out và quản lý trạng thái phòng để vận hành khu lưu trú | CN13 – Khách sạn | High | 8 | ✅ Done |
| US-19 | Là Nhân viên, tôi muốn hỏi AI Assistant về doanh thu, sản phẩm, khách hàng bằng ngôn ngữ tự nhiên để tra cứu nhanh | CN14 – AI Assistant | Medium | 8 | ✅ Done |
| US-20 | Là Quản lý, tôi muốn xem báo cáo doanh thu (ngày/tuần/tháng), sản phẩm bán chạy và xuất Excel/PDF để phân tích kinh doanh | CN15 – Báo cáo | High | 8 | ✅ Done |
| TASK-01 | **[Task Kỹ thuật]** Xây dựng REST API (ASP.NET Core) cho Vé, Đơn món, Phòng để Web và Android gọi vào | API Core – REST Endpoints | High | 8 | ✅ Done |

**Tổng Sprint 4: 32 SP**

---

### 🌐📱 SPRINT 5 — Web Portal + Android Staff App (29/04 – 12/05)

| ID | User Story | Nền tảng | Chức năng | Priority | SP | Status |
|---|---|---|---|---|---|---|
| US-21 | Là Khách hàng, tôi muốn đặt vé vào khu online, thanh toán và nhận QR code để vào cổng không xếp hàng | Web | WEB – Đặt vé online | High | 8 | 🔲 Todo |
| US-22 | Là Khách hàng, tôi muốn xem thực đơn và đặt món trước khi đến để tiết kiệm thời gian chờ | Web | WEB – Đặt món ăn | High | 5 | 🔲 Todo |
| US-23 | Là Khách hàng, tôi muốn tìm và đặt phòng khách sạn, xem ảnh và giá theo ngày để lên kế hoạch | Web | WEB – Đặt phòng KS | High | 8 | 🔲 Todo |
| US-24 | Là Nhân viên cổng, tôi muốn dùng điện thoại Android quét QR vé của khách để xác thực nhanh và ghi log | Android | APP – Quẹt vé cổng | High | 5 | 🔲 Todo |
| US-25 | Là Đầu bếp, tôi muốn nhận thông báo đơn món mới trên điện thoại và đánh dấu đã nấu xong để phục vụ đúng bàn | Android | APP – Nhận đơn & Nấu | High | 3 | 🔲 Todo |
| US-26 | Là Nhân viên dọn phòng, tôi muốn xem danh sách phòng cần dọn và xác nhận hoàn thành để cập nhật trạng thái realtime | Android | APP – Xác nhận dọn phòng | High | 2 | 🔲 Todo |

**Tổng Sprint 5: 31 SP**

---

## Tổng kết Product Backlog

| Chỉ số | Giá trị |
|---|---|
| Tổng User Story | 21 US + 2 Task kỹ thuật |
| Tổng Story Points | 152 SP |
| Sprint 1–4 (Done) | 15 US + 2 Task / 121 SP |
| Sprint 5 (Todo) | 6 US / 31 SP |
| Velocity trung bình S1-S5 | ~30.4 SP/sprint |

---

## Phân công thành viên

| Thành viên | Vai trò chính | Sprint 1 | Sprint 2 | Sprint 3 | Sprint 4 | Sprint 5 |
|---|---|---|---|---|---|---|
| Bùi Trí Nguyên | Team Lead / Full-stack | US-02 | US-11 | US-14 | TASK-01, US-20 | US-21, US-24 |
| [Thành viên 2] | Dev / BA | TASK-00, US-01 | US-08, US-09 | US-15, US-16 | US-18 | US-23, US-26 |
| [Thành viên 3] | Dev / Tester | US-03 | US-10, US-12 | US-17 | US-19 | US-22, US-25 |
