using Common.Clients;
using Common.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Client.Core
{
    internal class ChatClientService : IChatClientService
    {
        private readonly IDictionary<string, IDictionary<Guid, Action<string>>> _activeChats = new Dictionary<string, IDictionary<Guid, Action<string>>>();

        private readonly IClient _client;

        public ChatClientService(IClient client)
        {
            _client = client;
        }

        public void Connect()
        {
            _client.Connect(message =>
            {
                var messages = message.Split(new string[] { "---end-marker---" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var receivedMessage in messages)
                {
                    var deserializedMessage = JsonConvert.DeserializeObject<ChatNotification>(receivedMessage);

                    var isChatExist = _activeChats.TryGetValue(deserializedMessage.ChatName, out var membersSessions);
                    if (!isChatExist)
                    {
                        return;
                    }

                    var isSessionExist = membersSessions.TryGetValue(deserializedMessage.SessionId, out var callbackAction);
                    if (!isSessionExist)
                    {
                        return;
                    }
                    callbackAction(deserializedMessage.Message);
                }
            });
        }

        public void Disconnect()
        {
            _client.Disconnect();
        }

        public IChatClient JoinChat(string chatName, string login, Action<string> onMessageReceived)
        {
            var sessionId = Guid.NewGuid();
            var joinRequestMessage = new JoinRequest
            {
                SessionId = sessionId,
                ChatName = chatName,
                Login = login,
                ClientHostName = Dns.GetHostName(),
            };

            var isChatExist = _activeChats.ContainsKey(chatName);
            if (isChatExist)
            {
                _activeChats[chatName].Add(sessionId, onMessageReceived);
            }
            else
            {
                var newChatSessions = new Dictionary<Guid, Action<string>>
                {
                    {sessionId, onMessageReceived}
                };
                _activeChats.Add(chatName, newChatSessions);
            }

            PushMessage(joinRequestMessage);

            var chatClient = new ChatClient(sessionId, chatName, PushMessage);
            return chatClient;
        }

        public void QuitChat(string chatName, Guid sessionId)
        {
            var quitRequestMessage = new QuitRequest
            {
                SessionId = sessionId,
                ChatName = chatName,
            };
            PushMessage(quitRequestMessage);
        }

        private void PushMessage<T>(T messageObject)
        {
            var serializedMessage = JsonConvert.SerializeObject(messageObject);
            _client.PushMessage(serializedMessage);
        }
    }
}
