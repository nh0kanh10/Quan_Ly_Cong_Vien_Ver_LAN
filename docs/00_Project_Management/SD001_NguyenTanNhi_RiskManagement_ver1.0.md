# SD001: RỦI RO DỰ ÁN THỰC TẾ & CÁCH PHÒNG NGỪA (DỄ HIỂU)
**Dự án**: Phần mềm Quản lý Công viên (Bản LAN)
**Quy mô**: Team 3 thành viên (M1, M2, M3)
**Ngày cập nhật**: 2026-03-18

---

## I. TỔNG QUAN
Bảng này tập trung vào các tình huống "đời thường" nhất mà team 3 người hay gặp phải khi làm phần mềm chạy mạng nội bộ, tránh dùng thuật ngữ kỹ thuật quá khó hiểu.

---

## II. DANH MỤC 50 RỦI RO SÁT THỰC TẾ

| STT | Tên rủi ro (Dễ hiểu) | Chuyện gì sẽ xảy ra? | Cách phòng ngừa (Dễ làm) |
| :--- | :--- | :--- | :--- |
| **01** | **Mất kết nối mạng LAN** | Máy trạm không thấy máy chủ, nhân viên không thể bán vé. | Kiểm tra dây mạng, Switch thường xuyên. App nên có thông báo "Mất kết nối tới máy chủ". |
| **02** | **"Nút thắt" Backend** | Người làm Code chính (M1) chưa xong DB thì người làm Giao diện (M2) phải ngồi chơi. | M1 liệt kê sớm các cột dữ liệu cần thiết để M2 vẽ giao diện và gắn dữ liệu giả trước. |
| **03** | **Quên Backup dữ liệu** | Máy chủ hỏng hay bị virus là mất sạch dữ liệu khách hàng, hóa đơn. | Cuối ngày copy file Database ra một ổ cứng rời hoặc USB khác. |
| **04** | **Thành viên chủ chốt rời đi** | 1 trong 3 người nghỉ là mất luôn kiến thức phần đó, không ai biết sửa tiếp. | **Tuyệt đối không để ai nắm giữ "bí mật"**. Mọi người phải đẩy code lên Git mỗi ngày và viết ghi chú dễ hiểu. |
| **05** | **Nhập liệu nhầm lẫn** | Nhân viên gõ nhầm giá vé 100k thành 10k hoặc nhập ngày sinh vào ô tên. | Thiết kế các ô nhập liệu chỉ cho nhập số, hoặc chọn ngày từ lịch (Calendar). |
| **06** | **Sai lệch doanh thu** | Báo cáo doanh thu trên app khác với số tiền thực tế trong két. | Thiết kế logic cộng trừ tiền cực kỳ cẩn thận, test thử nhiều lần với các vé giảm giá, combo. |
| **07** | **Mất điện đột ngột** | Máy chủ đang lưu dữ liệu thì sập nguồn, dễ gây hỏng file database. | Khuyên khách hàng mua bộ tích điện (UPS) cho máy chủ. |
| **08** | **Cài vào máy khách không chạy** | Ở nhà mình chạy tốt, nhưng sang máy chủ khách thiếu phần mềm hỗ trợ. | Ghi lại danh sách các phần mềm cần cài thêm (như .NET Framework, SQL Server). |
| **09** | **Nhân viên táy máy** | Nhân viên tự ý vào sửa giá vé hoặc xóa hóa đơn cũ để lấy tiền chênh lệch. | Chia quyền: Nhân viên chỉ bán vé, quản lý mới được sửa/xóa. |
| **10** | **Quá hạn quy định (Deadline)** | Làm giao diện quá cầu kỳ dẫn đến không kịp làm chức năng tính tiền chính. | Ưu tiên làm chức năng "Sống còn" (Bán vé, Lưu DB) trước khi làm đẹp. |
| **11** | **Mất cấu hình kết nối** | Đổi máy chủ mới là app không biết tìm database ở đâu để chạy. | Để địa chỉ máy chủ trong một file cài đặt dễ sửa (file text), không ghi chết trong code. |
| **12** | **Vé bán quá số lượng** | Khu vui chơi chỉ chứa được 50 người nhưng app lỡ bán cho 60 người do 2 máy cùng bấm. | App phải kiểm tra lại số chỗ trống 1 lần nữa ngay khi bấm nút "Thanh toán". |
| **13** | **Máy chạy chậm (Lag)** | Dùng lâu app bị lag, chiếm hết bộ nhớ máy tính. | Tắt bớt các cửa sổ không dùng, thỉnh thoảng khởi động lại app. |
| **14** | **Lỗi in hóa đơn** | Bấm in mà máy in không chạy hoặc in ra chữ bị lỗi font, mất nét. | Test thử với máy in thật tại công viên trước khi bàn giao. |
| **15** | **Khách đòi thêm tính năng** | Đang làm dở thì khách đòi thêm chức năng quản lý kho, rất mất thời gian. | Thỏa thuận rõ: Chỉ làm đủ các chức năng đã chốt trong tài liệu SRS. |
| **16** | **Dùng sai phiên bản Windows** | App làm cho Win 10 nhưng máy khách vẫn dùng Win 7 cổ lỗ sĩ. | Phải hỏi rõ khách hàng dùng máy gì trước khi bắt đầu code giao diện. |
| **17** | **Lệch giờ hệ thống** | Giờ máy bán vé và máy chủ khác nhau, dẫn đến báo cáo bán vé bị sai ngày. | Luôn lấy ngày giờ trực tiếp từ Máy Chủ để lưu vào hóa đơn. |
| **18** | **Tài liệu hướng dẫn khó hiểu** | Giao app xong nhân viên không biết dùng, gọi điện hỏi team suốt ngày. | Làm clip hướng dẫn ngắn gọn hoặc tài liệu có nhiều hình ảnh minh họa. |
| **19** | **Mật khẩu quá dễ đoán** | Nhân viên đặt mật khẩu là "123456", khách hàng đứng gần nhìn thấy và đăng nhập. | Yêu cầu nhân viên đặt mật khẩu khó hơn (có cả chữ và số). |
| **25** | **Đặt tên khó hiểu** | Sau 1 tháng mở code cũ ra, không ai hiểu biến `a1`, `b2` là làm cái gì. | Đặt tên biến bằng tiếng Việt không dấu hoặc tiếng Anh đơn giản (ví dụ: `txtGiaVe`, `btnThanhToan`). |
| **26** | **Thứ tự nút Tab bị loạn** | Nhân viên nhấn Tab để nhảy ô nhưng con trỏ cứ nhảy lung tung không theo thứ tự. | Chỉnh lại thuộc tính `TabIndex` trong WinForms theo đúng luồng nhập liệu từ trên xuống dưới. |
| **27** | **Màn hình quá bé** | Thiết kế trên máy 24 inch cực đẹp nhưng bê sang máy khách 14 inch thì bị mất nút. | Dùng tính năng `Anchor` và `Dock` để giao diện tự co giãn theo kích thước màn hình. |
| **28** | **Lỗi dấu tiếng Việt** | Tìm kiếm khách hàng "Nguyễn" nhưng gõ không ra vì lỗi bảng mã Unicode. | Luôn để kiểu dữ liệu `NVARCHAR` trong Database và xử lý chuỗi chuẩn Unicode trong C#. |
| **29** | **Bấm nút Lưu nhiều lần** | Nhân viên bấm "Lưu" liên tục do mạng chậm, dẫn đến lưu trùng 5-6 hóa đơn. | Khi bấm nút Lưu xong phải vô hiệu hóa (`Enabled = false`) nút đó ngay lập tức. |
| **30** | **Quên copy Icon/Ảnh** | App chạy nhưng không hiện icon, màn hình trắng xóa do quên copy thư mục ảnh. | Để ảnh vào thư mục `Resources` của dự án hoặc tạo thư mục `Images` đi kèm bộ cài. |
| **31** | **Xóa nhầm dữ liệu đang dùng** | Xóa "Loại vé" khi vẫn còn vé đang bán thuộc loại đó, gây lỗi báo cáo. | Kiểm tra xem dữ liệu có đang bị khóa (Foreign Key) không trước khi cho phép xóa. |
| **32** | **Lỗi Copy-Paste** | Nhân viên copy văn bản từ Word có chứa ký tự lạ vào app gây lỗi hiển thị. | Viết code lọc bỏ các ký tự đặc biệt khi dán vào các ô nhập liệu. |
| **33** | **Tài liệu hướng dẫn cũ** | App đã cập nhật nút bấm mới nhưng hướng dẫn vẫn là hình ảnh của bản cũ. | Mỗi lần đổi giao diện chính là phải chụp lại ảnh cho User Guide ngay. |
| **34** | **Đặt mật khẩu DB dễ quá** | Để mật khẩu SQL là "123" hay "admin", rất dễ bị lộ nếu có người ngoài vào mạng LAN. | Đặt mật khẩu có độ khó vừa phải và không viết nó lên tờ giấy dán cạnh máy tính. |
| **35** | **Màu sắc gây mỏi mắt** | Dùng màu đỏ, xanh neon quá chóe khiến nhân viên nhìn 8 tiếng/ngày bị đau mắt. | Dùng các tông màu dịu (Pastel) hoặc màu chuẩn của Windows (Segoe UI, Gray/White). |
| **36** | **Lỗi phiên bản SQL Server** | Máy mình dùng SQL 2022 nhưng khách dùng SQL 2012, không đính kèm (Attach) được DB. | Tạo file Script `.sql` để cài đặt Database thay vì copy file `.mdf` trực tiếp. |
| **37** | **Thành viên "người làm người chơi"** | 1 người làm quá nhiều, 2 người còn lại không nắm được logic để hỗ trợ. | Chia nhỏ Task trong Sprint, ai cũng phải có sản phẩm riêng để báo cáo cuối tuần. |
| **38** | **Mất file cấu hình kết nối** | File chứa IP máy chủ bị xóa mất, app không khởi động nổi. | Luôn có một file dự phòng hoặc cho phép người dùng tự nhập lại IP nếu mất file. |
| **39** | **Tìm kiếm không ra kết quả** | Gõ "Vé trẻ em" nhưng app không tìm ra vì thừa một dấu cách ở cuối. | Dùng hàm `.Trim()` để loại bỏ khoảng trắng dư thừa trước khi tìm kiếm. |
| **40** | **App sập không báo lỗi** | App tự tắt (Crash) mà không hiện thông báo gì, nhân viên không biết bị làm sao. | Dùng `Try-Catch` bọc các chức năng quan trọng và hiện thông báo lỗi dễ hiểu cho người dùng. |
| **41** | **Báo cáo in ra quá dài** | Hóa đơn bán vé in ra dài cả mét do định dạng sai khổ giấy. | Thiết kế lại mẫu in (Report) cho đúng kích thước giấy in nhiệt (80mm). |
| **42** | **Quên tắt kết nối DB** | Mở kết nối SQL mà quên không đóng, dùng một lúc là Server báo "Quá nhiều kết nối". | Luôn dùng lệnh `using` hoặc `connection.Close()` sau khi truy vấn xong dữ liệu. |
| **43** | **Lỗi ngày tháng (MM/DD)** | Máy khách để kiểu ngày Mỹ (Tháng/Ngày) nhưng app tính theo kiểu Việt (Ngày/Tháng). | Luôn dùng định dạng chuẩn ISO (YYYY-MM-DD) khi giao tiếp giữa App và Database. |
| **44** | **Lấy nhầm code cũ** | Hai người cùng sửa 1 file, người sau đè lên người trước mất hết code mới. | Dùng Git và luôn "Pull" (tải về) code mới nhất trước khi bắt đầu sửa. |
| **45** | **App chiếm quá nhiều RAM** | Load hàng nghìn ảnh khu vui chơi lên cùng lúc làm máy đứng hình. | Chỉ tải những gì cần hiện lên màn hình (Lười tải - Lazy loading). |
| **46** | **Nút bấm quá nhỏ** | Nhân viên POS dùng màn hình cảm ứng khó bấm trúng nút "Thanh toán". | Làm các nút chức năng chính to, rõ ràng và có khoảng cách hợp lý. |
| **47** | **In hóa đơn khi chưa lưu** | In được vé cho khách nhưng trong DB lại chưa lưu, dẫn đến mất tiền. | Chỉ cho phép in khi việc lưu dữ liệu vào Database đã thành công 100%. |
| **48** | **Lỗi font chữ khi xuất Excel** | Xuất báo cáo ra Excel bị lỗi font tiếng Việt (ô vuông, ký tự lạ). | Sử dụng thư viện xuất Excel hỗ trợ tốt Unicode (như EPPlus hoặc ClosedXML). |
| **49** | **Nhân viên quên đăng xuất** | Người này làm xong không thoát, người sau vào dùng tiếp gây sai lệch doanh thu cá nhân. | Có chức năng tự động đăng xuất sau 15-30 phút nếu không có thao tác gì. |
| **50** | **Demo bị lỗi mạng** | Lúc trình bày đồ án thì mạng LAN ở phòng máy bị lỗi, app không chạy được. | Luôn chuẩn bị sẵn một bản "Local" chạy ngay trên máy tính xách tay của mình để dự phòng. |

