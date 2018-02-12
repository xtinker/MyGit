namespace View45
{
    partial class EnumExportForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.txtFileKeyWord = new System.Windows.Forms.TextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.txtSitePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTips = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblPercent = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.rbtAll = new System.Windows.Forms.RadioButton();
            this.rbtSlxt = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTip = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(1137, 5);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(135, 52);
            this.btnAnalyze.TabIndex = 17;
            this.btnAnalyze.Text = "解析枚举";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // txtFileKeyWord
            // 
            this.txtFileKeyWord.Location = new System.Drawing.Point(200, 35);
            this.txtFileKeyWord.Name = "txtFileKeyWord";
            this.txtFileKeyWord.Size = new System.Drawing.Size(452, 21);
            this.txtFileKeyWord.TabIndex = 16;
            this.txtFileKeyWord.Visible = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(147, 37);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 15;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "其他";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "文件过滤：";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(83, 38);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 13;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "所有";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // txtSitePath
            // 
            this.txtSitePath.Location = new System.Drawing.Point(83, 6);
            this.txtSitePath.Name = "txtSitePath";
            this.txtSitePath.Size = new System.Drawing.Size(976, 21);
            this.txtSitePath.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "站点目录：";
            // 
            // lblTips
            // 
            this.lblTips.AutoSize = true;
            this.lblTips.Location = new System.Drawing.Point(2, 15);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(0, 12);
            this.lblTips.TabIndex = 7;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(0, 34);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1123, 23);
            this.progressBar.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.lblPercent);
            this.panel1.Controls.Add(this.txtContent);
            this.panel1.Controls.Add(this.lblTips);
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Location = new System.Drawing.Point(11, 125);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1261, 543);
            this.panel1.TabIndex = 18;
            this.panel1.Visible = false;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(1126, 7);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(135, 51);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "导出Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(1089, 16);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(0, 12);
            this.lblPercent.TabIndex = 10;
            this.lblPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtContent
            // 
            this.txtContent.HideSelection = false;
            this.txtContent.Location = new System.Drawing.Point(0, 63);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent.Size = new System.Drawing.Size(1258, 477);
            this.txtContent.TabIndex = 9;
            this.txtContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContent_KeyDown);
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(1059, 5);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 23);
            this.btnScan.TabIndex = 20;
            this.btnScan.Text = "浏览";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // rbtAll
            // 
            this.rbtAll.AutoSize = true;
            this.rbtAll.Location = new System.Drawing.Point(18, 25);
            this.rbtAll.Name = "rbtAll";
            this.rbtAll.Size = new System.Drawing.Size(47, 16);
            this.rbtAll.TabIndex = 22;
            this.rbtAll.Text = "所有";
            this.rbtAll.UseVisualStyleBackColor = true;
            this.rbtAll.CheckedChanged += new System.EventHandler(this.rbtAll_CheckedChanged);
            // 
            // rbtSlxt
            // 
            this.rbtSlxt.AutoSize = true;
            this.rbtSlxt.Checked = true;
            this.rbtSlxt.Location = new System.Drawing.Point(83, 25);
            this.rbtSlxt.Name = "rbtSlxt";
            this.rbtSlxt.Size = new System.Drawing.Size(47, 16);
            this.rbtSlxt.TabIndex = 23;
            this.rbtSlxt.TabStop = true;
            this.rbtSlxt.Text = "售楼";
            this.rbtSlxt.UseVisualStyleBackColor = true;
            this.rbtSlxt.CheckedChanged += new System.EventHandler(this.rbtSlxt_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtAll);
            this.groupBox1.Controls.Add(this.rbtSlxt);
            this.groupBox1.Location = new System.Drawing.Point(11, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1261, 57);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "系统范围";
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(658, 40);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(473, 12);
            this.lblTip.TabIndex = 25;
            this.lblTip.Text = "示例：实体文件“Mysoft.Slxt.FinanceMng.dll”中针对“FinanceMng”部分的模糊查询";
            this.lblTip.Visible = false;
            // 
            // EnumExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 680);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.txtFileKeyWord);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.txtSitePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "EnumExportForm";
            this.Text = "导出枚举工具";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.TextBox txtFileKeyWord;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox txtSitePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTips;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.RadioButton rbtAll;
        private System.Windows.Forms.RadioButton rbtSlxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Button btnExport;
    }
}

