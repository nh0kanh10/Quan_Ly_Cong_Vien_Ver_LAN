using BUS;
using DAL;
using ET;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class POS_BANVE : Form
    {
        KhuyenMai kmDangApDung = null;
        public POS_BANVE()
        {
            InitializeComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void loadLoaiVe()
        {
            dgvLoaiVe.DataSource = BUS_BANVE.Instance.LayDSLoaiVe();
            dgvLoaiVe.Columns["NgayTao"].Visible = false;
            dgvLoaiVe.Columns["NgayCapNhat"].Visible = false;
            dgvLoaiVe.Columns["LaComBo"].Visible = false;
        }
        private void loadDichVu()
        {
            dgvDichVu.DataSource = BUS_BANVE.Instance.LayDSDichVu();
        }
        private void loadKhachHang()
        {
            dgvKhachHang.DataSource = null;
            dgvKhachHang.DataSource = BUS_KhachHang.Instance.LayDSKhachHang();
            dgvKhachHang.Columns["NgayTao"].Visible = false;
            dgvKhachHang.Columns["MaThanhVien"].Visible = false;
            dgvKhachHang.Columns["TongChiTieu"].Visible = false;
            dgvKhachHang.Columns["NgayHetHanThe"].Visible = false;
            dgvKhachHang.Columns["DiemTichLuy"].Visible = false;
            dgvKhachHang.Columns["NgayCapNhat"].Visible = false;
            dgvKhachHang.ClearSelection();

        }
        private void loadKhuyenMai()
        {
            cboKhuyenMai.DataSource = BUS_KhuyenMai.Instance.LayKhuyenMaiHopLe();
            cboKhuyenMai.DisplayMember = "TenKhuyenMai";
            cboKhuyenMai.ValueMember = "MaKhuyenMai";
            cboKhuyenMai.SelectedIndex = -1;
        }
        private void POS_BANVE_Load(object sender, EventArgs e)
        {
            
            loadLoaiVe();
            loadDichVu();
            loadKhachHang();
            loadKhuyenMai();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dgvDichVu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void loadGioHang()
        {
            dgvGioHang.DataSource = null;
            dgvGioHang.DataSource = BUS_BANVE.Instance.gioHang;

        }
        private void capNhatThanhToan()
        {
            decimal tamTinh = BUS_BANVE.Instance.tamTinh(); // tổng giỏ hàng
            decimal giamGia = 0;

            if (kmDangApDung != null)
            {
                giamGia = BUS_BANVE.Instance.tinhTienGiam(kmDangApDung, tamTinh);
            }

            decimal tongTien = tamTinh - giamGia;

            if (tongTien < 0) tongTien = 0;

            // hiển thị đúng 3 ô
            txtTamTinh.Text = tamTinh.ToString("N0");
            txtGiamGia.Text = giamGia.ToString("N0");
            txtTongTien.Text = tongTien.ToString("N0");
        }
        private void btnThemDV_Click(object sender, EventArgs e)
        {
            
                if (dgvDichVu.CurrentRow == null) return;

                int sl = (int)numDV.Value;
                if (sl <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0!");
                    return;
                }
                var row = dgvDichVu.CurrentRow;

                ET_GioHang item = new ET_GioHang
                {
                    MaSanPham = (int)row.Cells["MaDichVu"].Value,
                    TenSanPham = row.Cells["TenDichVu"].Value.ToString(),
                    DonGia = Convert.ToDecimal(row.Cells["GiaBan"].Value),
                    SoLuong = sl,
                    Loai = "Dịch vụ"
                };

                BUS_BANVE.Instance.themGioHang(item);

                loadGioHang();
                capNhatThanhToan();
           
        }

        private void btnThemVe_Click(object sender, EventArgs e)
        {
            
                if (dgvLoaiVe.CurrentRow == null) return;

                int sl = (int)numVe.Value;
                if (sl <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0!");
                    return;
                }

                var row = dgvLoaiVe.CurrentRow;

                ET_GioHang item = new ET_GioHang
                {
                    MaSanPham = (int)row.Cells["MaLoaiVe"].Value,
                    TenSanPham = row.Cells["TenLoaiVe"].Value.ToString(),
                    DonGia = Convert.ToDecimal(row.Cells["GiaVe"].Value),
                    SoLuong = sl,
                    Loai = "Vé"
                };

                BUS_BANVE.Instance.themGioHang(item);

                loadGioHang();
                capNhatThanhToan();
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Xóa sản phẩm này khỏi giỏ hàng?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if( r == DialogResult.Yes)
            {
                if (dgvGioHang.CurrentRow == null) return;

                string ten = dgvGioHang.CurrentRow.Cells["TenSanPham"].Value.ToString();
                string loai = dgvGioHang.CurrentRow.Cells["Loai"].Value.ToString();

                BUS_BANVE.Instance.xoaGioHang(ten, loai);

                loadGioHang();
                capNhatThanhToan();
            }            
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            //Check giỏ hàng rỗng
            if (BUS_BANVE.Instance.gioHang.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!");
                return;
            }
            if (khDangChon == null)
            {
                DialogResult r = MessageBox.Show(
                    "Chưa chọn khách hàng. Thanh toán khách lẻ?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo);

                if (r == DialogResult.No) return;
            }
            int? maKH = khDangChon?.MaKhachHang; // có thì lấy, không thì null

            frmThanhToan f = new frmThanhToan();
            f.StartPosition = FormStartPosition.CenterParent;
            
            f.soTien = decimal.Parse(txtTongTien.Text);

            if (f.ShowDialog() == DialogResult.OK)
            {
                BUS_BANVE.Instance.thanhToan(maKH, f.PhuongThuc);
                loadGioHang();
                capNhatThanhToan();

                MessageBox.Show("Thanh toán thành công!");
            }            

        }   

        private void btnTimKH_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                loadKhachHang(); // nếu rỗng thì load lại toàn bộ
                return;
            }

            dgvKhachHang.DataSource = BUS_KhachHang.Instance.timKhachHang(keyword);
        }
        ET_KHACHHANG khDangChon = null;
        private void dgvKhachHang_Click(object sender, EventArgs e)
        {
            
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (BUS_BANVE.Instance.gioHang.Count > 0)
            {
                DialogResult r = MessageBox.Show(
                    "Bạn đang có giỏ hàng. Đổi khách sẽ xóa giỏ. Tiếp tục?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo);

                if (r == DialogResult.No) return;

                // nếu đồng ý thì clear giỏ
                BUS_BANVE.Instance.gioHang.Clear();
                loadGioHang();
                capNhatThanhToan();
            }
            var row = dgvKhachHang.Rows[e.RowIndex];

            khDangChon = new ET_KHACHHANG
            {
                MaKhachHang = (int)row.Cells["MaKhachHang"].Value,
                HoTen = row.Cells["HoTen"].Value.ToString(),
                //SoDienThoai = row.Cells["SoDienThoai"].Value.ToString()
            };

            txtTimKiem.Text = khDangChon.HoTen;
            
        }

        private void cboKhuyenMai_SelectedIndexChanged(object sender, EventArgs e)
        {
            kmDangApDung = null; 
            capNhatThanhToan();
        }

        private void btnApDung_Click(object sender, EventArgs e)
        {
            if (cboKhuyenMai.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi!");
                return;
            }

            kmDangApDung = cboKhuyenMai.SelectedItem as KhuyenMai;

            capNhatThanhToan();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("Bạn đang thoát màn hình ?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
