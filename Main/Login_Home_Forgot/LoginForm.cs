using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String userName = txtUsername.Text.Trim();
            String passWord = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                if (string.IsNullOrEmpty(userName))
                {
                    MessageBox.Show("Tên tài khoản không được bỏ trống !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (string.IsNullOrEmpty(passWord))
                {
                    MessageBox.Show("Mật khẩu không được bỏ trống !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Kiểm tra thông tin đăng nhập
                string myQuery = $"SELECT maChucVu FROM NhanVien nv INNER JOIN TaiKhoan tk ON tk.maNhanVien = nv.maNhanVien WHERE tenDangNhap = '{userName}' AND matKhau = '{passWord}'";

                DataTable data = Function.GetDataQuery(myQuery);

                if (data.Rows.Count > 0)
                {
                    string maChucVu = data.Rows[0][0].ToString();

                    // Xác định vai trò người dùng và hiển thị form tương ứng
                    if (maChucVu.StartsWith("ad"))
                    {
                        new HomeForm(userName, passWord).Show();
                    }
                    else if (maChucVu.StartsWith("TP"))
                    {
                        new HomeForm_TP(userName, passWord).Show();
                    }
                    else
                    {
                        new Home_NV(userName, passWord).Show();
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng, vui lòng thử lại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát ứng dụng không?", "Thoát",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void lblForget_Click(object sender, EventArgs e)
        {
            ForgotPassForm forgotPassForm = new ForgotPassForm();
            forgotPassForm.Show();
            this.Hide();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
