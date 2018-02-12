namespace View
{
    partial class PlusForm
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
            this.components = new System.ComponentModel.Container();
            this.appContainer = new MyControl.AppContainer(this.components);
            this.SuspendLayout();
            // 
            // appContainer
            // 
            this.appContainer.AppFilename = "";
            this.appContainer.AppProcess = null;
            this.appContainer.Location = new System.Drawing.Point(4, 3);
            this.appContainer.Name = "appContainer";
            this.appContainer.Size = new System.Drawing.Size(1276, 675);
            this.appContainer.TabIndex = 0;
            // 
            // PlusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 680);
            this.Controls.Add(this.appContainer);
            this.Name = "PlusForm";
            this.Text = "PlusForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PlusForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private MyControl.AppContainer appContainer;
    }
}