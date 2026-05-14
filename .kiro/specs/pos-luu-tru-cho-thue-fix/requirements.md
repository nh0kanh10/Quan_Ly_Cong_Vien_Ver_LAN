# Tài Liệu Yêu Cầu

## Giới Thiệu

Dự án C# WinForms 3-tier quản lý Khu vui chơi Đại Nam đã phát hiện **12 lỗi nghiệp vụ** trong 3 module: POS (Bán hàng), Lưu Trú (Khách sạn), và Cho Thuê (Đồ dùng). Các lỗi này ảnh hưởng đến tính toàn vẹn dữ liệu, báo cáo doanh thu, và trải nghiệm vận hành. Spec này mô tả yêu cầu fix từng lỗi theo kiến trúc 3-tier (ET → DAL → BUS → GUI), tuân thủ SKILL-forAI và CSHARP_CODING_RULES.

---

## Bảng Chú Giải (Glossary)

- **BUS_LuuTru_Booking**: Lớp BUS xử lý nghiệp vụ đặt phòng, check-in, check-out của module Lưu Trú.
- **BUS_ThueDo**: Lớp BUS xử lý nghiệp vụ giao đồ và nhận trả đồ của module Cho Thuê.
- **BUS_POS**: Lớp BUS xử lý nghiệp vụ thanh toán tại quầy POS.
- **DAL_ThueDo**: Lớp DAL thực thi giao dịch DB cho module Cho Thuê.
- **DAL_POS**: Lớp DAL thực thi giao dịch DB cho module POS.
- **AppConstants**: Lớp hằng số tập trung trong tầng ET, chứa mọi magic string của hệ thống.
- **OperationResult**: Kiểu trả về chuẩn của tầng BUS, bao gồm `Success`, `ErrorMessage` (key dịch), và `Data`.
- **Transaction**: Giao dịch DB (SQL Transaction) đảm bảo tính nguyên tử — hoặc tất cả thành công, hoặc rollback toàn bộ.
- **SubmitChanges**: Lệnh LINQ-to-SQL ghi dữ liệu xuống DB; nếu gọi ngoài transaction thì không thể rollback.
- **SYS_PHU_THU**: Mã sản phẩm hệ thống dùng để ghi nhận các khoản phụ thu (lố giờ, hư hỏng) vào chi tiết đơn hàng.
- **TrangThaiPhong**: Hằng số C# mô tả trạng thái phòng khách sạn (Trong, DangO, ChoDon, BaoTri).
- **TrangThaiBooking**: Hằng số C# mô tả trạng thái phiếu đặt phòng (DatTruoc, DangO, DaTra, DaHuy).
- **ThueDoChiTiet**: Bảng DB lưu từng dòng phiên thuê đồ, có cột `IdPhienTra` để track ca hoàn cọc.
- **IdPhienThuNgan**: Khóa ngoại liên kết đơn hàng với ca làm việc (phiên thu ngân) của nhân viên.
- **ET_CartItem**: Entity chứa thông tin 1 dòng trong giỏ hàng POS, có trường `DonGiaGoc` (giá gốc trước giảm).
- **ET_CartSession**: Entity chứa toàn bộ phiên giỏ hàng POS, bao gồm danh sách `ET_CartItem` và thông tin thanh toán.
- **Race_Condition**: Tình huống 2 tiến trình đồng thời cùng đọc-ghi 1 tài nguyên, dẫn đến kết quả sai.
- **ROWVERSION**: Cột DB tự động tăng mỗi khi row được cập nhật, dùng để phát hiện xung đột đồng thời.

---

## Yêu Cầu

---

### Yêu Cầu 1: Lưu Trú — Đảm Bảo Tính Nguyên Tử Khi Tạo Khách Hàng Mới

**User Story:** Là lễ tân, tôi muốn khi đặt phòng thất bại ở bất kỳ bước nào thì toàn bộ dữ liệu (khách hàng, phiếu đặt, đơn hàng) đều được rollback, để tránh dữ liệu rác trong hệ thống.

#### Tiêu Chí Chấp Nhận

