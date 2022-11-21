using Client.Core;
using System;
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

        private void DisplayMessage(string message)
        {
            var formattedMessage = $"{message}{Environment.NewLine}";
            messagesRichTextBox.Invoke((MethodInvoker)delegate
            {
                messagesRichTextBox.AppendText(formattedMessage);
            });
        }
    }
}
