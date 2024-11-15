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
            this.components = new System.ComponentModel.Container();
            this.dgvTB_CaNhan = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTB_CaNhan)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTB_CaNhan
            // 
            this.dgvTB_CaNhan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTB_CaNhan.BackgroundColor = System.Drawing.Color.White;
            this.dgvTB_CaNhan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTB_CaNhan.ContextMenuStrip = this.contextMenuStrip2;
            this.dgvTB_CaNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTB_CaNhan.Location = new System.Drawing.Point(0, 0);
            this.dgvTB_CaNhan.Name = "dgvTB_CaNhan";
            this.dgvTB_CaNhan.ReadOnly = true;
            this.dgvTB_CaNhan.RowHeadersWidth = 51;
            this.dgvTB_CaNhan.RowTemplate.Height = 24;
            this.dgvTB_CaNhan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTB_CaNhan.Size = new System.Drawing.Size(900, 394);
            this.dgvTB_CaNhan.TabIndex = 3;
            this.dgvTB_CaNhan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTB_CaNhan_CellClick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem9,
            this.toolStripMenuItem10});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(232, 56);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Image = global::Main.Properties.Resources.downloadfolder_993671;
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItem9.Size = new System.Drawing.Size(231, 26);
            this.toolStripMenuItem9.Text = "Download PDF";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Image = global::Main.Properties.Resources.repeat_update_refresh_reload_sync_icon_225556;
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.toolStripMenuItem10.Size = new System.Drawing.Size(231, 26);
            this.toolStripMenuItem10.Text = "Update";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.toolStripMenuItem10_Click);
            // 
            // NhanVien_TBCN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 394);
            this.Controls.Add(this.dgvTB_CaNhan);
            this.Name = "NhanVien_TBCN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NhanVien_TBCN";
            this.Load += new System.EventHandler(this.NhanVien_TBCN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTB_CaNhan)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTB_CaNhan;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
    }
}