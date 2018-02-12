using System.Collections.Generic;
using System.Xml.Serialization;

namespace View45
{
    /// <summary>
    /// 表示一个语言的资源文件对象。
    /// </summary>
    [XmlType("Resource")]
    public class LanguageResource
    {
        /// <summary>
        /// 语言区域的名称。
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 包含StringResource的列表。
        /// </summary>
        public List<StringResource> List { get; set; }
    }
}
