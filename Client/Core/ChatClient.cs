using Newtonsoft.Json;
using System;
using System.Net;
using Common.Clients;
using Common.Contracts;
using Common.Servers;

namespace Client.Core
{
    internal class ChatClient : IChatClient
    {
        private readonly Guid _sessionId = Guid.NewGuid();
        private readonly IClient _serverMainClient;
        private readonly IClient _chatClient;
        private readonly IServer _sessionServer;
        private readonly ConnectionInfo _connectionInfo;

        public ChatClient(ConnectionInfo connectionInfo)
        {
            _connectionInfo = connectionInfo;

            var serverHostName = connectionInfo.ServerHostName;
            _serverMainClient = new MessageQueueClient(serverHostName, "ServerMainQueue");
            _chatClient = new MessageQueueClient(serverHostName, _connectionInfo.ChatName);
            _sessionServer = new MessageQueueServer(_sessionId.ToString());
        }

        public string GetInfo()
        {
            var info = $"{_connectionInfo.ServerHostName} : {_connectionInfo.ChatName} (as {_connectionInfo.Login})";
            return info;
        }

        public void Join(Action<string> onMessageAction)
        {
            var joinRequest = new JoinRequestMessage
            {
                SessionId = _sessionId,
                ClientHostName = Dns.GetHostName(),
                ChatName = _connectionInfo.ChatName,
                Login = _connectionInfo.Login
            };

            _sessionServer.Start(onMessageAction);

            var joinRequestSerialized = JsonConvert.SerializeObject(joinRequest);
            _serverMainClient.PushMessage(joinRequestSerialized);
        }

        public void SendMessage(string text)
        {
            var messageDto = new ChatMessage
            {
                SenderLogin = _connectionInfo.Login,
                Text = text
            };
            var serializedMessage = JsonConvert.SerializeObject(messageDto);
            _chatClient.PushMessage(serializedMessage);
        }

        public void Quit()
        {
            var quitRequest = new QuitRequestMessage()
            {
                SessionId = _sessionId,
                ChatName = _connectionInfo.ChatName,
                Login = _connectionInfo.Login
            };

            var quitRequestSerialized = JsonConvert.SerializeObject(quitRequest);
            _serverMainClient.PushMessage(quitRequestSerialized);

            _sessionServer.Stop();
        }
    }
}