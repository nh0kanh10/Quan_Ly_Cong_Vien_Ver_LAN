using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using BUS.Services.DanhMuc;
using BUS.Services.DoiTac;
using BUS.Services.Kho;
using ET.Models.DoiTac;
using GUI.Infrastructure;

namespace GUI.AI
{
    // Đăng ký và thực thi các Tools (Function Calling) cho AI.
    // Mỗi tool được nhóm theo context (module đang mở).
    // AI chỉ thấy tools liên quan đến module hiện tại.
    public static class AIToolRegistry
    {
        private static readonly JavaScriptSerializer _json = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };

        // Lưu tool theo nhóm: context chứa list of tools
        private static readonly Dictionary<string, List<ToolDefinition>> _registry
            = new Dictionary<string, List<ToolDefinition>>();

        private static bool _initialized;

        // Cấu trúc 1 tool
        public class ToolDefinition
        {
            public GeminiFunctionDeclaration Declaration { get; set; }
            public Func<Dictionary<string, object>, string> Execute { get; set; }
        }

        // Khởi tạo lần đầu

        private static void EnsureInitialized()
        {
            if (_initialized) return;
            _initialized = true;

            RegisterGlobalTools();
            RegisterNavigationTools();
            RegisterSanPhamTools();
            RegisterComboTools();
            RegisterKhoTools();
            RegisterKhachHangTools();
        }



        // Lấy danh sách tool definitions cho 1 context để gửi cho Gemini
        public static List<GeminiToolDef> GetToolDefs(string context)
        {
            EnsureInitialized();
            var declarations = new List<GeminiFunctionDeclaration>();

            // Luôn có global + navigation
            AddDeclarations(declarations, "global");
            AddDeclarations(declarations, "navigation");

            // Thêm tools của context hiện tại
            if (context != "global" && context != "navigation")
                AddDeclarations(declarations, context);

            if (declarations.Count == 0) return null;
            return new List<GeminiToolDef> { new GeminiToolDef { functionDeclarations = declarations } };
        }

        // Thực thi 1 tool theo tên
        public static string ExecuteTool(string toolName, Dictionary<string, object> args)
        {
            EnsureInitialized();

            foreach (var group in _registry.Values)
            {
                var tool = group.FirstOrDefault(t => t.Declaration.name == toolName);
                if (tool != null)
                {
                    try { return tool.Execute(args); }
                    catch (Exception ex) { return "Error: " + ex.Message; }
                }
            }
            return "Tool not found: " + toolName;
        }



        private static void Register(string context, ToolDefinition tool)
        {
            if (!_registry.ContainsKey(context))
                _registry[context] = new List<ToolDefinition>();
            _registry[context].Add(tool);
        }

        private static void AddDeclarations(List<GeminiFunctionDeclaration> list, string context)
        {
            if (_registry.TryGetValue(context, out var tools))
                list.AddRange(tools.Select(t => t.Declaration));
        }

        private static string GetArg(Dictionary<string, object> args, string key, string defaultVal = "")
        {
            if (args != null && args.TryGetValue(key, out object val) && val != null)
                return val.ToString();
            return defaultVal;
        }

        // Giới hạn số dòng dữ liệu trả về cho AI để tránh vượt token
        private static string SummarizeList<T>(List<T> list, Func<T, string> formatter)
        {
            int max = AIConfig.MaxDataRows;
            var sb = new StringBuilder();
            sb.AppendLine($"Tổng: {list.Count} bản ghi" + (list.Count > max ? $" (hiển thị {max} đầu tiên)" : ""));

            int count = Math.Min(list.Count, max);
            for (int i = 0; i < count; i++)
                sb.AppendLine(formatter(list[i]));

            return sb.ToString();
        }

        // Đăng ký từng nhóm tools

