using System;
using Common.ClientWriters;

namespace Server.Core
{
    internal class MemberSession
    {
        private readonly IClientWriter _clientWriter;

        public MemberSession(Guid sessionId, string login, IClientWriter clientWriter)
        {
            Id = sessionId;
            Login = login;
            _clientWriter = clientWriter;
        }

        public Guid Id { get; }
        public string Login { get; }

        public void SendResponse(string message)
        {
            _clientWriter.SendMessage(message);
        }
    }
}
