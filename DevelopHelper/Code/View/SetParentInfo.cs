using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace View
{
    public class SetParentInfo
    {
        /// <summary>
        /// 设置父窗体的状态栏toolStripStatusLabel1相关值
        /// </summary>
        /// <param name="mainForm">父窗体</param>
        /// <param name="message">消息</param>
        /// <param name="statusStripName">StatusStripName</param>
        /// <param name="toolStripStatusLabelName">ToolStripStatusLabelName</param>
        public static void SetStatusString(Form mainForm, string message, string statusStripName = "statusStrip1",
            string toolStripStatusLabelName = "toolStripStatusLabel1")
        {
            ((StatusStrip)mainForm.Controls[statusStripName]).Items[toolStripStatusLabelName].Text = message;
            ((StatusStrip)mainForm.Controls[statusStripName]).Update();
        }

        /// <summary>
        /// 设置父窗体的状态栏toolStripStatusLabel2相关值
        /// </summary>
        /// <param name="mainForm">父窗体</param>
        /// <param name="message">消息</param>
        /// <param name="statusStripName">StatusStripName</param>
        /// <param name="toolStripStatusLabelName">ToolStripStatusLabelName</param>
        public static void SetRightStatusString(Form mainForm, string message, string statusStripName = "statusStrip1",
            string toolStripStatusLabelName = "toolStripStatusLabel2")
        {
            ((StatusStrip)mainForm.Controls[statusStripName]).Items[toolStripStatusLabelName].Text = message;
            ((StatusStrip)mainForm.Controls[statusStripName]).Update();
        }

        /// <summary>
        /// 设置父窗体的状态栏toolStripStatusLabel2相关值
        /// </summary>
        /// <param name="mainForm">父窗体</param>
        /// <param name="value">进度值，0到100</param>
        /// <param name="statusStripName">StatusStripName</param>
        /// <param name="toolStripStatusLabelName">ToolStripStatusLabelName</param>
        public static void SetToolStripProgressBar(Form mainForm, int value, string statusStripName = "statusStrip1", string toolStripStatusLabelName = "toolStripProgressBar1")
        {
            ProgressBar progress = new ProgressBar();
            progress.Parent = (StatusStrip) mainForm.Controls[statusStripName];
            progress.Value = value;
            
            ((StatusStrip)mainForm.Controls[statusStripName]).Show();
        }
    }
}


