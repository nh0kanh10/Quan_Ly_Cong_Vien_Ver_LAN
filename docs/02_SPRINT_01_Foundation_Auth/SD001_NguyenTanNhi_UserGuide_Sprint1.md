# HƯỚNG DẪN CÀI ĐẶT VÀ SỬ DỤNG – Sprint 1

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_UG_Sprint1_v1.0  
**Nhóm**: NguyenTanNhi  
**Đề tài**: Hệ thống Quản lý Công viên Vui chơi Giải trí  
**Phiên bản**: 1.0  
**Ngày tạo**: 16/03/2026  

---

## 1. YÊU CẦU HỆ THỐNG

### 1.1. Máy chủ (Server)
| Yêu cầu | Chi tiết |
|----------|---------|
| Hệ điều hành | Windows 10/11 hoặc Windows Server 2016+ |
| SQL Server | SQL Server 2019 Express trở lên |
| RAM | Tối thiểu 4 GB |
| Dung lượng trống | 500 MB |
| Mạng | Kết nối mạng LAN |

### 1.2. Máy trạm (Client)
| Yêu cầu | Chi tiết |
|----------|---------|
| Hệ điều hành | Windows 10/11 |
| .NET Framework | 4.7.2 trở lên |
| RAM | Tối thiểu 2 GB |
| Mạng | Kết nối cùng mạng LAN với Server |

---

## 2. CÀI ĐẶT

### 2.1. Cài đặt SQL Server (Máy chủ)

1. Tải **SQL Server 2019 Express** từ trang chính thức Microsoft.
2. Cài đặt với tùy chọn **Basic Installation**.
3. Ghi nhớ **Instance Name** (mặc định: `SQLEXPRESS`).
4. Cài đặt **SQL Server Management Studio (SSMS)** để quản lý database.

### 2.2. Tạo Database

1. Mở SSMS, kết nối đến SQL Server Instance.
2. Click phải vào **Databases** → **New Database** → Đặt tên: `QuanLyCongVien`.
3. Chạy script tạo bảng có sẵn trong thư mục dự án (nếu có).
4. Hoặc sử dụng chức năng **Restore Database** nếu có file `.bak`.

### 2.3. Cấu hình SQL Server cho kết nối LAN

1. Mở **SQL Server Configuration Manager**.
2. Vào **SQL Server Network Configuration** → Protocols for SQLEXPRESS.
3. Bật **TCP/IP** → Click phải → Properties → Tab **IP Addresses**:
   - Kéo xuống **IPAll** → Đặt **TCP Port** = `1433`
   - Xóa giá trị ở **TCP Dynamic Ports**
4. Restart dịch vụ SQL Server.
5. Mở **Windows Firewall** → Cho phép cổng **1433** (Inbound Rule).

### 2.4. Cài đặt ứng dụng (Máy trạm)

1. Copy thư mục `bin\Debug` (hoặc `bin\Release`) sang máy trạm.
2. Đảm bảo máy trạm đã cài **.NET Framework 4.7.2**.
3. Chạy file `WindowsFormsApp1.exe`.

### 2.5. Cấu hình kết nối

Khi chạy ứng dụng lần đầu:
1. Vào **Hệ thống** → **Cấu hình kết nối**.
2. Nhập thông tin:
   - **Server**: `IP_MÁY_CHỦ\SQLEXPRESS` (vd: `192.168.1.100\SQLEXPRESS`)
   - **Database**: `QuanLyCongVien`
   - **Xác thực**: Chọn Windows Authentication hoặc SQL Authentication
   - Nếu SQL Auth: Nhập **User** và **Password**
3. Nhấn **Kiểm tra kết nối** để xác nhận.
4. Nhấn **Lưu**.

> **Lưu ý**: File `connection.txt` sẽ được tạo tự động trong cùng thư mục với ứng dụng.

---

## 3. HƯỚNG DẪN SỬ DỤNG

### 3.1. Đăng nhập

1. Mở ứng dụng → Hiện form **Đăng nhập**.
2. Nhập **Tên đăng nhập** và **Mật khẩu**.
3. Nhấn **Đăng nhập**.
4. Nếu chưa có tài khoản → Nhấn **Đăng ký** để tạo mới.

### 3.2. Giao diện chính

Sau khi đăng nhập thành công:
- **Thanh menu** phía trên gồm các tab: Hệ thống, Khu vực, Trò chơi, Vé...
- **Thanh trạng thái** phía dưới hiển thị: Tên người dùng, Quyền, Ngày hiện tại.

### 3.3. Quản lý Loại Vé

1. Vào menu **Vé** → **Quản lý loại vé**.
2. **Xem danh sách**: Bảng grid hiển thị tất cả loại vé.
3. **Thêm loại vé mới**:
   - Nhấn **Làm mới** để xóa form.
   - Điền thông tin: Tên vé, Giá, Đối tượng, Trạng thái.
   - Bật **Là vé combo** nếu muốn tạo vé combo → Chọn vé con, nhấn Thêm vé con.
   - Nhấn **Thêm**.
4. **Sửa loại vé**: Click chọn vé trong grid → Sửa thông tin → Nhấn **Sửa**.
5. **Xóa loại vé**: Click chọn vé → Nhấn **Xóa** → Xác nhận.
6. **Tìm kiếm**: Nhập từ khóa → Nhấn Tìm. Có thể lọc theo Đối tượng.

### 3.4. Quản lý Khu Vực

1. Vào menu **Khu vực** → **Quản lý khu vực**.
2. **Thêm khu vực**: Nhấn Làm mới → Điền Tên, Mô tả, Trạng thái → Nhấn Thêm.
3. **Sửa/Xóa**: Tương tự form Loại Vé.
4. **Lọc**: Sử dụng Cbo Lọc trạng thái để lọc nhanh.

> **Lưu ý**: Không thể xóa khu vực đang có trò chơi.

### 3.5. Quản lý Trò Chơi

1. Vào menu **Trò chơi** → **Quản lý trò chơi**.
2. **Thêm trò chơi**: Nhấn Làm mới → Điền: Tên, Khu vực, Loại, Sức chứa, Tuổi tối thiểu, Chiều cao, Thời gian/lượt → Nhấn Thêm.
3. **Lọc kép**: Lọc theo Khu vực + Loại trò chơi đồng thời.
4. **Sửa/Xóa**: Tương tự.

> **Lưu ý**: Sức chứa phải > 0. Các ô số chỉ được nhập số nguyên.

---

## 4. XỬ LÝ SỰ CỐ THƯỜNG GẶP

| Sự cố | Nguyên nhân | Giải pháp |
|-------|------------|-----------|
| Không kết nối được Server | TCP/IP chưa bật, Firewall chặn | Bật TCP/IP, mở port 1433 |
| Lỗi đăng nhập SQL | Sai credentials hoặc chưa bật Mixed Mode | Kiểm tra User/Pass, bật Mixed Mode Authentication |
| Ứng dụng không chạy | Thiếu .NET Framework | Cài .NET Framework 4.7.2 |
| Lỗi "Access Denied" | Quyền truy cập SQL Server | Thêm quyền cho user trong SSMS |

---

## 5. LIÊN HỆ HỖ TRỢ

Nếu gặp vấn đề, vui lòng liên hệ nhóm phát triển:
- **Nhóm**: NguyenTanNhi
- **Email**: [Email nhóm]
- **Điện thoại**: [SĐT nhóm]
