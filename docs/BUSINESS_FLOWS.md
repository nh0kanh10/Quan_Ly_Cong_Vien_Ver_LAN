# LUỒNG NGHIỆP VỤ TỔNG HỢP — KHU DU LỊCH ĐẠI NAM

> **Mục tiêu:** Mô tả luồng xử lý nghiệp vụ **theo đúng triển khai trong source** (tầng `BUS`, form WinForms, hằng số `ET.AppConstants`).  
> **Cập nhật:** 2026-04-03  
> **Tham chiếu chính:** `BUS/BUS_DonHang.cs`, `BUS/BUS_VeDienTu.cs`, `BUS/BUS_KhoHang.cs`, `BUS/BUS_GiaoDichVi.cs`, `BUS/BUS_ThueDo.cs`, `BUS/BUS_Phong.cs`, `BUS/BUS_GuiXe.cs`, `BUS/BUS_BangGia.cs`, `BUS/BUS_KhuyenMai.cs`, `BUS/BUS_QuyenHan.cs`, `WindowsFormsApp1/Form1.cs`, `WindowsFormsApp1/frmBanHang.cs`, `ET/AppConstants.cs`.

---

## 0. KHUNG ỨNG DỤNG (SHELL, ĐĂNG NHẬP, PHÂN QUYỀN)

### 0.1. Khởi động

- `Program.cs`: nếu `BUS_Connection.Instance.CheckConnection()` thành công → `Application.Run(new frmLogin())`, ngược lại → `frmConfigConnect`.
- Đăng nhập (`frmLogin.cs`): `BUS_NhanVien.Instance.DangNhap(user, pass)` → nếu OK: `SessionManager.CurrentUser = tk`, mở `Form1`, gán `main.Tag = tk`.

### 0.2. Phiên làm việc

- `ET.SessionManager.CurrentUser` giữ `ET_NhanVien` đăng nhập (dùng chung cho RBAC và một số BUS như `BUS_KhuyenMai.ThemKhuyenMai`).
- `Form1` cache child form theo `Type` (`_formCache`), mỗi form con `Dock = Fill`, `Tag` được gán bằng user (pattern legacy + `SessionManager`).

### 0.3. Điều hướng menu (ribbon)

| Nút ribbon | Nhóm nút sidebar được bật | Ghi chú RBAC (`BUS_QuyenHan.HasPermission`) |
|------------|---------------------------|-----------------------------------------------|
| **DANH MỤC** | Sản phẩm, Bảng giá, Combo, Khu vực, KH, Kho, Phiếu nhập/xuất, Phiếu thu/chi, Thẻ RFID, Ví | `VIEW_PRICE`, `VIEW_REGION`, `VIEW_CUSTOMER`, `VIEW_INVENTORY`, `VIEW_LEDGER`, `VIEW_RFID`, `VIEW_WALLET` |
| **VẬN HÀNH** | Nhân viên, Lịch làm việc, Sự cố, Bảo trì, Trò chơi, Khu thú/biển, Nhà hàng, Đặt bàn, Động vật, Chất lượng nước | Chủ yếu `VIEW_STAFF` / `VIEW_REGION` |
| **GIAO DỊCH** | POS, Tra cứu HĐ, Nạp tiền RFID, App đặt vé mô phỏng, Đặt phòng, Kiểm soát vé, Thuê đồ, Đặt bàn, Gửi xe | `VIEW_POS`, `VIEW_DONHANG`, `VIEW_RFID_TOPUP`, `VIEW_TICKET_SIMULATION`, `VIEW_HOTEL`, `VIEW_ACCESS_CONTROL` |
| **BÁO CÁO** | Mở `frmDashboard` | Cần `VIEW_REPORT` (ẩn một phần UI nếu không có) |
| **HỆ THỐNG** | Khuyến mãi, Vai trò, Phân quyền, Đăng xuất | `VIEW_PROMOTION`, `MANAGE_USER` |

### 0.4. RBAC — hành vi quan trọng

- `BUS_QuyenHan.HasPermission(idVaiTro, maQuyen)`:
  - Load `QuyenHan` + `PhanQuyen` qua DAL, cache theo vai trò.
  - **Compat mode:** nếu `maQuyen` **không tồn tại** trong bảng `QuyenHan` → trả `true` (tránh “tắt hết UI” khi DB chưa seed đủ).
- Menu chỉ **ẩn/hiện** theo quyền; một số form gọi thêm `ApplyPermissions()` để khóa nút CRUD (`MANAGE_*`).

---