1. WHEN `BUS_LuuTru_Booking` thực thi `DatPhongVaCheckIn()`, THE `BUS_LuuTru_Booking` SHALL không gọi `db.SubmitChanges()` cho các bước tạo `ThongTin`, `KhachHang`, `PhieuDatPhong`, và `DonHang` trước khi `transaction.Commit()`.
2. WHEN bất kỳ bước nào trong `DatPhongVaCheckIn()` ném ngoại lệ sau khi đã tạo `ThongTin` hoặc `KhachHang`, THE `Transaction` SHALL rollback toàn bộ, không để lại bản ghi `ThongTin` hay `KhachHang` mồ côi trong DB.
3. THE `BUS_LuuTru_Booking` SHALL chỉ gọi `db.SubmitChanges()` duy nhất một lần ngay trước `transaction.Commit()` để flush toàn bộ các thay đổi đã được theo dõi bởi LINQ DataContext.
4. IF `db.SubmitChanges()` cuối cùng ném ngoại lệ, THEN THE `Transaction` SHALL rollback và `DatPhongVaCheckIn()` SHALL trả về `OperationResult<bool>.Fail` với message từ exception.

---

### Yêu Cầu 2: Cho Thuê — Ngăn Race Condition Khi Giao Tài Sản Vật Lý

**User Story:** Là quản lý vận hành, tôi muốn hệ thống đảm bảo mỗi tài sản vật lý (xe điện, xe đạp) chỉ được giao cho một khách tại một thời điểm, dù có nhiều nhân viên thao tác đồng thời.

#### Tiêu Chí Chấp Nhận

1. WHEN `DAL_ThueDo` thực thi `GiaoDo()` và có tài sản vật lý (`IdTaiSanChoThue` có giá trị), THE `DAL_ThueDo` SHALL kiểm tra lại `TrangThai == AppConstants.TrangThaiTaiSan.SanSang` của tài sản đó **bên trong transaction** trước khi cập nhật trạng thái sang `DangThue`.
2. IF tài sản vật lý đã có `TrangThai != AppConstants.TrangThaiTaiSan.SanSang` tại thời điểm kiểm tra trong transaction, THEN THE `DAL_ThueDo` SHALL ném `InvalidOperationException` với key `ERR_RENTAL_TAISAN_DANG_THUE` để transaction rollback.
3. THE `BUS_ThueDo` SHALL bắt `InvalidOperationException` từ `DAL_ThueDo.GiaoDo()` và trả về `OperationResult.Fail(AppConstants.ErrorMessages.ERR_RENTAL_TAISAN_DANG_THUE)`.
4. WHILE kiểm tra trạng thái tài sản trong `DAL_ThueDo.GiaoDo()`, THE `DAL_ThueDo` SHALL thực hiện kiểm tra này trong cùng `DaiNamDBDataContext` đang giữ transaction, không tạo context mới.

---

### Yêu Cầu 3: Lưu Trú — Bắt Buộc Tồn Tại Sản Phẩm SYS_PHU_THU Trước Khi Ghi Phụ Thu

**User Story:** Là kế toán, tôi muốn các khoản phụ thu (lố giờ, hư hỏng) luôn được gắn đúng vào sản phẩm hệ thống `SYS_PHU_THU`, để báo cáo doanh thu không bị sai lệch.

#### Tiêu Chí Chấp Nhận

1. WHEN `BUS_LuuTru_Booking` cần ghi dòng phụ thu trong `XuLyCheckOut_MotPhong()` hoặc `PhuThuDichVu()`, THE `BUS_LuuTru_Booking` SHALL truy vấn `SanPham` theo `MaSanPham == "SYS_PHU_THU"`.
2. IF `SanPham` với `MaSanPham == "SYS_PHU_THU"` không tồn tại trong DB, THEN THE `BUS_LuuTru_Booking` SHALL trả về `OperationResult<bool>.Fail(AppConstants.ErrorMessages.ERR_SYS_PHU_THU_KHONG_TON_TAI)` thay vì fallback về `Id = 1`.
3. THE `AppConstants.ErrorMessages` SHALL chứa hằng số `ERR_SYS_PHU_THU_KHONG_TON_TAI = "ERR_SYS_PHU_THU_KHONG_TON_TAI"`.
4. THE `UIStrings.resx` (vi), `UIStrings.en-US.resx` (en), và `UIStrings.zh-CN.resx` (zh) SHALL chứa key `ERR_SYS_PHU_THU_KHONG_TON_TAI` với nội dung phù hợp từng ngôn ngữ.

---

### Yêu Cầu 4: Lưu Trú — Đồng Bộ Hằng Số Trạng Thái Phòng Giữa C# và Database

