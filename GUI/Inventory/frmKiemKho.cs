using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;

namespace GUI
{
    public partial class frmKiemKho : Form
    {
        private DataTable _dtKiemKe;
        private int _currentUserId;
        private bool _isManager;

        public frmKiemKho()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            gridView1.OptionsBehavior.Editable = true;

            _currentUserId = GetCurrentUserId();
            _isManager = CheckIsManager();

            InitHeader();
            InitGrid();
            WireEvents();

            // Auto-focus barcode on load
            this.Load += (s, e) => txtBarcode.Focus();
        }

        // ===========================
        // INIT
        // ===========================

        private void InitHeader()
        {
            // Load danh sách kho
            var dsKho = BUS_KhoHang.Instance.LoadDS();
            cboKho.DataSource = dsKho;
            cboKho.DisplayMember = "TenKho";
            cboKho.ValueMember = "Id";
            if (dsKho.Count > 0) cboKho.SelectedIndex = 0;

            lblNgay.Text = "Ngày: " + DateTime.Now.ToString("dd/MM/yyyy");

            var nv = SessionManager.CurrentUser;
            lblNhanVien.Text = "NV: " + (nv != null ? nv.HoTen : "Admin");

            // Blind Mode: chỉ quản lý mới thấy checkbox
            chkBlindMode.Visible = _isManager;
            chkBlindMode.Checked = false;
        }

        private void InitGrid()
        {
            _dtKiemKe = new DataTable();
            _dtKiemKe.Columns.Add("IdSanPham", typeof(int));
            _dtKiemKe.Columns.Add("MaSanPham", typeof(string));
            _dtKiemKe.Columns.Add("TenSanPham", typeof(string));
            _dtKiemKe.Columns.Add("DonViTinh", typeof(string));
            _dtKiemKe.Columns.Add("TonHeThong", typeof(int));
            _dtKiemKe.Columns.Add("TonThucTe", typeof(int));
            _dtKiemKe.Columns.Add("ChenhLech", typeof(int));
            _dtKiemKe.Columns.Add("GhiChu", typeof(string));

            gridControl1.DataSource = _dtKiemKe;

            // Configure columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            // Read-only columns
            SetColumnReadOnly("IdSanPham", true, false); // Hidden
            SetColumnReadOnly("MaSanPham", false, true);
            SetColumnReadOnly("TenSanPham", false, true);
            SetColumnReadOnly("DonViTinh", false, true);
            SetColumnReadOnly("TonHeThong", false, true);
            SetColumnReadOnly("ChenhLech", false, true);

            // Editable columns
            var colThucTe = gridView1.Columns["TonThucTe"];
            if (colThucTe != null)
            {
                colThucTe.OptionsColumn.AllowEdit = true;
                colThucTe.Caption = "TỒN THỰC TẾ";
                colThucTe.AppearanceHeader.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                colThucTe.AppearanceHeader.ForeColor = Color.FromArgb(74, 137, 115);
                var spinRepo = new RepositoryItemSpinEdit();
                spinRepo.MinValue = 0;
                spinRepo.MaxValue = 999999;
                spinRepo.IsFloatValue = false;
                colThucTe.ColumnEdit = spinRepo;
            }

            var colGhiChu = gridView1.Columns["GhiChu"];
            if (colGhiChu != null)
            {
                colGhiChu.OptionsColumn.AllowEdit = true;
                colGhiChu.Caption = "Ghi Chú";
            }

            // Column captions
            SetCaption("MaSanPham", "Mã SP");
            SetCaption("TenSanPham", "Tên Sản Phẩm");
            SetCaption("DonViTinh", "ĐVT");
            SetCaption("TonHeThong", "Tồn Hệ Thống");
            SetCaption("ChenhLech", "Chênh Lệch");

            // Column widths
            SetWidth("MaSanPham", 80);
            SetWidth("TenSanPham", 220);
            SetWidth("DonViTinh", 60);
            SetWidth("TonHeThong", 110);
            SetWidth("TonThucTe", 120);
            SetWidth("ChenhLech", 100);
            SetWidth("GhiChu", 180);

            // Grid options — keyboard-driven
            gridView1.OptionsBehavior.Editable = true;
            gridView1.OptionsNavigation.EnterMoveNextColumn = true;
            gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            gridView1.OptionsView.RowAutoHeight = false;
            gridView1.RowHeight = 30;
            gridView1.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            gridView1.Appearance.Row.Font = new Font("Segoe UI", 9.5f);

            // Load data for selected kho
            LoadKiemKeData();
        }

