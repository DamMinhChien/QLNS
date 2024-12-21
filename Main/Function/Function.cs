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
using Excel = Microsoft.Office.Interop.Excel;

namespace Main
{
    internal class Function
    {
        private static string strCon = @"Data Source=localhost;Initial Catalog=QuanLyNhanSuNew;Integrated Security=True;";

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
            Function.SoleRowColor(dgv);
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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

        internal static void SoleRowColor(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Index %2 != 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        public static void ExportToExcel(DataGridView dgv)
        {
            // Khởi tạo hộp thoại lưu file
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                saveFileDialog.Title = "Lưu file Excel";
                saveFileDialog.FileName = "Dữ liệu"; // Tên mặc định

                // Kiểm tra xem người dùng đã chọn đường dẫn hay không
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Tạo ứng dụng Excel
                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook workbook = excelApp.Workbooks.Add();
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                    // Xuất tiêu đề cột
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
                    }

                    // Xuất dữ liệu hàng
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgv.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value?.ToString();
                        }
                    }

                    // Lưu file Excel
                    workbook.SaveAs(saveFileDialog.FileName);
                    workbook.Close();
                    excelApp.Quit();

                    // Giải phóng tài nguyên
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                    MessageBox.Show("Xuất dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        internal static void EnterFormat(Button button)
        {
            button.BackColor = ColorTranslator.FromHtml("#FFC107");
            button.ForeColor = ColorTranslator.FromHtml("#343A40");
            button.Font = new Font(button.Font.FontFamily, 13, button.Font.Style);
        }

        internal static void LeaveFormat(Button button)
        {
            button.BackColor = ColorTranslator.FromHtml("#66B3FF");
            button.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
            button.Font = new Font(button.Font.FontFamily, 11, button.Font.Style);
        }

        internal static void EnterFormatPanel(Panel panel)
        {
            panel.BackColor = Color.FromArgb(220, 220, 220); // Màu nền khi hover

        }
        internal static void LeaveFormatPanel(Panel panel)
        {
            // Đưa mọi thứ về trạng thái ban đầu
            panel.BackColor = Color.White; // Màu nền mặc định

        }
    }
}
