# TEST CASE – Sprint 1

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_TC_Sprint1_v1.8  
**Họ tên**: Nguyễn Tấn Nhị  
**Đề tài**: Hệ thống Quản lý Công viên Vui chơi Giải trí  
**Phiên bản**: 1.8  
**Ngày cập nhật**: 16/03/2026  

---

## RECORD OF CHANGE

| Effective Date | Changed Items | A/M/D | Change Description | New Version |
|:---|:---|:---:|:---|:---:|
| 15/03/2026 | First release | A | Khởi tạo Test Case | 1.0 |
| 16/03/2026 | Defect Integration | A | Cập nhật kịch bản từ Defect List | 1.6 |
| 16/03/2026 | Defect Marking | M | Đánh dấu N/A cho kịch bản thuộc Defect List | 1.8 |

---

## 1. QUẢN LÝ LOẠI VÉ (Ticket Management)

| TC ID | Title | Estimation (mins) | Test Type | Area | Procedure / Steps | Expected Results | Priority | RESULT |
|:---|:---|:---:|:---|:---|:---|:---|:---:|:---:|
| **LV_01** | Xem danh sách loại vé | 2 | UI/Logic | Loại vé | 1. Mở menu "Quản lý loại vé" | 1. DataGridView hiển thị đầy đủ các loại vé: Mã, Tên, Giá, Đối tượng, Trạng thái | High | PENDING |
| **LV_02** | Hiển thị khi DB rỗng | 2 | UI/Logic | Loại vé | 1. Mở form quản lý loại vé khi không có dữ liệu | 1. DataGridView rỗng, không lỗi | Medium | PENDING |
| **LV_03** | Thêm loại vé hợp lệ | 5 | Functional | Loại vé | 1. Nhập tên: "Vé trẻ em" 2. Nhập giá: 100000 3. Chọn đối tượng: Trẻ em 4. Nhấn "Thêm" | 1. Thêm thành công, hiển thị thông báo 2. Danh sách được cập nhật | High | PENDING |
| **LV_04** | Thêm với tên bỏ trống | 2 | Validation | Loại vé | 1. Để trống tên 2. Nhập giá: 100000 3. Nhấn "Thêm" | 1. Thông báo lỗi: "Vui lòng nhập tên loại vé" | High | PENDING |
| **LV_05** | Thêm với giá vé âm | 2 | Validation | Loại vé | 1. Nhập tên: "Vé test" 2. Nhập giá: -50000 3. Nhấn "Thêm" | 1. Thông báo lỗi: "Giá vé không hợp lệ" | High | PENDING |
| **LV_06** | Thêm với giá không phải số | 2 | Validation | Loại vé | 1. Nhập tên: "Vé test" 2. Nhập giá: "abc" 3. Nhấn "Thêm" | 1. Thông báo lỗi: "Giá vé phải là số hợp lệ" | High | PENDING |
| **LV_07** | Giá cuối tuần sai | 2 | Validation | Loại vé | 1. Nhập đầy đủ thông tin 2. Giá cuối tuần: "xyz" 3. Nhấn "Thêm" | 1. Thông báo: "Giá cuối tuần phải là số hợp lệ" | Medium | PENDING |
| **LV_08** | Thêm với tên trùng | 2 | Validation | Loại vé | 1. Nhập tên loại vé đã có sẵn 2. Nhấn "Thêm" | 1. Thông báo: "Tên loại vé đã tồn tại" | High | PENDING |
| **LV_09** | Thêm loại vé combo | 5 | Functional | Loại vé | 1. Nhập thông tin cơ bản 2. Bật toggle "Là vé combo" 3. Chọn vé con, số lượt 4. Nhấn Thêm vé con 5. Nhấn "Thêm" | 1. Thêm thành công loại vé combo kèm chi tiết | Medium | PENDING |
| **LV_10** | Thêm với giá = 0 | 2 | Functional | Loại vé | 1. Nhập tên, chọn đối tượng 2. Giá = 0 3. Nhấn "Thêm" | 1. Thêm thành công (giá = 0 hợp lệ) | Low | PENDING |
| **LV_11** | Sửa loại vé hợp lệ | 3 | Functional | Loại vé | 1. Click chọn hàng trong grid 2. Sửa tên thành "Vé VIP" 3. Nhấn "Sửa" | 1. Cập nhật thành công, grid refresh | High | PENDING |
| **LV_12** | Sửa với tên bỏ trống | 2 | Validation | Loại vé | 1. Xóa trắng tên 2. Nhấn "Sửa" | 1. Thông báo lỗi: "Vui lòng nhập tên loại vé" | High | PENDING |
| **LV_13** | Sửa khi chưa chọn hàng | 2 | Validation | Loại vé | 1. Nhấn "Sửa" khi chưa chọn dòng | 1. Thông báo: "Vui lòng chọn loại vé cần sửa" | Medium | PENDING |
| **LV_14** | Xóa loại vé hợp lệ | 3 | Functional | Loại vé | 1. Click chọn hàng 2. Nhấn "Xóa" 3. Xác nhận "Có" | 1. Xóa thành công, grid refresh | High | PENDING |
| **LV_15** | Xóa vé có giao dịch | 3 | Validation | Loại vé | 1. Chọn vé đã có dữ liệu bán 2. Nhấn "Xóa" | 1. Thông báo: "Không thể xóa loại vé đã phát sinh giao dịch" | High | PENDING |
| **LV_16** | Hủy xóa | 1 | Functional | Loại vé | 1. Nhấn "Xóa" 2. Xác nhận "Không" | 1. Không xóa, giữ nguyên dữ liệu | Medium | PENDING |
| **LV_17** | Xóa khi chưa chọn | 1 | Validation | Loại vé | 1. Nhấn "Xóa" khi chưa chọn dòng | 1. Thông báo: "Vui lòng chọn loại vé cần xóa" | Medium | PENDING |
| **LV_18** | Tìm kiếm theo tên | 2 | Functional | Loại vé | 1. Nhập "VIP" vào ô tìm kiếm 2. Nhấn "Tìm" | 1. Grid hiển thị các vé có tên chứa "VIP" | High | PENDING |
| **LV_19** | Tìm kiếm sai | 2 | Functional | Loại vé | 1. Nhập "XXXXXX" 2. Nhấn "Tìm" | 1. Grid rỗng, không lỗi | Medium | PENDING |
| **LV_20** | Lọc theo đối tượng | 2 | Functional | Loại vé | 1. Chọn "Trẻ em" từ combo Lọc đối tượng | 1. Grid chỉ hiển thị vé có đối tượng "Trẻ em" | High | PENDING |
| **LV_21** | Tìm ko phân biệt hoa thường| 2 | Functional | Loại vé | 1. Nhập "vé người lớn" 2. Nhấn "Tìm" | 1. Tìm được kết quả chính xác | Medium | PENDING |
| **LV_22** | Làm mới - Giá trị mặc định (DEF) | 2 | Functional | Loại vé | 1. Nhấn "Làm mới" | 1. Tên, Giá trống 2. Đối tượng: "Người lớn" 3. Trạng thái: "Hoạt động" | Medium | N/A |
| **LV_23** | Validation Trạng thái (DEF) | 2 | Validation | Loại vé | 1. Để trống Trạng thái 2. Nhấn "Thêm" | 1. Thông báo yêu cầu chọn trạng thái | High | N/A |
| **LV_24** | Giá vé bỏ trống (DEF) | 2 | Validation | Loại vé | 1. Để trống giá vé 2. Nhấn "Thêm" | 1. Báo lỗi: "Vui lòng nhập giá vé" | Medium | N/A |

