using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConfigHelper
{
    /// <summary>
    /// 数据库连接字符串解析类
    /// </summary>
    public static class ConfigParse
    {
        /// <summary>
        /// 连接字符串转连接类
        /// </summary>
        /// <param name="conString">连接字符串</param>
        /// <returns>连接类</returns>
        public static T GetConn<T>(string conString) where T : class, new()
        {
            if (String.IsNullOrWhiteSpace(conString))
            {
                return new T();
            }

            var obj = new T();
            using (SqlConnection connection = new SqlConnection(conString))
            {
                var pis = connection.GetType().GetProperties();
                foreach (PropertyInfo pi in pis)
                {
                    if (!pi.CanWrite)
                        continue;

                    PropertyInfo piNew = typeof(T).GetProperty(pi.Name);
                    if (piNew != null)
                        piNew.SetValue(obj, pi.GetValue(connection, null), null);
                }
            }
            return obj;
        }

        /// <summary>
        /// 连接类转连接字符串
        /// </summary>
        /// <param name="con">连接类</param>
        /// <returns>连接字符串</returns>
        public static string GetConnectionString<T>(T con) where T : class, new()
        {
            var connectionString = "";
            if (con != null)
            {
                var pi = typeof(T).GetProperty("ConnectionString");
                if (pi != null && pi.CanRead)
                {
                    var obj = pi.GetValue(con, null);
                    if (obj != null)
                    {
                        connectionString = obj.ToString();
                    }
                }
            }
            return connectionString;
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="name">配置节点名称</param>
        /// <param name="connectionString">连接字符串</param>
        public static void SaveConfig(string name, string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connectionString");

            string mCurPath = AppDomain.CurrentDomain.BaseDirectory;
            var mConfigFullName = Path.Combine(mCurPath, "MainTool.exe.config");
            ExeConfigurationFileMap configFile = new ExeConfigurationFileMap { ExeConfigFilename = mConfigFullName };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
            config.UpdateConnectionStringsConfig(name, connectionString, ConfigurationManager.ConnectionStrings[name].ProviderName);
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="conn">连接类</param>
        /// <param name="name">配置节点名称</param>
        /// <param name="connectionString">连接字符串</param>
        public static void SaveConfig<T>(T conn, string name, ref string connectionString) where T : class, new()
        {
            if (connectionString == null)
                throw new ArgumentNullException("connectionString");

            string mCurPath = AppDomain.CurrentDomain.BaseDirectory;
            var mConfigFullName = Path.Combine(mCurPath, "MainTool.exe.config");
            ExeConfigurationFileMap configFile = new ExeConfigurationFileMap { ExeConfigFilename = mConfigFullName };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
            connectionString = GetConnectionString(conn);
            config.UpdateConnectionStringsConfig(name, connectionString, ConfigurationManager.ConnectionStrings[name].ProviderName);
        }

        /// <summary>
        /// 保存AppSetting节点
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SaveAppSettings(string key, string value)
        {
            string mCurPath = AppDomain.CurrentDomain.BaseDirectory;
            var mConfigFullName = Path.Combine(mCurPath, "MainTool.exe.config");
            ExeConfigurationFileMap configFile = new ExeConfigurationFileMap { ExeConfigFilename = mConfigFullName };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
            config.UpdateAppSettingsItemValue(key, value);
        }

        /// <summary>
        /// 保存configSections节点
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SaveSqlServerConfigSections(string key, string value)
        {
            SaveConfigSections(key, value, "SqlServer");
        }

        /// <summary>
        /// 保存configSections节点
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SaveOracleConfigSections(string key, string value)
        {
            SaveConfigSections(key, value, "Oracle");
        }

        /// <summary>
        /// 保存configSections节点
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="sectionName">数据源类型：SqlServer|Oracle等，具体看配置文件configSections节点下的子节点</param>
        public static void SaveConfigSections(string key, string value, string sectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
                return;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var dic = config.GetKeyValueSectionValues(sectionName);
            if (dic == null)
            {
                dic = new Dictionary<string, string> { { key, value } };
            }
            else
            {
                //判断是否已经包含，有则修改
                if (dic.ContainsKey(key))
                {
                    dic[key] = value;
                }
                else
                {
                    //新增
                    //最多显示常用的8个连接，超过8个，删除最后一个，将新的连接添加到第一个
                    if (dic.Count >= 8)
                    {
                        dic.Remove(dic.Last().Key);
                    }

                    var dicNew = new Dictionary<string, string>();
                    dicNew.Add(key, value);
                    foreach (KeyValuePair<string, string> item in dic)
                    {
                        dicNew.Add(item.Key, item.Value);
                    }

                    dic = dicNew;
                }
            }
            ConfigurationExtensions.UpdateKeyValueSection(config, sectionName, dic);
            config.Save();

            ConfigurationManager.RefreshSection(sectionName);  //让修改之后的结果生效
        }
    }
}
