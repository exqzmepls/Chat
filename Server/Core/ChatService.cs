using Common.Dtos;
using Common.NamedPipeClient;
using Common.NamedPipeServer;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Server.Core
{
    internal class ChatService : IChatService
    {
        private readonly Dictionary<string, Chat> _chats = new Dictionary<string, Chat>();

        private readonly IDataChannelServer _mainDataChannelServer;
        private readonly Logger _logger;

        public ChatService(Logger logger)
        {
            _logger = logger;

            _mainDataChannelServer = new MailSlotServer("ServerMainPipe"); //new NamedPipeServer("ServerMainPipe");
        }

        public void AddChat(string name)
        {
            var newChat = new Chat(name, _logger);
            _chats.Add(name, newChat);
            newChat.Create();
        }

        public void Dispose()
        {
            _mainDataChannelServer.Dispose();

            foreach (var chat in _chats.Values)
            {
                chat.Dispose();
            }
        }

        public void Start()
        {
            _mainDataChannelServer.Start(ProcessMessage);
        }

        private void ProcessMessage(string message)
        {
            _logger.Log(message);

            var deserializedMessage = JsonConvert.DeserializeObject<RequestMessage>(message);


            switch (deserializedMessage.RequestType)
            {
                case RequestType.Ping:
                    var clientHostName = deserializedMessage.ClientHostName;
                    var responseWriter = new MailSlotClient(clientHostName, "Ping");
                    responseWriter.PushMessage(Dns.GetHostName());
                    break;

                case RequestType.Join:
                    {
                        var chatInstance = _chats[deserializedMessage.ChatName];
                        var sessionId = deserializedMessage.SessionId;
                        var sessionPipeClient = new MailSlotClient(deserializedMessage.ClientHostName, sessionId.ToString()); // new NamedPipeClient(deserializedMessage.ClientHostName, sessionId.ToString());
                        chatInstance.AddClientSession(sessionId, sessionPipeClient);
                        chatInstance.SendSystemMessage($"{deserializedMessage.Login} joined");
                        break;
                    }


                case RequestType.Quit:
                    {
                        var chatInstance = _chats[deserializedMessage.ChatName];
                        var sessionId = deserializedMessage.SessionId;
                        chatInstance.RemoveClientSession(sessionId);
                        chatInstance.SendSystemMessage($"{deserializedMessage.Login} quitted");
                        break;
                    }
            }
        }
    }
}
