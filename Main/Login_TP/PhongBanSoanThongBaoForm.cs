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
    public partial class PhongBanSoanThongBaoForm : Form
    {
        private string maPhongBan;

        public PhongBanSoanThongBaoForm()
        {
            InitializeComponent();
        }

        public PhongBanSoanThongBaoForm(string maPhongBan)
        {
            InitializeComponent();
            this.maPhongBan = maPhongBan;
        }

        private void PhongBanSoanThongBaoForm_Load(object sender, EventArgs e)
        {
            string query = "select tb.maThongBao, nv.hoTen,tieuDe, noiDung,ngayDang, fileDinhKem from ThongBao tb inner join NhanVien_ThongBao nv_tb on tb.maThongBao = nv_tb.maThongBao inner join NhanVien nv on nv.maNhanVien = nv_tb.maNhanVien where nv.maPhongBan = '"+maPhongBan+"'";
            LoadDataGridView(dgvTB_NV, query);
        }
        private void LoadDataGridView(DataGridView dgv, String myQuery)
        {
            dgv.Columns.Add("STT", "STT"); //thêm cột STT trước khi đổ data
            dgv.DataSource = Function.GetDataQuery(myQuery);

            // Điền số thứ tự vào cột STT
            for (int i = 0; i < dgv.Rows.Count - 1; i++)
            {
                dgv.Rows[i].Cells[0].Value = i + 1; // Gán số thứ tự
            }

            //Ẩn cột đường dẫn cuối cùng
            dgv.Columns["fileDinhKem"].Visible = false;

            Function.RemoveDuplicateColumns(dgv);
            Function.SoleRowColor(dgv);
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
        internal void LoadData(string query)
        {
            Function.LoadDataGridView(dgvTB_NV, query);
        }
        internal void RefreshData()
        {
            string query = "select tb.maThongBao, nv.hoTen,tieuDe, noiDung,ngayDang, fileDinhKem from ThongBao tb inner join NhanVien_ThongBao nv_tb on tb.maThongBao = nv_tb.maThongBao inner join NhanVien nv on nv.maNhanVien = nv_tb.maNhanVien where nv.maPhongBan = '" + maPhongBan + "'";
            LoadDataGridView(dgvTB_NV, query);
        }
        private static string filePath_NV;

        public static string getPath()
        {
            return filePath_NV;
        }

        private void dgvTB_NV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem hàng được nhấn có hợp lệ không
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTB_NV.Rows[e.RowIndex];

                // Kiểm tra cột "fileDinhKem" có tồn tại không
                if (row.Cells["fileDinhKem"].Value != null)
                {
                    // Lấy đường dẫn file từ cột "fileDinhKem"
                    filePath_NV = row.Cells["fileDinhKem"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Không có giá trị trong cột fileDinhKem.");
                }

                selectedMaThongBao = row.Cells["mathongbao"].Value.ToString().Trim();
                selectedHoTen = row.Cells["hoTen"].Value.ToString().Trim();
                selectedTieuDe = row.Cells["tieuDe"].Value.ToString().Trim();
                selectedNoiDung = row.Cells["noiDung"].Value.ToString().Trim();
                selectedFileDinhKem = row.Cells["fileDinhKem"].Value.ToString().Trim();

            }
        }

        private string selectedMaThongBao;
        private string selectedHoTen;
        private string selectedTieuDe;
        private string selectedNoiDung;
        private string selectedFileDinhKem;

        internal string getSelectedMaThongBao()
        {
            return selectedMaThongBao;
        }

        internal string getSelectedHoTen()
        {
            return selectedHoTen;
        }

        internal string getSelectedTieuDe()
        {
            return selectedTieuDe;
        }

        internal string getSelectedNoiDung()
        {
            return selectedNoiDung;
        }

        internal string getSelectedFieDinhKem()
        {
            return selectedFileDinhKem;
        }

        internal void deleteRow()
        {
            // Kiểm tra nếu có bản ghi nào được chọn
            if (string.IsNullOrEmpty(selectedMaThongBao))
            {
                MessageBox.Show("Vui lòng chọn một thông báo để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận trước khi xóa
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa thông báo này không?",
                                                 "Xác nhận xóa",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                // Câu lệnh SQL để xóa bản ghi
                string query1 = "DELETE FROM NhanVien_ThongBao WHERE maThongBao = @maThongBao"; // Sửa câu lệnh này
                string query2 = "DELETE FROM ThongBao WHERE maThongBao = @maThongBao";

                using (SqlConnection sqlConnection = new SqlConnection(Function.GetConnectionString()))
                {
                    sqlConnection.Open(); // Mở kết nối

                    using (SqlCommand cmd1 = new SqlCommand(query1, sqlConnection))
                    {
                        cmd1.Parameters.AddWithValue("@maThongBao", selectedMaThongBao); // Thêm tham số cho câu lệnh đầu tiên

                        try
                        {
                            // Thực thi câu lệnh xóa trong bảng NhanVien_ThongBao
                            int rowsAffected1 = cmd1.ExecuteNonQuery();

                            using (SqlCommand cmd2 = new SqlCommand(query2, sqlConnection))
                            {
                                cmd2.Parameters.AddWithValue("@maThongBao", selectedMaThongBao); // Thêm tham số cho câu lệnh thứ hai

                                // Thực thi câu lệnh xóa trong bảng ThongBao
                                int rowsAffected2 = cmd2.ExecuteNonQuery();

                                if (rowsAffected1 > 0 || rowsAffected2 > 0)
                                {
                                    MessageBox.Show("Thông báo đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    RefreshData(); // Cập nhật lại dữ liệu trong DataGridView
                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy thông báo để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        internal void exportFileExcel()
        {
            Function.ExportToExcel(dgvTB_NV);
        }
    }
}
