# 📋 JIRA PRODUCT BACKLOG — MASTER VIEW
## SD001 — Hệ thống Quản Lý Khu Vui Chơi Giải Trí Đại Nam

**Mã dự án:** SD001  
**Product Owner:** Đại diện KDL Đại Nam  
**Scrum Master:** Nguyên (Trưởng nhóm)  
**Team:** Nguyên (Lead Dev), Tấn (Frontend), Nhi (QA/Doc)  
**Sprint Length:** 2 tuần  
**Start Date:** 04/03/2026  
**Velocity:** ~15-20 Story Points / Sprint  

---

## EPIC REGISTRY

| Epic ID | Epic Name | Description | Priority |
|:--------|:----------|:------------|:--------:|
| EPIC-01 | Nền Tảng & Danh Mục | DB, Auth, CRUD master data | 🔴 Highest |
| EPIC-02 | Dịch Vụ Giải Trí | KS, NH, Thuê đồ, Gửi xe, Biển, Thú | 🟠 High |
| EPIC-03 | Bán Hàng POS | Bán vé, Giỏ hàng, Thanh toán, Vé điện tử | 🔴 Highest |
| EPIC-04 | Tài Chính & Ví | Ví RFID, PhieuThu/Chi, Loyalty | 🟠 High |
| EPIC-05 | Kho Vận | Nhập/Xuất/Kiểm kho, Thẻ kho, NCC | 🟡 Medium |
| EPIC-06 | Nhân Sự & Vận Hành | NV, Lịch ca, Bảo trì, Sự cố, RBAC | 🟡 Medium |
| EPIC-07 | Tái Cấu Trúc (Refactoring) | DB Migration, Gateway Pattern, MVP | 🔴 Highest |
| EPIC-08 | Web B2C & DevOps | Kestrel API, Blazor WASM, CI/CD | 🟠 High |

---

## 🚀 SPRINT 1: Xây Dựng Nền Tảng — DB Generic + CRUD Danh Mục
**Thời gian:** 04/03/2026 → 17/03/2026 | **Goal:** Thiết kế DB v1 generic, dựng kiến trúc WinForms 3 lớp, code CRUD danh mục cơ bản.

> [!NOTE]
> Team **chưa khảo sát kỹ nghiệp vụ thực tế của KDL Đại Nam** → thiết kế DB v1 (`dbCu.sql` — 23 bảng)
> theo hướng **"công viên giải trí chung"** (generic), chỉ quản lý: Khu vực, Trò chơi, Loại vé, Combo, Dịch vụ F&B.
> DB đã có sẵn bảng ViDienTu, VeDaPhatHanh, BangGia... nhưng **chưa có form tương ứng**.

| Ticket ID | Type | Summary | Story Pts | Assignee | Status | Epic |
|:----------|:-----|:--------|:---------:|:---------|:------:|:-----|
| SD-001 | 📋 Task | Thiết kế Database v1 (`dbCu.sql`) — 23 bảng generic (TaiKhoan, KhuVuc, TroChoi, LoaiVe, ChiTietCombo, DichVu, DanhMucDichVu, NhanVien, CaLam, PhanCa, KhachHang, SuKien, KhuyenMai, HoaDon, ChiTietHoaDon_Ve, ChiTietHoaDon_DichVu, BaoTri, VeDaPhatHanh, LichSuQuetVe, QuyenLoiVe, BangGia, ViDienTu, LichSuGiaoDichVi + CauHinhHeThong) | 5 | Nguyên | ✅ Done | EPIC-01 |
| SD-002 | 📖 Story | Thiết lập kiến trúc 3 lớp (DAL → BUS → GUI) với pattern `DAL_*.Instance` singleton | 3 | Nguyên | ✅ Done | EPIC-01 |
| SD-003 | 📖 Story | [CRUD] Quản lý Khu Vực — Thêm/Sửa/Xóa/Tìm kiếm (bảng `KhuVuc`) | 3 | Tấn | ✅ Done | EPIC-01 |
| SD-004 | 📖 Story | [CRUD] Quản lý Trò Chơi — Master-Detail với Khu vực cha (bảng `TroChoi`) | 3 | Tấn | ✅ Done | EPIC-01 |
| SD-005 | 📖 Story | [CRUD] Quản lý Loại Vé + Combo vé — Vé đơn + Vé combo cha-con (bảng `LoaiVe`, `ChiTietCombo`) | 3 | Tấn | ✅ Done | EPIC-01 |
| SD-006 | 📖 Story | [CRUD] Quản lý Dịch vụ F&B — Sản phẩm theo danh mục + khu vực (bảng `DichVu`, `DanhMucDichVu`). Lưu ý: DonViTinh là **cột NVARCHAR** trong `DichVu`, không phải bảng riêng | 3 | Nhi | ✅ Done | EPIC-01 |
| SD-007 | 📖 Story | Form Login + Đăng ký tài khoản (bảng `TaiKhoan` — chỉ 2 vai trò: Admin/NhanVien) | 2 | Nguyên | ✅ Done | EPIC-01 |
| SD-008 | 📖 Story | Form cấu hình kết nối LAN (`frmConfigConnect`) — lưu connection string vào file | 1 | Nguyên | ✅ Done | EPIC-01 |
| SD-009 | 📋 Task | Viết SRS Sprint 1 + Test Case (478 TC: KhuVuc 105, TroChoi 108, LoaiVe+Combo 112, DichVu 103, Login 32, Config 18) + Defect List | 2 | Nhi | ✅ Done | — |
| **Sprint Total** | | | **25** | | | |

