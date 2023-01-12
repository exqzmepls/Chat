using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common.ClientListeners
{
    public class TcpSocketClientListener : IClientListener
    {
        private readonly TcpClient _tcpClient;

        public TcpSocketClientListener(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }

        public void Start(Action<string> onMessageAction)
        {
            var buff = new byte[1024];
            var networkStream = _tcpClient.GetStream();
            Task.Run(() =>
            {
                while (true)
                {
                    if (!_tcpClient.Connected)
                    {
                        return;
                    }
                    var readBytes = networkStream.Read(buff, 0, buff.Length);
                    if (readBytes == 0)
                    {
                        continue;
                    }

                    var message = Encoding.Unicode.GetString(buff, 0, readBytes);
                    onMessageAction(message);
                }
            });
        }

        public void Stop()
        {
            _tcpClient.Close();
        }
    }
}
