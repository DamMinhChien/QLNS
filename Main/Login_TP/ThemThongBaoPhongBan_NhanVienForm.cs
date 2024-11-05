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
    public partial class ThemThongBaoPhongBan_NhanVienForm : Form
    {
        private string ID;
        private string tenPhongBan;
        public ThemThongBaoPhongBan_NhanVienForm()
        {
            InitializeComponent();
        }

        public ThemThongBaoPhongBan_NhanVienForm(string tenPhongBan)
        {
            InitializeComponent();
            this.ID = GenerateRandomEmployeeId();
            this.tenPhongBan = tenPhongBan;
        }
        private void UpdateCmbHoTen()
        {
            cmbTen.Items.Clear();
            string tenPhongBanCurrent = txtPhongBan.Text.Trim();
            string tenChucVuCurrent = cmbChucVu.SelectedItem?.ToString().Trim();

            // Kiểm tra xem có giá trị nào được chọn không
            if (!string.IsNullOrEmpty(tenPhongBanCurrent) || !string.IsNullOrEmpty(tenChucVuCurrent))
            {
                string query3 = "select hoTen from NhanVien nv inner join PhongBan pb on nv.maPhongBan = pb.maPhongBan inner join ChucVu cv on cv.maChucVu = nv.maChucVu where tenPhongBan = N'" + tenPhongBanCurrent + "' and tenChucVu = N'" + tenChucVuCurrent + "'";
                DataTable datatabe3 = Function.GetDataQuery(query3);
                foreach (DataRow row in datatabe3.Rows)
                {
                    string hoTen = row[0].ToString();
                    cmbTen.Items.Add(hoTen);
                }

            }

        }
        private void UpdateCmbID()
        {
            cmbID.Items.Clear();
            string hoTenCurrent = cmbTen.SelectedItem?.ToString().Trim();
            if (!string.IsNullOrEmpty(hoTenCurrent))
            {
                string query4 = "select maNhanVien from NhanVien where hoTen = N'" + hoTenCurrent + "'";
                DataTable datatabe4 = Function.GetDataQuery(query4);
                foreach (DataRow row in datatabe4.Rows)
                {
                    string id = row[0].ToString();
                    cmbID.Items.Add(id);
                }
            }
        }
        private void ThemThongBaoPhongBan_NhanVienForm_Load(object sender, EventArgs e)
        {
            txtPhongBan.Text = tenPhongBan;
            cmbChucVu.Items.Clear();

            string query = "select distinct tenChucVu from ChucVu";
            DataTable dataTable = Function.GetDataQuery(query);
            // Xóa các item cũ trong ComboBox
            cmbChucVu.Items.Clear();

            // Duyệt qua từng hàng trong DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Lấy giá trị của cột hoTen
                string chucVu = row[0].ToString();
                // Thêm vào ComboBox
                cmbChucVu.Items.Add(chucVu);
            }
        }

        private void cmbChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCmbHoTen();
        }

        private void cmbTen_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCmbID();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Lấy data từ các controls
            string idNV = cmbID.SelectedItem?.ToString().Trim();
            string idTB = this.ID;
            string tieuDe = txtTieuDe.Text.Trim();
            string noiDung = txtNoiDung.Text.Trim();
            DateTime ngayDang = DateTime.Now;
            string fileDinhKem = lblLink.Text.ToString();

            if (idNV == null || tieuDe == null || noiDung == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo");
                return;
            }
            string query1 = "insert into ThongBao values ('" + idTB + "',N'" + tieuDe + "', N'" + noiDung + "', '" + ngayDang + "', '" + fileDinhKem + "')";
            Function.UpdateDataQuery(query1);

            string query2 = "insert into NhanVien_ThongBao values ('" + idNV + "', '" + idTB + "')";
            Function.UpdateDataQuery(query2);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
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
