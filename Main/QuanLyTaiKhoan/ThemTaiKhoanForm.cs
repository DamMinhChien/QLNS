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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Main
{

    public partial class ThemTaiKhoanForm : Form
    {
        private string ID;

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
            string maChucVu = cmbLoaiTaiKhoan_tenCV.SelectedItem.ToString();


            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(tenTaiKhoan) ||
            string.IsNullOrEmpty(matKhau) ||
            string.IsNullOrEmpty(nhapLaiMatKhau) ||
            string.IsNullOrEmpty(maChucVu))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (matKhau != nhapLaiMatKhau)
            {
                MessageBox.Show("Mật khẩu không trùng khớp, vui lòng nhập lại.");
                return;
            }

            string query = "insert into TaiKhoan values ( '" + ID + "', '" + tenTaiKhoan + "','" + matKhau + "','" + maChucVu + "')";

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

        private void ThemTaiKhoanForm_Load(object sender, EventArgs e)
        {
            txtID.Text = this.ID;
            string query = "select distinct tenChucVu from ChucVu";
            DataTable dataTable = Function.GetDataQuery(query);
            // Xóa các item cũ trong ComboBox
            cmbLoaiTaiKhoan_tenCV.Items.Clear();

            // Duyệt qua từng hàng trong DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Lấy giá trị của cột tenChucVu
                string chucVu = row[0].ToString();
                // Thêm vào ComboBox
                cmbLoaiTaiKhoan_tenCV.Items.Add(chucVu);
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
    }
}
