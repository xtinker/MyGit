using Model.Enum;

namespace Model
{
    /// <summary>
    /// 字段比对信息封装
    /// </summary>
    public class FieldCompare : DataDict
    {
        /// <summary>
        /// 字段状态
        /// </summary>
        public FieldStatusEnum FieldStatus { get; set; }
    }
}
