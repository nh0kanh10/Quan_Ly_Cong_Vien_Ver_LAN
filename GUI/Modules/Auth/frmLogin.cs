using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GUI.Infrastructure;
using BUS.Services.HeThong;

namespace GUI.Modules.Auth
{
    public partial class frmLogin : XtraForm
    {
        private bool _isInitializing = true;

        public frmLogin()
        {
            InitializeComponent();
            ApplyStyle();
            LoadSettings();
            DichNgonNgu();
            AppStyle.FixEditorForeColor(this);
            _isInitializing = false;
        }

        #region Style & Init

        private void ApplyStyle()
        {
            lblTitle.ForeColor = AppStyle.Teal;
            lblSubtitle.ForeColor = AppStyle.TextMuted;
            AppStyle.StyleBtnPrimary(btnLogin);
            AppStyle.StyleBtnOutline(btnExit, AppStyle.Navy);
            cboLanguage.Properties.Items.Add(new ImageComboBoxItem("Tiếng Việt", "vi-VN", -1));
            cboLanguage.Properties.Items.Add(new ImageComboBoxItem("English", "en-US", -1));
            cboLanguage.Properties.Items.Add(new ImageComboBoxItem("中文", "zh-CN", -1));
        }

        private void LoadSettings()
        {
            string savedLang = "vi-VN";
            string savedUser = "";
            bool remember = false;

            try
            {
                if (System.IO.File.Exists("login.cfg"))
                {
                    var lines = System.IO.File.ReadAllLines("login.cfg");
                    if (lines.Length >= 3)
                    {
                        savedLang = lines[0];
                        savedUser = lines[1];
                        remember = lines[2] == "1";
                    }
                }
            }
            catch { }

            GUI.Infrastructure.SessionManager.CurrentLanguage = savedLang;

            int langIndex = 0;
            if (savedLang == "en-US") langIndex = 1;
            else if (savedLang == "zh-CN") langIndex = 2;
            cboLanguage.SelectedIndex = langIndex;

            chkGhiNho.Checked = remember;
            if (chkGhiNho.Checked)
            {
                txtUsername.Text = savedUser;
                txtPassword.Focus();
            }
        }

        private void DichNgonNgu()
        {
            Text = LanguageManager.GetString("FRM_LOGIN_TITLE") ?? "Đăng nhập hệ thống";
            lblTitle.Text = LanguageManager.GetString("LBL_LOGIN_BRAND") ?? "ĐẠI NAM RESORT";
            lblSubtitle.Text = LanguageManager.GetString("LBL_LOGIN_SUBTITLE") ?? "Hệ thống quản lý khu du lịch";
            lblUsername.Text = LanguageManager.GetString("LBL_USERNAME") ?? "Tên đăng nhập";
            lblPassword.Text = LanguageManager.GetString("LBL_PASSWORD") ?? "Mật khẩu";
            lblLanguage.Text = LanguageManager.GetString("LBL_LANGUAGE") ?? "Ngôn ngữ";
            chkGhiNho.Text = LanguageManager.GetString("CHK_REMEMBER_USER") ?? "Nhớ tên đăng nhập";
            btnLogin.Text = LanguageManager.GetString("BTN_LOGIN") ?? "Đăng nhập";
            btnExit.Text = LanguageManager.GetString("BTN_EXIT") ?? "Thoát";
        }

        #endregion

        #region Events

        private void CboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing) return;

            var item = cboLanguage.SelectedItem as ImageComboBoxItem;
            if (item != null)
            {
                string code = item.Value.ToString();
                GUI.Infrastructure.SessionManager.CurrentLanguage = code;
                DichNgonNgu();
                SaveConfig(code, txtUsername.Text.Trim(), chkGhiNho.Checked);
            }
        }

        private void SaveConfig(string lang, string user, bool remember)
        {
            try
            {
                System.IO.File.WriteAllLines("login.cfg", new[] { lang, user, remember ? "1" : "0" });
            }
            catch { }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var result = BUS_Auth.Instance.DangNhap(txtUsername.Text.Trim(), txtPassword.Text);
            if (result.Success)
            {
                var sessionDto = result.Data as ET.DTOs.DTO_Session;
                if (sessionDto != null)
                {
                    SessionManager.DangNhap(sessionDto.IdDoiTac, sessionDto.MaDoiTac, sessionDto.HoTen, sessionDto.LoaiTaiKhoan, sessionDto.DanhSachQuyen);
                }

                string lang = "vi-VN";
                var item = cboLanguage.SelectedItem as ImageComboBoxItem;
                if (item != null) lang = item.Value.ToString();

                SaveConfig(lang, chkGhiNho.Checked ? txtUsername.Text.Trim() : "", chkGhiNho.Checked);

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                UIHelper.Loi(LanguageManager.GetString(result.Message) ?? result.Message);
                txtPassword.Text = string.Empty;
                txtPassword.Focus();
            }
        }

        private void TxtPassword_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                if (txtPassword.Properties.PasswordChar == '*')
                {
                    txtPassword.Properties.PasswordChar = '\0';
                }
                else
                {
                    txtPassword.Properties.PasswordChar = '*';
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        private class ImageComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public int ImageIndex { get; set; }

            public ImageComboBoxItem(string text, object value, int imageIndex)
            {
                Text = text;
                Value = value;
                ImageIndex = imageIndex;
            }
            public override string ToString() => Text;
        }
    }
}
