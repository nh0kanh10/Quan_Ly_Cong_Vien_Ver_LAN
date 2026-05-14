---
name: coding-conventions
description: Các quy chuẩn cốt lõi (Coding Conventions) khi viết code cho dự án Quản Lý Công Viên Đại Nam (C# WinForms). Áp dụng bắt buộc trước khi tạo hoặc sửa code giao diện và logic.
---

# 📋 CODING CONVENTIONS — Đại Nam Park Management System

> **Mục đích**: Đây là Single Source of Truth cho coding style của dự án.
> **AI Instruction**: KHÔNG ĐƯỢC VI PHẠM BẤT KỲ ĐIỀU NÀO DƯỚI ĐÂY KHI GENERATE CODE.

## I. KIẾN TRÚC 3-TIER BẮT BUỘC
- **ET (Entity):** Chứa POCO classes, không chứa logic. Mọi magic string ở bài đặt ở đây (trong `AppConstants`).
- **DAL (Data Access Layer):** LINQ to SQL qua DataContext. Áp dụng chuẩn Singleton `DAL_...Instance`.
- **BUS (Business Logic):** Chứa Validation. Áp dụng chuẩn Singleton `BUS_...Instance`. Luôn trả về `OperationResult`.
- **GUI (WinForms):** Frontend sử dụng DevExpress (GridControl, SearchLookUpEdit, v.v.). **GUI KHÔNG ĐƯỢC PHÉP CHẠM TRỰC TIẾP VÀO DAL**.

## II. ĐẶT TÊN — LUẬT SẮT
**Classes & Files**:
- Entity: `ET_[TenBang]` | DTO: `DTO_[MucDich]` 
- DAL: `DAL_[TenBang]` | BUS: `BUS_[NghiepVu]`
- Form: `frm[TenChucNang]` | UserControl: `uc[TenControl]`
- Constants: `TrangThai_[Entity]` (Ví dụ: `TrangThai_DonHang`)

**Controls trên Form (prefix bắt buộc)**:
- `txt`: TextEdit | `cbo`: ComboBoxEdit | `slk`: SearchLookUpEdit | `btn`: SimpleButton
- `lbl`: LabelControl | `gb`: GroupControl | `grid` / `gridView`: GridControl / GridView
- `pnl`: PanelControl | `dt`: DateEdit | `chk`: CheckEdit | `spin`: SpinEdit | `pic`: PictureEdit

## III. VIẾT CODE & SỰ KIỆN (EVENT HANDLING)
1. **Magic Strings (CẤM)**: Không if/else với text cứng (VD: `TrangThai == "ChoThanhToan"`). Bắt buộc lấy từ hằng số `AppConstants`.
2. **TabControl**: Cấm dùng nguyên bản, khuyến khích tối đa 10 tab để chống tràn RAM.
3. **Layout**: Tuyệt đối không dùng toạ độ tuyệt đối. Dùng `SplitContainer`, `Dock`, hoặc `TableLayoutPanel`.
4. **Hàm Event**: CẤM dùng thẻ nặc danh lambda: `btn.Click += (s, e) => { ... }`. Phải đăng ký hàm ở `Designer.cs` và khai báo hàm ở Code-behind.
5. **Bắt Lỗi ở BUS**: BUS KHÔNG ĐƯỢC DÙNG DÒNG LỆNH `throw new Exception`, mà phải **bắt lỗi và trả về** một giá trị kiểu hộp đựng `OperationResult.Fail("lỗi")`. Trên GUI mới kiểm tra `Result.Success` rồi xuất thông báo.
6. Nghiên cấm tự ý in hoa  ghi chú 

7. **Luân tuân thủ tách rời các file không không làm biếng để chung <-  Cực kỳ quan trọng ........**

## IV. QUY TẮC COMMENT & ĐA NGÔN NGỮ (I18N)
1. **Tiếng Việt & Thuật ngữ cho Sinh Viên**: Code viết cho sinh viên đại học. Khuyến khích comment bằng Tiếng Việt thân thiện. **CẤM** dùng các thuật ngữ kỹ thuật đậm chất Senior/Kỹ sư làm màu (Ví dụ: Interceptor, Wipe-and-Replace, Eager Loading...). Hãy dùng ngôn từ mô tả dân dã ("Chặn không cho sửa", "Xóa chi tiết cũ rồi lưu lại", "Kết nối nhiều bảng").
2. **Dùng XmlDoc (///)**: Bắt buộc dùng thẻ `<summary>`, kèm `<param>` và `<returns>` với các hàm quan trọng để hỗ trợ nhắc lệnh thông minh trong Visual Studio.
3. **Giải thích Logic**: Giải thích CÁCH TÍNH TOÁN và TẠI SAO (ví dụ quy trình nhiều bước, tính toán giá thuê phức tạp, giao dịch liên tục). Không comment những hàm Tầm Thường (như CRUD GetAll, Them, Xoa).
4. **TUYỆT ĐỐI KHÔNG HARD-CODE ERROR TEXT BẰNG TIẾNG VIỆT**: Trong `DAL/BUS`, khi quăng lỗi hoặc báo thành công qua `OperationResult.Fail() / Ok()`, KHÔNG ĐƯỢC NHẢ TEXT ĐÍCH THỊ TIẾNG VIỆT MÀ **PHẢI NÉM RA KEY DỊCH THUẬT** (Ví dụ `ERR_KHO_MA_RONG` hoặc `MSG_LUU_OK`).
5. **Cắm Key vào Resx**: UI sẽ hứng Key đó và đưa qua `LanguageManager`. Bắt buộc phải bổ sung Key cẩn thận vào `GUI/Properties/UIStrings.resx` và các file ngoại ngữ `.en-US.resx` và `.zh-CN.resx` tương ứng.
6.- ❌ Lambda event `btn.Click += (s,e) => {}` → ✅ Đăng ký trong .Designer.cs
## V. QUY CHUẨN VỀ REGION CỦA CÁC LỚP
**Region chuẩn cho file BUS / DAL**:
```csharp
#region Khởi tạo (hoặc cấu trúc Singleton)
#endregion
#region Truy vấn dữ liệu
#endregion
#region Thêm / Sửa / Xoá
#endregion
#region Nghiệp vụ đặc thù
#endregion
```

**Region chuẩn cho Code-behind Form**:
```csharp
#region Khởi tạo và tải dữ liệu
#endregion
#region Xử lý sự kiện (Click, SelectedChanged...)
#endregion
#region Kiểm tra dữ liệu nhập (Validation)
#endregion
#region Hàm hỗ trợ 
#endregion
```

## VI. BỘ ICON - THƯ VIỆN DEV-EXPRESS SVG
**Tuyệt đối cấm tải icon ngoài ném vào form**. Phải dùng SVG Image Gallery có sẵn của DevExpress.
Gắn qua Code (`ImageUri` tự động tìm Icon):
- `Add` (Thêm)
- `Edit` (Sửa)
- `Delete` (Xoá)
- `Save` (Lưu)
- `Cancel` (Hủy)
- `Find` (Tìm kiếm)
- `Refresh` (Làm mới)
- `Print` (In)
- `ExportToXLSX` (Xuất Excel)

## VII. NHỮNG BÀI HỌC KIẾN TRÚC & SAI LẦM AI THƯỜNG MẮC PHẢI (MUST AVOID)
1. **Sai Cấu trúc C# (.csproj)**: Trong project WinForms (.NET Framework), khi tạo file mới bằng script (vd: `DAL_KhuVuc.cs`), TUYỆT ĐỐI không chỉ tạo file vật lý mà PHẢI parse và nhét `<Compile Include="..."/>` vào file `.csproj` tương ứng. Nếu quên, project sửa code chán chê xong MSBuild sẽ báo "không tồn tại namespace".
2. **Quên Namespaces (`using`)**: Thường xuyên gọi `.FirstOrDefault()`, `.Select()`, `.Where()` nhưng hay QUÊN bổ sung `using System.Linq;` lên đỉnh C# files.
3. **Nhầm mục đích `HeThong.TuDien` & `UIStrings.resx`**:
   - `UIStrings.resx`: Nơi chứa Text tĩnh của App (Button, Tiêu đề cột, Thông báo tĩnh).
   - `HeThong.TuDien`: Nơi cấu hình TRẠNG THÁI NGHIỆP VỤ (Enums) động dưới Database kèm thông tin cấu hình như `MauSac`, `BieuTuong`.
   - **SAI LẦM CỦA AI**: Thích ôm hết các Enum bỏ vào `.resx` (đẻ ra logic `LanguageManager.GetString("TRANGTHAI_"...)`) và bắt UI lấy giá trị text mà LỜ ĐI KHÔNG SỬ DỤNG bảng TuDien. Dẫn tới màn hình bị "mù màu" vì mất thuộc tính cấu hình màu sắc từ DB.
   - **GIẢI PHÁP ĐÚNG**: UI Control (Combox, Grid) ưu tiên bốc dữ liệu List Config từ `TuDien`. Để load hiển thị: Ưu tiên bắt Key = Mã đẩy vào `LanguageManager.GetString`. Nếu nó trả Rỗng/Trùng Key => Nghĩa là không có cấu hình ResX, hãy lấy fallback là `NhanHienThi` tiếng Việt từ TuDien dưới DB.
4. **Vi Phạm Single Responsibility Principle (SRP)**: Dễ tiện tay gộp Domain độc lập vào chung tầng quản lý khác. Ví dụ viết logic Lấy danh sách `KhuVuc` bỏ tuốt vào `DAL_SanPham` vì nghĩ "Tí form Sản phẩm nó dùng". Phải tách bạch: Area có `DAL_KhuVuc` và `BUS_KhuVuc` riêng, Form Sản Phẩm khi cần thì Import cắm `BUS_KhuVuc` vào.
5. **Format file `.Designer.cs` sai lệch với Visual Studio Designer**: AI hay lười và ném tất cả các Property/Controls.Add vào 1 cục bừa bãi hoặc ghi chú sai chuẩn `// Ghi chú`.
   - **MUST DO**: Cấu trúc method `InitializeComponent()` trong file `.Designer.cs` PHẢI đúng trình tự Native của Visual Studio: 
     (1) Khởi tạo `new Control()`
     (2) Gọi `BeginInit()` của các control
     (3) Định nghĩa Property (Size, Location, Text...) của từng Control kèm chú thích `// \n// controlName\n// `
     (4) Khai báo `this.Controls.Add(...)` hoặc `.Panel.Controls.Add(...)` ở cuối cùng
     (5) Các lệnh `EndInit()` và `ResumeLayout()`.
     Toàn bộ file phải bọc bằng `#region Component Designer generated code` (với UserControl) hoặc `#region Windows Form Designer generated code` (với Form). Khởi tạo hàm `Dispose(bool disposing)` chuẩn hóa phía trên region này.
