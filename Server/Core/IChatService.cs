namespace Server.Core
{
    internal interface IChatService
    {
        void Start();

        void Stop();

        void AddChat(string name);
    }
}