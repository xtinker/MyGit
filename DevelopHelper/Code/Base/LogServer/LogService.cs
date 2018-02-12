using System;
using log4net;

namespace LogServer
{
    /// <summary>
    /// 日志记录类
    /// </summary>
    public class LogService
    {
        static readonly ILog Log = LogManager.GetLogger(typeof(LogService));

        /// <summary>
        /// 数据出错信息
        /// </summary>
        /// <param name="message"></param>
        public static void Info(object message)
        {
            Log.Info(message);
        }
        /// <summary>
        /// 系统出错信息
        /// </summary>
        /// <param name="message"></param>
        public static void Error(object message)
        {
            Log.Error(message);
        }
        /// <summary>
        /// 系统出错信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Error(object message, Exception exception)
        {
            Log.Error(message, exception);
        }
    }
}
