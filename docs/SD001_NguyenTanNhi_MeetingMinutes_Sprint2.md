# BIÊN BẢN CUỘC HỌP – Sprint 2

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_MOM_Sprint2_v1.0  
**Nhóm**: NguyenTanNhi  
**Đề tài**: Hệ thống Quản lý Công viên Giải trí (Generic — Phase 1)  
**Khách hàng**: KDL Đại Nam  

---

## BIÊN BẢN HỌP #3: SPRINT 2 PLANNING

| Hạng mục | Chi tiết |
|----------|---------|
| **Ngày họp** | 18/03/2026 |
| **Địa điểm** | TP. Hồ Chí Minh |
| **Thành phần tham dự** | Nguyên (Lead), Tấn (Dev), Nhi (QA) |
| **Chủ trì** | Nguyên |
| **Thư ký** | Nhi |

### 1. Mục đích cuộc họp
- Tổng hợp phản hồi Sprint 1 Review (45 Defects)
- Phân tích yêu cầu Sprint 2: Các module vận hành còn lại trong dbCu.sql (NV, KH, Bán hàng, Vé, Ví, Bảo trì, Bảng giá, Ca làm, Sự kiện)
- Phân công công việc
- Lên kế hoạch Fix defects Sprint 1 đồng thời

### 2. Nội dung thảo luận

#### 2.1. Review phản hồi Sprint 1
- 45 Defects phát hiện ở Sprint 1 → ưu tiên fix 15 defects Critical/Major trong Sprint 2 (DEF-001 đến DEF-015)
- 5 defects còn lại sẽ kiểm thử hồi quy để xác nhận vẫn tồn tại
- **KH Đại Nam chưa tham gia Sprint 1** — team tự phát triển theo hướng generic

#### 2.2. Phạm vi Sprint 2
Nhóm thống nhất Sprint 2 sẽ phát triển **3 chức năng** chính:

| # | Chức năng | Mô tả | Logic phức tạp |
|:-:|:----------|:------|:---------------|
| 1 | **Quản lý Nhân viên** | CRUD nhân viên + hình ảnh chân dung + chức vụ + khu vực trực (bảng `NhanVien` đã có trong dbCu) | Auto-generate MaCode (NV001...), Upload ảnh vào thư mục + Image path DB |
| 2 | **Quản lý Khách hàng** | CRUD khách hàng + tìm theo SĐT + điểm tích lũy hiện tại (bảng `KhachHang` đã có trong dbCu) | SĐT unique constraint, Tổng chi tiêu tích lũy |
| 3 | **Bán hàng cơ bản (POS)** | Tạo HoaDon + ChiTietHoaDon_Ve/DichVu + Chọn KH + Thanh toán (bảng `HoaDon` đã có trong dbCu) | Workflow POS: thêm vào giỏ → chọn KH → thanh toán |
| 4 | **Vé phát hành + Quét vé** | Sinh QR + Kiểm soát cổng (bảng `VeDaPhatHanh`, `LichSuQuetVe` đã có trong dbCu) | QR code + lịch sử quét |
| 5 | **Ví điện tử cơ bản** | Cấp thẻ RFID + Nạp tiền + Giao dịch (bảng `ViDienTu`, `LichSuGiaoDichVi` đã có trong dbCu) | Nạp/Rút/Thanh toán qua ví |
| 6 | **Bảo trì thiết bị** | Lịch bảo trì trò chơi + Chi phí (bảng `BaoTri` đã có trong dbCu) | Liên kết trò chơi → bảo trì |
| 7 | **Bảng giá + Sự kiện/KM + Ca làm** | Giá linh hoạt, KM gắn HĐ, phân ca NV (bảng `BangGia`, `SuKien`, `KhuyenMai`, `CaLam`, `PhanCa` đã có trong dbCu) | Dynamic pricing, ca Kanban |

#### 2.3. Xác định Database Sprint 2
- Lưu ý: Tất cả bảng (NhanVien, KhachHang, HoaDon, ViDienTu, BaoTri, BangGia, SuKien, CaLam...) **đã có sẵn** trong `dbCu.sql` (23 bảng)
- Sprint 2 chỉ **viết form GUI** tương ứng — không tạo bảng mới
- Ghi nhận: DB v1 **thiếu** các bảng nâng cao (Phong, Ban, DoanKhach, PhieuKho, VaiTro, DongVat...) → chưa thể phát triển nghiệp vụ Đại Nam

#### 2.4. Kiến trúc kỹ thuật Sprint 2
- Tiếp tục mô hình **3 lớp** (DAL → BUS → GUI)
- Thêm **ImageHelper** utility class để xử lý upload/load ảnh nhân viên
- Thêm **Auto-Code Generator** trong BUS: tự sinh mã NV001, KH001...
- DAL pattern: vẫn dùng `DAL_*.Instance` singleton (Technical Debt ghi nhận)

#### 2.5. Phân công công việc

