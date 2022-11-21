using Common.Dtos;
using Common.NamedPipeClient;
using Common.NamedPipeServer;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Client.Core
{
    internal class ChatClient : IChatClient
    {
        private readonly Guid _sessionId = Guid.NewGuid();
        private readonly INamedPipeClient _serverMainPipeClient;
        private readonly INamedPipeClient _chatPipeClient;
        private readonly INamedPipeServer _sessionPipeServer;
        private readonly ConnectionInfo _connectionInfo;

        public ChatClient(ConnectionInfo connectionInfo)
        {
            _connectionInfo = connectionInfo;

            var serverHostName = connectionInfo.ServerHostName;
            _serverMainPipeClient = new NamedPipeClient(serverHostName, "ServerMainPipe");
            _chatPipeClient = new NamedPipeClient(serverHostName, _connectionInfo.ChatName);
            _sessionPipeServer = new NamedPipeServer(_sessionId.ToString());

        }

        public void Dispose()
        {
            var quitRequest = new RequestMessage
            {
                SessionId = _sessionId,
                RequestType = RequestType.Quit,
                ClientHostName = Dns.GetHostName(),
                ChatName = _connectionInfo.ChatName,
                Login = _connectionInfo.Login
            };

            var quitRequestSerialized = JsonConvert.SerializeObject(quitRequest);
            _serverMainPipeClient.PushMessage(quitRequestSerialized);

            _sessionPipeServer.Dispose();
        }

        public string GetInfo()
        {
            var info = $"{_connectionInfo.ServerHostName} : {_connectionInfo.ChatName} (as {_connectionInfo.Login})";
            return info;
        }

        public void Join(Action<string> onMessageAction)
        {
            var joinRequest = new RequestMessage
            {
                SessionId = _sessionId,
                RequestType = RequestType.Join,
                ClientHostName = Dns.GetHostName(),
                ChatName = _connectionInfo.ChatName,
                Login = _connectionInfo.Login
            };

            var joinRequestSerialized = JsonConvert.SerializeObject(joinRequest);
            _serverMainPipeClient.PushMessage(joinRequestSerialized);

            _sessionPipeServer.Start(onMessageAction);
        }

        public void SendMessage(string text)
        {
            var messageDto = new ChatMessage
            {
                SenderLogin = _connectionInfo.Login,
                Text = text
            };
            var serializedMessage = JsonConvert.SerializeObject(messageDto);
            _chatPipeClient.PushMessage(serializedMessage);
        }
    }
}
