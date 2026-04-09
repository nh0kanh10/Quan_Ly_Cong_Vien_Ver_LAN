using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public class TDCTextBox : UserControl
    {
        private Color borderColor = Color.FromArgb(209, 213, 219);
        private Color borderFocusColor = Color.FromArgb(0, 150, 136); // Teal
        private int borderSize = 1;
        private bool underlinedStyle = false;
        private TextBox textBox1;

        public TDCTextBox()
        {
            textBox1 = new TextBox();
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Dock = DockStyle.Fill;
            textBox1.Font = new Font("Segoe UI", 11f);
            textBox1.BackColor = Color.White;
            
            this.Controls.Add(textBox1);
            this.Padding = new Padding(7);
            this.Size = new Size(250, 35);
            this.BackColor = Color.White;

            textBox1.Enter += (s, e) => { borderSize = 2; this.Invalidate(); };
            textBox1.Leave += (s, e) => { borderSize = 1; this.Invalidate(); };
        }

        public override string Text
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public bool Multiline
        {
            get { return textBox1.Multiline; }
            set { 
                textBox1.Multiline = value;
                if (value) this.Padding = new Padding(7);
                else this.Height = 35; 
            }
        }

        public bool ReadOnly
        {
            get { return textBox1.ReadOnly; }
            set 
            { 
                textBox1.ReadOnly = value;
                Color readOnlyColor = Color.FromArgb(232, 232, 232);
                this.BackColor = value ? readOnlyColor : Color.White;
                textBox1.BackColor = value ? readOnlyColor : Color.White;
            }
        }

        public bool UseSystemPasswordChar
        {
            get { return textBox1.UseSystemPasswordChar; }
            set { textBox1.UseSystemPasswordChar = value; }
        }

        public void Clear() { textBox1.Clear(); }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            using (Pen penBorder = new Pen(borderColor, borderSize))
            {
                penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                if (textBox1.Focused) penBorder.Color = borderFocusColor;

                if (underlinedStyle)
                {
                    graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                }
                else
                {
                    graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!this.Multiline)
                this.Height = textBox1.Height + this.Padding.Top + this.Padding.Bottom;
        }
    }
}
