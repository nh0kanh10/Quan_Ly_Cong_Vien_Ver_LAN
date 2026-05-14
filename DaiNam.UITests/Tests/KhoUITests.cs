using DaiNam.UITests.Core;
using DaiNam.UITests.Pages;
using NUnit.Framework;

namespace DaiNam.UITests.Tests
{
    [TestFixture]
    public class KhoUITests : TestBase
    {
        [Test]
        public void Test_MoTabKhoHang_ThanhCong()
        {
            var khoPage = new KhoPage(AppManager.MainWindow);
            
            // 1. Điều hướng tới Kho
            khoPage.NavigateToKho();
            
            // 2. Chờ màn hình kho load xong và bấm nút Tạo Phiếu
            khoPage.ClickTaoPhieu();
            
            // Assert that the Creation tab is now active (can check if GhiChu textbox is visible)
            Assert.IsNotNull(khoPage.TxtGhiChu, "Không tìm thấy ô Ghi chú sau khi bấm Tạo Phiếu.");
        }

        [Test]
        public void Test_TaoPhieuKho_KhongThongTin_SeBaoLoi()
        {
            var khoPage = new KhoPage(AppManager.MainWindow);
            
            khoPage.NavigateToKho();
            khoPage.ClickTaoPhieu();
            
            // Cố tình không nhập gì và bấm lưu
            khoPage.LuuPhieu();
            
            // Assert thông báo lỗi
            // Implement finding the error message dialog and assert it's visible.
        }
    }
}
