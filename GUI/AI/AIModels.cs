using System.Collections.Generic;

namespace GUI.AI
{
    // ══════════════════════════════════════════════════════════════
    //  GEMINI API REQUEST MODELS
    // ══════════════════════════════════════════════════════════════

    public class GeminiRequest
    {
        public GeminiSystemInstruction system_instruction { get; set; }
        public List<GeminiContent> contents { get; set; } = new List<GeminiContent>();
        public List<GeminiToolDef> tools { get; set; }
    }

    public class GeminiSystemInstruction
    {
        public List<GeminiPart> parts { get; set; } = new List<GeminiPart>();
    }

    public class GeminiContent
    {
        public string role { get; set; } // "user" or "model"
        public List<GeminiPart> parts { get; set; } = new List<GeminiPart>();
    }

    public class GeminiPart
    {
        public string text { get; set; }
        public GeminiFunctionCall functionCall { get; set; }
        public GeminiFunctionResponse functionResponse { get; set; }
    }

    public class GeminiFunctionCall
    {
        public string name { get; set; }
        public Dictionary<string, object> args { get; set; } = new Dictionary<string, object>();
    }

    public class GeminiFunctionResponse
    {
        public string name { get; set; }
        public Dictionary<string, object> response { get; set; } = new Dictionary<string, object>();
    }

    // ══════════════════════════════════════════════════════════════
    //  GEMINI API RESPONSE MODELS
    // ══════════════════════════════════════════════════════════════

    public class GeminiApiResponse
    {
        public List<GeminiCandidate> candidates { get; set; }
    }

    public class GeminiCandidate
    {
        public GeminiContent content { get; set; }
    }

    // ══════════════════════════════════════════════════════════════
    //  TOOL DEFINITION MODELS (for Gemini Function Calling)
    // ══════════════════════════════════════════════════════════════

    public class GeminiToolDef
    {
        public List<GeminiFunctionDeclaration> functionDeclarations { get; set; } = new List<GeminiFunctionDeclaration>();
    }

    public class GeminiFunctionDeclaration
    {
        public string name { get; set; }
        public string description { get; set; }
        public GeminiParameterSchema parameters { get; set; }
    }

    public class GeminiParameterSchema
    {
        public string type { get; set; } = "OBJECT";
        public Dictionary<string, GeminiPropertyDef> properties { get; set; } = new Dictionary<string, GeminiPropertyDef>();
        public List<string> required { get; set; }
    }

    public class GeminiPropertyDef
    {
        public string type { get; set; } // "STRING", "INTEGER", "NUMBER", "BOOLEAN"
        public string description { get; set; }
    }

    // ══════════════════════════════════════════════════════════════
    //  APP-LEVEL AI RESPONSE (parsed from Gemini)
    // ══════════════════════════════════════════════════════════════

    public class AIResponse
    {
        /// <summary>Văn bản trả lời hiển thị cho người dùng</summary>
        public string Text { get; set; }

        /// <summary>"open_form" | "respond" | null</summary>
        public string Action { get; set; }

        /// <summary>Tên form cần mở (khi Action = "open_form")</summary>
        public string FormTarget { get; set; }

        /// <summary>True nếu API gặp lỗi</summary>
        public bool IsError { get; set; }
    }
}
