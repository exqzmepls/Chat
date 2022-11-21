using System;

namespace Server.Core
{
    internal interface IChatService : IDisposable
    {
        void Start();
    }
}
