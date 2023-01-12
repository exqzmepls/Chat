using Common.Dtos;
using System;

namespace Client.Core
{
    internal class ChatClient : IChatClient
    {
        private readonly string _chatName;
        private readonly Action<ChatMessage> _sendMessageAction;

        public ChatClient(Guid sessionId, string chatName, Action<ChatMessage> sendMessageAction)
        {
            SessionId = sessionId;
            _chatName = chatName;
            _sendMessageAction = sendMessageAction;
        }

        public Guid SessionId { get; }

        public void SendMessage(string text)
        {
            var messageDto = new ChatMessage
            {
                SessionId = SessionId,
                Chat = _chatName,
                Text = text
            };
            _sendMessageAction(messageDto);
        }
    }
}
