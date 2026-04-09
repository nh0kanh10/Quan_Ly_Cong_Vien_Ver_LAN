using System;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmTemplate_CRUD : Form
    {
        public frmTemplate_CRUD()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
            SetupBasicEvents();
        }

        private void SetupBasicEvents()
        {
            txtTimKiem.TextChanged += (s, e) => gridView.ApplyFindFilter(txtTimKiem.Text);
            
            // Icon initialization
            btnThem.Image = IconHelper.GetBitmap(IconChar.Plus, Color.White, 16);
            btnSua.Image = IconHelper.GetBitmap(IconChar.Edit, Color.White, 16);
            btnXoa.Image = IconHelper.GetBitmap(IconChar.Trash, Color.White, 16);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.Sync, Color.White, 16);
            
            this.Load += (s, e) => LoadData();
        }

        protected virtual void LoadData()
        {
            // Override this in specific forms
        }

        protected virtual void btnThem_Click(object sender, EventArgs e) { }
        protected virtual void btnSua_Click(object sender, EventArgs e) { }
        protected virtual void btnXoa_Click(object sender, EventArgs e) { }
        protected virtual void btnLamMoi_Click(object sender, EventArgs e) { 
            LoadData(); 
        }
        
        protected virtual void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Logic to display details when row changes
        }
    }
}
