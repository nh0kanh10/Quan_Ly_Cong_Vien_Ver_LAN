using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GUI.AI
{
    /// <summary>
    /// Core AI Engine — Giao tiếp Gemini API + ReAct Loop + Context Switching.
    /// Đây là "bộ não" của toàn bộ hệ thống AI Chatbox.
    /// </summary>
    public class AIChatService
    {
        private static readonly HttpClient _http = new HttpClient { Timeout = TimeSpan.FromSeconds(AIConfig.TimeoutSeconds) };
        private static readonly JavaScriptSerializer _json = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };

        private readonly AIToolRegistry _registry = new AIToolRegistry();

        // ── Conversation state ──
        private readonly List<GeminiContent> _history = new List<GeminiContent>();
        private string _currentContext = "navigation";
        private string _systemPrompt = "";

        // ── Event: khi AI yêu cầu mở form ──
        public event Action<string> OnOpenFormRequested;
        // ── Event: khi AI ra lệnh điều khiển UI (Action Dispatching) ──
        public event Action<string, Dictionary<string, object>> OnUICommandRequested;
        // ── Event: khi AI đang xử lý (typing indicator) ──
        public event Action<bool> OnProcessingChanged;

        // ══════════════════════════════════════════════════════════════
        //  CONTEXT SWITCHING — "ĐỔI NÃO"
        // ══════════════════════════════════════════════════════════════

        /// <summary>Đổi ngữ cảnh AI khi chuyển form.</summary>
        public void SwitchContext(string contextName, string contextDescription)
        {
            _currentContext = contextName ?? "navigation";
            _history.Clear(); // Reset conversation khi đổi não

            if (contextName == "navigation" || string.IsNullOrEmpty(contextName))
            {
                _systemPrompt = BuildNavigationPrompt();
            }
            else
            {
                _systemPrompt = BuildFormSpecificPrompt(contextName, contextDescription);
            }
        }

        /// <summary>Xoá lịch sử hội thoại (giữ nguyên context)</summary>
        public void ClearHistory()
        {
            _history.Clear();
        }

        // ══════════════════════════════════════════════════════════════
        //  SEND MESSAGE — REACT LOOP
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// Gửi tin nhắn cho AI và nhận phản hồi. 
        /// Tự động thực hiện ReAct loop nếu AI cần gọi hàm lấy data.
        /// </summary>
        public async Task<AIResponse> SendMessage(string userMessage)
        {
            if (!AIConfig.HasApiKey())
                return new AIResponse { Text = "Chưa cấu hình API Key. Vào Settings để nhập Gemini API Key.", IsError = true };

            // Thêm tin nhắn user
            _history.Add(new GeminiContent
            {
                role = "user",
                parts = new List<GeminiPart> { new GeminiPart { text = userMessage } }
            });

            // Trim history nếu quá dài
            while (_history.Count > AIConfig.MaxConversationTurns * 2)
                _history.RemoveAt(0);

            OnProcessingChanged?.Invoke(true);

            try
            {
                // ── ReAct Loop ──
                string pendingFormOpen = null; // Defer open_form to avoid mid-loop history clear

                for (int loop = 0; loop < AIConfig.MaxReActLoops; loop++)
                {
                    var response = await CallGeminiAPI();

                    if (response == null)
                        return new AIResponse { Text = "❌ Lỗi: Cấu trúc API trả về không hợp lệ.", IsError = true };

                    // Parse response
                    var candidate = response.candidates?.FirstOrDefault();
                    var parts = candidate?.content?.parts;
                    if (parts == null || parts.Count == 0)
                        return new AIResponse { Text = "AI không trả lời được. Vui lòng thử lại.", IsError = true };

                    var part = parts[0];

                    // CASE A: AI muốn gọi hàm (Function Call)
                    if (part.functionCall != null)
                    {
                        string toolName = part.functionCall.name;
                        var toolArgs = part.functionCall.args ?? new Dictionary<string, object>();

                        // Thêm function call vào history
                        _history.Add(new GeminiContent
                        {
                            role = "model",
                            parts = new List<GeminiPart> { new GeminiPart { functionCall = part.functionCall } }
                        });

                        // Kiểm tra nếu là open_form → DEFER, không fire event trong loop
                        if (toolName == "open_form")
                        {
                            string formName = toolArgs.ContainsKey("form_name") ? toolArgs["form_name"]?.ToString() : "";
                            pendingFormOpen = formName;

                            // Gửi function response thành công
                            _history.Add(new GeminiContent
                            {
                                role = "user",
                                parts = new List<GeminiPart> { new GeminiPart
                                {
                                    functionResponse = new GeminiFunctionResponse
                                    {
                                        name = "open_form",
                                        response = new Dictionary<string, object> { ["result"] = $"Đã mở form {formName} thành công." }
                                    }
                                }}
                            });

                            // Tiếp tục loop để AI gửi tin nhắn xác nhận
                            continue;
                        }

                        // Thực thi Tool trên BUS layer
                        string toolResult = _registry.ExecuteTool(toolName, toolArgs);

                        // Xử lý báo hiệu Action từ Tool (dành cho UI Dispatching)
                        if (toolName.StartsWith("ui_"))
                        {
                            OnUICommandRequested?.Invoke(toolName, toolArgs);
                        }
                        else if (toolResult.StartsWith("__ACTION__:ui_"))
                        {
                            // Lấy tên action từ chuỗi (Vd: __ACTION__:ui_select_customer)
                            string firstLine = toolResult.Split('\n')[0];
                            string actionName = firstLine.Split(':')[1];
                            OnUICommandRequested?.Invoke(actionName, toolArgs);
                        }

                        // Thêm function response vào history
                        _history.Add(new GeminiContent
                        {
                            role = "user",
                            parts = new List<GeminiPart> { new GeminiPart
                            {
                                functionResponse = new GeminiFunctionResponse
                                {
                                    name = toolName,
                                    response = new Dictionary<string, object> { ["result"] = toolResult }
                                }
                            }}
                        });

                        // TIẾP TỤC LOOP → gửi lại cho AI để AI xử lý data
                        continue;
                    }

                    // CASE B: AI trả lời text → THOÁT LOOP
                    if (!string.IsNullOrEmpty(part.text))
                    {
                        // Thêm response vào history
                        _history.Add(new GeminiContent
                        {
                            role = "model",
                            parts = new List<GeminiPart> { new GeminiPart { text = part.text } }
                        });

                        // Fire open_form SAU KHI loop xong
                        if (!string.IsNullOrEmpty(pendingFormOpen))
                            OnOpenFormRequested?.Invoke(pendingFormOpen);

                        return new AIResponse { Text = part.text, Action = "respond" };
                    }
                }

                // Nếu hết vòng lặp mà AI vẫn chưa trả lời text
                var fallback = new AIResponse { Text = "⏳ Tôi đang xử lý quá nhiều bước. Bạn thử hỏi đơn giản hơn nhé!", IsError = false };

                // Fire open_form SAU KHI loop xong để không clear history giữa loop
                if (!string.IsNullOrEmpty(pendingFormOpen))
                    OnOpenFormRequested?.Invoke(pendingFormOpen);

                return fallback;
            }
            catch (TaskCanceledException)
            {
                return new AIResponse { Text = "Quá thời gian chờ. Gemini API không phản hồi kịp.", IsError = true };
            }
            catch (HttpRequestException ex)
            {
                return new AIResponse { Text = $"Lỗi mạng: {ex.Message}", IsError = true };
            }
            catch (Exception ex)
            {
                return new AIResponse { Text = $"Lỗi: {ex.Message}", IsError = true };
            }
            finally
            {
                OnProcessingChanged?.Invoke(false);
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  GEMINI API CALL
        // ══════════════════════════════════════════════════════════════

        private async Task<GeminiApiResponse> CallGeminiAPI()
        {
            var tools = _registry.GetToolDef(_currentContext);

            // Build request body manually (JavaScriptSerializer-friendly)
            var requestBody = new Dictionary<string, object>
            {
                ["system_instruction"] = new Dictionary<string, object>
                {
                    ["parts"] = new object[] { new Dictionary<string, string> { ["text"] = _systemPrompt } }
                },
                ["contents"] = _history.Select(ContentToDict).ToArray(),
                ["tools"] = new object[] { ToolDefToDict(tools) }
            };

            string json = _json.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await _http.PostAsync(AIConfig.Endpoint, content);

            if ((int)resp.StatusCode == 429)
                return ParseResponse("{\"candidates\":[{\"content\":{\"parts\":[{\"text\":\"Tôi đang hơi mệt (quá nhiều yêu cầu), chờ tôi 5 giây nhé!\"}],\"role\":\"model\"}}]}");

            string responseJson = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                string errMsg = responseJson;
                try
                {
                    var errDict = _json.Deserialize<Dictionary<string, object>>(responseJson);
                    if (errDict != null && errDict.ContainsKey("error"))
                    {
                        var errObj = errDict["error"] as Dictionary<string, object>;
                        if (errObj != null && errObj.ContainsKey("message"))
                            errMsg = errObj["message"].ToString();
                    }
                }
                catch { }
                throw new Exception($"Lỗi Gemini (HTTP {(int)resp.StatusCode}): {errMsg}");
            }

            var parsed = ParseResponse(responseJson);
            if (parsed == null)
            {
                string limitJson = responseJson.Length > 300 ? responseJson.Substring(0, 300) + "..." : responseJson;
                throw new Exception($"Không thể phân tích JSON (HTTP 200): {limitJson}");
            }
            return parsed;
        }

        // 
        //  SERIALIZATION HELPERS
        // 

        private GeminiApiResponse ParseResponse(string json)
        {
            try
            {
                // JavaScriptSerializer returns Dictionary<string,object> for nested objects
                var raw = _json.Deserialize<Dictionary<string, object>>(json);
                if (raw == null) return null;

                var result = new GeminiApiResponse { candidates = new List<GeminiCandidate>() };
                var candidates = raw.ContainsKey("candidates") ? raw["candidates"] as System.Collections.IEnumerable : null;
                if (candidates == null) return null;

                foreach (object candidateItem in candidates)
                {
                    var c = candidateItem as Dictionary<string, object>;
                    if (c == null) continue;
                    var candidate = new GeminiCandidate { content = new GeminiContent() };
                    var contentDict = c.ContainsKey("content") ? c["content"] as Dictionary<string, object> : null;
                    if (contentDict == null) continue;

                    candidate.content.role = contentDict.ContainsKey("role") ? contentDict["role"]?.ToString() : "model";
                    candidate.content.parts = new List<GeminiPart>();

                    var partsArr = contentDict.ContainsKey("parts") ? contentDict["parts"] as System.Collections.IEnumerable : null;
                    if (partsArr == null) continue;

                    foreach (object partItem in partsArr)
                    {
                        var p = partItem as Dictionary<string, object>;
                        if (p == null) continue;

                        var gPart = new GeminiPart();

                        if (p.ContainsKey("text"))
                            gPart.text = p["text"]?.ToString();

                        if (p.ContainsKey("functionCall"))
                        {
                            var fc = p["functionCall"] as Dictionary<string, object>;
                            if (fc != null)
                            {
                                gPart.functionCall = new GeminiFunctionCall
                                {
                                    name = fc.ContainsKey("name") ? fc["name"]?.ToString() : "",
                                    args = fc.ContainsKey("args") ? fc["args"] as Dictionary<string, object> ?? new Dictionary<string, object>() : new Dictionary<string, object>()
                                };
                            }
                        }

                        candidate.content.parts.Add(gPart);
                    }

                    result.candidates.Add(candidate);
                }

                return result;
            }
            catch { return null; }
        }

        private Dictionary<string, object> ContentToDict(GeminiContent c)
        {
            var dict = new Dictionary<string, object> { ["role"] = c.role };
            var parts = new List<object>();

            foreach (var p in c.parts)
            {
                if (!string.IsNullOrEmpty(p.text))
                    parts.Add(new Dictionary<string, string> { ["text"] = p.text });

                else if (p.functionCall != null)
                    parts.Add(new Dictionary<string, object>
                    {
                        ["functionCall"] = new Dictionary<string, object>
                        {
                            ["name"] = p.functionCall.name,
                            ["args"] = p.functionCall.args
                        }
                    });

                else if (p.functionResponse != null)
                    parts.Add(new Dictionary<string, object>
                    {
                        ["functionResponse"] = new Dictionary<string, object>
                        {
                            ["name"] = p.functionResponse.name,
                            ["response"] = p.functionResponse.response
                        }
                    });
            }

            dict["parts"] = parts.ToArray();
            return dict;
        }

        private Dictionary<string, object> ToolDefToDict(GeminiToolDef toolDef)
        {
            var declarations = toolDef.functionDeclarations.Select(fd =>
            {
                var d = new Dictionary<string, object>
                {
                    ["name"] = fd.name,
                    ["description"] = fd.description,
                };

                if (fd.parameters != null)
                {
                    var paramDict = new Dictionary<string, object> { ["type"] = fd.parameters.type ?? "OBJECT" };
                    var props = new Dictionary<string, object>();
                    foreach (var kvp in fd.parameters.properties)
                    {
                        props[kvp.Key] = new Dictionary<string, string>
                        {
                            ["type"] = kvp.Value.type,
                            ["description"] = kvp.Value.description
                        };
                    }
                    paramDict["properties"] = props;
                    if (fd.parameters.required != null && fd.parameters.required.Count > 0)
                        paramDict["required"] = fd.parameters.required.ToArray();
                    d["parameters"] = paramDict;
                }

                return d;
            }).ToArray();

            return new Dictionary<string, object> { ["functionDeclarations"] = declarations };
        }

        private static string EscapeJson(string s) => (s ?? "").Replace("\"", "'").Replace("\n", " ").Replace("\r", "");

        // ══════════════════════════════════════════════════════════════
        //  PROMPT BUILDERS
        // ══════════════════════════════════════════════════════════════

        private string BuildNavigationPrompt()
        {
            return @"Bạn là trợ lý AI của hệ thống Quản Lý Công Viên Đại Nam (POS WinForms).
Vai trò: Giúp nhân viên ĐIỀU HƯỚNG đến đúng màn hình (form) cần thiết.
Hiện tại người dùng đang ở MÀN HÌNH CHÍNH.

DANH SÁCH FORM CÓ THỂ MỞ:
TIỀN SẢNH:
- frmBanHang: Bán hàng POS, tạo đơn hàng mới, thanh toán
- frmKiemSoatVe: Soát vé điện tử, quét QR vé
- frmDatPhong: Đặt phòng khách sạn
- frmDatBan: Đặt bàn nhà hàng
- frmThueDo: Cho thuê vật dụng (áo phao, xe đạp, tủ khóa)
- frmGuiXe: Quản lý bãi đỗ xe, vé xe

QUẢN TRỊ:
- frmSanPham: Danh mục sản phẩm, dịch vụ
- frmBangGia: Bảng giá vé theo ngày thường/cuối tuần/lễ
- frmCombo: Quản lý combo vé + dịch vụ
- frmKhuVuc: Khu vực công viên (Khu A, B, C...)
- frmKhachHang: Hồ sơ khách hàng, tích điểm, VIP
- frmDoanKhach: Đoàn khách (trường học, công ty)
- frmKhuyenMai: Chương trình khuyến mãi
- frmTheRFID: Quản lý thẻ RFID cho khách
- frmViDienTu: Ví điện tử & RFID Wallet

VẬN HÀNH:
- frmNhanVien: Quản lý nhân sự
- frmLichLamViec: Lịch làm việc, chấm công
- frmKhoHang: Kho hàng, tồn kho, nhập/xuất
- frmPhieuNhapXuat: Phiếu nhập xuất kho
- frmSuCo: Báo cáo sự cố
- frmBaoTri: Lịch bảo trì thiết bị
- frmTroChoi: Quản lý trò chơi
- frmNhaHang: Nhà hàng, thực đơn
- frmDongVat: Quản lý động vật
- frmChatLuongNuoc: Chất lượng nước khu biển
- frmKhuVucThu: Khu vực thú (sở thú)
- frmKhuVucBien: Khu vực biển nhân tạo

BÁO CÁO:
- frmDashboard: Dashboard tổng quan doanh thu
- frmDonHang: Tra cứu đơn hàng, hoá đơn
- frmPhieuThuChi: Sổ thu chi, phiếu thu/chi

HỆ THỐNG:
- frmVaiTro: Quản lý vai trò
- frmPhanQuyen: Phân quyền truy cập
- frmMarketing: Chiến dịch Marketing

QUY TẮC BẮT BUỘC:
1. Khi người dùng muốn mở/xem/tra cứu một chức năng → BẮT BUỘC gọi tool open_form NGAY LẬP TỨC. TUYỆT ĐỐI KHÔNG chỉ gợi ý bằng text.
2. Khi người dùng hỏi về dữ liệu (khách hàng, kho, đơn hàng...) → PHẢI gọi open_form để chuyển form trước, rồi dùng tool lấy data.
3. Nếu không chắc form nào phù hợp → Hỏi lại ngắn gọn (tối đa 1 câu).
4. Trả lời bằng tiếng Việt, thân thiện, ngắn gọn.
5. Sau khi mở form, hãy gợi ý 1-2 thao tác người dùng có thể làm tiếp.
6. KHÔNG BAO GIỜ nói 'bạn hãy vào form X' — thay vào đó PHẢI gọi tool open_form để mở form đó cho người dùng.";
        }

        private string BuildFormSpecificPrompt(string formName, string formDescription)
        {
            return $@"Bạn là trợ lý AI của hệ thống Quản Lý Công Viên Đại Nam.
Hiện tại người dùng đang ở form: {formName}
Mô tả: {formDescription}

BẠN CÓ THỂ:
1. Trả lời câu hỏi về nghiệp vụ của form này
2. Dùng các tool được cung cấp để lấy dữ liệu thực từ hệ thống
3. Dùng tool open_form để chuyển sang form khác nếu người dùng yêu cầu

QUY TẮC:
1. Khi cần DATA để trả lời → Gọi tool phù hợp, KHÔNG bịa số liệu
2. Trả lời bằng tiếng Việt, thân thiện, có số liệu cụ thể
3. Nếu phát hiện vấn đề (hàng sắp hết, đơn bất thường...) → Chủ động cảnh báo
4. Data trả về từ tool là dữ liệu THẬT từ database, hãy phân tích và tóm tắt dễ hiểu";
        }
    }
}
