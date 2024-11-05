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

        public HomeForm()
        {
            InitializeComponent();
            canChangePhongBan = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            AutoResizer.Init(this);
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
            QuanLyTaiKhoanForm quanLyTaiKhoan = new QuanLyTaiKhoanForm();
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
    }
}
