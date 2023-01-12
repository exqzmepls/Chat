using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common.Clients
{
    public class TcpSocketClient : IClient
    {
        private readonly TcpClient _tcpClient;
        private readonly string _serverHostname;
        private readonly int _serverPort;

        public TcpSocketClient(string serverHostname, int serverPort)
        {
            _tcpClient = new TcpClient();
            _serverHostname = serverHostname;
            _serverPort = serverPort;
        }

        public void Connect(Action<string> onDataReceived)
        {
            _tcpClient.Connect(_serverHostname, _serverPort);
            Task.Run(() => ListenResponses(onDataReceived));
        }

        public void Disconnect()
        {
            _tcpClient.Close();
        }

        public void PushMessage(string message)
        {
            var buff = Encoding.Unicode.GetBytes(message);
            var stream = _tcpClient.GetStream();
            stream.Write(buff, 0, buff.Length);
        }

        private void ListenResponses(Action<string> onDataReceived)
        {
            var buff = new byte[1024];
            var networkStream = _tcpClient.GetStream();
            while (true)
            {
                try
                {
                    var readBytes = networkStream.Read(buff, 0, buff.Length);
                    var data = Encoding.Unicode.GetString(buff, 0, readBytes);
                    onDataReceived(data);
                }
                catch
                {
                    return;
                }
            }
        }
    }
}
