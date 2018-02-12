using System;
using System.Collections.Generic;
using System.Reflection;
using System.Data;

namespace Common
{
    public static class TransformExtensions
    {
        /// <summary>
        /// 将实体对象转换为字典
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>转换后的字典</returns>
        public static Dictionary<string, string> ObjectToDictionary<TEntity>(TEntity entity)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Type t = entity.GetType();
            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic)
                {
                    var value = mi.Invoke(entity, new Object[] { });
                    if (value == null)
                        dic.Add(p.Name, null);
                    else
                        dic.Add(p.Name, value.ToString());
                }
            }
            return dic;
        }

        /// <summary>
        /// DataTable对象转换为字典
        /// </summary>
        /// <param name="dt">DataTable对象</param>
        /// <returns>转换后的字典</returns>
        public static List<Dictionary<string, string>> DataTableToDictionary(DataTable dt)
        {
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (DataColumn dc in dt.Columns)
                {
                    var value = dr[dc.ColumnName];
                    if (value == null)
                        dic.Add(dc.ColumnName, null);
                    else
                        dic.Add(dc.ColumnName, value.ToString());
                }
                list.Add(dic);
            }
            return list;
        }

        /// <summary>
        /// 通过身份证号码获取出生日期
        /// </summary>
        /// <param name="cardId">身份证号码，支持15、18位</param>
        /// <returns>出身日期</returns>
        public static DateTime? GetBirthDateByCardID(string cardId)
        {
            if (string.IsNullOrEmpty(cardId))
                return null;

            string dateStr = string.Empty;
            if (cardId.Length == 15)
            {
                dateStr = $"19{dateStr.Substring(6, 2)}-{dateStr.Substring(8, 2)}-{dateStr.Substring(10, 2)} 00:00:00.000";
            }
            else if (cardId.Length == 18)
            {
                dateStr = $"{dateStr.Substring(6, 4)}-{dateStr.Substring(10, 2)}-{dateStr.Substring(12, 2)} 00:00:00.000";
            }

            DateTime birthday;
            var result = DateTime.TryParse(dateStr, out birthday);

            return result && (birthday >= System.Data.SqlTypes.SqlDateTime.MinValue.Value) && (birthday < System.Data.SqlTypes.SqlDateTime.MaxValue.Value) ? birthday : (DateTime?)null;
        }
    }
}
