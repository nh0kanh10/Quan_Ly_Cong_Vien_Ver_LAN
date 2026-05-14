# PROMPT HƯỚNG DẪN AI VIẾT BÁO CÁO ĐỒ ÁN TỐT NGHIỆP CHI TIẾT

**Bạn hãy copy toàn bộ nội dung bên dưới (từ chữ "ĐÓNG VAI" trở đi) và dán vào một AI khác (như ChatGPT, Claude) để nó viết nội dung chi tiết cho bạn.**

---

**ĐÓNG VAI VÀ NHIỆM VỤ:**
Bạn là một kỹ sư phần mềm xuất sắc và là một chuyên gia viết tài liệu kỹ thuật. Nhiệm vụ của bạn là hỗ trợ sinh viên hoàn thiện cuốn **Báo cáo đồ án tốt nghiệp "Phân tích, thiết kế và xây dựng Hệ thống Quản lý Công viên Vui chơi Giải trí Đại Nam"**.
Dưới đây là phần nội dung nhóm sinh viên đã tự viết rất chi tiết (bao gồm Lời cảm ơn, Tóm tắt, Lời mở đầu và Chương 1). Nhiệm vụ của bạn là:
1. Đọc kỹ để thấu hiểu bối cảnh, quy trình nghiệp vụ và các bài toán cốt lõi.
2. Dựa vào đó, viết tiếp **Chương 2, Chương 3 và Chương 4** thật chi tiết, có chiều sâu về mặt kỹ thuật (phân tích thiết kế, cơ sở dữ liệu, kiến trúc 3 lớp, thuật toán).
3. Đảm bảo văn phong thống nhất với phần sinh viên đã viết: Học thuật, chuyên nghiệp, súc tích và mang tính thực tiễn cao.

==================================================
**PHẦN DỮ LIỆU ĐÃ CÓ (SINH VIÊN ĐÃ VIẾT - BẠN CHỈ CẦN THAM KHẢO VÀ GIỮ NGUYÊN):**

**LỜI CẢM ƠN**
Trong suốt quá trình thực hiện đồ án và hoàn thành báo cáo, nhóm xin gửi lời cảm ơn chân thành đến ThS. Lê Thọ – người đã tận tình hướng dẫn, góp ý và hỗ trợ nhóm trong suốt thời gian thực hiện đề tài, giúp nhóm có định hướng rõ ràng để hoàn thành đúng tiến độ.
Mặc dù nhóm đã cố gắng hoàn thiện đề tài ở mức tốt nhất, song do thời gian và kiến thức còn hạn chế nên không tránh khỏi những thiếu sót. Nhóm rất mong nhận được sự góp ý của thầy để tiếp tục cải thiện.
Nhóm xin chân thành cảm ơn.

**TÓM TẮT**
Đồ án "Phân tích, thiết kế và xây dựng Hệ thống Quản lý Công viên Vui chơi Giải trí Đại Nam" hướng đến việc xây dựng một ứng dụng phần mềm quản lý tập trung cho mô hình dịch vụ phức hợp quy mô lớn. Hệ thống bao gồm năm phân hệ chính: kiểm soát vé tại cổng, bán hàng tại quầy (POS), quản lý nhà hàng, quản lý khách sạn và quản lý kho – tất cả được kết nối và đồng bộ dữ liệu với nhau.
Ứng dụng được xây dựng dưới dạng desktop sử dụng C# (WinForms) và SQL Server, thiết kế theo mô hình 3 lớp (Presentation – Business – Data Access) nhằm đảm bảo tính phân tách và dễ bảo trì. Cơ sở dữ liệu được tổ chức theo hướng chuẩn hóa để hạn chế dư thừa và đảm bảo tính nhất quán.
Kết quả đạt được là một ứng dụng thử nghiệm có khả năng mô phỏng luồng vận hành tổng thể của hệ thống. Các giao dịch phát sinh tại quầy bán hàng và nhà hàng được ghi nhận đồng thời cập nhật vào hệ thống quản lý kho; thông tin sử dụng dịch vụ của khách có thể liên kết với dữ liệu lưu trú trong phân hệ khách sạn. Sự liên thông dữ liệu giữa các phân hệ giúp hệ thống hỗ trợ theo dõi và quản lý tập trung, giảm thiểu sai sót so với phương pháp thủ công.
Hệ thống hiện tại ở mức mô phỏng, chưa tích hợp với thiết bị phần cứng thực tế hoặc cổng thanh toán bên ngoài. Tuy nhiên, kết quả đồ án đã đáp ứng được các mục tiêu đề ra và thể hiện được cách tiếp cận thiết kế hệ thống theo hướng tổng thể, có khả năng mở rộng trong tương lai.

