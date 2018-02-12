using System;
using System.Windows.Forms;
using EncryptType;

namespace View.EncryptTool
{
    public partial class Base64Form : Form
    {
        public Base64Form()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (CheckTextBox())
            {
                var entryStr = txtEncrypt.Text;
                try
                {
                    txtDecrypt.Text = BASE64.Encrypt(entryStr);
                }
                catch (Exception ex)
                {
                    txtDecrypt.Text = ex.Message;
                }
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (CheckTextBox())
            {
                var entryStr = txtEncrypt.Text;

                try
                {
                    txtDecrypt.Text = BASE64.Decrypt(entryStr);
                }
                catch (Exception ex)
                {
                    txtDecrypt.Text = ex.Message;
                }
            }
        }

        private bool CheckTextBox()
        {
            if (string.IsNullOrWhiteSpace(txtEncrypt.Text))
            {
                MessageBox.Show("总得输入点什么吧！");
                return false;
            }
            return true;
        }

        private void txtDecrypt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void txtEncrypt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }
    }
}
