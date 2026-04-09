# HƯỚNG DẪN KIẾN TRÚC VÀ PHÁT TRIỂN AI CHATBOX (DAINAM AI)

Tài liệu này là "Whitepaper" chi tiết về cách Đại Nam AI được thiết kế, những kỹ thuật cốt lõi phía sau, những bài học xương máu (pitfalls) khi làm việc với Gemini API, và hướng dẫn chi tiết cách để một Developer mới có thể đấu nối và bắt bệnh hệ thống.

---

## 1. TỔNG QUAN KIẾN TRÚC HỆ THỐNG (ARCHITECTURE OVERVIEW)

Hệ thống AI không chỉ đơn thuần là gửi text và nhận text. Nó được xây dựng dựa trên kiến trúc **Agent ReAct (Reasoning and Acting)** kết hợp với **Function Calling** của Google Gemini 2.5 Flash.

### Các thành phần chính (Key Files):
1. **`AIModels.cs`**: Nền móng dữ liệu. Chứa cấu trúc JSON chính xác theo đặc tả của Gemini REST API. Bao gồm các class như `GeminiRequest`, `GeminiContent` (với role `user` / `model`), `GeminiFunctionCall`, và `GeminiFunctionResponse`.
2. **`AIChatService.cs`**: "Bộ não trung tâm". Đảm nhiệm việc giao tiếp HTTP với Google, duy trì `_history` (ngữ cảnh trò chuyện), và quản lý vòng lặp ReAct (ReAct Loop) một cách tự động.
3. **`AIToolRegistry.cs`**: "Kho vũ khí". Nơi định nghĩa các khả năng của AI (đọc CSDL, lọc lưới, thao tác). Nó ánh xạ các chức năng C# sang dạng JSON Schema để báo với Gemini là "tớ biết làm những việc này".
4. **`IAIFormContext.cs`**: "Dây cắm điện". Interface dùng để gắn vào các Form. Khi Form nào được kích hoạt hiển thị lên, hệ thống sẽ đọc interface này để biết đổi "Não" cho phù hợp.
5. **`AIChatPanel.cs`**: Giao diện UI WinForms. Quản lý việc vẽ bong bóng chat, hiệu ứng typing, và khóa an toàn chống spam-click.

---

## 2. NHỮNG KHÓ KHĂN & BÀI HỌC "XƯƠNG MÁU" (CHALLENGES & PITFALLS)

Trong quá trình thiết kế và ổn định AI, team phát triển đã gặp và giải quyết những lỗi đặc thù rất cứng nhắc của LLMs. Đây là những lý do tại sao code hiện tại trông như vậy:

### A. Lỗi 400 - Đứt gãy chuỗi Function Calling (History Structure Validation)
*   **Vấn đề:** Gemini 2.5 cực kỳ khắt khe về định dạng lịch sử trò chuyện. Nếu trong lịch sử có một `functionResponse`, nó **BẮT BUỘC** phải nằm ngay phía sau một tin nhắn `functionCall` của role `model`. Mọi sự chen ngang, sai role (dùng `function` thay vì `"user"` tuỳ SDK), hoặc xoá history giữa chừng đều dẫn đến lỗi HTTP 400 lập tức.
*   **Cách giải quyết:** Chúng ta phải đảm bảo cấu trúc mảng `parts` của `user` phải chứa object `functionResponse` bao gồm `name` và `response`.

### B. Lỗi "Mất Trí Nhớ Giữa Chừng" (Mid-Loop History Swap)
*   **Vấn đề:** Khi AI ra lệnh mở một form (`open_form`), hệ thống bọc lệnh này trong ReAct Loop. Khổ nỗi, form được mở ra ngay lập tức kích hoạt hàm `SwitchContext()`. Hàm này lại có lệnh `_history.Clear()` nhằm xóa ngữ cảnh để chuyển sang form mới. Điều này làm cho vòng lặp hiện tại của AI bị mất lịch sử ngay giữa chừng, mất luôn `functionCall` trước đó -> Chết API.
*   **Cách giải quyết (Deferred Execution):** Cờ `pendingFormOpen` ra đời. Lệnh mở form không thực thi ngay lập tức trong vòng lặp. Hệ thống ghi chú lại lệnh này, tiếp tục chèn `functionResponse` giả để thỏa mãn Gemini, ép Gemini kết thúc vòng lặp rồi ***sau khi hoàn thành chu trình*** mới gọi Invoke để mở Form. Cực kỳ an toàn!

