---
name: coding-conventions
description: Các quy chuẩn cốt lõi (Coding Conventions) khi viết code cho dự án Quản Lý Công Viên Đại Nam (C# WinForms). Áp dụng bắt buộc trước khi tạo hoặc sửa code giao diện và logic.
---

# 📋 CODING CONVENTIONS — Đại Nam Park Management System

> **Mục đích**: Đây là Single Source of Truth cho coding style của dự án. 
> **AI Instruction**: KHÔNG ĐƯỢC VI PHẠM BẤT KỲ ĐIỀU NÀO DƯỚI ĐÂY kHI GENERATE CODE.

## I. KIẾN TRÚC 3-TIER BẮT BUỘC
- **ET (Entity):** Chứa POCO classes, không chứa logic. Mọi magic string ở đây (`AppConstants`).
- **DAL (Data Access Layer):** LINQ to SQL qua `DataQuanLyDaiNam.designer.cs`. Pattern Singleton.
- **BUS (Business Logic):** Chứa Validation. Pattern Singleton. Trả về `OperationResult`.
- **GUI (WinForms):** Guna.UI2 + DevExpress controls.

## II. GUI & CONTROLS BẮT BUỘC
1. **Thông báo (MessageBox)**:
   - ❌ KHÔNG dùng: `MessageBox.Show()`
   - ✅ BẮT BUỘC dùng: `TDCMessageBox.Show(...)`
2. **TabControl**: KHÔNG dùng nguyên bản, BẮT BUỘC dùng `Guna2TabControl` chuẩn hoá.
3. **ComboBox**:
   - Khoá ngoại DB (FK Lookup): Dùng `DevExpress SearchLookUpEdit (slk)`.
   - Trạng thái / Enum tĩnh: Dùng `Guna2ComboBox (cbo)`.
4. **Layout**:
   - Dùng `SplitContainer` / `Dock (Fill, Top, Right... )`, TUYỆT ĐỐI KHÔNG absolute layout.
   - Các bảng Grid: Dùng `DevExpress GridControl`.

## III. EVENT HANDLING BẮT BUỘC TÁCH BẠCH
- ❌ KHÔNG gom event vào `SetupEvents()`.
- ❌ KHÔNG dùng thẻ nặc danh lambda: `btn.Click += (s, e) => { ... }`
- ✅ BẮT BUỘC: Đăng ký hàm ở `.Designer.cs` (`this.btn.Click += new System.EventHandler(this.btn_Click)`) và viết logic event ở Code-behind `.cs` rõ ràng.

## IV. HARDCODE & NAMING
- ❌ KHÔNG Hardcode string trạng thái. ✅ PHẢI dùng `AppConstants`.
- **Tiền tố chuẩn:**
  - `txt` = TextBox | `cbo` = ComboBox | `slk` = SearchLookUpEdit | `btn` = Button
  - `lbl` = Label | `gb` = GroupBox | `grid` = GridControl | `pnl` = Panel
  - `ET_...`, `DAL_...`, `BUS_...`, `frm...`

## V. THEME & FILE MỚI BẮT BUỘC
- Mọi form CẦN có: `ThemeManager.ApplyTheme(this);` (trường hợp chứa Grid thêm `ThemeManager.StyleDevExpressGrid(grid);`)
- Form thiết kế mới bắt buộc khai báo vào Menu của `Form1.cs`.
- Tự động include file mới khai báo vào các file `.csproj` phù hợp.

## VI. FORM MẪU ĐỂ CLONE
- Code CRUD: → Xem `frmKhachHang`
- Code POS/Transaction: → Xem `frmThueDo`
- Code Dashboard/Parking: → Xem `frmGuiXe`
