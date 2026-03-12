namespace WindowsFormsApp1
{
    partial class frmKhuVuc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.dgvKhuVuc = new System.Windows.Forms.DataGridView();
            this.pnlTimKiem = new System.Windows.Forms.Panel();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.pnlTopContainer = new System.Windows.Forms.Panel();
            this.gbThongTin = new System.Windows.Forms.GroupBox();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.txtMoTa = new System.Windows.Forms.RichTextBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.dtpNgayTao = new System.Windows.Forms.DateTimePicker();
            this.btnXoa = new System.Windows.Forms.Button();
            this.txtMaCode = new System.Windows.Forms.TextBox();
            this.btnSua = new System.Windows.Forms.Button();
            this.lblMaKV_ID = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtMaKV = new System.Windows.Forms.TextBox();
            this.lblTenKV = new System.Windows.Forms.Label();
            this.txtTenKV = new System.Windows.Forms.TextBox();
            this.lblMaCode = new System.Windows.Forms.Label();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.lblNgayCapNhat = new System.Windows.Forms.Label();
            this.dtpNgayCapNhat = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhuVuc)).BeginInit();
            this.pnlTimKiem.SuspendLayout();
            this.pnlTopContainer.SuspendLayout();
            this.gbThongTin.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvKhuVuc
            // 
            this.dgvKhuVuc.AllowUserToAddRows = false;
            this.dgvKhuVuc.AllowUserToDeleteRows = false;
            this.dgvKhuVuc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhuVuc.BackgroundColor = System.Drawing.Color.White;
            this.dgvKhuVuc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKhuVuc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhuVuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKhuVuc.Location = new System.Drawing.Point(0, 310);
            this.dgvKhuVuc.Name = "dgvKhuVuc";
            this.dgvKhuVuc.ReadOnly = true;
            this.dgvKhuVuc.RowHeadersVisible = false;
            this.dgvKhuVuc.RowHeadersWidth = 51;
            this.dgvKhuVuc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhuVuc.Size = new System.Drawing.Size(1332, 287);
            this.dgvKhuVuc.TabIndex = 3;
            this.dgvKhuVuc.Click += new System.EventHandler(this.dgvKhuVuc_Click);
            // 
            // pnlTimKiem
            // 
            this.pnlTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlTimKiem.Controls.Add(this.txtTimKiem);
            this.pnlTimKiem.Controls.Add(this.btnTimKiem);
            this.pnlTimKiem.Controls.Add(this.lblTimKiem);
            this.pnlTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimKiem.Location = new System.Drawing.Point(0, 260);
            this.pnlTimKiem.Name = "pnlTimKiem";
            this.pnlTimKiem.Padding = new System.Windows.Forms.Padding(8);
            this.pnlTimKiem.Size = new System.Drawing.Size(1332, 50);
            this.pnlTimKiem.TabIndex = 4;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(100, 15);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(400, 22);
            this.txtTimKiem.TabIndex = 0;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(520, 10);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 30);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(20, 18);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(66, 17);
            this.lblTimKiem.TabIndex = 2;
            this.lblTimKiem.Text = "Từ khóa:";
            // 
            // pnlTopContainer
            // 
            this.pnlTopContainer.Controls.Add(this.gbThongTin);
            this.pnlTopContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlTopContainer.Name = "pnlTopContainer";
            this.pnlTopContainer.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTopContainer.Size = new System.Drawing.Size(1332, 260);
            this.pnlTopContainer.TabIndex = 5;
            // 
            // gbThongTin
            // 
            this.gbThongTin.Controls.Add(this.cboTrangThai);
            this.gbThongTin.Controls.Add(this.lblTrangThai);
            this.gbThongTin.Controls.Add(this.btnThoat);
            this.gbThongTin.Controls.Add(this.txtMoTa);
            this.gbThongTin.Controls.Add(this.btnLamMoi);
            this.gbThongTin.Controls.Add(this.dtpNgayTao);
            this.gbThongTin.Controls.Add(this.btnXoa);
            this.gbThongTin.Controls.Add(this.txtMaCode);
            this.gbThongTin.Controls.Add(this.btnSua);
            this.gbThongTin.Controls.Add(this.lblMaKV_ID);
            this.gbThongTin.Controls.Add(this.btnThem);
            this.gbThongTin.Controls.Add(this.txtMaKV);
            this.gbThongTin.Controls.Add(this.lblTenKV);
            this.gbThongTin.Controls.Add(this.txtTenKV);
            this.gbThongTin.Controls.Add(this.lblMaCode);
            this.gbThongTin.Controls.Add(this.lblNgayTao);
            this.gbThongTin.Controls.Add(this.lblMoTa);
            this.gbThongTin.Controls.Add(this.lblNgayCapNhat);
            this.gbThongTin.Controls.Add(this.dtpNgayCapNhat);
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTin.Location = new System.Drawing.Point(10, 10);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(1312, 240);
            this.gbThongTin.TabIndex = 0;
            this.gbThongTin.TabStop = false;
            this.gbThongTin.Text = "Thông tin khu vực";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(180, 140);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(200, 24);
            this.cboTrangThai.TabIndex = 34;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(70, 143);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(77, 17);
            this.lblTrangThai.TabIndex = 33;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(1140, 180);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(120, 35);
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(822, 40);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(250, 140);
            this.txtMoTa.TabIndex = 32;
            this.txtMoTa.Text = "";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(1140, 135);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 35);
            this.btnLamMoi.TabIndex = 0;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // dtpNgayTao
            // 
            this.dtpNgayTao.Enabled = false;
            this.dtpNgayTao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayTao.Location = new System.Drawing.Point(530, 90);
            this.dtpNgayTao.Name = "dtpNgayTao";
            this.dtpNgayTao.Size = new System.Drawing.Size(200, 22);
            this.dtpNgayTao.TabIndex = 31;
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(1140, 90);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(120, 35);
            this.btnXoa.TabIndex = 1;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // txtMaCode
            // 
            this.txtMaCode.Enabled = false;
            this.txtMaCode.Location = new System.Drawing.Point(530, 40);
            this.txtMaCode.Name = "txtMaCode";
            this.txtMaCode.Size = new System.Drawing.Size(200, 22);
            this.txtMaCode.TabIndex = 30;
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.Black;
            this.btnSua.Location = new System.Drawing.Point(1140, 45);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(120, 35);
            this.btnSua.TabIndex = 2;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // lblMaKV_ID
            // 
            this.lblMaKV_ID.AutoSize = true;
            this.lblMaKV_ID.Location = new System.Drawing.Point(70, 43);
            this.lblMaKV_ID.Name = "lblMaKV_ID";
            this.lblMaKV_ID.Size = new System.Drawing.Size(84, 17);
            this.lblMaKV_ID.TabIndex = 0;
            this.lblMaKV_ID.Text = "Mã khu vực:";
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(1140, 5);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(120, 35);
            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtMaKV
            // 
            this.txtMaKV.Enabled = false;
            this.txtMaKV.Location = new System.Drawing.Point(180, 40);
            this.txtMaKV.Name = "txtMaKV";
            this.txtMaKV.Size = new System.Drawing.Size(200, 22);
            this.txtMaKV.TabIndex = 1;
            // 
            // lblTenKV
            // 
            this.lblTenKV.AutoSize = true;
            this.lblTenKV.Location = new System.Drawing.Point(70, 93);
            this.lblTenKV.Name = "lblTenKV";
            this.lblTenKV.Size = new System.Drawing.Size(90, 17);
            this.lblTenKV.TabIndex = 2;
            this.lblTenKV.Text = "Tên khu vực:";
            // 
            // txtTenKV
            // 
            this.txtTenKV.Location = new System.Drawing.Point(180, 90);
            this.txtTenKV.Name = "txtTenKV";
            this.txtTenKV.Size = new System.Drawing.Size(200, 22);
            this.txtTenKV.TabIndex = 3;
            // 
            // lblMaCode
            // 
            this.lblMaCode.AutoSize = true;
            this.lblMaCode.Location = new System.Drawing.Point(420, 43);
            this.lblMaCode.Name = "lblMaCode";
            this.lblMaCode.Size = new System.Drawing.Size(66, 17);
            this.lblMaCode.TabIndex = 4;
            this.lblMaCode.Text = "Mã code:";
            // 
            // lblNgayTao
            // 
            this.lblNgayTao.AutoSize = true;
            this.lblNgayTao.Location = new System.Drawing.Point(420, 93);
            this.lblNgayTao.Name = "lblNgayTao";
            this.lblNgayTao.Size = new System.Drawing.Size(69, 17);
            this.lblNgayTao.TabIndex = 6;
            this.lblNgayTao.Text = "Ngày tạo:";
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(750, 43);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(47, 17);
            this.lblMoTa.TabIndex = 10;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // lblNgayCapNhat
            // 
            this.lblNgayCapNhat.AutoSize = true;
            this.lblNgayCapNhat.Location = new System.Drawing.Point(420, 143);
            this.lblNgayCapNhat.Name = "lblNgayCapNhat";
            this.lblNgayCapNhat.Size = new System.Drawing.Size(104, 17);
            this.lblNgayCapNhat.TabIndex = 12;
            this.lblNgayCapNhat.Text = "Ngày cập nhật:";
            // 
            // dtpNgayCapNhat
            // 
            this.dtpNgayCapNhat.Enabled = false;
            this.dtpNgayCapNhat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayCapNhat.Location = new System.Drawing.Point(530, 140);
            this.dtpNgayCapNhat.Name = "dtpNgayCapNhat";
            this.dtpNgayCapNhat.Size = new System.Drawing.Size(200, 22);
            this.dtpNgayCapNhat.TabIndex = 25;
            // 
            // frmKhuVuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 597);
            this.Controls.Add(this.dgvKhuVuc);
            this.Controls.Add(this.pnlTimKiem);
            this.Controls.Add(this.pnlTopContainer);
            this.Name = "frmKhuVuc";
            this.Text = "Quản lý khu vực";
            this.Load += new System.EventHandler(this.frmKhuVuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhuVuc)).EndInit();
            this.pnlTimKiem.ResumeLayout(false);
            this.pnlTimKiem.PerformLayout();
            this.pnlTopContainer.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvKhuVuc;
        private System.Windows.Forms.Panel pnlTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Panel pnlTopContainer;
        private System.Windows.Forms.GroupBox gbThongTin;
        private System.Windows.Forms.DateTimePicker dtpNgayTao;
        private System.Windows.Forms.TextBox txtMaCode;
        private System.Windows.Forms.Label lblMaKV_ID;
        private System.Windows.Forms.TextBox txtMaKV;
        private System.Windows.Forms.Label lblTenKV;
        private System.Windows.Forms.TextBox txtTenKV;
        private System.Windows.Forms.Label lblMaCode;
        private System.Windows.Forms.Label lblNgayTao;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.Label lblNgayCapNhat;
        private System.Windows.Forms.DateTimePicker dtpNgayCapNhat;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.RichTextBox txtMoTa;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTrangThai;
    }
}