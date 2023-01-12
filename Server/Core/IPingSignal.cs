namespace Server.Core
{
    internal interface IPingSignal
    {
        void Run(string serverIp, int port);

        void Cancel();
    }
}
