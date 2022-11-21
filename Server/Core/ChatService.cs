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
        private readonly INamedPipeServer _mainPipeServer;
        private readonly IReadOnlyDictionary<string, Chat> _chats;
        private readonly Logger _logger;

        public ChatService(Logger logger, params string[] chats)
        {
            _logger = logger;

            _mainPipeServer = new NamedPipeServer("ServerMainPipe");

            _chats = chats.ToDictionary(c => c, c => new Chat(c, _logger));
        }

        public void Dispose()
        {
            _mainPipeServer.Dispose();

            foreach (var chat in _chats.Values)
            {
                chat.Dispose();
            }
        }

        public void Start()
        {
            _mainPipeServer.Start(ProcessMessage);

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
                    var sessionPipeClient = new NamedPipeClient(deserializedMessage.ClientHostName, sessionId.ToString());
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
