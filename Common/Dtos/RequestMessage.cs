using System;

namespace Common.Dtos
{
    public class RequestMessage
    {
        public RequestType RequestType { get; set; }

        public Guid SessionId { get; set; }

        public string ClientHostName { get; set; }

        public string ChatName { get; set; }

        public string Login { get; set; }
    }
}
