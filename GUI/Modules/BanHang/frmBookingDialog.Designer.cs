using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace GUI.Modules.BanHang
{
    partial class frmBookingDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.txtKhachHang = new DevExpress.XtraEditors.TextEdit();
            this.txtSDT = new DevExpress.XtraEditors.TextEdit();
            this.slkPhong = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.slkPhongView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dateCheckIn = new DevExpress.XtraEditors.DateEdit();
            this.dateCheckOut = new DevExpress.XtraEditors.DateEdit();
            this.spinTienCoc = new DevExpress.XtraEditors.SpinEdit();
            this.btnCheckIn = new DevExpress.XtraEditors.SimpleButton();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            
            this.layoutKhachHang = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutSDT = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutPhong = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutCheckIn = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutCheckOut = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutTienCoc = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutBtnCheckIn = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutBtnThoat = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();

            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKhachHang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkPhong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkPhongView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCheckIn.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCheckIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCheckOut.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCheckOut.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTienCoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSDT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCheckIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCheckOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTienCoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnCheckIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnThoat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();

            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnThoat);
            this.layoutControl1.Controls.Add(this.btnCheckIn);
            this.layoutControl1.Controls.Add(this.spinTienCoc);
            this.layoutControl1.Controls.Add(this.dateCheckOut);
            this.layoutControl1.Controls.Add(this.dateCheckIn);
            this.layoutControl1.Controls.Add(this.slkPhong);
            this.layoutControl1.Controls.Add(this.txtSDT);
            this.layoutControl1.Controls.Add(this.txtKhachHang);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(480, 260);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";

            // 
            // txtKhachHang
            // 
            this.txtKhachHang.Location = new System.Drawing.Point(100, 12);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.Size = new System.Drawing.Size(368, 20);
            this.txtKhachHang.StyleController = this.layoutControl1;
            this.txtKhachHang.TabIndex = 4;

            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(100, 36);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(368, 20);
            this.txtSDT.StyleController = this.layoutControl1;
            this.txtSDT.TabIndex = 5;

            // 
            // slkPhong
            // 
            this.slkPhong.Location = new System.Drawing.Point(100, 60);
            this.slkPhong.Name = "slkPhong";
            this.slkPhong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slkPhong.Properties.NullText = "Chọn phòng...";
            this.slkPhong.Properties.PopupView = this.slkPhongView;
            this.slkPhong.Size = new System.Drawing.Size(368, 20);
            this.slkPhong.StyleController = this.layoutControl1;
            this.slkPhong.TabIndex = 6;

            // 
            // slkPhongView
            // 
            this.slkPhongView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.slkPhongView.Name = "slkPhongView";
            this.slkPhongView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.slkPhongView.OptionsView.ShowGroupPanel = false;

            // 
            // dateCheckIn
            // 
            this.dateCheckIn.EditValue = null;
            this.dateCheckIn.Location = new System.Drawing.Point(100, 84);
            this.dateCheckIn.Name = "dateCheckIn";
            this.dateCheckIn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateCheckIn.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateCheckIn.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dateCheckIn.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateCheckIn.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dateCheckIn.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateCheckIn.Properties.MaskSettings.Set("mask", "dd/MM/yyyy HH:mm");
            this.dateCheckIn.Size = new System.Drawing.Size(368, 20);
            this.dateCheckIn.StyleController = this.layoutControl1;
            this.dateCheckIn.TabIndex = 7;

            // 
            // dateCheckOut
            // 
            this.dateCheckOut.EditValue = null;
            this.dateCheckOut.Location = new System.Drawing.Point(100, 108);
            this.dateCheckOut.Name = "dateCheckOut";
            this.dateCheckOut.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateCheckOut.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateCheckOut.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dateCheckOut.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateCheckOut.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dateCheckOut.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateCheckOut.Properties.MaskSettings.Set("mask", "dd/MM/yyyy HH:mm");
            this.dateCheckOut.Size = new System.Drawing.Size(368, 20);
            this.dateCheckOut.StyleController = this.layoutControl1;
            this.dateCheckOut.TabIndex = 8;

            // 
            // spinTienCoc
            // 
            this.spinTienCoc.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            this.spinTienCoc.Location = new System.Drawing.Point(100, 132);
            this.spinTienCoc.Name = "spinTienCoc";
            this.spinTienCoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinTienCoc.Properties.DisplayFormat.FormatString = "N0";
            this.spinTienCoc.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTienCoc.Size = new System.Drawing.Size(368, 20);
            this.spinTienCoc.StyleController = this.layoutControl1;
            this.spinTienCoc.TabIndex = 9;

            // 
            // btnCheckIn
            // 
            this.btnCheckIn.Location = new System.Drawing.Point(234, 218);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(120, 30);
            this.btnCheckIn.StyleController = this.layoutControl1;
            this.btnCheckIn.TabIndex = 10;
            this.btnCheckIn.Text = "Nhận Phòng";
            this.btnCheckIn.Click += new System.EventHandler(this.BtnCheckIn_Click);

            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(358, 218);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(110, 30);
            this.btnThoat.StyleController = this.layoutControl1;
            this.btnThoat.TabIndex = 11;
            this.btnThoat.Text = "Hủy bỏ";
            this.btnThoat.Click += new System.EventHandler(this.BtnThoat_Click);

            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutKhachHang,
            this.layoutSDT,
            this.layoutPhong,
            this.layoutCheckIn,
            this.layoutCheckOut,
            this.layoutTienCoc,
            this.emptySpaceItem1,
            this.layoutBtnCheckIn,
            this.layoutBtnThoat});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(480, 260);
            this.Root.TextVisible = false;

            // 
            // layoutKhachHang
            // 
            this.layoutKhachHang.Control = this.txtKhachHang;
            this.layoutKhachHang.Location = new System.Drawing.Point(0, 0);
            this.layoutKhachHang.Name = "layoutKhachHang";
            this.layoutKhachHang.Size = new System.Drawing.Size(460, 24);
            this.layoutKhachHang.Text = "Tên Khách Hàng";
            this.layoutKhachHang.TextSize = new System.Drawing.Size(85, 13);

            // 
            // layoutSDT
            // 
            this.layoutSDT.Control = this.txtSDT;
            this.layoutSDT.Location = new System.Drawing.Point(0, 24);
            this.layoutSDT.Name = "layoutSDT";
            this.layoutSDT.Size = new System.Drawing.Size(460, 24);
            this.layoutSDT.Text = "Số Điện Thoại";
            this.layoutSDT.TextSize = new System.Drawing.Size(85, 13);

            // 
            // layoutPhong
            // 
            this.layoutPhong.Control = this.slkPhong;
            this.layoutPhong.Location = new System.Drawing.Point(0, 48);
            this.layoutPhong.Name = "layoutPhong";
            this.layoutPhong.Size = new System.Drawing.Size(460, 24);
            this.layoutPhong.Text = "Phòng Lựa Chọn";
            this.layoutPhong.TextSize = new System.Drawing.Size(85, 13);

            // 
            // layoutCheckIn
            // 
            this.layoutCheckIn.Control = this.dateCheckIn;
            this.layoutCheckIn.Location = new System.Drawing.Point(0, 72);
            this.layoutCheckIn.Name = "layoutCheckIn";
            this.layoutCheckIn.Size = new System.Drawing.Size(460, 24);
            this.layoutCheckIn.Text = "Giờ Check-in";
            this.layoutCheckIn.TextSize = new System.Drawing.Size(85, 13);

            // 
            // layoutCheckOut
            // 
            this.layoutCheckOut.Control = this.dateCheckOut;
            this.layoutCheckOut.Location = new System.Drawing.Point(0, 96);
            this.layoutCheckOut.Name = "layoutCheckOut";
            this.layoutCheckOut.Size = new System.Drawing.Size(460, 24);
            this.layoutCheckOut.Text = "Giờ Check-out";
            this.layoutCheckOut.TextSize = new System.Drawing.Size(85, 13);

            // 
            // layoutTienCoc
            // 
            this.layoutTienCoc.Control = this.spinTienCoc;
            this.layoutTienCoc.Location = new System.Drawing.Point(0, 120);
            this.layoutTienCoc.Name = "layoutTienCoc";
            this.layoutTienCoc.Size = new System.Drawing.Size(460, 24);
            this.layoutTienCoc.Text = "Tiền Đặt Cọc";
            this.layoutTienCoc.TextSize = new System.Drawing.Size(85, 13);

            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 144);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(460, 62);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);

            // 
            // layoutBtnCheckIn
            // 
            this.layoutBtnCheckIn.Control = this.btnCheckIn;
            this.layoutBtnCheckIn.Location = new System.Drawing.Point(222, 206);
            this.layoutBtnCheckIn.Name = "layoutBtnCheckIn";
            this.layoutBtnCheckIn.Size = new System.Drawing.Size(124, 34);
            this.layoutBtnCheckIn.TextSize = new System.Drawing.Size(0, 0);
            this.layoutBtnCheckIn.TextVisible = false;

            // 
            // layoutBtnThoat
            // 
            this.layoutBtnThoat.Control = this.btnThoat;
            this.layoutBtnThoat.Location = new System.Drawing.Point(346, 206);
            this.layoutBtnThoat.Name = "layoutBtnThoat";
            this.layoutBtnThoat.Size = new System.Drawing.Size(114, 34);
            this.layoutBtnThoat.TextSize = new System.Drawing.Size(0, 0);
            this.layoutBtnThoat.TextVisible = false;

            // 
            // frmBookingDialog
            // 
            this.ClientSize = new System.Drawing.Size(480, 500);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmBookingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhận Phòng / Đặt Phòng";
            this.Load += new System.EventHandler(this.frmBookingDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKhachHang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSDT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkPhong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkPhongView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCheckIn.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCheckIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCheckOut.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCheckOut.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTienCoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSDT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCheckIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCheckOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutTienCoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnCheckIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnThoat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtKhachHang;
        private DevExpress.XtraEditors.TextEdit txtSDT;
        private DevExpress.XtraEditors.SearchLookUpEdit slkPhong;
        private DevExpress.XtraGrid.Views.Grid.GridView slkPhongView;
        private DevExpress.XtraEditors.DateEdit dateCheckIn;
        private DevExpress.XtraEditors.DateEdit dateCheckOut;
        private DevExpress.XtraEditors.SpinEdit spinTienCoc;
        private DevExpress.XtraEditors.SimpleButton btnCheckIn;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraLayout.LayoutControlItem layoutKhachHang;
        private DevExpress.XtraLayout.LayoutControlItem layoutSDT;
        private DevExpress.XtraLayout.LayoutControlItem layoutPhong;
        private DevExpress.XtraLayout.LayoutControlItem layoutCheckIn;
        private DevExpress.XtraLayout.LayoutControlItem layoutCheckOut;
        private DevExpress.XtraLayout.LayoutControlItem layoutTienCoc;
        private DevExpress.XtraLayout.LayoutControlItem layoutBtnCheckIn;
        private DevExpress.XtraLayout.LayoutControlItem layoutBtnThoat;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
