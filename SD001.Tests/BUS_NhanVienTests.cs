using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BUS;
using ET;

namespace SD001.Tests
{
    [TestClass]
    public class BUS_NhanVienTests
    {
        private Mock<INhanVienGateway> _mockGateway;
        private BUS_NhanVien _bus;

        [TestInitialize]
        public void Setup()
        {
            _mockGateway = new Mock<INhanVienGateway>();
            _bus = new BUS_NhanVien(_mockGateway.Object);
        }

        #region Kiểm Thử Toàn Diện (Coverage)
        
        [TestMethod]
        public void LoadDS_TraVeDanhSach()
        {
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_NhanVien> { new ET_NhanVien() });
            Assert.AreEqual(1, _bus.LoadDS().Count);
        }

        [TestMethod]
        public void GetById_TraVeNhanVien()
        {
            _mockGateway.Setup(g => g.LayTheoId(1)).Returns(new ET_NhanVien { Id = 1 });
            Assert.IsNotNull(_bus.GetById(1));
        }

        [TestMethod]
        public void DangNhap_UsernameRong_TraVeNull()
        {
            var ketQua = _bus.DangNhap("", "123456");
            Assert.IsNull(ketQua);
        }

        [TestMethod]
        public void DangNhap_ThongTinHopLe_TraVeKetQua()
        {
            var nv = new ET_NhanVien { TenDangNhap = "admin", HoTen = "Quản trị viên" };
            _mockGateway.Setup(g => g.DangNhap("admin", "123456")).Returns(nv);

            var ketQua = _bus.DangNhap("admin", "123456");
            Assert.IsNotNull(ketQua);
            Assert.AreEqual("Quản trị viên", ketQua.HoTen);
        }

        [TestMethod]
        public void Them_Valid_Success()
        {
            var et = new ET_NhanVien { 
                HoTen = "A", DienThoai = "0123456789", Cccd = "012345678912", NgaySinh = new DateTime(2000, 1, 1) 
            };
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_NhanVien>());
            _mockGateway.Setup(g => g.Them(et)).Returns(true);

            Assert.IsTrue(_bus.Them(et).IsSuccess);
        }

        [TestMethod]
        public void Them_LoiValidate_Fail()
        {
            var result = _bus.Them(new ET_NhanVien());
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Sua_Valid_Success()
        {
            var et = new ET_NhanVien { 
                Id = 1, HoTen = "A", DienThoai = "0123456789", Cccd = "012345678912", NgaySinh = new DateTime(2000, 1, 1) 
            };
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_NhanVien>());
            _mockGateway.Setup(g => g.Sua(et)).Returns(true);

            Assert.IsTrue(_bus.Sua(et).IsSuccess);
        }

        [TestMethod]
        public void Xoa_QuyenGoiToiGateway()
        {
            _mockGateway.Setup(g => g.Xoa(1)).Returns(true);
            Assert.IsTrue(_bus.Xoa(1).IsSuccess);
        }

        [TestMethod]
        public void LayMaCodeTiepTheo_DSTrong_TraVeNV001()
        {
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_NhanVien>());
            Assert.AreEqual("NV001", _bus.LayMaCodeTiepTheo());
        }

        [TestMethod]
        public void LayMaCodeTiepTheo_CoDuoi_TraVeNV003()
        {
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_NhanVien> { new ET_NhanVien { MaCode = "NV002"} });
            Assert.AreEqual("NV003", _bus.LayMaCodeTiepTheo());
        }

        [TestMethod]
        public void TimKiem_CoVaiTuKhoa_TraVeDungData()
        {
            var ds = new List<ET_NhanVien>
            {
                new ET_NhanVien { HoTen = "Tấn Nhị", DienThoai = "0123" }
            };
            _mockGateway.Setup(g => g.LoadDS()).Returns(ds);

            var list = _bus.TimKiem("nhị", "Tất cả");

            Assert.AreEqual(1, list.Count);
        }
        #endregion

        #region Kịch bản FAILED CỐ TÌNH (Bắt lỗi Logic Tính Tuổi)
        [TestMethod]
        public void Validate_NhanVienKhongCoNgaySinh_PhaiTraVeLoi18Tuoi()
        {
            var et = new ET_NhanVien { 
                HoTen = "Nguyễn A", DienThoai = "0901234567", Cccd = "012345678912" 
            };
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_NhanVien>());

            var error = _bus.ValidateNhanVien(et, true);

            // Nếu NgaySinh = null, Year = 0. Khi đó 2026 - 0 = 2026 tuổi (Lớn hơn 18). 
            Assert.AreNotEqual(string.Empty, error, "[TDD Expectation] Hệ thống phải phát hiện trường hợp Null/Empty đối với Ngày sinh và yêu cầu nhập liệu hợp lệ.");
            Assert.AreEqual("Vui lòng nhập ngày sinh.", error);
        }

        [TestMethod]
        public void Validate_NhanVien17Tuoi_PhaiTraVeLoi()
        {
            var nv = new ET_NhanVien { HoTen = "Test C", NgaySinh = DateTime.Now.AddYears(-17), DienThoai = "0123456789" };
            var result = _bus.Sua(nv);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Nhân viên phải từ 18 tuổi trở lên.", result.ErrorMessage, "[TDD Expectation] Phải validate độ tuổi >= 18 ngay cả khi thiếu dữ liệu CCCD.");
        }
        #endregion
    }
}
