# TÀI LIỆU QUẢN LÝ KIẾN THỨC CODEBASE CỦA AI (AI KNOWLEDGE BASE)
> **Mục tiêu:** Lưu trữ sự thấu hiểu chuyên sâu về Project `Quan_Ly_Cong_Vien_Ver_LAN` — Hệ thống Quản lý Tổng hợp Khu Du lịch Đại Nam.  
> **Cập nhật lần cuối:** 2026-03-28

---

## 1. KIẾN TRÚC TỔNG THỂ (3-TIER ARCHITECTURE)

```
┌─────────────────────────────────────────────┐
│  GUI (WindowsFormsApp1)                      │
│  WinForms + Guna2 + DevExpress GridControl   │
│  ThemeManager.cs | Form1 (MDI + Cache)       │
├─────────────────────────────────────────────┤
│  BUS (Business Logic Layer)                  │
│  Singleton Pattern | OperationResult         │
│  BUS_QuyenHan (Permission Engine)            │
│  BUS_BangGia (Pricing Engine)                │
│  BUS_DonHang (POS Transaction)               │
├─────────────────────────────────────────────┤
│  DAL (Data Access Layer)                     │
│  LINQ to SQL (DataQuanLyDaiNam.dbml)         │
│  ConnectionManager.cs (file-based config)    │
│  Singleton DAL_*.Instance                    │
├─────────────────────────────────────────────┤
│  DB: SQL Server — DaiNamResort               │
│  45+ tables | ACID | Optimistic Concurrency  │
│  Database_DaiNam.sql (Schema + Core Seed)    │
│  insert.sql (Extended Seed Data)             │
└─────────────────────────────────────────────┘
```

### Quy tắc kiến trúc:
- **ET** (Entity Transfer): POCO classes, mỗi table = 1 `ET_TenBang`
- **DAL**: Singleton `DAL_TenBang.Instance`, bắt exception → trả `false`
- **BUS**: Validation → gọi DAL → trả `OperationResult { IsSuccess, ErrorMessage }`
- **GUI**: KHÔNG chứa logic nghiệp vụ, chỉ bind data và xử lý UI event

---

## 2. DATABASE — DaiNamResort

### 2.1 Cấu trúc file SQL
| File | Vai trò |
|------|---------|
| `Database_DaiNam.sql` | Schema + Foreign Keys + Indexes + Triggers + **Core Seed Data (Section 19)** |
| `insert.sql` | Extended seed: NhanVien bổ sung, KhachHang, SanPham, DonHang, giao dịch mẫu |

### 2.2 Core Seed Data (KHÔNG ĐƯỢC SỬA nếu không hiểu)
Nằm trong `Database_DaiNam.sql` Section 19, bao gồm:
- **VaiTro**: Admin, QuanLy, NhanVien, ThuKho, KeToan
- **QuyenHan**: 30+ MaQuyen (VIEW_*, MANAGE_*, CREATE_*, DELETE_*)
- **PhanQuyen**: Phân quyền đầy đủ cho tất cả 5 vai trò
- **DonViTinh**: L, C, T, kg, V, S
- **KhuVuc**: KV01-KV08 (8 khu vực Đại Nam)
- **NhanVien Admin**: MaCode=NV001, login=`1/1`

