using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;

namespace GUI
{
    public partial class frmLichLamViec : Form
    {
        private DateTime currentMonday;
        private string currentCa = "Sang";

        // Data sources
        private List<ET_NhanVien> dsNhanVien;
        private List<ET_KhuVuc> dsKhuVuc;
        private string[] tenNgay = { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "CN" };

        public frmLichLamViec()
        {
            InitializeComponent();
        }

        private void frmLichLamViec_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);

            // Fix viền trắng GroupBox
            this.BackColor = ThemeManager.BackgroundColor;
            gbLichTuan.BorderColor = Color.Transparent;
            gbLichTuan.CustomBorderColor = Color.Transparent;
            gbLichTuan.FillColor = Color.White;
            gbPhanCong.BorderColor = Color.Transparent;
            gbPhanCong.FillColor = Color.FromArgb(241, 245, 249);
            gbNVChuaPhan.BorderColor = Color.Transparent;
            gbNVChuaPhan.FillColor = Color.White;
            gbNVDaPhan.BorderColor = Color.Transparent;
            gbNVDaPhan.FillColor = Color.FromArgb(236, 253, 245);
            pnlButtons.BackColor = ThemeManager.BackgroundColor;
            splitMain.BackColor = ThemeManager.BackgroundColor;

            // Tính thứ 2 tuần hiện tại
            currentMonday = BUS_LichLamViec.LayThu2CuaTuan(DateTime.Today);

            // Load ComboBox Ca
            cboCaLam.Items.Clear();
            cboCaLam.Items.Add("Ca Sáng (7:30 - 12:00)");
            cboCaLam.Items.Add("Ca Chiều (12:00 - 17:30)");
            cboCaLam.Items.Add("Ca Đêm (18:00 - 7:00)");
            cboCaLam.SelectedIndex = 0;

            // Load DS Nhân viên + Khu vực
            LoadMasterData();

            // Load ComboBox Khu vực
            cboKhuVuc.DataSource = null;
            cboKhuVuc.Items.Clear();
            foreach (var kv in dsKhuVuc)
                cboKhuVuc.Items.Add(kv.TenKhuVuc);
            if (cboKhuVuc.Items.Count > 0) cboKhuVuc.SelectedIndex = 0;

            // Load ComboBox Ngày trong tuần
            CapNhatComboNgay();

            // Events
            btnTuanTruoc.Click += BtnTuanTruoc_Click;
            btnTuanSau.Click += BtnTuanSau_Click;
            cboCaLam.SelectedIndexChanged += CboCaLam_SelectedIndexChanged;
            cboKhuVuc.SelectedIndexChanged += (s, ev) => LoadDualList();
            cboNgayTrongTuan.SelectedIndexChanged += (s, ev) => LoadDualList();
            btnPhanCong.Click += BtnPhanCong_Click;
            btnGoBo.Click += BtnGoBo_Click;
            btnCopyTuan.Click += BtnCopyTuan_Click;

            // Drag & Drop setup
            SetupDragDrop();

