namespace Client.Core
{
    internal class ConnectionInfo
    {
        public ConnectionInfo(string serverHost, string serverPort, string chatName, string login)
        {
            ServerHost = serverHost;
            ServerPort = serverPort;
            ChatName = chatName;
            Login = login;
        }

        public string ServerHost { get; }
        public string ServerPort { get; }
        public string ChatName { get; }
        public string Login { get; }
    }
}
