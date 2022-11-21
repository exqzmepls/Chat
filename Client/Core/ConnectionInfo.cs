namespace Client.Core
{
    internal class ConnectionInfo
    {
        public ConnectionInfo(string serverHostName, string chatName, string login)
        {
            ServerHostName = serverHostName;
            ChatName = chatName;
            Login = login;
        }

        public string ServerHostName { get; }
        public string ChatName { get; }
        public string Login { get; }
    }
}