            // Load lần đầu
            LoadGridTuan();
            LoadDualList();
        }

        // ========================================
        // MASTER DATA
        // ========================================
        private void LoadMasterData()
        {
            try
            {
                dsNhanVien = BUS_NhanVien.Instance.LoadDS()
                    .Where(nv => nv.TrangThai != "Nghỉ việc").ToList();
                dsKhuVuc = BUS_KhuVuc.Instance.LoadDS();
            }
            catch
            {
                dsNhanVien = new List<ET_NhanVien>();
                dsKhuVuc = new List<ET_KhuVuc>();
            }
        }

        // ========================================
        // TUẦN NAVIGATION
        // ========================================
        private void BtnTuanTruoc_Click(object sender, EventArgs e)
        {
            currentMonday = currentMonday.AddDays(-7);
            CapNhatUI();
        }

        private void BtnTuanSau_Click(object sender, EventArgs e)
        {
            currentMonday = currentMonday.AddDays(7);
            CapNhatUI();
        }

        private void CboCaLam_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboCaLam.SelectedIndex)
            {
                case 0: currentCa = "Sang"; break;
                case 1: currentCa = "Chieu"; break;
                case 2: currentCa = "Dem"; break;
            }
            CapNhatUI();
        }

        private void CapNhatUI()
        {
            CapNhatLabelTuan();
            CapNhatComboNgay();
            LoadGridTuan();
            LoadDualList();
        }

        private void CapNhatLabelTuan()
        {
            DateTime sunday = currentMonday.AddDays(6);
            lblTuanHienTai.Text = string.Format("Tuần: {0:dd/MM} -> {1:dd/MM/yyyy}",
                currentMonday, sunday);
        }

        private void CapNhatComboNgay()
        {
            cboNgayTrongTuan.Items.Clear();
            for (int i = 0; i < 7; i++)
            {
                DateTime d = currentMonday.AddDays(i);
                cboNgayTrongTuan.Items.Add(string.Format("{0} - {1:dd/MM}", tenNgay[i], d));
            }
            // Mặc định chọn ngày hôm nay (hoặc T2)
            int todayOffset = (DateTime.Today - currentMonday).Days;
            if (todayOffset >= 0 && todayOffset <= 6)
                cboNgayTrongTuan.SelectedIndex = todayOffset;
            else
                cboNgayTrongTuan.SelectedIndex = 0;
        }

        // ========================================
        // GRID LỊCH TUẦN (CrossTab / Pivot)
        // ========================================
        private void LoadGridTuan()
        {
            CapNhatLabelTuan();

            var lichTuan = BUS_LichLamViec.Instance.LoadTheoTuan(currentMonday, currentCa);

            // Build DataTable dạng CrossTab: KhuVuc | T2 | T3 | T4 | T5 | T6 | T7 | CN
            DataTable dt = new DataTable();
            dt.Columns.Add("Khu Vực", typeof(string));
            for (int i = 0; i < 7; i++)
            {
                DateTime d = currentMonday.AddDays(i);
                dt.Columns.Add(string.Format("{0}\n{1:dd/MM}", tenNgay[i], d), typeof(string));
            }

            foreach (var kv in dsKhuVuc)
            {
                DataRow row = dt.NewRow();
                row[0] = kv.TenKhuVuc;
                for (int i = 0; i < 7; i++)
                {
                    DateTime d = currentMonday.AddDays(i);
                    var nvTrongO = lichTuan
                        .Where(x => x.IdKhuVuc == kv.Id && x.NgayLam.Date == d.Date)
                        .Select(x => x.TenNhanVien)
                        .ToList();
                    row[i + 1] = nvTrongO.Count > 0 ? string.Join("\n", nvTrongO) : "";
                }
                dt.Rows.Add(row);
            }

            gridLichTuan.DataSource = dt;
            gridViewLichTuan.PopulateColumns();

            // Style columns
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridViewLichTuan.Columns)
            {
                col.OptionsColumn.AllowEdit = false;
                if (col.FieldName == "Khu Vực")
                {
                    col.Width = 130;
                    col.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    col.AppearanceCell.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    col.AppearanceCell.ForeColor = Color.FromArgb(30, 41, 59);
                }
                else
                {
                    col.Width = 120;
                    col.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
                    col.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                }
            }

            // Row height tự động cho multiline
            gridViewLichTuan.OptionsView.RowAutoHeight = true;

            // Highlight ô có người vs không có
            gridViewLichTuan.RowCellStyle -= GridViewLichTuan_RowCellStyle;
            gridViewLichTuan.RowCellStyle += GridViewLichTuan_RowCellStyle;
        }

        private void GridViewLichTuan_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "Khu Vực") return;
            var val = e.CellValue?.ToString();
            if (string.IsNullOrWhiteSpace(val))
            {
                e.Appearance.BackColor = Color.FromArgb(254, 242, 242); // Đỏ nhạt
                e.Appearance.ForeColor = Color.FromArgb(185, 28, 28);
            }
            else
            {
                e.Appearance.BackColor = Color.FromArgb(236, 253, 245); // Xanh nhạt
                e.Appearance.ForeColor = Color.FromArgb(6, 95, 70);
            }
        }

        // ========================================
        // DUAL LIST: NV Chưa phân ↔ NV Đã phân
        // ========================================
        private void LoadDualList()
        {
            if (cboKhuVuc.SelectedIndex < 0 || cboNgayTrongTuan.SelectedIndex < 0) return;
            if (dsKhuVuc.Count == 0) return;

            var kv = dsKhuVuc[cboKhuVuc.SelectedIndex];
            DateTime ngay = currentMonday.AddDays(cboNgayTrongTuan.SelectedIndex);

            // NV đã phân vào ô này
            var daPhan = BUS_LichLamViec.Instance.LoadTheoO(kv.Id, ngay, currentCa);

            // NV đã phân trong ngày+ca (bất kỳ khu vực nào)
            var idsDaPhan = BUS_LichLamViec.Instance.LayDsIdNVDaPhanTrongNgay(ngay, currentCa);

            // NV chưa phân ca
            var chuaPhan = dsNhanVien.Where(nv => !idsDaPhan.Contains(nv.Id)).ToList();

            // Cập nhật ListBox trái (chưa phân)
            lstNVChuaPhan.Items.Clear();
            foreach (var nv in chuaPhan)
                lstNVChuaPhan.Items.Add(new NVItem { Id = nv.Id, HoTen = nv.HoTen });

            // Cập nhật ListBox phải (đã phân)
            lstNVDaPhan.Items.Clear();
            foreach (var l in daPhan)
                lstNVDaPhan.Items.Add(new NVItem { Id = l.Id, HoTen = l.TenNhanVien, IdNhanVien = l.IdNhanVien });

            // Cập nhật title
            gbNVDaPhan.Text = string.Format("✅ ĐÃ PHÂN: {0} — {1} — {2} ({3} người)",
                kv.TenKhuVuc, tenNgay[cboNgayTrongTuan.SelectedIndex],
                ET_LichLamViec.LayTenCa(currentCa), daPhan.Count);
        }

        // ========================================
        // NÚT PHÂN CÔNG / GỠ BỎ
        // ========================================
        private void BtnPhanCong_Click(object sender, EventArgs e)
        {
            if (lstNVChuaPhan.SelectedItems.Count == 0)
            {
                MessageBox.Show("Chọn ít nhất 1 nhân viên để phân công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboKhuVuc.SelectedIndex < 0 || cboNgayTrongTuan.SelectedIndex < 0) return;

            var kv = dsKhuVuc[cboKhuVuc.SelectedIndex];
            DateTime ngay = currentMonday.AddDays(cboNgayTrongTuan.SelectedIndex);
            int count = 0;

            foreach (var item in lstNVChuaPhan.SelectedItems)
            {
                var nv = item as NVItem;
                if (nv != null && BUS_LichLamViec.Instance.ThemNVVaoCa(nv.Id, kv.Id, ngay, currentCa))
                    count++;
            }

            if (count > 0)
            {
                LoadGridTuan();
                LoadDualList();
            }
        }

        private void BtnGoBo_Click(object sender, EventArgs e)
        {
            if (lstNVDaPhan.SelectedItems.Count == 0)
            {
                MessageBox.Show("Chọn nhân viên cần gỡ khỏi ca!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int count = 0;
            foreach (var item in lstNVDaPhan.SelectedItems)
            {
                var nv = item as NVItem;
                if (nv != null && BUS_LichLamViec.Instance.GoNVKhoiCa(nv.Id))
                    count++;
            }

            if (count > 0)
            {
                LoadGridTuan();
                LoadDualList();
            }
        }

        // ========================================
        // COPY TUẦN
        // ========================================
        private void BtnCopyTuan_Click(object sender, EventArgs e)
        {
            DateTime mondayDich = currentMonday.AddDays(7);
            var result = MessageBox.Show(
                string.Format("Sao chép lịch {0} từ tuần {1:dd/MM} sang tuần {2:dd/MM}?",
                    ET_LichLamViec.LayTenCa(currentCa),
                    currentMonday, mondayDich),
                "Xác nhận Copy Tuần",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int copied = BUS_LichLamViec.Instance.CopyTuan(currentMonday, mondayDich, currentCa);
                MessageBox.Show(string.Format("Đã sao chép {0} lịch phân công sang tuần sau!", copied),
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Chuyển sang tuần mới
                currentMonday = mondayDich;
                CapNhatUI();
            }
        }

        // ========================================
        // DRAG & DROP
        // ========================================
        private void SetupDragDrop()
        {
            // ListBox NV Chưa Phân: cho phép kéo ra
            lstNVChuaPhan.MouseDown += LstNVChuaPhan_MouseDown;

            // ListBox NV Đã Phân: nhận drop
            lstNVDaPhan.AllowDrop = true;
            lstNVDaPhan.DragOver += LstNVDaPhan_DragOver;
            lstNVDaPhan.DragDrop += LstNVDaPhan_DragDrop;

            // Ngược lại: Kéo từ Đã Phân về Chưa Phân = Gỡ
            lstNVDaPhan.MouseDown += LstNVDaPhan_MouseDown;
            lstNVChuaPhan.AllowDrop = true;
            lstNVChuaPhan.DragOver += LstNVChuaPhan_DragOver;
            lstNVChuaPhan.DragDrop += LstNVChuaPhan_DragDrop;
        }

        // --- Kéo từ Chưa Phân -> Đã Phân (Phân công) ---
        private void LstNVChuaPhan_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && lstNVChuaPhan.SelectedItem != null)
            {
                lstNVChuaPhan.DoDragDrop(new DragData
                {
                    Source = "ChuaPhan",
                    Item = lstNVChuaPhan.SelectedItem as NVItem
                }, DragDropEffects.Move);
            }
        }

        private void LstNVDaPhan_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DragData)))
            {
                var data = e.Data.GetData(typeof(DragData)) as DragData;
                if (data != null && data.Source == "ChuaPhan")
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.None;
            }
        }

        private void LstNVDaPhan_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(typeof(DragData)) as DragData;
            if (data == null || data.Item == null || data.Source != "ChuaPhan") return;

            if (cboKhuVuc.SelectedIndex < 0 || cboNgayTrongTuan.SelectedIndex < 0) return;

            var kv = dsKhuVuc[cboKhuVuc.SelectedIndex];
            DateTime ngay = currentMonday.AddDays(cboNgayTrongTuan.SelectedIndex);

            if (BUS_LichLamViec.Instance.ThemNVVaoCa(data.Item.Id, kv.Id, ngay, currentCa))
            {
                LoadGridTuan();
                LoadDualList();
            }
        }

        // --- Kéo từ Đã Phân -> Chưa Phân (Gỡ bỏ) ---
        private void LstNVDaPhan_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && lstNVDaPhan.SelectedItem != null)
            {
                lstNVDaPhan.DoDragDrop(new DragData
                {
                    Source = "DaPhan",
                    Item = lstNVDaPhan.SelectedItem as NVItem
                }, DragDropEffects.Move);
            }
        }

        private void LstNVChuaPhan_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DragData)))
            {
                var data = e.Data.GetData(typeof(DragData)) as DragData;
                if (data != null && data.Source == "DaPhan")
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.None;
            }
        }

        private void LstNVChuaPhan_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(typeof(DragData)) as DragData;
            if (data == null || data.Item == null || data.Source != "DaPhan") return;

            // data.Item.Id ở đây là Id của bảng LichLamViec (vì load từ LoadTheoO)
            if (BUS_LichLamViec.Instance.GoNVKhoiCa(data.Item.Id))
            {
                LoadGridTuan();
                LoadDualList();
            }
        }

        // ========================================
        // HELPER CLASSES
        // ========================================

        /// <summary>
        /// Item hiển thị trong ListBox
        /// </summary>
        private class NVItem
        {
            public int Id { get; set; }           // Id LichLamViec (khi ở list Đã Phân) hoặc Id NhanVien (khi ở list Chưa Phân)
            public int IdNhanVien { get; set; }
            public string HoTen { get; set; }
            public override string ToString() { return HoTen; }
        }

        /// <summary>
        /// Data truyền qua DragDrop
        /// </summary>
        [Serializable]
        private class DragData
        {
            public string Source { get; set; }    // "ChuaPhan" hoặc "DaPhan"
            public NVItem Item { get; set; }
        }
    }
}