## 1. BÁN HÀNG POS — `frmBanHang` → `BUS_DonHang`

### 1.1. Vai trò form

- Giỏ hàng trong RAM (`List<CartItem>`): dòng **sản phẩm** (`IdSanPham`) hoặc **combo** (`IdCombo`).
- Quét mã (`txtScanner` / camera): hỗ trợ `SốLượng*Mã` (ví dụ `10*VEM001`).
- Chọn **kho trừ tồn** (`cboKhoXuLy`): `BUS_KhoHang.Instance.LoadDS()` — ID này truyền vào `WriteLedgerTuDong` sau thanh toán.
- Phím tắt: F12 thanh toán tiền mặt, F1 tìm SP, F2 mã KH, Esc hủy đơn, F8 tiền khách đưa.

### 1.2. Giá hiển thị & thêm giỏ

- Giá động: `BUS_BangGia.Instance.GetDynamicPrice(idSanPham, "GiaBan", DateTime.Now)` (xem mục 8).
- Phân loại UI: nút category lọc `AppConstants.LoaiSanPham`: `Ve`, `AnUong`, `Thue`.

### 1.3. Khách hàng & loyalty (trước thanh toán)

- Tra cứu KH: `BUS_KhachHang.Instance.GetByMaCodeOrSdt`.
- Chiết khấu VIP: hàm nội bộ `GetVipDiscount(LoaiKhach)` (phần trăm theo loại KH).
- **Không cộng dồn** khuyến mãi sự kiện với VIP: lấy `pctApDung = Max(pctVip, pctEvent)` với `pctEvent` từ `BUS_KhuyenMai.Instance.GetBestActivePromotion(tongTienGoc)` (chương trình có `GiaTriGiam` lớn nhất trong các KM đang active và đủ điều kiện tối thiểu).
- **Điểm tích lũy:** so sánh `tienGiamDiem` (từ `BUS_TichDiem`) với `tienGiamChietKhau`; nếu điểm lợi hơn → hỏi popup có dùng điểm không.
- `TongTien` đơn = tổng giỏ; `TienGiamGia` trên `ET_DonHang` = khấu trừ đã chốt (VIP/KM hoặc điểm).

### 1.4. Luồng thanh toán — `frmBanHang.ThanhToan(phuongThuc)`

1. Validate: giỏ không rỗng; nếu `ViRfid` thì **bắt buộc** có `_selectedKH`.
2. Tính `tongTienGoc`, giảm giá, `soTienThucThu = tongTienGoc - tienGiam`.
3. Xác nhận popup tổng hợp (gồm gợi ý điểm sẽ cộng sau thanh toán).
4. Tạo `ET_DonHang`: `MaCode` dạng `DH-` + `yyMMddHHmmss` + suffix GUID; `TrangThai = DaThanhToan`; `TongTien = tongTienGoc`; `TienGiamGia = tienGiam`.
5. Map giỏ → `List<ET_ChiTietDonHang>` (mỗi dòng `IdSanPham` hoặc `IdCombo`, `SoLuong`, `DonGiaGoc`/`DonGiaThucTe`).

**Nhánh A — Tiền mặt / CK / thẻ (có `PhieuThu`):**

- Tạo `ET_PhieuThu` với `SoTien = soTienThucThu`, `PhuongThuc` = tham số.
- Gọi `BUS_DonHang.Instance.ThemDonHangVaChiTiet(donHang, chiTiet, phieuThu, idKhoDeduct)`.

**Nhánh B — Ví RFID:**

- Gọi `BUS_DonHang.Instance.ThanhToanBangVi(donHang, chiTiet, _selectedKH.Id, currentUserId, idKhoDeduct)`  
  - **Không** tạo `PhieuThu` trong nhánh này (tiền trừ ví + log `GiaoDichVi`).

### 1.5. Hậu xử lý POS (sau khi `OperationResult` thành công)

- **Loyalty:** nếu dùng điểm → `BUS_TichDiem.TieuDiem(...)`; nếu có điểm thưởng → `BUS_TichDiem.CongDiem(...)` (tính trên `tongTienGoc`).
- **Vé:** `BUS_VeDienTu.LayVeTheoDonHang(idDonHang)` — nếu có vé → popup `frmPhatVe` (`ThemeManager.ShowAsPopup`); không thì thông báo thành công thu tiền.

---

## 2. `BUS_DonHang` — GIAO DỊCH LÕI (ĐƠN + CHI TIẾT + VÉ + KHO)

### 2.1. `ThemDonHangVaChiTiet` (một `TransactionScope`)

