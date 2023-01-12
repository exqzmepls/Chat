using System;

namespace Client.Core
{
    internal interface IChatClient
    {
        Guid SessionId { get; }

        void SendMessage(string text);
    }
}
