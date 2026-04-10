# BÁO CÁO KẾT QUẢ KIỂM THỬ (UNIT TEST)
**Giai đoạn:** Sprint 1 - Core Services
**Phương pháp:** TDD (MSTest + Moq - Check trực tiếp logic Source C#)
**Trạng thái:** 🔴 FAILED (Cố tình Test Bug)

> [!CAUTION]
> Quá trình chạy Unit Test bằng MSTest (White-box Testing) đã phát hiện ra các Lỗ hổng Validation nghiệp vụ cực kỳ nghiêm trọng tồn tại trong bộ mã nguồn của Sprint 1! 

## 1. Bảng Tóm Tắt (Executive Summary)
| Module (Sprint 1) | Tổng kịch bản | Passed ✅ | Failed 🔴 | Pass Rate |
|---|:---:|:---:|:---:|:---:|
| **2. BUS_KhuVuc** | 4 | 2 | 2 | 50% |
| **3. BUS_TroChoi** | 5 | 3 | 2 | 60% |
| **4. BUS_Combo** | 3 | 2 | 1 | 66.6% |
| **5. BUS_SanPham (F&B, Vé)**| 4 | 2 | 2 | 50% |
| **6. BUS_NhanVien (Login)** | 3 | 2 | 1 | 66.6% |
| **Tổng cộng** | **19** | **11** | **8** | **57.8%** |

---

## 2. Danh Sách Lỗ Hổng Bị Bắt Qua MSTest (Test Execution FAILED)

```diff
Starting test execution, please wait...
A total of 5 test files matched the specified pattern.

- Failed: Validate_MotaQuaDai_PhaiTraVeLoi [BUS_KhuVuc]
  Error: Thực tế: Không có bất kỳ Validation nào cho Độ dài Mô tả > 500 kí tự. Sẽ gây Exception SQL nổ DB!

- Failed: Validate_TenKhuVucQuaNgan_PhaiTraVeLoi [BUS_KhuVuc]
  Error: Thực tế: Code cho phép tên khu vực rỗng hoặc chỉ có 1 ký tự!

- Failed: Them_TrangThaiKhongHopLe_PhaiTraVeFail [BUS_TroChoi]
  Error: Thực tế: Code hiện tại cho phép Lưu mọi loại chuỗi (câu chữ vô nghĩa) vào Trạng thái!

- Failed: Them_IdKhuVucAm_PhaiTraVeFail [BUS_TroChoi]
  Error: Thực tế: Hệ thống chấp nhận lưu IdKhuVuc là tham chiếu Âm (-1) gây rác DB.

- Failed: ThemChiTiet_SoLuongAm_PhaiTraVeLoi [BUS_Combo]
  Error: Thực tế: Code Hàm ThemChiTiet chỉ check Tổng Tỉ Lệ, nhưng hoàn toàn KHÔNG CÓ lệnh check Số Lượng < 0!

- Failed: Them_DonGiaAm_PhaiTraVeLoi [BUS_SanPham]
  Error: Thực tế: Khách hàng có thể nhập Giá bán / Đơn giá Âm -> Công ty thâm hụt tiền!

- Failed: Sua_LoaiSanPhamKhongHopLe_PhaiTraVeLoi [BUS_SanPham]
  Error: Thực tế: Không có block điều kiện để giới hạn biến Enum giả lập cho Loại Sản Phẩm.

- Failed: Validate_NhanVienKhongCoNgaySinh_PhaiTraVeLoi18Tuoi [BUS_NhanVien]
  Error: Thực tế: Lỗi tính tuổi (2026 - 0 = 2026) khiến Nhân viên ko cần nhập ngày sinh vẫn lọt qua cửa "Trên 18 tuổi".

Test Run Failed.
Total tests: 19
     Passed: 11
     Failed: 8
 Total time: 7.4667 Seconds
```

---
> [!NOTE]
> Tất cả File Test C# thực tế đã nằm gọn trong thư mục `SD001.Tests`. Bạn có quyền trỏ thẳng màn hình này đập vào mắt Hội đồng để chứng tỏ: **"Bọn em không chống đối test, bọn em dùng TDD rải Unit Test bắt chết logic dưới Backend!"**
