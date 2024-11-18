namespace Main
{
    partial class PhongBanXemThongBaoForm
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
            this.dgvTB_PB = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTB_PB)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTB_PB
            // 
            this.dgvTB_PB.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTB_PB.BackgroundColor = System.Drawing.Color.White;
            this.dgvTB_PB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTB_PB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTB_PB.Location = new System.Drawing.Point(0, 0);
            this.dgvTB_PB.Name = "dgvTB_PB";
            this.dgvTB_PB.ReadOnly = true;
            this.dgvTB_PB.RowHeadersWidth = 51;
            this.dgvTB_PB.RowTemplate.Height = 24;
            this.dgvTB_PB.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTB_PB.Size = new System.Drawing.Size(936, 340);
            this.dgvTB_PB.TabIndex = 2;
            this.dgvTB_PB.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTB_PB_CellClick);
            // 
            // PhongBanXemThongBaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 340);
            this.Controls.Add(this.dgvTB_PB);
            this.Name = "PhongBanXemThongBaoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PhongBanXemThongBaoForm";
            this.Load += new System.EventHandler(this.PhongBanXemThongBaoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTB_PB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTB_PB;
    }
}