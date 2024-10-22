using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Main
{
    internal class Function
    {
        private static string strCon = @"Data Source=localhost;Initial Catalog=QLNS;Integrated Security=True;";

        public string StrCon { get => strCon; }

        public static void ExitApp()
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát ứng dụng không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public static void LogOutApp(Form currentForm)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất không?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Lặp qua danh sách tạm thời và đóng từng form
                foreach (Form form in Application.OpenForms)
                {
                    // Đóng từng form
                    if (form is LoginForm)
                        continue;
                    form.Hide();
                }
                new LoginForm().Show();
            }
        }

        internal static DataTable GetDataQuery(String myQuery)
        {
            
            DataTable dataTable = new DataTable(); //Tạo đối tượng để lưu dữ liệu trả về từ CSDl
            using (SqlConnection sqlCon = new SqlConnection(strCon)) //Tạo đối tượng kết nối
            {
                string query = myQuery; //truy vấn SQL
                SqlCommand cmd = new SqlCommand(query, sqlCon); //Tạo đối tượng Command
                SqlDataAdapter adapter = new SqlDataAdapter(cmd); // Tạo đối tượng adapter và truyền vào cmd

                try
                {
                    sqlCon.Open(); // Mở kết nối
                    adapter.Fill(dataTable); // Đổ dữ liệu vào đối tượng dataTable
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
            // Kết nối tự đóng khi hết using
            return dataTable;
        }
        internal static void LoadDataGridView(DataGridView dgv, String myQuery)
        {
            dgv.Columns.Add("STT", "STT"); //thêm cột STT trước khi đổ data
            dgv.DataSource = GetDataQuery(myQuery);
            // Điền số thứ tự vào cột STT
            for (int i = 0; i < dgv.Rows.Count - 1; i++)
            {
                dgv.Rows[i].Cells[0].Value = i + 1; // Gán số thứ tự
            }

            RemoveDuplicateColumns(dgv);
        }

        internal static void UpdateDataQuery(String myQuery)
        {
            using (SqlConnection sqlCon = new SqlConnection(strCon)) // Tạo đối tượng kết nối
            {
                SqlCommand cmd = new SqlCommand(myQuery, sqlCon); // Tạo đối tượng Command

                try
                {
                    sqlCon.Open(); // Mở kết nối
                    int rowsAffected = cmd.ExecuteNonQuery(); // Thực thi câu lệnh và nhận số bản ghi bị ảnh hưởng

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không có bản ghi nào được cập nhật.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); // Thông báo lỗi
                }
            }
        }
        //Hiện chữ chìm mặc định
        internal static void Find(TextBox txt,string shadowText)
        {
            txt.Text = shadowText;
            txt.ForeColor = Color.Gray;
            txt.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Italic);
        }
        // Khi người dùng nhấn vào TextBox
        internal static void Enter_text(TextBox txt, string shadowText)
        {
            if (txt.Text == shadowText)
            {
                txt.Text = ""; // Xóa nội dung
                txt.ForeColor = Color.Black; // Đặt lại màu chữ
                txt.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
            }
        }
        // Khi người dùng rời khỏi TextBox
        internal static void Leave(TextBox txt, string shadowText)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                Find(txt, shadowText);
            }
        }
        // Xóa cột bị trùng (cột STT)
        internal static void RemoveDuplicateColumns(DataGridView dgv)
        {
            // Kiểm tra số lượng cột
            if (dgv.Columns.Count < 2) return;

            // Lấy tên của cột đầu tiên
            string firstColumnName = dgv.Columns[0].Name;

            // Duyệt qua các cột từ cuối lên đầu
            for (int i = dgv.Columns.Count - 1; i > 0; i--)
            {
                // Nếu tên cột giống với cột đầu tiên, xóa cột này
                if (dgv.Columns[i].Name == firstColumnName)
                {
                    dgv.Columns.RemoveAt(i);
                }
            }
        }

        public static string GetConnectionString()
        {
            return strCon;
        }

        










    }
}
