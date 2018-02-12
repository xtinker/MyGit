namespace View
{
    partial class CompareForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSourceGUID = new System.Windows.Forms.TextBox();
            this.btnCompare = new System.Windows.Forms.Button();
            this.btnCompareByFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "源文件夹（左边）：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "目标文件夹（右边）：";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(145, 10);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(1127, 21);
            this.txtSource.TabIndex = 2;
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(145, 50);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(1127, 21);
            this.txtTarget.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "元数据GUID：";
            // 
            // txtSourceGUID
            // 
            this.txtSourceGUID.Location = new System.Drawing.Point(145, 91);
            this.txtSourceGUID.Name = "txtSourceGUID";
            this.txtSourceGUID.Size = new System.Drawing.Size(501, 21);
            this.txtSourceGUID.TabIndex = 5;
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(652, 85);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(143, 30);
            this.btnCompare.TabIndex = 6;
            this.btnCompare.Text = "比对";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // btnCompareByFolder
            // 
            this.btnCompareByFolder.Location = new System.Drawing.Point(801, 85);
            this.btnCompareByFolder.Name = "btnCompareByFolder";
            this.btnCompareByFolder.Size = new System.Drawing.Size(143, 30);
            this.btnCompareByFolder.TabIndex = 7;
            this.btnCompareByFolder.Text = "文件夹比对";
            this.btnCompareByFolder.UseVisualStyleBackColor = true;
            this.btnCompareByFolder.Click += new System.EventHandler(this.btnCompareByFolder_Click);
            // 
            // CompareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 680);
            this.Controls.Add(this.btnCompareByFolder);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.txtSourceGUID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTarget);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CompareForm";
            this.Text = "CompareForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSourceGUID;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Button btnCompareByFolder;
    }
}