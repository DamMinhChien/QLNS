using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class SuaTaiKhoanForm : Form
    {
        private string maTaiKhoan;
        private string tenDangNhap;
        private string matKhau;
        private string maChucVu;
        public SuaTaiKhoanForm()
        {
            InitializeComponent();
        }

        public SuaTaiKhoanForm(string maTaiKhoan, string tenDangNhap, string matKhau, string maChucVu)
        {
            InitializeComponent();
            this.maTaiKhoan = maTaiKhoan;
            this.tenDangNhap = tenDangNhap;
            this.matKhau = matKhau;
            this.maChucVu = maChucVu;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtAcc.Text = "";
            txtPass.Text = "";
            cmbLoaiTaiKhoan_tenCV.SelectedIndex = -1;

        }

        private void SuaTaiKhoanForm_Load(object sender, EventArgs e)
        {
            txtID.Text = maTaiKhoan;
            txtAcc.Text = tenDangNhap;
            txtPass.Text = matKhau;
            cmbLoaiTaiKhoan_tenCV.SelectedItem = maChucVu;

        }
        // Kiểm tra mã tk đã tồn tại trong cơ sở dữ liệu
        private bool CheckIfEmployeeIdExists(string employeeId)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE maTaiKhoan = @maTaiKhoan"; // Sử dụng tham số
            using (SqlConnection sqlConnection = new SqlConnection(Function.GetConnectionString()))
            {
                sqlConnection.Open(); // Mở kết nối
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@maTaiKhoan", employeeId); // Thêm tham số vào câu lệnh
                    int count = (int)cmd.ExecuteScalar(); // Lấy số lượng bản ghi
                    return count > 0; // Trả về true nếu mã đã tồn tại
                }
            }
        }

        private bool CheckIfAccExists(string employeeId)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE tenDangNhap = @tenDangNhap"; // Sử dụng tham số
            using (SqlConnection sqlConnection = new SqlConnection(Function.GetConnectionString()))
            {
                sqlConnection.Open(); // Mở kết nối
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@tenDangNhap", employeeId); // Thêm tham số vào câu lệnh
                    int count = (int)cmd.ExecuteScalar(); // Lấy số lượng bản ghi
                    return count > 0; // Trả về true nếu mã đã tồn tại
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string ID = txtID.Text;
            string tenDangNhapNew = txtAcc.Text;
            string matKhauNew = txtPass.Text;
            string maChucVuNew = cmbLoaiTaiKhoan_tenCV.SelectedItem.ToString();

            if (string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhauNew) || string.IsNullOrEmpty(maChucVuNew))
            {
                MessageBox.Show("Vui lòng nhập dầy đủ thông tin.");
                return;
            }
            if (!CheckIfEmployeeIdExists(ID))
            {
                MessageBox.Show("Không tìm thấy mã phòng ban, vui lòng nhập lại.");
                return;
            }

            if (CheckIfAccExists(tenDangNhapNew))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại, vui lòng nhập lại.");
                return;
            }

            string query = "update TaiKhoan set maTaiKhoan = '" + ID + "', tenDangNhap = '" + tenDangNhap + "' , matKhau = '" + matKhauNew + "', maChucVu = '" + maChucVuNew + "' where maTaiKhoan = '" + this.maTaiKhoan + "'";

            Function.UpdateDataQuery(query);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
