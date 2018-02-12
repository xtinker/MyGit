using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DbHelper
{
    public abstract class SqlHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString;

        protected SqlHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #region SQL无返回值可操作的方法

        /// <summary>
        /// 带SQL执行类型，无返回值的SQL执行
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdtype"></param>
        /// <returns></returns>
        public abstract string ExecuteNonQuery(string cmdText, CommandType cmdtype);

        /// <summary>
        /// 带错误输出，无返回值的SQL执行（默认是Text）
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public abstract string ExecuteNonQuery(string cmdText);

        /// <summary>
        /// 执行带参数的SQL语句
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public abstract string ExecuteWithParam(string cmdText, string paramName, string paramValue);

        /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        public abstract bool ConnectionTest(string conString);

        #endregion

        #region SQL有返回值可操作的方法

        /// <summary>
        /// 带参，有返回值的SQL执行
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdtype"></param>
        /// <returns></returns>
        public abstract object ExecuteScalar(string cmdText, CommandType cmdtype);

        /// <summary>
        /// 有返回值的SQL执行
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public abstract object ExecuteScalar(string cmdText);

        #endregion

        #region 返回数据读取器(DataReader)可操作的方法

        /// <summary>
        /// 带SQL执行类型，有返回值的SQL执行
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdtype"></param>
        /// <returns></returns>
        public abstract DbDataReader ExecuteReader(string cmdText, CommandType cmdtype);

        /// <summary>
        /// 有返回值的SQL执行
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public abstract DbDataReader ExecuteReader(string cmdText);

        #endregion

        #region 返回DataTable可操作的方法

        /// <summary>
        /// 带参，带SQL执行类型，返回DataTable的SQL执行
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="statRowNum"></param>
        /// <param name="maxRowNum"></param>
        /// <param name="cmdtype"></param>
        /// <returns></returns>
        public abstract DataTable ExecuteDataTable(string cmdText, int statRowNum, int maxRowNum, CommandType cmdtype);

        /// <summary>
        /// 带SQL执行类型，返回DataTable的SQL执行
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdtype"></param>
        /// <returns></returns>
        public abstract DataTable ExecuteDataTable(string cmdText, CommandType cmdtype);

        /// <summary>
        /// 返回DataTable的SQL执行
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public abstract DataTable ExecuteDataTable(string cmdText);

        #endregion

        public T ExecuteObject<T>(string sql, T obj = null) where T : class, new()
        {
            DataTable dt = this.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
                return RowToObj<T>(dt.Rows[0], obj);
            return obj;
        }

        public List<T> ExecuteObjectList<T>(string sql, T obj = null) where T : class, new()
        {
            DataTable dt = this.ExecuteDataTable(sql);
            List<T> list = new List<T>();
            if (dt.Rows.Count > 0)
                list = RowsToObjList<T>(dt.Rows, obj);
            return list;
        }

        #region 私有方法

        private T RowToObj<T>(DataRow row, T obj = null) where T : class, new()
        {
            if (obj == null)
                obj = new T();

            foreach (var pi in typeof(T).GetProperties())
            {
                if (!row.Table.Columns.Contains(pi.Name))
                    continue;
                if (row[pi.Name] != System.DBNull.Value)
                {
                    object value = row[pi.Name];
                    if (value is Int64 && pi.PropertyType.FullName.Contains("Int32"))
                        value = int.Parse(value.ToString());
                    if (pi.PropertyType.FullName.Contains("Double"))
                        value = double.Parse(value.ToString());
                    if (pi.PropertyType.FullName.Contains("Int64"))
                        value = Convert.ToInt64(value);
                    pi.SetValue(obj, value, null);
                }
            }

            return obj;
        }

        private List<T> RowsToObjList<T>(DataRowCollection rows, T obj = null) where T : class, new()
        {
            List<T> list = new List<T>();
            if (rows != null && rows.Count > 0)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    list.Add(RowToObj<T>(rows[i], new T()));
                }
            }

            return list;
        }

        #endregion
    }
}
