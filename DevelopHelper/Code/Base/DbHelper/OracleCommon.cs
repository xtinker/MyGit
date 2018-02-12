using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbHelper
{
    public class OracleCommon
    {
        public static string[] DataTypes = { "TIMESTAMP(3)", "DATE" };
        public static string[] LobTypes = { "BLOB", "CLOB", "NCLOB", "LONG" };
        public static string[] NumberTypes = { "NUMBER", "BINARY_DOUBLE", "BINARY_FLOAT" };

        /// <summary>
        /// 获取表结构
        /// </summary>
        /// <param name="sqlHelper">数据库连接类</param>
        /// <param name="tableName">表名称</param>
        public static List<TableStruct> GetTableStruct(SqlHelper sqlHelper, string tableName)
        {
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
            }

            return tableStructs;
        }

        /// <summary>
        /// 结构表结构拼接插入语句
        /// </summary>
        /// <param name="tableStructs">表结构描述</param>
        /// <param name="tableName">表名</param>
        public static string GetInsertTemplate(List<TableStruct> tableStructs, string tableName)
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
        /// 获取插入脚本
        /// </summary>
        /// <param name="tableStructs">表结构描述</param>
        /// <param name="tableName">表名</param>
        /// <param name="dtSource">数据源</param>
        /// <param name="flowCode">流程编号</param>
        public static string GetScriptText(List<TableStruct> tableStructs, string tableName, DataTable dtSource, string flowCode = "")
        {
            StringBuilder scriptText = new StringBuilder();
            var insertTmplete = GetInsertTemplate(tableStructs, tableName);

            var declareParam = tableName.Substring(0, 1) + tableName.Substring(tableName.Length - 1, 1);
            if (!string.IsNullOrWhiteSpace(flowCode))
            {
                declareParam += flowCode.Substring(0, 1) + flowCode.Substring(flowCode.Length - 1, 1);
            }

            if (dtSource.Rows.Count > 0)
            {
                //如果有clob类型字段，则申明变量
                var clobStructs =
                    tableStructs.Where(i => LobTypes.Contains(i.type)).ToList();
                if (clobStructs.Count > 0)
                {
                    foreach (TableStruct clobStruct in clobStructs)
                    {
                        scriptText.Append("DECLARE " + declareParam.ToUpper() + clobStruct.coumnname + " NCLOB;\r\n");
                    }
                }

                bool hasBegin = false;
                if (scriptText.Length > 0)
                {
                    scriptText.Append("BEGIN\r\n");
                    hasBegin = true;
                }

                foreach (DataRow dataRow in dtSource.Rows)
                {
                    var values = "";
                    foreach (TableStruct tableStruct in tableStructs)
                    {
                        if (DataTypes.Contains(tableStruct.type))
                        {
                            values += "TO_DATE('" + dataRow[tableStruct.coumnname] + "', 'yyyy-mm-dd hh24:mi:ss'),";
                        }
                        else if (LobTypes.Contains(tableStruct.type))
                        {
                            var varName = declareParam.ToUpper() + tableStruct.coumnname;
                            values += varName + ",";
                            var varValue = "'" + dataRow[tableStruct.coumnname].ToString().Replace("'", "''") + "'";
                            scriptText.Append(varName + ":=" + varValue + ";\r\n");
                        }
                        else if (NumberTypes.Contains(tableStruct.type))
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
                            values += "'" + ResetSingleQuoteValue(dataRow[tableStruct.coumnname]) + "',";
                        }
                    }
                    values = values.Substring(0, values.Length - 1);

                    scriptText.Append(String.Format(insertTmplete, values) + "\r\n");
                }

                if (hasBegin)
                {
                    scriptText.Append("END;\r\n");
                }
            }

            return scriptText.ToString();
        }

        public static string ResetSingleQuoteValue(object name)
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
    }
}
