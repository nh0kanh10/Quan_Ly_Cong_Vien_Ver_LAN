using System.Windows.Forms;
using DevExpress.XtraEditors;
using GUI.Infrastructure;

namespace GUI.Modules.BanHang
{
    // Form đơn giản để NV nhập tiền phạt mất đồ.
    // Default = tiền cọc, NV tự sửa nếu không phạt nguyên.
    public partial class frmPhatMatDo : DevExpress.XtraEditors.XtraForm
    {
        public decimal TienPhat { get; private set; }
        
        
        // tenSanPham: Tên SP bị mất (hiển thị cho NV biết)
        // soLuongMat: SL mất
        // tienCocGocMoiCai: Tiền cọc:  gốc (default cho NV)
        public frmPhatMatDo(string tenSanPham, int soLuongMat, decimal tienCocGocMoiCai)
        {
            InitializeComponent();

            lblMoTa.Text = string.Format(
                LanguageManager.GetString("RETURN_PENALTY_DESC") ?? "Sản phẩm: {0}\nSố lượng mất: {1}\nSuggested: {2:N0} ₫\n(NV tự nhập tiền phạt thực tế)",
                tenSanPham, soLuongMat, tienCocGocMoiCai * soLuongMat);

            spinTienPhat.Value = tienCocGocMoiCai * soLuongMat;
        }

        private void BtnDongY_Click(object sender, System.EventArgs e)
        {
            TienPhat = spinTienPhat.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
