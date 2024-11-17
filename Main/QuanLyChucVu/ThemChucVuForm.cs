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
    public partial class ThemChucVuForm : Form
    {
        public ThemChucVuForm()
        {
            InitializeComponent();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.LogOutApp(this);
        }

        private void thoátỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExitApp();
        }

        private void ThemChucVuForm_Load(object sender, EventArgs e)
        {

        }

        private bool CheckIfEmployeeIdExists(string employeeId)
        {
            string query = "SELECT COUNT(*) FROM ChucVu WHERE maChucVu = @maChucVu"; // Sử dụng tham số
            using (SqlConnection sqlConnection = new SqlConnection(Function.GetConnectionString()))
            {
                sqlConnection.Open(); // Mở kết nối
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@maChucVu", employeeId); // Thêm tham số vào câu lệnh
                    int count = (int)cmd.ExecuteScalar(); // Lấy số lượng bản ghi
                    return count > 0; // Trả về true nếu mã đã tồn tại
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string ID = txtChucVu.Text.Trim();
            string tenChucVu = txtTenChucVu.Text.Trim();
            float heSoChucVu;

            if (!float.TryParse(txtHeSoChucVu.Text.Trim(), out heSoChucVu))
            {
                MessageBox.Show("Vui lòng nhập một giá trị hợp lệ cho hệ số lương.");
                return; // Ngừng thực hiện nếu không chuyển đổi thành công
            }

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(ID) ||
            string.IsNullOrEmpty(tenChucVu))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (CheckIfEmployeeIdExists(ID))
            {
                MessageBox.Show("Mã phòng ban đã tồn tại, vui lòng nhập lại.");
                return;
            }

            string query = "insert into ChucVu values ( '" + ID + "', N'" + tenChucVu + "', '" + heSoChucVu + "'  )";

            Function.UpdateDataQuery(query);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cmbChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMaChucVu();
        }
        private void UpdateMaChucVu()
        {
            string tenChucVuCurrent = cmbChucVu.SelectedItem?.ToString().Trim();

            // Kiểm tra xem có giá trị nào được chọn không
            if (!string.IsNullOrEmpty(tenChucVuCurrent))
            {
                if (tenChucVuCurrent == "Admin")
                {
                    txtChucVu.Text = "ad" + GenerateRandomEmployeeId();
                }
                else if (tenChucVuCurrent == "Trưởng Phòng")
                {
                    txtChucVu.Text = "TP" + GenerateRandomEmployeeId();
                }
                else
                {
                    txtChucVu.Text = "NV" + GenerateRandomEmployeeId();
                }
            }
        }
        private string GenerateRandomEmployeeId()
        {
            Random random = new Random();
            string employeeId;

            do
            {
                int randomNumber = random.Next(100, 1000); // Sinh số ngẫu nhiên từ 100 đến 999
                employeeId = randomNumber.ToString(); 
            } while (CheckIfEmployeeIdExists(employeeId)); // Kiểm tra xem mã đã tồn tại chưa

            return employeeId;
        }
        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