### Sprint 1 Review
- ✅ 9/9 items Done — Hoàn thành nền tảng + 7 form CRUD
- 45 Defects phát hiện (8 Critical, 14 Major, 13 Medium, 10 Minor) → Xem `SD001_DefectList_Sprint1.md`
- **KH Đại Nam chưa tham gia Sprint 1 Review** — team tự phát triển theo hướng generic
- **Retrospective:** DB v1 thiết kế theo hướng "công viên giải trí chung", chưa biết nghiệp vụ cụ thể của Đại Nam (có khách sạn, vườn thú, biển nhân tạo, nhà hàng...).
- **Technical Debt ghi nhận:** DAL dùng `Instance` singleton → coupling.

---

## 🚀 SPRINT 2: Vận Hành Cơ Bản — NV, KH, Bán Hàng, Vé, Ví
**Thời gian:** 18/03/2026 → 31/03/2026 | **Goal:** Code các module vận hành còn lại trong dbCu.sql: Nhân viên, Khách hàng, Bán hàng, Vé, Ví, Bảo trì. KH Đại Nam lần đầu tham dự Review → phản hồi gay gắt.

> [!WARNING]
> **Điểm ngoặt Sprint 2 Review:** KH Đại Nam **lần đầu tham dự** và phản hồi:
> *"Phần mềm làm chung chung cho mọi công viên giải trí. Không khớp nghiệp vụ thực tế của Đại Nam.*
> *Chúng tôi có khách sạn (200+ phòng), vườn thú (100+ loài), biển nhân tạo, nhà hàng — cần quản lý riêng từng module."*
> DB v1 (`dbCu.sql`) hoàn toàn **thiếu** các bảng: Phong, LoaiPhong, DatPhong, Ban, DatBan, NhaCungCap, PhieuKho, TheKho, DoanKhach, VaiTro, QuyenHan, DongVat, SuCo...
> → Quyết định: **Sprint 3 = Tái cấu trúc toàn diện + bổ sung nghiệp vụ Đại Nam.**