| Công việc | Người phụ trách | Deadline |
|-----------|----------------|----------|
| Viết SRS Sprint 2 | Nhi | 20/03/2026 |
| Code DAL + BUS: Bán hàng, Vé, Ví | Nguyên | 26/03/2026 |
| Code GUI frmNhanVien + frmKhachHang + frmBangGia | Tấn | 28/03/2026 |
| Code frmSuKien + frmKhuyenMai + frmBaoTri + frmLichLamViec | Nhi | 29/03/2026 |
| Fix 7 Defects Critical Sprint 1 (Hồi quy) | Nguyên + Tấn | 25/03/2026 |
| Viết TestCase Sprint 2 (858 TCs mới) | Nhi | 27/03/2026 |
| Test + Defect List Sprint 2 (77 lỗi) | Nhi | 30/03/2026 |
| Viết User Guide Sprint 2 + MeetingMinutes | Nhi | 31/03/2026 |

### 3. Kết luận
- Sprint 2 kéo dài **2 tuần** (18/03 – 31/03/2026)
- Ưu tiên hoàn thành 3 form quản lý mới + fix 7 defects cũ
- Tổng TestCase mục tiêu: 858 TCs (NhanVien 112, KhachHang 110, BanHang 125, PhatVe+KiemSoatVe 115, ViDienTu+RFID 120, BaoTri 103, BangGia 108, LichLamViec 65)
- Review code cuối ngày thứ 5 (28/03) trước sprint review

### 4. Ký xác nhận

| Vai trò | Họ tên | Chữ ký |
|---------|--------|--------|
| Chủ trì (Lead) | Nguyên | ____________ |
| Developer | Tấn | ____________ |
| QA/Doc | Nhi | ____________ |

---

## BIÊN BẢN HỌP #4: SPRINT 2 REVIEW / RETROSPECTIVE

| Hạng mục | Chi tiết |
|----------|---------|
| **Ngày họp** | 31/03/2026 |
| **Địa điểm** | TP. Hồ Chí Minh |
| **Thành phần tham dự** | Nguyên (Lead), Tấn (Dev), Nhi (QA), **Đại diện KDL Đại Nam (PO)** |
| **Chủ trì** | Nguyên |
| **Thư ký** | Tấn |

### 1. Mục đích cuộc họp
- Demo sản phẩm Sprint 2 cho khách hàng
- Đánh giá chất lượng: 3 form mới + fix defects
- Lấy phản hồi khách hàng (PO)
- Retrospective nội bộ team

### 2. Kết quả Sprint 2

#### 2.1. Chức năng đã hoàn thành

| # | Chức năng | Trạng thái | Ghi chú |
|---|----------|:----------:|---------| 
| 1 | Quản lý Nhân viên (CRUD + Ảnh + Lọc) | ✅ Hoàn thành | Auto-sinh MaCode, Upload ảnh OK |
| 2 | Quản lý Khách hàng (CRUD + Tìm SĐT + Điểm) | ✅ Hoàn thành | SĐT unique, Tổng chi hiển thị |
| 3 | Quản lý Dịch vụ / F&B (CRUD + Danh mục) | ✅ Hoàn thành | Phân bổ khu vực, ẩn/hiện POS |
| 4 | Fix 7 Defects Sprint 1 | ✅ Hoàn thành | Xem Defect List cập nhật |

#### 2.2. Kết quả kiểm thử
- **Tổng Test Case đã viết**: 858 TCs cho Sprint 2 (8 module chính)
- **Defects phát hiện Sprint 2**: 65 (12 Critical, 20 Major, 18 Medium, 15 Minor)
- **Defects hồi quy Sprint 1**: 12 (tìm lại từ Sprint trước)
- **Tổng Defect List Sprint 2**: 77 defects (65 mới + 12 hồi quy)

#### 2.3. Demo cho khách hàng — Phản hồi PO

> **Phản hồi tích cực:**
> - 👍 Giao diện Nhân viên chuyên nghiệp, có ảnh chân dung → rất tốt
> - 👍 Tìm kiếm khách theo SĐT nhanh → đáp ứng yêu cầu POS
> - 👍 Phân bổ dịch vụ theo khu vực → logic nghiệp vụ đúng

> **Phản hồi cần cải thiện:**
> - ⚠️ "Nút bấm quá nhỏ cho nhân viên POS cảm ứng" → **Ghi nhận CR-01**
> - ⚠️ "Thiếu tính năng tích điểm chi tiết, chỉ có 1 cột sơ sài" → **Ghi nhận CR-02**
> - ⚠️ "Cần hệ thống quản lý đoàn khách tour" → Ghi nhận kế hoạch Sprint 3

### 3. Retrospective

#### 3.1. Những điều làm tốt (What went well)
- Hoàn thành đúng deadline cả 3 chức năng
- TestCase đạt 858 TCs (đạt mục tiêu)
- Hình ảnh nhân viên rotate và upload tốt
- Defect List chi tiết, dễ theo dõi

