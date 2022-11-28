using Client.Core;
using System;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void joinChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var serversLookup = new ArpServersLookup();
            var hostNameProvider = new HostNameProvider();
            var joinChatDialog = new JoinChatDialog(serversLookup, hostNameProvider);
            var dialogResult = joinChatDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var connectionInfo = joinChatDialog.GetConnectionInfo();
                var chatClient = new ChatClient(connectionInfo);
                var chatForm = new ChatForm(chatClient)
                {
                    MdiParent = this
                };
                chatForm.Show();
            }
        }
    }
}
