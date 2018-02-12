namespace DbHelper
{
    /// <summary>
    /// 表结构描述类
    /// </summary>
    public class TableStruct
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string coumnname { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public int isnullable { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public short length { get; set; }
    }
}
