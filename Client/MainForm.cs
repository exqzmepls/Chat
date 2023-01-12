using Client.Core;
using Client.Core.ServersLookups;
using Common.Clients;
using System;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm : Form
    {
        private readonly UdpServersLookup _serversLookup;

        private ChatClientService _chatClientService;
        private ServerInfo _serverInfo;

        public MainForm()
        {
            InitializeComponent();
            _serversLookup = new UdpServersLookup();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var serverConnectDialog = new ServerConnectDialog(_serversLookup);
            if (serverConnectDialog.ShowDialog() != DialogResult.OK)
            {
                Close();
                return;
            }

            _serverInfo = serverConnectDialog.GetServer();
            var client = new TcpSocketClient(_serverInfo.IP, _serverInfo.Port);
            _chatClientService = new ChatClientService(client);
            _chatClientService.Connect();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var child in MdiChildren)
            {
                child.Close();
            }

            _chatClientService?.Disconnect();
        }

        private void joinChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var joinChatDialog = new JoinChatDialog();
            var dialogResult = joinChatDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var chatName = joinChatDialog.GetChatName();
                var login = joinChatDialog.GetLogin();
                var chatForm = new ChatForm(_chatClientService, chatName, login, _serverInfo)
                {
                    MdiParent = this
                };
                chatForm.Show();
            }
        }
    }
}
