using Common.Dtos;
using Common.Clients;
using Common.Servers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Client.Core
{
    internal class MailSlotServersLookup : IServersLookup
    {
        private readonly IClient _pingChannelClient = new MailSlotClient("*", "ServerMainPipe");
        private readonly IServer _dataChannelServer = new MailSlotServer("Ping");

        public IEnumerable<string> GetServers()
        {
            var servers = new List<string>();
            _dataChannelServer.Start(s => servers.Add(s));
            var request = new RequestMessage
            {
                RequestType = RequestType.Ping,
                ClientHostName = Dns.GetHostName()
            };
            var serialized = JsonConvert.SerializeObject(request);
            _pingChannelClient.PushMessage(serialized);
            Thread.Sleep(1000);
            _dataChannelServer.Dispose();
            return servers;
        }
    }
}
