using Newtonsoft.Json;
using System.Collections.Generic;
using Common.Clients;
using Common.Contracts;
using Common.Servers;

namespace Server.Core
{
    internal class ChatService : IChatService
    {
        private readonly Dictionary<string, Chat> _chats = new Dictionary<string, Chat>();

        private readonly IServer _mainServer;
        private readonly Logger _logger;

        public ChatService(Logger logger)
        {
            _logger = logger;

            _mainServer = new MessageQueueServer("ServerMainQueue");
        }

        public void Stop()
        {
            _mainServer.Stop();

            foreach (var chat in _chats.Values)
            {
                chat.Terminate();
            }
        }

        public void AddChat(string name)
        {
            var newChat = new Chat(name, _logger);
            _chats.Add(name, newChat);
            newChat.Create();
        }

        public void Start()
        {
            _mainServer.Start(ProcessMessage);
        }

        private void ProcessMessage(string message)
        {
            _logger.Log(message);

            var deserializedJoinRequestMessage = DeserializeMessageOrDefault<JoinRequestMessage>(message);
            if (deserializedJoinRequestMessage != null)
            {
                var chatName = deserializedJoinRequestMessage.ChatName;
                if (!_chats.ContainsKey(chatName))
                {
                    _logger.Log($"{chatName} does not exist");
                    return;
                }

                var chat = _chats[chatName];
                var sessionId = deserializedJoinRequestMessage.SessionId;
                var sessionPipeClient =
                    new MessageQueueClient(deserializedJoinRequestMessage.ClientHostName, sessionId.ToString());
                chat.AddClientSession(sessionId, sessionPipeClient);
                chat.SendSystemMessage($"{deserializedJoinRequestMessage.Login} joined");
                return;
            }

            var deserializedQuitRequestMessage = DeserializeMessageOrDefault<QuitRequestMessage>(message);
            if (deserializedQuitRequestMessage != null)
            {
                var chatName = deserializedQuitRequestMessage.ChatName;
                if (!_chats.ContainsKey(chatName))
                {
                    _logger.Log($"{chatName} does not exist");
                    return;
                }

                var chat = _chats[chatName];
                var sessionId = deserializedQuitRequestMessage.SessionId;
                chat.RemoveClientSession(sessionId);
                chat.SendSystemMessage($"{deserializedQuitRequestMessage.Login} quited");
                return;
            }
        }

        private static T DeserializeMessageOrDefault<T>(string message)
        {
            try
            {
                var deserialized = JsonConvert.DeserializeObject<T>(message);
                return deserialized;
            }
            catch
            {
                return default;
            }
        }
    }
}