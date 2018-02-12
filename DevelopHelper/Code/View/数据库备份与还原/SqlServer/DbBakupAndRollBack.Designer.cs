namespace View.SqlServer
{
    partial class DbBakupAndRollBack
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtSourceName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.treeView = new System.Windows.Forms.TreeView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblPercent = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.cbIsHasDate = new System.Windows.Forms.CheckBox();
            this.btnCreateAndRollback = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnRollBack = new System.Windows.Forms.Button();
            this.btnBakup = new System.Windows.Forms.Button();
            this.btnScan2 = new System.Windows.Forms.Button();
            this.btnScan1 = new System.Windows.Forms.Button();
            this.txtRollBackPath = new System.Windows.Forms.TextBox();
            this.txtBakupPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblSelectedBase = new System.Windows.Forms.Label();
            this.lblSelectedFileStore = new System.Windows.Forms.Label();
            this.btnSetFileStore = new System.Windows.Forms.Button();
            this.btnSetUploadPath = new System.Windows.Forms.Button();
            this.txtFileServices = new System.Windows.Forms.TextBox();
            this.txtUploadPath = new System.Windows.Forms.TextBox();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.txtPasswordText = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.txtSourceName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(858, 164);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登录";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(385, 84);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(107, 35);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(385, 25);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(107, 35);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(76, 98);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(270, 21);
            this.txtPassword.TabIndex = 5;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(76, 62);
            this.txtUser.Margin = new System.Windows.Forms.Padding(2);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(270, 21);
            this.txtUser.TabIndex = 4;
            // 
            // txtSourceName
            // 
            this.txtSourceName.Location = new System.Drawing.Point(76, 25);
            this.txtSourceName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSourceName.Name = "txtSourceName";
            this.txtSourceName.Size = new System.Drawing.Size(270, 21);
            this.txtSourceName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 101);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "登录名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.treeView);
            this.groupBox2.Location = new System.Drawing.Point(872, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(401, 659);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据库列表";
            // 
            // treeView
            // 
            this.treeView.CheckBoxes = true;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(2, 16);
            this.treeView.Margin = new System.Windows.Forms.Padding(2);
            this.treeView.Name = "treeView";
            this.treeView.ShowLines = false;
            this.treeView.ShowRootLines = false;
            this.treeView.Size = new System.Drawing.Size(397, 641);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblPercent);
            this.groupBox3.Controls.Add(this.progressBar);
            this.groupBox3.Controls.Add(this.cbIsHasDate);
            this.groupBox3.Controls.Add(this.btnCreateAndRollback);
            this.groupBox3.Controls.Add(this.lblMessage);
            this.groupBox3.Controls.Add(this.btnRollBack);
            this.groupBox3.Controls.Add(this.btnBakup);
            this.groupBox3.Controls.Add(this.btnScan2);
            this.groupBox3.Controls.Add(this.btnScan1);
            this.groupBox3.Controls.Add(this.txtRollBackPath);
            this.groupBox3.Controls.Add(this.txtBakupPath);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(11, 178);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(857, 269);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "操作";
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(788, 224);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(35, 12);
            this.lblPercent.TabIndex = 12;
            this.lblPercent.Text = "0/100";
            this.lblPercent.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(14, 218);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(764, 23);
            this.progressBar.TabIndex = 11;
            this.progressBar.Visible = false;
            // 
            // cbIsHasDate
            // 
            this.cbIsHasDate.AutoSize = true;
            this.cbIsHasDate.Location = new System.Drawing.Point(100, 122);
            this.cbIsHasDate.Margin = new System.Windows.Forms.Padding(2);
            this.cbIsHasDate.Name = "cbIsHasDate";
            this.cbIsHasDate.Size = new System.Drawing.Size(84, 16);
            this.cbIsHasDate.TabIndex = 10;
            this.cbIsHasDate.Text = "库名带日期";
            this.cbIsHasDate.UseVisualStyleBackColor = true;
            // 
            // btnCreateAndRollback
            // 
            this.btnCreateAndRollback.Location = new System.Drawing.Point(674, 112);
            this.btnCreateAndRollback.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreateAndRollback.Name = "btnCreateAndRollback";
            this.btnCreateAndRollback.Size = new System.Drawing.Size(107, 35);
            this.btnCreateAndRollback.TabIndex = 9;
            this.btnCreateAndRollback.Text = "创建并还原";
            this.btnCreateAndRollback.UseVisualStyleBackColor = true;
            this.btnCreateAndRollback.Click += new System.EventHandler(this.btnCreateAndRollback_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(15, 194);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 12);
            this.lblMessage.TabIndex = 8;
            // 
            // btnRollBack
            // 
            this.btnRollBack.Location = new System.Drawing.Point(527, 112);
            this.btnRollBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnRollBack.Name = "btnRollBack";
            this.btnRollBack.Size = new System.Drawing.Size(107, 35);
            this.btnRollBack.TabIndex = 7;
            this.btnRollBack.Text = "还原";
            this.btnRollBack.UseVisualStyleBackColor = true;
            this.btnRollBack.Click += new System.EventHandler(this.btnRollBack_Click);
            // 
            // btnBakup
            // 
            this.btnBakup.Location = new System.Drawing.Point(380, 112);
            this.btnBakup.Margin = new System.Windows.Forms.Padding(2);
            this.btnBakup.Name = "btnBakup";
            this.btnBakup.Size = new System.Drawing.Size(107, 35);
            this.btnBakup.TabIndex = 6;
            this.btnBakup.Text = "备份";
            this.btnBakup.UseVisualStyleBackColor = true;
            this.btnBakup.Click += new System.EventHandler(this.btnBakup_Click);
            // 
            // btnScan2
            // 
            this.btnScan2.Location = new System.Drawing.Point(785, 70);
            this.btnScan2.Margin = new System.Windows.Forms.Padding(2);
            this.btnScan2.Name = "btnScan2";
            this.btnScan2.Size = new System.Drawing.Size(68, 27);
            this.btnScan2.TabIndex = 5;
            this.btnScan2.Text = "浏览";
            this.btnScan2.UseVisualStyleBackColor = true;
            this.btnScan2.Click += new System.EventHandler(this.btnScan2_Click);
            // 
            // btnScan1
            // 
            this.btnScan1.Location = new System.Drawing.Point(785, 27);
            this.btnScan1.Margin = new System.Windows.Forms.Padding(2);
            this.btnScan1.Name = "btnScan1";
            this.btnScan1.Size = new System.Drawing.Size(68, 27);
            this.btnScan1.TabIndex = 4;
            this.btnScan1.Text = "浏览";
            this.btnScan1.UseVisualStyleBackColor = true;
            this.btnScan1.Click += new System.EventHandler(this.btnScan1_Click);
            // 
            // txtRollBackPath
            // 
            this.txtRollBackPath.Location = new System.Drawing.Point(100, 74);
            this.txtRollBackPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtRollBackPath.Name = "txtRollBackPath";
            this.txtRollBackPath.Size = new System.Drawing.Size(681, 21);
            this.txtRollBackPath.TabIndex = 3;
            // 
            // txtBakupPath
            // 
            this.txtBakupPath.Location = new System.Drawing.Point(100, 31);
            this.txtBakupPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtBakupPath.Name = "txtBakupPath";
            this.txtBakupPath.Size = new System.Drawing.Size(681, 21);
            this.txtBakupPath.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 77);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "数据库文件路径";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "备份文件路径";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblSelectedBase);
            this.groupBox4.Controls.Add(this.lblSelectedFileStore);
            this.groupBox4.Controls.Add(this.btnSetFileStore);
            this.groupBox4.Controls.Add(this.btnSetUploadPath);
            this.groupBox4.Controls.Add(this.txtFileServices);
            this.groupBox4.Controls.Add(this.txtUploadPath);
            this.groupBox4.Controls.Add(this.btnResetPassword);
            this.groupBox4.Controls.Add(this.txtPasswordText);
            this.groupBox4.Location = new System.Drawing.Point(10, 453);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(858, 214);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "数据初始化";
            // 
            // lblSelectedBase
            // 
            this.lblSelectedBase.AutoSize = true;
            this.lblSelectedBase.Location = new System.Drawing.Point(15, 42);
            this.lblSelectedBase.Name = "lblSelectedBase";
            this.lblSelectedBase.Size = new System.Drawing.Size(0, 12);
            this.lblSelectedBase.TabIndex = 7;
            // 
            // lblSelectedFileStore
            // 
            this.lblSelectedFileStore.AutoSize = true;
            this.lblSelectedFileStore.Location = new System.Drawing.Point(16, 107);
            this.lblSelectedFileStore.Name = "lblSelectedFileStore";
            this.lblSelectedFileStore.Size = new System.Drawing.Size(0, 12);
            this.lblSelectedFileStore.TabIndex = 6;
            // 
            // btnSetFileStore
            // 
            this.btnSetFileStore.Location = new System.Drawing.Point(668, 173);
            this.btnSetFileStore.Name = "btnSetFileStore";
            this.btnSetFileStore.Size = new System.Drawing.Size(134, 28);
            this.btnSetFileStore.TabIndex = 5;
            this.btnSetFileStore.Text = "设置文件服务地址";
            this.btnSetFileStore.UseVisualStyleBackColor = true;
            this.btnSetFileStore.Click += new System.EventHandler(this.btnSetFileStore_Click);
            // 
            // btnSetUploadPath
            // 
            this.btnSetUploadPath.Location = new System.Drawing.Point(400, 134);
            this.btnSetUploadPath.Name = "btnSetUploadPath";
            this.btnSetUploadPath.Size = new System.Drawing.Size(134, 28);
            this.btnSetUploadPath.TabIndex = 4;
            this.btnSetUploadPath.Text = "设置存储路径";
            this.btnSetUploadPath.UseVisualStyleBackColor = true;
            this.btnSetUploadPath.Click += new System.EventHandler(this.btnSetUploadPath_Click);
            // 
            // txtFileServices
            // 
            this.txtFileServices.Location = new System.Drawing.Point(15, 178);
            this.txtFileServices.Name = "txtFileServices";
            this.txtFileServices.Size = new System.Drawing.Size(647, 21);
            this.txtFileServices.TabIndex = 3;
            // 
            // txtUploadPath
            // 
            this.txtUploadPath.Location = new System.Drawing.Point(15, 139);
            this.txtUploadPath.Name = "txtUploadPath";
            this.txtUploadPath.Size = new System.Drawing.Size(379, 21);
            this.txtUploadPath.TabIndex = 2;
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.Location = new System.Drawing.Point(164, 55);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(134, 28);
            this.btnResetPassword.TabIndex = 1;
            this.btnResetPassword.Text = "初始化密码";
            this.btnResetPassword.UseVisualStyleBackColor = true;
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // txtPasswordText
            // 
            this.txtPasswordText.Location = new System.Drawing.Point(15, 60);
            this.txtPasswordText.Name = "txtPasswordText";
            this.txtPasswordText.Size = new System.Drawing.Size(143, 21);
            this.txtPasswordText.TabIndex = 0;
            // 
            // DbBakupAndRollBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 680);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DbBakupAndRollBack";
            this.Text = "DbBakupAndRollBack";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtSourceName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRollBack;
        private System.Windows.Forms.Button btnBakup;
        private System.Windows.Forms.Button btnScan2;
        private System.Windows.Forms.Button btnScan1;
        private System.Windows.Forms.TextBox txtRollBackPath;
        private System.Windows.Forms.TextBox txtBakupPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button btnCreateAndRollback;
        private System.Windows.Forms.CheckBox cbIsHasDate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.TextBox txtPasswordText;
        private System.Windows.Forms.Button btnSetFileStore;
        private System.Windows.Forms.Button btnSetUploadPath;
        private System.Windows.Forms.TextBox txtFileServices;
        private System.Windows.Forms.TextBox txtUploadPath;
        private System.Windows.Forms.Label lblSelectedFileStore;
        private System.Windows.Forms.Label lblSelectedBase;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblPercent;
    }
}