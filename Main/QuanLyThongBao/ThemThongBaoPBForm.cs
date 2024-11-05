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
    public partial class ThemThongBaoPBForm : Form
    {
        private string ID;
        public ThemThongBaoPBForm()
        {
            InitializeComponent();
            this.ID = GenerateRandomEmployeeId();
        }

        private string GenerateRandomEmployeeId()
        {
            Random random = new Random();
            string employeeId;

            do
            {
                int randomNumber = random.Next(100, 1000); // Sinh số ngẫu nhiên từ 100 đến 999
                employeeId = "TB" + randomNumber.ToString(); // Kết hợp "NV" với số ngẫu nhiên
            } while (CheckIfEmployeeIdExists(employeeId)); // Kiểm tra xem mã đã tồn tại chưa

            return employeeId;
        }

        // Kiểm tra mã nhân viên đã tồn tại trong cơ sở dữ liệu
        private bool CheckIfEmployeeIdExists(string employeeId)
        {
            string query = "SELECT COUNT(*) FROM ThongBao WHERE maThongBao = @maThongBao"; // Sử dụng tham số
            using (SqlConnection sqlConnection = new SqlConnection(Function.GetConnectionString()))
            {
                sqlConnection.Open(); // Mở kết nối
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@maThongBao", employeeId); // Thêm tham số vào câu lệnh
                    int count = (int)cmd.ExecuteScalar(); // Lấy số lượng bản ghi
                    return count > 0; // Trả về true nếu mã đã tồn tại
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ThemThongBaoPB_Load(object sender, EventArgs e)
        {
            cmbPhongBan.Items.Clear();

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
        }
        private void UpdateCmbID()
        {
            cmbID.Items.Clear();
            string tenPhongBanCurrent = cmbPhongBan.SelectedItem?.ToString().Trim();
            if (!string.IsNullOrEmpty(tenPhongBanCurrent))
            {
                string query = "select maPhongBan from PhongBan where tenPhongBan = N'" + tenPhongBanCurrent + "'";
                DataTable datatabe = Function.GetDataQuery(query);
                foreach (DataRow row in datatabe.Rows)
                {
                    string id = row[0].ToString();
                    cmbID.Items.Add(id);
                }
            }
        }

        private void cmbPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCmbID();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Lấy data từ các controls
            string idPB = cmbID.SelectedItem?.ToString().Trim();
            string idTB = this.ID;
            string tieuDe = txtTieuDe.Text.Trim();
            string noiDung = txtNoiDung.Text.Trim();
            DateTime ngayDang = DateTime.Now;
            string fileDinhKem = lblLink.Text.ToString();

            if (idPB == null || tieuDe == null || noiDung == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo");
                return;
            }
            string query1 = "insert into ThongBao values ('" +idTB+ "',N'" + tieuDe + "', N'" + noiDung + "', '" + ngayDang + "', '" + fileDinhKem + "')";
            Function.UpdateDataQuery(query1);

            string query2 = "insert into PhongBan_ThongBao values ('" +idPB+ "', '" +idTB+ "')";
            Function.UpdateDataQuery(query2);
        }

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open file";
            openFile.Filter = "Fille pdf|*.pdf";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                lblLink.Text = openFile.FileName.ToString();
                picPDF.Visible = true;
            }
        }
    }
}
