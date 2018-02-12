using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageOCR
{
    /// <summary>
    /// 数字图像处理通用方法
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 将ARGB图像矩阵bm变为2值图像矩阵im
        /// </summary>
        /// <param name="bm">输入的影像</param>
        /// <param name="t"> 最佳阈值</param>
        /// <returns>输出的目标二值矩阵</returns>
        public static byte[,] ToBinary(Bitmap bm, int t)
        {
            var w = bm.Width;
            var h = bm.Height;
            var im = new byte[w, h];
            var data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0;
                //fixed (byte* p = &im[0, 0])
                //{
                //    for (int j = 0; j < h; j++)
                //    {
                //        for (int i = 0; i < w; i++)
                //        {
                //            *(p + j * w + i) = ((ptr[0] + ptr[1] + ptr[2]) / 3) > t ? (byte)0 : (byte)1;//前景色为1，背景色为0
                //            ptr += 3;
                //        }
                //        ptr += data.Stride - w * 3;
                //    }
                //}
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        im[i, j] = ((ptr[0] + ptr[1] + ptr[2]) / 3) > t ? (byte)0 : (byte)1;//前景色为1，背景色为0
                        ptr += 3;
                    }
                    ptr += data.Stride - w * 3;
                }
            }

            bm.UnlockBits(data);

            return im;
        }

        /// <summary>
        /// 将ARGB图像矩阵bm变为2值图像矩阵im
        /// </summary>
        /// <param name="bm">输入的影像</param>
        /// <param name="t"> 最佳阈值</param>
        /// <returns>输出的目标二值矩阵</returns>
        public static byte[,] ToBinary(ref Bitmap bm, int t)
        {
            var w = bm.Width;
            var h = bm.Height;
            var im = new byte[w, h];
            var data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0;
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        var v = ((ptr[0] + ptr[1] + ptr[2]) / 3) > t ? (byte)0 : (byte)1;//前景色为1，背景色为0
                        im[i, j] = v;
                        ptr[2] = v == 0 ? (byte)255 : (byte)0;
                        ptr[1] = v == 0 ? (byte)255 : (byte)0;
                        ptr[0] = v == 0 ? (byte)255 : (byte)0;
                        ptr += 3;
                    }
                    ptr += data.Stride - w * 3;
                }
            }

            bm.UnlockBits(data);

            return im;
        }

        /// <summary>
        /// 将二值矩阵转换为影像
        /// </summary>
        /// <param name="bs">影像的二值矩阵</param>
        /// <returns>输出的目标影像</returns>
        public static Bitmap ToBitmap(byte[,] bs)
        {
            var w = bs.GetLength(0);
            var h = bs.GetLength(1);
            var bm = new Bitmap(w, h);
            BitmapData data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0;
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        ptr[0] = ptr[1] = ptr[2] = bs[i, j] == 1 ? (byte)0 : (byte)255;
                        ptr += 3;
                    }
                    ptr += data.Stride - w * 3;
                }
            }

            bm.UnlockBits(data);

            return bm;
        }

        /// <summary>
        /// 将RGB矩阵转换为影像
        /// </summary>
        /// <param name="im">影像的RGB矩阵</param>
        /// <returns>输出的目标影像</returns>
        public static Bitmap ToBitmap(int[,] im)
        {
            var w = im.GetLength(0);
            var h = im.GetLength(1);
            var bm = new Bitmap(w, h);
            var data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0.ToPointer();
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        ptr[0] = ptr[1] = ptr[2] = (byte)im[i, j];
                        ptr += 3;
                    }
                    ptr += data.Stride - w * 3;
                }
            }

            //unsafe
            //{
            //    var ptr = (byte*)data.Scan0.ToPointer();
            //    for (int j = 0; j < h; j++)
            //    {
            //        for (int i = 0; i < w; i++)
            //        {
            //            var p = ptr + j * data.Stride + i * 3;
            //            p[0] = p[1] = p[2] = (byte)im[i, j];
            //        }
            //    }
            //}

            bm.UnlockBits(data);

            return bm;
        }

        /// <summary>
        /// 初始化影像颜色
        /// </summary>
        /// <param name="bm">要初始化的影像</param>
        /// <param name="c">初始化颜色</param>
        /// <returns>初始化后的影像</returns>
        public static Bitmap GetBitmap(Bitmap bm, int c)
        {
            return getBitmap(bm, new Rectangle(), c);
        }

        /// <summary>
        /// 截取内容区
        /// </summary>
        /// <param name="bm">要截取内容的影像</param>
        /// <param name="rectangle">内容区矩形</param>
        /// <returns>截取后的影像</returns>
        public static Bitmap GetBitmap(Bitmap bm, Rectangle rectangle)
        {
            return getBitmap(bm, rectangle);
        }

        /// <summary>
        /// 截取或初始化影像
        /// </summary>
        /// <param name="bm">要截取或初始化的影像</param>
        /// <param name="rectangle">截取矩形区</param>
        /// <param name="c">初始化颜色（0到255）</param>
        /// <returns>截取或初始化后的影像</returns>
        private static Bitmap getBitmap(Bitmap bm, Rectangle rectangle, int c = -1)
        {
            //根据颜色初始化图像
            if (c != -1)
            {
                var w = bm.Width;
                var h = bm.Height;
                var data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                unsafe
                {
                    var ptr1 = (byte*)data.Scan0.ToPointer();

                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            var p = ptr1 + j * data.Stride + i * 3;
                            p[2] = p[1] = p[0] = (byte)c;
                        }
                    }
                }
                bm.UnlockBits(data);
                return bm;
            }

            //截取图像中的矩形区，并依此生成一个新的图像
            var newBm = new Bitmap(rectangle.Width, rectangle.Height);
            var dataBm = bm.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            var dataNew = newBm.LockBits(new Rectangle(0, 0, rectangle.Width, rectangle.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr1 = (byte*)dataBm.Scan0.ToPointer();
                var ptr2 = (byte*)dataNew.Scan0.ToPointer();

                for (int j = 0; j < rectangle.Height; j++)
                {
                    for (int i = 0; i < rectangle.Width; i++)
                    {
                        var p1 = ptr1 + j * dataBm.Stride + i * 3;
                        var p2 = ptr2 + j * dataNew.Stride + i * 3;
                        p2[2] = p1[2];
                        p2[1] = p1[1];
                        p2[0] = p1[0];
                    }
                }
            }

            bm.UnlockBits(dataBm);
            newBm.UnlockBits(dataNew);

            return newBm;
        }

        /// <summary>
        /// 将数据流转换成Bitmap类型图像
        /// </summary>
        /// <param name="imageData">要转换的数据流</param>
        /// <returns>转换后的图像</returns>
        public static Bitmap BytesToBitmap(byte[] imageData)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream(imageData);
                var bm = (Bitmap)System.Drawing.Image.FromStream(ms);
                return bm;
            }
            finally
            {
                if (ms != null) ms.Close();
            }
        }

        /// <summary>
        /// 将Bitmap类型图像转换成数据流，只取有效部分（没有Stride部分）
        /// </summary>
        /// <param name="bm">要转换的图像</param>
        /// <returns>转换后的数据流</returns>
        public static byte[] BitmapToBytes(Bitmap bm)
        {
            var im = new byte[bm.Width * bm.Height];
            var data = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly,
                                   PixelFormat.Format24bppRgb);
            unsafe
            {
                var ptr = (byte*)data.Scan0.ToPointer();
                for (int j = 0; j < bm.Height; j++)
                {
                    for (int i = 0; i < bm.Width; i++)
                    {
                        im[j * bm.Width + i] = ptr[2];
                        ptr += 3;
                    }
                    ptr += data.Stride - bm.Width * 3;
                }
            }
            bm.UnlockBits(data);
            return im;
        }

    }
}
