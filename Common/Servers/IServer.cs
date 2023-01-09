using System;

namespace Common.Servers
{
    public interface IServer : IDisposable
    {
        void Start(Action<string> onMessageAction);
    }
}
