using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using ImageOCR;

namespace View.OCR
{
    public partial class OcrForm : Form
    {
        public OcrForm()
        {
            InitializeComponent();
        }

        private Bitmap curBitmap;
        private int iw, ih;
        private string imageFileName;
        private void 二值化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Load Image
            var open = new OpenFileDialog
            {
                Filter = "Image file(*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif;*.tiff;*.wmf)|*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif;*.tiff;*.wmf"
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    curBitmap = (Bitmap)Image.FromFile(open.FileName);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }

                pictureBox.Refresh();
                pictureBox.Image = curBitmap;
                iw = curBitmap.Width;
                ih = curBitmap.Height;
                imageFileName = open.FileName.Substring(open.FileName.LastIndexOf('\\') + 1);
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null)
                return;

            var saveFileDialog = new SaveFileDialog
            {
                FileName = imageFileName,
                Filter = "TIF(文件)(*.TIF)|*.TIF|All File(*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string str = saveFileDialog.FileName;
                pictureBox.Image.Save(str, ImageFormat.Tiff);
            }
        }

        private void 二值化ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (getCurrentImage())
            {
                var bm = new Bitmap(curBitmap);
                var t = Matlab.GetThreshold(bm);
                bm = Matlab.Thresh(bm, iw, ih, t);
                pictureBox.Refresh();
                pictureBox.Image = bm;
            }
        }

        private void 识别ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (getCurrentImage())
            {
                var bm = new Bitmap(curBitmap);
                if (!Directory.Exists("Images"))
                {
                    Directory.CreateDirectory("Images");
                }
                var _path = "Images\\" + imageFileName.Substring(0, imageFileName.LastIndexOf('.')) + ".tif";
                bm.Save(_path, ImageFormat.Tiff);
                string str = "";
                ImageOCR.OCR.ImageToOCR(_path, ref str);
                if (!String.IsNullOrEmpty(str))
                {
                    txtContent.Text = str;
                }
                else
                {
                    MessageBox.Show("未能识别。", "Message");
                }
            }
        }

        //get pictureBox's image
        private bool getCurrentImage()
        {
            var flag = false;
            if (pictureBox.Image == null)
            {
                MessageBox.Show("请选择一张图片。", "提示信息");

                var open = new OpenFileDialog
                {
                    Filter = "Image file(*.bmp;*.jpg;*gif;*png;*tif;*wmf)|*.bmp;*.jpg;*gif;*png;*tif;*wmf"
                };

                if (open.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        curBitmap = (Bitmap)Image.FromFile(open.FileName);
                        //imagePath = 
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                    }

                    pictureBox.Refresh();
                    pictureBox.Image = curBitmap;
                    iw = curBitmap.Width;
                    ih = curBitmap.Height;
                    imageFileName = open.FileName.Substring(open.FileName.LastIndexOf('\\') + 1);
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
            return flag;
        }
    }
}
