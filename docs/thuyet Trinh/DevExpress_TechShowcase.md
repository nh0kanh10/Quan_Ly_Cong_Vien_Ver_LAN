# 🎯 DevExpress Technology Showcase
## Dự án Quản Lý Công Viên Đại Nam — Tổng hợp công nghệ DevExpress

> **Dành cho:** Demo ngày mai | **Phiên bản:** Ver 1.2 | **Stack:** C# WinForms + DevExpress

---

## 📦 Danh sách thư viện DevExpress đã tích hợp

| Namespace | Component | Dùng ở đâu |
|---|---|---|
| `DevExpress.XtraGrid` | GridControl + View system | Toàn hệ thống (15+ form) |
| `DevExpress.XtraCharts` | ChartControl | `frmBaoCao` |
| `DevExpress.XtraGauges` | CircularGauge | `frmBaoCao` |
| `DevExpress.XtraPivotGrid` | PivotGridControl | `frmBaoCao` |
| `DevExpress.XtraTreeList` | TreeList + Nodes | `frmPhanQuyen` |
| `DevExpress.XtraEditors` | DateEdit, SplitContainer... | `frmBaoCao`, nhiều form |
| `DevExpress.XtraEditors.Repository` | RepositoryItem (editors) | `frmBanHang`, `frmKiemKho` |
| `DevExpress.XtraVerticalGrid` | VerticalGrid | `frmPhieuThuChi` |

---

## 🔥 ĐIỂM NHẤN KỸ THUẬT — Top 7 thứ hay nhất

---

### 1. 🗂️ XtraGrid — Multi-View Architecture

**Giải thích:** DevExpress GridControl không phải grid thường. Nó cho phép **cùng 1 GridControl** chạy nhiều View khác nhau.

**Project dùng 2 loại View khác nhau trong 1 form POS:**

```
frmBanHang:
  ├── gridSanPham  →  TileView  (hiển thị sản phẩm dạng thẻ như Shopee)
  └── gridGioHang  →  GridView  (bảng giỏ hàng chuẩn)
```

**Code minh chứng** (`frmBanHang.Designer.cs`):
```csharp
// Sản phẩm hiển thị dạng TileView — như app mua hàng
this.tileViewSanPham = new DevExpress.XtraGrid.Views.Tile.TileView();

// Giỏ hàng hiển thị dạng bảng chuẩn
this.gridViewGioHang = new DevExpress.XtraGrid.Views.Grid.GridView();
```

**Điểm thú vị để nói với thầy:**
> "Em không dùng DataGridView thuần. GridControl của DevExpress cho phép thay đổi View mà không cần tạo lại control — đây là pattern Strategy trong UI."

---

### 2. 🎨 CustomRowCellEdit — Per-Row Dynamic Editor

**Đây là tính năng KHÔNG CÓ trong DataGridView thường.**

**Bài toán:** Cột ĐVT trong giỏ hàng — sản phẩm có quy đổi (chai/thùng) thì cho ComboBox, sản phẩm thường thì TextEdit readonly.

```csharp
// frmBanHang.cs - line 300
private void OnCartCustomRowCellEdit(
    object sender, 
    DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
{
    if (e.Column.FieldName != "TenDVT") return;
    var item = gridViewGioHang.GetRow(e.RowHandle) as CartItem;

    if (item.CoNhieuDVT)
    {
        // Mỗi dòng có thể có ComboBox KHÁC NHAU
        var combo = new RepositoryItemComboBox();
        // ... thêm DVT tương ứng với sản phẩm đó
        e.RepositoryItem = combo;
    }
    else
    {
        // Dòng này readonly — không cho đổi DVT
        var readOnly = new RepositoryItemTextEdit();
        readOnly.ReadOnly = true;
        e.RepositoryItem = readOnly;
    }
}
```

**Điểm thú vị:**
> "Mỗi ô trong cùng 1 cột có thể dùng editor khác nhau — đây là RepositoryItem pattern. Rất khó làm với DataGridView thuần."

---

### 3. 📊 frmBaoCao — Real-time Dashboard 5-in-1

Dashboard báo cáo tích hợp **5 component DevExpress** trên cùng 1 form:

