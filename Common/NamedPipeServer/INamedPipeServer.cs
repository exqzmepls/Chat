using System;

namespace Common.NamedPipeServer
{
    public interface INamedPipeServer : IDisposable
    {
        void Start(Action<string> onMessageAction);
    }
}
