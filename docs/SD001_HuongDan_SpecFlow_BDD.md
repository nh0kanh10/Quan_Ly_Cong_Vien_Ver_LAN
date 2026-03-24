# Hướng dẫn quy trình Test BDD với SpecFlow cho dự án Đại Nam (SD001)

Tài liệu này hướng dẫn chi tiết từng bước (Step-by-step) để áp dụng Behavior-Driven Development (BDD) cho dự án C# WinForms, từ lúc phân tích yêu cầu đến lúc xuất báo cáo trang Web.

---

## TỔNG QUAN QUY TRÌNH (The BDD Workflow)

Quy trình BDD gồm 4 cột mốc chính:
1. **Khách hàng / BA / Tester / Giáo viên:** Viết kịch bản tính năng bằng tiếng Việt (`.feature`).
2. **Tự động hóa:** SpecFlow phân tích kịch bản và báo cáo phần nào chưa được cài đặt.
3. **Developer:** Viết Code C# (Step Definitions) để thực thi các câu tiếng Việt đó.
4. **Hệ thống:** Chạy kiểm thử tự động toàn bộ và xuất báo cáo Web (LivingDoc).

---

## CHI TIẾT CÁC BƯỚC THỰC HIỆN

### Bước 1: Cài đặt công cụ nền tảng
*Yêu cầu: Đã cài Visual Studio và .NET SDK.*

1. Mở Terminal / PowerShell tại thư mục đồ án chứa file Test (`SD001.Tests`).
2. Cài thư viện SpecFlow xUnit/MsTest:
   ```cmd
   dotnet add package SpecFlow.MsTest --version 3.9.74
   ```
3. Cài công cụ xuất báo cáo Web trực quan của SpecFlow:
   ```cmd
   dotnet add package SpecFlow.Plus.LivingDocPlugin --version 3.9.57
   dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
   ```

### Bước 2: Viết kịch bản bằng Gherkin (.feature)
Tất cả kịch bản (Scenario) được viết bằng tiếng người (Gherkin syntax). Nên dùng VS Code hoặc Visual Studio, cài thêm Extension `SpecFlow for Visual Studio` để được hỗ trợ tự động gợi ý code (IntelliSense).

1. Tạo file `.feature`, ví dụ `POS_BanHang.feature` nằm trong thư mục `Features/`.
2. Theo format chuẩn `Given` (Giả định), `When` (Hành động), `Then` (Kết quả):
   ```gherkin
   @SD-027 @happy-path
   Scenario: Số lượng không hợp lệ
     Given hệ thống có sản phẩm "Vé người lớn" mã "SP001" giá 200000
     When thêm -1 sản phẩm "SP001" vào đơn hàng
     And thanh toán bằng "TienMat"
     Then thao tác bị từ chối vì "Số lượng phải lớn hơn 0"
   ```

### Bước 3: Chạy Test tự động sinh mã
Mọi câu lệnh Gherkin lúc này hệ thống chưa hiểu. Hãy chạy Test lần đầu tiên.
1. Chạy lệnh ở terminal: `dotnet test`
2. Bạn sẽ thấy Test bị Failed và log báo lỗi bằng chữ màu vàng: `Pending! No matching step definition found...` kèm theo mã `C#` mẫu.
3. Chép toàn bộ đoạn mã C# mẫu mà máy tạo ra vào bộ nhớ.

### Bước 4: Chắp nối Gherkin với C# (Step Definitions) 
Lúc này Developer đóng vai trò "Thông dịch viên" giữa Tiếng Việt và Hệ thống.

1. Tạo file C# mới, ví dụ `POS_BanHangSteps.cs` nằm trong thư mục `Steps/`.
2. Dán mã mẫu hồi nãy vào. Sửa logic bên trong để nó gọi các dịch vụ gốc của phần mềm. Ví dụ:
   ```csharp
   [When(@"thêm (.*) sản phẩm ""(.*)"" vào đơn hàng")]
   public void WhenThemSanPhamVaoDonHang(int soLuong, string maSP)
   {
       // Gọi code logic thật của hệ thống BUS_DonHang để kiểm tra
       bus.ThemDonHang(soLuong, maSP);
   }
   ```
3. Từ nay về sau, nếu file `.feature` có xài lại dòng `When thêm xx sản phẩm "SPxxx" vào đơn hàng` thì không cần viết lại Bước 4 nữa.

### Bước 5: Chạy kiểm thử tự động & Xuất báo cáo Web (LivingDoc)
1. Giờ đây C# và Tiếng Việt đã hòa làm 1. Chạy lệnh Test tổng:
   ```cmd
   dotnet test --logger "console;verbosity=normal"
   ```
   Hệ thống sẽ chạy song song các file Tiếng Việt cùng các Unit Tests bằng C#.
2. Lệnh xuất báo cáo trang Web:
   ```cmd
   livingdoc test-assembly "bin\Debug\net472\SD001.Tests.dll" -t "bin\Debug\net472\TestExecution.json" -o "SD001_LivingDoc.html"
   ```
3. Mở file HTML vừa tạo ra bằng trình duyệt để xem kết quả trực quan (Analytics + Document).

---

## CÁCH TÍCH HỢP VỚI JIRA / CONFLUENCE

Trong môi trường chuyên nghiệp, báo cáo LivingDoc này sẽ không nằm cứng trong máy tính Developer mà được nối thẳng vào hạ tầng quản lý:

### 1. Đồng bộ ngầm với Jira (Traceability)
- **Tag dính liền Jira Ticket:** Bạn để ý thấy chữ `@SD-027` trên đỉnh các khối `Scenario` không? Đó chính là thẻ liên kết.
- Khi cài ứng dụng **Zephyr Scale** hoặc **Xray** trong Jira, mỗi khi bạn chạy `dotnet test`, hệ thống CI/CD sẽ gửi kết quả `.json` lên mạng.
- Lúc sếp mở Jira ticket mã `#SD-027` (Cải tiến POS bán hàng), Jira sẽ tự động nhúng 1 khung thông báo *"Tính năng này có 4 kịch bản BDD, Pass cả 4! Máy chủ xác nhận."*

### 2. Nối lên Confluence (Living Documentation)
- **Thực tế đồ án môn học:** Việc kết nối CI/CD (như Jenkins hay Azure DevOps) cho C# Winform đồ án là hơi tốn kém thời gian thao tác. Cách tối ưu cho sinh viên là chạy ra file HTML như trên, dùng Macro `Iframe` hoặc **đính kèm thẳng file HTML gốc** vào trang Confluence `ADR-005: Kiểm thử Tự động` để giảng viên tải về chạy 1 click.
- **Thực tế công ty:** Sử dụng plugin `SpecFlow+ LivingDoc` cho nền tảng Azure DevOps / Jira. Mỗi khi Developer commit sửa code, server tạo ngay 1 trang web báo cáo và ghim thẳng 1 Tab tên là "BDD" vào trong dự án Jira của cả phòng.
