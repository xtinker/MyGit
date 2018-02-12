using System;
using System.Text;
using System.Windows.Forms;
using EncryptType;

namespace View.EncryptTool
{
    public partial class DESForm : Form
    {
        public DESForm()
        {
            InitializeComponent();
            cbBase64.Checked = true;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (CheckTextBox())
            {
                var entryStr = txtEncrypt.Text;
                string key = txtKey.Text;
                try
                {
                    if (cbBase64.Checked)
                    {
                        //返回加密后的base64密文
                        byte[] bytes = DES.Encrypt(entryStr, key);
                        txtDecrypt.Text = FormatString.ToBase64String(bytes);
                    }
                    if (cbHex16.Checked)
                    {
                        //返回加密后的16进制密文
                        byte[] bytes = DES.Encrypt(entryStr, key);
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
                string key = txtKey.Text;
                try
                {
                    if (cbBase64.Checked)
                    {
                        //将base64转成byte后再解密
                        byte[] entryBytes = FormatString.FromBase64String(txtEncrypt.Text);
                        byte[] bytes = DES.Decrypt(entryBytes, key);
                        txtDecrypt.Text = Encoding.Default.GetString(bytes);

                    }
                    if (cbHex16.Checked)
                    {
                        //将16进制转成2进制后再解密
                        byte[] entryBytes = FormatString.Hex_16To2(txtEncrypt.Text);
                        byte[] bytes = DES.Decrypt(entryBytes, key);
                        txtDecrypt.Text = Encoding.Default.GetString(bytes);
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
                MessageBox.Show("要密钥的啊！");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtKey.Text) && txtKey.Text.Length != 8)
            {
                MessageBox.Show("密钥必须为8位");
            }
            return true;
        }

        private void cbBase64_Click(object sender, EventArgs e)
        {
            cbBase64.Checked = false;
            if (!cbHex16.Checked)
            {
                cbHex16.Checked = true;
            }
        }

        private void cbHex16_Click(object sender, EventArgs e)
        {
            cbHex16.Checked = false;
            if (!cbBase64.Checked)
            {
                cbBase64.Checked = true;
            }
        }

        private void txtEncrypt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void txtDecrypt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }
    }
}
