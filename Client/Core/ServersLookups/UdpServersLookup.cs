using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Common.Dtos;
using Newtonsoft.Json;

namespace Client.Core.ServersLookups
{
    public class UdpServersLookup : IServersLookup
    {
        private readonly UdpClient _udpClient = new UdpClient(50370, AddressFamily.InterNetwork)
        {
            EnableBroadcast = true
        };

        public IEnumerable<ServerInfo> GetServers()
        {
            var servers = new HashSet<ServerInfo>();
            var endPoint = new IPEndPoint(IPAddress.Any, 50760);
            var brodcastAddress = IPAddress.Parse("235.5.5.11");
            _udpClient.JoinMulticastGroup(brodcastAddress);

            var timeout = TimeSpan.FromSeconds(2);
            var timer = Stopwatch.StartNew();
            while (timer.Elapsed < timeout)
            {
                var result = _udpClient.Receive(ref endPoint);
                var message = Encoding.Unicode.GetString(result);
                try
                {
                    var deserializedMessage = JsonConvert.DeserializeObject<PingResponse>(message);
                    var serverInfo = new ServerInfo
                    {
                        IP = deserializedMessage.IP,
                        Port = deserializedMessage.Port
                    };
                    servers.Add(serverInfo);
                }
                catch
                {
                    continue;
                }
            }
            _udpClient.DropMulticastGroup(brodcastAddress);
            return servers;
        }
    }
}
