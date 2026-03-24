# MA TRẬN RASCI — PHÂN CÔNG TRÁCH NHIỆM
## SD001 — Hệ thống Quản Lý Khu Du lịch Đại Nam

**Mã tài liệu**: SD001_RASCI_v1.0  
**Ngày tạo**: 04/03/2026  
**Cập nhật**: 09/04/2026  

---

## 1. THÀNH VIÊN DỰ ÁN

| Ký hiệu | Họ tên | Vai trò | Trách nhiệm chính |
|:--------:|:-------|:--------|:-------------------|
| **NG** | Nguyên | Trưởng nhóm / Scrum Master / Lead Dev | Kiến trúc hệ thống, DB design, BUS/DAL core, DevOps, đưa ra quyết định kỹ thuật |
| **TN** | Tấn | Senior Developer / Frontend Lead | Code GUI, UI/UX design, API integration, Web B2C (Blazor) |
| **NH** | Nhi | QA Engineer / Technical Writer | Viết TestCase, kiểm thử, Defect tracking, viết SRS/tài liệu, User Guide |
| **TH** | Thầy [Tên] | Project Supervisor / Giảng viên | Giám sát tiến độ, review kỹ thuật, phê duyệt ADR, chấm điểm |
| **DN** | Đại diện KDL Đại Nam | Product Owner / Khách hàng | Cung cấp yêu cầu, phản hồi demo, duyệt CR, nghiệm thu sản phẩm |

---

## 2. GIẢI THÍCH RASCI

| Ký tự | Vai trò | Ý nghĩa |
|:-----:|:--------|:--------|
| **R** | Responsible | Người **trực tiếp thực hiện** công việc |
| **A** | Accountable | Người **chịu trách nhiệm cuối cùng** (phê duyệt/ký) — chỉ 1 người |
| **S** | Support | Người **hỗ trợ** thực hiện khi cần |
| **C** | Consulted | Người được **tham vấn** ý kiến trước khi thực hiện |
| **I** | Informed | Người được **thông báo** kết quả sau khi hoàn thành |

---

## 3. MA TRẬN RASCI TỔNG THỂ

### 3.1 Quản lý dự án & Quy trình

| # | Công việc | NG (Lead) | TN (Dev) | NH (QA) | TH (Thầy) | DN (Đại Nam) |
|:-:|:----------|:---------:|:--------:|:-------:|:----------:|:------------:|
| 1 | Ký hợp đồng phần mềm | **A** | I | I | C | **R** |
| 2 | Sprint Planning (lập kế hoạch) | **R/A** | S | S | C | C |
| 3 | Sprint Review (demo sản phẩm) | **R** | R | R | I | **A** |
| 4 | Sprint Retrospective | **R/A** | R | R | I | — |
| 5 | Viết Sprint Backlog | **R/A** | S | S | I | C |
| 6 | Quản lý rủi ro | **R/A** | S | S | C | I |
| 7 | Xử lý Change Request | R | S | I | C | **A** |
| 8 | Báo cáo tiến độ cho Thầy | **R/A** | S | S | **I** | — |
| 9 | Báo cáo tiến độ cho KH | **R** | S | S | I | **I** |
| 10 | Nghiệm thu cuối (M6) | R | R | R | **A** | **R** |

### 3.2 Phân tích & Thiết kế

| # | Công việc | NG (Lead) | TN (Dev) | NH (QA) | TH (Thầy) | DN (Đại Nam) |
|:-:|:----------|:---------:|:--------:|:-------:|:----------:|:------------:|
| 11 | Phân tích yêu cầu (Sprint 1-2: generic) | **R/A** | S | S | C | I |
| 12 | Khảo sát nghiệp vụ Đại Nam (Sprint 3+) | **R** | S | S | I | **A** |
| 13 | Viết SRS Sprint | **R** | S | **R** | C | **A** |
| 14 | Thiết kế DB Schema v1 (dbCu.sql) | **R/A** | I | I | C | — |
| 15 | Thiết kế DB Schema v3 (Database_DaiNam.sql) | **R/A** | S | I | C | I |
| 16 | Viết ADR (Architecture Decision Records) | **R/A** | C | I | **C** | I |
| 17 | Thiết kế UI Mockup / Prototype | S | **R/A** | C | I | C |
| 18 | Thiết kế kiến trúc Gateway Pattern | **R/A** | S | I | C | — |

