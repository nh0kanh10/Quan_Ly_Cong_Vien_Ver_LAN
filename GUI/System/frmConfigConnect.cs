using System;
using System.Drawing;
using System.Threading.Tasks;
using FontAwesome.Sharp;
using System.Windows.Forms;
using BUS;

namespace GUI
{
    public partial class frmConfigConnect : Form
    {
        public frmConfigConnect()
        {
            InitializeComponent();
            InitIcons();
            ThemeManager.ApplyTheme(this);
        }

        private void InitIcons()
        {
            btnTest.Image = FontAwesome.Sharp.IconChar.Plug.ToBitmap(Color.White, 20);
            btnSave.Image = FontAwesome.Sharp.IconChar.FloppyDisk.ToBitmap(Color.White, 20);
            btnRefreshServer.Image = FontAwesome.Sharp.IconChar.SyncAlt.ToBitmap(Color.White, 14);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string server = cboServer.Text.Trim();
            string database = cboDatabase.Text.Trim();

            if (string.IsNullOrEmpty(server))
            {
                TDCMessageBox.Show("Vui lòng chọn hoặc nhập tên Server!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(database))
            {
                TDCMessageBox.Show("Vui lòng chọn hoặc nhập tên Database!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string result = BUS_Connection.Instance.TestConnection(server, database, txtUser.Text, txtPassword.Text, chkWindowsAuth.Checked);
            if (result == "OK")
            {
                TDCMessageBox.Show("Kết nối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                TDCMessageBox.Show("Kết nối thất bại!\n\nChi tiết lỗi: " + result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string server = cboServer.Text.Trim();
            string database = cboDatabase.Text.Trim();

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(database))
            {
                TDCMessageBox.Show("Server và Database không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BUS_Connection.Instance.SaveConnectionString(server, database, txtUser.Text, txtPassword.Text, chkWindowsAuth.Checked);
            TDCMessageBox.Show("Đã lưu cấu hình. Vui lòng khởi động lại ứng dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void chkWindowsAuth_CheckedChanged(object sender, EventArgs e)
        {
            txtUser.ReadOnly = txtPassword.ReadOnly = chkWindowsAuth.Checked;
            // Reload danh sách DB khi đổi auth mode (vì credentials thay đổi)
            if (!string.IsNullOrEmpty(cboServer.Text.Trim()))
            {
                LoadDatabasesAsync(cboServer.Text.Trim());
            }
        }

        private async void frmConfigConnect_Load(object sender, EventArgs e)
        {
            // Auto-detect SQL Server instances trên máy hiện tại
            await LoadServerInstancesAsync();
        }

        /// <summary>
        /// Lấy danh sách sql server
        /// </summary>
        /// <returns></returns>
        private async Task LoadServerInstancesAsync()
        {
            lblLoading.Text = " Đang quét SQL Server instances...";
            lblLoading.Visible = true;
            cboServer.Items.Clear();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                var instances = await Task.Run(() => BUS_Connection.Instance.GetSqlServerInstances());

                cboServer.Items.Clear();
                foreach (var inst in instances)
                {
                    cboServer.Items.Add(inst);
                }

                if (cboServer.Items.Count > 0)
                {
                    cboServer.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                cboServer.Items.Add($".\\SQLEXPRESS");
                cboServer.SelectedIndex = 0;
                System.Diagnostics.Debug.WriteLine("Error loading SQL instances: " + ex.Message);
            }
            finally
            {
                lblLoading.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Load danh sách databases từ server đã chọn
        /// </summary>
        /// <param name="serverName"></param>
        private async void LoadDatabasesAsync(string serverName)
        {
            lblLoading.Text = " Đang tải danh sách Database...";
            lblLoading.Visible = true;
            cboDatabase.Items.Clear();

            try
            {
                var databases = await Task.Run(() =>
                    BUS_Connection.Instance.GetDatabases(serverName, txtUser.Text, txtPassword.Text, chkWindowsAuth.Checked));

                cboDatabase.Items.Clear();
                foreach (var db in databases)
                {
                    cboDatabase.Items.Add(db);
                }

                for (int i = 0; i < cboDatabase.Items.Count; i++)
                {
                    string dbName = cboDatabase.Items[i].ToString();
                    if (dbName.Equals("QuanLyCongVien", StringComparison.OrdinalIgnoreCase) ||
                        dbName.Equals("DaiNamResort", StringComparison.OrdinalIgnoreCase))
                    {
                        cboDatabase.SelectedIndex = i;
                        break;
                    }
                }

                if (cboDatabase.SelectedIndex < 0 && cboDatabase.Items.Count > 0)
                {
                    cboDatabase.SelectedIndex = 0;
                }

                if (cboDatabase.Items.Count == 0)
                {
                    lblLoading.Text = "Không tìm thấy database nào. Hãy nhập tên DB thủ công.";
                    lblLoading.Visible = true;
                    return;
                }
            }
            catch
            {
                lblLoading.Text = "⚠ Không thể kết nối server để lấy danh sách DB.";
                lblLoading.Visible = true;
                return;
            }

            lblLoading.Visible = false;
        }

        /// <summary>
        /// Khi chọn Server khác -> tự động reload danh sách Database.
        /// </summary>
        private void cboServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedServer = cboServer.Text.Trim();
            if (!string.IsNullOrEmpty(selectedServer))
            {
                LoadDatabasesAsync(selectedServer);
            }
        }

        /// <summary>
        /// Nút refresh để quét lại danh sách server.
        /// </summary>
        private async void btnRefreshServer_Click(object sender, EventArgs e)
        {
            await LoadServerInstancesAsync();
        }
    }
}

