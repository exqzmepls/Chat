using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Dtos;
using Newtonsoft.Json;

namespace Server.Core
{
    internal class UdpPingSignal : IPingSignal
    {
        private readonly UdpClient _udpClient = new UdpClient(50760)
        {
            EnableBroadcast = true
        };
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public void Run(string serverIp, int port)
        {
            var brodcastAddress = IPAddress.Parse("235.5.5.11");
            var remoteEndPoint = new IPEndPoint(brodcastAddress, 50370);
            var spamDelayTimeSpan = TimeSpan.FromSeconds(0.5);
            var response = new PingResponse
            {
                IP = serverIp,
                Port = port
            };
            var serializedMessage = JsonConvert.SerializeObject(response);
            var buff = Encoding.Unicode.GetBytes(serializedMessage);

            var token = _cancellationTokenSource.Token;
            Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    _udpClient.Send(buff, buff.Length, remoteEndPoint);
                    Task.Delay(spamDelayTimeSpan).Wait();
                }
                Console.WriteLine("Canceled");
            }, token);
        }

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
        }
    }
}
