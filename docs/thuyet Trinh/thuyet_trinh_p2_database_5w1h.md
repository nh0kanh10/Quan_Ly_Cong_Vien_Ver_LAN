# THUYẾT TRÌNH — PHẦN 2: PHÂN TÍCH DATABASE (5W1H)
## Thời gian: ~5 phút (3:30 — 8:30)

---

> Mỗi bảng em sẽ trả lời: **What** (bảng gì), **Why** (tại sao cần), **Who** (ai dùng), **When** (khi nào dùng), **Where** (ở module nào), **How** (hoạt động ra sao).

---

## NHÓM 1: DANH MỤC CƠ BẢN (11 bảng)

### Bảng `NhanVien`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Lưu thông tin nhân viên: tên, SĐT, email, chức vụ, vai trò |
| **Why** | Cần biết ai làm gì, ai tạo đơn hàng nào (audit trail qua cột `CreatedBy`) |
| **Who** | Admin quản lý, mọi module đều reference |
| **When** | Khi tuyển nhân viên mới, hoặc khi tra cứu "đơn này ai tạo?" |
| **Where** | Module Staff |
| **How** | Có `IsDeleted` — nghỉ việc thì đánh dấu, không xóa hẳn vì hàng ngàn đơn hàng cũ vẫn cần biết ai tạo |

### Bảng `VaiTro` + `QuyenHan` + `PhanQuyen`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | 3 bảng phân quyền: VaiTro (Admin, QuanLy, NhanVien, ThuKho, KeToan), QuyenHan (36 mã quyền), PhanQuyen (bảng trung gian N-N) |
| **Why** | Để phân quyền linh động — thêm/bớt quyền không cần sửa code |
| **Who** | Admin setup, code C# kiểm tra trước khi hiển thị menu |
| **When** | Mỗi lần đăng nhập, hệ thống load quyền theo vai trò |
| **How** | Quan hệ N-N: 1 vai trò có nhiều quyền, 1 quyền thuộc nhiều vai trò. VD: Admin có tất cả 36 quyền, NhanVien chỉ có 12 quyền |

### Bảng `KhachHang`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Khách hàng: cá nhân, đoàn, VIP, sinh viên, nội bộ |
| **Why** | Định danh khách, tích điểm, gắn ví RFID, xuất hóa đơn |
| **Who** | Lễ tân tạo, POS tra cứu |
| **When** | Khách đăng ký thẻ RFID hoặc mua vé lần đầu |
| **How** | Có cột `LoaiKhach` phân loại, `IdDoan` nếu thuộc đoàn, `DiemTichLuy` cho loyalty |

### Bảng `DoanKhach` + `DoanKhach_DichVu`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Đoàn khách (công ty, trường học). `DoanKhach_DichVu` lưu các dịch vụ đoàn đã đặt |
| **Why** | Đoàn 500 người cần chiết khấu riêng, xuất 1 hóa đơn VAT chung |
| **Who** | Sales tạo booking, POS quét |
| **When** | Khi công ty đặt tour trước |
| **How** | `ChietKhau` (%) áp vào đơn hàng. `DoanKhach_DichVu` track từng dịch vụ đã dùng bao nhiêu/còn bao nhiêu (`SoLuongDaDung`) |

### Bảng `KhuVuc` + `KhuVucBien` + `KhuVucThu`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | KhuVuc là bảng cha (11 khu vực chính). KhuVucBien/KhuVucThu là **weak entity** mở rộng thuộc tính riêng |
| **Why** | Biển cần thêm: độ sâu, yêu cầu phao. Vườn thú cần: diện tích, sức chứa động vật. Mà không muốn nhồi vào bảng KhuVuc gốc |
| **How** | PK của KhuVucBien chính là FK trỏ về KhuVuc. Tức là KhuVucBien không tự đứng 1 mình được — nó phụ thuộc KhuVuc |

### Bảng `DonViTinh` + `SanPham` + `QuyDoiDonVi`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | DonViTinh (Lon, Chai, Thùng, Vé, Suất), SanPham (hàng hóa + dịch vụ), QuyDoiDonVi (1 Thùng = 24 Lon) |
| **Why** | Thực tế: bán nước suối lúc bán lẻ 1 lon, lúc bán sỉ 1 thùng. Nếu tạo 2 sản phẩm riêng thì kho loạn |
| **Who** | Quản lý setup, POS + Kho dùng |
| **How** | SanPham gắn `IdDonViCoBan` (đơn vị nhỏ nhất — Lon). QuyDoiDonVi khai báo tỷ lệ. Bán 1 thùng → kho trừ 24 lon. Có `GiaBanRieng` cho đơn vị lớn (mua sỉ rẻ hơn lẻ) |

