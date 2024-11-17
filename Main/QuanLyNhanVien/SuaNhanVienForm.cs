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

namespace Main
{
    public partial class SuaNhanVienForm : Form
    {
        private string maNhanVien;
        private string hoTen;
        private string phongBan;
        private string gioiTinh;
        private DateTime ngaySinh;
        private float luongCoBan;
        private string sdt;
        private string diaChi;
        private string email;
        private string chucVu;

        public SuaNhanVienForm()
        {
            InitializeComponent();
        }

        public SuaNhanVienForm(string maNhanVien, string hoTen, string phongBan, DateTime ngaySinh, float luongCoBan, string sdt, string diaChi, string email, string tenChucVu, string gioiTinh)
        {
            InitializeComponent();
            this.maNhanVien = maNhanVien;
            this.hoTen = hoTen;
            this.phongBan = phongBan;
            this.gioiTinh = gioiTinh;
            this.ngaySinh = ngaySinh;
            this.luongCoBan = luongCoBan;
            this.sdt = sdt;
            this.diaChi = diaChi;
            this.email = email;
            this.chucVu = tenChucVu;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SuaNhanVienForm_Load(object sender, EventArgs e)
        {
            txtID.Text = maNhanVien;
            txtHoTen.Text = hoTen;
            dtpNgaySinh.Value = ngaySinh;
            txtLuongCoBan.Text = luongCoBan.ToString();
            txtSDT.Text = sdt;
            txtDiaChi.Text = diaChi;
            txtEmail.Text = email;

            if (gioiTinh == "Nam")
            {
                rbtNam.Checked = true;
            }
            else if (gioiTinh == "Nữ")
            {
                rbtNu.Checked = true;
            }

            string query = "select distinct tenPhongBan from PhongBan";
            DataTable dataTable = Function.GetDataQuery(query);
            // Xóa các item cũ trong ComboBox
            cmbPhongBan.Items.Clear();

            // Duyệt qua từng hàng trong DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Lấy giá trị của cột hoTen
                string hoTen = row[0].ToString();
                // Thêm vào ComboBox
                cmbPhongBan.Items.Add(hoTen);
            }
            cmbPhongBan.SelectedItem = phongBan;

            string query2 = "select distinct tenChucVu from ChucVu";
            DataTable dataTable2 = Function.GetDataQuery(query2);
            // Xóa các item cũ trong ComboBox
            cmbChucVu.Items.Clear();

            // Duyệt qua từng hàng trong DataTable
            foreach (DataRow row in dataTable2.Rows)
            {
                // Lấy giá trị của cột hoTen
                string chucVu = row[0].ToString();
                // Thêm vào ComboBox
                cmbChucVu.Items.Add(chucVu);
            }
            cmbChucVu.SelectedItem = chucVu;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các điều khiển
            string ID = txtID.Text.Trim();
            string tenNhanVien = txtHoTen.Text.Trim();
            string gioiTinh = rbtNam.Checked ? "Nam" : "Nữ";
            DateTime ngaySinh = dtpNgaySinh.Value;
            string formattedDate = ngaySinh.ToString("yyyy-MM-dd");
            string email = txtEmail.Text.Trim();
            string soDienThoai = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string maChucVu = cmbMaChucVu.SelectedItem?.ToString(); // ma chuc vu se null khi cmb null
            string maPhongBan = cmbMaPhongBan.SelectedItem?.ToString();
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(ID) ||
            string.IsNullOrEmpty(tenNhanVien) ||
            string.IsNullOrEmpty(soDienThoai) ||
            string.IsNullOrEmpty(diaChi) ||
            string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(maChucVu) ||
            string.IsNullOrEmpty(maPhongBan) ||
            string.IsNullOrEmpty(phongBan) ||
            string.IsNullOrEmpty(chucVu)
            )
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }
            // Khai báo biến heSoLuong kiểu float
            float heSoLuong;
            if (!float.TryParse(txtLuongCoBan.Text.Trim(), out heSoLuong))
            {
                MessageBox.Show("Vui lòng nhập một giá trị hợp lệ cho hệ số lương.");
                return; // Ngừng thực hiện nếu không chuyển đổi thành công
            }
            // Kiểm tra email
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isEmailValid = Regex.IsMatch(email, emailPattern);

