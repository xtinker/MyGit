﻿namespace View.Cmd_MarkDown
{
    partial class MarkDownForm
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
            this.webBrowser1 = new WebKit.WebKitBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1183, 712);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Tag = "Cmd MarkDown";
            this.webBrowser1.Url = new System.Uri("https://www.zybuluo.com/mdeditor", System.UriKind.Absolute);
            // 
            // MarkDownForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 712);
            this.Controls.Add(this.webBrowser1);
            this.Name = "MarkDownForm";
            this.Text = "MarkDownForm";
            this.ResumeLayout(false);

        }

        #endregion

        private WebKit.WebKitBrowser webBrowser1;
    }
}