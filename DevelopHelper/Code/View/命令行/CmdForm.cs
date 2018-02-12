using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace View
{
    public partial class CmdForm : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CmdForm()
        {
            InitializeComponent();

            init();
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        private void init()
        {
            Process p = new Process();

            p.StartInfo.FileName = "cmd.exe ";

            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;//加上这句效果更好

            p.Start();
            Thread.Sleep(100);//加上，100如果效果没有就继续加大

            SetParent(p.MainWindowHandle, panel1.Handle); //panel1.Handle为要显示外部程序的容器

            ShowWindow(p.MainWindowHandle, 3);
        }


        [DllImport("User32.dll ", EntryPoint = "SetParent")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll ", EntryPoint = "ShowWindow")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

    }
}
