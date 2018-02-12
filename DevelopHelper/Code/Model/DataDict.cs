using System;

namespace Model
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class DataDict
    {
        /// <summary>
        /// 表主键
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// 表ID，对应元数据entityId
        /// </summary>
        public Guid TableGUID { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string table_name { get; set; }

        /// <summary>
        /// 表中文名称
        /// </summary>
        public string table_name_c { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string field_name { get; set; }

        /// <summary>
        /// 字段中文名称
        /// </summary>
        public string field_name_c { get; set; }

        /// <summary>
        /// 字段序号
        /// </summary>
        public int field_sequence { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string data_type { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// 小数位
        /// </summary>
        public int DecimalPrecision { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