| Ticket ID | Type | Summary | Story Pts | Assignee | Status | Epic |
|:----------|:-----|:--------|:---------:|:---------|:------:|:-----|
| SD-010 | 📖 Story | [CRUD] Quản lý Nhân viên — Hồ sơ + Hình ảnh + Chức vụ + Khu vực trực (bảng `NhanVien` đã có trong dbCu) | 3 | Tấn | ✅ Done | EPIC-06 |
| SD-011 | 📖 Story | [CRUD] Quản lý Khách hàng — SĐT unique + Điểm tích lũy + Tổng chi tiêu (bảng `KhachHang` đã có trong dbCu) | 3 | Tấn | ✅ Done | EPIC-01 |
| SD-012 | 📖 Story | [POS] Bán hàng cơ bản — Tạo HoaDon + ChiTietHoaDon_Ve/DichVu + Chọn KH + Thanh toán (bảng `HoaDon`, `ChiTietHoaDon_*` đã có trong dbCu) | 5 | Nguyên | ✅ Done | EPIC-03 |
| SD-013 | 📖 Story | [Module] Sự kiện + Khuyến mãi — CRUD + Gắn KM vào HoaDon (bảng `SuKien`, `KhuyenMai` đã có trong dbCu) | 3 | Nhi | ✅ Done | EPIC-01 |
| SD-014 | 📖 Story | [Module] Vé phát hành — Sinh QR + Quét vé cổng + Lịch sử quét (bảng `VeDaPhatHanh`, `LichSuQuetVe`, `QuyenLoiVe` đã có trong dbCu) | 5 | Nguyên | ✅ Done | EPIC-03 |
| SD-015 | 📖 Story | [Module] Ví điện tử RFID cơ bản — Cấp thẻ + Nạp tiền + Giao dịch (bảng `ViDienTu`, `LichSuGiaoDichVi` đã có trong dbCu) | 5 | Nguyên | ✅ Done | EPIC-04 |
| SD-016 | 📖 Story | [Module] Bảo trì thiết bị — Lịch bảo trì trò chơi + Chi phí (bảng `BaoTri` đã có trong dbCu) | 3 | Nhi | ✅ Done | EPIC-06 |
| SD-017 | 📖 Story | [Module] Bảng giá linh hoạt — Giá theo Ngày thường/Cuối tuần/Lễ tết/Cao điểm (bảng `BangGia` đã có trong dbCu) | 3 | Tấn | ✅ Done | EPIC-01 |
| SD-018 | 📖 Story | [Module] Ca làm + Phân ca — Quản lý lịch ca nhân viên (bảng `CaLam`, `PhanCa` đã có trong dbCu) | 2 | Nhi | ✅ Done | EPIC-06 |
| SD-019 | 🐛 Bug | **[🔥 CRITICAL]** DB v1 thiếu bảng `DoanKhach` → không track được chiết khấu đoàn tour, không quản lý cọc đoàn | — | Nguyên | 🔴 Blocked | EPIC-07 |
| SD-020 | 🐛 Bug | **[🔥 CRITICAL]** `HoaDon.PhuongThuc` chỉ 1 cột NVARCHAR → không bóc tách luồng Ví vs Tiền mặt vs Chuyển khoản khi thanh toán hỗn hợp | — | Nguyên | 🔴 Blocked | EPIC-07 |
| SD-021 | 🐛 Bug | **[HIGH]** DB v1 thiếu hoàn toàn: Phong, LoaiPhong, DatPhong, Ban, DatBan, NhaCungCap, PhieuKho, DongVat, SuCo, VaiTro, QuyenHan → Không thể phát triển nghiệp vụ Đại Nam | — | All | 🔴 Blocked | EPIC-07 |
| SD-022 | 🐛 Bug | **[HIGH]** Spaghetti Code: BUS gọi `DAL_*.Instance` static → untestable, coupling chặt | — | All | 🔴 Blocked | EPIC-07 |
| SD-023 | 📋 Task | Viết SRS Sprint 2 + TestCase (858 TC: NhanVien 112, KhachHang 110, BanHang 125, PhatVe+KiemSoatVe 115, ViDienTu+RFID 120, BaoTri 103, BangGia 108, LichLamViec 65) + Defect List (77 lỗi) + User Guide + MeetingMinutes | 2 | Nhi | ✅ Done | — |
| **Sprint Total** | | | **34** | | | |

### Sprint 2 Review (31/03/2026 — KH Đại Nam tham dự)
- ✅ 10/14 items Done, **4 Bugs BLOCKED** (cần Refactoring + DB Migration)
- **Phản hồi KH Đại Nam:**
  - 👎 "Phần mềm thiếu quản lý khách sạn, nhà hàng, vườn thú — đây là dịch vụ trọng yếu của chúng tôi"
  - 👎 "Nút bấm quá nhỏ cho POS cảm ứng 15.6 inch" → Ghi nhận **CR-01**
  - 👎 "Tích điểm quá đơn giản, đối thủ có hệ số VIP/VVIP" → Ghi nhận **CR-02**
  - 👍 "Quét vé QR tốt, ví RFID cơ bản ổn"
