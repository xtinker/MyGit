using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ConfigHelper;
using DbHelper;

namespace View.Goodway.Oracle
{
    public partial class FlowScriptForm : Form
    {
        public FlowScriptForm()
        {
            InitializeComponent();

            init();
        }

        private string connectionString;
        private SqlHelper sqlHelper;

        /// <summary>
        /// 初始化界面
        /// </summary>
        private void init()
        {
            connectionString = Config.WorkflowSource.ConnectionString;

            var con = ConfigParse.GetConn<OracleConnection>(connectionString);

            txtDataSource.Text = con.DataSource;
            txtUserId.Text = Config.WorkflowSourceUser;
            txtPassword.Text = Config.WorkflowSourcePassword;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtDataSource.Text.Trim()))
            {
                MessageBox.Show("服务器名称不能为空");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtUserId.Text.Trim()))
            {
                MessageBox.Show("用户名不能为空");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                MessageBox.Show("密码不能为空");
                return;
            }

            //测试连接
            connectionString = String.Format("DATA SOURCE={0};USER ID={1};PASSWORD={2};", txtDataSource.Text.Trim().ToUpper(), txtUserId.Text.Trim().ToUpper(), txtPassword.Text.Trim());
            sqlHelper = new OracleHelper(connectionString);
            var flag = sqlHelper.ConnectionTest(connectionString);
            btnTest.Text = flag ? "连接成功" : "连接失败";
            if (flag)
            {
                //保存配置
                saveConfig();
            }
            btnTest.Enabled = false;
            txtFlowCode.Focus();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string flowCodes = txtFlowCode.Text.Trim();

            if (String.IsNullOrWhiteSpace(flowCodes))
            {
                MessageBox.Show("请输入流程编号，以英文逗号隔开");
                return;
            }

            if (sqlHelper == null)
            {
                MessageBox.Show("请先连接数据库！");
                return;
            }

            StringBuilder sb = new StringBuilder();

            var flowCodesStr = "'" + flowCodes.Replace(",", "','") + "'";
            string delSql = "DELETE FROM S_WF_DefFlow  where Code in ({0});";
            delSql = string.Format(delSql, flowCodesStr);
            sb.Append(delSql + "\r\n\r\nBEGIN");

            var codes = flowCodes.Split(',');
            foreach (string code in codes)
            {
                sb.Append("\r\n--插入流程定义\r\n");
                var defFlow = Write_S_WF_DefFlow(code);
                sb.Append(defFlow);

                sb.Append("\r\n--插入流程环节\r\n");
                var defStep = Write_S_WF_DEFSTEP(code);
                sb.Append(defStep);

                sb.Append("\r\n--插入流程路由\r\n");
                var defRouting = Write_S_WF_DEFROUTING(code);
                sb.Append(defRouting);
            }

            sb.Append("END;");
            txtScript.Text = sb.ToString();
            txtScript.Focus();
            txtScript.SelectAll();
        }

        private void txtDataSource_TextChanged(object sender, EventArgs e)
        {
            btnTest.Text = "测试连接";
            btnTest.Enabled = true;
        }

        private void txtUserId_TextChanged(object sender, EventArgs e)
        {
            btnTest.Text = "测试连接";
            btnTest.Enabled = true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            btnTest.Text = "测试连接";
            btnTest.Enabled = true;
        }

        private void txtScript_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtScript.Text))
                return;

            var saveFileDialog = new SaveFileDialog
            {
                FileName = txtUserId.Text,
                Filter = "SQL(文件)(*.sql)|*.sql|All File(*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string str = saveFileDialog.FileName;
                if (!File.Exists(str))
                {
                    FileInfo myfile = new FileInfo(str);
                    FileStream fs = myfile.Create();
                    fs.Close();
                }
                StreamWriter sw = File.AppendText(str);
                sw.WriteLine(txtScript.Text);
                sw.Flush();
                sw.Close();
            }
        }

        #region Private method

        /// <summary>
        /// 保存设置源的数据库连接配置
        /// </summary>
        private void saveConfig()
        {
            ConfigParse.SaveConfig("WorkflowSource", connectionString);
            ConfigParse.SaveAppSettings("WorkflowSourceUser", txtUserId.Text.Trim().ToUpper());
            ConfigParse.SaveAppSettings("WorkflowSourcePassword", txtPassword.Text.Trim());
        }

        /// <summary>
        /// 添加流程路由
        /// </summary>
        private string Write_S_WF_DEFROUTING(string flowCode)
        {
            var tableName = "S_WF_DEFROUTING";
            List<TableStruct> tableStructs = OracleCommon.GetTableStruct(sqlHelper, tableName);

            const string sqlRouting = "SELECT * FROM S_WF_DEFROUTING WHERE DEFFLOWID=(SELECT ID FROM S_WF_DEFFLOW WHERE CODE='{0}')";
            var dtRouting = sqlHelper.ExecuteDataTable(String.Format(sqlRouting, flowCode));
            var scriptText = OracleCommon.GetScriptText(tableStructs, tableName, dtRouting, flowCode);

            return scriptText;
        }

        /// <summary>
        /// 添加流程环节
        /// </summary>
        private string Write_S_WF_DEFSTEP(string flowCode)
        {
            var tableName = "S_WF_DEFSTEP";
            List<TableStruct> tableStructs = OracleCommon.GetTableStruct(sqlHelper, tableName);

            const string sqlRouting = "SELECT * FROM S_WF_DEFSTEP WHERE DEFFLOWID=(SELECT ID FROM S_WF_DEFFLOW WHERE CODE='{0}')";
            var dtRouting = sqlHelper.ExecuteDataTable(String.Format(sqlRouting, flowCode));
            var scriptText = OracleCommon.GetScriptText(tableStructs, tableName, dtRouting, flowCode);

            return scriptText;
        }

        /// <summary>
        /// 添加流程主体
        /// </summary>
        private string Write_S_WF_DefFlow(string code)
        {
            var tableName = "S_WF_DEFFLOW";
            List<TableStruct> tableStructs = OracleCommon.GetTableStruct(sqlHelper, tableName);

            const string sqlRouting = "SELECT * FROM S_WF_DEFFLOW WHERE CODE='{0}'";
            var dtRouting = sqlHelper.ExecuteDataTable(String.Format(sqlRouting, code));
            var scriptText = OracleCommon.GetScriptText(tableStructs, tableName, dtRouting, code);

            return scriptText;
        }

        #endregion
    }
}
