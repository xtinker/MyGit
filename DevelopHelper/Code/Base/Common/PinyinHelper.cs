using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.International.Converters.PinYinConverter;

namespace Common
{
    public static class PinyinHelper
    {
        /// <summary>
        /// 获取拼音
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>拼音</returns>
        public static string GetPinyin(string str)
        {
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                list2.Clear();
                try
                {
                    ChineseChar chineseChar = new ChineseChar(ch);
                    ReadOnlyCollection<string> pinyins = chineseChar.Pinyins;
                    foreach (string current in pinyins)
                    {
                        if (!string.IsNullOrEmpty(current))
                        {
                            string item = current.Substring(0, current.Length - 1);
                            if (!list2.Contains(item))
                            {
                                list2.Add(item);
                            }
                        }
                    }
                }
                catch
                {
                    list2.Add(ch.ToString());
                }
                list = GetMultiplyList(new List<string>(list), new List<string>(list2));
            }
            return string.Join(",", list.ToArray());
        }

        /// <summary>
        /// 获取汉字首拼
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>首拼字符串</returns>
        public static string GetFirstPinyin(string str)
        {
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                list2.Clear();
                try
                {
                    ChineseChar chineseChar = new ChineseChar(ch);
                    ReadOnlyCollection<string> pinyins = chineseChar.Pinyins;
                    foreach (string current in pinyins)
                    {
                        if (!string.IsNullOrEmpty(current))
                        {
                            string item = current.Substring(0, 1);
                            if (!list2.Contains(item))
                            {
                                list2.Add(item);
                            }
                        }
                    }
                }
                catch
                {
                    list2.Add(ch.ToString());
                }
                list = GetMultiplyList(new List<string>(list), new List<string>(list2));
            }
            return string.Join(",", list.ToArray());
        }


        private static List<string> GetMultiplyList(List<string> arr1, List<string> arr2)
        {
            List<string> list = new List<string>();
            List<string> result;
            if (arr1.Count == 0)
            {
                result = arr2;
            }
            else if (arr2.Count == 0)
            {
                result = arr1;
            }
            else
            {
                foreach (string current in arr1)
                {
                    foreach (string current2 in arr2)
                    {
                        if (!list.Contains(current + current2))
                        {
                            list.Add(current + current2);
                        }
                    }
                }
                result = list;
            }
            return result;
        }
    }
}
