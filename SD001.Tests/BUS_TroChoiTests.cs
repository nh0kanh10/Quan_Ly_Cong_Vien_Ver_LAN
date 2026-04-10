using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BUS;
using ET;

namespace SD001.Tests
{
    [TestClass]
    public class BUS_TroChoiTests
    {
        private Mock<ITroChoiGateway> _mockGateway;
        private BUS_TroChoi _bus;

        [TestInitialize]
        public void Setup()
        {
            _mockGateway = new Mock<ITroChoiGateway>();
            _bus = new BUS_TroChoi(_mockGateway.Object);
        }

        #region Kiểm Thử Toàn Diện (Coverage)
        
        [TestMethod]
        public void LoadDS_GoiDungGateway_TraVeDuLieuTonTai()
        {
            // Arrange
            var list = new List<ET_TroChoi> { new ET_TroChoi { TenTroChoi = "A" } };
            _mockGateway.Setup(g => g.LoadDS()).Returns(list);
            
            // Act
            var result = _bus.LoadDS();
            
            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Them_TenHopLe_TraVeSuccess()
        {
            var et = new ET_TroChoi { TenTroChoi = "Tàu Lượn Siêu Tốc", MoTa = "Đu quay mạo hiểm" };
            _mockGateway.Setup(g => g.Them(It.IsAny<ET_TroChoi>())).Returns(true);

            var result = _bus.Them(et);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(et.MaCode.StartsWith("TC-"));
        }

        [TestMethod]
        public void Them_ThieuTenTroChoi_TraVeFail()
        {
            var et = new ET_TroChoi { TenTroChoi = "", MoTa = "Test lỗi rỗng" };
            var result = _bus.Them(et);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Tên trò chơi không được rỗng!", result.ErrorMessage);
        }
        
        [TestMethod]
        public void Them_LoiGateway_TraVeFail()
        {
            var et = new ET_TroChoi { TenTroChoi = "Tàu Lượn Siêu Tốc" };
            _mockGateway.Setup(g => g.Them(It.IsAny<ET_TroChoi>())).Returns(false); // Gateway trả false

            var result = _bus.Them(et);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Lỗi khi thêm trò chơi vào CSDL.", result.ErrorMessage);
        }

        [TestMethod]
        public void Sua_IdKhongHopLe_TraVeLoi()
        {
            var et = new ET_TroChoi { Id = 0, TenTroChoi = "Nhà Ma" };
            var result = _bus.Sua(et);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("ID không hợp lệ!", result.ErrorMessage);
        }
        
        [TestMethod]
        public void Sua_IdHopLe_TraVeSuccess()
        {
            var et = new ET_TroChoi { Id = 10, TenTroChoi = "Nhà Ma" };
            _mockGateway.Setup(g => g.Sua(It.IsAny<ET_TroChoi>())).Returns(true);
            
            var result = _bus.Sua(et);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Sua_ChayLenDL_TraVeLoiCanhBao()
        {
            var et = new ET_TroChoi { Id = 10, TenTroChoi = "Nhà Ma" };
            _mockGateway.Setup(g => g.Sua(It.IsAny<ET_TroChoi>())).Returns(false);
            
            var result = _bus.Sua(et);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Lỗi khi cập nhật thông tin.", result.ErrorMessage);
        }

        [TestMethod]
        public void Xoa_IdKhongTonTai_PhaiTraVeFail()
        {
            _mockGateway.Setup(g => g.Xoa(999)).Returns(false);
            var result = _bus.Xoa(999);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Lỗi khi xóa trò chơi!", result.ErrorMessage);
        }
        
        [TestMethod]
        public void Xoa_ThanhCong()
        {
            _mockGateway.Setup(g => g.Xoa(1)).Returns(true);
            var result = _bus.Xoa(1);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void TimKiem_TraVeDungChucNang()
        {
            _mockGateway.Setup(g => g.TimKiem("a", "Tất cả")).Returns(new List<ET_TroChoi>());
            var result = _bus.TimKiem("a", "Tất cả");
            Assert.IsNotNull(result);
        }

        #endregion

        #region Kịch bản CỐ TÌNH TẠO LỖI (Failed do code hiện tại thiếu Validate)

        [TestMethod]
        public void Them_TrangThaiKhongHopLe_PhaiTraVeFail()
        {
            var et = new ET_TroChoi { TenTroChoi = "Đu quay", TrangThai = "Tạm ngưng siêu cấp" };
            _mockGateway.Setup(g => g.Them(It.IsAny<ET_TroChoi>())).Returns(true);

            var result = _bus.Them(et);

            // Code hiện hành không chặn Trạng thái lạ -> Lưu thẳng -> Test FAILED MÀU ĐỎ!
            Assert.IsFalse(result.IsSuccess, "Thực tế: Code hiện tại cho phép Lưu mọi loại chuỗi vào Trạng thái (Lỗi Nghiệp vụ)!");
            Assert.AreEqual("Trạng thái không hợp lệ.", result.ErrorMessage);
        }

        [TestMethod]
        public void Them_IdKhuVucAm_PhaiTraVeFail()
        {
            var et = new ET_TroChoi { TenTroChoi = "Nhà Ma", IdKhuVuc = -1 };
            _mockGateway.Setup(g => g.Them(It.IsAny<ET_TroChoi>())).Returns(true);

            var result = _bus.Them(et);

            // Mong đợi hệ thống chặn Khu vực có ID rác âm, nhưng code không có -> Test FAILED MÀU ĐỎ!
            Assert.IsFalse(result.IsSuccess, "Thực tế: Hệ thống chấp nhận lưu IdKhuVuc là số âm gây rác Database!");
            Assert.AreEqual("Khu vực không hợp lệ.", result.ErrorMessage);
        }

        #endregion
    }
}
