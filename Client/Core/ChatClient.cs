using Common.Dtos;
using Newtonsoft.Json;
using System;

namespace Client.Core
{
    internal class ChatClient : IChatClient
    {
        private readonly Guid _sessionId;
        private readonly string _chatName;
        private readonly Action<string> _sendMessageAction;

        public ChatClient(Guid sessionId, string chatName, Action<string> sendMessageAction)
        {
            _sessionId = sessionId;
            _chatName = chatName;
            _sendMessageAction = sendMessageAction;
        }

        public string GetInfo()
        {
            var info = ""; //$"{_connectionInfo.ServerHostName} : {_connectionInfo.ChatName} (as {_connectionInfo.Login})";
            return info;
        }

        public void SendMessage(string text)
        {
            var messageDto = new ChatMessage
            {
                SessionId = _sessionId,
                Chat = _chatName,
                Text = text
            };
            var serializedMessage = JsonConvert.SerializeObject(messageDto);
            _sendMessageAction(serializedMessage);
        }
    }
}
