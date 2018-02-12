using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using ConfigHelper;
using DbHelper;

namespace View.OracleScript
{
    public partial class ScriptForm : Form
    {
        private readonly string[] dataTypes = { "TIMESTAMP(3)", "DATE" };
        private readonly string[] lobTypes = { "BLOB", "CLOB", "NCLOB", "LONG" };
        private readonly string[] numberTypes = { "NUMBER", "BINARY_DOUBLE", "BINARY_FLOAT" };
        private readonly Form mainForm;
        public ScriptForm(Form mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        public OracleConnection Conn;

        private string whereString;
        private string statusString;
        private ConDbForm conDbForm;

        private void 连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conDbForm = ConDbForm.GetInstance(this);
            conDbForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            conDbForm.TopLevel = true;
            conDbForm.ControlBox = true;
            conDbForm.Dock = DockStyle.Fill;
            conDbForm.StartPosition = FormStartPosition.Manual;
            var left = Parent.Parent.Width / 2 + Parent.Parent.Left - conDbForm.Width / 2;
            var right = Parent.Parent.Height / 2 + Parent.Parent.Top - conDbForm.Height / 2;
            conDbForm.Location = new Point(left, right);
            conDbForm.ShowDialog();
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtWhereStr.Text))
            {
                whereString = "Where " + txtWhereStr.Text.Trim();
            }
            else
            {
                whereString = "";
            }

            txtScript.Text = "";

