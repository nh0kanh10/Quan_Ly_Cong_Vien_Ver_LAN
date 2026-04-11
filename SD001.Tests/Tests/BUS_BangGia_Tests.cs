using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ET;
using BUS;

namespace SD001.Tests.Tests
{
    [TestClass]
    public class BUS_BangGia_Tests
    {
        private Mock<IBangGiaGateway> _mockBangGiaGateway;
        private Mock<ISanPhamGateway> _mockSanPhamGateway;
        private Mock<ICauHinhNgayLeGateway> _mockNgayLeGateway;
        private Mock<IQuyDoiDonViGateway> _mockQuyDoiGateway;
        private BUS_BangGia _busBangGia;

        [TestInitialize]
        public void Setup()
        {
            _mockBangGiaGateway = new Mock<IBangGiaGateway>();
            _mockSanPhamGateway = new Mock<ISanPhamGateway>();
            _mockNgayLeGateway = new Mock<ICauHinhNgayLeGateway>();
            _mockQuyDoiGateway = new Mock<IQuyDoiDonViGateway>();

            _busBangGia = new BUS_BangGia(
                _mockBangGiaGateway.Object,
                _mockSanPhamGateway.Object,
                _mockNgayLeGateway.Object,
                _mockQuyDoiGateway.Object
            );
        }

