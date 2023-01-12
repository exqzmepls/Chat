using Client.Core;
using System.Linq;
using System.Windows.Forms;

namespace Client
{
    public partial class JoinChatDialog : Form
    {
        private readonly IServersLookup _serversLookup;

        public JoinChatDialog(IServersLookup serversLookup)
        {
            InitializeComponent();
            _serversLookup = serversLookup;
        }

        internal ConnectionInfo GetConnectionInfo()
        {
            var info = new ConnectionInfo(serverComboBox.Text, chatTextBox.Text, loginTextBox.Text);
            return info;
        }

        private void JoinChatDialog_Load(object sender, System.EventArgs e)
        {
            var servers =  _serversLookup.GetServers();
            serverComboBox.Items.AddRange(servers.ToArray());
            serverComboBox.SelectedIndex = 0;
        }
    }
}
