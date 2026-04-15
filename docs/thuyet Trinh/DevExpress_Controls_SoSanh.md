# 🎛️ DevExpress Controls — So sánh với WinForms Thuần
## Tài liệu demo: "Tại sao chọn DevExpress?"

> **Gốc câu hỏi của thầy:** "Dùng thư viện gì? Khác gì thư viện mặc định?"

---

## PHẦN 1 — Editors Độc Lập (Đứng trên Form)

### 1.1 DateEdit vs DateTimePicker

| | `DateTimePicker` (WinForms) | `DateEdit` (DevExpress) |
|---|---|---|
| **Namespace** | `System.Windows.Forms` | `DevExpress.XtraEditors` |
| **Dùng trong project** | `frmNhanVien`, `frmBaoCao` | `dtpNgaySinh`, `deTuNgay`, `deDenNgay` |
| **Giao diện** | Kiểu Windows cổ | Vector, skin WXI hiện đại |
| **Validate ngày** | Tự làm | Built-in (min/max, format mask) |
| **Format mask** | Limited | Đầy đủ: `dd/MM/yyyy`, `HH:mm`... |
| **Nullable** | ❌ | ✅ (nullable DateTime) |
| **Tích hợp Grid** | ❌ | ✅ qua `RepositoryItemDateEdit` |

**Code trong project** (`frmBaoCao.cs`):
```csharp
// DevExpress DateEdit - chọn khoảng ngày lọc báo cáo
deTuNgay.DateTime = DateTime.Today.AddDays(-30);
deDenNgay.DateTime = DateTime.Today;
```

---

### 1.2 SpinEdit vs NumericUpDown

| | `NumericUpDown` (WinForms) | `SpinEdit` (DevExpress) |
|---|---|---|
| **Namespace** | `System.Windows.Forms` | `DevExpress.XtraEditors` |
| **Dùng trong project** | Một số form cũ | `spnDoSau`, `spnPH`, `spnNhietDo`... |
| **Giao diện** | Nhỏ, thô, mũi tên nhỏ | Vector, mũi tên to rõ |
| **Decimal precision** | Giới hạn | Tùy ý |
| **Spin button style** | Dọc bên phải | Tùy chỉnh layout |
| **Tích hợp Grid** | ❌ | ✅ qua `RepositoryItemSpinEdit` |

**Code trong project** (`frmChatLuongNuoc.Designer.cs`):
```csharp
// SpinEdit đo thông số nước — buộc nhập số, có min/max
this.spnPH    = new DevExpress.XtraEditors.SpinEdit(); // pH 0-14
this.spnDoMan = new DevExpress.XtraEditors.SpinEdit(); // Độ mặn
this.spnNhietDo = new DevExpress.XtraEditors.SpinEdit(); // Nhiệt độ
```

---

### 1.3 ListBoxControl vs ListBox

| | `ListBox` (WinForms) | `ListBoxControl` (DevExpress) |
|---|---|---|
| **Namespace** | `System.Windows.Forms` | `DevExpress.XtraEditors` |
| **Dùng trong project** | ❌ Không dùng | `lstNVChuaPhan`, `lstNVDaPhan`, `lbVaiTro` |
| **Giao diện** | Thô, không có skin | Mượt mà, tích hợp theme |
| **Highlight chọn** | Màu hệ thống | Theo palette PrimaryColor |
| **Multi-select** | Có | Có + đẹp hơn |
| **DataBinding** | Cơ bản | Nâng cao |

**Dùng ở đâu:**
- `frmPhanQuyen`: `lbVaiTro` — danh sách vai trò để chọn
- `frmLichLamViec`: `lstNVChuaPhan` + `lstNVDaPhan` — drag nhân viên qua lại 2 danh sách

---

### 1.4 SplitContainerControl vs SplitContainer

| | `SplitContainer` (WinForms) | `SplitContainerControl` (DevExpress) |
|---|---|---|
| **Namespace** | `System.Windows.Forms` | `DevExpress.XtraEditors` |
| **Dùng trong project** | ❌ | `pnlMain` của 6+ form |
| **Splitter style** | Win98-style | Flat, hiện đại |
| **Collapsible panel** | ❌ | ✅ (click để thu gọn 1 bên) |
| **Skin integration** | ❌ | ✅ |

