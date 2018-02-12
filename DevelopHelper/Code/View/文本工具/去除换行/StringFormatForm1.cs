using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace View
{
    public partial class StringFormatForm1 : Form
    {
        public StringFormatForm1()
        {
            InitializeComponent();
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            var stringOld = getOldString();
            var stringNew = Regex.Replace(stringOld, @"[\r\n]", "");
            txtStringNew.Text = stringNew;
        }

        private void btnToJson_Click(object sender, EventArgs e)
        {
            var stringOld = getOldString();
            txtStringNew.Text = xmlToJson(stringOld);
        }

        private void btnJsonFormat_Click(object sender, EventArgs e)
        {
            var stringOld = getOldString();
            txtStringNew.Text = formatJsonString(stringOld);
        }

        private void btnToXml_Click(object sender, EventArgs e)
        {
            var stringOld = getOldString();
            txtStringNew.Text = jsonToXml(stringOld);
        }

        private void btnXmlFormat_Click(object sender, EventArgs e)
        {
            var stringOld = getOldString();
            txtStringNew.Text = formatXmlString(stringOld);
        }

        private void btnToUpper_Click(object sender, EventArgs e)
        {
            var stringOld = getOldString();
            var stringNew = stringOld.ToUpper();
            txtStringNew.Text = stringNew;
        }

        private void btnToLower_Click(object sender, EventArgs e)
        {
            var stringOld = getOldString();
            var stringNew = stringOld.ToLower();
            txtStringNew.Text = stringNew;
        }

        /// <summary>
        /// 全选
        /// </summary>
        private void txtStringOld_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        private void txtStringNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        #region 私有方法

        private string getOldString()
        {
            var stringOld = txtStringOld.Text;
            if (string.IsNullOrWhiteSpace(stringOld.Trim()))
            {
                MessageBox.Show("请输入需要格式化的字符串！");
            }

            return stringOld;
        }

        private string xmlToJson(string xml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string json = JsonConvert.SerializeXmlNode(doc);
                return json;
            }
            catch (Exception ex)
            {
                LogServer.LogService.Error("xmlToJson", ex);
                MessageBox.Show("XML转JSON失败！");
            }

            return null;
        }

        private string jsonToXml(string json)
        {
            string tmpl = @"{
              ""?xml"": {
                ""@version"": ""1.0"",
                ""@encoding"": ""utf-8""
              },
                ""root"":" + json + "}";
            
            try
            {
                XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(tmpl);
                return xmlDoc.OuterXml;
            }
            catch (Exception ex)
            {
                LogServer.LogService.Error("xmlToJson", ex);
                MessageBox.Show("JSON转XML失败！");
            }

            return null;
        }

        private string formatJsonString(string str)
        {
            //格式化json字符串
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                TextReader tr = new StringReader(str);
                JsonTextReader jtr = new JsonTextReader(tr);
                object obj = serializer.Deserialize(jtr);
                if (obj != null)
                {
                    StringWriter textWriter = new StringWriter();
                    JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                    {
                        Formatting = Newtonsoft.Json.Formatting.Indented,
                        Indentation = 4,
                        IndentChar = ' '
                    };
                    serializer.Serialize(jsonWriter, obj);
                    return textWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                LogServer.LogService.Error("xmlToJson", ex);
                MessageBox.Show("JSON格式化失败！");
            }

            return str;
        }

        private string formatXmlString(string str)
        {
            //格式化xml字符串
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(str);
                StringWriter sw = new StringWriter();
                using (XmlTextWriter writer = new XmlTextWriter(sw))
                {
                    writer.Indentation = 4;
                    writer.Formatting = System.Xml.Formatting.Indented;
                    doc.WriteContentTo(writer);
                    writer.Close();
                }

                return sw.ToString();
            }
            catch (Exception ex)
            {
                LogServer.LogService.Error("xmlToJson", ex);
                MessageBox.Show("XML格式化失败！");
            }

            return str;
        }

        #endregion
    }
}
