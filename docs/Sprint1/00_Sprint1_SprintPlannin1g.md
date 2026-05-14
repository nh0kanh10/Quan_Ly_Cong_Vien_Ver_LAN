ID,Công việc,Giờ,Bắt đầu,Kết thúc,Resource Name,Chi phí (VNĐ),NG,TN,NH,GV,KH,Tiêu chí chất lượng (Acceptance Criteria),Phụ thuộc
S1.0,Khởi động Sprint 1,8,04/03/2026,05/03/2026,,400000,,,,,,,
S1.0.1,Họp Sprint Planning: chốt danh sách công việc và phân công từng người,4,04/03/2026,04/03/2026,Phòng họp,200000,A,R,R,C,C,Biên bản ghi rõ ai làm việc gì và hạn xong từng phần,
S1.0.2,Viết tiêu chí nghiệm thu cho toàn bộ chức năng Sprint 1,4,04/03/2026,05/03/2026,Word,200000,S,I,R,I,C,Mỗi chức năng có ít nhất 3 tiêu chí kiểm tra cụ thể,S1.0.1
S1.1,TASK-00: Màn hình chính frmMain + Đăng nhập + Đa ngôn ngữ,20,05/03/2026,10/03/2026,,1000000,,,,,,,
S1.1.1,Thiết kế giao diện frmMain: thanh menu bên trái và vùng hiển thị nội dung bên phải,6,05/03/2026,07/03/2026,C# DevExpress,300000,I,R,I,I,I,Menu hiển thị đủ các mục theo vai trò. Bấm mục nào thì mở đúng form đó bên phải,S1.0.2
S1.1.2,"Viết code đăng nhập: kiểm tra tài khoản trong cơ sở dữ liệu, khóa 15 phút sau 5 lần nhập sai",5,05/03/2026,07/03/2026,C# BUS,250000,R,I,I,I,I,Đăng nhập đúng thì vào được hệ thống. Sai 5 lần thì hiện thông báo và không cho đăng nhập tiếp trong 15 phút,S1.0.2
S1.1.3,Viết code mở form theo menu: bấm mục nào trên menu thì form đó xuất hiện ở vùng nội dung và form cũ đóng lại,5,07/03/2026,10/03/2026,C# BUS,250000,R,I,I,I,I,Mỗi mục menu mở đúng một form và không bị chồng form lên nhau,S1.1.1
S1.1.4,"Tích hợp đa ngôn ngữ: tạo file tiếng Việt, Anh, Trung và nút đổi ngôn ngữ trên giao diện",4,07/03/2026,10/03/2026,C# Resx,200000,S,I,R,I,I,Bấm đổi ngôn ngữ thì toàn bộ nhãn trên giao diện đổi theo ngay mà không cần tắt mở lại phần mềm,S1.1.1
S1.2,US-01: Phân quyền theo vai trò,12,07/03/2026,12/03/2026,,600000,,,,,,,
S1.2.1,"Tạo bảng Vai trò, Quyền hạn, Phân quyền trong cơ sở dữ liệu và thêm sẵn 3 vai trò mẫu",2,07/03/2026,07/03/2026,SQL Server,100000,R,I,I,I,I,Bảng có đủ cột và ràng buộc khóa ngoại. Seed sẵn 3 vai trò: Quản lý và Thu ngân và Thủ kho,S1.1.2
S1.2.2,"Màn hình danh sách vai trò: xem, thêm, sửa, xóa vai trò",4,07/03/2026,10/03/2026,C# DevExpress,200000,I,R,I,I,I,Thêm vai trò mới lưu vào cơ sở dữ liệu. Xóa vai trò đang có người dùng thì báo lỗi không cho xóa,S1.2.1
S1.2.3,Màn hình gán quyền: chọn một vai trò và tick vào các màn hình mà vai trò đó được vào,3,10/03/2026,12/03/2026,C# DevExpress,150000,S,R,I,I,I,Sau khi gán quyền và lưu thì đăng nhập lại hiển thị đúng menu đã phân quyền,S1.2.2
S1.2.4,Kiểm tra quyền khi đăng nhập: ẩn hoặc vô hiệu hóa mục menu mà vai trò đó không được vào,3,10/03/2026,12/03/2026,C# BUS,150000,R,I,I,I,I,Tài khoản Thu ngân chỉ thấy menu bán hàng và không thấy menu quản lý người dùng,S1.2.3
S1.3,US-02: Quản lý Sản phẩm và Vé cổng và Định mức nguyên liệu BOM,28,05/03/2026,14/03/2026,,1400000,,,,,,,
S1.3.1,"Màn hình danh sách sản phẩm và form thêm sửa xóa: tên, giá, loại, đơn vị tính",6,05/03/2026,07/03/2026,C# BUS,300000,R,I,I,I,I,Thêm sản phẩm mới lưu được. Xóa sản phẩm đang có trên đơn hàng thì bị chặn và báo lỗi,S1.0.2
S1.3.2,"Tab bảng giá: đặt giá khác nhau cho người lớn và trẻ em, ngày thường và ngày lễ",4,07/03/2026,10/03/2026,C# BUS,200000,R,I,I,I,I,Lưu được ít nhất 2 mức giá. Khi tạo đơn hàng hệ thống tự lấy đúng mức giá theo ngày và đối tượng khách,S1.3.1
S1.3.3,Tab vé cổng: cấu hình loại vé theo đối tượng và tự sinh mã QR khi kích hoạt,6,10/03/2026,12/03/2026,C# BUS,300000,R,I,I,I,I,Mỗi vé có mã QR riêng không trùng nhau. Quét mã QR đọc được thông tin vé đúng,S1.3.1
S1.3.4,Tab BOM thực phẩm: chọn nguyên liệu và nhập số lượng tiêu hao khi bán một đơn vị sản phẩm,4,10/03/2026,12/03/2026,C# BUS,200000,R,I,I,I,I,Lưu được ít nhất 3 nguyên liệu cho một sản phẩm. Không cho lưu khi số lượng định mức bằng 0,S1.3.1
S1.3.5,Tìm kiếm sản phẩm theo tên và lọc theo nhóm sản phẩm trên danh sách,4,12/03/2026,14/03/2026,C# BUS,200000,S,R,I,I,I,Gõ tên sản phẩm thì danh sách lọc ngay theo ký tự đang gõ mà không cần bấm nút tìm kiếm,S1.3.1
S1.3.6,Viết tài liệu SRS và bộ test case cho phần sản phẩm,4,05/03/2026,14/03/2026,Word Excel,200000,I,I,R,I,I,SRS mô tả đủ 3 luồng: thêm và sửa và xóa. Test case có ít nhất 1 trường hợp đúng và 1 trường hợp lỗi cho mỗi luồng,S1.0.2
S1.4,US-03: Quản lý Combo,18,10/03/2026,17/03/2026,,900000,,,,,,,
S1.4.1,Viết logic combo: kiểm tra tổng tỷ lệ phân bổ phải bằng đúng 100 phần trăm mới cho lưu,5,10/03/2026,12/03/2026,C# BUS,250000,R,I,I,I,I,Tổng tỷ lệ không bằng 100 thì hiện thông báo và không cho lưu. Bằng 100 thì lưu thành công và chuyển trạng thái sang Hoạt động,S1.3.1
S1.4.2,Màn hình frmCombo: chọn nhiều sản phẩm con và nhập tỷ lệ phân bổ cho từng sản phẩm,6,12/03/2026,14/03/2026,C# DevExpress,300000,I,R,I,I,C,Chọn được ít nhất 2 sản phẩm và nhập tỷ lệ cho từng cái. Nút lưu chỉ sáng lên khi tổng đúng 100,S1.4.1
S1.4.3,Biểu đồ cột hiển thị tỷ lệ phân bổ từng sản phẩm trong combo và cập nhật ngay khi nhập tỷ lệ,4,14/03/2026,14/03/2026,DevExpress Chart,200000,S,R,I,I,I,Biểu đồ hiển thị đúng màu và con số tỷ lệ của từng sản phẩm. Thay đổi tỷ lệ thì biểu đồ cập nhật ngay,S1.4.2
S1.4.4,Viết tài liệu SRS và bộ test case cho phần combo,3,10/03/2026,14/03/2026,Word Excel,150000,I,I,R,I,I,Test case có trường hợp tổng tỷ lệ đúng 100 và trường hợp tổng không bằng 100,S1.0.2
S1.5,Kiểm thử và tổng kết Sprint 1,18,14/03/2026,17/03/2026,,900000,,,,,,,
S1.5.1,Chạy thử toàn bộ test case Sprint 1 theo danh sách đã viết,4,14/03/2026,14/03/2026,App chạy thật,200000,I,S,R,I,I,Không dưới 90 phần trăm test case đạt yêu cầu,S1.1.4 và S1.2.4 và S1.3.6 và S1.4.4
S1.5.2,Ghi lại toàn bộ lỗi tìm thấy vào file Defect List theo mẫu,2,14/03/2026,14/03/2026,Excel,100000,I,S,R,I,I,Mỗi lỗi ghi rõ: mô tả và mức độ và người tìm và ngày tìm,S1.5.1
S1.5.3,Sửa lỗi Sprint 1: ưu tiên sửa lỗi làm treo phần mềm hoặc tính sai số tiền trước,6,14/03/2026,17/03/2026,C# IDE,300000,R,R,S,I,I,Không còn lỗi làm treo phần mềm hoặc tính sai số tiền trước khi demo,S1.5.2
S1.5.4,Cập nhật hướng dẫn sử dụng và tổng hợp tài liệu SRS Sprint 1 thành một file hoàn chỉnh,2,17/03/2026,17/03/2026,Word,100000,I,S,R,C,I,File tài liệu SRS đầy đủ các màn hình Sprint 1 và có hình chụp giao diện thực tế,S1.5.3
S1.5.5,Cài đặt phần mềm lên máy demo và kiểm tra toàn bộ chức năng lần cuối,1,17/03/2026,17/03/2026,SQL Server,50000,R,S,I,I,I,Phần mềm khởi động được và chạy qua các chức năng Sprint 1 mà không báo lỗi,S1.5.3
S1.5.6,Demo Sprint 1 cho giảng viên: đăng nhập và phân quyền và thêm sản phẩm và tạo combo,2,17/03/2026,17/03/2026,App,100000,A,R,R,A,A,Demo đủ 4 tính năng chính mà không bị treo hoặc crash trong quá trình trình bày,S1.5.5
S1.5.7,Họp rút kinh nghiệm: ghi lại điều làm tốt và điều cần cải thiện ở sprint sau,1,17/03/2026,17/03/2026,Phòng họp,50000,A,R,R,C,I,Ghi lại ít nhất 2 điểm cần cải thiện vào biên bản họp,S1.5.6
TỔNG SPRINT 1,,104,04/03/2026,17/03/2026,,5200000,,,,,,,