```
frmBaoCao Layout:
  ┌─────────────────────┬────────────────────┐
  │  ChartControl       │  CircularGauge     │
  │  (Bar/Line/Area)    │  (KPI % đạt mục  │
  │                     │   tiêu 50 triệu)   │
  ├─────────────────────┼────────────────────┤
  │  ChartControl       │  PivotGridControl  │
  │  (Pie Chart)        │  (Bảng chéo phân  │
  │                     │   tích đa chiều)   │
  └─────────┬───────────┴────────────────────┘
            │  GridControl (Bảng chi tiết)    │
            └─────────────────────────────────┘
```

**Auto-refresh mỗi 30 giây:**
```csharp
_refreshTimer = new Timer();
_refreshTimer.Interval = 30000; // 30 giây
_refreshTimer.Tick += (s, e) => LoadData(); // Real-time!
```

**Xuất file không cần thư viện 3rd-party:**
```csharp
gridControl.ExportToXlsx(dlg.FileName); // Export Excel
gridControl.ExportToPdf(dlg.FileName);  // Export PDF
```

---

### 4. 📈 XtraCharts — Dynamic Chart Switching

**Người dùng có thể chuyển loại biểu đồ ngay trong runtime** chỉ bằng ComboBox:

```csharp
ViewType viewType = ViewType.Bar;
switch (cboLoaiBieuDo.SelectedIndex)
{
    case 0: viewType = ViewType.Bar;        break; // Cột
    case 1: viewType = ViewType.Line;       break; // Đường
    case 2: viewType = ViewType.Area;       break; // Vùng
    case 3: viewType = ViewType.StackedBar; break; // Cột chồng
}
var series = new Series("Doanh thu", viewType);
```

**Pie Chart có thể click để "tách miếng":**
```csharp
pieView.RuntimeExploding = true; // Click vào miếng bánh → tách ra!
pieView.ExplodedDistancePercentage = 5;
series.Label.TextPattern = "{A}: {VP:P1}"; // "Đồ uống: 25.3%"
```

---

### 5. 🔄 XtraPivotGrid — Phân tích đa chiều như Excel

**PivotGrid = Pivot Table của Excel, nhưng trong WinForms.**

```csharp
// Hàng: Nhóm sản phẩm
var fieldNhom = new PivotGridField("NhomSanPham", PivotArea.RowArea);

// Cột: Kênh bán (POS / Web / App)
var fieldNguon = new PivotGridField("NguonBan", PivotArea.ColumnArea);

// Giá trị: Tổng doanh thu
var fieldDoanhThu = new PivotGridField("DoanhThu", PivotArea.DataArea);
fieldDoanhThu.SummaryType = PivotSummaryType.Sum;

// Số lượng bán
var fieldSoLuong = new PivotGridField("SoLuong", PivotArea.DataArea);
```

**Kết quả bảng trực quan:**

| Nhóm SP | POS | Web | App | Grand Total |
|---|---|---|---|---|
| Vé vào cổng | 12,500,000 | 5,200,000 | 3,100,000 | 20,800,000 |
| Đồ ăn | 8,700,000 | - | 1,200,000 | 9,900,000 |
| Đồ uống | 4,300,000 | - | 800,000 | 5,100,000 |

---

### 6. 🌳 XtraTreeList — Phân quyền dạng cây

**TreeList cho phép hiển thị dữ liệu phân cấp (hierarchy) với checkbox.**

```csharp
// frmPhanQuyen.cs
treeQuyen.OptionsView.ShowCheckBoxes = true;
treeQuyen.OptionsBehavior.AllowRecursiveNodeChecking = true; // Tick cha → tick hết con!

// Bind dữ liệu thông qua KeyField + ParentField
treeQuyen.DataSource   = nodes;
treeQuyen.KeyFieldName = "Id";
treeQuyen.ParentFieldName = "ParentId";
treeQuyen.ExpandAll();
```

**Cấu trúc cây quyền:**
```
☑ Tất cả quyền hệ thống
  ☑ Bán hàng & Hóa đơn (POS)
    ☑ Xem dữ liệu
    ☑ Quản lý (Thêm/Sửa/Xóa)
  ☑ Kho hàng & Sản phẩm
    ☑ Xem dữ liệu
    ☐ Quản lý (Thêm/Sửa/Xóa)
  ...
```

---

### 7. 🎨 ThemeManager — Global DevExpress Grid Styling

