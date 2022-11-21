using System;

namespace Client.Core
{
    internal interface IChatClient : IDisposable
    {
        string GetInfo();

        void Join(Action<string> onMessageAction);

        void SendMessage(string text);
    }
}