**LỜI MỞ ĐẦU**
**1. Lý do chọn đề tài**
Trong quá trình học tập các môn chuyên ngành Công nghệ thông tin, sinh viên thường tiếp cận các bài toán quen thuộc như quản lý thư viện hay quản lý sinh viên. Tuy nhiên, thực tế có nhiều mô hình doanh nghiệp với cấu trúc phức hợp hơn, đòi hỏi hệ thống phải xử lý đồng thời nhiều loại nghiệp vụ và có sự liên kết chặt chẽ giữa các bộ phận.
Khu du lịch Đại Nam là một ví dụ tiêu biểu cho mô hình này. Với quy mô lớn và đa dạng dịch vụ – bao gồm khu vui chơi, biển nhân tạo, khu thú, nhà hàng, khách sạn và dịch vụ cho thuê – việc quản lý nếu thực hiện thủ công hoặc bằng nhiều phần mềm rời rạc sẽ dễ dẫn đến dữ liệu phân tán, khó kiểm soát, đặc biệt trong những thời điểm đông khách.
Từ thực tế đó, nhóm lựa chọn đề tài "Phân tích, thiết kế và xây dựng phần mềm quản lý khu du lịch phức hợp (mô phỏng quy trình KDL Đại Nam)" nhằm áp dụng các kiến thức đã học vào một bài toán có tính thực tiễn cao.

**2. Mục tiêu đề tài**
Mục tiêu của đề tài là xây dựng một ứng dụng phần mềm hỗ trợ quản lý các nghiệp vụ chính trong khu du lịch theo hướng tập trung và đồng bộ. Cụ thể, hệ thống hướng tới:
• Số hóa quy trình bán hàng, quản lý nhà hàng, khách sạn, kiểm soát vé và cho thuê tài sản, giúp giảm bớt thao tác thủ công.
• Xây dựng giao diện bán hàng (POS) cho phép xử lý nhiều loại dịch vụ trên cùng một màn hình.
• Quản lý trạng thái phòng khách sạn và bàn nhà hàng, phản ánh đúng tình trạng sử dụng dịch vụ trong thực tế.
• Đồng bộ dữ liệu kho với các giao dịch phát sinh, đảm bảo tính nhất quán và hạn chế sai lệch.
• Mô phỏng được một phần quy trình hoạt động thực tế của khu du lịch.

**3. Phạm vi và giới hạn đề tài**
Do đây là đồ án môn học với thời gian có hạn, đề tài được giới hạn trong phạm vi xây dựng ứng dụng desktop sử dụng WinForms, phục vụ quản lý nội bộ. Các phân hệ dành cho khách hàng như Web hay Mobile chưa được triển khai.
Về phần cứng, hệ thống không kết nối trực tiếp với cổng xoay, đầu đọc RFID hay máy in vé. Các thao tác liên quan được mô phỏng bằng cách nhập mã ID và sử dụng điện thoại giả lập thiết bị quét.
Ngoài ra, chức năng thanh toán thực tế qua ngân hàng hoặc ví điện tử chưa được tích hợp. Việc thanh toán chỉ được mô phỏng bằng dữ liệu nội bộ, nhằm tập trung vào xử lý nghiệp vụ và quản lý dữ liệu trong phạm vi đề tài.

**CHƯƠNG 1: KHẢO SÁT HIỆN TRẠNG VÀ PHÂN TÍCH YÊU CẦU**
**1.1. Khảo sát hiện trạng và phát biểu bài toán**
Khu du lịch Đại Nam vận hành nhiều loại hình dịch vụ có mối liên kết chặt chẽ và dữ liệu phát sinh liên tục: bán vé, khu vui chơi, nhà hàng, khách sạn và cho thuê tài sản. Qua quá trình khảo sát, nhóm xác định sáu bài toán cốt lõi mà hệ thống cần giải quyết:
• Quản lý thông tin tập trung (Single Source of Truth): Một cá nhân có thể đóng nhiều vai trò (nhân viên, khách hàng). Hệ thống cần lưu trữ dữ liệu thống nhất, tránh trùng lặp và đảm bảo toàn vẹn.
• Định giá linh hoạt (Dynamic Pricing): Giá vé và dịch vụ thay đổi theo đối tượng (trẻ em/người lớn) và thời điểm (ngày thường, lễ). Hệ thống cần hỗ trợ cấu hình giá thay vì cố định trong mã nguồn.
• Khuyến mãi tự động (Rule-based Promotion): Cho phép thiết lập điều kiện áp dụng (loại khách, giá trị hóa đơn, thời gian) để hệ thống tự động áp dụng giảm giá khi thanh toán.
• Thanh toán không tiền mặt – mô phỏng RFID (Cashless): Hỗ trợ nạp/trừ tiền qua thẻ vòng tay (mô phỏng), giúp khách không cần sử dụng tiền mặt trong khu vui chơi.
• Đồng bộ bán hàng và kho (Auto-BOM): Khi phát sinh giao dịch, hệ thống tự động trừ nguyên liệu theo định mức (BOM – Bill of Materials), đảm bảo tồn kho chính xác.
• Quản lý theo trạng thái (State-based Processing): Các đối tượng như phòng khách sạn, đơn thuê tài sản phải tuân theo luồng trạng thái nhất định (ví dụ: Trống → Đang sử dụng → Đang dọn), tránh sai lệch quy trình.

