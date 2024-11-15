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
    public partial class ThemNhanVienForm : Form
    {
        public event Action<string, string> OnNhanVienAdded; // Thêm sự kiện

        private string ID;
        private string selectedMaNhanVien;
        private string selectedTenNhanVien;

        public string GetID()
        {
            return this.ID;
        }
        public string ID1 { get => ID; set => ID = value; }

        public ThemNhanVienForm()
        {
            InitializeComponent();
            this.ID = GenerateRandomEmployeeId();
        }

        private void tranhRamdomTrung()
        {
            
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }
        //Random mã NV
        private string GenerateRandomEmployeeId()
        {
            Random random = new Random();
            string employeeId;

            do
            {
                int randomNumber = random.Next(100, 1000); // Sinh số ngẫu nhiên từ 100 đến 999
                employeeId = "NV" + randomNumber.ToString(); // Kết hợp "NV" với số ngẫu nhiên
            } while (CheckIfEmployeeIdExists(employeeId)); // Kiểm tra xem mã đã tồn tại chưa

            return employeeId;
        }

        // Kiểm tra mã nhân viên đã tồn tại trong cơ sở dữ liệu
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các điều khiển
            string ID = this.ID;
            string tenNhanVien = txtHoTen.Text.Trim();
            string gioiTinh = rbtNam.Checked ? "Nam" : "Nữ";
            DateTime ngaySinh = dtpNgaySinh.Value;
            string formattedDate = ngaySinh.ToString("yyyy-MM-dd");
            string email = txtEmail.Text.Trim();
            string soDienThoai = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string maChucVu = cmbMaChucVu.SelectedItem?.ToString();
            string maPhongBan = cmbMaPhongBan.SelectedItem?.ToString();
            // Khai báo biến heSoLuong kiểu float
            float luongCoBan;
            if (!float.TryParse(txtLuongCoBan.Text.Trim(), out luongCoBan))
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
            selectedMaNhanVien = ID;
            selectedTenNhanVien = tenNhanVien;

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(tenNhanVien) ||
            string.IsNullOrEmpty(soDienThoai) ||
            string.IsNullOrEmpty(diaChi) ||
            string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(maChucVu) ||
            string.IsNullOrEmpty(maPhongBan))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            string query = "insert into NhanVien values ( '" +ID+ "', N'" +tenNhanVien+ "',N'" +gioiTinh+ "', '" +formattedDate+ "', '" +soDienThoai+ "',N'" +diaChi+ "','" +email+ "', '" +luongCoBan+ "','"+maPhongBan+"' ,'"+maChucVu+"')";

            Function.UpdateDataQuery(query);
            DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            string maChamCong;
            using (SqlConnection connection = new SqlConnection(Function.GetConnectionString()))
            {
                connection.Open();
                maChamCong = GenerateUniqueMaChamCong(connection);

                // Thêm vào bảng chấm công
                string query1 = "INSERT INTO ChamCong (maChamCong, ngayChamCong, maNhanVien, status) VALUES (@maChamCong, @ngayChamCong, @ID, @status)";

                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    command.Parameters.AddWithValue("@maChamCong", maChamCong);
                    command.Parameters.AddWithValue("@ngayChamCong", currentDate); // Truyền kiểu DateTime
                    command.Parameters.AddWithValue("@ID", ID);
                    command.Parameters.AddWithValue("@status", DBNull.Value); 

                    command.ExecuteNonQuery();
                }
            }

            this.ID = GenerateRandomEmployeeId();
            OnNhanVienAdded?.Invoke(ID, tenNhanVien); // Gọi sự kiện
        }
        private string GenerateUniqueMaChamCong(SqlConnection connection)
        {
            Random random = new Random();
            string newID;

            do
            {
                newID = "CC" + random.Next(10000000, 99999999).ToString(); // Tạo ID mới
            } while (ID_ChamCong_Exists(newID, connection));

            return newID;
        }

        private bool ID_ChamCong_Exists(string id, SqlConnection connection)
        {
            string query = "SELECT COUNT(1) FROM ChamCong WHERE maChamCong = @AttendanceID";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@AttendanceID", id);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
        private void ThemNhanVienForm_Load(object sender, EventArgs e)
        {
            txtID.Text = this.ID;
            string query1 = "select distinct tenPhongBan from PhongBan";
            DataTable dataTable = Function.GetDataQuery(query1);
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
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void cmbPhongBan_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            UpdateMaPhongBan();
        }

        private string GetMaNhanVien()
        {
            return selectedMaNhanVien;
        }
        internal string GetTenNhanVien()
        {
            return selectedTenNhanVien;
        }
    }
}
