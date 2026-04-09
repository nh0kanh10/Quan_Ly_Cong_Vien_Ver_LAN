# CODEBASE MAP — QUẢN LÝ CÔNG VIÊN (LAN)
> **Mục đích**: File này mô tả kiến trúc, pattern và quy ước của toàn bộ source code để AI có thể đọc lại mà không cần scan lại codebase.  
> **Cập nhật lần cuối**: 2026-03-21

---

## 1. Kiến trúc tổng quan (3-Layer Architecture)

```
┌──────────────┐    ┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│  GUI (Forms) │───►│  BUS (Logic) │───►│  DAL (Data)  │───►│  SQL Server  │
│  WinForms    │    │  Validate    │    │  LINQ to SQL │    │  15 Tables   │
└──────────────┘    └──────────────┘    └──────────────┘    └──────────────┘
                         │                    │
                         ▼                    ▼
                    ┌──────────┐        ┌─────────────────┐
                    │ ET (POCO)│        │QLKVCGTDataContext│
                    │ Entities │        │  (.dbml file)   │
                    └──────────┘        └─────────────────┘
```

**Solution**: `QuanLyKhuVuiChoiGiaiTri.sln` gồm 4 projects:
- **ET** (Class Library) — Entity Transfer objects
- **DAL** (Class Library) — Data Access Layer (references: ET)
- **BUS** (Class Library) — Business Logic (references: DAL, ET)
- **WindowsFormsApp1** / GUI (WinForms) — UI (references: BUS, ET, DAL)

---

## 2. Database Schema (SQL Server — `QuanLyCongVien`)

### 2.1. Danh sách 15 bảng

| Bảng | PK | Mô tả | Sprint |
|------|-----|-------|--------|
| `TaiKhoan` | MaTaiKhoan (INT IDENTITY) | Đăng nhập, VaiTro: Admin/NhanVien | S1 |
| `KhuVuc` | MaKhuVuc (INT IDENTITY) | Khu vực công viên, MaCode: KV001 | S1 |
| `TroChoi` | MaTroChoi (INT IDENTITY) | Trò chơi, FK → KhuVuc, MaCode: TC001 | S1 |
| `LoaiVe` | MaLoaiVe (INT IDENTITY) | Loại vé (Combo hoặc thường), MaCode: V001 | S1 |
| `ChiTietCombo` | MaChiTietCombo (INT IDENTITY) | Vé con trong combo, FK → LoaiVe×2 | S1 |
| `DichVu` | MaDichVu (INT IDENTITY) | Dịch vụ (Ăn uống/Lưu niệm/Cho thuê), MaCode: DV001 | **S2** |
| `KhachHang` | MaKhachHang (INT IDENTITY) | Khách hàng, MaThanhVien, TongChiTieu | **S2** |
| `HoaDon` | MaHoaDon (INT IDENTITY) | Hóa đơn bán hàng, FK → KhachHang, NhanVien | **S2** |
| `ChiTietHoaDon` | MaChiTiet (INT IDENTITY) | Chi tiết HD, LoaiSanPham: Ve/DichVu | **S2** |
| `NhanVien` | MaNhanVien (INT IDENTITY) | Nhân sự, FK → KhuVuc, MaCode: NV001 | S3 |
| `CaLam` | MaCaLam (INT IDENTITY) | Ca làm việc | S3 |
| `PhanCa` | MaPhanCa (INT IDENTITY) | Phân ca, FK → NhanVien + CaLam | S3 |
| `SuKien` | MaSuKien (INT IDENTITY) | Sự kiện công viên | S4 |
| `KhuyenMai` | MaKhuyenMai (INT IDENTITY) | Khuyến mãi, FK → SuKien | S4 |
| `BaoTri` | MaBaoTri (INT IDENTITY) | Bảo trì thiết bị, FK → TroChoi + NhanVien | S4 |

### 2.2. Quy ước Database

