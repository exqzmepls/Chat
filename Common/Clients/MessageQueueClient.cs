using System.Messaging;
using Common.Tools;

namespace Common.Clients
{
    public class MessageQueueClient : IClient
    {
        private readonly MessageQueue _queue;

        public MessageQueueClient(string hostName, string queueName)
        {
            var queuePath = PathProvider.GetMessageQueuePath(hostName, queueName);
            _queue = new MessageQueue(queuePath);
        }

        public void PushMessage(string message)
        {
            _queue.Send(message);
        }
    }
}