using Common.Dtos;
using Common.NamedPipeClient;
using Common.NamedPipeServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Server.Core
{
    internal class Chat : IDisposable
    {
        private readonly Dictionary<Guid, INamedPipeClient> _sessions = new Dictionary<Guid, INamedPipeClient>();
        private readonly INamedPipeServer _chatPipeServer;
        private readonly Logger _logger;

        public Chat(string name, Logger logger)
        {
            _chatPipeServer = new NamedPipeServer(name);
            _logger = logger;
        }

        public void Create()
        {
            _chatPipeServer.Start(SendUserNewMessage);
        }

        public void AddClientSession(Guid sessionId, INamedPipeClient sessionPipeClient)
        {
            _sessions[sessionId] = sessionPipeClient;
        }

        public void RemoveClientSession(Guid sessionId)
        {
            _sessions.Remove(sessionId);
        }

        public void Dispose()
        {
            _chatPipeServer.Dispose();
        }

        public void SendSystemMessage(string text)
        {
            var formattedMessage = $"* {text} *";
            SendMessage(formattedMessage);
        }

        private void SendUserNewMessage(string message)
        {
            _logger.Log(message);

            var deserializedMessage = JsonConvert.DeserializeObject<ChatMessage>(message);
            var formattedMessage = $"{deserializedMessage.SenderLogin} >> {deserializedMessage.Text}";
            SendMessage(formattedMessage);
        }

        private void SendMessage(string message)
        {
            foreach (var session in _sessions.Values)
            {
                session.PushMessage(message);
            }
        }
    }
}
