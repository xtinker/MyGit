using System.Configuration;

namespace ConfigHelper
{
    public static class Config
    {
        /// <summary>
        /// 根据Key值获取AppSetting配置信息
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }

        /// <summary>
        /// Oracle流程同步处理对象
        /// </summary>
        public static ConnectionStringSettings WorkflowObject
        {
            get { return ConfigurationManager.ConnectionStrings["WorkflowObject"]; }
        }

        /// <summary>
        /// Oracle流程定义SQL源
        /// </summary>
        public static ConnectionStringSettings WorkflowSource
        {
            get { return ConfigurationManager.ConnectionStrings["WorkflowSource"]; }
        }

        /// <summary>
        /// SqlServer流程定义SQL源
        /// </summary>
        public static ConnectionStringSettings Workflow
        {
            get { return ConfigurationManager.ConnectionStrings["Workflow"]; }
        }

        /// <summary>
        /// SqlServer脚本生成工具SQL源
        /// </summary>
        public static ConnectionStringSettings SqlServerSource
        {
            get { return ConfigurationManager.ConnectionStrings["SqlServerSource"]; }
        }

        /// <summary>
        /// Oracle脚本生成工具SQL源
        /// </summary>
        public static ConnectionStringSettings OracleSource
        {
            get { return ConfigurationManager.ConnectionStrings["OracleSource"]; }
        }

        #region 用户名和密码

        /// <summary>
        /// Oracle流程同步用户名
        /// </summary>
        public static string WorkflowObjectUser
        {
            get { return ConfigurationManager.AppSettings.Get("WorkflowObjectUser"); }
        }

        /// <summary>
        /// Oracle流程同步密码
        /// </summary>
        public static string WorkflowObjectPassword
        {
            get { return ConfigurationManager.AppSettings.Get("WorkflowObjectPassword"); }
        }

        /// <summary>
        /// Oracle流程定义户名
        /// </summary>
        public static string WorkflowSourceUser
        {
            get { return ConfigurationManager.AppSettings.Get("WorkflowSourceUser"); }
        }

        /// <summary>
        /// Oracle流程定义密码
        /// </summary>
        public static string WorkflowSourcePassword
        {
            get { return ConfigurationManager.AppSettings.Get("WorkflowSourcePassword"); }
        }

        /// <summary>
        /// SqlServer流程定义用户名
        /// </summary>
        public static string WorkflowUser
        {
            get { return ConfigurationManager.AppSettings.Get("WorkflowUser"); }
        }

        /// <summary>
        /// SqlServer流程定义密码
        /// </summary>
        public static string WorkflowPassword
        {
            get { return ConfigurationManager.AppSettings.Get("WorkflowPassword"); }
        }

        /// <summary>
        /// SqlServer脚本生成工具用户名
        /// </summary>
        public static string SqlServerSourceUser
        {
            get { return ConfigurationManager.AppSettings.Get("SqlServerSourceUser"); }
        }

        /// <summary>
        /// SqlServer脚本生成工具密码
        /// </summary>
        public static string SqlServerSourcePassword
        {
            get { return ConfigurationManager.AppSettings.Get("SqlServerSourcePassword"); }
        }

        /// <summary>
        /// Oracle脚本生成工具用户名
        /// </summary>
        public static string OracleSourceUser
        {
            get { return ConfigurationManager.AppSettings.Get("OracleSourceUser"); }
        }

        /// <summary>
        /// Oracle脚本生成工具密码
        /// </summary>
        public static string OracleSourcePassword
        {
            get { return ConfigurationManager.AppSettings.Get("OracleSourcePassword"); }
        }

        /// <summary>
        /// Oracle脚本生成工具密码
        /// </summary>
        public static string DefaultTheme
        {
            get { return ConfigurationManager.AppSettings.Get("DefaultTheme"); }
        }

        #endregion
    }
}
