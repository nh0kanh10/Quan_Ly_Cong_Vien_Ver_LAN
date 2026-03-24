# SPRINT REPORT — Tổng Hợp 5 Sprints
## SD001 — Hệ thống Quản Lý Khu Vui Chơi Giải Trí Đại Nam

**Mã dự án:** SD001  
**Mã tài liệu:** SD001_SprintReport_All_v1.0  
**Nhóm:** NguyenTanNhi  

---

## SPRINT 1 REPORT: Xây Dựng Nền Tảng Ban Đầu
**Thời gian:** 04/03/2026 → 17/03/2026

### Tổng kết

| Hạng mục | Giá trị |
|:---------|:--------|
| **Sprint Goal** | Thiết kế DB v1 generic (`dbCu.sql` — 23 bảng), dựng kiến trúc WinForms 3 lớp, 7 form CRUD danh mục |
| **Planned Items** | 9 |
| **Completed Items** | 9 ✅ |
| **Story Points** | 25/25 (100%) |
| **Defects Found** | 45 (8 Critical, 14 Major, 13 Medium, 10 Minor) |
| **KH Đại Nam** | Chưa tham gia Sprint 1 |

### Chức năng đã hoàn thành

| # | Feature | Form/File | Status |
|:-:|:--------|:----------|:------:|
| 1 | Database v1 (23 bảng generic) | `dbCu.sql` | ✅ |
| 2 | Quản lý Khu Vực | `frmKhuVuc.cs` | ✅ |
| 3 | Quản lý Trò Chơi | `frmTroChoi.cs` | ✅ |
| 4 | Quản lý Loại Vé + Combo | `frmLoaiVe.cs`, `frmCombo.cs` | ✅ |
| 5 | Quản lý Dịch vụ F&B | `frmDichVu.cs` | ✅ |
| 6 | Đăng nhập + Cấu hình LAN | `frmLogin.cs`, `frmConfigConnect.cs` | ✅ |

### Demo Highlights
- Form CRUD hoạt động stable, data validation cơ bản
- Ribbon navigation + MainForm layout chuyên nghiệp
- Kết nối LAN qua SQL Server hoạt động tốt

### Retrospective

| 😊 Went Well | 😞 Needs Improvement | 🎯 Action Items |
|:-------------|:---------------------|:----------------|
| Hoàn thành đúng deadline | 45 defects → validate chưa kỹ | Fix defects đầu Sprint 2 |
| Kiến trúc 3 lớp rõ ràng | DB v1 generic chưa gắn nghiệp vụ Đại Nam | Sẽ đánh giá Sprint 2 |
| Git flow ổn định | Test case viết sau code | Viết TC song song với code |

---

## SPRINT 2 REPORT: Mở Rộng & Khủng Hoảng
**Thời gian:** 18/03/2026 → 31/03/2026

### Tổng kết

| Hạng mục | Giá trị |
|:---------|:--------|
| **Sprint Goal** | Module vận hành còn lại trong dbCu: NV, KH, Bán hàng, Vé, Ví, Bảo trì, Bảng giá, Ca làm, Sự kiện |
| **Planned Items** | 14 |
| **Completed Items** | 10 ✅ + 4 🔴 BLOCKED |
| **Story Points** | 25/34 (74%) |
| **Defects Found** | 77 (65 mới + 12 hồi quy) |
| **KH Đại Nam** | **LẦN ĐẦU tham gia Sprint 2 Review** → phản hồi "phần mềm generic" |

### Chức năng đã hoàn thành (dùng bảng đã có trong dbCu.sql)

| # | Feature | Form/File | Status |
|:-:|:--------|:----------|:------:|
| 1 | Nhân viên (CRUD + Ảnh) | `frmNhanVien.cs` | ✅ |
| 2 | Khách hàng (CRUD + SĐT + Điểm) | `frmKhachHang.cs` | ✅ |
| 3 | Bán hàng cơ bản (POS v1) | `frmBanHang.cs` (v1) | ✅ |
| 4 | Vé phát hành + Quét vé | `frmPhatVe.cs`, `frmKiemSoatVe.cs` | ✅ |
| 5 | Ví RFID cơ bản | `frmViDienTu.cs`, `frmQuayNapTien.cs`, `frmTheRFID.cs` | ✅ |
| 6 | Bảo trì | `frmBaoTri.cs`, `frmThemBaoTri.cs` | ✅ |
| 7 | Bảng giá + Sự kiện/KM | `frmBangGia.cs`, `frmMarketing.cs`, `frmKhuyenMai.cs` | ✅ |
| 8 | Lịch ca làm việc | `frmLichLamViec.cs` | ✅ |