### C. Quản trị Race Condition tại UI
*   **Vấn đề:** Khi API gọi chậm, User bấm gửi tin nhắn liên tục -> Gửi đi 4-5 request cùng lúc. Các luồng này tranh giành cùng can thiệp vào `_history` chung (List không thread-safe) -> Vỡ cấu trúc mảng.
*   **Cách giải quyết:** Đồng bộ hóa. Biến `_isProcessing` không được phép set tự do, nó được Lock trực tiếp ngay từ khoảnh khắc nút `btnSend` được click, không chờ đến vòng lặp Async của Task. Nút tắt/bật đồng bộ hóa lập tức.

### D. "Tự Kỷ" & Giới hạn Data (Data Overflow)
*   **Vấn đề:** Trả về toàn bộ danh sách 5,000 khách hàng trong CSDL cho AI đọc -> Vượt quá số lượng Token cho phép, treo app.
*   **Cách giải quyết:** Phải thiết kế Tool chặn Limit trước. Luôn dùng `Take(AIConfig.MaxDataRows)` (thường là mốc 20-50 dòng). AI chỉ đọc Top 20 và tóm tắt lại cho User.

---

## 3. LUỒNG HOẠT ĐỘNG CỦA VÒNG LẶP REACT (REACT LOOP FLOW)

Hiểu vòng lặp này là hiểu toàn bộ AI. Mọi request không gọi Google 1 lần, mà gọi N lần thông qua vòng For nội bộ `AIConfig.MaxReActLoops`:

