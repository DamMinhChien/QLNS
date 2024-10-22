using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public static class AutoResizer
    {
        // Biến lưu trữ kích thước ban đầu của form và control
        private static Dictionary<Control, Rectangle> originalSizes = new Dictionary<Control, Rectangle>();
        private static Size originalFormSize;

        // Phương thức khởi tạo, lưu kích thước ban đầu của form và các control
        public static void Init(Form form)
        {
            // Lưu kích thước ban đầu của form
            originalFormSize = form.Size;

            // Lưu kích thước ban đầu của các control trong form
            foreach (Control control in form.Controls)
            {
                originalSizes[control] = new Rectangle(control.Location, control.Size);
            }

            // Gán sự kiện Resize cho form
            form.Resize += (sender, e) => ResizeControls(form);
        }

        // Phương thức xử lý thay đổi kích thước theo tỷ lệ
        private static void ResizeControls(Form form)
        {
            // Tính toán tỷ lệ thay đổi kích thước
            float widthRatio = (float)form.ClientSize.Width / originalFormSize.Width;
            float heightRatio = (float)form.ClientSize.Height / originalFormSize.Height;

            // Cập nhật kích thước và vị trí cho tất cả các control trong form
            ResizeAllControls(form.Controls, widthRatio, heightRatio);
        }

        private static void ResizeAllControls(Control.ControlCollection controls, float widthRatio, float heightRatio)
        {
            foreach (Control control in controls)
            {
                if (originalSizes.ContainsKey(control))
                {
                    Rectangle originalControlSize = originalSizes[control];

                    // Cập nhật kích thước mới của control
                    control.Width = (int)(originalControlSize.Width * widthRatio);
                    control.Height = (int)(originalControlSize.Height * heightRatio);

                    // Cập nhật vị trí của control theo tỷ lệ
                    control.Left = (int)(originalControlSize.Left * widthRatio);
                    control.Top = (int)(originalControlSize.Top * heightRatio);
                }

                // Kiểm tra xem control có chứa các control con không
                if (control.HasChildren)
                {
                    ResizeAllControls(control.Controls, widthRatio, heightRatio);
                }
            }
        }
    }

}