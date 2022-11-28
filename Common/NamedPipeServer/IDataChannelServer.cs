using System;

namespace Common.NamedPipeServer
{
    public interface IDataChannelServer : IDisposable
    {
        void Start(Action<string> onMessageAction);
    }
}
