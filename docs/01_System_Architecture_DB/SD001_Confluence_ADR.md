# 📜 CONFLUENCE: ARCHITECTURE DECISION RECORDS (ADR)
## SD001 — Khu Vui Chơi Giải Trí Đại Nam

**Space:** SD001-Engineering  
**Author:** Nguyễn Tấn Nhị  
**Cập nhật lần cuối:** 09/04/2026  

---

## ADR-001: Universal Product Catalog
**Ngày quyết định:** 01/04/2026 (Sprint 3)  
**Trạng thái:** ✅ Accepted  
**Người quyết định:** Team (M1 đề xuất, PO duyệt)

### Bối cảnh (Context)
DB v1 (`dbCu.sql`) tách riêng 3 bảng: `LoaiVe`, `DichVu`, `DanhMucDichVu`. Mỗi loại sản phẩm có bảng riêng → `ChiTietHoaDon_Ve` và `ChiTietHoaDon_DichVu` phải tách thành 2 bảng detail. Khi mở rộng thêm sản phẩm "Thuê đồ", "Lưu trú"... phải tạo thêm bảng detail → không scale.

### Quyết định (Decision)
Gộp tất cả vào **1 bảng `SanPham`**, phân biệt bằng cột `LoaiSanPham` (`'Ve','Combo','Thue','AnUong','LuuTru','DoLuuNiem','GuiXe','DichVu','Khac'`). `ChiTietDonHang` chỉ cần **1 FK duy nhất** (`IdSanPham`).

### Hệ quả (Consequences)
| ✅ Pros | ⚠️ Cons |
|:--------|:--------|
| Giảm 60% số JOIN trong query báo cáo | Một số cột NULL cho LoaiSanPham không liên quan |
| BangGia engine apply cho TẤT CẢ loại SP | Cần CHECK constraint cẩn thận hơn |
| 1 SP duy nhất query tất cả | LoaiSanPham cần Enum quản lý bên BUS |

### Bằng chứng so sánh
```diff
- dbCu.sql: LoaiVe(22 cột) + DichVu(11 cột) + DanhMucDichVu(5 cột)
-           ChiTietHoaDon_Ve + ChiTietHoaDon_DichVu (2 bảng detail)
+ Database_DaiNam.sql: SanPham(1 bảng, 16 cột) + SanPham_Ve (Weak Entity)
+           ChiTietDonHang (1 bảng hub duy nhất)
```

---

## ADR-002: Hub & Spoke (Universal Line Item)
**Ngày quyết định:** 01/04/2026 (Sprint 3)  
**Trạng thái:** ✅ Accepted

### Bối cảnh
Sprint 2 bộc lộ vấn đề: Mỗi dịch vụ (Thuê đồ, KS, Bãi xe...) FK trực tiếp vào `DonHang`. Khi cần query "tất cả dịch vụ của 1 đơn hàng" phải LEFT JOIN 5+ bảng trực tiếp → query phình to, khó maintain.

### Quyết định
Mọi dịch vụ FK qua **`ChiTietDonHang` (HUB)** thay vì `DonHang` trực tiếp. 1 Stored Procedure duy nhất (`SpGetChiTietDonHangToanPhan`) query tất cả.

```
DonHang ──► ChiTietDonHang (HUB) ──┬──► ThueDoChiTiet
                                    ├──► DatPhongChiTiet ──► Phong
                                    ├──► DatBan ──► BanAn
                                    ├──► VeDoXeChiTiet
                                    └──► DatChoThuAn
```

### Bằng chứng so sánh
```diff
- dbcu.txt (v2): ThueDoChiTiet.IdDonHang (FK trực tiếp)
-                DatBan.IdDonHang (FK trực tiếp)
-                VeDoXeChiTiet.IdDonHang (FK trực tiếp)
+ Database_DaiNam.sql (v3): ThueDoChiTiet.IdChiTietDonHang (qua HUB)
+                           DatBan.IdChiTietDonHang (qua HUB)
+                           VeDoXeChiTiet.IdChiTietDonHang (qua HUB)
```

---

## ADR-003: DB Migration — Từ bỏ Schema v1
**Ngày quyết định:** 01/04/2026 (Sprint 3)  
**Trạng thái:** ✅ Accepted  
**Trigger:** Sprint 2 Bugs SD-018, SD-019

### Bối cảnh
DB v1 (`dbCu.sql`) do team **tự thiết kế ở Sprint 1** với giả định ban đầu quá đơn giản:
- DB name `QuanLyCongVien` (generic, không chuyên nghiệp)
- Bảng `TaiKhoan` tách riêng khỏi `NhanVien` → duplicate
- `HoaDon` dùng `PhuongThuc` 1 cột → không track multi-payment
- Không có bảng `DoanKhach` → Bug "bốc hơi tiền cọc" (SD-018)
- Không có `PhieuThu`/`PhieuChi` → không tách được luồng tiền (SD-019)
- Không có RBAC → Admin/NhanVien hardcode trong `VaiTro` column

### Quyết định
**Drop toàn bộ schema v1. Thiết kế lại từ đầu** với tên mới `DaiNamResort`, áp dụng 5 design patterns mới (Universal Product Catalog, Hub & Spoke, Weak Entity, Immutable Ledger, OCC).

### Phân tích tác động
| Module | Impact Level | Chi tiết |
|:-------|:-------------|:---------|
| DAL (40+ files) | 🔴 HIGH | Đổi tên bảng, cột, kiểu dữ liệu |
| BUS (33+ files) | 🔴 HIGH | Refactor toàn bộ logic + Gateway |
| GUI (21 forms) | 🟡 MEDIUM | Rebind data, cập nhật column names |
| Test | 🟢 LOW | Viết mới hoàn toàn |