## 2. QUẢN LÝ KHU VỰC (Area Management)

| TC ID | Title | Estimation (mins) | Test Type | Area | Procedure / Steps | Expected Results | Priority | RESULT |
|:---|:---|:---:|:---|:---|:---|:---|:---:|:---:|
| **KV_01** | Xem danh sách KV | 2 | UI/Logic | Khu vực | 1. Mở menu "Quản lý khu vực" | 1. Grid hiện: Mã, MaCode, Tên, Mô tả, Trạng thái | High | PENDING |
| **KV_02** | KV rỗng | 2 | UI/Logic | Khu vực | 1. Mở form khi DB không có khu vực | 1. Grid rỗng, không lỗi | Medium | PENDING |
| **KV_03** | Thêm KV hợp lệ | 4 | Functional | Khu vực | 1. Nhấn Làm mới 2. Nhập tên: "Khu Mạo Hiểm" 3. Chọn trạng thái: Mở cửa 4. Nhấn "Thêm" | 1. Thêm thành công, MaCode tự sinh (Axx) | High | PENDING |
| **KV_04** | Tên KV bỏ trống | 2 | Validation | Khu vực | 1. Để trống tên 2. Nhấn "Thêm" | 1. Thông báo: "Vui lòng nhập tên khu vực" | High | PENDING |
| **KV_05** | Tên KV trùng | 2 | Validation | Khu vực | 1. Nhập tên khu vực đã tồn tại 2. Nhấn "Thêm" | 1. Thông báo: "Tên khu vực đã tồn tại" | High | PENDING |
| **KV_06** | Tên KV quá dài (DEF) | 2 | Validation | Khu vực | 1. Nhập tên > 100 ký tự 2. Nhấn "Thêm" | 1. Báo lỗi: "Tên khu vực không được vượt quá 100 ký tự" | High | N/A |
| **KV_07** | Mô tả quá dài (DEF) | 2 | Validation | Khu vực | 1. Nhập mô tả > 500 ký tự 2. Nhấn "Thêm" | 1. Báo lỗi: "Mô tả không được vượt quá 500 ký tự" | High | N/A |
| **KV_08** | Sửa KV hợp lệ | 3 | Functional | Khu vực | 1. Chọn 1 khu vực 2. Sửa tên 3. Nhấn "Sửa" | 1. Cập nhật thành công | High | PENDING |
| **KV_09** | Đóng cửa khu vực | 3 | Logic | Khu vực | 1. Đổi trạng thái -> "Đóng cửa" 2. Nhấn "Sửa" | 1. Cập nhật thành công, ảnh hưởng các trò chơi con | High | PENDING |
| **KV_10** | Sửa khi chưa chọn | 2 | Validation | Khu vực | 1. Nhấn "Sửa" khi chưa chọn dòng | 1. Thông báo: "Vui lòng chọn khu vực cần sửa" | Medium | PENDING |
| **KV_11** | Xóa KV không trò chơi | 3 | Functional | Khu vực | 1. Chọn khu vực trống 2. Nhấn "Xóa" 3. Xác nhận "Có" | 1. Xóa thành công | High | PENDING |
| **KV_12** | Xóa KV có trò chơi (DEF) | 3 | Validation | Khu vực | 1. Chọn khu vực đang có trò chơi 2. Nhấn "Xóa" 3. Xác nhận "Có" | 1. Báo rõ ràng: "Không thể xóa. Khu vực đang có trò chơi." | Critical | N/A |
| **KV_13** | Hủy xóa KV | 1 | Functional | Khu vực | 1. Nhấn "Xóa" 2. Xác nhận "Không" | 1. Không xóa dữ liệu | Medium | PENDING |
| **KV_14** | Tìm KV theo tên | 2 | Functional | Khu vực | 1. Nhập "Nước" 2. Nhấn "Tìm" | 1. Grid hiển thị khu vực tên chứa "Nước" | High | PENDING |
| **KV_15** | Lọc theo trạng thái | 2 | Functional | Khu vực | 1. Chọn "Bảo trì" từ combo lọc | 1. Grid hiển thị đúng các KV bảo trì | High | PENDING |
| **KV_16** | Tìm ko phân biệt hoa thường| 2 | Functional | Khu vực | 1. Nhập "khu trẻ em" 2. Nhấn "Tìm" | 1. Tìm được kết quả | Medium | PENDING |
| **KV_17** | Làm mới KV | 1 | Functional | Khu vực | 1. Nhấn "Làm mới" | 1. Ô nhập liệu trống, grid reload | High | PENDING |
| **KV_18** | Thoát form KV - Xác nhận (DEF) | 2 | Functional | Khu vực | 1. Nhấn nút "Thoát" | 1. Hiện hộp thoại hỏi "Bạn có muốn thoát không?" | High | N/A |
| **KV_19** | Cbo Trạng thái KV giá trị (DEF) | 2 | UI | Khu vực | 1. Mở Cbo Trạng thái | 1. Hiện đủ 3 giá trị: Mở cửa, Bảo trì, Đóng cửa | Medium | N/A |

