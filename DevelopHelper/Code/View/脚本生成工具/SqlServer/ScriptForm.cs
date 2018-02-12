using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common;
using DbHelper;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace View.SqlServerScript
{
    public partial class ScriptForm : Form
    {
        private readonly string[] numberTypes = { "decimal", "int", "float", "smallint", "bigint", "bit", "money", "numeric", "smallmoney", "tinyint" };
        private readonly Form mainForm;
        public ScriptForm(Form mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();

            生成清库语句ToolStripMenuItem.Enabled = false;
        }

        public SqlConnection CurConn;

        private string whereString;
        private string statusString;
        private ConDbForm conDbForm;

        private void ScriptForm_Load(object sender, EventArgs e)
        {
            Hashtable sections = (Hashtable)ConfigurationManager.GetSection("SqlServer");
            if (sections != null)
            {
                foreach (var item in sections.Keys)
                {
                    AddContextMenu(item.ToString(), 使用过的连接ToolStripMenuItem.DropDownItems, MenuClicked);
                }
            }
        }

        void AddContextMenu(string text, ToolStripItemCollection cms, EventHandler callback)
        {
            if (text == "-")
            {
                ToolStripSeparator tsp = new ToolStripSeparator();
                cms.Add(tsp);
                return;
            }

            if (!string.IsNullOrEmpty(text))
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(text);
                tsmi.Tag = text + "TAG";
                if (callback != null) tsmi.Click += callback;
                cms.Add(tsmi);
            }
        }

        void MenuClicked(object sender, EventArgs e)
        {
            var stripMenuItem = sender as ToolStripMenuItem;
            if (stripMenuItem != null)
            {
                Hashtable sections = (Hashtable)ConfigurationManager.GetSection("SqlServer");
                CurConn = new SqlConnection(sections[stripMenuItem.Text].ToString());
                BindTreeView();
                //MessageBox.Show("connection success!");
            }
        }

        private void 连接ToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void 刷新ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtWhereStr.Text))
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
                if (treeView.SelectedNode.Level == 2)
                {
                    statusString = "正在查询...";
                    SetParentInfo.SetStatusString(mainForm, statusString);

                    string dbName = treeView.SelectedNode.Parent.Text;
                    string tableName = treeView.SelectedNode.Text;
                    txtScript.Text = getInsertScript(dbName, tableName);

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
            var sql = sb.ToString();
            if (String.IsNullOrWhiteSpace(sql))
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

        /// <summary>
        /// 选中事件
        /// </summary>
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtWhereStr.Text = "";
            whereString = "";
            if (treeView.SelectedNode != null && treeView.SelectedNode.Level == 2)
            {
                生成清库语句ToolStripMenuItem.Enabled = false;
                try
                {
                    txtScript.Clear();
                    statusString = "正在查询...";
                    SetParentInfo.SetStatusString(mainForm, statusString);

                    string dbName = treeView.SelectedNode.Parent.Text;
                    string tableName = treeView.SelectedNode.Text;

                    txtScript.Text = getInsertScript(dbName, tableName);

                    SetParentInfo.SetStatusString(mainForm, statusString);
                }
                finally
                {
                    GC.Collect();
                }
            }
            else if (treeView.SelectedNode != null && treeView.SelectedNode.Level == 1)
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
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
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

            var exist = false;
            foreach (TreeNode node in treeView.Nodes)
            {
                if (node.Text == CurConn.DataSource)
                    exist = true;
            }

            if (!exist)
            {
                //添加结点
                TreeNode root = new TreeNode();
                root.Text = CurConn.DataSource;
                //一级
                var dbNodes = getDbNodes();

                //多线程提高效率
                Parallel.For(0, dbNodes.Count, i =>
                {
                    var dbNode = dbNodes[i];
                    //二级
                    var tableNodes = getTableNodes(dbNode.Text);
                    dbNode.Nodes.AddRange(tableNodes.ToArray());
                });
                root.Nodes.AddRange(dbNodes.ToArray());
                root.Expand();
                treeView.Nodes.Add(root);
            }
        }

        private List<TreeNode> getDbNodes()
        {
            List<TreeNode> nodes = new List<TreeNode>();
            const string sql = "SELECT name FROM sysdatabases ORDER BY name";
            SqlHelper sqlHelper = new SqlServerHelper(CurConn.ConnectionString);
            try
            {
                var dt = sqlHelper.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        TreeNode node = new TreeNode();
                        node.Text = dataRow["name"].ToString();
                        nodes.Add(node);
                    }
                }
            }
            catch
            {

            }

            return nodes;
        }

        private List<TreeNode> getTableNodes(string dbName)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            const string sql = "SELECT name FROM sysobjects WHERE xtype = 'U' ORDER BY name";
            SqlHelper sqlHelper = new SqlServerHelper(CurConn.ConnectionString, dbName);
            try
            {
                var dt = sqlHelper.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        TreeNode node = new TreeNode();
                        node.Text = dataRow["name"].ToString();
                        nodes.Add(node);
                    }
                }
            }
            catch
            {

            }

            return nodes;
        }

        private string getInsertTemplate(List<TableStruct> tableStructs, string tableName)
        {
            string insertTmpl = "Insert Into [{0}] ({1}) Values ({2})";

            var columns = tableStructs.Select(i => i.coumnname).ToArray();
            string columnsTmpl = "";
            for (int i = 0; i < columns.Length; i++)
            {
                columnsTmpl += "[" + columns[i] + "],";
            }
            columnsTmpl = columnsTmpl.Substring(0, columnsTmpl.Length - 1);

            insertTmpl = String.Format(insertTmpl, tableName, columnsTmpl, "{0}");

            return insertTmpl;
        }

        /// <summary>
        /// 获取脚本
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tableName">表名</param>
        private string getInsertScript(string dbName, string tableName)
        {
            StringBuilder scriptText = new StringBuilder();
            SqlHelper sqlHelper = new SqlServerHelper(CurConn.ConnectionString, dbName);
            string sql = String.Format("SELECT syscolumns.name AS coumnname ,systypes.name AS type,syscolumns.isnullable,syscolumns.length FROM syscolumns,systypes WHERE syscolumns.xusertype = systypes.xusertype AND syscolumns.id = OBJECT_ID('{0}') AND COLUMNPROPERTY( OBJECT_ID('[{0}]'),syscolumns.name,'IsIdentity') <> 1", tableName);
            List<TableStruct> tableStructs = new List<TableStruct>();
            var dt = sqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                HiPerfTimer hiperTimer = new HiPerfTimer();
                hiperTimer.Start();

                foreach (DataRow dataRow in dt.Rows)
                {
                    var tableStruct = new TableStruct();
                    tableStruct.coumnname = dataRow["coumnname"].ToString();
                    tableStruct.type = dataRow["type"].ToString();
                    tableStruct.isnullable = Convert.ToInt32(dataRow["isnullable"]);
                    tableStruct.length = Convert.ToInt16(dataRow["length"]);

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

                sql = String.Format("SELECT TOP 3000 * FROM {0} {1}", tableName, whereString);
                var dtSource = sqlHelper.ExecuteDataTable(sql);
                if (dtSource.Rows.Count > 0)
                {
                    //单线程，用于测试
                    //for (int i = 0; i < dtSource.Rows.Count; i++)
                    //{
                    //    var dataRow = dtSource.Rows[i];
                    //    var values = "";
                    //    foreach (TableStruct tableStruct in tableStructs)
                    //    {
                    //        if (numberTypes.Contains(tableStruct.type))     //数值类型处理
                    //        {
                    //            if (DBNull.Value != dataRow[tableStruct.coumnname] &&
                    //                dataRow[tableStruct.coumnname].ToString() != "")
                    //                values += "'" + dataRow[tableStruct.coumnname] + "',";
                    //            else
                    //            {
                    //                if (tableStruct.isnullable == 0)
                    //                {
                    //                    values += "'0',";
                    //                }
                    //                else
                    //                {
                    //                    values += "null,";
                    //                }
                    //            }
                    //        }
                    //        else if (tableStruct.type.ToUpper() == "UNIQUEIDENTIFIER")      //UNIQUEIDENTIFIER类型处理
                    //        {
                    //            var valueStr = dataRow[tableStruct.coumnname].ToString();
                    //            if (string.IsNullOrWhiteSpace(valueStr))
                    //            {
                    //                values += "CAST(NULL AS UNIQUEIDENTIFIER),";
                    //            }
                    //            else
                    //            {
                    //                values += string.Format("CAST('{0}' AS UNIQUEIDENTIFIER),", dataRow[tableStruct.coumnname].ToString());
                    //            }
                    //        }
                    //        else if (tableStruct.type.ToUpper() == "TIMESTAMP")
                    //        {
                    //            values += "DEFAULT,";
                    //        }
                    //        else
                    //        {
                    //            values += "'" + dataRow[tableStruct.coumnname].ToString().Replace("'", "''") + "',";
                    //        }
                    //    }
                    //    values = values.Substring(0, values.Length - 1);

                    //    scriptText.Append(String.Format(insertTmplete, values) + "\r\n");
                    //}

                    //多线程
                    object lockObj = new object();
                    Parallel.For(0, dtSource.Rows.Count, i =>
                    {
                        var dataRow = dtSource.Rows[i];
                        var values = "";
                        foreach (TableStruct tableStruct in tableStructs)
                        {
                            if (numberTypes.Contains(tableStruct.type))     //数值类型处理
                            {
                                if (DBNull.Value != dataRow[tableStruct.coumnname] &&
                                    dataRow[tableStruct.coumnname].ToString() != "")
                                    values += "'" + dataRow[tableStruct.coumnname] + "',";
                                else
                                {
                                    if (tableStruct.isnullable == 0)
                                    {
                                        values += "'0',";
                                    }
                                    else
                                    {
                                        values += "null,";
                                    }
                                }
                            }
                            else if (tableStruct.type.ToUpper() == "UNIQUEIDENTIFIER")      //UNIQUEIDENTIFIER类型处理
                            {
                                var valueStr = dataRow[tableStruct.coumnname].ToString();
                                if (string.IsNullOrWhiteSpace(valueStr))
                                {
                                    values += "CAST(NULL AS UNIQUEIDENTIFIER),";
                                }
                                else
                                {
                                    values += string.Format("CAST('{0}' AS UNIQUEIDENTIFIER),", dataRow[tableStruct.coumnname].ToString());
                                }
                            }
                            else if (tableStruct.type.ToUpper() == "TIMESTAMP")     //TIMESTAMP类型处理，不能显示的插入值
                            {
                                values += "DEFAULT,";
                            }
                            else
                            {
                                values += "'" + dataRow[tableStruct.coumnname].ToString().Replace("'", "''") + "',";
                            }
                        }
                        values = values.Substring(0, values.Length - 1);

                        lock (lockObj)
                        {
                            scriptText.Append(String.Format(insertTmplete, values) + "\r\n");
                        }
                    });
                }

                hiperTimer.Stop();

                statusString = "共计" + dtSourceCount + "条数据，取数" + dtSource.Rows.Count + "条用时：" + hiperTimer.Duration + "秒";
            }

            return scriptText.ToString();
        }
    }
}
