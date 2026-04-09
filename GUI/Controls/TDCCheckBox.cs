using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.ComponentModel;

namespace GUI
{
    public class TDCCheckBox : UserControl
    {
        private Guna2ToggleSwitch _switch;
        private Label _label;

        public event EventHandler CheckedChanged;

        [Category("TDC Appearance")]
        public bool Checked
        {
            get { return _switch.Checked; }
            set { _switch.Checked = value; }
        }

        [Category("TDC Appearance")]
        public override string Text
        {
            get { return _label.Text; }
            set { 
                _label.Text = value;
                this.Width = _switch.Width + _label.Width + 10;
            }
        }

        public TDCCheckBox()
        {
            this.Size = new Size(150, 25);
            this.BackColor = Color.Transparent;

            // 1. Tạo nút gạt siêu gọn
            _switch = new Guna2ToggleSwitch();
            _switch.Size = new Size(35, 18); // Gọn hơn bản cũ
            _switch.Location = new Point(0, (this.Height - _switch.Height) / 2);
            _switch.CheckedState.FillColor = Color.FromArgb(14, 165, 233);
            _switch.CheckedState.InnerColor = Color.White;
            _switch.UncheckedState.FillColor = Color.FromArgb(203, 213, 225);
            _switch.UncheckedState.InnerColor = Color.White;
            _switch.Cursor = Cursors.Hand;
            _switch.Animated = true;
            _switch.CheckedChanged += (s, e) => { if (CheckedChanged != null) CheckedChanged(this, e); };

            // 2. Tạo Label nằm bên ngoài
            _label = new Label();
            _label.AutoSize = true;
            _label.Location = new Point(_switch.Width + 5, (this.Height - 18) / 2);
            _label.Font = new Font("Segoe UI", 10F);
            _label.ForeColor = Color.FromArgb(30, 41, 59);
            _label.Cursor = Cursors.Hand;
            _label.Click += (s, e) => _switch.Checked = !_switch.Checked;

            this.Controls.Add(_switch);
            this.Controls.Add(_label);
        }

        // Mapping thuộc tính để Designer không lỗi
        [Category("TDC Appearance")] public Color OnBackColor { get { return _switch.CheckedState.FillColor; } set { _switch.CheckedState.FillColor = value; } }
        [Category("TDC Appearance")] public Color OffBackColor { get { return _switch.UncheckedState.FillColor; } set { _switch.UncheckedState.FillColor = value; } }
        [Category("TDC Appearance")] public Color OnToggleColor { get; set; }
        [Category("TDC Appearance")] public Color OffToggleColor { get; set; }
        [Category("TDC Appearance")] public bool SolidStyle { get; set; }
        [Category("TDC Appearance")] public Color ToggleColor { get; set; }
        [Category("TDC Appearance")] public CheckState CheckState { get { return Checked ? CheckState.Checked : CheckState.Unchecked; } set { Checked = (value == CheckState.Checked); } }
        [Category("TDC Appearance")] public bool UseVisualStyleBackColor { get; set; }
    }
}
