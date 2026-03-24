# HƯỚNG DẪN CÀI ĐẶT VÀ SỬ DỤNG – Sprint 2

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_UG_Sprint2_v1.0  
**Nhóm**: NguyenTanNhi  
**Đề tài**: Hệ thống Quản lý Công viên Vui chơi Giải trí  
**Phiên bản**: 1.0  
**Ngày tạo**: 31/03/2026  
**Tham chiếu**: SD001_UG_Sprint1_v1.0 (Cài đặt cơ bản)  

---

## 1. THAY ĐỔI SO VỚI SPRINT 1

| Hạng mục | Sprint 1 | Sprint 2 |
|:---------|:---------|:---------|
| Database | `QuanLyCongVien` (22 bảng) | +3 bảng mới (NhanVien mở rộng, KhachHang, cập nhật SanPham) |
| Forms mới | 7 forms | +3 forms mới (Nhân viên, Khách hàng, Dịch vụ) |
| Tính năng | CRUD Khu vực, Trò chơi, Loại vé | + Quản lý Nhân sự + Khách hàng + F&B |

### 1.1 Yêu cầu bổ sung Sprint 2
| Yêu cầu | Chi tiết |
|----------|---------|
| Thư mục ảnh | Tạo thư mục `Images\NhanVien\` trong cùng thư mục exe |
| Font | Cài font `Segoe UI` (thường có sẵn trên Windows 10+) |

---

## 2. CẬP NHẬT DATABASE CHO SPRINT 2

### 2.1 Script bổ sung

Nếu đã có DB Sprint 1 (`QuanLyCongVien`), chạy script bổ sung:

```sql
-- 1. Mở rộng bảng NhanVien
ALTER TABLE NhanVien ADD MaCode NVARCHAR(10) NULL;
ALTER TABLE NhanVien ADD CCCD NVARCHAR(12) NULL;
ALTER TABLE NhanVien ADD Email NVARCHAR(100) NULL;
ALTER TABLE NhanVien ADD HinhAnh NVARCHAR(500) NULL;
ALTER TABLE NhanVien ADD NgayVaoLam DATE NULL;
ALTER TABLE NhanVien ADD TrangThai NVARCHAR(20) DEFAULT N'Đang làm việc';

-- 2. Tạo bảng KhachHang
CREATE TABLE KhachHang (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MaCode NVARCHAR(10) NULL,
    HoTen NVARCHAR(100) NOT NULL,
    DienThoai VARCHAR(15) NOT NULL UNIQUE,
    Email NVARCHAR(100) NULL,
    GioiTinh NVARCHAR(5) DEFAULT N'Nam',
    NgaySinh DATE NULL,
    DiaChi NVARCHAR(300) NULL,
    DiemTichLuy INT DEFAULT 0,
    TongChiTieu DECIMAL(15,0) DEFAULT 0,
    TrangThai NVARCHAR(20) DEFAULT N'Hoạt động'
);

