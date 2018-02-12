using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace View.PostSubmit
{
    [ComVisible(true)]
    public partial class PostSubmitForm : Form
    {
        string content = "";
        public PostSubmitForm()
        {
            InitializeComponent();
            webBrowser.Url = new Uri(Application.StartupPath + "\\kindeditor\\e.html", UriKind.Absolute);
            webBrowser.ObjectForScripting = this;
            webBrowser.Visible = false;
            richTextBox.Visible = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //地址  
            string url = txtUrl.Text.Trim();

            if (String.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("提交地址不能为空");
                return;
            }

            //这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来
            string postString = txtSubmitData.Text.Trim();

            //编码，尤其是汉字，事先要看下抓取网页的编码方式 
            byte[] postData = Encoding.UTF8.GetBytes(postString);

            WebClient webClient = new WebClient();

            var protocol = txtProtocol.Text.Trim();
            if (!String.IsNullOrWhiteSpace(protocol))
            {
                //采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
                webClient.Headers.Add("Content-Type", txtProtocol.Text.Trim());
            }

            try
            {
                //得到返回字符流
                byte[] responseData = webClient.UploadData(url, "POST", postData);
                //解码 
                string coding = cboEncoding.Text;
                switch (coding)
                {
                    case "GBK":
                        content = Encoding.GetEncoding("GBK").GetString(responseData);
                        break;
                    case "UTF8":
                        content = Encoding.UTF8.GetString(responseData);
                        break;
                    case "BIG5":
                        content = Encoding.GetEncoding("BIG5").GetString(responseData);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            richTextBox.Text = content;
        }

        private bool flag = true;
        private void btnView_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                btnView.Text = "显示源码";
                richTextBox.Visible = false;
                webBrowser.Visible = true;
                SetDetailContent();
                flag = false;
            }
            else
            {
                btnView.Text = "显示页面";
                richTextBox.Visible = true;
                webBrowser.Visible = false;
                flag = true;
            }
        }

        private void webBrowser_Resize(object sender, EventArgs e)
        {
            webBrowser.Refresh();
        }

        public void SetDetailContent()
        {
            if (webBrowser.Document != null) webBrowser.Document.InvokeScript("setContent", new object[] { content });
        }
    }
}
