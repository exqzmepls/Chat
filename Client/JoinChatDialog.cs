using Client.Core;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Client
{
    public partial class JoinChatDialog : Form
    {
        private readonly IServersLookup _serversLookup;
        private readonly IHostNameProvider _hostNameProvider;

        public JoinChatDialog(IServersLookup serversLookup, IHostNameProvider hostNameProvider)
        {
            InitializeComponent();
            _serversLookup = serversLookup;
            _hostNameProvider = hostNameProvider;
        }

        internal ConnectionInfo GetConnectionInfo()
        {
            var info = new ConnectionInfo(serverComboBox.Text, chatTextBox.Text, loginTextBox.Text);
            return info;
        }

        private void JoinChatDialog_Load(object sender, System.EventArgs e)
        {
            var servers = _serversLookup.GetServers();
            serverComboBox.Items.AddRange(servers.ToArray());
            serverComboBox.SelectedIndex = 0;
        }

        private void getHostNameButton_Click(object sender, System.EventArgs e)
        {
            var ip = serverComboBox.Text.Trim();
            var host = _hostNameProvider.GetHostName(ip);
            if (host == default)
            {
                MessageBox.Show("Hostname does not provided.");
                return;
            }

            serverComboBox.Text = host;
        }
    }
}
