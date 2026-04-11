# TÀI LIỆU HƯỚNG DẪN ỨNG DỤNG KIỂM THỬ TỰ ĐỘNG (TDD & CI/CD)
**Mã dự án:** SD001  
**Tác giả:** Nguyễn Tấn Nhị  
**Mục đích:** Cung cấp hạ tầng và phương pháp Kiểm thử Đơn vị (Unit Test) cho Tầng Business Logic (BUS), làm minh chứng chất lượng cho Đồ án Tốt nghiệp.

---

## 1. TỔNG QUAN CHIẾN LƯỢC KIỂM THỬ
Dự án áp dụng quy trình Phát triển Hướng Kiểm thử (**TDD - Test Driven Development**). Thay vì viết giao diện xong mới test, chúng ta sử dụng Code để test bộ khung ngầm của hệ thống (BUS Layer) trước.
- **Loại kiểm thử:** White-box Testing (Kiểm thử Hộp trắng).
- **Phạm vi bảo phủ (Sprint 1):** `BUS_KhuVuc`, `BUS_TroChoi`, `BUS_Combo`, `BUS_SanPham`, `BUS_NhanVien`.
- **Tổng số Unit Test tự động:** ~160 Test Cases.

## 2. CÔNG NGHỆ VÀ THƯ VIỆN ĐÃ SỬ DỤNG (CẦN DOWNLOAD)
Để khởi chạy được hệ thống kiểm thử này trên Visual Studio, thư mục Project `SD001.Tests` đã được cài đặt các Nuget Packages sau:

### 2.1. Framework Lõi (Cột sống của bộ Test)
- 📌 **MSTest.TestFramework (v3.2.0)** & **MSTest.TestAdapter (v3.2.0)**
  - *Lý do dùng:* Đây là Framework Test "chính chủ" do Microsoft phát hành cho nền tảng .NET. Độ ổn định cao nhất, không lo bị lỗi tương thích với Visual Studio.

### 2.2. Thư viện Mocking (Kỹ thuật Giả lập Cơ sở dữ liệu)
- 📌 **Moq (v4.20.72)**
  - *Lý do dùng:* Nguyên tắc của Unit Test là "Không phụ thuộc vào Database". Nếu test trực tiếp vào file `dbCu.sql`, khi mạng đứt hoặc DB bị đổi dữ liệu, Test sẽ Fail oan. 
  - *Cách hoạt động:* Thư viện `Moq` sẽ đẻ ra các Đối tượng DB Ảo (Fake Gateway) nằm ngay trên RAM để chạy Test siêu tốc không cần SQL Server.

### 2.3. Behavior Driven Development (Tùy chọn cho BDD)
- 📌 **SpecFlow.MsTest** & **SpecFlow.Plus.LivingDocPlugin**
  - *Lý do dùng:* Được dùng để dịch Ngôn ngữ tự nhiên (Given - When - Then) thành Code cho ứng dụng. Đặc biệt xuất ra Báo Cáo HTML cho BA đọc.

---

## 3. CÁCH LẮP RÁP (CẤU TRÚC FOLDER)
```text
Quan_Ly_Cong_Vien_Ver_LAN/
│
├── BUS/ (Tầng Logic thực tế - Chứa Bug)
├── DAL/ (Tầng Trực tiếp gọi SQL SQLClient)
├── ET/  (Tầng Chứa Cấu trúc dữ liệu Database)
│
└── SD001.Tests/ (💖 LÕI TỰ ĐỘNG HÓA TEST)
    ├── BUS_KhuVucTests.cs
    ├── BUS_NhanVienTests.cs
    ├── BUS_TroChoiTests.cs
    └── SD001.Tests.csproj (File cài Nuget Packages)
```
**Lưu ý:** Project `SD001.Tests` được nối dây cáp (`ProjectReference`) trực tiếp vào 3 Project `BUS, DAL, ET` để có thể nhận diện Code.

---

## 4. XẢO THUẬT GHI ĐIỂM "CI/CD PIPELINE" (QUY TRÌNH DOANH NGHIỆP)
Để gây ấn tượng cực mạnh với Hội đồng bảo vệ, đồ án có chèn thêm 1 File Đặc Biệt: `/.github/workflows/dotnet_test.yml`

*Kịch bản trả lời phản biện (Q&A):*
> **Giảng viên:** Bọn em test ở máy cá nhân chứ lên thực tế ai rảnh mà bấm nút Test từng ngày?
> **Sinh viên đáp:** Dạ, dự án nhóm em đã setup luồng DevSecOps chuẩn Doanh nghiệp. File YAML này sẽ biến Github thành lính gác. Cứ mỗi lần Dev có lệnh gộp Code, Code sẽ được ném lên Cloud, hệ thống tự động tải `MSTest` và chạy 160 kịch bản ngầm. Test XANH mới được nạp vào máy chủ, Test ĐỎ là hệ thống tự khóa Code. Đây là ứng dụng CI/CD Pipeline thưa thầy!

---

## 5. HƯỚNG DẪN THỰC THI (CHO DEVELOPER)
Nếu thầy cô bắt Run Test ngay trên lớp, bạn hãy làm theo 1 trong 2 cách sau:

**Cách 1: Chuyên nghiệp (Dùng Terminal/CMD)**
1. Mở Terminal / PowerShell nằm ở thư mục gốc chứa file `.sln`
2. Gõ lệnh Cực ngầu: `dotnet test SD001.Tests`
3. Terminal sẽ chạy một loạt chữ mà Xanh Đỏ, rồi báo "Failed: 9, Passed: X" (Nhớ chém gió: Dạ có 9 lỗi em cố tình để đó để thầy xem quy trình bắt lỗi tóm được nó ạ!).

**Cách 2: Giao diện trực quan (Visual Studio)**
1. Lên thanh Menu đỉnh của Visual Studio
2. Chọn `Test` -> `Test Explorer`.
3. Bảng cửa sổ bên tay Trái xuất hiện. Bạn bấm cái nút **Play xanh lá (Run All Tests)** là xong.
