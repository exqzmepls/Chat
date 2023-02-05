using Server.Core;
using System;
using System.Net;
using System.Windows.Forms;

namespace Server
{
    public partial class MainForm : Form
    {
        private readonly IChatService _chatService;

        internal MainForm(IChatService chatService)
        {
            InitializeComponent();

            _chatService = chatService;
        }

        public void LogMessage(string message)
        {
            var log = $"{message}{Environment.NewLine}";
            logsRichTextBox.AppendText(log);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var hostname = Dns.GetHostName();
            Text = hostname;

            _chatService.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _chatService.Stop();
        }

        private void createChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var createChatDialog = new CreateChatDialog();
            if (createChatDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var chatName = createChatDialog.GetChatName();
            _chatService.AddChat(chatName);
        }
    }
}
