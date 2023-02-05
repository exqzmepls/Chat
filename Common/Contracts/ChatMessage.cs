using Newtonsoft.Json;

namespace Common.Contracts
{
    public class ChatMessage
    {
        [JsonProperty(Required = Required.Always)]
        public string Text { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string SenderLogin { get; set; }
    }
}
