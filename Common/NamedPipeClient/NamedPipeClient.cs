using System;
using System.Text;

namespace Common.NamedPipeClient
{
    public class NamedPipeClient : INamedPipeClient
    {
        private readonly string _pipePath;

        public NamedPipeClient(string serverHostName, string pipeName)
        {
            _pipePath = $"\\\\{serverHostName}\\pipe\\{pipeName}";
        }

        public void PushMessage(string message)
        {
            uint bytesWritten = 0;
            byte[] buff = Encoding.Unicode.GetBytes(message);

            var pipeHandle = Import.CreateFile(_pipePath, Types.EFileAccess.GenericWrite, Types.EFileShare.Read, 0, Types.ECreationDisposition.OpenExisting, 0, 0);
            var writeResult = Import.WriteFile(pipeHandle, buff, Convert.ToUInt32(buff.Length), ref bytesWritten, 0);
            var closeResult = Import.CloseHandle(pipeHandle);
        }
    }
}
