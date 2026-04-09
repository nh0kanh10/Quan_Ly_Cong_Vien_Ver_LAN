using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    /// <summary>
    /// Dialog popup để chọn dịch vụ (Combo/Phòng/Bàn ăn) cho đoàn.
    /// Layout: SearchLookup chọn SP + SpinEdit SL + DateEdit ngày SD + TextEdit ghi chú + OK/Cancel
    /// </summary>
    public partial class frmChonDichVuDoanDialog : Form
    {
        public DichVuItem SelectedItem { get; private set; }
        public int SoLuong { get; private set; }
        public DateTime? NgaySuDung { get; private set; }
        public string GhiChu { get; private set; }
        public bool ShowPaxField { get; set; }
        public int DefaultPax { get; set; }

        private readonly List<DichVuItem> _items;
        private readonly string _title;

        public frmChonDichVuDoanDialog(string title, List<DichVuItem> items)
        {
            _title = title;
            _items = items;
            InitializeComponent();
        }

        private void frmChonDichVuDoanDialog_Load(object sender, EventArgs e)
        {
            this.Text = "Thêm dịch vụ: " + _title;
            lblDV.Text = "Chọn " + _title + ":";
            slkDichVu.Properties.DataSource = _items;
            slkDichVu.Properties.ValueMember = "Id";
            slkDichVu.Properties.DisplayMember = "Ten";
            
            var gv = slkDichVu.Properties.View;
            gv.Columns.Clear();
            gv.Columns.AddField("Ten").Visible = true;
            gv.Columns["Ten"].Caption = "Tên";
            gv.Columns.AddField("DonGia").Visible = true;
            gv.Columns["DonGia"].Caption = "Đơn giá";
            gv.Columns["DonGia"].DisplayFormat.FormatString = "N0";

            // Áp dụng Theme cho popup grid
            ThemeManager.StyleDevExpressGrid(gv.GridControl);

            // Làm đẹp cho thanh nhập text của SearchLookUpEdit để tệp màu với Guna
            slkDichVu.Properties.Appearance.BackColor = System.Drawing.Color.White;
            slkDichVu.Properties.Appearance.ForeColor = ThemeManager.TextPrimaryColor;
            slkDichVu.Properties.Appearance.Font = ThemeManager.GetFont(10f);
            slkDichVu.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            slkDichVu.Properties.Appearance.BorderColor = ThemeManager.BorderColor;
            slkDichVu.Properties.AppearanceFocused.BorderColor = ThemeManager.PrimaryColor;
            slkDichVu.Properties.AutoHeight = false;
            slkDichVu.Height = 30;

            dtpNgaySuDung.Value = DateTime.Today;

            ThemeManager.ApplyTheme(this);
            if (ShowPaxField && DefaultPax > 0)
                spnSoLuong.Value = DefaultPax;
        }

        private void frmChonDichVuDoanDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SelectedItem = null; 
            this.Close();
        }


        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (slkDichVu.EditValue == null || slkDichVu.EditValue == DBNull.Value)
            {
                TDCMessageBox.Show("Vui lòng chọn dịch vụ!", "Thiếu thông tin");
                return;
            }

            int selectedId = Convert.ToInt32(slkDichVu.EditValue);
            SelectedItem = _items.Find(x => x.Id == selectedId);
            SoLuong = (int)spnSoLuong.Value;
            NgaySuDung = dtpNgaySuDung.Value;
            GhiChu = txtGhiChu.Text.Trim();

            this.Close();
        }
    }

    /// <summary>
    /// Model nhẹ cho danh sách chọn dịch vụ
    /// </summary>
    public class DichVuItem
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public decimal DonGia { get; set; }
        public bool IsCombo { get; set; }
    }
}