### 2.3 Các nhóm bảng chính (45+ tables)
| Nhóm | Bảng |
|------|------|
| **Master Data** | VaiTro, QuyenHan, PhanQuyen, NhanVien, KhuVuc, DonViTinh |
| **Khách hàng** | KhachHang, DoanKhach |
| **Sản phẩm & Giá** | SanPham, BangGia, QuyDoiDonVi, Combo, ComboChiTiet |
| **Đơn hàng** | DonHang, ChiTietDonHang, VeDienTu |
| **Tài chính** | ViDienTu, TheRFID, GiaoDichVi, PhieuThu, PhieuChi |
| **Thuê đồ** | ThueDoChiTiet, TuDo, ThueTu, ChoiNghiMat, ThueChoi |
| **Khách sạn** | Phong, DatPhongChiTiet, ChiTietDatPhong |
| **Bãi đỗ xe** | BaiDoXe, LuotVaoRaBaiXe, VeDoXeChiTiet |
| **Biển nhân tạo** | KhuVucBien, ThietBiTaoSong, LichTaoSong, ChatLuongNuoc, CaTrucCuuHo |
| **Trường đua** | DuongDua, LoaiHinhDua, GiaiDua, LichThiDau, VanDongVien, NguaDua, PhuongTienDua, KetQuaDua, KhanDai, ViTriNgoi |
| **Vườn thú** | KhuVucThu, DongVat, ChuongTrai, LichChoAn, DatChoThuAn |
| **Nhà hàng** | NhaHang, BanAn, DatBan, ChiTietDatBan |
| **Kho** | NhaCungCap, KhoHang, PhieuNhapKho, ChiTietNhapKho, PhieuXuatKho, ChiTietXuatKho, TonKho |
| **Kiosk** | Kiosk, TonKhoKiosk |
| **An toàn** | SuCo, ThatLac |
| **App** | ThietBiApp |
| **Sự kiện** | SuKien, KhuyenMai |

---

## 3. HỆ THỐNG PHÂN QUYỀN (RBAC)

### 3.1 Luồng hoạt động
```
Login → UserSession.CurrentUser (NhanVien)
  → IdVaiTro → BUS_QuyenHan.HasPermission(IdVaiTro, "MANAGE_POS")
  → PhanQuyen JOIN QuyenHan → true/false
```

### 3.2 MaQuyen đang dùng trong code C#
| MaQuyen | Form sử dụng |
|---------|-------------|
| VIEW_DONHANG, MANAGE_DONHANG | frmDonHang, frmChiTietHoaDon |
| VIEW_STAFF, MANAGE_STAFF | frmNhanVien |
| MANAGE_USER | Form1 (btnVaiTro, btnPhanQuyen visibility) |
| VIEW_PRICE, MANAGE_PRICE | frmSanPham, frmBangGia |
| VIEW_INVENTORY, MANAGE_INVENTORY | frmKhoHang, frmNhaCungCap |
| VIEW_POS, MANAGE_POS | frmBanHang, frmQuayNapTien |
| VIEW_CUSTOMER, MANAGE_CUSTOMER | frmKhachHang |
| VIEW_PROMOTION, MANAGE_PROMOTION | frmMarketing, frmKhuyenMai |
| VIEW_LEDGER | frmPhieuThuChi |
| VIEW_RFID, MANAGE_RFID | frmTheRFID |
| VIEW_WALLET | frmViDienTu |
| VIEW_COMBO, MANAGE_COMBO | frmCombo, frmComboChiTiet |
| VIEW_REGION, MANAGE_REGION | frmKhuVuc |
| VIEW_UOM, MANAGE_UOM | frmDonViTinh |
| VIEW_REPORT | frmDashboard |
| VIEW_RFID_TOPUP, MANAGE_RFID_TOPUP | frmQuayNapTien |
| VIEW_TICKET_SIMULATION, MANAGE_TICKET_SIMULATION | frmAppDatVeMoPhong |

### 3.3 Compat Mode (BUS_QuyenHan)
Nếu MaQuyen **không tồn tại** trong DB → trả `true` (cho phép) để không phá UI khi seed chưa đủ.

---

## 4. ENGINE NGHIỆP VỤ QUAN TRỌNG

### 4.1 BUS_BangGia — Pricing Engine
- Tra giá từ bảng `BangGia` theo `IdSanPham` + `LoaiGia` + ngày hiện tại
- LoaiGia: `TheoNgay`, `NghiTrua`, `CuoiTuan`, `TheoGio`, `TienCoc`, `GioDau`, `GioSau`
- Trigger `TrgBangGiaNoOverlap` chặn overlap thời gian cùng (IdSanPham, LoaiGia)
- Quy tắc khách sạn: Giá nghỉ trưa = 50% giá ngày

