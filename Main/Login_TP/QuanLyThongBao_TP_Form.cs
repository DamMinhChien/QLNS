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
    public partial class QuanLyThongBao_TP_Form : Form
    {
        private string maPhongBan;
        private string tenPhongBan;
        public QuanLyThongBao_TP_Form()
        {
            InitializeComponent();
        }

        public QuanLyThongBao_TP_Form(string maPhongBan, string tenPhongBan)
        {
            InitializeComponent();
            this.maPhongBan = maPhongBan;
            this.tenPhongBan = tenPhongBan;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        const string shadowText = "Nhập tên tiêu đề thông báo";
        private void QuanLyThongBao_TP_Form_Load(object sender, EventArgs e)
        {
            OpenChildForm(new PhongBanXemThongBaoForm(maPhongBan));
            lblTitle.Text = "Danh sách thông báo "+tenPhongBan;
            Function.Find(txtTimTB, shadowText);
            currentButton = "PB";
            refresh_Click(sender, e);
        }

        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
                currentFormChild.Close();

            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel_Body.Controls.Add(childForm);
            panel_Body.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }
        private string currentButton;

       

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            currentButton = "NV";
            OpenChildForm(new PhongBanSoanThongBaoForm(maPhongBan));
            refresh_Click(sender, e);
        }


        private void refresh_Click(object sender, EventArgs e)
        {
            if (currentButton == "NV")
            {
                ((PhongBanSoanThongBaoForm)currentFormChild).RefreshData();
            }
            if (currentButton == "PB")
            {
                ((PhongBanXemThongBaoForm)currentFormChild).RefreshData();
            }
        }

        private void txtTimTB_Enter(object sender, EventArgs e)
        {
            Function.Enter_text(txtTimTB, shadowText);
        }

        private void txtTimTB_Leave(object sender, EventArgs e)
        {
            Function.Leave(txtTimTB, shadowText);
        }

        private void picTimTK_Click(object sender, EventArgs e)
        {
            string search = txtTimTB.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                if (currentButton == "NV")
                {
                    string query1 = "select tb.maThongBao, nv.hoTen,tieuDe, noiDung,ngayDang, fileDinhKem from ThongBao tb inner join NhanVien_ThongBao nv_tb on tb.maThongBao = nv_tb.maThongBao inner join NhanVien nv on nv.maNhanVien = nv_tb.maNhanVien where tieuDe like N'%" + search + "%'";
                    // Sử dụng query để lấy dữ liệu và hiển thị kết quả trên form con ThongBao_NhanVienForm
                    ((ThongBao_NhanVienForm)currentFormChild).LoadData(query1);
                }
                if (currentButton == "PB")
                {
                    string query2 = "select tb.maThongBao,tieuDe, noiDung,ngayDang, fileDinhKem from ThongBao tb inner join PhongBan_ThongBao pb_tb on tb.maThongBao = pb_tb.maThongBao inner join PhongBan pb on pb.maPhongBan = pb_tb.maPhongBan where tieuDe like N'%" + search + "%'";
                    // Sử dụng query để lấy dữ liệu và hiển thị kết quả trên form con ThongBao_PhongBanForm
                    ((ThongBao_PhongBanForm)currentFormChild).LoadData(query2);
                }
            }
        }

        private void btnPhongBan_Click_1(object sender, EventArgs e)
        {
            currentButton = "PB";
            OpenChildForm(new PhongBanXemThongBaoForm(maPhongBan));
            refresh_Click(sender, e);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string filePath;
            if (currentButton == "NV")
            {
                filePath = ThongBao_NhanVienForm.getPath();
            }
            else
            {
                filePath = ThongBao_PhongBanForm.getPath();
            }
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Vui lòng chọn tệp để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại nếu không có tệp hợp lệ
            }
            //Mở hộp thoại lưu
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = System.IO.Path.GetFileName(filePath), // Đặt tên file mặc định
                Filter = "Text Files (*.pdf)|*.pdf",
                InitialDirectory = System.IO.Path.GetDirectoryName(filePath)
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lưu file vào đường dẫn đã chọn
                System.IO.File.Copy(filePath, saveFileDialog.FileName, overwrite: true);
                MessageBox.Show($"File saved to {saveFileDialog.FileName}");
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentButton == "NV")
            {
                ThemThongBaoPhongBan_NhanVienForm themThongBao = new ThemThongBaoPhongBan_NhanVienForm(tenPhongBan);
                themThongBao.Show();
            }
        }

        private string selectedMaThongBao;
        private string selectedHoTen;
        private string selectedTieuDe;
        private string selectedNoiDung;
        private string selectedFieDinhKem;

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (currentButton == "NV")
            {
                selectedMaThongBao = ((PhongBanSoanThongBaoForm)currentFormChild).getSelectedMaThongBao();
                selectedHoTen = ((PhongBanSoanThongBaoForm)currentFormChild).getSelectedHoTen();
                selectedTieuDe = ((PhongBanSoanThongBaoForm)currentFormChild).getSelectedTieuDe();
                selectedNoiDung = ((PhongBanSoanThongBaoForm)currentFormChild).getSelectedNoiDung();
                selectedFieDinhKem = ((PhongBanSoanThongBaoForm)currentFormChild).getSelectedFieDinhKem();
                PhongBanSuaThongBaoForm phongBanSuaThongBao = new PhongBanSuaThongBaoForm(selectedMaThongBao, selectedHoTen, selectedTieuDe, selectedNoiDung, selectedFieDinhKem);
                phongBanSuaThongBao.Show();

            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (currentButton == "NV")
            {
                ((PhongBanSoanThongBaoForm)currentFormChild).deleteRow();
            }
            
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void btnPhongBan_MouseEnter(object sender, EventArgs e)
        {
            Function.EnterFormat(btnPhongBan);
        }

        private void btnPhongBan_MouseLeave(object sender, EventArgs e)
        {
            Function.LeaveFormat(btnPhongBan);
        }

        private void btnNhanVien_MouseEnter(object sender, EventArgs e)
        {
            Function.EnterFormat(btnNhanVien);
        }

        private void btnNhanVien_MouseLeave(object sender, EventArgs e)
        {
            Function.LeaveFormat(btnNhanVien);
        }
    }
}
