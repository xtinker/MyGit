using System.Collections.Generic;

namespace View45
{
    /// <summary>
    /// 枚举描述实体
    /// </summary>
    public class EnumObject
    {
        public string EnumObjectName { get; set; }

        public List<string> EntityAndField { get; set; }

        public List<EnumInfo> EnumInfos { get; set; } 
    }
}
