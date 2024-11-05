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
    public partial class SuaThongBaoPBForm : Form
    {
        private string maThongBao;
        private string tenPhongBan;
        private string tieuDe;
        private string noiDung;
        private string fileDinhKem;
        public SuaThongBaoPBForm()
        {
            InitializeComponent();
        }

        public SuaThongBaoPBForm(string maThongBao, string tenPhongBan, string tieuDe, string noiDung, string fileDinhKem)
        {
            InitializeComponent();
            this.maThongBao = maThongBao;
            this.tenPhongBan = tenPhongBan;
            this.tieuDe = tieuDe;
            this.noiDung = noiDung;
            this.fileDinhKem = fileDinhKem;
        }

        private void SuaThongBaoPBForm_Load(object sender, EventArgs e)
        {
            txtTenPhongBan.Text = tenPhongBan;
            txtTieuDe.Text = tieuDe;
            txtNoiDung.Text = noiDung;
            if (fileDinhKem != null)
            {
                lblLink.Text = fileDinhKem;
                picPDF.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open file";
            openFile.Filter = "Fille pdf|*.pdf";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                lblLink.Text = openFile.FileName.ToString();
                picPDF.Visible = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string maThongBao = this.maThongBao;
            string tieuDe = txtTieuDe.Text;
            string noiDung = txtNoiDung.Text;
            string fileDinhKem = lblLink.Text;
            DateTime dateTime = DateTime.Now;
            if (string.IsNullOrEmpty(tieuDe) || string.IsNullOrEmpty(noiDung))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo");
                return;
            }
            string query = "update ThongBao set tieuDe = N'" + tieuDe + "', noiDung = N'" + noiDung + "', ngayDang = '" + dateTime + "',fileDinhKem = '" + fileDinhKem + "' where maThongBao = '" + maThongBao + "'";
            Function.UpdateDataQuery(query);
        }
    }
}
