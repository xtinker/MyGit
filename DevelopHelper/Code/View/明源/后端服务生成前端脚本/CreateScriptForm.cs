using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using ConfigHelper;

namespace View
{
    public partial class CreateScriptForm : Form
    {
        public CreateScriptForm()
        {
            InitializeComponent();

            initSite();
        }

        private void initSite()
        {
            cbErpSite.Items.Clear();
            Hashtable sections = (Hashtable)ConfigurationManager.GetSection("ERPSite");
            if (sections != null)
            {
                foreach (var item in sections.Keys)
                {
                    cbErpSite.Items.Add(item.ToString());
                }
            }
        }

        private void cbErpSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            setUrl();
        }

        private void cbErpSite_TextChanged(object sender, EventArgs e)
        {
            setUrl();
        }

        private void txtServiceName_TextChanged(object sender, EventArgs e)
        {
            setUrl();
        }

        private void btnCreateScript_Click(object sender, EventArgs e)
        {
            var url = txtUrl.Text.Trim();
            if (string.IsNullOrEmpty(url))
            {
                MessageBox.Show("请正确输入URL链接地址！");
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                var taskResult = client.PostAsync(url, null);
                var result = taskResult.Result;

                txtScript.Text = result.Content.ReadAsStringAsync().Result;
            }

            //记录ERP站点地址
            ConfigParse.SaveConfigSections(cbErpSite.Text, cbErpSite.Text, "ERPSite");

            //刷新ERP站点的选项
            initSite();
        }

        private void txtScript_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        #region 私有方法

        private void setUrl()
        {
            var site = cbErpSite.Text;
            if (!string.IsNullOrEmpty(site))
            {
                if (site.IndexOf("://", StringComparison.Ordinal) > 0)
                {
                    site = site.Replace("://","|").Split('|')[1];
                }
                var urlString = "http://" + site + "/script/{0}/proxy.aspx";
                if (string.IsNullOrEmpty(txtServiceName.Text.Trim()))
                {
                    urlString = string.Format(urlString, "服务类名称");
                }
                else
                {
                    urlString = string.Format(urlString, txtServiceName.Text.Trim());
                }

                txtUrl.Text = urlString;
            }
        }

        #endregion

    }
}
