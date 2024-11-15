using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Main
{
    public partial class QuanLyTaiKhoanForm : Form
    {
        private string ID;
        public QuanLyTaiKhoanForm()
        {
            InitializeComponent();
        }

        public QuanLyTaiKhoanForm(string ID)
        {
            this.ID = ID;
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sắpXếpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        const string shadowText = "Nhập tên tài khoản";
        private void QuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            Function.Find(txtTimTK, shadowText);
            Function.LoadDataGridView(dgvDanhSachTaiKhoan, "select * from TaiKhoan");
        }

        
        private void txtTimTK_Enter(object sender, EventArgs e)
        {
            Function.Enter_text(txtTimTK, shadowText);
        }

        private void txtTimTK_Leave(object sender, EventArgs e)
        {
            Function.Leave(txtTimTK, shadowText);
        }

        private void picTimTK_Click(object sender, EventArgs e)
        {
            string search = txtTimTK.Text.Trim();
            if (string.IsNullOrEmpty(search) || txtTimTK.Font.Style.HasFlag(FontStyle.Italic))
            {
                return;
            }
            string query = "select * from TaiKhoan where tenDangNhap like '%" + search + "%'";
            Function.LoadDataGridView(dgvDanhSachTaiKhoan, query);
        }

        private void txtTimTK_TextChanged(object sender, EventArgs e)
        {

        }

        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemTaiKhoanForm themTaiKhoanForm = new ThemTaiKhoanForm();
            themTaiKhoanForm.ShowDialog();
        }

        private void cậpNhậtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.LoadDataGridView(dgvDanhSachTaiKhoan, "select * from TaiKhoan ");
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.LogOutApp(this);
        }

        private void thoátỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExitApp();
        }

        private string selectedMaTaiKhoan;
        private string selectedTenDangNhap;
        private string selectedMatKhau;
        private string selectedMaNhanVien;

        private void sửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaTaiKhoan))
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để sửa.");
                return;
            }
            SuaTaiKhoanForm suaTaiKhoanForm = new SuaTaiKhoanForm(selectedMaTaiKhoan, selectedTenDangNhap, selectedMatKhau, selectedMaNhanVien);
            suaTaiKhoanForm.ShowDialog();
        }

        private void dgvDanhSachTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachTaiKhoan.Rows[e.RowIndex];
                selectedMaTaiKhoan = row.Cells[1].Value.ToString();
                selectedTenDangNhap = row.Cells[2].Value.ToString();
                selectedMatKhau = row.Cells[3].Value.ToString();
                selectedMaNhanVien = row.Cells[4].Value.ToString();
            }
        }

        private void dgvDanhSachTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(selectedMaTaiKhoan == ID)
            {
                MessageBox.Show("Không thể xóa tài khoản của chính mình!");
                return;
            }
            if (string.IsNullOrEmpty(selectedMaTaiKhoan))
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để xóa.");
                return;
            }

            // Xác nhận việc xóa
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string query1 = "DELETE FROM TaiKhoan WHERE maTaiKhoan = '" + selectedMaTaiKhoan + "'";
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
                                int rowsAffected = command.ExecuteNonQuery();
                                // Lệnh DELETE thứ hai
                                //command.CommandText = query2;


                                if (rowsAffected >= 0)
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
                cậpNhậtToolStripMenuItem_Click(sender, e);
            }
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void thốngkêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExportToExcel(dgvDanhSachTaiKhoan);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thêmToolStripMenuItem_Click(sender, e);
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
