using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BUS;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SD001.Tests
{
    /// <summary>
    /// Unit Tests cho BUS_KhachHang — Module Khách hàng
    /// Sử dụng Moq để mock toàn bộ Gateway (DAL) dependencies.
    /// Mỗi test case hoàn toàn độc lập, không cần DB thật.
    /// </summary>
    [TestClass]
    public class BUS_KhachHangTests
    {
        private Mock<IKhachHangGateway> _mockKH;
        private Mock<IDonHangGateway> _mockDonHang;
        private Mock<IViDienTuGateway> _mockVi;
        private Mock<ITheRfidGateway> _mockTheRfid;
        private Mock<ILichSuDiemGateway> _mockLichSuDiem;
        private Mock<ISuCoGateway> _mockSuCo;
        private BUS_KhachHang _bus;

        [TestInitialize]
        public void Setup()
        {
            _mockKH = new Mock<IKhachHangGateway>();
            _mockDonHang = new Mock<IDonHangGateway>();
            _mockVi = new Mock<IViDienTuGateway>();
            _mockTheRfid = new Mock<ITheRfidGateway>();
            _mockLichSuDiem = new Mock<ILichSuDiemGateway>();
            _mockSuCo = new Mock<ISuCoGateway>();

            _bus = new BUS_KhachHang(
                _mockKH.Object,
                _mockDonHang.Object,
                _mockVi.Object,
                _mockTheRfid.Object,
                _mockLichSuDiem.Object,
                _mockSuCo.Object
            );
        }

        #region CRUD Tests

        [TestMethod]
        [TestCategory("CRUD")]
        public void LoadDS_LoaiBoDaXoa_ChiTraVeKhachHangChuaBiXoa()
        {
            // Arrange
            _mockKH.Setup(x => x.LoadDS()).Returns(new List<ET_KhachHang>
            {
                new ET_KhachHang { Id = 1, HoTen = "Nguyên", IsDeleted = false },
                new ET_KhachHang { Id = 2, HoTen = "Tấn", IsDeleted = true },
                new ET_KhachHang { Id = 3, HoTen = "Nhi", IsDeleted = false }
            });

            // Act
            var result = _bus.LoadDS();

            // Assert — chỉ trả về 2 record chưa bị soft-delete
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(x => !x.IsDeleted));
        }

        [TestMethod]
        [TestCategory("CRUD")]
        public void Them_ThanhCong_TraVeSuccess()
        {
            // Arrange
            _mockKH.Setup(x => x.Them(It.IsAny<ET_KhachHang>())).Returns(true);
            _mockKH.Setup(x => x.LoadDS()).Returns(new List<ET_KhachHang>());

            var kh = new ET_KhachHang { HoTen = "Test User", DienThoai = "0901234567" };

            // Act
            var result = _bus.Them(kh);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(kh.MaCode, "MaCode phải được tự sinh khi thêm mới");
            Assert.IsNotNull(kh.CreatedAt);
        }

        [TestMethod]
        [TestCategory("CRUD")]
        public void Them_ThatBai_TraVeError()
        {
            // Arrange — DB trả về false (lỗi insert)
            _mockKH.Setup(x => x.Them(It.IsAny<ET_KhachHang>())).Returns(false);
            _mockKH.Setup(x => x.LoadDS()).Returns(new List<ET_KhachHang>());

            // Act
            var result = _bus.Them(new ET_KhachHang { HoTen = "Test" });

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        [TestCategory("CRUD")]
        public void LayMaCodeTiepTheo_CoKhachHangCu_TangMa()
        {
            // Arrange — DB hiện có KH00001, KH00002
            _mockKH.Setup(x => x.LoadDS()).Returns(new List<ET_KhachHang>
            {
                new ET_KhachHang { Id = 1, MaCode = "KH00001" },
                new ET_KhachHang { Id = 2, MaCode = "KH00002" }
            });

            // Act
            var newCode = _bus.LayMaCodeTiepTheo();

            // Assert — phải = KH00003
            Assert.AreEqual("KH00003", newCode);
        }

        [TestMethod]
        [TestCategory("CRUD")]
        public void LayMaCodeTiepTheo_DanhSachTrong_TraVeKH00001()
        {
            // Arrange
            _mockKH.Setup(x => x.LoadDS()).Returns(new List<ET_KhachHang>());

            // Act
            var newCode = _bus.LayMaCodeTiepTheo();

            // Assert
            Assert.AreEqual("KH00001", newCode);
        }

        #endregion

        #region Validation Tests

        [TestMethod]
        [TestCategory("Validation")]
        public void Validate_HoTenTrong_TraVeLoiHoTen()
        {
            // Arrange
            var kh = new ET_KhachHang { HoTen = "", DienThoai = "0901234567" };
            _mockKH.Setup(x => x.GetAll()).Returns(new List<ET_KhachHang>());

            // Act
            var error = _bus.ValidateKhachHang(kh, true);

            // Assert
            Assert.AreEqual("Họ tên không được để trống.", error);
        }

        [TestMethod]
        [TestCategory("Validation")]
        public void Validate_SdtTrung_KhiThemMoi_TraVeLoi()
        {
            // Arrange — SĐT 0901234567 đã tồn tại
            var kh = new ET_KhachHang { HoTen = "Nguyên", DienThoai = "0901234567" };
            _mockKH.Setup(x => x.GetAll()).Returns(new List<ET_KhachHang>
            {
                new ET_KhachHang { Id = 99, DienThoai = "0901234567" }
            });

            // Act
            var error = _bus.ValidateKhachHang(kh, isAdd: true);

            // Assert
            Assert.AreEqual("Số điện thoại này đã tồn tại trong hệ thống.", error);
        }

        [TestMethod]
        [TestCategory("Validation")]
        public void Validate_SdtTrung_NhungCungId_KhiSua_KhongLoi()
        {
            // Arrange — cùng SĐT nhưng cùng ID = sửa chính mình
            var kh = new ET_KhachHang { Id = 5, HoTen = "Nguyên", DienThoai = "0901234567" };
            _mockKH.Setup(x => x.GetAll()).Returns(new List<ET_KhachHang>
            {
                new ET_KhachHang { Id = 5, DienThoai = "0901234567" }
            });

            // Act
            var error = _bus.ValidateKhachHang(kh, isAdd: false);

            // Assert — không lỗi vì sửa chính mình
            Assert.AreEqual(string.Empty, error);
        }

        [TestMethod]
        [TestCategory("Validation")]
        public void Validate_NgaySinhTuongLai_TraVeLoi()
        {
            // Arrange
            var kh = new ET_KhachHang
            {
                HoTen = "Nguyên",
                DienThoai = "0901234567",
                NgaySinh = DateTime.Today.AddDays(1)
            };
            _mockKH.Setup(x => x.GetAll()).Returns(new List<ET_KhachHang>());

            // Act
            var error = _bus.ValidateKhachHang(kh, true);

            // Assert
            Assert.AreEqual("Ngày sinh không thể là ngày tương lai.", error);
        }

        #endregion

        #region Tìm kiếm Tests

        [TestMethod]
        [TestCategory("Search")]
        public void TimKiem_TheoTen_TraVeDung()
        {
            // Arrange
            _mockKH.Setup(x => x.LoadDS()).Returns(new List<ET_KhachHang>
            {
                new ET_KhachHang { Id = 1, HoTen = "Nguyễn Văn A", IsDeleted = false },
                new ET_KhachHang { Id = 2, HoTen = "Trần Thị B", IsDeleted = false },
                new ET_KhachHang { Id = 3, HoTen = "Nguyễn Văn C", IsDeleted = false }
            });

            // Act
            var result = _bus.TimKiem("Nguyễn");

            // Assert — tìm "Nguyễn" trả về 2 kết quả
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        [TestCategory("Search")]
        public void TimKiem_TheoSdt_TraVeDung()
        {
            // Arrange
            _mockKH.Setup(x => x.LoadDS()).Returns(new List<ET_KhachHang>
            {
                new ET_KhachHang { Id = 1, HoTen = "A", DienThoai = "0901111111", IsDeleted = false },
                new ET_KhachHang { Id = 2, HoTen = "B", DienThoai = "0902222222", IsDeleted = false }
            });

            // Act
            var result = _bus.TimKiem("0901");

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("A", result[0].HoTen);
        }

        [TestMethod]
        [TestCategory("Search")]
        public void TimKiem_LocGioiTinh_ChiTraVeNam()
        {
            // Arrange
            _mockKH.Setup(x => x.LoadDS()).Returns(new List<ET_KhachHang>
            {
                new ET_KhachHang { Id = 1, HoTen = "A", GioiTinh = "Nam", IsDeleted = false },
                new ET_KhachHang { Id = 2, HoTen = "B", GioiTinh = "Nữ", IsDeleted = false },
                new ET_KhachHang { Id = 3, HoTen = "C", GioiTinh = "Nam", IsDeleted = false }
            });

            // Act
            var result = _bus.TimKiem(null, "Nam");

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(x => x.GioiTinh == "Nam"));
        }

        #endregion

        #region Xóa Thông Minh Tests (Business Logic nâng cao)

        [TestMethod]
        [TestCategory("SmartDelete")]
        public void XoaThongMinh_ViConSoDu_TuChoiXoa()
        {
            // Arrange — KH có ví với 500k
            _mockKH.Setup(x => x.LayTheoId(1)).Returns(new ET_KhachHang { Id = 1, HoTen = "Test" });
            _mockVi.Setup(x => x.LayTheoKhachHang(1)).Returns(new ET_ViDienTu { Id = 10, IdKhachHang = 1, SoDuKhaDung = 500000 });

            // Act
            var result = _bus.XoaThongMinh(1);

            // Assert — PHẢI từ chối xóa
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("500") && result.ErrorMessage.Contains("VNĐ"));
        }

        [TestMethod]
        [TestCategory("SmartDelete")]
        public void XoaThongMinh_TienDongBang_TuChoiXoa()
        {
            // Arrange — KH có tiền cọc đóng băng
            _mockKH.Setup(x => x.LayTheoId(1)).Returns(new ET_KhachHang { Id = 1, HoTen = "Test" });
            _mockVi.Setup(x => x.LayTheoKhachHang(1)).Returns(new ET_ViDienTu
            {
                Id = 10, IdKhachHang = 1, SoDuKhaDung = 0, SoDuDongBang = 200000
            });

            // Act
            var result = _bus.XoaThongMinh(1);

            // Assert — có cọc -> không cho xóa
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("đóng băng"));
        }

        [TestMethod]
        [TestCategory("SmartDelete")]
        public void XoaThongMinh_ViSoDu0_DatIsDeletedTrue()
        {
            // Arrange — KH ví trống, thẻ không active
            _mockKH.Setup(x => x.LayTheoId(1)).Returns(new ET_KhachHang { Id = 1, HoTen = "Test" });
            _mockVi.Setup(x => x.LayTheoKhachHang(1)).Returns(new ET_ViDienTu { Id = 10, SoDuKhaDung = 0, SoDuDongBang = 0 });
            _mockTheRfid.Setup(x => x.LoadDS()).Returns(new List<ET_TheRFID>());
            _mockKH.Setup(x => x.Sua(It.IsAny<ET_KhachHang>())).Returns(true);

            // Act
            var result = _bus.XoaThongMinh(1);

            // Assert — xóa mềm thành công
            Assert.IsTrue(result.IsSuccess);
            _mockKH.Verify(x => x.Sua(It.Is<ET_KhachHang>(kh => kh.IsDeleted == true)), Times.Once);
        }

        [TestMethod]
        [TestCategory("SmartDelete")]
        public void XoaThongMinh_KhongTimThayKH_TraVeError()
        {
            // Arrange
            _mockKH.Setup(x => x.LayTheoId(999)).Returns((ET_KhachHang)null);

            // Act
            var result = _bus.XoaThongMinh(999);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.ErrorMessage.Contains("Không tìm thấy"));
        }

        #endregion

        #region Customer 360 (Tích hợp Ví + Thẻ + Điểm)

        [TestMethod]
        [TestCategory("Customer360")]
        public void LayViTheoKhachHang_CoVi_TraVeVi()
        {
            // Arrange
            _mockVi.Setup(x => x.LayTheoKhachHang(1)).Returns(new ET_ViDienTu { Id = 10, SoDuKhaDung = 1000000 });

            // Act
            var vi = _bus.LayViTheoKhachHang(1);

            // Assert
            Assert.IsNotNull(vi);
            Assert.AreEqual(1000000, vi.SoDuKhaDung);
        }

        [TestMethod]
        [TestCategory("Customer360")]
        public void CapViMoi_DaCo_TraVeLoi()
        {
            // Arrange — KH đã có ví
            _mockVi.Setup(x => x.LayTheoKhachHang(1)).Returns(new ET_ViDienTu { Id = 10 });

            // Act
            var result = _bus.CapViMoi(1);

            // Assert — không cho tạo ví trùng
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        [TestCategory("Customer360")]
        public void CapViMoi_ChuaCo_TaoViMoi()
        {
            // Arrange
            _mockVi.Setup(x => x.LayTheoKhachHang(1)).Returns((ET_ViDienTu)null);
            _mockVi.Setup(x => x.Them(It.IsAny<ET_ViDienTu>())).Returns(true);

            // Act
            var result = _bus.CapViMoi(1);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            _mockVi.Verify(x => x.Them(It.Is<ET_ViDienTu>(v => v.SoDuKhaDung == 0 && v.IdKhachHang == 1)), Times.Once);
        }

        #endregion
    }
}
