# 📘 Hướng Dẫn Sử Dụng Trợ Lý AI (Antigravity Kit)

Tài liệu này hướng dẫn cách khai thác tối đa hệ thống **Antigravity Kit** đã được cài đặt vào thư mục `.agent/` của dự án. Hệ thống sẽ biến AI (trong IDE của bạn) thành tổ hợp Đa Chuyên Gia có tính kỷ luật cao.

---

## 🌟 1. Gọi Lệnh Theo Workflow thần Tốc (Slash Commands)
Thay vì prompt dài dòng, hãy gõ 1 trong các lệnh sau vào kênh chat của AI:

*   `/brainstorm`: Bắt AI phân tích hệ thống, đưa ra các kịch bản cạnh (Edge-Cases) và đặt câu hỏi ngược lại cho bạn TRƯỚC KHI code. Dùng khi muốn nghĩ logic mới.
*   `/plan`: Yêu cầu AI vạch ra từng step (WBS) cực chi tiết trước khi đánh codebase khổng lồ.
*   `/ui-ux-pro-max`: Bật khi bạn bí giao diện. AI sẽ quét thư viện 50+ style thiết kế đẹp nhất để phối màu, typography, và animation.
*   `/debug`: Truy tìm mìn ẩn. AI sẽ khoanh vùng nguyên nhân theo quy trình 4 bước chuẩn khoa học.
*   `/create`: Tự động setup khung sườn dự án/mô-đun theo Best Practice 2026.

---

## 🤖 2. Gọi Đội Đặc Nhiệm Chuyên Gia (Agents)
Bạn không cần phải dạy AI làm mọi thứ. Khi bạn chat, hệ thống "phân giải ngữ nghĩa" (**Intelligent Routing**) sẽ tự động kéo các chuyên gia vào cuộc. Hoặc bạn có thể gọi tên họ bằng cách `@`:
1.  **`@frontend-specialist`** *(Hoặc `@mobile-developer`)*: Thiết kế UI cực sắc, tẩy sạch màu quê mùa (Purple Ban/Template Ban), bắt buộc làm theo chuẩn "Đẹp & Lộng lẫy".
2.  **`@backend-specialist`**: Phụ trách viết C# / API xịn.
3.  **`@database-design`**: Kiểm soát chặt DB (SQL), lo vụ Normal Form, N+1 query, Indexing khét lẹt.
4.  **`@security-auditor`**: Quét lỗ hổng (Pen-Test), ngăn chặn tiêm mã ẩn.
5.  **`@debugger`**: Sửa lỗi bằng "hệ tâm linh" debug log, sửa triệt để chứ không cắm băng keo.

---

## 🛠 3. Dạy AI Theo Luật Của Bạn (Skills)
Bộ quy tắc (Skill) được lưu thành từng file `SKILL.md` nằm riêng ở `.agent/skills/`.
Bạn có thể tự đẻ thêm quy tắc làm việc cho AI (VD: Dạy nó làm theo convention cty bạn) bằng cách:
1.  Tạo folder `.agent/skills/[ten-quy-tac]`
2.  Thêm file `SKILL.md` và viết mô tả:
    *Ví dụ: "Skill này dùng để hướng dẫn dev. Cấm đặt tên biến tiếng Việt. Phải dùng try-catch."*
3.  Từ lần sau, AI gặp code của bạn nó sẽ kích hoạt Skill này tự động mà bạn không cần phải dặn lại!

---

## 📝 4. Tiêu Chuẩn Văn Bản Căn Bản 
AI ở đây đã được nạp sẵn Skill `documentation-templates` và `clean-code`:
1.  **Code Comments**: AI chỉ comment **"Tại sao lại code thế này?"**, tuyệt đối không comment "Code này làm gì?" (giảm ồn code).
2.  **Thiết kế Lỗi**: Mặc định áp dụng phương châm "Nếu cùng lỗi xuất hiện 2 lần -> Báo động, không tự sửa mù quáng".
3.  **Trở thành B.A (Phân tích viên)**: Nó sẽ nhắc nhở bạn update file `PROJECT_STATUS.md` hoặc `ARCHITECTURE.md` sau khi hoàn thành cụm cụm tính năng lớn (Milestone).

---
*Powered by Antigravity Kit & Vudovn's Framework*

---

## 📚 5. Thư Viện "Phép Thuật" Có Sẵn (37 Skills)

