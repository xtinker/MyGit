using System;
using System.Windows.Forms;
using EncryptType;

namespace View.EncryptTool
{
    public partial class MySoftForm : Form
    {
        public MySoftForm()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            var stringOld = getOldString();
            var stringNew = Cryptography.EnCode(stringOld);
            txtOutput.Text = stringNew;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            var stringOld = getOldString();
            var stringNew = Cryptography.DeCode(stringOld);
            txtOutput.Text = stringNew;
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void txtOutput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private string getOldString()
        {
            var stringOld = txtInput.Text;
            if (string.IsNullOrWhiteSpace(stringOld.Trim()))
            {
                MessageBox.Show("请输入有效字符串！");
            }

            return stringOld;
        }
    }
}
