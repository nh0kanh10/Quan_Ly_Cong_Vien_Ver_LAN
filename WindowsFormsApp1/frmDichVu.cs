using BUS;
using ET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmDichVu : Form
    {
        private int _maDichVuDangChon = -1;

        public frmDichVu()
        {
            InitializeComponent();
            
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox4_Click(object sender, EventArgs e)
        {

        }
        private void loadDS()
        {
            
            var ds = BUS_DichVu.Instance.loadDS();
            dgvDanhSachDV.DataSource = ds;
            dgvDanhSachDV.Columns["MaDichVu"].Visible = false;
            dgvDanhSachDV.Columns["GiaBan"].DisplayIndex = 2;
            dgvDanhSachDV.Columns["MaDanhMuc"].DisplayIndex = 3;
        }
        
        private void loadKhuVuc()
        {
            var ds = BUS_KhuVuc.Instance.LoadDSKhuVuc();
            cboKhuVuc.DisplayMember = "TenKhuVuc";
            cboKhuVuc.ValueMember = "MaKhuVuc";
            cboKhuVuc.DataSource = ds;
        }
        private void loadDanhMuc()
        {
            var ds = BUS_DanhMucDichVu.Instance.loadDS();
            cboDanhMuc.DisplayMember = "TenDanhMuc";
            cboDanhMuc.ValueMember = "MaDanhMuc";
            cboDanhMuc.DataSource = ds;
        }
        private void loadTrangThai()
        {
            cboTrangThai.DataSource = new List<string>
            {
                "Hoạt động", "Ngừng bán"
            };
        }
        private void frmDichVu_Load(object sender, EventArgs e)
        {
            loadDS();
            loadKhuVuc();
            loadDanhMuc();
            loadTrangThai();
            txtMaCode.Text = BUS_DichVu.Instance.layMaCodeTiepTheo();
           
             
        }

        private void dgvDanhSachDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDanhSachDV_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachDV.CurrentRow == null) return;
            int dong = dgvDanhSachDV.CurrentCell.RowIndex;

            _maDichVuDangChon = Convert.ToInt32(dgvDanhSachDV.Rows[dong].Cells["MaDichVu"].Value);

            // load bảng giá của dịch vụ đang chọn
            LoadBangGia(_maDichVuDangChon);

            txtMaDV.Text = dgvDanhSachDV.Rows[dong].Cells["MaDichVu"].Value.ToString();
            txtMaCode.Text = dgvDanhSachDV.Rows[dong].Cells["MaCode"].Value.ToString();
            txtTenDV.Text = dgvDanhSachDV.Rows[dong].Cells["TenDichVu"].Value.ToString();

            cboKhuVuc.SelectedValue = dgvDanhSachDV.Rows[dong].Cells["MaKhuVuc"].Value;
            cboDanhMuc.SelectedValue = dgvDanhSachDV.Rows[dong].Cells["MaDanhMuc"].Value;


            txtSoLuongTon.Text = dgvDanhSachDV.Rows[dong].Cells["SoLuongTon"].Value.ToString();
            cboTrangThai.Text = dgvDanhSachDV.Rows[dong].Cells["TrangThai"].Value.ToString();
            txtGiaBan.Text = dgvDanhSachDV.Rows[dong].Cells["GiaBan"].Value.ToString();
            txtDonViTinh.Text = dgvDanhSachDV.Rows[dong].Cells["DonViTinh"].Value.ToString();
           

            if (DateTime.TryParse(dgvDanhSachDV.Rows[dong].Cells["NgayTao"].Value.ToString(), out DateTime ngayTao))
            {
                dtpNgayTao.Value = ngayTao;
            }
            if (DateTime.TryParse(dgvDanhSachDV.Rows[dong].Cells["NgayCapNhat"].Value?.ToString(), out DateTime ngayCapNhat))
            {
                dtpNgayCapNhat.Value = ngayCapNhat;
            }
        }
        private ET_DichVu docDuLieu()
        {
            try
            {
                ET_DichVu et = new ET_DichVu
                {
                    MaCode = txtMaCode.Text,
                    TenDichVu = txtTenDV.Text,
                    MaDanhMuc = cboDanhMuc.SelectedValue == null ? 0 : Convert.ToInt32(cboDanhMuc.SelectedValue),
                    MaKhuVuc = cboKhuVuc.SelectedValue == null ? 0 : Convert.ToInt32(cboKhuVuc.SelectedValue),
                    GiaBan = string.IsNullOrWhiteSpace(txtGiaBan.Text) ? 0 : Convert.ToDecimal(txtGiaBan.Text),
                    SoLuongTon = string.IsNullOrWhiteSpace(txtSoLuongTon.Text) ? 0 : Convert.ToInt32(txtSoLuongTon.Text),
                    DonViTinh = txtDonViTinh.Text,
                    TrangThai = cboTrangThai.Text,
                }; 
                return et;
            }
            catch (FormatException)
            {
                MessageBox.Show("Dữ liệu số không hợp lệ. Vui lòng kiểm tra Giá bán, Số lượng tồn, mã danh mục và mã khu vực",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ET_DichVu et = docDuLieu();
            if (et == null) return;

            string loiValidate = BUS_DichVu.Instance.ValidateDichVu(et, laThem: true);
            if (!string.IsNullOrEmpty(loiValidate))
            {
                MessageBox.Show(loiValidate, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool kq = BUS_DichVu.Instance.themDichVu(et);
            if (kq)
            {
                MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDS();
                lamMoi();
            }
            else
            {
                MessageBox.Show("Thêm dịch vụ thất bại! Vui lòng kiểm tra dữ liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachDV.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maCodeCanXoa = dgvDanhSachDV.CurrentRow.Cells["MaCode"].Value.ToString();

            DialogResult r = MessageBox.Show($"Bạn có chắc chắn muốn xóa trò chơi có mã {maCodeCanXoa}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                bool kq = BUS_DichVu.Instance.Xoa(maCodeCanXoa);
                if (kq)
                {
                    MessageBox.Show("Xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa dịch vụ thất bại! Dịch vụ có thể đang được sử dụng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                loadDS();
                lamMoi();
            }
        }
        

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCode.Text) || txtMaCode.Text == BUS_DichVu.Instance.layMaCodeTiepTheo())
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ET_DichVu et = docDuLieu();
            if (et == null) return;

            string loiValidate = BUS_DichVu.Instance.ValidateDichVu(et, laThem: false);
            if (!string.IsNullOrEmpty(loiValidate))
            {
                MessageBox.Show(loiValidate, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult r = MessageBox.Show($"Bạn có chắc chắn muốn sửa thông tin trò chơi có mã {et.MaCode}?", "Xác nhận sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                bool kq = BUS_DichVu.Instance.suaDichVu(et);
                if (kq)
                {
                    MessageBox.Show("Sửa thông tin dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sửa thông tin dịch vụ thất bại! Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                loadDS();
                lamMoi();
            }
        }
        private void LoadBangGia(int maDichVu)
        {
            var dsBangGia = BUS_BangGia.Instance.loadDS(maDichVu);
            dgvBangGia.DataSource = dsBangGia;
            dgvBangGia.Columns["MaBangGia"].Visible = false;
            dgvBangGia.Columns["MaLoaiVe"].Visible = false;
        }
        private void lamMoi()
        {
            txtMaCode.Text = BUS_DichVu.Instance.layMaCodeTiepTheo();
            txtMaDV.Text = BUS_DichVu.Instance.layMaDichVuLonNhat().ToString();
            txtMaDV.Text = "";
            txtTenDV.Text = "";
            cboDanhMuc.SelectedIndex = -1;
            cboKhuVuc.SelectedIndex = -1;
            txtGiaBan.Text = "";
            txtSoLuongTon.Text = "";
            txtDonViTinh.Text = "";
            cboTrangThai.SelectedIndex = -1;
            dtpNgayTao.Value = DateTime.Now;
            dtpNgayCapNhat.Value = DateTime.Now;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lamMoi();
        }

        private void btnThemGia_Click(object sender, EventArgs e)
        {
            if (_maDichVuDangChon == -1)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ trước");
                return;
            }

            add_edit_BangGia f = new add_edit_BangGia(_maDichVuDangChon);
            f.StartPosition = FormStartPosition.CenterParent;
            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadBangGia(_maDichVuDangChon); // reload lại bảng giá
               
            }

           
        }

        private void btnXoaGia_Click(object sender, EventArgs e)
        {
            if (dgvBangGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn bảng giá cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int maBangGia = Convert.ToInt32(dgvBangGia.CurrentRow.Cells["MaBangGia"].Value);

            DialogResult r = MessageBox.Show($"Bạn có chắc chắn muốn xóa bảng giá có mã {maBangGia}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                bool kq = BUS_BangGia.Instance.xoaBangGia(maBangGia);
                if (kq)
                {
                    MessageBox.Show("Xóa bảng giá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBangGia(_maDichVuDangChon); // reload lại bảng giá sau khi xóa
                }
                else
                {
                    MessageBox.Show("Xóa bảng giá thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (dgvBangGia.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn bảng giá cần sửa");
                return;
            }
            ET_BangGia bg = dgvBangGia.CurrentRow.DataBoundItem as ET_BangGia;
            add_edit_BangGia f = new add_edit_BangGia(_maDichVuDangChon,bg);
            f.StartPosition = FormStartPosition.CenterParent;
            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadBangGia(_maDichVuDangChon); // reload lại bảng giá

            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                loadDS(); 
            }
            else
            {
                dgvDanhSachDV.DataSource = BUS_DichVu.Instance.TimKiem(tuKhoa);
            }
        }
    }
}
