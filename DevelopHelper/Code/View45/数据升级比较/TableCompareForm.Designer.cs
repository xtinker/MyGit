namespace View45
{
    partial class TableCompareForm
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
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtSourceName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gpbSource = new System.Windows.Forms.GroupBox();
            this.cbSourceDbName = new System.Windows.Forms.ComboBox();
            this.gpbTarget = new System.Windows.Forms.GroupBox();
            this.cbTargetDbName = new System.Windows.Forms.ComboBox();
            this.btnCompare = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.RowNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.table_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.table_name_c = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbtnAll = new System.Windows.Forms.RadioButton();
            this.rbtnCreated = new System.Windows.Forms.RadioButton();
            this.rbtnUpdated = new System.Windows.Forms.RadioButton();
            this.rbtnDeleted = new System.Windows.Forms.RadioButton();
            this.btnExport = new System.Windows.Forms.Button();
            this.gpbSource.SuspendLayout();
            this.gpbTarget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(978, 7);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(107, 35);
            this.btnLogin.TabIndex = 21;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(704, 15);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(270, 21);
            this.txtPassword.TabIndex = 20;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(384, 15);
            this.txtUser.Margin = new System.Windows.Forms.Padding(2);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(270, 21);
            this.txtUser.TabIndex = 19;
            // 
            // txtSourceName
            // 
            this.txtSourceName.Location = new System.Drawing.Point(56, 15);
            this.txtSourceName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSourceName.Name = "txtSourceName";
            this.txtSourceName.Size = new System.Drawing.Size(270, 21);
            this.txtSourceName.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(671, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(339, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "登录名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "服务器";
            // 
            // gpbSource
            // 
            this.gpbSource.Controls.Add(this.cbSourceDbName);
            this.gpbSource.Location = new System.Drawing.Point(13, 56);
            this.gpbSource.Name = "gpbSource";
            this.gpbSource.Size = new System.Drawing.Size(617, 70);
            this.gpbSource.TabIndex = 22;
            this.gpbSource.TabStop = false;
            this.gpbSource.Text = "源数据库";
            // 
            // cbSourceDbName
            // 
            this.cbSourceDbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceDbName.FormattingEnabled = true;
            this.cbSourceDbName.Location = new System.Drawing.Point(7, 21);
            this.cbSourceDbName.Name = "cbSourceDbName";
            this.cbSourceDbName.Size = new System.Drawing.Size(604, 20);
            this.cbSourceDbName.TabIndex = 0;
            // 
            // gpbTarget
            // 
            this.gpbTarget.Controls.Add(this.cbTargetDbName);
            this.gpbTarget.Location = new System.Drawing.Point(655, 56);
            this.gpbTarget.Name = "gpbTarget";
            this.gpbTarget.Size = new System.Drawing.Size(617, 70);
            this.gpbTarget.TabIndex = 23;
            this.gpbTarget.TabStop = false;
            this.gpbTarget.Text = "目标数据库";
            // 
            // cbTargetDbName
            // 
            this.cbTargetDbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTargetDbName.FormattingEnabled = true;
            this.cbTargetDbName.Location = new System.Drawing.Point(6, 21);
            this.cbTargetDbName.Name = "cbTargetDbName";
            this.cbTargetDbName.Size = new System.Drawing.Size(604, 20);
            this.cbTargetDbName.TabIndex = 1;
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(1090, 7);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(107, 35);
            this.btnCompare.TabIndex = 24;
            this.btnCompare.Text = "比对";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RowNum,
            this.table_name,
            this.table_name_c,
            this.TableStatus});
            this.dataGridView.Location = new System.Drawing.Point(13, 179);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(1259, 490);
            this.dataGridView.TabIndex = 25;
            // 
            // RowNum
            // 
            this.RowNum.DataPropertyName = "RowNum";
            this.RowNum.HeaderText = "序号";
            this.RowNum.Name = "RowNum";
            this.RowNum.ReadOnly = true;
            this.RowNum.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // table_name
            // 
            this.table_name.DataPropertyName = "table_name";
            this.table_name.HeaderText = "表名";
            this.table_name.Name = "table_name";
            this.table_name.ReadOnly = true;
            // 
            // table_name_c
            // 
            this.table_name_c.DataPropertyName = "table_name_c";
            this.table_name_c.HeaderText = "中文名称";
            this.table_name_c.Name = "table_name_c";
            this.table_name_c.ReadOnly = true;
            // 
            // TableStatus
            // 
            this.TableStatus.DataPropertyName = "TableStatus";
            this.TableStatus.HeaderText = "升级类型";
            this.TableStatus.Name = "TableStatus";
            this.TableStatus.ReadOnly = true;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(1203, 18);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 12);
            this.lblMessage.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 147);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 27;
            this.label4.Text = "查询类型";
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.Checked = true;
            this.rbtnAll.Location = new System.Drawing.Point(83, 145);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(47, 16);
            this.rbtnAll.TabIndex = 28;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "全部";
            this.rbtnAll.UseVisualStyleBackColor = true;
            this.rbtnAll.CheckedChanged += new System.EventHandler(this.rbtnAll_CheckedChanged);
            // 
            // rbtnCreated
            // 
            this.rbtnCreated.AutoSize = true;
            this.rbtnCreated.Location = new System.Drawing.Point(147, 145);
            this.rbtnCreated.Name = "rbtnCreated";
            this.rbtnCreated.Size = new System.Drawing.Size(47, 16);
            this.rbtnCreated.TabIndex = 29;
            this.rbtnCreated.Text = "新增";
            this.rbtnCreated.UseVisualStyleBackColor = true;
            this.rbtnCreated.CheckedChanged += new System.EventHandler(this.rbtnCreated_CheckedChanged);
            // 
            // rbtnUpdated
            // 
            this.rbtnUpdated.AutoSize = true;
            this.rbtnUpdated.Location = new System.Drawing.Point(214, 145);
            this.rbtnUpdated.Name = "rbtnUpdated";
            this.rbtnUpdated.Size = new System.Drawing.Size(47, 16);
            this.rbtnUpdated.TabIndex = 30;
            this.rbtnUpdated.Text = "修改";
            this.rbtnUpdated.UseVisualStyleBackColor = true;
            this.rbtnUpdated.CheckedChanged += new System.EventHandler(this.rbtnUpdated_CheckedChanged);
            // 
            // rbtnDeleted
            // 
            this.rbtnDeleted.AutoSize = true;
            this.rbtnDeleted.Location = new System.Drawing.Point(279, 145);
            this.rbtnDeleted.Name = "rbtnDeleted";
            this.rbtnDeleted.Size = new System.Drawing.Size(47, 16);
            this.rbtnDeleted.TabIndex = 31;
            this.rbtnDeleted.Text = "删除";
            this.rbtnDeleted.UseVisualStyleBackColor = true;
            this.rbtnDeleted.CheckedChanged += new System.EventHandler(this.rbtnDeleted_CheckedChanged);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(1165, 136);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(107, 35);
            this.btnExport.TabIndex = 32;
            this.btnExport.Text = "导出Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // TableCompareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 680);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.rbtnDeleted);
            this.Controls.Add(this.rbtnUpdated);
            this.Controls.Add(this.rbtnCreated);
            this.Controls.Add(this.rbtnAll);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.txtSourceName);
            this.Controls.Add(this.gpbTarget);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.gpbSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Name = "TableCompareForm";
            this.Text = "TableCompareForm";
            this.gpbSource.ResumeLayout(false);
            this.gpbTarget.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtSourceName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gpbSource;
        private System.Windows.Forms.GroupBox gpbTarget;
        private System.Windows.Forms.ComboBox cbSourceDbName;
        private System.Windows.Forms.ComboBox cbTargetDbName;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbtnAll;
        private System.Windows.Forms.RadioButton rbtnCreated;
        private System.Windows.Forms.RadioButton rbtnUpdated;
        private System.Windows.Forms.RadioButton rbtnDeleted;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn table_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn table_name_c;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableStatus;
    }
}