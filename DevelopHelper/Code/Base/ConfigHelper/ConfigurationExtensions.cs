using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace ConfigHelper
{
    static class ConfigurationExtensions
    {
        #region Update ConnectionStrings

        ///<summary> 
        ///更新连接字符串  
        ///</summary> 
        ///<param name="newName">连接字符串名称</param> 
        ///<param name="newConString">连接字符串内容</param> 
        ///<param name="newProviderName">数据提供程序名称</param> 
        ///<param name="config">Configuration实例</param>
        public static void UpdateConnectionStringsConfig(this Configuration config, string newName, string newConString, string newProviderName = null)
        {
            bool isModified = config.ConnectionStrings.ConnectionStrings[newName] != null;
            //记录该连接串是否已经存在      
            //如果要更改的连接串已经存在      

            //新建一个连接字符串实例    
            ConnectionStringSettings mySettings = String.IsNullOrWhiteSpace(newProviderName) ? new ConnectionStringSettings(newName, newConString) : new ConnectionStringSettings(newName, newConString, newProviderName);

            // 如果连接串已存在，首先删除它      
            if (isModified)
            {
                config.ConnectionStrings.ConnectionStrings.Remove(newName);
            }
            // 将新的连接串添加到配置文件中.      
            config.ConnectionStrings.ConnectionStrings.Add(mySettings);
            // 保存对配置文件所作的更改      
            config.Save(ConfigurationSaveMode.Modified);
        }

        #endregion

        #region Update AppSettings

        ///<summary>  
        ///更新在config文件中appSettings配置节增加一对键、值对。
        ///</summary>  
        ///<param name="newKey"></param>  
        ///<param name="newValue"></param>  
        ///<param name="config"></param>
        public static void UpdateAppSettingsItemValue(this Configuration config, string newKey, string newValue)
        {
            UpdateAppSettingsItemNoSave(config, newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
        }

        private static void UpdateAppSettingsItemNoSave(Configuration config, string newKey, string newValue)
        {
            bool isModified = false;
            foreach (KeyValueConfigurationElement key in config.AppSettings.Settings)
            {
                if (key.Key == newKey)
                {
                    isModified = true;
                }
            }

            if (isModified)
            {
                config.AppSettings.Settings.Remove(newKey);
            }

            config.AppSettings.Settings.Add(newKey, newValue);
        }

        #endregion

        #region Update configSections



        #endregion

        #region Common Method

        /// <summary>
        /// 通用获取key-value 键值对Section值的集合，可用于DictionarySectionHandler或NameValueSectionHandler 定义的配置节 NameValueSectionHandler的Key值不能重复
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="config"></param>
        /// <returns>没有配置节时返回null</returns>
        public static Dictionary<string, string> GetKeyValueSectionValues(this Configuration config, string sectionName)
        {
            var section = config.GetSection(sectionName);

            if (section == null)
                return null;

            Dictionary<string, string> result = new Dictionary<string, string>();

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(section.SectionInformation.GetRawXml());
            XmlNode xnode = xdoc.ChildNodes[0];

            IDictionary dict = (IDictionary)(new DictionarySectionHandler().Create(null, null, xnode));
            foreach (string str in dict.Keys)
            {
                result[str] = (string)dict[str];
            }

            return result;
        }

        /// <summary>
        /// 更新配置节，相同的就修改，没有的就增加。
        /// </summary>
        /// <param name="config"></param>
        /// <param name="sectionName"></param>
        /// <param name="items"></param>
        public static void UpdateKeyValueSectionValues(this Configuration config, string sectionName, Dictionary<string, string> items)
        {
            Dictionary<string, string> orgItem = GetKeyValueSectionValues(config, sectionName) ??
                                                 new Dictionary<string, string>();
            foreach (string key in items.Keys)
            {
                orgItem[key] = items[key];
            }
            UpdateKeyValueSection(config, sectionName, orgItem);
        }

        public static void UpdateKeyValueSection(Configuration config, string sectionName, Dictionary<string, string> items)
        {
            config.Sections.Remove(sectionName);

            AppSettingsSection section = new AppSettingsSection();
            config.Sections.Add(sectionName, section);

            foreach (string key in items.Keys)
            {
                section.Settings.Add(new KeyValueConfigurationElement(key, items[key]));
            }
            section.SectionInformation.Type = typeof(DictionarySectionHandler).AssemblyQualifiedName;
            config.Save(ConfigurationSaveMode.Modified);
        }

        #endregion
    }
}
