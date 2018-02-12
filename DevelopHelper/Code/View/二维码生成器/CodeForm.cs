using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;

namespace View.CreateQrImage
{
    public partial class CodeForm : Form
    {
        public CodeForm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            encoder.QRCodeScale = 8;
            encoder.QRCodeVersion = 8;
            encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            string data = txtContent.Text.Trim();
            Bitmap image = encoder.Encode(data, Encoding.UTF8);
            picQrImage.Image = image;
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            var content = txtContent.Text;
            var chars = content.ToCharArray();
            var length = chars.Length;
            lblCharCount.Text = length + "/300";
        }
    }
}