### Bảng `SanPham_Ve` (Weak Entity)
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Thuộc tính riêng cho sản phẩm loại "Vé": CanTaoToken, SoLuotQuyDoi, IdThietBi |
| **Why** | Vé trò chơi cần biết gắn với thiết bị nào (Tàu lượn). Vé combo cần biết quy đổi bao nhiêu lượt |
| **How** | PK = FK → SanPham. Chỉ insert khi SanPham.LoaiSanPham = 'Ve' |

---

## NHÓM 2: BẢNG GIÁ (2 bảng)

### Bảng `BangGia` (Ma trận Giá Phẳng)
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Lưu giá bán thực tế theo 3 loại ngày: ngày thường, cuối tuần, ngày lễ |
| **Why** | Phòng KS giá cuối tuần đắt hơn ngày thường. Vé trò chơi ngày lễ tăng giá. Cần 1 chỗ lưu tất cả |
| **Who** | Quản lý thiết lập, POS tra cứu khi tính tiền |
| **When** | Mỗi khi bán hàng, code tra BangGia theo IdSanPham + ngày hiện tại |
| **How** | 3 cột giá: `GiaNgayThuong`, `GiaCuoiTuan`, `GiaNgayLe`. Có thêm cấu hình thuê theo giờ: `PhutBlock`, `PhutTiep`, `GiaPhuThu`, `TienCoc` |

> Logic tra giá trong code C#:
> 1. Lấy ngày hôm nay → check bảng `CauHinhNgayLe` → có → dùng `GiaNgayLe`
> 2. Không phải ngày lễ → check thứ mấy → T7/CN → `GiaCuoiTuan`
> 3. Còn lại → `GiaNgayThuong`

### Bảng `CauHinhNgayLe`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Danh sách ngày lễ trong năm (1/1, 30/4, 2/9, Tết Nguyên Đán...) |
| **Why** | Để hệ thống biết hôm nay có phải ngày lễ không → áp giá lễ |
| **How** | PK là `Ngay DATE`. Code check: `SELECT 1 FROM CauHinhNgayLe WHERE Ngay = @today` |

---

## NHÓM 3: ĐƠN HÀNG & TÀI CHÍNH (10 bảng)

### Bảng `DonHang`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Bảng trung tâm — mọi giao dịch mua bán đều đổ về đây |
| **Why** | Để gom tất cả (vé, cơm, phòng, phao) vào 1 bill duy nhất → tính tiền 1 lần |
| **Who** | POS tạo, kế toán xem, quản lý audit |
| **When** | Mỗi khi khách mua bất cứ gì |
| **How** | Có `IdKhachHang` (NULL nếu khách vãng lai), `IdDoan` (nếu đoàn), `IdKhuyenMai` (nếu có giảm giá). Trạng thái: ChoThanhToan → DaThanhToan / GhiNoCongTy / DaHuy |

> **Lưu ý**: DonHang **KHÔNG có IsDeleted**. Tiền đã vào sổ là phải giữ. Muốn hủy thì chuyển trạng thái 'DaHuy', không xóa record.

### Bảng `ChiTietDonHang`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Từng dòng mua: sản phẩm nào, số lượng bao nhiêu, giá gốc, giá sau giảm |
| **Why** | 1 đơn hàng có nhiều sản phẩm → cần bảng con chi tiết |
| **How** | Có cột `ThanhTien AS (SoLuong * DonGiaThucTe) PERSISTED` — SQL tự tính, không cần code làm. Từ đây "mọc cành" ra các bảng con: VeDienTu, DatPhongChiTiet, ThueDoChiTiet, DatBan, VeDoXeChiTiet |

### Bảng `VeDienTu`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Vé QR: mỗi vé 1 mã GUID duy nhất |
| **Why** | Mua 50 vé → ChiTietDonHang chỉ 1 dòng SL=50. Nhưng cần 50 mã QR riêng để 50 người quét riêng |
| **Who** | POS sinh vé, cổng kiểm soát quét |
| **How** | PK là `UNIQUEIDENTIFIER` (GUID). Trạng thái: ChuaSuDung → DaSuDung. `SoLuotConLai` cho vé nhiều lượt (combo trò chơi) |

