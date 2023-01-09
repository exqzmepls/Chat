using System;

namespace Common
{
    public class Types
    {
        [Flags]
        public enum EFileAccess : uint
        {
            GenericRead = 0x80000000,
            GenericWrite = 0x40000000,
            GenericExecute = 0x20000000,
            GenericAll = 0x10000000
        }
        [Flags]
        public enum EFileShare : uint
        {
            None = 0x00000000,
            Read = 0x00000001,
            Write = 0x00000002,
            Delete = 0x00000004
        }
        public enum ECreationDisposition : uint
        {
            New = 1,
            CreateAlways = 2,
            OpenExisting = 3,
            OpenAlways = 4,
            TruncateExisting = 5
        }

        public const uint PIPE_ACCESS_DUPLEX = 0x00000003;
        public const uint PIPE_TYPE_BYTE = 0x00000000;
        public const uint PIPE_TYPE_MESSAGE = 0x00000004;
        public const uint PIPE_WAIT = 0x00000000;
        public const uint PIPE_UNLIMITED_INSTANCES = 255;
        public const int NMPWAIT_WAIT_FOREVER = -1;
        public const uint PIPE_OPEN_MODE = 0x00000003;

        public const int MAILSLOT_WAIT_FOREVER = -1;
    }
}
