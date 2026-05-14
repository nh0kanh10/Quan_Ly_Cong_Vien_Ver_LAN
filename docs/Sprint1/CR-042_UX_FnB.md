# PHIẾU YÊU CẦU THAY ĐỔI (CHANGE REQUEST)
**Mã CR:** CR-042
**Dự án:** Hệ thống Quản lý Khu Du Lịch Đại Nam (DN01)
**Ngày lập:** 25/04/2026
**Người yêu cầu:** Nguyễn Văn A (Trưởng bộ phận Vận hành F&B)
**Trạng thái:** Đã phê duyệt và hoàn thành (Closed)

---

## 1. MÔ TẢ VẤN ĐỀ HIỆN TẠI
Trong tuần vận hành thử nghiệm vừa qua, nhân viên nhập liệu phản ánh tính năng tạo Sản Phẩm (Tab F&B) đang có thiết kế dễ gây nhầm lẫn:
- **Tình trạng 1:** Nhân viên thêm các món pha chế (như Trà Sữa, Cafe) nhưng lại có thói quen tích vào ô `[x] Là vật tư có quản lý tồn kho` và bỏ qua bước nhập định mức nguyên liệu (BOM). Hệ quả là khi bán trên POS, hệ thống không trừ nguyên liệu trong kho mà chỉ ghi nhận doanh thu. Kế toán cuối ngày không thể khớp kho.
- **Tình trạng 2:** Đối với các mặt hàng mua đi bán lại nguyên đai nguyên kiện (Nước suối Aquafina, Xúc xích gói), nhân viên lại quên tích `[x] Là vật tư` khiến hàng không thể nhập kho qua module Quản lý Kho, và phần mềm cứ bắt phải có định mức.

**Kết luận từ khách hàng:** Giao diện bắt người dùng tự nhớ luật "Cái nào cần BOM, cái nào không cần" là quá rủi ro. Cần phần mềm tự động hóa việc này.

## 2. ĐỀ XUẤT THAY ĐỔI (SOLUTION)
Bổ sung cơ chế chống sai sót bằng cách chia nhỏ danh mục "Đồ Uống" và "Đồ Ăn" thành các loại chuyên biệt để hệ thống tự động hóa việc này:

1. **Thêm loại sản phẩm mới:**
   - Thêm `Đồ uống đóng chai` và `Đồ ăn tiện lợi` vào từ điển hệ thống.
2. **Khóa cứng giao diện:**
   - Nếu chọn `Đồ Uống` / `Đồ Ăn` (Pha chế/Chế biến): Tự động **tắt** và **khóa** checkbox "Là vật tư". Cấm chọn.
   - Nếu chọn `Đồ uống đóng chai` / `Đồ ăn tiện lợi`: Tự động **bật** và **khóa** checkbox "Là vật tư". Cấm bỏ chọn.
3. **Chặn lưu (Hard Error):**
   - Nếu là món pha chế mà nhân viên lưu khi chưa có ít nhất 1 dòng định mức nguyên liệu (BOM), hệ thống hiện thông báo lỗi đỏ chặn lưu. Không dùng cảnh báo (Warning/Yes-No) như cũ.

## 3. PHẠM VI ẢNH HƯỞNG (IMPACT ANALYSIS)
- **Database:** Bổ sung 2 record vào bảng `HeThong.TuDien` (Nhóm `SP_LOAI`).
- **Backend:** Cập nhật file `ProductTypeHelper.cs` (logic phân loại vật tư và prefix).
- **UI/UX:** Cập nhật `ucSanPham_Detail.cs` và `UIStrings.resx`.
- **Nghiệp vụ POS:** Giúp POS có thể tách tab "Đồ đóng chai" và "Pha chế" gọn gàng hơn trong tương lai.

## 4. XÁC NHẬN VÀ PHÊ DUYỆT
- **Phân tích viên (BA):** Đã đánh giá (Approve)
- **Tech Lead:** Đã phê duyệt kiến trúc (Approve)
- **Khách hàng (Operations):** Đã đồng ý giải pháp (Approve)

---
*Ghi chú của Dev: Đã implement xong trong phiên bản v1.3 của SRS và mã nguồn.*
