namespace View
{
    partial class CreateScriptForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbErpSite = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnCreateScript = new System.Windows.Forms.Button();
            this.txtScript = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ERP站点：";
            // 
            // cbErpSite
            // 
            this.cbErpSite.FormattingEnabled = true;
            this.cbErpSite.Location = new System.Drawing.Point(77, 6);
            this.cbErpSite.Name = "cbErpSite";
            this.cbErpSite.Size = new System.Drawing.Size(268, 20);
            this.cbErpSite.TabIndex = 1;
            this.cbErpSite.SelectedIndexChanged += new System.EventHandler(this.cbErpSite_SelectedIndexChanged);
            this.cbErpSite.TextChanged += new System.EventHandler(this.cbErpSite_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(363, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "服务类名(命名空间全名称):";
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(524, 5);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(748, 21);
            this.txtServiceName.TabIndex = 3;
            this.txtServiceName.TextChanged += new System.EventHandler(this.txtServiceName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "URL:";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(77, 36);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(1094, 21);
            this.txtUrl.TabIndex = 5;
            // 
            // btnCreateScript
            // 
            this.btnCreateScript.Location = new System.Drawing.Point(1181, 34);
            this.btnCreateScript.Name = "btnCreateScript";
            this.btnCreateScript.Size = new System.Drawing.Size(91, 23);
            this.btnCreateScript.TabIndex = 6;
            this.btnCreateScript.Text = "生成脚本";
            this.btnCreateScript.UseVisualStyleBackColor = true;
            this.btnCreateScript.Click += new System.EventHandler(this.btnCreateScript_Click);
            // 
            // txtScript
            // 
            this.txtScript.Location = new System.Drawing.Point(12, 96);
            this.txtScript.Multiline = true;
            this.txtScript.Name = "txtScript";
            this.txtScript.ReadOnly = true;
            this.txtScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtScript.Size = new System.Drawing.Size(1260, 567);
            this.txtScript.TabIndex = 7;
            this.txtScript.WordWrap = false;
            this.txtScript.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScript_KeyDown);
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.ForeColor = System.Drawing.Color.Red;
            this.lblRemark.Location = new System.Drawing.Point(12, 69);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(467, 12);
            this.lblRemark.TabIndex = 8;
            this.lblRemark.Text = "请将“/script”加入白名单（站点目录下App_Data中的AuthorizedWhitelist.config）";
            // 
            // CreateScriptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 680);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.txtScript);
            this.Controls.Add(this.btnCreateScript);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtServiceName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbErpSite);
            this.Controls.Add(this.label1);
            this.Name = "CreateScriptForm";
            this.Text = "CreateScriptForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbErpSite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnCreateScript;
        private System.Windows.Forms.TextBox txtScript;
        private System.Windows.Forms.Label lblRemark;
    }
}