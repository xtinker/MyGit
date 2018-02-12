using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DbHelper;

namespace View45
{
    public partial class DataDictForm : Form
    {
        private bool isLogin;
        private SqlHelper sqlHelper;
        private readonly string[] ignoreDbNames = { "master", "model", "msdb", "tempdb" };

        public DataDictForm()
        {
            InitializeComponent();

            //初始化默认值
            txtSourceName.Text = "10.5.107.88\\MSSQL2016";
            txtUser.Text = "sa";
            txtPassword.Text = "95938";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (checkLoginInfo())
            {
                var connectionString = $"Data Source={txtSourceName.Text.Trim()};Initial Catalog={"master"};User Id={txtUser.Text.Trim()};Password={txtPassword.Text.Trim()};";
                sqlHelper = new SqlServerHelper(connectionString);
                isLogin = sqlHelper.ConnectionTest(connectionString);
                if (isLogin)
                {
                    //初始化数据库选择项
                    initDbName();
                    lblMessage.Text = "登录成功";
                }
                else
                {
                    lblMessage.Text = "";
                    MessageBox.Show("连接失败");
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (checkSelectInfo())
            {
                string dbName = cbDbName.Text;
                string tableName = txtTableName.Text.Trim();
                bool matchedPattern = cbMatchedPattern.Checked;
                setViewData(dbName, tableName, matchedPattern);
            }
        }

        private void cbMatchedPattern_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSelectInfo())
            {
                string dbName = cbDbName.Text;
                string tableName = txtTableName.Text.Trim();
                bool matchedPattern = cbMatchedPattern.Checked;
                setViewData(dbName, tableName, matchedPattern);
            }
        }

        #region 私有方法

        /// <summary>
        /// 检测登录信息
        /// </summary>
        /// <returns></returns>
        private bool checkLoginInfo()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(txtSourceName.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("服务器名称不能为空！");
            }
            else if (string.IsNullOrWhiteSpace(txtUser.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("用户名不能为空！");
            }
            else if (string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("密码不能为空！");
            }

            return isValid;
        }

        /// <summary>
        /// 检测查询信息
        /// </summary>
        /// <returns></returns>
        private bool checkSelectInfo()
        {
            bool isValid = true;
            if (!isLogin)
            {
                isValid = false;
                MessageBox.Show("请先登录！");
            }
            else if (string.IsNullOrWhiteSpace(cbDbName.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("请选择数据库！");
            }
            else if (string.IsNullOrWhiteSpace(txtTableName.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("请输入要查询的表名称！");
            }

            return isValid;
        }

        /// <summary>
        /// 初始化数据库选择项
        /// </summary>
        private void initDbName()
        {
            //清除原数据库名称选项
            cbDbName.Text = "";
            cbDbName.Items.Clear();

            const string sqlDb = "SELECT name FROM sysdatabases ORDER BY name";
            var dt = sqlHelper.ExecuteDataTable(sqlDb);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var dbName = row["name"].ToString();
                    if (!ignoreDbNames.Contains(dbName))
                    {
                        cbDbName.Items.Add(row["name"]);
                    }
                }
            }
        }

        /// <summary>
        /// 是否是高版本
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <returns>是否高版本</returns>
        private bool isHigher(string dbName)
        {
            DataTable dt = sqlHelper.ExecuteDataTable(string.Format(sqlVersion, dbName));

            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// 查询数据字典
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">表名称</param>
        /// <param name="matchedPattern">匹配模式，默认模糊匹配</param>
        private void setViewData(string dbName, string tableName, bool matchedPattern)
        {
            string sqlSelect;
            bool ishigher = isHigher(dbName);
            if (ishigher)
            {
                sqlSelect = sqlLikeHigher;
                if (matchedPattern)
                    sqlSelect = sqlHigher;
            }
            else
            {
                sqlSelect = sqlLike;
                if (matchedPattern)
                    sqlSelect = sql;
            }

            DataTable dt = sqlHelper.ExecuteDataTable(string.Format(sqlSelect, dbName, tableName));
            if (dt.Rows.Count > 0)
            {
                dataGridView.DataSource = dt;
            }
            else
            {
                DataTable emptyDt = new DataTable();
                emptyDt.Columns.Add("Message");
                DataRow emptyRow = emptyDt.NewRow();
                emptyRow["Message"] = "无数据";
                emptyDt.Rows.Add(emptyRow);
                dataGridView.DataSource = emptyDt;
            }
        }

        private readonly string sqlVersion = "USE [{0}];SELECT [ApplicationName],[Application],[Version] FROM [myApplication] WHERE [ApplicationName]='销售系统' AND [Application]='0011' AND [Version] LIKE '1.0%'";

        private readonly string sqlLikeHigher = "USE [{0}];SELECT ROW_NUMBER () OVER (ORDER BY [field_name] ) AS rownum,[TableGUID],[table_name],[table_name_c],[field_name],[field_name_c],[data_type],[width],[DecimalPrecision],[Description] FROM [data_dict] WHERE [table_name] LIKE '%{1}%'";

        private readonly string sqlHigher = "USE [{0}];SELECT ROW_NUMBER () OVER (ORDER BY [field_name] ) AS rownum,[TableGUID],[table_name],[table_name_c],[field_name],[field_name_c],[data_type],[width],[DecimalPrecision],[Description] FROM [data_dict] WHERE [table_name] = '{1}'";

        private readonly string sqlLike = "USE [{0}];SELECT ROW_NUMBER () OVER (ORDER BY [field_name] ) AS rownum,[TableGUID],[table_name],[table_name_c],[field_name],[field_name_c],[data_type],[width],[defaultvalue],[Description] FROM [data_dict] WHERE [table_name] LIKE '%{1}%'";

        private readonly string sql = "USE [{0}];SELECT ROW_NUMBER () OVER (ORDER BY [field_name] ) AS rownum,[TableGUID],[table_name],[table_name_c],[field_name],[field_name_c],[data_type],[width],[defaultvalue],[Description] FROM [data_dict] WHERE [table_name] = '{1}'";

        #endregion
    }
}
