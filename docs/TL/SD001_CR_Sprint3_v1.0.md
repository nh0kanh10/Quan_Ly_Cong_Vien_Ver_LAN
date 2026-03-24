# PHIẾU YÊU CẦU THAY ĐỔI (CHANGE REQUEST)

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_CR_Sprint3_v1.0  
**Nhóm**: NguyenTanNhi  
**Đề tài**: Hệ thống Quản lý Khu Vui Chơi Giải Trí Đại Nam  

---

## CHANGE REQUEST #1: Tái Cấu Trúc Giao Diện UI/UX

### 1. THÔNG TIN CHUNG
| Hạng mục | Chi tiết |
|----------|---------|
| **Mã yêu cầu (ID)** | CR-01 |
| **Ngày yêu cầu** | 31/03/2026 (Cuối Sprint 2) |
| **Người yêu cầu** | Product Owner (Khách hàng) |
| **Mức độ ưu tiên** | 🔴 Critical |
| **Trạng thái** | ✅ Đã phê duyệt & Hoàn thành (Sprint 3) |

### 2. MÔ TẢ THAY ĐỔI

#### 2.1 Hiện trạng
Giao diện WinForms hiện tại (Sprint 1-2) sử dụng layout truyền thống Master-Detail với thanh Ribbon cũ. Nút bấm nhỏ, font chữ mặc định, không phù hợp cho nhân viên POS thao tác nhanh trên **màn hình cảm ứng** tại quầy vé.

#### 2.2 Nội dung yêu cầu thay đổi
1. **Vứt bỏ 100%** giao diện cũ. Thiết kế lại toàn bộ Forms theo Style mới:
   - Nút bấm lớn, rõ ràng (>= 48px height) cho cảm ứng POS
   - Font hiện đại (Segoe UI, Cascadia Code cho số tiền)
   - Bảng giá/giỏ hàng TileView thay vì DataGridView cũ
2. **Hệ thống ThemeManager** tập trung: Quản lý Colors, Fonts, BorderRadius đồng nhất
3. **Phím tắt chuyên nghiệp**: F1 (tìm kiếm), F2 (mã KH), F8 (tiền đưa), F12 (thanh toán), ESC (hủy)

#### 2.3 Lý do thay đổi
- Nhân viên POS thao tác 8h/ngày → UI cần tối ưu **tốc độ** và **giảm mỏi mắt**
- Hệ thống kiosk tự phục vụ yêu cầu nút bấm lớn + layout đơn giản
- Design hiện tại "sai hướng" — quá giống phần mềm kế toán, không phải POS retail

### 3. PHÂN TÍCH TÁC ĐỘNG (IMPACT ANALYSIS)
| Hạng mục | Tác động | Chi tiết |
|----------|:--------:|----------|
| **Tiến độ (Schedule)** | [x] Có | Delay ~1 tuần Sprint 3 thêm cho redesign |
| **Chi phí (Cost)** | [x] Có | Mua thêm license Guna.UI2 + DevExpress |
| **Nguồn lực (Resources)** | [x] Có | M2 (Frontend) tăng ca |
| **Kỹ thuật (Technical)** | [x] Có | Không ảnh hưởng logic BUS/DAL — chỉ thay GUI layer |

### 4. PHÊ DUYỆT (APPROVAL)
| Vai trò | Họ tên | Ý kiến | Kết quả |
|---------|--------|--------|:---------:|
| **Quản trị dự án** | [Thành viên 1] | Gộp chung Sprint 3 cùng DB Migration | [x] Duyệt |
| **Khách hàng/PO** | [PO] | Đồng ý — phù hợp yêu cầu thực tế POS | [x] Duyệt |

---

## CHANGE REQUEST #2: Bổ Sung Hệ Thống Tích Điểm Loyalty

### 1. THÔNG TIN CHUNG
| Hạng mục | Chi tiết |
|----------|---------|
| **Mã yêu cầu (ID)** | CR-02 |
| **Ngày yêu cầu** | 31/03/2026 (Cuối Sprint 2) |
| **Người yêu cầu** | Product Owner (Khách hàng) |
| **Mức độ ưu tiên** | 🟠 High |
| **Trạng thái** | ✅ Đã phê duyệt & Hoàn thành (Sprint 3) |

### 2. MÔ TẢ THAY ĐỔI

#### 2.1 Hiện trạng
DB v1 (`dbCu.sql`) chỉ có 1 cột `DiemTichLuy INT` trong bảng `KhachHang` + 2 dòng cấu hình trong `CauHinhHeThong`. Không có lịch sử tích/tiêu điểm, không có quy tắc upgrade VIP, không có bảng chính sách điểm.

#### 2.2 Nội dung yêu cầu thay đổi
1. Tạo bảng **`LichSuDiem`** — Immutable ledger ghi lại mọi giao dịch điểm (tích/tiêu/hết hạn/điều chỉnh)
2. Tạo bảng **`QuyTacDiem`** — Engine quy tắc: TongDonToiThieu → SoDiemThuong, theo LoaiKhach
3. Logic business:
   - Tích: 100,000đ = 1 điểm × hệ số loại khách
   - Tiêu: Tối đa 50% giá trị đơn hàng, 1 điểm = 1,000 VND
   - Auto-upgrade: VIP (≥200 điểm), VVIP (≥500 điểm)
