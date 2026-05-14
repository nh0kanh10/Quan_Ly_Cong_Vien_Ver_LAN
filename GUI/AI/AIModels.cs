using System.Collections.Generic;

namespace GUI.AI
{
    // Các model dùng để gửi request đến Gemini API

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
        public string role { get; set; } // "user" hoặc "model"
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

    // Các model dùng để đọc response từ Gemini API

    public class GeminiApiResponse
    {
        public List<GeminiCandidate> candidates { get; set; }
    }

    public class GeminiCandidate
    {
        public GeminiContent content { get; set; }
    }

    // Định nghĩa tool cho Function Calling

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

    // Response nội bộ của app sau khi AI xử lý xong

    public class AIResponse
    {
        public string Text { get; set; }
        public string Action { get; set; }         // "open_module" hoặc "respond"
        public string ModuleTarget { get; set; }    // MenuKey cần mở
        public bool IsError { get; set; }
    }
}
