using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Windows.Forms;
using ConfigHelper;
using DbHelper;

namespace View.Goodway.Oracle
{
    public partial class FlowDefForm : Form
    {
        private string connectionString1;
        private string connectionString2;
        private DataTable dt1;
        private DataTable dt2;

        public FlowDefForm()
        {
            InitializeComponent();

            init();
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtDataSource1.Text.Trim()))
            {
                MessageBox.Show("服务器名称不能为空");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtUserId1.Text.Trim()))
            {
                MessageBox.Show("用户名不能为空");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtPassword1.Text.Trim()))
            {
                MessageBox.Show("密码不能为空");
                return;
            }

            //测试连接
            connectionString1 = String.Format("DATA SOURCE={0};USER ID={1};PASSWORD={2};", txtDataSource1.Text.Trim().ToUpper(), txtUserId1.Text.Trim().ToUpper(), txtPassword1.Text.Trim());
            SqlHelper sqlHelper = new OracleHelper(connectionString1);
            var flag = sqlHelper.ConnectionTest(connectionString1);
            btnTest1.Text = flag ? "连接成功" : "连接失败";
            if (flag)
            {
                //保存配置
                saveConfig1();
            }
            btnTest1.Enabled = false;
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtDataSource2.Text.Trim()))
            {
                MessageBox.Show("服务器名称不能为空");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtUserId2.Text.Trim()))
            {
                MessageBox.Show("用户名不能为空");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtPassword2.Text.Trim()))
            {
                MessageBox.Show("密码不能为空");
                return;
            }

            //测试连接
            connectionString2 = String.Format("DATA SOURCE={0};USER ID={1};PASSWORD={2};", txtDataSource2.Text.Trim().ToUpper(), txtUserId2.Text.Trim().ToUpper(), txtPassword2.Text.Trim());
            SqlHelper sqlHelper = new OracleHelper(connectionString2);
            var flag = sqlHelper.ConnectionTest(connectionString2);
            btnTest2.Text = flag ? "连接成功" : "连接失败";
            if (flag)
            {
                //保存配置
                saveConfig2();
            }
            btnTest2.Enabled = false;
        }


        private void btnCheck_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            SqlHelper sqlHelper1 = new OracleHelper(connectionString1);
            const string sql = "SELECT ID,CONNNAME,CODE,NAME,MODIFYTIME FROM S_WF_DefFlow ORDER BY CONNNAME,CODE";
            dt1 = sqlHelper1.ExecuteDataTable(sql);

            SqlHelper sqlHelper2 = new OracleHelper(connectionString2);
            dt2 = sqlHelper2.ExecuteDataTable(sql);
            if (dt2.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt2;
                var cellStyle1 = new DataGridViewCellStyle { BackColor = Color.Yellow };
                var cellStyle2 = new DataGridViewCellStyle { BackColor = Color.Green };

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["ID"].ToString() == dataGridView1.Rows[i].Cells["ID"].Value.ToString())
                        {
                            if (dt1.Rows[j]["MODIFYTIME"].ToString() ==
                                dataGridView1.Rows[i].Cells["MODIFYTIME"].Value.ToString())
                            {
                                dataGridView1.Rows[i].DefaultCellStyle = cellStyle2;
                            }
                            else
                            {
                                dataGridView1.Rows[i].DefaultCellStyle = cellStyle1;
                            }
                        }
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (connectionString1 == connectionString2)
            {
                return;
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var list = new List<IdAndCode>();
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    var item = new IdAndCode
                    {
                        ID = dataGridView1.SelectedRows[i].Cells[0].Value.ToString(),
                        Code = dataGridView1.SelectedRows[i].Cells[1].Value.ToString()
                    };
                    list.Add(item);
                }

                Update up = new Update();
                up.UpdateDB(list);

                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    dataGridView1.SelectedRows[i].DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.Green };
                }

                MessageBox.Show("成功更新： " + dataGridView1.SelectedRows.Count + " 条流程！");
            }
        }

        private void txtDataSource1_TextChanged(object sender, EventArgs e)
        {
            btnTest1.Text = "测试连接";
            btnTest1.Enabled = true;
        }

        private void txtUserId1_TextChanged(object sender, EventArgs e)
        {
            btnTest1.Text = "测试连接";
            btnTest1.Enabled = true;
        }

        private void txtPassword1_TextChanged(object sender, EventArgs e)
        {
            btnTest1.Text = "测试连接";
            btnTest1.Enabled = true;
        }

        private void txtDataSource2_TextChanged(object sender, EventArgs e)
        {
            btnTest2.Text = "测试连接";
            btnTest2.Enabled = true;
        }

        private void txtUserId2_TextChanged(object sender, EventArgs e)
        {
            btnTest2.Text = "测试连接";
            btnTest2.Enabled = true;
        }

        private void txtPassword2_TextChanged(object sender, EventArgs e)
        {
            btnTest2.Text = "测试连接";
            btnTest2.Enabled = true;
        }

        #region 私有方法

        /// <summary>
        /// 初始化界面
        /// </summary>
        private void init()
        {
            connectionString1 = Config.WorkflowObject.ConnectionString;
            connectionString2 = Config.WorkflowSource.ConnectionString;

            var con1 = ConfigParse.GetConn<OracleConnection>(connectionString1);
            var con2 = ConfigParse.GetConn<OracleConnection>(connectionString2);

            txtDataSource1.Text = con1.DataSource;
            txtUserId1.Text = Config.WorkflowObjectUser;
            txtPassword1.Text = Config.WorkflowObjectPassword;

            txtDataSource2.Text = con2.DataSource;
            txtUserId2.Text = Config.WorkflowSourceUser;
            txtPassword2.Text = Config.WorkflowSourcePassword;
        }

        /// <summary>
        /// 保存设置对象的数据库连接配置
        /// </summary>
        private void saveConfig1()
        {
            ConfigParse.SaveConfig("WorkflowObject", connectionString1);
            ConfigParse.SaveAppSettings("WorkflowObjectUser", txtUserId1.Text.Trim().ToUpper());
            ConfigParse.SaveAppSettings("WorkflowObjectPassword", txtPassword1.Text.Trim());
        }

        /// <summary>
        /// 保存设置源的数据库连接配置
        /// </summary>
        private void saveConfig2()
        {
            ConfigParse.SaveConfig("WorkflowSource", connectionString2);
            ConfigParse.SaveAppSettings("WorkflowSourceUser", txtUserId2.Text.Trim().ToUpper());
            ConfigParse.SaveAppSettings("WorkflowSourcePassword", txtPassword2.Text.Trim());
        }

        #endregion
    }
}
