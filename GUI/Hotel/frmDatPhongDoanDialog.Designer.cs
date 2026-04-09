namespace GUI
{
    partial class frmDatPhongDoanDialog
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
            this.pnlBackground = new Guna.UI2.WinForms.Guna2Panel();
            this.txtSearchBooking = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnTimDoan = new Guna.UI2.WinForms.Guna2Button();
            this.lblTimKetQua = new System.Windows.Forms.Label();
            this.lblSection1 = new System.Windows.Forms.Label();
            this.lblDoan = new System.Windows.Forms.Label();
            this.txtTenDoan = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNguoiDaiDien = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblSdt = new System.Windows.Forms.Label();
            this.txtSdt = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblCheckIn = new System.Windows.Forms.Label();
            this.dtpCheckIn = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblCheckOut = new System.Windows.Forms.Label();
            this.dtpCheckOut = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblNights = new System.Windows.Forms.Label();
            this.lblSection2 = new System.Windows.Forms.Label();
            this.dgvRooms = new DevExpress.XtraGrid.GridControl();
            this.gridViewRooms = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblSection3 = new System.Windows.Forms.Label();
            this.lblTitleRoomTotal = new System.Windows.Forms.Label();
            this.lblTotalRoom = new System.Windows.Forms.Label();
            this.lblTitleChietKhau = new System.Windows.Forms.Label();
            this.numChietKhau = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.lblGiamGia = new System.Windows.Forms.Label();
            this.lblTitleFinal = new System.Windows.Forms.Label();
            this.lblFinalTotal = new System.Windows.Forms.Label();
            this.lblTitleCoc = new System.Windows.Forms.Label();
            this.txtTienCoc = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTitlePT = new System.Windows.Forms.Label();
            this.rbTienMat = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rbChuyenKhoan = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rbViRFID = new Guna.UI2.WinForms.Guna2RadioButton();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.pnlBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRooms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChietKhau)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBackground
            // 
            this.pnlBackground.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlBackground.BorderThickness = 1;
            this.pnlBackground.Controls.Add(this.txtSearchBooking);
            this.pnlBackground.Controls.Add(this.btnTimDoan);
            this.pnlBackground.Controls.Add(this.lblTimKetQua);
            this.pnlBackground.Controls.Add(this.lblSection1);
            this.pnlBackground.Controls.Add(this.lblDoan);
            this.pnlBackground.Controls.Add(this.txtTenDoan);
            this.pnlBackground.Controls.Add(this.txtNguoiDaiDien);
            this.pnlBackground.Controls.Add(this.lblSdt);
            this.pnlBackground.Controls.Add(this.txtSdt);
            this.pnlBackground.Controls.Add(this.lblCheckIn);
            this.pnlBackground.Controls.Add(this.dtpCheckIn);
            this.pnlBackground.Controls.Add(this.lblCheckOut);
            this.pnlBackground.Controls.Add(this.dtpCheckOut);
            this.pnlBackground.Controls.Add(this.lblNights);
            this.pnlBackground.Controls.Add(this.lblSection2);
            this.pnlBackground.Controls.Add(this.dgvRooms);
            this.pnlBackground.Controls.Add(this.lblSection3);
            this.pnlBackground.Controls.Add(this.lblTitleRoomTotal);
            this.pnlBackground.Controls.Add(this.lblTotalRoom);
            this.pnlBackground.Controls.Add(this.lblTitleChietKhau);
            this.pnlBackground.Controls.Add(this.numChietKhau);
            this.pnlBackground.Controls.Add(this.lblGiamGia);
            this.pnlBackground.Controls.Add(this.lblTitleFinal);
            this.pnlBackground.Controls.Add(this.lblFinalTotal);
            this.pnlBackground.Controls.Add(this.lblTitleCoc);
            this.pnlBackground.Controls.Add(this.txtTienCoc);
            this.pnlBackground.Controls.Add(this.lblTitlePT);
            this.pnlBackground.Controls.Add(this.rbTienMat);
            this.pnlBackground.Controls.Add(this.rbChuyenKhoan);
            this.pnlBackground.Controls.Add(this.rbViRFID);
            this.pnlBackground.Controls.Add(this.btnCancel);
            this.pnlBackground.Controls.Add(this.btnSave);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Location = new System.Drawing.Point(0, 0);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(680, 700);
            this.pnlBackground.TabIndex = 0;
            // 
            // txtSearchBooking
            // 
            this.txtSearchBooking.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.txtSearchBooking.BorderRadius = 4;
            this.txtSearchBooking.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearchBooking.DefaultText = "";
            this.txtSearchBooking.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearchBooking.ForeColor = System.Drawing.Color.Black;
            this.txtSearchBooking.Location = new System.Drawing.Point(20, 15);
            this.txtSearchBooking.Name = "txtSearchBooking";
            this.txtSearchBooking.PlaceholderText = "Nhập BK-xxx để liên kết đoàn đã đặt (hoặc bỏ trống để tạo mới)...";
            this.txtSearchBooking.SelectedText = "";
            this.txtSearchBooking.Size = new System.Drawing.Size(440, 30);
            this.txtSearchBooking.TabIndex = 0;
            // 
            // btnTimDoan
            // 
            this.btnTimDoan.BorderRadius = 4;
            this.btnTimDoan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimDoan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnTimDoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimDoan.ForeColor = System.Drawing.Color.White;
            this.btnTimDoan.Location = new System.Drawing.Point(465, 15);
            this.btnTimDoan.Name = "btnTimDoan";
            this.btnTimDoan.Size = new System.Drawing.Size(100, 30);
            this.btnTimDoan.TabIndex = 1;
            this.btnTimDoan.Text = "🔍 TÌM ĐOÀN";
            this.btnTimDoan.Click += new System.EventHandler(this.btnTimDoan_Click);
            // 
            // lblTimKetQua
            // 
            this.lblTimKetQua.AutoSize = true;
            this.lblTimKetQua.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblTimKetQua.ForeColor = System.Drawing.Color.Gray;
            this.lblTimKetQua.Location = new System.Drawing.Point(20, 48);
            this.lblTimKetQua.Name = "lblTimKetQua";
            this.lblTimKetQua.Size = new System.Drawing.Size(250, 15);
            this.lblTimKetQua.TabIndex = 31;
            this.lblTimKetQua.Text = "Bỏ trống ô trên -> sẽ tạo đoàn MỚI khi lưu.";
            // 
            // lblSection1
            // 
            this.lblSection1.AutoSize = true;
            this.lblSection1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSection1.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblSection1.Location = new System.Drawing.Point(20, 60);
            this.lblSection1.Name = "lblSection1";
            this.lblSection1.Size = new System.Drawing.Size(204, 17);
            this.lblSection1.TabIndex = 2;
            this.lblSection1.Text = "[ THÔNG TIN ĐẠI DIỆN ĐOÀN ]";
            // 
            // lblDoan
            // 
            this.lblDoan.AutoSize = true;
            this.lblDoan.Location = new System.Drawing.Point(20, 90);
            this.lblDoan.Name = "lblDoan";
            this.lblDoan.Size = new System.Drawing.Size(120, 17);
            this.lblDoan.TabIndex = 3;
            this.lblDoan.Text = "Tên Công ty/Đoàn :";
            // 
            // txtTenDoan
            // 
            this.txtTenDoan.BorderColor = System.Drawing.Color.LightGray;
            this.txtTenDoan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDoan.DefaultText = "";
            this.txtTenDoan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenDoan.ForeColor = System.Drawing.Color.Black;
            this.txtTenDoan.Location = new System.Drawing.Point(160, 85);
            this.txtTenDoan.Name = "txtTenDoan";
            this.txtTenDoan.PlaceholderText = "Tên công ty/đoàn...";
            this.txtTenDoan.SelectedText = "";
            this.txtTenDoan.Size = new System.Drawing.Size(200, 28);
            this.txtTenDoan.TabIndex = 4;
            // 
            // txtNguoiDaiDien
            // 
            this.txtNguoiDaiDien.BorderColor = System.Drawing.Color.LightGray;
            this.txtNguoiDaiDien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNguoiDaiDien.DefaultText = "";
            this.txtNguoiDaiDien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNguoiDaiDien.ForeColor = System.Drawing.Color.Black;
            this.txtNguoiDaiDien.Location = new System.Drawing.Point(370, 85);
            this.txtNguoiDaiDien.Name = "txtNguoiDaiDien";
            this.txtNguoiDaiDien.PlaceholderText = "Trưởng đoàn...";
            this.txtNguoiDaiDien.SelectedText = "";
            this.txtNguoiDaiDien.Size = new System.Drawing.Size(200, 28);
            this.txtNguoiDaiDien.TabIndex = 5;
            // 
            // lblSdt
            // 
            this.lblSdt.AutoSize = true;
            this.lblSdt.Location = new System.Drawing.Point(20, 125);
            this.lblSdt.Name = "lblSdt";
            this.lblSdt.Size = new System.Drawing.Size(128, 17);
            this.lblSdt.TabIndex = 6;
            this.lblSdt.Text = "Số điện thoại          :";
            // 
            // txtSdt
            // 
            this.txtSdt.BorderColor = System.Drawing.Color.LightGray;
            this.txtSdt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSdt.DefaultText = "";
            this.txtSdt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSdt.ForeColor = System.Drawing.Color.Black;
            this.txtSdt.Location = new System.Drawing.Point(160, 120);
            this.txtSdt.Name = "txtSdt";
            this.txtSdt.PlaceholderText = "";
            this.txtSdt.SelectedText = "";
            this.txtSdt.Size = new System.Drawing.Size(200, 28);
            this.txtSdt.TabIndex = 7;
            // 
            // lblCheckIn
            // 
            this.lblCheckIn.AutoSize = true;
            this.lblCheckIn.Location = new System.Drawing.Point(20, 160);
            this.lblCheckIn.Name = "lblCheckIn";
            this.lblCheckIn.Size = new System.Drawing.Size(119, 17);
            this.lblCheckIn.TabIndex = 8;
            this.lblCheckIn.Text = "Ngày Check-in      :";
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Checked = true;
            this.dtpCheckIn.CustomFormat = "HH:mm - dd/MM/yyyy";
            this.dtpCheckIn.FillColor = System.Drawing.Color.White;
            this.dtpCheckIn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpCheckIn.ForeColor = System.Drawing.Color.Black;
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckIn.Location = new System.Drawing.Point(160, 155);
            this.dtpCheckIn.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpCheckIn.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(200, 28);
            this.dtpCheckIn.TabIndex = 9;
            this.dtpCheckIn.Value = new System.DateTime(2026, 4, 4, 5, 55, 42, 229);
            this.dtpCheckIn.ValueChanged += new System.EventHandler(this.dtpCheckIn_ValueChanged);
            // 
            // lblCheckOut
            // 
            this.lblCheckOut.AutoSize = true;
            this.lblCheckOut.Location = new System.Drawing.Point(20, 195);
            this.lblCheckOut.Name = "lblCheckOut";
            this.lblCheckOut.Size = new System.Drawing.Size(120, 17);
            this.lblCheckOut.TabIndex = 10;
            this.lblCheckOut.Text = "Ngày Check-out    :";
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Checked = true;
            this.dtpCheckOut.CustomFormat = "HH:mm - dd/MM/yyyy";
            this.dtpCheckOut.FillColor = System.Drawing.Color.White;
            this.dtpCheckOut.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpCheckOut.ForeColor = System.Drawing.Color.Black;
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckOut.Location = new System.Drawing.Point(160, 190);
            this.dtpCheckOut.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpCheckOut.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(200, 28);
            this.dtpCheckOut.TabIndex = 11;
            this.dtpCheckOut.Value = new System.DateTime(2026, 4, 4, 5, 55, 42, 317);
            this.dtpCheckOut.ValueChanged += new System.EventHandler(this.dtpCheckIn_ValueChanged);
            // 
            // lblNights
            // 
            this.lblNights.AutoSize = true;
            this.lblNights.Location = new System.Drawing.Point(370, 195);
            this.lblNights.Name = "lblNights";
            this.lblNights.Size = new System.Drawing.Size(90, 17);
            this.lblNights.TabIndex = 12;
            this.lblNights.Text = "(Tổng: 1 đêm)";
            // 
            // lblSection2
            // 
            this.lblSection2.AutoSize = true;
            this.lblSection2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSection2.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblSection2.Location = new System.Drawing.Point(20, 240);
            this.lblSection2.Name = "lblSection2";
            this.lblSection2.Size = new System.Drawing.Size(326, 17);
            this.lblSection2.TabIndex = 13;
            this.lblSection2.Text = "[ DANH SÁCH PHÒNG ĐÃ CHỌN (ROOMING LIST) ]";
            // 
            // dgvRooms
            // 
            this.dgvRooms.Location = new System.Drawing.Point(20, 270);
            this.dgvRooms.MainView = this.gridViewRooms;
            this.dgvRooms.Name = "dgvRooms";
            this.dgvRooms.Size = new System.Drawing.Size(640, 130);
            this.dgvRooms.TabIndex = 14;
            this.dgvRooms.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRooms});
            // 
            // gridViewRooms
            // 
            this.gridViewRooms.GridControl = this.dgvRooms;
            this.gridViewRooms.Name = "gridViewRooms";
            this.gridViewRooms.OptionsBehavior.Editable = false;
            this.gridViewRooms.OptionsView.ShowGroupPanel = false;
            // 
            // lblSection3
            // 
            this.lblSection3.AutoSize = true;
            this.lblSection3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSection3.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblSection3.Location = new System.Drawing.Point(20, 420);
            this.lblSection3.Name = "lblSection3";
            this.lblSection3.Size = new System.Drawing.Size(209, 17);
            this.lblSection3.TabIndex = 15;
            this.lblSection3.Text = "[ THANH TOÁN (MASTER BILL) ]";
            // 
            // lblTitleRoomTotal
            // 
            this.lblTitleRoomTotal.AutoSize = true;
            this.lblTitleRoomTotal.Location = new System.Drawing.Point(20, 450);
            this.lblTitleRoomTotal.Name = "lblTitleRoomTotal";
            this.lblTitleRoomTotal.Size = new System.Drawing.Size(138, 17);
            this.lblTitleRoomTotal.TabIndex = 16;
            this.lblTitleRoomTotal.Text = "💵 Tổng tiền phòng  :";
            // 
            // lblTotalRoom
            // 
            this.lblTotalRoom.AutoSize = true;
            this.lblTotalRoom.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTotalRoom.ForeColor = System.Drawing.Color.Crimson;
            this.lblTotalRoom.Location = new System.Drawing.Point(160, 450);
            this.lblTotalRoom.Name = "lblTotalRoom";
            this.lblTotalRoom.Size = new System.Drawing.Size(27, 17);
            this.lblTotalRoom.TabIndex = 17;
            this.lblTotalRoom.Text = "0 đ";
            // 
            // lblTitleChietKhau
            // 
            this.lblTitleChietKhau.AutoSize = true;
            this.lblTitleChietKhau.Location = new System.Drawing.Point(20, 480);
            this.lblTitleChietKhau.Name = "lblTitleChietKhau";
            this.lblTitleChietKhau.Size = new System.Drawing.Size(132, 17);
            this.lblTitleChietKhau.TabIndex = 18;
            this.lblTitleChietKhau.Text = "🏷️ Chiết khấu Đoàn :";
            // 
            // numChietKhau
            // 
            this.numChietKhau.BackColor = System.Drawing.Color.Transparent;
            this.numChietKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numChietKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numChietKhau.Location = new System.Drawing.Point(160, 475);
            this.numChietKhau.Name = "numChietKhau";
            this.numChietKhau.Size = new System.Drawing.Size(60, 26);
            this.numChietKhau.TabIndex = 19;
            this.numChietKhau.ValueChanged += new System.EventHandler(this.numChietKhau_ValueChanged);
            // 
            // lblGiamGia
            // 
            this.lblGiamGia.AutoSize = true;
            this.lblGiamGia.Location = new System.Drawing.Point(230, 480);
            this.lblGiamGia.Name = "lblGiamGia";
            this.lblGiamGia.Size = new System.Drawing.Size(86, 17);
            this.lblGiamGia.TabIndex = 20;
            this.lblGiamGia.Text = "=> Giảm: 0 đ";
            // 
            // lblTitleFinal
            // 
            this.lblTitleFinal.AutoSize = true;
            this.lblTitleFinal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitleFinal.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblTitleFinal.Location = new System.Drawing.Point(20, 515);
            this.lblTitleFinal.Name = "lblTitleFinal";
            this.lblTitleFinal.Size = new System.Drawing.Size(208, 20);
            this.lblTitleFinal.TabIndex = 21;
            this.lblTitleFinal.Text = "💰 TỔNG CẦN KHÁCH TRẢ :";
            // 
            // lblFinalTotal
            // 
            this.lblFinalTotal.AutoSize = true;
            this.lblFinalTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblFinalTotal.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblFinalTotal.Location = new System.Drawing.Point(210, 515);
            this.lblFinalTotal.Name = "lblFinalTotal";
            this.lblFinalTotal.Size = new System.Drawing.Size(31, 20);
            this.lblFinalTotal.TabIndex = 22;
            this.lblFinalTotal.Text = "0 đ";
            // 
            // lblTitleCoc
            // 
            this.lblTitleCoc.AutoSize = true;
            this.lblTitleCoc.Location = new System.Drawing.Point(20, 550);
            this.lblTitleCoc.Name = "lblTitleCoc";
            this.lblTitleCoc.Size = new System.Drawing.Size(144, 17);
            this.lblTitleCoc.TabIndex = 23;
            this.lblTitleCoc.Text = "🔒 ĐẶT CỌC GIỮ CHỖ :";
            // 
            // txtTienCoc
            // 
            this.txtTienCoc.BackColor = System.Drawing.Color.Transparent;
            this.txtTienCoc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTienCoc.DefaultText = "";
            this.txtTienCoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTienCoc.Location = new System.Drawing.Point(165, 545);
            this.txtTienCoc.Name = "txtTienCoc";
            this.txtTienCoc.PlaceholderText = "";
            this.txtTienCoc.SelectedText = "";
            this.txtTienCoc.Size = new System.Drawing.Size(120, 26);
            this.txtTienCoc.TabIndex = 24;
            // 
            // lblTitlePT
            // 
            this.lblTitlePT.AutoSize = true;
            this.lblTitlePT.Location = new System.Drawing.Point(20, 580);
            this.lblTitlePT.Name = "lblTitlePT";
            this.lblTitlePT.Size = new System.Drawing.Size(117, 17);
            this.lblTitlePT.TabIndex = 25;
            this.lblTitlePT.Text = "Phương thức cọc  :";
            // 
            // rbTienMat
            // 
            this.rbTienMat.AutoSize = true;
            this.rbTienMat.Checked = true;
            this.rbTienMat.CheckedState.BorderThickness = 0;
            this.rbTienMat.Location = new System.Drawing.Point(165, 580);
            this.rbTienMat.Name = "rbTienMat";
            this.rbTienMat.Size = new System.Drawing.Size(76, 21);
            this.rbTienMat.TabIndex = 26;
            this.rbTienMat.TabStop = true;
            this.rbTienMat.Text = "Tiền mặt";
            this.rbTienMat.UncheckedState.BorderThickness = 0;
            // 
            // rbChuyenKhoan
            // 
            this.rbChuyenKhoan.AutoSize = true;
            this.rbChuyenKhoan.CheckedState.BorderThickness = 0;
            this.rbChuyenKhoan.Location = new System.Drawing.Point(260, 580);
            this.rbChuyenKhoan.Name = "rbChuyenKhoan";
            this.rbChuyenKhoan.Size = new System.Drawing.Size(107, 21);
            this.rbChuyenKhoan.TabIndex = 27;
            this.rbChuyenKhoan.Text = "Chuyển khoản";
            this.rbChuyenKhoan.UncheckedState.BorderThickness = 0;
            // 
            // rbViRFID
            // 
            this.rbViRFID.AutoSize = true;
            this.rbViRFID.CheckedState.BorderThickness = 0;
            this.rbViRFID.Location = new System.Drawing.Point(380, 580);
            this.rbViRFID.Name = "rbViRFID";
            this.rbViRFID.Size = new System.Drawing.Size(67, 21);
            this.rbViRFID.TabIndex = 28;
            this.rbViRFID.Text = "Ví RFID";
            this.rbViRFID.UncheckedState.BorderThickness = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BorderRadius = 4;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(76, 610);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 30);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "HỦY BỎ (ESC)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BorderRadius = 4;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(391, 610);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(250, 30);
            this.btnSave.TabIndex = 30;
            this.btnSave.Text = "LƯU && XUẤT PHIẾU THU (F9)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmDatPhongDoanDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(680, 700);
            this.Controls.Add(this.pnlBackground);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmDatPhongDoanDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.pnlBackground.ResumeLayout(false);
            this.pnlBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRooms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChietKhau)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlBackground;
        
        private System.Windows.Forms.Label lblSection1;
        private System.Windows.Forms.Label lblDoan;
        private Guna.UI2.WinForms.Guna2TextBox txtTenDoan;
        private Guna.UI2.WinForms.Guna2TextBox txtNguoiDaiDien;
        private System.Windows.Forms.Label lblSdt;
        private Guna.UI2.WinForms.Guna2TextBox txtSdt;
        private System.Windows.Forms.Label lblCheckIn;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpCheckIn;
        private System.Windows.Forms.Label lblCheckOut;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpCheckOut;
        private System.Windows.Forms.Label lblNights;

        private System.Windows.Forms.Label lblSection2;
        private DevExpress.XtraGrid.GridControl dgvRooms;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRooms;

        private System.Windows.Forms.Label lblSection3;
        private System.Windows.Forms.Label lblTitleRoomTotal;
        private System.Windows.Forms.Label lblTotalRoom;
        private System.Windows.Forms.Label lblTitleChietKhau;
        private Guna.UI2.WinForms.Guna2NumericUpDown numChietKhau;
        private System.Windows.Forms.Label lblGiamGia;
        private System.Windows.Forms.Label lblTitleFinal;
        private System.Windows.Forms.Label lblFinalTotal;
        
        private System.Windows.Forms.Label lblTitleCoc;
        private Guna.UI2.WinForms.Guna2TextBox txtTienCoc;
        private System.Windows.Forms.Label lblTitlePT;
        private Guna.UI2.WinForms.Guna2RadioButton rbTienMat;
        private Guna.UI2.WinForms.Guna2RadioButton rbChuyenKhoan;
        private Guna.UI2.WinForms.Guna2RadioButton rbViRFID;

        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnSave;

        private Guna.UI2.WinForms.Guna2TextBox txtSearchBooking;
        private Guna.UI2.WinForms.Guna2Button btnTimDoan;
        private System.Windows.Forms.Label lblTimKetQua;
    }
}
