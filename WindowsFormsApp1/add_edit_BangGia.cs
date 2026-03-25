using BUS;
using DAL;
using ET;
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
    public partial class add_edit_BangGia : Form
    {
        private int _maDichVu;
        private ET_BangGia _bangGia;
        private bool _isEdit = false;

        public add_edit_BangGia(int maDV)
        {
            InitializeComponent();
            _maDichVu = maDV;
            _isEdit = false;
        }

        public add_edit_BangGia(int maDV, ET_BangGia bg)
        {
            InitializeComponent();
            _maDichVu = maDV;
            _bangGia = bg;
            _isEdit = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpNgayKetThuc.Value <= dtpNgayBatDau.Value)
                {
                    MessageBox.Show("Ngày kết thúc phải lớn hơn ngày bắt đầu");
                    return;
                }
                if (_isEdit)
                {
                    // UPDATE
                    _bangGia.GiaBan = Convert.ToDecimal(txtGiaBan.Text);
                    _bangGia.LoaiNgay = cboLoaiNgay.Text;
                    _bangGia.TrangThai = cboTrangThai.Text;
                    _bangGia.NgayBatDau = dtpNgayBatDau.Value;
                    _bangGia.NgayKetThuc = dtpNgayKetThuc.Value;
                    _bangGia.NgayCapNhat = DateTime.Now;

                    bool kq = BUS_BangGia.Instance.suaBangGia(_bangGia);

                    if (kq)
                    {
                        MessageBox.Show("Cập nhật thành công");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    ET_BangGia bg = new ET_BangGia
                    {

                        MaDichVu = _maDichVu,
                        MaLoaiVe = null,

                        GiaBan = Convert.ToDecimal(txtGiaBan.Text),
                        LoaiNgay = cboLoaiNgay.Text,
                        TrangThai = cboTrangThai.Text,
                        NgayBatDau = dtpNgayBatDau.Value,
                        NgayKetThuc = dtpNgayKetThuc.Value,
                        NgayTao = DateTime.Now

                    };

                    bool kq = BUS_BangGia.Instance.themBangGia(bg);

                    if (kq)
                    {
                        MessageBox.Show("Thêm bảng giá thành công");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại");
                    }
                }
                
            }
            catch
            {
                MessageBox.Show("Dữ liệu không hợp lệ");
            }
        }
        private void loadTrangThai()
        {
            cboTrangThai.DataSource = new List<string>
            {
                "Hoạt động", "Ngưng áp dụng"
            };
        }
        private void loadLoaiNgay()
        {
            cboLoaiNgay.DataSource = new List<string>
            {
                "NgayThuong", "CuoiTuan", "LeTet", "CaoDiem"
            };
        }

        private void loadLoaiVe()
        {
            var dsLoaiVe = BUS_LoaiVe.Instance.LoadDS();
            cboLoaiVe.DataSource = dsLoaiVe;
            cboLoaiVe.DisplayMember = "TenLoaiVe";
            cboLoaiVe.ValueMember = "MaLoaiVe";
        }
        private void add_edit_BangGia_Load(object sender, EventArgs e)
        {
            
            loadTrangThai();
            loadLoaiVe();
            loadLoaiNgay();
            if (_isEdit)
            {
                txtMaBG.Text = _bangGia.MaBangGia.ToString();
                txtMaDV.Text = _bangGia.MaDichVu.ToString();
                txtGiaBan.Text = _bangGia.GiaBan.ToString();
                cboLoaiNgay.Text = _bangGia.LoaiNgay;
                cboTrangThai.Text = _bangGia.TrangThai;
                dtpNgayBatDau.Value = _bangGia.NgayBatDau;
                dtpNgayKetThuc.Value = _bangGia.NgayKetThuc;
            }
            else
            {
                txtMaBG.Text = BUS_BangGia.Instance.layBangGiaLonNhat().ToString();
                txtMaDV.Text = _maDichVu.ToString();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show($"Bạn có chắc chắn muốn thoát không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
               this.Close();

            }
        }
    }
}