Thứ tự logic:

1. `ThemVaLayId` đơn hàng.
2. Với từng dòng chi tiết: insert chi tiết; nếu có `IdSanPham` và tồn tại bản ghi `SanPham_Ve` (DAL) → `BUS_VeDienTu.GenerateTicket(...)` với `tongLuot = SoLuotQuyDoi * SoLuong`; nếu là **combo** → duyệt `ComboChiTiet` của combo đó, với mỗi SP có cấu hình vé → sinh vé với `tongLuot = SoLuotQuyDoi * SoLuongTrongCombo * SoLuongCombo`.
3. Nếu có `PhieuThu` → gán `IdDonHang`, insert.
4. `BUS_KhoHang.WriteLedgerTuDong(donHangId, chiTietList, createdBy, idKhoXuLy)` — nếu fail → toàn bộ rollback.

### 2.2. `ThanhToanBangVi`

- `soTienThucThu = TongTien - TienGiamGia`.
- Đọc ví theo KH; kiểm tra `SoDuKhaDung`.
- Trong transaction: tạo đơn + chi tiết + vé (giống trên); sau đó **trừ** `vi.SoDuKhaDung`, `DAL_ViDienTu.Sua(vi)` → có thể **`DBConcurrencyException`** (OCC).
- Insert `GiaoDichVi` loại `ThanhToanDichVu`, liên kết `IdDonHangLienQuan`.
- `WriteLedgerTuDong` — **không** tạo `PhieuThu` trong method này.

---

## 3. VÉ ĐIỆN TỬ — `BUS_VeDienTu`

### 3.1. Sinh vé — `GenerateTicket`

- `MaCode = "TK" + yyMMdd + 5 ký tự random` (không dùng prefix `VEDT` trong code hiện tại).
- `Id` = `Guid`; `IdSanPham` **lưu denormalized** trên `VeDienTu` để soát cổng O(1).
- `SoLuotConLai` ban đầu = tổng lượt quy đổi; `TrangThai` ban đầu `ChuaSuDung`.

### 3.2. Soát vé — `CheckTicket(maCode, idKhuVucTramGac, out veInfo, idThietBi)`

| Mã trả về | Ý nghĩa (theo code) |
|-----------|---------------------|
| **0** | Hợp lệ, đã trừ lượt / cập nhật trạng thái |
| **1** | Sai khu vực (`SanPham.IdKhuVuc` ≠ trạm gác) |
| **2** | Hết lượt / vé hủy hoặc hết hạn |
| **3** | Không tìm thấy / lỗi cập nhật |
| **4** | Đúng khu nhưng sai “trò chơi” khi truyền `idThietBi` (so khớp `SanPham.Id` với thiết bị/trò chọn) |

- Sau khi trừ: nếu `SoLuotConLai <= 0` → `DaSuDung`; nếu còn lượt → `DangSuDung`.
- `frmKiemSoatVe` gọi `CheckTicket` với `idTroChoi` từ UI (ràng buộc vé đúng trò).

### 3.3. `LayVeTheoDonHang`

- 3 bước query có kiểm soát: chi tiết theo đơn → vé theo `IdChiTietDonHang` → tên `SanPham` (không N+1 từng vé).

---

## 4. KHO & BOM POS — `BUS_KhoHang.WriteLedgerTuDong`

- **Prefetch:** dictionary sản phẩm + `ComboChiTiet` theo combo trong giỏ.
- Với dòng **sản phẩm lẻ:** chỉ ghi `TheKho` nếu `LoaiSanPham` ∈ `{ AnUong, DoLuuNiem }` → âm số lượng, `LoaiGiaoDich = "XUAT_POS"`, `GhiChu` dạng `POS Auto-Deduct`.
- Với dòng **combo:** nhân định mức `ComboChiTiet.SoLuong * SoLuong` dòng bán; ghi `TheKho` cho từng SP vật lý thỏa loại trên, `GhiChu` `POS BOM-Deduct (Combo #...)`.
- **Không** cập nhật trực tiếp cột tồng tồn tại đây; tồn suy ra từ bảng sổ (`TheKho`) / đồng bộ qua `DongBoTonKhoTrucTiep` (gọi từ chỗ khác).
- `GetTonKhoChiTiet` / dashboard kho: chỉ hiển thị SP loại `AnUong`, `DoLuuNiem` (và logic cảnh báo ngưỡng 5).

---

## 5. VÍ & RFID — `BUS_GiaoDichVi`, `BUS_ViDienTu` (DAL)

