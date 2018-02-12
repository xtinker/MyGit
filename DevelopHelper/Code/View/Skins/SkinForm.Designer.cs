namespace View.Skins
{
    partial class SkinForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点9");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点10");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点11");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("节点12");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("节点13");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点14");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("节点2", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("节点15");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("节点3", new System.Windows.Forms.TreeNode[] {
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("节点4");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("节点5");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("节点6");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("节点7");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("节点8");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("节点0", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode8,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15});
            this.lbSkinNames = new System.Windows.Forms.ListBox();
            this.txtSkinName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUseSkin = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.gpSkinList = new System.Windows.Forms.GroupBox();
            this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.gpSkinList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbSkinNames
            // 
            this.lbSkinNames.FormattingEnabled = true;
            this.lbSkinNames.ItemHeight = 12;
            this.lbSkinNames.Location = new System.Drawing.Point(6, 20);
            this.lbSkinNames.Name = "lbSkinNames";
            this.lbSkinNames.Size = new System.Drawing.Size(260, 628);
            this.lbSkinNames.TabIndex = 0;
            this.lbSkinNames.SelectedIndexChanged += new System.EventHandler(this.lbSkinNames_SelectedIndexChanged);
            // 
            // txtSkinName
            // 
            this.txtSkinName.Enabled = false;
            this.txtSkinName.Location = new System.Drawing.Point(380, 30);
            this.txtSkinName.Name = "txtSkinName";
            this.txtSkinName.Size = new System.Drawing.Size(627, 21);
            this.txtSkinName.TabIndex = 1;
            this.txtSkinName.TextChanged += new System.EventHandler(this.txtSkinName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(290, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "皮肤文件:";
            // 
            // btnUseSkin
            // 
            this.btnUseSkin.Location = new System.Drawing.Point(1013, 25);
            this.btnUseSkin.Name = "btnUseSkin";
            this.btnUseSkin.Size = new System.Drawing.Size(119, 31);
            this.btnUseSkin.TabIndex = 3;
            this.btnUseSkin.Text = "保存为默认主题";
            this.btnUseSkin.UseVisualStyleBackColor = true;
            this.btnUseSkin.Click += new System.EventHandler(this.btnUseSkin_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(1138, 25);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(119, 31);
            this.btnDefault.TabIndex = 4;
            this.btnDefault.Text = "系统默默";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(23, 29);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(78, 16);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(23, 164);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 9;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(23, 239);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 10;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(417, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(531, 522);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeView1);
            this.tabPage1.Controls.Add(this.hScrollBar1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(523, 496);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(6, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点9";
            treeNode1.Text = "节点9";
            treeNode2.Name = "节点10";
            treeNode2.Text = "节点10";
            treeNode3.Name = "节点11";
            treeNode3.Text = "节点11";
            treeNode4.Name = "节点12";
            treeNode4.Text = "节点12";
            treeNode5.Name = "节点1";
            treeNode5.Text = "节点1";
            treeNode6.Name = "节点13";
            treeNode6.Text = "节点13";
            treeNode7.Name = "节点14";
            treeNode7.Text = "节点14";
            treeNode8.Name = "节点2";
            treeNode8.Text = "节点2";
            treeNode9.Name = "节点15";
            treeNode9.Text = "节点15";
            treeNode10.Name = "节点3";
            treeNode10.Text = "节点3";
            treeNode11.Name = "节点4";
            treeNode11.Text = "节点4";
            treeNode12.Name = "节点5";
            treeNode12.Text = "节点5";
            treeNode13.Name = "节点6";
            treeNode13.Text = "节点6";
            treeNode14.Name = "节点7";
            treeNode14.Text = "节点7";
            treeNode15.Name = "节点8";
            treeNode15.Text = "节点8";
            treeNode16.Checked = true;
            treeNode16.Name = "节点0";
            treeNode16.Text = "节点0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode16});
            this.treeView1.Size = new System.Drawing.Size(511, 465);
            this.treeView1.TabIndex = 0;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(3, 468);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(517, 25);
            this.hScrollBar1.TabIndex = 17;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(523, 496);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(23, 469);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(352, 45);
            this.trackBar1.TabIndex = 12;
            this.trackBar1.Value = 2;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(114, 29);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(95, 16);
            this.radioButton1.TabIndex = 13;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(23, 519);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(352, 23);
            this.progressBar1.TabIndex = 14;
            this.progressBar1.Value = 45;
            // 
            // gpSkinList
            // 
            this.gpSkinList.Controls.Add(this.lbSkinNames);
            this.gpSkinList.Location = new System.Drawing.Point(12, 12);
            this.gpSkinList.Name = "gpSkinList";
            this.gpSkinList.Size = new System.Drawing.Size(272, 653);
            this.gpSkinList.TabIndex = 15;
            this.gpSkinList.TabStop = false;
            this.gpSkinList.Text = "皮肤列表";
            // 
            // domainUpDown1
            // 
            this.domainUpDown1.Location = new System.Drawing.Point(23, 200);
            this.domainUpDown1.Name = "domainUpDown1";
            this.domainUpDown1.Size = new System.Drawing.Size(120, 21);
            this.domainUpDown1.TabIndex = 16;
            this.domainUpDown1.Text = "domainUpDown1";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(213, 64);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(65, 12);
            this.linkLabel1.TabIndex = 18;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(23, 277);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 19;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(23, 64);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(156, 84);
            this.listView1.TabIndex = 20;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.monthCalendar1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.domainUpDown1);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Location = new System.Drawing.Point(293, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(963, 584);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览";
            // 
            // SkinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 667);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gpSkinList);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnUseSkin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSkinName);
            this.Name = "SkinForm";
            this.Text = "SkinForm";
            this.Load += new System.EventHandler(this.SkinForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.gpSkinList.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbSkinNames;
        private System.Windows.Forms.TextBox txtSkinName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUseSkin;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox gpSkinList;
        private System.Windows.Forms.DomainUpDown domainUpDown1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}