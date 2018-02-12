using System.Collections.Generic;
using Model.Enum;

namespace Model
{
    /// <summary>
    /// 数据字典比较类，记录比较信息
    /// </summary>
    public class DataDictCompare
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int RowNum { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string table_name { get; set; }

        /// <summary>
        /// 表中文名称
        /// </summary>
        public string table_name_c { get; set; }

        /// <summary>
        /// 表状态
        /// </summary>
        public TableStatusEnum TableStatus { get; set; }

        /// <summary>
        /// 表对应数据字典
        /// </summary>
        public List<FieldCompare> FieldCompareList { get; set; }
    }
}
