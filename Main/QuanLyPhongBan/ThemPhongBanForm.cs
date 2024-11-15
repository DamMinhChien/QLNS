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
    public partial class ThemPhongBanForm : Form
    {
        public ThemPhongBanForm()
        {
            InitializeComponent();
        }

        private void ThemPhongBanForm_Load(object sender, EventArgs e)
        {

        }

        // Kiểm tra mã tk đã tồn tại trong cơ sở dữ liệu
        private bool CheckIfEmployeeIdExists(string employeeId)
        {
            string query = "SELECT COUNT(*) FROM PhongBan WHERE maPhongBan = @maPhongBan"; // Sử dụng tham số
            using (SqlConnection sqlConnection = new SqlConnection(Function.GetConnectionString()))
            {
                sqlConnection.Open(); // Mở kết nối
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@maPhongBan", employeeId); // Thêm tham số vào câu lệnh
                    int count = (int)cmd.ExecuteScalar(); // Lấy số lượng bản ghi
                    return count > 0; // Trả về true nếu mã đã tồn tại
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string ID = txtID.Text.Trim();
            string tenPhongBan = txtTenPhongBan.Text.Trim();
            float heSoPhongBan;
            if (!float.TryParse(txtHeSoPhongBan.Text.Trim(), out heSoPhongBan))
            {
                MessageBox.Show("Vui lòng nhập một giá trị hợp lệ cho hệ số lương.");
                return; // Ngừng thực hiện nếu không chuyển đổi thành công
            }

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(ID) ||
            string.IsNullOrEmpty(tenPhongBan))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }



            if (CheckIfEmployeeIdExists(ID))
            {
                MessageBox.Show("Mã phòng ban đã tồn tại, vui lòng nhập lại.");
                return ;
            }

            string query = "insert into PhongBan values ( '" + ID + "', N'" + tenPhongBan + "', '" + heSoPhongBan + "')";

            Function.UpdateDataQuery(query);
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.LogOutApp(this);
        }

        private void thoátỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExitApp();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }
    }
}
