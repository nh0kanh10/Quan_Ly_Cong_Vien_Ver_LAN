namespace GUI
{
    partial class frmKyLuatDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblNgayApDung    = new System.Windows.Forms.Label();
            this.dtpNgayApDung    = new DevExpress.XtraEditors.DateEdit();
            this.lblHetHieuLuc    = new System.Windows.Forms.Label();
            this.dtpHetHieuLuc    = new DevExpress.XtraEditors.DateEdit();
            this.lblHinhThuc      = new System.Windows.Forms.Label();
            this.cboHinhThuc      = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblTienTru       = new System.Windows.Forms.Label();
            this.numTienTru       = new System.Windows.Forms.NumericUpDown();
            this.lblNgayDinhChi   = new System.Windows.Forms.Label();
            this.numNgayDinhChi   = new System.Windows.Forms.NumericUpDown();
            this.pnlCanhBao       = new System.Windows.Forms.Panel();
            this.lblCanhBao       = new System.Windows.Forms.Label();
            this.lblMoTa          = new System.Windows.Forms.Label();
            this.txtMoTa          = new System.Windows.Forms.TextBox();
            this.btnLuu           = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy           = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayApDung.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpHetHieuLuc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTienTru)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNgayDinhChi)).BeginInit();
            this.pnlCanhBao.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNgayApDung
            // 
            this.lblNgayApDung.AutoSize  = true;
            this.lblNgayApDung.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayApDung.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblNgayApDung.Location  = new System.Drawing.Point(20, 20);
            this.lblNgayApDung.Name      = "lblNgayApDung";
            this.lblNgayApDung.Text      = "Ngày áp dụng: *";
            // 
            // dtpNgayApDung
            // 
            this.dtpNgayApDung.Location  = new System.Drawing.Point(20, 40);
            this.dtpNgayApDung.Name      = "dtpNgayApDung";
            this.dtpNgayApDung.Size      = new System.Drawing.Size(180, 20);
            this.dtpNgayApDung.TabIndex  = 0;
            this.dtpNgayApDung.DateTime  = System.DateTime.Now;
            // 
            // lblHetHieuLuc
            // 
            this.lblHetHieuLuc.AutoSize  = true;
            this.lblHetHieuLuc.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHetHieuLuc.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblHetHieuLuc.Location  = new System.Drawing.Point(218, 20);
            this.lblHetHieuLuc.Name      = "lblHetHieuLuc";
            this.lblHetHieuLuc.Text      = "Hết hiệu lực:";
            // 
            // dtpHetHieuLuc
            // 
            this.dtpHetHieuLuc.Location  = new System.Drawing.Point(218, 40);
            this.dtpHetHieuLuc.Name      = "dtpHetHieuLuc";
            this.dtpHetHieuLuc.Size      = new System.Drawing.Size(184, 20);
            this.dtpHetHieuLuc.TabIndex  = 1;
            this.dtpHetHieuLuc.DateTime  = System.DateTime.Now.AddYears(1);
            // 
            // lblHinhThuc
            // 
            this.lblHinhThuc.AutoSize  = true;
            this.lblHinhThuc.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHinhThuc.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblHinhThuc.Location  = new System.Drawing.Point(20, 78);
            this.lblHinhThuc.Name      = "lblHinhThuc";
            this.lblHinhThuc.Text      = "Hình thức kỷ luật: *";
            // 
            // cboHinhThuc
            // 
            this.cboHinhThuc.DrawMode      = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboHinhThuc.DropDownStyle  = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHinhThuc.ItemHeight     = 30;
            this.cboHinhThuc.Location       = new System.Drawing.Point(20, 98);
            this.cboHinhThuc.Name           = "cboHinhThuc";
            this.cboHinhThuc.Size           = new System.Drawing.Size(382, 36);
            this.cboHinhThuc.TabIndex       = 2;
            this.cboHinhThuc.SelectedIndexChanged += new System.EventHandler(this.cboHinhThuc_SelectedIndexChanged);
            // 
            // lblTienTru
            // 
            this.lblTienTru.AutoSize  = true;
            this.lblTienTru.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTienTru.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblTienTru.Location  = new System.Drawing.Point(20, 148);
            this.lblTienTru.Name      = "lblTienTru";
            this.lblTienTru.Text      = "Số tiền trừ (VNĐ):";
            // 
            // numTienTru
            // 
            this.numTienTru.Enabled           = false;
            this.numTienTru.Font              = new System.Drawing.Font("Segoe UI", 9.5F);
            this.numTienTru.Location          = new System.Drawing.Point(20, 168);
            this.numTienTru.Maximum           = new decimal(new int[] { 50000000, 0, 0, 0 });
            this.numTienTru.Increment         = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numTienTru.Name              = "numTienTru";
            this.numTienTru.Size              = new System.Drawing.Size(182, 30);
            this.numTienTru.TabIndex          = 3;
            this.numTienTru.ThousandsSeparator = true;
            // 
            // lblNgayDinhChi
            // 
            this.lblNgayDinhChi.AutoSize  = true;
            this.lblNgayDinhChi.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgayDinhChi.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblNgayDinhChi.Location  = new System.Drawing.Point(218, 148);
            this.lblNgayDinhChi.Name      = "lblNgayDinhChi";
            this.lblNgayDinhChi.Text      = "Số ngày đình chỉ (≤15):";
            // 
            // numNgayDinhChi
            // 
            this.numNgayDinhChi.Enabled  = false;
            this.numNgayDinhChi.Font     = new System.Drawing.Font("Segoe UI", 9.5F);
            this.numNgayDinhChi.Location = new System.Drawing.Point(218, 168);
            this.numNgayDinhChi.Maximum  = new decimal(new int[] { 15, 0, 0, 0 });
            this.numNgayDinhChi.Name     = "numNgayDinhChi";
            this.numNgayDinhChi.Size     = new System.Drawing.Size(184, 30);
            this.numNgayDinhChi.TabIndex = 4;
            // 
            // pnlCanhBao
            // 
            this.pnlCanhBao.BackColor = System.Drawing.Color.FromArgb(254, 202, 202);
            this.pnlCanhBao.Controls.Add(this.lblCanhBao);
            this.pnlCanhBao.Location  = new System.Drawing.Point(20, 212);
            this.pnlCanhBao.Name      = "pnlCanhBao";
            this.pnlCanhBao.Size      = new System.Drawing.Size(382, 36);
            this.pnlCanhBao.TabIndex  = 5;
            this.pnlCanhBao.Visible   = false;
            // 
            // lblCanhBao
            // 
            this.lblCanhBao.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblCanhBao.Font      = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblCanhBao.ForeColor = System.Drawing.Color.FromArgb(153, 27, 27);
            this.lblCanhBao.Location  = new System.Drawing.Point(0, 0);
            this.lblCanhBao.Name      = "lblCanhBao";
            this.lblCanhBao.Padding   = new System.Windows.Forms.Padding(6, 6, 0, 0);
            this.lblCanhBao.Size      = new System.Drawing.Size(382, 36);
            this.lblCanhBao.Text      = "⚠️  Sa thải cần họp hội đồng kỷ luật, NLĐ ký xác nhận (Điều 128 BLLĐ 2019)";
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize  = true;
            this.lblMoTa.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblMoTa.Location  = new System.Drawing.Point(20, 260);
            this.lblMoTa.Name      = "lblMoTa";
            this.lblMoTa.Text      = "Lý do / Mô tả vi phạm: *";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Font          = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMoTa.Location      = new System.Drawing.Point(20, 280);
            this.txtMoTa.Multiline     = true;
            this.txtMoTa.Name          = "txtMoTa";
            this.txtMoTa.ScrollBars    = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMoTa.Size          = new System.Drawing.Size(382, 80);
            this.txtMoTa.TabIndex      = 6;
            // 
            // btnLuu
            // 
            this.btnLuu.FillColor  = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnLuu.Font       = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor  = System.Drawing.Color.White;
            this.btnLuu.Location   = new System.Drawing.Point(20, 378);
            this.btnLuu.Name       = "btnLuu";
            this.btnLuu.Size       = new System.Drawing.Size(186, 40);
            this.btnLuu.TabIndex   = 7;
            this.btnLuu.Text       = "⚠️  Lập kỷ luật";
            this.btnLuu.Click     += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.FillColor  = System.Drawing.Color.FromArgb(107, 114, 128);
            this.btnHuy.ForeColor  = System.Drawing.Color.White;
            this.btnHuy.Location   = new System.Drawing.Point(216, 378);
            this.btnHuy.Name       = "btnHuy";
            this.btnHuy.Size       = new System.Drawing.Size(186, 40);
            this.btnHuy.TabIndex   = 8;
            this.btnHuy.Text       = "Hủy";
            this.btnHuy.Click     += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmKyLuatDialog
            // 
            this.AutoScaleMode   = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor       = System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize      = new System.Drawing.Size(424, 436);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblNgayApDung, this.dtpNgayApDung,
                this.lblHetHieuLuc, this.dtpHetHieuLuc,
                this.lblHinhThuc,   this.cboHinhThuc,
                this.lblTienTru,    this.numTienTru,
                this.lblNgayDinhChi, this.numNgayDinhChi,
                this.pnlCanhBao,
                this.lblMoTa,       this.txtMoTa,
                this.btnLuu,        this.btnHuy });
            this.Font            = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.Name            = "frmKyLuatDialog";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Lập biên bản kỷ luật";
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayApDung.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpHetHieuLuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTienTru)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNgayDinhChi)).EndInit();
            this.pnlCanhBao.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label              lblNgayApDung;
        private DevExpress.XtraEditors.DateEdit          dtpNgayApDung;
        private System.Windows.Forms.Label              lblHetHieuLuc;
        private DevExpress.XtraEditors.DateEdit          dtpHetHieuLuc;
        private System.Windows.Forms.Label              lblHinhThuc;
        private Guna.UI2.WinForms.Guna2ComboBox         cboHinhThuc;
        private System.Windows.Forms.Label              lblTienTru;
        private System.Windows.Forms.NumericUpDown       numTienTru;
        private System.Windows.Forms.Label              lblNgayDinhChi;
        private System.Windows.Forms.NumericUpDown       numNgayDinhChi;
        private System.Windows.Forms.Panel              pnlCanhBao;
        private System.Windows.Forms.Label              lblCanhBao;
        private System.Windows.Forms.Label              lblMoTa;
        private System.Windows.Forms.TextBox            txtMoTa;
        private Guna.UI2.WinForms.Guna2Button           btnLuu;
        private Guna.UI2.WinForms.Guna2Button           btnHuy;
    }
}
