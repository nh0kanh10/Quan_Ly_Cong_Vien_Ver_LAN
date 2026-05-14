# Danh Sách Task — pos-luu-tru-cho-thue-fix

## Task 1: Thêm error keys mới vào AppConstants và UIStrings.resx
- [x] 1.1 Thêm `ERR_SYS_PHU_THU_KHONG_TON_TAI` và `ERR_CHECKOUT_TIEN_KHONG_DU` vào `AppConstants.ErrorMessages`
- [x] 1.2 Thêm 2 key trên vào `UIStrings.resx` (vi), `UIStrings.en-US.resx`, `UIStrings.zh-CN.resx`

## Task 2: Fix BUS_LuuTru_Booking — SubmitChanges ngoài transaction (LỖI-01)
- [x] 2.1 Bỏ tất cả `db.SubmitChanges()` trung gian trong `DatPhongVaCheckIn()`, chỉ giữ 1 lần duy nhất trước `transaction.Commit()`

## Task 3: Fix BUS_LuuTru_Booking — SYS_PHU_THU fallback Id=1 (LỖI-03)
- [x] 3.1 Sửa `XuLyCheckOut_MotPhong()`: fail với `ERR_SYS_PHU_THU_KHONG_TON_TAI` thay vì fallback `Id = 1`
- [x] 3.2 Sửa `PhuThuDichVu()`: cùng fix như trên

## Task 4: Fix BUS_LuuTru_Booking — Validate tiền check-out và double-count (LỖI-06, LỖI-07)
- [x] 4.1 Thêm validation `tienKhachTra >= soTienConLai` trước khi xử lý DB trong `XuLyCheckOut_MotPhong()`
- [x] 4.2 Sửa tính `TongTienHang`: gọi `SubmitChanges()` trước khi query `allDetails`, bỏ cộng thủ công

## Task 5: Fix DAL_ThueDo — Race condition tài sản vật lý (LỖI-02)
- [x] 5.1 Trong `GiaoDo()`, thêm check lại `TrangThai == SanSang` bên trong transaction trước khi đổi sang `DangThue`

## Task 6: Fix DAL_ThueDo — IdPhienThuNgan và TienPhiDichVu (LỖI-09, LỖI-10)
- [x] 6.1 Gán `donHang.IdPhienThuNgan = req.IdPhienThuNgan` khi tạo DonHang trong `GiaoDo()`
- [x] 6.2 Sửa `TienPhiDichVu = 0`, thêm comment giải thích tiền cọc theo dõi qua ChungTuTC

## Task 7: Fix DAL_ThueDo — IdPhienTra khi trả đồ (LỖI-05)
- [x] 7.1 Thêm tham số `int idPhienThuNgan` vào `TraDo()`
- [x] 7.2 Gán `phienThue.IdPhienTra = idPhienThuNgan` cho từng dòng ThueDoChiTiet được xử lý

## Task 8: Fix BUS_ThueDo — Validate phiên và truyền idPhienThuNgan (LỖI-05, LỖI-09)
- [x] 8.1 Thêm validate `idPhienThuNgan > 0` trong `XuLyGiaoDo()` và `XuLyTraDo()`
- [x] 8.2 Cập nhật `XuLyTraDo()` để truyền `idPhienThuNgan` xuống `DAL_ThueDo.TraDo()`

## Task 9: Fix BUS_POS — Reflection, tồn kho, sinh mã đơn hàng (LỖI-08, LỖI-11, LỖI-12)
- [x] 9.1 Thêm hàm `SinhMaDonHang()` vào `BUS_POS`
- [x] 9.2 Bỏ toàn bộ đoạn reflection + query DB thừa trong `ThanhToanDonHang()`, dùng `item.DonGiaGoc` trực tiếp
- [x] 9.3 Thêm bước kiểm tra tồn kho vật tư trước khi gọi `DAL_POS.Checkout()`

## Task 10: Fix GUI ucPOS — Dùng BUS_POS.SinhMaDonHang() (LỖI-08)
- [x] 10.1 Sửa `ucPOS.cs`: thay `"DH" + DateTime.Now.ToString(...)` bằng `BUS_POS.Instance.SinhMaDonHang()`
