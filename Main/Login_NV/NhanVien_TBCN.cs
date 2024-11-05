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
    public partial class NhanVien_TBCN : Form
    {
        private string maNhanVien;
        public NhanVien_TBCN()
        {
            InitializeComponent();
        }

        public NhanVien_TBCN(string maNhanVien)
        {
            InitializeComponent();
            this.maNhanVien = maNhanVien;
        }

        private void NhanVien_TBCN_Load(object sender, EventArgs e)
        {
            string query = "select tb.maThongBao, nv.hoTen,tieuDe, noiDung,ngayDang, fileDinhKem from ThongBao tb inner join NhanVien_ThongBao nv_tb on tb.maThongBao = nv_tb.maThongBao inner join NhanVien nv on nv.maNhanVien = nv_tb.maNhanVien where nv.maNhanVien = '"+maNhanVien+"'";
            LoadDataGridView(dgvTB_CaNhan, query);
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


    }
}