### Bảng `ViDienTu` + `TheRFID` + `GiaoDichVi`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | ViDienTu = ví tiền gắn với khách. TheRFID = vòng tay vật lý quẹt máy. GiaoDichVi = sổ cái ghi mọi giao dịch tiền |
| **Why** | Khách đi bơi cầm tiền mặt dễ ướt/mất. Quẹt vòng tay tiện hơn |
| **Who** | Lễ tân nạp tiền, POS trừ tiền, quản lý audit |
| **How** | ViDienTu có 2 số dư: `SoDuKhaDung` (tiêu được) và `SoDuDongBang` (tiền cọc bị giữ). Có `RowVer` chống 2 người trừ tiền cùng lúc. TheRFID có trạng thái: Active, Locked, Lost |

> GiaoDichVi có 8 loại: NapTien, ThanhToanDichVu, ThuCoc, HoanCoc, ThuTienPhat, HoanTien, DieuChinhTang, DieuChinhGiam. Mỗi lần tiền thay đổi đều ghi 1 dòng vào đây.

### Bảng `PhieuThu` + `PhieuChi`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | PhieuThu = tiền vào (khách trả). PhieuChi = tiền ra (trả nhà cung cấp, chi phí) |
| **Why** | Kế toán cần biết tiền vào bao nhiêu, bằng gì (mặt, chuyển khoản, RFID, MoMo) |
| **How** | PhieuThu có `PhuongThuc` (TienMat, ChuyenKhoan, ViRFID, MoMo, QR...). 1 đơn hàng có thể có NHIỀU phiếu thu (thanh toán chia — trả 1 phần ví, phần còn lại tiền mặt) |

### Bảng `KhuyenMai` + `SuKien`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Khuyến mãi (giảm %, giảm số tiền, đồng giá, mua X tặng Y). SuKien = chương trình bao trùm |
| **Why** | Cần tracking ROI: giảm giá bao nhiêu, thu lại bao nhiêu khách |
| **How** | KhuyenMai gắn `IdSuKien`, có `NgayBatDau/NgayKetThuc`, `DonToiThieu` (mua tối thiểu bao nhiêu mới được giảm) |

---

## NHÓM 4: KHÁCH SẠN (4 bảng)

### Bảng `LoaiPhong` + `Phong`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | LoaiPhong (Superior, Deluxe, Family, Villa) + Phong (phòng vật lý: 101, 102...) |
| **Why** | Tách "loại" và "phòng cụ thể" ra. LoaiPhong gắn với SanPham để dùng chung hệ thống giá BangGia |
| **How** | LoaiPhong.IdSanPham → SanPham → BangGia. Phong có `RowVer` chống 2 lễ tân đặt cùng phòng |

> Seed sẵn 18 phòng: 8 Superior, 5 Deluxe, 3 Family, 2 Villa.

### Bảng `DatPhongChiTiet` + `ChiTietDatPhong`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | DatPhongChiTiet = 1 lần đặt (ngày nhận, ngày trả). ChiTietDatPhong = từng phòng trong lần đặt đó |
| **Why** | 1 booking có thể đặt nhiều phòng. Mỗi phòng giá riêng (phòng VIP khác giá Standard) |
| **How** | DatPhongChiTiet gắn vào ChiTietDonHang qua `IdChiTietDonHang`. Trạng thái: DaDat → DaNhan → DaTra |

---

## NHÓM 5: NHÀ HÀNG + GỬI XE + CHO THUÊ (8 bảng)

### Bảng `NhaHang` + `BanAn` + `DatBan` + `ChiTietDatBan`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | 4 nhà hàng (seed sẵn), 50 bàn ăn (30 bàn NH Đại Nam + 20 bàn Phố ẩm thực). DatBan = booking bàn |
| **Why** | Khách đặt trước bàn → nhà hàng giữ bàn → khách đến → trạng thái chuyển 'DangSuDung' |
| **How** | BanAn có `RowVer` chống tranh chấp. Trạng thái: Trong → DaDat → DangSuDung → Trong |

### Bảng `LuotVaoRaBaiXe` + `GiaGuiXe` + `VeDoXeChiTiet`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Track xe vào/ra, giá gửi theo loại xe, chi tiết vé đỗ gắn vào đơn hàng |
| **Why** | Khi xe vào, ghi biển số + thời gian. Xe ra, tính tiền tự động |
| **How** | LuotVaoRaBaiXe ghi `BienSo`, `ThoiGianVao`. Lúc ra: DATEDIFF tính giờ × GiaGuiXe → tạo VeDoXeChiTiet → gộp vào DonHang |