            // Kiểm tra số điện thoại
            string phonePattern = @"^(\+84|0)[1-9][0-9]{8,9}$";
            bool isPhoneValid = Regex.IsMatch(soDienThoai, phonePattern);

            // Xuất kết quả kiểm tra
            if (!isEmailValid)
            {
                MessageBox.Show("Email không hợp lệ.");
                return;
            }

            if (!isPhoneValid)
            {
                MessageBox.Show("Số điện thoại không hợp lệ.");
                return;
            }
            if (CheckIfEmployeeIdExists(ID) && ID != maNhanVien)
            {
                MessageBox.Show("Tài Khoản đã tồn tại, vui lòng nhập lại.");
                return;
            }

            string query = "update NhanVien set maNhanVien = '" + ID + "', hoTen = N'" + tenNhanVien + "', gioiTinh = N'" + gioiTinh + "', ngaySinh =  '" + formattedDate + "', soDienThoai =  '" + soDienThoai + "', diaChi = N'" + diaChi + "', email = '" + email + "', luongCoBan =  '" + luongCoBan + "',maPhongBan = '"+maPhongBan+"' , maChucVu = '"+maChucVu+"' where maNhanVien = '" + this.maNhanVien+"' ";

            Function.UpdateDataQuery(query);
        }
        private bool CheckIfEmployeeIdExists(string employeeId)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE maNhanVien = @maNhanVien"; // Sử dụng tham số
            using (SqlConnection sqlConnection = new SqlConnection(Function.GetConnectionString()))
            {
                sqlConnection.Open(); // Mở kết nối
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@maNhanVien", employeeId); // Thêm tham số vào câu lệnh
                    int count = (int)cmd.ExecuteScalar(); // Lấy số lượng bản ghi
                    return count > 0; // Trả về true nếu mã đã tồn tại
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtHoTen.Text = "";
            rbtNam.Checked = false; 
            dtpNgaySinh.Value = DateTime.Now; // Gán về giá trị mặc định (ví dụ: ngày hiện tại)
            txtLuongCoBan.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            cmbChucVu.SelectedIndex = -1; // Bỏ chọn trong ComboBox
            cmbPhongBan.SelectedIndex = -1; // Bỏ chọn trong ComboBox
            cmbMaChucVu.SelectedIndex = -1; // Bỏ chọn trong ComboBox
            cmbMaPhongBan.SelectedIndex = -1; // Bỏ chọn trong ComboBox
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void UpdateMaChucVu()
        {
            cmbMaChucVu.Items.Clear();
            string tenChucVuCurrent = cmbChucVu.SelectedItem?.ToString().Trim();

            // Kiểm tra xem có giá trị nào được chọn không
            if (!string.IsNullOrEmpty(tenChucVuCurrent))
            {
                string query3 = "select maChucVu from ChucVu where tenChucVu = N'" + tenChucVuCurrent + "' ";
                DataTable datatabe3 = Function.GetDataQuery(query3);
                foreach (DataRow row in datatabe3.Rows)
                {
                    string maChucVu = row[0].ToString();
                    cmbMaChucVu.Items.Add(maChucVu);
                }

            }
        }
        private void UpdateMaPhongBan()
        {
            cmbMaPhongBan.Items.Clear();
            string tenPhongBanCurrent = cmbPhongBan.SelectedItem?.ToString().Trim();

            // Kiểm tra xem có giá trị nào được chọn không
            if (!string.IsNullOrEmpty(tenPhongBanCurrent))
            {
                string query3 = "select maPhongBan from PhongBan where tenPhongBan = N'" + tenPhongBanCurrent + "' ";
                DataTable datatabe3 = Function.GetDataQuery(query3);
                foreach (DataRow row in datatabe3.Rows)
                {
                    string maPhongBan = row[0].ToString();
                    cmbMaPhongBan.Items.Add(maPhongBan);
                }

            }
        }

        private void cmbChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMaChucVu();
        }

        private void cmbPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMaPhongBan();
        }

        private void rbtNam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtNu_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void cmbMaPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbMaChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtLuongCoBan_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