- **Quyết định Sprint 3 = FULL REFACTORING:**
  - DB: Tạo `Database_DaiNam.sql` mới (60+ bảng) thay thế `dbCu.sql`
  - Architecture: Gateway Pattern thay `DAL_*.Instance`
  - UI: Redesign POS cảm ứng
  - Nghiệp vụ: Bổ sung KS, NH, Biển, Thú, Kho, RBAC... theo yêu cầu Đại Nam

## 🚀 SPRINT 3: Tâm Bão — DB Migration + Gateway + Nghiệp vụ Đại Nam
**Thời gian:** 01/04/2026 → 14/04/2026 | **Goal:** Tái cấu trúc toàn diện. Bổ sung nghiệp vụ KDL Đại Nam (KS, NH, Kho, RBAC...).

> [!IMPORTANT]
> **KH Đại Nam cung cấp nghiệp vụ chi tiết:** Sơ đồ 200+ phòng KS, Menu nhà hàng, Quy trình bãi xe, Vườn thú 100+ loài...
> **CR-01:** PO yêu cầu redesign 100% UI sang chuẩn POS cảm ứng 15.6 inch.
> **CR-02:** Bổ sung hệ thống Tích Điểm Loyalty (VIP/VVIP, hệ số nhân).
> **Technical:** Team tạo DB mới `Database_DaiNam.sql` (60+ bảng) thay thế hoàn toàn `dbCu.sql` (23 bảng).

| Ticket ID | Type | Summary | Story Pts | Assignee | Status | Epic |
|:----------|:-----|:--------|:---------:|:---------|:------:|:-----|
| SD-024 | 🔧 Tech | **[DB MIGRATION]** Tạo `Database_DaiNam.sql` (60+ bảng) thay thế `dbCu.sql`. Thêm: Phong, LoaiPhong, DatPhong, ChiTietDatPhong, Ban, DatBan, NhaCungCap, PhieuKho, ChiTietPhieuKho, TheKho, DoanKhach, VaiTro, QuyenHan, NhanVienQuyen, DongVat, KhuVucBien, ChatLuongNuoc, ThueDo, SuCo, DonHang, ChiTietDonHang, PhieuThu, LichSuDiem, QuyTacDiem, VeDienTu... Xem ADR-003 | 8 | Nguyên | ✅ Done | EPIC-07 |
| SD-025 | 🔧 Tech | **[GATEWAY PATTERN]** Tạo `BUS_Dependencies.cs` — 30+ interfaces, cắt dứt DAL.Instance. Xem ADR-005 | 8 | Nguyên | ✅ Done | EPIC-07 |
| SD-026 | 🔄 CR | **[CR-01]** Redesign 100% UI → Chuẩn POS cảm ứng (ThemeManager + Guna2 + DevExpress) | 5 | Tấn | ✅ Done | EPIC-07 |
| SD-027 | 📖 Story | **[POS ★]** Form Bán Hàng v2 — 1020 dòng, Full: Cart + Loyalty + Dynamic Pricing + Camera + Kho xuất | 8 | Nguyên | ✅ Done | EPIC-03 |
| SD-028 | 📖 Story | [Module] Khách sạn — frmDatPhong + BookingDialog + ReserveDialog + DatPhongDoanDialog (**MỚI — theo yêu cầu Đại Nam**) | 5 | Tấn | ✅ Done | EPIC-02 |
| SD-029 | 📖 Story | [Module] Nhà hàng — frmNhaHang + frmDatBan + DatBanTruoc + GhepBan + ThanhToanHinhThuc (**MỚI — theo yêu cầu Đại Nam**) | 5 | Tấn | ✅ Done | EPIC-02 |
| SD-030 | 📖 Story | [Module] RBAC — 5 Vai trò × 36 Quyền hạn + Dynamic permission gate (**MỚI — dbCu chỉ có Admin/NhanVien**) | 3 | Nguyên | ✅ Done | EPIC-06 |
| SD-031 | 📖 Story | [Module] Kho hàng — frmKhoHang + frmNhaCungCap + frmTaoPhieuKho + frmPhieuNhapXuat + frmKiemKho + frmTheKho (**MỚI**) | 5 | Nguyên | ✅ Done | EPIC-05 |
| SD-032 | 📖 Story | [Module] Thuê đồ — frmThueDo: Phao, Tủ, Chòi + Cọc ví (**MỚI — theo yêu cầu Đại Nam**) | 3 | Nhi | ✅ Done | EPIC-02 |
| SD-033 | 📖 Story | [Module] Gửi xe — frmGuiXe: OCR biển số + Barcode + Tính phí (**MỚI**) | 3 | Tấn | ✅ Done | EPIC-02 |
| SD-034 | 📖 Story | [Module] Đơn hàng v2 + Phiếu Thu/Chi + Khách hàng v2 (Cá nhân + Đoàn) | 3 | Tấn | ✅ Done | EPIC-04 |
| SD-035 | 📖 Story | [Module] Combo v2 + Bảng giá Flat Matrix | 3 | Nhi | ✅ Done | EPIC-01 |
| SD-036 | 📖 Story | [Module] Dashboard báo cáo (4 cards + filter + grid) | 2 | Nhi | ✅ Done | EPIC-06 |
| SD-037 | 📋 Task | Xây dựng hệ thống Unit Testing với Mock Interfaces | 3 | Nguyên | ✅ Done | EPIC-07 |
| **Sprint Total** | | | **66** | | **🔥 Velocity Peak** | |

