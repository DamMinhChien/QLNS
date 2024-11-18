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
    public partial class PhongBanXemThongBaoForm : Form
    {
        private string maPhongBan;
        public PhongBanXemThongBaoForm()
        {
            InitializeComponent();
        }

        public PhongBanXemThongBaoForm(string maPhongBan)
        {
            InitializeComponent();
            this.maPhongBan = maPhongBan;
        }

        private void PhongBanXemThongBaoForm_Load(object sender, EventArgs e)
        {
            string query = "select tb.maThongBao ,tieuDe, noiDung,ngayDang, fileDinhKem from ThongBao tb inner join PhongBan_ThongBao pb_tb on tb.maThongBao = pb_tb.maThongBao inner join PhongBan pb on pb.maPhongBan = pb_tb.maPhongBan where pb.maPhongBan = '"+maPhongBan+"'";
            LoadDataGridView(dgvTB_PB, query);
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
            Function.LoadDataGridView(dgvTB_PB, query);
        }
        internal void RefreshData()
        {
            string query = "select tb.maThongBao ,tieuDe, noiDung,ngayDang, fileDinhKem from ThongBao tb inner join PhongBan_ThongBao pb_tb on tb.maThongBao = pb_tb.maThongBao inner join PhongBan pb on pb.maPhongBan = pb_tb.maPhongBan where pb.maPhongBan = '" + maPhongBan + "'";
            LoadDataGridView(dgvTB_PB, query);
        }
        private static string filePath_PB;

        public static string getPath()
        {
            return filePath_PB;
        }

        private void dgvTB_PB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem hàng được nhấn có hợp lệ không
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTB_PB.Rows[e.RowIndex];

                // Kiểm tra cột "fileDinhKem" có tồn tại không
                if (row.Cells["fileDinhKem"].Value != null)
                {
                    // Lấy đường dẫn file từ cột "fileDinhKem"
                    filePath_PB = row.Cells["fileDinhKem"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Không có giá trị trong cột fileDinhKem.");
                }
            }
        }
    }
}
