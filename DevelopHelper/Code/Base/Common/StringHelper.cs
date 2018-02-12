using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class StringHelper
    {
        /// <summary>
        /// 取得MD5加密串
        /// </summary>
        /// <param name="input">源明文字符串</param>
        /// <param name="encoding">编码方式，默认UTF-8</param>
        /// <returns>密文字符串</returns>
        public static string GetMD5Hash(string input, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bs = encoding.GetBytes(input);
            bs = md5.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToUpper());
            }
            string password = s.ToString();
            return password;
        }

        /// <summary>
        /// 取得SHA1加密串
        /// </summary>
        /// <param name="input">源明文字符串</param>
        /// <param name="encoding">编码方式，默认UTF-8</param>
        /// <returns>密文字符串</returns>
        public static string GetSHA1Hash(string input, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            SHA1CryptoServiceProvider md5 = new SHA1CryptoServiceProvider();
            byte[] bs = encoding.GetBytes(input);
            bs = md5.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToUpper());
            }
            string password = s.ToString();
            return password;
        }
    }
}