**User Story:** Là lập trình viên bảo trì, tôi muốn các hằng số trạng thái phòng trong C# khớp chính xác với giá trị trong bảng `HeThong.TuDien` của DB, để trigger và luồng trạng thái hoạt động đúng.

#### Tiêu Chí Chấp Nhận

1. THE `AppConstants.TrangThaiPhong.ChoDon` SHALL có giá trị `"DonDep"` (khớp với giá trị seed trong `HeThong.TuDien`), không phải `"ChoDon"`.
2. THE `AppConstants.TrangThaiPhong.DangO` SHALL có giá trị `"DangSuDung"` (khớp với giá trị seed trong `HeThong.TuDien`).
3. THE `AppConstants.TrangThaiPhong.Trong` SHALL có giá trị `"Trong"` (khớp với giá trị seed trong `HeThong.TuDien`).
4. THE `AppConstants.TrangThaiPhong.BaoTri` SHALL có giá trị `"BaoTri"` (khớp với giá trị seed trong `HeThong.TuDien`).
5. WHEN `BUS_LuuTru_Booking` cập nhật trạng thái phòng sang `ChoDon` sau check-out, THE `BUS_LuuTru_Booking` SHALL sử dụng `AppConstants.TrangThaiPhong.ChoDon` (giá trị `"DonDep"`) để đảm bảo trigger DB nhận diện đúng.

> **Ghi chú phân tích:** Sau khi đọc `AppConstants.cs`, xác nhận `TrangThaiPhong.ChoDon = "DonDep"` và `TrangThaiPhong.DangO = "DangSuDung"` đã đúng trong code hiện tại. Yêu cầu này đảm bảo không ai thay đổi các giá trị này về sau mà không có spec rõ ràng.

---

### Yêu Cầu 5: Cho Thuê — Ghi Nhận Ca Hoàn Cọc Khi Trả Đồ

**User Story:** Là thủ quỹ, tôi muốn biết ca làm việc nào đã thực hiện hoàn cọc cho khách, để đối soát tiền mặt cuối ca chính xác khi có giao ca chéo.

#### Tiêu Chí Chấp Nhận

1. THE `DAL_ThueDo.TraDo()` SHALL nhận thêm tham số `int idPhienThuNgan` trong signature của hàm.
2. WHEN `DAL_ThueDo` cập nhật `ThueDoChiTiet` sang trạng thái `DaTra` hoặc `MatDo`, THE `DAL_ThueDo` SHALL gán `phienThue.IdPhienTra = idPhienThuNgan` cho từng dòng `ThueDoChiTiet` được xử lý.
3. THE `BUS_ThueDo.XuLyTraDo()` SHALL truyền `idPhienThuNgan` (từ phiên thu ngân hiện tại của nhân viên) xuống `DAL_ThueDo.TraDo()`.
4. WHEN `idPhienThuNgan <= 0`, THE `BUS_ThueDo` SHALL trả về `OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_NO_OPEN_SESSION)` trước khi gọi DAL.

---

### Yêu Cầu 6: Lưu Trú — Validate Tiền Khách Trả Đủ Trước Khi Check-Out

**User Story:** Là lễ tân, tôi muốn hệ thống ngăn check-out khi khách chưa trả đủ tiền, để tránh thất thu doanh thu.

#### Tiêu Chí Chấp Nhận

1. WHEN `BUS_LuuTru_Booking` thực thi `XuLyCheckOut_MotPhong()`, THE `BUS_LuuTru_Booking` SHALL tính `soTienConLai = TongThanhToan - TienCoc` trước khi xử lý bất kỳ thay đổi DB nào.
2. IF `tienKhachTra < soTienConLai` (tiền khách trả nhỏ hơn số tiền còn lại sau khi trừ cọc), THEN THE `BUS_LuuTru_Booking` SHALL trả về `OperationResult<bool>.Fail(AppConstants.ErrorMessages.ERR_CHECKOUT_TIEN_KHONG_DU)` ngay lập tức.
3. THE `AppConstants.ErrorMessages` SHALL chứa hằng số `ERR_CHECKOUT_TIEN_KHONG_DU = "ERR_CHECKOUT_TIEN_KHONG_DU"`.
4. THE `UIStrings.resx` (vi), `UIStrings.en-US.resx` (en), và `UIStrings.zh-CN.resx` (zh) SHALL chứa key `ERR_CHECKOUT_TIEN_KHONG_DU` với nội dung phù hợp từng ngôn ngữ.
5. WHERE `tienKhachTra == 0` và `soTienConLai == 0` (khách đã cọc đủ 100%), THE `BUS_LuuTru_Booking` SHALL cho phép check-out bình thường mà không báo lỗi.

