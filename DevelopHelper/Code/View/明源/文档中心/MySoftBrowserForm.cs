using System;
using System.Windows.Forms;

namespace View
{
    /// <summary>
    /// 明源文档中心，用浏览器内核打开
    /// </summary>
    public partial class MySoftBrowserForm : Form
    {
        private readonly string _url;
        public MySoftBrowserForm(string url)
        {
            _url = url;
            InitializeComponent();

            init(_url);
        }

        private static MySoftBrowserForm _instance;
        /// <summary>
        /// 单例化
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static MySoftBrowserForm GetInstance(string url)
        {
            //判断是否存在该窗体,或时候该字窗体是否被释放过,如果不存在该窗体,则 new 一个新窗体  
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new MySoftBrowserForm(url);
            }
            return _instance;
        }

        private void init(string url)
        {
            webBrowser1.Url = new Uri(url, UriKind.Absolute);
        }
    }
}
