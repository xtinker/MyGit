using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

namespace DbHelper
{
    /// <summary>
    /// Oracle数据库连接类
    /// </summary>
    public class OracleHelper : SqlHelper
    {
        public OracleHelper(string connectionString, string userName=null)
            : base(connectionString)
        {
            if (!String.IsNullOrWhiteSpace(userName))
            {
                var items = connectionString.Split(';');
                for (int i = 0; i < items.Length; i++)
                {
                    var item = items[i];
                    if (item.Split('=')[0].ToLower() == "user id")
                    {
                        items[i] = "user id=" + userName;
                    }
                }
                ConnectionString = String.Join(";", items);
            }
        }

        #region 静态构造方法

        public static OracleHelper CreateSqlHelper(string connName, string userName = null)
        {
            var sqlHelper = new OracleHelper(ConfigurationManager.ConnectionStrings[connName].ConnectionString, userName);

            return sqlHelper;
        }

        #endregion

        public override string ExecuteNonQuery(string cmdText, CommandType cmdtype)
        {
            cmdText = SqlTransfer(cmdText);
            string retVal;
            using (var conn = new OracleConnection(ConnectionString))
            {
                var cmd = new OracleCommand(cmdText, conn)
                {
                    CommandType = cmdtype
                };

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    retVal = cmd.ExecuteNonQuery().ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception exp)
                {
                    LogWriter.Error(cmdText, exp);
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return retVal;
        }

        public override string ExecuteNonQuery(string cmdText)
        {
            cmdText = SqlTransfer(cmdText);
            string retVal;
            using (var conn = new OracleConnection(ConnectionString))
            {
                var cmd = new OracleCommand(cmdText, conn)
                {
                    CommandType = CommandType.Text
                };

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    retVal = cmd.ExecuteNonQuery().ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception exp)
                {
                    LogWriter.Error(cmdText, exp);
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return retVal;
        }

        public override string ExecuteWithParam(string cmdText, string paramName, string paramValue)
        {
            cmdText = SqlTransfer(cmdText);
            string retVal;
            using (var conn = new OracleConnection(ConnectionString))
            {
                var cmd = new OracleCommand(cmdText, conn);

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    OracleParameter op = new OracleParameter(paramName, OracleType.Clob);
                    op.Value = paramValue;
                    cmd.Parameters.Add(op);
                    retVal = cmd.ExecuteNonQuery().ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception exp)
                {
                    LogWriter.Error(cmdText, exp);
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return retVal;
        }

        public override bool ConnectionTest(string conString)
        {
            bool flag;
            using (var conn = new OracleConnection(conString))
            {
                try
                {
                    conn.Open();
                    flag = true;
                }
                catch (Exception)
                {
                    flag = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return flag;
        }

        public override object ExecuteScalar(string cmdText, CommandType cmdtype)
        {
            cmdText = SqlTransfer(cmdText);
            object retVal;

            using (var conn = new OracleConnection(ConnectionString))
            {
                var cmd = new OracleCommand(cmdText, conn)
                {
                    CommandType = cmdtype
                };

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    retVal = cmd.ExecuteScalar();
                }
                catch (Exception exp)
                {
                    LogWriter.Error(cmdText, exp);
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return retVal;
        }

        public override object ExecuteScalar(string cmdText)
        {
            cmdText = SqlTransfer(cmdText);
            object retVal;

            using (var conn = new OracleConnection(ConnectionString))
            {
                var cmd = new OracleCommand(cmdText, conn)
                {
                    CommandType = CommandType.Text
                };

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    retVal = cmd.ExecuteScalar();
                }
                catch (Exception exp)
                {
                    LogWriter.Error(cmdText, exp);
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

            return retVal;
        }

        public override DbDataReader ExecuteReader(string cmdText, CommandType cmdtype)
        {
            cmdText = SqlTransfer(cmdText);

            var conn = new OracleConnection(ConnectionString);
            var cmd = new OracleCommand(cmdText, conn)
            {
                CommandType = cmdtype
            };
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception exp)
            {
                LogWriter.Error(cmdText, exp);
                throw;
            }
        }

        public override DbDataReader ExecuteReader(string cmdText)
        {
            cmdText = SqlTransfer(cmdText);

            var conn = new OracleConnection(ConnectionString);
            var cmd = new OracleCommand(cmdText, conn)
            {
                CommandType = CommandType.Text
            };
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                OracleDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception exp)
            {
                LogWriter.Error(cmdText, exp);
                throw;
            }
        }

        public override DataTable ExecuteDataTable(string cmdText, int statRowNum, int maxRowNum, CommandType cmdtype)
        {
            cmdText = SqlTransfer(cmdText);

            var ds = new DataSet();
            var dt = new DataTable();
            int dtflag = 0;

            var conn = new OracleConnection(ConnectionString);
            var apt = new OracleDataAdapter(cmdText, conn)
            {
                SelectCommand = { CommandType = cmdtype }
            };

            try
            {
                if (statRowNum == 0 && maxRowNum == 0)
                {
                    apt.Fill(dt);
                }
                else
                {
                    apt.Fill(ds, statRowNum, maxRowNum, "ThisTable");
                    dtflag = 1;
                }
            }
            catch (Exception exp)
            {
                LogWriter.Error(cmdText, exp);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            if (dtflag == 1)
            {
                dt = ds.Tables["ThisTable"];
            }


            DataTableTransfer(cmdText, dt);
            return dt;
        }

        public override DataTable ExecuteDataTable(string cmdText, CommandType cmdtype)
        {
            cmdText = SqlTransfer(cmdText);

            var dt = new DataTable();

            var conn = new OracleConnection(ConnectionString);
            var apt = new OracleDataAdapter(cmdText, conn)
            {
                SelectCommand = { CommandType = cmdtype }
            };
            try
            {
                apt.Fill(dt);
            }
            catch (Exception exp)
            {
                LogWriter.Error(cmdText, exp);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            DataTableTransfer(cmdText, dt);
            return dt;
        }

        public override DataTable ExecuteDataTable(string cmdText)
        {
            cmdText = SqlTransfer(cmdText);

            var dt = new DataTable();
            var conn = new OracleConnection(ConnectionString);
            var apt = new OracleDataAdapter(cmdText, conn)
            {
                SelectCommand = { CommandType = CommandType.Text }
            };
            try
            {
                apt.Fill(dt);
            }
            catch (Exception exp)
            {
                LogWriter.Error(cmdText, exp);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            DataTableTransfer(cmdText, dt);
            return dt;
        }

        #region 私有方法

        private string SqlTransfer(string sql)
        {
            var regex = new Regex("isnull", RegexOptions.IgnoreCase);
            string result = regex.Replace(sql, m => "nvl");

            return result;
        }

        /// <summary>
        /// 使得DataTable的字段区分大小写
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dt"></param>
        private void DataTableTransfer(string sql, DataTable dt)
        {
            string[] dic = sql.Split(' ', ',', '.', '[', ']', '\r', '\n', '(', ')', '|');
            foreach (DataColumn col in dt.Columns)
            {
                if (col.ColumnName != "ID" && col.ColumnName == col.ColumnName.ToUpper() &&
                    !dic.Contains(col.ColumnName))
                {
                    foreach (string item in dic)
                    {
                        if (item.ToUpper() == col.ColumnName)
                        {
                            col.ColumnName = item;
                            break;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
