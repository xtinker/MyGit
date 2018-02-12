using System;
using System.Windows.Forms;
using ConfigHelper;
using DbHelper;
using System.Data.OracleClient;

namespace View.OracleScript
{
    public partial class ConDbForm : Form
    {
        private readonly ScriptForm parent;
        private OracleConnection conn;

        private ConDbForm(ScriptForm parent)
        {
            this.parent = parent;
            InitializeComponent();

            OracleConnection con = ConfigParse.GetConn<OracleConnection>(Config.OracleSource.ConnectionString);
            txtDataSource.Text = con.DataSource;
            txtUserId.Text = Config.OracleSourceUser;
            txtPassword.Text = Config.OracleSourcePassword;
        }

        private static ConDbForm _instance;
        public static ConDbForm GetInstance(ScriptForm parent)
        {
            //判断是否存在该窗体,或时候该字窗体是否被释放过,如果不存在该窗体,则 new 一个新窗体  
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new ConDbForm(parent);
            }
            return _instance;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            var dbSource = txtDataSource.Text.Trim();
            var dbUser = txtUserId.Text.Trim();
            var dbPass = txtPassword.Text.Trim();

            if (String.IsNullOrWhiteSpace(dbSource))
            {
                MessageBox.Show("服务器名称不能为空");
                return;
            }

            if (String.IsNullOrWhiteSpace(dbUser))
            {
                MessageBox.Show("用户名不能为空");
                return;
            }

            if (String.IsNullOrWhiteSpace(dbPass))
            {
                MessageBox.Show("密码不能为空");
                return;
            }

            connectionString = String.Format("DATA SOURCE={0};USER ID={1};PASSWORD={2};", txtDataSource.Text.Trim().ToUpper(), txtUserId.Text.Trim().ToUpper(), txtPassword.Text.Trim());
            conn = new OracleConnection(connectionString);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string connectionString;
        private void ConDbForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (conn != null)
            {
                //测试连接
                SqlHelper sqlHelper = new OracleHelper(connectionString);
                var flag = sqlHelper.ConnectionTest(connectionString);

                if (flag)
                {
                    //保存配置
                    saveConfig();

                    parent.Conn = conn;
                    parent.BindTreeView();
                }
                else
                {
                    MessageBox.Show("连接失败");
                }
            }
        }

        /// <summary>
        /// 保存设置源的数据库连接配置
        /// </summary>
        private void saveConfig()
        {
            ConfigParse.SaveConfig("OracleSource", connectionString);
            ConfigParse.SaveAppSettings("OracleSourceUser", txtUserId.Text.Trim().ToUpper());
            ConfigParse.SaveAppSettings("OracleSourcePassword", txtPassword.Text.Trim());
        }
    }
}
