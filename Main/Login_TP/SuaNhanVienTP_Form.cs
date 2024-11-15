using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class SuaNhanVienTP_Form : Form
    {
        private string maNhanVien;
        private string hoTen;
        private string maPhongBan;
        private DateTime ngaySinh;
        private float luongCoBan;
        private string sdt;
        private string diaChi;
        private string email;
        private string chucVu;
        private string gioiTinh;

        public SuaNhanVienTP_Form()
        {
            InitializeComponent();
        }

        public SuaNhanVienTP_Form(string maNhanVien, string hoTen, string maPhongBan, DateTime ngaySinh, float luongCoBan, string sdt, string diaChi, string email, string chucVu, string gioiTinh)
        {
            InitializeComponent();
            this.maNhanVien = maNhanVien;
            this.hoTen = hoTen;
            this.maPhongBan = maPhongBan;
            this.ngaySinh = ngaySinh;
            this.luongCoBan = luongCoBan;
            this.sdt = sdt;
            this.diaChi = diaChi;
            this.email = email;
            this.chucVu = chucVu;
            this.gioiTinh = gioiTinh;
        }

        private void SuaNhanVienTP_Form_Load(object sender, EventArgs e)
        {
            txtID.Text = maNhanVien;
            txtHoTen.Text = hoTen;
            dtpNgaySinh.Value = ngaySinh;
            txtLuongCoBan.Text = luongCoBan.ToString();
            txtSDT.Text = sdt;
            txtDiaChi.Text = diaChi;
            txtEmail.Text = email;

            if (gioiTinh == "Nam")
            {
                rbtNam.Checked = true;
            }
            else if (gioiTinh == "Nữ")
            {
                rbtNu.Checked = true;
            }

            string query2 = "select distinct tenChucVu from ChucVu";
            DataTable dataTable2 = Function.GetDataQuery(query2);
            // Xóa các item cũ trong ComboBox
            cmbChucVu.Items.Clear();

            // Duyệt qua từng hàng trong DataTable
            foreach (DataRow row in dataTable2.Rows)
            {
                // Lấy giá trị của cột hoTen
                string chucVu = row[0].ToString();
                // Thêm vào ComboBox
                cmbChucVu.Items.Add(chucVu);
            }
            cmbChucVu.SelectedItem = chucVu;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các điều khiển
            string ID = txtID.Text.Trim();
            string tenNhanVien = txtHoTen.Text.Trim();
            string gioiTinh = rbtNam.Checked ? "Nam" : "Nữ";
            DateTime ngaySinh = dtpNgaySinh.Value;
            string formattedDate = ngaySinh.ToString("yyyy-MM-dd");
            string email = txtEmail.Text.Trim();
            string soDienThoai = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string maChucVu = cmbMaChucVu.SelectedItem?.ToString(); // ma chuc vu se null khi cmb null
            // Khai báo biến heSoLuong kiểu float
            float heSoLuong;
            if (!float.TryParse(txtLuongCoBan.Text.Trim(), out heSoLuong))
            {
                MessageBox.Show("Vui lòng nhập một giá trị hợp lệ cho hệ số lương.");
                return; // Ngừng thực hiện nếu không chuyển đổi thành công
            }
            // Kiểm tra email
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isEmailValid = Regex.IsMatch(email, emailPattern);

            // Kiểm tra số điện thoại
            string phonePattern = @"^(\+84|0)[1-9][0-9]{8,9}$";
            bool isPhoneValid = Regex.IsMatch(soDienThoai, phonePattern);

            // Xuất kết quả kiểm tra
            if (!isEmailValid)
            {
                MessageBox.Show("Email không hợp lệ.");
                return;
            }

            if (!isPhoneValid)
            {
                MessageBox.Show("Số điện thoại không hợp lệ.");
                return;
            }
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(ID) ||
            string.IsNullOrEmpty(tenNhanVien) ||
            string.IsNullOrEmpty(soDienThoai) ||
            string.IsNullOrEmpty(diaChi) ||
            string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(maChucVu) ||
            string.IsNullOrEmpty(maPhongBan) ||
            string.IsNullOrEmpty(chucVu)
            )
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            string query = "update NhanVien set maNhanVien = '" + ID + "', hoTen = N'" + tenNhanVien + "', gioiTinh = N'" + gioiTinh + "', ngaySinh =  '" + formattedDate + "', soDienThoai =  '" + soDienThoai + "', diaChi = N'" + diaChi + "', email = '" + email + "', luongCoBan =  '" + luongCoBan + "',maPhongBan = '" + maPhongBan + "' , maChucVu = '" + maChucVu + "' where maNhanVien = '" + this.maNhanVien + "' ";

            Function.UpdateDataQuery(query);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtHoTen.Text = "";
            rbtNam.Checked = false;
            dtpNgaySinh.Value = DateTime.Now; // Gán về giá trị mặc định (ví dụ: ngày hiện tại)
            txtLuongCoBan.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            cmbChucVu.SelectedIndex = -1; // Bỏ chọn trong ComboBox
            cmbMaChucVu.SelectedIndex = -1; // Bỏ chọn trong ComboBox
        }
        private void UpdateMaChucVu()
        {
            cmbMaChucVu.Items.Clear();
            string tenChucVuCurrent = cmbChucVu.SelectedItem?.ToString().Trim();

            // Kiểm tra xem có giá trị nào được chọn không
            if (!string.IsNullOrEmpty(tenChucVuCurrent))
            {
                string query3 = "select maChucVu from ChucVu where tenChucVu = N'" + tenChucVuCurrent + "' ";
                DataTable datatabe3 = Function.GetDataQuery(query3);
                foreach (DataRow row in datatabe3.Rows)
                {
                    string maChucVu = row[0].ToString();
                    cmbMaChucVu.Items.Add(maChucVu);
                }

            }
        }

        private void cmbChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMaChucVu();
        }
    }
}
