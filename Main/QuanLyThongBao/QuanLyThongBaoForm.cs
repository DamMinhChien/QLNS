using Microsoft.Office.Interop.Excel;
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
    public partial class QuanLyThongBaoForm : Form
    {
        public QuanLyThongBaoForm()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        const string shadowText = "Nhập tên tiêu đề thông báo";
        private void QuanLyThongBaoForm_Load(object sender, EventArgs e)
        {
            OpenChildForm(new ThongBao_PhongBanForm());
            Function.Find(txtTimTB, shadowText);
            currentButton = "PB";
            cậpNhậtToolStripMenuItem_Click(sender, e);
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
        private string currentButton; // Biến để theo dõi nút nào đã nhấn
        private void btnPhongBan_Click(object sender, EventArgs e)
        {
            currentButton = "PB";
            OpenChildForm(new ThongBao_PhongBanForm());
            cậpNhậtToolStripMenuItem_Click(sender, e);
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            currentButton = "NV";
            OpenChildForm(new ThongBao_NhanVienForm());
            cậpNhậtToolStripMenuItem_Click(sender, e);
        }
        
        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(currentButton == "NV")
            {
                ThemThongBaoNVForm themThongBaoNVForm = new ThemThongBaoNVForm();
                themThongBaoNVForm.ShowDialog();
            }
            if (currentButton == "PB")
            {
                ThemThongBaoPBForm themThongBaoPBForm = new ThemThongBaoPBForm();
                themThongBaoPBForm.ShowDialog();
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
            if (currentButton == "NV")
            {
                string query = "select tb.maThongBao, nv.hoTen,tieuDe, noiDung,ngayDang, fileDinhKem from ThongBao tb inner join NhanVien_ThongBao nv_tb on tb.maThongBao = nv_tb.maThongBao inner join NhanVien nv on nv.maNhanVien = nv_tb.maNhanVien where tieuDe like N'%" +search+ "%'";
                // Sử dụng query để lấy dữ liệu và hiển thị kết quả trên form con ThongBao_NhanVienForm
                ((ThongBao_NhanVienForm)currentFormChild).LoadData(query);
            }
            if (currentButton == "PB")
            {
                string query = "select tb.maThongBao, tenPhongBan,tieuDe, noiDung,ngayDang, fileDinhKem from ThongBao tb inner join PhongBan_ThongBao pb_tb on tb.maThongBao = pb_tb.maThongBao inner join PhongBan pb on pb.maPhongBan = pb_tb.maPhongBan where tieuDe like N'%" +search+ "%'";
                // Sử dụng query để lấy dữ liệu và hiển thị kết quả trên form con ThongBao_PhongBanForm
                ((ThongBao_PhongBanForm)currentFormChild).LoadData(query);
            }
        }

        private void cậpNhậtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentButton == "NV")
            {
                ((ThongBao_NhanVienForm)currentFormChild).RefreshData();
            }
            if (currentButton == "PB")
            {
                ((ThongBao_PhongBanForm)currentFormChild).RefreshData();
            }
        }
        private string selectedMaThongBao;
        private string selectedTenPhongBan;
        private string selectedHoTen;
        private string selectedTieuDe;
        private string selectedNoiDung;
        private string selectedFieDinhKem;
        private void sửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentButton == "NV")
            {
                selectedMaThongBao = ((ThongBao_NhanVienForm)currentFormChild).getSelectedMaThongBao();
                selectedHoTen = ((ThongBao_NhanVienForm)currentFormChild).getSelectedHoTen();
                selectedTieuDe = ((ThongBao_NhanVienForm)currentFormChild).getSelectedTieuDe();
                selectedNoiDung = ((ThongBao_NhanVienForm)currentFormChild).getSelectedNoiDung();
                selectedFieDinhKem = ((ThongBao_NhanVienForm)currentFormChild).getSelectedFieDinhKem();
                SuaThongBaoNVForm suaThongBaoNVForm = new SuaThongBaoNVForm(selectedMaThongBao, selectedHoTen, selectedTieuDe, selectedNoiDung, selectedFieDinhKem);
                suaThongBaoNVForm.ShowDialog();

            }
            if (currentButton == "PB")
            {
                selectedMaThongBao = ((ThongBao_PhongBanForm)currentFormChild).getSelectedMaThongBao();
                selectedTenPhongBan = ((ThongBao_PhongBanForm)currentFormChild).getSelectedHoTen();
                selectedTieuDe = ((ThongBao_PhongBanForm)currentFormChild).getSelectedTieuDe();
                selectedNoiDung = ((ThongBao_PhongBanForm)currentFormChild).getSelectedNoiDung();
                selectedFieDinhKem = ((ThongBao_PhongBanForm)currentFormChild).getSelectedFieDinhKem();
                SuaThongBaoPBForm suaThongBaoNVForm = new SuaThongBaoPBForm(selectedMaThongBao, selectedTenPhongBan, selectedTieuDe, selectedNoiDung, selectedFieDinhKem);
                suaThongBaoNVForm.ShowDialog();
            }
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentButton == "NV")
            {
                ((ThongBao_NhanVienForm)currentFormChild).deleteRow();
            }
            if (currentButton == "PB")
            {
                ((ThongBao_PhongBanForm)currentFormChild).deleteRow();
            }
        }

        private void thốngkêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentButton == "NV")
            {
                ((ThongBao_NhanVienForm)currentFormChild).exportFileExcel();
            }
            if (currentButton == "PB")
            {
                ((ThongBao_PhongBanForm)currentFormChild).exportFileExcel();
            }
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.LogOutApp(this);
        }

        private void thoátỨngDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.ExitApp();
        }
    }
}
