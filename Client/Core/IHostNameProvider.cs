namespace Client.Core
{
    public interface IHostNameProvider
    {
        string GetHostName(string ip);
    }
}
