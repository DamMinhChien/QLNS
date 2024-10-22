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
    public partial class ChamCongForm : Form
    {
        private string strCon;
        private int daysInMonth;
        public ChamCongForm()
        {
            InitializeComponent();
            this.strCon = Function.GetConnectionString();
            daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month);
            LoadAttendanceData();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ChamCongForm_Load(object sender, EventArgs e)
        {
            lblTieuDe.Text = "Bảng chấm công tháng " + DateTime.Now.Month + "/" + DateTime.Now.Year;
            LoadAttendanceData();
        }



        private string GenerateUniqueID(SqlConnection connection)
        {
            Random random = new Random();
            string newID;

            do
            {
                newID = "CC" + random.Next(100000, 999999).ToString(); // Tạo ID mới
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
        private void btnSaveAttendance_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                connection.Open();
                foreach (DataGridViewRow row in dgvChamCong.Rows)
                {
                    if (row.Cells["ID"].Value != null)
                    {
                        // Tạo một mã chấm công ngẫu nhiên duy nhất
                        string maChamCong = GenerateUniqueID(connection);

                        string query = "INSERT INTO ChamCong (maChamCong, maNhanVien, ngayChamCong, soNgayLamViec, soNgayNghi, soNgayDiMuon, status) " +
                                       "VALUES (@AttendanceID, @EmployeeID, @AttendanceDate, @TotalDays, @OffDays, @LateDays, @Status)";

                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.OwningColumn.Name.StartsWith("Day"))
                            {
                                string status = cell.Value?.ToString() ?? "N/A";
                                DateTime attendanceDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, int.Parse(cell.OwningColumn.Name.Substring(3)));

                                int totalDays = 0; // Tính tổng số ngày làm việc
                                int offDays = 0;   // Tính số ngày nghỉ
                                int lateDays = 0;  // Tính số ngày đi muộn

                                // Logic để xác định totalDays, offDays, và lateDays dựa trên trạng thái
                                if (status == "x") totalDays++;
                                else if (status == "p") offDays++;
                                else if (status == "m") lateDays++;

                                using (SqlCommand cmd = new SqlCommand(query, connection))
                                {
                                    cmd.Parameters.AddWithValue("@AttendanceID", maChamCong);
                                    cmd.Parameters.AddWithValue("@EmployeeID", row.Cells["ID"].Value);
                                    cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate);
                                    cmd.Parameters.AddWithValue("@TotalDays", totalDays);
                                    cmd.Parameters.AddWithValue("@OffDays", offDays);
                                    cmd.Parameters.AddWithValue("@LateDays", lateDays);
                                    cmd.Parameters.AddWithValue("@Status", status);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                MessageBox.Show("Dữ liệu chấm công đã được lưu.");
            }
        }




        private void LoadAttendanceData()
        {
            using (SqlConnection connection = new SqlConnection(strCon))
            {
                string queryEmployees = @"
                SELECT nv.maNhanVien, nv.hoTen, cv.tenChucVu, pb.tenPhongBan, nv.luongCoBan,
                       SUM(pb.heSoPhongBan + cv.heSoChucVu) AS tongHeSoLuong 
                FROM NhanVien AS nv 
                INNER JOIN PhongBan pb ON nv.maPhongBan = pb.maPhongBan 
                INNER JOIN ChucVu cv ON nv.maChucVu = cv.maChucVu 
                GROUP BY nv.maNhanVien, nv.hoTen, cv.tenChucVu, pb.tenPhongBan, nv.luongCoBan;";

                SqlCommand cmdEmployees = new SqlCommand(queryEmployees, connection);
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

                for (int i = 1; i <= daysInMonth; i++)
                {
                    DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
                    string dayOfWeek = currentDate.ToString("ddd");
                    dgvChamCong.Columns.Add($"Day{i}", $"{i}/\n{dayOfWeek}");
                }

                dgvChamCong.Columns.Add("totalDays", "Tổng");
                dgvChamCong.Columns.Add("salary", "Lương");

                string queryAttendance = @"
                SELECT maNhanVien, ngayChamCong, soNgayLamViec, soNgayNghi, soNgayDiMuon, status 
                FROM ChamCong 
                WHERE MONTH(ngayChamCong) = @Month AND YEAR(ngayChamCong) = @Year;";

                SqlCommand cmdAttendance = new SqlCommand(queryAttendance, connection);
                cmdAttendance.Parameters.AddWithValue("@Month", DateTime.Now.Month);
                cmdAttendance.Parameters.AddWithValue("@Year", DateTime.Now.Year);

                SqlDataAdapter adapterAttendance = new SqlDataAdapter(cmdAttendance);
                DataTable attendanceTable = new DataTable();
                adapterAttendance.Fill(attendanceTable);

                foreach (DataRow empRow in employeeTable.Rows)
                {
                    int totalDays = 0;
                    float salary = 0;
                    float luong_co_ban = Convert.ToSingle(empRow["luongCoBan"]);
                    float he_so = Convert.ToSingle(empRow["tongHeSoLuong"]);

                    int rowIndex = dgvChamCong.Rows.Add();
                    dgvChamCong.Rows[rowIndex].Cells["ID"].Value = empRow["maNhanVien"];
                    dgvChamCong.Rows[rowIndex].Cells["name"].Value = empRow["hoTen"];
                    dgvChamCong.Rows[rowIndex].Cells["position"].Value = empRow["tenChucVu"];
                    dgvChamCong.Rows[rowIndex].Cells["departName"].Value = empRow["tenPhongBan"];
                    dgvChamCong.Rows[rowIndex].Cells["normalSalary"].Value = luong_co_ban;
                    dgvChamCong.Rows[rowIndex].Cells["heSo"].Value = he_so;

                    for (int i = 1; i <= daysInMonth; i++)
                    {
                        DateTime attendanceDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
                        string status = ""; // Default value

                        DataRow[] foundRows = attendanceTable.Select($"maNhanVien = '{empRow["maNhanVien"]}' AND ngayChamCong = '{attendanceDate.ToString("yyyy-MM-dd")}'");

                        if (foundRows.Length > 0)
                        {
                            status = foundRows[0]["status"].ToString();
                        }

                        dgvChamCong.Rows[rowIndex].Cells[$"Day{i}"].Value = status;

                        if (status == "x") totalDays++;
                    }

                    salary += (luong_co_ban / daysInMonth) * totalDays * he_so;

                    dgvChamCong.Rows[rowIndex].Cells["totalDays"].Value = totalDays;
                    dgvChamCong.Rows[rowIndex].Cells["salary"].Value = salary;
                }
            }
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

        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSaveAttendance_Click(sender, e);
            cậpNhậtToolStripMenuItem_Click(sender,e);
        }
    }
}