**Tự xây ThemeManager bọc DevExpress API** — 50+ form được style nhất quán chỉ bằng 1 lời gọi:

```csharp
// Gọi 1 lần → toàn bộ app thống nhất style
ThemeManager.ApplyTheme(this);

// Custom draw Header phẳng hoàn toàn
view.CustomDrawColumnHeader += (s, e) =>
{
    e.Cache.FillRectangle(new SolidBrush(GridHeaderColor), e.Bounds);
    e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
    // Đường phân cách cột mờ — Slate-300 opacity 60%
    using (Pen pen = new Pen(Color.FromArgb(60, 203, 213, 225)))
        e.Graphics.DrawLine(pen, ...);
    e.Handled = true; // Override hoàn toàn DevExpress rendering
};
```

**Anti-Flicker refresh tất cả form khi đổi theme:**
```csharp
public static void RefreshAllOpenForms()
{
    foreach (Form f in Application.OpenForms)
    {
        f.SuspendLayout();         // Chặn render tạm thời
        f.BackColor = BackgroundColor;
        ApplyStyleToControls(f.Controls);
        f.ResumeLayout(true);      // Render lại 1 lần duy nhất
        f.Refresh();
    }
}
```

---

## 🏆 BONUS: RepositoryItem — Editor Factory Pattern

**Các loại RepositoryItem đã dùng trong project:**

| RepositoryItem | Dùng ở đâu | Mục đích |
|---|---|---|
| `RepositoryItemSpinEdit` | Cột SL giỏ hàng | Spin +/- số lượng trực tiếp trong ô |
| `RepositoryItemComboBox` | Cột ĐVT giỏ hàng | Chọn đơn vị tính per-row |
| `RepositoryItemTextEdit` (ReadOnly) | Cột ĐVT sản phẩm đơn | Không cho sửa |
| `RepositoryItemButtonEdit` | Cột Xóa giỏ hàng | Nút icon thùng rác trong ô |

```csharp
// SpinEdit cho cột số lượng — có nút mũi tên lên/xuống
RepositoryItemSpinEdit spinEdit = new RepositoryItemSpinEdit();
spinEdit.IsFloatValue = false;
spinEdit.MinValue = 0;
spinEdit.MaxValue = 999;
v.Columns["SoLuong"].ColumnEdit = spinEdit;

// ButtonEdit cho nút xóa — icon trashcan trong ô grid
btnXoa = new RepositoryItemButtonEdit();
btnXoa.TextEditStyle = TextEditStyles.HideTextEditor;
btnXoa.Buttons[0].Kind = ButtonPredefines.Glyph;
btnXoa.Buttons[0].Image = IconHelper.GetBitmap(IconChar.TrashAlt, DangerColor, 24);
btnXoa.ButtonClick += btnXoa_ButtonClick;
```

---

## 💡 Gợi ý điểm nhấn khi demo với thầy

| Điểm nói | Câu mẫu |
|---|---|
| **TileView** | "Thay vì DataGridView thông thường, em dùng TileView để hiển thị menu sản phẩm theo dạng card — giống app thương mại điện tử" |
| **CustomRowCellEdit** | "Mỗi dòng trong giỏ hàng có thể có editor khác nhau — đây là kỹ thuật không thể làm với DataGridView thuần" |
| **Dashboard 5-in-1** | "Form báo cáo tích hợp Chart, Gauge, PivotGrid, Grid trong một layout responsive — tự điều chỉnh tỉ lệ khi resize" |
| **ExportToXlsx/PDF** | "Xuất báo cáo ra Excel hay PDF chỉ cần 1 dòng code — không cần cài thêm thư viện nào" |
| **ThemeManager** | "Em tự xây ThemeManager wrap DevExpress API — đổi theme live, 50+ form cập nhật ngay lập tức không bị flicker" |
| **TreeList** | "Phân quyền dạng cây — tick cha tự tick toàn bộ con, đây là AllowRecursiveNodeChecking của DevExpress XtraTreeList" |
| **PivotGrid** | "Pivot Table y chang Excel nhưng trong WinForms — phân tích doanh thu theo Nhóm SP × Kênh bán cross-tab" |

---

*Tài liệu được tạo tự động từ codebase — 2026-04-13*