## 3. QUẢN LÝ TRÒ CHƠI (Game Management)

| TC ID | Title | Estimation (mins) | Test Type | Area | Procedure / Steps | Expected Results | Priority | RESULT |
|:---|:---|:---:|:---|:---|:---|:---|:---:|:---:|
| **TC_01** | Xem danh sách Trò chơi | 2 | UI/Logic | Trò chơi | 1. Mở menu "Quản lý trò chơi" | 1. Grid hiện: Mã, MaCode, Tên, Khu vực, Loại, Sức chứa | High | PENDING |
| **TC_02** | Trò chơi rỗng | 2 | UI/Logic | Trò chơi | 1. Mở form khi chưa có game | 1. Grid rỗng, không lỗi | Medium | PENDING |
| **TC_03** | Thêm game hợp lệ | 5 | Functional | Trò chơi | 1. Nhấn Làm mới 2. Nhập tên, Chọn khu vực, Nhập sức chứa: 20 3. Nhấn "Thêm" | 1. Thêm thành công, MaCode tự sinh (Gxxx) | High | PENDING |
| **TC_04** | Tên game bỏ trống | 2 | Validation | Trò chơi | 1. Để trống tên 2. Nhấn "Thêm" | 1. Thông báo: "Vui lòng nhập tên trò chơi" | High | PENDING |
| **TC_05** | Chưa chọn khu vực | 2 | Validation | Trò chơi | 1. Nhập tên, ko chọn KV 2. Nhấn "Thêm" | 1. Thông báo: "Vui lòng chọn khu vực" | High | PENDING |
| **TC_06** | Chưa chọn loại game | 2 | Validation | Trò chơi | 1. Nhập tên, chọn KV, ko chọn Loại 2. Nhấn "Thêm" | 1. Thông báo: "Vui lòng chọn loại trò chơi" | High | PENDING |
| **TC_07** | Sức chứa <= 0 | 2 | Validation | Trò chơi | 1. Nhập sức chứa: 0 hoặc -1 2. Nhấn "Thêm" | 1. Thông báo: "Sức chứa phải lớn hơn 0" | High | PENDING |
| **TC_08** | Validation Sức chứa (Chữ) (DEF) | 2 | Validation | Trò chơi | 1. Nhập "abc" vào Sức chứa 2. Nhấn "Thêm" | 1. Báo rõ: "Sức chứa phải là số nguyên" | Critical | N/A |
| **TC_09** | Tuổi ko là số | 2 | Validation | Trò chơi | 1. Nhập tuổi: "xyz" 2. Nhấn "Thêm" | 1. Thông báo: "Tuổi phải là số nguyên" | High | PENDING |
| **TC_10** | Thời gian ko là số | 2 | Validation | Trò chơi | 1. Nhập thời gian: "abc" 2. Nhấn "Thêm" | 1. Thông báo: "Thời gian phải là số nguyên" | High | PENDING |
| **TC_11** | Trùng tên cùng KV | 2 | Validation | Trò chơi | 1. Nhập tên đã có ở cùng KV đó 2. Nhấn "Thêm" | 1. Thông báo: "Tên trò chơi đã tồn tại trong khu vực này" | High | PENDING |
| **TC_12** | Trùng tên khác KV | 2 | Functional | Trò chơi | 1. Nhập tên đã có ở KV khác 2. Nhấn "Thêm" | 1. Thêm thành công | Medium | PENDING |
| **TC_13** | Tên game quá dài | 2 | Validation | Trò chơi | 1. Nhập tên 151 ký tự 2. Nhấn "Thêm" | 1. Thông báo: "Tên trò chơi không được vượt quá 150 ký tự" | Medium | PENDING |
| **TC_14** | Mô tả game quá dài | 2 | Validation | Trò chơi | 1. Nhập mô tả 501 ký tự 2. Nhấn "Thêm" | 1. Thông báo: "Mô tả không được vượt quá 500 ký tự" | Medium | PENDING |
| **TC_15** | Tuổi tối thiểu âm | 2 | Validation | Trò chơi | 1. Nhập tuổi -1 2. Nhấn "Thêm" | 1. Thông báo: "Tuổi tối thiểu không được âm" | Medium | PENDING |
| **TC_16** | Chiều cao âm | 2 | Validation | Trò chơi | 1. Nhập chiều cao -10 2. Nhấn "Thêm" | 1. Thông báo: "Chiều cao tối thiểu không được âm" | Medium | PENDING |
| **TC_17** | Sửa game hợp lệ | 3 | Functional | Trò chơi | 1. Chọn 1 game 2. Sửa thông tin 3. Nhấn "Sửa" | 1. Cập nhật thành công | High | PENDING |
| **TC_18** | Sửa khi chưa chọn | 2 | Validation | Trò chơi | 1. Nhấn "Sửa" khi chưa chọn dòng | 1. Thông báo: "Vui lòng chọn trò chơi cần sửa" | Medium | PENDING |
| **TC_19** | Sửa dữ liệu lỗi | 2 | Validation | Trò chơi | 1. Xóa trắng tên game 2. Nhấn "Sửa" | 1. Thông báo lỗi validate tương ứng | High | PENDING |
| **TC_20** | Xóa game hợp lệ | 3 | Functional | Trò chơi | 1. Chọn 1 game 2. Nhấn "Xóa" 3. Xác nhận "Có" | 1. Xóa thành công | High | PENDING |
| **TC_21** | Hủy xóa game | 1 | Functional | Trò chơi | 1. Nhấn "Xóa" 2. Xác nhận "Không" | 1. Không xóa dữ liệu | Medium | PENDING |
| **TC_22** | Xóa khi chưa chọn | 1 | Validation | Trò chơi | 1. Nhấn "Xóa" khi chưa chọn hàng | 1. Thông báo: "Vui lòng chọn trò chơi cần xóa" | Medium | PENDING |
| **TC_23** | Lọc theo khu vực | 2 | Functional | Trò chơi | 1. Chọn "Khu Mạo Hiểm" từ combo lọc | 1. Grid chỉ hiện game thuộc KV đó | High | PENDING |
| **TC_24** | Lọc theo loại game | 2 | Functional | Trò chơi | 1. Chọn "Cảm giác mạnh" từ combo lọc | 1. Grid chỉ hiện game loại đó | High | PENDING |
| **TC_25** | Tìm game theo tên | 2 | Functional | Trò chơi | 1. Nhập "Tàu" 2. Nhấn "Tìm" | 1. Grid hiện game tên chứa "Tàu" | High | PENDING |
| **TC_26** | Kết hợp lọc + tìm | 3 | Functional | Trò chơi | 1. Chọn KV 2. Nhập tên 3. Nhấn "Tìm" | 1. Kết quả thỏa cả 2 điều kiện | Medium | PENDING |
| **TC_27** | Chọn "Tất cả" lọc | 2 | Functional | Trò chơi | 1. Chọn "Tất cả" tại các combo lọc | 1. Grid hiện toàn bộ trò chơi | Medium | PENDING |
| **TC_28** | Làm mới game | 1 | Functional | Trò chơi | 1. Nhấn "Làm mới" | 1. Xóa trắng form, reset lọc, reload grid | High | PENDING |
| **TC_29** | Thời gian lượt = 0 hợp lệ (DEF) | 2 | Functional | Trò chơi | 1. Nhập Thời gian = 0 2. Nhấn "Thêm" | 1. Chấp nhận lưu (theo SRS >= 0) | Medium | N/A |
| **TC_30** | Cbo Trạng thái Game đầy đủ (DEF) | 2 | UI | Trò chơi | 1. Mở Cbo Trạng thái | 1. Có đủ 4: Hoạt động, Bảo trì, Ngừng HĐ, Hỏng/Sự cố | Medium | N/A |

