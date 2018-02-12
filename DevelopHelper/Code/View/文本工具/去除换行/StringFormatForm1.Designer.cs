namespace View
{
    partial class StringFormatForm1
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
            this.txtStringOld = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStringNew = new System.Windows.Forms.TextBox();
            this.btnFormat = new System.Windows.Forms.Button();
            this.btnToJson = new System.Windows.Forms.Button();
            this.btnToXml = new System.Windows.Forms.Button();
            this.btnToUpper = new System.Windows.Forms.Button();
            this.btnToLower = new System.Windows.Forms.Button();
            this.btnJsonFormat = new System.Windows.Forms.Button();
            this.btnXmlFormat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "需要格式化字符串：";
            // 
            // txtStringOld
            // 
            this.txtStringOld.Location = new System.Drawing.Point(15, 29);
            this.txtStringOld.Multiline = true;
            this.txtStringOld.Name = "txtStringOld";
            this.txtStringOld.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStringOld.Size = new System.Drawing.Size(1257, 196);
            this.txtStringOld.TabIndex = 1;
            this.txtStringOld.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStringOld_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "格式化结果：";
            // 
            // txtStringNew
            // 
            this.txtStringNew.Location = new System.Drawing.Point(12, 257);
            this.txtStringNew.Multiline = true;
            this.txtStringNew.Name = "txtStringNew";
            this.txtStringNew.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStringNew.Size = new System.Drawing.Size(1260, 411);
            this.txtStringNew.TabIndex = 3;
            this.txtStringNew.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStringNew_KeyDown);
            // 
            // btnFormat
            // 
            this.btnFormat.Location = new System.Drawing.Point(132, 2);
            this.btnFormat.Name = "btnFormat";
            this.btnFormat.Size = new System.Drawing.Size(75, 23);
            this.btnFormat.TabIndex = 4;
            this.btnFormat.Text = "去掉换行";
            this.btnFormat.UseVisualStyleBackColor = true;
            this.btnFormat.Click += new System.EventHandler(this.btnFormat_Click);
            // 
            // btnToJson
            // 
            this.btnToJson.Location = new System.Drawing.Point(214, 2);
            this.btnToJson.Name = "btnToJson";
            this.btnToJson.Size = new System.Drawing.Size(75, 23);
            this.btnToJson.TabIndex = 5;
            this.btnToJson.Text = "XmlToJson";
            this.btnToJson.UseVisualStyleBackColor = true;
            this.btnToJson.Click += new System.EventHandler(this.btnToJson_Click);
            // 
            // btnToXml
            // 
            this.btnToXml.Location = new System.Drawing.Point(376, 2);
            this.btnToXml.Name = "btnToXml";
            this.btnToXml.Size = new System.Drawing.Size(75, 23);
            this.btnToXml.TabIndex = 6;
            this.btnToXml.Text = "JsonToXML";
            this.btnToXml.UseVisualStyleBackColor = true;
            this.btnToXml.Click += new System.EventHandler(this.btnToXml_Click);
            // 
            // btnToUpper
            // 
            this.btnToUpper.Location = new System.Drawing.Point(538, 2);
            this.btnToUpper.Name = "btnToUpper";
            this.btnToUpper.Size = new System.Drawing.Size(75, 23);
            this.btnToUpper.TabIndex = 7;
            this.btnToUpper.Text = "转大写";
            this.btnToUpper.UseVisualStyleBackColor = true;
            this.btnToUpper.Click += new System.EventHandler(this.btnToUpper_Click);
            // 
            // btnToLower
            // 
            this.btnToLower.Location = new System.Drawing.Point(619, 2);
            this.btnToLower.Name = "btnToLower";
            this.btnToLower.Size = new System.Drawing.Size(75, 23);
            this.btnToLower.TabIndex = 8;
            this.btnToLower.Text = "转小写";
            this.btnToLower.UseVisualStyleBackColor = true;
            this.btnToLower.Click += new System.EventHandler(this.btnToLower_Click);
            // 
            // btnJsonFormat
            // 
            this.btnJsonFormat.Location = new System.Drawing.Point(295, 2);
            this.btnJsonFormat.Name = "btnJsonFormat";
            this.btnJsonFormat.Size = new System.Drawing.Size(75, 23);
            this.btnJsonFormat.TabIndex = 9;
            this.btnJsonFormat.Text = "Json格式化";
            this.btnJsonFormat.UseVisualStyleBackColor = true;
            this.btnJsonFormat.Click += new System.EventHandler(this.btnJsonFormat_Click);
            // 
            // btnXmlFormat
            // 
            this.btnXmlFormat.Location = new System.Drawing.Point(457, 2);
            this.btnXmlFormat.Name = "btnXmlFormat";
            this.btnXmlFormat.Size = new System.Drawing.Size(75, 23);
            this.btnXmlFormat.TabIndex = 10;
            this.btnXmlFormat.Text = "XML格式化";
            this.btnXmlFormat.UseVisualStyleBackColor = true;
            this.btnXmlFormat.Click += new System.EventHandler(this.btnXmlFormat_Click);
            // 
            // StringFormatForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 680);
            this.Controls.Add(this.btnXmlFormat);
            this.Controls.Add(this.btnJsonFormat);
            this.Controls.Add(this.btnToLower);
            this.Controls.Add(this.btnToUpper);
            this.Controls.Add(this.btnToXml);
            this.Controls.Add(this.btnToJson);
            this.Controls.Add(this.btnFormat);
            this.Controls.Add(this.txtStringNew);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStringOld);
            this.Controls.Add(this.label1);
            this.Name = "StringFormatForm1";
            this.Text = "StringFormatForm1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStringOld;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStringNew;
        private System.Windows.Forms.Button btnFormat;
        private System.Windows.Forms.Button btnToJson;
        private System.Windows.Forms.Button btnToXml;
        private System.Windows.Forms.Button btnToUpper;
        private System.Windows.Forms.Button btnToLower;
        private System.Windows.Forms.Button btnJsonFormat;
        private System.Windows.Forms.Button btnXmlFormat;
    }
}