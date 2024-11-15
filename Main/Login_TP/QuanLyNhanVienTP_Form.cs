using Microsoft.Office.Interop.Excel;
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
    public partial class QuanLyNhanVienTP_Form : Form
    {
        private string maPhongBan;
        private string tenPhongBan;
        public QuanLyNhanVienTP_Form()
        {
            InitializeComponent();
        }

        public QuanLyNhanVienTP_Form(string maPhongBan, string tenPhongBan)
        {
            InitializeComponent();
            this.maPhongBan = maPhongBan;
            this.tenPhongBan = tenPhongBan;
        }
        const string shadowText = "Nhập tên nhân viên";

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void QuanLyNhanVienTP_Form_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "Danh sách thông báo " + tenPhongBan;
            Function.Find(txtTimNV, shadowText);
            string query = "SELECT NV.maNhanVien, NV.hoTen, CV.tenChucVu, PB.tenPhongBan, NV.luongCoBan, NV.gioiTinh, NV.ngaySinh, NV.soDienThoai, NV.diaChi, NV.email FROM NhanVien NV JOIN PhongBan PB ON NV.maPhongBan = PB.maPhongBan join ChucVu CV on NV.maChucVu = CV.maChucVu where pb.maPhongBan = '"+maPhongBan+"'";
            Function.LoadDataGridView(dvgDanhSachNhanVien,query);
        }

        private void thoátỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExitApp();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.LogOutApp(this);
        }

        private void txtTimNV_Enter(object sender, EventArgs e)
        {
            Function.Enter_text(txtTimNV, shadowText);
        }

        private void txtTimNV_Leave(object sender, EventArgs e)
        {
            Function.Leave(txtTimNV, shadowText);
        }

        private void picTimNV_Click(object sender, EventArgs e)
        {
            string search = txtTimNV.Text.Trim();
            if (string.IsNullOrEmpty(search) || txtTimNV.Font.Style.HasFlag(FontStyle.Italic))
            {
                return;
            }
            string query = "SELECT NV.maNhanVien, NV.hoTen, CV.tenChucVu, PB.tenPhongBan, NV.luongCoBan, NV.gioiTinh, NV.ngaySinh, NV.soDienThoai, NV.diaChi, NV.email FROM NhanVien NV JOIN PhongBan PB ON NV.maPhongBan = PB.maPhongBan join ChucVu CV on NV.maChucVu = CV.maChucVu where hoTen like N'%" + search + "%' and pb.maPhongBan = '"+maPhongBan+"'";
            Function.LoadDataGridView(dvgDanhSachNhanVien, query);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ThemNhanVienTP_Form themNhanVienTP = new ThemNhanVienTP_Form(maPhongBan);
            themNhanVienTP.ShowDialog();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string query = "SELECT NV.maNhanVien, NV.hoTen, CV.tenChucVu, PB.tenPhongBan, NV.luongCoBan, NV.gioiTinh, NV.ngaySinh, NV.soDienThoai, NV.diaChi, NV.email FROM NhanVien NV JOIN PhongBan PB ON NV.maPhongBan = PB.maPhongBan join ChucVu CV on NV.maChucVu = CV.maChucVu where pb.maPhongBan = '" + maPhongBan + "'";
            Function.LoadDataGridView(dvgDanhSachNhanVien,query);
        }
        private string selectedMaNhanVien; // Chỉ số 1
        private string selectedHoTen; // Chỉ số 2
        private string selectedTenChucVu; // Chỉ số 3
        private string selectedPhongBan; // Chỉ số 4
        private float selectedLuongCoBan; // Chỉ số 5
        private string selectedGioiTinh; // Chỉ số 6
        private DateTime selectedNgaySinh; // Chỉ số 7
        private string selectedSDT; // Chỉ số 8
        private string selectedDiaChi; // Chỉ số 9
        private string selectedEmail; // Chỉ số 10

        private void dvgDanhSachNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dvgDanhSachNhanVien.Rows[e.RowIndex];
                selectedMaNhanVien = row.Cells[1].Value.ToString(); // maNhanVien (Chỉ số 1)
                selectedHoTen = row.Cells[2].Value.ToString(); // hoTen (Chỉ số 2)
                selectedTenChucVu = row.Cells[3].Value.ToString(); // tenChucVu (Chỉ số 3)
                selectedPhongBan = row.Cells[4].Value.ToString(); // tenPhongBan (Chỉ số 4)
                                                                  // Sử dụng TryParse
                float.TryParse(row.Cells[5].Value.ToString(), out selectedLuongCoBan); // luongCoBan (Chỉ số 5)

                selectedGioiTinh = row.Cells[6].Value.ToString(); // gioiTinh (Chỉ số 6)

                DateTime.TryParse(row.Cells[7].Value.ToString(), out selectedNgaySinh); // ngaySinh (Chỉ số 7)
                selectedSDT = row.Cells[8].Value.ToString(); // soDienThoai (Chỉ số 8)
                selectedDiaChi = row.Cells[9].Value.ToString(); // diaChi (Chỉ số 9)
                selectedEmail = row.Cells[10].Value.ToString(); // email (Chỉ số 10)
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaNhanVien))
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa.");
                return;
            }
            SuaNhanVienTP_Form suaNhanVienForm = new SuaNhanVienTP_Form(
                selectedMaNhanVien,
                selectedHoTen,
                maPhongBan,
                selectedNgaySinh,
                selectedLuongCoBan,
                selectedSDT,
                selectedDiaChi,
                selectedEmail,
                selectedTenChucVu, // thêm thuộc tính tenChucVu
                selectedGioiTinh
            );
            suaNhanVienForm.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaNhanVien))
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.");
                return;
            }

            // Xác nhận việc xóa
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string query1 = "DELETE FROM NhanVien_ThongBao WHERE maNhanVien = '" + selectedMaNhanVien + "'";
                //string query2 = "DELETE FROM HopDongLaoDong WHERE maNhanVien = '"+selectedMaNhanVien+"';";
                string query3 = "DELETE FROM ChamCong WHERE maNhanVien = '" + selectedMaNhanVien + "';";
                string query4 = "DELETE FROM NhanVien WHERE maNhanVien = '" + selectedMaNhanVien + "';";
                using (SqlConnection connection = new SqlConnection(Function.GetConnectionString()))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand())
                            {
                                command.Connection = connection;
                                command.Transaction = transaction;

                                // Lệnh DELETE đầu tiên
                                command.CommandText = query1;
                                command.ExecuteNonQuery();

                                // Lệnh DELETE thứ hai
                                //command.CommandText = query2;
                                //command.ExecuteNonQuery();

                                // Lệnh DELETE thứ ba
                                command.CommandText = query3;
                                command.ExecuteNonQuery();

                                // Lệnh DELETE cuối cùng
                                command.CommandText = query4;
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Xóa thành công!");
                                }
                                else
                                {
                                    MessageBox.Show("Không có bản ghi nào được xóa.");
                                }
                            }

                            // Xác nhận giao dịch
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // Rollback nếu có lỗi
                            transaction.Rollback();
                            // Xử lý lỗi
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                //refresh luôn
                toolStripMenuItem4_Click(sender, e);
            }
        }

        private void thốngkêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExportToExcel(dvgDanhSachNhanVien);
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1_Click(sender, e);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2_Click(sender, e);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripMenuItem3_Click(sender, e);
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripMenuItem4_Click(sender,e);
        }
    }
}