### 5.1. Nạp tiền — `NapTien(maRfid, soTien, phuongThuc)`

- `soTien` ∈ (0; 50_000_000].
- Thẻ phải `TrangThai == Active`.
- Transaction: `SoDuKhaDung += soTien` → `GiaoDichVi` (`NapTien`) → `PhieuThu` (audit, `IdGiaoDichVi`).

### 5.2. Thanh toán POS bằng ví

- Xem mục 2.2; lỗi concurrency trả message cố định cho UI.

### 5.3. Khóa / báo mất thẻ — `KhoaMoThe`

- Cập nhật `TrangThai`; nếu `Lost` hoặc `Locked` → set `NgayHuy`.

---

## 6. THUÊ ĐỒ — `BUS_ThueDo`

### 6.1. `RentMultipleItems`

- Tổng tiền thuê + cọc trên toàn giỏ.
- **ViRfid:** một lần trừ `SoDuKhaDung` cho (thuê + cọc), đồng thời `SoDuDongBang += tongCoc`; log GD thuê (`ThanhToanDichVu`) và GD cọc (`ThuCoc`) nếu có số tiền tương ứng.
- **Tiền mặt:** một `PhieuThu` cho tổng.
- Mỗi instance thuê: tạo `ChiTietDonHang` (bắt buộc có ID) → `ThueDoChiTiet` gắn `IdChiTietDonHang`, `TrangThaiCoc = ChuaHoan`.

### 6.2. `ReturnItem` (trả đồ)

- Chỉ xử lý khi `TrangThaiCoc == ChuaHoan`.
- Tính `tienHoanVeVi`, `tienPhatVuotCoc`.
- Nếu có `IdGiaoDichCoc`: giải phóng đóng băng + cộng lại khả dụng; ghi `GiaoDichVi` loại `HoanCoc` cho phần hoàn.
- Tiền mặt: `PhieuChi` hoàn cọc nếu cần.
- Nếu phạt vượt cọc: tạo thêm `PhieuThu` tiền mặt (`PT-PEN-...`).
- Cập nhật `TrangThaiCoc`: `DaHoan` hoặc `DaPhat`.

---

## 7. KHÁCH SẠN / PHÒNG — `BUS_Phong`

### 7.1. Check-in trực tiếp — `CheckIn(...)`

- Overlap: `GetBusyRoomIds` trong cùng `TransactionScope` (giảm TOCTOU).
- Tạo `DonHang` (`DaThanhToan`), thanh toán `ViRFID` hoặc `PhieuThu`.
- Đổi `Phong.TrangThai = DangSuDung`.
- `ChiTietDonHang` gắn `IdSanPham = phong.IdSanPham`; `DatPhongChiTiet` (`DaNhan`) + `ChiTietDatPhong` (giá qua `TinhGiaPhong`).

### 7.2. Đặt trước — `ReserveRoom`

- Nếu `tienCoc > 0`: `DonHang` (`DaDatCoc`) + `PhieuThu`; `DatPhongChiTiet` không có `IdChiTietDonHang` (chưa gắn line item).
- `TrangThai` booking `DaDat`.

### 7.3. Check-in từ booking — `CheckInFromReservation`

- Thu thêm nếu cần (cập nhật `TongTien` đơn cũ hoặc tạo đơn mới); thanh toán ví hoặc phiếu thu.
- Đổi booking → `DaNhan`, phòng → `DangSuDung`.

### 7.4. Check-out — `CalculateCheckOut` / `ConfirmCheckOut`

- `CalculateCheckOut`: đọc booking `DaNhan` mới nhất theo phòng; tính `TienPhongGoc`, `TinhPhuThuTreGio` (quy tắc bậc: ≤2h 30%, 2–4h 50%, >4h +1 ngày giá `TheoNgay`), `DaThanhToan` từ đơn.
- `ConfirmCheckOut`: thu chênh lệch (nếu có), đổi trạng thái phòng/booking (chi tiết trong phần còn lại file BUS).

---

## 8. BẢNG GIÁ — `BUS_BangGia`

### 8.1. `GetDynamicPrice(idSanPham, maLoaiGia, thoiDiem)`

- Fallback: `SanPham.DonGia`.
- Lọc bảng giá: `TrangThai = Hoạt động`, khoảng ngày, **thứ trong tuần** (`NgayTrongTuan` chứa mã `T2`…`CN`).
- Chọn dòng **ưu tiên nhỏ nhất** (`MucUuTien`).

### 8.2. `TinhTienThueTheoPhut`