### 🔴 BLOCKED Items (DB v1 thiếu bảng)

| ID | Bug | Root Cause | Impact |
|:---|:----|:-----------|:-------|
| SD-019 | Thiếu bảng `DoanKhach` | DB v1 generic không có bảng này | 🔴 Không thể track chiết khấu đoàn |
| SD-020 | Không tách luồng tiền | `HoaDon.PhuongThuc` chỉ 1 cột | 🔴 Kế toán không đối soát được |
| SD-021 | DB v1 thiếu bảng KS/NH/Kho... | dbCu.sql không có Phong/Ban/PhieuKho | 🔴 KH Đại Nam yêu cầu bổ sung |
| SD-022 | Spaghetti Code | DAL_*.Instance static khắp nơi | 🔴 Không viết được Unit Test |

### Retrospective

| 😊 Went Well | 😞 Needs Improvement | 🎯 Action Items |
|:-------------|:---------------------|:----------------|
| Code nhanh, nhiều form vận hành | DB v1 generic → KH Đại Nam phản hồi thiếu nghiệp vụ | **Sprint 3 = FULL REFACTOR + Nghiệp vụ Đại Nam** |
| Vé QR + Ví RFID hoạt động tốt | DB thiếu KS/NH/Kho/RBAC/Đoàn | Thiết kế lại DB từ đầu |
| POS cơ bản OK | BUS untestable (static coupling) | Áp dụng Gateway Pattern |

> [!CAUTION]
> **Sprint 2 Retro Key Decision:**  
> Team ĐỒNG THUẬN: DB v1 (`dbCu.sql`) được thiết kế **generic cho mọi công viên giải trí**, không khớp nghiệp vụ thực tế KDL Đại Nam.  
> KH Đại Nam yêu cầu bổ sung: Khách sạn, nhà hàng, vườn thú, biển, kho, RBAC...  
> → **Sprint 3 sẽ là Sprint Refactoring toàn phần + Bổ sung nghiệp vụ Đại Nam.**

---

## SPRINT 3 REPORT: Tâm Bão Chuyển Đổi ⚡
**Thời gian:** 01/04/2026 → 14/04/2026

### Tổng kết

| Hạng mục | Giá trị |
|:---------|:--------|
| **Sprint Goal** | DB Migration + Gateway Pattern + UI Redesign + Loyalty + POS |
| **Planned Items** | 14 |
| **Completed Items** | 14 ✅ |
| **Story Points** | 61/61 (100%) 🔥 |
| **Change Requests** | 3 (CR-01: UI, CR-02: Loyalty, CR-03: DB+Arch) |

### Epic Achievements

| Category | Before (Sprint 2) | After (Sprint 3) |
|:---------|:-------------------|:------------------|
| **DB Tables** | 23 (dbCu.sql) | 60+ (Database_DaiNam.sql) |
| **DB Name** | QuanLyCongVien | DaiNamResort |
| **Architecture** | DAL_*.Instance (static) | Gateway + DI + MVP |
| **BUS Files** | ~25 (coupled) | 50+ (decoupled) |
| **UI Style** | Traditional Ribbon | POS Touch-Optimized |
| **Test** | Manual only | Mock Interfaces ready |
| **Loyalty** | 1 cột DiemTichLuy | LichSuDiem Ledger + QuyTacDiem Engine |

### Retrospective

| 😊 Went Well | 😞 Needs Improvement | 🎯 Action Items |
|:-------------|:---------------------|:----------------|
| 61 SP hoàn thành = kỷ lục team | Crunch mode = burnout risk | Sprint 4 giảm pace |
| DB v3 design solid | Velocity không bền vững | Back to 20-25 SP |
| Gateway Pattern = unit test ready | Cần document ADR cho v3 | Viết ADR Sprint 4 |

---

## SPRINT 4 REPORT: Khởi Nguyên Mây ☁️
**Thời gian:** 15/04/2026 → 28/04/2026

### Tổng kết

| Hạng mục | Giá trị |
|:---------|:--------|
| **Sprint Goal** | Web B2C (Kestrel + Blazor) + Security Fix + Zone forms |
| **Planned Items** | 9 |
| **Completed Items** | 9 ✅ |
| **Story Points** | 23/23 (100%) |
| **Security Fix** | 1 CRITICAL (HTTP inject → Server-side Recalculation) |

