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
    public partial class SuaPhongBanForm : Form
    {
        private string maPhongBan;
        private string tenPhongBan;
        private float heSoPhongBan;
        public SuaPhongBanForm()
        {
            InitializeComponent();
        }

        public SuaPhongBanForm(string maPhongBan, string tenPhongBan, float heSoPhongBan)
        {
            InitializeComponent();
            this.maPhongBan = maPhongBan;
            this.tenPhongBan = tenPhongBan;
            this.heSoPhongBan = heSoPhongBan;
        }

        private void SuaPhongBanForm_Load(object sender, EventArgs e)
        {
            txtID.Text = maPhongBan;
            txtTenPB.Text = tenPhongBan;
            txtHeSoPhongBan.Text = heSoPhongBan.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtTenPB.Text = "";
            txtHeSoPhongBan.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string ID = txtID.Text;
            string tenPhongBanNew  = txtTenPB.Text;
            float heSoPhongBanNew;
            if (!float.TryParse(txtHeSoPhongBan.Text.Trim(), out heSoPhongBanNew))
            {
                MessageBox.Show("Vui lòng nhập một giá trị hợp lệ cho hệ số lương.");
                return;
            }

            if (string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(tenPhongBanNew)) {
                MessageBox.Show("Vui lòng nhập dầy đủ thông tin.");
                return;
            }
            if (!CheckIfEmployeeIdExists(ID))
            {
                MessageBox.Show("Không tìm thấy mã phòng ban, vui lòng nhập lại.");
                return;
            }

            string query = "update PhongBan set maPhongBan = '"+ID+"', tenPhongBan = '"+tenPhongBanNew+ "', heSoPhongBan = '" + heSoPhongBanNew+ "' where maPhongBan = '"+this.maPhongBan+"'";

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

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }
    }
}