### Migration Path
```
dbCu.sql (22 bảng) ──[Sprint 2]──► dbcu.txt (50 bảng, thử nghiệm)
                                      │
                                      ▼ ✗ Over-engineering BangGia (3 bảng)
                                      │ ✗ FK trực tiếp (chưa Hub & Spoke)
                                      │ ✗ TonKhoKiosk duplicate logic
                                      │
                     ──[Sprint 3]──► Database_DaiNam.sql (60+ bảng) ✅
                                      • Flat Pricing Matrix
                                      • Hub & Spoke
                                      • Weak Entity Pattern
                                      • Immutable Ledger
```

---

## ADR-004: Flat Pricing Matrix (Thay thế 3-bảng v2)
**Ngày quyết định:** 03/04/2026 (Sprint 3)  
**Trạng thái:** ✅ Accepted

### Bối cảnh
Trong quá trình thử nghiệm DB v2 (`dbcu.txt`), team thiết kế hệ thống giá 3 bảng: `DM_LoaiGia` (từ điển) + `BangGia` (header) + `BangGia_BieuDoGio` (block giờ). Quá phức tạp cho nhu cầu thực tế chỉ cần 3 loại ngày.

### Quyết định
Bỏ `DM_LoaiGia` + `BangGia_BieuDoGio`. Thay bằng **3 cột giá inline** trong 1 bảng `BangGia`:
```sql
GiaNgayThuong DECIMAL(15,0)
GiaCuoiTuan   DECIMAL(15,0)
GiaNgayLe     DECIMAL(15,0)
```
Bổ sung bảng `CauHinhNgayLe` để engine tự xác định ngày lễ.

---

## ADR-005: Gateway Pattern (Break Monolith DAL)
**Ngày quyết định:** 02/04/2026 (Sprint 3)  
**Trạng thái:** ✅ Accepted  
**Trigger:** Bug SD-020 (Spaghetti Code)

### Bối cảnh
Sprint 1-2: Toàn bộ 33+ BUS files gọi `DAL_*.Instance` static. Hệ quả:
- **Không thể Unit Test** (cần SQL Server thật)
- BUS coupling chặt với DAL → đổi 1 DAL phải sờ N BUS
- BUS gọi BUS khác qua static → circular dependency risk

### Quyết định
Tạo **`BUS_Dependencies.cs`** (37KB) chứa 30+ interfaces. Mỗi BUS inject dependency qua constructor thay vì gọi static.

```csharp
// TRƯỚC (Sprint 1-2):
public class BUS_KhachHang {
    public DataTable GetAll() => DAL_KhachHang.Instance.GetAll();
}

// SAU (Sprint 3+):
public class BUS_KhachHang {
    private readonly IDAL_KhachHang _dal;
    public BUS_KhachHang(IDAL_KhachHang dal) { _dal = dal; }
    public DataTable GetAll() => _dal.GetAll();
}
```

### Lợi ích đạt được
- ✅ Unit Test bằng Mock: `new BUS_KhachHang(mockDal)` → test logic không cần DB
- ✅ Swap DAL implementation dễ dàng (SQL Server → SQLite → API)
- ✅ BUS-to-BUS decoupled qua interfaces (`IBUS_GiaoDichVi`, `IBUS_DoanKhach`)

---

## ADR-006: Weak Entity Pattern
**Ngày quyết định:** 03/04/2026 (Sprint 3)  
**Trạng thái:** ✅ Accepted

### Bối cảnh
Khu vực Biển và Vườn thú có thuộc tính riêng (DoSauToiDa, YeuCauPhao, DienTich, SucChuaDongVat...) nhưng vẫn là KhuVuc. DB v2 dùng `KhuVucBien.Id` riêng (IDENTITY) → duplicate `TenKhuVuc`.

### Quyết định
`KhuVucBien` và `KhuVucThu` dùng **PK = FK → KhuVuc.Id** (Weak Entity). Không có IDENTITY riêng.

```sql
-- Weak Entity: PK chính là FK
CREATE TABLE KhuVucBien (
    IdKhuVuc INT NOT NULL PRIMARY KEY,  -- PK = FK → KhuVuc(Id)
    DoSauToiDa DECIMAL(5,2),
    YeuCauPhao BIT NOT NULL DEFAULT 0
);
```

---

## ADR-007: Immutable Ledger cho Tài Chính
**Ngày quyết định:** 04/04/2026 (Sprint 3)  
**Trạng thái:** ✅ Accepted

### Quyết định
Các bảng tài chính **KHÔNG BAO GIỜ UPDATE/DELETE**:
- `GiaoDichVi` → Hash SHA256 chống giả mạo (`HashSignature`)
- `TheKho` → Sổ kho bất biến
- `LichSuDiem` → Ledger điểm tích lũy
- `AuditDonHang` → Trigger tự ghi log chuyển trạng thái
- `PhieuThu`/`PhieuChi` → Không có `IsDeleted`, hủy = đổi TrangThai

---

## ADR-008: Merge TonKhoKiosk vào TonKho
**Ngày quyết định:** 05/04/2026 (Sprint 3)  
**Trạng thái:** ✅ Accepted

### Bối cảnh
DB v2 (`dbcu.txt`) có bảng `TonKhoKiosk` riêng với `RowVersion`, `NguongCanhBao`. Logic duplicate với `TonKho`.

### Quyết định
Xóa `TonKhoKiosk`. Dùng `KhoHang.LoaiKho = 'Kiosk'` để phân biệt. Bổ sung `TonKho.NguongCanhBao` và `V_CanhBaoTonKho` view thay thế.
