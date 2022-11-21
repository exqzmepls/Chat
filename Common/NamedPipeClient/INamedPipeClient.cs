namespace Common.NamedPipeClient
{
    public interface INamedPipeClient
    {
        void PushMessage(string message);
    }
}
