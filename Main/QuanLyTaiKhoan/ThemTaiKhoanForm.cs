using Microsoft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Main
{

    public partial class ThemTaiKhoanForm : Form
    {
        private string ID;
        private string username;
        private string password;

        public string ID1 { get => ID; set => ID = value; }
        public ThemTaiKhoanForm()
        {
            InitializeComponent();
            this.ID = GenerateRandomEmployeeId();

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ID = this.ID;
            string tenTaiKhoan = txtTenTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string nhapLaiMatKhau = txtNhapLaiMatKhau.Text.Trim();
            string maNhanVien = cmbMaNV.SelectedItem?.ToString();

            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";

            bool isValidPassword = Regex.IsMatch(matKhau, passwordPattern);

            if (!isValidPassword)
            {
                MessageBox.Show("Mật khẩu không hợp lệ. Mật khẩu phải có ít nhất 8 ký tự, ít nhất 1 chữ hoa, 1 chữ thường, 1 số và 1 ký tự đặc biệt.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(tenTaiKhoan) ||
            string.IsNullOrEmpty(matKhau) ||
            string.IsNullOrEmpty(nhapLaiMatKhau) ||
            string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (matKhau != nhapLaiMatKhau)
            {
                MessageBox.Show("Mật khẩu không trùng khớp, vui lòng nhập lại.");
                return;
            }

            //if (!IsMaNhanVienExist())
            //{
            //    DialogResult result = MessageBox.Show("Nhân viên này chưa tồn tại, bạn có muốn thêm nhân viên mới không","Thông báo",MessageBoxButtons.YesNo);
            //    if(result == DialogResult.Yes)
            //    {
                    
            //        ThemNhanVienForm themNhanVienForm = new ThemNhanVienForm();
            //        themNhanVienForm.ShowDialog();
            //        // Đăng ký sự kiện
            //        themNhanVienForm.OnNhanVienAdded += (maNV, tenNV) =>
            //        {
            //            // Cập nhật ComboBox khi form ThemNhanVien đóng
            //            this.cmbMaNV.Items.Add(maNV); // Thêm mã nhân viên
            //            this.cmbLoaiTaiKhoan_tenCV.Items.Add(tenNV); // Thêm tên nhân viên
            //        };
            //        cmbLoaiTaiKhoan_tenCV.SelectedValue = themNhanVienForm.GetTenNhanVien();
            //        cmbMaNV.SelectedValue = themNhanVienForm.GetID();
            //    }
                
            //}
            string query = "insert into TaiKhoan values ( '" + ID + "', '" + tenTaiKhoan + "','" + matKhau + "','" + maNhanVien + "')";

            Function.UpdateDataQuery(query);

            this.ID = GenerateRandomEmployeeId();

        }

        //Random mã NV
        private string GenerateRandomEmployeeId()
        {
            Random random = new Random();
            string employeeId;

            do
            {
                int randomNumber = random.Next(100, 1000); // Sinh số ngẫu nhiên từ 100 đến 999
                employeeId = "AC" + randomNumber.ToString(); // Kết hợp "NV" với số ngẫu nhiên
            } while (CheckIfEmployeeIdExists(employeeId)); // Kiểm tra xem mã đã tồn tại chưa

            return employeeId;
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

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }
        private bool IsMaNhanVienExist()
        {
            if (kiemTraCoDungUsername_pass())
            {
                string query = "select maNhanVien from TaiKhoan tk where tenDangNhap = '" + username+ "' and matKhau = '" + password+"' ";
                DataTable dataTable = new DataTable();
                dataTable = Function.GetDataQuery(query);
                string maNhanVien = dataTable.Rows[0][0].ToString();

                if(maNhanVien == cmbMaNV.SelectedItem?.ToString())
                {
                    return true;
                }
            }
            return false;
        }
        private bool kiemTraCoDungUsername_pass()
        {
            using (SqlConnection conn = new SqlConnection(Function.GetConnectionString()))
            {
                string query = "SELECT COUNT(*) FROM TaiKhoan WHERE tenDangNhap = @username AND matKhau = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                conn.Open();

                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private void UpdateMaNhanVien()
        {
            cmbMaNV.Items.Clear();
            string tenNhanVienCurrent = cmbLoaiTaiKhoan_tenCV.SelectedItem?.ToString().Trim();

            // Kiểm tra xem có giá trị nào được chọn không
            if (!string.IsNullOrEmpty(tenNhanVienCurrent))
            {
                string query3 = "select maNhanVien from NhanVien where hoTen = N'" + tenNhanVienCurrent + "' ";
                DataTable datatabe3 = Function.GetDataQuery(query3);
                foreach (DataRow row in datatabe3.Rows)
                {
                    string maNhanVien = row[0].ToString();
                    cmbMaNV.Items.Add(maNhanVien);
                }

            }
        }
        private void ThemTaiKhoanForm_Load(object sender, EventArgs e)
        {
            
            txtID.Text = this.ID;
            string query = "select distinct hoTen from NhanVien";
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
            
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.LogOutApp(this);
        }

        private void thoátỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExitApp();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {

        }
        private void cmbLoaiTaiKhoan_tenCV_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMaNhanVien();
        }

        private void txtTenTaiKhoan_Leave(object sender, EventArgs e)
        {
            this.username = txtTenTaiKhoan.Text.Trim();
        }

        private void txtMatKhau_Leave(object sender, EventArgs e)
        {
            this.password = txtMatKhau.Text.Trim();
        }

        private void lblTaoMoi_Click(object sender, EventArgs e)
        {
            this.Close();
            ThemNhanVienForm themNhanVienForm = new ThemNhanVienForm();
            themNhanVienForm.FormClosed += ThemNhanVienForm_FormClosed;
            themNhanVienForm.ShowDialog();
            
             
            cmbLoaiTaiKhoan_tenCV.SelectedValue = themNhanVienForm.GetTenNhanVien();
            cmbMaNV.SelectedValue = themNhanVienForm.GetID();
        }

        private void ThemNhanVienForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ThemTaiKhoanForm themTaiKhoanForm = new ThemTaiKhoanForm();
            themTaiKhoanForm.ShowDialog();
        }
    }
}
