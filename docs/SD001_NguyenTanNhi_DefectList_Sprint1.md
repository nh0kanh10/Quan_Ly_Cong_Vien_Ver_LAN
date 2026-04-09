# DEFECT LIST – Sprint 1

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_DL_Sprint1_v1.0  
**Nhóm**: NguyenTanNhi  
**Đề tài**: Hệ thống Quản lý Công viên Vui chơi Giải trí  
**Phiên bản**: 1.0  
**Ngày tạo**: 16/03/2026  
**Tham chiếu SRS**: SD001_SRS_Sprint1_v1.2  

---

## RECORD OF CHANGE

| Effective Date | Changed Items | A/M/D | Change Description | New Version |
|----------------|--------------|-------|-------------------|-------------|
| 16/03/2026 | First release | A | Khởi tạo Defect List Sprint 1 | 1.0 |

---

## TỔNG HỢP

| Mức độ | Số lượng |
|--------|:--------:|
| 🔴 Critical | 2 |
| 🟠 Major | 5 |
| 🟡 Minor | 5 |
| **Tổng** | **12** |

---

## CHI TIẾT DEFECT

| DEF_ID | Mức độ | Module | Mô tả lỗi | SRS Reference | Bước tái hiện | Kết quả thực tế | Kết quả mong đợi | File liên quan | Trạng thái |
|--------|--------|--------|-----------|---------------|--------------|-----------------|------------------|---------------|:----------:|
| DEF-001 | 🟠 Major | frmKhuVuc | Không validate tên khu vực > 100 ký tự | SRS 2.1.7 Rule#3 (ERR_AREA_NAME_MAX_LENGTH) | 1. Mở frmKhuVuc<br>2. Nhập tên > 100 ký tự<br>3. Nhấn Thêm | Thêm thành công, không báo lỗi | Báo lỗi: "Tên khu vực không được vượt quá 100 ký tự" | `BUS_KhuVuc.cs` dòng 65-73 | Open |
| DEF-002 | 🟠 Major | frmKhuVuc | Không validate mô tả > 500 ký tự | SRS 2.1.7 Rule#4 (ERR_AREA_DESC_MAX_LENGTH) | 1. Mở frmKhuVuc<br>2. Nhập mô tả > 500 ký tự<br>3. Nhấn Thêm | Thêm thành công, không báo lỗi | Báo lỗi: "Mô tả không được vượt quá 500 ký tự" | `BUS_KhuVuc.cs` dòng 65-73 | Open |
| DEF-003 | 🟡 Minor | frmKhuVuc | Giá trị Cbo Trạng thái không khớp SRS | SRS 2.1.5.2 Row#8 | 1. Mở frmKhuVuc<br>2. Click Cbo Trạng thái | Chỉ có: "Hoạt động", "Ngừng hoạt động" | Phải có: "Mở cửa", "Bảo trì", "Đóng cửa" | `frmKhuVuc.cs` dòng 64 | Open |
| DEF-004 | 🔴 Critical | frmKhuVuc | Xóa khu vực có trò chơi: thông báo lỗi không rõ ràng | SRS 2.1.7 Rule#5 (ERR_AREA_HAS_GAMES) | 1. Chọn KV có ≥1 trò chơi<br>2. Nhấn Xóa → Có | Báo: "Xóa thất bại! Khu vực có thể đang chứa trò chơi" | Phải báo: "Không thể xóa. Khu vực đang có trò chơi." | `frmKhuVuc.cs` dòng 159-176 | Open |
| DEF-005 | 🟠 Major | frmLoaiVe | Không validate trạng thái bỏ trống | SRS 1.5.2 Row#11 (Required = Yes) | 1. Nhấn Làm mới (Cbo = -1)<br>2. Nhấn Thêm | Có thể lưu trạng thái rỗng vào DB | Phải báo lỗi khi trạng thái rỗng | `BUS_LoaiVe.cs` dòng 60-81 | Open |
| DEF-006 | 🟡 Minor | frmLoaiVe | Làm mới: Đối tượng trống thay vì default "Người lớn" | SRS 1.5.2 Row#10 (Default = Người lớn) | 1. Nhấn "Làm mới" | cboDoiTuong hiện trống | Phải hiện "Người lớn" | `frmLoaiVe.cs` dòng 250 | Open |
| DEF-007 | 🟡 Minor | frmLoaiVe | Làm mới: Trạng thái trống thay vì default "Hoạt động" | SRS 1.5.2 Row#11 (Default = Hoạt động) | 1. Nhấn "Làm mới" | cboTrangThai hiện trống | Phải hiện "Hoạt động" | `frmLoaiVe.cs` dòng 251 | Open |
| DEF-008 | 🔴 Critical | frmTroChoi | Nhập chữ vào TextBox số: báo lỗi chung, không chỉ rõ trường | SRS 2.2.7 Rule#10,11,12 | 1. Nhập "abc" vào Sức chứa<br>2. Nhấn Thêm | Báo: "Dữ liệu số không hợp lệ..." (chung chung) | Phải báo cụ thể: "Sức chứa phải là số nguyên" | `frmTroChoi.cs` dòng 211-216 | Open |
| DEF-009 | 🟠 Major | frmTroChoi | Thời gian lượt = 0 bị báo lỗi (SRS cho phép ≥ 0) | SRS 2.2.5.2 Row#15 (≥ 0) | 1. Nhập Thời gian = 0<br>2. Nhấn Thêm | Báo: "Thời gian lượt phải lớn hơn 0" | Phải cho phép = 0 (≥ 0 theo SRS) | `BUS_TroChoi.cs` dòng 100-101 | Open |
| DEF-010 | 🟡 Minor | frmTroChoi | Cbo Trạng thái thiếu giá trị "Hỏng/Sự cố" | SRS 2.2.5.2 Row#11 | 1. Click Cbo Trạng thái | Chỉ có 3 option: "Hoạt động, Bảo trì, Ngừng HĐ" | Phải có 4: thêm "Hỏng/Sự cố" | `frmTroChoi.cs` dòng 83-86 | Open |
| DEF-011 | 🟠 Major | frmKhuVuc | Nút Thoát đóng form không hỏi xác nhận | Workflow pattern (frmLoaiVe, frmTroChoi đều hỏi) | 1. Nhấn nút Thoát ở frmKhuVuc | Form đóng ngay lập tức | Phải hiện hộp thoại xác nhận trước khi đóng | `frmKhuVuc.cs` dòng 196-199 | Open |
| DEF-012 | 🟡 Minor | frmLoaiVe | Giá vé bỏ trống tự gán = 0, không bắt lỗi | SRS 1.5.2 Row#8 (Required = Yes) | 1. Để trống ô Giá vé<br>2. Nhấn Thêm | Lưu thành công với giá = 0 | Nên bắt lỗi "Vui lòng nhập giá vé" | `frmLoaiVe.cs` dòng 271 | Open |

---

## THỐNG KÊ THEO MODULE

| Module | 🔴 Critical | 🟠 Major | 🟡 Minor | Tổng |
|--------|:-----------:|:--------:|:--------:|:----:|
| frmLoaiVe | 0 | 1 | 3 | 4 |
| frmKhuVuc | 1 | 2 | 1 | 4 |
| frmTroChoi | 1 | 2 | 1 | 4 |
| **Tổng** | **2** | **5** | **5** | **12** |
