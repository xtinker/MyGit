using System.Xml.Serialization;

namespace View45
{
    /// <summary>
	/// 表示一条语言资源配置信息。
	/// </summary>
	[XmlType("string")]
    public class StringResource
    {
        /// <summary>
        /// 资源KEY
        /// 注意：KEY要求在应用程序内全局唯一。
        /// </summary>
        [XmlAttribute]
        public string Key { get; set; }

        /// <summary>
        /// 资源文本字符串。
        /// </summary>
        [XmlText]
        public string Text { get; set; }

        /// <summary>
        /// 输出到JavaScript的范围名称。
        /// </summary>
        [XmlAttribute]
        public string JsScope { get; set; }

        /// <summary>
        /// 语言区域的名称（仅供运行时内部使用，不做持久化处理）。
        /// </summary>
        [XmlIgnore]
        public string Language { get; set; }
    }
}