1.  **User hỏi:** *"Tổng chi tiêu của Hoàng Nam là bao nhiêu?"* -> Ghi vào History.
2.  **Lặp (Lần 1):** Gọi API Goolge. Google trả về: *"Tôi không biết, nhưng tôi muốn dùng hàm `get_customers` với `args: {"keyword": "Nam"}`"* (Đây là `functionCall`).
3.  **Hệ Thống (C#):** Thấy `functionCall`, C# chặn lại không gửi text cho User. Nó tự động gọi xuống `BUS_KhachHang` với lệnh tìm chữ "Nam".
4.  **Hệ Thống (C#):** Nhận được KQ (list Khách Hàng). C# gói kq này thành `functionResponse`, tống vào _history dưới tư cách là User thông báo cho AI.
5.  **Lặp (Lần 2):** Quá trình For tự động chạy lại, gửi History (đã chứa kêt quả DB) lên Google.
6.  **Google lặp (Lần 2):** AI phân tích data thô và nhận ra: "À, Hoàng Nam tiêu 2.500.000đ". AI trả về Text tinh sạch.
7.  **Hệ Thống (C#):** Thấy Text thuần, gõ búa **THOÁT LOOP**, hiển thị câu chat lên Panel cho User.

---

## 4. HƯỚNG DẪN TÍCH HỢP AI VÀO FORM MỚI

Làm sao để dạy AI cách làm việc trên một màn hình hoàn toàn xa lạ? Có 3 chặng cụ thể.

### CHẶNG 1: Gắn "Dây Cắm Điện" vào Form (`Context Awareness`)

Bắt buộc phải báo cho `Form1` (Vỏ bọc hệ thống) biết Form này là gì bằng cách khai báo `IAIFormContext`.

```csharp
// Mở Code-Behind của form cần làm (VD: frmHoSoCaNhan)
public partial class frmHoSoCaNhan : Form, IBaseForm, IAIFormContext
{
    // Xác định ID nội bộ (DUY NHẤT). Định danh này liên kết chặt với AIToolRegistry.
    public string AIContextName => "frmHoSoCaNhan"; 
    
    // Đừng đùa với Description. Đây chính là "System Prompt" thu nhỏ.
    // Dặn rõ AI sẽ thấy gì và được làm gì ở Form này.
    public string AIContextDescription => "Màn hình Hồ sơ các nhân viên. Người dùng có tìm thông tin nhân viên, xin nghỉ phép, truy xuất lịch làm việc.";
}
```

### CHẶNG 2: Rèn khí tài cho Form (`AIToolRegistry.cs`)

Form đã được nhận diện, nhưng AI chưa có bất cứ kỹ năng cụ thể nào. Vào `AIToolRegistry.cs` tạo hàm `Register...Tools()`:

```csharp
private void RegisterNhanVienTools()
{
    Register("frmHoSoCaNhan", new ToolDefinition
    {
        // ------------- BƯỚC A: ĐỊNH NGHĨA CHO AI ĐỌC -------------
        // AI dựa vào 'description' để quyết định có dùng Tool này không.
        Declaration = new GeminiFunctionDeclaration
        {
            name = "kiem_tra_cham_cong",
            description = "Lọc thống kê chấm công/số ngày nghỉ của nhân viên trong tháng.",
            parameters = new GeminiParameterSchema
            {
                properties = new Dictionary<string, GeminiPropertyDef>
                {
                    // Liệt kê chi tiết những tham số bạn cần AI bóc tách từ câu hỏi của User.
                    ["ma_nv"] = new GeminiPropertyDef { type = "STRING", description = "Mã Nhân Viên (VD: NV001)" },
                    ["thang"] = new GeminiPropertyDef { type = "INTEGER", description = "Tháng cần check (1-12)" }
                },
                // Đảm bảo AI phải bắt đủ thì mới chịu chạy
                required = new List<string> { "ma_nv", "thang" } 
            }
        },

        // ------------- BƯỚC B: VIẾT CODE C# THỰC CHIẾN -------------
        Execute = (args) =>
        {
            try
            {
                // Bóc chuỗi an toàn thông qua Helper GetArg
                string maNV = GetArg(args, "ma_nv");
                int thang = GetIntArg(args, "thang", DateTime.Now.Month);

                // Code chọc vào BAL/DAL quen thuộc
                var data = BUS_ChamCong.LietKeChamCong(maNV, thang);
                
                // MẸO (QUAN TRỌNG): Format KQ mạch lạc thành gạch đầu dòng, 
                // AI sẽ tự nhai nốt khúc này và dịch thành văn phong mượt mà.
                return $"THÔNG TIN NV {maNV} THÁNG {thang}:\n" +
                       $"- Ngày đi làm: {data.SoNgayLam}\n" +
                       $"- Ngày vắng: {data.SoNgayVang}\n" +
                       $"- Trễ: {data.LanTre}";
            }
            catch (Exception ex) 
            { 
                // Có lỗi Database thì AI vẫn thông báo tĩnh tâm lại không sụp cmn rứa App.
                return $"Database lỗi cmnr: {ex.Message}"; 
            }
        }
    });
}
```
**Nhớ gọi đăng ký (`RegisterNhanVienTools()`) trong constructor của `AIToolRegistry()`.**

### CHẶNG 3: Action Dispatching - Não Điều Khiển (ĐẪ HOÀN THIỆN)

Đây là tầng cao nhất (Action). AI không những đọc dữ liệu mà còn ra lệnh trực tiếp để Form tự click, tự cuộn, tự bung modal.
Cách thức thực hiện:
1. Đăng ký Tool ở `AIToolRegistry`, đặt tiền tố `ui_` (VD: `ui_open_recharge_modal`).
2. Tool này không cần gọi database, chỉ cần return đúng chuỗi format chuẩn: `__ACTION__:ui_open_recharge_modal:sdt=090...`
3. Trong Form, bạn triển khai interface `IAICommandHandler` với hàm `ExecuteAICommand`. Tham số `commandName` và `args` (chứa Dictionary JSON bóc ra từ AI) sẽ được chuyển xuống đây tự động.
4. Dùng lệnh `BeginInvoke` trong C# để đảm bảo Form quét lưới, Focus chuột vào dòng khách hàng, và gọi hàm mở Modal. Toàn bộ là Async nên không lo sập Cross-thread.

**Proactive Multi-Tool (AI Chủ động)**: Mặc dù bạn có thể nhắc nhở AI trong `AIContextDescription` tự động gọi nhiều tool 1 lượt (Parallel Function Calling), nhưng Google API thường có giới hạn Rate Limit (sẽ bị lỗi 429 Too Many Requests nếu cố gọi 2 tool cùng một lúc quá nhanh).

**Tuyệt chiêu tối thượng: Implicit Triggering (Auto-Trigger Ngầm bằng C#)**
Thay vì bắt AI tự chọt UI, ta ép C# tự làm việc đó!
Trong các tool get data (VD `get_customers` hoặc `get_customer_transactions`), nếu C# thấy thoả mãn điều kiện (VD: Tìm ra đúng 1 người), ta chỉ cần chêm tiền tố `__ACTION__:tên_lệnh` lên ĐẦU chuỗi `return` của C#.
```csharp
if (totalCustomers == 1) {
    prefix = $"__ACTION__:ui_select_customer:sdt={top[0].DienThoai}\n";
}
return prefix + "TÌM THẤY 1 KHÁCH HÀNG: ...";
```
Bộ phân giải `AIChatService` sẽ tự động tách chuỗi trên, bóc lệnh `ui_select_customer` ra Invoke ngay lập tức lên Form, đồng thời phần chữ phiá sau `\n` vẫn được gửi vào History cho AI phân tích bình thường.
-> **Lợi ích**: Tốc độ phản hồi UI ngay lập tức (0s), giảm thiểu hoàn toàn 1 request Call dư thừa lên Gemini, và không bao giờ gặp lỗi vướng mắc của LLM!
