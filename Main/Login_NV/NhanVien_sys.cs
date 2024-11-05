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
    public partial class NhanVien_sys : Form
    {
        public NhanVien_sys()
        {
            InitializeComponent();
        }

        private void NhanVien_sys_Load(object sender, EventArgs e)
        {

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Function.LogOutApp(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Function.ExitApp();
        }
    }
}
