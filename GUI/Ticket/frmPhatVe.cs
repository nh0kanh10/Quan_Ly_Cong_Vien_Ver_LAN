using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using Guna.UI2.WinForms;
using ZXing;
using ZXing.Common;

namespace GUI
{
    public partial class frmPhatVe : Form
    {
        private int _idDonHang;
        private string _maDonHang;
        private string _tenKhach;
        private string _phuongThuc;
        private List<TicketDisplayItem> _tickets;
        private Guna2Panel _selectedCard = null;

        public frmPhatVe(int idDonHang, string maDonHang, string tenKhach, string phuongThuc = "TienMat")
        {
            _idDonHang = idDonHang;
            _maDonHang = maDonHang ?? "";
            _tenKhach = tenKhach ?? "Khách vãng lai";
            _phuongThuc = phuongThuc ?? "TienMat";

            InitializeComponent();
            WireEvents();
            LoadTickets();
        }

        private void WireEvents()
        {
            btnXong.Click += (s, e) => this.Close();
            btnInTatCa.Click += BtnInTatCa_Click;
            btnInLai.Click += BtnInLai_Click;
            btnLuuQR.Click += BtnLuuQR_Click;
        }

        // ════════════════════════════════════════
        // DATA LOADING
        // ════════════════════════════════════════

        private void LoadTickets()
        {
            bool isRfid = _phuongThuc == AppConstants.PhuongThucThanhToan.ViRfid;
            _tickets = BUS_VeDienTu.Instance.LayVeTheoDonHang(_idDonHang);

            lblTitle.Text = "🎫 PHÁT VÉ — " + _maDonHang;
            lblSubtitle.Text = string.Format("Khách: {0}  |  Tổng: {1} vé  |  TT: {2}",
                _tenKhach, _tickets.Count, isRfid ? "Ví RFID" : "Tiền Mặt");

            flowCards.Controls.Clear();

            foreach (var ve in _tickets)
            {
                var card = TicketCardFactory.CreateCardFrame(isRfid);
                BindCardData(card, ve, isRfid);
                card.Click += Card_Click;
                flowCards.Controls.Add(card);
            }

            btnInTatCa.Visible = !isRfid;
            btnInLai.Visible = !isRfid;

            if (_tickets.Count == 0)
            {
                var lbl = new Label
                {
                    Text = "Đơn hàng này không có vé điện tử.",
                    Font = new Font("Segoe UI", 12),
                    ForeColor = ThemeManager.SecondaryColor,
                    AutoSize = true,
                    Padding = new Padding(20)
                };
                flowCards.Controls.Add(lbl);
            }
        }

        /// <summary>
        /// Gán data từ DTO vào card đã tạo bởi TicketCardFactory
        /// </summary>
        private void BindCardData(Guna2Panel card, TicketDisplayItem ve, bool isRfid)
        {
            card.Tag = ve;

            foreach (Control c in card.Controls)
            {
                if (c.Name == "pbIcon" && c is PictureBox pb)
                {
                    var icon = TicketCardFactory.GetServiceIcon(ve.TenDichVu);
                    pb.Image = IconHelper.GetBitmap(icon,
                        isRfid ? ThemeManager.PrimaryColor : ThemeManager.WarningColor,
                        TicketCardFactory.IconRenderSize);
                    pb.Click += (s, e) => Card_Click(card, e);
                }
                else if (c.Name == "lblTen" && c is Label lblTen)
                {
                    lblTen.Text = ve.TenDichVu;
                    lblTen.Click += (s, e) => Card_Click(card, e);
                }
                else if (c.Name == "lblCode" && c is Label lblCode)
                {
                    lblCode.Text = ve.MaCode;
                    lblCode.Click += (s, e) => Card_Click(card, e);
                }
                else if (c.Name == "lblLuot" && c is Label lblLuot)
                {
                    lblLuot.Text = ve.SoLuotConLai + " lượt";
                }
            }
        }

        // ════════════════════════════════════════
        // SELECTION
        // ════════════════════════════════════════

        private void Card_Click(object sender, EventArgs e)
        {
            var clicked = sender as Guna2Panel;
            if (clicked == null) return;

            bool isRfid = _phuongThuc == AppConstants.PhuongThucThanhToan.ViRfid;

            if (_selectedCard != null && _selectedCard != clicked)
                TicketCardFactory.StyleDeselected(_selectedCard, isRfid);

            if (_selectedCard == clicked)
            {
                TicketCardFactory.StyleDeselected(clicked, isRfid);
                _selectedCard = null;
            }
            else
            {
                TicketCardFactory.StyleSelected(clicked);
                _selectedCard = clicked;
            }
        }

        // ════════════════════════════════════════
        // PRINTING
        // ════════════════════════════════════════

        private void BtnInTatCa_Click(object sender, EventArgs e)
        {
            if (_tickets == null || _tickets.Count == 0) return;

            var doc = new PrintDocument();
            doc.DocumentName = "VeDienTu_" + _maDonHang;
            doc.PrintPage += PrintAllTickets;

            var preview = new PrintPreviewDialog();
            preview.Document = doc;
            preview.WindowState = FormWindowState.Maximized;
            preview.ShowDialog();

            foreach (Control c in flowCards.Controls)
            {
                if (c is Guna2Panel card) TicketCardFactory.StyleCompleted(card);
            }
        }

