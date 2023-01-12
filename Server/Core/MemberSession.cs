using Common.ClientWriters;

namespace Server.Core
{
    internal class MemberSession
    {
        private readonly IClientWriter _clientWriter;

        public MemberSession(string login, IClientWriter clientWriter)
        {
            Login = login;
            _clientWriter = clientWriter;
        }

        public string Login { get; }

        public void SendResponse(string message)
        {
            _clientWriter.SendMessage(message);
        }
    }
}
