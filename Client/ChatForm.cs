using Client.Core;
using GEmojiSharp;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Client
{
    public partial class ChatForm : Form
    {
        private readonly IChatClient _chatClient;

        internal ChatForm(IChatClient chatClient)
        {
            InitializeComponent();

            _chatClient = chatClient;

            Text = chatClient.GetInfo();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            _chatClient.Join(DisplayMessage);
            listView.Items.AddRange(Emoji.All.Select(em => new ListViewItem(em.Raw)).ToArray());
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _chatClient.Dispose();
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            var messageText = inputTextBox.Text.Trim();

            if (string.IsNullOrEmpty(messageText))
                return;

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
