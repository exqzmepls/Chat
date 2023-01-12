using System;

namespace Common.Dtos
{
    public class ChatMessage
    {
        public Guid SessionId { get; set; }

        public string Chat { get; set; }

        public string Text { get; set; }
    }
}
