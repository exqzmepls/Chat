using KdSoft.MailSlot;
using System.Text;

namespace Common.Clients
{
    public class MailSlotClient : IClient
    {
        private readonly string _serverHostName;
        private readonly string _name;

        public MailSlotClient(string serverHostName, string mailSlotName)
        {
            _serverHostName = serverHostName;
            _name = mailSlotName;
        }

        public void PushMessage(string message)
        {
            using (var client = MailSlot.CreateClient(_name, _serverHostName))
            {
                var bytes = Encoding.Unicode.GetBytes(message);
                client.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
