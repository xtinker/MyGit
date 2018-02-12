using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows.Forms;
using ConfigHelper;
using System.Data.SqlClient;
using System.IO;
using DbHelper;
namespace View.SqlServerScript
{
    public partial class ConDbForm : Form
    {
        private readonly ScriptForm parent;
        private string[] dataSourceList;
        private ConDbForm(ScriptForm parent)
        {
            this.parent = parent;
            //速度太慢，注释掉
            //dataSourceList = SqlLocator.GetLocalSqlServerNamesWithSqlClientFactory();
            cbFlag = false;
            InitializeComponent();

            init();
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

        public SqlConnection Conn;
        private bool cbFlag;
        private string connectionString;

        /// <summary>
        /// 初始化界面
        /// </summary>
        private void init()
        {
            connectionString = Config.SqlServerSource.ConnectionString;
            if (cbSource.Items.Count > 0)
            {
                cbSource.SelectedIndex = 0;
            }
        }

        private void cbSource_DropDown(object sender, EventArgs e)
        {
            if (cbFlag)
            {
                if (cbSource.Items.Count > 0)
                {
                    cbSource.DataSource = null;
                    cbSource.Items.Clear();
                }
                cbSource.DataSource = dataSourceList;
                cbFlag = false;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataSourceList = SqlLocator.GetLocalSqlServerNamesWithSqlClientFactory();
            if (cbSource.Items.Count > 0)
            {
                cbSource.SelectedIndex = 0;
            }
            cbFlag = true;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            connectionString = radioButton1.Checked ?
                String.Format("Data Source={0};Initial Catalog=master;Integrated Security=True;", cbSource.Text) :
                String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", cbSource.Text, "master", txtUserId.Text.Trim(), txtPassword.Text.Trim());
            Conn = new SqlConnection(connectionString);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConDbForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Conn != null)
            {
                //测试连接
                SqlHelper sqlHelper = new SqlServerHelper(connectionString);
                var flag = sqlHelper.ConnectionTest(connectionString);
                if (flag)
                {
                    //保存配置
                    saveConfig();

                    parent.CurConn = Conn;
                    parent.BindTreeView();
                }
                else
                {
                    MessageBox.Show("连接失败");
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            var r = (RadioButton)(sender);
            if (r.Checked)
            {
                radioButton2.Checked = false;
                txtUserId.Enabled = false;
                txtPassword.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            var r = (RadioButton)(sender);
            if (r.Checked)
            {
                radioButton1.Checked = false;
                txtUserId.Enabled = true;
                txtPassword.Enabled = true;
                txtUserId.Text = Config.SqlServerSourceUser;
                txtPassword.Text = Config.SqlServerSourcePassword;
            }
        }

        /// <summary>
        /// 保存设置源的数据库连接配置
        /// </summary>
        private void saveConfig()
        {
            var sourceName = cbSource.Text;
            ConfigParse.SaveSqlServerConfigSections(sourceName, connectionString);
        }
    }
}
