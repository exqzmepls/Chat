using KdSoft.MailSlot;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common.NamedPipeServer
{
    public class MailSlotServer : IDataChannelServer
    {
        private readonly string _name;
        private FileStream _server;

        public MailSlotServer(string name)
        {
            _name = name;
        }

        public void Dispose()
        {
            _server?.Dispose();
        }

        public void Start(Action<string> onMessageAction)
        {
            Task.Run(() =>
            {
                var buffer = new byte[1024];
                _server = MailSlot.CreateServer(_name);
                try
                {
                    while (true)
                    {
                        var count = _server.Read(buffer, 0, buffer.Length);
                        var msg = Encoding.Unicode.GetString(buffer, 0, count);
                        onMessageAction(msg);
                    }
                }
                catch
                {
                    return;
                }
            });
        }
    }
}
