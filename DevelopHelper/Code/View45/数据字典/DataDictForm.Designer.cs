namespace View45
{
    partial class DataDictForm
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtSourceName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTableName = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDbName = new System.Windows.Forms.ComboBox();
            this.cbMatchedPattern = new System.Windows.Forms.CheckBox();
            this.rownum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.table_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.table_name_c = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.field_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.field_name_c = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(1019, 48);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(107, 35);
            this.btnSelect.TabIndex = 15;
            this.btnSelect.Text = "查询";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(1019, 7);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(107, 35);
            this.btnLogin.TabIndex = 14;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(745, 16);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(270, 21);
            this.txtPassword.TabIndex = 13;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(409, 16);
            this.txtUser.Margin = new System.Windows.Forms.Padding(2);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(270, 21);
            this.txtUser.TabIndex = 12;
            // 
            // txtSourceName
            // 
            this.txtSourceName.Location = new System.Drawing.Point(73, 16);
            this.txtSourceName.Margin = new System.Windows.Forms.Padding(2);
            this.txtSourceName.Name = "txtSourceName";
            this.txtSourceName.Size = new System.Drawing.Size(270, 21);
            this.txtSourceName.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(694, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(347, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "登录名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "服务器";
            // 
            // lblTableName
            // 
            this.lblTableName.AutoSize = true;
            this.lblTableName.Location = new System.Drawing.Point(694, 59);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(29, 12);
            this.lblTableName.TabIndex = 16;
            this.lblTableName.Text = "表名";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(745, 56);
            this.txtTableName.Margin = new System.Windows.Forms.Padding(2);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(270, 21);
            this.txtTableName.TabIndex = 18;
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
            this.rownum,
            this.TableGUID,
            this.table_name,
            this.table_name_c,
            this.field_name,
            this.field_name_c,
            this.data_type,
            this.width,
            this.Description});
            this.dataGridView.Location = new System.Drawing.Point(13, 95);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(1259, 573);
            this.dataGridView.TabIndex = 20;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(1139, 25);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 12);
            this.lblMessage.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 59);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "数据库";
            // 
            // cbDbName
            // 
            this.cbDbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDbName.FormattingEnabled = true;
            this.cbDbName.Location = new System.Drawing.Point(73, 56);
            this.cbDbName.Name = "cbDbName";
            this.cbDbName.Size = new System.Drawing.Size(606, 20);
            this.cbDbName.TabIndex = 23;
            // 
            // cbMatchedPattern
            // 
            this.cbMatchedPattern.AutoSize = true;
            this.cbMatchedPattern.Location = new System.Drawing.Point(1141, 58);
            this.cbMatchedPattern.Name = "cbMatchedPattern";
            this.cbMatchedPattern.Size = new System.Drawing.Size(72, 16);
            this.cbMatchedPattern.TabIndex = 24;
            this.cbMatchedPattern.Text = "全字匹配";
            this.cbMatchedPattern.UseVisualStyleBackColor = true;
            this.cbMatchedPattern.CheckedChanged += new System.EventHandler(this.cbMatchedPattern_CheckedChanged);
            // 
            // rownum
            // 
            this.rownum.DataPropertyName = "rownum";
            this.rownum.HeaderText = "rownum";
            this.rownum.Name = "rownum";
            this.rownum.ReadOnly = true;
            // 
            // TableGUID
            // 
            this.TableGUID.DataPropertyName = "TableGUID";
            this.TableGUID.HeaderText = "TableGUID";
            this.TableGUID.Name = "TableGUID";
            this.TableGUID.ReadOnly = true;
            // 
            // table_name
            // 
            this.table_name.DataPropertyName = "table_name";
            this.table_name.HeaderText = "table_name";
            this.table_name.Name = "table_name";
            this.table_name.ReadOnly = true;
            // 
            // table_name_c
            // 
            this.table_name_c.DataPropertyName = "table_name_c";
            this.table_name_c.HeaderText = "table_name_c";
            this.table_name_c.Name = "table_name_c";
            this.table_name_c.ReadOnly = true;
            // 
            // field_name
            // 
            this.field_name.DataPropertyName = "field_name";
            this.field_name.HeaderText = "field_name";
            this.field_name.Name = "field_name";
            this.field_name.ReadOnly = true;
            // 
            // field_name_c
            // 
            this.field_name_c.DataPropertyName = "field_name_c";
            this.field_name_c.HeaderText = "field_name_c";
            this.field_name_c.Name = "field_name_c";
            this.field_name_c.ReadOnly = true;
            // 
            // data_type
            // 
            this.data_type.DataPropertyName = "data_type";
            this.data_type.HeaderText = "data_type";
            this.data_type.Name = "data_type";
            this.data_type.ReadOnly = true;
            // 
            // width
            // 
            this.width.DataPropertyName = "width";
            this.width.HeaderText = "width";
            this.width.Name = "width";
            this.width.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // DataDictForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 680);
            this.Controls.Add(this.cbMatchedPattern);
            this.Controls.Add(this.cbDbName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.lblTableName);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtSourceName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DataDictForm";
            this.Text = "DataDictForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtSourceName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTableName;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbDbName;
        private System.Windows.Forms.CheckBox cbMatchedPattern;
        private System.Windows.Forms.DataGridViewTextBoxColumn rownum;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn table_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn table_name_c;
        private System.Windows.Forms.DataGridViewTextBoxColumn field_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn field_name_c;
        private System.Windows.Forms.DataGridViewTextBoxColumn data_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn width;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}