## 4. HỆ THỐNG / BẢO MẬT (System / Security)

| TC ID | Title | Estimation (mins) | Test Type | Area | Procedure / Steps | Expected Results | Priority | RESULT |
|:---|:---|:---:|:---|:---|:---|:---|:---:|:---:|
| **SYS_01** | Đăng nhập thành công | 2 | Security | Login | 1. Nhập đúng Tài khoản và Mật khẩu 2. Nhấn Đăng nhập | 1. Chuyển vào Form chính 2. Hiển thị đúng tên người dùng | High | PENDING |
| **SYS_02** | Đăng nhập sai | 2 | Security | Login | 1. Nhập sai Tài khoản hoặc Mật khẩu 2. Nhấn Đăng nhập | 1. Thông báo: "Tài khoản hoặc mật khẩu không đúng" | High | PENDING |
| **DASH_01** | Dashboard bảo trì | 2 | UI/Logic | Báo cáo | 1. Nhấn vào menu Báo cáo | 1. Thông báo: "Tính năng đang được bảo trì tạm thời" 2. Dashboard không mở nội dung | Medium | PENDING |

---

## 5. PHIÊN BẢN DÀNH CHO EXCEL (Copy & Paste)

> [!TIP]
> **Cách sử dụng:** Bôi đen và copy đoạn văn bản trong khung dưới đây, sau đó mở Excel và nhấn **Ctrl + V** (Dán). Các cột sẽ tự động chia đúng ô.

