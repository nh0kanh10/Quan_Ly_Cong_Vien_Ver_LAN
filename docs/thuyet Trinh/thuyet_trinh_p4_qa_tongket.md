# THUYẾT TRÌNH — PHẦN 4: TỔNG KẾT + Q&A CHUẨN BỊ
## Thời gian: ~2 phút tổng kết (13:30 — 15:00) + Q&A sau đó

---

## 4.1 Tổng kết (13:30 — 14:30)

> Tóm lại, hệ thống gồm 56 bảng, 15 module, quản lý từ bán vé cho tới kho hàng. Mọi giao dịch mua bán đều chạy qua bảng DonHang. Thanh toán hỗ trợ nhiều hình thức (tiền mặt, RFID, chuyển khoản, chia bill).

> **Nhóm em tự đánh giá:**

| Cái làm được | Cái chưa tốt |
|-------------|-------------|
| Centralized Order — gom tất cả vào 1 đơn hàng | TongTien không tự validate ở DB |
| Ví RFID có đóng băng cọc | Không có Job tự hết hạn vé |
| RowVersion chống tranh chấp | BangGia thiếu UNIQUE constraint |
| Phân quyền RBAC linh động | GiaGuiXe tách rời khỏi BangGia chung |
| TheKho ledger track mọi biến động kho | Không có lịch sử giá |
| Audit Trail tự động | View doanh thu bỏ sót Combo |

### Hướng phát triển nếu có thời gian:
1. SQL Agent Job hết hạn vé + giải phóng bàn/phòng bỏ trống
2. Bảng lịch sử giá
3. Mobile App cho khách (hiện chỉ có Desktop)
4. Dashboard biểu đồ trực quan
5. Offline mode cho POS khi mất mạng

> Cảm ơn thầy/cô và các bạn, nhóm em sẵn sàng trả lời câu hỏi.

---

## 4.2 CHUẨN BỊ Q&A — 20 CÂU HỎI CÓ THỂ BỊ HỎI

---

### NHÓM A: CÂU HỎI VỀ KIẾN TRÚC & KỸ THUẬT

---

**Câu 1: "Tại sao không dùng Entity Framework mà dùng ADO.NET?"**

> Trả lời: Nhóm em muốn hiểu rõ cách SQL thực sự chạy. EF tiện nhưng giấu hết query đi — viết LINQ mà không biết SQL nó sinh ra gì. Với đề tài học thuật, viết Parameterized Query cho thấy mình hiểu hơn. Ngoài ra, EF có overhead khi ánh xạ entity phức tạp — với 56 bảng thì config cũng mất thời gian.

---

**Câu 2: "56 bảng có nhiều quá không? Có bảng nào dư?"**

> Trả lời: Thành thật thì có 1 số bảng chưa implement hết trong code (trường đua, biển nhân tạo chi tiết). Nhưng schema để sẵn vì bài toán thực tế của Đại Nam cần những bảng đó. Cột `DoanKhach.IdCombo` là ví dụ dư — nó đã deprecated nhưng chưa xóa.

---

**Câu 3: "RowVersion hoạt động cụ thể thế nào trong code?"**

> Trả lời: Khi đọc record, mình lưu RowVer vào biến. Khi UPDATE, mình đặt trong WHERE:
> ```sql
> UPDATE Phong SET TrangThai = 'DaDat' 
> WHERE Id = @id AND RowVer = @oldRowVer
> ```
> Nếu ai sửa trước mình → RowVer đã đổi → WHERE không match → UPDATE ảnh hưởng 0 dòng → code bắt lỗi, thông báo "Dữ liệu đã thay đổi, vui lòng thử lại".

---

**Câu 4: "Soft Delete thì mọi query phải thêm WHERE IsDeleted=0, có phiền không?"**

> Trả lời: Phiền. Nếu quên thì sẽ lấy cả dữ liệu đã xóa. Nhóm em dùng Filtered Index để SQL Server tối ưu performance. Trong code, DAL layer luôn thêm điều kiện `WHERE IsDeleted = 0` mặc định. Nếu cần xem cả dữ liệu đã xóa (để audit), phải gọi hàm riêng.

---

**Câu 5: "Hệ thống có chống SQL Injection không?"**

> Trả lời: Có. DAL dùng Parameterized Queries 100%. Không bao giờ nối chuỗi string vào SQL. Ví dụ dùng `cmd.Parameters.AddWithValue("@Id", id)` thay vì `"WHERE Id = " + id`.

---

**Câu 6: "Kiến trúc 3-tier của nhóm có thể scale lên nhiều máy không?"**

