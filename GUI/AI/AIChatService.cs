using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using GUI.Infrastructure;

namespace GUI.AI
{
    // Bộ não AI chính. Thực hiện vòng lặp ReAct (Suy luận + Hành động) với Gemini API.
    // Quản lý lịch sử hội thoại, đổi ngữ cảnh, và thực thi tool.
    public class AIChatService
    {
        private static readonly HttpClient _http = new HttpClient();
        private readonly JavaScriptSerializer _json = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };

        private List<GeminiContent> _history = new List<GeminiContent>();
        private string _systemPrompt = "";
        private string _currentContext = "navigation";
        private List<GeminiToolDef> _currentTools;

        // Sự kiện để giao diện và Shell lắng nghe
        public event Action<string, Dictionary<string, object>> OnOpenModuleRequested;
        public event Action<string, Dictionary<string, object>> OnUICommandRequested;
        public event Action<bool> OnProcessingChanged;
            // Lấy danh sách module theo ngôn ngữ hiện tại
        private static Dictionary<string, string> LayModuleMap()
        {
            return new Dictionary<string, string>
            {
                { "DASHBOARD", LanguageManager.GetString("AI_MOD_DASHBOARD") ?? "Bảng điều khiển tổng quan" },
                { "POS_BAN_LE", LanguageManager.GetString("AI_MOD_POS") ?? "Màn hình POS bán lẻ" },
                { "SAN_PHAM", LanguageManager.GetString("AI_MOD_SANPHAM") ?? "Quản lý sản phẩm, dịch vụ, vé" },
                { "COMBO", LanguageManager.GetString("AI_MOD_COMBO") ?? "Quản lý combo (gói sản phẩm)" },
                { "DANH_MUC_KHO", LanguageManager.GetString("AI_MOD_KHO") ?? "Danh mục kho hàng" },
                { "TRUNG_TAM_KHO", LanguageManager.GetString("AI_MOD_TTKHO") ?? "Trung tâm kho: nhập/xuất, tồn kho, cảnh báo" },
                { "KHACH_HANG_CRM", LanguageManager.GetString("AI_MOD_KH") ?? "Quản lý khách hàng, ví RFID, điểm tích lũy" },
                { "PHAN_QUYEN", LanguageManager.GetString("AI_MOD_PQ") ?? "Phân quyền hệ thống" }
            };
        }