---

### Yêu Cầu 7: Lưu Trú — Tính Đúng TongTienHang Khi Check-Out (Tránh Double-Count)

**User Story:** Là kế toán, tôi muốn tổng tiền hàng trên đơn check-out phản ánh chính xác tất cả các khoản phụ thu, không bị tính hai lần.

#### Tiêu Chí Chấp Nhận

1. WHEN `BUS_LuuTru_Booking` tính `bill.TongTienHang` trong `XuLyCheckOut_MotPhong()`, THE `BUS_LuuTru_Booking` SHALL gọi `db.SubmitChanges()` để flush các dòng phụ thu mới vào DB **trước khi** query `allDetails` từ DB.
2. THE `BUS_LuuTru_Booking` SHALL tính `bill.TongTienHang = allDetails.Sum(x => x.ThanhTien ?? 0)` mà **không** cộng thêm `tienPhuThuLoGio` hay `soTienPhuThu` thủ công sau đó.
3. WHEN `allDetails` được query sau `SubmitChanges()`, THE `allDetails` SHALL bao gồm tất cả các dòng phụ thu vừa được insert (lố giờ và phụ thu thủ công).
4. IF một dòng `ChiTietDonHang` có `ThanhTien == null` hoặc `ThanhTien == 0`, THEN THE `BUS_LuuTru_Booking` SHALL tính lại `ThanhTien = SoLuong * DonGiaThucTe` trước khi sum.

---

### Yêu Cầu 8: POS — Sinh Mã Đơn Hàng Không Trùng Lặp

**User Story:** Là quản trị hệ thống, tôi muốn mã đơn hàng POS luôn là duy nhất dù nhiều máy POS hoạt động đồng thời, để tránh lỗi vi phạm ràng buộc UNIQUE của DB.

#### Tiêu Chí Chấp Nhận

1. THE `BUS_POS` SHALL cung cấp hàm `SinhMaDonHang()` để tạo mã đơn hàng duy nhất, thay vì để GUI tự sinh.
2. THE `BUS_POS.SinhMaDonHang()` SHALL sinh mã theo định dạng `"DH" + DateTime.Now.ToString("yyMMddHHmmssfff") + "-" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper()` để đảm bảo đủ entropy ngay cả khi 2 máy POS gọi cùng millisecond.
3. THE `GUI/Modules/BanHang/ucPOS.cs` SHALL gọi `BUS_POS.Instance.SinhMaDonHang()` để lấy mã đơn hàng, không tự sinh bằng `"DH" + DateTime.Now.ToString(...)` trực tiếp trong GUI.
4. WHEN `BUS_POS.SinhMaDonHang()` được gọi nhiều lần liên tiếp, THE `BUS_POS` SHALL trả về các mã khác nhau trong mọi trường hợp.

---

### Yêu Cầu 9: Cho Thuê — Gán IdPhienThuNgan Vào Đơn Thuê

**User Story:** Là quản lý ca, tôi muốn mỗi đơn thuê đồ được liên kết với ca làm việc tương ứng, để báo cáo doanh thu theo ca chính xác.

#### Tiêu Chí Chấp Nhận

1. WHEN `DAL_ThueDo` tạo `DonHang` trong `GiaoDo()`, THE `DAL_ThueDo` SHALL gán `donHang.IdPhienThuNgan = req.IdPhienThuNgan`.
2. THE `DTO_RentalCheckoutRequest` SHALL có trường `IdPhienThuNgan` (đã tồn tại) và `DAL_ThueDo.GiaoDo()` SHALL sử dụng giá trị này khi tạo đơn hàng.
3. WHEN `req.IdPhienThuNgan <= 0`, THE `BUS_ThueDo.XuLyGiaoDo()` SHALL trả về `OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_NO_OPEN_SESSION)` trước khi gọi DAL.

---

### Yêu Cầu 10: Cho Thuê — Tách Bạch Tiền Cọc Khỏi TienPhiDichVu

**User Story:** Là kế toán, tôi muốn tiền cọc và phí dịch vụ được ghi nhận vào đúng trường tương ứng trong đơn hàng, để đối soát tài chính chính xác.

#### Tiêu Chí Chấp Nhận

