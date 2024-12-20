﻿using System;
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
    public partial class QuanLyChucVuForm : Form
    {
        const string shadowText = "Nhập tên chức vụ";
        public QuanLyChucVuForm()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void QuanLyChucVu_Load(object sender, EventArgs e)
        {

            Function.Find(txtTimCV, shadowText);
            Function.LoadDataGridView(dgvDanhSachChucVu, "select * from ChucVu");
        }

        private void picTimCV_Click(object sender, EventArgs e)
        {
            string search = txtTimCV.Text.Trim();
            if (string.IsNullOrEmpty(search) || txtTimCV.Font.Style.HasFlag(FontStyle.Italic))
            {
                return;
            }
            string query = "select * from ChucVu where tenChucVu like '%"+search+"%'";
            Function.LoadDataGridView(dgvDanhSachChucVu, query);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ThemChucVuForm themChucVuForm = new ThemChucVuForm();
            themChucVuForm.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.LogOutApp(this);
        }

        private void thoátỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExitApp();
        }

        private string selectedMaChucVu;
        private string selectedTenChucVu;
        private float selectedHeSoChucVu;


        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Function.LoadDataGridView(dgvDanhSachChucVu, "select * from ChucVu");
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaChucVu))
            {
                MessageBox.Show("Vui lòng chọn một chức vụ để xóa.");
                return;
            }

            // Lấy mã nhân viên liên kết với chức vụ
            string query = $"SELECT maNhanVien FROM NhanVien WHERE maChucVu = '{selectedMaChucVu}'";
            DataTable dataTable = Function.GetDataQuery(query);

            // Xác nhận việc xóa
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa chức vụ này?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
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

                                // Xóa dữ liệu từ bảng ChamCong trước
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    string maNhanVien = row["maNhanVien"].ToString();
                                    string queryChamCong = $"DELETE FROM ChamCong WHERE maNhanVien = '{maNhanVien}';";
                                    command.CommandText = queryChamCong;
                                    command.ExecuteNonQuery();
                                }

                                // Xóa tài khoản của nhân viên
                                foreach (DataRow row in dataTable.Rows)
                                {
                                    string maNhanVien = row["maNhanVien"].ToString();
                                    string queryTaiKhoan = $"DELETE FROM TaiKhoan WHERE maNhanVien = '{maNhanVien}';";
                                    command.CommandText = queryTaiKhoan;
                                    command.ExecuteNonQuery();
                                }

                                // Xóa NhanVien_ThongBao
                                string queryNhanVien_ThongBao = $"DELETE FROM NhanVien_ThongBao WHERE maNhanVien IN (SELECT maNhanVien FROM NhanVien WHERE maChucVu = '{selectedMaChucVu}');";
                                command.CommandText = queryNhanVien_ThongBao;
                                command.ExecuteNonQuery();

                                // Xóa thông báo liên quan
                                string queryThongBao = $"DELETE FROM ThongBao WHERE maThongBao IN (SELECT maThongBao FROM NhanVien_ThongBao WHERE maNhanVien IN (SELECT maNhanVien FROM NhanVien WHERE maChucVu = '{selectedMaChucVu}'));";
                                command.CommandText = queryThongBao;
                                command.ExecuteNonQuery();

                                // Xóa nhân viên
                                string queryNhanVien = $"DELETE FROM NhanVien WHERE maChucVu = '{selectedMaChucVu}';";
                                command.CommandText = queryNhanVien;
                                command.ExecuteNonQuery();

                                // Xóa chức vụ
                                string queryChucVu = $"DELETE FROM ChucVu WHERE maChucVu = '{selectedMaChucVu}';";
                                command.CommandText = queryChucVu;
                                command.ExecuteNonQuery();

                                // Xác nhận giao dịch
                                transaction.Commit();
                                MessageBox.Show("Xóa thành công!");
                            }
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

                // Refresh dữ liệu
                toolStripMenuItem7_Click(sender, e);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaChucVu))
            {
                MessageBox.Show("Vui lòng chọn một chức vụ để sửa.");
                return;
            }

            SuaChucVuForm suaChucVuForm = new SuaChucVuForm(
                selectedMaChucVu,
                selectedTenChucVu,
                selectedHeSoChucVu
            );
            suaChucVuForm.ShowDialog();
        }

        private void dgvDanhSachChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachChucVu.Rows[e.RowIndex];
                selectedMaChucVu = row.Cells[1].Value.ToString();
                selectedTenChucVu = row.Cells[2].Value.ToString();
                float.TryParse(row.Cells[3].Value.ToString(), out selectedHeSoChucVu);
            }
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void thốngkêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExportToExcel(dgvDanhSachChucVu);
        }

        private void txtTimCV_Enter(object sender, EventArgs e)
        {
            Function.Enter_text(txtTimCV, shadowText);
        }

        private void txtTimCV_Leave(object sender, EventArgs e)
        {
            Function.Leave(txtTimCV, shadowText);
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            ThemChucVuForm themChucVuForm = new ThemChucVuForm();
            themChucVuForm.Show();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2_Click_1(sender, e);
        }
    }
}