Để không bị choáng ngợp bởi con số 37 thủ thuật khác nhau được cấp cho hệ thống, bạn có thể dựa vào nguyên tắc **Triệu hồi theo yêu cầu (Lazy-load)**: Chỉ khi nào bạn miêu tả một tính năng liên quan đến lĩnh vực nào, IDE mới bốc thẻ bài đó ra để nạp vào đầu. Các thẻ bài có sẵn bao gồm:

### 🎨 1. Giao Diện & Thiết Kế (UI/UX)
*   `frontend-design`, `mobile-design`: Tư duy thiết kế chuẩn 2026. Bắt buộc từ bỏ các gradient gắt, border to, và màu sắc diêm dúa (Purple ban).
*   `tailwind-patterns`: Các mẹo viết CSS theo chuẩn Tailwind V4 và container queries. Nhấn mạnh việc không vung vãi utilities tự do mà gom vào design tokens.
*   `web-design-guidelines`: Chuyên cầm kính lúp săm soi "Khoảng cách có bị lệch không?", "Màu có đủ độ tương phản cho người mờ mắt đọc không?"

### ⚙️ 2. Kiến Trúc & Cấu Trúc Dữ Liệu
*   `architecture`: Giám sát việc dựng lên hệ thống to, luôn viết ADR lưu lịch sử quyết định (Dùng REST hay tRPC? Database SQL hay NoSQL?).
*   `database-design`: Thuốc đặc trị cho các Database đang chạy chậm. Tư duy thiết kế BCNF, nhồi index và cắt giảm vòng lặp `N+1` như đã chỉ ra trong Database Đại Nam.
*   `api-patterns`: Bộ cẩm nang thiết kế Endpoint sao cho người dùng gọi API không bị chửi.

### 🐛 3. Dò Mìn & Tối Ưu Code
*   `systematic-debugging`: Giải thuật dò Bug 4 bước của kỹ sư Silicon Valley. Từ việc khoanh vùng log, cắm trace, check edge-case đến triệt tận gốc. Điển hình là không được rò mã lỗi ra client.
*   `code-review-checklist` / `lint-and-validate`: Ông kẹ giám sát code "Clean code". Mã nguồn dơ hoặc viết lởm chởm là bị check ra đổi format liền.
*   `performance-profiling`: Cắt ngọn phần cứng nếu thấy web chạy đuối hoặc hàm chạy vòng lặp bất thường.

### 🛡 4. Bảo Mật & Đánh Chặn (Cybersecurity)
*   `vulnerability-scanner`: Máy quét siêu tinh tường. Bắt bạn phải validate gắt gao ID truyền vào, không để lộ JWT, không Hardcode mật khẩu chuỗi.
*   `red-team-tactics`: Suy nghĩ như một Hacker để cố chủ động chọc vỡ phần mềm của chính mình vừa viết (MITRE ATT&CK rules).

### 🤖 5. Quản Trị Trí Tuệ Nhân Tạo & Quy Trình
*   `brainstorming`: Mở giao diện "Ông thầy Socratic" để hỏi dồn ý tưởng trước khi gõ phím.
*   `plan-writing` / `documentation-templates`: Kỹ năng vẽ WBS và cấu trúc YAML/JSON đỉnh cao khi bạn nản chuyện phải tự làm báo cáo.
*   `intelligent-routing` / `parallel-agents`: Tự giác nhận biết "Ồ đây là Task phức tạp, phải vừa gọi kĩ sư Mobile và kỹ sư Backend ra code cùng 1 lúc cho nhánh này".

### 💻 6. Dành Riêng Cho Ngôn Ngữ
*   Tích hợp sẵn luật vàng của vô số ngôn ngữ như: `nextjs-react-expert` (React 19), `nodejs-best-practices`, `rust-pro`, `python-patterns`, `powershell-windows` hay `bash-linux`. Bất kể bạn đụng vào tầng nào, AI sẽ gọi cuốn tự điển quy tắc ngôn ngữ đó của chính cha đẻ framework.

> 💡 **Khuyên Dùng:** Nếu rảnh có thể vào thư mục `.agent/skills/` đọc lướt qua một file `SKILL.md` để xem thử "À thì ra AI bị dặn dò là khi gặp React thì không được xài Hook kiểu cũ" để thấy bộ não nó tổ chức thế nào!

