namespace GUI.Modules.HeThong
{
    partial class frmPhanQuyen
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
            this.pnlMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.gbVaiTro = new DevExpress.XtraEditors.GroupControl();
            this.lbVaiTro = new DevExpress.XtraEditors.ListBoxControl();
            this.gbQuyenHan = new DevExpress.XtraEditors.GroupControl();
            this.treeQuyen = new DevExpress.XtraTreeList.TreeList();
            this.colDisplayName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.pnlFooter = new DevExpress.XtraEditors.PanelControl();
            this.btnLamMoi = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel1)).BeginInit();
            this.pnlMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel2)).BeginInit();
            this.pnlMain.Panel2.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbVaiTro)).BeginInit();
            this.gbVaiTro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbVaiTro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbQuyenHan)).BeginInit();
            this.gbQuyenHan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeQuyen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            // 
            // pnlMain.Panel1
            // 
            this.pnlMain.Panel1.Controls.Add(this.gbVaiTro);
            this.pnlMain.Panel1.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMain.Panel1.Text = "Panel1";
            // 
            // pnlMain.Panel2
            // 
            this.pnlMain.Panel2.Controls.Add(this.gbQuyenHan);
            this.pnlMain.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMain.Panel2.Text = "Panel2";
            this.pnlMain.Size = new System.Drawing.Size(1200, 640);
            this.pnlMain.SplitterPosition = 300;
            this.pnlMain.TabIndex = 0;
            // 
            // gbVaiTro
            // 
            this.gbVaiTro.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbVaiTro.AppearanceCaption.Options.UseFont = true;
            this.gbVaiTro.Controls.Add(this.lbVaiTro);
            this.gbVaiTro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbVaiTro.Location = new System.Drawing.Point(10, 10);
            this.gbVaiTro.Name = "gbVaiTro";
            this.gbVaiTro.Size = new System.Drawing.Size(280, 620);
            this.gbVaiTro.TabIndex = 0;
            this.gbVaiTro.Text = "Chọn Vai Trò";
            // 
            // lbVaiTro
            // 
            this.lbVaiTro.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lbVaiTro.Appearance.Options.UseFont = true;
            this.lbVaiTro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbVaiTro.Location = new System.Drawing.Point(2, 23);
            this.lbVaiTro.Name = "lbVaiTro";
            this.lbVaiTro.Size = new System.Drawing.Size(276, 595);
            this.lbVaiTro.TabIndex = 0;
            this.lbVaiTro.SelectedIndexChanged += new System.EventHandler(this.LbVaiTro_SelectedIndexChanged);
            // 
            // gbQuyenHan
            // 
            this.gbQuyenHan.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbQuyenHan.AppearanceCaption.Options.UseFont = true;
            this.gbQuyenHan.Controls.Add(this.treeQuyen);
            this.gbQuyenHan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbQuyenHan.Location = new System.Drawing.Point(10, 10);
            this.gbQuyenHan.Name = "gbQuyenHan";
            this.gbQuyenHan.Size = new System.Drawing.Size(870, 620);
            this.gbQuyenHan.TabIndex = 0;
            this.gbQuyenHan.Text = "Phân quyền (Hierarchy Tree)";
            // 
            // treeQuyen
            // 
            this.treeQuyen.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.treeQuyen.Appearance.Row.Options.UseFont = true;
            this.treeQuyen.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colDisplayName});
            this.treeQuyen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeQuyen.Location = new System.Drawing.Point(2, 23);
            this.treeQuyen.Name = "treeQuyen";
            this.treeQuyen.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.treeQuyen.OptionsBehavior.Editable = false;
            this.treeQuyen.OptionsView.CheckBoxStyle = DevExpress.XtraTreeList.DefaultNodeCheckBoxStyle.Check;
            this.treeQuyen.OptionsView.ShowIndicator = false;
            this.treeQuyen.Size = new System.Drawing.Size(866, 595);
            this.treeQuyen.TabIndex = 0;
            // 
            // colDisplayName
            // 
            this.colDisplayName.Caption = "Danh mục quyền";
            this.colDisplayName.FieldName = "DisplayName";
            this.colDisplayName.Name = "colDisplayName";
            this.colDisplayName.Visible = true;
            this.colDisplayName.VisibleIndex = 0;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnLamMoi);
            this.pnlFooter.Controls.Add(this.btnLuu);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 640);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1200, 60);
            this.pnlFooter.TabIndex = 1;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.Appearance.Options.UseFont = true;
            this.btnLamMoi.Location = new System.Drawing.Point(860, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(150, 40);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "LÀM MỚI";
            this.btnLamMoi.Click += new System.EventHandler(this.BtnLamMoi_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Location = new System.Drawing.Point(1030, 10);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(150, 40);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "LƯU THAY ĐỔI";
            this.btnLuu.Click += new System.EventHandler(this.BtnLuu_Click);
            // 
            // frmPhanQuyen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlFooter);
            this.Name = "frmPhanQuyen";
            this.Text = "A";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel1)).EndInit();
            this.pnlMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel2)).EndInit();
            this.pnlMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbVaiTro)).EndInit();
            this.gbVaiTro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbVaiTro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbQuyenHan)).EndInit();
            this.gbQuyenHan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeQuyen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SplitContainerControl pnlMain;
        private DevExpress.XtraEditors.GroupControl gbVaiTro;
        private DevExpress.XtraEditors.ListBoxControl lbVaiTro;
        private DevExpress.XtraEditors.GroupControl gbQuyenHan;
        private DevExpress.XtraTreeList.TreeList treeQuyen;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDisplayName;
        private DevExpress.XtraEditors.PanelControl pnlFooter;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnLamMoi;
        private System.ComponentModel.IContainer components = null;





    }}