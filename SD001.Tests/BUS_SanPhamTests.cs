using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BUS;
using ET;

namespace SD001.Tests
{
    [TestClass]
    public class BUS_SanPhamTests
    {
        private Mock<ISanPhamGateway> _mockGateway;
        private Mock<ISanPhamVeGateway> _mockVeGateway;
        private Mock<IQuyDoiDonViGateway> _mockQuyDoi;
        private BUS_SanPham _bus;

        [TestInitialize]
        public void Setup()
        {
            _mockGateway = new Mock<ISanPhamGateway>();
            _mockVeGateway = new Mock<ISanPhamVeGateway>();
            _mockQuyDoi = new Mock<IQuyDoiDonViGateway>();

            _bus = new BUS_SanPham(_mockGateway.Object, _mockVeGateway.Object, _mockQuyDoi.Object);
        }

        #region Kiểm Thử Toàn Diện (Coverage)
        
        [TestMethod]
        public void LoadDS_LoaiBoSanPhamDaXoa()
        {
            var ds = new List<ET_SanPham> { 
                new ET_SanPham { Id = 1, IsDeleted = false },
                new ET_SanPham { Id = 2, IsDeleted = true }
            };
            _mockGateway.Setup(g => g.LoadDS()).Returns(ds);
            
            var result = _bus.LoadDS();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetById_GoiDungGateway_Success()
        {
            _mockGateway.Setup(g => g.LayTheoId(1)).Returns(new ET_SanPham());
            Assert.IsNotNull(_bus.GetById(1));
        }

        [TestMethod]
        public void GetByMaCode_GoiDungGateway_Success()
        {
            _mockGateway.Setup(g => g.LayTheoMaCode("SP01")).Returns(new ET_SanPham());
            Assert.IsNotNull(_bus.GetByMaCode("SP01"));
        }

        [TestMethod]
        public void Them_SanPhamTenTrong_PhaiTraVeLoi()
        {
            var result = _bus.Them(new ET_SanPham { Ten = "" });
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Tên sản phẩm không được để trống.", result.ErrorMessage);
        }
        
        [TestMethod]
        public void Them_ErrorKhiThemSanPham_PhaiTraVeLoi()
        {
            var et = new ET_SanPham { Ten = "SP 1", DonGia = 1000 };
            _mockGateway.Setup(g => g.ThemVaLayId(et)).Returns(0);
            var result = _bus.Them(et);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Không thể thêm sản phẩm.", result.ErrorMessage);
        }

        [TestMethod]
        public void Them_GhiSanPhamVeThanhCong_TraVeSuccess()
        {
            var et = new ET_SanPham 
            { 
                Ten = "Vé vào cổng", 
                LoaiSanPham = AppConstants.LoaiSanPham.Ve, 
                DonGia = 1000,
                _veInfo = new ET_SanPham_Ve() 
            };
            _mockGateway.Setup(g => g.ThemVaLayId(et)).Returns(10);
            _mockVeGateway.Setup(g => g.ThemHoacCapNhat(It.IsAny<ET_SanPham_Ve>())).Returns(true);

            var result = _bus.Them(et);
            Assert.IsTrue(result.IsSuccess);
        }
        
        [TestMethod]
        public void Them_GhiSanPhamVeLoi_TraVeLoi()
        {
            var et = new ET_SanPham 
            { 
                Ten = "Vé VIP", 
                LoaiSanPham = AppConstants.LoaiSanPham.Ve, 
                DonGia = 1000,
                _veInfo = new ET_SanPham_Ve() 
            };
            _mockGateway.Setup(g => g.ThemVaLayId(et)).Returns(20);
            _mockVeGateway.Setup(g => g.ThemHoacCapNhat(It.IsAny<ET_SanPham_Ve>())).Returns(false);

            var result = _bus.Them(et);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Đã tạo sản phẩm nhưng lỗi khi ghi thông tin vé.", result.ErrorMessage);
        }

        #endregion

        #region Kịch bản CỐ TÌNH TẠO LỖI (Failed Đỏ do Code chưa Validate)
        [TestMethod]
        public void Them_DonGiaAm_PhaiTraVeLoi()
        {
            var et = new ET_SanPham { Ten = "Nước suối", DonGia = -20000 };
            _mockGateway.Setup(g => g.ThemVaLayId(et)).Returns(11);

            var result = _bus.Them(et);

            // Hiện tại code chỉ check Tên rỗng, KHÔNG check Đơn Giá âm -> Test FAILED ĐỎ!
            Assert.IsFalse(result.IsSuccess, "[TDD Expectation] Không cho phép tạo Sản phẩm có Đơn giá <= 0.");
        }
        
        [TestMethod]
        public void Sua_LoaiSanPhamKhongHopLe_PhaiTraVeLoi()
        {
            var et = new ET_SanPham { Id = 1, Ten = "ABC", DonGia = 100, LoaiSanPham = "LOAI_RONG" };
            var result = _bus.Sua(et);

            // Tương tự, Code cũ chỉ Check string.IsNullOrWhitespace chứ ko Check IN ("Đồ ăn", "Nước uống", "Quà lưu niệm")
            Assert.IsFalse(result.IsSuccess, "[TDD Expectation] Loại Sản phẩm phải thuộc danh mục được hệ thống cho phép.");
            Assert.AreEqual("Loại sản phẩm không hợp lệ.", result.ErrorMessage);
        }
        #endregion
    }
}
