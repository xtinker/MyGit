using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace View.DataChange
{
    public partial class DataChangeForm : Form
    {
        public DataChangeForm()
        {
            InitializeComponent();
        }

        private void btn10To16_Click(object sender, EventArgs e)
        {

        }

        private void btn16To10_Click(object sender, EventArgs e)
        {

        }

        private void btn10To2_Click(object sender, EventArgs e)
        {

        }

        private void btn2To10_Click(object sender, EventArgs e)
        {

        }

        private void btn16To2_Click(object sender, EventArgs e)
        {

        }

        private void btn2To16_Click(object sender, EventArgs e)
        {

        }

        private void btnXToY_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbx.Text))
            {
                MessageBox.Show("请选择输入的进制类型");
                return;
            }
            if (string.IsNullOrWhiteSpace(cby.Text))
            {
                MessageBox.Show("请选择要转换的进制类型");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtXToY_X.Text.Trim()))
            {
                MessageBox.Show("请输入要转换的进制数");
                return;
            }

            string dstr = txtXToY_X.Text.Trim();
            DataType fromType = (DataType)Enum.Parse(typeof(DataType), cbx.Text);
            DataType toType = (DataType)Enum.Parse(typeof(DataType), cby.Text);
            txtXToY_Y.Text = convert(dstr,fromType, toType);
        }
        
        private string convert(string dstr, DataType fromType, DataType toType)
        {
            var type = Convert.ToInt32(fromType);
            ConvertDelegate convertDelegate = null;
            if (type == 2)
            {
                convertDelegate = _2To;
            }
            else if (type == 4)
            {
                convertDelegate = _4To;
            }
            else if (type == 8)
            {
                convertDelegate = _8To;
            }
            else if (type == 10)
            {
                convertDelegate = _10To;
            }
            else if (type == 16)
            {
                convertDelegate = _16To;
            }
            else if (type == 32)
            {
                convertDelegate = _32To;
            }
            else if (type == 64)
            {
                convertDelegate = _64To;
            }

            var result = convertDelegate != null ? convertDelegate(dstr, toType) : "";

            return result;
        }

        public delegate string ConvertDelegate(string dstr, DataType toType);

        /// <summary>
        /// 2进制转其它进制数
        /// </summary>
        /// <param name="dstr">2进制字符串</param>
        /// <param name="toType">需要转换的类型</param>
        private string _2To(string dstr, DataType toType)
        {
            string result;
            int d2;
            try
            {
                d2 = Convert.ToInt32(dstr,2);
            }
            catch
            {
                result = "输入的2进制数不正确，请输入2进制数据";
                return result;
            }

            var d10Str = Convert.ToString(d2, 10);
            result = _10To(d10Str, toType);

            return result;
        }

        /// <summary>
        /// 4进制转其它进制数
        /// </summary>
        /// <param name="dstr">4进制字符串</param>
        /// <param name="toType">需要转换的类型</param>
        private string _4To(string dstr, DataType toType)
        {
            string result;
            int d4;
            try
            {
                d4 = Convert.ToInt32(dstr, 4);
            }
            catch
            {
                result = "输入的4进制数不正确，请输入4进制数据";
                return result;
            }

            var d10Str = Convert.ToString(d4, 10);
            result = _10To(d10Str, toType);

            return result;
        }

        /// <summary>
        /// 8进制转其它进制数
        /// </summary>
        /// <param name="dstr">8进制字符串</param>
        /// <param name="toType">需要转换的类型</param>
        private string _8To(string dstr, DataType toType)
        {
            string result;
            int d8;
            try
            {
                d8 = Convert.ToInt32(dstr, 8);
            }
            catch
            {
                result = "输入的8进制数不正确，请输入8进制数据";
                return result;
            }

            var d10Str = Convert.ToString(d8, 10);
            result = _10To(d10Str, toType);

            return result;
        }

        /// <summary>
        /// 10进制转其它进制数
        /// </summary>
        /// <param name="dstr">10进制字符串</param>
        /// <param name="toType">需要转换的类型</param>
        private string _10To(string dstr, DataType toType)
        {
            string result;
            int d;
            try
            {
                d = Convert.ToInt32(dstr);
            }
            catch
            {
                result = "输入的10进制数不正确，请输入10进制整形数据";
                return result;
            }
            
            var type = Convert.ToInt32(toType);
            result = Convert.ToString(d,type);

            return result;
        }

        /// <summary>
        /// 16进制转其它进制数
        /// </summary>
        /// <param name="dstr">16进制字符串</param>
        /// <param name="toType">需要转换的类型</param>
        private string _16To(string dstr, DataType toType)
        {
            string result;
            int d16;
            try
            {
                d16 = Convert.ToInt32(dstr,16);
            }
            catch
            {
                result = "输入的16进制数不正确，请输入16进制数据";
                return result;
            }

            var d10Str = Convert.ToString(d16, 10);
            result = _10To(d10Str,toType);

            return result;
        }

        /// <summary>
        /// 32进制转其它进制数
        /// </summary>
        /// <param name="dstr">32进制字符串</param>
        /// <param name="toType">需要转换的类型</param>
        private string _32To(string dstr, DataType toType)
        {
            string result;
            int d32;
            try
            {
                d32 = Convert.ToInt32(dstr, 32);
            }
            catch
            {
                result = "输入的32进制数不正确，请输入32进制数据";
                return result;
            }

            var d10Str = Convert.ToString(d32, 10);
            result = _10To(d10Str, toType);

            return result;
        }

        /// <summary>
        /// 64进制转其它进制数
        /// </summary>
        /// <param name="dstr">64进制字符串</param>
        /// <param name="toType">需要转换的类型</param>
        private string _64To(string dstr, DataType toType)
        {
            string result;
            int d16;
            try
            {
                d16 = Convert.ToInt32(dstr, 64);
            }
            catch
            {
                result = "输入的64进制数不正确，请输入64进制数据";
                return result;
            }

            var d10Str = Convert.ToString(d16, 10);
            result = _10To(d10Str, toType);

            return result;
        }
    }

    /// <summary>
    /// 进制类型
    /// </summary>
    public enum DataType
    {
        _2 = 2,
        _8 = 8,
        _10 = 10,
        _16 = 16,
        _32 = 32,
        _64 = 64
    }
}
