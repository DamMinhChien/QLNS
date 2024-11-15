using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class QuanLyPhongBanForm : Form
    {
        private bool canChangePhongBan;
        public QuanLyPhongBanForm()
        {
            InitializeComponent();
        }

        public QuanLyPhongBanForm(bool canChangePhongBan)
        {
            InitializeComponent();
            this.canChangePhongBan = canChangePhongBan;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        const string shadowText = "Nhập tên phòng ban";
        private void QuanLyPhongBanForm_Load(object sender, EventArgs e)
        {
            Function.Find(txtTimPB, shadowText);
            Function.LoadDataGridView(dgvDanhSachPhongBan, "SELECT PhongBan.maPhongBan, tenPhongBan, COUNT(NhanVien.hoTen) AS SoLuongNhanVien, heSoPhongBan FROM PhongBan left JOIN NhanVien ON PhongBan.maPhongBan = NhanVien.maPhongBan GROUP BY PhongBan.maPhongBan, tenPhongBan, heSoPhongBan;");
            if (!canChangePhongBan)
            {
                menuStrip2.Visible = false;
                contextMenuStrip1.Visible = false;
                contextMenuStrip1.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTimPB_Enter(object sender, EventArgs e)
        {
            Function.Enter_text(txtTimPB, shadowText);
        }

        private void txtTimPB_Leave(object sender, EventArgs e)
        {
            Function.Leave(txtTimPB, shadowText);
        }

        private void picTimPB_Click(object sender, EventArgs e)
        {
            string search = txtTimPB.Text.Trim();
            if (string.IsNullOrEmpty(search) || txtTimPB.Font.Style.HasFlag(FontStyle.Italic))
            {
                return;
            }
            string query = "SELECT PhongBan.maPhongBan, tenPhongBan, COUNT(NhanVien.hoTen) AS SoLuongNhanVien, heSoPhongBan FROM PhongBan left JOIN NhanVien ON PhongBan.maPhongBan = NhanVien.maPhongBan where tenPhongBan like '%" +search+ "%' GROUP BY PhongBan.maPhongBan, tenPhongBan, heSoPhongBan;";
            Function.LoadDataGridView(dgvDanhSachPhongBan, query);
        }

        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemPhongBanForm themPhongBanForm = new ThemPhongBanForm();
            themPhongBanForm.ShowDialog();
        }

        private void cậpNhậtToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Function.LoadDataGridView(dgvDanhSachPhongBan, "SELECT PhongBan.maPhongBan, tenPhongBan, COUNT(NhanVien.hoTen) AS SoLuongNhanVien, heSoPhongBan FROM PhongBan left JOIN NhanVien ON PhongBan.maPhongBan = NhanVien.maPhongBan GROUP BY PhongBan.maPhongBan, tenPhongBan, heSoPhongBan;");
        
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.LogOutApp(this);
        }

        private void thoátỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExitApp();
        }

        private string selectedMaPhongBan;
        private string selectedTenPhongBan;
        private float selectedHeSoPhongBan;

        private void dgvDanhSachPhongBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachPhongBan.Rows[e.RowIndex];
                selectedMaPhongBan = row.Cells[1].Value.ToString();
                selectedTenPhongBan = row.Cells[2].Value.ToString();
                float.TryParse(row.Cells[3].Value.ToString(), out selectedHeSoPhongBan);
            }
        }

        private void sửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaPhongBan))
            {
                MessageBox.Show("Vui lòng chọn một phòng ban để sửa.");
                return;
            }
            SuaPhongBanForm suaPhongBanForm = new SuaPhongBanForm(selectedMaPhongBan, selectedTenPhongBan,selectedHeSoPhongBan);
            suaPhongBanForm.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaPhongBan))
            {
                MessageBox.Show("Vui lòng chọn một phòng ban để xóa.");
                return;
            }

            // Xác nhận việc xóa
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng ban này?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM PhongBan WHERE maPhongBan = '" + selectedMaPhongBan + "'";
                Function.UpdateDataQuery(query);
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
            Function.ExportToExcel(dgvDanhSachPhongBan);
        }
    }
}
