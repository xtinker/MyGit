using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ConfigHelper;
using View.Cmd_MarkDown;
using View.CreateQrImage;
using View.DataChange;
using View.EncryptTool;
using View.Goodway.Oracle;
using View.OracleScript;
using View.PostSubmit;
using View.Skins;
using View.SqlServer;
using MainTool.Properties;
using Sunisoft.IrisSkin;
using View;
using View.OCR;
using View45;

namespace MainTool
{
    public partial class MainForm : Form
    {
        private string skinName = Config.DefaultTheme;
        private SkinEngine skin = new SkinEngine();
        private Form subForm = new Form();

        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            skin.SkinFile = Environment.CurrentDirectory + "\\skins\\" + skinName + ".ssk";
            skin.Active = true;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            subForm.WindowState = FormWindowState.Normal;
            subForm.Dock = DockStyle.Fill;
        }

        /// <summary>
        ///   Base64编码
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <returns>编码后的字符串</returns>
        public static string Base64Encoding(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            str = Convert.ToBase64String(bytes);
            return str;
        }

        private void 重置配置文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mCurPath = AppDomain.CurrentDomain.BaseDirectory;
            var nConfigFullName = Path.Combine(mCurPath, "MainTool.exe.config");
            var oConfigFullName = Path.Combine(mCurPath, "MainTool.exe.bak.config");
            File.Delete(nConfigFullName);
            File.Copy(oConfigFullName, nConfigFullName);
            MessageBox.Show(Resources.MainForm_重置配置文件ToolStripMenuItem_Click_ResetConfigSuccess);
        }

        private void 加密解密ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new Form1();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void oracle脚本生成工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new ScriptForm(this);//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void Oracle流程定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new TabForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void pOST提交工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new PostSubmitForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void SqlServer脚本生成工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new View.SqlServerScript.ScriptForm(this);//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void SqlServer流程定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new View.Goodway.SqlServer.FlowScriptForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void oCRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new OcrForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void sqlServer备份与还原ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new DbBakupAndRollBack();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void 进制转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new DataChangeForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new SkinForm(skin);//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void dataProblemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var path = Path.Combine(Application.StartupPath, "Log\\DataProblem\\" + date + ".Log");
            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void systemErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var path = Path.Combine(Application.StartupPath, "Log\\SystemError\\" + date + ".Log");
            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdMarkDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new MarkDownForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void 二维码生成器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new CodeForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void parserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new CodeForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void 反编译ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = Path.Combine(new[] { AppDomain.CurrentDomain.BaseDirectory, "Plus\\IL Spy\\ILSpy.exe" });
            p.StartInfo.LoadUserProfile = true;
            p.StartInfo.WorkingDirectory = Path.Combine(new[] { AppDomain.CurrentDomain.BaseDirectory, "Plus\\IL Spy" });
            p.Start();
        }

        #region 命令行相关

        private void inetmgrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProcess(@"%windir%\system32\inetsrv\InetMgr.exe");
        }

        private void servicesmscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProcess("services.msc");
        }

        private void regeditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProcess("regedit");
        }

        private void gpeditmscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProcess("gpedit.msc");
        }

        private void 计算器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProcess("calc");
        }

        private void 记事本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProcess("notepad");
        }

        private void wordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProcess("winword");
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProcess("excel");
        }

        private void 画图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProcess("mspaint");
        }

        private void ipconfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startMyCmdForm("ipconfig");
        }

        private void iisresetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startMyCmdForm("iisreset");
        }

        /// <summary>
        /// 显示命令行窗口
        /// </summary>
        /// <param name="commandString">命令行字符串，如：ipconfig</param>
        private void startMyCmdForm(string commandString)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new MyCmdForm(commandString);//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new CmdForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void openProcess(string processName)
        {
            Process p = Process.Start(processName);
            if (p != null)
            {
                p.WaitForInputIdle();
                SetParent(p.MainWindowHandle, Handle);
                ShowWindowAsync(p.MainWindowHandle, 3);
            }
            else
            {
                MessageBox.Show("命令不正确！");
            }
        }

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        #endregion

        private void 工作日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new WorkLogForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void 字符串格式化toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new StringFormatForm1();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string temp1 = "DELETE FROM p_Project WHERE p_projectId IN ('')";
            //string temp2 = "DELETE p_Project WHERE p_projectId IN ('')";
            //string temp3 = "DELETE s_ProjectTeam FROM s_ProjectTeam INNER JOIN vp_interface_project p ON p.ProjectId=s_ProjectTeam.ProjGUID WHERE ProjGUID IN ('c6053e7e-037f-40e0-afd0-f74b97dd5dfa') ";

            ////Regex regex = new Regex(@"\bDELETE\b.*?\bFROM\b", RegexOptions.IgnoreCase);
            //Regex regex = new Regex(@"\bDELETE\b", RegexOptions.IgnoreCase);
            //bool isMatching1 = regex.IsMatch(temp1);
            //bool isMatching2 = regex.IsMatch(temp2);
            //bool isMatching3 = regex.IsMatch(temp3);

            //MessageBox.Show($"{isMatching1},{isMatching2},{isMatching3}");

            //项目过滤匹配测试
            //Regex regex = new Regex(@"{(?<=\{)(项目过滤|目标项目过滤)[^}]*(?=\})}", RegexOptions.IgnoreCase);
            //string strTemp = "DELETE FROM p_Project WHERE {项目过滤|p_projectId} AND {目标项33目过滤}";
            //var matches = regex.Matches(strTemp);
            //for (int i = 0; i < matches.Count; i++)
            //{
            //    Match match = matches[i];
            //    var str = match.Value;
            //    string[] args = str.Replace("{", "").Replace("}", "").Split('|');
            //    string projField = "ProjGUID";
            //    if (args.Length > 1)
            //    {
            //        projField = args[1];
            //    }
            //    strTemp = regex.Replace(strTemp, $"{projField} IN ('111','222')", 1);
            //}

            string strTemp = "项目团队表data ";
            bool f = strTemp.Trim().EndsWith("data");

            MessageBox.Show(f.ToString());
        }

        #region 明源

        private void 建模文档中心ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new MySoftBrowserForm(Resources.Document_建模文档中心);//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.WindowState = FormWindowState.Maximized;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void 后端服务生成前端脚本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new CreateScriptForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void 元数据比对ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new CompareForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void 枚举导出工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new EnumExportForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void 数据字典ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new DataDictForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        private void 数据升级比较ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new TableCompareForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }

        #endregion

        private void 翻译助手ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            IsMdiContainer = true;//设置父窗体是容器
            subForm = new TranslateForm();//实例化子窗体
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.TopLevel = false;
            subForm.ControlBox = false;
            subForm.Dock = DockStyle.Fill;
            subForm.MdiParent = this;//设置窗体的父子关系
            subForm.Parent = panel1;//设置子窗体的容器为父窗体中的Panel
            subForm.Show();//显示子窗体，此句很重要，否则子窗体不会显示
        }
    }
}