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
    public partial class HomeForm : Form
    {
        private bool canChangePhongBan;
        private string ID;
        private string username;
        private string password;

        public HomeForm()
        {
            InitializeComponent();
            canChangePhongBan = true;
        }

        public HomeForm(string username, string password)
        {
            InitializeComponent();
            canChangePhongBan = true;
            this.username = username;
            this.password = password;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            AutoResizer.Init(this);
            GetMaTaiKhoan();
        }

        private void gprQuanLy_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void thoátỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExitApp();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.LogOutApp(this);
        }

        private void pnlQuanLyTaiKhoan_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void picQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            QuanLyTaiKhoanForm quanLyTaiKhoan = new QuanLyTaiKhoanForm(ID);
            quanLyTaiKhoan.ShowDialog();
        }

        private void pnlQuanLyTaiKhoan_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            QuanLyPhongBanForm quanLyPhongBanForm = new QuanLyPhongBanForm(canChangePhongBan);
            quanLyPhongBanForm.ShowDialog();
        }

        private void picQuanLyNhanVien_Click_1(object sender, EventArgs e)
        {
            QuanLyNhanVienForm quanLyNhanVienForm = new QuanLyNhanVienForm();
            quanLyNhanVienForm.ShowDialog();
        }

        private void lblQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            QuanLyNhanVienForm quanLyNhanVienForm = new QuanLyNhanVienForm();
            quanLyNhanVienForm.ShowDialog();
        }

        private void lblQuanLyPhongBan_Click(object sender, EventArgs e)
        {
            QuanLyPhongBanForm quanLyPhongBanForm = new QuanLyPhongBanForm();
            quanLyPhongBanForm.ShowDialog();
        }

        private void picQuanLyChamCong_Click(object sender, EventArgs e)
        {
            ChamCongForm chamCongForm = new ChamCongForm();
            chamCongForm.ShowDialog();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            QuanLyChucVuForm quanLyChucVuForm = new QuanLyChucVuForm();
            quanLyChucVuForm.ShowDialog();
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void picThongBao_Click(object sender, EventArgs e)
        {
            QuanLyThongBaoForm quanLyThongBaoForm = new QuanLyThongBaoForm();
            quanLyThongBaoForm.Show();
        }

        private void lbllThongBao_Click(object sender, EventArgs e)
        {
            QuanLyThongBaoForm quanLyThongBaoForm = new QuanLyThongBaoForm();
            quanLyThongBaoForm.Show();
        }
        private void GetMaTaiKhoan()
        {
            string query = "select maTaiKhoan from TaiKhoan where tenDangNhap = '" + username + "' and matKhau = '" + password + "'";
            DataTable dataTable = Function.GetDataQuery(query);
            if (dataTable.Rows.Count > 0)  // Kiểm tra xem có hàng nào không
            {
                DataRow row = dataTable.Rows[0];  // Lấy hàng đầu tiên

                // Lấy giá trị từ cột đầu tiên
                string value = row[0].ToString();  // Hoặc row["SingleColumn"]
                ID = value;
            }
        }

        private void pnlQuanLyTaiKhoan_MouseEnter(object sender, EventArgs e)
        {
            Function.EnterFormatPanel(pnlQuanLyTaiKhoan);
        }

        private void pnlQuanLyTaiKhoan_MouseLeave(object sender, EventArgs e)
        {
            Function.LeaveFormatPanel(pnlQuanLyTaiKhoan);
        }

        private void pnlQuanLyNhanVien_MouseEnter(object sender, EventArgs e)
        {
            Function.EnterFormatPanel(pnlQuanLyNhanVien);
        }

        private void pnlQuanLyNhanVien_MouseLeave(object sender, EventArgs e)
        {
            Function.LeaveFormatPanel(pnlQuanLyNhanVien);
        }

        private void pnlQuanLyPhongBan_MouseEnter(object sender, EventArgs e)
        {
            Function.EnterFormatPanel(pnlQuanLyPhongBan);
        }

        private void pnlQuanLyPhongBan_MouseLeave(object sender, EventArgs e)
        {
            Function.LeaveFormatPanel(pnlQuanLyPhongBan);
        }

        private void pnlChucVu_MouseEnter(object sender, EventArgs e)
        {
            Function.EnterFormatPanel(pnlChucVu);
        }

        private void pnlChucVu_MouseLeave(object sender, EventArgs e)
        {
            Function.LeaveFormatPanel(pnlChucVu);
        }

        private void pnlQuanLyChamCong_MouseEnter(object sender, EventArgs e)
        {
            Function.EnterFormatPanel(pnlQuanLyChamCong);
        }

        private void pnlQuanLyChamCong_MouseLeave(object sender, EventArgs e)
        {
            Function.LeaveFormatPanel(pnlQuanLyChamCong);
        }

        private void pnlThongBao_MouseEnter(object sender, EventArgs e)
        {
            Function.EnterFormatPanel(pnlThongBao);
        }

        private void pnlThongBao_MouseLeave(object sender, EventArgs e)
        {
            Function.LeaveFormatPanel(pnlThongBao);
        }

        private void pnlQuanLyTaiKhoan_MouseEnter_1(object sender, EventArgs e)
        {
            pnlQuanLyTaiKhoan_MouseEnter(sender, e);
        }

        private void pnlQuanLyTaiKhoan_MouseLeave_1(object sender, EventArgs e)
        {
            pnlQuanLyTaiKhoan_MouseLeave(sender, e);
        }

        private void picQuanLyNhanVien_MouseEnter(object sender, EventArgs e)
        {
            pnlQuanLyNhanVien_MouseEnter(sender,e);
        }

        private void picQuanLyNhanVien_MouseLeave(object sender, EventArgs e)
        {
            pnlQuanLyNhanVien_MouseLeave(sender, e);
        }

        private void picQuanLyphongBan_MouseEnter(object sender, EventArgs e)
        {
            pnlQuanLyPhongBan_MouseEnter(sender,e );
        }

        private void picQuanLyphongBan_MouseLeave(object sender, EventArgs e)
        {
            pnlQuanLyPhongBan_MouseLeave(sender, e);
        }

        private void picQuanLyChucVu_MouseEnter(object sender, EventArgs e)
        {
            pnlChucVu_MouseEnter(sender, e);
        }

        private void picQuanLyChucVu_MouseLeave(object sender, EventArgs e)
        {
            pnlChucVu_MouseLeave(sender, e);
        }

        private void picQuanLyChamCong_MouseEnter(object sender, EventArgs e)
        {
            pnlQuanLyChamCong_MouseEnter(sender, e);
        }

        private void picQuanLyChamCong_MouseLeave(object sender, EventArgs e)
        {
            pnlQuanLyChamCong_MouseLeave(sender, e);
        }

        private void picThongBao_MouseEnter(object sender, EventArgs e)
        {
            pnlThongBao_MouseEnter(sender, e);
        }

        private void picThongBao_MouseLeave(object sender, EventArgs e)
        {
            pnlThongBao_MouseLeave(sender, e);
        }
    }
}
