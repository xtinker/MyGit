using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using DbHelper;
using Model;

namespace View.SqlServer
{
    public partial class DbBakupAndRollBack : Form
    {
        private bool isLogin;
        private SqlHelper sqlHelper;

        public DbBakupAndRollBack()
        {
            InitializeComponent();

            //初始化默认值
            txtSourceName.Text = ".";
            txtUser.Text = "sa";
            txtPassword.Text = "95938";
        }

        /// <summary>
        /// 登录
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSourceName.Text.Trim()))
            {
                MessageBox.Show("服务器名称不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtUser.Text.Trim()))
            {
                MessageBox.Show("用户名不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            var connectionString = string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", txtSourceName.Text.Trim(), "master", txtUser.Text.Trim(), txtPassword.Text.Trim());
            sqlHelper = new SqlServerHelper(connectionString);
            isLogin = sqlHelper.ConnectionTest(connectionString);
            if (isLogin)
            {
                getDbNodes();
            }
            else
            {
                MessageBox.Show("连接失败");
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (isLogin)
            {
                getDbNodes();
            }
            else
            {
                MessageBox.Show("请先登录！");
            }
        }

        /// <summary>
        /// 浏览数据库备份文件存放目录
        /// </summary>
        private void btnScan1_Click(object sender, EventArgs e)
        {
            var openFile = new FolderBrowserDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                txtBakupPath.Text = openFile.SelectedPath;
            }
        }

        /// <summary>
        /// 浏览数据库文件目录
        /// </summary>
        private void btnScan2_Click(object sender, EventArgs e)
        {
            var openFile = new FolderBrowserDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                txtRollBackPath.Text = openFile.SelectedPath;
            }
        }

        /// <summary>
        /// 备份
        /// </summary>
        private void btnBakup_Click(object sender, EventArgs e)
        {
            if (!isLogin)
            {
                MessageBox.Show("请先登录！");
                return;
            }
            var path = txtBakupPath.Text.Trim();
            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("备份路径不能为空");
                return;
            }

            var isHasData = cbIsHasDate.Checked;
            var dbNameTemp = ".bak";
            if (isHasData)
            {
                dbNameTemp = "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".bak";
            }

            var bakSql = "BACKUP DATABASE [{0}] TO DISK = '{1}'";
            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                var node = treeView.Nodes[i];
                if (node.Checked)
                {
                    try
                    {
                        var dbName = node.Text;
                        var sql = string.Format(bakSql, node.Text, path + "\\" + node.Text + dbNameTemp);
                        DoBackup(dbName, sql);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }

            lblMessage.Text = "备份操作完成";
            Application.DoEvents();
        }

        /// <summary>
        /// 还原
        /// </summary>
        private void btnRollBack_Click(object sender, EventArgs e)
        {
            if (!isLogin)
            {
                MessageBox.Show("请先登录！");
                return;
            }
            var bakPath = txtBakupPath.Text.Trim();
            var rollBackPath = txtRollBackPath.Text.Trim();
            if (string.IsNullOrWhiteSpace(bakPath))
            {
                MessageBox.Show("备份路径不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(rollBackPath))
            {
                MessageBox.Show("还原路径不能为空");
                return;
            }

            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                var node = treeView.Nodes[i];
                if (node.Checked)
                {
                    var bakFileName = getBakFileName(node.Text, bakPath);
                    if (!string.IsNullOrWhiteSpace(bakFileName))
                    {
                        //获取主逻辑名称
                        var sqlRestore = "RESTORE filelistonly from disk='{0}'";
                        var dt = sqlHelper.ExecuteDataTable(string.Format(sqlRestore, bakFileName));
                        var logicalName = "";
                        var logicalLogName = "";
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row["Type"].ToString() == "D")
                                {
                                    logicalName = row["LogicalName"].ToString();
                                }
                                else if (row["Type"].ToString() == "L")
                                {
                                    logicalLogName = row["LogicalName"].ToString();
                                }
                            }
                        }

                        var sql = string.Format(rollBackSqlTemp, node.Text, rollBackPath, bakFileName, logicalName, logicalLogName);
                        try
                        {
                            var dbName = node.Text;
                            DoRollback(dbName, sql);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message);
                        }
                    }
                }
            }

            lblMessage.Text = "还原操作完成";
            Application.DoEvents();
        }

        /// <summary>
        /// 创建并还原
        /// </summary>
        private void btnCreateAndRollback_Click(object sender, EventArgs e)
        {
            if (!isLogin)
            {
                MessageBox.Show("请先登录！");
                return;
            }
            var bakPath = txtBakupPath.Text.Trim();
            var rollBackPath = txtRollBackPath.Text.Trim();
            if (string.IsNullOrWhiteSpace(bakPath))
            {
                MessageBox.Show("备份路径不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(rollBackPath))
            {
                MessageBox.Show("还原路径不能为空");
                return;
            }

            var files = getBakFileNames(bakPath);
            foreach (string file in files)
            {
                //获取主逻辑名称
                var sqlRestore = "RESTORE filelistonly from disk='{0}'";
                var dt = sqlHelper.ExecuteDataTable(string.Format(sqlRestore, file));
                var logicalName = "";
                var logicalLogName = "";
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Type"].ToString() == "D")
                        {
                            logicalName = row["LogicalName"].ToString();
                        }
                        else if (row["Type"].ToString() == "L")
                        {
                            logicalLogName = row["LogicalName"].ToString();
                        }
                    }
                }

                var dbName = file.Substring(file.LastIndexOf("\\") + 1, file.LastIndexOf(".") - file.LastIndexOf("\\") - 1); //文件名
                var sql = string.Format(rollBackSqlTemp, dbName, rollBackPath, file, logicalName, logicalLogName);
                try
                {
                    DoRollback(dbName, sql);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }

            lblMessage.Text = "还原操作完成";
            Application.DoEvents();
        }

        private string selectedBase;
        private string selectedFileStore;
        private string fsRootFolderId;
        private string fsServerId;
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode.Text.Contains("FileStore"))
            {
                selectedFileStore = treeView.SelectedNode.Text;
                lblSelectedFileStore.Text = "已选中：" + selectedFileStore;
                var sql = "SELECT a.Id AS FsRootFolderId,a.RootFolderPath,b.Id AS FsServerId,b.HttpUrl FROM {0}.dbo.FsRootFolder a,{0}.dbo.FsServer b";
                var dt = sqlHelper.ExecuteDataTable(string.Format(sql, selectedFileStore));
                if (dt.Rows.Count > 0)
                {
                    fsRootFolderId = dt.Rows[0]["FsRootFolderId"].ToString();
                    fsServerId = dt.Rows[0]["FsServerId"].ToString();
                    var uploadPath = dt.Rows[0]["RootFolderPath"].ToString();
                    var fileServices = dt.Rows[0]["HttpUrl"].ToString();

                    txtUploadPath.Text = uploadPath;
                    txtFileServices.Text = fileServices;
                }
            }
            else if (treeView.SelectedNode.Text.Contains("Base"))
            {
                selectedBase = treeView.SelectedNode.Text;
                lblSelectedBase.Text = "已选中：" + selectedBase;
            }
            else
            {
                selectedBase = null;
                selectedFileStore = null;

                lblSelectedBase.Text = "";
                lblSelectedFileStore.Text = "";
                txtUploadPath.Text = "";
                txtFileServices.Text = "";
            }
        }

        #region 进度条

        /// <summary>
        /// 当前数据库是否还原完成
        /// </summary>
        private bool isComplete;

        /// <summary>
        /// 备份委托
        /// </summary>
        public delegate string BackupHandler(string sql);

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="dbName">要备份的数据库名称</param>
        /// <param name="sql">备份脚本</param>
        private void DoBackup(string dbName, string sql)
        {
            //初始化进度条
            initProgressBar("正在备份数据库：" + dbName + "...");

            //异步进行执行备份语句
            RollbackHandler handler = DoExecSql;
            handler.BeginInvoke(sql, setCompleteStatus, null);

            //监测备份状态
            do
            {
                //设置进度条
                setProgressBar(sqlPercentForBackup);

                // 等待，用于UI刷新界面，很重要  
                System.Threading.Thread.Sleep(100);
            } while (!isComplete);

            //重置进度条
            resetProgressBar(dbName + "备份成功");
        }

        /// <summary>
        /// 还原委托
        /// </summary>
        public delegate string RollbackHandler(string sql);

        /// <summary>
        /// 还原数据库
        /// </summary>
        /// <param name="dbName">要还原的数据库名称</param>
        /// <param name="sql">还原脚本</param>
        private void DoRollback(string dbName, string sql)
        {
            //初始化进度条
            initProgressBar("正在还原数据库：" + dbName + "...");

            //异步进行还原
            RollbackHandler handler = DoExecSql;
            handler.BeginInvoke(sql, setCompleteStatus, null);

            //监测还原状态
            do
            {
                //设置进度条
                setProgressBar(sqlPercentForRollback);

                // 等待，用于UI刷新界面，很重要  
                System.Threading.Thread.Sleep(100);
            } while (!isComplete);

            //重置进度条
            resetProgressBar(dbName + "还原成功");
        }

        /// <summary>
        /// 初始化进度条
        /// </summary>
        private void initProgressBar(string msg)
        {
            //显示进度条
            progressBar.Visible = true;
            lblPercent.Text = "0%";
            lblPercent.Visible = true;

            lblMessage.Text = msg;
            Application.DoEvents();
        }

        /// <summary>
        /// 设置进度条
        /// </summary>
        private void setProgressBar(string sqlPercent)
        {
            SqlCompletePercent cp = getCompletePercent(sqlPercent);
            if (cp != null)
            {
                int percent = cp.Complete_Percent;
                progressBar.Value = percent;
                lblPercent.Text = percent + "%";
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 重置进度条
        /// </summary>
        private void resetProgressBar(string msg)
        {
            isComplete = false;

            progressBar.Value = 0;
            progressBar.Visible = false;
            lblPercent.Text = "";
            lblPercent.Visible = false;

            lblMessage.Text = msg;
            Application.DoEvents();
        }

        /// <summary>
        /// 异步回调函数，设置数据库还原完成状态
        /// </summary>
        private void setCompleteStatus(IAsyncResult asyncResult)
        {
            isComplete = true;
        }

        /// <summary>
        /// 执行还原脚本方法
        /// </summary>
        /// <param name="sql">还原脚本</param>
        public string DoExecSql(string sql)
        {
            string res = "0";
            try
            {
                res = sqlHelper.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return res;
        }

        /// <summary>
        /// 获取还原完成百分比
        /// </summary>
        private SqlCompletePercent getCompletePercent(string sqlPercent)
        {
            return sqlHelper.ExecuteObject<SqlCompletePercent>(sqlPercent);
        }

        #endregion

        #region 上海金慧专用功能

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (!isLogin)
            {
                MessageBox.Show("请先登录！");
                return;
            }

            if (string.IsNullOrWhiteSpace(selectedBase))
            {
                MessageBox.Show("请选择要重置密码的数据库，如XXXX_Base", "系统提示");
                return;
            }

            var newPassword = txtPasswordText.Text.Trim();
            var sqlUpdateTmpl = "UPDATE {0}.dbo.S_A_User SET Password='{2}' WHERE ID='{1}'\r\n";

            var sb = new StringBuilder();
            var sql = string.Format("SELECT ID,Code,Password FROM {0}.dbo.S_A_User", selectedBase);
            var dt = sqlHelper.ExecuteDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                var id = row["ID"];
                var code = row["Code"].ToString();
                var newPass = StringHelper.GetSHA1Hash(code + newPassword);
                sb.Append(string.Format(sqlUpdateTmpl, selectedBase, id, newPass));
            }

            var sqlStr = sb.ToString();
            if (!string.IsNullOrWhiteSpace(sqlStr))
            {
                try
                {
                    sqlHelper.ExecuteNonQuery(sqlStr);
                    MessageBox.Show("修改成功", "系统提示");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void btnSetUploadPath_Click(object sender, EventArgs e)
        {
            if (!isLogin)
            {
                MessageBox.Show("请先登录！");
                return;
            }

            if (string.IsNullOrWhiteSpace(selectedFileStore))
            {
                MessageBox.Show("请选择相关数据库，如XXXX_FileStore", "系统提示");
                return;
            }

            var uploadPath = txtUploadPath.Text.Trim();
            if (string.IsNullOrWhiteSpace(uploadPath))
            {
                MessageBox.Show("上传路径不能为空", "系统提示");
                return;
            }

            if (!string.IsNullOrWhiteSpace(selectedFileStore) && !string.IsNullOrWhiteSpace(fsRootFolderId))
            {
                var sql = "UPDATE [{0}].dbo.FsRootFolder SET RootFolderPath='{2}' WHERE Id='{1}'";
                try
                {
                    sqlHelper.ExecuteNonQuery(string.Format(sql, selectedFileStore, fsRootFolderId, uploadPath));
                    MessageBox.Show("修改成功", "系统提示");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void btnSetFileStore_Click(object sender, EventArgs e)
        {
            if (!isLogin)
            {
                MessageBox.Show("请先登录！");
                return;
            }

            if (string.IsNullOrWhiteSpace(selectedFileStore))
            {
                MessageBox.Show("请选择相关数据库，如XXXX_FileStore", "系统提示");
                return;
            }

            var fileServices = txtFileServices.Text.Trim();
            if (string.IsNullOrWhiteSpace(fileServices))
            {
                MessageBox.Show("服务地址不能为空", "系统提示");
                return;
            }

            if (!string.IsNullOrWhiteSpace(selectedFileStore) && !string.IsNullOrWhiteSpace(fsServerId))
            {
                var sql = "UPDATE [{0}].dbo.FsServer SET HttpUrl='{2}' WHERE Id='{1}'";
                try
                {
                    sqlHelper.ExecuteNonQuery(string.Format(sql, selectedFileStore, fsServerId, fileServices));
                    MessageBox.Show("修改成功", "系统提示");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        #endregion

        #region  私有方法

        private void getDbNodes()
        {
            treeView.Nodes.Clear();
            const string sql = "SELECT * FROM sysdatabases ORDER BY name";
            var dt = sqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i];
                    var node = new TreeNode();
                    node.Text = row["name"].ToString();
                    treeView.Nodes.Add(node);
                }
            }
        }

        /// <summary>
        /// 获取指定目录下所有数据库备份文件目录
        /// </summary>
        /// <param name="bakPath">指定的数据库备份文件存放目录</param>
        /// <returns></returns>
        private List<string> getBakFileNames(string bakPath)
        {
            var files = Directory.GetFiles(bakPath, "*.bak", SearchOption.AllDirectories).ToList();
            return files;
        }

        /// <summary>
        /// 根据数据库名称获取备份文件名称
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="bakPath">数据库备份文件目录</param>
        /// <returns></returns>
        private string getBakFileName(string dbName, string bakPath)
        {
            var files = Directory.GetFiles(bakPath, dbName + "*.bak", SearchOption.AllDirectories).ToList();
            if (files.Any())
            {
                return files[0];
            }
            return "";
        }

        #endregion

        #region 脚本模板

        /// <summary>
        /// 数据库还原语句模板
        /// </summary>
        private string rollBackSqlTemp = @"USE master

--关闭数据库的引用
IF EXISTS ( SELECT  name
            FROM    master.dbo.sysdatabases
            WHERE   name = N'{0}' )
    ALTER DATABASE [{0}] SET OFFLINE WITH ROLLBACK IMMEDIATE;

--如果要创建的数据库已经存在，那么删除它
IF EXISTS ( SELECT  name
            FROM    master.dbo.sysdatabases
            WHERE   name = N'{0}' )
    DROP DATABASE [{0}]

--创建一个新数据库，要指定新建数据库的数据文件和日志文件的名称和位置，初始化大小增长幅度，最大值等内容

CREATE DATABASE [{0}] ON 

( NAME = N'{0}_Data',

   FILENAME = N'{1}\{0}_Data.mdf',

   SIZE = 5MB,

   MAXSIZE = 50MB,

   FILEGROWTH = 5MB ) LOG ON

( NAME = N'{0}_Log',

   FILENAME = N'{1}\{0}_Log.ldf',

   SIZE = 5MB,

   MAXSIZE = 25MB,

   FILEGROWTH = 5MB )

--把指定的数据库备份文件恢复到刚刚建立的数据库里，这里要指定数据库备份文件的位置

--以及要恢复到的数据库，因为备份文件来自未知的机器，备份的时候原数据库和新数据库

--的数据文件和日志文件的位置不匹配，所以要用with move指令来完成强制文件移动，如果

--是通过管理器备份的数据库文件，数据库文件和日志文件名分别是数据库名跟上_Data或

--_Log，这是一个假设哦，如果不是这样，脚本有可能会出错

RESTORE DATABASE [{0}]

   FROM DISK = '{2}'

WITH  FILE = 1,

      MOVE '{3}' TO '{1}\{0}.mdf', 

      MOVE '{4}' TO '{1}\{0}.ldf',

      NOUNLOAD, 

      REPLACE, 

      STATS = 10

--打开数据库的引用
ALTER DATABASE [{0}] set ONLINE;
";

        /// <summary>
        /// 数据库备份进度查询语句
        /// </summary>
        private string sqlPercentForBackup = @"SELECT DB_NAME(er.[database_id]) [DatabaseName],er.[session_id] AS [SessionID],er.[command] AS [CommandType]
,est.[text] [StatementText],er.[status] AS [Status],CONVERT(int, er.[percent_complete]) AS [Complete_Percent],
CONVERT(DECIMAL(38, 2), er.[total_elapsed_time] / 60000.00) AS [ElapsedTime_m],CONVERT(DECIMAL(38, 2),
 er.[estimated_completion_time] / 60000.00) AS [EstimatedCompletionTime_m],er.[last_wait_type] [LastWait],
 er.[wait_resource] [CurrentWait]
 FROM sys.dm_exec_requests AS er 
 INNER JOIN sys.dm_exec_sessions AS es ON er.[session_id] = es.[session_id]
 CROSS APPLY sys.dm_exec_sql_text(er.[sql_handle]) est
 WHERE er.[command] = 'BACKUP DATABASE'";

        /// <summary>
        /// 数据库还原进度查询语句
        /// </summary>
        private string sqlPercentForRollback = @"SELECT DB_NAME(er.[database_id]) [DatabaseName],er.[session_id] AS [SessionID],er.[command] AS [CommandType]
,est.[text] [StatementText],er.[status] AS [Status],CONVERT(int, er.[percent_complete]) AS [Complete_Percent],
CONVERT(DECIMAL(38, 2), er.[total_elapsed_time] / 60000.00) AS [ElapsedTime_m],CONVERT(DECIMAL(38, 2),
 er.[estimated_completion_time] / 60000.00) AS [EstimatedCompletionTime_m],er.[last_wait_type] [LastWait],
 er.[wait_resource] [CurrentWait]
 FROM sys.dm_exec_requests AS er 
 INNER JOIN sys.dm_exec_sessions AS es ON er.[session_id] = es.[session_id]
 CROSS APPLY sys.dm_exec_sql_text(er.[sql_handle]) est
 WHERE er.[command] = 'RESTORE DATABASE'";

        #endregion
    }
}
