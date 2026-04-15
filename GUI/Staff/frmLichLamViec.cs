using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmLichLamViec : Form
    {
        private DateTime currentMonday;
        private int currentCaId;

        // Data sources
        private List<ET_NhanVien> dsNhanVien;
        private List<ET_KhuVuc> dsKhuVuc;
        private List<ET_CaLamMau> dsCaLam;
        private string[] tenNgay = { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "CN" };

        public frmLichLamViec()
        {
            InitializeComponent();
            InitIcons();
        }

        private void InitIcons()
        {
            btnTuanTruoc.Image = IconHelper.GetBitmap(IconChar.ChevronLeft,  System.Drawing.Color.White, 14);
            btnTuanSau.Image   = IconHelper.GetBitmap(IconChar.ChevronRight, System.Drawing.Color.White, 14);
            btnCopyTuan.Image  = IconHelper.GetBitmap(IconChar.Copy,         System.Drawing.Color.White, 14);
            btnPhanCong.Image  = IconHelper.GetBitmap(IconChar.ArrowDown,    System.Drawing.Color.White, 14);
            btnGoBo.Image      = IconHelper.GetBitmap(IconChar.ArrowUp,      System.Drawing.Color.White, 14);
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

            // Load DS Nhân viên + Khu vực + Ca làm
            LoadMasterData();

            // Load ComboBox Ca
            cboCaLam.DataSource = null;
            if (dsCaLam != null && dsCaLam.Count > 0)
            {
                cboCaLam.DataSource = dsCaLam;
                cboCaLam.DisplayMember = "TenCa";
                cboCaLam.ValueMember = "Id";
                if (cboCaLam.Items.Count > 0) cboCaLam.SelectedIndex = 0;
            }

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

            // Che do xem
            cboCheDo.Items.Add("Lịch Tuần");
            cboCheDo.Items.Add("Lịch Tháng");
            cboCheDo.SelectedIndex = 0;

            // Drag & Drop đã được wire trong Designer.cs

            // Load lần đầu
            CapNhatUI();
        }

        private void CboKhuVuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDualList();
        }

        private void CboNgayTrongTuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDualList();
        }

        // =
        // MASTER DATA
        // =
        private void LoadMasterData()
        {
            try
            {
                dsNhanVien = BUS_NhanVien.Instance.LoadDS()
                    .Where(nv => nv.TrangThai != "Nghỉ việc").ToList();
                dsKhuVuc = BUS_KhuVuc.Instance.LoadDS();
                dsCaLam = BUS_LichLamViec.Instance.LoadCaLamMau();
            }
            catch
            {
                dsNhanVien = new List<ET_NhanVien>();
                dsKhuVuc = new List<ET_KhuVuc>();
                dsCaLam = new List<ET_CaLamMau>();
            }
        }

        // =
        // TUẦN NAVIGATION
        // =
        private bool IsMonthMode => cboCheDo.SelectedIndex == 1;

        private void CboCheDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsMonthMode) {
                // Đưa về đầu tháng
                currentMonday = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                btnCopyTuan.Text = "Copy sang tháng sau";
                gbLichTuan.Text = "LỊCH PHÂN CA THÁNG";
            } else {
                currentMonday = GetMonday(DateTime.Today);
                btnCopyTuan.Text = "Copy sang tuần sau";
                gbLichTuan.Text = "LỊCH PHÂN CA TUẦN";
            }
            CapNhatUI();
        }

        private void BtnTuanTruoc_Click(object sender, EventArgs e)
        {
            if (IsMonthMode) currentMonday = currentMonday.AddMonths(-1);
            else currentMonday = currentMonday.AddDays(-7);
            CapNhatUI();
        }

        private void BtnTuanSau_Click(object sender, EventArgs e)
        {
            if (IsMonthMode) currentMonday = currentMonday.AddMonths(1);
            else currentMonday = currentMonday.AddDays(7);
            CapNhatUI();
        }

        private void CboCaLam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCaLam.SelectedValue != null && cboCaLam.SelectedValue is int id)
            {
                currentCaId = id;
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

        private DateTime GetMonday(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-diff).Date;
        }

        private void CapNhatLabelTuan()
        {
            if (IsMonthMode) {
                lblTuanHienTai.Text = string.Format("Tháng: {0:MM/yyyy}", currentMonday);
            } else {
                DateTime sunday = currentMonday.AddDays(6);
                lblTuanHienTai.Text = string.Format("Tuần: {0:dd/MM} -> {1:dd/MM/yyyy}", currentMonday, sunday);
            }
        }

        private void CapNhatComboNgay()
        {
            var oldIdx = cboNgayTrongTuan.SelectedIndex;
            cboNgayTrongTuan.Items.Clear();
            int days = IsMonthMode ? DateTime.DaysInMonth(currentMonday.Year, currentMonday.Month) : 7;
            for (int i = 0; i < days; i++)
            {
                DateTime d = currentMonday.AddDays(i);
                int dayOfWeekIndex = d.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)d.DayOfWeek - 1;
                cboNgayTrongTuan.Items.Add(string.Format("{0} - {1:dd/MM}", tenNgay[dayOfWeekIndex], d));
            }
            // Mặc định chọn ngày hôm nay
            int todayOffset = (DateTime.Today - currentMonday).Days;
            if (todayOffset >= 0 && todayOffset < days)
                cboNgayTrongTuan.SelectedIndex = todayOffset;
            else if (oldIdx >= 0 && oldIdx < days)
                cboNgayTrongTuan.SelectedIndex = oldIdx;
            else
                cboNgayTrongTuan.SelectedIndex = 0;
        }

        // =
        // GRID LỊCH TUẦN (CrossTab / Pivot)
        // =
        private void LoadGridTuan()
        {
            CapNhatLabelTuan();
            int days = IsMonthMode ? DateTime.DaysInMonth(currentMonday.Year, currentMonday.Month) : 7;
            
            // Xử lý lấy danh sách bằng cách gọi LoadTheoTuan nhiều lần nếu là tháng
            var dsAllLich = new List<ET.ET_LichLamViec>();
            int weeks = (int)Math.Ceiling((double)days / 7);
            for (int w = 0; w < weeks; w++)
            {
                var tmp = BUS_LichLamViec.Instance.LoadTheoTuan(currentMonday.AddDays(w * 7), currentCaId);
                if (tmp != null) dsAllLich.AddRange(tmp);
            }

            // Build DataTable dạng CrossTab
            DataTable dt = new DataTable();
            dt.Columns.Add("Khu Vực", typeof(string));
            for (int i = 0; i < days; i++)
            {
                DateTime d = currentMonday.AddDays(i);
                int dayOfWeekIndex = d.DayOfWeek == DayOfWeek.Sunday ? 6 : (int)d.DayOfWeek - 1;
                dt.Columns.Add(string.Format("{0}\n{1:dd/MM}", tenNgay[dayOfWeekIndex], d), typeof(string));
            }

            // Fill Data
            foreach (var kv in dsKhuVuc)
            {
                DataRow r = dt.NewRow();
                r["Khu Vực"] = kv.TenKhuVuc;
                for (int i = 0; i < days; i++)
                {
                    DateTime d = currentMonday.AddDays(i);
                    // Đếm số NV trong ca, ngày, khu vực này
                    int count = dsAllLich.Count(l =>
                        l.NgayLam.Date == d.Date &&
                        l.IdKhuVuc == kv.Id);

                    r[i + 1] = count > 0 ? string.Format("{0} NV", count) : "-";
                }
                dt.Rows.Add(r);
            }

            gridLichTuan.DataSource = dt;
            gridViewLichTuan.PopulateColumns();

            // Cấu hình giao diện chuẩn UI/UX
            gridViewLichTuan.OptionsView.ShowGroupPanel = false;
            gridViewLichTuan.OptionsView.ShowIndicator = false;
            gridViewLichTuan.OptionsView.ColumnAutoWidth = !IsMonthMode; // Tháng thì tắt AutoWidth để cuộn ngang
            gridViewLichTuan.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridViewLichTuan.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridViewLichTuan.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewLichTuan.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            gridViewLichTuan.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            gridViewLichTuan.ColumnPanelRowHeight = 50;

            foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridViewLichTuan.Columns)
            {
                col.OptionsColumn.AllowEdit = false;
                if (col.FieldName == "Khu Vực")
                {
                    col.Width = 150;
                    col.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    col.AppearanceCell.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
                    col.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
                    col.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
                }
                else
                {
                    col.Width = 110;
                    col.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    col.AppearanceHeader.Options.UseTextOptions = true;
                    col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
            }

            // Row height tự động
            gridViewLichTuan.RowHeight = 35;

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

        // =
        // DUAL LIST: NV Chưa phân ↔ NV Đã phân
        // =
        private void LoadDualList()
        {
            if (cboKhuVuc.SelectedIndex < 0 || cboNgayTrongTuan.SelectedIndex < 0) return;
            if (dsKhuVuc.Count == 0) return;

            var kv = dsKhuVuc[cboKhuVuc.SelectedIndex];
            DateTime ngay = currentMonday.AddDays(cboNgayTrongTuan.SelectedIndex);

            // NV đã phân vào ô này
            var daPhan = BUS_LichLamViec.Instance.LoadTheoO(kv.Id, ngay, currentCaId);

            // NV đã phân trong ngày+ca (bất kỳ khu vực nào)
            var idsDaPhan = BUS_LichLamViec.Instance.LayDsIdNVDaPhanTrongNgay(ngay, currentCaId);

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

            // Cập nhật title (dung text thuần, icon sẽ hiển thị qua GroupBox)
            var strCa = cboCaLam.Text;
            gbNVDaPhan.Text = string.Format("ĐA PHÂN: {0} — {1} — {2} ({3} người)",
                kv.TenKhuVuc, tenNgay[cboNgayTrongTuan.SelectedIndex],
                strCa, daPhan.Count);
        }

        // =
        // NÚT PHÂN CÔNG / GỠ BỎ
        // =
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
                if (nv != null && BUS_LichLamViec.Instance.ThemNVVaoCa(nv.Id, kv.Id, ngay, currentCaId))
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

        // =
        // COPY TUẦN
        // =
        private void BtnCopyTuan_Click(object sender, EventArgs e)
        {
            DateTime mondayDich = currentMonday.AddDays(7);
            var strCa = cboCaLam.Text;
            var result = MessageBox.Show(
                string.Format("Sao chép lịch {0} từ tuần {1:dd/MM} sang tuần {2:dd/MM}?",
                    strCa,
                    currentMonday, mondayDich),
                "Xác nhận Copy Tuần",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int copied = BUS_LichLamViec.Instance.CopyTuan(currentMonday, mondayDich, currentCaId);
                MessageBox.Show(string.Format("Đã sao chép {0} lịch phân công sang tuần sau!", copied),
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Chuyển sang tuần mới
                currentMonday = mondayDich;
                CapNhatUI();
            }
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

            if (BUS_LichLamViec.Instance.ThemNVVaoCa(data.Item.Id, kv.Id, ngay, currentCaId))
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

        // =
        // HELPER CLASSES
        // =

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
