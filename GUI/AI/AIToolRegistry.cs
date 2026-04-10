using System;
using System.Collections.Generic;
using System.Linq;
using BUS;

namespace GUI.AI
{
    /// <summary>
    /// Registry quản lý danh sách Tools (hàm) mà AI có thể gọi, phân theo ngữ cảnh Form.
    /// Mỗi Tool = 1 hàm lấy data hoặc 1 hành động, mapped sang BUS layer.
    /// </summary>
    public class AIToolRegistry
    {
        // 
        //  TOOL DEFINITION: Mỗi Tool gồm metadata (cho Gemini) + executor (cho App)
        // 

        public class ToolDefinition
        {
            public GeminiFunctionDeclaration Declaration { get; set; }
            public Func<Dictionary<string, object>, string> Execute { get; set; }
        }

        // ── All registered tools, grouped by context name ──
        private readonly Dictionary<string, List<ToolDefinition>> _tools
            = new Dictionary<string, List<ToolDefinition>>(StringComparer.OrdinalIgnoreCase);

        public AIToolRegistry()
        {
            RegisterGlobalTools();
            RegisterNavigationTools();
            RegisterKhoHangTools();
            RegisterDonHangTools();
            RegisterKhachHangTools();
        }

        // 
        //  PUBLIC API
        // 

        /// <summary>Lấy danh sách Tool declarations cho Gemini API theo context</summary>
        public GeminiToolDef GetToolDef(string context)
        {
            var toolDef = new GeminiToolDef();

            // Mọi context đều có tool global + open_form
            if (_tools.ContainsKey("global"))
                toolDef.functionDeclarations.AddRange(_tools["global"].Select(t => t.Declaration));
            if (_tools.ContainsKey("navigation"))
                toolDef.functionDeclarations.AddRange(_tools["navigation"].Select(t => t.Declaration));

            // Thêm tools riêng của context (skip nếu đã add ở trên)
            if (!string.IsNullOrEmpty(context)
                && !context.Equals("global", StringComparison.OrdinalIgnoreCase)
                && !context.Equals("navigation", StringComparison.OrdinalIgnoreCase)
                && _tools.ContainsKey(context))
                toolDef.functionDeclarations.AddRange(_tools[context].Select(t => t.Declaration));

            return toolDef;
        }

        /// <summary>Thực thi một Tool theo tên, trả về kết quả dạng string (JSON-friendly)</summary>
        public string ExecuteTool(string toolName, Dictionary<string, object> args)
        {
            foreach (var group in _tools.Values)
            {
                var tool = group.FirstOrDefault(t =>
                    t.Declaration.name.Equals(toolName, StringComparison.OrdinalIgnoreCase));
                if (tool != null)
                    return tool.Execute(args ?? new Dictionary<string, object>());
            }

            return $"Lỗi: Không tìm thấy tool '{toolName}'.";
        }

        // ══════════════════════════════════════════════════════════════
        //  TOOL REGISTRATIONS
        // ══════════════════════════════════════════════════════════════

        private void Register(string context, ToolDefinition tool)
        {
            if (!_tools.ContainsKey(context))
                _tools[context] = new List<ToolDefinition>();
            _tools[context].Add(tool);
        }

        #region Global Tools (Available everywhere)

