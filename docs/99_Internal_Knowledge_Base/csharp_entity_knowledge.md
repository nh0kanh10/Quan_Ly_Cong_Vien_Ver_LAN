# 📘 C# Entity / DTO — Tổng Hợp Kỹ Thuật

> **Context**: WinForms + DevExpress GridControl + .NET Framework 4.x + 3-tier architecture
> Đánh giá ROI thực tế, không phải lý thuyết sách giáo khoa.

---

## TIER 1: DÙNG NGAY ✅

### 1.1 `[Browsable(false)]` — Ẩn cột khỏi Grid
```csharp
[Browsable(false)]
public int Id { get; set; }          // Grid không hiện
[Browsable(false)]
public int IdSanPham { get; set; }   // Grid không hiện
[Browsable(false)]
public DateTime CreatedAt { get; set; }
```
> DevExpress GridControl **đọc được**. Thay thế hoàn toàn `column.Visible = false` trong code.

---

### 1.2 `[DisplayName("...")]` — Tên cột tiếng Việt
```csharp
[DisplayName("Giá Ngày Thường")]
public decimal GiaNgayThuong { get; set; }
```
> DevExpress **đọc được**. Thay thế `column.Caption = "..."`.

---

### 1.3 `[ReadOnly(true)]` — Không cho sửa
```csharp
[ReadOnly(true)]
public string LoaiGia => PhutBlock.HasValue ? "Thuê giờ" : "Bán đứt";
```
> Dùng cho computed property hoặc cột audit.

---

### 1.4 `[Description("...")]` — Tooltip khi hover
```csharp
[Description("Để trống nếu SP bán đứt. VD: 60 = khách được dùng 60 phút đầu")]
public int? PhutBlock { get; set; }
```
> DevExpress hiện tooltip khi hover header.

---

### 1.5 `[DefaultValue(...)]` — Giá trị mặc định
```csharp
[DefaultValue(0)]
public decimal GiaNgayThuong { get; set; }

[DefaultValue("00:00")]
public TimeSpan GioBatDau { get; set; }
```
> Hữu ích khi thêm dòng mới trên grid.

---

### 1.6 `[Required]` + `[Range]` — Validation
```csharp
using System.ComponentModel.DataAnnotations;

[Required(ErrorMessage = "Bắt buộc nhập giá")]
[Range(0, 999999999, ErrorMessage = "Giá không được âm")]
public decimal GiaNgayThuong { get; set; }
```
> ⚠️ WinForms **KHÔNG** tự validate như ASP.NET MVC.
> Phải gọi thủ công ở BUS:
```csharp
var ctx = new ValidationContext(entity);
var errors = new List<ValidationResult>();
bool ok = Validator.TryValidateObject(entity, ctx, errors, true);
if (!ok) return OperationResult.Fail(string.Join("\n", errors.Select(e => e.ErrorMessage)));
```

---

### 1.7 `IDataErrorInfo` — DevExpress hiện ❌ đỏ tự động
```csharp
public class ET_BangGia : IDataErrorInfo
{
    [Browsable(false)]
    public string Error => null;

    [Browsable(false)]
    public string this[string col]
    {
        get
        {
            if (col == nameof(GiaNgayThuong) && GiaNgayThuong < 0)
                return "Giá không được âm";
            if (col == nameof(PhutBlock) && PhutBlock.HasValue && PhutBlock <= 0)
                return "Phút block phải > 0";
            if (col == nameof(GiaNgayThuong) && GiaNgayLe > 0 && GiaNgayThuong > GiaNgayLe)
                return "Giá thường đắt hơn giá Lễ?";
            return null;
        }
    }
}
```
> **DevExpress tự đọc interface này** → hiện icon lỗi trong cell. Không cần code GUI validation.
> Đây là feature mạnh nhất cho project hiện tại.

---

### 1.8 `ICloneable` — Cancel/Undo không cần reload DB
```csharp
public class ET_BangGia : ICloneable
{
    public object Clone() => MemberwiseClone();
}

// Sử dụng:
var backup = (ET_BangGia)original.Clone();  // Lưu bản gốc
// User sửa lung tung trên grid...
// User bấm Hủy → restore backup
```
> 3 dòng code, giá trị cao. Dùng khi cho edit trực tiếp trên grid.

---

### 1.9 Computed Properties — Cột tính toán tự động
```csharp
[DisplayName("Loại")]
[ReadOnly(true)]
public string LoaiGia => PhutBlock.HasValue ? "Thuê giờ" : "Bán đứt";

[DisplayName("Khung giờ")]
[ReadOnly(true)]  
public string KhungGio => $"{GioBatDau:hh\\:mm} - {GioKetThuc:hh\\:mm}";
```
> Grid hiện cột này nhưng không lưu DB. Thay thế code format ở GUI.

