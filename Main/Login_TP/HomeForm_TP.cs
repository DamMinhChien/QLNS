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
        private bool canChangePhongBan;

        private string maPhongBan;
        private string tenPhongBan;

        private string username;
        private string password;
        public HomeForm_TP()
        {
            InitializeComponent();
            canChangePhongBan = false;
        }

        public HomeForm_TP(string username, string password)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
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
            QuanLyNhanVienTP_Form quanLyNhanVienTP_Form = new QuanLyNhanVienTP_Form(maPhongBan, tenPhongBan);
            quanLyNhanVienTP_Form.Show();
        }

        private void picQuanLyphongBan_Click(object sender, EventArgs e)
        {
            QuanLyPhongBanForm quanLyPhongBanForm = new QuanLyPhongBanForm(canChangePhongBan);
            quanLyPhongBanForm.Show();
        }

        private void picQuanLyChamCong_Click(object sender, EventArgs e)
        {
            ChamCongTP_Form chamCongTP_Form = new ChamCongTP_Form(maPhongBan,tenPhongBan);
            chamCongTP_Form.Show();
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void HomeForm_TP_Load(object sender, EventArgs e)
        {
            string query = "select tenPhongBan from PhongBan pb inner join NhanVien nv on pb.maPhongBan = nv.maPhongBan inner join ChucVu cv on cv.maChucVu = nv.maChucVu inner join TaiKhoan tk on tk.maNhanVien = nv.maNhanVien where tenDangNhap = '" + username+"' and matKhau = '"+password+"'";
            DataTable dataTable = Function.GetDataQuery(query);
            if (dataTable.Rows.Count > 0)  // Kiểm tra xem có hàng nào không
            {
                DataRow row = dataTable.Rows[0];  // Lấy hàng đầu tiên

                // Lấy giá trị từ cột đầu tiên
                string value = row[0].ToString();  // Hoặc row["SingleColumn"]

                tenPhongBan = value;
                gprQuanLy.Text = "Quản lý " + value;  // In ra giá trị
            }
            GetMaPhongBan();
        }
        private void GetMaPhongBan()
        {
            string query = "select pb.maPhongBan from PhongBan pb inner join NhanVien nv on pb.maPhongBan = nv.maPhongBan inner join ChucVu cv on cv.maChucVu = nv.maChucVu inner join TaiKhoan tk on tk.maNhanVien = nv.maNhanVien where tenDangNhap = '" + username + "' and matKhau = '" + password + "'";
            DataTable dataTable = Function.GetDataQuery(query);
            if (dataTable.Rows.Count > 0)  // Kiểm tra xem có hàng nào không
            {
                DataRow row = dataTable.Rows[0];  // Lấy hàng đầu tiên

                // Lấy giá trị từ cột đầu tiên
                string value = row[0].ToString();  // Hoặc row["SingleColumn"]
                maPhongBan = value;
            }
        }
        private void picThongBao_Click(object sender, EventArgs e)
        {
            QuanLyThongBao_TP_Form quanLyThongBao_TP_Form = new QuanLyThongBao_TP_Form(maPhongBan, tenPhongBan);
            quanLyThongBao_TP_Form.Show();
        }
    }
}
