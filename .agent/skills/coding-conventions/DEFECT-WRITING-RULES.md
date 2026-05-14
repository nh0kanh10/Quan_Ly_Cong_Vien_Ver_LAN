# DEFECT LIST WRITING RULES

Tài liệu này quy định các tiêu chuẩn, định dạng và văn phong bắt buộc khi AI hoặc QA viết Báo cáo lỗi (Defect List) cho dự án phần mềm (đặc biệt là các dự án WinForms 3-tier / Đồ án sinh viên).

## 1. NGUYÊN TẮC TRACEABILITY (TRUY XUẤT NGUỒN GỐC)
- **Tuyệt đối không bịa đặt (No Hallucination):** Các trường thông tin gốc từ Test Case bao gồm `TC'ID`, `Pre-condition`, `Steps`, và `Expected Results` phải được **copy y nguyên 100%** từ tài liệu Test Case sang Defect List.
- **Tính liên kết:** Một Defect phải luôn được map với một Test Case ID cụ thể để Dev có thể tái hiện lỗi chính xác.

## 2. VĂN PHONG VÀ NGÔN TỪ CHUYÊN NGHIỆP (PROFESSIONAL LANGUAGE)
> [!CAUTION]
> Tuyệt đối **KHÔNG** dùng các từ ngữ "chợ búa", thiếu chuyên môn, hoặc ảo tưởng sang kiến trúc Web/Cloud nếu đang làm WinForms.
- ❌ **CẤM DÙNG:** "đơ", "văng", "im lặng", "treo máy", "gọi API" (nếu là WinForms thuần), "không có mạng" (nếu là kết nối LAN/SQL), "tải ảnh 10MB" (nếu chức năng không có).
- ✅ **NÊN DÙNG:** 
  - "Phát sinh ngoại lệ (Exception / NullReferenceException...)"
  - "Hệ thống không phản hồi (Not Responding)"
  - "Mất kết nối cơ sở dữ liệu (Database Connection Timeout)"
  - "Lỗi ràng buộc dữ liệu (Constraint / SqlException)"
  - "Thiếu validation (Required / MaxLength / MinValue...)"

## 3. NGUYÊN TẮC KẾT QUẢ THỰC TẾ (ACTUAL RESULTS)
Cột Kết quả thực tế phải tuân thủ định dạng **Step-by-step** để đối ứng trực tiếp với Kết quả mong muốn (Expected Results).
- **Cách viết:** Copy toàn bộ các bước của Expected Results, nhưng ở bước cuối cùng (nơi xảy ra lỗi), thay thế bằng mô tả lỗi chuyên nghiệp.
- *Ví dụ Expected:* 
  `Bước 3. Hệ thống chặn lại và báo lỗi 'Vui lòng nhập tên'.`
- *Ví dụ Actual:* 
  `Bước 3. Hệ thống không thực hiện Trim() chuỗi hoặc kiểm tra rỗng, cho phép lưu dữ liệu trắng xuống CSDL.`

## 4. LỰA CHỌN LỖI (DEFECT SELECTION)
Khi viết giả định hoặc test case lỗi, hãy tập trung vào các lỗi "chạm đất" (Grounded), thường gặp trong quá trình làm phần mềm thực tế/đồ án:
1. **Lỗi Validation (Bắt lỗi):** Nhập số âm vào trường tiền tệ, bỏ trống trường bắt buộc, nhập vượt quá giới hạn ký tự (String truncated).
2. **Lỗi Logic & Nghiệp vụ:** Tính sai tổng tỷ lệ (khác 100%), ngày bắt đầu lớn hơn ngày kết thúc, cho phép xóa dữ liệu đang được tham chiếu (Foreign Key).
3. **Lỗi UI/UX:** Quên tắt tính năng Editable của Grid/TreeList, thiếu cảnh báo mất dữ liệu chưa lưu (Dirty Tracking), nút bấm không bị mờ (Disable) khi không có dữ liệu.
4. **Lỗi Exception cơ bản:** NullReferenceException khi click dòng trống, DivideByZero, FormatException khi parse chuỗi.

## 5. CẤU TRÚC CỘT CHUẨN (STANDARD COLUMNS)
Một file `DefectList.csv` chuẩn phải bao gồm đủ các cột sau (hoặc tương đương theo format dự án):
1. `Title`: Tiêu đề lỗi (Nên có tiền tố, ví dụ: [DF_001] Thiếu validation bỏ trống Mã SP).
2. `Screen`: Màn hình xảy ra lỗi.
3. `Pre-condition`: Điều kiện tiền quyết (Lấy từ TC).
4. `Steps`: Các bước thực hiện (Lấy từ TC).
5. `Test Data`: Dữ liệu test (nếu có).
6. `Expected Results`: Kết quả mong muốn (Lấy từ TC).
7. `Actual results`: Kết quả thực tế (Step-by-step).
8. `Priority`: Mức độ ưu tiên fix (High/Medium/Low).
9. `Serverity`: Mức độ nghiêm trọng của lỗi (Critical/High/Medium/Low).
10. `Date`: Ngày log lỗi.
11. `Owner`: Người log lỗi (QA).
12. `TC'ID`: Mã Test Case tham chiếu.
13. `Build`: Phiên bản phần mềm (VD: Sprint 1).
14. `System`: Tên hệ thống.
15. `Assign To`: Người chịu trách nhiệm fix (Dev).
16. `Status`: Trạng thái (New / Open / In Progress / Resolved / Closed).
17. `Evident (Hình ảnh lỗi)`: Link/Tên file ảnh đính kèm.
