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
        private string maNhanVien;
        public SuaTaiKhoanForm()
        {
            InitializeComponent();
        }

        public SuaTaiKhoanForm(string maTaiKhoan, string tenDangNhap, string matKhau, string maNhanVien)
        {
            InitializeComponent();
            this.maTaiKhoan = maTaiKhoan;
            this.tenDangNhap = tenDangNhap;
            this.matKhau = matKhau;
            this.maNhanVien = maNhanVien;
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
            string myQuery = "select tenChucVu from NhanVien nv inner join ChucVu cv on nv.maChucVu = cv.maChucVu where maNhanVien = '" + maNhanVien + "'";
            DataTable data = new DataTable();
            data = Function.GetDataQuery(myQuery);
            string tenChucVu = data.Rows[0][0].ToString();

            string query = "select distinct tenChucVu from ChucVu";
            DataTable dataTable = Function.GetDataQuery(query);
            // Xóa các item cũ trong ComboBox
            cmbLoaiTaiKhoan_tenCV.Items.Clear();
            // Duyệt qua từng hàng trong DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Lấy giá trị của cột hoTen
                string hoTen = row[0].ToString();
                // Thêm vào ComboBox
                cmbLoaiTaiKhoan_tenCV.Items.Add(hoTen);
            }
            cmbLoaiTaiKhoan_tenCV.SelectedItem = tenChucVu;
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
            string maChucVuNew = cmbMaChucVu.SelectedItem.ToString();

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
            //Lay maNhanVien tu ten chuc vu
            string myQuery = "select maNhanVien from NhanVien where maChucVu = '" + maChucVuNew + "'";
            DataTable data = new DataTable();
            data = Function.GetDataQuery(myQuery);
            string maNhanVien = data.Rows[0][0].ToString();

            string query = "update TaiKhoan set maTaiKhoan = '" + ID + "', tenDangNhap = '" + tenDangNhap + "' , matKhau = '" + matKhauNew + "', maNhanVien = '" + maNhanVien + "' where maTaiKhoan = '" + this.maTaiKhoan + "'";

            Function.UpdateDataQuery(query);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thốngkêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void cmbLoaiTaiKhoan_tenCV_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMaCV();
        }

        private void UpdateMaCV()
        {
            cmbMaChucVu.Items.Clear();
            string tenChucVuCurrent = cmbLoaiTaiKhoan_tenCV.SelectedItem?.ToString().Trim();

            // Kiểm tra xem có giá trị nào được chọn không
            if (!string.IsNullOrEmpty(tenChucVuCurrent))
            {
                string query3 = "select maChucVu from ChucVu where tenChucVu = N'" + tenChucVuCurrent + "' ";
                DataTable datatabe3 = Function.GetDataQuery(query3);
                foreach (DataRow row in datatabe3.Rows)
                {
                    string hoTen = row[0].ToString();
                    cmbMaChucVu.Items.Add(hoTen);
                }

            }
        }
    }
}