> Trả lời: Hiện tại thì chưa. 3-tier ở đây là logical separation (chia trong 1 solution), không phải physical (chia ra nhiều server). Muốn scale thì phải tách BUS/DAL thành Web API riêng, GUI gọi qua HTTP. Nhưng scope đề tài là Desktop app nên chưa cần.

---

### NHÓM B: CÂU HỎI VỀ NGHIỆP VỤ

---

**Câu 7: "Đoàn khách 500 người thì xử lý đơn hàng thế nào?"**

> Trả lời: Tạo 1 bản ghi DoanKhach (chiết khấu 15%), 1 DonHang tổng, ChiTietDonHang 1 dòng SL=500. Nhưng bảng VeDienTu phải sinh 500 mã GUID riêng → 500 QR để 500 người quét riêng ở cổng. Khách quét mã → trạng thái chuyển 'DaSuDung' ngay → người sau quét lại bị từ chối.

---

**Câu 8: "Khách muốn thanh toán 1 phần tiền mặt, 1 phần RFID thì sao?"**

> Trả lời: Đó là Split Payment. 1 DonHang có thể có NHIỀU PhieuThu. Ví dụ: Đơn 2 triệu → PhieuThu #1: 1.5tr (ViRFID) → PhieuThu #2: 500k (TienMat). Khi SUM(PhieuThu.SoTien) = DonHang.TongTien thì đơn chuyển 'DaThanhToan'.

---

**Câu 9: "Combo phân bổ doanh thu cho từng bộ phận kiểu gì?"**

> Trả lời: ComboChiTiet có cột TyLePhanBo (%). Ví dụ combo 1 triệu: Vé 20%, Biển 50%, Ăn 30%. Cuối tháng kế toán chạy query JOIN ComboChiTiet → chia: Vé 200k, Biển 500k, Ăn 300k. Trigger bắt buộc tổng phải = 100%.

---

**Câu 10: "Khách thuê phao, trả phao bị rách, phạt 30k hoàn 70k — DB xử lý ra sao?"**

> Trả lời: Bọc trong 1 Transaction. Cục đóng băng 100k trong SoDuDongBang được xẻ:
> - GiaoDichVi #1 (HoanCoc, 70k): cộng lại SoDuKhaDung
> - GiaoDichVi #2 (ThuTienPhat, 30k): ghi nhận doanh thu phạt
> - SoDuDongBang trừ đi 100k
> Nếu bất kỳ bước nào fail → ROLLBACK hết → không mất tiền khách.

---

**Câu 11: "Bảng giá thay đổi theo mùa thế nào? Ví dụ hè đắt hơn đông?"**

> Trả lời: Hiện tại BangGia chỉ phân biệt ngày thường/cuối tuần/lễ. Chưa có giá theo mùa. Nếu muốn thêm, cần bổ sung cột `HieuLucTu`/`HieuLucDen` vào BangGia. Nhưng scope hiện tại chưa làm.

---

**Câu 12: "Nếu mất mạng thì POS có hoạt động được không?"**

> Trả lời: Hiện tại chưa. POS kết nối trực tiếp SQL Server qua TCP. Mất mạng = không bán được. Hướng phát triển: dùng SQLite local cache, bán offline rồi sync lại khi có mạng.

---

### NHÓM C: CÂU HỎI "ĐÁNH ĐỐ" — THẦY HAY HỎI

---

**Câu 13: "2 lễ tân cùng đặt 1 phòng thì sao? Code xử lý ra sao?"**

> Trả lời: Bảng Phong có RowVer. Người đặt trước → RowVer thay đổi. Người đặt sau gửi kèm RowVer cũ → SQL WHERE không match → UPDATE 0 dòng → code trả về DBConcurrencyException → thông báo "Phòng đã được đặt, vui lòng làm mới".

---

**Câu 14: "Khách screenshot QR vé gửi bạn, 10 người cùng quét thì sao?"**

> Trả lời: Khi 1 người quét → UPDATE VeDienTu.TrangThai = 'DaSuDung'. SQL khóa record đó ngay. 9 người sau quét → trạng thái đã là 'DaSuDung' → từ chối. Thực tế thì turnstile đổ chuông cảnh báo.

---

**Câu 15: "Kho còn 1 lon nước, 2 người cùng bấm mua thì sao?"**

> Trả lời: Bảng TonKho có RowVer. Giống logic phòng: người mua trước → TonKho.SoLuong trừ 1, RowVer đổi. Người sau cập nhật → RowVer khác → fail → thông báo "Sản phẩm vừa được bán, kiểm tra lại tồn kho".

---

**Câu 16: "Tại sao DonHang, GiaoDichVi không có IsDeleted?"**

