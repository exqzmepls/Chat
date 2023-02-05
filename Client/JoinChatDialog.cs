using Client.Core;
using System.Windows.Forms;

namespace Client
{
    public partial class JoinChatDialog : Form
    {
        public JoinChatDialog()
        {
            InitializeComponent();
        }

        internal ConnectionInfo GetConnectionInfo()
        {
            var info = new ConnectionInfo(serverTextBox.Text, chatTextBox.Text, loginTextBox.Text);
            return info;
        }
    }
}