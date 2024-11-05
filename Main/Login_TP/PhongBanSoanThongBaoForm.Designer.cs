namespace Main
{
    partial class PhongBanSoanThongBaoForm
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
            this.dgvTB_NV = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTB_NV)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTB_NV
            // 
            this.dgvTB_NV.BackgroundColor = System.Drawing.Color.White;
            this.dgvTB_NV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTB_NV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTB_NV.Location = new System.Drawing.Point(0, 0);
            this.dgvTB_NV.Name = "dgvTB_NV";
            this.dgvTB_NV.ReadOnly = true;
            this.dgvTB_NV.RowHeadersWidth = 51;
            this.dgvTB_NV.RowTemplate.Height = 24;
            this.dgvTB_NV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTB_NV.Size = new System.Drawing.Size(936, 340);
            this.dgvTB_NV.TabIndex = 2;
            this.dgvTB_NV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTB_NV_CellClick);
            // 
            // PhongBanSoanThongBaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 340);
            this.Controls.Add(this.dgvTB_NV);
            this.Name = "PhongBanSoanThongBaoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PhongBanSoanThongBaoForm";
            this.Load += new System.EventHandler(this.PhongBanSoanThongBaoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTB_NV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTB_NV;
    }
}