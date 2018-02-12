using System;
using System.Windows.Forms;
using System.Web;

namespace View
{
    public partial class TranslateForm : Form
    {
        private string _host = string.Empty;
        private string _searchKey = string.Empty;

        public TranslateForm()
        {
            InitializeComponent();
        }
        
        private void txtSearchKey_TextChanged(object sender, EventArgs e)
        {
            _searchKey = txtSearchKey.Text;
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_searchKey))
            {
                MessageBox.Show("请输入需要翻译的词");
                return;
            }

            if (radioButton1.Checked)
            {
                _host = radioButton1.Tag + _searchKey;
            }
            else if (radioButton2.Checked)
            {
                _host = radioButton2.Tag + HttpUtility.UrlEncode(_searchKey);
            }
            else if (radioButton3.Checked)
            {
                _host = radioButton3.Tag + HttpUtility.UrlEncode(_searchKey);
            }

            webBrowserFresh(_host);
        }

        private void txtSearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTranslate_Click(sender, e);
            }
        }

        /// <summary>
        /// 翻译助手
        /// </summary>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _host = radioButton1.Tag + _searchKey;
            webBrowserFresh(_host);
        }

        /// <summary>
        /// 百度翻译
        /// </summary>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _host = radioButton2.Tag + HttpUtility.UrlEncode(_searchKey);
            webBrowserFresh(_host);
        }

        /// <summary>
        /// 谷歌翻译
        /// </summary>
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            _host = radioButton3.Tag + HttpUtility.UrlEncode(_searchKey);
            webBrowserFresh(_host);
        }

        private void webBrowserFresh(string url)
        {
            webBrowser.Navigate("about:blank");
            webBrowser.Url = new Uri(url, UriKind.Absolute);
        }
    }
}