### Sprint 3 Review (14/04/2026 — KH Đại Nam tham dự)
- ✅ 14/14 items Done — **Sprint năng suất nhất** (crunch mode)
- DB tăng từ 23 → 60+ bảng, BUS files tăng từ ~25 → 50+
- KH Đại Nam hài lòng: "Đúng nghiệp vụ của chúng tôi rồi. Phòng khách sạn, nhà hàng, bãi xe đều có."
- Backend "Thép" thành hình: Gateway + DI + MVP + Unit Test ready

---

## 🚀 SPRINT 4: Khởi Nguyên Mây (Web B2C + Hardening)
**Thời gian:** 15/04/2026 → 28/04/2026 | **Goal:** Xây Web B2C cho KH Đại Nam, Fix bảo mật, Hoàn thiện Biển + Thú.

| Ticket ID | Type | Summary | Story Pts | Assignee | Status | Epic |
|:----------|:-----|:--------|:---------:|:---------|:------:|:-----|
| SD-038 | 📖 Story | Khởi tạo Kestrel C# API — RESTful endpoints nối CSDL Local | 5 | Nguyên | ✅ Done | EPIC-08 |
| SD-039 | 📖 Story | Blazor WebAssembly B2C — Website đặt vé online cho Đại Nam | 5 | Tấn | ✅ Done | EPIC-08 |
| SD-040 | 📋 Task | UX Giỏ hàng B2C + Kết nối API xuyên lục địa | 3 | Nhi | ✅ Done | EPIC-08 |
| SD-041 | 🐛 Bug | **[🔥 CRITICAL]** Hacker inject HTTP sửa giá giỏ hàng Web → 0đ. Fix: Server-side Recalculation | — | Nguyên | ✅ Fixed | EPIC-08 |
| SD-042 | 📖 Story | [Module] Khu vực Biển + Chất lượng nước (**theo yêu cầu Đại Nam — biển nhân tạo**) | 2 | Tấn | ✅ Done | EPIC-02 |
| SD-043 | 📖 Story | [Module] Vườn thú + Động vật (**theo yêu cầu Đại Nam — vườn thú 100+ loài**) | 2 | Nhi | ✅ Done | EPIC-02 |
| SD-044 | 📖 Story | Customer Dialogs: ThêmNhanh, Sửa, ĐiềuChỉnhĐiểm, ChọnDịchVụĐoàn | 3 | Tấn | ✅ Done | EPIC-01 |
| SD-045 | 📖 Story | Inventory phụ: Thẻ kho embedded + NCC | 2 | Nguyên | ✅ Done | EPIC-05 |
| SD-046 | 📖 Story | [Module] Sự cố + Thất lạc | 1 | Nhi | ✅ Done | EPIC-06 |
| **Sprint Total** | | | **23** | | | |

