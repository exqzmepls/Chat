using KdSoft.MailSlot;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Common.NamedPipeServer
{
    public class MailSlotServer : IDataChannelServer
    {
        private readonly string _name;
        private Task _listenTask;

        public MailSlotServer(string name)
        {
            _name = name;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Start(Action<string> onMessageAction)
        {
            _listenTask = Task.Run(() =>
            {
                AsyncMailSlotListener

                var buffer = new byte[1024];
                using (var server = MailSlot.CreateServer(_name))
                {
                    while (true)
                    {
                        var count = server.ReadAsync(buffer, 0, buffer.Length).Result;
                        var msg = Encoding.Unicode.GetString(buffer, 0, count);
                        onMessageAction(msg);
                    }
                }
            });
        }
    }
}