#### 3.2. Những điều cần cải thiện (What to improve)
- DB schema bắt đầu cho thấy giới hạn:
  - Không tách được `TaiKhoan` riêng → phải merge cùng NhanVien
  - `HoaDon.PhuongThuc` chỉ 1 cột → thiếu cho luồng tài chính ví
  - Không có bảng `DoanKhach` → phản hồi KH yêu cầu quản lý đoàn tour
- BUS gọi `DAL_*.Instance` static → coupling chặt, không unit test được
- UI chưa phù hợp cảm ứng POS theo phản hồi khách

#### 3.3. Hành động cải tiến (Action items)

| # | Action | Người phụ trách | Deadline | Ghi chú |
|---|--------|----------------|----------|---------|
| 1 | **[CRITICAL]** Lên kế hoạch tái cấu trúc DB cho Sprint 3 | Nguyên | Sprint 3 Planning | DB v1 generic → cần DB mới cho Đại Nam |
| 2 | Xử lý phản hồi PO Đại Nam: Redesign UI cho POS cảm ứng | Tấn | Sprint 3 | Change Request CR-01 |
| 3 | Thiết kế module Tích Điểm Loyalty hoàn chỉnh | Nguyên | Sprint 3 | Change Request CR-02 |
| 4 | Fix 25+ Defects Sprint 1+2 | All | Sprint 3 đầu | Ưu tiên Critical + Major |
| 5 | Nghiên cứu Gateway Pattern thay DAL.Instance | Nguyên | Sprint 3 | ADR-005 |
| 6 | Bổ sung nghiệp vụ Đại Nam: KS, NH, Kho, RBAC, Gửi xe, Thuê đồ... | All | Sprint 3 | Theo yêu cầu PO |

> [!WARNING]
> **Key Decision từ Retrospective Sprint 2:**  
> Team nhận ra DB v1 (`dbCu.sql`) do nhóm **tự thiết kế ở Sprint 1** đã **sai hướng phát triển**.
> Quyết định: Sprint 3 sẽ là **Sprint Refactoring toàn diện** — DB + Architecture + UI.

### 4. Ký xác nhận

| Vai trò | Họ tên | Chữ ký |
|---------|--------|--------|
| Chủ trì (Lead) | Nguyên | ____________ |
| Developer | Tấn | ____________ |
| QA/Doc | Nhi | ____________ |
| **KH/PO** | **Đại diện KDL Đại Nam** | ____________ |

---

## BIÊN BẢN HỌP #5: HỌP BỔ SUNG VỚI KHÁCH HÀNG (Change Request)

| Hạng mục | Chi tiết |
|----------|---------|
| **Ngày họp** | 31/03/2026 (Ngay sau Sprint 2 Review) |
| **Địa điểm** | TP. Hồ Chí Minh |
| **Thành phần tham dự** | Nguyên (Lead), **Đại diện KDL Đại Nam (PO)** |
| **Mục đích** | Chốt yêu cầu thay đổi CR-01 + CR-02 + Nghiệp vụ Đại Nam cho Sprint 3 |

### 1. Nội dung khách hàng yêu cầu cụ thể

#### CR-01: Redesign UI/UX
- **Khách hàng:** "Nhân viên quầy vé công viên dùng màn hình cảm ứng 15.6inch. Nút bấm hiện tại quá nhỏ, nhân viên hay bấm nhầm. Cần nút to hơn, rõ ràng hơn."
- **Team:** Đánh giá tác động → Không ảnh hưởng logic BUS/DAL. Chỉ cần redesign GUI layer. Thời gian ước lượng: +5 ngày Sprint 3.
- **Kết luận:** ✅ Đồng ý thực hiện.

#### CR-02: Hệ thống Tích Điểm Loyalty
- **Khách hàng:** "Đối thủ đều có hệ thống tích điểm. Cần tối thiểu: mỗi 100k chi tiêu = 1 điểm, khách VIP được nhân hệ số, có lịch sử tích/tiêu điểm."
- **Team:** Đánh giá tác động → Cần thêm 2 bảng DB mới (LichSuDiem, QuyTacDiem) + 1 BUS file + sửa 2 form. Thời gian ước lượng: +3 ngày Sprint 3.
- **Kết luận:** ✅ Đồng ý thực hiện. Gộp vào Sprint 3 cùng DB Migration.

### 2. Ký xác nhận

| Vai trò | Họ tên | Ý kiến | Chữ ký |
|---------|--------|--------|--------|
| **Team Lead** | Nguyên | Cam kết hoàn thành CR-01 + CR-02 + nghiệp vụ Đại Nam trong Sprint 3 | ____________ |
| **KH/PO** | Đại diện KDL Đại Nam | Đồng ý scope Sprint 3 bao gồm Refactoring + CR + Bổ sung KS/NH/Kho | ____________ |