        private void BtnInLai_Click(object sender, EventArgs e)
        {
            if (_selectedCard == null)
            {
                TDCMessageBox.Show("Chọn 1 card vé (click vào) trước khi bấm In Lại!", "Hướng dẫn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var ve = _selectedCard.Tag as TicketDisplayItem;
            if (ve == null) return;

            var doc = new PrintDocument();
            doc.DocumentName = "InLai_" + ve.MaCode;
            doc.PrintPage += (s, args) => PrintSingleTicket(args, ve);

            var preview = new PrintPreviewDialog();
            preview.Document = doc;
            preview.WindowState = FormWindowState.Maximized;
            preview.ShowDialog();

            TicketCardFactory.StyleCompleted(_selectedCard);
        }

        private void PrintAllTickets(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            int x = 40, y = 40;
            int col = 0;

            g.DrawString("ĐẠI NAM — VÉ ĐIỆN TỬ", TicketCardFactory.PrintTitleFont, Brushes.Black, x, y);
            y += 30;
            g.DrawString("Đơn hàng: " + _maDonHang + "  |  Khách: " + _tenKhach,
                TicketCardFactory.PrintSubFont, Brushes.Gray, x, y);
            y += 40;

            foreach (var ve in _tickets)
            {
                g.DrawRectangle(Pens.DarkGray, x, y, TicketCardFactory.PrintCardW, TicketCardFactory.PrintCardH);
                g.DrawString(ve.TenDichVu, TicketCardFactory.PrintNameFont, Brushes.Black, x + 10, y + 10);
                g.DrawString(ve.MaCode, TicketCardFactory.PrintCodeFont, Brushes.DarkBlue, x + 10, y + 40);

                // Sinh barcode thật bằng ZXing (CODE_128)
                using (var barcodeBmp = GenerateBarcode(ve.MaCode, 180, 35))
                {
                    if (barcodeBmp != null)
                        g.DrawImage(barcodeBmp, x + 10, y + 70, 180, 35);
                }

                g.DrawString(ve.SoLuotConLai + " lượt", TicketCardFactory.PrintLuotFont, Brushes.Gray, x + 10, y + 115);
                g.DrawString("Chúc vui vẻ!", TicketCardFactory.PrintFooterFont, Brushes.Gray, x + 150, y + 115);

                col++;
                if (col >= 2) { col = 0; x = 40; y += TicketCardFactory.PrintCardH + 20; }
                else { x += TicketCardFactory.PrintCardW + 30; }
            }
            e.HasMorePages = false;
        }

        private void PrintSingleTicket(PrintPageEventArgs e, TicketDisplayItem ve)
        {
            var g = e.Graphics;
            int x = 40, y = 40;

            g.DrawString("ĐẠI NAM — VÉ ĐIỆN TỬ (IN LẠI)", TicketCardFactory.ReprintTitleFont, Brushes.Black, x, y);
            y += 40;
            g.DrawRectangle(Pens.DarkGray, x, y, 300, 170);
            g.DrawString(ve.TenDichVu, TicketCardFactory.ReprintNameFont, Brushes.Black, x + 15, y + 15);
            g.DrawString(ve.MaCode, TicketCardFactory.ReprintCodeFont, Brushes.DarkBlue, x + 15, y + 50);

            // Sinh barcode thật bằng ZXing (CODE_128)
            using (var barcodeBmp = GenerateBarcode(ve.MaCode, 220, 40))
            {
                if (barcodeBmp != null)
                    g.DrawImage(barcodeBmp, x + 15, y + 85, 220, 40);
            }
            g.DrawString(ve.SoLuotConLai + " lượt  |  " + _tenKhach,
                TicketCardFactory.ReprintInfoFont, Brushes.Gray, x + 15, y + 135);
            e.HasMorePages = false;
        }

        /// <summary>
        /// Lưu tất cả vé ra file QR code PNG chất lượng cao trên Desktop.
        /// </summary>
        private void BtnLuuQR_Click(object sender, EventArgs e)
        {
            if (_tickets == null || _tickets.Count == 0)
            {
                TDCMessageBox.Show("Không có vé nào để lưu!", "Thông báo");
                return;
            }

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folder = System.IO.Path.Combine(desktopPath, "QR_Ve_" + _maDonHang);
            System.IO.Directory.CreateDirectory(folder);

            int count = 0;
            foreach (var ve in _tickets)
            {
                // Sinh QR code 400x400 pixel (chất lượng cao, dễ scan)
                using (var qrBmp = GenerateBarcode(ve.MaCode, 400, 400, true))
                {
                    if (qrBmp != null)
                    {
                        string fileName = ve.MaCode + ".png";
                        string filePath = System.IO.Path.Combine(folder, fileName);
                        qrBmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                TDCMessageBox.Show(
                    string.Format("Đã lưu {0} QR code vào:\n{1}", count, folder),
                    "Lưu QR thành công \u2705");

                // Mở folder cho tiện
                System.Diagnostics.Process.Start("explorer.exe", folder);
            }
        }
        /// <summary>
        /// Sinh ảnh barcode thật (CODE_128) hoặc QR code từ chuỗi text.
        /// Trả về Bitmap — caller phải Dispose.
        /// </summary>
        private Bitmap GenerateBarcode(string text, int width, int height, bool useQR = false)
        {
            try
            {
                var writer = new BarcodeWriter
                {
                    Format = useQR ? BarcodeFormat.QR_CODE : BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Width = width,
                        Height = height,
                        Margin = 1,
                        PureBarcode = true
                    }
                };
                return writer.Write(text);
            }
            catch
            {
                return null;
            }
        }
    }
}
