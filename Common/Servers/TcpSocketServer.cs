using Common.ClientListeners;
using Common.ClientWriters;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Common.Servers
{
    public class TcpSocketServer : IServer
    {
        private readonly TcpListener _tcpListener;

        public TcpSocketServer(string hostIp, int port)
        {
            var ipAddress = IPAddress.Parse(hostIp);
            _tcpListener = new TcpListener(ipAddress, port);
        }

        public void Start(Action<IClientListener, IClientWriter> onAcceptedClient)
        {
            _tcpListener.Start();
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var acceptedTcpClient = _tcpListener.AcceptTcpClient();
                        var clientListener = new TcpSocketClientListener(acceptedTcpClient);
                        var clientWriter = new TcpSocketClientWriter(acceptedTcpClient);
                        onAcceptedClient(clientListener, clientWriter);
                    }
                    catch
                    {
                        return;
                    }
                }
            });
        }

        public void Stop()
        {
            _tcpListener.Stop();
        }
    }
}