            if (treeView.SelectedNode != null)
            {
                if (treeView.SelectedNode.Level == 1)
                {
                    statusString = "正在查询...";
                    SetParentInfo.SetStatusString(mainForm, statusString);

                    string userName = treeView.SelectedNode.Parent.Text;
                    string tableName = treeView.SelectedNode.Text;
                    txtScript.Text = getInsertScript(userName,tableName);

                    SetParentInfo.SetStatusString(mainForm, statusString);
                }
            }
            else
            {
                MessageBox.Show("未连接数据库");
            }
        }

        private void 生成清库语句ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var deleteTepl = "DELETE {0};\r\n";
            var sb = new StringBuilder();
            foreach (TreeNode node in treeView.SelectedNode.Nodes)
            {
                sb.Append(string.Format(deleteTepl, node.Text));
            }
            sb.Append("COMMIT;");
            var sql = sb.ToString();
            if (string.IsNullOrWhiteSpace(sql))
            {
                MessageBox.Show("此数据库没有相关数据表");
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                FileName = treeView.SelectedNode.Text + "清库语句.sql",
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
                sw.WriteLine(sql);
                sw.Flush();
                sw.Close();
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtWhereStr.Text = "";
            whereString = "";
            if (treeView.SelectedNode != null && treeView.SelectedNode.Level == 1)
            {
                生成清库语句ToolStripMenuItem.Enabled = false;
                txtScript.Text = "";
                statusString = "正在查询...";
                SetParentInfo.SetStatusString(mainForm, statusString);

                string userName = treeView.SelectedNode.Parent.Text;
                string tableName = treeView.SelectedNode.Text;

                txtScript.Text = getInsertScript(userName,tableName);

                SetParentInfo.SetStatusString(mainForm, statusString);
            }
            else if (treeView.SelectedNode != null && treeView.SelectedNode.Level == 0)
            {
                生成清库语句ToolStripMenuItem.Enabled = true;
            }
            else
            {
                生成清库语句ToolStripMenuItem.Enabled = false;
            }
        }

        private void treeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(51, 153, 255)), e.Node.Bounds);

                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.White, Rectangle.Inflate(e.Bounds, 2, 0));
            }
            else
            {
                e.DrawDefault = true;
            }

            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(Color.FromArgb(51, 153, 255)))
                {
                    focusPen.DashStyle = DashStyle.Dot;
                    Rectangle focusBounds = e.Node.Bounds;
                    focusBounds.Size = new Size(focusBounds.Width - 1,
                    focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        private void txtScript_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        public void BindTreeView()
        {
            treeView.LabelEdit = false;//不可编辑

            bool exist = false;
            foreach (TreeNode node in treeView.Nodes)
            {
                if (node.Text == Conn.ConnectionString.Split(';')[1].Split('=')[1].ToUpper())
                    exist = true;
            }

            if (!exist)
            {
                //添加结点
                TreeNode root = new TreeNode();
                root.Text = Conn.ConnectionString.Split(';')[1].Split('=')[1].ToUpper();
                //一级
                var dbNodes = getDbNodes();

                root.Nodes.AddRange(dbNodes.ToArray());
                treeView.Nodes.Add(root);
                treeView.ExpandAll();
            }
        }

        #region 私有方法

        private List<TreeNode> getDbNodes()
        {
            List<TreeNode> nodes = new List<TreeNode>();
            const string sql = "SELECT TABLE_NAME AS NAME FROM USER_TABLES ORDER BY TABLE_NAME";
            SqlHelper sqlHelper = new OracleHelper(ConfigParse.GetConnectionString(Conn));
            var dt = sqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    node.Text = dataRow["NAME"].ToString();
                    nodes.Add(node);
                }
            }
            return nodes;
        }

        private string getInsertTemplate(List<TableStruct> tableStructs, string tableName)
        {
            string insertTmpl = "INSERT INTO {0} ({1}) VALUES ({2});";

            var columns = tableStructs.Select(i => i.coumnname).ToArray();
            string columnsTmpl = "";
            for (int i = 0; i < columns.Length; i++)
            {
                columnsTmpl += "\"" + columns[i] + "\",";
            }
            columnsTmpl = columnsTmpl.Substring(0, columnsTmpl.Length - 1);

            insertTmpl = string.Format(insertTmpl, tableName, columnsTmpl, "{0}");

            return insertTmpl;
        }

        /// <summary>
        /// 获取脚本
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="tableName">表名</param>
        private string getInsertScript(string userName,string tableName)
        {
            statusString = "共计0条数据";
            StringBuilder scriptText = new StringBuilder();
            SqlHelper sqlHelper = new OracleHelper(Conn.ConnectionString, userName);
            string sql = string.Format("SELECT COLUMN_NAME AS COUMNNAME,DATA_TYPE AS TYPE,(CASE WHEN NULLABLE='Y' THEN 1 ELSE 0 END) AS ISN,DATA_LENGTH AS LENGTH FROM USER_TAB_COLUMNS WHERE TABLE_NAME='{0}' ORDER BY COLUMN_ID", tableName);
            List<TableStruct> tableStructs = new List<TableStruct>();
            var dt = sqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    var tableStruct = new TableStruct();
                    tableStruct.coumnname = dataRow["COUMNNAME"].ToString();
                    tableStruct.type = dataRow["TYPE"].ToString();
                    tableStruct.isnullable = Convert.ToInt32(dataRow["ISN"]);
                    tableStruct.length = Convert.ToInt16(dataRow["LENGTH"]);

                    tableStructs.Add(tableStruct);
                }

                var insertTmplete = getInsertTemplate(tableStructs, tableName);

                sql = String.Format("SELECT COUNT(1) AS DtSourceCount FROM {0} {1}", tableName, whereString);
                //数据行数，显示最多3000行，太多会内存溢出
                int dtSourceCount = 0;
                var dtCount = sqlHelper.ExecuteDataTable(sql);
                if (dtCount.Rows.Count > 0)
                {
                    int.TryParse(dtCount.Rows[0][0].ToString(), out dtSourceCount);
                }

                sql = string.Format("SELECT * FROM {0} {1}", tableName, whereString);
                var dtSource = sqlHelper.ExecuteDataTable(sql);
                if (dtSource.Rows.Count > 0)
                {
                    //如果有clob类型字段，则申明变量
                    var clobStructs =
                        tableStructs.Where(i => lobTypes.Contains(i.type)).ToList();
                    if (clobStructs.Count > 0)
                    {
                        foreach (TableStruct clobStruct in clobStructs)
                        {
                            scriptText.Append("DECLARE V_" + clobStruct.coumnname + " NCLOB;\r\n");
                        }
                    }

                    bool hasBegin = false;
                    if (scriptText.Length > 0)
                    {
                        scriptText.Append("BEGIN\r\n");
                        hasBegin = true;
                    }

                    HiPerfTimer hiperTimer = new HiPerfTimer();
                    hiperTimer.Start();

                    //object lockObj = new object();
                    //Parallel.For(0, dtSource.Rows.Count, i =>
                    //{
                    //    var dataRow = dtSource.Rows[i];
                    //    var values = "";
                    //    foreach (TableStruct tableStruct in tableStructs)
                    //    {
                    //        if (dataTypes.Contains(tableStruct.type))
                    //        {
                    //            values += "TO_DATE('" + dataRow[tableStruct.coumnname] + "', 'yyyy-mm-dd hh24:mi:ss'),";
                    //        }
                    //        else if (lobTypes.Contains(tableStruct.type))
                    //        {
                    //            var varName = "V_" + tableStruct.coumnname;
                    //            values += varName + ",";
                    //            var varValue = "'" + dataRow[tableStruct.coumnname] + "'";

                    //            lock (lockObj)
                    //            {
                    //                scriptText.Append(varName + ":=" + varValue + ";\r\n");
                    //            }
                    //        }
                    //        else if (numberTypes.Contains(tableStruct.type))
                    //        {
                    //            if (DBNull.Value != dataRow[tableStruct.coumnname] &&
                    //                dataRow[tableStruct.coumnname].ToString() != "")
                    //            {
                    //                values += "'" + dataRow[tableStruct.coumnname] + "',";
                    //            }
                    //            else
                    //            {
                    //                if (tableStruct.isnullable == 0)
                    //                {
                    //                    values += "0,";
                    //                }
                    //                else
                    //                {
                    //                    values += "NULL,";
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            values += "'" + dataRow[tableStruct.coumnname] + "',";
                    //        }
                    //    }
                    //    values = values.Substring(0, values.Length - 1);

                    //    lock (lockObj)
                    //    {
                    //        scriptText.Append(string.Format(insertTmplete, values) + "\r\n");
                    //    }
                    //});

                    foreach (DataRow dataRow in dtSource.Rows)
                    {
                        var values = "";
                        foreach (TableStruct tableStruct in tableStructs)
                        {
                            if (dataTypes.Contains(tableStruct.type))
                            {
                                values += "TO_DATE('" + dataRow[tableStruct.coumnname] + "', 'yyyy-mm-dd hh24:mi:ss'),";
                            }
                            else if (lobTypes.Contains(tableStruct.type))
                            {
                                var varName = "V_" + tableStruct.coumnname;
                                values += varName + ",";
                                var varValue = "'" + dataRow[tableStruct.coumnname].ToString().Replace("'", "''") + "'";
                                scriptText.Append(varName + ":=" + varValue + ";\r\n");
                            }
                            else if (numberTypes.Contains(tableStruct.type))
                            {
                                if (DBNull.Value != dataRow[tableStruct.coumnname] &&
                                    dataRow[tableStruct.coumnname].ToString() != "")
                                {
                                    values += "'" + dataRow[tableStruct.coumnname] + "',";
                                }
                                else
                                {
                                    if (tableStruct.isnullable == 0)
                                    {
                                        values += "0,";
                                    }
                                    else
                                    {
                                        values += "NULL,";
                                    }
                                }
                            }
                            else
                            {
                                values += "'" + dataRow[tableStruct.coumnname] + "',";
                            }
                        }
                        values = values.Substring(0, values.Length - 1);

                        scriptText.Append(String.Format(insertTmplete, values) + "\r\n");
                    }

                    if (hasBegin)
                    {
                        scriptText.Append("END;");
                    }

                    hiperTimer.Stop();

                    statusString = "共计" + dtSourceCount + "条数据，取数" + dtSource.Rows.Count + "条用时：" + hiperTimer.Duration + "秒";
                }
            }

            return scriptText.ToString();
        }

        #endregion
        AutoSizeFormClass asc = new AutoSizeFormClass();
        private void ScriptForm_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
        }

        private void ScriptForm_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }
    }
}
