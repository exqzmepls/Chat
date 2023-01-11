using Common.ClientListeners;
using Common.ClientWriters;
using System;

namespace Common.Servers
{
    public interface IServer
    {
        void Start(Action<IClientListener, IClientWriter> onAcceptedClient);

        void Stop();
    }
}
