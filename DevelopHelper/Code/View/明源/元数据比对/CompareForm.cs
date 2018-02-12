using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace View
{
    public partial class CompareForm : Form
    {
        public CompareForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 文本比较
        /// </summary>
        private void btnCompare_Click(object sender, EventArgs e)
        {
            string pathLeft = "";
            string pathRight = "";

            ProcessStartInfo start = new ProcessStartInfo("D:\\Program Files\\Beyond Compare 4\\BCompare.exe");//设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到   
                                                                                                               //如果是其它exe文件，则有可能需要指定详细路径，如运行winRar.exe   
                                                                                                               //start.Arguments = txtCommand.Text;//设置命令参数   
            start.CreateNoWindow = true;//不显示dos命令行窗口   
            start.RedirectStandardOutput = false;//   
            start.RedirectStandardInput = true;//   
            start.UseShellExecute = false;//是否指定操作系统外壳进程启动程序   
            Process p = Process.Start(start);
        }

        /// <summary>
        /// 文件夹比较
        /// </summary>
        private void btnCompareByFolder_Click(object sender, EventArgs e)
        {

        }

        #region 私有方法

        /// <summary>
        /// 获取元数据目录
        /// </summary>
        /// <param name="folderPath">文件夹目录</param>
        /// <param name="sourceGuid">元数据GUID</param>
        /// <returns></returns>
        private string getFilePath(string folderPath, string sourceGuid)
        {
            string resultStr = Path.Combine(folderPath, sourceGuid + ".metadata.config");
            return resultStr;
        }

        /// <summary>
        /// 向Beyond Compare的BCSessions.xml中写入节点
        /// </summary>
        /// <param name="xmlPath">BCSessions.xml的路径</param>
        /// <param name="left">要比较的文件夹或文件目录(左边)</param>
        /// <param name="right">要比较的文件夹或文件目录(右边)</param>
        private void addXmlNode(string xmlPath, string left, string right)
        {
            //< TDirCompareSession Value = ""AppGrid"" >

            //                         < LastModified Value = ""2017 - 06 - 08 08:40:31"" />

            //                              < Specs >

            //                                  < Left Value = ""D:\MySoft\01Projects\03项目\中山雅居乐\ERP60SP1\源代码\分支9\00 - ERP站点\_metadata\AppGrid"" />

            //                                     < Right Value = ""D:\MySoft\01Projects\03项目\中山雅居乐\ERP60SP1\源代码\分支9\00 - ERP站点\Customize\x_MetaData\AppGrid"" />

            //                                    </ Specs >

            //                               </ TDirCompareSession >
            string xmlTemp = @"<LastModified Value = ""{time}"" />
                                    <Specs >
                                        <Left Value = ""{left}"" />
                                        <Right Value = ""{right}"" />
                                    </Specs > ";
            string time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string xmlString = string.Format(xmlTemp, time, left, right);
            //加载xml文档
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            //创建节点
            XmlElement xmlElement = doc.CreateElement("TDirCompareSession");

            //添加属性
            xmlElement.SetAttribute("Value", "元数据");
            xmlElement.InnerText = xmlString;

            //将节点加入到指定的节点下
            doc.DocumentElement.PrependChild(xmlElement);

            doc.Save(xmlPath);
        }

        #endregion
    }
}