---

## 🚀 SPRINT 5: Siêu Tự Động Hóa Triển Khai (CI/CD + Delivery)
**Thời gian:** 29/04/2026 → 12/05/2026 | **Goal:** DevOps, Seed data, Tài liệu bàn giao, Đệ trình cho thầy + KH Đại Nam.

| Ticket ID | Type | Summary | Story Pts | Assignee | Status | Epic |
|:----------|:-----|:--------|:---------:|:---------|:------:|:-----|
| SD-047 | 🔧 DevOps | `run.bat` — Auto start 3 kênh: SQL + Kestrel API + Ngrok Tunnel | 3 | Nguyên | ✅ Done | EPIC-08 |
| SD-048 | 🔧 DevOps | `deploy.bat` — PowerShell bẻ khóa Vercel `_framework` cho Blazor WASM | 3 | Nguyên | ✅ Done | EPIC-08 |
| SD-049 | 📋 Task | Seed Fake Data Audit — Doanh thu + Log thẻ giả chuẩn kế toán (`insert.sql` — 65KB) | 3 | Nhi | ✅ Done | — |
| SD-050 | 📋 Task | Viết bộ tài liệu SD001 (SRS, TestCase, MeetingMinutes, DefectList, UserGuide, RiskMgmt, HĐ, RASCI) | 5 | Nhi | ✅ Done | — |
| SD-051 | 📖 Story | AI Chatbox Action Dispatcher tích hợp frmKhachHang | 3 | Nguyên | ✅ Done | EPIC-01 |
| SD-052 | 📋 Task | Khóa phiên bản đệ trình — Git tag v1.0 + Build final + Bàn giao KH Đại Nam | 1 | All | ✅ Done | — |
| **Sprint Total** | | | **18** | | | |

---

## 📊 BURNDOWN & VELOCITY

| Sprint | Committed (SP) | Completed (SP) | Velocity | Notes |
|:------:|:--------------:|:--------------:|:--------:|:------|
| S1 | 25 | 25 | 25 | Stable, on track. KH chưa tham dự. |
| S2 | 34 | 25 | 25 | 4 items BLOCKED → Tech Debt + KH feedback |
| S3 | 66 | 66 | 66 | 🔥 Crunch mode — Full Refactor + Nghiệp vụ Đại Nam |
| S4 | 23 | 23 | 23 | Back to sustainable pace |
| S5 | 18 | 18 | 18 | Documentation + Deploy + Bàn giao |
| **TOTAL** | **166** | **157** | **Avg: 31** | 4 Blocked items resolved in S3 |

---

## 📋 LABELS & FILTERS (Jira Simulation)

### Quick Filters:
- `label:critical-bug` → SD-019, SD-020, SD-041
- `label:refactoring` → SD-024, SD-025, SD-026, SD-037
- `label:change-request` → SD-026 (CR-01), SD-027 (CR-02)
- `label:devops` → SD-047, SD-048
- `label:security` → SD-041
- `label:dai-nam-requirement` → SD-028, SD-029, SD-031, SD-032, SD-033, SD-042, SD-043

### Component Map:
| Component | Forms | Tickets |
|:----------|:-----:|:--------|
| POS | 3 | SD-012 (basic), SD-027 (v2) |
| Customer | 6 | SD-011, SD-034, SD-044 |
| Hotel | 4 | SD-028 (**Sprint 3 — Đại Nam**) |
| Restaurant | 5 | SD-029 (**Sprint 3 — Đại Nam**) |
| Wallet/RFID | 3 | SD-015 (basic), SD-027 (v2) |
| Inventory | 6 | SD-031 (**Sprint 3 — Đại Nam**), SD-045 |
| Zone/Game | 6 | SD-003, SD-004, SD-042, SD-043 |
| System/RBAC | 5 | SD-030, SD-036 |

