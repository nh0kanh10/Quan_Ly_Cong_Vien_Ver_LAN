namespace GUI.Modules.Kho
{
    partial class ucKhoHang_Detail
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.txtMaKho = new DevExpress.XtraEditors.TextEdit();
            this.txtTenKho = new DevExpress.XtraEditors.TextEdit();
            this.slkKhuVuc = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.chkKhoAo = new DevExpress.XtraEditors.CheckEdit();
            this.chkTonAm = new DevExpress.XtraEditors.CheckEdit();
            this.cboTrangThai = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.layoutGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciMaKho = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciTenKho = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciKhuVuc = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciKhoAo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciTonAm = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciTrangThai = new DevExpress.XtraLayout.LayoutControlItem();
            this.pnlFooter = new DevExpress.XtraEditors.PanelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhuVuc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKhoAo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTonAm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrangThai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMaKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTenKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciKhuVuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciKhoAo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTonAm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTrangThai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.txtMaKho);
            this.layoutControl.Controls.Add(this.txtTenKho);
            this.layoutControl.Controls.Add(this.slkKhuVuc);
            this.layoutControl.Controls.Add(this.chkKhoAo);
            this.layoutControl.Controls.Add(this.chkTonAm);
            this.layoutControl.Controls.Add(this.cboTrangThai);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.Root = this.layoutGroup;
            this.layoutControl.Size = new System.Drawing.Size(512, 344);
            this.layoutControl.TabIndex = 0;
            // 
            // txtMaKho
            // 
            this.txtMaKho.Location = new System.Drawing.Point(76, 36);
            this.txtMaKho.Name = "txtMaKho";
            this.txtMaKho.Size = new System.Drawing.Size(421, 20);
            this.txtMaKho.StyleController = this.layoutControl;
            this.txtMaKho.TabIndex = 4;
            this.txtMaKho.EditValueChanged += new System.EventHandler(this.control_EditValueChanged);
            // 
            // txtTenKho
            // 
            this.txtTenKho.Location = new System.Drawing.Point(76, 156);
            this.txtTenKho.Name = "txtTenKho";
            this.txtTenKho.Size = new System.Drawing.Size(421, 20);
            this.txtTenKho.StyleController = this.layoutControl;
            this.txtTenKho.TabIndex = 5;
            this.txtTenKho.EditValueChanged += new System.EventHandler(this.control_EditValueChanged);
            // 
            // slkKhuVuc
            // 
            this.slkKhuVuc.Location = new System.Drawing.Point(76, 132);
            this.slkKhuVuc.Name = "slkKhuVuc";
            this.slkKhuVuc.Size = new System.Drawing.Size(421, 20);
            this.slkKhuVuc.StyleController = this.layoutControl;
            this.slkKhuVuc.TabIndex = 6;
            this.slkKhuVuc.EditValueChanged += new System.EventHandler(this.control_EditValueChanged);
            // 
            // chkKhoAo
            // 
            this.chkKhoAo.Location = new System.Drawing.Point(15, 108);
            this.chkKhoAo.Name = "chkKhoAo";
            this.chkKhoAo.Properties.Caption = "Là kho ảo (chỉ trên sổ sách)";
            this.chkKhoAo.Size = new System.Drawing.Size(482, 20);
            this.chkKhoAo.StyleController = this.layoutControl;
            this.chkKhoAo.TabIndex = 7;
            this.chkKhoAo.EditValueChanged += new System.EventHandler(this.control_EditValueChanged);
            // 
            // chkTonAm
            // 
            this.chkTonAm.Location = new System.Drawing.Point(15, 84);
            this.chkTonAm.Name = "chkTonAm";
            this.chkTonAm.Properties.Caption = "Cho phép tồn âm";
            this.chkTonAm.Size = new System.Drawing.Size(482, 20);
            this.chkTonAm.StyleController = this.layoutControl;
            this.chkTonAm.TabIndex = 8;
            this.chkTonAm.EditValueChanged += new System.EventHandler(this.control_EditValueChanged);
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.Location = new System.Drawing.Point(76, 60);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(421, 20);
            this.cboTrangThai.StyleController = this.layoutControl;
            this.cboTrangThai.TabIndex = 9;
            this.cboTrangThai.EditValueChanged += new System.EventHandler(this.control_EditValueChanged);
            // 
            // layoutGroup
            // 
            this.layoutGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciMaKho,
            this.lciTenKho,
            this.lciKhuVuc,
            this.lciKhoAo,
            this.lciTonAm,
            this.lciTrangThai});
            this.layoutGroup.Name = "layoutGroup";
            this.layoutGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(12, 12, 12, 12);
            this.layoutGroup.Size = new System.Drawing.Size(512, 344);
            // 
            // lciMaKho
            // 
            this.lciMaKho.Control = this.txtMaKho;
            this.lciMaKho.Location = new System.Drawing.Point(0, 0);
            this.lciMaKho.Name = "lciMaKho";
            this.lciMaKho.Size = new System.Drawing.Size(486, 24);
            this.lciMaKho.Text = "Mã kho";
            this.lciMaKho.TextSize = new System.Drawing.Size(49, 13);
            // 
            // lciTenKho
            // 
            this.lciTenKho.Control = this.txtTenKho;
            this.lciTenKho.Location = new System.Drawing.Point(0, 120);
            this.lciTenKho.Name = "lciTenKho";
            this.lciTenKho.Size = new System.Drawing.Size(486, 177);
            this.lciTenKho.Text = "Tên kho";
            this.lciTenKho.TextSize = new System.Drawing.Size(49, 13);
            // 
            // lciKhuVuc
            // 
            this.lciKhuVuc.Control = this.slkKhuVuc;
            this.lciKhuVuc.Location = new System.Drawing.Point(0, 96);
            this.lciKhuVuc.Name = "lciKhuVuc";
            this.lciKhuVuc.Size = new System.Drawing.Size(486, 24);
            this.lciKhuVuc.Text = "Khu vực";
            this.lciKhuVuc.TextSize = new System.Drawing.Size(49, 13);
            // 
            // lciKhoAo
            // 
            this.lciKhoAo.Control = this.chkKhoAo;
            this.lciKhoAo.Location = new System.Drawing.Point(0, 72);
            this.lciKhoAo.Name = "lciKhoAo";
            this.lciKhoAo.Size = new System.Drawing.Size(486, 24);
            this.lciKhoAo.Text = " ";
            this.lciKhoAo.TextSize = new System.Drawing.Size(0, 0);
            this.lciKhoAo.TextVisible = false;
            // 
            // lciTonAm
            // 
            this.lciTonAm.Control = this.chkTonAm;
            this.lciTonAm.Location = new System.Drawing.Point(0, 48);
            this.lciTonAm.Name = "lciTonAm";
            this.lciTonAm.Size = new System.Drawing.Size(486, 24);
            this.lciTonAm.Text = " ";
            this.lciTonAm.TextSize = new System.Drawing.Size(0, 0);
            this.lciTonAm.TextVisible = false;
            // 
            // lciTrangThai
            // 
            this.lciTrangThai.Control = this.cboTrangThai;
            this.lciTrangThai.Location = new System.Drawing.Point(0, 24);
            this.lciTrangThai.Name = "lciTrangThai";
            this.lciTrangThai.Size = new System.Drawing.Size(486, 24);
            this.lciTrangThai.Text = "Trạng thái";
            this.lciTrangThai.TextSize = new System.Drawing.Size(49, 13);
            // 
            // pnlFooter
            // 
            this.pnlFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFooter.Controls.Add(this.btnHuy);
            this.pnlFooter.Controls.Add(this.btnLuu);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 344);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlFooter.Size = new System.Drawing.Size(512, 46);
            this.pnlFooter.TabIndex = 1;
            // 
            // btnHuy
            // 
            this.btnHuy.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnHuy.ImageOptions.ImageUri.Uri = "Cancel";
            this.btnHuy.Location = new System.Drawing.Point(257, 8);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(115, 30);
            this.btnHuy.TabIndex = 0;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLuu.ImageOptions.ImageUri.Uri = "Save";
            this.btnLuu.Location = new System.Drawing.Point(372, 8);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(128, 30);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // ucKhoHang_Detail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl);
            this.Controls.Add(this.pnlFooter);
            this.Name = "ucKhoHang_Detail";
            this.Size = new System.Drawing.Size(512, 390);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMaKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhuVuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKhoAo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTonAm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrangThai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMaKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTenKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciKhuVuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciKhoAo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTonAm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTrangThai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraEditors.TextEdit txtMaKho;
        private DevExpress.XtraEditors.TextEdit txtTenKho;
        private DevExpress.XtraEditors.SearchLookUpEdit slkKhuVuc;
        private DevExpress.XtraEditors.CheckEdit chkKhoAo;
        private DevExpress.XtraEditors.CheckEdit chkTonAm;
        private DevExpress.XtraEditors.ImageComboBoxEdit cboTrangThai;
        private DevExpress.XtraLayout.LayoutControlGroup layoutGroup;
        private DevExpress.XtraLayout.LayoutControlItem lciMaKho;
        private DevExpress.XtraLayout.LayoutControlItem lciTenKho;
        private DevExpress.XtraLayout.LayoutControlItem lciKhuVuc;
        private DevExpress.XtraLayout.LayoutControlItem lciKhoAo;
        private DevExpress.XtraLayout.LayoutControlItem lciTonAm;
        private DevExpress.XtraLayout.LayoutControlItem lciTrangThai;
        private DevExpress.XtraEditors.PanelControl pnlFooter;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private System.ComponentModel.IContainer components = null;





    }}