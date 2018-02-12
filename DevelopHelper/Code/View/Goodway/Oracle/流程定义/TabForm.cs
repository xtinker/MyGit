using System;
using System.Reflection;
using System.Windows.Forms;

namespace View.Goodway.Oracle
{
    public partial class TabForm : Form
    {
        public int[] FormFlag = { 0, 0, 0, 0, 0, 0, 0 };

        public TabForm()
        {
            InitializeComponent();
        }

        private void TabForm_Load(object sender, EventArgs e)
        {
            BuilderForm("View.Goodway.Oracle.FlowScriptForm", tabControl1);
        }

        private void BuilderForm(string formTag, object sender)
        {
            Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(formTag);
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.TopLevel = false;
            fm.Parent = ((TabControl)sender).SelectedTab;
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();

            FormFlag[((TabControl)sender).SelectedIndex] = 1;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormFlag[tabControl1.SelectedIndex] == 0)    //只生成一次  
            {
                tabPage1_Click(sender, e);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            var formTag = string.Format("View.Goodway.Oracle.{0}", ((TabControl)sender).SelectedTab.Tag);
            BuilderForm(formTag, sender);
        }
    }
}
