using Client.Core;
using GEmojiSharp;
using System;
using System.Linq;
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
            var serversLookup = new MailSlotServersLookup();
            var joinChatDialog = new JoinChatDialog(serversLookup);
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
