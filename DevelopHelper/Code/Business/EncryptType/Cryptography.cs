using System;

namespace EncryptType
{
    /// <summary>
    /// 明源密码加解密
    /// </summary>
    public class Cryptography
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="inStr">要加密的字符串</param>
        /// <returns>密文</returns>
        public static string EnCode(string inStr)
        {
            string str = null;
            int length = inStr.Trim(' ').Length;
            int num = length % 3;
            int num2 = length % 9;
            int num3 = length % 5;
            int num4 = num + num3;
            for (int i = 1; i <= length; i++)
            {
                str += ((char)(Convert.ToInt32(inStr[length + 1 - i - 1]) - num4)).ToString();
                if (num4 == num + num3)
                {
                    num4 = num2 + num3;
                }
                else
                {
                    num4 = num + num3;
                }
            }
            return str + new string(' ', inStr.Length - length);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="inStr">要解密的字符串</param>
        /// <returns>明文</returns>
        public static string DeCode(string inStr)
        {
            string str = "";
            int length = inStr.Trim(' ').Length;
            int num = length % 3;
            int num2 = length % 9;
            int num3 = length % 5;
            int num4;
            if (Math.Abs(length / 2.0 - (int)Math.Floor(length / 2.0)) < 0.000001)
            {
                num4 = num2 + num3;
            }
            else
            {
                num4 = num + num3;
            }
            for (int i = 1; i <= length; i++)
            {
                str += ((char)(Convert.ToInt32(inStr[length + 1 - i - 1]) + num4)).ToString();
                if (num4 == num + num3)
                {
                    num4 = num2 + num3;
                }
                else
                {
                    num4 = num + num3;
                }
            }
            return str + new string(' ', inStr.Length - length);
        }
    }
}
