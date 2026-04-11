using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BUS;
using ET;

namespace SD001.Tests
{
    [TestClass]
    public class BUS_KhuVucTests
    {
        private Mock<IKhuVucGateway> _mockGateway;
        private Mock<ITroChoiGateway> _mockTroChoiGateway;
        private BUS_KhuVuc _bus;

        [TestInitialize]
        public void Setup()
        {
            _mockGateway = new Mock<IKhuVucGateway>();
            _mockTroChoiGateway = new Mock<ITroChoiGateway>();
            _bus = new BUS_KhuVuc(_mockGateway.Object, _mockTroChoiGateway.Object);
        }

        #region Kịch bản FAILED CỐ TÌNH (Lỗi Source Code hiện hành)
        
        [TestMethod]
        public void Validate_TenKhuVucQuaNgan_PhaiTraVeLoi()
        {
            var et = new ET_KhuVuc { TenKhuVuc = "A", MaCode = "KV01" };
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_KhuVuc>());

            var error = _bus.ValidateKhuVuc(et, isAdd: true);

            Assert.AreNotEqual(string.Empty, error, "[TDD Expectation] Tên Khu vực phải có độ dài tối thiểu để đảm bảo định dạng dữ liệu.");
        }

        [TestMethod]
        public void Validate_MoTaQuaDai_PhaiTraVeLoi()
        {
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_KhuVuc>());
             ET_KhuVuc kv = new ET_KhuVuc
            {
                 TenKhuVuc = "Khu V",
                 MoTa = new string('A', 505) // Mô tả dài 505 ký tự
            };

            var error = _bus.ValidateKhuVuc(kv, isAdd: true);
            
            // Hiện tại hàm chỉ check rỗng, ko check Limit độ dài, sẽ ném SQL Truncate Exception khi insert
            Assert.AreNotEqual(string.Empty, error, "[TDD Expectation] Chiều dài Mô tả không được vượt quá Data Schema (500 ký tự).");
        }

        #endregion

        #region Kiểm Thử Toàn Diện (Coverage)
        
        [TestMethod]
        public void LoadDS_ChungChiLayKhuVucChuaXoaMem()
        {
            var dataList = new List<ET_KhuVuc>
            {
                new ET_KhuVuc { Id = 1, TenKhuVuc = "Khu 1", IsDeleted = false },
                new ET_KhuVuc { Id = 2, TenKhuVuc = "Khu 2", IsDeleted = true }
            };
            _mockGateway.Setup(g => g.LoadDS()).Returns(dataList);

            var ketQua = _bus.LoadDS();

            Assert.AreEqual(1, ketQua.Count);
            Assert.AreEqual("Khu 1", ketQua[0].TenKhuVuc);
        }
        
        [TestMethod]
        public void LayMaCodeTiepTheo_DBTrong_TraVeKV01()
        {
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_KhuVuc>());
            var result = _bus.LayMaCodeTiepTheo();
            Assert.AreEqual("KV01", result);
        }
        
        [TestMethod]
        public void LayMaCodeTiepTheo_CoDuLieu_TraVeMaTangDan()
        {
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_KhuVuc>{
                new ET_KhuVuc { MaCode = "KV05" }, new ET_KhuVuc { MaCode = "KV07" }
            });
            var result = _bus.LayMaCodeTiepTheo();
            Assert.AreEqual("KV08", result);
        }

        [TestMethod]
        public void ThemKhuVuc_TuDongCapMaCode_TraVeSuccess()
        {
            var et = new ET_KhuVuc { TenKhuVuc = "Khu Test", MaCode = string.Empty };
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_KhuVuc>());
            _mockGateway.Setup(g => g.Them(It.IsAny<ET_KhuVuc>())).Returns(true);

            var result = _bus.ThemKhuVuc(et);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("KV01", et.MaCode);
        }

        [TestMethod]
        public void Xoa_KhongTimThayId_TraVeError()
        {
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_KhuVuc>());
            var result = _bus.Xoa(99);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Không tìm thấy khu vực.", result.ErrorMessage);
        }

        [TestMethod]
        public void XoaKhuVuc_DangCoTroChoi_PhaiBiChan()
        {
            var khuvuc = new ET_KhuVuc { Id = 1, MaCode = "KV01" };
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_KhuVuc> { khuvuc });
            
            var games = new List<ET_TroChoi> { new ET_TroChoi { IdKhuVuc = 1 } };
            _mockTroChoiGateway.Setup(g => g.LoadDS()).Returns(games);

            var result = _bus.XoaKhuVuc("KV01");

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Không thể xóa. Khu vực này đang chứa trò chơi trực thuộc.", result.ErrorMessage);
        }
        
        [TestMethod]
        public void XoaKhuVuc_ThanhCong_TraVeSuccess()
        {
            var khuvuc = new ET_KhuVuc { Id = 1, MaCode = "KV01" };
            _mockGateway.Setup(g => g.LoadDS()).Returns(new List<ET_KhuVuc> { khuvuc });
            _mockTroChoiGateway.Setup(g => g.LoadDS()).Returns(new List<ET_TroChoi>());
            _mockGateway.Setup(g => g.Xoa(1)).Returns(true);

            var result = _bus.XoaKhuVuc("KV01");

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void TimKiem_CoVaiTuKhoa_TraVeDungData()
        {
            var dummyDs = new List<ET_KhuVuc>
            {
                new ET_KhuVuc { TenKhuVuc = "Công viên Nước", TrangThai = "Mở cửa" },
                new ET_KhuVuc { TenKhuVuc = "Sân bay", TrangThai = "Đóng cửa" }
            };
            _mockGateway.Setup(g => g.LoadDS()).Returns(dummyDs);

            var list = _bus.TimKiem("nước", "Tất cả");

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Công viên Nước", list[0].TenKhuVuc);
        }

        #endregion
    }
}
