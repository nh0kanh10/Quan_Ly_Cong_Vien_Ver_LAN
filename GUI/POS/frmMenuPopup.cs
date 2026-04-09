using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using DevExpress.XtraGrid.Views.Tile;

namespace GUI
{
    public partial class frmMenuPopup : Form
    {
        private List<ET_SanPham> _menuCache;
        
        public class CartItem
        {
            public int ProductId { get; set; }
            public string TenMon { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Total => Quantity * Price;
        }

        public List<CartItem> SelectedItems { get; private set; } = new List<CartItem>();

        public frmMenuPopup(List<ET_SanPham> menuCache)
        {
            InitializeComponent();
            this.Text = "MENU MÓN ĂN";

            _menuCache = menuCache ?? new List<ET_SanPham>();

            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridMenu);

            SetupCartUI();

            txtTimMon.TextChanged += (s, e) => RenderMenu(txtTimMon.Text);
            
            // Allow Escape to cancel
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) { DialogResult = DialogResult.Cancel; Close(); } };

            tileViewMenu.ItemDoubleClick += tileViewMenu_ItemDoubleClick;
            // Allow single click for faster UX
            tileViewMenu.ItemClick += tileViewMenu_ItemClick;

            RenderMenu("");
            this.Shown += (s, e) => txtTimMon.Focus();
        }

        private void SetupCartUI()
        {
            // GridView Settings
            gridViewCart.OptionsView.ShowGroupPanel = false;
            gridCart.DataSource = SelectedItems;

            ThemeManager.StyleDevExpressGrid(gridCart);
            
            gridViewCart.OptionsBehavior.Editable = true; 

            gridViewCart.CellValueChanged += GridViewCart_CellValueChanged;
            
            // Add delete button column
            var colDel = new DevExpress.XtraGrid.Columns.GridColumn();
            colDel.FieldName = "DeleteBtn";
            colDel.Caption = "";
            colDel.Width = 35;
            colDel.MaxWidth = 35;
            colDel.MinWidth = 35;
            colDel.UnboundType = DevExpress.Data.UnboundColumnType.String;
            colDel.OptionsColumn.AllowEdit = true;
            colDel.VisibleIndex = 0;

            var btnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            btnEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            btnEdit.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            btnEdit.Buttons[0].Caption = "X";
            btnEdit.Buttons[0].Appearance.ForeColor = Color.FromArgb(239, 68, 68);
            btnEdit.ButtonClick += (s, ev) =>
            {
                var row = gridViewCart.GetFocusedRow() as CartItem;
                if (row != null)
                {
                    SelectedItems.Remove(row);
                    RefreshCart();
                }
            };
            colDel.ColumnEdit = btnEdit;
            gridCart.RepositoryItems.Add(btnEdit);
            gridViewCart.Columns.Add(colDel);

            // Bind Event
            btnXacNhan.Click += (s, e) => { DialogResult = DialogResult.OK; Close(); };
        }

        private void GridViewCart_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Quantity")
            {
                gridViewCart.RefreshData();
                RefreshCart();
            }
        }

        private void RefreshCart()
        {
            gridCart.RefreshDataSource();
            lblCartTitle.Text = string.Format("CÁC MÓN ĐÃ CHỌN ({0})", SelectedItems.Sum(x => x.Quantity));
            
            if (gridViewCart.Columns["ProductId"] != null) gridViewCart.Columns["ProductId"].Visible = false;
            if (gridViewCart.Columns["Price"] != null) gridViewCart.Columns["Price"].Visible = false;
            
            if (gridViewCart.Columns["TenMon"] != null) 
            {
                gridViewCart.Columns["TenMon"].Caption = "Tên món";
                gridViewCart.Columns["TenMon"].OptionsColumn.AllowEdit = false;
            }
            if (gridViewCart.Columns["Quantity"] != null) 
            {
                gridViewCart.Columns["Quantity"].Caption = "SL";
                gridViewCart.Columns["Quantity"].Width = 50;
                gridViewCart.Columns["Quantity"].MaxWidth = 60;
            }
            if (gridViewCart.Columns["Total"] != null) 
            {
                gridViewCart.Columns["Total"].Caption = "Thành tiền";
                gridViewCart.Columns["Total"].DisplayFormat.FormatString = "N0";
                gridViewCart.Columns["Total"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewCart.Columns["Total"].OptionsColumn.AllowEdit = false;
            }
        }

        private class MenuDisplayItem
        {
            public int Id { get; set; }
            public string Ten { get; set; }
            public decimal DonGia { get; set; }
            public string DonGiaText { get; set; }
        }

        private void RenderMenu(string keyword)
        {
            var filtered = _menuCache.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string kw = keyword.ToLower();
                filtered = filtered.Where(x =>
                    (x.Ten != null && x.Ten.ToLower().Contains(kw)) ||
                    (x.MaCode != null && x.MaCode.ToLower().Contains(kw)));
            }

            var displayList = filtered.Select(sp => new MenuDisplayItem
            {
                Id = sp.Id,
                Ten = sp.Ten,
                DonGia = sp.DonGia,
                DonGiaText = string.Format("{0:N0}đ", sp.DonGia)
            }).ToList();

            gridMenu.DataSource = displayList;
        }

        private void tileViewMenu_ItemClick(object sender, TileViewItemClickEventArgs e)
        {
            // Just select the item immediately on single click for faster UX
            AddItemToCart(e);
        }

        private void tileViewMenu_ItemDoubleClick(object sender, TileViewItemClickEventArgs e)
        {
             // AddItemToCart(e); 
        }

        private void AddItemToCart(TileViewItemClickEventArgs e)
        {
            var view = tileViewMenu;
            int pId = Convert.ToInt32(view.GetRowCellValue(e.Item.RowHandle, "Id"));
            string ten = view.GetRowCellValue(e.Item.RowHandle, "Ten").ToString();
            decimal price = Convert.ToDecimal(view.GetRowCellValue(e.Item.RowHandle, "DonGia"));

            var existing = SelectedItems.FirstOrDefault(x => x.ProductId == pId);
            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                SelectedItems.Add(new CartItem
                {
                    ProductId = pId,
                    TenMon = ten,
                    Price = price,
                    Quantity = 1
                });
            }
            RefreshCart();
        }
    }
}
