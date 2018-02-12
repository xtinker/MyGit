using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace View.Cmd_MarkDown
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public partial class MarkDownForm : Form
    {
        public MarkDownForm()
        {
            InitializeComponent();
        }
    }
}
