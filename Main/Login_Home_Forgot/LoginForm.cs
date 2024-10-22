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
            String userName = txtUsername.Text.Trim().ToString();
            String passWord = txtPassword.Text.Trim().ToString();

            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
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
                string query = "select * from TaiKhoan";
                DataTable dataTable = Function.GetDataQuery(query);

                foreach(DataRow row in dataTable.Rows)
                {
                    if (row["tenDangNhap"].ToString().Trim() == userName && row["matKhau"].ToString().Trim() == passWord)
                    {
                        //admin
                        if (row["maChucVu"].ToString().Trim() == "admin")
                        {
                            new HomeForm().Show();
                            this.Hide();
                            return;
                        }

                        else if (row["maChucVu"].ToString().Trim() == "TP")
                        {
                            new HomeForm_TP().Show();
                            this.Hide();
                            return;
                        }

                        else
                        {
                            MessageBox.Show("Bạn không có quyền đăng nhập vào hệ thống!", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    
                    
                }
                MessageBox.Show("Tài khoản hoăc mật khẩu không đúng, vui lòng thử lại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
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
    }
}
