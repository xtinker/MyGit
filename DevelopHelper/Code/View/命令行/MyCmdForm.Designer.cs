namespace View
{
    partial class MyCmdForm
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
            this.txtOutputInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtOutputInfo
            // 
            this.txtOutputInfo.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtOutputInfo.Enabled = false;
            this.txtOutputInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOutputInfo.ForeColor = System.Drawing.Color.White;
            this.txtOutputInfo.Location = new System.Drawing.Point(12, 12);
            this.txtOutputInfo.Multiline = true;
            this.txtOutputInfo.Name = "txtOutputInfo";
            this.txtOutputInfo.Size = new System.Drawing.Size(1260, 656);
            this.txtOutputInfo.TabIndex = 0;
            // 
            // MyCmdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 680);
            this.Controls.Add(this.txtOutputInfo);
            this.Name = "MyCmdForm";
            this.Text = "MyCmdForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutputInfo;
    }
}