        [TestMethod]
        public void GetDynamicPrice_NgayThuong_ReturnsMacDinh()
        {
            // Arrange
            int spId = 1;
            DateTime thoiDiem = new DateTime(2023, 10, 18); // Wednesday
            
            _mockBangGiaGateway.Setup(g => g.LayGiaHienTai(spId, It.IsAny<TimeSpan>())).Returns(new List<ET_BangGia>
            {
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.MacDinh, GiaBan = 50000 },
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.CuoiTuan, GiaBan = 80000 },
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.NgayLe, GiaBan = 100000 }
            });

            // Act
            var gia = _busBangGia.GetDynamicPrice(spId, thoiDiem);

            // Assert
            Assert.AreEqual(50000, gia);
        }

        [TestMethod]
        public void GetDynamicPrice_CuoiTuan_ReturnsCuoiTuan()
        {
            // Arrange
            int spId = 1;
            DateTime thoiDiem = new DateTime(2023, 10, 21); // Saturday
            
            _mockBangGiaGateway.Setup(g => g.LayGiaHienTai(spId, It.IsAny<TimeSpan>())).Returns(new List<ET_BangGia>
            {
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.MacDinh, GiaBan = 50000 },
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.CuoiTuan, GiaBan = 80000 },
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.NgayLe, GiaBan = 100000 }
            });

            // Act
            var gia = _busBangGia.GetDynamicPrice(spId, thoiDiem);

            // Assert
            Assert.AreEqual(80000, gia);
        }

        [TestMethod]
        public void GetDynamicPrice_NgayLe_ReturnsNgayLe()
        {
            // Arrange
            int spId = 1;
            DateTime thoiDiem = new DateTime(2023, 9, 2); // Holiday test match
            
            _mockBangGiaGateway.Setup(g => g.LayGiaHienTai(spId, It.IsAny<TimeSpan>())).Returns(new List<ET_BangGia>
            {
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.MacDinh, GiaBan = 50000 },
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.CuoiTuan, GiaBan = 80000 },
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.NgayLe, GiaBan = 100000 }
            });
            
            _mockNgayLeGateway.Setup(g => g.LayNgayLeChoNgay(It.IsAny<DateTime>())).Returns(new ET_CauHinhNgayLe { Id = 1, TenNgayLe = "Quốc khánh" });

            // Act
            var gia = _busBangGia.GetDynamicPrice(spId, thoiDiem);

            // Assert
            Assert.AreEqual(100000, gia);
        }

        [TestMethod]
        public void GetDynamicPrice_NoBangGia_FallsBackToSanPhamDonGia()
        {
            // Arrange
            int spId = 1;
            DateTime thoiDiem = new DateTime(2023, 10, 18);
            
            // Database returns no BangGia
            _mockBangGiaGateway.Setup(g => g.LayGiaHienTai(spId, It.IsAny<TimeSpan>())).Returns(new List<ET_BangGia>());
            // It should fall back to SanPham base price
            _mockSanPhamGateway.Setup(g => g.LayTheoId(spId)).Returns(new ET_SanPham { DonGia = 40000 });

            // Act
            var gia = _busBangGia.GetDynamicPrice(spId, thoiDiem);

            // Assert
            Assert.AreEqual(40000, gia);
        }

        [TestMethod]
        public void ThemGia_GiaBanAm_ReturnsError()
        {
            // Arrange
            var bg = new ET_BangGia { GiaBan = -1000 };

            // Act
            var result = _busBangGia.ThemGia(bg);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Giá tiền không được âm.", result.ErrorMessage);
        }

        [TestMethod]
        public void ThemGia_DuplicateActive_ReturnsConstraintError()
        {
            // Arrange
            var bg = new ET_BangGia { GiaBan = 50000 };
            
            _mockBangGiaGateway.Setup(g => g.Them(bg)).Throws(new Exception("Cannot insert duplicate key... UxBangGia_ActiveSPGio violated"));

            // Act
            var result = _busBangGia.ThemGia(bg);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Đã tồn tại cấu hình giá Đụng Hàng cho sản phẩm này (trùng điều kiện hoặc khung giờ).", result.ErrorMessage);
        }

        [TestMethod]
        public void ThemGia_Valid_ReturnsSuccess()
        {
            // Arrange
            var bg = new ET_BangGia { GiaBan = 50000 };
            
            _mockBangGiaGateway.Setup(g => g.Them(bg)).Returns(true);

            // Act
            var result = _busBangGia.ThemGia(bg);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void GetTienCoc_HasTienCoc_ReturnsCorrectAmount()
        {
            // Arrange
            int spId = 2;
            _mockBangGiaGateway.Setup(g => g.LayTheoSanPham(spId)).Returns(new List<ET_BangGia>
            {
                new ET_BangGia { GiaBan = 50000, TienCoc = 20000, TrangThai = AppConstants.TrangThaiChung.HoatDong } 
            });

            // Act
            var result = _busBangGia.GetTienCoc(spId);

            // Assert
            Assert.AreEqual(20000, result);
        }

        [TestMethod]
        public void TinhTienThueTheoPhut_TrongBlockDau_ReturnsGiaGoc()
        {
            // Arrange
            int spId = 3;
            DateTime thoiDiem = new DateTime(2023, 10, 18);
            _mockBangGiaGateway.Setup(g => g.LayGiaHienTai(spId, It.IsAny<TimeSpan>())).Returns(new List<ET_BangGia>
            {
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.MacDinh, GiaBan = 50000, PhutBlock = 60, PhutTiep = 30, GiaPhuThu = 20000 }
            });

            // Act
            var gia = _busBangGia.TinhTienThueTheoPhut(spId, thoiDiem, 45); // Under 60 mins

            // Assert
            Assert.AreEqual(50000, gia); // Only base price
        }

        [TestMethod]
        public void TinhTienThueTheoPhut_LoGioMotBlock_ReturnsGiaGocCong1PhuThu()
        {
            // Arrange
            int spId = 3;
            DateTime thoiDiem = new DateTime(2023, 10, 18);
            _mockBangGiaGateway.Setup(g => g.LayGiaHienTai(spId, It.IsAny<TimeSpan>())).Returns(new List<ET_BangGia>
            {
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.MacDinh, GiaBan = 50000, PhutBlock = 60, PhutTiep = 30, GiaPhuThu = 20000 }
            });

            // Act
            var gia = _busBangGia.TinhTienThueTheoPhut(spId, thoiDiem, 70); // 70 mins = 1 block base + 10 mins over (requires 1 extra block)

            // Assert
            Assert.AreEqual(70000, gia); // 50000 + 20000
        }

        [TestMethod]
        public void TinhTienThueTheoPhut_LoGioHaiBlock_ReturnsGiaGocCong2PhuThu()
        {
            // Arrange
            int spId = 3;
            DateTime thoiDiem = new DateTime(2023, 10, 18);
            _mockBangGiaGateway.Setup(g => g.LayGiaHienTai(spId, It.IsAny<TimeSpan>())).Returns(new List<ET_BangGia>
            {
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.MacDinh, GiaBan = 50000, PhutBlock = 60, PhutTiep = 30, GiaPhuThu = 20000 }
            });

            // Act
            var gia = _busBangGia.TinhTienThueTheoPhut(spId, thoiDiem, 100); // 100 mins = 60 base + 40 mins over (requires 2 extra blocks of 30 mins)

            // Assert
            Assert.AreEqual(90000, gia); // 50000 + (2 * 20000)
        }

        [TestMethod]
        public void GetPriceByUnit_KhongCoQuyDoi_ReturnsGiaGoc()
        {
            // Arrange
            int spId = 1;
            int dvtChon = 99;
            DateTime thoiDiem = new DateTime(2023, 10, 18);
            _mockQuyDoiGateway.Setup(g => g.LoadDS()).Returns(new List<ET_QuyDoiDonVi>()); // Empty mappings
            _mockBangGiaGateway.Setup(g => g.LayGiaHienTai(spId, It.IsAny<TimeSpan>())).Returns(new List<ET_BangGia>
            {
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.MacDinh, GiaBan = 30000 }
            });

            // Act
            var gia = _busBangGia.GetPriceByUnit(spId, dvtChon, thoiDiem);

            // Assert
            Assert.AreEqual(30000, gia);
        }

        [TestMethod]
        public void GetPriceByUnit_CoGiaBanRieng_ReturnsGiaBanRieng()
        {
            // Arrange
            int spId = 1;
            int dvtChon = 2; // e.g. "Thùng"
            DateTime thoiDiem = new DateTime(2023, 10, 18);
            _mockQuyDoiGateway.Setup(g => g.LoadDS()).Returns(new List<ET_QuyDoiDonVi>
            {
                new ET_QuyDoiDonVi { IdSanPham = 1, IdDonViLon = 2, TyLeQuyDoi = 24, GiaBanRieng = 500000 }
            });

            // Act
            var gia = _busBangGia.GetPriceByUnit(spId, dvtChon, thoiDiem);

            // Assert
            Assert.AreEqual(500000, gia);
        }

        [TestMethod]
        public void GetPriceByUnit_KhongCoGiaBanRieng_ReturnsTyLeNhanGiaGoc()
        {
            // Arrange
            int spId = 1;
            int dvtChon = 2; 
            DateTime thoiDiem = new DateTime(2023, 10, 18);
            _mockQuyDoiGateway.Setup(g => g.LoadDS()).Returns(new List<ET_QuyDoiDonVi>
            {
                new ET_QuyDoiDonVi { IdSanPham = 1, IdDonViLon = 2, TyLeQuyDoi = 10, GiaBanRieng = 0 }
            });
            _mockBangGiaGateway.Setup(g => g.LayGiaHienTai(spId, It.IsAny<TimeSpan>())).Returns(new List<ET_BangGia>
            {
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.MacDinh, GiaBan = 30000 }
            });

            // Act
            var gia = _busBangGia.GetPriceByUnit(spId, dvtChon, thoiDiem);

            // Assert
            Assert.AreEqual(300000, gia); 
        }

        [TestMethod]
        public void SuaGia_Valid_ReturnsSuccess()
        {
            // Arrange
            var bg = new ET_BangGia { Id = 1, GiaBan = 60000 };
            _mockBangGiaGateway.Setup(g => g.Sua(bg)).Returns(true);

            // Act
            var result = _busBangGia.SuaGia(bg);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void XoaGia_Valid_ReturnsSuccess()
        {
            // Arrange
            _mockBangGiaGateway.Setup(g => g.Xoa(1)).Returns(true);

            // Act
            var result = _busBangGia.XoaGia(1);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }
        [TestMethod]
        public void GetDynamicPrice_NgayLeTrungCuoiTuan_UuTienNgayLe()
        {
            // Arrange
            int spId = 1;
            DateTime thoiDiem = new DateTime(2023, 9, 2);
            
            _mockBangGiaGateway.Setup(g => g.LayGiaHienTai(spId, It.IsAny<TimeSpan>())).Returns(new List<ET_BangGia>
            {
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.MacDinh, GiaBan = 50000 },
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.CuoiTuan, GiaBan = 80000 },
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.NgayLe, GiaBan = 100000 }
            });
            
            _mockNgayLeGateway.Setup(g => g.LayNgayLeChoNgay(It.IsAny<DateTime>())).Returns(new ET_CauHinhNgayLe { Id = 1, TenNgayLe = "Quốc khánh" });

            // Act
            var gia = _busBangGia.GetDynamicPrice(spId, thoiDiem);

            // Assert
            Assert.AreEqual(100000, gia); // Ưu tiên Ngày Lễ trước, dù là Cuối Tuần
        }

        [TestMethod]
        public void TinhTienThueTheoPhut_PhutTiepBangKhong_KhongBiCrash()
        {
            // Arrange
            int spId = 3;
            DateTime thoiDiem = new DateTime(2023, 10, 18);
            _mockBangGiaGateway.Setup(g => g.LayGiaHienTai(spId, It.IsAny<TimeSpan>())).Returns(new List<ET_BangGia>
            {
                new ET_BangGia { LoaiGiaApDung = AppConstants.LoaiGiaApDung.MacDinh, GiaBan = 50000, PhutBlock = 60, PhutTiep = 0, GiaPhuThu = 20000 }
            });

            // Act
            var gia = _busBangGia.TinhTienThueTheoPhut(spId, thoiDiem, 100);

            // Assert
            Assert.AreEqual(90000, gia); // 50000 + 2*20000 = 90000
        }

        [TestMethod]
        public void SuaGia_GiaBanAm_ReturnsError()
        {
            // Arrange
            var bg = new ET_BangGia { Id = 1, GiaBan = -50000 };

            // Act
            var result = _busBangGia.SuaGia(bg);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Giá tiền không được âm.", result.ErrorMessage);
        }

        [TestMethod]
        public void XoaGia_DatabaseLoi_ReturnsFailed()
        {
            // Arrange
            _mockBangGiaGateway.Setup(g => g.Xoa(1)).Throws(new Exception("Mất kết nối SQL"));

            // Act
            var result = _busBangGia.XoaGia(1);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Mất kết nối SQL"));
        }

        [TestMethod]
        public void SuaGia_DatabaseLoi_ReturnsFailed()
        {
            // Arrange
            var bg = new ET_BangGia { Id = 1, GiaBan = 60000 };
            _mockBangGiaGateway.Setup(g => g.Sua(bg)).Throws(new Exception("Deadlock Timeout SQL"));

            // Act
            var result = _busBangGia.SuaGia(bg);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Deadlock Timeout SQL"));
        }

        [TestMethod]
        public void GetTienCoc_KhongCoBangGia_ReturnsZero()
        {
            // Arrange
            _mockBangGiaGateway.Setup(g => g.LayTheoSanPham(99)).Returns((List<ET_BangGia>)null); // DB might return null or empty list depending on DAL

            // Act
            var result = _busBangGia.GetTienCoc(99);

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}
