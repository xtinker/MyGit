using System;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DbHelper
{
    /// <summary>
    /// 获取网内的数据库服务器名称
    /// </summary>
    public class SqlLocator
    {
        /// <summary>
        /// 禁止实例化
        /// </summary>
        private SqlLocator() { }

        /// <summary>
        /// 获取局域网内的所有数据库服务器名称
        /// </summary>
        /// <returns>服务器名称数组</returns>
        public static string[] GetLocalSqlServerNamesWithSqlClientFactory()
        {
            DbDataSourceEnumerator dbDataSourceEnumerator = SqlClientFactory.Instance.CreateDataSourceEnumerator();
            if (dbDataSourceEnumerator != null)
            {
                DataTable dataSources = dbDataSourceEnumerator.GetDataSources();
                DataColumn column2 = dataSources.Columns["ServerName"];
                DataColumn column = dataSources.Columns["InstanceName"];
                DataRowCollection rows = dataSources.Rows;
                string[] array = new string[rows.Count];
                for (int i = 0; i < array.Length; i++)
                {
                    string str2 = rows[i][column2] as string;
                    string str = rows[i][column] as string;
                    if (((string.IsNullOrEmpty(str))) || ("MSSQLSERVER" == str))
                    {
                        array[i] = str2;
                    }
                    else
                    {
                        array[i] = str2 + @"\" + str;
                    }
                }
                Array.Sort(array);

                return array;
            }

            return new string[0];
        }

        /// <summary>
        /// 获取局域网内的所有数据库服务器名称
        /// </summary>
        /// <returns>服务器名称数组</returns>
        public static string[] GetLocalSqlServerNames()
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable table = instance.GetDataSources();
            var count = table.Rows.Count;
            if (count > 0)
            {
                string[] array = new string[count];
                for (int i = 0; i < count; i++)
                {
                    string serverName = table.Rows[i]["ServerName"] as string;
                    string instanceName = table.Rows[i]["InstanceName"] as string;
                    if (((string.IsNullOrEmpty(instanceName))) || ("MSSQLSERVER" == instanceName))
                    {
                        array[i] = serverName;
                    }
                    else
                    {
                        array[i] = serverName + @"\" + instanceName;
                    }
                }
                Array.Sort(array);

                return array;
            }
            
            return new string[0];
        }
    }
}
