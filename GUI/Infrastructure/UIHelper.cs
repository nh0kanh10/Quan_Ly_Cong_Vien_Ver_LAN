using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace GUI.Infrastructure
{
    // Hộp thoại thông báo chuẩn hoá — thay thế MessageBox.Show() rải rác.
    // Dùng XtraMessageBox (DevExpress) để giữ đúng skin WXI.
    public static class UIHelper
    {
        /// <summary>
        /// Thông báo thành công (icon check xanh).
        /// </summary>
        /// <param name="msg">Nội dung hiển thị</param>
        /// <param name="title">Tiêu đề hộp thoại</param>
        public static void ThongBao(string msg, string title = "Thông báo")
        {
            XtraMessageBox.Show(msg, title,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Cảnh báo (icon tam giác vàng).
        /// </summary>
        /// <param name="msg">Nội dung cảnh báo</param>
        /// <param name="title">Tiêu đề hộp thoại</param>
        public static void CanhBao(string msg, string title = "Cảnh báo")
        {
            XtraMessageBox.Show(msg, title,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Lỗi nghiêm trọng (icon X đỏ).
        /// </summary>
        /// <param name="msg">Nội dung lỗi</param>
        /// <param name="title">Tiêu đề hộp thoại</param>
        public static void Loi(string msg, string title = "Lỗi")
        {
            XtraMessageBox.Show(msg, title,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ThongBaoLoi(string msg, string title = "Lỗi")
        {
            Loi(msg, title);
        }

        /// <summary>
        /// Hỏi xác nhận Có/Không (icon dấu hỏi).
        /// </summary>
        /// <param name="msg">Câu hỏi hiển thị</param>
        /// <param name="title">Tiêu đề hộp thoại</param>
        /// <returns>true nếu người dùng bấm Yes</returns>
        public static bool XacNhan(string msg, string title = "Xác nhận")
        {
            return XtraMessageBox.Show(msg, title,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// Xác nhận xoá — câu hỏi mặc định rõ ràng, tránh xoá nhầm.
        /// </summary>
        /// <param name="tenDoiTuong">Tên đối tượng cần xoá (VD: "khách hàng Nguyễn Văn A")</param>
        /// <returns>true nếu người dùng đồng ý xoá</returns>
        public static bool XacNhanXoa(string tenDoiTuong)
        {
            return XacNhan($"Bạn có chắc muốn xoá {tenDoiTuong}?\nHành động này không thể hoàn tác.", "Xác nhận xoá");
        }

        /// <summary>
        /// Xác nhận hủy — cảnh báo mất dữ liệu khi đang nhập dở.
        /// </summary>
        /// <returns>true nếu người dùng đồng ý hủy và mất dữ liệu</returns>
        public static bool XacNhanHuy()
        {
            return XacNhan("Dữ liệu chưa được lưu.\nBạn có chắc muốn hủy và đóng khung nhập liệu không?", "Cảnh báo mất dữ liệu");
        }
    }
}
