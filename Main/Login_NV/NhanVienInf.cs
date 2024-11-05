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
    public partial class NhanVienInf : Form
    {
        public NhanVienInf()
        {
            InitializeComponent();
        }

        public NhanVienInf(string username, string password)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
        }

        private string username;
        private string password;

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        private void NhanVienInf_Load(object sender, EventArgs e)
        {
            string query = "select hoTen, gioiTinh, ngaySinh, cv.tenChucVu, pb.tenPhongBan, diaChi, email, luongCoBan, soDienThoai from NhanVien nv " +
                "inner join ChucVu cv on nv.maChucVu = cv.maChucVu inner join PhongBan pb on pb.maPhongBan = nv.maPhongBan" +
                " inner join TaiKhoan tk on tk.maNhanVien = nv.maNhanVien where tenDangNhap = '"+username+"' and matKhau = '"+password+"'";
            DataTable data = Function.GetDataQuery(query);
            DataRow row = data.Rows[0];

            lblTen.Text = row["hoTen"].ToString();
            lblEmail.Text = row["email"].ToString();
            lblCV.Text = row["tenChucVu"].ToString();
            lblDC.Text = row["diaChi"].ToString();
            lblGT.Text = row["gioiTinh"].ToString();
            lblLuong.Text = row["luongCoBan"].ToString();
            lblNS.Text = row["ngaySinh"].ToString();
            lblPB.Text = row["tenPhongBan"].ToString();
            lblSdt.Text = row["soDienThoai"].ToString();

        }
    }
}
