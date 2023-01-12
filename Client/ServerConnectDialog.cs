using System;
using System.Linq;
using System.Windows.Forms;
using Client.Core.ServersLookups;

namespace Client
{
    public partial class ServerConnectDialog : Form
    {
        private readonly IServersLookup _lookup;

        public ServerConnectDialog(IServersLookup lookup)
        {
            InitializeComponent();
            _lookup = lookup;
        }

        public ServerInfo GetServer()
        {
            var server = serverComboBox.SelectedItem as ServerInfo;
            return server;
        }

        private void ServerConnectDialog_Load(object sender, EventArgs e)
        {
            var servers = _lookup.GetServers().ToArray();
            serverComboBox.Items.AddRange(servers);
            serverComboBox.SelectedIndex = 0;
        }
    }
}