4. UI: Grid lịch sử điểm tích hợp trong `frmKhachHang` (tab Khách hàng, bottom panel)

#### 2.3 Lý do thay đổi
- Khách hàng (PO) nhận thấy đối thủ cạnh tranh đều có hệ thống Loyalty
- Tăng tỷ lệ quay lại (retention) của khách tham quan

### 3. PHÂN TÍCH TÁC ĐỘNG (IMPACT ANALYSIS)
| Hạng mục | Tác động | Chi tiết |
|----------|:--------:|----------|
| **Tiến độ (Schedule)** | [x] Có | Thêm ~3 ngày trong Sprint 3 |
| **Chi phí (Cost)** | [ ] Không | Không thêm chi phí |
| **Nguồn lực (Resources)** | [x] Có | M1 viết BUS_TichDiem, M2 thêm UI grid điểm |
| **Kỹ thuật (Technical)** | [x] Có | Thêm 2 bảng DB + 1 BUS file + sửa frmKhachHang + frmBanHang |

**Files bị ảnh hưởng:**
- `Database_DaiNam.sql` → +2 bảng (`LichSuDiem`, `QuyTacDiem`)
- `BUS_TichDiem.cs` → **NEW** (10,534 bytes)
- `BUS_KhachHang.cs` → Thêm logic auto-upgrade
- `frmKhachHang.cs` → Thêm tab Lịch sử điểm
- `frmBanHang.cs` → Tích hợp Loyalty Engine vào checkout

### 4. PHÊ DUYỆT (APPROVAL)
| Vai trò | Họ tên | Ý kiến | Kết quả |
|---------|--------|--------|:---------:|
| **Quản trị dự án** | [Thành viên 1] | Gộp chung Sprint 3 DB Migration | [x] Duyệt |
| **Khách hàng/PO** | [PO] | Tính năng quan trọng cho retention | [x] Duyệt |

---

## CHANGE REQUEST #3: Tái Cấu Trúc Database & Architecture (Internal)

### 1. THÔNG TIN CHUNG
| Hạng mục | Chi tiết |
|----------|---------|
| **Mã yêu cầu (ID)** | CR-03 |
| **Ngày yêu cầu** | 31/03/2026 (Sprint 2 Retrospective) |
| **Người yêu cầu** | Team (Internal — Technical Debt) |
| **Mức độ ưu tiên** | 🔴 Critical |
| **Trạng thái** | ✅ Đã phê duyệt & Hoàn thành (Sprint 3) |

### 2. MÔ TẢ THAY ĐỔI

#### 2.1 Hiện trạng
- DB v1 (`dbCu.sql` — 22 bảng) do team tự thiết kế ở Sprint 1 → **sai hướng phát triển**
- Schema không đủ mô hình hóa luồng tài chính phức tạp (Ví + Tiền mặt + Chuyển khoản)
- BUS layer 33+ files gọi `DAL_*.Instance` static → Spaghetti, untestable
- Không có RBAC, Audit trail, OCC

#### 2.2 Nội dung yêu cầu thay đổi
1. **DB Migration:** `dbCu.sql` (22 bảng) → `Database_DaiNam.sql` (60+ bảng)
   - Xem chi tiết: ADR-001 đến ADR-008
2. **Gateway Pattern:** Tạo `BUS_Dependencies.cs` (30+ interfaces)
3. **Hub & Spoke:** Mọi dịch vụ FK qua `ChiTietDonHang`
4. **Unit Testing Infrastructure:** Mock interfaces + automated tests

#### 2.3 Lý do thay đổi
- 2 Bug CRITICAL ở Sprint 2 (SD-018, SD-019) không thể fix trên schema cũ
- Schema cũ **không hỗ trợ O2O** (Web + Desktop) — thiếu `NguonBan` column
- Architecture cũ block việc viết Unit Test → chất lượng code không kiểm soát

### 3. PHÂN TÍCH TÁC ĐỘNG (IMPACT ANALYSIS)
| Hạng mục | Tác động | Chi tiết |
|----------|:--------:|----------|
| **Tiến độ (Schedule)** | [x] Có | Sprint 3 trở thành "Sprint Refactoring" toàn phần |
| **Chi phí (Cost)** | [ ] Không | Nội bộ team |
| **Nguồn lực (Resources)** | [x] Có | Team crunch mode (~61 SP vs bình thường 25 SP) |
| **Kỹ thuật (Technical)** | [x] Có | Đập đi xây lại 80% backend |

### 4. PHÊ DUYỆT (APPROVAL)
| Vai trò | Họ tên | Ý kiến | Kết quả |
|---------|--------|--------|:---------:|
| **Quản trị dự án** | [Thành viên 1] | "Không thể trì hoãn thêm nữa" | [x] Duyệt |
| **Khách hàng/PO** | [PO] | Đồng ý vì lợi ích dài hạn | [x] Duyệt |
