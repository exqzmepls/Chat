using System;
using System.Text;
using System.Threading.Tasks;

namespace Common.NamedPipeServer
{
    public class NamedPipeServer : IDataChannelServer
    {
        private readonly string _pipePath;
        private int _pipeHandle;
        private Task _listenTask;

        public NamedPipeServer(string pipeName)
        {
            _pipePath = $"\\\\.\\pipe\\{pipeName}";
        }

        public void Dispose()
        {
            //_listenTask.Dispose();
            var closeResult = Import.CloseHandle(_pipeHandle);
        }

        public void Start(Action<string> onMessageAction)
        {
            _pipeHandle = Import.CreateNamedPipe(_pipePath, Types.PIPE_ACCESS_DUPLEX, Types.PIPE_TYPE_BYTE | Types.PIPE_WAIT, Types.PIPE_UNLIMITED_INSTANCES, 0, 1024, Types.NMPWAIT_WAIT_FOREVER, 0);

            _listenTask = Task.Run(() =>
            {
                while (true)
                {
                    var isMessageExists = Import.ConnectNamedPipe(_pipeHandle, 0);
                    if (isMessageExists)
                    {
                        byte[] buff = new byte[1024];
                        Import.FlushFileBuffers(_pipeHandle);
                        var realBytesReaded = 0u;
                        Import.ReadFile(_pipeHandle, buff, 1024, ref realBytesReaded, 0);
                        var message = Encoding.Unicode.GetString(buff, 0, (int)realBytesReaded);

                        onMessageAction(message);

                        Import.DisconnectNamedPipe(_pipeHandle);
                    }
                }
            });
        }
    }
}