**1.2. Khảo sát quy trình nghiệp vụ tại KDL Đại Nam**
Dựa trên các bài toán đã phân tích, nhóm tiến hành mô hình hóa lại các quy trình nghiệp vụ theo hướng có thể triển khai trên phần mềm.
**1.2.1. Quy trình bán vé và kiểm soát**
Khách hàng mua vé tại quầy dưới dạng vé lẻ hoặc vé combo. Sau khi thanh toán, hệ thống sinh vé điện tử dưới dạng mã vạch (Barcode/QR).
Tại cổng kiểm soát, nhân viên sử dụng thiết bị quét mã để xác thực quyền truy cập. Mỗi lần quét được ghi nhận theo thời gian thực phục vụ kiểm soát lượt vào/ra và thống kê lưu lượng khách.
**1.2.2. Quy trình cho thuê tài sản**
Nhân viên tạo giao dịch trên hệ thống, chọn loại sản phẩm hoặc tài sản thuê, ghi nhận thời điểm bắt đầu và tiền đặt cọc. Đối với tài sản có định danh (xe điện, tủ đồ), hệ thống kiểm tra và cập nhật trạng thái từng tài sản; các sản phẩm thuê thông thường chỉ cần quản lý theo số lượng.
Khi kết thúc, hệ thống tính thời gian thực tế, quy đổi chi phí và cấn trừ với tiền đặt cọc. Nếu phát sinh chênh lệch, hệ thống xử lý hoàn tiền hoặc thu thêm phí, sau đó cập nhật trạng thái tài sản về "trống" để sẵn sàng cho giao dịch tiếp theo.
**1.2.3. Quy trình F&B và quản lý kho**
Tại khu nhà hàng, hệ thống hoạt động như một điểm bán (POS) riêng, hỗ trợ gọi món, tạo hóa đơn và thanh toán tại chỗ. Mỗi điểm bán được cấu hình menu và danh mục sản phẩm riêng phù hợp với từng khu vực.
Hệ thống hỗ trợ đặt bàn trước cho khách lẻ và khách đoàn. Đối với khách đoàn, hệ thống quản lý danh sách thành viên, suất ăn và các quyền lợi đi kèm. Sau khi thanh toán, hệ thống tự động trừ nguyên liệu trong kho theo định mức cấu thành (BOM) của từng món ăn, đảm bảo đồng bộ giữa bán hàng và kho.
**1.2.4. Quy trình khách sạn và lưu trú**
Hệ thống quản lý toàn bộ vòng đời lưu trú của khách, bao gồm đặt phòng, nhận phòng (check-in) và trả phòng (check-out). Trạng thái phòng được kiểm soát xuyên suốt (trống, đã đặt, đang sử dụng, đang dọn) nhằm tránh trùng phòng và hỗ trợ lễ tân điều phối hợp lý.
Đối với khách đoàn, hệ thống hỗ trợ đặt nhiều phòng trong cùng một giao dịch, quản lý danh sách thành viên theo đoàn và ghi nhận thanh toán tập trung qua trưởng đoàn. Các dịch vụ phát sinh trong thời gian lưu trú có thể được tổng hợp vào cùng hóa đơn hoặc tách riêng theo cấu hình. Khi trả phòng, hệ thống thực hiện tổng hợp chi phí, đối soát thanh toán và cập nhật trạng thái phòng.
**1.2.5. Quy trình bán combo và dịch vụ tổng hợp**
Hệ thống cho phép tạo các gói dịch vụ kết hợp (combo) như vé + ăn uống, vé + trò chơi hoặc vé + dịch vụ khác. Khi phát sinh giao dịch, hệ thống quản lý quyền lợi đi kèm, theo dõi số lượt sử dụng và phân bổ doanh thu về từng bộ phận theo cấu hình.
**1.2.6. Quản lý danh mục và cấu hình hệ thống**
Hệ thống cung cấp chức năng quản lý dữ liệu nền (Master Data) bao gồm sản phẩm, dịch vụ, đơn vị tính và quy đổi, bảng giá theo thời gian và khu vực hoạt động. Các dữ liệu này đóng vai trò nền tảng cho toàn bộ hệ thống, đảm bảo tính nhất quán trong quá trình vận hành.