        private void RegisterGlobalTools()
        {
            Register("global", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_current_datetime",
                    description = "Lấy ngày giờ hiện tại của hệ thống",
                    parameters = new GeminiParameterSchema { properties = new Dictionary<string, GeminiPropertyDef>() }
                },
                Execute = (args) => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss (dddd)")
            });
        }

        #endregion

        #region Navigation Tools (open_form)

        private void RegisterNavigationTools()
        {
            Register("navigation", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "open_form",
                    description = "Mở/chuyển sang một form (màn hình) khác trong ứng dụng POS Đại Nam",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["form_name"] = new GeminiPropertyDef
                            {
                                type = "STRING",
                                description = "Tên form cần mở. Phải là một trong: frmBanHang, frmKiemSoatVe, frmDatPhong, frmDatBan, frmThueDo, frmGuiXe, frmSanPham, frmBangGia, frmCombo, frmKhuVuc, frmKhachHang, frmDoanKhach, frmKhuyenMai, frmTheRFID, frmViDienTu, frmNhanVien, frmLichLamViec, frmKhoHang, frmPhieuNhapXuat, frmSuCo, frmBaoTri, frmTroChoi, frmNhaHang, frmDongVat, frmChatLuongNuoc, frmDashboard, frmDonHang, frmPhieuThuChi, frmVaiTro, frmPhanQuyen, frmMarketing, frmKhuVucThu, frmKhuVucBien"
                            }
                        },
                        required = new List<string> { "form_name" }
                    }
                },
                // Execute sẽ được override bởi AIChatService (cần reference Form1)
                Execute = (args) =>
                {
                    string formName = GetArg(args, "form_name");
                    return $"__ACTION__:open_form:{formName}";
                }
            });
        }

        #endregion

        #region Kho Hàng Tools

        private void RegisterKhoHangTools()
        {
            Register("frmKhoHang", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_tonkho",
                    description = "Lấy danh sách tồn kho chi tiết (tên SP, số lượng, đơn giá, thành tiền). Trả về TÓM TẮT top 20 sản phẩm + tổng giá trị.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["id_kho"] = new GeminiPropertyDef { type = "INTEGER", description = "ID kho cần xem. Để trống = kho đầu tiên (kho mặc định)." }
                        }
                    }
                },
                Execute = (args) =>
                {
                    try
                    {
                        int idKho = GetIntArg(args, "id_kho", 0);
                        if (idKho <= 0)
                        {
                            var dsKho = BUS_KhoHang.Instance.LoadDS();
                            if (dsKho.Count > 0) idKho = dsKho[0].Id;
                            else return "Chưa có kho hàng nào trong hệ thống.";
                        }

                        var tonKho = BUS_KhoHang.Instance.GetTonKhoChiTiet(idKho);
                        var metrics = BUS_KhoHang.Instance.GetDashboardMetrics(idKho);

                        // Tóm tắt data thay vì đổ hết (CTO Review: Data Overflow protection)
                        int total = tonKho.Count;
                        var top = tonKho.Take(AIConfig.MaxDataRows).Select(t => new
                        {
                            t.TenSanPham,
                            t.SoLuong,
                            t.DonViTinh,
                            t.DonGia,
                            t.ThanhTien,
                            CanhBao = t.SoLuong <= (t.NguongCanhBao > 0 ? t.NguongCanhBao : 5) ? "⚠ SẮP HẾT" : ""
                        }).ToList();

                        var sapHet = tonKho.Where(t => t.SoLuong <= (t.NguongCanhBao > 0 ? t.NguongCanhBao : 5)).ToList();

                        return $"TỔNG QUAN KHO (ID={idKho}):\n" +
                               $"- Tổng loại SP: {total}\n" +
                               $"- Tổng giá trị tồn: {metrics.TongVon:N0}đ\n" +
                               $"- Sắp hết hàng: {metrics.SapHet} loại\n" +
                               $"- Âm kho (cảnh báo): {metrics.AmKho} loại\n\n" +
                               $"CHI TIẾT (Top {Math.Min(total, AIConfig.MaxDataRows)}/{total}):\n" +
                               string.Join("\n", top.Select(t =>
                                   $"  • {t.TenSanPham}: {t.SoLuong:N0} {t.DonViTinh} × {t.DonGia:N0}đ = {t.ThanhTien:N0}đ {t.CanhBao}")) +
                               (sapHet.Any() ? $"\n\n⚠ CẢNH BÁO SẮP HẾT:\n" +
                                   string.Join("\n", sapHet.Select(s => $"  🔴 {s.TenSanPham}: chỉ còn {s.SoLuong} {s.DonViTinh}")) : "");
                    }
                    catch (Exception ex) { return $"Lỗi truy vấn kho: {ex.Message}"; }
                }
            });

            Register("frmKhoHang", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_dashboard_kho",
                    description = "Lấy số liệu tổng quan nhanh của kho: tổng giá trị vốn, số SP sắp hết, số SP âm kho.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["id_kho"] = new GeminiPropertyDef { type = "INTEGER", description = "ID kho. Để trống = kho mặc định." }
                        }
                    }
                },
                Execute = (args) =>
                {
                    try
                    {
                        int idKho = GetIntArg(args, "id_kho", 0);
                        if (idKho <= 0)
                        {
                            var dsKho = BUS_KhoHang.Instance.LoadDS();
                            if (dsKho.Count > 0) idKho = dsKho[0].Id;
                            else return "Chưa có kho hàng nào.";
                        }
                        var m = BUS_KhoHang.Instance.GetDashboardMetrics(idKho);
                        return $"Dashboard Kho (ID={idKho}):\n- Tổng vốn: {m.TongVon:N0}đ\n- Sắp hết: {m.SapHet} loại\n- Âm kho: {m.AmKho} loại";
                    }
                    catch (Exception ex) { return $"Lỗi: {ex.Message}"; }
                }
            });

            Register("frmKhoHang", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_danh_sach_kho",
                    description = "Lấy danh sách tất cả các kho hàng trong hệ thống (tên kho, ID).",
                    parameters = new GeminiParameterSchema { properties = new Dictionary<string, GeminiPropertyDef>() }
                },
                Execute = (args) =>
                {
                    try
                    {
                        var dsKho = BUS_KhoHang.Instance.LoadDS();
                        if (dsKho.Count == 0) return "Chưa có kho hàng nào.";
                        return "Danh sách kho:\n" + string.Join("\n", dsKho.Select(k => $"  • ID={k.Id}: {k.TenKho}"));
                    }
                    catch (Exception ex) { return $"Lỗi: {ex.Message}"; }
                }
            });
        }

        #endregion

        #region Đơn Hàng Tools

        private void RegisterDonHangTools()
        {
            Register("frmDonHang", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_orders",
                    description = "Tìm kiếm đơn hàng theo ngày, trạng thái. Trả về tóm tắt top 20 đơn mới nhất.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["from_date"] = new GeminiPropertyDef { type = "STRING", description = "Ngày bắt đầu (yyyy-MM-dd). Để trống = hôm nay." },
                            ["to_date"]   = new GeminiPropertyDef { type = "STRING", description = "Ngày kết thúc (yyyy-MM-dd). Để trống = hôm nay." },
                            ["trang_thai"] = new GeminiPropertyDef { type = "STRING", description = "Trạng thái: DaThanhToan, ChuaThanhToan, DaHuy. Để trống = tất cả." }
                        }
                    }
                },
                Execute = (args) =>
                {
                    try
                    {
                        var ds = BUS_DonHang.Instance.LoadDS();

                        string fromStr = GetArg(args, "from_date");
                        string toStr = GetArg(args, "to_date");
                        string status = GetArg(args, "trang_thai");

                        if (DateTime.TryParse(fromStr, out DateTime from))
                            ds = ds.Where(d => d.ThoiGian >= from).ToList();
                        if (DateTime.TryParse(toStr, out DateTime to))
                            ds = ds.Where(d => d.ThoiGian <= to.AddDays(1)).ToList();
                        if (!string.IsNullOrEmpty(status))
                            ds = ds.Where(d => d.TrangThai.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

                        ds = ds.OrderByDescending(d => d.ThoiGian).ToList();
                        int total = ds.Count;
                        decimal tongTien = ds.Sum(d => d.TongTien);
                        var top = ds.Take(AIConfig.MaxDataRows);

                        return $"KẾT QUẢ TÌM KIẾM ĐƠN HÀNG:\n" +
                               $"- Tổng số đơn: {total}\n" +
                               $"- Tổng tiền: {tongTien:N0}đ\n\n" +
                               $"TOP {Math.Min(total, AIConfig.MaxDataRows)} ĐƠN MỚI NHẤT:\n" +
                               string.Join("\n", top.Select(d =>
                                   $"  • [{d.MaCode}] {d.ThoiGian:dd/MM/yyyy HH:mm} — {d.TongTien:N0}đ — {d.TrangThai}"));
                    }
                    catch (Exception ex) { return $"Lỗi: {ex.Message}"; }
                }
            });
        }

        #endregion

        #region Khách Hàng Tools

        private void RegisterKhachHangTools()
        {
            Register("frmKhachHang", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_customers",
                    description = "Tìm kiếm khách hàng theo từ khoá (tên, SĐT), loại khách hàng, hoặc xếp hạng Top khách hàng có tổng chi tiêu cao nhất / điểm cao nhất.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["keyword"] = new GeminiPropertyDef { type = "STRING", description = "Từ khoá tìm kiếm (tên hoặc SĐT). Để trống = hiện tất cả." },
                            ["loai_khach"] = new GeminiPropertyDef { type = "STRING", description = "Lọc theo loại: CaNhan, VIP, VVIP. Để trống = tất cả." },
                            ["sort_by"] = new GeminiPropertyDef { type = "STRING", description = "Sắp xếp theo: chi_tieu_desc (mặc định nếu tìm tiêu phí nhiều nhất), chi_tieu_asc, diem_desc, diem_asc. Để trống = không sắp xếp." }
                        }
                    }
                },
                Execute = (args) =>
                {
                    try
                    {
                        var ds = BUS_KhachHang.Instance.LoadDS();
                        string kw = GetArg(args, "keyword");
                        string loai = GetArg(args, "loai_khach");
                        string sortBy = GetArg(args, "sort_by");

                        if (!string.IsNullOrEmpty(kw))
                            ds = ds.Where(k => (k.HoTen ?? "").IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0
                                             || (k.DienThoai ?? "").Contains(kw)
                                             || (k.MaCode ?? "").IndexOf(kw, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                        if (!string.IsNullOrEmpty(loai))
                            ds = ds.Where(k => (k.LoaiKhach ?? "").Equals(loai, StringComparison.OrdinalIgnoreCase)).ToList();

                        if (sortBy == "chi_tieu_desc") ds = ds.OrderByDescending(k => k.TongChiTieu).ToList();
                        else if (sortBy == "chi_tieu_asc") ds = ds.OrderBy(k => k.TongChiTieu).ToList();
                        else if (sortBy == "diem_desc") ds = ds.OrderByDescending(k => k.DiemTichLuy).ToList();
                        else if (sortBy == "diem_asc") ds = ds.OrderBy(k => k.DiemTichLuy).ToList();

                        int total = ds.Count;
                        var top = ds.Take(AIConfig.MaxDataRows).ToList();

                        string prefix = "";
                        if (total == 1 && !string.IsNullOrEmpty(top[0].DienThoai))
                        {
                            prefix = $"__ACTION__:ui_select_customer:sdt={top[0].DienThoai}\n";
                        }

                        return prefix + $"TÌM THẤY {total} KHÁCH HÀNG:\n" +
                               string.Join("\n", top.Select(k =>
                                   $"  • [{k.MaCode}] {k.HoTen} — {k.GioiTinh} — SĐT: {k.DienThoai} — Loại: {k.LoaiKhach} — Điểm: {k.DiemTichLuy} — Chi tiêu: {k.TongChiTieu:N0}đ"));
                    }
                    catch (Exception ex) { return $"Lỗi: {ex.Message}"; }
                }
            });

            Register("frmKhachHang", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "ui_select_customer",
                    description = "THAO TÁC GIAO DIỆN: Chọn và hiển thị chi tiết một khách hàng cụ thể LÊN TRÊN MÀN HÌNH hệ thống.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["sdt"] = new GeminiPropertyDef { type = "STRING", description = "Số điện thoại khách cần chọn (phải chính xác)" }
                        },
                        required = new List<string> { "sdt" }
                    }
                },
                Execute = (args) =>
                {
                    string sdt = GetArg(args, "sdt");
                    return $"__ACTION__:ui_select_customer:sdt={sdt}\nĐã gửi lệnh thao tác mượt mà lên giao diện hệ thống.";
                }
            });

            Register("frmKhachHang", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "get_customer_transactions",
                    description = "Lấy lịch sử giao dịch nạp tiền/thanh toán (Giao dịch Ví) của một khách hàng dựa vào SĐT.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["sdt"] = new GeminiPropertyDef { type = "STRING", description = "Số điện thoại của khách hàng" }
                        },
                        required = new List<string> { "sdt" }
                    }
                },
                Execute = (args) =>
                {
                    try
                    {
                        string sdt = GetArg(args, "sdt");
                        var kqKhach = BUS_KhachHang.Instance.LoadDS().FirstOrDefault(k => k.DienThoai == sdt);
                        if (kqKhach == null) return "Không tìm thấy khách hàng có SĐT này.";

                        var vi = BUS_KhachHang.Instance.LayViTheoKhachHang(kqKhach.Id);
                        if (vi == null) return "Khách hàng này chưa mở Ví điện tử.";

                        var dsGD = BUS_KhachHang.Instance.LayLichSuGiaoDich(vi.Id);
                        if (dsGD == null || dsGD.Count == 0) return "Khách hàng chưa có giao dịch nào.";

                        var top = dsGD.OrderByDescending(x => x.ThoiGian).Take(10).ToList();
                        return $"__ACTION__:ui_select_customer:sdt={sdt}\n" + 
                               $"10 GIAO DỊCH MỚI NHẤT CỦA {kqKhach.HoTen} (Tổng dư: {vi.SoDuKhaDung:N0}đ):\n" +
                               string.Join("\n", top.Select(t => $"  • {t.ThoiGian:dd/MM/yyyy HH:mm} | {t.LoaiGiaoDich} | {t.SoTien:N0}đ"));
                    }
                    catch (Exception ex) { return $"Lỗi: {ex.Message}"; }
                }
            });

            Register("frmKhachHang", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "ui_open_recharge_modal",
                    description = "THAO TÁC GIAO DIỆN: Mở hộp thoại (Modal) Nạp Tiền cho một khách hàng bằng SĐT.",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["sdt"] = new GeminiPropertyDef { type = "STRING", description = "Số điện thoại của khách hàng cần nạp" }
                        },
                        required = new List<string> { "sdt" }
                    }
                },
                Execute = (args) =>
                {
                    string sdt = GetArg(args, "sdt");
                    return $"__ACTION__:ui_open_recharge_modal:sdt={sdt}\nĐã gửi lệnh mở bảng nạp tiền lên giao diện cho SĐT {sdt}.";
                }
            });

            Register("frmKhachHang", new ToolDefinition
            {
                Declaration = new GeminiFunctionDeclaration
                {
                    name = "ui_sort_customers",
                    description = "THAO TÁC GIAO DIỆN: Sắp xếp danh sách khách hàng TRÊN MÀN HÌNH theo một tiêu chí (tiêu phí nhiều nhất, điểm cao nhất).",
                    parameters = new GeminiParameterSchema
                    {
                        properties = new Dictionary<string, GeminiPropertyDef>
                        {
                            ["sort_column"] = new GeminiPropertyDef { type = "STRING", description = "Cột cần sắp xếp (TongChiTieu, DiemTichLuy, HoTen, MaCode)" },
                            ["sort_direction"] = new GeminiPropertyDef { type = "STRING", description = "Hướng sắp xếp: ASC (tăng dần) hoặc DESC (giảm dần)" }
                        },
                        required = new List<string> { "sort_column", "sort_direction" }
                    }
                },
                Execute = (args) =>
                {
                    string col = GetArg(args, "sort_column");
                    string dir = GetArg(args, "sort_direction");
                    return $"__ACTION__:ui_sort_customers:col={col},dir={dir}\nĐã gửi lệnh sắp xếp dữ liệu lên giao diện.";
                }
            });
        }

        #endregion

        // ══════════════════════════════════════════════════════════════
        //  HELPERS
        // ══════════════════════════════════════════════════════════════

        private static string GetArg(Dictionary<string, object> args, string key)
        {
            if (args != null && args.TryGetValue(key, out object val) && val != null)
                return val.ToString();
            return null;
        }

        private static int GetIntArg(Dictionary<string, object> args, string key, int defaultVal)
        {
            string s = GetArg(args, key);
            if (!string.IsNullOrEmpty(s) && int.TryParse(s, out int result))
                return result;
            return defaultVal;
        }
    }
}
