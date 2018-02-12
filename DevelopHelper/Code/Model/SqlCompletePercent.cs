namespace Model
{
    /// <summary>
    /// 数据库还原进度描述实体
    /// </summary>
    public class SqlCompletePercent
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DatabaseName { get; set; }

        public int SessionID { get; set; }

        public string CommandType { get; set; }

        /// <summary>
        /// 还原脚本
        /// </summary>
        public string StatementText { get; set; }

        public string Status { get; set; }

        /// <summary>
        /// 还原完成百分比
        /// </summary>
        public int Complete_Percent { get; set; }

        public decimal ElapsedTime_m { get; set; }

        public decimal EstimatedCompletionTime_m { get; set; }

        public string LastWait { get; set; }

        public string CurrentWait { get; set; }
    }
}