- **PK**: Luôn INT IDENTITY(1,1)
- **MaCode**: NVARCHAR(10), UNIQUE — Dùng cho hiển thị (VD: KV001, TC003)
- **TrangThai**: NVARCHAR(20) — Giá trị cố định theo CHECK constraint
- **Ngày**: NgayTao (DEFAULT GETDATE()), NgayCapNhat (NULL khi mới tạo)
- **Tiền**: DECIMAL(18,2)
- **FK**: Đặt tên `FK_BangCon_BangCha`

### 2.3. Dữ liệu mẫu đã có (insert.sql)

- 10 KhuVuc (KV001–KV010)
- 23 TroChoi (TC001–TC023)
- 7 LoaiVe (V001–V007, gồm 2 Combo)
- 4 ChiTietCombo
- 3 NhanVien (NV001–NV003)
- 3 KhachHang (1 "Khách vãng lai" + 2 thành viên VIP/MEM)
- 11 HoaDon (HD260101 → HD260316)
- ChiTietHoaDon tương ứng

---

## 3. Connection String

File: `DAL/ConnectionManager.cs`  
- Đọc từ file `connection.txt` (cạnh exe). Nếu không có → fallback `.\SQLEXPRESS`
- Form `frmConfigConnect` cho phép nhập lại Server/DB/Auth

---

## 4. Pattern — Tầng ET (Entity Transfer)

**Namespace**: `ET`  
**Quy ước**:
- Class: `ET_<TenBang>` (VD: `ET_KhuVuc`, `ET_TroChoi`)
- Properties: Map 1:1 với cột DB
- Constructor: Rỗng `()` + Đầy đủ `(all params)`
- Extra properties: Tên cột join (VD: `TenKhuVuc` trong `ET_TroChoi`)
- View model: `ComboDetailView` (flat join cho DataGridView)

**Entities hiện có**: ET_KhuVuc, ET_TroChoi, ET_LoaiVe, ET_TaiKhoan, ET_ComboDetailView

---

## 5. Pattern — Tầng DAL (Data Access Layer)

**Namespace**: `DAL`  
**ORM**: LINQ to SQL — `QLKVCGTDataContext` (file `QLKVCGT.dbml`)  
**Quy ước**:
- Class: `DAL_<TenBang>` — Singleton pattern (`Instance` property)
- Mỗi method tạo connection mới: `using (var db = new QLKVCGTDataContext(ConnectionManager.GetConnectionString()))`
- Methods chuẩn:
  - `LoadDS()` / `LoadDSKhuVucHoatDong()` — Trả List<ET_xxx> hoặc List<LinqEntity>
  - `TimKiem(string tuKhoa)` — Search theo TenXxx + MaCode
  - `LayMaCodeTiepTheo()` — Prefix + auto-increment (VD: "KV" + D3 → "KV011")
  - `KiemTraTrungTen(string ten, string maCodeHienTai = null)` — Check unique
  - `Them<Entity>(ET_xxx et)` — Insert, return bool
  - `Sua<Entity>(ET_xxx et)` — Update by MaCode, return bool
  - `Xoa<Entity>(string maCode)` — Delete, return bool
- **Ngoại lệ**: `DAL_Dashboard` dùng raw `SqlCommand` + `SqlDataAdapter` (không LINQ)
- **Xử lý lỗi**: try-catch trả `false`, không throw

---

## 6. Pattern — Tầng BUS (Business Logic)

**Namespace**: `BUS`  
**Quy ước**:
- Class: `BUS_<TenBang>` — Singleton pattern
- Delegate 100% xuống DAL cho CRUD
- **Validate method**: `string Validate<Entity>(ET_xxx et, bool laThem = true)`
  - Trả `""` (empty) = OK, trả string = Error message
  - Kiểm tra: null/empty, length limit, giá trị hợp lệ, trùng tên
- **TimKiemNangCao**: Load tất cả → filter in-memory bằng LINQ (chấp nhận được vì <1000 records)

---

## 7. Pattern — Tầng GUI (Windows Forms)

