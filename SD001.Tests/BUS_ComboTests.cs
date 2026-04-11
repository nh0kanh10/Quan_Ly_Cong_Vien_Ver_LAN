using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BUS;
using ET;

namespace SD001.Tests
{
    [TestClass]
    public class BUS_ComboTests
    {
        private Mock<IComboGateway> _mockGateway;
        private Mock<IComboChiTietGateway> _mockChiTietGateway;
        private BUS_Combo _bus;

        [TestInitialize]
        public void Setup()
        {
            _mockGateway = new Mock<IComboGateway>();
            _mockChiTietGateway = new Mock<IComboChiTietGateway>();
            _bus = new BUS_Combo(_mockGateway.Object, _mockChiTietGateway.Object);
        }

        #region Kịch bản ĐÚNG NGHIỆP VỤ (Pass)
        
        [TestMethod]
        public void LoadDS_LoaiBoCacComboDaXoa()
        {
            var ds = new List<ET_Combo>
            {
                new ET_Combo { Id = 1, IsDeleted = false },
                new ET_Combo { Id = 2, IsDeleted = true }
            };
            _mockGateway.Setup(g => g.LoadDS()).Returns(ds);
            
            var result = _bus.LoadDS();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Id);
        }
        
        [TestMethod]
        public void GetById_GoiRaDungID()
        {
            var cbo = new ET_Combo { Id = 10 };
            _mockGateway.Setup(g => g.LayTheoId(10)).Returns(cbo);
            var result = _bus.GetById(10);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetByMaCode_KhongTuKhoa_TraVeNull()
        {
            var result = _bus.GetByMaCode("");
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void GetByMaCode_DungCo_TraVeKetQua()
        {
            var ds = new List<ET_Combo> { new ET_Combo { MaCode = "CB01", IsDeleted = false } };
            _mockGateway.Setup(g => g.LoadDS()).Returns(ds);
            var result = _bus.GetByMaCode("CB01");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Them_NullTen_Loi()
        {
            var result = _bus.Them(new ET_Combo { Ten = "" });
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Them_ThanhCong_GoiGateway()
        {
            _mockGateway.Setup(g => g.Them(It.IsAny<ET_Combo>())).Returns(true);
            var result = _bus.Them(new ET_Combo { Ten = "Test" });
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Them_GatewayLoi_TraVeFalse()
        {
            _mockGateway.Setup(g => g.Them(It.IsAny<ET_Combo>())).Returns(false);
            var result = _bus.Them(new ET_Combo { Ten = "Test" });
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Sua_GatewayTrue_Success()
        {
            _mockGateway.Setup(g => g.Sua(It.IsAny<ET_Combo>())).Returns(true);
            var result = _bus.Sua(new ET_Combo());
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Xoa_GatewayTrue_Success()
        {
            _mockGateway.Setup(g => g.Xoa(1)).Returns(true);
            var result = _bus.Xoa(1);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void TimKiem_TraVeDuLieu()
        {
            var combo = new ET_Combo { Ten = "combo VIP", MaCode = "CBVIP", TrangThai = "A", IsDeleted = false };
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_Combo> { combo });

            var result = _bus.TimKiem("vip", "A");
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void ThemChiTiet_VuotTyLe_BaoLoi()
        {
            var items = new List<ET_ComboChiTiet> { new ET_ComboChiTiet { IdCombo = 1, TyLePhanBo = 60 } };
            _mockChiTietGateway.Setup(g => g.LoadDS()).Returns(items);
            
            var result = _bus.ThemChiTiet(1, 1, 1, 50);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Tổng tỷ lệ phân bổ không được vượt quá 100%.", result.ErrorMessage, "[TDD Expectation] Các chi tiết trong Combo không được phép có tổng tỷ lệ lớn hơn 100%.");
        }
        
        [TestMethod]
        public void XoaChiTiet_TrVeSuccess()
        {
            _mockChiTietGateway.Setup(g => g.Xoa(1)).Returns(true);
            Assert.IsTrue(_bus.XoaChiTiet(1).IsSuccess);
        }

        [TestMethod]
        public void LuuChiTiet_TongTyLe100_TraVeSuccess()
        {
            var items = new List<ET_ComboChiTiet>
            {
                new ET_ComboChiTiet { IdSanPham = 1, SoLuong = 1, TyLePhanBo = 60 },
                new ET_ComboChiTiet { IdSanPham = 2, SoLuong = 1, TyLePhanBo = 40 }
            };
            _mockChiTietGateway.Setup(g => g.XoaTheoCombo(1)).Returns(true);
            _mockChiTietGateway.Setup(g => g.Them(It.IsAny<ET_ComboChiTiet>())).Returns(true);

            var result = _bus.LuuChiTiet(1, items);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void LuuChiTiet_Rong_TrVeLoi()
        {
            var result = _bus.LuuChiTiet(1, new List<ET_ComboChiTiet>());
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Rổ combo đang trống. Vui lòng thêm ít nhất 1 sản phẩm.", result.ErrorMessage);
        }

        [TestMethod]
        public void LuuChiTiet_SoLuongAm_TrVeLoi()
        {
            var items = new List<ET_ComboChiTiet> { new ET_ComboChiTiet { SoLuong = -1, TyLePhanBo = 100 } };
            var result = _bus.LuuChiTiet(1, items);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Số lượng phải > 0 cho mỗi sản phẩm.", result.ErrorMessage);
        }

        [TestMethod]
        public void LuuChiTiet_TyLePhanBoAm_TrVeLoi()
        {
            var items = new List<ET_ComboChiTiet> { new ET_ComboChiTiet { SoLuong = 1, TyLePhanBo = -5 } };
            var result = _bus.LuuChiTiet(1, items);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Tỷ lệ phân bổ phải > 0% cho mỗi sản phẩm.", result.ErrorMessage);
        }

        [TestMethod]
        public void LuuChiTiet_XoaCuLoi_TrVeFalse()
        {
            var items = new List<ET_ComboChiTiet> { new ET_ComboChiTiet { SoLuong = 1, TyLePhanBo = 100 } };
            _mockChiTietGateway.Setup(g => g.XoaTheoCombo(1)).Returns(false);
            
            var result = _bus.LuuChiTiet(1, items);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Lỗi khi xóa chi tiết cũ.", result.ErrorMessage);
        }

        #endregion

        #region Kịch bản LỖI NGHIỆP VỤ TRONG SOURCE HIỆN TẠI (Failed Đỏ)
        [TestMethod]
        public void ThemChiTiet_SoLuongAm_PhaiTraVeLoi()
        {
            _mockChiTietGateway.Setup(g => g.LoadDS()).Returns(new List<ET_ComboChiTiet>());
            _mockChiTietGateway.Setup(g => g.Them(It.IsAny<ET_ComboChiTiet>())).Returns(true);

            // Gọi hàm thêm chi tiết lẻ nhưng chèn Số lượng = -5
            var result = _bus.ThemChiTiet(idCombo: 1, idSanPham: 2, soLuong: -5, tyLePhanBo: 20);

            // Trong hàm ThemChiTiet hiện tại CHỈ Check Tổng tỉ lệ > 100, hoàn toàn KHÔNG check Số lượng < 0
            Assert.IsFalse(result.IsSuccess, "[TDD Expectation] Bắt buộc ném lỗi khi Số lượng linh kiện chèn vào Combo <= 0.");
            Assert.AreEqual("Số lượng phải lớn hơn 0.", result.ErrorMessage);
        }
        #endregion
    }
}