==================================================

**YÊU CẦU VIẾT TIẾP CÁC CHƯƠNG SAU (ĐÂY LÀ NHIỆM VỤ CHÍNH CỦA BẠN):**

Dựa trên các bài toán và quy trình nghiệp vụ đã có ở Chương 1, hãy viết thật chi tiết và có chiều sâu kỹ thuật cho các chương sau:

**CHƯƠNG 2: PHÂN TÍCH VÀ THIẾT KẾ HỆ THỐNG**
2.1. Phân tích yêu cầu hệ thống:
- Chi tiết hóa các chức năng tương ứng với nghiệp vụ ở trên (POS, Quản lý Kho, Khách sạn, Nhà hàng, Quét vé tự động với Camera).
- Yêu cầu phi chức năng (Kiến trúc 3 lớp, Bảo mật, Hiệu năng khi truy xuất dữ liệu lớn, Đa ngôn ngữ i18n).
2.2. Biểu đồ Use Case: Mô tả các Use Case chính cho các Actor: Nhân viên bán hàng, Thủ kho, Lễ tân khách sạn, Quản lý.
2.3. Thiết kế Cơ sở dữ liệu:
- Mô tả cấu trúc các cụm bảng chính thay vì vẽ sơ đồ (Cụm Bán Hàng/Đơn Hàng, Cụm Kho/Sổ Cái, Cụm Lưu Trú/Phòng, Cụm Luồng Trạng Thái).
- Phân tích kỹ **bảng Sổ cái kho (SoCai)** phục vụ phương pháp kế toán kép (Kho thực - Kho ảo).

**CHƯƠNG 3: TRIỂN KHAI VÀ XÂY DỰNG ỨNG DỤNG**
3.1. Thiết kế Kiến trúc hệ thống: Giải thích sâu về Kiến trúc 3 lớp (Presentation - Business Logic - Data Access). Nêu lợi ích của việc tách lớp.
3.2. Cấu trúc mã nguồn: Sự phân chia thư mục C# (GUI, BUS, DAL, ET).
3.3. Giải quyết các bài toán kỹ thuật phức tạp (Vận dụng linh hoạt các thông tin tôi cung cấp sau đây để mô tả cách team chúng tôi code):
- Bài toán Quản lý tồn kho kép (Dual-entry Inventory): Mọi giao dịch xuất nhập đều phải có kho đối ứng (Kho thực -> Kho ảo như KHO_KHACH, KHO_NCC).
- Chống đặt trùng phòng Khách sạn: Sử dụng SQL Trigger (`trg_CheckOverlappingBooking`) mức Database để khóa tranh chấp khi hai khách đặt cùng 1 phòng.
- Luồng trạng thái động (State Machine): Sử dụng bảng `LuongTrangThai` để kiểm soát trạng thái động (ví dụ phòng từ Đang dọn -> Trống) thay vì code if/else cứng.
- Thuật toán gộp Bill (Satellite Billing): Dùng bảng `ChiTietDonHang` làm trung tâm, gộp tiền phòng, vé, đồ thuê, ăn uống nhà hàng vào một Master Bill duy nhất.
- Tích hợp kiểm soát vé qua Camera (Ticket Scanning): Dùng thư viện đọc QR Code, xử lý chống quay vòng vé (Anti-passback) khi vào/ra cổng.

**CHƯƠNG 4: TỔNG KẾT VÀ ĐÁNH GIÁ**
4.1. Kết quả đạt được (So sánh với các mục tiêu đã đề ra ở phần Mở đầu).
4.2. Hạn chế của ứng dụng (Ví dụ: Chưa tích hợp in ấn hóa đơn qua máy in nhiệt, kiến trúc MDI navigation còn tốn tài nguyên máy trạm nếu thao tác nhanh).
4.3. Hướng phát triển tương lai (Mở rộng thêm phân hệ quản lý Sở thú, xây dựng Mobile App cho khách hàng tự đặt vé/nhà hàng).

**HƯỚNG DẪN TRẢ LỜI CHO AI:** 
Hãy viết **Chương 2** trước. Giọng văn phải học thuật, nghiêm túc, tương đồng tuyệt đối với cách viết của sinh viên ở Chương 1. Sau khi viết xong Chương 2, hãy dừng lại và yêu cầu tôi gõ "Tiếp tục" để viết Chương 3, tránh bị ngắt quãng do quá dài. Trình bày bằng Markdown.