        // Hướng dẫn sử dụng app nhúng vào system prompt
        private static string LayAppGuide()
        {
            return LanguageManager.GetString("AI_APP_GUIDE") ??
@"HƯỚNG DẪN Sử DỤNG APP ĐẠI NAM:
- Tạo đơn hàng: Vào Tiền Sảnh > POS Bán Lẻ > Quét mã/gõ tên SP > Thanh toán
- Nhập kho: Vào Kho & F&B > Trung tâm Kho > Tab Tạo Phiếu > Chọn Nhập kho
- Xuất kho: Vào Kho & F&B > Trung tâm Kho > Tab Tạo Phiếu > Chọn Xuất kho
- Xem tồn kho: Vào Kho & F&B > Trung tâm Kho > Tab Tồn Kho
- Thêm khách hàng: Vào Quản Trị > Khách Hàng > Bấm Thêm mới
- Tìm khách hàng: Vào Quản Trị > Khách Hàng > Gõ vào ô tìm kiếm
- Quản lý combo: Vào Danh mục > Combo > Tạo mới hoặc chỉnh sửa
- Phân quyền: Vào Quản Trị > Phân Quyền;
        } Quản Trị > Khách Hàng > Bấm Thêm mới
- Tìm khách hàng: Vào Quản Trị > Khách Hàng > Gõ vào ô tìm kiếm
- Quản lý combo: Vào Danh mục > Combo > Tạo mới hoặc chỉnh sửa
- Phân quyền: Vào Quản Trị > Phân Quyền";
        }

        public AIChatService()
        {
            _http.Timeout = TimeSpan.FromSeconds(AIConfig.TimeoutSeconds);
        }

        // Đổi ngữ cảnh AI khi người dùng chuyển module
        public void SwitchContext(string contextName, string contextDescription)
        {
            _currentContext = contextName ?? "navigation";
            ClearHistory();

            if (contextName == "navigation" || string.IsNullOrEmpty(contextDescription))
                _systemPrompt = TaoPromptDieuHuong();
            else
                _systemPrompt = TaoPromptModule(contextName, contextDescription);

            _currentTools = AIToolRegistry.GetToolDefs(_currentContext);
        }

        public void ClearHistory()
        {
            _history.Clear();
        }

        // Gửi tin nhắn và thực hiện vòng lặp ReAct
        public async Task<AIResponse> SendMessage(string userText, CancellationToken ct = default)
        {
            if (!AIConfig.HasApiKey())
                return new AIResponse { Text = "Chưa cấu hình API Key. Vào Settings để thiết lập.", IsError = true };

            _history.Add(new GeminiContent
            {
                role = "user",
                parts = new List<GeminiPart> { new GeminiPart { text = userText } }
            });

            CatLichSu();
            OnProcessingChanged?.Invoke(true);

            string deferredModuleKey = null;
            Dictionary<string, object> deferredModuleArgs = null;

            try
            {
                // Vòng lặp ReAct: gửi API, nhận response, nếu có functionCall thì thực thi rồi gửi lại
                for (int loop = 0; loop < AIConfig.MaxReActLoops; loop++)
                {
                    ct.ThrowIfCancellationRequested();
                    var apiResponse = await GoiAPICoRetry(ct);

                    if (apiResponse == null)
                        return new AIResponse { Text = LayThongBaoLoi("timeout"), IsError = true };

                    var content = apiResponse.candidates?[0]?.content;
                    if (content == null)
                        return new AIResponse { Text = LayThongBaoLoi("empty"), IsError = true };

                    _history.Add(content);

                    foreach (var part in content.parts)
                    {
                        // Trường hợp AI gọi tool
                        if (part.functionCall != null)
                        {
                            string fnName = part.functionCall.name;
                            var fnArgs = part.functionCall.args ?? new Dictionary<string, object>();

                            // Nếu là lệnh mở module: trì hoãn, chờ AI trả text xong mới mở
                            if (fnName == "open_module")
                            {
                                if (fnArgs.ContainsKey("menu_key"))
                                    deferredModuleKey = fnArgs["menu_key"]?.ToString();
                                deferredModuleArgs = fnArgs;

                                ThemFunctionResponse(fnName, new Dictionary<string, object>
                                {
                                    ["status"] = "ok",
                                    ["message"] = "Module will be opened after response"
                                });
                                continue;
                            }

                            // Thực thi tool bình thường
                            string ketQua = AIToolRegistry.ExecuteTool(fnName, fnArgs);

                            // Kiểm tra có phải lệnh UI không (prefix __ACTION__)
                            if (ketQua != null && ketQua.StartsWith("__ACTION__:"))
                            {
                                PhanTichVaGuiLenhUI(ketQua);
                                ThemFunctionResponse(fnName, new Dictionary<string, object>
                                {
                                    ["status"] = "ok",
                                    ["message"] = "UI command executed"
                                });
                            }
                            else
                            {
                                ThemFunctionResponse(fnName, new Dictionary<string, object>
                                {
                                    ["status"] = "ok",
                                    ["data"] = ketQua ?? "No data"
                                });
                            }
                        }

                        // Trường hợp AI trả text: kết thúc vòng lặp
                        if (part.text != null)
                        {
                            if (deferredModuleKey != null)
                                OnOpenModuleRequested?.Invoke(deferredModuleKey, deferredModuleArgs);

                            return new AIResponse
                            {
                                Text = part.text,
                                Action = deferredModuleKey != null ? "open_module" : "respond",
                                ModuleTarget = deferredModuleKey
                            };
                        }
                    }
                }

                // Hết vòng lặp mà chưa có text
                if (deferredModuleKey != null)
                    OnOpenModuleRequested?.Invoke(deferredModuleKey, deferredModuleArgs);

                return new AIResponse { Text = LayThongBaoLoi("max_loops") };
            }
            catch (OperationCanceledException)
            {
                return new AIResponse { Text = LayThongBaoLoi("cancelled"), IsError = true };
            }
            catch (HttpRequestException ex)
            {
                return new AIResponse { Text = LayThongBaoLoi("network") + "\n" + ex.Message, IsError = true };
            }
            catch (Exception ex)
            {
                return new AIResponse { Text = LayThongBaoLoi("unknown") + "\n" + ex.Message, IsError = true };
            }
            finally
            {
                OnProcessingChanged?.Invoke(false);
            }
        }

        // Gọi Gemini API, tự động thử lại nếu lỗi
        private async Task<GeminiApiResponse> GoiAPICoRetry(CancellationToken ct)
        {
            int maxRetries = AIConfig.RetryCount;
            for (int attempt = 0; attempt <= maxRetries; attempt++)
            {
                try { return await GoiGemini(ct); }
                catch (Exception) when (attempt < maxRetries)
                {
                    await Task.Delay(AIConfig.RetryDelayMs, ct);
                }
            }
            return null;
        }

        private async Task<GeminiApiResponse> GoiGemini(CancellationToken ct)
        {
            var request = new GeminiRequest
            {
                system_instruction = new GeminiSystemInstruction
                {
                    parts = new List<GeminiPart> { new GeminiPart { text = _systemPrompt } }
                },
                contents = _history,
                tools = _currentTools
            };

            string json = _json.Serialize(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(AIConfig.Endpoint, httpContent, ct);

            if ((int)response.StatusCode == 429)
                throw new HttpRequestException("Rate limited (429). Vui lòng thử lại sau.");

            string responseJson = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return _json.Deserialize<GeminiApiResponse>(responseJson);
        }

        // Thêm kết quả tool vào lịch sử để gửi lại cho AI
        private void ThemFunctionResponse(string fnName, Dictionary<string, object> result)
        {
            _history.Add(new GeminiContent
            {
                role = "user",
                parts = new List<GeminiPart>
                {
                    new GeminiPart
                    {
                        functionResponse = new GeminiFunctionResponse { name = fnName, response = result }
                    }
                }
            });
        }

        // Cắt lịch sử để tránh vượt giới hạn token.
        // Giữ tin đầu (ngữ cảnh gốc) + N tin gần nhất.
        private void CatLichSu()
        {
            int max = AIConfig.MaxConversationTurns * 2;
            while (_history.Count > max)
            {
                if (_history.Count > 2)
                    _history.RemoveAt(1);
                else
                    _history.RemoveAt(0);
            }
        }

        // Phân tích chuỗi __ACTION__ thành lệnh UI và gửi event
        private void PhanTichVaGuiLenhUI(string actionStr)
        {
            string[] parts = actionStr.Split(new[] { ':' }, 3);
            if (parts.Length < 2) return;

            string cmd = parts[1];
            var args = new Dictionary<string, object>();

            if (parts.Length >= 3 && !string.IsNullOrEmpty(parts[2]))
            {
                try { args = _json.Deserialize<Dictionary<string, object>>(parts[2]); }
                catch { }
            }

            OnUICommandRequested?.Invoke(cmd, args);
        }

        // Tạo system prompt cho chế độ điều hướng (khi chưa mở module nào cụ thể)
        private string TaoPromptDieuHuong()
        {
            string lang = AIConfig.LanguageLabel;
            var sb = new StringBuilder();

            sb.AppendLine($"Bạn là trợ lý AI của hệ thống Quản Lý Khu Du Lịch Đại Nam.");
            sb.AppendLine($"LUÔN trả lời bằng {lang}.");
            sb.AppendLine();
            sb.AppendLine("NHIỆM VỤ: Giúp người dùng điều hướng đến đúng chức năng hoặc trả lời câu hỏi.");
            sb.AppendLine("Khi người dùng muốn mở 1 chức năng, PHẢI gọi tool open_module với menu_key tương ứng.");
            sb.AppendLine("KHÔNG BAO GIỜ chỉ dẫn bằng lời nói khi có thể gọi tool.");
            sb.AppendLine();
            sb.AppendLine("DANH SÁCH MODULE:");
            foreach (var kv in LayModuleMap())
                sb.AppendLine($"- {kv.Key}: {kv.Value}");
            sb.AppendLine();
            sb.AppendLine(LayAppGuide());

            return sb.ToString();
        }

        // Tạo system prompt cho chế độ module cụ thể (VD: đang ở Sản phẩm)
        private string TaoPromptModule(string contextName, string description)
        {
            string lang = AIConfig.LanguageLabel;
            var sb = new StringBuilder();

            sb.AppendLine($"Bạn là trợ lý AI của hệ thống Quản Lý Khu Du Lịch Đại Nam.");
            sb.AppendLine($"LUÔN trả lời bằng {lang}.");
            sb.AppendLine();
            sb.AppendLine($"Người dùng ĐANG Ở module: {contextName}");
            sb.AppendLine($"Mô tả: {description}");
            sb.AppendLine();
            sb.AppendLine("NHIỆM VỤ:");
            sb.AppendLine("1. Trả lời câu hỏi về dữ liệu trong module này bằng cách gọi tools.");
            sb.AppendLine("2. Lọc/Filter grid khi người dùng yêu cầu bằng ngôn ngữ tự nhiên (gọi tool ui_filter_grid).");
            sb.AppendLine("3. Phân tích dữ liệu và đưa khuyến nghị khi được hỏi.");
            sb.AppendLine("4. Hướng dẫn cách sử dụng chức năng trong app.");
            sb.AppendLine("5. Nếu người dùng muốn chuyển sang module khác, gọi tool open_module.");
            sb.AppendLine();
            sb.AppendLine("KHI PHÂN TÍCH DỮ LIỆU:");
            sb.AppendLine("- Tìm vấn đề (hết hàng, sắp hết hạn, bất thường)");
            sb.AppendLine("- Đưa khuyến nghị cụ thể");
            sb.AppendLine("- Sử dụng số liệu chính xác từ tool");
            sb.AppendLine();
            sb.AppendLine(LayAppGuide());

            return sb.ToString();
        }

        // Thông báo lỗi theo ngôn ngữ hiện tại
        private string LayThongBaoLoi(string loaiLoi)
        {
            string lang = AIConfig.SystemLanguage;

            if (lang == "en-US")
            {
                switch (loaiLoi)
                {
                    case "timeout": return "Request timed out. Please try again.";
                    case "empty": return "AI returned empty response.";
                    case "max_loops": return "AI reached max processing loops.";
                    case "cancelled": return "Request was cancelled.";
                    case "network": return "Network error.";
                    default: return "An unexpected error occurred.";
                }
            }
            if (lang == "zh-CN")
            {
                switch (loaiLoi)
                {
                    case "timeout": return "请求超时，请重试。";
                    case "empty": return "AI返回空响应。";
                    case "max_loops": return "AI达到最大处理循环。";
                    case "cancelled": return "请求已取消。";
                    case "network": return "网络错误。";
                    default: return "发生意外错误。";
                }
            }

            // Mặc định: Tiếng Việt
            switch (loaiLoi)
            {
                case "timeout": return "Yêu cầu bị timeout. Vui lòng thử lại.";
                case "empty": return "AI trả về response rỗng.";
                case "max_loops": return "AI đạt giới hạn xử lý. Vui lòng hỏi lại ngắn gọn hơn.";
                case "cancelled": return "Yêu cầu đã bị hủy.";
                case "network": return "Lỗi mạng.";
                default: return "Có lỗi xảy ra.";
            }
        }
    }
}

