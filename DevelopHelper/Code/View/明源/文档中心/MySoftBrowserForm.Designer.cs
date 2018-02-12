namespace View
{
    partial class MySoftBrowserForm
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
            this.webBrowser1.AutoScroll = true;
            this.webBrowser1.BackColor = System.Drawing.Color.White;
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(960, 600);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1183, 712);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Tag = "建模文档中心";
            this.webBrowser1.Url = new System.Uri("http://mpdoc.mingyuanyun.com/#!index.md", System.UriKind.Absolute);
            // 
            // MySoftBrowserForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1183, 712);
            this.Controls.Add(this.webBrowser1);
            this.Name = "MySoftBrowserForm";
            this.Text = "MySoftBrowserForm";
            this.ResumeLayout(false);

        }

        #endregion

        private WebKit.WebKitBrowser webBrowser1;
    }
}