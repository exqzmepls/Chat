using Common.Dtos;
using Common.Clients;
using Common.Servers;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Client.Core
{
    internal class ChatClient : IChatClient
    {
        private readonly Guid _sessionId = Guid.NewGuid();
        private readonly IClient _serverMainDataChannelClient;
        private readonly IClient _chatDataChannelClient;
        private readonly IServer _sessionDataChannelServer;
        private readonly ConnectionInfo _connectionInfo;

        public ChatClient(ConnectionInfo connectionInfo)
        {
            _connectionInfo = connectionInfo;

            var serverHostName = connectionInfo.ServerHostName;
            _serverMainDataChannelClient = new MailSlotClient(serverHostName, "ServerMainPipe"); //new NamedPipeClient(serverHostName, "ServerMainPipe");
            _chatDataChannelClient = new MailSlotClient(serverHostName, _connectionInfo.ChatName); //new NamedPipeClient(serverHostName, _connectionInfo.ChatName);
            _sessionDataChannelServer = new MailSlotServer(_sessionId.ToString());  // new NamedPipeServer(_sessionId.ToString());
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
            _serverMainDataChannelClient.PushMessage(quitRequestSerialized);

            _sessionDataChannelServer.Dispose();
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

            _sessionDataChannelServer.Start(onMessageAction);

            var joinRequestSerialized = JsonConvert.SerializeObject(joinRequest);
            _serverMainDataChannelClient.PushMessage(joinRequestSerialized);
        }

        public void SendMessage(string text)
        {
            var messageDto = new ChatMessage
            {
                SenderLogin = _connectionInfo.Login,
                Text = text
            };
            var serializedMessage = JsonConvert.SerializeObject(messageDto);
            _chatDataChannelClient.PushMessage(serializedMessage);
        }
    }
}
