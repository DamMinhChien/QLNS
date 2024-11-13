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
    public partial class ChamCongTP_Form : Form
    {
        private string maPhongBan;
        private string tenPhongBan;
        private string strCon;
        private int daysInMonth;
        public ChamCongTP_Form()
        {
            InitializeComponent();
            this.strCon = Function.GetConnectionString();
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;
            int now_month = cmbMonth.SelectedIndex + 1;
            int now_year = dtpYear.Value.Year;
            daysInMonth = DateTime.DaysInMonth(now_year, now_month);

            LoadAttendanceData();
        }

        public ChamCongTP_Form(string maPhongBan, string tenPhongBan)
        {
            InitializeComponent();
            this.maPhongBan = maPhongBan;
            this.tenPhongBan = tenPhongBan;
            this.strCon = Function.GetConnectionString();
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;
            int now_month = cmbMonth.SelectedIndex + 1;
            int now_year = dtpYear.Value.Year;
            daysInMonth = DateTime.DaysInMonth(now_year, now_month);

            LoadAttendanceData();
        }

        private string GenerateUniqueID(SqlConnection connection)
        {
            Random random = new Random();
            string newID;

            do
            {
                newID = "CC" + random.Next(10000000, 99999999).ToString(); // Tạo ID mới
            } while (IDExists(newID, connection));

            return newID;
        }

        private bool IDExists(string id, SqlConnection connection)
        {
            string query = "SELECT COUNT(1) FROM ChamCong WHERE maChamCong = @AttendanceID";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@AttendanceID", id);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadAttendanceData()
        {
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                string queryEmployees = @"
                SELECT nv.maNhanVien, nv.hoTen, cv.tenChucVu, pb.tenPhongBan, nv.luongCoBan,
                       pb.heSoPhongBan + cv.heSoChucVu as tongHeSoLuong
                FROM NhanVien AS nv 
                INNER JOIN PhongBan pb ON nv.maPhongBan = pb.maPhongBan 
                INNER JOIN ChucVu cv ON nv.maChucVu = cv.maChucVu
                inner join ChamCong cc on nv.maNhanVien = cc.maNhanVien where nv.maPhongBan = '"+maPhongBan+"' GROUP BY nv.maNhanVien, nv.hoTen, cv.tenChucVu, pb.tenPhongBan, nv.luongCoBan,pb.heSoPhongBan + cv.heSoChucVu";

                SqlCommand cmdEmployees = new SqlCommand(queryEmployees, connection);
                int month = cmbMonth.SelectedIndex + 1;
                int year = dtpYear.Value.Year;

                SqlDataAdapter adapterEmployees = new SqlDataAdapter(cmdEmployees);
                DataTable employeeTable = new DataTable();
                adapterEmployees.Fill(employeeTable);

                dgvChamCong.Columns.Clear();
                dgvChamCong.Rows.Clear();

                dgvChamCong.Columns.Add("ID", "ID");
                dgvChamCong.Columns.Add("name", "Họ tên");
                dgvChamCong.Columns.Add("position", "Chức vụ");
                dgvChamCong.Columns.Add("departName", "Phòng ban");
                dgvChamCong.Columns.Add("normalSalary", "Lương cơ bản");
                dgvChamCong.Columns.Add("heSo", "Hệ số");

                dgvChamCong.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvChamCong.Columns["ID"].Frozen = true;

                dgvChamCong.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvChamCong.Columns["name"].Frozen = true;

                dgvChamCong.Columns["position"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvChamCong.Columns["position"].Frozen = true;

                dgvChamCong.Columns["departName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvChamCong.Columns["departName"].Frozen = true;

                dgvChamCong.Columns["normalSalary"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvChamCong.Columns["normalSalary"].Frozen = true;

                dgvChamCong.Columns["heSo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvChamCong.Columns["heSo"].Frozen = true;

                for (int i = 1; i <= daysInMonth; i++)
                {
                    DateTime currentDate = new DateTime(year, month, i);
                    string dayOfWeek = currentDate.ToString("ddd");
                    dgvChamCong.Columns.Add($"Day{i}", $"{i}/\n{dayOfWeek}");
                }

                dgvChamCong.Columns.Add("totalDays", "Tổng");
                dgvChamCong.Columns.Add("salary", "Lương");

                string queryAttendance = @"
                SELECT maNhanVien,ngayChamCong, status 
                FROM ChamCong 
                WHERE MONTH(ngayChamCong) = @Month AND YEAR(ngayChamCong) = @Year;
                ";

                SqlCommand cmdAttendance = new SqlCommand(queryAttendance, connection);
                cmdAttendance.Parameters.AddWithValue("@Month", month);
                cmdAttendance.Parameters.AddWithValue("@Year", year);

                SqlDataAdapter adapterAttendance = new SqlDataAdapter(cmdAttendance);
                DataTable attendanceTable = new DataTable();
                adapterAttendance.Fill(attendanceTable);

                //Căn giữa các cell ngày
                foreach (DataGridViewColumn column in dgvChamCong.Columns)
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                foreach (DataRow empRow in employeeTable.Rows)
                {
                    int countP = 0;
                    int totalDays = 0;
                    float salary = 0;
                    float luong_co_ban = Convert.ToSingle(empRow["luongCoBan"]);
                    float he_so = Convert.ToSingle(empRow["tongHeSoLuong"]);

                    int rowIndex = dgvChamCong.Rows.Add();
                    dgvChamCong.Rows[rowIndex].Cells["ID"].Value = empRow["maNhanVien"];
                    dgvChamCong.Rows[rowIndex].Cells["name"].Value = empRow["hoTen"];
                    dgvChamCong.Rows[rowIndex].Cells["position"].Value = empRow["tenChucVu"];
                    dgvChamCong.Rows[rowIndex].Cells["departName"].Value = empRow["tenPhongBan"];
                    dgvChamCong.Rows[rowIndex].Cells["normalSalary"].Value = empRow["luongCoBan"];
                    dgvChamCong.Rows[rowIndex].Cells["heSo"].Value = he_so;

                    for (int i = 1; i <= daysInMonth; i++)
                    {
                        DateTime attendanceDate = new DateTime(year, month, i);
                        string status = ""; // Default value

                        DataRow[] foundRows = attendanceTable.Select($"maNhanVien = '{empRow["maNhanVien"]}' AND ngayChamCong = '{attendanceDate.ToString("yyyy-MM-dd")}'");

                        if (foundRows.Length > 0)
                        {
                            status = foundRows[0]["status"].ToString();
                        }

                        dgvChamCong.Rows[rowIndex].Cells[$"Day{i}"].Value = status;

                        if (status == "x") totalDays++;
                        else if (status == "P")
                        {
                            countP++; // Đếm số ngày "p"
                        }
                        // Kiểm tra số lượng "p"
                        if (countP > 2)
                        {
                            // Tính "p" như không có mặt
                            // Không tăng totalDays
                        }
                        else
                        {
                            totalDays += countP; // Nếu có ít hơn hoặc bằng 2 "p", tính vào tổng ngày công
                        }
                    }

                    salary += (luong_co_ban / daysInMonth) * totalDays * he_so;

                    dgvChamCong.Rows[rowIndex].Cells["totalDays"].Value = totalDays;
                    dgvChamCong.Rows[rowIndex].Cells["salary"].Value = Math.Round(salary);

                }
            }
            HighlightWeekendColumns();
            Function.SoleRowColor(dgvChamCong);
        }
        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
                int month = cmbMonth.SelectedIndex + 1;
                int year = dtpYear.Value.Year;

                using (SqlConnection connection = new SqlConnection(strCon))
                {
                    connection.Open();

                    // Bước 1: Xóa dữ liệu cũ
                    string deleteQuery = "DELETE FROM ChamCong WHERE MONTH(ngayChamCong) = @Month AND YEAR(ngayChamCong) = @Year";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("@Month", month);
                        deleteCmd.Parameters.AddWithValue("@Year", year);
                        deleteCmd.ExecuteNonQuery();
                    }

                    // Bước 2: Lưu dữ liệu mới
                    foreach (DataGridViewRow row in dgvChamCong.Rows)
                    {
                        if (row.Cells["ID"].Value != null)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (cell.OwningColumn.Name.StartsWith("Day")) // Kiểm tra cột ngày
                                {
                                    string status = cell.Value?.ToString() ?? "";
                                    DateTime attendanceDate = new DateTime(year, month, int.Parse(cell.OwningColumn.Name.Substring(3)));


                                    // Chỉ lưu nếu status là "x"
                                    if (status == "x")
                                    {
                                        // Tạo một mã chấm công ngẫu nhiên duy nhất
                                        string maChamCong = GenerateUniqueID(connection);
                                        string query = "INSERT INTO ChamCong (maChamCong, maNhanVien, ngayChamCong, status) " +
                                                       "VALUES (@AttendanceID, @EmployeeID, @AttendanceDate, @Status)";

                                        using (SqlCommand cmd = new SqlCommand(query, connection))
                                        {
                                            cmd.Parameters.AddWithValue("@AttendanceID", maChamCong);
                                            cmd.Parameters.AddWithValue("@EmployeeID", row.Cells["ID"].Value);
                                            cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate);
                                            cmd.Parameters.AddWithValue("@Status", status);
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    MessageBox.Show("Dữ liệu chấm công đã được lưu.");
                }
            cậpNhậtToolStripMenuItem_Click(sender, e);
        }

        private void cậpNhậtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadAttendanceData();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.LogOutApp(this);
        }

        private void thoátỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExitApp();
        }

        private void thốngkêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExportToExcel(dgvChamCong);
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void ChamCongTP_Form_Load(object sender, EventArgs e)
        {
            lblTieuDe.Text = "Bảng chấm công tháng " + (cmbMonth.SelectedIndex + 1) + "/" + dtpYear.Value.Year;
            LoadAttendanceData();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAttendanceData();
        }

        private void dtpYear_TabIndexChanged(object sender, EventArgs e)
        {
            LoadAttendanceData();
        }
        private void HighlightWeekendColumns()
        {
            int month = cmbMonth.SelectedIndex + 1;
            int year = dtpYear.Value.Year;

            for (int i = 1; i <= daysInMonth; i++)
            {
                DateTime currentDate = new DateTime(year, month, i);
                string dayOfWeek = currentDate.ToString("ddd"); // Lấy tên ngày trong tuần

                if (dayOfWeek == "T7" || dayOfWeek == "CN") // Kiểm tra nếu là T7 hoặc CN
                {
                    dgvChamCong.Columns[$"Day{i}"].DefaultCellStyle.BackColor = Color.Yellow; // Tô màu vàng nhạt
                }
            }
        }
        private void ChamCongTP_Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) // Kiểm tra phím Space
            {
                // Lấy chỉ số hàng và cột của ô đang được chọn
                int rowIndex = dgvChamCong.CurrentCell.RowIndex;
                int columnIndex = dgvChamCong.CurrentCell.ColumnIndex;

                // Tạo đối tượng DataGridViewCellEventArgs với chỉ số hàng và cột hiện tại
                DataGridViewCellEventArgs cellEventArgs = new DataGridViewCellEventArgs(columnIndex, rowIndex);

                // Gọi hàm dgvChamCong_CellClick với sender và cellEventArgs
                dgvChamCong_CellClick(sender, cellEventArgs);

                e.SuppressKeyPress = true; // Ngăn chặn âm thanh bíp khi nhấn phím
            }
        }

        private void ChamCongTP_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn lưu thay đổi không", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                cậpNhậtToolStripMenuItem_Click(sender, e);
            }
            else if (result == DialogResult.No) { }
            else
            {
                e.Cancel = true;
            }
        }

        private void dgvChamCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (
                e.RowIndex >= 0 &&
                e.RowIndex < dgvChamCong.Rows.Count - 1 &&
                e.ColumnIndex >= 0 &&
                e.ColumnIndex >= 6 &&
                e.ColumnIndex < dgvChamCong.Columns.Count - 2
                ) // Đảm bảo không nhấp vào tiêu đề cột 
            {
                // Lấy ô được nhấp
                DataGridViewCell cell = dgvChamCong.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Kiểm tra giá trị hiện tại của ô
                if (cell.Value.ToString() == "x")
                {
                    cell.Value = "P";
                }
                else if (cell.Value.ToString() == "P")
                {

                    cell.Value = "";
                }
                else
                {
                    cell.Value = "x";
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lưuToolStripMenuItem_Click(sender, e);
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadAttendanceData();
        }
    }
}