- Dựa trên master `BangGia` + chi tiết `BieuDoGio` (block phút, lặp tier cuối).

---

## 9. KHUYẾN MÃI — `BUS_KhuyenMai`

- `GetBestActivePromotion`: KM không xóa, `TrangThai` bật, trong khoảng ngày, đủ `DonToiThieu`, sắp xếp **`GiaTriGiam` giảm dần** → lấy một chương trình “mạnh nhất” cho rule Max-Discount ở POS.
- `KiemTraKhuyenMai(code, totalAmount)`: tra theo mã (khác luồng auto ở trên).

---

## 10. BÃI XE — `BUS_GuiXe`

### 10.1. `NhanXe`

- Kiểm tra biển chưa còn lượt đang gửi; tạo `LuotVaoRaBaiXe` (`DangGui`).

### 10.2. `TraXe`

- `TinhTienGuiXe` theo loại xe + quy tắc block 12h / phụ trội qua đêm (xem comment trong BUS).
- Tạo `DonHang` + `ChiTietDonHang` + `VeDoXeChiTiet` liên kết lượt.
- Thu: **ViRfid** → trừ ví + `GiaoDichVi`; ngược lại → `PhieuThu`.
- Đóng lượt: `DaTra`.

---

## 11. BÁO CÁO — `frmDashboard`

- Lọc ngày; load chỉ số doanh thu / lượt / hóa đơn (gọi các hàm thống kê trong form — không còn `BUS_Dashboard` theo comment trong code).
- `VIEW_REPORT`: nếu không có quyền có thể ẩn `cardRevenue`.

---

## 12. ÁNH XẠ NHANH: FORM → TẦNG NGHIỆP VỤ

| Form / vùng | BUS / service chính |
|-------------|---------------------|
| `frmBanHang` | `BUS_DonHang`, `BUS_SanPham`, `BUS_Combo`, `BUS_BangGia`, `BUS_KhuyenMai`, `BUS_TichDiem`, `BUS_KhoHang`, `BUS_VeDienTu` |
| `frmDonHang`, `frmChiTietHoaDon` | `BUS_DonHang`, `BUS_ChiTietDonHang`, … |
| `frmPhatVe` | Dữ liệu từ `BUS_VeDienTu.LayVeTheoDonHang` (mở sau POS) |
| `frmKiemSoatVe` | `BUS_VeDienTu.CheckTicket`, `BUS_KhuVuc`, `DAL` khu vực / trò chơi |
| `frmQuayNapTien` | `BUS_GiaoDichVi.NapTien` |
| `frmViDienTu`, `frmTheRFID` | `BUS_ViDienTu`, `BUS_GiaoDichVi`, `BUS_TheRFID` |
| `frmThueDo` | `BUS_ThueDo`, `BUS_BangGia` |
| `frmDatPhong` | `BUS_Phong`, `BUS_BangGia` |
| `frmGuiXe` | `BUS_GuiXe` |
| `frmKhoHang`, `frmPhieuNhapXuat`, `frmKiemKho` | `BUS_KhoHang`, phiếu nhập/xuất, `HoanTatKiemKe` |
| `frmPhieuThuChi` | `BUS_PhieuThuChi` |
| `Form1` menu | `BUS_QuyenHan` |

---

## 13. TÓM TẮT MỘT DÒNG (VẬN HÀNH TỔNG)

**Chuỗi lõi:** Cấu hình danh mục & giá → POS (đơn + thu) → sinh vé / trừ kho BOM → (tuỳ module) ví RFID / thuê đồ / phòng / bãi xe → ghi nhận phiếu thu chi & dashboard; cổng soát vé dùng `VeDienTu` + khớp khu vực / trò chơi.

---

## 14. GHI CHÚ KHÁC BIỆT SO VỚI TÀI LIỆU CŨ / TRUYỀN MIỆNG

- Mã vé trong DB là prefix **`TK` + ngày**, không cố định `VEDT+7 số` trong code hiện tại.
- `ThanhToanBangVi` **không** tạo `PhieuThu`; đối soát doanh thu ví qua `GiaoDichVi` (+ báo cáo tổng hợp).
- POS dùng rule **một lớp giảm giá mạnh nhất** (VIP vs KM tự động), và **điểm vs chiết khấu** không cộng dồn (chọn max).
- Trừ kho POS chỉ áp cho **đồ ăn / lưu niệm** (`AnUong`, `DoLuuNiem`), không trừ vé/dịch vụ ảo qua `WriteLedgerTuDong`.
