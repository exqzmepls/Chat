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
        private readonly IDictionary<string, IList<Action<string>>> _activeChats = new Dictionary<string, IList<Action<string>>>();

        private readonly IClient _client;

        public ChatClientService(IClient client)
        {
            _client = client;
        }

        public void Connect()
        {
            _client.Connect(message =>
            {
                var deserializedMessage = JsonConvert.DeserializeObject<ChatNotification>(message);

                var isChatExist = _activeChats.TryGetValue(deserializedMessage.ChatName, out var membersCallbacks);
                if (!isChatExist)
                    return;

                foreach (var callback in membersCallbacks)
                {
                    callback(deserializedMessage.Message);
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
            var joinRequestMessage = new RequestMessage
            {
                SessionId = sessionId,
                ChatName = chatName,
                Login = login,
                ClientHostName = Dns.GetHostName(),
                RequestType = RequestType.Join
            };

            var isChatExist = _activeChats.ContainsKey(chatName);
            if (isChatExist)
            {
                _activeChats[chatName].Add(onMessageReceived);
            }
            else
            {
                _activeChats.Add(chatName, new List<Action<string>> { onMessageReceived });
            }

            PushMessage(joinRequestMessage);

            var chatClient = new ChatClient(sessionId, chatName, PushMessage);
            return chatClient;
        }

        public void QuitChat(string chatName, Guid sessionId)
        {
            var quitRequestMessage = new RequestMessage
            {
                SessionId = sessionId,
                ChatName = chatName,
                ClientHostName = Dns.GetHostName(),
                RequestType = RequestType.Quit
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
