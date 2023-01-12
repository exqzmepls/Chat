using Server.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class MainForm : Form
    {
        private readonly IPingSignal _pingSignal = new UdpPingSignal();

        private IChatService _chatService;

        internal MainForm()
        {
            InitializeComponent();
        }

        public void LogMessage(string message)
        {
            var log = $"{message}{Environment.NewLine}";
            logsRichTextBox.AppendText(log);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var createChatDialog = new SelectHostPointDialog();
            if (createChatDialog.ShowDialog() != DialogResult.OK)
            {
                Close();
                return;
            }

            var hostname = createChatDialog.GetIpAddress();
            var port = createChatDialog.GetPort();

            Text = hostname;

            var logger = new Logger();
            logger.SetLogAction((m) =>
            {
                Invoke((MethodInvoker)delegate
                {
                    LogMessage(m);
                });
            });
            _chatService = new ChatService(hostname, port, logger);
            _chatService.Start();

            _pingSignal.Run(hostname, port);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _pingSignal.Cancel();
            _chatService?.Stop();
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