### Bảng `ThueDoChiTiet` + `TuDo` + `ThueTu`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Thuê phao, tủ đồ, xe điện. TuDo = 50 tủ vật lý (S, M, L). ThueTu = gán khách vào tủ cụ thể + mã PIN |
| **Why** | Thuê đồ khác mua hàng ở chỗ: có cọc, có trả lại, có phạt nếu hư |
| **How** | ThueDoChiTiet có `SoTienCoc`, `TrangThaiCoc` (ChuaHoan/DaHoan/DaPhat), và 3 FK giao dịch ví (IdGiaoDichCoc, IdGiaoDichHoanCoc, IdGiaoDichPhat) |

---

## NHÓM 6: KHO HÀNG (7 bảng)

### Bảng `KhoHang` + `NhaCungCap`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | KhoHang có 3 loại: TrungTam, Kiosk, NhaHang. NhaCungCap = nơi mua hàng |
| **Why** | Mỗi loại kho phục vụ mục đích khác nhau. Cần biết mua hàng từ ai |

### Bảng `PhieuNhapKho` + `ChiTietNhapKho`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Phiếu nhập: từ nhà cung cấp nào, vào kho nào, ngày nào, tổng bao nhiêu tiền |
| **Why** | Giá nhập thay đổi liên tục (lúc bia đắt lúc rẻ). Phải lưu giá vốn tại thời điểm nhập |
| **How** | ChiTietNhapKho có `DonGiaNhap` riêng (khác `SanPham.DonGia` là giá bán), có `TyLeQuyDoi` tính theo đơn vị nhập |

### Bảng `PhieuXuatKho` + `ChiTietXuatKho`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Xuất kho: bán POS, chuyển kho, hủy hàng |
| **How** | Có `IdKhoXuat` và `IdKhoNhan` — nếu cả 2 đều có thì là chuyển kho (kho trung tâm → kiosk) |

### Bảng `TonKho` + `TheKho`
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | TonKho = số lượng hiện tại (snapshot). TheKho = lịch sử mọi thay đổi (ledger) |
| **Why** | TonKho cho biết con số cuối cùng. TheKho cho biết TẠI SAO nó thay đổi (ai xuất, khi nào, lý do gì) |
| **How** | TheKho có 5 loại: NHAP_KHO, XUAT_POS, XUAT_HUY, CHUYEN_KHO, KIEM_KE. Mỗi dòng ghi `TonCuoi` sau giao dịch |

---

## NHÓM 7: CÁC BẢNG PHỤ TRỢ (~21 bảng)

### Combo (`Combo` + `ComboChiTiet`)
| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Đóng gói nhiều sản phẩm thành 1 combo bán chung |
| **Why** | Combo 1 triệu gồm Vé+Ăn+Biển. Cuối tháng cần chia doanh thu cho từng bộ phận |
| **How** | ComboChiTiet có `TyLePhanBo` (%). Trigger kiểm tra tổng = 100%. VD: Vé 20% + Ăn 30% + Biển 50% = 100% |

### Trường đua (7 bảng)
> DuongDua, LoaiHinhDua, GiaiDua, LichThiDau, VanDongVien, NguaDua, PhuongTienDua, KetQuaDua, KhanDai, ViTriNgoi — quản lý lịch đua, kết quả, ghế ngồi khán đài.

### Biển nhân tạo (5 bảng)
> ThietBiTaoSong, LichTaoSong, ChatLuongNuoc, CaTrucCuuHo, ChoiNghiMat — quản lý sóng, chất lượng nước, cứu hộ, chòi nghỉ.

### Vườn thú (4 bảng)
> DongVat, ChuongTrai, LichChoAn, DatChoThuAn — tránh cho thú ăn quá nhiều, track ai chịu trách nhiệm.

### Bảo trì & An toàn (4 bảng)
> DanhSachThietBi, LichBaoTri — track tất cả thiết bị toàn khu, lên lịch bảo trì. SuCo, ThatLac — ghi nhận sự cố, đồ thất lạc.

### Đánh giá & Nhân sự (3 bảng)
> DanhGiaDichVu (1-5 sao), LichLamViec (ca làm), LichKiemKho + ChiTietKiemKho (kiểm kê hàng).

### Audit (1 bảng)
> AuditDonHang — tự động ghi log mỗi khi trạng thái đơn hàng thay đổi (qua Trigger).

### Loyalty (2 bảng)
> LichSuDiem (tích điểm/dùng điểm), QuyTacDiem (cứ mua 100k được 1 điểm cho khách thường, 3 điểm cho khách VIP).
