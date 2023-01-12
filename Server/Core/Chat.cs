using System;
using System.Collections.Generic;
using Common.ClientWriters;
using Common.Dtos;
using Newtonsoft.Json;

namespace Server.Core
{
    internal class Chat
    {
        private readonly IDictionary<Guid, MemberSession> _sessions = new Dictionary<Guid, MemberSession>();

        private readonly string _name;
        private readonly Logger _logger;

        public Chat(string name, Logger logger)
        {
            _name = name;
            _logger = logger;
        }

        public void AddClientSession(Guid sessionId, string login, IClientWriter clientWriter)
        {
            _logger.Log($"{login} added to chat (session id = {sessionId})");
            _sessions[sessionId] = new MemberSession(login, clientWriter);
            SendSystemMessage($"{login} joined");
        }

        public void RemoveClientSession(Guid sessionId)
        {
            var isSessionExist = _sessions.TryGetValue(sessionId, out var session);
            if (!isSessionExist)
            {
                _logger.Log("No such session in chat.");
                return;
            }

            _sessions.Remove(sessionId);
            SendSystemMessage($"{session.Login} quitted");
            _logger.Log($"{session.Login} removed from chat (session id = {sessionId})");
        }

        public void AddNewMessage(Guid sessionId, string messageText)
        {
            var isSessionExist = _sessions.TryGetValue(sessionId, out var session);

            if (!isSessionExist)
            {
                _logger.Log("Sender session does not exit.");
                return;
            }

            _logger.Log("Sending message to all chat members...");
            SendUserNewMessage(session.Login, messageText);
        }

        private void SendSystemMessage(string text)
        {
            var formattedMessage = $"* {text} *";
            SendMessage(formattedMessage);
        }

        private void SendUserNewMessage(string senderLogin, string messageText)
        {
            var formattedMessage = $"{senderLogin} >> {messageText}";
            SendMessage(formattedMessage);
        }

        private void SendMessage(string message)
        {
            var chatNotification = new ChatNotification
            {
                ChatName = _name,
                Message = message
            };
            var serializedMessage = JsonConvert.SerializeObject(chatNotification);
            foreach (var session in _sessions.Values)
            {
                session.SendResponse(serializedMessage);
            }
        }
    }
}
