using Client.Core;
using Client.Core.ServersLookups;
using GEmojiSharp;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Client
{
    public partial class ChatForm : Form
    {
        private readonly IChatClientService _chatClientService;
        private readonly string _chatName;
        private readonly string _login;
        private readonly ServerInfo _serverInfo;

        private IChatClient _chatClient;

        internal ChatForm(IChatClientService chatClientService, string chatName, string login, ServerInfo serverInfo)
        {
            InitializeComponent();

            _chatClientService = chatClientService;
            _chatName = chatName;
            _login = login;
            _serverInfo = serverInfo;
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            _chatClient = _chatClientService.JoinChat(_chatName, _login, DisplayMessage);

            Text = $"server -> {_serverInfo}; chat -> {_chatName}; login -> {_login}";
            listView.Items.AddRange(Emoji.All.Select(em => new ListViewItem(em.Raw)).ToArray());
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _chatClientService.QuitChat(_chatName, _chatClient.SessionId);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            var messageText = inputTextBox.Text.Trim();

            if (string.IsNullOrEmpty(messageText))
            {
                return;
            }

            _chatClient.SendMessage(messageText);

            inputTextBox.Clear();
        }

        private void listView_ItemActivate(object sender, EventArgs e)
        {
            var selected = listView.SelectedItems[0];
            inputTextBox.AppendText(selected.Text);
        }

        private void DisplayMessage(string message)
        {
            var formattedMessage = $"{message}{Environment.NewLine}";
            messagesTextBox.Invoke((MethodInvoker)delegate
            {
                messagesTextBox.AppendText(formattedMessage);
            });
        }
    }
}