```tsv
TC ID	Title	Estimation (mins)	Test Type	Area	Procedure / Steps	Expected Results	Priority	RESULT
LV_01	Xem danh sách loại vé	2	UI/Logic	Loại vé	1. Mở menu Danh mục. 2. Chọn Quản lý loại vé	1. Form hiển thị các cột dữ liệu. 2. Dữ liệu khớp Database	High	PENDING
LV_22	Làm mới - Giá trị mặc định	2	Functional	Loại vé	1. Nhấn Làm mới	1. Đối tượng: Người lớn. 2. Trạng thái: Hoạt động	Medium	N/A
LV_23	Validation Trạng thái	2	Validation	Loại vé	1. Để trống Trạng thái. 2. Nhấn Thêm	1. Báo lỗi yêu cầu chọn trạng thái	High	N/A
LV_24	Giá vé bỏ trống	2	Validation	Loại vé	1. Để trống giá. 2. Nhấn Thêm	1. Báo lỗi vui lòng nhập giá	Medium	N/A
KV_06	Tên KV quá dài	2	Validation	Khu vực	1. Nhập tên > 100 ký tự. 2. Nhấn Thêm	1. Báo lỗi tên không quá 100 ký tự	High	N/A
KV_07	Mô tả quá dài	2	Validation	Khu vực	1. Nhập mô tả > 500 ký tự. 2. Nhấn Thêm	1. Báo lỗi mô tả không quá 500 ký tự	High	N/A
KV_12	Xóa KV có game	3	Validation	Khu vực	1. Chọn KV có game. 2. Nhấn Xóa	1. Báo rõ: Không thể xóa. KV đang có game	Critical	N/A
KV_18	Thoát form KV - Xác nhận	2	Functional	Khu vực	1. Nhấn Thoát	1. Hiện thông báo xác nhận thoát	High	N/A
KV_19	Cbo Trạng thái KV giá trị	2	UI	Khu vực	1. Mở Cbo Trạng thái	1. Hiện đủ: Mở cửa, Bảo trì, Đóng cửa	Medium	N/A
TC_08	Validation Sức chứa	2	Validation	Trò chơi	1. Nhập chữ vào Sức chứa	1. Báo rõ: Sức chứa phải là số nguyên	Critical	N/A
TC_29	Thời gian lượt = 0	2	Functional	Trò chơi	1. Nhập thời gian = 0. 2. Nhấn Thêm	1. Thêm thành công (Hợp lệ)	Medium	N/A
TC_30	Cbo Trạng thái Game	2	UI	Trò chơi	1. Kiểm tra Cbo Trạng thái	1. Có đủ 4 giá trị theo SRS	Medium	N/A
SYS_01	Đăng nhập thành công	2	Security	Hệ thống	1. Nhập đúng TK/MK	1. Vào màn hình chính	High	PENDING
DASH_01	Dashboard bảo trì	2	UI/Logic	Báo cáo	1. Mở Báo cáo	1. Hiện thông báo bảo trì	Medium	PENDING
```
