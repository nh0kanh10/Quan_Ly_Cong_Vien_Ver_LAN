using System.Linq;

namespace ET.Constants
{
    /// <summary>
    /// Class hỗ trợ (Helper) phân loại nghiệp vụ Loại Sản Phẩm.
    /// Giúp tách biệt Logic khỏi tầng UI (WinForms Code-Behind).
    /// </summary>
    public static class ProductTypeHelper
    {
        /// <summary>
        /// Kiểm tra xem một Loại sản phẩm có phải là "Sản phẩm ảo/dịch vụ" hay không.
        /// Sản phẩm ảo là những sản phẩm KHÔNG BAO GIỜ cần theo dõi thẻ kho vật lý.
        /// </summary>
        /// <param name="loaiSanPham">Mã loại sản phẩm (từ hằng số LoaiSanPham)</param>
        /// <returns>True nếu là hàng ảo, False nếu là hàng vật lý</returns>
        public static bool IsVirtualProduct(string loaiSanPham)
        {
            if (string.IsNullOrEmpty(loaiSanPham)) return false;

            // Danh sách các loại sản phẩm mang tính chất Dịch vụ/Vé (Phi vật lý)
            string[] virtualTypes = new[]
            {
                AppConstants.LoaiSanPham.VeVaoKhu,
                AppConstants.LoaiSanPham.VeTroChoi,
                AppConstants.LoaiSanPham.LuuTru,
                AppConstants.LoaiSanPham.GuiXe,
                AppConstants.LoaiSanPham.DatChoThuAn,
                AppConstants.LoaiSanPham.TuDo,
                AppConstants.LoaiSanPham.ChoiNghiMat
            };

            return virtualTypes.Contains(loaiSanPham);
        }

        /// <summary>
        /// Kiểm tra xem một Loại sản phẩm BẮT BUỘC phải là Vật tư (Tồn kho) hay không.
        /// </summary>
        public static bool IsInventoryForced(string loaiSanPham)
        {
            if (string.IsNullOrEmpty(loaiSanPham)) return false;

            string[] inventoryTypes = new[]
            {
                AppConstants.LoaiSanPham.NguyenLieu,
                AppConstants.LoaiSanPham.HangHoa,
                AppConstants.LoaiSanPham.DoChoThue,
                AppConstants.LoaiSanPham.DoUongDongChai,
                AppConstants.LoaiSanPham.AnUongTienLoi
            };

            return inventoryTypes.Contains(loaiSanPham);
        }

        /// Kiểm tra xem một Loại sản phẩm NGHIÊM CẤM tích chọn Vật tư (Phải trừ kho qua BOM).
        public static bool IsInventoryForbidden(string loaiSanPham)
        {
            if (string.IsNullOrEmpty(loaiSanPham)) return false;

            string[] forbiddenTypes = new[]
            {
                AppConstants.LoaiSanPham.AnUong,
                AppConstants.LoaiSanPham.DoUong
            };

            return forbiddenTypes.Contains(loaiSanPham) || IsVirtualProduct(loaiSanPham);
        }

        /// <summary>
        /// Kiểm tra loại sản phẩm có được áp giảm giá (chiết khấu / KM) hay không.
        /// F&B (AnUong, DoUong) và NguyenLieu -> KHÔNG bao giờ giảm giá.
        /// </summary>
        public static bool IsDiscountable(string loaiSanPham)
        {
            if (string.IsNullOrEmpty(loaiSanPham)) return false;

            string[] khongGiam = new[]
            {
                AppConstants.LoaiSanPham.AnUong,
                AppConstants.LoaiSanPham.DoUong,
                AppConstants.LoaiSanPham.DoUongDongChai,
                AppConstants.LoaiSanPham.AnUongTienLoi,
                AppConstants.LoaiSanPham.NguyenLieu
            };

            return !khongGiam.Contains(loaiSanPham);
        }

        /// <summary>
        /// Lấy Tiền tố (Prefix) gợi ý cho Mã Sản Phẩm dựa theo Loại hình.
        /// <param name="loaiSanPham">Mã loại sản phẩm (từ hằng số AppConstants.LoaiSanPham)</param>
        /// <returns>Tiền tố gợi ý (VD: VE_, FB_, HH_, P_, NL_, SP_)</returns>
        /// </summary>
        public static string GetPrefix(string loaiSanPham)
        {
            if (string.IsNullOrEmpty(loaiSanPham)) return "";

            switch (loaiSanPham)
            {
                case AppConstants.LoaiSanPham.VeVaoKhu:
                case AppConstants.LoaiSanPham.VeTroChoi:
                    return "VE_";
                case AppConstants.LoaiSanPham.AnUong:
                case AppConstants.LoaiSanPham.DoUong:
                    return "FB_";
                case AppConstants.LoaiSanPham.HangHoa:
                case AppConstants.LoaiSanPham.DoChoThue:
                case AppConstants.LoaiSanPham.DoUongDongChai:
                case AppConstants.LoaiSanPham.AnUongTienLoi:
                    return "HH_";
                case AppConstants.LoaiSanPham.LuuTru:
                case AppConstants.LoaiSanPham.ChoiNghiMat:
                    return "P_";
                case AppConstants.LoaiSanPham.NguyenLieu:
                    return "NL_";
                case AppConstants.LoaiSanPham.TuDo:
                    return "TD_";
                case AppConstants.LoaiSanPham.PhuongTien:
                    return "XE_";
                default:
                    return "SP_";
            }
        }
    }
}
