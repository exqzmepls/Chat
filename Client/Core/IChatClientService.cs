using System;

namespace Client.Core
{
    internal interface IChatClientService
    {
        void Connect();

        void Disconnect();

        IChatClient JoinChat(string chatName, string login, Action<string> onMessageReceived);

        void QuitChat(string chatName, Guid sessionId);
    }
}
