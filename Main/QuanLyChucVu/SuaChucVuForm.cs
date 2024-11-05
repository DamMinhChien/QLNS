using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Main
{
    public partial class SuaChucVuForm : Form
    {
        public string maChucVu;
        public string tenChucVu;
        public float heSoChucVu;

        public SuaChucVuForm()
        {
            InitializeComponent();
        }

        public SuaChucVuForm(string maChucVu, string tenChucVu, float heSoChucVu)
        {
            InitializeComponent();
            this.maChucVu = maChucVu;
            this.tenChucVu = tenChucVu;
            this.heSoChucVu = heSoChucVu;
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.LogOutApp(this);
        }

        private void thoátỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExitApp();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtHeSoChucVu.Text = "";
            txtTenChucVu.Text = "";
            txtID.Text = "";
        }

        private void SuaChucVuForm_Load(object sender, EventArgs e)
        {
            txtTenChucVu.Text = tenChucVu.Trim();
            txtHeSoChucVu.Text = heSoChucVu.ToString();
            txtID.Text = maChucVu.Trim();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            // Lấy dữ liệu từ các điều khiển
            string ID = txtID.Text.Trim();
            string tenChucVu = txtTenChucVu.Text.Trim();
            // Khai báo biến heSoLuong kiểu float
            float heSoChucVu;
            if (!float.TryParse(txtHeSoChucVu.Text.Trim(), out heSoChucVu))
            {
                MessageBox.Show("Vui lòng nhập một giá trị hợp lệ cho hệ số lương.");
                return; // Ngừng thực hiện nếu không chuyển đổi thành công
            }

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(ID) ||
            string.IsNullOrEmpty(tenChucVu))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            string query = "update ChucVu set maChucVu = '" + ID + "', tenChucVu = '" + tenChucVu + "', heSoChucVu = '" + heSoChucVu + "'where maChucVu = '"+this.maChucVu+"' ";

            Function.UpdateDataQuery(query);
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }
    }
}
