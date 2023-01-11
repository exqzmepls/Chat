using System;

namespace Common.Clients
{
    public interface IClient
    {
        void Connect(Action<string> onResponseReceived);
        void Disconnect();
        void PushMessage(string message);
    }
}
