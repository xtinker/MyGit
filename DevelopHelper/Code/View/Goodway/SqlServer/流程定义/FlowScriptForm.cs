using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ConfigHelper;
using DbHelper;

namespace View.Goodway.SqlServer
{
    public partial class FlowScriptForm : Form
    {
        public FlowScriptForm()
        {
            InitializeComponent();

            init();
        }

        private string connectionString;

        /// <summary>
        /// 初始化界面
        /// </summary>
        private void init()
        {
            connectionString = Config.Workflow.ConnectionString;

            var con = ConfigParse.GetConn<SqlConnection>(connectionString);

            txtDataSource.Text = con.DataSource;
            txtDbName.Text = con.Database;
            txtUserId.Text = Config.WorkflowUser;
            txtPassword.Text = Config.WorkflowPassword;
        }

        private void txtDataSource_TextChanged(object sender, EventArgs e)
        {
            btnTest.Text = "测试连接";
            btnTest.Enabled = true;
        }

        private void txtDbName_TextChanged(object sender, EventArgs e)
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

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtDataSource.Text.Trim()))
            {
                MessageBox.Show("服务器名称不能为空");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtDbName.Text.Trim()))
            {
                MessageBox.Show("数据库名称不能为空");
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
            connectionString = String.Format("data source={0};initial catalog={1};user id={2};password={3};", txtDataSource.Text.Trim(), txtDbName.Text.Trim(), txtUserId.Text.Trim(), txtPassword.Text.Trim());
            SqlHelper sqlHelper = new SqlServerHelper(connectionString);
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

            StringBuilder sb = new StringBuilder();
            flowCodes = "'" + flowCodes.Replace(",", "','") + "'";
            string delSql = "DELETE FROM S_WF_DefFlow  where Code in({0});";
            delSql = string.Format(delSql, flowCodes);
            sb.Append(delSql + "\r\n");

            string sql = " SELECT * FROM S_WF_DefFlow  where Code in({0})";
            sql = string.Format(sql, flowCodes);

            SqlHelper sqlHelper = new SqlServerHelper(connectionString);
            var dtFlow = sqlHelper.ExecuteDataTable(sql);

            for (int i = 0; i < dtFlow.Rows.Count; i++)
            {
                var defFlow = Write_S_WF_DefFlow(dtFlow, i);
                sb.Append(defFlow);

                var defStep = Write_S_WF_DEFSTEP(dtFlow.Rows[i]["ID"].ToString());
                sb.Append(defStep);

                var defRouting = Write_S_WF_DEFROUTING(dtFlow.Rows[i]["ID"].ToString());
                sb.Append(defRouting);
            }

            txtScript.Text = sb.ToString();
            txtScript.Focus();
            txtScript.SelectAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtScript.Text))
            {
                MessageBox.Show("请先生成脚本");
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                FileName = txtDbName.Text,
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

        private void txtScript_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        #region Private method

        /// <summary>
        /// 保存设置源的数据库连接配置
        /// </summary>
        private void saveConfig()
        {
            ConfigParse.SaveConfig("Workflow", connectionString);
            ConfigParse.SaveAppSettings("WorkflowUser", txtUserId.Text.Trim());
            ConfigParse.SaveAppSettings("WorkflowPassword", txtPassword.Text.Trim());
        }

        private string resetValue(object name)
        {
            string str = null;
            if (name != null)
            {
                str = name.ToString();
                if (str.Contains("'"))
                {
                    str = str.Replace("'", "''");
                }
            }
            return str;
        }

        /// <summary>
        /// 添加流程路由
        /// </summary>
        /// <param name="flowId"></param>
        private string Write_S_WF_DEFROUTING(string flowId)
        {
            var str = "\r\n";
            SqlHelper sqlHelper = new SqlServerHelper(connectionString);
            const string sqlRouting = "SELECT * FROM S_WF_DEFROUTING WHERE DEFFLOWID='{0}'";
            var dtRouting = sqlHelper.ExecuteDataTable(String.Format(sqlRouting, flowId));
            if (dtRouting.Rows.Count > 0)
            {
                for (int j = 0; j < dtRouting.Rows.Count; j++)
                {
                    string sqlInserRouting = "INSERT INTO S_WF_DefRouting(" + "ID, " +
                                              "DEFFLOWID, " +
                                              "DEFSTEPID," +
                                              "ENDID," +
                                              "CODE," +
                                              "NAME," +
                                              "TYPE," +
                                              "VALUE," +
                                              "NOTNULLFIELDS," +
                                              "VARIABLESET," +
                                              "FORMDATASET," +
                                              "SAVEFORM," +
                                              "MUSTINPUTCOMMENT," +
                                              "SAVEFORMVERSION," +
                                              "DEFAULTCOMMENT," +
                                              "SIGNATUREFIELD," +
                                              "SIGNATUREPROTECTFIELDS," +
                                              "SIGNATUREDIVID," +
                                              "SIGNATURECANCELDIVIDS," +
                                              "SORTINDEX," +
                                              "AUTHFORMDATA," +
                                              "AUTHORGIDS," +
                                              "AUTHORGNAMES," +
                                              "AUTHROLEIDS," +
                                              "AUTHROLENAMES," +
                                              "AUTHUSERIDS," +
                                              "AUTHUSERNAMES," +
                                              "AUTHVARIABLE," +
                                              "USERIDS," +
                                              "USERNAMES," +
                                              "USERIDSFROMSTEP," +
                                              "USERIDSFROMSTEPSENDER," +
                                              "USERIDSFROMSTEPEXEC," +
                                              "USERIDSFROMFIELD," +
                                              "USERIDSGROUPFROMFIELD," +
                                              "ROLEIDS," +
                                              "ROLENAMES," +
                                              "ROLEIDSFROMFIELD," +
                                              "ORGIDS," +
                                              "ORGNAMES," +
                                              "ORGIDFROMFIELD," +
                                              "ORGIDFROMUSER," +
                                              "SELECTMODE," +
                                              "SELECTAGAIN," +
                                              "ALLOWWITHDRAW) VALUES(" +
                                              "'{0}'," +
                                              "'{1}'," +
                                              "'{2}'," +
                                              "'{3}'," +
                                              "'{4}'," +
                                              "'{5}'," +
                                              "'{6}'," +
                                              "'{7}'," +
                                              "'{8}'," +
                                              "'{9}'," +
                                              "'{10}'," +
                                              "'{11}'," +
                                              "'{12}'," +
                                              "'{13}'," +
                                              "'{14}'," +
                                              "'{15}'," +
                                              "'{16}'," +
                                              "'{17}'," +
                                              "'{18}'," +
                                              "'{19}'," +
                                              "'{20}'," +
                                              "'{21}'," +
                                              "'{22}'," +
                                              "'{23}'," +
                                              "'{24}'," +
                                              "'{25}'," +
                                              "'{26}'," +
                                              "'{27}'," +
                                              "'{28}'," +
                                              "'{29}'," +
                                              "'{30}'," +
                                              "'{31}'," +
                                              "'{32}'," +
                                              "'{33}'," +
                                              "'{34}'," +
                                              "'{35}'," +
                                              "'{36}'," +
                                              "'{37}'," +
                                              "'{38}'," +
                                              "'{39}'," +
                                              "'{40}'," +
                                              "'{41}'," +
                                              "'{42}'," +
                                              "'{43}'," +
                                              "'{44}');";
                    sqlInserRouting = String.Format(sqlInserRouting, dtRouting.Rows[j]["ID"], dtRouting.Rows[j]["DEFFLOWID"], dtRouting.Rows[j]["DEFSTEPID"], dtRouting.Rows[j]["ENDID"], dtRouting.Rows[j]["CODE"], dtRouting.Rows[j]["NAME"], dtRouting.Rows[j]["TYPE"], dtRouting.Rows[j]["VALUE"], dtRouting.Rows[j]["NOTNULLFIELDS"], resetValue(dtRouting.Rows[j]["VARIABLESET"]), resetValue(dtRouting.Rows[j]["FORMDATASET"]), dtRouting.Rows[j]["SAVEFORM"], dtRouting.Rows[j]["MUSTINPUTCOMMENT"], dtRouting.Rows[j]["SAVEFORMVERSION"], dtRouting.Rows[j]["DEFAULTCOMMENT"], dtRouting.Rows[j]["SIGNATUREFIELD"], dtRouting.Rows[j]["SIGNATUREPROTECTFIELDS"], dtRouting.Rows[j]["SIGNATUREDIVID"], dtRouting.Rows[j]["SIGNATURECANCELDIVIDS"], dtRouting.Rows[j]["SORTINDEX"], resetValue(dtRouting.Rows[j]["AUTHFORMDATA"]), dtRouting.Rows[j]["AUTHORGIDS"], dtRouting.Rows[j]["AUTHORGNAMES"], dtRouting.Rows[j]["AUTHROLEIDS"], dtRouting.Rows[j]["AUTHROLENAMES"], dtRouting.Rows[j]["AUTHUSERIDS"], dtRouting.Rows[j]["AUTHUSERNAMES"], resetValue(dtRouting.Rows[j]["AUTHVARIABLE"]), dtRouting.Rows[j]["USERIDS"], dtRouting.Rows[j]["USERNAMES"], dtRouting.Rows[j]["USERIDSFROMSTEP"], dtRouting.Rows[j]["USERIDSFROMSTEPSENDER"], dtRouting.Rows[j]["USERIDSFROMSTEPEXEC"], dtRouting.Rows[j]["USERIDSFROMFIELD"], dtRouting.Rows[j]["USERIDSGROUPFROMFIELD"], dtRouting.Rows[j]["ROLEIDS"], dtRouting.Rows[j]["ROLENAMES"], dtRouting.Rows[j]["ROLEIDSFROMFIELD"], dtRouting.Rows[j]["ORGIDS"], dtRouting.Rows[j]["ORGNAMES"], dtRouting.Rows[j]["ORGIDFROMFIELD"], dtRouting.Rows[j]["ORGIDFROMUSER"], dtRouting.Rows[j]["SELECTMODE"], dtRouting.Rows[j]["SELECTAGAIN"], dtRouting.Rows[j]["ALLOWWITHDRAW"]);

                    str += sqlInserRouting + "\r\n";
                }
            }

            return str;
        }

        /// <summary>
        /// 添加流程环节
        /// </summary>
        /// <param name="flowId"></param>
        private string Write_S_WF_DEFSTEP(string flowId)
        {
            var str = "\r\n";
            SqlHelper sqlHelper = new SqlServerHelper(connectionString);
            const string sqlStep = "SELECT * FROM S_WF_DEFSTEP WHERE DEFFLOWID='{0}'";
            var dtStep = sqlHelper.ExecuteDataTable(String.Format(sqlStep, flowId));

            if (dtStep.Rows.Count > 0)
            {
                for (int j = 0; j < dtStep.Rows.Count; j++)
                {
                    string sqlInserStep =
                  "INSERT INTO S_WF_DEFSTEP(" + "ID, " +
                  "DEFFLOWID," +
                  "CODE," +
                  "NAME," +
                  "TYPE," +
                  "SORTINDEX," +
                  "ALLOWDELEGATE," +
                  "ALLOWCIRCULATE," +
                  "ALLOWASK," +
                  "ALLOWSAVE," +
                  "SAVEVARIABLEAUTH," +
                  "SUBFLOWCODE," +
                  "WAITINGSTEPIDS," +
                  "COOPERATIONMODE," +
                  "PHASE," +
                  "HIDDENELEMENTS," +
                  "VISIBLEELEMENTS," +
                  "EDITABLEELEMENTS," +
                  "DISABLEELEMENTS," +
                  "URGENCY" +
                  ") VALUES('{0}'," +
                  "'{1}'," +
                  "'{2}'," +
                  "'{3}'," +
                  "'{4}'," +
                  "'{5}'," +
                  "'{6}'," +
                  "'{7}'," +
                  "'{8}'," +
                  "'{9}'," +
                  "'{10}'," +
                  "'{11}'," +
                  "'{12}'," +
                  "'{13}'," +
                  "'{14}'," +
                  "'{15}'," +
                  "'{16}'," +
                  "'{17}'," +
                  "'{18}'," +
                  "'{19}');";

                    sqlInserStep = String.Format(sqlInserStep, dtStep.Rows[j]["ID"],
                        dtStep.Rows[j]["DEFFLOWID"], dtStep.Rows[j]["CODE"],
                        dtStep.Rows[j]["NAME"], dtStep.Rows[j]["TYPE"], dtStep.Rows[j]["SORTINDEX"],
                        dtStep.Rows[j]["ALLOWDELEGATE"], dtStep.Rows[j]["ALLOWCIRCULATE"],
                        dtStep.Rows[j]["ALLOWASK"], dtStep.Rows[j]["ALLOWSAVE"],
                        dtStep.Rows[j]["SAVEVARIABLEAUTH"], dtStep.Rows[j]["SUBFLOWCODE"],
                        dtStep.Rows[j]["WAITINGSTEPIDS"], dtStep.Rows[j]["COOPERATIONMODE"],
                        dtStep.Rows[j]["PHASE"], dtStep.Rows[j]["HIDDENELEMENTS"],
                        dtStep.Rows[j]["VISIBLEELEMENTS"], dtStep.Rows[j]["EDITABLEELEMENTS"],
                        dtStep.Rows[j]["DISABLEELEMENTS"], dtStep.Rows[j]["URGENCY"]);

                    str += sqlInserStep + "\r\n";
                }
            }

            return str;
        }

        /// <summary>
        /// 添加流程主体
        /// </summary>
        /// <param name="dtFlow"></param>
        /// <param name="i"></param>
        private string Write_S_WF_DefFlow(DataTable dtFlow, int i)
        {
            string str = "\r\n";

            string sqlInserFlow = "INSERT INTO S_WF_DefFlow(" + "ID, " +
                                                "CODE," +
                                                "NAME," +
                                                "CONNNAME," +
                                                "TABLENAME," +
                                                "FORMURL," +
                                                "FORMWIDTH," +
                                                "FORMHEIGHT," +
                                                "FLOWNAMETEMPLETE," +
                                                "FLOWCATEGORYTEMPLETE," +
                                                "FLOWSUBCATEGORYTEMPLETE," +
                                                "TASKNAMETEMPLETE," +
                                                "TASKCATEGORYTEMPLETE," +
                                                "TASKSUBCATEGORYTEMPLETE," +
                                                "INITVARIABLE," +
                                                "ALLOWDELETEFORM," +
                                                "SENDMSGTOAPPLICANT," +
                                                "VIEWCONFIG," +
                                                "MODIFYTIME," +
                                                "CATEGORYID," +
                                                "DESCRIPTION," +
                                                "FORMNUMBERPARTA," +
                                                "FORMNUMBERPARTB," +
                                                "FORMNUMBERPARTC," +
                                                "ALREADYRELEASED" +
                                                ") VALUES(" +
                                                "'{0}'," +
                                                "'{1}'," +
                                                "'{2}'," +
                                                "'{3}'," +
                                                "'{4}'," +
                                                "'{5}'," +
                                                "'{6}'," +
                                                "'{7}'," +
                                                "'{8}'," +
                                                "'{9}'," +
                                                "'{10}'," +
                                                "'{11}'," +
                                                "'{12}'," +
                                                "'{13}'," +
                                                "'{14}'," +
                                                "'{15}'," +
                                                "'{16}'," +
                                                "'{24}'," +
                                                "'{17}'," +
                                                "'{18}'," +
                                                "'{19}'," +
                                                "'{20}'," +
                                                "'{21}'," +
                                                "'{22}'," +
                                                "'{23}');";

            sqlInserFlow = String.Format(sqlInserFlow, dtFlow.Rows[i]["ID"], dtFlow.Rows[i]["CODE"], dtFlow.Rows[i]["NAME"],
                dtFlow.Rows[i]["CONNNAME"], dtFlow.Rows[i]["TABLENAME"], dtFlow.Rows[i]["FORMURL"],
                dtFlow.Rows[i]["FORMWIDTH"],
                dtFlow.Rows[i]["FORMHEIGHT"], dtFlow.Rows[i]["FLOWNAMETEMPLETE"],
                dtFlow.Rows[i]["FLOWCATEGORYTEMPLETE"], dtFlow.Rows[i]["FLOWSUBCATEGORYTEMPLETE"],
                dtFlow.Rows[i]["TASKNAMETEMPLETE"], dtFlow.Rows[i]["TASKCATEGORYTEMPLETE"],
                dtFlow.Rows[i]["TASKSUBCATEGORYTEMPLETE"], dtFlow.Rows[i]["INITVARIABLE"],
                dtFlow.Rows[i]["ALLOWDELETEFORM"], dtFlow.Rows[i]["SENDMSGTOAPPLICANT"],
                dtFlow.Rows[i]["MODIFYTIME"], dtFlow.Rows[i]["CATEGORYID"], dtFlow.Rows[i]["DESCRIPTION"],
                dtFlow.Rows[i]["FORMNUMBERPARTA"], dtFlow.Rows[i]["FORMNUMBERPARTB"],
                dtFlow.Rows[i]["FORMNUMBERPARTC"], dtFlow.Rows[i]["ALREADYRELEASED"],
                dtFlow.Rows[i]["VIEWCONFIG"]);

            str += sqlInserFlow + "\r\n";

            return str;
        }

        #endregion
    }
}