**Dùng ở đâu:** `frmKhuVuc`, `frmDongVat`, `frmKhuVucBien`, `frmTroChoi`, `frmNhanVien`, `frmChatLuongNuoc`
```csharp
// Pattern chuẩn: Bên trái = Grid, Bên phải = Form nhập liệu
this.pnlMain = new DevExpress.XtraEditors.SplitContainerControl();
```

---

## PHẦN 2 — RepositoryItem (Editor trong Grid)

> **Nguyên tắc:** Muốn control gì trong Grid → dùng `RepositoryItem` tương ứng

### Bảng tổng hợp RepositoryItem đã dùng

| RepositoryItem | Tương đương ngoài Grid | Dùng ở Form | Mục đích |
|---|---|---|---|
| `RepositoryItemSpinEdit` | `SpinEdit` / `NumericUpDown` | `frmBanHang`, `frmKiemKho`, `frmThueDo` | Nhập số lượng trong ô grid |
| `RepositoryItemComboBox` | `ComboBox` | `frmBanHang` (cột ĐVT) | Chọn đơn vị tính per-row |
| `RepositoryItemButtonEdit` | `Button` trong ô | `frmBanHang`, `frmMenuPopup`, `frmDatBan`, `frmCombo` | Nút hành động (Xóa, Sửa) trong ô |
| `RepositoryItemTextEdit` | `TextBox` | `frmBanHang` (ĐVT readonly) | TextBox readonly trong ô |

### 2.1 RepositoryItemSpinEdit — Spin số trong ô

```csharp
// frmBanHang.cs - Cột Số Lượng
RepositoryItemSpinEdit spinEdit = new RepositoryItemSpinEdit();
spinEdit.IsFloatValue = false;   // Chỉ số nguyên
spinEdit.MinValue = 0;           // Không nhập âm
spinEdit.MaxValue = 999;
spinEdit.Buttons[0].Visible = true; // Hiện nút mũi tên
v.Columns["SoLuong"].ColumnEdit = spinEdit;
```

```csharp
// frmKiemKho.cs - Cột Số Lượng Thực Tế khi kiểm kho
var spinRepo = new RepositoryItemSpinEdit();
spinRepo.MinValue = 0;
```

```csharp
// frmThueDo.cs - Cột Số Lượng Trả / Mất
DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinEditTra = 
    new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
```

---

### 2.2 RepositoryItemButtonEdit — Nút action trong ô

```csharp
// frmBanHang.cs - Nút XÓA dòng trong giỏ hàng
btnXoa = new RepositoryItemButtonEdit();
btnXoa.TextEditStyle = TextEditStyles.HideTextEditor; // Ẩn phần text, chỉ hiện nút
btnXoa.Buttons[0].Kind = ButtonPredefines.Glyph;      // Dùng icon tự chọn
btnXoa.Buttons[0].Image = IconHelper.GetBitmap(IconChar.TrashAlt, DangerColor, 24);
btnXoa.ButtonClick += btnXoa_ButtonClick;              // Bắt sự kiện click
v.Columns["colXoa"].ColumnEdit = btnXoa;
```

```csharp
// frmDatBan.cs - Nút SỬA trạng thái bàn
var btnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
btnEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
```

```csharp
// frmCombo.cs - Nút XÓA sản phẩm trong combo
var btnRepo = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
```

---

### 2.3 RepositoryItemComboBox — Dropdown per-row

```csharp
// frmBanHang.cs - Cột ĐVT: mỗi dòng có options KHÁC NHAU
private void OnCartCustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
{
    if (e.Column.FieldName != "TenDVT") return;
    var item = gridViewGioHang.GetRow(e.RowHandle) as CartItem;

    if (item.CoNhieuDVT)
    {
        var combo = new RepositoryItemComboBox();
        combo.DropDownRows = 6;
        combo.Items.Add("Chai");  // DVT cơ bản
        combo.Items.Add("Thùng"); // DVT quy đổi
        e.RepositoryItem = combo; // Gán cho đúng dòng này
    }
    else
    {
        var readOnly = new RepositoryItemTextEdit();
        readOnly.ReadOnly = true; // Không cho đổi
        e.RepositoryItem = readOnly;
    }
}
```

