namespace View.CreateQrImage
{
    partial class CodeForm
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
            this.txtContent = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblCharCount = new System.Windows.Forms.Label();
            this.picQrImage = new System.Windows.Forms.PictureBox();
            this.btnCreate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picQrImage)).BeginInit();
            this.SuspendLayout();
            // 
            // txtContent
            // 
            this.txtContent.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtContent.Location = new System.Drawing.Point(12, 47);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(425, 394);
            this.txtContent.TabIndex = 0;
            this.txtContent.TextChanged += new System.EventHandler(this.txtContent_TextChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(13, 29);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(257, 12);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "推荐150字以内，支持普通文本/网址/EMAIL地址";
            // 
            // lblCharCount
            // 
            this.lblCharCount.AutoSize = true;
            this.lblCharCount.Location = new System.Drawing.Point(15, 448);
            this.lblCharCount.Name = "lblCharCount";
            this.lblCharCount.Size = new System.Drawing.Size(35, 12);
            this.lblCharCount.TabIndex = 2;
            this.lblCharCount.Text = "0/300";
            // 
            // picQrImage
            // 
            this.picQrImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picQrImage.Location = new System.Drawing.Point(444, 47);
            this.picQrImage.Name = "picQrImage";
            this.picQrImage.Size = new System.Drawing.Size(394, 394);
            this.picQrImage.TabIndex = 3;
            this.picQrImage.TabStop = false;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(373, 475);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(128, 38);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "生成二维码";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // CodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 551);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.picQrImage);
            this.Controls.Add(this.lblCharCount);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtContent);
            this.Name = "CodeForm";
            this.Text = "二维码生成器";
            ((System.ComponentModel.ISupportInitialize)(this.picQrImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblCharCount;
        private System.Windows.Forms.PictureBox picQrImage;
        private System.Windows.Forms.Button btnCreate;
    }
}