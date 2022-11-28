namespace Common.NamedPipeClient
{
    public interface IDataChannelClient
    {
        void PushMessage(string message);
    }
}
