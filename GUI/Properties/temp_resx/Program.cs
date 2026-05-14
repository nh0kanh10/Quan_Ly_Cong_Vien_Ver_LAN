using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Resources;
using System.IO;

namespace ResxUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = new[] { 
                new { Path = @"c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Properties\UIStrings.resx", Lang = "vi" },
                new { Path = @"c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Properties\UIStrings.en-US.resx", Lang = "en" },
                new { Path = @"c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Properties\UIStrings.zh-CN.resx", Lang = "zh" }
            };

            var keys = new[] {
                new { Key = "COL_THANHTIEN", Vi = "Thành tiền", En = "Amount", Zh = "总金额" },
                new { Key = "COL_DONGIA", Vi = "Đơn giá", En = "Unit Price", Zh = "单价" },
                new { Key = "COL_SOLUONG", Vi = "Số lượng", En = "Quantity", Zh = "数量" },
                new { Key = "COL_MUCCANHBAO", Vi = "Mức cảnh báo", En = "Alert Quota", Zh = "警报配额" },
                new { Key = "COL_TRANGTHAI", Vi = "Trạng thái", En = "Status", Zh = "状态" },
                new { Key = "COL_TONKHO", Vi = "Tồn kho", En = "In Stock", Zh = "现有库存" },
                new { Key = "LBL_KHO", Vi = "Kho thao tác:", En = "Warehouse:", Zh = "仓库:" },
                new { Key = "LBL_NHOMSP", Vi = "Nhóm SP:", En = "Category:", Zh = "类别:" },
                new { Key = "BTN_LOC", Vi = "Lọc dữ liệu", En = "Filter", Zh = "应用筛选" },
                new { Key = "BTN_EXCEL", Vi = "Xuất Excel", En = "Export Excel", Zh = "导出 Excel" },
                new { Key = "DANH_MUC_KHO", Vi = "DANH MỤC KHO", En = "WAREHOUSES", Zh = "仓库清单" },
                new { Key = "TRUNG_TAM_KHO", Vi = "TRUNG TÂM KHO", En = "INVENTORY HUB", Zh = "库存中心" },
                new { Key = "MNU_TAOPHIEU", Vi = "Tạo/Sửa phiếu", En = "Create/Edit Voucher", Zh = "创建/编辑凭证" },
                new { Key = "MNU_TONKHO", Vi = "Tồn kho hiện tại", En = "Current Inventory", Zh = "当前库存" },
                new { Key = "MNU_LICHSU", Vi = "Lịch sử giao dịch", En = "Transaction History", Zh = "交易历史" },
                new { Key = "MNU_CANHBAO", Vi = "Cảnh báo", En = "Alerts", Zh = "警报" },
                new { Key = "TITLE_PHIEUGANDAY", Vi = "Phiếu gần đây", En = "Recent Vouchers", Zh = "最近的凭证" },
                new { Key = "COL_MA", Vi = "Mã", En = "Code", Zh = "代码" },
                new { Key = "COL_TEN", Vi = "Tên", En = "Name", Zh = "名称" },
                new { Key = "COL_LOAIPHIEU", Vi = "Loại", En = "Type", Zh = "类型" },
                new { Key = "COL_MAPHIEU", Vi = "Mã Phiếu", En = "Voucher", Zh = "凭单号码" },
                new { Key = "COL_KHOXUAT", Vi = "Kho Xuất", En = "From", Zh = "发货仓库" },
                new { Key = "COL_KHONHAP", Vi = "Kho Nhập", En = "To", Zh = "收货仓库" },
                new { Key = "COL_CHONSP", Vi = "-- Chọn Sản Phẩm --", En = "-- Select Product --", Zh = "-- 选择产品 --" },
                new { Key = "COL_NGAYLAP", Vi = "Ngày Lập", En = "Date", Zh = "创建日期" },
                new { Key = "LBL_TUNGAY", Vi = "Từ ngày:", En = "From:", Zh = "从:" },
                new { Key = "LBL_DENNGAY", Vi = "Đến ngày:", En = "To:", Zh = "到:" },
                new { Key = "BTN_TIMKIEM", Vi = "TÌM KIẾM", En = "SEARCH", Zh = "搜索" },
                new { Key = "COL_LOAISP", Vi = "Loại sản phẩm", En = "Product Type", Zh = "产品类型" },
                new { Key = "NHAP_MUA", Vi = "Nhập mua", En = "Purchase Receipt", Zh = "采购入库" },
                new { Key = "CHUYEN_KHO", Vi = "Chuyển kho", En = "Transfer", Zh = "调拨出库" },
                new { Key = "XUAT_BAN", Vi = "Xuất bán", En = "Sales Issue", Zh = "销售出库" },
                new { Key = "HUY_HONG", Vi = "Hủy hỏng", En = "Damaged", Zh = "损坏报废" },
                new { Key = "KIEM_KE", Vi = "Kiểm kê", En = "Inventory Count", Zh = "盘点" },
                new { Key = "TRA_NCC", Vi = "Trả NCC", En = "Return to Supplier", Zh = "退回供应商" },
                new { Key = "LBL_DOITAC", Vi = "Đối tác:", En = "Partner:", Zh = "合作伙伴:" },
                new { Key = "ToChuc", Vi = "Tổ chức", En = "Organization", Zh = "组织" },
                new { Key = "CaNhan", Vi = "Cá nhân", En = "Individual", Zh = "个人" },
                new { Key = "CongTyLuHanh", Vi = "Công ty lữ hành", En = "Travel Agency", Zh = "旅行社" },
                new { Key = "ERR_POS_RFID_EMPTY", Vi = "Mã RFID không được để trống.", En = "RFID code cannot be empty.", Zh = "RFID 代码不能为空。" },
                new { Key = "ERR_POS_RFID_NOT_FOUND_OR_INACTIVE", Vi = "Thẻ RFID không tồn tại hoặc đã bị khóa.", En = "RFID card not found or inactive.", Zh = "未找到 RFID 卡或卡已被锁定。" },
                new { Key = "ERR_POS_CART_EMPTY", Vi = "Giỏ hàng hiện đang rỗng.", En = "The cart is currently empty.", Zh = "购物车目前为空。" },
                new { Key = "ERR_POS_NO_SESSION", Vi = "Vui lòng mở phiên thu ngân trước khi thanh toán.", En = "Please open a cashier session before checking out.", Zh = "结账前请先开启收银班次。" },
                new { Key = "ERR_POS_PAYMENT_INSUFFICIENT", Vi = "Số tiền thanh toán chưa đủ.", En = "Insufficient payment amount.", Zh = "付款金额不足。" },
                new { Key = "ERR_POS_RFID_PAYMENT_NO_ID", Vi = "Thanh toán bằng vòng chữ thập (RFID) nhưng không tìm thấy thông tin ví.", En = "RFID payment selected but wallet information not found.", Zh = "选择了 RFID 付款，但未找到钱包信息。" },
                new { Key = "ERR_POS_RFID_INSUFFICIENT_BALANCE", Vi = "Số dư trong vòng chữ thập (RFID) không đủ để thanh toán.", En = "Insufficient balance in the RFID card.", Zh = "RFID 卡内余额不足。" },
                new { Key = "MSG_POS_CHECKOUT_SUCCESS", Vi = "Thanh toán hóa đơn thành công!", En = "Checkout completed successfully!", Zh = "结账成功！" },
                new { Key = "ERR_POS_CHECKOUT_FAILED_UNKNOWN", Vi = "Lỗi trong quá trình thanh toán, vui lòng thử lại.", En = "Checkout failed due to an unknown error, please try again.", Zh = "由于未知错误导致结账失败，请重试。" },
                new { Key = "ERR_POS_NO_OPEN_SESSION", Vi = "Bạn chưa mở phiên thu ngân nào.", En = "You don't have any open cashier session.", Zh = "您没有任何已开启的收银班次。" },
                new { Key = "ERR_POS_MACHINE_IN_USE", Vi = "Máy POS này đang có nhân viên khác xài, vui lòng kiểm tra lại.", En = "This POS machine is currently in use by another cashier.", Zh = "该 POS 机目前正被其他收银员使用。" },
                new { Key = "ERR_POS_ALREADY_OPENED_SESSION", Vi = "Bạn đang có một phiên thu ngân đang mở rồi không thể mở thêm.", En = "You already have an active cashier session.", Zh = "您已有一个开启的收银班次。" },
                new { Key = "MSG_POS_SESSION_OPENED", Vi = "Mở phiên thu ngân thành công. Chúc bạn làm việc vui vẻ!", En = "Session opened successfully.", Zh = "班次开启成功。" },
                new { Key = "ERR_POS_INVALID_SESSION_ID", Vi = "Mã phiên không hợp lệ.", En = "Invalid session ID.", Zh = "无效的班次 ID。" },
                new { Key = "MSG_POS_SESSION_CLOSED", Vi = "Chốt ca và đóng phiên thu ngân thành công.", En = "Session closed successfully.", Zh = "班次关闭成功。" },

                // ── POS Module: ucPOS ──
                new { Key = "POS_TITLE", Vi = "ĐẠI NAM POS", En = "DAI NAM POS", Zh = "大南 POS" },
                new { Key = "POS_CASHIER", Vi = "Thu ngân: ---", En = "Cashier: ---", Zh = "收银员: ---" },
                new { Key = "POS_SHIFT", Vi = "Ca: ---", En = "Shift: ---", Zh = "班次: ---" },
                new { Key = "POS_NO_SESSION", Vi = "Chưa mở phiên thu ngân", En = "No active cashier session", Zh = "尚未开启收银班次" },
                new { Key = "POS_TAB_ALL", Vi = "Tất cả", En = "All", Zh = "全部" },
                new { Key = "POS_TAB_TICKET", Vi = "Vé", En = "Tickets", Zh = "门票" },
                new { Key = "POS_TAB_FNB", Vi = "F&&B", En = "F&&B", Zh = "餐饮" },
                new { Key = "POS_TAB_GOODS", Vi = "Hàng hóa", En = "Goods", Zh = "商品" },
                new { Key = "POS_BARCODE_HINT", Vi = "Quét mã vạch hoặc gõ tên sản phẩm...", En = "Scan barcode or type product name...", Zh = "扫描条码或输入产品名称..." },
                new { Key = "POS_COL_PRODUCT", Vi = "Sản phẩm", En = "Product", Zh = "产品" },
                new { Key = "POS_SUBTOTAL", Vi = "Tiền hàng:", En = "Subtotal:", Zh = "小计:" },
                new { Key = "POS_VAT", Vi = "Thuế VAT:", En = "VAT:", Zh = "增值税:" },
                new { Key = "POS_DISCOUNT", Vi = "Giảm giá:", En = "Discount:", Zh = "折扣:" },
                new { Key = "POS_TOTAL", Vi = "TỔNG:", En = "TOTAL:", Zh = "总计:" },
                new { Key = "POS_BTN_CHECKOUT", Vi = "THANH TOÁN (F5)", En = "CHECKOUT (F5)", Zh = "结账 (F5)" },
                new { Key = "POS_BTN_CLEAR", Vi = "Xóa giỏ", En = "Clear Cart", Zh = "清空" },
                new { Key = "POS_BTN_CLOSE_SESSION", Vi = "Đóng phiên (F8)", En = "Close Shift (F8)", Zh = "关班 (F8)" },
                new { Key = "MSG_POS_CONFIRM_CLEAR_CART", Vi = "Bạn chắc chắn muốn xóa toàn bộ giỏ hàng?", En = "Are you sure you want to clear the entire cart?", Zh = "您确定要清空整个购物车吗？" },
                new { Key = "MSG_CONFIRM", Vi = "Xác nhận", En = "Confirm", Zh = "确认" },
                new { Key = "MSG_POS_PROMO_NOT_READY", Vi = "Khuyến mãi chưa sẵn sàng.", En = "Promo engine not ready.", Zh = "促销引擎尚未就绪。" },
                new { Key = "ERR_POS_PRODUCT_NOT_FOUND", Vi = "Không tìm thấy sản phẩm.", En = "Product not found.", Zh = "未找到产品。" },
                new { Key = "ERR_POS_MACHINE_REQUIRED", Vi = "Vui lòng nhập mã máy POS.", En = "Please enter POS machine code.", Zh = "请输入 POS 机代码。" },
                new { Key = "ERR_POS_PAYMENT_NOT_ENOUGH", Vi = "Số tiền trả chưa đủ!", En = "Payment amount is insufficient!", Zh = "付款金额不足！" },

                // ── frmThanhToan ──
                new { Key = "PAY_TITLE", Vi = "Thanh toán", En = "Payment", Zh = "付款" },
                new { Key = "PAY_HEADER", Vi = "THANH TOÁN", En = "PAYMENT", Zh = "付款" },
                new { Key = "PAY_TOTAL_DUE", Vi = "Tổng phải thu:", En = "Total Due:", Zh = "应收总额:" },
                new { Key = "PAY_COL_METHOD", Vi = "Phương thức", En = "Method", Zh = "付款方式" },
                new { Key = "PAY_COL_AMOUNT", Vi = "Số tiền", En = "Amount", Zh = "金额" },
                new { Key = "PAY_COL_NOTE", Vi = "Ghi chú", En = "Note", Zh = "备注" },
                new { Key = "PAY_BTN_ADD_LINE", Vi = "+ Thêm dòng thanh toán", En = "+ Add payment line", Zh = "+ 添加付款行" },
                new { Key = "PAY_BTN_EXACT", Vi = "Vừa đủ", En = "Exact", Zh = "刚好" },
                new { Key = "PAY_PAID", Vi = "Đã trả:", En = "Paid:", Zh = "已付:" },
                new { Key = "PAY_REMAINING", Vi = "Còn thiếu:", En = "Remaining:", Zh = "尚欠:" },
                new { Key = "PAY_CHANGE", Vi = "Tiền thừa:", En = "Change:", Zh = "找零:" },
                new { Key = "PAY_RFID_CODE", Vi = "Mã thẻ:", En = "Card ID:", Zh = "卡号:" },
                new { Key = "PAY_BTN_CANCEL", Vi = "HỦY (Esc)", En = "CANCEL (Esc)", Zh = "取消 (Esc)" },
                new { Key = "PAY_BTN_CONFIRM", Vi = "XÁC NHẬN", En = "CONFIRM", Zh = "确认" },

                // ── frmPhienThuNgan ──
                new { Key = "SES_FORM_TITLE", Vi = "Phiên thu ngân", En = "Cashier Session", Zh = "收银班次" },
                new { Key = "SES_OPEN_TITLE", Vi = "MỞ PHIÊN THU NGÂN", En = "OPEN CASHIER SESSION", Zh = "开启收银班次" },
                new { Key = "SES_CLOSE_TITLE", Vi = "ĐÓNG PHIÊN THU NGÂN", En = "CLOSE CASHIER SESSION", Zh = "关闭收银班次" },
                new { Key = "SES_BTN_OPEN", Vi = "MỞ PHIÊN", En = "OPEN", Zh = "开启" },
                new { Key = "SES_BTN_CLOSE", Vi = "ĐÓNG PHIÊN", En = "CLOSE", Zh = "关闭" },
                new { Key = "SES_MACHINE", Vi = "Mã máy POS:", En = "POS Machine:", Zh = "POS 机号:" },
                new { Key = "SES_START_CASH", Vi = "Tiền đầu ca:", En = "Opening Cash:", Zh = "期初现金:" },
                new { Key = "SES_WAREHOUSE", Vi = "Kho bán hàng:", En = "Sales Warehouse:", Zh = "销售仓库:" },
                new { Key = "SES_NOTE", Vi = "Ghi chú:", En = "Note:", Zh = "备注:" },
                new { Key = "SES_END_CASH", Vi = "Tiền cuối ca:", En = "Closing Cash:", Zh = "期末现金:" },
                new { Key = "SES_TOTAL_REV", Vi = "Tổng thu trong ca:", En = "Session Revenue:", Zh = "班次收入:" },
                new { Key = "SES_DIFF", Vi = "Chênh lệch:", En = "Variance:", Zh = "差异:" },
                new { Key = "SES_SELECT_WH", Vi = "-- Chọn kho --", En = "-- Select --", Zh = "-- 选择 --" },

                // ── Customer Management / Khach Hang ──
                new { Key = "TXT_THE_DANGDUNG", Vi = "Đang dùng", En = "Active", Zh = "使用中" },
                new { Key = "TXT_THE_DAKHOA", Vi = "Đã khoá", En = "Locked", Zh = "已锁定" },
                new { Key = "TXT_THE_CHUAKICHHOAT", Vi = "Chưa kích hoạt", En = "Inactive", Zh = "未激活" },
                new { Key = "TXT_THE_DATRA", Vi = "Đã trả", En = "Returned", Zh = "已退回" },

                new { Key = "TXT_GD_NAP", Vi = "Nạp", En = "Top-up", Zh = "充值" },
                new { Key = "TXT_GD_TRU", Vi = "Trừ", En = "Deduct", Zh = "扣除" },
                new { Key = "TXT_GD_THU", Vi = "Thu", En = "Receive", Zh = "收入" },
                new { Key = "TXT_GD_CHI", Vi = "Chi", En = "Spend", Zh = "支出" },
                new { Key = "TXT_GD_CONG", Vi = "Cộng", En = "Add", Zh = "增加" },
                new { Key = "TXT_DIEM_CONG", Vi = "Cộng Điểm", En = "Earn Points", Zh = "获得积分" },
                new { Key = "TXT_DIEM_TRU", Vi = "Trừ Điểm", En = "Use Points", Zh = "使用积分" },

                new { Key = "COL_KH_MAGD", Vi = "Mã GD", En = "Txn ID", Zh = "交易代码" },
                new { Key = "COL_KH_NHOMGD", Vi = "Nhóm GD", En = "Group", Zh = "交易组" },
                new { Key = "COL_KH_LOAIGD", Vi = "Loại GD", En = "Type", Zh = "交易类型" },
                new { Key = "COL_KH_SOTIEN", Vi = "Số Tiền", En = "Amount", Zh = "金额" },
                new { Key = "COL_KH_SODIEM", Vi = "Số Điểm", En = "Points", Zh = "积分" },
                new { Key = "COL_KH_SODUSAUGD", Vi = "Số Dư Sau GD", En = "Balance After", Zh = "交易后余额" },
                new { Key = "COL_KH_MOTA", Vi = "Mô Tả", En = "Description", Zh = "描述" },
                new { Key = "COL_KH_THOIGIAN", Vi = "Thời Gian", En = "Time", Zh = "时间" },

                new { Key = "BTN_KH_KHOA_THE", Vi = "Khóa thẻ", En = "Lock Card", Zh = "锁定卡片" },
                new { Key = "BTN_KH_MOKHOA_THE", Vi = "Mở khóa thẻ", En = "Unlock Card", Zh = "解锁卡片" },
                new { Key = "BTN_KH_CHINH_DIEM", Vi = "Chỉnh sửa điểm", En = "Adjust Points", Zh = "调整积分" },
                new { Key = "BTN_KH_CAP_VI_THE", Vi = "Cấp ví & thẻ", En = "Issue E-Wallet", Zh = "发行电子钱包" },
                
                new { Key = "MSG_KH_NHAP_DIEM", Vi = "Nhập điểm (+) hoặc (-):", En = "Enter Points (+) or (-):", Zh = "输入积分 (+) 或 (-):" },
                new { Key = "MSG_KH_NHAP_LYDO", Vi = "Nhập lý do điều chỉnh điểm:", En = "Enter adjustment reason:", Zh = "输入调整原因:" },
                new { Key = "TXT_KH_LYDO_MACDINH", Vi = "Điều chỉnh cộng/trừ thao tác tay", En = "Manual adjustment", Zh = "手动调整" },
                new { Key = "MSG_KH_NHAP_MA_THE", Vi = "Nhập mã thẻ RFID:", En = "Enter RFID code:", Zh = "输入 RFID 代码:" }
            };

            foreach (var f in files)
            {
                var dict = new System.Collections.Generic.Dictionary<string, string>();
                
                // Read existing
                if (File.Exists(f.Path))
                {
                    using (ResXResourceReader reader = new ResXResourceReader(f.Path))
                    {
                        reader.UseResXDataNodes = true;
                        foreach (DictionaryEntry d in reader)
                        {
                            var node = (ResXDataNode)d.Value;
                            dict[d.Key.ToString()] = node.GetValue((ITypeResolutionService)null).ToString();
                        }
                    }
                }

                // Append/Overwrite new
                foreach (var k in keys)
                {
                    string val = f.Lang == "vi" ? k.Vi : (f.Lang == "en" ? k.En : k.Zh);
                    dict[k.Key] = val;
                }

                // Write back
                using (ResXResourceWriter writer = new ResXResourceWriter(f.Path))
                {
                    foreach (var kvp in dict)
                    {
                        writer.AddResource(kvp.Key, kvp.Value);
                    }
                    writer.Generate();
                }
                Console.WriteLine("Updated " + f.Path);
            }
        }
    }
}
