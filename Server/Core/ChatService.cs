using Common.Dtos;
using Common.NamedPipeClient;
using Common.NamedPipeServer;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Server.Core
{
    internal class ChatService : IChatService
    {
        private readonly IDataChannelServer _mainDataChannelServer;
        private readonly IReadOnlyDictionary<string, Chat> _chats;
        private readonly Logger _logger;

        public ChatService(Logger logger, params string[] chats)
        {
            _logger = logger;

            _mainDataChannelServer = new MailSlotServer("ServerMainPipe"); //new NamedPipeServer("ServerMainPipe");

            _chats = chats.ToDictionary(c => c, c => new Chat(c, _logger));
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

            foreach (var chat in _chats.Values)
            {
                chat.Create();
            }
        }

        private void ProcessMessage(string message)
        {
            _logger.Log(message);

            var deserializedMessage = JsonConvert.DeserializeObject<RequestMessage>(message);
            var chatInstance = _chats[deserializedMessage.ChatName];
            var sessionId = deserializedMessage.SessionId;

            switch (deserializedMessage.RequestType)
            {
                case RequestType.Join:
                    var sessionPipeClient = new MailSlotClient(deserializedMessage.ClientHostName, sessionId.ToString()); // new NamedPipeClient(deserializedMessage.ClientHostName, sessionId.ToString());
                    chatInstance.AddClientSession(sessionId, sessionPipeClient);
                    chatInstance.SendSystemMessage($"{deserializedMessage.Login} joined");
                    break;

                case RequestType.Quit:
                    chatInstance.RemoveClientSession(sessionId);
                    chatInstance.SendSystemMessage($"{deserializedMessage.Login} quitted");
                    break;
            }
        }
    }
}