### 3.3 Phát triển (Sprint 1: CRUD Danh Mục — dbCu.sql)

| # | Công việc | NG (Lead) | TN (Dev) | NH (QA) | TH (Thầy) | DN (Đại Nam) |
|:-:|:----------|:---------:|:--------:|:-------:|:----------:|:------------:|
| 19 | Code DAL Layer (Singleton pattern) | **R/A** | S | — | — | — |
| 20 | Code BUS Layer (Business Logic) | **R/A** | S | — | — | — |
| 21 | Code GUI — frmKhuVuc (quản lý khu vực) | S | **R/A** | — | — | — |
| 22 | Code GUI — frmTroChoi (Master-Detail khu vực) | S | **R/A** | — | — | — |
| 23 | Code GUI — frmLoaiVe + frmCombo (vé đơn + combo) | S | **R/A** | — | — | — |
| 24 | Code GUI — frmDichVu (F&B theo DanhMuc, DonViTinh = cột NVARCHAR) | S | S | **R** | — | — |
| 25 | Code frmLogin, frmDangKy (TaiKhoan: Admin/NhanVien) | **R/A** | S | — | — | — |
| 26 | Code frmConfigConnect (LAN connection string) | **R/A** | — | — | — | — |

### 3.3b Phát triển (Sprint 2: Vận Hành Cơ Bản — dbCu.sql)

| # | Công việc | NG (Lead) | TN (Dev) | NH (QA) | TH (Thầy) | DN (Đại Nam) |
|:-:|:----------|:---------:|:--------:|:-------:|:----------:|:------------:|
| 27 | Code GUI — frmNhanVien (hình ảnh + chức vụ) | S | **R/A** | — | — | — |
| 28 | Code GUI — frmKhachHang (SĐT + điểm tích lũy) | S | **R/A** | — | — | — |
| 29 | Code frmBanHang v1 (HoaDon + ChiTietHoaDon_Ve/DichVu) | **R/A** | S | — | — | — |
| 30 | Code frmSuKien + frmKhuyenMai | S | S | **R** | — | — |
| 31 | Code frmPhatVe + frmKiemSoatVe (VeDaPhatHanh, LichSuQuetVe) | **R/A** | S | — | — | — |
| 32 | Code frmViDienTu + frmQuayNapTien (ViDienTu, LichSuGiaoDichVi) | **R/A** | S | — | — | — |
| 33 | Code frmBaoTri (bảo trì trò chơi) | S | S | **R** | — | — |
| 34 | Code frmBangGia (giá linh hoạt theo thời điểm) | S | **R/A** | — | — | — |
| 35 | Code frmLichLamViec (CaLam + PhanCa) | S | S | **R** | — | — |

### 3.4 Phát triển (Sprint 3+: Nghiệp vụ Đại Nam)

| # | Công việc | NG (Lead) | TN (Dev) | NH (QA) | TH (Thầy) | DN (Đại Nam) |
|:-:|:----------|:---------:|:--------:|:-------:|:----------:|:------------:|
| 36 | DB Migration (23 → 60+ bảng) | **R/A** | S | I | C | I |
| 29 | BUS_Dependencies.cs (Gateway) | **R/A** | S | I | C | — |
| 30 | frmBanHang (POS 1020 dòng) | **R/A** | S | — | — | C |
| 31 | frmDatPhong (Khách sạn) | **R** | **R** | — | — | C |
| 32 | frmNhaHang + frmDatBan | S | **R/A** | — | — | C |
| 33 | frmViDienTu + frmTheRFID (Ví) | **R/A** | S | — | — | C |
| 34 | frmGuiXe (OCR + Barcode) | S | **R/A** | — | — | — |
| 35 | frmThueDo (Phao, Tủ, Chòi) | S | S | **R** | — | — |
| 36 | Inventory (5 forms kho) | **R/A** | S | — | — | — |
| 37 | frmBaoTri + DanhSachThietBi | S | S | **R** | — | — |
| 38 | RBAC (VaiTro × QuyenHan) | **R/A** | S | — | C | I |
| 39 | ThemeManager + UI Redesign POS | S | **R/A** | — | — | C |

