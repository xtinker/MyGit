namespace View.EncryptTool
{
    partial class DES3Form
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
            this.txtDecrypt = new System.Windows.Forms.TextBox();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.txtEncrypt = new System.Windows.Forms.TextBox();
            this.cbHex16 = new System.Windows.Forms.CheckBox();
            this.cbBase64 = new System.Windows.Forms.CheckBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDecrypt);
            this.groupBox1.Controls.Add(this.btnDecrypt);
            this.groupBox1.Controls.Add(this.btnEncrypt);
            this.groupBox1.Controls.Add(this.txtEncrypt);
            this.groupBox1.Controls.Add(this.cbHex16);
            this.groupBox1.Controls.Add(this.cbBase64);
            this.groupBox1.Controls.Add(this.txtKey);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1000, 600);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DES3";
            // 
            // txtDecrypt
            // 
            this.txtDecrypt.Location = new System.Drawing.Point(26, 347);
            this.txtDecrypt.Multiline = true;
            this.txtDecrypt.Name = "txtDecrypt";
            this.txtDecrypt.Size = new System.Drawing.Size(951, 196);
            this.txtDecrypt.TabIndex = 15;
            this.txtDecrypt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDecrypt_KeyDown);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(890, 294);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(87, 32);
            this.btnDecrypt.TabIndex = 14;
            this.btnDecrypt.Text = "解密";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(779, 294);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(87, 32);
            this.btnEncrypt.TabIndex = 13;
            this.btnEncrypt.Text = "加密";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // txtEncrypt
            // 
            this.txtEncrypt.Location = new System.Drawing.Point(26, 80);
            this.txtEncrypt.Multiline = true;
            this.txtEncrypt.Name = "txtEncrypt";
            this.txtEncrypt.Size = new System.Drawing.Size(951, 196);
            this.txtEncrypt.TabIndex = 12;
            this.txtEncrypt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEncrypt_KeyDown);
            // 
            // cbHex16
            // 
            this.cbHex16.AutoSize = true;
            this.cbHex16.Location = new System.Drawing.Point(532, 37);
            this.cbHex16.Name = "cbHex16";
            this.cbHex16.Size = new System.Drawing.Size(75, 19);
            this.cbHex16.TabIndex = 11;
            this.cbHex16.Text = "16进制";
            this.cbHex16.UseVisualStyleBackColor = true;
            this.cbHex16.Click += new System.EventHandler(this.cbHex16_Click);
            // 
            // cbBase64
            // 
            this.cbBase64.AutoSize = true;
            this.cbBase64.Location = new System.Drawing.Point(435, 37);
            this.cbBase64.Name = "cbBase64";
            this.cbBase64.Size = new System.Drawing.Size(77, 19);
            this.cbBase64.TabIndex = 10;
            this.cbBase64.Text = "Base64";
            this.cbBase64.UseVisualStyleBackColor = true;
            this.cbBase64.Click += new System.EventHandler(this.cbBase64_Click);
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(115, 35);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(301, 25);
            this.txtKey.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "密钥(8位):";
            // 
            // DES3Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 625);
            this.Controls.Add(this.groupBox1);
            this.Name = "DES3Form";
            this.Text = "DES3Form";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDecrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.TextBox txtEncrypt;
        private System.Windows.Forms.CheckBox cbHex16;
        private System.Windows.Forms.CheckBox cbBase64;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label label1;
    }
}