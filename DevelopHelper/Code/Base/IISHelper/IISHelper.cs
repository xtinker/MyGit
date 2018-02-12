using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Common;
using Microsoft.Web.Administration;

namespace IISHelper
{
    public class IISHelper
    {
        public static bool HasInstalled()
        {
            return true;
        }
        public static int GetIISVersionByDri(string domainname)
        {
            int result;
            try
            {
                if (string.IsNullOrEmpty(domainname))
                {
                    domainname = "LOCALHOST";
                }
                DirectoryEntry directoryEntry = new DirectoryEntry("IIS://" + domainname + "/W3SVC/INFO");
                int num = Convert.ToInt32(directoryEntry.Properties["MajorIISVersionNumber"].Value);
                result = num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static int GetIISMajorVersion()
        {
            int result;
            try
            {
                string localMachineKeyValue = RegistryHelper.GetLocalMachineKeyValue("SOFTWARE\\Microsoft\\InetStp", "MajorVersion");
                if (string.IsNullOrEmpty(localMachineKeyValue))
                {
                    throw new Exception("本地服务器未安装IIS。");
                }
                int num = -1;
                if (!int.TryParse(localMachineKeyValue, out num))
                {
                    throw new Exception("本地服务器未安装IIS。");
                }
                result = num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static Dictionary<string, SiteObj> GetSiteNames()
        {
            Dictionary<string, SiteObj> dictionary = new Dictionary<string, SiteObj>();
            int iISMajorVersion = IISHelper.GetIISMajorVersion();
            if (iISMajorVersion == 6)
            {
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\MicrosoftIISv2", "SELECT * FROM IIsWebServerSetting");
                using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        ManagementObject managementObject = (ManagementObject)enumerator.Current;
                        SiteObj siteObj = new SiteObj();
                        siteObj.SiteName = managementObject["ServerComment"].ToString();
                        siteObj.SiteNameQuanPin = PinyinHelper.GetPinyin(siteObj.SiteName).ToUpper();
                        siteObj.SiteNameDuanPin = PinyinHelper.GetFirstPinyin(siteObj.SiteName).ToUpper();
                        siteObj.PoolName = managementObject["AppPoolId"].ToString();
                        ManagementBaseObject[] array = (ManagementBaseObject[])managementObject["ServerBindings"];
                        siteObj.Port = array[0]["Port"].ToString();
                        int num = 0;
                        while (dictionary.ContainsKey(siteObj.SiteName))
                        {
                            num++;
                            siteObj.SiteName = managementObject["ServerComment"].ToString() + "_" + num.ToString();
                        }
                        dictionary.Add(siteObj.SiteName, siteObj);
                    }
                }
            }
            else
            {
                ServerManager serverManager = new ServerManager();
                foreach (Site current in serverManager.Sites)
                {
                    SiteObj siteObj = new SiteObj();
                    siteObj.SiteName = current.Name;
                    siteObj.SiteNameQuanPin = PinyinHelper.GetPinyin(siteObj.SiteName).ToUpper();
                    siteObj.SiteNameDuanPin = PinyinHelper.GetFirstPinyin(siteObj.SiteName).ToUpper();
                    siteObj.PoolName = ((current.Applications.Count > 0) ? current.Applications[0].ApplicationPoolName : "");
                    siteObj.Port = current.Bindings[0].EndPoint.Port.ToString();
                    siteObj.SitePath = ((current.Applications.Count > 0) ? current.Applications[0].VirtualDirectories[0].PhysicalPath : "");
                    int num = 0;
                    while (dictionary.ContainsKey(siteObj.SiteName))
                    {
                        num++;
                        siteObj.SiteName = current.Name + "_" + num.ToString();
                    }
                    dictionary.Add(siteObj.SiteName, siteObj);
                }
            }
            return dictionary;
        }
        public static List<string> GetSiteNames(string strKeyWord)
        {
            List<string> list = new List<string>();
            int iISMajorVersion = IISHelper.GetIISMajorVersion();
            if (iISMajorVersion == 6)
            {
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\MicrosoftIISv2", "SELECT * FROM IIsWebServerSetting");
                using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        ManagementObject managementObject = (ManagementObject)enumerator.Current;
                        list.Add(managementObject["ServerComment"].ToString());
                    }
                }
            }
            else
            {
                ServerManager serverManager = new ServerManager();
                foreach (Site current in serverManager.Sites)
                {
                    if (current.Name.ToUpper().Contains(strKeyWord.ToUpper()) || PinyinHelper.GetPinyin(current.Name).ToUpper().Contains(strKeyWord.ToUpper()) || PinyinHelper.GetFirstPinyin(current.Name).ToUpper().Contains(strKeyWord.ToUpper()))
                    {
                        list.Add(current.Name);
                    }
                }
            }
            return list;
        }
        public static string GetWebSiteNumByName(string siteName)
        {
            Regex regex = new Regex(siteName);
            string path = "IIS://localhost/w3svc";
            DirectoryEntry directoryEntry = new DirectoryEntry(path);
            string result;
            foreach (DirectoryEntry directoryEntry2 in directoryEntry.Children)
            {
                if (directoryEntry2.SchemaClassName == "IIsWebServer")
                {
                    if (directoryEntry2.Properties["ServerBindings"].Value != null)
                    {
                        string b = directoryEntry2.Properties["ServerBindings"].Value.ToString();
                        if (string.Equals(siteName, b, StringComparison.InvariantCultureIgnoreCase))
                        {
                            result = directoryEntry2.Name;
                            return result;
                        }
                    }
                    if (directoryEntry2.Properties["ServerComment"].Value != null)
                    {
                        string b = directoryEntry2.Properties["ServerComment"].Value.ToString();
                        if (string.Equals(siteName, b, StringComparison.InvariantCultureIgnoreCase))
                        {
                            result = directoryEntry2.Name;
                            return result;
                        }
                    }
                }
            }
            result = "";
            return result;
        }
        public static string GetSitePath(string siteName)
        {
            string result;
            try
            {
                int iISMajorVersion = IISHelper.GetIISMajorVersion();
                if (iISMajorVersion == 6)
                {
                    string webSiteNumByName = IISHelper.GetWebSiteNumByName(siteName);
                    DirectoryEntry directoryEntry = new DirectoryEntry(string.Format("IIS://LOCALHOST/W3SVC/{0}/root", webSiteNumByName));
                    result = directoryEntry.Properties["Path"].Value.ToString();
                }
                else
                {
                    string sitePathFor = IISHelper.GetSitePathFor7(siteName);
                    result = sitePathFor;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static string GetSitePathFor7(string siteName)
        {
            ServerManager serverManager = new ServerManager();
            string result;
            foreach (Site current in serverManager.Sites)
            {
                if (current.Name.ToLower() == siteName.ToLower())
                {
                    result = current.Applications[0].VirtualDirectories[0].PhysicalPath;
                    return result;
                }
            }
            result = "";
            return result;
        }
        public static int GetSitePortFor7(string siteName)
        {
            ServerManager serverManager = new ServerManager();
            int result;
            foreach (Site current in serverManager.Sites)
            {
                if (current.Name.ToLower() == siteName.ToLower())
                {
                    result = current.Bindings[0].EndPoint.Port;
                    return result;
                }
            }
            result = 80;
            return result;
        }
        public static string GetSitePoolNameFor7(string siteName)
        {
            ServerManager serverManager = new ServerManager();
            string result;
            foreach (Site current in serverManager.Sites)
            {
                if (current.Name.ToLower() == siteName.ToLower())
                {
                    if (current.Applications.Count > 0)
                    {
                        result = current.Applications[0].ApplicationPoolName;
                        return result;
                    }
                }
            }
            result = "";
            return result;
        }
        public static List<string> GetSiteMimeList(string siteName)
        {
            string webSiteNumByName = IISHelper.GetWebSiteNumByName(siteName);
            List<string> result = new List<string>();
            DirectoryEntry directoryEntry = new DirectoryEntry(string.Format("IIS://localhost/w3svc/{0}/root", webSiteNumByName));
            PropertyValueCollection propertyValueCollection = directoryEntry.Properties["MimeMap"];
            foreach (object current in propertyValueCollection)
            {
            }
            return result;
        }
        public static List<string> GetSiteMimeListFor7(string siteName)
        {
            List<string> list = new List<string>();
            ServerManager serverManager = new ServerManager();
            Configuration webConfiguration = serverManager.GetWebConfiguration(siteName);
            ConfigurationSection section = webConfiguration.GetSection("system.webServer/staticContent");
            ConfigurationElement collection = section.GetCollection();
            ConfigurationElementCollection collection2 = collection.GetCollection();
            foreach (ConfigurationElement current in collection2)
            {
                list.Add(current.GetAttributeValue("fileExtension") + "|" + current.GetAttributeValue("mimeType"));
            }
            return list;
        }
        public static List<string> GetSiteScriptList(string siteName)
        {
            string webSiteNumByName = IISHelper.GetWebSiteNumByName(siteName);
            List<string> list = new List<string>();
            DirectoryEntry directoryEntry = new DirectoryEntry(string.Format("IIS://localhost/w3svc/{0}/root", webSiteNumByName));
            PropertyValueCollection propertyValueCollection = directoryEntry.Properties["ScriptMaps"];
            foreach (object current in propertyValueCollection)
            {
                list.Add(current.ToString());
            }
            return list;
        }
        public static List<string> GetSiteScriptListFor7(string siteName)
        {
            string webSiteNumByName = IISHelper.GetWebSiteNumByName(siteName);
            List<string> list = new List<string>();
            ServerManager serverManager = new ServerManager();
            Configuration webConfiguration = serverManager.GetWebConfiguration(siteName);
            ConfigurationSection section = webConfiguration.GetSection("system.webServer/handlers");
            ConfigurationElementCollection collection = section.GetCollection();
            foreach (ConfigurationElement current in collection)
            {
                list.Add(string.Concat(new object[]
                {
                    current.GetAttributeValue("name"),
                    "|",
                    current.GetAttributeValue("path"),
                    "|",
                    current.GetAttributeValue("scriptProcessor")
                }));
            }
            return list;
        }
        public static bool CheckRootScriptIsHave(string scriptName)
        {
            ServerManager serverManager = new ServerManager();
            Configuration applicationHostConfiguration = serverManager.GetApplicationHostConfiguration();
            ConfigurationSection section = applicationHostConfiguration.GetSection("system.webServer/handlers");
            ConfigurationElementCollection collection = section.GetCollection();
            bool result;
            foreach (ConfigurationElement current in collection)
            {
                if (scriptName.ToLower() == current.GetAttributeValue("name").ToString().ToLower())
                {
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }
        public static List<string> GetSiteHttpHeadList(string siteName)
        {
            string webSiteNumByName = IISHelper.GetWebSiteNumByName(siteName);
            List<string> list = new List<string>();
            DirectoryEntry directoryEntry = new DirectoryEntry(string.Format("IIS://localhost/w3svc/{0}/root", webSiteNumByName));
            PropertyValueCollection propertyValueCollection = directoryEntry.Properties["HttpCustomHeaders"];
            foreach (object current in propertyValueCollection)
            {
                list.Add(current.ToString());
            }
            return list;
        }
        public static bool CheckIIS6()
        {
            bool result;
            try
            {
                DirectoryEntry directoryEntry = new DirectoryEntry("IIS://localhost/w3svc");
                directoryEntry.Children.GetEnumerator();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public static string GetNewWebSiteID()
        {
            int num = 0;
            DirectoryEntry directoryEntry = new DirectoryEntry("IIS://localhost/w3svc");
            foreach (DirectoryEntry directoryEntry2 in directoryEntry.Children)
            {
                if (directoryEntry2.SchemaClassName == "IIsWebServer")
                {
                    string text = directoryEntry2.Properties["ServerComment"].Value.ToString();
                    string name = directoryEntry2.Name;
                    int num2 = Convert.ToInt32(name);
                    if (num < num2)
                    {
                        num = num2;
                    }
                }
            }
            if (num < 10)
            {
                num = 10;
            }
            return Convert.ToString(++num);
        }
        public static bool IsExistSite(string strSiteName)
        {
            bool result = false;
            DirectoryEntry directoryEntry = new DirectoryEntry("IIS://localhost/w3svc");
            foreach (DirectoryEntry directoryEntry2 in directoryEntry.Children)
            {
                if (directoryEntry2.SchemaClassName == "IIsWebServer")
                {
                    if (directoryEntry2.Properties["ServerComment"].Value != null)
                    {
                        if (directoryEntry2.Properties["ServerComment"].Value.ToString().ToLower() == strSiteName.ToLower())
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }
        public static DirectoryEntry CreateSite(SiteObj siteObj)
        {
            DirectoryEntry result;
            try
            {
                if (IISHelper.IsExistSite(siteObj.SiteName))
                {
                    IISHelper.DelSite(siteObj.SiteName);
                }
                DirectoryEntry directoryEntry = new DirectoryEntry("IIS://localhost/w3svc");
                DirectoryEntry directoryEntry2 = directoryEntry.Children.Add(IISHelper.GetNewWebSiteID(), "IIsWebServer");
                directoryEntry2.Properties["ServerComment"].Value = siteObj.SiteName;
                directoryEntry2.Properties["ServerBindings"].Value = string.Format("{0}:{1}:{2}", "", siteObj.Port, "");
                directoryEntry2.Properties["ServerAutoStart"].Value = true;
                DirectoryEntry directoryEntry3 = directoryEntry2.Children.Add("Root", "IIsWebVirtualDir");
                directoryEntry3.Properties["Path"].Value = siteObj.SitePath;
                directoryEntry3.Properties["AccessRead"][0] = true;
                directoryEntry3.Properties["AccessExecute"][0] = false;
                directoryEntry3.Properties["AccessWrite"][0] = false;
                directoryEntry3.Properties["AccessScript"][0] = true;
                directoryEntry3.Properties["EnableDirBrowsing"][0] = false;
                directoryEntry3.Properties["DefaultDoc"][0] = "UserLogin.aspx,Default.aspx";
                directoryEntry3.Properties["AppPoolId"][0] = siteObj.SiteName;
                directoryEntry3.Properties["AspEnableParentPaths"][0] = true;
                if (IISHelper.GetIISMajorVersion() < 7)
                {
                    string text = ".asp,[system32]\\asp.dll,5,GET,HEAD,POST,TRACE\r\n.cer,[system32]\\asp.dll,5,GET,HEAD,POST,TRACE\r\n.cdx,[system32]\\asp.dll,5,GET,HEAD,POST,TRACE\r\n.asa,[system32]\\asp.dll,5,GET,HEAD,POST,TRACE\r\n.idc,[system32]\\httpodbc.dll,5,GET,POST\r\n.shtm,[system32]\\ssinc.dll,5,GET,POST\r\n.shtml,[system32]\\ssinc.dll,5,GET,POST\r\n.stm,[system32]\\ssinc.dll,5,GET,POST\r\n.asax,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.ascx,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.ashx,[netpath]\\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG\r\n.asmx,[netpath]\\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG\r\n.aspx,[netpath]\\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG\r\n.axd,[netpath]\\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG\r\n.vsdisco,[netpath]\\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG\r\n.rem,[netpath]\\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG\r\n.soap,[netpath]\\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG\r\n.config,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.cs,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.csproj,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.vb,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.vbproj,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.webinfo,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.licx,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.resx,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.resources,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.xoml,[netpath]\\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG\r\n.rules,[netpath]\\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG\r\n.master,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.skin,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.compiled,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.browser,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.mdb,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.jsl,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.vjsproj,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.sitemap,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.msgx,[netpath]\\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG\r\n.ad,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.dd,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.ldd,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.sd,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.cd,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.adprototype,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.lddprototype,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.sdm,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.sdmDocument,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.ldb,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.svc,[netpath]\\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG\r\n.mdf,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.ldf,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.java,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.exclude,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG\r\n.refresh,[netpath]\\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG";
                    string newValue = Environment.SystemDirectory + "\\inetsrv";
                    string text2 = RuntimeEnvironment.GetRuntimeDirectory().TrimEnd(new char[]
                    {
                        '\\'
                    });
                    if (IISHelper.GetOSBit() == 64 && text2.IndexOf("Framework64") == -1)
                    {
                        text2 = text2.Replace("Framework", "Framework64");
                    }
                    text = text.Replace("[system32]", newValue).Replace("[netpath]", text2);
                    string[] value = Regex.Split(text, "\r\n");
                    directoryEntry3.Properties["ScriptMaps"].Value = value;
                    directoryEntry3.Properties["AppFriendlyName"][0] = "默认应用程序";
                    directoryEntry3.Invoke("AppCreate", new object[]
                    {
                        true
                    });
                }
                directoryEntry3.CommitChanges();
                directoryEntry2.CommitChanges();
                result = directoryEntry3;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool DelSite(string WebSiteName)
        {
            bool result;
            try
            {
                string webSiteNumByName = IISHelper.GetWebSiteNumByName(WebSiteName);
                if (webSiteNumByName == null)
                {
                    result = false;
                }
                else
                {
                    DirectoryEntry directoryEntry = new DirectoryEntry("IIS://localhost/W3SVC");
                    DirectoryEntry entry = new DirectoryEntry();
                    directoryEntry.RefreshCache();
                    entry = directoryEntry.Children.Find(webSiteNumByName, "IIsWebServer");
                    directoryEntry.Children.Remove(entry);
                    directoryEntry.CommitChanges();
                    directoryEntry.Close();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static Dictionary<string, int> GetAppPoolName()
        {
            int value = util.Is64BitProcess() ? 64 : 32;
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            try
            {
                DirectoryEntry directoryEntry = new DirectoryEntry("IIS://localhost/W3SVC/AppPools");
                foreach (DirectoryEntry directoryEntry2 in directoryEntry.Children)
                {
                    if (directoryEntry2.Properties.Contains("Enable32BitAppOnWin64") && directoryEntry2.Properties["Enable32BitAppOnWin64"].Value.Equals(true))
                    {
                        dictionary.Add(directoryEntry2.Name, 32);
                    }
                    else
                    {
                        dictionary.Add(directoryEntry2.Name, value);
                    }
                }
            }
            catch
            {
            }
            return dictionary;
        }
        public static bool IsAppPoolName(string AppPoolName)
        {
            bool result = false;
            DirectoryEntry directoryEntry = new DirectoryEntry("IIS://localhost/W3SVC/AppPools");
            foreach (DirectoryEntry directoryEntry2 in directoryEntry.Children)
            {
                if (directoryEntry2.Name.ToLower() == AppPoolName.ToLower())
                {
                    result = true;
                }
            }
            return result;
        }
        public static bool DeleteAppPool(string AppPoolName)
        {
            bool result = false;
            DirectoryEntry directoryEntry = new DirectoryEntry("IIS://localhost/W3SVC/AppPools");
            foreach (DirectoryEntry directoryEntry2 in directoryEntry.Children)
            {
                if (directoryEntry2.Name.ToLower() == AppPoolName.ToLower())
                {
                    try
                    {
                        directoryEntry2.DeleteTree();
                        result = true;
                    }
                    catch
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
        public static bool CreateAppPool(string sAppPoolName, bool Enable32BitAppOnWin64, int OSBit)
        {
            bool result;
            try
            {
                if (IISHelper.IsAppPoolName(sAppPoolName))
                {
                    IISHelper.DeleteAppPool(sAppPoolName);
                }
                DirectoryEntry directoryEntry = new DirectoryEntry("IIS://localhost/W3SVC/AppPools");
                DirectoryEntry directoryEntry2 = directoryEntry.Children.Add(sAppPoolName, "IIsApplicationPool");
                directoryEntry2.Properties["AppPoolIdentityType"].Value = 2;
                if (IISHelper.GetIISMajorVersion() > 6)
                {
                    if (Enable32BitAppOnWin64 && OSBit == 64)
                    {
                        directoryEntry2.Properties["Enable32BitAppOnWin64"].Value = true;
                    }
                    directoryEntry2.Properties["ManagedRuntimeVersion"].Value = "v2.0";
                    directoryEntry2.Properties["ManagedPipelineMode"].Value = 1;
                }
                directoryEntry2.CommitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool RecycleAppPool(string AppPoolName)
        {
            bool result;
            try
            {
                DirectoryEntry directoryEntry = new DirectoryEntry("IIS://localhost/W3SVC/AppPools");
                DirectoryEntry directoryEntry2 = directoryEntry.Children.Find(AppPoolName, "IIsApplicationPool");
                directoryEntry2.Invoke("Recycle", null);
                directoryEntry.CommitChanges();
                directoryEntry.Close();
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public static bool AddMime()
        {
            return true;
        }
        public static bool AddScript()
        {
            return true;
        }
        public static string GetDefaultSitePath(string siteName)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            string arg = "C:\\";
            long num = 0L;
            DriveInfo[] array = drives;
            for (int i = 0; i < array.Length; i++)
            {
                DriveInfo driveInfo = array[i];
                if (driveInfo.IsReady && driveInfo.DriveType == DriveType.Fixed)
                {
                    if (driveInfo.TotalFreeSpace > num)
                    {
                        num = driveInfo.TotalFreeSpace;
                        arg = driveInfo.Name;
                    }
                }
            }
            return string.Format("{0}Mysoft\\{1}", arg, siteName);
        }
        public static int GetOSBit()
        {
            int result;
            try
            {
                string s = string.Empty;
                ConnectionOptions options = new ConnectionOptions();
                ManagementScope scope = new ManagementScope("\\\\localhost", options);
                ObjectQuery query = new ObjectQuery("select AddressWidth from Win32_Processor");
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
                using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        ManagementObject managementObject = (ManagementObject)enumerator.Current;
                        s = managementObject["AddressWidth"].ToString();
                    }
                }
                if (int.Parse(s) == 32)
                {
                    result = 32;
                }
                else
                {
                    result = 64;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = 32;
            }
            return result;
        }
        public static bool CheckIISSiteName(string siteName, out string errorMsg)
        {
            bool result;
            if (string.IsNullOrEmpty(siteName) || string.IsNullOrEmpty(siteName.Trim()))
            {
                errorMsg = "站点名称不能为空。";
                result = false;
            }
            else
            {
                Regex regex = new Regex("^[A-Za-z0-9_()-]+$");
                if (!regex.IsMatch(siteName))
                {
                    errorMsg = "站点名称只能包含英文、数字、下划线、小括号和横杠。";
                    result = false;
                }
                else
                {
                    regex = new Regex("^[A-Za-z0-9]+\\S+$");
                    if (!regex.IsMatch(siteName))
                    {
                        errorMsg = "站点名称必须以字母或数字开头。";
                        result = false;
                    }
                    else
                    {
                        errorMsg = string.Empty;
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
