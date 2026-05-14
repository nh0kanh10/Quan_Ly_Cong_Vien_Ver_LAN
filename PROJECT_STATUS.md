# PROJECT_STATUS: DAI NAM NEW POS

## 🎯 Current Milestone: Rental Asset Management (Module Thuê Đồ)
**Status:** ✅ COMPLETED

### Lịch sử các cột mốc đã đạt được:
1. **Quản lý danh mục & Kiến trúc chuẩn 3 lớp:** Đã hoàn thiện toàn bộ luồng Danh mục hàng hóa (Khu vực, Sản phẩm, Tủ/Chòi, Giờ thuê, Combo). (✅ COMPLETED)
2. **Module POS Bán Lẻ:** Hoàn thiện giao diện, luồng quét Barcode/RFID, tích hợp đa phương thức thanh toán, hoàn trả. (✅ COMPLETED)
3. **Quản lý Kho (Inventory Hub):** Cơ chế chia tách Kho (Kho ảo, Kho vật lý), duyệt phiếu Nhập/Xuất, cảnh báo tồn kho, xử lý Split-Insert theo điểm bán. (✅ COMPLETED)
4. **Quản lý Khách Hàng (CRM):** Phát hành thẻ RFID, nạp điểm, trừ ví điện tử. (✅ COMPLETED)
5. **Module Thuê Đồ (Rental Asset Management):**
    - Nghiệp vụ Giao đồ: Phân loại tài sản định danh (Phương tiện di chuyển, Tủ, Chòi) và không định danh (Phao, Khăn). Áp dụng tính năng tự động hiển thị số lượng (InputBox).
    - Nghiệp vụ Nhận trả: 
        - Tính tiền phạt lố giờ Real-time, phân loại phạt mất đồ và lố giờ (độc lập, không cộng dồn quá đáng).
        - Trả đồ theo Batch (Hàng loạt).
        - Ghi nhận Chứng từ Tài chính (ChungTuTC) rạch ròi 2 luồng: Hoàn cọc & Thu Surcharge Invoice (Phạt).
    - Data Binding & Security: 
        - Khóa trả sai khu vực đối với Tủ/Chòi (Cố định).
        - Ràng buộc 100% bằng Transact-SQL để Rollback khi có lỗi.
6. **I18N (Đa ngôn ngữ):** Toàn bộ giao diện và MessageBox đều được đồng bộ qua `LanguageManager` và `UpdateResx.cs` (Hỗ trợ: VN, EN, ZH). Đã vá lỗi hiển thị Raw Key tại `frmCheckOut` và bổ sung các Key giao diện mới.

7. **Kiểm Soát Vé Cổng (Access Control):** Tích hợp Camera quét QR/Barcode, kiểm tra quyền truy cập theo trò chơi/khu vực, xử lý Real-time lượt vé, chuyển giao đúng thư mục vận hành. (✅ COMPLETED)
8. **Tối ưu FnB Đóng Chai & Sửa lỗi Quy đổi POS:** Khóa tab cấu hình không cần thiết cho đồ uống đóng chai. Đồng bộ trực tiếp hệ số quy đổi vào DataRow ngầm của POS. Tự động quy đổi đơn giá sang ĐVT gốc khi lập phiếu xuất kho để khấu trừ tồn kho chính xác 100%. (✅ COMPLETED)
9. **Tối ưu Tab Thuê đồ (Shift Management):** Sửa lỗi khóa cứng Tab thành màu xám không bấm được khi đóng ca hoặc hủy mở ca. Thay thế bằng nút bấm thông minh "Mở Phiên Thu Ngân" giữa màn hình để user dễ tương tác. (✅ COMPLETED)
10. **Tái cấu trúc Giao diện Kiểm Soát Vé Cổng (Access Control UI):** Thiết kế lại layout 2 cột hiện đại, bổ sung thẻ KPI real-time (Khách vào hôm nay, Quét thành công, Bị từ chối). Áp dụng màu sắc mềm mại theo chuẩn Premium UI, tích hợp bộ lọc Khu vực/Trò chơi đáp ứng yêu cầu thực tế. (✅ COMPLETED)

11. **Đồng bộ Đặt phòng Online (Reservation Sync):** 
    - Sửa lỗi không hiển thị đặt phòng từ Web vào danh sách "Chờ nhận" (Inbox) khi bước sang ngày Chủ Nhật. Chuyển đổi bộ lọc từ "Tuần dương lịch" sang **"7 ngày tới"** (Rolling window) giúp nhân viên luôn thấy được các booking sắp đến.
    - Vá lỗi hiển thị tên khách hàng: Tự động lấy thông tin từ `TenNguoiDat` và `SoDienThoai` trên phiếu nếu khách hàng chưa có hồ sơ cũ trong hệ thống (Customer Conversion Fallback).
    - Mở rộng cảnh báo: Badge thông báo (nút chuông) trên sơ đồ phòng giờ đây đếm toàn bộ booking trong 7 ngày tới thay vì chỉ đếm ngày hôm nay, giúp bộ phận Lễ tân chủ động sắp xếp phòng sớm. (✅ COMPLETED)

### 🐛 Vấn đề còn tồn đọng (Backlog/Tech Debt):
- Tính năng in ấn Hóa đơn và Vé mã QR (XtraReport) hiện đang giả lập bằng XtraMessageBox và Form Preview tĩnh. Cần nối với DevExpress Reports ở Phase sau.
- App Navigation: Kiến trúc `MDI / DocumentManager` đã được vá tạm bằng `XtraUserControl + BeginInvoke` để xử lý giật lag. Nếu scale thêm nhiều form phức tạp, cần đánh giá lại Navigation Frame.

### 📝 Ghi chú Kỹ Thuật (Architecture Overview):
- **Kiến trúc:** 3-Tier (GUI -> BUS -> DAL).
- **ORM:** LINQ to SQL (DaiNamDB.dbml).
- **UI Framework:** DevExpress WinForms.
- **Quy tắc quan trọng:** Bất kỳ thao tác tài chính hay kho bãi nào cũng phải sinh ra `ChungTu` tương ứng (ChungTuKho, ChungTuTC) để Kế toán có Audit Trail đối soát. Khách hàng sử dụng `TheRFID` và `ViDienTu` làm ví nạp trả trước.

---
*Cập nhật lần cuối: 03/05/2026*