### 3.5 Phát triển (Sprint 4-5: Web + DevOps)

| # | Công việc | NG (Lead) | TN (Dev) | NH (QA) | TH (Thầy) | DN (Đại Nam) |
|:-:|:----------|:---------:|:--------:|:-------:|:----------:|:------------:|
| 40 | Kestrel C# API | **R/A** | S | — | — | — |
| 41 | Blazor WebAssembly B2C | S | **R/A** | — | — | C |
| 42 | Security Fix (HTTP inject) | **R/A** | S | — | C | — |
| 43 | run.bat (SQL + API + Ngrok) | **R/A** | — | — | — | — |
| 44 | deploy.bat (Vercel + PowerShell) | **R/A** | — | — | — | — |
| 45 | Seed Data (insert.sql — 65KB) | S | — | **R/A** | — | — |
| 46 | AI Chatbox Action Dispatcher | **R/A** | S | — | — | — |

### 3.6 Kiểm thử & Tài liệu

| # | Công việc | NG (Lead) | TN (Dev) | NH (QA) | TH (Thầy) | DN (Đại Nam) |
|:-:|:----------|:---------:|:--------:|:-------:|:----------:|:------------:|
| 47 | Viết Test Cases (200+ TCs/Sprint) | S | S | **R/A** | I | — |
| 48 | Thực hiện Test + Ghi nhận Defect | S | S | **R/A** | I | — |
| 49 | Fix Defects | **R** | **R** | **A** | — | — |
| 50 | Kiểm thử hồi quy (5+ lỗi/Sprint) | — | — | **R/A** | — | — |
| 51 | Viết User Guide + Ảnh thật | — | S | **R/A** | I | I |
| 52 | Viết Biên bản họp | — | S | **R/A** | I | I |
| 53 | Viết Change Request | **R** | S | S | C | **A** |
| 54 | Viết Risk Management | **R/A** | S | S | C | — |
| 55 | Code Review | **R/A** | R | I | C | — |

---

## 4. PHÂN BỔ THEO SPRINT

### Sprint 1 (04/03 → 17/03/2026) — Nền tảng Generic (dbCu.sql)

| Người | Effort (%) | Công việc chính |
|:------|:----------:|:----------------|
| **Nguyên** | 100% | DB Design v1 (23 bảng generic). DAL + BUS core (Singleton). frmLogin, frmConfigConnect. Sprint Planning. |
| **Tấn** | 100% | GUI: frmKhuVuc, frmTroChoi, frmLoaiVe + frmCombo, frmDichVu. UI Mockup. |
| **Nhi** | 100% | SRS Sprint 1. 478 Test Cases (KhuVuc 105, TroChoi 108, LoaiVe+Combo 112, DichVu 103, Login 32, Config 18). 45 Defects. User Guide. Biên bản họp. |
| Thầy | — | Duyệt Sprint 1 (Review cuối Sprint) |
| Đại Nam | — | **Chưa tham gia** — team tự phát triển theo hướng generic |

### Sprint 2 (18/03 → 31/03/2026) — Vận hành cơ bản + KH phản hồi

| Người | Effort (%) | Công việc chính |
|:------|:----------:|:----------------|
| **Nguyên** | 100% | BUS: BanHang cơ bản (HoaDon), VePhatHanh + QuetVe, ViDienTu cơ bản. Fix Defects S1. |
| **Tấn** | 100% | GUI: frmNhanVien (ảnh), frmKhachHang, frmBangGia. Fix UI S1. |
| **Nhi** | 100% | GUI: frmSuKien/frmKhuyenMai + frmBaoTri + frmLichLamViec. SRS S2. 858 TCs (NhanVien 112, KhachHang 110, BanHang 125, PhatVe+KiemSoatVe 115, ViDienTu+RFID 120, BaoTri 103, BangGia 108, LichLamViec 65). 77 Defects. |
| Thầy | — | Tham dự Sprint 2 Review |
| **Đại Nam** | ★ | **LẦN ĐẦU tham dự Sprint 2 Review** → "Phần mềm generic, không khớp nghiệp vụ Đại Nam (KS, Thú, Biển, NH)" |

