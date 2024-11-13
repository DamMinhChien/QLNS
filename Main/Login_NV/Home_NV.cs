using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Home_NV : Form
    {
        private bool canChangePhongBan;
        public Home_NV()
        {
            InitializeComponent();
        }

        public Home_NV(string username, string password)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
            canChangePhongBan = false;
        }

        private string username;
        private string password;
        private string maNhanVien;

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
                currentFormChild.Close();

            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel_Body.Controls.Add(childForm);
            panel_Body.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NhanVienInf(username, password));
        }

        private void btn_Sys_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NhanVien_sys());
        }

        private void btn_About_Click(object sender, EventArgs e)
        {
            OpenChildForm(new About_NV());
        }

        private void Home_NV_Load(object sender, EventArgs e)
        {
            string query = "select maNhanVien ,hoTen from NhanVien nv inner join TaiKhoan tk on tk.maNhanVien = nv.maNhanVien where tenDangNhap = '" + username + "' and matKhau = '" + password + "'";

            OpenChildForm(new NhanVienInf(username,password));
        }

        private void panel_Body_Paint(object sender, PaintEventArgs e)
        {

        }
        private void GetMaNhanVien()
        {
            string query = "select maNhanVien from TaiKhoan where tenDangNhap = '" + username + "' and matKhau = '" + password + "'";
            DataTable dataTable = Function.GetDataQuery(query);
            if (dataTable.Rows.Count > 0)  // Kiểm tra xem có hàng nào không
            {
                DataRow row = dataTable.Rows[0];  // Lấy hàng đầu tiên

                // Lấy giá trị từ cột đầu tiên
                string value = row[0].ToString();  // Hoặc row["SingleColumn"]
                maNhanVien = value;
            }
        }

        private void btnThongBaoPhongBan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new PhongBanXemThongBaoForm());
        }

        private void btnThongBaoCaNhan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NhanVien_TBCN(maNhanVien));
        }
        private void EnterFormat(Button button)
        {
            button.BackColor = ColorTranslator.FromHtml("#FFC107");
            button.ForeColor = ColorTranslator.FromHtml("#343A40");
            button.Font = new Font(button.Font.FontFamily, 13, button.Font.Style);
        }

        private void LeaveFormat(Button button)
        {
            button.BackColor = ColorTranslator.FromHtml("#66B3FF");
            button.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            button.Font = new Font(button.Font.FontFamily, 11, button.Font.Style);
        }

        private void btnInf_MouseEnter_1(object sender, EventArgs e)
        {
            EnterFormat(btnInf);
        }

        private void btnInf_MouseLeave(object sender, EventArgs e)
        {
            LeaveFormat(btnInf);
        }

        private void btnThongBaoPhongBan_MouseEnter(object sender, EventArgs e)
        {
            EnterFormat(btnThongBaoPhongBan);
        }

        private void btnThongBaoPhongBan_MouseLeave(object sender, EventArgs e)
        {
            LeaveFormat(btnThongBaoPhongBan);
        }

        private void btnThongBaoCaNhan_MouseEnter(object sender, EventArgs e)
        {
            EnterFormat(btnThongBaoCaNhan);
        }

        private void btnThongBaoCaNhan_MouseLeave(object sender, EventArgs e)
        {
            LeaveFormat(btnThongBaoCaNhan);
        }

        private void btn_Sys_MouseEnter(object sender, EventArgs e)
        {
            EnterFormat(btn_Sys);
        }

        private void btn_Sys_MouseLeave(object sender, EventArgs e)
        {
            LeaveFormat(btn_Sys);
        }

        private void btn_About_MouseEnter(object sender, EventArgs e)
        {
            EnterFormat(btn_About);
        }

        private void btn_About_MouseLeave(object sender, EventArgs e)
        {
            LeaveFormat(btn_About);
        }
    }
}