### Key Deliverables
- ✅ Kestrel C# API → RESTful endpoints
- ✅ Blazor WebAssembly B2C Website → Vercel deployment
- ✅ Server-side Recalculation chặn hacker inject giá
- ✅ Zone forms: Biển + Chất lượng nước + Vườn thú + Động vật
- ✅ Customer dialogs: ThêmNhanh, Sửa, ĐiềuChỉnhĐiểm

### Retrospective

| 😊 Went Well | 😞 Needs Improvement | 🎯 Action Items |
|:-------------|:---------------------|:----------------|
| Sustainable velocity | Zone forms 60-70% completeness | Phase 2 backlog |
| Security fix ngay khi phát hiện | Dashboard 50% | Cải thiện Sprint 5+ |
| Web B2C chạy mượt | Cần DevOps automation | Sprint 5 focus DevOps |

---

## SPRINT 5 REPORT: Siêu Tự Động Hóa 🚀
**Thời gian:** 29/04/2026 → 12/05/2026

### Tổng kết

| Hạng mục | Giá trị |
|:---------|:--------|
| **Sprint Goal** | DevOps (run.bat + deploy.bat) + Seed data + Documentation + Submit |
| **Planned Items** | 6 |
| **Completed Items** | 6 ✅ |
| **Story Points** | 18/18 (100%) |

### Key Deliverables
- ✅ `run.bat` — Auto start 3 kênh (SQL + API + Ngrok)
- ✅ `deploy.bat` — PowerShell bẻ khóa Vercel `_framework`
- ✅ `insert.sql` — 65KB seed data chuẩn kế toán
- ✅ Bộ tài liệu SD001 hoàn chỉnh (15+ files)
- ✅ AI Chatbox tích hợp Customer module
- ✅ Git tag v1.0 — Freeze phiên bản đệ trình

### Retrospective (Final)

| 😊 Went Well | 😞 Needs Improvement (Phase 2) |
|:-------------|:-------------------------------|
| O2O deployment trơn tru | Dashboard chưa có chart |
| Seed data chuẩn kế toán | Module trường đua chưa có GUI |
| Tất cả core forms 95-100% | Vườn thú/Biển 60-70% |
| AI chatbox bonus feature | Export báo cáo chưa có |

---

## 📊 TỔNG KẾT DỰ ÁN

### Velocity Chart

```
Sprint 1: ████████████████████████████████ 25 SP
Sprint 2: ████████████████████████████████████████ 30 SP (+3 BLOCKED)
Sprint 3: ████████████████████████████████████████████████████████████████████████████ 61 SP 🔥
Sprint 4: ██████████████████████████████ 23 SP
Sprint 5: ████████████████████ 18 SP
```

### Cumulative Flow

| Sprint | Total Forms | Total BUS | DB Tables | Docs |
|:------:|:----------:|:---------:|:---------:|:----:|
| S1 | 7 | 10 | 23 | 3 |
| S2 | 21 | 25 | 23 | 6 |
| S3 | 52 | 50+ | 60+ | 10 |
| S4 | 62 | 50+ | 60+ | 12 |
| S5 | 62 | 50+ | 60+ | 15+ |

### "Tiểu Thuyết Trinh Thám" — Đường Dây Kịch Bản

```
Sprint 1 ──── Sprint 2 ──── Sprint 3 ──── Sprint 4 ──── Sprint 5
   │              │              │              │              │
   ▼              ▼              ▼              ▼              ▼
DB generic    Vận hành      ⚡ TÂM BÃO     Mở Web B2C   DevOps
(23 bảng)     cơ bản         KH Đại Nam    + Security   + Deploy
+ 7 forms    + 9 forms      cung cấp NV    + Fix hack   + Submit
              KH phản hồi   DB 60+ bảng
              "quá generic"  Gateway DI
              4 BLOCKED 🔴   UI POS ★
                             Loyalty ★
                             KS/NH/Kho ★
```

> **Moral of the Story:**  
> Bắt đầu từ DB generic (23 bảng cho mọi CV giải trí) → Xây form vận hành cơ bản →  
> KH Đại Nam tham dự Sprint 2 Review → phản hồi: "phần mềm generic, không khớp nghiệp vụ" →  
> Team **tái cấu trúc toàn diện** Sprint 3 (DB mới 60+ bảng + Gateway + UI POS + Nghiệp vụ Đại Nam) →  
> Thành công: Backend "Thép" + Web B2C + Deploy trơn tru ở Sprint 5. 🏆

