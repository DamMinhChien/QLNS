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
    public partial class HomeForm_TP : Form
    {
        public HomeForm_TP()
        {
            InitializeComponent();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.LogOutApp(this);
        }

        private void thoátỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExitApp();
        }

        private void picQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            QuanLyNhanVienForm quanLyNhanVienForm = new QuanLyNhanVienForm();
            quanLyNhanVienForm.Show();
        }

        private void picQuanLyphongBan_Click(object sender, EventArgs e)
        {
            QuanLyPhongBanForm quanLyPhongBanForm = new QuanLyPhongBanForm();
            quanLyPhongBanForm.Show();
        }

        private void picQuanLyChamCong_Click(object sender, EventArgs e)
        {
            ChamCongForm chamCongForm = new ChamCongForm();
            chamCongForm.Show();
        }
    }
}