        private void WireEvents()
        {
            // Combobox kho changed
            cboKho.SelectedIndexChanged += (s, e) => LoadKiemKeData();

            // Blind mode toggle
            chkBlindMode.CheckedChanged += (s, e) =>
            {
                var colHT = gridView1.Columns["TonHeThong"];
                var colCL = gridView1.Columns["ChenhLech"];
                if (colHT != null) colHT.Visible = !chkBlindMode.Checked;
                if (colCL != null) colCL.Visible = !chkBlindMode.Checked;
            };

            // Barcode/search box
            txtBarcode.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    HandleBarcodeInput(txtBarcode.Text.Trim());
                    txtBarcode.Text = "";
                    txtBarcode.Focus();
                }
            };

            // Cell value changed -> recalculate chênh lệch
            gridView1.CellValueChanged += (s, e) =>
            {
                if (e.Column.FieldName == "TonThucTe")
                {
                    int tonHT = Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "TonHeThong"));
                    int tonTT = Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "TonThucTe"));
                    gridView1.SetRowCellValue(e.RowHandle, "ChenhLech", tonTT - tonHT);
                    UpdateStats();
                }
            };

            // Visual cues — row style (color coding)
            gridView1.RowStyle += (s, e) =>
            {
                if (e.RowHandle >= 0)
                {
                    object val = gridView1.GetRowCellValue(e.RowHandle, "ChenhLech");
                    if (val != null && val != DBNull.Value)
                    {
                        int cl = Convert.ToInt32(val);
                        if (cl < 0)
                            e.Appearance.ForeColor = Color.FromArgb(180, 83, 83); // Mất hàng -> đỏ
                        else if (cl > 0)
                            e.Appearance.ForeColor = Color.FromArgb(180, 130, 60); // Dư hàng -> vàng
                        else
                            e.Appearance.ForeColor = Color.FromArgb(100, 116, 139); // Khớp -> xám nhạt
                    }
                }
            };

            // Cell select all on enter (UX: click vào ô -> bôi đen toàn bộ)
            gridView1.ShownEditor += (s, e) =>
            {
                if (gridView1.ActiveEditor != null)
                    gridView1.ActiveEditor.SelectAll();
            };

            // Buttons
            btnHoanTat.Click += BtnHoanTat_Click;
            btnHuy.Click += (s, e) => this.Close();
            btnResetAll.Click += (s, e) =>
            {
                if (TDCMessageBox.Show("Reset toàn bộ số thực tế về bằng tồn hệ thống?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    LoadKiemKeData();
            };
        }

        // ===========================
        // DATA
        // ===========================

        private void LoadKiemKeData()
        {
            if (cboKho.SelectedValue == null) return;
            int idKho = (int)cboKho.SelectedValue;

            var tonKhoList = BUS_KhoHang.Instance.GetTonKhoChiTiet(idKho);

            _dtKiemKe.Rows.Clear();
            foreach (var item in tonKhoList)
            {
                var row = _dtKiemKe.NewRow();
                row["IdSanPham"] = item.Id > 0 ? item.Id : 0;
                row["MaSanPham"] = item.MaSanPham;
                row["TenSanPham"] = item.TenSanPham;
                row["DonViTinh"] = item.DonViTinh;
                row["TonHeThong"] = item.SoLuong;
                row["TonThucTe"] = item.SoLuong; // Mặc định = tồn hệ thống
                row["ChenhLech"] = 0;
                row["GhiChu"] = "";
                _dtKiemKe.Rows.Add(row);
            }

            gridView1.RefreshData();
            UpdateStats();
        }

        private void HandleBarcodeInput(string input)
        {
            if (string.IsNullOrEmpty(input)) return;

            string lower = input.ToLower();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                string ma = (gridView1.GetRowCellValue(i, "MaSanPham") ?? "").ToString().ToLower();
                string ten = (gridView1.GetRowCellValue(i, "TenSanPham") ?? "").ToString().ToLower();

                if (ma == lower || ten.Contains(lower))
                {
                    // Tìm thấy -> cộng 1 vào Tồn Thực Tế
                    int current = Convert.ToInt32(gridView1.GetRowCellValue(i, "TonThucTe"));
                    gridView1.SetRowCellValue(i, "TonThucTe", current + 1);

                    // Recalc chênh lệch
                    int tonHT = Convert.ToInt32(gridView1.GetRowCellValue(i, "TonHeThong"));
                    gridView1.SetRowCellValue(i, "ChenhLech", (current + 1) - tonHT);

                    // Focus vào dòng đó
                    gridView1.FocusedRowHandle = i;
                    UpdateStats();
                    return;
                }
            }

            TDCMessageBox.Show("Không tìm thấy sản phẩm: " + input, "Thông báo");
        }

        private void UpdateStats()
        {
            int total = _dtKiemKe.Rows.Count;
            int lechTang = 0, lechGiam = 0;

            foreach (DataRow row in _dtKiemKe.Rows)
            {
                int cl = Convert.ToInt32(row["ChenhLech"]);
                if (cl > 0) lechTang++;
                else if (cl < 0) lechGiam++;
            }

            lblStats.Text = string.Format("Tổng SP: {0} | Lệch tăng: {1} | Lệch giảm: {2}", total, lechTang, lechGiam);
        }

        // ===========================
        // HOÀN TẤT
        // ===========================

        private void BtnHoanTat_Click(object sender, EventArgs e)
        {
            // Kiểm tra có chênh lệch không
            var rowsLech = new List<(int IdSanPham, int ChenhLech, string GhiChu)>();
            foreach (DataRow row in _dtKiemKe.Rows)
            {
                int cl = Convert.ToInt32(row["ChenhLech"]);
                if (cl != 0)
                {
                    // IdSanPham trong DataTable là Id của TonKho, cần map lại sang IdSanPham thật
                    // Dùng MaSanPham để tìm SanPham
                    string masp = row["MaSanPham"].ToString();
                    var sp = DAL.DAL_SanPham.Instance.LoadDS().FirstOrDefault(x => x.MaCode == masp);
                    if (sp != null)
                        rowsLech.Add((sp.Id, cl, row["GhiChu"].ToString()));
                }
            }

            if (rowsLech.Count == 0)
            {
                TDCMessageBox.Show("Kho hoàn toàn khớp — không có chênh lệch!", "Thông báo");
                return;
            }

            string msg = string.Format("Phát hiện {0} sản phẩm chênh lệch.\nBạn có muốn tạo phiếu điều chỉnh kho?", rowsLech.Count);
            if (TDCMessageBox.Show(msg, "Hoàn tất kiểm kê", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            int idKho = (int)cboKho.SelectedValue;
            bool success = BUS_KhoHang.Instance.HoanTatKiemKe(idKho, rowsLech, _currentUserId);

            if (success)
            {
                TDCMessageBox.Show("Kiểm kê hoàn tất! Tồn kho đã được đồng bộ.", "Thành công");
                LoadKiemKeData(); // Reload to show updated stock
            }
            else
            {
                TDCMessageBox.Show("Lỗi khi xử lý kiểm kê!", "Lỗi");
            }
        }

        // ===========================
        // HELPERS
        // ===========================

        private int GetCurrentUserId()
        {
            var nv = SessionManager.CurrentUser;
            return nv != null ? nv.Id : 1;
        }

        private bool CheckIsManager()
        {
            var nv = SessionManager.CurrentUser;
            if (nv == null) return true; // Dev mode = full access
            // IdVaiTro: 1=Admin, 2=QuanLy -> có quyền Blind Mode
            return nv.IdVaiTro <= 2;
        }

        private void SetColumnReadOnly(string fieldName, bool hidden, bool readOnly)
        {
            var col = gridView1.Columns[fieldName];
            if (col == null) return;
            col.Visible = !hidden;
            col.OptionsColumn.AllowEdit = !readOnly;
        }

        private void SetCaption(string fieldName, string caption)
        {
            var col = gridView1.Columns[fieldName];
            if (col != null) col.Caption = caption;
        }

        private void SetWidth(string fieldName, int width)
        {
            var col = gridView1.Columns[fieldName];
            if (col != null) col.Width = width;
        }
    }
}