-- 3. Cập nhật bảng SanPham/DichVu (thêm cột)
-- (Chi tiết trong file script Sprint 2)
```

### 2.2 Cài đặt nhanh (Trường hợp mới)

Nếu cài đặt mới hoàn toàn, tham khảo **User Guide Sprint 1 mục 2** cho:
- Cài đặt SQL Server
- Cấu hình TCP/IP + Firewall
- Cấu hình connection string

---

## 3. HƯỚNG DẪN SỬ DỤNG CÁC CHỨC NĂNG MỚI

### 3.1 Quản lý Nhân viên

**Đường dẫn**: Menu → Nhân sự → Quản lý Nhân viên

#### 3.1.1 Giao diện tổng quan

```text
╔══════════════════════════════════════════════════════════════════╗
║  QUẢN LÝ NHÂN SỰ                                       [_][X] ║
╠══════════════════════════════════════════════════════════════════╣
║ Tìm kiếm: [____________] Chức vụ: [▼ Tất cả]  [🔍 Tìm kiếm] ║
║                                                                  ║
║ ┌──────────────────────────────────┐  ┌────────────────────┐    ║
║ │ DANH SÁCH NHÂN VIÊN             │  │    HÌNH ẢNH        │    ║
║ │ Mã  │ Họ Tên     │ SĐT         │  │  ┌──────────────┐  │    ║
║ │ NV01│ Nguyễn A   │ 0901234567  │  │  │   AVATAR     │  │    ║
║ │ NV02│ Trần B     │ 0917888999  │  │  └──────────────┘  │    ║
║ └──────────────────────────────────┘  │  [📷 Chọn ảnh]   │    ║
║                                        └────────────────────┘    ║
║ --- THÔNG TIN CHI TIẾT ---                                      ║
║ Mã Code: [NV003] (Auto)    Họ Tên: [________________]          ║
║ CCCD:    [____________]     SĐT:    [________________]          ║
║ Email:   [________________________] Giới tính: [▼ Nam]         ║
║ Khu vực: [▼ Khu Mạo hiểm      ]  Trạng thái: [▼ Đang làm]   ║
║                                                                  ║
║ [+ Thêm] [✏ Sửa] [🗑 Xóa] [↻ Làm mới] [💾 Lưu] [🚪 Thoát]   ║
╚══════════════════════════════════════════════════════════════════╝
```

#### 3.1.2 Các thao tác

**Thêm nhân viên mới:**
1. Nhấn **"Làm mới"** → Mã Code tự sinh (NV003, NV004...)
2. Điền các thông tin: Họ tên, CCCD, SĐT, Email, Chức vụ, Khu vực
3. Nhấn **"Chọn ảnh"** → Chọn file ảnh (.jpg, .png) từ máy tính
4. Ảnh hiển thị tại khung Preview bên phải
5. Nhấn **"Thêm"** → Hệ thống validate + lưu vào DB + copy ảnh vào thư mục

**Sửa thông tin:**
1. Click chọn nhân viên trên **Grid** (danh sách)
2. Thông tin tự động nạp vào form bên dưới + ảnh hiện lên
3. Chỉnh sửa các trường cần thiết
4. Nhấn **"Sửa"** → Cập nhật DB

**Xóa nhân viên:**
1. Chọn nhân viên trên Grid
2. Nhấn **"Xóa"** → Hộp thoại xác nhận xuất hiện
3. **Lưu ý:** Nhân viên đã từng lập hóa đơn → **KHÔNG thể xóa** (phải chuyển trạng thái sang "Đã nghỉ")

**Tìm kiếm / Lọc:**
- Nhập tên hoặc mã vào ô **"Tìm kiếm"** → Nhấn Tìm
- Chọn **Chức vụ** tại ComboBox → Grid tự động lọc
- Có thể kết hợp cả 2 điều kiện

---

### 3.2 Quản lý Khách hàng

**Đường dẫn**: Menu → Khách hàng → Quản lý Khách hàng

#### 3.2.1 Giao diện tổng quan

```text
╔══════════════════════════════════════════════════════════════════╗
║  QUẢN LÝ KHÁCH HÀNG                                    [_][X] ║
╠══════════════════════════════════════════════════════════════════╣
║ Tìm kiếm (SĐT/Tên): [____________]  [🔍 Tìm]                 ║
║                                                                  ║
║ ┌────────────────────────────────────────────────────────────┐  ║
║ │ Mã    │ Họ Tên           │ SĐT          │ Điểm │ Tổng chi │  ║
║ │ KH001 │ Nguyễn Minh Anh  │ 0908111222   │ 150  │ 1,500k   │  ║
║ │ KH002 │ Trần Thị Bình    │ 0917333444   │ 0    │ 0        │  ║
║ └────────────────────────────────────────────────────────────┘  ║
║                                                                  ║
║ --- THÔNG TIN ---                                                ║
║ Họ Tên: [________________]  SĐT:      [__________]             ║
║ Email:  [________________]  Giới tính: [▼ Nam]                  ║
║ Địa chỉ: [______________________________________________]      ║
║ ┌── Tích lũy ──┐                                                ║
║ │ Điểm:    150  │  Tổng chi: 1,500,000đ                        ║
║ └───────────────┘                                                ║
║ [+ Thêm] [✏ Sửa] [🗑 Xóa] [↻ Làm mới] [🚪 Thoát]             ║
╚══════════════════════════════════════════════════════════════════╝
```

#### 3.2.2 Các thao tác

**Thêm khách hàng:**
1. Nhấn **"Làm mới"** → Mã tự sinh
2. Nhập **SĐT** (bắt buộc, 10 số, bắt đầu bằng 0, không trùng)
3. Nhập **Họ tên** (bắt buộc)
4. Email, Địa chỉ (không bắt buộc)
5. Nhấn **"Thêm"** → Điểm mặc định = 0, Tổng chi = 0

**Tìm kiếm nhanh:**
- Nhập **SĐT** hoặc **Tên** → Grid lọc realtime
- Dùng khi nhân viên POS cần nhận diện khách tại quầy

**Đăng ký nhanh từ POS:**
- Tại form Bán Hàng → Nhấn **"Đăng ký nhanh"**
- Popup form Khách hàng mở → Nhập SĐT + Tên → Lưu
- Thông tin tự động trả về form Bán Hàng

---

### 3.3 Quản lý Dịch vụ / F&B

**Đường dẫn**: Menu → Dịch vụ → Quản lý Dịch vụ

#### 3.3.1 Các thao tác

**Thêm sản phẩm/dịch vụ:**
1. Nhấn **"Làm mới"** → Mã tự sinh theo danh mục (F001 = Food, D001 = Drink)
2. Chọn **Danh mục** (bắt buộc): Đồ ăn, Đồ uống, Dịch vụ thuê...
3. Nhập **Tên** + **Đơn giá** (≥ 0)
4. Chọn **Khu vực**: "-- Toàn công viên --" hoặc khu vực cụ thể
5. Nhấn **"Thêm"**

**Ẩn sản phẩm khỏi POS:**
- Sửa trạng thái → **"Ngừng bán"** → Sản phẩm không hiện trên menu POS
- Dùng khi hết hàng hoặc seasonal item

**Lưu ý quan trọng:**
> ⚠️ **Không thể xóa** sản phẩm đã có trong hóa đơn. Nếu muốn ngừng bán, hãy đổi trạng thái thay vì xóa.

---

## 4. CÁC PHÍM TẮT SPRINT 2

| Phím | Chức năng | Form áp dụng |
|:----:|:----------|:-------------|
| F5 | Làm mới dữ liệu | Tất cả form CRUD |
| Esc | Đóng form hiện tại | Tất cả |
| Enter | Xác nhận thao tác | Tất cả |
| Ctrl+S | Lưu dữ liệu | frmNhanVien, frmKhachHang |

---

## 5. XỬ LÝ SỰ CỐ SPRINT 2

| Sự cố | Nguyên nhân | Giải pháp |
|:------|:-----------|:----------|
| Ảnh NV không hiển thị | Thư mục `Images\NhanVien\` chưa tạo hoặc thiếu file ảnh | Tạo thư mục + copy ảnh vào đúng tên MaCode |
| Lỗi "Duplicate entry SĐT" | Khách hàng đã đăng ký trước đó | Tìm kiếm theo SĐT để xác nhận trước khi thêm |
| Ảnh bị xoay ngang | Bug DEF-S2-002 (ảnh EXIF chụp dọc từ điện thoại) | Tạm thời: Xoay ảnh 90° trên máy trước khi upload |
| Grid NV trống sau startup | Connection string sai hoặc DB chưa có dữ liệu | Kiểm tra frmConfigConnect → Test kết nối |
| Combo Khu vực không load | Bug DEF-S2-018: cần đóng mở lại form | Đóng form → Mở lại để refresh combo |

---

## 6. LIÊN HỆ HỖ TRỢ

Nếu gặp vấn đề, vui lòng liên hệ nhóm phát triển:
- **Nhóm**: NguyenTanNhi
- **Email**: [Email nhóm]
- **Điện thoại**: [SĐT nhóm]