> Trả lời: Vì đây là sổ cái tài chính. Tiền đã vào/ra là phải có dấu vết. Nếu thu nhầm, kế toán phải làm phiếu hoàn tiền (giao dịch đảo ngược). Xóa giao dịch cũ = xóa bằng chứng kế toán, vi phạm nguyên tắc sổ cái.

---

**Câu 17: "HashSignature dùng thuật toán gì? Có bảo mật thật không?"**

> Trả lời: Dùng SHA256. Input = (IdVi + LoaiGiaoDich + SoTien + ThoiGian + SecretKey). SecretKey nằm trong file config, không hardcode. Nó không phải bảo mật tuyệt đối — nếu ai biết SecretKey thì vẫn giả hash được. Nhưng đủ để phát hiện sửa DB bất thường trong phạm vi đề tài.

---

**Câu 18: "Trigger ComboChiTiet check 100% — vậy lúc tạo combo mới thêm sản phẩm đầu tiên 30% thì bị chặn à?"**

> Trả lời: Đúng, đây là 1 lỗ hổng nhóm em đã phát hiện. Trigger không phân biệt combo đang ở trạng thái 'Bản nháp' hay 'Kích hoạt'. Giải pháp tạm: khi code thêm ComboChiTiet, SET NOCOUNT ON hoặc wrapping trong 1 batch thêm hết rồi mới COMMIT. Giải pháp đúng: sửa trigger thêm điều kiện chỉ check khi Combo.TrangThai = 'Kích hoạt'.

---

**Câu 19: "Nếu nhân viên IT vào thẳng SQL sửa số dư ví thì phát hiện bằng cách nào?"**

> Trả lời: Bằng HashSignature trong GiaoDichVi. Mỗi giao dịch hợp lệ đều có hash. Khi audit: tính lại SUM(GiaoDichVi.SoTien) của ví đó → so với SoDuKhaDung. Nếu khác nhau → có ai sửa. Thêm nữa, kiểm tra hash từng giao dịch — nếu hash không khớp → giao dịch bị sửa.

---

**Câu 20: "Công ty đoàn chưa trả tiền (mua chịu), hệ thống xử lý sao? 500 người có vào cổng được không?"**

> Trả lời: DonHang có trạng thái 'GhiNoCongTy'. Khi chọn trạng thái này, 500 vé vẫn được kích hoạt (VeDienTu.TrangThai = 'ChuaSuDung' → quét vào cổng được). Tiền 425 triệu nằm ở Bảng Công Nợ Phải Thu. Cuối tháng công ty chuyển khoản → kế toán lập PhieuThu cấn trừ.

---

## 4.3 MẸO KHI BỊ HỎI KHÔNG BIẾT

> Nếu bị hỏi cái chưa làm, **đừng bịa**. Nói thẳng:
> - "Phần này nhóm em chưa kịp implement do thời gian hạn chế"
> - "Nhóm em đã nhận ra vấn đề này và đã ghi vào hướng phát triển Phase 2"
> - "Schema đã thiết kế sẵn nhưng code chưa cover hết"

> Việc tự nhận ra thiếu sót thể hiện hiểu bài hơn là nói "nó chạy được hết rồi thầy". Giảng viên đánh giá cao sự trung thực.

---

## 4.4 ER DIAGRAM — NHÓM BẢNG CHÍNH (vẽ lên giấy A3 nếu cần chỉ)

```
VaiTro ──N:N── QuyenHan (qua PhanQuyen)
   │
NhanVien ──→ KhuVuc ──→ KhuVucBien / KhuVucThu (weak)
   │
   ├── DonHang ──→ ChiTietDonHang ──→ VeDienTu
   │      │                       ├── DatPhongChiTiet → ChiTietDatPhong → Phong
   │      │                       ├── ThueDoChiTiet → TuDo/ThueTu
   │      │                       ├── DatBan → ChiTietDatBan → BanAn
   │      │                       ├── VeDoXeChiTiet → LuotVaoRaBaiXe
   │      │                       └── DatChoThuAn → DongVat
   │      │
   │      ├── PhieuThu (N phiếu / 1 đơn)
   │      └── KhuyenMai
   │
   ├── KhachHang ──→ DoanKhach
   │      └── ViDienTu ──→ TheRFID
   │             └── GiaoDichVi
   │
   ├── SanPham ──→ BangGia
   │      ├── SanPham_Ve (weak)
   │      ├── QuyDoiDonVi
   │      └── DonViTinh
   │
   ├── Combo ──→ ComboChiTiet
   │
   └── KhoHang ──→ TonKho / TheKho
          ├── PhieuNhapKho → ChiTietNhapKho
          └── PhieuXuatKho → ChiTietXuatKho
```
