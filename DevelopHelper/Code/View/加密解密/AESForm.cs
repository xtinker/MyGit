using System;
using System.Text;
using System.Windows.Forms;
using EncryptType;

namespace View.EncryptTool
{
    public partial class AESForm : Form
    {
        public AESForm()
        {
            InitializeComponent();
            cbBase64.Checked = true;
        }

        private void cbBase64_Click(object sender, EventArgs e)
        {
            cbHex16.Checked = false;
            if (!cbBase64.Checked)
            {
                cbBase64.Checked = true;
            }
        }

        private void cbHex16_Click(object sender, EventArgs e)
        {
            cbBase64.Checked = false;
            if (!cbHex16.Checked)
            {
                cbHex16.Checked = true;
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (CheckTextBox())
            {
                string entryStr = txtEncrypt.Text;
                string key = txtKey.Text;
                try
                {
                    if (cbBase64.Checked)
                    {
                        byte[] bytes = AES.Encrypt(entryStr, key);
                        txtDecrypt.Text = FormatString.ToBase64String(bytes);
                    }
                    if (cbHex16.Checked)
                    {
                        byte[] bytes = AES.Encrypt(entryStr, key);
                        txtDecrypt.Text = FormatString.Hex_2To16(bytes);
                    }
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
                string entryStr = txtEncrypt.Text;
                string key = txtKey.Text;
                try
                {
                    if (cbBase64.Checked)
                    {
                        byte[] bytes = FormatString.FromBase64String(entryStr);
                        txtDecrypt.Text = Encoding.Default.GetString(AES.Decrypt(bytes, key));
                    }
                    if (cbHex16.Checked)
                    {
                        byte[] bytes = FormatString.Hex_16To2(entryStr);
                        txtDecrypt.Text = Encoding.Default.GetString(AES.Decrypt(bytes, key));
                    }
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
            if (string.IsNullOrWhiteSpace(txtKey.Text))
            {
                MessageBox.Show("密钥是必须的啊！");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtKey.Text))
            {
                if (txtKey.Text.Length != 16 && txtKey.Text.Length != 32)
                {
                    MessageBox.Show("密钥长度必须为16或32位");
                }
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