### 4.2 BUS_DonHang — POS Transaction
- Tạo đơn hàng: DonHang → ChiTietDonHang → VeDienTu (nếu CanTaoToken)
- TrangThai: ChoThanhToan → DaThanhToan / DaHuy
- MaCode: `DH` + `yyMM` + 8 ký tự GUID (unique)

### 4.3 BUS_ThueDo — Rental Engine
- Luồng: Tạo thuê → Thu cọc (GiaoDichVi.ThuCoc) → Trả đồ → Tính phí giờ → Hoàn cọc/Phạt
- TrangThaiCoc: ChuaHoan → DaHoan / DaPhat
- Liên kết: ThueDoChiTiet ↔ DonHang, ThueTu ↔ TuDo, ThueChoi ↔ ChoiNghiMat

### 4.4 BUS_Phong — Hotel Engine (27KB - phức tạp nhất)
- Luồng: Đặt phòng → DatPhongChiTiet + ChiTietDatPhong → Check-in → Check-out
- TrangThai Phong: Trong → DaDat → DangSuDung → DonDep → Trong
- Optimistic Concurrency via RowVersion

### 4.5 BUS_Combo — Combo Engine
- Combo = gói nhiều SanPham, mỗi item có TyLePhanBo (tổng = 100%)
- Trigger `TrgComboChiTietTyLe100` enforce tổng 100%
- TrangThai: Bản nháp → Kích hoạt → Ngừng áp dụng

---

## 5. UI / UX & DESIGN SYSTEM