---

### 1.10 Extension Methods — Mở rộng ET không sửa file gốc
```csharp
// File riêng: Extensions/KhachHangExtensions.cs
public static class KhachHangExtensions
{
    public static bool LaKhachVIP(this ET_KhachHang kh)
        => kh.LoaiKhach == "VIP" || kh.DiemTichLuy > 10000;

    public static string TenVietTat(this ET_KhachHang kh)
        => string.Join("", kh.HoTen.Split(' ').Select(w => w[0]));
}

// Sử dụng:
if (khach.LaKhachVIP()) { /* ưu đãi */ }
```
> Giữ ET sạch (chỉ chứa data), logic bổ sung tách ra file riêng.

---

## TIER 2: LÀM SAU KHI DEFENSE ⏳

### 2.1 `IEquatable<T>` — Dirty tracking (biết dòng nào bị sửa)
```csharp
public bool Equals(ET_BangGia other)
{
    if (other == null) return false;
    return GiaNgayThuong == other.GiaNgayThuong
        && GiaCuoiTuan == other.GiaCuoiTuan
        && GiaNgayLe == other.GiaNgayLe;
}
```
> Hay nhưng grid chỉ vài chục dòng → reload DB đủ nhanh cho defense.

### 2.2 `Lazy<T>` — Load dữ liệu nặng khi cần
```csharp
private Lazy<List<ET_GiaoDich>> _giaoDich;
public List<ET_GiaoDich> GiaoDich => _giaoDich.Value;
```
> Có ích khi danh sách lớn (>1000 items). Project hiện tại ~50 SP → chưa cần.

### 2.3 Indexers — Truy cập kiểu `entity["TenCot"]`
```csharp
private Dictionary<string, object> _extra = new Dictionary<string, object>();
public object this[string key]
{
    get => _extra.ContainsKey(key) ? _extra[key] : null;
    set => _extra[key] = value;
}
```
> Hữu ích cho Custom Fields (thuộc tính mở rộng). Chưa cần cho defense.

---

## TIER 3: BỎ QUA ❌

| Feature | Lý do bỏ |
|---|---|
| `INotifyPropertyChanged` | WPF cần, WinForms không. BindingList đủ dùng. Thêm = boilerplate khổng lồ |
| `IComparable<T>` | LINQ `.OrderBy()` linh hoạt hơn. IComparable lock cứng 1 cách sort |
| Operator Overloading (`+`) | Show-off. Business logic không phải phép toán |
| Custom Attributes + Reflection | Over-engineering. Viết audit thẳng ở BUS đơn giản hơn |
| Implicit/Explicit Conversion | **NGUY HIỂM**: `ET_KhachHang kh = 1001` ẩn DB call trong toán tử. Anti-pattern debug nightmare |
| `[DisplayFormat]` | DevExpress WinForms **không đọc** attribute này. Chỉ đúng cho ASP.NET MVC |
| C# 9 Records | Project dùng **.NET Framework 4.x** → records không tồn tại. Cần .NET 5+ |

---

## TEMPLATE: ET class chuẩn cho project

```csharp
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ET
{
    public class ET_Example : IDataErrorInfo, ICloneable
    {
        // ═══ PK + FK: ẨN ═══
        [Browsable(false)]
        public int Id { get; set; }

        // ═══ DATA: HIỆN + VALIDATE ═══
        [DisplayName("Tên hiển thị")]
        [Required(ErrorMessage = "Bắt buộc")]
        public string TenField { get; set; }

        [DisplayName("Số tiền")]
        [Range(0, 999999999, ErrorMessage = "Không được âm")]
        [DefaultValue(0)]
        public decimal SoTien { get; set; }

        // ═══ COMPUTED: CHỈ ĐỌC ═══
        [DisplayName("Trạng thái")]
        [ReadOnly(true)]
        public string TrangThaiHienThi => /* logic */;

        // ═══ AUDIT: ẨN ═══
        [Browsable(false), ReadOnly(true)]
        public DateTime CreatedAt { get; set; }

        // ═══ IDataErrorInfo ═══
        [Browsable(false)] public string Error => null;
        [Browsable(false)]
        public string this[string col]
        {
            get
            {
                // Validate logic ở đây
                return null;
            }
        }

        // ═══ ICloneable ═══
        public object Clone() => MemberwiseClone();
    }
}
```

---

> **Nguyên tắc**: ET không chỉ là túi chứa data.
> Nhưng cũng **đừng biến ET thành God Object**.
> Chỉ đặt vào ET những gì liên quan trực tiếp đến **hiển thị** và **validation**.
> Business logic vẫn ở BUS.