---

## III. CHIẾN LƯỢC SỐNG CÒN KHI LÀM TEAM 3 NGƯỜI
1.  **Chia sẻ kiến thức**: Mỗi tuần 1 lần "show code" cho nhau xem để ai cũng hiểu sơ bộ phần của người kia.
2.  **Đơn giản là nhất**: Đừng làm những gì quá cao siêu khi nhân lực ít, tập trung vào sự ổn định.
3.  **Lưu trữ tập trung**: Mọi tài liệu, code, ảnh đều để trên mây (Google Drive/Git) để lỡ 1 người "biến mất" thì dự án vẫn chạy tiếp được.
4.  **Kiểm tra chéo**: Sau khi xong 1 chức năng, hãy mời 2 bạn còn lại dùng thử để tìm ra những lỗi "vô lý" mà mình không tự thấy được.


---

## III. CHIẾN LƯỢC SỐNG CÒN KHI LÀM TEAM 3 NGƯỜI
1.  **Chia sẻ kiến thức**: Mỗi tuần 1 lần "show code" cho nhau xem để ai cũng hiểu sơ bộ phần của người kia.
2.  **Đơn giản là nhất**: Đừng làm những gì quá cao siêu khi nhân lực ít, tập trung vào sự ổn định.
3.  **Lưu trữ tập trung**: Mọi tài liệu, code, ảnh đều để trên mây (Google Drive/Git) để lỡ 1 người "biến mất" thì dự án vẫn chạy tiếp được.
