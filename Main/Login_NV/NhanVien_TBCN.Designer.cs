namespace Main
{
    partial class NhanVien_TBCN
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
            this.dgvTB_CaNhan = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTB_CaNhan)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTB_CaNhan
            // 
            this.dgvTB_CaNhan.BackgroundColor = System.Drawing.Color.White;
            this.dgvTB_CaNhan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTB_CaNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTB_CaNhan.Location = new System.Drawing.Point(0, 0);
            this.dgvTB_CaNhan.Name = "dgvTB_CaNhan";
            this.dgvTB_CaNhan.ReadOnly = true;
            this.dgvTB_CaNhan.RowHeadersWidth = 51;
            this.dgvTB_CaNhan.RowTemplate.Height = 24;
            this.dgvTB_CaNhan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTB_CaNhan.Size = new System.Drawing.Size(900, 547);
            this.dgvTB_CaNhan.TabIndex = 3;
            // 
            // NhanVien_TBCN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 547);
            this.Controls.Add(this.dgvTB_CaNhan);
            this.Name = "NhanVien_TBCN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NhanVien_TBCN";
            this.Load += new System.EventHandler(this.NhanVien_TBCN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTB_CaNhan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTB_CaNhan;
    }
}