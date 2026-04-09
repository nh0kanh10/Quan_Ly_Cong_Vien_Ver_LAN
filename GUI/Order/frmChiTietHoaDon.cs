using System;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using ET;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace GUI
{
    public partial class frmChiTietHoaDon : Form, IBaseForm
    {
        private string _maCode;
        private ET_DonHang _donHang;

        public frmChiTietHoaDon()
        {
            InitializeComponent();
            
            InitIcons();
            ApplyStyles();
            ApplyPermissions();
        }

        public frmChiTietHoaDon(string maCode) : this()
        {
            this._maCode = maCode;
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_DONHANG"))
            {
                this.Enabled = false;
                return;
            }
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
        }

        public void InitIcons()
        {
            // Detail form usually doesn't have many icons on buttons, but we can add 'Close' if needed
        }

        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            LoadDetails();
        }

        public void LoadDetails()
        {
            if (string.IsNullOrEmpty(_maCode)) return;
            
            _donHang = BUS_DonHang.Instance.GetByMaCode(_maCode);
            if (_donHang == null) return;

            lblMaDon.Text = "Mã đơn: " + _donHang.MaCode;
            lblNgay.Text = "Thời gian: " + _donHang.ThoiGian.ToString("dd/MM/yyyy HH:mm");
            lblTongTien.Text = string.Format("Tổng: {0:N0} đ", _donHang.TongTien);
            
            var kh = BUS_KhachHang.Instance.GetById(_donHang.IdKhachHang ?? 0);
            lblKhachHang.Text = "Khách hàng: " + (kh != null ? kh.HoTen : "Khách vãng lai");

            var details = BUS_ChiTietDonHang.Instance.LoadByDonHang(_donHang.Id);
            
            var dsSanPham = BUS_SanPham.Instance.LoadDS();
            var dsCombo = BUS_Combo.Instance.LoadDS();

            var displayList = details.Select(d => new {
                TenSanPham = d.IdSanPham.HasValue ? dsSanPham.FirstOrDefault(s => s.Id == d.IdSanPham.Value)?.Ten : 
                             d.IdCombo.HasValue ? "[Combo] " + dsCombo.FirstOrDefault(c => c.Id == d.IdCombo.Value)?.Ten : "Không xác định",
                d.SoLuong,
                d.DonGiaThucTe,
                ThanhTien = d.SoLuong * d.DonGiaThucTe
            }).ToList();

            gridControl.DataSource = new BindingList<dynamic>(displayList.Cast<dynamic>().ToList());
            FormatGrid();
        }

        private void FormatGrid()
        {
            if (gridView.Columns["TenSanPham"] != null) gridView.Columns["TenSanPham"].Caption = "Tên Sản Phẩm / Combo";
            if (gridView.Columns["SoLuong"] != null) gridView.Columns["SoLuong"].Caption = "SL";
            if (gridView.Columns["DonGiaThucTe"] != null)
            {
                gridView.Columns["DonGiaThucTe"].Caption = "Đơn giá";
                gridView.Columns["DonGiaThucTe"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView.Columns["DonGiaThucTe"].DisplayFormat.FormatString = "N0";
            }
            if (gridView.Columns["ThanhTien"] != null)
            {
                gridView.Columns["ThanhTien"].Caption = "Thành tiền";
                gridView.Columns["ThanhTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView.Columns["ThanhTien"].DisplayFormat.FormatString = "N0";
            }
            gridView.BestFitColumns();
        }
    }
}


