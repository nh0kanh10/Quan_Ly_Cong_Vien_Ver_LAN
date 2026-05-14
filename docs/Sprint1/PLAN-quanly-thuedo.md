# PLAN: QUẢN LÝ THUÊ ĐỒ (RENTAL ASSET MANAGEMENT)
**Module**: Bán Hàng & Danh Mục (Rental Operations)
**Agent Assigned**: `project-planner` -> `backend-specialist` (DAL) -> `frontend-specialist` (UI)

## MỤC TIÊU (GOAL)
Triển khai trọn vẹn nghiệp vụ Cho Thuê Tài Sản (Option B - Serialized Assets) với kiến trúc phân tách rạch ròi giữa **Tài sản Cố định (Fixed)** và **Tài sản Lưu động (Movable)**, kết hợp bộ lọc quyền bán hàng `Menu_POS` theo từng máy POS.

## PHASE 1: DATABASE & ENTITY (Data Layer)
- [x] Thêm cột `IdKhuVucHienTai` vào bảng `DanhMuc.PhuongTienDiChuyen` (Đã chạy ngầm lúc nãy).
- [x] Thêm Property `IdKhuVucHienTai` vào file `ET_PhuongTienDiChuyen.cs` (Đã chạy ngầm lúc nãy).
- [ ] Bổ sung trường `IdSanPham` vào `ET_TaiSanChoThue.cs` nếu chưa có, để lấy giá từ bảng Sản Phẩm.

## PHASE 2: DAL (Data Access Layer) - Cốt lõi
**Target File**: `DAL_ThueDo.cs`
- [ ] **Viết lại hàm `LayDanhSachTaiSanSẵnSàng(int idDiemBan, int idKhuVucPOS)`**:
  - `INNER JOIN Menu_POS` để chỉ lấy các tài sản thuộc `IdSanPham` mà Điểm Bán đó được phép kinh doanh.
  - Nếu là `PhuongTienDiChuyen` -> Filter theo `IdKhuVucHienTai = idKhuVucPOS`.
  - Nếu là `TuDo` hoặc `ChoiNghiMat` -> Filter theo `IdKhuVuc = idKhuVucPOS`.
- [ ] **Cập nhật hàm `TraDo(int idTaiSan, int idKhuVucPOS)`**:
  - Tìm loại tài sản (Kế thừa từ đâu).
  - Nếu là `PhuongTien` -> `UPDATE PhuongTienDiChuyen SET IdKhuVucHienTai = idKhuVucPOS`. Cập nhật `TrangThai = SanSang`.
  - Nếu là `TuDo` -> Kiểm tra **Hard Block** (Nếu tủ này ở Khu Biển mà đem ra Cổng trả -> Ném Exception từ chối trả khóa). Nếu hợp lệ -> Cập nhật `TrangThai = SanSang`.

## PHASE 3: BUSINESS LOGIC (BUS Layer)
**Target File**: `BUS_ThueDo.cs`
- [ ] Bọc lại các hàm DAL xử lý exception.
- [ ] Xử lý logic tính tiền thuê (Cọc, Thời gian thực tế, Tính thêm phụ phí nếu trễ giờ).

## PHASE 4: GIAO DIỆN & UI (Frontend Layer)
**Target File**: `ucGiaoDo.cs` và `ucNhanTra.cs`
- [ ] **ucGiaoDo (Cho Thuê)**:
  - Load danh sách Sẵn sàng lên `gridNguon` (Gồm cả Tủ/Xe định danh và Phao/Khăn tắm không định danh).
  - Phân loại thao tác UI:
    - **Nhóm Định danh (Tủ/Xe)**: Gõ mã vào TextBox (giả lập quét) hoặc click đúp -> Quăng vào giỏ hàng (Số lượng mặc định = 1).
    - **Nhóm Không định danh (Phao bơi)**: Click đúp vào tên Phao trên lưới -> Hiển thị pop-up/ô nhập Số lượng -> Quăng vào giỏ hàng (Số lượng = N).
- [ ] **ucNhanTra (Trả Đồ)**:
  - **2 Luồng tìm kiếm (Từ V1.2)**: 
    - Quẹt RFID khách -> Lấy toàn bộ đồ chưa trả.
    - Quét mã biên lai (DT-xxx) -> Lấy đồ của đơn hàng đó.
  - **Nhóm Không định danh (Phao)**: Kế thừa thuật toán V1.2 (Grid gộp nhóm có cột `Số lượng đang thuê`, `Khách Trả (SL)`, `Báo Hỏng/Mất (SL)`). Hỗ trợ phím tắt `F12` để điền nhanh Trả Đủ.
  - **Nhóm Định danh (Xe/Tủ)**: Gõ mã xe/chìa khóa vào TextBox để đánh dấu trả. Nếu báo mất xe/tủ -> Chuyển trạng thái sang báo mất.
  - **Thuật toán Phạt & Hoàn cọc (Từ V1.2)**:
    - Nếu khách trả quá giờ: Tự động trừ tiền cọc tính phí lố giờ.
    - Nếu có đồ bị "Báo Mất": Bật Pop-up `InputBox` tính tự động [Tiền lố giờ] + [Tiền cọc], cho phép thu ngân nhập số tiền Phạt Đền Bù.
  - **Giám sát phiên thuê chưa trả (Từ V1.2)**:
    - Bổ sung lưới phụ hiển thị danh sách toàn bộ các đơn hàng Đang Thuê (chưa trả) lọc theo ngày.
    - Hiển thị cảnh báo trực quan: *"Đang có X phiên chờ (Y món chưa về)"*.
    - Hỗ trợ Double-click hoặc Click chuột phải vào một đơn hàng bất kỳ trên lưới để load thẳng sang màn hình Nhận Trả (Dùng để cứu nét cho các trường hợp khách làm mất biên lai giấy mà không có thẻ RFID).
  - Catch exception "Hard Block" từ DAL nếu trả chìa khóa sai khu vực.

## PHASE 5: VERIFICATION CHECKLIST (Kiểm thử)
- [ ] Khởi động máy POS A (Khu Biển, chỉ Menu Tủ Đồ). Quét lấy tủ, xem có bị dính Xe Đạp vào danh sách không.
- [ ] Khởi động máy POS B (Khu Dã Ngoại, Menu Xe Đạp). Cho thuê chiếc `XD-01`.
- [ ] Sang máy POS C (Khu Biển, Menu Xe Đạp). Quét trả chiếc `XD-01`. Xem xe có đổi hộ khẩu sang Khu Biển không.
- [ ] Cầm chìa khóa Tủ Biển sang máy POS Dã Ngoại trả. Xem hệ thống có chặn "Hard Block" đúng như thiết kế không.
