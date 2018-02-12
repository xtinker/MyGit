using System;
using log4net;

namespace Common
{
    /// <summary>
    /// 日志记录类
    /// </summary>
    public class LogWriter
    {
        static readonly ILog Log = LogManager.GetLogger(typeof(LogWriter));

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
