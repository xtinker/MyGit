using System;
using MODI;
using Image = MODI.Image;

namespace ImageOCR
{
    /// <summary>
    /// 影像识别类
    /// </summary>
    public class OCR
    {
        /// <summary>
        /// 影像OCR方法
        /// </summary>
        /// <param name="path">影像存放的目录路径（包括影像名称）</param>
        /// <param name="value">识别后的字符串</param>
        /// <returns>是否识别</returns>
        public static bool ImageToOCR(string path, ref string value)
        {
            bool flag = true;
            var modiDocument = new Document();
            try
            {
                modiDocument.Create(path);

                modiDocument.OCR(MiLANGUAGES.miLANG_CHINESE_SIMPLIFIED, false, false);
                var mage = modiDocument.Images[0] as Image;
                if (mage != null)
                {
                    value = mage.Layout.Text;
                }

                modiDocument.Save();
            }
            catch (Exception)
            {
                flag = false;
            }
            finally
            {
                modiDocument.Close();
            }

            return flag;
        }
    }
}
