using Common.Dtos;
using Common.Servers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Server.Core
{
    internal class ChatService : IChatService
    {
        private readonly Dictionary<string, Chat> _chats = new Dictionary<string, Chat>();

        private readonly IServer _server;
        private readonly Logger _logger;

        public ChatService(string hostIp, int hostPort, Logger logger)
        {
            _server = new TcpSocketServer(hostIp, hostPort);
            _logger = logger;
        }

        public void AddChat(string name)
        {
            var newChat = new Chat(name, _logger);
            _chats.Add(name, newChat);
        }

        public void Start()
        {
            _server.Start((clientListener, clientWriter) =>
            {
                _logger.Log("New client connected.");

                clientListener.Start(message =>
                {
                    _logger.Log(message);

                    var deserializedRequestMessage = JsonConvert.DeserializeObject<RequestMessage>(message);
                    if (deserializedRequestMessage != null)
                    {
                        var chatName = deserializedRequestMessage.ChatName;
                        if (!_chats.ContainsKey(chatName))
                        {
                            _logger.Log($"{chatName} does not exist");
                            return;
                        }

                        var chat = _chats[chatName];
                        var sessionId = deserializedRequestMessage.SessionId;
                        switch (deserializedRequestMessage.RequestType)
                        {
                            // если присоединился к чату, то создаем сессию и добавляем сессию в чат (сессия == гуид + ответчик клиенту)
                            case RequestType.Join:
                                chat.AddClientSession(sessionId, deserializedRequestMessage.Login, clientWriter);
                                break;

                            // если вышел из чата, то удаляем сессию из чата
                            case RequestType.Quit:
                                chat.RemoveClientSession(sessionId);
                                break;
                        }
                        return;
                    }

                    var deserializedChatMessage = JsonConvert.DeserializeObject<ChatMessage>(message);
                    if (deserializedChatMessage != null)
                    {
                        var chatName = deserializedChatMessage.Chat;
                        if (!_chats.ContainsKey(chatName))
                        {
                            _logger.Log($"{chatName} does not exist");
                            return;
                        }
                        var chat = _chats[chatName];
                        var sessionId = deserializedChatMessage.SessionId;
                        chat.AddNewMessage(sessionId, deserializedChatMessage.Text);
                    }
                });
            });
        }

        public void Stop()
        {
            _server.Stop();
        }
    }
}
