using System.Diagnostics;
using System.Windows.Forms;
using Common;

namespace View
{
    public partial class PlusForm : Form
    {
        private readonly bool isNeedCloseProxy;
        private PlusForm(string appFilename, bool isNeedCloseProxy = false)
        {
            InitializeComponent();

            this.isNeedCloseProxy = isNeedCloseProxy;
            appContainer.AppFilename = appFilename;
            appContainer.Start();
        }

        private static PlusForm _instance;
        public static PlusForm GetInstance(string appFilename, bool isNeedCloseProxy = false)
        {
            //判断是否存在该窗体,或时候该字窗体是否被释放过,如果不存在该窗体,则 new 一个新窗体  
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new PlusForm(appFilename, isNeedCloseProxy);
            }
            return _instance;
        }

        private void PlusForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (isNeedCloseProxy)
            //{
            //    ProxyHelper.UnsetProxy();
            //}

            //Process p = appContainer.AppProcess;
            //try
            //{
            //    p.Kill();
            //    p.Close();
            //}
            //catch
            //{

            //}
        }
    }
}
