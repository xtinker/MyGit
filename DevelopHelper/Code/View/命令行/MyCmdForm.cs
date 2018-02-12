using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace View
{
    public partial class MyCmdForm : Form
    {
        private readonly string _commandString;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commandString">命令行字符串，如：ipconfig</param>
        public MyCmdForm(string commandString)
        {
            _commandString = commandString;
            InitializeComponent();
            init();
        }

        private static MyCmdForm _instance;
        public static MyCmdForm GetInstance(string commandString)
        {
            //判断是否存在该窗体,或时候该字窗体是否被释放过,如果不存在该窗体,则 new 一个新窗体  
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new MyCmdForm(commandString);
            }
            return _instance;
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        private void init()
        {
            Process p = new Process();
            //CheckForIllegalCrossThreadCalls = false;
            //p.StartInfo.FileName = "cmd.exe";
            //p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            //p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            //p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            //p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            //p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            //p.OutputDataReceived += OutputHandler;
            //p.Start();//启动程序
            //p.BeginOutputReadLine();

            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口

            p.Start();//启动程序
            System.Threading.Thread.Sleep(100);//加上，100如果效果没有就继续加大

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(_commandString + "&exit");

            p.StandardInput.AutoFlush = true;
            p.StandardInput.WriteLine("exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令

            //获取cmd窗口的输出信息
            string sOutput = p.StandardOutput.ReadToEnd();

            txtOutputInfo.Text = sOutput;
        }

        private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                StringBuilder sb = new StringBuilder(txtOutputInfo.Text);
                txtOutputInfo.Text = sb.AppendLine(outLine.Data).ToString();
                txtOutputInfo.SelectionStart = txtOutputInfo.Text.Length;
                txtOutputInfo.ScrollToCaret();
            }
        }
    }
}
