namespace View.OCR
{
    partial class OcrForm
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.打开影像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.二值化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.识别ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.二值化ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.txtContent = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(13, 40);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(545, 607);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.打开影像ToolStripMenuItem,
            this.二值化ToolStripMenuItem1,
            this.识别ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1060, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 24);
            // 
            // 打开影像ToolStripMenuItem
            // 
            this.打开影像ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.二值化ToolStripMenuItem,
            this.保存ToolStripMenuItem});
            this.打开影像ToolStripMenuItem.Name = "打开影像ToolStripMenuItem";
            this.打开影像ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.打开影像ToolStripMenuItem.Text = "文件";
            // 
            // 二值化ToolStripMenuItem
            // 
            this.二值化ToolStripMenuItem.Name = "二值化ToolStripMenuItem";
            this.二值化ToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.二值化ToolStripMenuItem.Text = "打开影像";
            this.二值化ToolStripMenuItem.Click += new System.EventHandler(this.二值化ToolStripMenuItem_Click);
            // 
            // 识别ToolStripMenuItem
            // 
            this.识别ToolStripMenuItem.Name = "识别ToolStripMenuItem";
            this.识别ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.识别ToolStripMenuItem.Text = "识别";
            this.识别ToolStripMenuItem.Click += new System.EventHandler(this.识别ToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 二值化ToolStripMenuItem1
            // 
            this.二值化ToolStripMenuItem1.Name = "二值化ToolStripMenuItem1";
            this.二值化ToolStripMenuItem1.Size = new System.Drawing.Size(66, 24);
            this.二值化ToolStripMenuItem1.Text = "二值化";
            this.二值化ToolStripMenuItem1.Click += new System.EventHandler(this.二值化ToolStripMenuItem1_Click);
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(565, 40);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent.Size = new System.Drawing.Size(483, 607);
            this.txtContent.TabIndex = 2;
            // 
            // OcrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 659);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "OcrForm";
            this.Text = "OcrForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 打开影像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 二值化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 二值化ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 识别ToolStripMenuItem;
        private System.Windows.Forms.TextBox txtContent;
    }
}