        private static void RegisterGlobalTools()
        {
            Register("global", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_current_datetime",
                    description = "Lay ngay gio hien tai cua he thong",
                    parameters = new GeminiParameterSchema { properties = new Dictionary<string, GeminiPropertyDef>() }
                },
                Execute = args => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss (dddd)")
            });

            // Tool hướng dẫn sử dụng app
            Register("global", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_app_guide",
                    description = "Lay huong dan su dung ung dung. Goi khi user hoi cach lam gi do trong app.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["topic"] = new GeminiPropertyDef { type = "STRING", description = "Chu de can huong dan (VD: tao don hang, nhap kho, them khach hang)" }
                        },
                        required = new List<string> { "topic" }
                    }
                },
                Execute = args => GetAppGuide(GetArg(args, "topic"))
            });
        }

        private static void RegisterNavigationTools()
        {
            Register("navigation", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "open_module",
                    description = "Mo 1 module/chuc nang trong ung dung. Danh sach menu_key hop le: DASHBOARD, POS_BAN_LE, SAN_PHAM, COMBO, DANH_MUC_KHO, TRUNG_TAM_KHO, KHACH_HANG_CRM, PHAN_QUYEN",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["menu_key"] = new GeminiPropertyDef { type = "STRING", description = "MenuKey cua module can mo" }
                        },
                        required = new List<string> { "menu_key" }
                    }
                },
                Execute = args => "OK"
            });
        }

        private static void RegisterSanPhamTools()
        {
            Register("SAN_PHAM", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_products",
                    description = "Lay danh sach san pham. Co the loc theo loai (VeVaoKhu, VeTroChoi, AnUong, TuDo, DoChoThue, VatTu...)",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["loai"] = new GeminiPropertyDef { type = "STRING", description = "Loai san pham can loc (null = tat ca)" }
                        }
                    }
                },
                Execute = args =>
                {
                    string loai = GetArg(args, "loai", null);
                    string lang = AIConfig.SystemLanguage;
                    var ds = BUS_SanPham.Instance.LayDanhSach(loai, lang);
                    return SummarizeList(ds, sp =>
                        $"[{sp.MaSanPham}] {sp.TenSanPham} | Loai: {sp.LoaiSanPham} | Gia: {sp.DonGia:N0} | TrangThai: {sp.TrangThai}");
                }
            });

            // Tool lọc grid bằng ngôn ngữ tự nhiên
            Register("SAN_PHAM", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "ui_filter_grid",
                    description = "Loc/filter grid san pham. Cot hop le: TenSanPham, MaSanPham, LoaiSanPham, DonGia, TrangThai. Operator: =, !=, >, <, >=, <=, contains",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["filter_expression"] = new GeminiPropertyDef
                            {
                                type = "STRING",
                                description = "Bieu thuc loc dang DevExpress FilterString. VD: [LoaiSanPham] = 'VeVaoKhu' AND [DonGia] > 200000"
                            }
                        },
                        required = new List<string> { "filter_expression" }
                    }
                },
                Execute = args =>
                {
                    string filter = GetArg(args, "filter_expression");
                    return $"__ACTION__:ui_filter_grid:{{\"filter\":\"{EscapeJson(filter)}\"}}";
                }
            });
        }

        private static void RegisterComboTools()
        {
            Register("COMBO", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_combos",
                    description = "Lay danh sach combo (goi san pham)",
                    parameters = new GeminiParameterSchema { properties = new Dictionary<string, GeminiPropertyDef>() }
                },
                Execute = args =>
                {
                    var ds = BUS_Combo.Instance.LayDanhSach();
                    return SummarizeList(ds, c =>
                        $"[{c.MaCombo}] {c.TenCombo} | Gia: {c.GiaCombo:N0} | TrangThai: {c.TrangThai}");
                }
            });

            Register("COMBO", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_combo_detail",
                    description = "Lay chi tiet thanh phan cua 1 combo",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["id_combo"] = new GeminiPropertyDef { type = "INTEGER", description = "Id cua combo" }
                        },
                        required = new List<string> { "id_combo" }
                    }
                },
                Execute = args =>
                {
                    int id;
                    int.TryParse(GetArg(args, "id_combo", "0"), out id);
                    var ds = BUS_Combo.Instance.LayChiTiet(id);
                    if (ds == null || ds.Count == 0) return "Combo khong co thanh phan hoac khong ton tai.";
                    return SummarizeList(ds, ct =>
                        $"  - {ct.TenSanPham} x{ct.SoLuong} | TyLe: {ct.TyLePhanBo}%");
                }
            });

            Register("COMBO", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "ui_filter_grid",
                    description = "Loc grid combo. Cot: TenCombo, MaCombo, GiaCombo, TrangThai",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["filter_expression"] = new GeminiPropertyDef
                            {
                                type = "STRING",
                                description = "Bieu thuc loc DevExpress FilterString"
                            }
                        },
                        required = new List<string> { "filter_expression" }
                    }
                },
                Execute = args =>
                {
                    string filter = GetArg(args, "filter_expression");
                    return $"__ACTION__:ui_filter_grid:{{\"filter\":\"{EscapeJson(filter)}\"}}";
                }
            });
        }

        private static void RegisterKhoTools()
        {
            Register("DANH_MUC_KHO", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_warehouses",
                    description = "Lay danh sach kho hang",
                    parameters = new GeminiParameterSchema { properties = new Dictionary<string, GeminiPropertyDef>() }
                },
                Execute = args =>
                {
                    string lang = AIConfig.SystemLanguage;
                    var ds = BUS_Kho.Instance.GetAllKho(lang);
                    return SummarizeList(ds, k =>
                        $"[{k.MaKho}] {k.TenKho} | KhoAo: {k.LaKhoAo} | TrangThai: {k.TrangThai}");
                }
            });

            Register("TRUNG_TAM_KHO", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_warehouses",
                    description = "Lay danh sach kho hang (dung cho phan tich tong quan kho)",
                    parameters = new GeminiParameterSchema { properties = new Dictionary<string, GeminiPropertyDef>() }
                },
                Execute = args =>
                {
                    string lang = AIConfig.SystemLanguage;
                    var ds = BUS_Kho.Instance.GetAllKho(lang);
                    return SummarizeList(ds, k =>
                        $"[{k.MaKho}] {k.TenKho} | TrangThai: {k.TrangThai}");
                }
            });
        }

        private static void RegisterKhachHangTools()
        {
            Register("KHACH_HANG_CRM", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_customers",
                    description = "Lay danh sach khach hang. Co the tim theo tu khoa (ten, SDT, ma KH)",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["keyword"] = new GeminiPropertyDef { type = "STRING", description = "Tu khoa tim kiem (null = tat ca)" }
                        }
                    }
                },
                Execute = args =>
                {
                    string kw = GetArg(args, "keyword", null);
                    var ds = BUS_KhachHang.Instance.LayDanhSach(kw);
                    return SummarizeList(ds, kh =>
                        $"[{kh.MaKhachHang}] {kh.HoTen} | SDT: {kh.DienThoai} | Hang: {kh.HangThanhVien} | Diem: {kh.DiemTichLuy}");
                }
            });

             Register("KHACH_HANG_CRM", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "ui_filter_grid",
                    description = "Loc grid khach hang. Cot: HoTen, MaKhachHang, DienThoai, HangThanhVien, DiemTichLuy, SoDu",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["filter_expression"] = new GeminiPropertyDef
                            {
                                type = "STRING",
                                description = "Bieu thuc loc DevExpress FilterString"
                            }
                        },
                        required = new List<string> { "filter_expression" }
                    }
                },
                Execute = args =>
                {
                    string filter = GetArg(args, "filter_expression");
                    return $"__ACTION__:ui_filter_grid:{{\"filter\":\"{EscapeJson(filter)}\"}}";
                }
            });

            // Xem chi tiết 1 khách hàng (ví, điểm, thẻ RFID, tổng chi tiêu)
            Register("KHACH_HANG_CRM", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_customer_detail",
                    description = "Xem chi tiet 1 khach hang: ho ten, SDT, email, CCCD, dia chi, hang thanh vien, diem tich luy, so du vi, tong chi tieu, trang thai the RFID",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["id"] = new GeminiPropertyDef { type = "INTEGER", description = "IdDoiTac cua khach hang" }
                        },
                        required = new List<string> { "id" }
                    }
                },
                Execute = args =>
                {
                    int id;
                    int.TryParse(GetArg(args, "id", "0"), out id);
                    var kh = BUS_KhachHang.Instance.LayChiTiet(id);
                    if (kh == null) return "Khong tim thay khach hang voi ID nay.";
                    var sb = new StringBuilder();
                    sb.AppendLine($"Ma: {kh.MaKhachHang} | Ho ten: {kh.HoTen}");
                    sb.AppendLine($"SDT: {kh.DienThoai} | Email: {kh.Email ?? "—"}");
                    sb.AppendLine($"CCCD: {kh.Cccd ?? "—"} | Dia chi: {kh.DiaChi ?? "—"}");
                    sb.AppendLine($"Loai khach: {kh.LoaiKhach} | Hang TV: {kh.HangThanhVien}");
                    sb.AppendLine($"Diem tich luy: {kh.DiemTichLuy} | So du vi: {kh.SoDuVi:N0}");
                    sb.AppendLine($"Tong chi tieu: {kh.TongChiTieu:N0} | Co vi: {kh.CoViDienTu}");
                    sb.AppendLine($"The RFID: {kh.MaTheRFID ?? "Chua cap"} | Trang thai the: {kh.TrangThaiThe ?? "—"}");
                    sb.AppendLine($"Ngay tao: {kh.NgayTao:dd/MM/yyyy}");
                    return sb.ToString();
                }
            });

            // Tìm khách hàng theo mã thẻ RFID
            Register("KHACH_HANG_CRM", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "find_customer_by_rfid",
                    description = "Tim khach hang theo ma the RFID",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["rfid_code"] = new GeminiPropertyDef { type = "STRING", description = "Ma the RFID can tim" }
                        },
                        required = new List<string> { "rfid_code" }
                    }
                },
                Execute = args =>
                {
                    string ma = GetArg(args, "rfid_code");
                    string lang = AIConfig.SystemLanguage;
                    var result = BUS_KhachHang.Instance.TimTheoRFID(ma, lang);
                    if (!result.Success) return "Khong tim thay khach hang voi the RFID: " + ma;
                    return "Tim thay. " + result.Message;
                }
            });

            // Thống kê top khách hàng chi tiêu nhiều nhất
            Register("KHACH_HANG_CRM", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_top_customers",
                    description = "Lay top N khach hang chi tieu nhieu nhat hoac diem tich luy cao nhat. Dung de phan tich khach VIP.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["top_n"] = new GeminiPropertyDef { type = "INTEGER", description = "So luong top (mac dinh 10)" },
                            ["sort_by"] = new GeminiPropertyDef { type = "STRING", description = "Sap xep theo: TongChiTieu (mac dinh) hoac DiemTichLuy hoac SoDuVi" }
                        }
                    }
                },
                Execute = args =>
                {
                    int topN;
                    if (!int.TryParse(GetArg(args, "top_n", "10"), out topN) || topN <= 0) topN = 10;
                    string sortBy = GetArg(args, "sort_by", "TongChiTieu");

                    var ds = BUS_KhachHang.Instance.LayDanhSach(null);
                    if (ds == null || ds.Count == 0) return "Chua co du lieu khach hang.";

                    List<DTO_KhachHangChiTiet> sorted;
                    if (sortBy == "DiemTichLuy")
                        sorted = ds.OrderByDescending(k => k.DiemTichLuy).Take(topN).ToList();
                    else if (sortBy == "SoDuVi")
                        sorted = ds.OrderByDescending(k => k.SoDuVi).Take(topN).ToList();
                    else
                        sorted = ds.OrderByDescending(k => k.TongChiTieu).Take(topN).ToList();

                    return SummarizeList(sorted, (kh) =>
                        $"[{kh.MaKhachHang}] {kh.HoTen} | Hang: {kh.HangThanhVien} | ChiTieu: {kh.TongChiTieu:N0} | Diem: {kh.DiemTichLuy} | Vi: {kh.SoDuVi:N0}");
                }
            });

            // Phân tích tổng quan khách hàng (số lượng theo hạng, tổng điểm, tổng ví)
            Register("KHACH_HANG_CRM", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "analyze_customers",
                    description = "Phan tich tong quan khach hang: so luong theo hang thanh vien, tong diem, tong so du vi, trung binh chi tieu. Dung khi nguoi dung hoi thong ke, bao cao.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>()
                    }
                },
                Execute = args =>
                {
                    var ds = BUS_KhachHang.Instance.LayDanhSach(null);
                    if (ds == null || ds.Count == 0) return "Chua co du lieu khach hang.";

                    var sb = new StringBuilder();
                    sb.AppendLine($"TONG QUAN KHACH HANG:");
                    sb.AppendLine($"- Tong so: {ds.Count} khach");
                    sb.AppendLine($"- Tong diem tich luy: {ds.Sum(k => k.DiemTichLuy):N0}");
                    sb.AppendLine($"- Tong so du vi: {ds.Sum(k => k.SoDuVi):N0} VND");
                    sb.AppendLine($"- Tong chi tieu: {ds.Sum(k => k.TongChiTieu):N0} VND");
                    sb.AppendLine($"- Trung binh chi tieu/KH: {(ds.Count > 0 ? ds.Average(k => k.TongChiTieu) : 0):N0} VND");
                    sb.AppendLine();
                    sb.AppendLine("PHAN BO THEO HANG THANH VIEN:");
                    var nhomHang = ds.GroupBy(k => k.HangThanhVien ?? "Chua phan hang")
                                     .OrderByDescending(g => g.Count());
                    foreach (var g in nhomHang)
                        sb.AppendLine($"  - {g.Key}: {g.Count()} khach | TB chi tieu: {g.Average(k => k.TongChiTieu):N0}");
                    sb.AppendLine();
                    sb.AppendLine("PHAN BO THEO LOAI KHACH:");
                    var nhomLoai = ds.GroupBy(k => k.LoaiKhach ?? "Khong ro")
                                     .OrderByDescending(g => g.Count());
                    foreach (var g in nhomLoai)
                        sb.AppendLine($"  - {g.Key}: {g.Count()} khach");

                    int coVi = ds.Count(k => k.CoViDienTu);
                    sb.AppendLine();
                    sb.AppendLine($"VI DIEN TU: {coVi}/{ds.Count} khach co vi ({(ds.Count > 0 ? coVi * 100 / ds.Count : 0)}%)");

                    return sb.ToString();
                }
            });

            // Chọn 1 khách hàng trên danh sách (highlight dòng)
            Register("KHACH_HANG_CRM", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "ui_select_customer",
                    description = "Chon 1 khach hang tren grid theo IdDoiTac. Dung khi can xem chi tiet hoac thao tac voi KH cu the.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["id"] = new GeminiPropertyDef { type = "INTEGER", description = "IdDoiTac cua khach hang can chon" }
                        },
                        required = new List<string> { "id" }
                    }
                },
                Execute = args =>
                {
                    string id = GetArg(args, "id");
                    return $"__ACTION__:ui_select_customer:{{\"id\":\"{id}\"}}";
                }
            });

            // Mở form nạp tiền cho khách hàng đang chọn
            Register("KHACH_HANG_CRM", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "ui_open_topup",
                    description = "Mo form nap tien vi cho khach hang dang chon tren grid. KH phai co vi dien tu. Nguoi dung se nhap so tien va xac nhan tren form.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>()
                    }
                },
                Execute = args => "__ACTION__:ui_open_topup:{}"
            });

            // Mở form chỉnh điểm cho khách hàng đang chọn
            Register("KHACH_HANG_CRM", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "ui_open_adjust_points",
                    description = "Mo form chinh sua diem tich luy (cong/tru) cho khach hang dang chon. Nguoi dung nhap so diem va ly do tren form.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>()
                    }
                },
                Execute = args => "__ACTION__:ui_open_adjust_points:{}"
            });

            // Mở form cấp ví & thẻ RFID cho khách hàng đang chọn
            Register("KHACH_HANG_CRM", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "ui_open_issue_card",
                    description = "Mo form cap vi dien tu va the RFID cho khach hang dang chon. Chi dung khi KH chua co vi.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>()
                    }
                },
                Execute = args => "__ACTION__:ui_open_issue_card:{}"
            });
        }

        // Trả về hướng dẫn sử dụng app theo chủ đề

        private static string GetAppGuide(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic)) return "Vui long cho biet ban can huong dan ve chuc nang nao?";

            string t = topic.ToLower();
            var sb = new StringBuilder();

            if (t.Contains("don hang") || t.Contains("pos") || t.Contains("ban hang") || t.Contains("thanh toan"))
            {
                sb.AppendLine("HUONG DAN TAO DON HANG:");
                sb.AppendLine("1. Mo module: Tien Sanh & Ban Hang > Man hinh POS (Ban le)");
                sb.AppendLine("2. Tim san pham bang cach go ten hoac quet ma vach");
                sb.AppendLine("3. Click san pham de them vao gio hang");
                sb.AppendLine("4. Dieu chinh so luong neu can");
                sb.AppendLine("5. Nhan nut Thanh Toan");
                sb.AppendLine("6. Chon phuong thuc thanh toan (Tien mat / The / Vi RFID)");
                sb.AppendLine("7. Xac nhan thanh toan");
            }
            else if (t.Contains("nhap kho") || t.Contains("xuat kho") || t.Contains("phieu"))
            {
                sb.AppendLine("HUONG DAN NHAP/XUAT KHO:");
                sb.AppendLine("1. Mo module: Kho & F&B > Trung tam Kho");
                sb.AppendLine("2. Chon tab 'Tao Phieu'");
                sb.AppendLine("3. Chon loai phieu: Nhap kho / Xuat kho / Kiem ke");
                sb.AppendLine("4. Chon kho dich");
                sb.AppendLine("5. Them san pham vao phieu, nhap so luong");
                sb.AppendLine("6. Bam Luu de tao phieu");
            }
            else if (t.Contains("khach hang") || t.Contains("them khach"))
            {
                sb.AppendLine("HUONG DAN QUAN LY KHACH HANG:");
                sb.AppendLine("1. Mo module: Quan Tri > Khach Hang");
                sb.AppendLine("2. De them moi: Bam nut 'Them moi', dien thong tin, bam Luu");
                sb.AppendLine("3. De tim kiem: Go ten/SDT vao o tim kiem");
                sb.AppendLine("4. De sua: Click chon KH tren grid, chinh sua, bam Luu");
            }
            else if (t.Contains("combo"))
            {
                sb.AppendLine("HUONG DAN QUAN LY COMBO:");
                sb.AppendLine("1. Mo module: Danh muc > Quan ly Combo");
                sb.AppendLine("2. De tao combo: Nhap ten + gia o panel trai, bam Luu");
                sb.AppendLine("3. De them san pham vao combo: Chon SP o grid phai, bam nut Them vao Ro");
                sb.AppendLine("4. Dieu chinh so luong va ty le phan bo");
                sb.AppendLine("5. Bam Luu Ro de xac nhan");
            }
            else if (t.Contains("ton kho") || t.Contains("canh bao"))
            {
                sb.AppendLine("HUONG DAN XEM TON KHO:");
                sb.AppendLine("1. Mo module: Kho & F&B > Trung tam Kho");
                sb.AppendLine("2. Chon tab 'Ton Kho' de xem so luong hien tai");
                sb.AppendLine("3. Chon tab 'Canh Bao' de xem SP sap het hoac het han");
            }
            else if (t.Contains("phan quyen") || t.Contains("vai tro"))
            {
                sb.AppendLine("HUONG DAN PHAN QUYEN:");
                sb.AppendLine("1. Mo module: Quan Tri > Phan Quyen");
                sb.AppendLine("2. Chon vai tro can chinh sua");
                sb.AppendLine("3. Tick cac quyen tuong ung");
                sb.AppendLine("4. Bam Luu");
            }
            else
            {
                sb.AppendLine("Khong tim thay huong dan cu the cho chu de: " + topic);
                sb.AppendLine("Cac chu de co san: don hang, nhap kho, xuat kho, khach hang, combo, ton kho, phan quyen");
            }

            return sb.ToString();
        }

        private static string EscapeJson(string s)
        {
            if (s == null) return "";
            return s.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }
    }
}
