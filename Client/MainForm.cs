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
            var joinChatDialog = new JoinChatDialog();
            var dialogResult = joinChatDialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;

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