**Namespace**: `WindowsFormsApp1`  
**Main Form**: `Form1.cs`
- **Navigation**: MenuStrip (menuDanhMuc, menuBaoCao, menuHeThong) + Sidebar buttons
- **Form Caching**: `Dictionary<Type, Form> _formCache` — Không tạo lại Form khi chuyển tab
- **OpenChildForm<T>()**: Generic method mở child form trong `pnlDesktop`
- **Theme**: `ThemeManager` quản lý color scheme tập trung
- **Icon**: `FontAwesome.Sharp` + `IconHelper`
- **Login flow**: `Program.cs` → `frmLogin` → (set Form1.Tag = ET_TaiKhoan) → `Form1`
- **Phân quyền đơn giản**: Check `ET_TaiKhoan.VaiTro == "Admin"` qua `Form1.Tag`

### 7.1. Forms hiện có (Sprint 1)

| Form | Chức năng | Sidebar Button |
|------|-----------|----------------|
| `frmDashboard` | Dashboard thống kê | (MenuStrip) |
| `frmKhuVuc` | CRUD Khu vực | btnKhuVuc |
| `frmTroChoi` | CRUD Trò chơi (Master-Detail) | btnTroChoi |
| `frmLoaiVe` | CRUD Loại vé + Chi tiết Combo | btnLoaiVe |
| `frmQuanLyTaiKhoan` | CRUD Tài khoản | btnTaiKhoan |
| `frmLogin` | Đăng nhập | (Startup) |
| `frmConfigConnect` | Cấu hình kết nối DB | (First run) |
| `frmCustomMessageBox` | Custom message box | (Utility) |

### 7.2. UI Controls thường dùng

- `Guna.UI2.WinForms` — Guna2Button, Guna2TextBox, Guna2Panel, Guna2DataGridView
- `TDCTextBox` — Custom TextBox wrapper
- `TDCCheckBox` — Custom CheckBox
- `TDCMessageBox` — Static wrapper cho MessageBox
- `SidebarNavItem` — Custom sidebar navigation item

### 7.3. Quy ước đặt tên Controls

| Prefix | Control | Ví dụ |
|--------|---------|-------|
| `txt` | TextBox | txtTenKhuVuc, txtGiaVe |
| `cbo` | ComboBox | cboKhuVuc, cboTrangThai |
| `btn` | Button | btnThem, btnSua, btnXoa, btnLuu |
| `dgv` | DataGridView | dgvDanhSach |
| `lbl` | Label | lblMaCode |
| `pnl` | Panel | pnlDetail, pnlDesktop |
| `chk` | CheckBox | chkLaCombo |

### 7.4. CRUD Flow chuẩn trong Form

```
1. Form_Load → BUS.LoadDS() → Bind to DataGridView
2. btnThem_Click → Clear fields → Set mode "Thêm" → Enable fields
3. btnLuu_Click → Collect ET from fields → BUS.Validate() → BUS.Them/Sua() → Reload
4. dgvDanhSach_CellClick → Fill fields from selected row
5. btnXoa_Click → Confirm dialog → BUS.Xoa(maCode) → Reload
6. txtTimKiem_TextChanged → BUS.TimKiem(keyword) → Rebind
```

---

## 8. NuGet Packages đã dùng

- `Guna.UI2.WinForms` — Modern UI controls
- `FontAwesome.Sharp` — Icon library
- `System.Data.Linq` — LINQ to SQL

---

## 9. Trạng thái DBML hiện tại

DBML đã kéo đủ **tất cả 15 bảng**. Tuy nhiên, một số cột được ALTER TABLE thêm sau chưa có trong DBML:
- `KhachHang`: Thiếu `DiaChi`, `NgaySinh`, `GioiTinh`
- `HoaDon`: Thiếu `TrangThai`
- `NhanVien`: Thiếu `CCCD`, `GioiTinh`, `NgaySinh`, `DiaChi`
- `BaoTri`: Thiếu `MaCode`

> **Hành động cần thiết**: Thêm tay các cột này vào `QLKVCGT.dbml` và `QLKVCGT.designer.cs` trước khi code Sprint 2.

**Pattern DAL Sprint 2**: Dùng LINQ to SQL giống Sprint 1 (Singleton + `QLKVCGTDataContext`).
