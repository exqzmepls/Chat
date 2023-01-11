using System;

namespace Common.ClientListeners
{
    public interface IClientListener
    {
        void Start(Action<string> onMessageAction);

        void Stop();
    }
}
