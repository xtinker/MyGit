using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using Common;

namespace DbHelper
{
    /// <summary>
    /// SqlServer数据库连接类
    /// </summary>
    public class SqlServerHelper : SqlHelper
    {
        public SqlServerHelper(string connectionString,string dbName=null)
            : base(connectionString)
        {
            if (!String.IsNullOrWhiteSpace(dbName))
            {
                var items = connectionString.Split(';');
                for (int i = 0; i < items.Length; i++)
                {
                    var item = items[i];
                    if (item.Split('=')[0].ToLower() == "initial catalog")
                    {
                        items[i] = "initial catalog=" + dbName;
                    }
                }
                ConnectionString = String.Join(";", items);
            }
        }

        public static SqlServerHelper CreateSqlHelper(string connName,string dbName=null)
        {
            return new SqlServerHelper(ConfigurationManager.ConnectionStrings[connName].ConnectionString,dbName);
        }

        public override string ExecuteNonQuery(string cmdText, CommandType cmdtype)
        {
            string retVal;
            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand(cmdText, conn)
                {
                    CommandType = cmdtype
                };

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    cmd.CommandTimeout = 600;
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
            string retVal;
            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand(cmdText, conn)
                {
                    CommandType = CommandType.Text
                };

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    cmd.CommandTimeout = 600;
                    retVal = cmd.ExecuteNonQuery().ToString();
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
            string retVal;
            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand(cmdText, conn);

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlParameter op = new SqlParameter(paramName, SqlDbType.Text) { Value = paramValue };
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
            using (var conn = new SqlConnection(conString))
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
            object retVal;
            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand(cmdText, conn)
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
            object retVal;
            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand(cmdText, conn)
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
            var conn = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = cmdtype
            };
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            var conn = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand(cmdText, conn)
            {
                CommandType = CommandType.Text
            };
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
            var ds = new DataSet();
            var dt = new DataTable();
            int dtflag = 0;

            var conn = new SqlConnection(ConnectionString);
            var apt = new SqlDataAdapter(cmdText, conn)
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

            return dt;
        }

        public override DataTable ExecuteDataTable(string cmdText, CommandType cmdtype)
        {
            var dt = new DataTable();

            var conn = new SqlConnection(ConnectionString);
            var apt = new SqlDataAdapter(cmdText, conn)
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

            return dt;
        }

        public override DataTable ExecuteDataTable(string cmdText)
        {
            var dt = new DataTable();
            var conn = new SqlConnection(ConnectionString);
            var apt = new SqlDataAdapter(cmdText, conn)
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

            return dt;
        }
    }
}
