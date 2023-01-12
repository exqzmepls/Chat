using Common.Dtos;
using Common.Servers;
using Newtonsoft.Json;
using System;
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

                    var deserializedJoinRequestMessage = DeserializeMessageOrDefault<JoinRequest>(message);
                    if (deserializedJoinRequestMessage != null)
                    {
                        // если присоединился к чату, то создаем сессию и добавляем сессию в чат (сессия == гуид + ответчик клиенту)
                        var chatName = deserializedJoinRequestMessage.ChatName;
                        if (!_chats.ContainsKey(chatName))
                        {
                            _logger.Log($"{chatName} does not exist");
                            return;
                        }

                        var chat = _chats[chatName];
                        var sessionId = deserializedJoinRequestMessage.SessionId;
                        chat.AddClientSession(sessionId, deserializedJoinRequestMessage.Login, clientWriter);
                        return;
                    }

                    var deserializedQuitRequestMessage = DeserializeMessageOrDefault<QuitRequest>(message);
                    if (deserializedQuitRequestMessage != null)
                    {
                        // если вышел из чата, то удаляем сессию из чата
                        var chatName = deserializedQuitRequestMessage.ChatName;
                        if (!_chats.ContainsKey(chatName))
                        {
                            _logger.Log($"{chatName} does not exist");
                            return;
                        }

                        var chat = _chats[chatName];
                        var sessionId = deserializedQuitRequestMessage.SessionId;
                        chat.RemoveClientSession(sessionId);
                        return;
                    }

                    var deserializedChatMessage = DeserializeMessageOrDefault<ChatMessage>(message);
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
