using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Sunisoft.IrisSkin;

namespace View.Skins
{
    public partial class SkinForm : Form
    {
        readonly string path = Application.StartupPath;    //目录名 也可以用相当路径
        readonly Dictionary<string, string> _mPathList = new Dictionary<string, string>();//包含所有文件路径的数组
        //加载皮肤
        readonly SkinEngine skin;
        private string skinName;
        private string skinFile;

        public SkinForm(SkinEngine skin)
        {
            InitializeComponent();
            this.skin = skin;
            skinName = skin.SkinFile.Substring(skin.SkinFile.LastIndexOf('\\') + 1).Split('.').First();
            skinFile = skin.SkinFile;

            treeView1.ExpandAll();
        }

        private void SkinForm_Load(object sender, EventArgs e)
        {
            //查询所有皮肤文件
            ParseDirectory(path, "*.ssk");

            //初始化皮肤列表
            foreach (KeyValuePair<string, string> keyValuePair in _mPathList)
            {
                lbSkinNames.Items.Add(keyValuePair.Key);

                if (keyValuePair.Key == skinName)
                    lbSkinNames.SetSelected(lbSkinNames.FindStringExact(keyValuePair.Key), true);
            }

            if (!string.IsNullOrWhiteSpace(skinName))
            {
                txtSkinName.Text = skinFile;
            }
        }

        private void txtSkinName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSkinName.Text))
            {
                btnUseSkin.Enabled = false;
            }
            else
            {
                btnUseSkin.Enabled = true;
            }
        }

        private void btnUseSkin_Click(object sender, EventArgs e)
        {
            //保存默认主题配置
            ConfigHelper.ConfigParse.SaveAppSettings("DefaultTheme", lbSkinNames.SelectedItems[0].ToString());
            MessageBox.Show("保存成功");
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            //设置系统默认，即置空
            ConfigHelper.ConfigParse.SaveAppSettings("DefaultTheme", "");
            MessageBox.Show("保存成功，重启应用程序生效");
        }

        void ParseDirectory(string path, string filter)
        {
            string[] dirs = Directory.GetDirectories(path);//得到子目录
            IEnumerator iter = dirs.GetEnumerator();
            while (iter.MoveNext())
            {
                string str = (string)(iter.Current);
                ParseDirectory(str, filter);
            }
            string[] fs = Directory.GetFiles(path, filter);
            if (fs.Length > 0)
            {
                for (int i = 0; i < fs.Length; i++)
                {
                    var fileName = fs[i];
                    var sn = fileName.Substring(fileName.LastIndexOf('\\') + 1).Split('.').First();
                    _mPathList.Add(sn, fileName);
                }
            }
        }

        private void lbSkinNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            skinName = lbSkinNames.SelectedItems[0].ToString();
            skinFile = _mPathList[skinName];
            skin.SkinFile = skinFile;
            skin.Active = true;

            txtSkinName.Text = skinFile;
        }
    }
}
