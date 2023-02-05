using System;

namespace Client.Core
{
    internal interface IChatClient
    {
        string GetInfo();

        void Join(Action<string> onMessageAction);

        void SendMessage(string text);

        void Quit();
    }
}