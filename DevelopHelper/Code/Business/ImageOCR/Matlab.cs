using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageOCR
{
    /// <summary>
    /// 数字图像处理
    /// </summary>
    public class Matlab
    {
        //将ARGB图像序列pixels变为灰度图像矩阵image
        public static int[,] ToGray(ref Bitmap bm)
        {
            var w = bm.Width;
            var h = bm.Height;
            var im = new int[w, h];
            var data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0;
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        //var gray = (byte)((ptr[0] + ptr[1] + ptr[2]) / 3);//平均值法
                        var gray = (byte)(0.299 * ptr[2] + 0.587 * ptr[1] + 0.114 * ptr[0]);//加权法
                        im[i, j] = ptr[0] = ptr[1] = ptr[2] = gray;
                        ptr += 3;
                    }
                    ptr += data.Stride - w * 3;
                }
            }

            bm.UnlockBits(data);

            return im;
        }

        //将ARGB图像序列pixels变为灰度图像矩阵image
        public static int[,] ToGray(Bitmap bm)
        {
            var w = bm.Width;
            var h = bm.Height;
            var im = new int[w, h];
            var data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0;
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        im[i, j] = (byte)((ptr[0] + ptr[1] + ptr[2]) / 3);
                        ptr += 3;
                    }
                    ptr += data.Stride - w * 3;
                }
            }

            bm.UnlockBits(data);

            return im;
        }

        /// <summary>
        /// 根据灰度平均值获取影像二值化的最佳阈值
        /// </summary>
        /// <param name="bm">输入的影像</param>
        /// <returns>最佳阈值</returns>
        public static int GetThreshold(Bitmap bm)
        {
            var sum = 0.0d;
            var w = bm.Width;
            var h = bm.Height;
            var data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0.ToPointer();
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        sum += (ptr + j * data.Stride + i * 3)[2];
                        //var r = (ptr + j*w*3 + (data.Stride - w*3)*j + i*3)[2];
                        //var g = (ptr + j*w*3 + (data.Stride - w*3)*j + i*3)[1];
                        //var b = (ptr + j * w * 3 + (data.Stride - w * 3) * j + i * 3)[0];

                        //sum += ((double)r + g + b)/3;
                    }
                }
            }

            var t = (int)(sum / (w * h) / 2);

            bm.UnlockBits(data);

            return t + 40;//加权，实践证明这样处理的影像更清晰
        }

        /// <summary>
        /// 边界提取
        /// </summary>
        /// <param name="im">输入二值图像</param>
        /// <param name="type">1 表示3X3正文形结构,2 表示菱形结构，3 表示5X5八角形结构</param>
        /// <returns>输出目标图像</returns>
        public static byte[,] Edge(byte[,] im, int type)
        {
            var w = im.GetLength(0);
            var h = im.GetLength(1);
            byte[,] outp = Erode(im, w, h, type);
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    outp[i, j] = (byte)(im[i, j] - outp[i, j]);
                }
            }

            return outp;
        }

        /// <summary>
        /// 二值形态腐蚀
        /// </summary>
        /// <param name="im">输入二值图像</param>
        /// <param name="w">输入图像宽</param>
        /// <param name="h">输入图像高</param>
        /// <param name="type">1 表示3X3正文形结构,2 表示菱形结构,3 表示5X5八角结构,4 空心的3X3结构，用于HMT变换</param>
        /// <returns>输出目标图像</returns>
        public static byte[,] Erode(byte[,] im, int w, int h, int type)
        {
            var outp = new byte[w, h];

            //正文形结构
            switch (type)
            {
                case 1:
                    for (int j = 1; j < h - 1; j++)
                    {
                        for (int i = 1; i < w - 1; i++)
                        {
                            byte s = im[i, j];
                            if (s == 0)
                            {
                                continue;
                            }

                            if (im[i - 1, j - 1] == 0)
                                continue;
                            if (im[i - 1, j] == 0)
                                continue;
                            if (im[i - 1, j + 1] == 0)
                                continue;
                            if (im[i, j - 1] == 0)
                                continue;
                            if (im[i + 1, j - 1] == 0)
                                continue;
                            if (im[i + 1, j] == 0)
                                continue;
                            if (im[i + 1, j + 1] == 0)
                                continue;

                            outp[i, j] = 1;
                        }
                    }
                    break;
                case 2:
                    for (int j = 1; j < h - 1; j++)
                    {
                        for (int i = 1; i < w - 1; i++)
                        {
                            byte s = im[i, j];
                            if (s == 0)
                                continue;
                            if (im[i - 1, j] == 0)
                                continue;
                            if (im[i + 1, j] == 0)
                                continue;
                            if (im[i, j - 1] == 0)
                                continue;
                            if (im[i, j + 1] == 0)
                                continue;

                            outp[i, j] = 1;
                        }
                    }
                    break;
                case 3:
                    for (int j = 2; j < h - 2; j++)
                    {
                        for (int i = 2; i < w - 2; i++)
                        {
                            byte s = im[i, j];
                            if (s == 0)
                                continue;

                            if (im[i - 2, j - 1] == 0) continue;
                            if (im[i - 2, j] == 0) continue;
                            if (im[i - 2, j + 1] == 0) continue;

                            if (im[i - 1, j - 2] == 0) continue;
                            if (im[i - 1, j - 1] == 0) continue;
                            if (im[i - 1, j] == 0) continue;
                            if (im[i - 1, j + 1] == 0) continue;
                            if (im[i - 1, j + 2] == 0) continue;

                            if (im[i, j - 2] == 0) continue;
                            if (im[i, j - 1] == 0) continue;
                            if (im[i, j + 1] == 0) continue;
                            if (im[i, j + 2] == 0) continue;

                            if (im[i + 1, j - 2] == 0) continue;
                            if (im[i + 1, j - 1] == 0) continue;
                            if (im[i + 1, j] == 0) continue;
                            if (im[i + 1, j + 1] == 0) continue;
                            if (im[i + 1, j + 2] == 0) continue;

                            if (im[i + 2, j - 1] == 0) continue;
                            if (im[i + 2, j] == 0) continue;
                            if (im[i + 2, j + 1] == 0) continue;

                            outp[i, j] = 1;
                        }
                    }
                    break;
                case 15:
                    for (int j = 7; j < h - 7; j++)
                    {
                        for (int i = 7; i < w - 7; i++)
                        {
                            bool bf = true;
                            if (im[i, j] == 0) continue;
                            for (int k = -7; k < 8; k++)
                            {
                                for (int l = -7; l < 8; l++)
                                {
                                    if (im[i + k, j + l] == 0)
                                    {
                                        bf = false;
                                        break;
                                    }

                                    if (bf)
                                    {
                                        outp[i, j] = 1;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            return outp;
        }

        /// <summary>
        /// 二值形态膨胀
        /// </summary>
        /// <param name="bm">输入的影像</param>
        /// <param name="im">输入影像的二值矩阵</param>
        /// <param name="type">1 表示3X3正方结构，2 表示菱形结构，3 表示5X5八角结构</param>
        /// <returns></returns>
        public static byte[,] Dilate(Bitmap bm, byte[,] im, int type)
        {
            var w = bm.Width;
            var h = bm.Height;
            var outp = new byte[w, h];

            switch (type)
            {
                //正方结构
                case 1:
                    for (int j = 1; j < h - 1; j++)
                    {
                        for (int i = 1; i < w - 1; i++)
                        {
                            var s = im[i, j];
                            if (s == 1)
                            {
                                outp[i, j] = s;
                                continue;
                            }
                            if (im[i - 1, j - 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i - 1, j] == 1) { outp[i, j] = 1; continue; }
                            if (im[i - 1, j + 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i, j - 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i - 1, j + 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i + 1, j - 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i + 1, j] == 1) { outp[i, j] = 1; continue; }
                            if (im[i + 1, j + 1] == 1) { outp[i, j] = 1; }
                        }
                    }
                    break;
                //菱形结构
                case 2:
                    for (int j = 1; j < h - 1; j++)
                    {
                        for (int i = 1; i < w - 1; i++)
                        {
                            var s = im[i, j];
                            if (s == 1)
                            {
                                outp[i, j] = s;
                                continue;
                            }
                            if (im[i - 1, j] == 1) { outp[i, j] = 1; continue; }
                            if (im[i + 1, j] == 1) { outp[i, j] = 1; continue; }
                            if (im[i, j - 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i, j + 1] == 1) { outp[i, j] = 1; }
                        }
                    }
                    break;
                //5X5八角结构
                case 3:
                    for (int j = 2; j < h - 2; j++)
                    {
                        for (int i = 2; i < w - 2; i++)
                        {
                            var s = im[i, j];
                            if (s == 1)
                            {
                                outp[i, j] = s;
                                continue;
                            }
                            if (im[i - 2, j - 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i - 2, j] == 1) { outp[i, j] = 1; continue; }
                            if (im[i - 2, j + 1] == 1) { outp[i, j] = 1; continue; }

                            if (im[i - 1, j - 2] == 1) { outp[i, j] = 1; continue; }
                            if (im[i - 1, j - 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i - 1, j] == 1) { outp[i, j] = 1; continue; }
                            if (im[i - 1, j + 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i - 1, j + 2] == 1) { outp[i, j] = 1; continue; }

                            if (im[i, j - 2] == 1) { outp[i, j] = 1; continue; }
                            if (im[i, j - 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i, j + 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i, j + 2] == 1) { outp[i, j] = 1; continue; }

                            if (im[i + 1, j - 2] == 1) { outp[i, j] = 1; continue; }
                            if (im[i + 1, j - 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i + 1, j] == 1) { outp[i, j] = 1; continue; }
                            if (im[i + 1, j + 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i + 1, j + 2] == 1) { outp[i, j] = 1; continue; }

                            if (im[i + 2, j - 1] == 1) { outp[i, j] = 1; continue; }
                            if (im[i + 2, j] == 1) { outp[i, j] = 1; continue; }
                            if (im[i + 2, j + 1] == 1) { outp[i, j] = 1; }
                        }
                    }
                    break;
            }

            return outp;
        }

        /// <summary>
        /// 灰度形态腐蚀
        /// </summary>
        /// <param name="im">输入二值图像</param>
        /// <param name="w">输入图像宽</param>
        /// <param name="h">输入图像高</param>
        /// <param name="type">1 不平顶结构,2 平顶结构</param>
        /// <returns>输出目标图像</returns>
        public static int[,] GrayErode(int[,] im, int w, int h, int type)
        {
            int m0 = 0, m1 = 0, m2 = 0;
            var outp = new int[w, h];

            switch (type)
            {
                case 1:
                    m0 = 80;
                    m1 = 10;
                    m2 = 50;
                    break;
                case 2:
                    m0 = 1;
                    m1 = 1;
                    m2 = 1;
                    break;
            }

            int[,] b = { { m2, m1, m2 }, { m1, m0, m1 }, { m2, m1, m2 } };

            for (int j = 1; j < h - 1; j++)
            {
                for (int i = 1; i < w - 1; i++)
                {
                    int min = 255;
                    for (int k = -1; k < 2; k++)
                    {
                        for (int l = -1; l < 2; l++)
                        {
                            int n = im[i + k, j + l] - b[k + 1, l + 1];
                            if (n < min)
                            {
                                min = n;
                            }
                        }
                    }

                    if (min < 0)
                    {
                        min = 0;
                    }
                    outp[i, j] = (byte)min;
                }
            }

            return outp;
        }

        /// <summary>
        /// 灰度形态膨胀
        /// </summary>
        /// <param name="im">输入灰度图像</param>
        /// <param name="w">输入图像宽</param>
        /// <param name="h">输入图像高</param>
        /// <param name="type">1 不平顶结构,2 平顶结构</param>
        /// <returns>输出目标图像</returns>
        public static int[,] GrayDilate(int[,] im, int w, int h, int type)
        {
            int m0 = 0, m1 = 0, m2 = 0;
            var outp = new int[w, h];

            switch (type)
            {
                case 1:
                    m0 = 80;
                    m1 = 10;
                    m2 = 50;
                    break;
                case 2:
                    m0 = 1;
                    m1 = 1;
                    m2 = 1;
                    break;
            }

            int[,] b = { { m2, m1, m2 }, { m1, m0, m1 }, { m2, m1, m2 } };

            for (int j = 1; j < h - 1; j++)
            {
                for (int i = 1; i < w - 1; i++)
                {
                    int max = 0;
                    for (int k = -1; k < 2; k++)
                    {
                        for (int l = -1; l < 2; l++)
                        {
                            int n = im[i - k, j - l] + b[k + 1, l + 1];
                            if (n > max)
                            {
                                max = n;
                            }
                        }
                    }

                    if (max > 255)
                    {
                        max = 255;
                    }

                    outp[i, j] = (byte)max;
                }
            }

            return outp;
        }

        /// <summary>
        /// HMT变换
        /// </summary>
        /// <param name="bm">输入的影像 </param>
        /// <param name="img">输入二值图像</param>
        /// <param name="w">输入图像宽</param>
        /// <param name="h">输入图像高</param>
        /// <returns>输出目标图像</returns>
        public static Bitmap Hmt(Bitmap bm, byte[,] img, int w, int h)
        {
            var outp = new byte[w, h];

            //构造img[,]的余集imgc[][]
            var imgc = new byte[w, h];
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    if (img[i, j] == 1)
                    {
                        imgc[i, j] = 0;
                    }
                    else
                    {
                        imgc[i, j] = 1;
                    }
                }
            }

            //对img[,]用E腐蚀
            var out1 = Erode(img, w, h, 15);

            //对imgc[,]用F腐蚀
            var out2 = Erode(imgc, w, h, 19);

            //求out1和out2的交集
            for (int j = 0; j < h - 1; j++)
            {
                for (int i = 0; i < w - 1; i++)
                {
                    if (out1[i, j] == 1 && out2[i, j] == 1)
                    {
                        outp[i, j] = 1;
                        outp[i - 2, j] = 1;
                        outp[i - 1, j] = 1;
                        outp[i + 1, j] = 1;
                        outp[i + 2, j] = 1;
                        outp[i, j - 2] = 1;
                        outp[i, j - 1] = 1;
                        outp[i, j + 1] = 1;
                        outp[i, j + 2] = 1;
                    }
                }
            }

            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    bm.SetPixel(i, j, outp[i, j] == 1 ? Color.FromArgb(255, 0, 0) : Color.FromArgb(255, 255, 255));
                }
            }

            return bm;
        }

        /// <summary>
        /// 灰度阈值变换
        /// </summary>
        /// <param name="bm">输入的二值图像</param>
        /// <param name="w">输入图像的宽</param>
        /// <param name="h">输入图像的高</param>
        /// <param name="th">阈值</param>
        /// <returns>目标二值图像</returns>
        public static Bitmap Thresh(Bitmap bm, int w, int h, int th)
        {
            BitmapData data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //转变为灰度图像矩阵
            unsafe
            {
                var ptr = (byte*)data.Scan0;
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        ptr[0] = ptr[1] = ptr[2] = ((ptr[0] + ptr[1] + ptr[2]) / 3) > th ? (byte)255 : (byte)0;
                        ptr += 3;
                    }
                    ptr += data.Stride - w * 3;
                }
            }

            bm.UnlockBits(data);

            return bm;
        }

        /// <summary>
        /// 灰度阈值变换
        /// </summary>
        /// <param name="bm">输入的二值图像</param>
        /// <param name="nx">横向分块数 </param>
        /// <param name="ny">纵向分块数 </param>
        /// <returns>目标二值图像</returns>
        public static Bitmap AverThresh(Bitmap bm, int nx, int ny)
        {
            var w = bm.Width;
            var h = bm.Height;
            int x1 = w / nx;//每小块宽
            int x2 = w % nx;//剩余部分宽 0<= x2 < x1
            int y1 = h / ny;//每小块高
            int y2 = h % ny;//剩余部分高 0<= y2 < y1
            BitmapData data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //转变为灰度图像矩阵
            unsafe
            {
                var ptr = (byte*)data.Scan0.ToPointer();
                for (int j = 0; j < h - y2; j = j + y1)
                {
                    for (int i = 0; i < w - x2; i = i + x1)
                    {
                        double sum = 0;
                        for (int k = 0; k < y1; k++)
                        {
                            for (int l = 0; l < x1; l++)
                            {
                                sum += (ptr + (j + k) * data.Stride + (i + l) * 3)[2];
                            }
                        }

                        var ave = (int)(sum / (x1 * y1) / 2);//小块的平均灰度阈值

                        for (int k = 0; k < y1; k++)
                        {
                            for (int l = 0; l < x1; l++)
                            {
                                var p = ptr + (j + k) * data.Stride + (i + l) * 3;
                                p[0] = p[1] = p[2] = ((p[0] + p[1] + p[2]) / 3) > ave ? (byte)255 : (byte)0;
                            }
                        }
                    }

                    if (x2 > 0)
                    {
                        for (int i = x1 * nx - 1; i < w; i++)
                        {
                            var sum = 0;
                            for (int k = 0; k < y1; k++)
                            {
                                for (int l = 0; l < x2; l++)
                                {
                                    sum += (ptr + (j + k) * data.Stride + (i + l) * 3)[2];
                                }
                            }

                            var ave = sum / (x2 * y1) / 2;//小块的平均灰度阈值

                            for (int k = 0; k < y1; k++)
                            {
                                for (int l = 0; l < x2; l++)
                                {
                                    var p = ptr + (j + k) * data.Stride + (i + l) * 3;
                                    p[0] = p[1] = p[2] = ((p[0] + p[1] + p[2]) / 3) > ave ? (byte)255 : (byte)0;
                                }
                            }
                        }
                    }
                }

                if (y2 > 0)
                {
                    for (int i = 0; i < w - x2; i = i + x1)
                    {
                        var sum = 0;
                        for (int k = y1 * ny; k < h; k++)
                        {
                            for (int l = 0; l < x1; l++)
                            {
                                sum += (ptr + k * data.Stride + (i + l) * 3)[2];
                            }
                        }

                        var ave = sum / (x1 * y2) / 2;//小块的平均灰度阈值

                        for (int k = y1 * ny; k < h; k++)
                        {
                            for (int l = 0; l < x1; l++)
                            {
                                var p = ptr + k * data.Stride + (i + l) * 3;
                                p[0] = p[1] = p[2] = ((p[0] + p[1] + p[2]) / 3) > ave ? (byte)255 : (byte)0;
                            }
                        }
                    }

                    if (x2 > 0)
                    {
                        var sum = 0;
                        for (int k = y1 * ny; k < h; k++)
                        {
                            for (int l = x1 * nx; l < x2; l++)
                            {
                                sum += (ptr + k * data.Stride + l * 3)[2];
                            }
                        }

                        var ave = sum / (x2 * y2) / 2;//小块的平均灰度阈值

                        for (int k = y1 * ny; k < h; k++)
                        {
                            for (int l = x1 * nx; l < x2; l++)
                            {
                                var p = ptr + k * data.Stride + l * 3;
                                p[0] = p[1] = p[2] = ((p[0] + p[1] + p[2]) / 3) > ave ? (byte)255 : (byte)0;
                            }
                        }
                    }
                }
            }

            bm.UnlockBits(data);

            return bm;
        }

        /// <summary>
        /// 根据灰度平均值获取影像二值化的最佳阈值
        /// </summary>
        /// <param name="bm">输入的影像</param>
        /// <param name="rgb"> </param>
        /// <returns>最佳阈值</returns>
        public static int GetThresholdByOneComponent(Bitmap bm, string rgb)
        {
            var sum = 0.0d;
            var w = bm.Width;
            var h = bm.Height;
            var data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0.ToPointer();
                var n = 0;
                switch (rgb)
                {
                    case "R":
                        n = 2;
                        break;
                    case "G":
                        n = 1;
                        break;
                    case "B":
                        n = 0;
                        break;
                }

                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        sum += (ptr + j * data.Stride + i * 3)[n];
                    }
                }
            }

            var t = (int)(sum / (w * h) / 2);

            bm.UnlockBits(data);

            return t + 100;
        }

        /// <summary>
        /// 线性灰度变换
        /// </summary>
        /// <param name="bm">输入图像</param>
        /// <param name="w">输入图像的宽</param>
        /// <param name="h">输入图像的高</param>
        /// <param name="p">钭率</param>
        /// <param name="q">截距</param>
        /// <returns>目标图像</returns>
        public static Bitmap Linetrans(Bitmap bm, int w, int h, double p, int q)
        {
            BitmapData data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0;
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        //增加图像亮度
                        var r = (int)(p * ptr[2] + q);
                        var g = (int)(p * ptr[1] + q);
                        var b = (int)(p * ptr[0] + q);

                        if (r >= 255) r = 255;
                        if (g >= 255) g = 255;
                        if (b >= 255) b = 255;

                        ptr[0] = (byte)b;
                        ptr[1] = (byte)g;
                        ptr[2] = (byte)r;

                        ptr += 3;
                    }
                    ptr += data.Stride - w * 3;
                }
            }

            bm.UnlockBits(data);

            return bm;
        }

        /// <summary>
        /// 伪彩色处理
        /// </summary>
        /// <param name="bm">输入的图像</param>
        /// <param name="w">输入图像的宽</param>
        /// <param name="h">输入图像的高</param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns>目标图像</returns>
        public static Bitmap FalseColorTrans(Bitmap bm, int w, int h, byte p, byte q)
        {
            BitmapData data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0;
                var stride = data.Stride;
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        //b变换
                        var cp = ptr + j * stride + i * 3;
                        if (cp[0] < p)
                        {
                            cp[0] = 255;
                        }
                        else if (cp[0] >= p && cp[0] < q)
                        {
                            cp[0] = (byte)((q - cp[0]) * 255 / (q - p));
                        }
                        else
                        {
                            cp[0] = 0;
                        }

                        //g变换
                        if (cp[1] < p)
                        {
                            cp[1] = (byte)(cp[1] * 255 / p);
                        }
                        else if (cp[1] >= p && cp[1] < q)
                        {
                            cp[1] = 255;
                        }
                        else
                        {
                            cp[1] = (byte)((255 - cp[1]) * 255 / (255 - q));
                        }

                        //r变换
                        if (cp[2] < p)
                        {
                            cp[2] = 0;
                        }
                        else if (cp[2] >= p && cp[2] < q)
                        {
                            cp[2] = (byte)((cp[2] - p) * 255 / (q - p));
                        }
                        else
                        {
                            cp[2] = 255;
                        }
                    }
                }
            }

            bm.UnlockBits(data);

            return bm;
        }

        /// <summary>
        /// 图像融合
        /// </summary>
        /// <param name="bm1">图像一</param>
        /// <param name="bm2">图像二</param>
        /// <param name="w">图像的宽度</param>
        /// <param name="h">图像的高度</param>
        /// <returns>融合后的图片</returns>
        public static Bitmap Combine(Bitmap bm1, Bitmap bm2, int w, int h)
        {
            const float _p = 0.5f;
            const float _q = 0.5f;
            BitmapData data1 = bm1.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData data2 = bm2.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite,
                                            PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr1 = (byte*)data1.Scan0;
                var ptr2 = (byte*)data2.Scan0;
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        ptr2[0] = (byte)(_p * ptr1[0] + _q * ptr2[0]);
                        ptr2[1] = (byte)(_p * ptr1[1] + _q * ptr2[1]);
                        ptr2[2] = (byte)(_p * ptr1[2] + _q * ptr2[2]);
                        ptr1 += 3;
                        ptr2 += 3;
                    }
                    ptr1 += data1.Stride - w * 3;
                    ptr2 += data2.Stride - w * 3;
                }
            }

            bm1.UnlockBits(data1);
            bm2.UnlockBits(data2);

            return bm2;
        }

        /// <summary>
        /// 自上而下，从左到右搜索边界点和左下角边界起始点（startX,startY）
        /// </summary>
        /// <param name="bw">输入的二值图像</param>
        /// <param name="w">输入图像的宽</param>
        /// <param name="h">输入图像的高</param>
        /// <returns>起始点</returns>
        public static int[] FindStart(byte[,] bw, int w, int h)
        {
            var st = new int[2];
            int startX = 0, startY = 255;
            for (int j = 0; j < h; j++)
            {
                for (int i = w - 1; i >= 0; i--)
                {
                    if (bw[i, j] == 1)
                    {
                        //储存边界起始点
                        startX = i;
                        startY = j;

                        //跳出循环
                        i = -1;
                        j = w;
                    }
                }
            }
            st[0] = startX;
            st[1] = startY;

            return st;
        }

        /// <summary>
        /// 轮廓跟踪
        /// </summary>
        /// <param name="bw">二值图像距阵</param>
        /// <param name="startX">最左下方边界点X轴</param>
        /// <param name="startY">最左下方边界点Y轴</param>
        /// <param name="w">二值图像宽</param>
        /// <param name="h">二值图像高</param>
        /// <returns>规范链码起始点坐标</returns>
        public static int[] OutLine(byte[,] bw, int startX, int startY, int w, int h)
        {
            //边界起点和所有边界点数组
            var nstart = new int[2];

            var nCurrX = new int[h * w / 4];
            var nCurrY = new int[h * w / 4];
            var codes = new int[h * w / 4];//边界链码
            var ncodes = new int[h * w / 4];//规范链码

            //8个方向和起始扫描方向
            int[,] director = { { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 } };

            //起始扫描按逆时针方向进行
            int currDirect = 0;

            //跟踪边界
            bool bFindStartPoint = false;

            //从初始点（startX,startY）开始扫描
            var inum = 0;
            nCurrY[0] = startY;
            nCurrX[0] = startX;

            //按逆时针方向搜索边界点
            while (!bFindStartPoint)
            {
                bool bFindPoint = false;

                while (!bFindPoint)
                {
                    int u = nCurrY[inum] + director[currDirect, 1];
                    if (u < 0)
                    {
                        u = 0;
                    }
                    if (u > 255)
                    {
                        u = 255;
                    }

                    int v = nCurrX[inum] + director[currDirect, 0];
                    if (v < 0)
                    {
                        v = 0;
                    }
                    if (v > 255)
                    {
                        v = 255;
                    }

                    //找到下一个边界点
                    if (bw[v, u] == 1)
                    {
                        bFindPoint = true;

                        //存储链码
                        codes[inum] = currDirect;

                        //存储坐标

                        nCurrY[inum + 1] = nCurrY[inum] + director[currDirect, 1];
                        nCurrX[inum + 1] = nCurrX[inum] + director[currDirect, 0];

                        inum++;

                        //检查结束搜索条件
                        if (nCurrY[inum] == startY && nCurrX[inum] == startX)
                        {
                            bFindStartPoint = true;
                        }

                        //扫描的方向逆时针旋转90度
                        currDirect--;
                        currDirect--;
                        if (currDirect < 0)
                        {
                            currDirect += 8;
                        }
                    }
                    else
                    {
                        //扫描方向顺时针旋转45度
                        currDirect++;
                        if (currDirect == 8)
                        {
                            currDirect = 0;
                        }
                    }
                }
            }

            //计算规范码
            int maxI = 0;
            for (int i = 0; i < inum; i++)
            {
                if (codes[i] == 0)
                {
                    int temI = i;
                    int n = 1;
                    while (codes[i + 1] == 0)
                    {
                        n++;
                        i++;
                    }
                    if (n > maxI)
                    {
                        maxI = temI;
                    }
                }
            }

            for (int i = 0; i < inum - maxI; i++)
            {
                ncodes[i] = codes[maxI + i];
            }
            for (int i = 0; i < maxI; i++)
            {
                ncodes[inum - maxI + i] = codes[i];
            }

            //返回规范链码的起始点坐标
            return nstart;
        }

        /// <summary>
        /// 边界检测方法
        /// </summary>
        /// <param name="bm">输入的影像 </param>
        /// <param name="bw">输入的二值图像</param>
        /// <param name="w">输入的图像宽度</param>
        /// <param name="h">输入的图像高度</param>
        /// <returns>输出的目标图像</returns>
        public static Bitmap BoundDetect(Bitmap bm, byte[,] bw, int w, int h)
        {
            int r;
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    r = bw[i, j];
                    bm.SetPixel(i, j, r == 1 ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255, 255, 255));
                }
            }

            for (int j = 1; j < h - 1; j++)
            {
                for (int i = 1; i < w - 1; i++)
                {
                    int p = bw[i, j];

                    if (p == 0) //如果当前像素是白色，进入下一个循环
                    {
                        continue;
                    }

                    r = 1;
                    for (int k = -1; k < 2; k++)
                    {
                        for (int l = -1; l < 2; l++)
                        {
                            if (bw[i + k, j + l] == 0)
                            {
                                r = 0;
                                k = 2;
                                l = 2;//跳出二重循环
                            }
                        }
                    }

                    //如果都是黑点，判定为内部点，改变颜色
                    if (r == 1)
                    {
                        bm.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                }
            }

            return bm;
        }

        /// <summary>
        /// 消除小洞方法
        /// </summary>
        /// <param name="bm">输入的影像 </param>
        /// <param name="bw">输入的二值图像</param>
        /// <param name="nMinArea">小洞的阈值面积像素数</param>
        /// <returns>目标图像</returns>
        public static Bitmap DelHole(Bitmap bm, byte[,] bw, int nMinArea)
        {
            var w = bm.Width;
            var h = bm.Height;
            int s, n;

            //空穴的数目以及面积阈值
            int nBlackPix;

            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    bw[i, j] = (byte)(-bw[i, j]);
                }
            }

            //空穴数赋初值
            int nHoleNum = 1;
            bool oflag = true;//外层while循环标志

            while (oflag)
            {
                s = 0;

                //寻找每个空穴的初始像素值
                for (int j = 1; j < h - 1; j++)
                {
                    for (int i = 1; i < w - 1; i++)
                    {
                        if (bw[i, j] == -1)
                        {
                            s = 1;
                            //将像素值改成当前的空穴数值
                            bw[i, j] = (byte)nHoleNum;

                            //跳出循环
                            i = w;
                            j = h;
                        }
                    }
                }

                bool iflag = true;//内层while循环标志

                if (s == 0) //没有初始像素，跳出循环
                {
                    oflag = false;
                }
                else
                {
                    while (iflag)
                    {
                        //正向和反向传播系数赋初值0
                        int nDir1 = 0;
                        int nDir2 = 0;

                        //正向扫描
                        for (int j = 1; j < h - 1; j++)
                        {
                            for (int i = 1; i < w - 1; i++)
                            {
                                nBlackPix = bw[i, j];

                                //如果像素已被扫描，或为背景色，进行下一个循环
                                if (nBlackPix != -1)
                                    continue;

                                //如果上侧或者左侧的像素值已被扫描，且属于当前的空穴，当前的像素值改成空穴的数值
                                nBlackPix = bw[i - 1, j];
                                if (nBlackPix == nHoleNum)
                                {
                                    bw[i, j] = (byte)nHoleNum;
                                    nDir1 = 1;
                                    continue;
                                }

                                nBlackPix = bw[i, j - 1];
                                if (nBlackPix == nHoleNum)
                                {
                                    bw[i, j] = (byte)nHoleNum;
                                    nDir1 = 1;
                                }
                            }
                        }

                        //正向像素全部被扫描，跳出循环
                        if (nDir1 == 0)
                        {
                            iflag = false;
                        }

                        //反向扫描
                        for (int j = h - 2; j >= 1; j--)
                        {
                            for (int i = w - 2; i >= 1; i--)
                            {
                                nBlackPix = bw[i, j];

                                //如果像素已经被扫描，或者是背景色，进行下一个循环
                                if (nBlackPix != -1)
                                    continue;

                                //如果下侧或者右侧的像素值已被扫描，且属于当前的空穴，当前的像素值改成空穴的数值
                                nBlackPix = bw[i + 1, j];
                                if (nBlackPix == nHoleNum)
                                {
                                    bw[i, j] = (byte)nHoleNum;
                                    nDir2 = 1;
                                    continue;
                                }

                                nBlackPix = bw[i, j + 1];
                                if (nBlackPix == nHoleNum)
                                {
                                    bw[i, j] = (byte)nHoleNum;
                                    nDir2 = 1;
                                }
                            }
                        }

                        if (nDir2 == 0)
                        {
                            iflag = false;
                        }
                    }
                }
                //空穴数增加
                nHoleNum++;
            }
            nHoleNum--;

            //寻找面积小于阈值的空穴区域
            for (n = 1; n <= nHoleNum; n++)
            {
                s = 0;

                for (int j = 0; j < h - 1; j++)
                {
                    for (int i = 0; i < w - 1; i++)
                    {
                        nBlackPix = bw[i, j];

                        if (nBlackPix == n)
                        {
                            s++;
                        }

                        //如果区域面积已经大于阈值，跳出循环
                        if (s > nMinArea)
                            break;
                    }
                }

                //小于阈值的区域，赋以与背景一样的颜色，进行消去
                if (s <= nMinArea)
                {
                    for (int j = 0; j < h - 1; j++)
                    {
                        for (int i = 0; i < w - 1; i++)
                        {
                            nBlackPix = bw[i, j + 1];

                            if (nBlackPix == n)
                                bw[i, j + 1] = 0;
                        }
                    }
                }
            }

            BitmapData data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0;
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        ptr[0] = ptr[1] = ptr[2] = bw[i, j] == 0 ? (byte)255 : (byte)0;
                        ptr += 3;
                    }
                    ptr += data.Stride - w * 3;
                }
            }

            bm.UnlockBits(data);

            return bm;
        }

        /// <summary>
        /// 获取直方图
        /// </summary>
        /// <param name="bm">输入的影像</param>
        /// <returns>直方图统计数据</returns>
        public static int[] GetHist(Bitmap bm)
        {
            var w = bm.Width;
            var h = bm.Height;
            var hist = new int[256];

            var data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                var ptr = (byte*)data.Scan0.ToPointer();
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        var gray = ptr[2];
                        hist[gray]++;
                        ptr += 3;
                    }
                    ptr += data.Stride - w * 3;
                }
            }
            bm.UnlockBits(data);

            return hist;
        }

        /// <summary>
        /// 冒泡排序，输出中值
        /// </summary>
        /// <param name="dt">采样范围中的灰度值数组</param>
        /// <param name="m">采样范围</param>
        /// <returns></returns>
        public static int[] MedianSorter(int[] dt, int m)
        {
            for (int i = m - 1; i >= 1; i--)
            {
                for (int j = 1; j < i; j++)
                {
                    if (dt[j - 1] > dt[j])
                    {
                        int tem = dt[j];
                        dt[j] = dt[j - 1];
                        dt[j - 1] = tem;
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// 计算形心
        /// </summary>
        /// <param name="bm">输入的影像</param>
        /// <param name="im">输入影像的二值矩阵</param>
        /// <param name="centerxy">形心坐标 </param>
        /// <returns>绘出形心的影像</returns>
        public static Bitmap Center(Bitmap bm, byte[,] im, ref int[] centerxy)
        {
            var w = bm.Width;
            var h = bm.Height;
            long m00 = 0;
            long m10 = 0;
            long m01 = 0;

            //计算0-1阶矩
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    if (im[i, j] == 0) continue;

                    //计算0阶矩
                    m00 += im[i, j];

                    //计算1阶矩
                    m10 += i * im[i, j];
                    m01 += j * im[i, j];
                }
            }

            //计算形心坐标
            centerxy[0] = (int)(m10 / m00);
            centerxy[1] = (int)(m01 / m00);

            var data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0.ToPointer();
                //用红色小方块标注形心
                for (int i = -9; i < 10; i++)
                {
                    for (int j = -9; j < 10; j++)
                    {
                        var p = ptr + (centerxy[1] + j) * data.Stride +
                                (centerxy[0] + i) * 3;
                        p[0] = 0;
                        p[1] = 0;
                        p[2] = 255;
                    }
                }
            }

            bm.UnlockBits(data);
            return bm;
        }

        /// <summary>
        /// 计算形心
        /// </summary>
        /// <param name="bm">输入的影像</param>
        /// <param name="im">输入影像的二值矩阵</param>
        /// <returns>形心坐标</returns>
        public static int[] Center(Bitmap bm, byte[,] im)
        {
            var centerxy = new int[2];
            var w = bm.Width;
            var h = bm.Height;
            long m00 = 0;
            long m10 = 0;
            long m01 = 0;

            //计算0-1阶矩
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    if (im[i, j] == 0) continue;

                    //计算0阶矩
                    m00 += im[i, j];

                    //计算1阶矩
                    m10 += i * im[i, j];
                    m01 += j * im[i, j];
                }
            }

            //计算形心坐标
            centerxy[0] = (int)(m10 / m00);
            centerxy[1] = (int)(m01 / m00);

            return centerxy;
        }

        /// <summary>
        /// 细化方法一
        /// </summary>
        /// <param name="im">输入影像的二值矩阵</param>
        /// <returns>细化后的影像二值矩阵</returns>
        public static byte[,] Thinner1(byte[,] im)
        {
            var w = im.GetLength(0);
            var h = im.GetLength(1);

            //5X5像素块
            var neighbour = new byte[5, 5];

            var bwTem = new byte[w, h];

            bool bModified = true;

            //细化循环开始
            while (bModified)
            {
                bModified = false;

                for (int j = 2; j < h - 2; j++)
                {
                    for (int i = 2; i < w - 2; i++)
                    {
                        bool bCondition4 = false;//细化条件4标志

                        if (im[i, j] == 0) continue;

                        for (int k = 0; k < 5; k++)
                        {
                            //取以当前点为中心的5X5块
                            for (int l = 0; l < 5; l++)
                            {
                                //1代表黑色，0代表白色
                                neighbour[k, l] = im[i + k - 2, j + l - 2];
                            }
                        }

                        //(1)判断条件2<=n(p)<=6
                        int nCount = neighbour[1, 1] + neighbour[1, 2] + neighbour[1, 3] + neighbour[2, 1] +
                                     neighbour[2, 3] + neighbour[3, 1] + neighbour[3, 2] + neighbour[3, 3];

                        if (nCount >= 2 && nCount <= 6)
                        {
                        }
                        else
                        {
                            bwTem[i, j] = 1;
                            continue;//跳过
                        }

                        //(2)判断s(p)=1
                        nCount = 0;
                        if (neighbour[2, 3] == 0 && neighbour[1, 3] == 1) nCount++;
                        if (neighbour[1, 3] == 0 && neighbour[1, 2] == 1) nCount++;
                        if (neighbour[1, 2] == 0 && neighbour[1, 1] == 1) nCount++;
                        if (neighbour[1, 1] == 0 && neighbour[2, 1] == 1) nCount++;
                        if (neighbour[2, 1] == 0 && neighbour[3, 1] == 1) nCount++;
                        if (neighbour[3, 1] == 0 && neighbour[3, 2] == 1) nCount++;
                        if (neighbour[3, 2] == 0 && neighbour[3, 3] == 1) nCount++;
                        if (neighbour[3, 3] == 0 && neighbour[2, 3] == 1) nCount++;

                        if (nCount != 1)
                        {
                            bwTem[i, j] = 1;
                            continue;
                        }

                        //(3)判断p0*p2*p4 = 0 or s(p2)!=1
                        if (neighbour[2, 3] * neighbour[1, 2] * neighbour[2, 1] == 0)
                        {
                        }
                        else
                        {
                            nCount = 0;
                            if (neighbour[0, 2] == 0 && neighbour[0, 1] == 1) nCount++;
                            if (neighbour[0, 1] == 0 && neighbour[1, 1] == 1) nCount++;
                            if (neighbour[1, 1] == 0 && neighbour[2, 1] == 1) nCount++;
                            if (neighbour[2, 1] == 0 && neighbour[2, 2] == 1) nCount++;
                            if (neighbour[2, 2] == 0 && neighbour[2, 3] == 1) nCount++;
                            if (neighbour[2, 3] == 0 && neighbour[1, 3] == 1) nCount++;
                            if (neighbour[1, 3] == 0 && neighbour[0, 3] == 1) nCount++;
                            if (neighbour[0, 3] == 0 && neighbour[0, 2] == 1) nCount++;

                            if (nCount == 1)
                            {
                                bwTem[i, j] = 1;
                                continue;
                            }
                        }

                        //(4)判断p2*p4*p6 = 0 or s(p4) !=1
                        if (neighbour[1, 2] * neighbour[2, 1] * neighbour[3, 2] == 0)
                        {
                            bCondition4 = true;
                        }
                        else
                        {
                            nCount = 0;
                            if (neighbour[1, 1] == 0 && neighbour[1, 0] == 1) nCount++;
                            if (neighbour[1, 0] == 0 && neighbour[2, 0] == 1) nCount++;
                            if (neighbour[2, 0] == 0 && neighbour[3, 0] == 1) nCount++;
                            if (neighbour[3, 0] == 0 && neighbour[3, 1] == 1) nCount++;
                            if (neighbour[3, 1] == 0 && neighbour[3, 2] == 1) nCount++;
                            if (neighbour[3, 2] == 0 && neighbour[2, 2] == 1) nCount++;
                            if (neighbour[2, 2] == 0 && neighbour[1, 2] == 1) nCount++;
                            if (neighbour[1, 2] == 0 && neighbour[1, 1] == 1) nCount++;

                            if (nCount != 1)//s(p4)!=1
                            {
                                bCondition4 = true;
                            }
                        }

                        if (bCondition4)
                        {
                            bwTem[i, j] = 0;
                            bModified = true;
                        }
                        else
                        {
                            bwTem[i, j] = 1;
                        }
                    }
                }

                //将细化了的临时图像bw_tem[w,h]复制到im[w,h],完成一次细化
                for (int j = 2; j < h - 2; j++)
                {
                    for (int i = 2; i < w - 2; i++)
                    {
                        im[i, j] = bwTem[i, j];
                    }
                }
            }

            return im;
        }

        /// <summary>
        /// 细化方法二--联结细化算法
        /// </summary>
        /// <param name="im">输入影像的二值矩阵</param>
        /// <returns>细化后的影像二值矩阵</returns>
        public static byte[,] Thinner2(byte[,] im)
        {
            var w = im.GetLength(0);
            var h = im.GetLength(1);

            var g = new byte[w, h];
            var n = new int[8];
            int[] a = { 0, -1, 1, 0, 0 };
            int[] b = { 0, 0, 0, 1, -1 };
            bool flag;

            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    g[i, j] = im[i, j];
                }
            }

            do
            {
                flag = false;
                for (int m = 1; m <= 4; m++)
                {
                    for (int j = 1; j < h - 1; j++)
                    {
                        var j1 = j + b[m];
                        for (int i = 1; i < w - 1; i++)
                        {
                            if (im[i, j] == 0) continue;
                            var i1 = i + a[m];
                            if (im[i1, j1] > 0) continue;

                            n[0] = im[i + 1, j];
                            n[1] = im[i + 1, j - 1];
                            n[2] = im[i, j - 1];
                            n[3] = im[i - 1, j - 1];
                            n[4] = im[i - 1, j];
                            n[5] = im[i - 1, j + 1];
                            n[6] = im[i, j + 1];
                            n[7] = im[i + 1, j + 1];

                            int nrnd = n[0] + n[1] + n[2] + n[3] + n[4] + n[5] + n[6] + n[7];

                            if (nrnd <= 1) continue;

                            int n02 = n[0] + n[2];
                            int n04 = n[0] + n[4];
                            int n06 = n[0] + n[6];
                            int n24 = n[2] + n[4];
                            int n26 = n[2] + n[6];
                            int n46 = n[4] + n[6];
                            int n123 = n[1] + n[2] + n[3];
                            int n345 = n[3] + n[4] + n[5];
                            int n567 = n[5] + n[6] + n[7];
                            int n701 = n[7] + n[0] + n[1];

                            if (n[0] == 1 && n26 == 0 && n345 > 0) continue;
                            if (n[2] == 1 && n04 == 0 && n567 > 0) continue;
                            if (n[4] == 1 && n26 == 0 && n701 > 0) continue;
                            if (n[6] == 1 && n04 == 0 && n123 > 0) continue;
                            if (n[1] == 1 && n02 == 0) continue;
                            if (n[3] == 1 && n24 == 0) continue;
                            if (n[5] == 1 && n46 == 0) continue;
                            if (n[7] == 1 && n06 == 0) continue;

                            g[i, j] = 0;
                            flag = true;
                        }
                    }

                    //将细化了的临时图像g[w,h]复制到im[w,h],完成一次细化
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            im[i, j] = g[i, j];
                        }
                    }
                }
            } while (flag);

            return im;
        }

        /// <summary>
        /// 图像锐化-带色彩
        /// </summary>
        /// <param name="bm">输入的影像</param>
        /// <param name="num">锐化类型 1表示Kirsch 2表示Laplace 3表示Prewitt 4表示Roberts 5表示Sobel</param>
        /// <returns>锐化后的影像</returns>
        public static Bitmap Detect(Bitmap bm, int num)
        {
            var w = bm.Width;
            var h = bm.Height;
            var inr = new int[w, h];
            var ing = new int[w, h];
            var inb = new int[w, h];

            //转变为灰度矩阵
            var data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0.ToPointer();
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        var p = ptr + j * data.Stride + i * 3;
                        inr[i, j] = p[2];
                        ing[i, j] = p[1];
                        inb[i, j] = p[0];
                    }
                }

                if (num == 1)//Kirsch
                {
                    int[,] kir0 = { { 5, 5, 5 }, { -3, 0, -3 }, { -3, -3, -3 } },
                               kir1 = { { -3, 5, 5 }, { -3, 0, 5 }, { -3, -3, -3 } },
                               kir2 = { { -3, -3, 5 }, { -3, 0, 5 }, { -3, -3, 5 } },
                               kir3 = { { -3, -3, -3 }, { -3, 0, 5 }, { -3, 5, 5 } },
                               kir4 = { { -3, -3, -3 }, { -3, 0, -3 }, { 5, 5, 5 } },
                               kir5 = { { -3, -3, -3 }, { 5, 0, -3 }, { 5, 5, -3 } },
                               kir6 = { { 5, -3, -3 }, { 5, 0, -3 }, { 5, -3, -3 } },
                               kir7 = { { 5, 5, -3 }, { 5, 0, -3 }, { -3, -3, -3 } };

                    //边缘检测
                    var edger0 = edgeEnhance(inr, kir0, w, h);
                    var edger1 = edgeEnhance(inr, kir1, w, h);
                    var edger2 = edgeEnhance(inr, kir2, w, h);
                    var edger3 = edgeEnhance(inr, kir3, w, h);
                    var edger4 = edgeEnhance(inr, kir4, w, h);
                    var edger5 = edgeEnhance(inr, kir5, w, h);
                    var edger6 = edgeEnhance(inr, kir6, w, h);
                    var edger7 = edgeEnhance(inr, kir7, w, h);

                    var edgeg0 = edgeEnhance(ing, kir0, w, h);
                    var edgeg1 = edgeEnhance(ing, kir1, w, h);
                    var edgeg2 = edgeEnhance(ing, kir2, w, h);
                    var edgeg3 = edgeEnhance(ing, kir3, w, h);
                    var edgeg4 = edgeEnhance(ing, kir4, w, h);
                    var edgeg5 = edgeEnhance(ing, kir5, w, h);
                    var edgeg6 = edgeEnhance(ing, kir6, w, h);
                    var edgeg7 = edgeEnhance(ing, kir7, w, h);

                    var edgeb0 = edgeEnhance(inb, kir0, w, h);
                    var edgeb1 = edgeEnhance(inb, kir1, w, h);
                    var edgeb2 = edgeEnhance(inb, kir2, w, h);
                    var edgeb3 = edgeEnhance(inb, kir3, w, h);
                    var edgeb4 = edgeEnhance(inb, kir4, w, h);
                    var edgeb5 = edgeEnhance(inb, kir5, w, h);
                    var edgeb6 = edgeEnhance(inb, kir6, w, h);
                    var edgeb7 = edgeEnhance(inb, kir7, w, h);

                    var temr = new int[8];
                    var temg = new int[8];
                    var temb = new int[8];
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            temr[0] = edger0[i, j];
                            temr[1] = edger1[i, j];
                            temr[2] = edger2[i, j];
                            temr[3] = edger3[i, j];
                            temr[4] = edger4[i, j];
                            temr[5] = edger5[i, j];
                            temr[6] = edger6[i, j];
                            temr[7] = edger7[i, j];

                            temg[0] = edgeg0[i, j];
                            temg[1] = edgeg1[i, j];
                            temg[2] = edgeg2[i, j];
                            temg[3] = edgeg3[i, j];
                            temg[4] = edgeg4[i, j];
                            temg[5] = edgeg5[i, j];
                            temg[6] = edgeg6[i, j];
                            temg[7] = edgeg7[i, j];

                            temb[0] = edgeb0[i, j];
                            temb[1] = edgeb1[i, j];
                            temb[2] = edgeb2[i, j];
                            temb[3] = edgeb3[i, j];
                            temb[4] = edgeb4[i, j];
                            temb[5] = edgeb5[i, j];
                            temb[6] = edgeb6[i, j];
                            temb[7] = edgeb7[i, j];

                            var maxr = 0;
                            var maxg = 0;
                            var maxb = 0;
                            for (int k = 0; k < 8; k++)
                            {
                                if (temr[k] > maxr)
                                {
                                    maxr = temr[k];
                                }
                                if (temg[k] > maxg)
                                {
                                    maxg = temg[k];
                                }
                                if (temb[k] > maxb)
                                {
                                    maxb = temb[k];
                                }
                            }

                            if (maxr > 255)
                            {
                                maxr = 255;
                            }
                            if (maxg > 255)
                            {
                                maxg = 255;
                            }
                            if (maxb > 255)
                            {
                                maxb = 255;
                            }

                            var r = 255 - maxr;
                            var g = 255 - maxg;
                            var b = 255 - maxb;
                            var p = ptr + j * data.Stride + i * 3;
                            p[0] = (byte)b;
                            p[1] = (byte)g;
                            p[2] = (byte)r;
                        }
                    }
                }
                else if (num == 2)//Laplace
                {
                    int[,] lap1 = { { 1, 1, 1 }, { 1, -8, 1 }, { 1, 1, 1 } };

                    //边缘增强
                    var edger = edgeEnhance(inr, lap1, w, h);
                    var edgeg = edgeEnhance(ing, lap1, w, h);
                    var edgeb = edgeEnhance(inb, lap1, w, h);
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            var r = edger[i, j];
                            var g = edgeg[i, j];
                            var b = edgeb[i, j];

                            if (r > 255) r = 255;
                            if (r < 0) r = 0;
                            if (g > 255) g = 255;
                            if (g < 0) g = 0;
                            if (b > 255) b = 255;
                            if (b < 0) b = 0;

                            var p = ptr + j * data.Stride + i * 3;
                            p[0] = (byte)b;
                            p[1] = (byte)g;
                            p[2] = (byte)r;
                        }
                    }
                }
                else if (num == 3)//Prewitt
                {
                    //Prewitt 算子D_x模板
                    int[,] pre1 = { { 1, 0, -1 }, { 1, 0, -1 }, { 1, 0, -1 } };

                    //Prewitt 算子D_y模板
                    int[,] pre2 = { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } };

                    var edger1 = edgeEnhance(inr, pre1, w, h);
                    var edger2 = edgeEnhance(inr, pre2, w, h);
                    var edgeg1 = edgeEnhance(ing, pre1, w, h);
                    var edgeg2 = edgeEnhance(ing, pre2, w, h);
                    var edgeb1 = edgeEnhance(inb, pre1, w, h);
                    var edgeb2 = edgeEnhance(inb, pre2, w, h);
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            var r = Math.Max(edger1[i, j], edger2[i, j]);
                            var g = Math.Max(edgeg1[i, j], edgeg2[i, j]);
                            var b = Math.Max(edgeb1[i, j], edgeb2[i, j]);
                            if (r > 255) r = 255;
                            if (g > 255) g = 255;
                            if (b > 255) b = 255;
                            var p = ptr + j * data.Stride + i * 3;
                            p[0] = (byte)b;
                            p[1] = (byte)g;
                            p[2] = (byte)r;
                        }
                    }
                }
                else if (num == 4)//Roberts
                {
                    for (int j = 1; j < h - 1; j++)
                    {
                        for (int i = 1; i < w - 1; i++)
                        {
                            var p0 = ptr + j * data.Stride + i * 3;
                            var p1 = ptr + (j + 1) * data.Stride + i * 3;
                            var p2 = ptr + j * data.Stride + (i + 1) * 3;
                            var p3 = ptr + (j + 1) * data.Stride + (i + 1) * 3;

                            var r0 = p0[2];
                            var r1 = p1[2];
                            var r2 = p2[2];
                            var r3 = p3[2];
                            var r = (int)Math.Sqrt((r0 - r3) * (r0 - r3) + (r1 - r2) * (r1 - r2));

                            var g0 = p0[1];
                            var g1 = p1[1];
                            var g2 = p2[1];
                            var g3 = p3[1];
                            var g = (int)Math.Sqrt((g0 - g3) * (g0 - g3) + (g1 - g2) * (g1 - g2));

                            var b0 = p0[0];
                            var b1 = p1[0];
                            var b2 = p2[0];
                            var b3 = p3[0];
                            var b = (int)Math.Sqrt((b0 - b3) * (b0 - b3) + (b1 - b2) * (b1 - b2));

                            if (r < 0) r = 0;
                            if (r > 255) r = 255;

                            if (g < 0) g = 0;
                            if (g > 255) g = 255;

                            if (b < 0) b = 0;
                            if (b > 255) b = 255;

                            p0[0] = (byte)b;
                            p0[1] = (byte)g;
                            p0[2] = (byte)r;
                        }
                    }
                }
                else if (num == 5)//Sobel
                {
                    int[,] sob1 = { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
                    int[,] sob2 = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

                    var edger1 = edgeEnhance(inr, sob1, w, h);
                    var edger2 = edgeEnhance(inr, sob2, w, h);
                    var edgeg1 = edgeEnhance(ing, sob1, w, h);
                    var edgeg2 = edgeEnhance(ing, sob2, w, h);
                    var edgeb1 = edgeEnhance(inb, sob1, w, h);
                    var edgeb2 = edgeEnhance(inb, sob2, w, h);
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            var r = Math.Max(edger1[i, j], edger2[i, j]);
                            var g = Math.Max(edgeg1[i, j], edgeg2[i, j]);
                            var b = Math.Max(edgeb1[i, j], edgeb2[i, j]);
                            if (r > 255) r = 255;
                            if (g > 255) g = 255;
                            if (b > 255) b = 255;
                            var p = ptr + j * data.Stride + i * 3;
                            p[0] = (byte)b;
                            p[1] = (byte)g;
                            p[2] = (byte)r;
                        }
                    }
                }
            }

            bm.UnlockBits(data);
            return bm;
        }

        /// <summary>
        /// 边界加强彩色图像锐化
        /// </summary>
        /// <param name="bm">输入的影像</param>
        /// <param name="num">锐化类型 1表示Kirsch 2表示Laplace 3表示Prewitt 4表示Roberts 5表示Sobel</param>
        /// <returns>锐化后的影像</returns>
        public static Bitmap DetectWithColor(Bitmap bm, int num)
        {
            var w = bm.Width;
            var h = bm.Height;
            var inr = new int[w, h];
            var ing = new int[w, h];
            var inb = new int[w, h];

            //转变为灰度矩阵
            var data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0.ToPointer();
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        var p = ptr + j * data.Stride + i * 3;
                        inr[i, j] = p[2];
                        ing[i, j] = p[1];
                        inb[i, j] = p[0];
                    }
                }

                if (num == 1)//Kirsch
                {
                    int[,] kir0 = { { 5, 5, 5 }, { -3, 0, -3 }, { -3, -3, -3 } },
                               kir1 = { { -3, 5, 5 }, { -3, 0, 5 }, { -3, -3, -3 } },
                               kir2 = { { -3, -3, 5 }, { -3, 0, 5 }, { -3, -3, 5 } },
                               kir3 = { { -3, -3, -3 }, { -3, 0, 5 }, { -3, 5, 5 } },
                               kir4 = { { -3, -3, -3 }, { -3, 0, -3 }, { 5, 5, 5 } },
                               kir5 = { { -3, -3, -3 }, { 5, 0, -3 }, { 5, 5, -3 } },
                               kir6 = { { 5, -3, -3 }, { 5, 0, -3 }, { 5, -3, -3 } },
                               kir7 = { { 5, 5, -3 }, { 5, 0, -3 }, { -3, -3, -3 } };

                    //边缘检测
                    var edger0 = edgeEnhance(inr, kir0, w, h);
                    var edger1 = edgeEnhance(inr, kir1, w, h);
                    var edger2 = edgeEnhance(inr, kir2, w, h);
                    var edger3 = edgeEnhance(inr, kir3, w, h);
                    var edger4 = edgeEnhance(inr, kir4, w, h);
                    var edger5 = edgeEnhance(inr, kir5, w, h);
                    var edger6 = edgeEnhance(inr, kir6, w, h);
                    var edger7 = edgeEnhance(inr, kir7, w, h);

                    var edgeg0 = edgeEnhance(ing, kir0, w, h);
                    var edgeg1 = edgeEnhance(ing, kir1, w, h);
                    var edgeg2 = edgeEnhance(ing, kir2, w, h);
                    var edgeg3 = edgeEnhance(ing, kir3, w, h);
                    var edgeg4 = edgeEnhance(ing, kir4, w, h);
                    var edgeg5 = edgeEnhance(ing, kir5, w, h);
                    var edgeg6 = edgeEnhance(ing, kir6, w, h);
                    var edgeg7 = edgeEnhance(ing, kir7, w, h);

                    var edgeb0 = edgeEnhance(inb, kir0, w, h);
                    var edgeb1 = edgeEnhance(inb, kir1, w, h);
                    var edgeb2 = edgeEnhance(inb, kir2, w, h);
                    var edgeb3 = edgeEnhance(inb, kir3, w, h);
                    var edgeb4 = edgeEnhance(inb, kir4, w, h);
                    var edgeb5 = edgeEnhance(inb, kir5, w, h);
                    var edgeb6 = edgeEnhance(inb, kir6, w, h);
                    var edgeb7 = edgeEnhance(inb, kir7, w, h);

                    var temr = new int[8];
                    var temg = new int[8];
                    var temb = new int[8];
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            temr[0] = edger0[i, j];
                            temr[1] = edger1[i, j];
                            temr[2] = edger2[i, j];
                            temr[3] = edger3[i, j];
                            temr[4] = edger4[i, j];
                            temr[5] = edger5[i, j];
                            temr[6] = edger6[i, j];
                            temr[7] = edger7[i, j];

                            temg[0] = edgeg0[i, j];
                            temg[1] = edgeg1[i, j];
                            temg[2] = edgeg2[i, j];
                            temg[3] = edgeg3[i, j];
                            temg[4] = edgeg4[i, j];
                            temg[5] = edgeg5[i, j];
                            temg[6] = edgeg6[i, j];
                            temg[7] = edgeg7[i, j];

                            temb[0] = edgeb0[i, j];
                            temb[1] = edgeb1[i, j];
                            temb[2] = edgeb2[i, j];
                            temb[3] = edgeb3[i, j];
                            temb[4] = edgeb4[i, j];
                            temb[5] = edgeb5[i, j];
                            temb[6] = edgeb6[i, j];
                            temb[7] = edgeb7[i, j];

                            var maxr = 0;
                            var maxg = 0;
                            var maxb = 0;
                            for (int k = 0; k < 8; k++)
                            {
                                if (temr[k] > maxr)
                                {
                                    maxr = temr[k];
                                }
                                if (temg[k] > maxg)
                                {
                                    maxg = temg[k];
                                }
                                if (temb[k] > maxb)
                                {
                                    maxb = temb[k];
                                }
                            }

                            if (maxr > 255)
                            {
                                maxr = 255;
                            }
                            if (maxg > 255)
                            {
                                maxg = 255;
                            }
                            if (maxb > 255)
                            {
                                maxb = 255;
                            }

                            var r = 255 - maxr;
                            var g = 255 - maxg;
                            var b = 255 - maxb;
                            var p = ptr + j * data.Stride + i * 3;
                            p[0] = (byte)b;
                            p[1] = (byte)g;
                            p[2] = (byte)r;
                        }
                    }
                }
                else if (num == 2)//Laplace
                {
                    int[,] lap1 = { { 1, 1, 1 }, { 1, -8, 1 }, { 1, 1, 1 } };

                    //边缘增强
                    var edger = edgeEnhance(inr, lap1, w, h);
                    var edgeg = edgeEnhance(ing, lap1, w, h);
                    var edgeb = edgeEnhance(inb, lap1, w, h);
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            var r = edger[i, j];
                            var g = edgeg[i, j];
                            var b = edgeb[i, j];

                            if (r > 255) r = 255;
                            if (r < 0) r = 0;
                            if (g > 255) g = 255;
                            if (g < 0) g = 0;
                            if (b > 255) b = 255;
                            if (b < 0) b = 0;

                            var p = ptr + j * data.Stride + i * 3;
                            p[0] = (byte)b;
                            p[1] = (byte)g;
                            p[2] = (byte)r;
                        }
                    }
                }
                else if (num == 3)//Prewitt
                {
                    //Prewitt 算子D_x模板
                    int[,] pre1 = { { 1, 0, -1 }, { 1, 0, -1 }, { 1, 0, -1 } };

                    //Prewitt 算子D_y模板
                    int[,] pre2 = { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } };

                    var edger1 = edgeEnhance(inr, pre1, w, h);
                    var edger2 = edgeEnhance(inr, pre2, w, h);
                    var edgeg1 = edgeEnhance(ing, pre1, w, h);
                    var edgeg2 = edgeEnhance(ing, pre2, w, h);
                    var edgeb1 = edgeEnhance(inb, pre1, w, h);
                    var edgeb2 = edgeEnhance(inb, pre2, w, h);
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            var r = Math.Max(edger1[i, j], edger2[i, j]);
                            var g = Math.Max(edgeg1[i, j], edgeg2[i, j]);
                            var b = Math.Max(edgeb1[i, j], edgeb2[i, j]);
                            if (r > 255) r = 255;
                            if (g > 255) g = 255;
                            if (b > 255) b = 255;
                            var p = ptr + j * data.Stride + i * 3;
                            p[0] = (byte)b;
                            p[1] = (byte)g;
                            p[2] = (byte)r;
                        }
                    }
                }
                else if (num == 4)//Roberts
                {
                    for (int j = 1; j < h - 1; j++)
                    {
                        for (int i = 1; i < w - 1; i++)
                        {
                            var p0 = ptr + j * data.Stride + i * 3;
                            var p1 = ptr + (j + 1) * data.Stride + i * 3;
                            var p2 = ptr + j * data.Stride + (i + 1) * 3;
                            var p3 = ptr + (j + 1) * data.Stride + (i + 1) * 3;

                            var r0 = p0[2];
                            var r1 = p1[2];
                            var r2 = p2[2];
                            var r3 = p3[2];
                            var r = (int)Math.Sqrt((r0 - r3) * (r0 - r3) + (r1 - r2) * (r1 - r2));

                            var g0 = p0[1];
                            var g1 = p1[1];
                            var g2 = p2[1];
                            var g3 = p3[1];
                            var g = (int)Math.Sqrt((g0 - g3) * (g0 - g3) + (g1 - g2) * (g1 - g2));

                            var b0 = p0[0];
                            var b1 = p1[0];
                            var b2 = p2[0];
                            var b3 = p3[0];
                            var b = (int)Math.Sqrt((b0 - b3) * (b0 - b3) + (b1 - b2) * (b1 - b2));

                            if (r < 0) r = 0;
                            if (r > 255) r = 255;

                            if (g < 0) g = 0;
                            if (g > 255) g = 255;

                            if (b < 0) b = 0;
                            if (b > 255) b = 255;

                            p0[0] = (byte)b;
                            p0[1] = (byte)g;
                            p0[2] = (byte)r;
                        }
                    }
                }
                else if (num == 5)//Sobel
                {
                    int[,] sob1 = { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
                    int[,] sob2 = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

                    var edger1 = edgeEnhance(inr, sob1, w, h);
                    var edger2 = edgeEnhance(inr, sob2, w, h);
                    var edgeg1 = edgeEnhance(ing, sob1, w, h);
                    var edgeg2 = edgeEnhance(ing, sob2, w, h);
                    var edgeb1 = edgeEnhance(inb, sob1, w, h);
                    var edgeb2 = edgeEnhance(inb, sob2, w, h);
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            var r = Math.Max(edger1[i, j], edger2[i, j]);
                            var g = Math.Max(edgeg1[i, j], edgeg2[i, j]);
                            var b = Math.Max(edgeb1[i, j], edgeb2[i, j]);
                            if (r > 255) r = 255;
                            if (g > 255) g = 255;
                            if (b > 255) b = 255;
                            var p = ptr + j * data.Stride + i * 3;
                            p[0] = (byte)b;
                            p[1] = (byte)g;
                            p[2] = (byte)r;
                        }
                    }
                }
            }

            bm.UnlockBits(data);
            return bm;
        }

        /// <summary>
        /// 图像锐化-灰度锐化
        /// </summary>
        /// <param name="bm">输入的影像</param>
        /// <param name="num">锐化类型 1表示Kirsch 2表示Laplace 3表示Prewitt 4表示Roberts 5表示Sobel</param>
        /// <returns>锐化后的影像</returns>
        public static Bitmap DetectWithGray(Bitmap bm, int num)
        {
            var w = bm.Width;
            var h = bm.Height;
            var gray = new int[w, h];

            //转变为灰度矩阵
            var data = bm.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr = (byte*)data.Scan0.ToPointer();
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        var p = ptr + j * data.Stride + i * 3;
                        gray[i, j] = (int)((p[0] + p[1] + p[2]) / 3.0f);
                    }
                }

                if (num == 1)//Kirsch
                {
                    int[,] kir0 = { { 5, 5, 5 }, { -3, 0, -3 }, { -3, -3, -3 } },
                               kir1 = { { -3, 5, 5 }, { -3, 0, 5 }, { -3, -3, -3 } },
                               kir2 = { { -3, -3, 5 }, { -3, 0, 5 }, { -3, -3, 5 } },
                               kir3 = { { -3, -3, -3 }, { -3, 0, 5 }, { -3, 5, 5 } },
                               kir4 = { { -3, -3, -3 }, { -3, 0, -3 }, { 5, 5, 5 } },
                               kir5 = { { -3, -3, -3 }, { 5, 0, -3 }, { 5, 5, -3 } },
                               kir6 = { { 5, -3, -3 }, { 5, 0, -3 }, { 5, -3, -3 } },
                               kir7 = { { 5, 5, -3 }, { 5, 0, -3 }, { -3, -3, -3 } };

                    //边缘检测
                    var edge0 = edgeEnhance(gray, kir0, w, h);
                    var edge1 = edgeEnhance(gray, kir1, w, h);
                    var edge2 = edgeEnhance(gray, kir2, w, h);
                    var edge3 = edgeEnhance(gray, kir3, w, h);
                    var edge4 = edgeEnhance(gray, kir4, w, h);
                    var edge5 = edgeEnhance(gray, kir5, w, h);
                    var edge6 = edgeEnhance(gray, kir6, w, h);
                    var edge7 = edgeEnhance(gray, kir7, w, h);

                    var tem = new int[8];
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            tem[0] = edge0[i, j];
                            tem[1] = edge1[i, j];
                            tem[2] = edge2[i, j];
                            tem[3] = edge3[i, j];
                            tem[4] = edge4[i, j];
                            tem[5] = edge5[i, j];
                            tem[6] = edge6[i, j];
                            tem[7] = edge7[i, j];
                            var max = 0;
                            for (int k = 0; k < 8; k++)
                            {
                                if (tem[k] > max)
                                {
                                    max = tem[k];
                                }
                            }

                            if (max > 255)
                            {
                                max = 255;
                            }

                            var r = 255 - max;
                            var p = ptr + j * data.Stride + i * 3;
                            p[0] = p[1] = p[2] = (byte)r;
                        }
                    }
                }
                else if (num == 2)//Laplace
                {
                    int[,] lap1 = { { 1, 1, 1 }, { 1, -8, 1 }, { 1, 1, 1 } };

                    //边缘增强
                    var edge = edgeEnhance(gray, lap1, w, h);
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            var r = edge[i, j];
                            if (r > 255) r = 255;
                            if (r < 0) r = 0;

                            var p = ptr + j * data.Stride + i * 3;
                            p[0] = p[1] = p[2] = (byte)r;
                        }
                    }
                }
                else if (num == 3)//Prewitt
                {
                    //Prewitt 算子D_x模板
                    int[,] pre1 = { { 1, 0, -1 }, { 1, 0, -1 }, { 1, 0, -1 } };

                    //Prewitt 算子D_y模板
                    int[,] pre2 = { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } };

                    var edge1 = edgeEnhance(gray, pre1, w, h);
                    var edge2 = edgeEnhance(gray, pre2, w, h);
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            var r = Math.Max(edge1[i, j], edge2[i, j]);
                            if (r > 255)
                            {
                                r = 255;
                            }
                            var p = ptr + j * data.Stride + i * 3;
                            p[0] = p[1] = p[2] = (byte)r;
                        }
                    }
                }
                else if (num == 4)//Roberts
                {
                    for (int j = 1; j < h - 1; j++)
                    {
                        for (int i = 1; i < w - 1; i++)
                        {
                            var p0 = ptr + j * data.Stride + i * 3;
                            var p1 = ptr + (j + 1) * data.Stride + i * 3;
                            var p2 = ptr + j * data.Stride + (i + 1) * 3;
                            var p3 = ptr + (j + 1) * data.Stride + (i + 1) * 3;

                            var r0 = p0[2];
                            var r1 = p1[2];
                            var r2 = p2[2];
                            var r3 = p3[2];
                            var r = (int)Math.Sqrt((r0 - r3) * (r0 - r3) + (r1 - r2) * (r1 - r2));

                            if (r < 0) r = 0;
                            if (r > 255) r = 255;

                            p0[0] = p0[1] = p0[2] = (byte)r;
                        }
                    }
                }
                else if (num == 5)//Sobel
                {
                    int[,] sob1 = { { 1, 0, -1 }, { 2, 0, -2 }, { 1, 0, -1 } };
                    int[,] sob2 = { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

                    var edge1 = edgeEnhance(gray, sob1, w, h);
                    var edge2 = edgeEnhance(gray, sob2, w, h);
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            var r = Math.Max(edge1[i, j], edge2[i, j]);
                            if (r > 255) r = 255;
                            var p = ptr + j * data.Stride + i * 3;
                            p[0] = p[1] = p[2] = (byte)r;
                        }
                    }
                }
            }

            bm.UnlockBits(data);
            return bm;
        }

        /// <summary>
        /// 边界加强
        /// </summary>
        /// <param name="ing">影像颜色矩阵</param>
        /// <param name="tmp">模板</param>
        /// <param name="w">影像宽度</param>
        /// <param name="h">影像高度</param>
        /// <returns>应用模板加强后的影像颜色矩阵</returns>
        private static int[,] edgeEnhance(int[,] ing, int[,] tmp, int w, int h)
        {
            var ed = new int[w, h];
            for (int j = 1; j < h - 1; j++)
            {
                for (int i = 1; i < w - 1; i++)
                {
                    ed[i, j] =
                        Math.Abs(tmp[0, 0] * ing[i - 1, j - 1] + tmp[0, 1] * ing[i - 1, j] + tmp[0, 2] * ing[i - 1, j + 1] +
                                 tmp[1, 0] * ing[i, j - 1] + tmp[1, 1] * ing[i, j] + tmp[1, 2] * ing[i, j + 1] +
                                 tmp[2, 0] * ing[i + 1, j - 1] + tmp[2, 1] * ing[i + 1, j] + tmp[2, 2] * ing[i + 1, j + 1]);
                }
            }
            return ed;
        }

        /// <summary>
        /// 图片转二值影像
        /// </summary>
        /// <param name="bmp">输入的影像</param>
        /// <param name="colorThreshold">阈值</param>
        /// <returns>目标二值影像</returns>
        public static Bitmap ConverImageTo2Value(Bitmap bmp, int colorThreshold)
        {
            int w = bmp.Width;
            int h = bmp.Height;

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                var p = (byte*)data.Scan0;

                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        p[0] = p[1] = p[2] = ((p[0] + p[1] + p[2]) / 3) > colorThreshold ? (byte)0 : (byte)255;
                        p += 3;
                    }
                    p += data.Stride - w * 3;
                }
            }

            bmp.UnlockBits(data);

            return bmp;
        }

        /// <summary>
        /// 将二值影像反色处理，黑变白，白变黑
        /// </summary>
        /// <param name="im">影像二值矩阵</param>
        /// <returns>反色后的二值矩阵</returns>
        public static byte[,] Inverse(byte[,] im)
        {
            var w = im.GetLength(0);
            var h = im.GetLength(1);
            unsafe
            {
                fixed (byte* ptr = &im[0, 0])
                {
                    for (int j = 0; j < h; j++)
                    {
                        for (int i = 0; i < w; i++)
                        {
                            var p = ptr + j * w + i;
                            *p = *p == 0 ? (byte)1 : (byte)0;
                        }
                    }
                }
            }

            return im;
        }

        /// <summary>
        /// 横向二值采样
        /// </summary>
        /// <param name="bm">输入的图像</param>
        /// <param name="space">采样间隔</param>
        /// <returns>输出的目标图像</returns>
        public static Bitmap Horizontal2ValueSampling(Bitmap bm, int space)
        {
            var w = bm.Width;
            var h = bm.Height;
            BitmapData data = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadWrite,
                                          PixelFormat.Format24bppRgb);
            int stride = data.Stride;

            unsafe
            {
                var ptr = (byte*)data.Scan0.ToPointer();
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i = i + space)
                    {
                        int c = 0;
                        for (int k = 0; k < space; k++)
                        {
                            var x = i + k >= w ? w - 1 : i + k;
                            c = c +
                                ((ptr + j * stride + (x * 3))[0] + (ptr + j * stride + (x * 3))[1] + (ptr + j * stride + (x * 3))[2]) /
                                3;
                        }

                        c = c / space;

                        for (int k = 0; k < space; k++)
                        {
                            var x = i + k >= w ? w - 1 : i + k;
                            (ptr + j * stride + (x * 3))[0] =
                                (ptr + j * stride + (x * 3))[1] =
                                (ptr + j * stride + (x * 3))[2] = c < 255 ? (byte)0 : (byte)255;
                        }
                    }
                }
            }

            bm.UnlockBits(data);

            return bm;
        }

        /// <summary>
        /// 横向二值采样
        /// </summary>
        /// <param name="im">输入影像的二值矩阵 </param>
        /// <param name="space">采样间隔 </param>
        /// <returns>输出的目标图像</returns>
        public static byte[,] Horizontal2ValueSampling(byte[,] im, int space)
        {
            var w = im.GetLength(0);
            var h = im.GetLength(1);

            //unsafe
            //{
            //    fixed (byte* p = &im[0, 0])
            //    {
            //        for (int j = 0; j < h; j++)
            //        {
            //            for (int i = 0; i < w; i++)
            //            {
            //                if (*(p + j * w + i) == 1)
            //                {
            //                    if (i + space > w)
            //                    {
            //                        break;
            //                    }
            //                    for (int k = space; k > 0; k--)
            //                    {
            //                        if (*(p + j * w + i + k) == 1)
            //                        {
            //                            for (int l = 1; l < k; l++)
            //                            {
            //                                *(p + j * w + i + l) = 1;
            //                            }
            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}


            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    if (im[i, j] == 1)
                    {
                        if (i + space >= w)
                        {
                            break;
                        }
                        for (int k = space; k > 0; k--)
                        {
                            if (im[i + k, j] == 1)
                            {
                                for (int l = 1; l < k; l++)
                                {
                                    im[i + l, j] = 1;
                                }
                                break;
                            }
                        }
                    }
                }
            }

            return im;
        }

        /// <summary>
        /// Hough变换检测直线
        /// </summary>
        /// <param name="bw">输入的二值图像</param>
        /// <param name="w">输入图像的宽</param>
        /// <param name="h">输入图像的高</param>
        /// <returns></returns>
        public static byte[,] DetecLine(byte[,] bw, int w, int h)
        {
            //计算变换域的尺寸
            var tMaxDist = (int)Math.Sqrt(w * w + h * h);//最大距离
            const int _tMaxAngle = 90;

            var bwTem = new byte[h, w];
            var ta = new int[tMaxDist, _tMaxAngle];

            //变换域的坐标
            int tDist, tAngel;

            //初始化临时图像距阵
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    bwTem[i, j] = 0;
                }
            }

            //初始化变换域矩阵
            for (tDist = 0; tDist < tMaxDist; tDist++)
            {
                for (tAngel = 0; tAngel < _tMaxAngle; tAngel++)
                {
                    ta[tDist, tAngel] = 0;
                }
            }

            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    //取得当前指针处的像素值
                    int pixel = bw[j, i];

                    //如果是黑点，则在变换域的对应各点上加1
                    if (pixel == 1)
                    {
                        //注意步长是2度
                        for (tAngel = 0; tAngel < _tMaxAngle; tAngel++)
                        {
                            tDist =
                                (int)Math.Abs(i * Math.Cos(tAngel * 2 * Math.PI / 180.0) + j * Math.Sin(tAngel * 2 * Math.PI / 180.0));

                            ta[tDist, tAngel] = ta[tDist, tAngel] + 1;
                        }
                    }
                }
            }

            //找到变换域中的两个最大值点
            int maxValue1 = 0;
            int maxDist1 = 0;
            int maxAngle1 = 0;
            int maxValue2 = 0;
            int maxDist2 = 0;
            int maxAngle2 = 0;

            //找到第一个最大值点
            for (tDist = 0; tDist < tMaxDist; tDist++)
            {
                for (tAngel = 0; tAngel < _tMaxAngle; tAngel++)
                {
                    if (ta[tDist, tAngel] > maxValue1)
                    {
                        maxValue1 = ta[tDist, tAngel];
                        maxDist1 = tDist;
                        maxAngle1 = tAngel;
                    }
                }
            }

            //将第一个最大值点(maxDist,maxAngle1)为中心的区域清零
            for (tDist = -9; tDist < 10; tDist++)
            {
                for (tAngel = -1; tAngel < 2; tAngel++)
                {
                    if (tDist + maxDist1 >= 0 && tDist + maxDist1 < tMaxDist && tAngel + maxAngle1 >= 0 && tAngel + maxAngle1 <= _tMaxAngle)
                    {
                        ta[tDist + maxDist1, tAngel + maxAngle1] = 0;
                    }
                }
            }

            //找到第二个最大值
            for (tDist = 0; tDist < tMaxDist; tDist++)
            {
                for (tAngel = 0; tAngel < _tMaxAngle; tAngel++)
                {
                    if (ta[tDist, tAngel] > maxValue2)
                    {
                        maxValue2 = ta[tDist, tAngel];
                        maxDist2 = tDist;
                        maxAngle2 = tAngel;
                    }
                }
            }

            //标注直线
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    //在第一条直线上
                    tDist =
                        (int)Math.Abs(i * Math.Cos(maxAngle1 * 2 * Math.PI / 180.0) + j * Math.Sin(maxAngle1 * 2 * Math.PI / 180.0));

                    if (tDist == maxDist1)
                    {
                        bwTem[j, i] = 1;
                    }

                    //在第二条直线上
                    tDist =
                        (int)Math.Abs(i * Math.Cos(maxAngle2 * 2 * Math.PI / 180.0) + j * Math.Sin(maxAngle2 * 2 * Math.PI / 180.0));

                    if (tDist == maxDist2)
                    {
                        bwTem[j, i] = 1;
                    }
                }
            }

            return bwTem;
        }

        /// <summary>
        /// 归一化处理方法
        /// </summary>
        /// <param name="pIm">图像投影矩阵</param>
        /// <param name="mea">归一化区间</param>
        /// <param name="type">投影类型：0：横向投影，1：纵向投影</param>
        /// <returns>归一化处理的的矩阵</returns>
        public static byte[,] Normalization(byte[,] pIm, int mea, int type = 0)
        {
            var w = pIm.GetLength(0);
            var h = pIm.GetLength(1);

            if (type == 0)
            {
                //构建质量数组
                var arg = new int[h];
                for (int j = 0; j < h; j++)
                {
                    var s = 0;
                    for (int i = 0; i < w; i++)
                    {
                        s += pIm[i, j];
                    }
                    arg[j] = s;
                }

                //归一化处理
                for (int i = 0; i < arg.Length; i++)
                {
                    arg[i] = (int)((double)arg[i] / w * mea);
                }

                //构建新的矩阵
                var newIm = new byte[mea + 1, arg.Length];
                for (int j = 0; j < arg.Length; j++)
                {
                    for (int i = 0; i < mea + 1; i++)
                    {
                        if (i < arg[j])
                        {
                            newIm[i, j] = 1;
                        }
                        else
                        {
                            newIm[i, j] = 0;
                        }
                    }
                }

                return newIm;
            }
            else
            {
                //构建质量数组
                var arg = new int[w];
                for (int j = 0; j < w; j++)
                {
                    var s = 0;
                    for (int i = 0; i < h; i++)
                    {
                        s += pIm[j, i];
                    }
                    arg[j] = s;
                }

                //归一化处理
                for (int i = 0; i < arg.Length; i++)
                {
                    arg[i] = (int)((double)arg[i] / h * mea);
                }

                //构建新矩阵
                var newIm = new byte[arg.Length, mea + 1];
                for (int j = 0; j < arg.Length; j++)
                {
                    for (int i = 0; i < mea + 1; i++)
                    {
                        if (i < mea - arg[j])
                        {
                            newIm[j, i] = 0;
                        }
                        else
                        {
                            newIm[j, i] = 1;
                        }
                    }
                }

                return newIm;
            }
        }

        /// <summary>
        /// 纵向形心投影（Y轴投影）
        /// </summary>
        public static byte[,] VerticalCenterProjection(byte[,] im, int y)
        {
            var w = im.GetLength(0);
            var h = im.GetLength(1);
            var gs = new byte[w, h];

            for (int i = 0; i < w; i++)
            {
                var s1 = 0;
                for (int j = 0; j < y; j++)
                {
                    s1 += im[i, j];
                }

                for (int j = y - s1; j < y; j++)
                {
                    gs[i, j] = 1;
                }

                var s2 = 0;
                for (int j = y; j < h; j++)
                {
                    s2 += im[i, j];
                }

                for (int j = y; j < y + s2; j++)
                {
                    gs[i, j] = 1;
                }
            }

            return gs;
        }

        /// <summary>
        /// Image horizontal projection analsis
        /// </summary>
        /// <param name="im">Image two-value matrix</param>
        /// <param name="gs">Projection matrix</param>
        /// <returns>New image two-value matrix</returns>
        public static byte[,] HorizontalProjectionAnalysis(byte[,] im, byte[,] gs)
        {
            var w = gs.GetLength(0);
            var h = gs.GetLength(1);
            var args = new int[h];
            for (int j = 0; j < h; j++)
            {
                var s = 0;
                for (int i = 0; i < w; i++)
                {
                    s += gs[i, j];
                }
                args[j] = s;
            }
            var n = 0;
            var index = 0;
            var upPoint = 0;
            var downPoint = 0;
            for (int i = 0; i < h / 2; i++) //get upPoint
            {
                if (args[i] > 0)
                {
                    n++;
                    if (n == 1)
                    {
                        index = i;
                    }
                    else if (n > 10 && index > 0)
                    {
                        var s = 0;
                        for (int j = index; j < index + 10; j++)
                        {
                            s += args[j];
                        }
                        if (s / 10 > 20)
                        {
                            upPoint = index;
                            break;
                        }
                        n = 0;
                    }
                }
                else
                {
                    n = 0;
                }
            }
            n = 0;
            index = 0;
            for (int i = h - 1; i > h / 2; i--) //get downPoint
            {
                if (args[i] > 0)
                {
                    n++;
                    if (n == 1)
                    {
                        index = i;
                    }
                    else if (n > 10 && index > 0)
                    {
                        var s = 0;
                        for (int j = index; j > index - 10; j--)
                        {
                            s += args[j];
                        }
                        if (s / 10 > 20)
                        {
                            downPoint = index;
                            break;
                        }
                        n = 0;
                    }
                }
                else
                {
                    n = 0;
                }
            }

            var newIm = new byte[w, downPoint - upPoint];
            //give newIm[,] assignment
            for (int j = 0; j < downPoint - upPoint; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    newIm[i, j] = im[i, j + upPoint];
                }
            }

            return newIm;
        }

        /// <summary>
        /// Image vertical projection analsis
        /// </summary>
        /// <param name="im">Image two-value matrix</param>
        /// <param name="gs">Projection matrix</param>
        /// <returns>New image two-value matrix</returns>
        public static byte[,] VerticalProjectionAnalysis(byte[,] im, byte[,] gs)
        {
            var w = gs.GetLength(0);
            var h = gs.GetLength(1);
            var args = new int[w];
            for (int i = 0; i < w; i++)
            {
                var s = 0;
                for (int j = 0; j < h; j++)
                {
                    s += gs[i, j];
                }
                args[i] = s;
            }
            var n = 0;
            var index = 0;
            var leftPoint = 0;
            var rightPoint = 0;
            for (int i = 0; i < w / 2; i++) //get upPoint
            {
                if (args[i] > 0)
                {
                    n++;
                    if (n == 1)
                    {
                        index = i;
                    }
                    else if (n > 10 && index > 0)
                    {
                        var s = 0;
                        for (int j = index; j < index + 10; j++)
                        {
                            s += args[j];
                        }
                        if (s / 10 > 20)
                        {
                            leftPoint = index;
                            break;
                        }
                        n = 0;
                    }
                }
                else
                {
                    n = 0;
                }
            }
            n = 0;
            index = 0;
            for (int i = w - 1; i > w / 2; i--) //get downPoint
            {
                if (args[i] > 0)
                {
                    n++;
                    if (n == 1)
                    {
                        index = i;
                    }
                    else if (n > 10 && index > 0)
                    {
                        var s = 0;
                        for (int j = index; j > index - 10; j--)
                        {
                            s += args[j];
                        }
                        if (s / 10 > 20)
                        {
                            rightPoint = index;
                            break;
                        }
                        n = 0;
                    }
                }
                else
                {
                    n = 0;
                }
            }

            var newIm = new byte[rightPoint - leftPoint, h];
            //give newIm[,] assignment
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < rightPoint - leftPoint; i++)
                {
                    newIm[i, j] = im[i + leftPoint, j];
                }
            }

            return newIm;
        }

        /// <summary>
        /// 原图边界放大方法
        /// </summary>
        /// <param name="bm">原图</param>
        /// <param name="enWidth">两边扩展长度</param>
        /// <param name="enHeigt">上下扩展长度</param>
        /// <returns>新图</returns>
        public static Bitmap RectangleEageEnlarge(Bitmap bm, int enWidth, int enHeigt)
        {
            var newW = bm.Width + enWidth * 2;
            var newH = bm.Height + enHeigt * 2;
            var newBm = new Bitmap(newW, newH);
            var data = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly,
                                   PixelFormat.Format24bppRgb);
            var dataNew = newBm.LockBits(new Rectangle(0, 0, newW, newH), ImageLockMode.ReadWrite,
                                         PixelFormat.Format24bppRgb);

            unsafe
            {
                var ptr1 = (byte*)data.Scan0.ToPointer();
                var ptr2 = (byte*)dataNew.Scan0.ToPointer();

                for (int j = 0; j < newH; j++)
                {
                    for (int i = 0; i < newW; i++)
                    {
                        var p = ptr2 + j * dataNew.Stride + i * 3;
                        p[0] = p[1] = p[2] = 255;
                    }
                }

                for (int j = 0; j < bm.Height; j++)
                {
                    for (int i = 0; i < bm.Width; i++)
                    {
                        var pNew = ptr2 + (j + enHeigt) * dataNew.Stride + (i + enWidth) * 3;
                        var p = ptr1 + j * data.Stride + i * 3;
                        pNew[0] = p[0];
                        pNew[1] = p[1];
                        pNew[2] = p[2];
                    }
                }
            }

            bm.UnlockBits(data);
            newBm.UnlockBits(dataNew);

            return newBm;
        }

        /// <summary>
        /// 导入外部C++动态库的二值化方法
        /// </summary>
        /// <param name="imageData">图像的数据流</param>
        /// <param name="width">图像宽</param>
        /// <param name="height">图像高</param>
        /// <param name="T">阈值</param>
        [DllImport("IMGDLL.dll", EntryPoint = "ThreeValue", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ThreeValue(byte[] imageData, int width, int height, int T);

        /// <summary>
        /// C++二值转换方法
        /// </summary>
        /// <param name="bm">要二值化的影像</param>
        /// <param name="t">阈值</param>
        /// <returns>二值化后的影像</returns>
        public static Bitmap CtoTwoValue(Bitmap bm, int t = 3*3*360)
        {
            var imageData = Common.BitmapToBytes(bm);
            ThreeValue(imageData, bm.Width, bm.Height, t);
            var im = new byte[bm.Width, bm.Height];
            for (int j = 0; j < bm.Height; j++)
            {
                for (int i = 0; i < bm.Width; i++)
                {
                    if (imageData[j * bm.Width + i] < 100)
                    {
                        im[i, j] = 1;
                    }
                    else
                    {
                        im[i, j] = 0;
                    }
                }
            }
            bm = Common.ToBitmap(im);
            return bm;
        }

        /// <summary>
        /// 最小连通区域
        /// </summary>
        /// <param name="bm">输入的二值影像</param>
        /// <param name="maxRange">最大范围</param>
        /// <param name="minRange">最小范围</param>
        /// <param name="space">允许断开的距离</param>
        /// <returns>最小连通区域集合</returns>
        public static List<Rectangle> PixelSmallestArea(Bitmap bm, int maxRange, int minRange, int space)
        {
            var recList = new List<Rectangle>();

            return recList;
        }
    }
}