### Sprint 3 (01/04 → 14/04/2026) — 🔥 TÂM BÃO — Nghiệp vụ Đại Nam

| Người | Effort (%) | Công việc chính |
|:------|:----------:|:----------------|
| **Nguyên** | 120% 🔥 | DB Migration (60+ bảng). Gateway Pattern. frmBanHang POS. Ví RFID. RBAC. Kho. |
| **Tấn** | 120% 🔥 | UI Redesign 100% POS. frmDatPhong. frmNhaHang. frmDatBan. ThemeManager. |
| **Nhi** | 100% | SRS Sprint 3 (UseCase + Activity Diagram). TestCase. Defects. frmThueDo. frmBaoTri. |
| Thầy | — | Duyệt ADR. Review Sprint 3. |
| **Đại Nam** | ★★★ | **Cung cấp nghiệp vụ chi tiết**: Sơ đồ phòng KS, Menu nhà hàng, Quy trình vé. Duyệt 3 CR. |

### Sprint 4-5 (15/04 → 12/05/2026) — Web + DevOps + Bàn giao

| Người | Effort (%) | Công việc chính |
|:------|:----------:|:----------------|
| **Nguyên** | 100% | Kestrel API. run.bat. deploy.bat. Security fix. AI Chatbox. |
| **Tấn** | 100% | Blazor WASM B2C. Customer Dialogs. Zone forms (Biển, Thú). |
| **Nhi** | 100% | Seed Data (65KB). Bộ tài liệu SD001. Risk Management. Hợp đồng viết lại. |
| Thầy | — | Nghiệm thu cuối cùng (M6). Chấm điểm. |
| **Đại Nam** | ★★ | Kiểm tra Web B2C. Phản hồi UI. **Ký nghiệm thu.** |

---

## 5. BIỂU ĐỒ WORKLOAD

```
Sprint 1:  Nguyên ████████████████ 40%  |  Tấn ████████████████ 40%  |  Nhi ████████ 20%
Sprint 2:  Nguyên ██████████████ 35%    |  Tấn ██████████████ 35%    |  Nhi ████████████ 30%
Sprint 3:  Nguyên ██████████████████ 45% |  Tấn ██████████████ 35%    |  Nhi ████████ 20%  🔥
Sprint 4:  Nguyên ████████████████ 40%  |  Tấn ████████████████ 40%  |  Nhi ████████ 20%
Sprint 5:  Nguyên ██████████ 25%        |  Tấn ██████████ 25%        |  Nhi ████████████████████ 50%
```

> **Insight:** Sprint 3 = Nguyên gánh nặng nhất (DB + Architecture + POS). Sprint 5 = Nhi bận nhất (tài liệu bàn giao).

---

## 6. SƠ ĐỒ TỔ CHỨC (Organization Chart)

```
                    ┌─────────────────┐
                    │  Thầy [Tên]     │
                    │  Project        │
                    │  Supervisor     │
                    └────────┬────────┘
                             │ Giám sát
                    ┌────────▼────────┐
           ┌───────┤    NGUYÊN       ├───────┐
           │       │ Trưởng nhóm     │       │
           │       │ Scrum Master    │       │
           │       │ Lead Developer  │       │
           │       └────────┬────────┘       │
           │                │                │
    ┌──────▼──────┐  ┌──────▼──────┐  ┌──────▼──────┐
    │    TẤN      │  │    NHI      │  │  ĐẠI NAM    │
    │  Senior Dev │  │  QA/Tester  │  │  Product    │
    │  Frontend   │  │  Tech Writer│  │  Owner      │
    │  UI/UX Lead │  │  Doc Lead   │  │  (Khách)    │
    └─────────────┘  └─────────────┘  └─────────────┘
```
