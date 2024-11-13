namespace Main
{
    partial class NhanVien_sys
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLogOut = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblTen = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLogOut
            // 
            this.btnLogOut.AutoSize = true;
            this.btnLogOut.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLogOut.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Location = new System.Drawing.Point(209, 335);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(195, 88);
            this.btnLogOut.TabIndex = 0;
            this.btnLogOut.Text = "Đăng xuất";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(453, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(254, 88);
            this.button1.TabIndex = 1;
            this.button1.Text = "Thoát ứng dụng";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Century Gothic", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTen.Location = new System.Drawing.Point(356, 132);
            this.lblTen.Name = "lblTen";
            this.lblTen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTen.Size = new System.Drawing.Size(206, 51);
            this.lblTen.TabIndex = 2;
            this.lblTen.Text = "Hệ thống";
            this.lblTen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NhanVien_sys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 609);
            this.Controls.Add(this.lblTen);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnLogOut);
            this.Name = "NhanVien_sys";
            this.Text = "NhanVien_sys";
            this.Load += new System.EventHandler(this.NhanVien_sys_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblTen;
    }
}