### 5.1 ThemeManager.cs (35KB)
- **Color Palette**: Slate (#1E293B), Blue (#3B82F6), Amber (#F59E0B), Green (#22C55E)
- **Font**: Segoe UI (global)
- **Pattern**: Quét đệ quy controls → áp dụng style tự động
- **Guard**: `IsInDesignMode()` chặn crash NRE trên Designer

### 5.2 Form1.cs (MDI Parent, 28KB)
- **Form Cache**: `Dictionary<Type, Form> _formCache` → mở form chỉ 1 lần
- **Sidebar**: Dynamic Button list + SidebarNavItem grouping
- **Permission Gate**: `Can("MANAGE_USER")` / `CanAny(...)` → show/hide buttons
- **Double-Buffer**: Force enable via Reflection để chống flicker

### 5.3 UI Components
| Component | Thay thế | Nguồn |
|-----------|---------|-------|
| GridControl + GridView | DataGridView | DevExpress |
| SearchLookUpEdit | ComboBox | DevExpress |
| Guna2TextBox, Guna2Button | TextBox, Button | Guna.UI2 |
| SplitContainerControl | SplitContainer | DevExpress |
| FontAwesome.Sharp | Image resources | IconHelper.cs |

---

## 6. SẢN PHẨM (SanPham) — PHÂN LOẠI

| LoaiSanPham | Ví dụ | MaCode pattern |
|-------------|-------|----------------|
| Ve | Vé cổng, vé đua, vé cho thú ăn | SP001-SP010, SP017-SP019 |
| Thue | Phao, tủ đồ, xe điện, chòi | SP004-SP007, SP020, THUE-* |
| LuuTru | Phòng khách sạn | SP011-SP015_VL |
| AnUong | Nước, suất ăn | SP015, SP101, SP103 |
| Combo | Gói combo | COMBO01-02, CB_GAME_* |
| Khac | Bãi xe | SP016 |

---

## 7. CAUTION / RISKS

### ⚠️ DBML Sync
Mọi thay đổi schema DB **bắt buộc** update `DataQuanLyDaiNam.dbml` thủ công (kéo thả table trong VS Designer). Nếu ET model và DBML không khớp → runtime crash.

### ⚠️ CreatedAt Column
`BangGia` table **KHÔNG có** cột `CreatedAt` trong schema! Các INSERT cũ dùng `CreatedAt` sẽ fail. Chỉ dùng: `IdSanPham, LoaiGia, GiaTien, NgayApDungTu, TrangThai`.

### ⚠️ Connection String
Chuỗi kết nối DB lưu ở `connection.txt`, load bởi `DAL/ConnectionManager.cs`. Fallback: local SQL Express.

### ⚠️ Cursor Memory Leak
`insert.sql` dùng nhiều CURSOR (khach_hang_cursor, rfid_cursor, song_cursor). Mỗi cursor phải CLOSE + DEALLOCATE đúng cặp.

### ⚠️ RAND() Determinism
`RAND()` trong SQL Server trả cùng giá trị trong 1 statement. Dùng `ABS(CHECKSUM(NEWID()))` cho random thực sự.

---

## 8. LUỒNG CHẠY DATABASE

```
1. Chạy Database_DaiNam.sql
   → Tạo DB DaiNamResort
   → Tạo 45+ tables
   → Tạo Indexes, SPs, Triggers
   → Tạo Foreign Keys
   → Core Seed: VaiTro, QuyenHan, PhanQuyen, DonViTinh, KhuVuc, NhanVien Admin

2. Chạy insert.sql (TÙY CHỌN - dữ liệu mẫu)
   → NhanVien bổ sung (NV002-NV008)
   → KhachHang (24 cá nhân + 5 đoàn)
   → SanPham (vé, thuê đồ, ăn uống, lưu trú)
   → BangGia (giá phòng, giá thuê, tiền cọc)
   → DonHang + ChiTietDonHang (1000+ đơn random)
   → ViDienTu + TheRFID + GiaoDichVi
   → Combo + ComboChiTiet
   → Physical resources: Phong, TuDo, BanAn, ChoiNghiMat
   → Trường đua: GiaiDua, LichThiDau, VanDongVien, NguaDua
   → Vườn thú: DongVat, ChuongTrai, LichChoAn
   → Biển: LichTaoSong, ChatLuongNuoc, CaTrucCuuHo
   → Kho: PhieuNhapKho, PhieuXuatKho, TonKho

3. Login hệ thống: TenDangNhap = "1", MatKhau = "1"
```

---

## 9. FILE QUAN TRỌNG THEO CHỨC NĂNG

| Chức năng | BUS | DAL | GUI |
|-----------|-----|-----|-----|
| Đăng nhập | BUS_NhanVien | DAL_NhanVien | frmLogin |
| Phân quyền | BUS_QuyenHan, BUS_PhanQuyen | DAL_QuyenHan, DAL_PhanQuyen | frmPhanQuyen |
| POS Bán hàng | BUS_DonHang, BUS_BangGia | DAL_DonHang, DAL_ChiTietDonHang | frmBanHang |
| Thuê đồ | BUS_ThueDo | DAL_ThueDoChiTiet | frmThueDo |
| Khách sạn | BUS_Phong | DAL_Phong, DAL_DatPhongChiTiet | frmDatPhong |
| Kho hàng | BUS_KhoHang | DAL_KhoHang, DAL_PhieuNhapKho | frmKhoHang |
| Thẻ RFID | BUS_TheRFID | DAL_TheRFID | frmTheRFID |
| Ví điện tử | BUS_ViDienTu, BUS_GiaoDichVi | DAL_ViDienTu, DAL_GiaoDichVi | frmViDienTu |
| Dashboard | — (SQL thuần) | DAL custom | frmDashboard |

---

**Summary for AI:** "Hệ thống quản lý khu du lịch Đại Nam — WinForms 3-tier (ET/DAL/BUS/GUI), 45+ tables SQL Server, RBAC 30+ permissions, Pricing Engine động, POS + Thuê đồ + Khách sạn + Vé điện tử. UI: Guna2 + DevExpress, ThemeManager Flat-Premium, Form Cache MDI. Core seed data trong Database_DaiNam.sql, extended seed trong insert.sql."
