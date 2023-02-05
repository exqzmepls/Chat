using System;
using System.Messaging;
using System.Threading.Tasks;
using Common.Tools;

namespace Common.Servers
{
    public class MessageQueueServer : IServer
    {
        private readonly string _queuePath;

        public MessageQueueServer(string queueName)
        {
            _queuePath = PathProvider.GetMessageQueueLocalPath(queueName);
        }

        public void Start(Action<string> onMessageAction)
        {
            Task.Run(() =>
            {
                var queue = MessageQueue.Create(_queuePath);
                queue.Formatter = new XmlMessageFormatter(new[] { typeof(string) });

                while (true)
                {
                    try
                    {
                        var message = queue.Receive();
                        var messageBody = message?.Body;

                        onMessageAction(messageBody?.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return;
                    }
                }
            });
        }

        public void Stop()
        {
            MessageQueue.Delete(_queuePath);
        }
    }
}