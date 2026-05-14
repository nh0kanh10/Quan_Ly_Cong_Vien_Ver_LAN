using DaiNam.UITests.Core;
using FlaUI.Core.AutomationElements;
using System.Threading;

namespace DaiNam.UITests.Pages
{
    public class KhoPage
    {
        private Window _window;

        public KhoPage(Window window)
        {
            _window = window;
        }

        // --- Elements ---
        
        // Navigation / Tab
        public Button BtnNavKhoHang => _window.FindFirstDescendant(cf => cf.ByName("Kho Hàng"))?.AsButton();
        
        // In ucTaoPhieu
        public Button BtnTaoPhieu => _window.FindFirstDescendant(cf => cf.ByName("Tạo Phiếu mới"))?.AsButton();
        public Button BtnLuu => _window.FindFirstDescendant(cf => cf.ByName("Lưu (Ctrl+S)"))?.AsButton();
        
        public TextBox TxtMaPhieu => _window.FindFirstDescendant(cf => cf.ByAutomationId("txtMaPhieu"))?.AsTextBox();
        public TextBox TxtGhiChu => _window.FindFirstDescendant(cf => cf.ByAutomationId("txtGhiChu"))?.AsTextBox();

        // --- Actions ---

        public void NavigateToKho()
        {
            UIHelper.ClickWithRetry(BtnNavKhoHang);
            // Wait for tab to load
            Thread.Sleep(1000); 
        }

        public void ClickTaoPhieu()
        {
            UIHelper.ClickWithRetry(BtnTaoPhieu);
        }

        public void NhapThongTin(string ghiChu)
        {
            UIHelper.EnterTextWithRetry(TxtGhiChu, ghiChu);
            // Mở rộng sau: Gõ vào GridView
        }

        public void LuuPhieu()
        {
            UIHelper.ClickWithRetry(BtnLuu);
        }

        public bool KiemTraLuuThanhCong()
        {
            // Tùy theo cách thông báo của app (MessageBox hay Toast)
            // Ví dụ tìm một thông báo "Thành công" trên màn hình
            var successMsg = _window.FindFirstDescendant(cf => cf.ByName("Thành công"));
            return successMsg != null;
        }
    }
}
