$file = 'c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Properties\UpdateResx.cs'
$content = Get-Content $file -Raw -Encoding UTF8

$newKeys = @"

                // KhuyenMai
                new { Key = "MENU_ITEM_KHUYEN_MAI", Vi = "Quản lý Khuyến mãi", En = "Promotion Management", Zh = "促销管理" },
                new { Key = "LBL_KM_BANNER", Vi = "QUẢN LÝ KHUYẾN MÃI", En = "PROMOTION MANAGEMENT", Zh = "促销管理" },
                new { Key = "LBL_KM_TAB_INFO", Vi = "Thông tin chung", En = "General Info", Zh = "基本信息" },
                new { Key = "LBL_KM_TAB_DIEUKIEN", Vi = "Điều kiện áp dụng", En = "Conditions", Zh = "适用条件" },
                new { Key = "LBL_KM_MA", Vi = "Mã KM:", En = "Promo Code:", Zh = "促销代码:" },
                new { Key = "LBL_KM_TEN", Vi = "Tên:", En = "Name:", Zh = "名称:" },
                new { Key = "LBL_KM_LOAI", Vi = "Loại giảm:", En = "Discount Type:", Zh = "折扣类型:" },
                new { Key = "LBL_KM_GIA_TRI", Vi = "Giá trị:", En = "Value:", Zh = "价值:" },
                new { Key = "LBL_KM_TU_NGAY", Vi = "Từ ngày:", En = "From:", Zh = "开始日期:" },
                new { Key = "LBL_KM_DEN_NGAY", Vi = "Đến ngày:", En = "To:", Zh = "结束日期:" },
                new { Key = "LBL_KM_DON_TT", Vi = "Đơn tối thiểu:", En = "Min. Order:", Zh = "最低订单:" },
                new { Key = "LBL_KM_SO_LAN", Vi = "Số lần tối đa:", En = "Max Uses:", Zh = "最大使用次数:" },
                new { Key = "LBL_KM_CHONG_CHEO", Vi = "Cho phép kết hợp KM khác", En = "Stackable with other promotions", Zh = "可与其他促销叠加" },
                new { Key = "LBL_KM_AND_HINT", Vi = "Điều kiện kết hợp AND. Muốn HOẶC thì tạo nhiều KM riêng.", En = "Conditions use AND logic. For OR, create separate promotions.", Zh = "条件使用AND逻辑。要使用OR，创建单独的促销。" },
                new { Key = "COL_KM_LOAI_DK", Vi = "Loại điều kiện", En = "Condition Type", Zh = "条件类型" },
                new { Key = "COL_KM_PHEP_SO", Vi = "Phép so", En = "Operator", Zh = "运算符" },
                new { Key = "COL_KM_GIA_TRI_DK", Vi = "Giá trị", En = "Value", Zh = "值" },
                new { Key = "TT_KM_THEM", Vi = "Tạo mới chương trình khuyến mãi", En = "Create new promotion", Zh = "创建新促销" },
                new { Key = "TT_KM_LUU", Vi = "Lưu thông tin chương trình KM", En = "Save promotion info", Zh = "保存促销信息" },
                new { Key = "TT_KM_XOA", Vi = "Xóa chương trình KM đang chọn", En = "Delete selected promotion", Zh = "删除选定的促销" },
                new { Key = "TT_KM_THEM_DK", Vi = "Thêm điều kiện mới", En = "Add new condition", Zh = "添加新条件" },
                new { Key = "TT_KM_LUU_DK", Vi = "Lưu danh sách điều kiện", En = "Save conditions", Zh = "保存条件" },
                new { Key = "MSG_KM_GRID_EMPTY", Vi = "Chưa có chương trình khuyến mãi nào.", En = "No promotions yet.", Zh = "暂无促销活动。" },
                new { Key = "MSG_KM_DK_EMPTY", Vi = "Chưa có điều kiện nào.", En = "No conditions yet.", Zh = "暂无条件。" },
                new { Key = "MSG_KM_CHON_TRUOC", Vi = "Vui lòng chọn một chương trình KM trước.", En = "Please select a promotion first.", Zh = "请先选择一个促销活动。" },
                new { Key = "MSG_KM_XOA_CONFIRM", Vi = "Bạn có chắc muốn xóa chương trình này?", En = "Are you sure you want to delete this promotion?", Zh = "您确定要删除此促销活动吗？" },
                new { Key = "MSG_KM_LUU_OK", Vi = "Lưu chương trình khuyến mãi thành công!", En = "Promotion saved successfully!", Zh = "促销活动保存成功！" },
                new { Key = "MSG_KM_XOA_OK", Vi = "Xóa chương trình khuyến mãi thành công!", En = "Promotion deleted successfully!", Zh = "促销活动删除成功！" },
                new { Key = "MSG_KM_DK_LUU_OK", Vi = "Lưu điều kiện thành công!", En = "Conditions saved successfully!", Zh = "条件保存成功！" },
                new { Key = "LBL_KM_TONG", Vi = "Tổng: {0}", En = "Total: {0}", Zh = "总计: {0}" },
                new { Key = "LBL_KM_ACTIVE", Vi = "Hoạt động: {0}", En = "Active: {0}", Zh = "活跃: {0}" },
                new { Key = "LBL_KM_EXPIRED", Vi = "Hết hạn: {0}", En = "Expired: {0}", Zh = "已过期: {0}" },
                new { Key = "FLT_KM_TAT_CA", Vi = "Tất cả", En = "All", Zh = "全部" },
                new { Key = "FLT_KM_ACTIVE", Vi = "Đang hoạt động", En = "Active", Zh = "活跃" },
                new { Key = "FLT_KM_EXPIRED", Vi = "Hết hạn", En = "Expired", Zh = "已过期" },
                new { Key = "ERR_KM_MA_RONG", Vi = "Mã KM không được để trống.", En = "Promo code cannot be empty.", Zh = "促销代码不能为空。" },
                new { Key = "ERR_KM_TEN_RONG", Vi = "Tên không được để trống.", En = "Name cannot be empty.", Zh = "名称不能为空。" },
                new { Key = "ERR_KM_GIA_TRI_SAI", Vi = "Giá trị giảm phải lớn hơn 0.", En = "Discount value must be greater than 0.", Zh = "折扣金额必须大于0。" },
                new { Key = "ERR_KM_PHANTRAM_VUOT", Vi = "Giảm % không được vượt quá 100.", En = "Percentage discount cannot exceed 100.", Zh = "百分比折扣不能超过100。" },
                new { Key = "ERR_KM_NGAY_SAI", Vi = "Ngày bắt đầu phải trước ngày kết thúc.", En = "Start date must be before end date.", Zh = "开始日期必须早于结束日期。" },
                new { Key = "ERR_KM_TRUNG_MA", Vi = "Mã KM đã tồn tại. Vui lòng dùng mã khác.", En = "Promo code already exists.", Zh = "促销代码已存在。" },

"@

$anchor = 'new { Key = "MSG_DIEMBAN_EMPTY"'
$content = $content.Replace($anchor, $anchor + $newKeys)
[System.IO.File]::WriteAllText($file, $content, [System.Text.Encoding]::UTF8)
Write-Host "Done - added KhuyenMai i18n keys"
