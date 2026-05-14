# Quy Chuẩn Lập Trình C# Dự Án Đại Nam (C# Coding Conventions)

Để đảm bảo toàn bộ mã nguồn của dự án đồng bộ, dễ đọc và dễ bảo trì khi dự án scale up, mọi lập trình viên (và AI) khi tham gia dự án phải tuân thủ nghiêm ngặt các quy tắc sau:

## 1. Xử lý Chuỗi (String Handling)

### 1.1. Ghép chuỗi & Định dạng nội suy (String Interpolation)
- **Bắt buộc** sử dụng String Interpolation `$"{variable}"` thay vì `string.Format("{0}", variable)` hoặc cộng chuỗi `+`.
- *Ngoại lệ duy nhất:* Khi truyền tham số đa ngôn ngữ qua hàm `LanguageManager.GetString("KEY")`, vì resource lưu chuỗi format `{0}` nên sẽ dùng `string.Format(LanguageManager.GetString("KEY"), param1)`.
- **Ví dụ đúng:**
  ```csharp
  // Nội bộ code C#
  string msg = $"Lỗi: Mã sản phẩm {sp.MaSanPham} đã tồn tại.";
  // Giao tiếp qua Resource (Đa ngôn ngữ)
  string msg = string.Format(LanguageManager.GetString("ERR_TRUNG_MASP"), sp.MaSanPham);
  ```

### 1.2. Kiểm tra chuỗi rỗng
- Luôn sử dụng `string.IsNullOrWhiteSpace(str)` để kiểm tra chuỗi rỗng hoặc toàn khoảng trắng. Tránh dùng `str == ""` hay `str.Length == 0`.

## 2. Chuyển đổi Kiểu Dữ Liệu (Type Conversion)

### 2.1. Parse Dữ Liệu
- Khi nhận dữ liệu từ UI (TextBox, DataGrid) mà không chắc chắn về format, **bắt buộc dùng `TryParse`** thay vì `Convert.ToInt32()` hoặc `int.Parse()` để tránh Exception làm sập app.
- **Ví dụ đúng:**
  ```csharp
  if (int.TryParse(txtSoLuong.Text, out int soLuong)) {
      // Xử lý logic
  }
  ```

### 2.2. Ép kiểu an toàn (Safe Casting)
- Sử dụng toán tử `as` kết hợp kiểm tra `null` hoặc pattern matching `is` thay vì ép kiểu trực tiếp `(Type)obj`.
- **Ví dụ đúng:**
  ```csharp
  if (sender is BaseEdit edit) {
      edit.EditValue = null;
  }
  ```

## 3. Quản lý Ngoại Lệ & Lỗi (Error Handling)

### 3.1. Tầng BUS (Nghiệp vụ)
- Không dùng `throw new Exception()` để ném lỗi nghiệp vụ thông thường.
- Luôn trả về đối tượng `OperationResult.Fail("MÃ_LỖI|ThamSo1")`.
- **Ví dụ đúng:**
  ```csharp
  return OperationResult.Fail($"ERR_TRUNG_MASP|{maSanPham}");
  ```

### 3.2. Tầng GUI (Hiển thị UI)
- Tại tầng GUI, khi nhận `OperationResult`, cần tách chuỗi bằng `.Split('|')` và gọi `LanguageManager.GetString()` để hiển thị.
- Hạn chế tối đa việc viết Hardcode tiếng Việt trong UI như `MessageBox.Show("Lưu thành công");`. Bắt buộc dùng `UIHelper.ThongBao(LanguageManager.GetString("MSG_LUU_THANH_CONG"));`.

## 4. Quy Tắc Hàm Sự Kiện (Event Handlers)

### 4.1. Không dùng Lambda Ẩn Danh
- **CẤM** sử dụng hàm nặc danh (anonymous lambda) khi đăng ký sự kiện UI. Ví dụ: `btn.Click += (s, e) => { ... }`.
- Lý do: Gây rác code-behind, khó gỡ lỗi (debug), và làm công cụ thiết kế (Designer) khó đồng bộ.
- **Bắt buộc:** Phải đăng ký sự kiện thông qua tab Events trong Properties ở file `[TênForm].Designer.cs` và khai báo hàm ở file `[TênForm].cs`. Đối với code tạo động, phải dùng toán tử `+=` với một tên hàm tường minh.
- **Ví dụ Đúng:**
  btn.Click += BtnThoat_Click;
  
  private void BtnThoat_Click(object sender, EventArgs e) {
      // Xử lý
  }
  ```

### 4.2. Khai báo Control và Event tại `.Designer.cs`
- **Tuyệt đối không** sinh các Control UI (như GridColumn, RepositoryItem, Button) hoặc gắn sự kiện (`+=`) tại Runtime trong code-behind `.cs` trừ khi thực sự bất khả kháng (vd: UI động hoàn toàn).
- Mọi thiết lập UI, Properties và Gắn Event (Delegate) **phải nằm ở file `.Designer.cs`** (đặt trong vùng `InitializeComponent()`). Điều này đảm bảo:
  - File `.cs` hoàn toàn sạch, chỉ tập trung xử lý Business Logic và thao tác với `DataSource`.
  - Công cụ Visual Studio Designer có thể Render và Preview chính xác 100% giao diện lúc thiết kế.
- **Ví dụ đúng (Tại file .Designer.cs):**
  ```csharp
  this.btnThemDong.Click += new System.EventHandler(this.BtnThemDong_Click);
  ```
và phải ghi chú như visual tự dộng có thể tham khảo frm khác để xem ví dụ ghi chú
## 5. Kiến Trúc 3 Lớp

- **Tầng GUI:** Không bao giờ chứa logic truy vấn DB bằng LINQ hay thao tác DAL. Chỉ gọi qua `BUS.Instance`.
- **Tầng BUS:** Không tham chiếu đến bất kỳ class UI nào (WinForms, DevExpress.XtraEditors).
- **Lưu ý vòng đời:** LINQ-to-SQL `DataContext` thường được đóng ngay sau truy vấn. Do đó ở DAL/BUS, bắt buộc phải `.ToList()` hoặc map sang đối tượng thuần (`ET_`) trước khi trả về cho UI, để tránh lỗi `ObjectDisposedException`.
