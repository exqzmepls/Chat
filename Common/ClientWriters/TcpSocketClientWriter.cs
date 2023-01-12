using System.Net.Sockets;
using System.Text;

namespace Common.ClientWriters
{
    public class TcpSocketClientWriter : IClientWriter
    {
        private readonly TcpClient _tcpClient;

        public TcpSocketClientWriter(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }

        public void SendMessage(string message)
        {
            var networkStream = _tcpClient.GetStream();
            var buff = Encoding.Unicode.GetBytes(message + "---end-marker---");
            networkStream.Write(buff, 0, buff.Length);
        }
    }
}
