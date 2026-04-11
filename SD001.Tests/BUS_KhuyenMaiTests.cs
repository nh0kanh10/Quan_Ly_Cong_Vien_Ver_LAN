using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using ET;
using BUS;
using System.Linq;

namespace SD001.Tests
{
    [TestClass]
    public class BUS_KhuyenMaiTests
    {
        private Mock<IKhuyenMaiGateway> _mockGw;
        private BUS_KhuyenMai _bus;

        [TestInitialize]
        public void Setup()
        {
            _mockGw = new Mock<IKhuyenMaiGateway>();
            _bus = new BUS_KhuyenMai(_mockGw.Object);
        }

        [TestMethod]
        public void LoadDS_ReturnsOnlyNotDeletedAndActive()
        {
            // Arrange
            _mockGw.Setup(g => g.LoadDS()).Returns(new List<ET_KhuyenMai>
            {
                new ET_KhuyenMai { Id = 1, IsDeleted = false },
                new ET_KhuyenMai { Id = 2, IsDeleted = true }
            });

            // Act
            var res = _bus.LoadDS();

            // Assert
            Assert.AreEqual(1, res.Count);
            Assert.AreEqual(1, res[0].Id);
        }

        [TestMethod]
        public void ThemKhuyenMai_SetDefaultMaCode_WhenEmpty()
        {
            // Arrange
            var et = new ET_KhuyenMai { MaCode = "", TenKhuyenMai = "KM Tân Thủ" };
            _mockGw.Setup(g => g.Them(It.IsAny<ET_KhuyenMai>())).Returns(true);

            // Act
            var res = _bus.ThemKhuyenMai(et);

            // Assert
            Assert.IsTrue(res.IsSuccess);
            Assert.AreEqual("KM Tân Thủ", et.MaCode);
            Assert.AreNotEqual(default(DateTime), et.CreatedAt);
        }

        [TestMethod]
        public void ThemKhuyenMai_FailAtGateway_ReturnsErrorResult()
        {
            var et = new ET_KhuyenMai { MaCode = "KM123", TenKhuyenMai = "Test" };
            _mockGw.Setup(g => g.Them(It.IsAny<ET_KhuyenMai>())).Returns(false);

            var res = _bus.ThemKhuyenMai(et);

            Assert.IsFalse(res.IsSuccess);
            Assert.AreEqual("Không thể thêm khuyến mãi.", res.ErrorMessage);
        }

        [TestMethod]
        public void SuaKhuyenMai_GatewayReturnsFalse_ReturnsError()
        {
            // Arrange
            var et = new ET_KhuyenMai { Id = 1 };
            _mockGw.Setup(g => g.Sua(It.IsAny<ET_KhuyenMai>())).Returns(false);

            // Act
            var res = _bus.SuaKhuyenMai(et);

            // Assert
            Assert.IsFalse(res.IsSuccess);
            Assert.AreEqual("Không thể cập nhật khuyến mãi.", res.ErrorMessage);
        }

        [TestMethod]
        public void KiemTraKhuyenMai_ValidCondition_ReturnsKhuyenMai()
        {
            // Arrange
            var now = DateTime.Now;
            _mockGw.Setup(g => g.LoadDS()).Returns(new List<ET_KhuyenMai>
            {
                new ET_KhuyenMai 
                { 
                    Id = 1, MaCode = "KM1", 
                    TrangThai = true, IsDeleted = false,
                    NgayBatDau = now.AddDays(-1), NgayKetThuc = now.AddDays(1),
                    DonToiThieu = 500
                }
            });

            // Act
            var km = _bus.KiemTraKhuyenMai("KM1", 600);

            // Assert
            Assert.IsNotNull(km);
            Assert.AreEqual("KM1", km.MaCode);
        }

        [TestMethod]
        public void KiemTraKhuyenMai_Expired_ReturnsNull()
        {
            // Arrange
            var now = DateTime.Now;
            _mockGw.Setup(g => g.LoadDS()).Returns(new List<ET_KhuyenMai>
            {
                new ET_KhuyenMai 
                { 
                    Id = 1, MaCode = "KM1", 
                    TrangThai = true, IsDeleted = false,
                    NgayBatDau = now.AddDays(-5), NgayKetThuc = now.AddDays(-1) // Expired
                }
            });

            // Act
            var km = _bus.KiemTraKhuyenMai("KM1", 600);

            // Assert
            Assert.IsNull(km);
        }

        [TestMethod]
        public void KiemTraKhuyenMai_UnderMinimumAmount_ReturnsNull()
        {
            // Arrange
            var now = DateTime.Now;
            _mockGw.Setup(g => g.LoadDS()).Returns(new List<ET_KhuyenMai>
            {
                new ET_KhuyenMai 
                { 
                    Id = 1, MaCode = "KM1", 
                    TrangThai = true, IsDeleted = false,
                    NgayBatDau = now.AddDays(-1), NgayKetThuc = now.AddDays(1),
                    DonToiThieu = 1000
                }
            });

            // Act
            var km = _bus.KiemTraKhuyenMai("KM1", 800); // 800 < 1000

            // Assert
            Assert.IsNull(km);
        }

        [TestMethod]
        public void GetBestActivePromotion_FiltersAndOrdersCorrectly()
        {
            // Arrange
            var now = DateTime.Now;
            var list = new List<ET_KhuyenMai>
            {
                new ET_KhuyenMai { Id = 1, GiaTriGiam = 10, NgayBatDau = now.AddDays(-1), NgayKetThuc = now.AddDays(1), TrangThai = true, IsDeleted = false, DonToiThieu = 500 },
                new ET_KhuyenMai { Id = 2, GiaTriGiam = 20, NgayBatDau = now.AddDays(-1), NgayKetThuc = now.AddDays(1), TrangThai = true, IsDeleted = false, DonToiThieu = 1000 },
                new ET_KhuyenMai { Id = 3, GiaTriGiam = 30, NgayBatDau = now.AddDays(1),  NgayKetThuc = now.AddDays(2), TrangThai = true, IsDeleted = false, DonToiThieu = 100 } // Not started yet
            };
            _mockGw.Setup(g => g.LoadDS()).Returns(list);

            // Act 1: 800 is enough for Id=1 but not Id=2
            var best1 = _bus.GetBestActivePromotion(800);
            Assert.IsNotNull(best1);
            Assert.AreEqual(1, best1.Id);

            // Act 2: 1200 is enough for both Id=1 and Id=2, it should pick Id=2 (higher discount)
            var best2 = _bus.GetBestActivePromotion(1200);
            Assert.IsNotNull(best2);
            Assert.AreEqual(2, best2.Id);
        }
    }
}