---

## PHẦN 3 — Layout/Container Controls

### SplitContainerControl — Master-Detail Layout

**Pattern dùng trong 6 form ZoneGame + Staff:**
```
┌──────────────────┬──────────────────────┐
│   GridControl    │   Form nhập liệu     │
│   (Danh sách)    │   (Chi tiết/Edit)    │
│                  │                      │
│  - Khu vực biển │  txtTen, spnDoSau... │
│  - Động vật     │  SpinEdit số liệu    │
│  - Trò chơi     │  DateEdit ngày tạo   │
└──────────────────┴──────────────────────┘
      Panel 1              Panel 2
   SplitContainerControl.Panel1/2
```

---

## PHẦN 4 — Tổng hợp DevExpress vs WinForms Thuần

### Bảng so sánh đầy đủ

| WinForms Thuần | DevExpress Equivalent | Đã dùng trong project |
|---|---|---|
| `DataGridView` | `GridControl + GridView` | ✅ 15+ form |
| `ListBox` | `ListBoxControl` | ✅ `frmPhanQuyen`, `frmLichLamViec` |
| `DateTimePicker` | `DateEdit` | ✅ 10+ form |
| `NumericUpDown` | `SpinEdit` | ✅ `frmKhuVucBien`, `frmChatLuongNuoc`... |
| `SplitContainer` | `SplitContainerControl` | ✅ 6+ form |
| `ComboBox` trong Grid | `RepositoryItemComboBox` | ✅ `frmBanHang` |
| `Button` trong Grid | `RepositoryItemButtonEdit` | ✅ `frmBanHang`, `frmCombo`, `frmDatBan` |
| `NumericUpDown` trong Grid | `RepositoryItemSpinEdit` | ✅ `frmBanHang`, `frmKiemKho`, `frmThueDo` |
| `TextBox` readonly trong Grid | `RepositoryItemTextEdit` | ✅ `frmBanHang` |
| ❌ Không có | `TileView` | ✅ `frmBanHang`, `frmDatBan`, `frmMenuPopup` |
| ❌ Không có | `TreeList` với checkbox | ✅ `frmPhanQuyen` |
| ❌ Không có | `PivotGridControl` | ✅ `frmBaoCao` |
| ❌ Không có | `ChartControl` | ✅ `frmBaoCao` |
| ❌ Không có | `CircularGauge` | ✅ `frmBaoCao` |

---

## PHẦN 5 — "Câu hỏi khó" thầy hay hỏi

### ❓ "Sao không dùng DataGridView thường?"

> **Trả lời:** DataGridView không có:
> - `TileView` (hiển thị dạng card)
> - `CustomRowCellEdit` (mỗi ô dùng editor khác nhau theo logic)
> - `RepositoryItemButtonEdit` (nút trong ô)
> - Footer tổng tiền built-in (`SummaryItem`)
> - `ExportToXlsx/PDF` 1 dòng code
> - Performance tốt hơn (Painting pattern - chỉ render editor thật cho ô đang focus)

---

### ❓ "RepositoryItem và control thường khác gì?"

> **Trả lời:** 
> - Control thường = **vật thể thật** tồn tại trên form, chiếm bộ nhớ
> - RepositoryItem = **bản thiết kế (blueprint)**, DevExpress render nó theo cơ chế "Painting"
> - 1000 dòng grid → chỉ 1 editor thật, còn lại là ảnh → **không lag**

---

### ❓ "SplitContainerControl khác SplitContainer?"

> **Trả lời:** Về chức năng tương đương, nhưng DevExpress có:
> - Collapsible (thu gọn 1 bên)
> - Tích hợp skin/theme
> - API đồng nhất với phần còn lại của DevExpress

---

### ❓ "DateEdit có gì hơn DateTimePicker?"

> **Trả lời:**
> - Nullable (có thể để trống — quan trọng cho DB có cột `NULL`)
> - Format mask mạnh hơn
> - Tích hợp vào Grid qua `RepositoryItemDateEdit`
> - Spin button để tăng/giảm ngày bằng bàn phím

---

*Tài liệu tổng hợp từ codebase — 2026-04-13*