1. WHEN `DAL_ThueDo` tạo `DonHang` trong `GiaoDo()`, THE `DAL_ThueDo` SHALL gán `donHang.TienPhiDichVu = 0` (không nhét tiền cọc vào đây).
2. THE `DAL_ThueDo` SHALL gán `donHang.TongThanhToan = tongThue + tongCoc` để tổng thanh toán vẫn đúng.
3. THE `DAL_ThueDo` SHALL thêm comment giải thích rõ: tiền cọc được theo dõi qua `ChungTuTC` loại `THU_COC` và bảng `ThueDoChiTiet.TienCoc`, không phải qua `TienPhiDichVu`.

---

### Yêu Cầu 11: POS — Kiểm Tra Tồn Kho Trước Khi Thanh Toán

**User Story:** Là thu ngân, tôi muốn hệ thống ngăn thanh toán khi tồn kho vật tư không đủ, để tránh bán âm kho.

#### Tiêu Chí Chấp Nhận

1. WHEN `BUS_POS` thực thi `ThanhToanDonHang()`, THE `BUS_POS` SHALL kiểm tra tồn kho cho tất cả các `ET_CartItem` có `LaVatTu == true` trước khi gọi `DAL_POS.Checkout()`.
2. IF tồn kho hiện tại của bất kỳ vật tư nào nhỏ hơn `(item.SoLuong * item.HeSoQuyDoi)`, THEN THE `BUS_POS` SHALL trả về `OperationResult.Fail(AppConstants.ErrorMessages.ERR_POS_OUT_OF_STOCK)` với thông tin tên sản phẩm thiếu hàng.
3. THE `BUS_POS` SHALL gọi `GetTonKhoHienTai(idKhoXuatBan, item.IdSanPham)` để lấy tồn kho, chỉ khi `idKhoXuatBan > 0`.
4. WHERE `idKhoXuatBan <= 0` (không có kho gắn với điểm bán), THE `BUS_POS` SHALL bỏ qua kiểm tra tồn kho và tiếp tục thanh toán bình thường.
5. IF `cart.DanhSachDong` không có item nào có `LaVatTu == true`, THE `BUS_POS` SHALL bỏ qua toàn bộ bước kiểm tra tồn kho.

---

### Yêu Cầu 12: POS — Loại Bỏ Reflection Query Giá Gốc Trong ThanhToanDonHang

**User Story:** Là lập trình viên bảo trì, tôi muốn luồng thanh toán POS không thực hiện query DB thừa và không dùng reflection trên anonymous type, để tránh crash runtime khi cấu trúc query thay đổi.

#### Tiêu Chí Chấp Nhận

1. THE `BUS_POS.ThanhToanDonHang()` SHALL không gọi `DAL_POS.Instance.LayDanhSachSanPhamPOS(null)` bên trong luồng thanh toán.
2. THE `BUS_POS.ThanhToanDonHang()` SHALL không sử dụng `System.Reflection` (`.GetType().GetProperties()`, `.GetValue()`) để lấy giá trị từ anonymous type.
3. WHEN `BUS_POS` cần giá gốc của một `ET_CartItem` để tính giảm giá, THE `BUS_POS` SHALL sử dụng trực tiếp `item.DonGiaGoc` đã có sẵn trong `ET_CartItem`.
4. WHEN `item.DonGiaGoc == 0` và `item.HeSoQuyDoi == 1`, THE `BUS_POS` SHALL sử dụng `item.DonGiaThucTe` làm giá gốc để tính tổng tiền được phép giảm giá.
5. THE `BUS_POS` SHALL vẫn đảm bảo logic rải tiền giảm giá (`TienGiamGiaDong`) hoạt động đúng sau khi bỏ reflection, dựa hoàn toàn vào dữ liệu đã có trong `cart.DanhSachDong`.

---

## Tóm Tắt Các Error Key Mới Cần Thêm

| Key | Module | Mô tả (vi) |
|-----|--------|------------|
| `ERR_SYS_PHU_THU_KHONG_TON_TAI` | Lưu Trú | Sản phẩm hệ thống SYS_PHU_THU chưa được seed vào DB |
| `ERR_CHECKOUT_TIEN_KHONG_DU` | Lưu Trú | Tiền khách trả chưa đủ để check-out |

> Các key `ERR_RENTAL_TAISAN_DANG_THUE`, `ERR_POS_NO_OPEN_SESSION`, `ERR_POS_OUT_OF_STOCK` đã tồn tại trong `AppConstants.ErrorMessages`.
