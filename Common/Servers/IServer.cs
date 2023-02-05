using System;

namespace Common.Servers
{
    public interface IServer
    {
        void Start(Action<string> onMessageAction);

        void Stop();
    }
}
